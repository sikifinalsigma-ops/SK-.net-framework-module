using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using WebApi.Filters;
using WebApi.Filters.Handler;
using WebApi.Filters.Logger;

namespace Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /*
            // Option A: Enable globally for the whole application
            var cors = new EnableCorsAttribute("https://example.com, https://localhost:3000", "*", "*");
            config.EnableCors(cors);
            */

            //CORS
            config.EnableCors();


            //filters
            //jwt
            // 注册全局认证 Filter（负责解析 Token 为 Principal）
            config.Filters.Add(new JwtAuthenticationFilter());
            // 注册全局授权 Filter（负责拦截未登录访问）
            config.Filters.Add(new JwtAuthorizeAttribute());


            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );




            ///handler
            //访问日志
            if (ConfigurationManager.AppSettings["RequestLog"] == "true")
                config.MessageHandlers.Add(new RequestLoggingHandler());
            //异常日志
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new SerilogExceptionLogger());



            ///Json设置
            // 1. 获取全局 Json 格式化器设置
            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            // 2. 启用小驼峰命名 (camelCase)
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // 3. 忽略 null 字段
            settings.NullValueHandling = NullValueHandling.Ignore;

            // 4. 解决 EF 等实体间的循环引用问题
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // 5. 统一日期输出格式
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            // (可选) 移除 XML 格式化器，强制所有接口默认返回 JSON
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

        }


    }
}
