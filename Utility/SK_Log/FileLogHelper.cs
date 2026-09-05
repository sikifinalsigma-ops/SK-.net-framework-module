using Serilog;
using Serilog.Events;
using Serilog.Context;
using System;
using System.IO;

namespace SaveLog
{
    public static class FileLogHelper
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// 初始化（可手动调用，也可自动触发）
        /// </summary>
        public static void Init()
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized)
                    return;

                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string logPath = Path.Combine(baseDir, "FileLog", "log-.txt");

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    // Console（开发用）
                    //.WriteTo.Console()
                    .WriteTo.Async(a => a.File(
                        path: logPath,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 7,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{TraceId}] {Message}{NewLine}{Exception}"
                    ))
                    .CreateLogger();

                _initialized = true;
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        public static void Info(string message, object data = null)
        {
            EnsureInit();
            Write(LogEventLevel.Information, message, null, data);
        }

        /// <summary>
        /// Debug
        /// </summary>
        public static void Debug(string message, object data = null)
        {
            EnsureInit();
            Write(LogEventLevel.Debug, message, null, data);
        }

        /// <summary>
        /// Warn
        /// </summary>
        public static void Warn(string message, object data = null)
        {
            EnsureInit();
            Write(LogEventLevel.Warning, message, null, data);
        }

        /// <summary>
        /// Error
        /// </summary>
        public static void Error(Exception ex, string message = "", object data = null)
        {
            EnsureInit();
            Write(LogEventLevel.Error, message, ex, data);
        }

        /// <summary>
        /// Fatal
        /// </summary>
        public static void Fatal(Exception ex, string message = "", object data = null)
        {
            EnsureInit();
            Write(LogEventLevel.Fatal, message, ex, data);
        }

        /// <summary>
        /// 核心写入
        /// </summary>
        private static void Write(LogEventLevel level, string message, Exception ex, object data)
        {
            var traceId = GetTraceId();

            using (LogContext.PushProperty("TraceId", traceId))
            {
                if (ex != null)
                {
                    if (data != null)
                        Log.Write(level, ex, "{Message} {@Data}", message, data);
                    else
                        Log.Write(level, ex, "{Message}", message);
                }
                else
                {
                    if (data != null)
                        Log.Write(level, "{Message} {@Data}", message, data);
                    else
                        Log.Write(level, "{Message}", message);
                }
            }
        }

        /// <summary>
        /// 确保已初始化（懒加载）
        /// </summary>
        private static void EnsureInit()
        {
            if (!_initialized)
            {
                Init();
            }
        }

        /// <summary>
        /// TraceId（跨场景通用）
        /// </summary>
        private static string GetTraceId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 程序结束时调用（可选）
        /// </summary>
        public static void Close()
        {
            Log.CloseAndFlush();
        }
    }
}

