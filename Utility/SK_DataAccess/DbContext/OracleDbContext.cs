using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.Diagnostics;

namespace SK_DataAccess
{
    public class OracleDbContext : DbContext
    {
        private string Schema = string.Empty;
        //public OracleDbContext(string connStr) : base(new OracleConnection(connStr), OracleDbCompiledModelFactory.Instance,true)
        public OracleDbContext(string connStr) : base(new OracleConnection(connStr),true)
        {
            Database.SetInitializer<OracleDbContext>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //自动变更检测，更新后自动变更状态，批量插入数据时可以关闭。
            Configuration.AutoDetectChangesEnabled = true;
            Schema = new OracleConnectionStringBuilder(connStr).UserID.ToUpper();

#if DEBUG
            //SQL语句输出到输出窗口
            Database.Log = log => Debug.WriteLine(log);
            //SQL语句输出到控制台
            //Database.Log = sql => Console.WriteLine(sql);
#else
    // 生产环境可接日志框架（可选）
    //Database.Log = log => logger.Debug(log);
#endif



        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            foreach (var entityType in EntityRegister.registerEntity)
            {
                modelBuilder.RegisterEntityType(entityType);
            }
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
