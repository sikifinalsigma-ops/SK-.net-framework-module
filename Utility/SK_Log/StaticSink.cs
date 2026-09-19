using Serilog;
using System;
using System.IO;

namespace SaveLog
{
    public class StaticSink
    {
        public static void CreateSink()
        {
            string logPath = Path.Combine(AppContext.BaseDirectory, "Log", "SaveLogs", "infolog-.txt");
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().Enrich.FromLogContext().WriteTo.Async(a => a.File(logPath, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}{NewLine}")).CreateLogger();
        }

        public static void SaveLog(string log) 
        {
            Log.Logger.Information(log);
        }

        public static void SaveLog(object log)
        {
            Log.Logger.Information("record log {@Log}",log);
        }

        public static void SaveLog<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        public static void SaveException(Exception exception, string messageTemplate)
        {
            Log.Logger.Error(exception,messageTemplate);
        }

        public static void SaveException(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Logger.Error(exception, messageTemplate, propertyValues);
            
        }

        public static void CloseSink() 
        {            
            Log.CloseAndFlush();
        }

    }
}
