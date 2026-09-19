using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SK_DataAccess.Repository
{
    public class Repository : IDisposable
    {
        private DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> QueryNoTracking<T>() where T : class
        {
            return context.Set<T>().AsNoTracking();
        }

        public int InsertEntity<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
            return SaveEntityIfNotInTransaction();
        }

        public int InsertEntityList<T>(List<T> entities) where T : class
        {
            if (entities == null || entities.Count == 0)
                return 0;
            try
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Set<T>().AddRange(entities);
                return SaveEntityIfNotInTransaction();
            }
            finally 
            {
                context.Configuration.AutoDetectChangesEnabled = true;
            }     
            
        }

        public int UpdateEntity<T>(T entity) where T : class
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
                entry = context.Entry(entity);
            }
            entry.State = EntityState.Modified;
            return SaveEntityIfNotInTransaction();
        }

        public int UpdateEntityProperties<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class
        {
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
                entry = context.Entry(entity);
            }

            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                entry.Property(propertyName).IsModified = false;
            }

            foreach (var property in properties)
            {
                string propertyName = GetPropertyName(property);
                entry.Property(propertyName).IsModified = true;
            }

            return SaveEntityIfNotInTransaction();
        }

        public int DeleteEntity<T>(T entity) where T : class
        {
            var dbSet = context.Set<T>();
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return SaveEntityIfNotInTransaction();
        }

        public int SaveEntity()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                foreach (var entry in e.Entries)
                {
                    entry.State = EntityState.Detached;
                }
                throw;
            }
        }

        private int SaveEntityIfNotInTransaction()
        {
            if (context.Database.CurrentTransaction != null)
            {
                return 0; 
            }
            return SaveEntity();
        }

        public void ExecuteInTransaction(Action<Repository> operation)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                operation(this);
                SaveEntity();
                transaction.Commit();
            }
        }

        public int ExecuteSql(string sql, params DbParameter[] Parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, Parameters);
        }

        public List<T> QueryEntitiesBySql<T>(string sql, params DbParameter[] Parameters) where T : class
        {
            return context.Database.SqlQuery<T>(sql, Parameters).ToList();
        }

        public T QueryEntityBySql<T>(string sql, params DbParameter[] Parameters) where T : class
        {
            return context.Database.SqlQuery<T>(sql, Parameters).FirstOrDefault();
        }


        public DataTable QueryEntitiesAsDataTable(string sql, params DbParameter[] Parameters)
        {
            var dataTable = new DataTable();

            var connection = context.Database.Connection;
            var shouldClose = false;

            try
            {
                // 确保连接打开
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    shouldClose = true;
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;
                    if (context.Database.CurrentTransaction != null)
                    {
                        command.Transaction = context.Database.CurrentTransaction.UnderlyingTransaction;
                    }

                    command.Parameters.Clear();
                    foreach (var p in Parameters)
                    {
                        // 若已有父级容器，先从原容器脱钩或克隆参数
                        if (p is ICloneable cloneable)
                            command.Parameters.Add(cloneable.Clone());
                        else
                            command.Parameters.Add(p);
                    }

                    using (var adapter = CreateDataAdapter(connection))
                    {
                        adapter.SelectCommand = command;
                        adapter.Fill(dataTable);
                    }
                }
            }
            finally
            {
                if (shouldClose)
                {
                    connection.Close();
                }
            }

            return dataTable;
        }


        public void Dispose()
        {
            context?.Dispose();
        }

        private static DbDataAdapter CreateDataAdapter(DbConnection connection)
        {
            // 优先根据具体类型直接匹配，防止 DbProviderFactories 未注册
            if (connection is OracleConnection)
            {
                return new OracleDataAdapter();
            }
            if (connection is MySqlConnection)
            {
                return new MySqlDataAdapter();
            }

            var factory = DbProviderFactories.GetFactory(connection);
            var adapter = factory?.CreateDataAdapter();
            if (adapter == null)
            {
                throw new NotSupportedException($"无法为驱动 {connection.GetType().FullName} 创建 DbDataAdapter。");
            }
            return adapter;
        }

        private static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression member)
            {
                return member.Member.Name;
            }

            // 处理值类型装箱为 object 产生的 UnaryExpression (如 x => (object)x.Age)
            if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
            {
                return unaryMember.Member.Name;
            }

            throw new ArgumentException("表达式必须是成员选择表达式，例如：x => x.UserName");
        }
    }

    public static class RepositoryFactory
    {
        public static Repository OracleRepository(string connStr) 
        {
            return new Repository(new OracleDbContext(connStr));
        }

        public static Repository MySqlRepository(string connStr)
        {
            return new Repository(new MySqlDbContext(connStr));
        }
    }

    public static class OracleParameterCreate
    {
        public static DbParameter Create(string name, object value)
        {
            return new OracleParameter(name, value);
        }
    }

    public static class MySqlParameterCreate
    {
        public static DbParameter Create(string name, object value)
        {
            return new MySqlParameter(name, value);
        }
    }
}
