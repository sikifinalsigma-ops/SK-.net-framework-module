using SK_Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Console
{
    public class TestProtocol
    {
        public static async Task test() 
        {
            Console.WriteLine("===== MQTT 客户端测试启动 =====");

            // 1. 初始化服务（默认连接 broker.emqx.io:1883）
            using (var mqttService = new Mqtt4ClientService()) 
            {
                try
                {
                    // 2. 启动连接并订阅主题 factory/device/sensor_01
                    await mqttService.StartAsync();

                    // 等待连接与订阅完成
                    await Task.Delay(2000);

                    // 3. 循环发送 5 条测试数据
                    string testTopic = "factory/device/sensor_01";
                    for (int i = 1; i <= 5; i++)
                    {
                        string jsonPayload = $"{{\"deviceId\":\"sensor_01\",\"temperature\":{20 + i},\"timestamp\":\"{DateTime.UtcNow:O}\"}}";

                        Console.WriteLine($"\n[Action] 正在发送第 {i} 条消息...");
                        await mqttService.PublishAsync(testTopic, jsonPayload,MessageQoS.AtLeastOnce);

                        await Task.Delay(1500);
                    }

                    Console.WriteLine("\n[Info] 测试消息发送完成，按回车键退出并断开连接...");
                    Console.ReadLine();

                    // 4. 优雅停止
                    await mqttService.StopAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Fatal] 发生异常: {ex.Message}");
                }
            }

                
        }
    }
}
