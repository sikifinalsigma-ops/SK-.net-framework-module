using SK_Encrypt;
using System;
using System.Text;

namespace SK_Console
{
    public class TestEncrypt
    {
        public static void Test()
        {
            string aaa = Sha256EncryptionHelper.StringTo32ByteKey("123456");
            string bcryaaa = BCryptEncryptionHelper.HashPassword(aaa);

            string aa = Sha256EncryptionHelper.StringTo32ByteKey("sikifinalsigma");

            string text = "123456";
            string md5Hash = Md5EncryptionHelper.ComputeStringMd5(text);

            Console.WriteLine($"原始字串: {text}");
            Console.WriteLine($"MD5 結果: {md5Hash}");



            string bcry = BCryptEncryptionHelper.HashPassword(md5Hash);
            bool istrue = BCryptEncryptionHelper.VerifyPassword(md5Hash, bcry);
            bool isfalse = BCryptEncryptionHelper.VerifyPassword("lsdfjaldfjaglkjaglajgkfjagfjdg", bcry);


            
            byte[] key = HexConverter.HexStringToByteArray(aa);

            string originalText = "Hello, .NET Framework AES 加密!";

            // 2. 加密
            string encryptedBase64 = AesEncryptionHelper.Encrypt(originalText, key);
            Console.WriteLine($"加密結果 (Base64): {encryptedBase64}");

            // 3. 解密
            string decryptedText = AesEncryptionHelper.Decrypt(encryptedBase64, key);
            Console.WriteLine($"解密結果: {decryptedText}");
        }

        public static void Test2()
        {
            // 1. 生成金钥对
            EccEncryptionHelper.GenerateKeyPair(out byte[] publicKey, out byte[] privateKey);
            Console.WriteLine($"公钥 (Hex, 32 bytes): {BitConverter.ToString(publicKey).Replace("-", "")}");
            Console.WriteLine($"私钥 (Hex, 32 bytes): {BitConverter.ToString(privateKey).Replace("-", "")}");

            // 2. 准备待签名数据
            string originalMessage = "Hello, Ed25519 in C#!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(originalMessage);

            // 3. 私钥签名
            byte[] signature = EccEncryptionHelper.Sign(messageBytes, privateKey);
            Console.WriteLine($"签名 (Hex, 64 bytes): {BitConverter.ToString(signature).Replace("-", "")}");

            // 4. 公钥验证
            bool isValid = EccEncryptionHelper.Verify(messageBytes, signature, publicKey);
            Console.WriteLine($"验证结果: {(isValid ? "✅ 成功" : "❌ 失败")}");

            // 5. 测试防篡改（篡改资料后验证）
            byte[] tamperedData = Encoding.UTF8.GetBytes("Hello, Tampered!");
            bool isTamperedValid = EccEncryptionHelper.Verify(tamperedData, signature, publicKey);
            Console.WriteLine($"篡改数据验证结果: {(isTamperedValid ? "✅ 通过" : "❌ 拦截成功")}");
        }
    }
}
