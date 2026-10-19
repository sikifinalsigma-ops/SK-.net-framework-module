using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_FileStream
{
    public class BitmapHelper
    {
        /// <summary>
        /// 从文件读取Bitmap，无文件锁
        /// </summary>
        public static Bitmap ReadBitmap(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("图片文件不存在", filePath);

            byte[] fileBytes = File.ReadAllBytes(filePath);
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                return new Bitmap(ms);
            }
        }
        
        
        //byte[] 转 Bitmap（数据库、网络流过来的图片字节）
        public static Bitmap BytesToBitmap(byte[] imgBytes)
        {
            using (MemoryStream ms = new MemoryStream(imgBytes))
            {
                return new Bitmap(ms);
            }
        }

        //Base64 字符串读取为 Bitmap
        public static Bitmap Base64ToBitmap(string base64Str)
        {
            // 去掉 data:image/png;base64, 前缀
            if (base64Str.StartsWith("data:image"))
            {
                base64Str = base64Str.Split(',')[1];
            }
            byte[] bytes = Convert.FromBase64String(base64Str);
            return BytesToBitmap(bytes);
        }



    }
}
