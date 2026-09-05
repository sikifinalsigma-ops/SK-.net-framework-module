using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace SK_Redis
{
    public class RedisCacheHelper
    {
        private readonly IDatabase _db;

        public RedisCacheHelper(int db = 0)
        {
            _db = RedisManager.GetDatabase(db);
        }

        public bool SetString(string key, string value, TimeSpan? expiry = null)
        {
            return _db.StringSet(key, value, expiry.Value);
        }

        public string GetString(string key)
        {

            return _db.StringGet(key);

        }

        public bool Set<T>(string key, T value, TimeSpan? expiry = null)
        {

            if (value == null) 
                return false;
            string json = JsonConvert.SerializeObject(value);
            return _db.StringSet(key, json, expiry.Value);

        }

        public T Get<T>(string key)
        {

            var value = _db.StringGet(key);
            if (!value.HasValue)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);

        }

        public bool Remove(string key)
        {

            return _db.KeyDelete(key);

        }

        public bool Exists(string key)
        {

            return _db.KeyExists(key);

        }

        public bool Expire(string key, TimeSpan expiry)
        {

            return _db.KeyExpire(key, expiry);

        }

        #region String / Object 异步接口

        public Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return _db.StringSetAsync(key, value, expiry.Value);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _db.StringGetAsync(key).ConfigureAwait(false);
        }

        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (value == null) return Task.FromResult(false);
            string json = JsonConvert.SerializeObject(value);
            return _db.StringSetAsync(key, json, expiry.Value);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            RedisValue value = await _db.StringGetAsync(key).ConfigureAwait(false);
            if (!value.HasValue) return default;
            return JsonConvert.DeserializeObject<T>(value);
        }

        public Task<bool> RemoveAsync(string key) => _db.KeyDeleteAsync(key);

        public Task<bool> ExistsAsync(string key) => _db.KeyExistsAsync(key);

        #endregion


        #region 分布式锁基础 API

        /// <summary>
        /// 获取分布式锁
        /// </summary>
        /// <param name="key">锁的 Key</param>
        /// <param name="token">锁的唯一标识（通常使用 Guid.NewGuid().ToString()），用于释放锁时校验身份</param>
        /// <param name="expiry">锁的过期时间（防止死锁）</param>
        /// <returns>是否成功加锁</returns>
        public bool LockTake(string key, string token, TimeSpan expiry)
        {
            return _db.LockTake(key, token, expiry);
        }

        /// <summary>
        /// 释放分布式锁
        /// </summary>
        /// <param name="key">锁的 Key</param>
        /// <param name="token">加锁时使用的唯一标识</param>
        /// <returns>是否成功释放锁</returns>
        public bool LockRelease(string key, string token)
        {
            return _db.LockRelease(key, token);
        }

        /// <summary>
        /// 延长分布式锁的过期时间（锁续期）
        /// </summary>
        public bool LockExtend(string key, string token, TimeSpan expiry)
        {
            return _db.LockExtend(key, token, expiry);
        }

        #endregion

        #region IDisposable 语法糖（推荐使用）

        /// <summary>
        /// 获取分布式锁并返回 disposable 对象，支持 using() 自动释放
        /// </summary>
        /// <param name="key">锁 Key</param>
        /// <param name="expiry">超时时间</param>
        /// <param name="lockObject">获取成功返回锁控制句柄，失败返回 null</param>
        /// <returns>是否成功获取锁</returns>
        public bool TryAcquireLock(string key, TimeSpan expiry, out IDisposable lockObject)
        {
            string token = Guid.NewGuid().ToString("N");
            if (_db.LockTake(key, token, expiry))
            {
                lockObject = new RedisLockInstance(_db, key, token);
                return true;
            }

            lockObject = null;
            return false;
        }

        /// <summary>
        /// 内部类：实现自动释放锁的 Disposable 包装器
        /// </summary>
        private class RedisLockInstance : IDisposable
        {
            private readonly IDatabase _database;
            private readonly string _key;
            private readonly string _token;
            private bool _disposed = false;

            public RedisLockInstance(IDatabase database, string key, string token)
            {
                _database = database;
                _key = key;
                _token = token;
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _database.LockRelease(_key, _token);
                    _disposed = true;
                }
            }
        }

        #endregion
    }
}
