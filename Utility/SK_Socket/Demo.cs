using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Socket
{
    public class Demo
    {
        public static async Task UdpTest() 
        {
            using (var udp = new UDP())
            {
                // 绑定接收事件
                udp.OnDataReceived += (data, remoteEP) =>
                {
                    string msg = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"收到来自 [{remoteEP}] 的消息: {msg}");
                };

                // 绑定异常事件
                udp.OnError += (ex) =>
                {
                    Console.WriteLine($"UDP 发生错误: {ex.Message}");
                };

                // 启动模块，监听 8080 端口
                udp.Start(8080);
                Console.WriteLine("UDP 模块已启动，监听端口 8080...");

                // 发送测试消息（发送给自己）
                await udp.SendStringAsync("Hello .NET Framework UDP!", "127.0.0.1", 8080);

                Console.WriteLine("按任意键停止...");
                Console.ReadKey();

                udp.Stop();
                Console.WriteLine("UDP 模块已关闭。");
            }
        }
    }
}
