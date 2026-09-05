using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SK_Task
{
    
    [DisallowConcurrentExecution]
    public class DemoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var map = context.MergedJobDataMap;
            string message = map.ContainsKey("Message") ? map.GetString("Message") : "默认消息";

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] DemoJob 执行，参数：{message}");
            return Task.CompletedTask;
        }
    }
}
