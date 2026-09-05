using SK_DataEntity.Generator;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace Server.Controller
{
    public class EntityGeneratorController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GenerateTable([FromBody] GenerateTableMessage msg)
        {
            switch (ConfigurationManager.ConnectionStrings[msg.databaseName].ProviderName)
            {
                case "Oracle.ManagedDataAccess.Client":
                    OracleEntityGenerator.Generate(ConfigurationManager.ConnectionStrings[msg.databaseName].ConnectionString,msg.tableName);
                    break;
                default:
                    break;
            }
            return Ok("生成成功。");
        }


        public class GenerateTableMessage 
        {
            public string databaseName { get; set; }

            public List<string> tableName { get; set; }
        }
    }
}