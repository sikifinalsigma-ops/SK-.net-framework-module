using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace SK_WebPrinter
{


    public static class WebView2HtmlPrintHelper
    {
        /// <summary>
        /// HTML字符串直接静默打印，仅WebView2，无其他第三方库
        /// </summary>
        /// <param name="html">完整HTML文本</param>
        /// <param name="printerName">指定打印机；null 使用默认打印机</param>
        /// <param name="landscape">true=横向；false=纵向A4</param>
        /// <param name="jsRenderWaitMs">JS动态渲染额外等待毫秒，无JS填0即可</param>
        /// <returns>打印是否成功</returns>
        public static async Task<bool> PrintHtmlAsync(string html, string printerName = null, bool landscape = false, int jsRenderWaitMs = 0,double pageWidth = 8.2677, double pageHeight = 11.6929)
        {
            using (var wv = new WebView2 { Visible = false })
            {
                //初始化WebView2
                await wv.EnsureCoreWebView2Async(null);
                var core = wv.CoreWebView2;

                //加载内存HTML
                core.NavigateToString(html);

                //等待导航完成
                var navTcs = new TaskCompletionSource<bool>();
                void OnNavCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
                {
                    core.NavigationCompleted -= OnNavCompleted;
                    navTcs.SetResult(e.IsSuccess);
                }
                core.NavigationCompleted += OnNavCompleted;

                bool navOk = await navTcs.Task;
                if (!navOk)
                    return false;

                // 如果页面有JS动态生成表格/二维码，额外等待
                if (jsRenderWaitMs > 0)
                    await Task.Delay(jsRenderWaitMs);

                //打印配置
                var printSettings = core.Environment.CreatePrintSettings();
                printSettings.PrinterName = printerName;
                printSettings.Orientation = landscape
                    ? CoreWebView2PrintOrientation.Landscape
                    : CoreWebView2PrintOrientation.Portrait;

                //printSettings.ShouldPrintHeaderAndFooter = false;

                // A4尺寸 英寸
                printSettings.PageWidth = pageWidth;
                printSettings.PageHeight = pageHeight;

                //⚠️建议CSS @page 控制边距，这里置0，防止边距叠加
                printSettings.MarginLeft = 0;
                printSettings.MarginRight = 0;
                printSettings.MarginTop = 0;
                printSettings.MarginBottom = 0;

                printSettings.ScaleFactor = 1.0;
                printSettings.ShouldPrintBackgrounds = true;
                printSettings.ShouldPrintHeaderAndFooter = false; //关闭URL、时间页眉页脚

                var status = await core.PrintAsync(printSettings);
                return status == CoreWebView2PrintStatus.Succeeded;
            }
        }



        /*

        /// <summary>
        /// 方案B：HTML → WebView2导出PDF内存流 → PdfiumViewer打印（推荐，排版最稳，可保存PDF）
        /// </summary>
        public static async Task PrintHtmlViaPdfAsync(string html, string printerName = null, bool landscape = false)
        {
            var wv = new WebView2 { Visible = false };
            try
            {
                await wv.EnsureCoreWebView2Async();
                var core = wv.CoreWebView2;
                core.NavigateToString(html);

                var tcs = new TaskCompletionSource<bool>();
                void OnNavComplete(object s, CoreWebView2NavigationCompletedEventArgs e)
                {
                    core.NavigationCompleted -= OnNavComplete;
                    tcs.SetResult(e.IsSuccess);
                }
                core.NavigationCompleted += OnNavComplete;
                await tcs.Task;

                var settings = core.Environment.CreatePrintSettings();
                settings.Orientation = landscape
                    ? CoreWebView2PrintOrientation.Landscape
                    : CoreWebView2PrintOrientation.Portrait;
                settings.PageWidth = 8.2677;
                settings.PageHeight = 11.6929;
                settings.MarginLeft = 0;
                settings.MarginRight = 0;
                settings.MarginTop = 0;
                settings.MarginBottom = 0;
                settings.ScaleFactor = 1.0;
                settings.ShouldPrintBackgrounds = true;
                settings.ShouldPrintHeaderAndFooter = false;

                //导出到内存流（不生成临时文件）
                using (Stream pdfStream = await core.PrintToPdfStreamAsync(settings))
                {
                    pdfStream.Position = 0;
                    using (var pdfDoc = PdfDocument.Load(pdfStream))
                    {
                        var printSetting = new PdfPrintSettings
                        {
                            PageSize = PdfPageSize.A4,
                            FitMode = PdfPrintFitMode.ActualSize, //禁止额外缩放！PDF内部已经排好版
                            ShrinkToMargin = false
                        };
                        if (string.IsNullOrWhiteSpace(printerName))
                            pdfDoc.Print(printSetting);
                        else
                            pdfDoc.Print(printerName, printSetting);
                    }
                }
            }
            finally
            {
                wv.Dispose();
            }
        }

        */

    }
    
}
