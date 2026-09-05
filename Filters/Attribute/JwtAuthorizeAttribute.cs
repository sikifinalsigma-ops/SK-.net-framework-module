using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 核心授权逻辑校验
        /// </summary>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // 1. 检查是否存在 [AllowAnonymous]
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return true;
            }

            // 2. 基础登录状态与系统角色检查（检查 Roles 与 Users 属性）
            if (!base.IsAuthorized(actionContext))
            {
                return false;
            }

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (principal == null || !principal.Identity.IsAuthenticated)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 授权失败处理：区分 401（未登录）与 403（已登录但权限不足）
        /// </summary>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var principal = actionContext.RequestContext.Principal;

            // 如果用户已登录但被拒绝，说明是角色/权限不足，返回 403
            if (principal != null && principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Forbidden,
                    new
                    {
                        code = 403,
                        message = "Forbidden: Insufficient role or permission."
                    }
                );
            }
            else
            {
                // 用户未登录或 Token 无效，返回 401
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.Unauthorized,
                    new
                    {
                        code = 401,
                        message = "Unauthorized: Authentication credentials required."
                    }
                );
            }
        }
    }
}