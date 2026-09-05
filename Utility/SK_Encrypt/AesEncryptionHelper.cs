using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SK_Encrypt
{
    public class AesEncryptionHelper
    {
        // 预设使用 256 bit (32 bytes) 金钥长度
        private const int KeySizeBits = 256;
        private const int BlockSizeBits = 128;

        /// <summary>
        /// 使用 AES-CBC 模式加密字串
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="key">32 字节 (256 bits) 的金钥</param>
        /// <returns>Base64 编码的密文 (包含 IV)</returns>
        public static string Encrypt(string plainText, byte[] key)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length != KeySizeBits / 8)
                throw new ArgumentException("Key 必须为 32 字节 (256 bits)", nameof(key));

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySizeBits;
                aes.BlockSize = BlockSizeBits;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.GenerateIV(); // 每次加密皆随机生成新的 IV

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    // 将 IV 写入输出串流的最前端，解密时才能提取
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 使用 AES-CBC 模式解密字串
        /// </summary>
        /// <param name="cipherTextBase64">Base64 编码的密文 (包含 IV)</param>
        /// <param name="key">32 字节 (256 bits) 的金钥</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string cipherTextBase64, byte[] key)
        {
            if (string.IsNullOrEmpty(cipherTextBase64))
                throw new ArgumentNullException(nameof(cipherTextBase64));
            if (key == null || key.Length != KeySizeBits / 8)
                throw new ArgumentException("Key 必须为 32 字节 (256 bits)", nameof(key));

            byte[] fullCipher = Convert.FromBase64String(cipherTextBase64);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = KeySizeBits;
                aes.BlockSize = BlockSizeBits;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                int ivLength = aes.BlockSize / 8; // 16 bytes
                byte[] iv = new byte[ivLength];
                byte[] cipher = new byte[fullCipher.Length - ivLength];

                // 从密文中分离出 IV 与实际加密数据
                Array.Copy(fullCipher, 0, iv, 0, ivLength);
                Array.Copy(fullCipher, ivLength, cipher, 0, cipher.Length);

                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipher))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
