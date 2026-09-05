using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Tool
{
    public static class PinYinHelper
    {
        /// 
        /// 获取中文字符串的拼音首字母简写（大写）
        /// 
        public static string GetFirstPinyin(string chineseText)
        {
            if (string.IsNullOrWhiteSpace(chineseText))
                return string.Empty;

            var sb = new StringBuilder();

            foreach (char c in chineseText)
            {
                if (ChineseChar.IsValidChar(c))
                {
                    var chineseChar = new ChineseChar(c);
                    if (chineseChar.Pinyins.Count > 0 && chineseChar.Pinyins[0] != null)
                    {
                        // Pinyins[0] 格式如 "NI3" 或 "ZHONG1"，取第 1 个字元
                        sb.Append(chineseChar.Pinyins[0][0]);
                    }
                }
                else
                {
                    // 非中文字符（英文、数字等）保留原样
                    sb.Append(c);
                }
            }

            return sb.ToString().ToUpper();
        }
    }
}
