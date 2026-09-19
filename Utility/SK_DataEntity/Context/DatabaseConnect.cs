using SK_DataAccess;
using SK_DataAccess.Repository;
using System.Configuration;

namespace SK_DataEntity
{
    public class DatabaseConnect
    {
        public IDatabase DbConnect(string dbName = null)
        {

            if (string.IsNullOrEmpty(dbName))
            {
                dbName = "SKOracle";
            }

            switch (ConfigurationManager.ConnectionStrings[dbName].ProviderName)
            {
                case "Oracle.ManagedDataAccess.Client":
                    return new OracleRepository(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
                case "MySql.Data.MySqlClient":
                    return new MySqlRepository(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
                default:
                    return null;
            }
        }

        
        public static Repository DbRepository(string dbName = null)
        {

            if (string.IsNullOrEmpty(dbName))
            {
                dbName = "SKOracle";
            }

            switch (ConfigurationManager.ConnectionStrings[dbName].ProviderName)
            {
                case "Oracle.ManagedDataAccess.Client":
                    return RepositoryFactory.OracleRepository(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
                case "MySql.Data.MySqlClient":
                    return RepositoryFactory.MySqlRepository(ConfigurationManager.ConnectionStrings[dbName].ConnectionString);
                default:
                    return null;
            }
        }
        
    }
}
