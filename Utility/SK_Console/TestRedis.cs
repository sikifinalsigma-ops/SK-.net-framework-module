using SK_Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Console
{
    public class TestRedis
    {
        public static void test() 
        {
            var cache = new RedisCacheHelper();
            string lockKey = "lock:update:order:1001";

            // 尝试获取锁，expiry 设置为 5 秒
            if (cache.TryAcquireLock(lockKey, TimeSpan.FromSeconds(5), out IDisposable lockHandle))
            {
                using (lockHandle)
                {

                    cache.SetString("user:id","name2345",TimeSpan.FromDays(1));
                    // 临界区：在这里执行并发安全的业务逻辑
                    // 如：查询 Oracle -> 修改 -> 更新缓存
                    Console.WriteLine("成功获取锁，正在执行业务...");
                }
                // 代码执行到这里（离开 using 作用域），会自动调用 Dispose() 释放 Redis 锁
            }
            else
            {
                Console.WriteLine("锁已被占用，稍后重试。");
            }

        }

    }
}
