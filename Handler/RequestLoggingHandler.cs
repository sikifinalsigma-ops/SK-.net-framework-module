using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SaveLog;
/// <summary>
/// 接收信息日志记录
/// </summary>
public class RequestLoggingHandler : DelegatingHandler
{
    // 限制日志读取 Body 的最大字节数（例如 100KB），防止大文件撑爆内存
    private const int MaxBodyLogSize = 100 * 1024;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        // 1. 安全地读取 Request Body
        string requestBody = null;
        if (request.Content != null)
        {
            await request.Content.LoadIntoBufferAsync(MaxBodyLogSize);
            requestBody = await request.Content.ReadAsStringAsync();
        }
        StaticSink.SaveLog("Request: {Method} {Url} Body: {Body} ", request.Method, request.RequestUri, requestBody);
        try
        {
            // 2. 执行后续管道
            var response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();

            // 3. 安全地读取 Response Body
            string responseBody = string.Empty;

            if (response?.Content != null && IsTextBasedContentType(response.Content.Headers.ContentType?.MediaType))
            {
                // 检查 ContentLength 是否超标
                var contentLength = response.Content.Headers.ContentLength;

                if (contentLength.HasValue && contentLength.Value > MaxBodyLogSize)
                {
                    responseBody = $"[Body 内容过大 ({contentLength.Value} bytes)，已略过日志记录]";
                }
                else
                {
                    try
                    {
                        // 不传参数，按默认或全局设置缓冲（超长时触发异常捕获）
                        await response.Content.LoadIntoBufferAsync();
                        responseBody = await response.Content.ReadAsStringAsync();

                        // 如果没有 ContentLength 响应头，读取后按字符串长度截断
                        if (responseBody.Length > MaxBodyLogSize)
                        {
                            responseBody = responseBody.Substring(0, MaxBodyLogSize) + "... [已截断]";
                        }
                    }
                    catch (Exception ex)
                    {
                        responseBody = $"[读取 Body 失败: {ex.Message}]";
                    }
                }

            }

            StaticSink.SaveLog("Response: {StatusCode} Duration: {Elapsed}ms Body: {Body}",
                (int)response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                responseBody);

            return response;

        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            // 捕获管道内的未处理异常并记录日志
            StaticSink.SaveLog("Response Error: {Message} Duration: {Elapsed}ms",
                ex.Message,
                stopwatch.ElapsedMilliseconds, ex.StackTrace);
            throw; // 继续向上抛出异常，交由 API 的 ExceptionFilter 处理
        }
    }

    /// <summary>
    /// 判断响应是否为可读取日志的文本类型（过滤文件流、图片等）
    /// </summary>
    private bool IsTextBasedContentType(string mediaType)
    {
        if (string.IsNullOrEmpty(mediaType)) return false;

        return mediaType.Contains("json") ||
               mediaType.Contains("xml") ||
               mediaType.Contains("text") ||
               mediaType.Contains("x-www-form-urlencoded");
    }

}