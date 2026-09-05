using System;
using System.Security.Cryptography;
using System.Text;


namespace SK_Encrypt
{
    public class Sha256EncryptionHelper
    {
        public static string StringTo32ByteKey(string customKeyString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(customKeyString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
