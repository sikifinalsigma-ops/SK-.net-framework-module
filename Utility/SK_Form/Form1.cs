using SK_WebPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SK_Form
{
    public partial class Form1 : Form
    {
        public async static Task test()
        {
            
        }
        public  Form1()
        {
            InitializeComponent();
            
        }


        private string GetReportHtmlFromTemplate()
        {
            string templatePath = Path.Combine(Application.StartupPath, "HTMLPage1.html");
            string template = File.ReadAllText(templatePath, Encoding.UTF8);

            //业务数据
            string title = "测试报表";
            StringBuilder sbRows = new StringBuilder();
            sbRows.Append("<tr class='no-split'><td>AAA</td><td>100</td></tr>");
            sbRows.Append("<tr class='no-split'><td>BBB</td><td>200</td></tr>");

            //替换占位符
            string html = template
                .Replace("{ReportTitle}", System.Web.HttpUtility.HtmlEncode(title))
                .Replace("{TableBodyRows}", sbRows.ToString());

            return html;
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            



        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await WebView2HtmlPrintHelper.PrintHtmlAsync(GetReportHtmlFromTemplate());
        }
    }
}
