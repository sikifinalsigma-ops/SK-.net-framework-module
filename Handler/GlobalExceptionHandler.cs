using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using SaveLog;
namespace WebApi.Filters.Handler
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            StaticSink.SaveException(context.Exception, "全域错误");
            var response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new { message = "系统异常，请联系管理人员。" });

            context.Result = new ResponseMessageResult(response);
        }
    }
}