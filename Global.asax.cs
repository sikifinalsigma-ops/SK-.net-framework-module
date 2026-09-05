using System;
using System.Web;
using System.Web.Http;
using SaveLog;
namespace Server
{
    public class WebApiApplication : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                StaticSink.CreateSink();
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application startup failed: {ex.Message}");
                throw;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            StaticSink.CloseSink();
        }
    }
}