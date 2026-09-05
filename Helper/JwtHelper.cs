using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServerService.SysService;
using SK_DataEntity.Entity;
using SK_Redis;

namespace Server.Helper
{
    public class JwtHelper
    {
        // 注意：保密密钥长度必须大于等于 256 比特（即至少 32 个字节/字符）
        private static readonly string SecretKey = ConfigurationManager.AppSettings["Jwt_Secret"];
        private static readonly string Issuer = ConfigurationManager.AppSettings["Jwt_Issuer"];     // 签发者
        private static readonly string Audience = ConfigurationManager.AppSettings["Jwt_Audience"]; // 接收者

        /// <summary>
        /// 生成 JWT Token 字符串
        /// </summary>
        /// <param name="userId">用户唯一ID</param>
        /// <param name="username">用户名</param>
        /// <param name="roles">用户角色列表（可选）</param>
        /// <param name="expireMinutes">过期时间，单位：分钟</param>
        /// <returns>返回生成的 JWT Token 签名字符串</returns>
        public static string CreateToken(string id,string userId, string username, IEnumerable<string> roles = null, int expireMinutes = 60*24)
        {
            // 1. 定义 Payload（载荷）中的 Claims 声明集合
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, id),                  // Subject (主题/用户标识)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID (Token 唯一标识，防重放)
            new Claim(ClaimTypes.NameIdentifier, userId),                     // .NET 标准用户ID Claim
            //new Claim(ClaimTypes.Name, username)                              // .NET 标准用户名 Claim
        };

            // 2. 注入角色 Claims（如果存在）
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // 3. 构建加密密钥与签名凭证 (使用 HMAC-SHA256 算法)
            var keyBytes = Encoding.UTF8.GetBytes(SecretKey);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // 4. 初始化 JwtSecurityToken 实体
            var now = DateTime.Now;
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: now,                             // 生效时间（UTC时间）
                expires: now.AddMinutes(expireMinutes),     // 过期时间（UTC时间）
                signingCredentials: credentials
            );

            // 5. 将 Token 实体序列化为标准字符串 (Header.Payload.Signature)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static SK_SYS_USER GetLoginUser()
        {
            // 方式 A：推荐读取 ClaimsPrincipal.Current
            var principal = ClaimsPrincipal.Current;

            // 方式 B：读取线程 Principal
            // var principal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            SK_SYS_USER userEntity = null;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            {
                string userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                RedisCacheHelper cache = new RedisCacheHelper();
                string userEntityStr = cache.GetString($"login:user:info:{userId}");
                if (string.IsNullOrEmpty(userEntityStr))
                {
                    LoginService ls = new LoginService();
                    userEntity = ls.GetLoginUser(userId);
                    string lockKey = $"lock:login:user:{userEntity.USER_ACCOUNT}";

                    if (cache.TryAcquireLock(lockKey, TimeSpan.FromSeconds(5), out IDisposable lockHandle))
                    {
                        using (lockHandle)
                        {
                            // 缓存用户实体（建议脱敏，避免存入密码哈希）
                            cache.SetString($"login:user:info:{userEntity.ID}", JsonConvert.SerializeObject(userEntity), TimeSpan.FromDays(1));
                        }
                    }
                }

                return userEntity;

            }

            return null;
        }

    }
}