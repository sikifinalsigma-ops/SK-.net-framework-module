using Newtonsoft.Json;
using Server.Helper;
using ServerService.SysService;
using SK_DataEntity.Entity;
using System.Web.Http;

namespace Server.Controller.SysModule
{

    public class UserSignInController : ApiController
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

            SK_SYS_USER userEntity = null;

            // 2. 账号密码/状态校验
            if (!LoginService.CheckLogin(userLogin.UserId, userLogin.UserPassword, out userEntity))
            {
                return Json(new { code = 401, message = "用户名或密码错误" });
            }


            if (!RedisHelper.SetValue($"{ServiceKeySetting.UserRedisLockPrefix}{userEntity.USER_ACCOUNT}", $"{ServiceKeySetting.UserRedisPrefix}{userEntity.USER_ACCOUNT}", JsonConvert.SerializeObject(userEntity))) 
            {
                return Json(new { code = 429, message = "请求过于频繁，请稍后再试" });
            }           

            // 生成 JWT Token
            string jwtToken = JwtHelper.CreateToken(userEntity.ID, userEntity.USER_ACCOUNT, userEntity.USER_NAME);

            // 返回规范化 JSON 结果
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
        public IHttpActionResult LogInfo()
        {
            SK_SYS_USER userEntity = JwtHelper.GetLoginUser();
            return Json(new
            {
                code = 200,
                message = "登录成功",
                data = new
                {                    
                    userInfo = new
                    {
                        id = userEntity.ID,
                        account = userEntity.USER_ACCOUNT,
                        userName = userEntity.USER_NAME
                    }
                }
            });
        }

        [HttpPost]
        public IHttpActionResult Logout()
        {           
            SK_SYS_USER userEntity = JwtHelper.GetLoginUser();
            RedisHelper.RemoveValue($"{ServiceKeySetting.UserRedisPrefix}{userEntity.ID}");
            return Json(new
            {
                code = 200,
                message="退出登录成功。"
            }) ;
        }

        [HttpPost]
        public IHttpActionResult UpdatePassword([FromBody] UpdatePasswordRequest reqData)
        {
            if (reqData == null || string.IsNullOrWhiteSpace(reqData.OldPassword) || string.IsNullOrWhiteSpace(reqData.NewPassword))
            {
                return Json(new { code = 400, message = "旧密码和新密码不能为空" });
            }

            SK_SYS_USER userEntity = JwtHelper.GetLoginUser();
            // 2. 账号密码/状态校验
            if (!LoginService.CheckLogin(userEntity.USER_ACCOUNT, reqData.OldPassword, out userEntity))
            {
                return Json(new { code = 401, message = "用户名或密码错误" });
            }

            LoginService.ChangePassword(userEntity,reqData.NewPassword);

            RedisHelper.RemoveValue($"{ServiceKeySetting.UserRedisPrefix}{userEntity.ID}");
            RedisHelper.SetValue($"{ServiceKeySetting.UserRedisLockPrefix}{userEntity.USER_ACCOUNT}", $"{ServiceKeySetting.UserRedisPrefix}{userEntity.USER_ACCOUNT}", JsonConvert.SerializeObject(userEntity));

            return Json(new
            {
                code = 200,
                message = "修改登录密码成功。"
            });
        }

        public class UpdatePasswordRequest
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
    }
}