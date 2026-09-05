using System;

namespace SK_Encrypt
{
    public class HexConverter
    {
        public static byte[] HexStringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Array.Empty<byte>();

            // 移除可能存在的破折号或空格
            hex = hex.Replace("-", "").Replace(" ", "");

            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex 字串长度必须为偶数", nameof(hex));

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static string ByteArrayToHexString(byte[] hashBytes)
        {
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }




    }
}
