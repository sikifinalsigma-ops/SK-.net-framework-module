using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SK_Encrypt
{
    public class Md5EncryptionHelper
    {
        /// <summary>
        /// 计算字串的 MD5 杂凑值 (回传 32 位小写 16 进制字串)
        /// </summary>
        public static string ComputeStringMd5(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (MD5 md5 = MD5.Create())
            {
                // 1. 将输入字串转为 byte[] (通常采用 UTF-8 编码)
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // 2. 计算 MD5 哈希 (输出固定 16 bytes / 128 bits)
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 3. 转为 32 位 16 进制字串 (x2 代表两位小写 16 进制，若要大写改为 X2)
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }


        /// <summary>
        /// 计算档案的 MD5 哈希值 (支援大档案)
        /// </summary>
        public static string ComputeFileMd5(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("找不到指定档案", filePath);

            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] hashBytes = md5.ComputeHash(stream);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}
