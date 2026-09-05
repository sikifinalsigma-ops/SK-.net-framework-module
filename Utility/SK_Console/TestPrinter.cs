using SK_WebPrinter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Console
{
    public class TestPrinter
    {
        public async static Task test() 
        {
            await WebView2HtmlPrintHelper.PrintHtmlAsync("HTMLPage1.html");
        }
    }
}
