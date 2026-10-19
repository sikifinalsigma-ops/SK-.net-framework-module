using SK_QRcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Console
{
    public class TestQrCode
    {
        public static void Test() 
        {
            Bitmap qrBmp = QRcodeHelper.CreateQrCode("https://www.baidu.com", 300, 2);
            qrBmp.Save(@"D:\qr.png", System.Drawing.Imaging.ImageFormat.Png);
            qrBmp.Dispose();
        }
    }
}
