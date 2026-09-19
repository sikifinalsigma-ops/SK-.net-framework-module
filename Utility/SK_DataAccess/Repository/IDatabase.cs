using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace SK_DataAccess
{
    public interface IDatabase
    {
        IQueryable<T> Query<T>() where T : class;

        IQueryable<T> QueryNoTracking<T>() where T : class;

        int InsertAndSaveEntity<T>(T entity) where T : class;

        int InsertAndSaveEntityList<T>(List<T> entities) where T : class;

        int UpdateAndSaveEntity<T>(T entity) where T : class;

        int UpdateAndSaveEntityProperties<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class;

        int DeleteAndSaveEntity<T>(T entity) where T : class;

        int SaveEntity();

        void InsertEntity<T>(T entity) where T : class;

        void UpdateEntity<T>(T entity) where T : class;

        void DeleteEntity<T>(T entity) where T : class;

        //void ExecuteInTransaction(Action<IDatabase> operation);

        int ExecuteSql(string sql, params DbParameter[] oracleParameters);

        List<T> QueryEntitiesBySql<T>(string sql, params DbParameter[] oracleParameters) where T : class;

        DataTable QueryEntitiesAsDataTable(string sql, params DbParameter[] oracleParameters);

    }
}
