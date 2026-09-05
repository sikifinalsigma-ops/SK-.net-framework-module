using System;
using Yitter.IdGenerator;

namespace SK_Tool
{
    /// <summary>
    /// 基于 Yitter 算法的雪花 ID 生成工具类
    /// </summary>
    public static class SnowflakeIdUtil
    {
        private static bool _isInitialized = false;
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 初始化雪花算法生成器（建议在 Program.cs 启动时调用一次）
        /// </summary>
        /// <param name="workerId">机器码/节点ID (默认支持 0 ~ 63)</param>
        /// <param name="baseTime">基准时间（设为项目创建或上线时间，有助于减少 ID 位数）</param>
        /// <param name="workerIdBitLength">机器码位长（默认 6，支持最多 64 台机器）</param>
        /// <param name="seqBitLength">序列数位长（默认 6，每毫秒最多生成 64 个 ID）</param>
        public static void Initialize()
        {
            if (_isInitialized) return;

            lock (_lockObj)
            {
                if (_isInitialized) return;

                var options = new IdGeneratorOptions(1)
                {
                    WorkerIdBitLength = 6,
                    SeqBitLength = 6,
                    BaseTime = new DateTime(2026, 8, 3, 0, 0, 0, DateTimeKind.Utc)
                };

                // 设置全局 ID 生成器
                YitIdHelper.SetIdGenerator(options);
                _isInitialized = true;
            }
        }
        /// <summary>
        /// 获取下一个 64 位整型雪花 ID
        /// </summary>
        /// <returns>15~16位长的整型 ID</returns>
        /// <exception cref="InvalidOperationException">未初始化时抛出异常</exception>
        public static long NextId()
        {
            Initialize();
            return YitIdHelper.NextId();
        }
    }
}
