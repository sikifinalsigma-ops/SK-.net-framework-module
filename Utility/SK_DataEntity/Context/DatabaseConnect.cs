using SK_DataAccess;
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
                default:
                    return null;
            }
        }
    }
}
