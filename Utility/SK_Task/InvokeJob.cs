using System;
using Quartz;
using System.Threading.Tasks;

namespace SK_Task
{
    [DisallowConcurrentExecution]
    public class InvokeJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            
            var data = context.MergedJobDataMap;

            string typeName = data.GetString("Type");
            string methodName = data.GetString("Method");

            var type = Type.GetType(typeName);
            var instance = Activator.CreateInstance(type);

            var method = type.GetMethod(methodName);

            var result = method.Invoke(instance, null);

            if (result is Task task)
            {
                await task;
            }
        }
    }
}
