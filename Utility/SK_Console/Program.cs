using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using SaveLog;
namespace SK_Console
{
    class Program
    {
        static
            //async Task
            void 
            Main(string[] args)
        {

            //FileLogHelper.Info("测速。");
            try
            {
                //using (Ping pi = new Ping()) 
                //{
                //    for (int i = 0; i < 256; i++) 
                //    {
                //        string aa = "192.168.20." + i;
                //        if (pi.Send(aa, 2000).Status == IPStatus.Success)
                //            Console.WriteLine(aa);
                //    }
                //}
                // 默认本地 Redis
                //Console.WriteLine(Class1.test());
                //TestRedis.test();
                //TestServer.test();

                //TestExcel.test();
                //TestTool.test();
                //TestEntity.test();
                TestEntity.generate();
                //TestEncrypt.Test();
                //await TestProtocol.test();
                //await TestPrinter.test();

                //FileLogHelper.Info("testconsole");
            }
            catch (Exception e)
            {
                FileLogHelper.Info(e.Message + " " + e.StackTrace);

            }
            Console.ReadKey();
        }
    }
}
