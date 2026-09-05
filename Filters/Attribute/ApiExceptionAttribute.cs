using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using SaveLog;
namespace WebApi.Filters
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            var ex = actionExecutedContext.Exception;

            // 🔹 避免重複記錄（如果已經標記過）
            if (!actionExecutedContext.Request.Properties.ContainsKey("Logged"))
            {
               StaticSink.SaveException(ex, "API 發生例外 {@Url} {@Method}",
                    actionExecutedContext.Request.RequestUri,
                    actionExecutedContext.Request.Method);

                actionExecutedContext.Request.Properties["Logged"] = true;
            }

            // 处理特定异常信息
            if (ex is ArgumentException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}