using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace SK_Protocol
{
    /// <summary>
    /// 业务层统一 QoS 级别（不依赖任何第三方 MQTT 库）
    /// </summary>
    public enum MessageQoS
    {
        AtMostOnce = 0,   // 最多一次 (QoS 0)
        AtLeastOnce = 1,  // 至少一次 (QoS 1)
        ExactlyOnce = 2   // 仅且只有一次 (QoS 2)
    }

    /// <summary>
    /// 业务层通用 MQTT 消息包
    /// </summary>
    public class MqttMessageReceivedEventArgs : EventArgs
    {
        public string Topic { get; }
        public string Payload { get; }
        public byte[] RawPayload { get; }

        public MqttMessageReceivedEventArgs(string topic, string payload, byte[] rawPayload)
        {
            Topic = topic;
            Payload = payload;
            RawPayload = rawPayload;
        }
    }

    public class Mqtt4ClientService : IDisposable
    {
        


        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;
        private readonly string _topic = "factory/device/sensor_01";
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isDisposed;

        // 对外暴露的异步消息接收事件
        public event Func<MqttMessageReceivedEventArgs, Task> MessageReceivedAsync;

        public bool IsConnected => _mqttClient?.IsConnected ?? false;

        public Mqtt4ClientService(string host = "broker.emqx.io", int port = 1883)
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            // 1. 构建连接选项
            _options = new MqttClientOptionsBuilder()
                .WithTcpServer(host, port)
                .WithClientId($"Client_{Guid.NewGuid():N}")
                .WithCleanSession()
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
                // .WithCredentials("user", "pass")
                // .WithTls()
                .Build();

            // 2. 绑定事件
            ConfigureEventHandlers();
        }

        private void ConfigureEventHandlers()
        {
            // 消息接收回调
            _mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                var topic = e.ApplicationMessage.Topic;
                var segment = e.ApplicationMessage.PayloadSegment;

                // 零额外拷贝解析 UTF-8
                string payload = string.Empty;
                byte[] rawBytes = Array.Empty<byte>();

                if (segment.Array != null && segment.Count > 0)
                {
                    payload = Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
                    rawBytes = segment.ToArray(); // 如需保留原始字节
                }

                var eventArgs = new MqttMessageReceivedEventArgs(topic, payload, rawBytes);

                // 触发外部业务委托
                if (MessageReceivedAsync != null)
                {
                    try
                    {
                        // 异步并发/串行触发所有外部注册的回调
                        var handlers = MessageReceivedAsync.GetInvocationList();
                        foreach (Func<MqttMessageReceivedEventArgs, Task> handler in handlers)
                        {
                            await handler(eventArgs);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Error] 业务消息处理异常: {ex.Message}");
                    }
                }

                Console.WriteLine($"[Received] 主题: {topic} | 内容: {payload}");
                return ;
            };

            // 连接成功回调：自动重新订阅主题
            _mqttClient.ConnectedAsync += async e =>
            {
                Console.WriteLine("[Status] 已成功连接至 Broker");

                var topicFilter = new MqttTopicFilterBuilder()
                    .WithTopic(_topic)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();

                await _mqttClient.SubscribeAsync(topicFilter, _cts.Token);
                Console.WriteLine($"[Subscribed] 已订阅主题: {_topic}");
            };

            // 断开连接回调：持续重连循环
            _mqttClient.DisconnectedAsync += async e =>
            {
                Console.WriteLine($"[Status] 连接断开: {e.Reason}");

                if (_isDisposed || _cts.IsCancellationRequested)
                    return;

                // 异步启动重连循环，防止单次重连失败导致永久断连
                _ = Task.Run(async () =>
                {
                    while (!_mqttClient.IsConnected && !_cts.IsCancellationRequested)
                    {
                        try
                        {
                            Console.WriteLine("[Status] 5 秒后尝试重新连接...");
                            await Task.Delay(TimeSpan.FromSeconds(5), _cts.Token);

                            if (_cts.IsCancellationRequested) break;

                            Console.WriteLine("[Status] 正在连接...");
                            await _mqttClient.ConnectAsync(_options, _cts.Token);
                            break; // 连接成功则退出重连循环
                        }
                        catch (OperationCanceledException)
                        {
                            break; // 正常停机取消
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[Error] 重连失败: {ex.Message}");
                        }
                    }
                }, _cts.Token);

                await Task.CompletedTask;
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, cancellationToken))
            {
                Console.WriteLine("[Status] 正在启动客户端...");
                await _mqttClient.ConnectAsync(_options, linkedCts.Token);
            }            
        }

        public async Task PublishAsync(string topic, string payload, MessageQoS qos = MessageQoS.AtLeastOnce)
        {
            if (!_mqttClient.IsConnected)
            {
                Console.WriteLine("[Warn] 客户端未连接，无法发布消息");
                return;
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(Encoding.UTF8.GetBytes(payload))
                .WithQualityOfServiceLevel(MapToMqttQoS(qos))
                .WithRetainFlag(false)
                .Build();

            await _mqttClient.PublishAsync(message, _cts.Token);
            Console.WriteLine($"[Published] 主题: {topic} | 内容: {payload}");
        }

        public async Task StopAsync()
        {
            _cts.Cancel();

            if (_mqttClient.IsConnected)
            {
                await _mqttClient.DisconnectAsync();
                Console.WriteLine("[Status] 客户端已断开连接");
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            _cts.Cancel();
            _cts.Dispose();
            _mqttClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        // 枚举转换辅助方法（由于数值完全对齐，强转或模式匹配均可）
        private static MqttQualityOfServiceLevel MapToMqttQoS(MessageQoS qos) 
        {
            switch (qos)
            {
                case MessageQoS.AtMostOnce: 
                    return MqttQualityOfServiceLevel.AtMostOnce;
                case MessageQoS.AtLeastOnce:
                    return MqttQualityOfServiceLevel.AtLeastOnce;
                case MessageQoS.ExactlyOnce:
                    return MqttQualityOfServiceLevel.ExactlyOnce;
                default:
                    return MqttQualityOfServiceLevel.AtLeastOnce;
            }
        }  
    }
}