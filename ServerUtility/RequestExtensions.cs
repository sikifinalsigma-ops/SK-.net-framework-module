using System;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ServerUtility
{ 

    public static class RequestExtensions
    {
        public static string GetClientIp(HttpRequestMessage request)
        {
            // 1. 优先尝试从 X-Forwarded-For 读取 (格式可能是: "client_ip, proxy1_ip, proxy2_ip")
            if (request.Headers.Contains("X-Forwarded-For"))
            {
                var forwardedFor = request.Headers.GetValues("X-Forwarded-For").FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(forwardedFor))
                {
                    // 取逗号分隔的第一个有效 IP（最左侧是原始客户端真实 IP）
                    string clientIp = forwardedFor.Split(',')[0].Trim();
                    if (!string.IsNullOrEmpty(clientIp))
                    {
                        return clientIp;
                    }
                }
            }

            // 2. 尝试从 X-Real-IP 读取
            if (request.Headers.Contains("X-Real-IP"))
            {
                var realIp = request.Headers.GetValues("X-Real-IP").FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(realIp))
                {
                    return realIp.Trim();
                }
            }

            // 3. 兜底方案：如果不是通过代理，读取底层的 UserHostAddress
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var ctx = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                if (ctx?.Request != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // 4. 自托管 (OWIN/Self-Host) 场景下的兜底
            if (request.Properties.ContainsKey("System.ServiceModel.Channels.RemoteEndpointMessageProperty"))
            {
                dynamic prop = request.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"];
                return prop?.Address;
            }

            return "Unknown";
        }
    }
}
