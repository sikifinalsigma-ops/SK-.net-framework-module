using SK_Redis;


namespace ServerService.SysService
{
    public static class Helper
    {
        public static RedisCacheHelper RedisHelper() 
        {
            return new RedisCacheHelper();
        }

    }
}
