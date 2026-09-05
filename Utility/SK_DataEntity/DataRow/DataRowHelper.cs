using System;
using System.Data;


namespace SK_DataEntity
{
    public class DataRowHelper
    {
        public static int GetInt(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? 0 : Convert.ToInt32(dr[columnName]);
        }

        public static string GetString(DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? null : dr[columnName].ToString();
        }
    }
}
