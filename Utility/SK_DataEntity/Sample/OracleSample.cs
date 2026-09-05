using System.Data.Common;
using System.Linq;
using SK_DataAccess;
using SK_DataEntity.Entity;

namespace SK_DataEntity.Sample
{
    public class OracleSample
    {
        OracleRepository db = new OracleRepository("");
        public void Query() 
        {
            db.Query<SK_TEST>().Where(x=>x.ID == "123").OrderBy(x => x.ID).ThenBy(x=>x.NAME).Skip(0).Take(10).ToList();
        }

        public void Modify()
        {
            db.ExecuteInTransaction(x =>
            {
                x.InsertEntity<SK_TEST>(new SK_TEST { ID = "3", NAME = "A" });
                x.UpdateEntity<SK_TEST>(new SK_TEST { ID = "4", NAME = "B" });
                x.DeleteEntity<SK_TEST>(new SK_TEST { ID = "5", NAME = "C" });
            });
        }

        public void Sql()
        {
            var rows = db.ExecuteSql("UPDATE USERS SET NAME = :NAME WHERE ID = :ID", OracleParameterFactory.Create("NAME", "Tom"), OracleParameterFactory.Create("ID", 1));
            var users = db.QueryEntitiesBySql<SK_TEST>("SELECT ID, NAME, AGE FROM USERS WHERE AGE > :AGE", OracleParameterFactory.Create("AGE", 18));
            var user = db.QueryEntityBySql<SK_TEST>("SELECT ID, NAME, AGE FROM USERS WHERE ID = :ID", OracleParameterFactory.Create("ID", 1));
            var dt = db.QueryEntitiesAsDataTable("SELECT * FROM USERS WHERE AGE > :AGE", OracleParameterFactory.Create("AGE", 18));
        }
    }
}
