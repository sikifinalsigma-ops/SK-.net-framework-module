using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerService.SysService
{
    public static class ServiceKeySetting
    {
        public const string UserRedisPrefix = "login:user:info:";

        public const string UserRedisLockPrefix = "lock:login:user:";
    }
}
