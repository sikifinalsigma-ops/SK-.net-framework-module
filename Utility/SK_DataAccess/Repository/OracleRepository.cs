using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SK_DataAccess
{
    public class OracleRepository : IDatabase, IDisposable
    {
        private OracleDbContext context;

        public OracleRepository(string connectionString)
        {
            context = new OracleDbContext(connectionString);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> QueryNoTracking<T>() where T : class
        {
            return context.Set<T>().AsNoTracking();
        }        

        public int InsertAndSaveEntity<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
            return SaveEntity();
        }

        public int InsertAndSaveEntityList<T>(List<T> entities) where T : class
        {
            if (entities == null || entities.Count == 0)
                return 0;
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Set<T>().AddRange(entities);
            context.Configuration.AutoDetectChangesEnabled = true;
            return SaveEntity();
        }

        public int UpdateAndSaveEntity<T>(T entity) where T : class
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
                entry = context.Entry(entity);
            }
            entry.State = EntityState.Modified;
            return SaveEntity();
        }

        public int UpdateAndSaveEntityProperties<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class
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

            foreach (var propertyName in properties)
            {
                entry.Property(propertyName).IsModified = true;
            }

            return SaveEntity();
        }

        public int DeleteAndSaveEntity<T>(T entity) where T : class
        {
            var dbSet = context.Set<T>();
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            return SaveEntity();
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
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.State = EntityState.Detached;
                            break;

                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;

                        default:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw e;
            }

        }



        public void InsertEntity<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
        }

        public void UpdateEntity<T>(T entity) where T : class
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
                entry = context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void DeleteEntity<T>(T entity) where T : class
        {
            var dbSet = context.Set<T>();
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void ExecuteInTransaction(Action<OracleRepository> operation)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    operation(this);
                    SaveEntity();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }



        //OracleParameter
        public int ExecuteSql(string sql, params DbParameter[] oracleParameters)
        {
            return context.Database.ExecuteSqlCommand(sql, oracleParameters);
        }

        public List<T> QueryEntitiesBySql<T>(string sql, params DbParameter[] oracleParameters) where T : class
        {
            return context.Database.SqlQuery<T>(sql, oracleParameters).ToList();
        }

        public T QueryEntityBySql<T>(string sql, params DbParameter[] oracleParameters) where T : class
        {
            return context.Database.SqlQuery<T>(sql, oracleParameters).FirstOrDefault();
        }


        public DataTable QueryEntitiesAsDataTable(string sql, params DbParameter[] oracleParameters)
        {
            var dataTable = new DataTable();

            var connection = (OracleConnection)context.Database.Connection;
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
                        command.Transaction = (OracleTransaction)context.Database.CurrentTransaction.UnderlyingTransaction;
                    }

                    if (oracleParameters != null && oracleParameters.Length > 0)
                    {
                        command.Parameters.AddRange(oracleParameters);
                    }

                    using (var adapter = new OracleDataAdapter((OracleCommand)command))
                    {
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


    }

    public static class OracleParameterFactory
    {
        public static DbParameter Create(string name, object value)
        {
            return new OracleParameter(name, value);
        }
    }

}
