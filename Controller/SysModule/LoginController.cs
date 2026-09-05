using Newtonsoft.Json;
using Server.Helper;
using ServerService.SysService;
using SK_DataEntity.Entity;
using SK_Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Server.Controller.SysModule
{

    public class UserLogController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginRequest userLogin)
        {
            // 1. 参数校验
            if (userLogin == null || string.IsNullOrWhiteSpace(userLogin.UserId) || string.IsNullOrWhiteSpace(userLogin.UserPassword))
            {
                return Json(new { code = 400, message = "用户名和密码不能为空" });
            }

            LoginService ls = new LoginService();
            SK_SYS_USER userEntity = null;

            // 2. 账号密码/状态校验
            if (!ls.CheckLogin(userLogin.UserId, userLogin.UserPassword, out userEntity))
            {
                return Json(new { code = 401, message = "用户名或密码错误" });
            }

            // 3. Redis 并发控制与缓存写入
            RedisCacheHelper cache = new RedisCacheHelper();
            string lockKey = $"lock:login:user:{userEntity.USER_ACCOUNT}";

            if (cache.TryAcquireLock(lockKey, TimeSpan.FromSeconds(5), out IDisposable lockHandle))
            {
                using (lockHandle)
                {
                    // 缓存用户实体（建议脱敏，避免存入密码哈希）
                    cache.SetString($"login:user:info:{userEntity.ID}", JsonConvert.SerializeObject(userEntity), TimeSpan.FromDays(1));
                }
            }
            else
            {
                return Json(new { code = 429, message = "请求过于频繁，请稍后再试" });
            }

            // 4. 生成 JWT Token
            string jwtToken = JwtHelper.CreateToken(userEntity.ID, userEntity.USER_ACCOUNT, userEntity.USER_NAME);

            // 5. 返回规范化 JSON 结果
            return Json(new
            {
                code = 200,
                message = "登录成功",
                data = new
                {
                    token = jwtToken,
                    tokenType = "Bearer",
                    expiresIn = 86400, // 与 Token 实际有效秒数保持一致
                    userInfo = new
                    {
                        id = userEntity.ID,
                        account = userEntity.USER_ACCOUNT,
                        userName = userEntity.USER_NAME
                    }
                }
            });
        }


        public class LoginRequest
        {
            public string UserId { get; set; }
            public string UserPassword { get; set; }
        }


        [HttpPost]
        public IHttpActionResult Logout()
        {
            RedisCacheHelper cache = new RedisCacheHelper();
            //cache.Remove($"login:user:info:{userEntity.USER_ACCOUNT}");
            return Json(new
            {
                code = 200,
                message="退出登录成功。"
            }) ;
        }
    }
}