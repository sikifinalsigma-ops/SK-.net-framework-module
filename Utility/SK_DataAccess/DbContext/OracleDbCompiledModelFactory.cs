using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace SK_DataAccess
{
    public static class OracleDbCompiledModelFactory
    {
        private static readonly Lazy<DbCompiledModel> _compiledModel
            = new Lazy<DbCompiledModel>(CreateModel);

        public static DbCompiledModel Instance => _compiledModel.Value;

        private static DbCompiledModel CreateModel()
        {
            var builder = new DbModelBuilder();

            // 自动注册实体
            foreach (var entityType in EntityRegister.registerEntity)
            {
                builder.RegisterEntityType(entityType);
            }

            var providerInfo = new DbProviderInfo("Oracle.ManagedDataAccess.Client", "12.2.0.0");

            var model = builder.Build(providerInfo);
            return model.Compile();
        }
    }
}
