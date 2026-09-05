using SaveLog;
using System.Web.Http.ExceptionHandling;

namespace WebApi.Filters.Logger
{
    public class SerilogExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            StaticSink.SaveException (context.Exception, "发生未处理异常");
        }
    }
}