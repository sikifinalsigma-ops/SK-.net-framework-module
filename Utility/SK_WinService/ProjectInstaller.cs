using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SK_WinService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            // 服务运行账户
            serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;

            // 服务名称：用于 net start / net stop
            serviceInstaller1.ServiceName = "SK_WinService";

            // 显示名称：services.msc 里看到的名称
            serviceInstaller1.DisplayName = "SK Windows Service";

            // 服务描述
            serviceInstaller1.Description = "SK 后台 Windows 服务程序";

            // 启动方式
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
