using SaveLog;
using StackExchange.Redis;
using System;
using System.Configuration;

namespace SK_Redis
{
    public static class RedisManager
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
            new Lazy<ConnectionMultiplexer>(() =>
            {
                var connStr = ConfigurationManager.AppSettings["RedisConnection"];

                if (string.IsNullOrWhiteSpace(connStr))
                {
                    throw new InvalidOperationException("AppSettings 中未配置 'RedisConnection' 连接字符串。");
                }

                var config = ConfigurationOptions.Parse(connStr);
                config.AbortOnConnectFail = false; // 首次连接失败时不缓存失败状态，允许后台自动重试
                config.ConnectTimeout = 5000;      // 连接超时时间 (ms)
                config.SyncTimeout = 5000;         // 同步读写超时时间 (ms)
                config.ConnectRetry = 3;           // 连接重试次数

                //var conn = ConnectionMultiplexer.Connect(connStr);
                var conn = ConnectionMultiplexer.Connect(config);
                conn.ConnectionFailed += (sender, args) =>
                {
                    FileLogHelper.Info("Redis 连接失败: " + args.Exception?.Message);
                };

                conn.ConnectionRestored += (sender, args) =>
                {
                    FileLogHelper.Info("Redis 连接恢复");
                };

                conn.ErrorMessage += (sender, args) =>
                {
                    FileLogHelper.Info($"Redis 服务器错误: {args.Message}");
                };
                return conn;
            });

        public static ConnectionMultiplexer Instance
        {
            get { return LazyConnection.Value; }
        }

        public static IDatabase GetDatabase(int db = 0)
        {
            return Instance.GetDatabase(db);
        }

        public static IServer GetServer(string host, int port)
        {
            return Instance.GetServer(host, port);
        }

    }
}
