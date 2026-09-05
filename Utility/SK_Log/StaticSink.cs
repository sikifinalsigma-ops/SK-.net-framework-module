using Serilog;
using System;

namespace SaveLog
{
    public class StaticSink
    {
        public static void CreateSink()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().Enrich.FromLogContext().WriteTo.Async(a => a.File(AppContext.BaseDirectory + "Log\\SaveLogs\\" + DateTime.Now.ToString("yyyyMMdd") + "\\infolog-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}{NewLine}")).CreateLogger();
        }

        public static void SaveLog(string log) 
        {
            Log.Logger.Information(log,Environment.NewLine);
        }

        public static void SaveLog(object log)
        {
            Log.Logger.Information("record log {@Log}",log, Environment.NewLine);
        }

        public static void SaveLog<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Log.Logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2,Environment.NewLine);
        }

        public static void SaveException(Exception exception, string messageTemplate)
        {
            Log.Logger.Error(exception,messageTemplate, Environment.NewLine);
        }

        public static void SaveException(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Log.Logger.Error(exception, messageTemplate, propertyValues, Environment.NewLine);
            
        }

        public static void CloseSink() 
        {            
            Log.CloseAndFlush();
        }

    }
}
