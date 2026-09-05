using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Tool
{
    public static class Base64Converter
    {

        public static byte[] ConvertToByte(string base64Str) 
        {
            return Convert.FromBase64String(base64Str);
        }

        public static string ConvertToBase64(byte[] OriginalByteArray) 
        {
            return Convert.ToBase64String(OriginalByteArray);
        }
        
    }
}
