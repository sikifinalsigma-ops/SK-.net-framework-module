using SaveLog;
using System;
using System.IO;


namespace SK_Tool
{
    public static class FormatTime
    {
        public static string formatDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }        
    }
}
