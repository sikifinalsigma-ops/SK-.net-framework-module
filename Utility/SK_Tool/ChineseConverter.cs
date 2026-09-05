using Microsoft.VisualBasic;

namespace SK_Tool
{
    public static class ChineseConverter
    {
        /// <summary>
        /// 简体转繁体
        /// </summary>
        public static string ToTraditional(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return Strings.StrConv(text, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 繁体转简体
        /// </summary>
        public static string ToSimplified(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return Strings.StrConv(text, VbStrConv.SimplifiedChinese, 0);
        }
    }
}
