using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace SK_QRcode
{
    public class QRcodeHelper
    {
        public static Bitmap CreateQrCode(string content, int size = 300, int margin = 2)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = size,
                    Height = size,
                    Margin = margin,
                    CharacterSet = "UTF-8" // 解决中文乱码
                }
            };
            Bitmap bmp = writer.Write(content);
            return bmp;
        }

        //网页展示
        public static string QrToBase64(string content, int size = 300)
        {
            using (Bitmap bmp = CreateQrCode(content, size))
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = ms.ToArray();
                return "data:image/png;base64," + Convert.ToBase64String(arr);
            }
        }

        //中间加logo
        public static Bitmap CreateQrWithLogo(string content, Bitmap logo, int size = 300)
        {
            using (Bitmap qrBmp = CreateQrCode(content, size))
            using (Graphics g = Graphics.FromImage(qrBmp))
            {
                int logoSize = size / 5;
                int x = (size - logoSize) / 2;
                int y = (size - logoSize) / 2;
                g.DrawImage(logo, x, y, logoSize, logoSize);
                g.Dispose();
                return qrBmp;
            }            
        }
    }
}
