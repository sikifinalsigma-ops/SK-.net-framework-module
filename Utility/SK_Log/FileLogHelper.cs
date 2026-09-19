using Serilog;
using Serilog.Events;
using Serilog.Context;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SaveLog
{
    public static class FileLogHelper
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();
        private static ILogger logger;
        private static int _isClosed = 0;
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

                logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    // Console（开发用）
                    //.WriteTo.Console()
                    .WriteTo.Async(a => a.File(
                        path: logPath,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 7,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{TraceId}] {Message:lj}{NewLine}{Exception}"
                    ))
                    .CreateLogger();
                AppDomain.CurrentDomain.ProcessExit += (s, e) => Close();
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
                        logger.Write(level, ex, "{LogMessage} {@Data}", message, data);
                    else
                        logger.Write(level, ex, "{LogMessage}", message);
                }
                else
                {
                    if (data != null)
                        logger.Write(level, "{LogMessage} {@Data}", message, data);
                    else
                        logger.Write(level, "{LogMessage}", message);
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
            // 兼容 .NET 运行时原生 Activity (W3C TraceContext)
            // 如果是在 ASP.NET Core 或启用了 OpenTelemetry 的环境中，可以直接拿到请求链的 TraceId
            var currentTraceId = Activity.Current?.TraceId.ToString();
            if (!string.IsNullOrEmpty(currentTraceId))
            {
                return currentTraceId;
            }

            // 纯单机离线任务场景：如果无调用链，使用 "-" 保持占位对齐，避免日志中充满无效的独立 GUID
            return "-";
        }

        /// <summary>
        /// 程序退出前务必调用，刷出异步缓冲区并释放资源
        /// </summary>
        public static void Close()
        {
            // 防止 ProcessExit 与手动调用发生并发冲突
            if (Interlocked.Exchange(ref _isClosed, 1) == 0)
            {
                (logger as IDisposable)?.Dispose();
            }
        }
    }
}

