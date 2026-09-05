using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class JwtAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        // 从 Web.config 读取配置
        private static readonly string SecretKey = ConfigurationManager.AppSettings["Jwt_Secret"];
        private static readonly string Issuer = ConfigurationManager.AppSettings["Jwt_Issuer"];
        private static readonly string Audience = ConfigurationManager.AppSettings["Jwt_Audience"];

        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 关键点：检查 Action 或 Controller 是否标记了 [AllowAnonymous]
            if (context.ActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0 ||
                context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
            {
                return Task.CompletedTask; // 遇到 AllowAnonymous 直接放行
            }

            var req = context.Request;
            if (req.Headers.Authorization == null || !string.Equals(req.Headers.Authorization.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase))
            {
                // 未携带 Token，委托给全局 AuthorizeFilter 处理（若接口有 [Authorize] 则返回 401）
                return Task.CompletedTask;
            }

            var token = req.Headers.Authorization.Parameter;
            if (TryValidateToken(token, out var principal))
            {
                // 绑定到上下文
                context.Principal = principal;

                // 建议增加以下兼容兜底：
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            else
            {
                // Token 无效或过期，标记为未通过认证
                context.ErrorResult = new AuthenticationFailureResult("Invalid or expired token.", req);
            }

            return Task.CompletedTask;
        }


        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private bool TryValidateToken(string token, out IPrincipal principal)
        {
            principal = null;

            // 防御性检查：确保配置已填写
            if (string.IsNullOrEmpty(SecretKey))
            {
                throw new InvalidOperationException("Jwt:SecretKey is not configured in Web.config.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = !string.IsNullOrEmpty(Issuer),
                ValidIssuer = Issuer,
                ValidateAudience = !string.IsNullOrEmpty(Audience),
                ValidAudience = Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };

            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}