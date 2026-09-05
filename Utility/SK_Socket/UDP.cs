using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SK_Socket
{
    public class UDP : IDisposable
    {
        private UdpClient _udpClient;
        private CancellationTokenSource _cts;
        private bool _isRunning;

        /// <summary>
        /// 收到数据时的事件回调 (数据字节数组, 发送方终点)
        /// </summary>
        public event Action<byte[], IPEndPoint> OnDataReceived;

        /// <summary>
        /// 发生异常时的事件回调
        /// </summary>
        public event Action<Exception> OnError;

        public bool IsRunning => _isRunning;

        /// <summary>
        /// 启动 UDP 监听服务
        /// </summary>
        /// <param name="localPort">本地监听端口</param>
        public void Start(int localPort)
        {
            if (_isRunning) 
                return;

            _udpClient = new UdpClient(localPort);
            _cts = new CancellationTokenSource();
            _isRunning = true;

            // 启动后台监听任务
            Task.Run(() => ListenAsync(_cts.Token));
        }

        private async Task ListenAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _isRunning)
            {
                try
                {
                    // 异步接收数据，不会阻塞主线程
                    UdpReceiveResult result = await _udpClient.ReceiveAsync();
                    OnDataReceived?.Invoke(result.Buffer, result.RemoteEndPoint);
                }
                catch (ObjectDisposedException)
                {
                    // UdpClient 已关闭，正常退出循环
                    break;
                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                    {
                        OnError?.Invoke(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 异步发送字节数据到指定目标
        /// </summary>
        public async Task SendAsync(byte[] data, string remoteIp, int remotePort)
        {
            if (!_isRunning || _udpClient == null)
                throw new InvalidOperationException("UDP 模块未启动。");

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            await _udpClient.SendAsync(data, data.Length, remoteEP);
        }

        /// <summary>
        /// 异步发送字符串数据到指定目标
        /// </summary>
        public async Task SendStringAsync(string message, string remoteIp, int remotePort, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(message);
            await SendAsync(bytes, remoteIp, remotePort);
        }

        /// <summary>
        /// 停止 UDP 通信并释放资源
        /// </summary>
        public void Stop()
        {
            if (!_isRunning) 
                return;

            _isRunning = false;
            _cts?.Cancel();
            _udpClient?.Close();
            _udpClient = null;
        }

        public void Dispose()
        {
            Stop();
            _cts?.Dispose();
        }

    }
}
