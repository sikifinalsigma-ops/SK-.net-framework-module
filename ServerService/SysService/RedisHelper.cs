using SK_Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerService.SysService
{
    public static class RedisHelper
    {
        public static string GetValue(string key)
        {
            RedisCacheHelper cache = Helper.RedisHelper();
            return cache.GetString(key);
        }

        public static bool SetValue(string lockKey,string key,string keyValue, int expiry = 1)
        {
            RedisCacheHelper cache = Helper.RedisHelper();
            if (cache.TryAcquireLock(lockKey, TimeSpan.FromSeconds(5), out IDisposable lockHandle))
            {
                using (lockHandle)
                {
                    // 缓存用户实体（建议脱敏，避免存入密码哈希）
                    cache.SetString($"{key}", keyValue, TimeSpan.FromDays(expiry));
                }
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static void RemoveValue(string key)
        {
            RedisCacheHelper cache = Helper.RedisHelper();
            cache.Remove($"{key}");
        }

    }
}
