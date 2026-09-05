using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SK_DataAccess.Helper
{
    public class SqlserverHelpler
    {

        /// <summary>
        /// SQL Server 数据库通用操作类
        /// 基于 .NET Framework，使用 SqlClient
        /// </summary>

        // 从配置文件读取数据库连接字符串
        //private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        public readonly string _connectionString = string.Empty;

        public SqlserverHelpler(string conn)
        {
            _connectionString = conn;
        }

        #region 执行非查询语句（增/删/改）
        /// <summary>
        /// 执行增删改操作，返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程名</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string cmdText, CommandType cmdType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandType = cmdType;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("数据库执行异常：" + ex.Message);
                    }
                }
            }
        }
        #endregion

        #region 执行查询，返回DataTable
        /// <summary>
        /// 执行查询，返回DataTable
        /// </summary>
        public DataTable ExecuteDataTable(string cmdText, CommandType cmdType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandType = cmdType;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("数据库查询异常：" + ex.Message);
                    }
                }
            }
        }
        #endregion

        #region 执行查询，返回首行首列数据
        /// <summary>
        /// 执行查询，返回结果集的第一行第一列
        /// </summary>
        public object ExecuteScalar(string cmdText, CommandType cmdType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandType = cmdType;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("数据库查询异常：" + ex.Message);
                    }
                }
            }
        }
        #endregion

        #region 事务执行多条SQL
        /// <summary>
        /// 事务执行：全部成功才提交，任意失败则回滚
        /// </summary>
        /// <param name="sqlList">SQL语句列表</param>
        /// <returns>是否执行成功</returns>
        public bool ExecuteTransaction(params string[] sqlList)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        foreach (string sql in sqlList)
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("事务执行失败，已回滚：" + ex.Message);
                }
            }
        }
        #endregion

        #region 带参数的事务执行
        /// <summary>
        /// 带参数的事务执行
        /// </summary>
        public bool ExecuteTransaction(string[] cmdTexts, CommandType cmdType, params SqlParameter[][] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;
                        cmd.CommandType = cmdType;

                        for (int i = 0; i < cmdTexts.Length; i++)
                        {
                            cmd.CommandText = cmdTexts[i];
                            cmd.Parameters.Clear();
                            if (parameters[i] != null)
                            {
                                cmd.Parameters.AddRange(parameters[i]);
                            }
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("事务执行失败，已回滚：" + ex.Message);
                }
            }
        }
        #endregion

    }
}
