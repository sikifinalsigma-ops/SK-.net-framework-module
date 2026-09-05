using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SK_WinService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 注册服务语句 ： cmd>sc create SKService binPath= "D:\Service\SK_WinService.exe"
        /// 删除服务语句 ： sc delete "SKService"
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                // 控制台调试模式
                Service1 service = new Service1();

                service.TestStart();

                Console.WriteLine("服务正在运行，按任意键退出...");
                //Console.ReadKey();

                service.TestStop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
