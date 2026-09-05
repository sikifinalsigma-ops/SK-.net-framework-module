using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SK_Socket
{
    public class TcpClientUtil : IDisposable
    {
        private readonly string _host;
        private readonly int _port;
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private CancellationTokenSource _cts;
        private bool _disposed = false;

        #region 事件与回调定义
        /// <summary> 收到消息时的回调事件 </summary>
        public event Action<string> OnMessageReceived;

        /// <summary> 连接成功时的回调事件 </summary>
        public event Action OnConnected;

        /// <summary> 连接断开时的回调事件 </summary>
        public event Action OnDisconnected;

        /// <summary> 发生异常时的回调事件 </summary>
        public event Action<Exception> OnError;
        #endregion

        /// <summary>
        /// 当前是否已连接
        /// </summary>
        public bool IsConnected => _tcpClient != null && _tcpClient.Connected;

        public TcpClientUtil(string host, int port)
        {
            _host = host;
            _port = port;
        }

        /// <summary>
        /// 异步连接到服务器并启动后台接收监听
        /// </summary>
        public async Task ConnectAsync()
        {
            if (IsConnected) return;

            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(_host, _port);
                _stream = _tcpClient.GetStream();

                _cts = new CancellationTokenSource();

                // 触发连接成功回调
                OnConnected?.Invoke();

                // 启动后台线程异步监听接收数据
                _ = Task.Run(() => StartReceiveLoopAsync(_cts.Token));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
                Close();
            }
        }

        /// <summary>
        /// 后台循环接收消息逻辑
        /// </summary>
        private async Task StartReceiveLoopAsync(CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[4096];

            try
            {
                while (!cancellationToken.IsCancellationRequested && IsConnected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                    // 当 ReadAsync 返回 0 时，说明对端（服务端）已优雅断开连接
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    // 解析收到的消息（UTF-8 编码）
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // 触发接收消息回调
                    OnMessageReceived?.Invoke(message);
                }
            }
            catch (OperationCanceledException)
            {
                // 属于正常的取消操作，不视作异常
            }
            catch (Exception ex)
            {
                if (!_disposed)
                {
                    OnError?.Invoke(ex);
                }
            }
            finally
            {
                // 退出循环代表连接已中断
                if (!_disposed)
                {
                    OnDisconnected?.Invoke();
                    Close();
                }
            }
        }

        /// <summary>
        /// 异步发送字符串消息
        /// </summary>
        public async Task SendMessageAsync(string message)
        {
            if (!IsConnected || _stream == null)
            {
                throw new InvalidOperationException("未建立连接，无法发送消息。");
            }

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await _stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
                throw;
            }
        }

        /// <summary>
        /// 主动关闭连接并释放资源
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        #region 标准 IDisposable 模式（释放托管和非托管资源）
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // 告知垃圾回收器，非托管资源已手动清理，无需再执行析构函数
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // 1. 取消后台接收任务
                _cts?.Cancel();
                _cts?.Dispose();

                // 2. 释放托管资源（Stream 与 Client）
                _stream?.Dispose();

#if NET45_OR_GREATER || NETCOREAPP
                _tcpClient?.Dispose();
#else
                _tcpClient?.Close();
#endif
            }

            // 3. 释放非托管资源（如果有显式句柄可在此清理；TcpClient.Close 本身内部已关闭非托管 Socket 句柄）

            _disposed = true;
        }

        // 析构函数（终结器）：防止调用者忘记手写 Dispose()
        ~TcpClientUtil()
        {
            Dispose(false);
        }
        #endregion






    }

    public class TcpListenerUtil : IDisposable
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private TcpListener _listener;
        private CancellationTokenSource _cts;
        private bool _isListening = false;
        private bool _disposed = false;

        // 保存所有在线客户端的字典 (Key: 客户端远程终结点信息, Value: TcpClient)
        private readonly ConcurrentDictionary<string, TcpClient> _connectedClients
            = new ConcurrentDictionary<string, TcpClient>();

        #region 事件与回调
        /// <summary> 当有新客户端连接时触发 (参数: 客户端标识字符串) </summary>
        public event Action<string, TcpClient> OnClientConnected;

        /// <summary> 当有客户端断开连接时触发 (参数: 客户端标识字符串) </summary>
        public event Action<string, TcpClient> OnClientDisconnected;

        /// <summary> 当收到客户端发送的消息时触发 (参数1: 客户端标识, 参数2: 消息内容) </summary>
        public event Action<string, TcpClient, string> OnMessageReceived;

        /// <summary> 当发生异常时触发 </summary>
        public event Action<Exception> OnError;
        #endregion

        /// <summary> 服务端是否处于监听状态 </summary>
        public bool IsListening => _isListening;

        /// <summary> 当前在线客户端数量 </summary>
        public int ClientCount => _connectedClients.Count;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="ipAddress">监听 IP（默认 IPAddress.Any）</param>
        public TcpListenerUtil(int port, IPAddress ipAddress = null)
        {
            _port = port;
            _ipAddress = ipAddress ?? IPAddress.Any;
        }

        /// <summary>
        /// 启动 TCP 监听
        /// </summary>
        public void Start()
        {
            if (_isListening) 
                return;

            try
            {
                _listener = new TcpListener(_ipAddress, _port);
                _listener.Start();
                _isListening = true;
                _cts = new CancellationTokenSource();

                // 后台启动客户端连接监听循环
                _ = Task.Run(() => AcceptClientsAsync(_cts.Token));
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
                Stop();
            }
        }

        /// <summary>
        /// 异步循环等待并接收客户端连接
        /// </summary>
        private async Task AcceptClientsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _isListening)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    string clientKey = client.Client.RemoteEndPoint.ToString();

                    // 保存客户端记录
                    _connectedClients.TryAdd(clientKey, client);

                    // 触发客户端连接回调
                    OnClientConnected?.Invoke(clientKey,client);

                    // 为该客户端单独开启一个异步任务处理其通信
                    _ = Task.Run(() => HandleClientCommunicationAsync(client, clientKey, token));
                }
                catch (ObjectDisposedException)
                {
                    // Listener 被关闭时引发，属于正常的停止逻辑
                    break;
                }
                catch (Exception ex)
                {
                    if (_isListening)
                    {
                        OnError?.Invoke(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 单个客户端的数据收发逻辑
        /// </summary>
        private async Task HandleClientCommunicationAsync(TcpClient client, string clientKey, CancellationToken token)
        {
            byte[] buffer = new byte[4096];

            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (!token.IsCancellationRequested && client.Connected)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                        // 读到 0 字节，表示客户端主动断开了连接 (FIN)
                        if (bytesRead == 0)
                        {
                            break;
                        }

                        // UTF-8 解码消息
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // 触发消息接收回调
                        OnMessageReceived?.Invoke(clientKey, client, message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 取消令牌生效，正常退出
            }
            catch (Exception ex)
            {
                if (!_disposed)
                {
                    OnError?.Invoke(ex);
                }
            }
            finally
            {
                // 清理断开连接的客户端
                RemoveClient(clientKey);
                OnClientDisconnected?.Invoke(clientKey, client);
            }
        }

        /// <summary>
        /// 向指定客户端发送消息
        /// </summary>
        public async Task SendToClientAsync(string clientKey, string message)
        {
            if (_connectedClients.TryGetValue(clientKey, out TcpClient client) && client.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
            }
            else
            {
                throw new InvalidOperationException($"目标客户端 {clientKey} 不存在或连接已关闭。");
            }
        }

        /// <summary>
        /// 向所有已连接的客户端广播发送消息
        /// </summary>
        public async Task BroadcastAsync(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            foreach (var kvp in _connectedClients)
            {
                try
                {
                    if (kvp.Value.Connected)
                    {
                        NetworkStream stream = kvp.Value.GetStream();
                        await stream.WriteAsync(data, 0, data.Length);
                    }
                }
                catch
                {
                    // 广播时单个客户端失败可忽略或记录日志，避免影响全局广播
                }
            }
        }

        /// <summary>
        /// 断开指定客户端连接
        /// </summary>
        private void RemoveClient(string clientKey)
        {
            if (_connectedClients.TryRemove(clientKey, out TcpClient client))
            {
                try
                {
#if NET45_OR_GREATER || NETCOREAPP
                    client?.Dispose();
#else
                    client?.Close();
#endif
                }
                catch { }
            }
        }

        /// <summary>
        /// 停止服务端监听并关闭所有客户端连接
        /// </summary>
        public void Stop()
        {
            Dispose();
        }

        #region IDisposable 模式实现
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _isListening = false;

                // 1. 发送取消信号
                _cts?.Cancel();

                // 2. 停止 TcpListener
                try
                {
                    _listener?.Stop();
                }
                catch { }

                // 3. 断开所有客户端并释放资源
                foreach (var key in _connectedClients.Keys)
                {
                    RemoveClient(key);
                }

                _cts?.Dispose();
            }

            _disposed = true;
        }

        ~TcpListenerUtil()
        {
            Dispose(false);
        }
        #endregion
    }
}

