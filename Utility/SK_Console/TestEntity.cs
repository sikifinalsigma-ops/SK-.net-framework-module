using SK_DataAccess;
using SK_DataAccess.Helper;
using SK_DataEntity;
using SK_DataEntity.Entity;
using SK_DataEntity.Generator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY_DataEntity.Entity;

namespace SK_Console
{
    public class TestEntity
    {
        public static void generate() 
        {
            OracleEntityGenerator.Generate(ConfigurationManager.ConnectionStrings["ZLOracle"].ConnectionString, new List<string>() { "MES_DISPATCH_MEASURE" });
        }

        public static void test() 
        {
            //using (OracleRepository db = new DatabaseConnect().DbConnect("SKOracle") as OracleRepository)
            using (OracleRepository db = new DatabaseConnect().DbConnect("WYOracle") as OracleRepository)
            {
                TB_BARRIER_BLACK_CAR one = db.Query<TB_BARRIER_BLACK_CAR>().Where(x => x.C_MAINID == "dbb07178-4679-4f76-ba3d-c11e76a9f0bc").First();
                TB_BARRIER_WHITE_CAR two = db.Query<TB_BARRIER_WHITE_CAR>().Where(x => x.C_MAINID == "b35a9e9f-807c-401c-a2d5-3d3fcf800f3c").First();
                TM_INOUTRECORD three = db.Query<TM_INOUTRECORD>().Where(x => x.C_INOUTRECORDID == "7adfcac1-bb3d-4af6-9de0-02321765eec0").First();
                TM_INOUTRECORDIDDETAIL four = db.Query<TM_INOUTRECORDIDDETAIL>().Where(x => x.C_INOUTRECORDID == "3c15e20b-ee9f-4dda-8861-cba7cbe31aaf").First();
                TB_BARRIER_INFO five = db.Query<TB_BARRIER_INFO>().Where(x => x.C_MAIND == "B0001").First();
                TM_ICCREATCARD six = db.Query<TM_ICCREATCARD>().Where(x => x.C_NOTEID == "a86d979d-06e4-4228-9b56-b7a14b1d29d9").First();
                //SK_TEST test3 = new SK_TEST() { ID = "217", NAME = "g45" };
                //db.InsertEntity(test3);

                //SK_TEST test1 = db.Query<SK_TEST>().Where(x=>x.ID == "96418").FirstOrDefault();
                //test1.NAME = "12333";
                //test1.VERSION += 1;
                //SK_TEST2 one = new SK_TEST2();
                /*
                SK_TEST2 one = db.Query<SK_TEST2>().Where(x => x.ID == 21).FirstOrDefault();
                one.NAME = "234aaa";
                */

                //db.UpdateAndSaveEntity(one);
                //db.InsertAndSaveEntity(one);
            }
            /*
            OracleEntityGenerator.Generate(ConfigurationManager.ConnectionStrings["SKOracle"].ConnectionString, new List<string>() { "SK_TEST2" });
            SqlserverHelpler sh = new SqlserverHelpler("server=192.168.10.223;database=AIS20251111095706;uid=sa;pwd=1QAZ2wsx;");
            DataTable dt = sh.ExecuteDataTable("select a.FBILLNO,b.FRecConditionId,d.FID,d.FReceiptConditionID from T_SAL_ORDER a inner join T_SAL_ORDERFIN b on a.fid = b.fid inner join T_SAL_OUTSTOCKENTRY_R c on a.FBILLNO = c.FSOORDERNO inner join T_SAL_OUTSTOCKFIN d on c.FID = d.FID where d.FRECEIPTCONDITIONID != b.FRECCONDITIONID");
            string sql = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                sql = "update T_SAL_OUTSTOCKFIN set FReceiptConditionID = " + dr[1].ToString() + " where fid = " + dr[2].ToString();
                sh.ExecuteNonQuery(sql);
            }
            OracleEntityGenerator.Generate(ConfigurationManager.ConnectionStrings["ZLOracle"].ConnectionString, new List<string>() { "MES_DISPATCH_MEASURE" });
            */
        }
        
    }
}
