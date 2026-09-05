using SaveLog;
using SK_Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SK_WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "SK_WinService";
        }

        protected override void OnStart(string[] args)
        {
            FileLogHelper.Info("Windows 服务启动");
            QuartzManager.Instance.InitializeAsync().Wait();
            QuartzManager.Instance.StartAsync().Wait();
            QuartzManager.Instance.addSimplejob("job1", "jg1", "tr1", "trg1", 5*60, new Dictionary<string, object>() { { "Type", "SK_WinService.TestJob,SK_WinService" }, { "Method", "testInsert" } }).Wait();

            //QuartzManager.Instance.addSimplejob("job1", "jg1", "tr1", "trg1", 5*60, new Dictionary<string, object>() { { "Type", "SK_WinService.YTjob,SK_WinService" }, { "Method", "TransSaleReturn" } }).Wait();
            FileLogHelper.Info("Quartz 调度器启动成功");
        }

        public void TestStart()
        {
            OnStart(null);
        }

        public void TestStop()
        {
            OnStop();
        }

        protected override void OnStop()
        {
            try
            {
                FileLogHelper.Info("服务停止开始");

                QuartzManager.Instance.Dispose();

                FileLogHelper.Info("Quartz 资源释放完成");
            }
            catch (Exception ex)
            {
                FileLogHelper.Error(ex, "服务停止异常");
            }
            finally
            {
                FileLogHelper.Close();
            }
         }
    }
}
