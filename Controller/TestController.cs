using SaveLog;
using SK_DataEntity.Entity;
using SK_DataEntity;
using SK_Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using SK_DataAccess;
using WY_DataEntity.Entity;
using Newtonsoft.Json.Linq;
using System.Web;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {

        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime LoginTime { get; set; }
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var cache = new RedisCacheHelper();

            cache.SetString("site:name", "My System", TimeSpan.FromMinutes(10));

            string siteName = cache.GetString("site:name");
            Debug.WriteLine(siteName);
            

            var user = new UserInfo
            {
                Id = 1,
                Name = "Alice",
                LoginTime = DateTime.Now
            };

            cache.Set("user:1", user, TimeSpan.FromMinutes(30));

            var userFromRedis = cache.Get<UserInfo>("user:1");
            if (userFromRedis != null)
            {
                Debug.WriteLine(userFromRedis.Name);
            }

            cache.Remove("user:1");

            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> Get2()
        {
            throw new Exception("testexception");
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IEnumerable<string> Get3()
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect("SKOracle") as OracleRepository)
                {
                    
                    SK_TEST test1 = new SK_TEST() { ID = new Random().Next(10000, 99999).ToString(), NAME = new Random().Next(100000, 999999).ToString(), DATETIME = DateTime.Now, NUM = 12 };
                    db.InsertAndSaveEntity(test1);
                    SK_TEST test2 = db.Query<SK_TEST>().Where(x => x.ID == test1.ID).OrderBy(x => x.ID).FirstOrDefault();


                    test1.NAME = "233";
                    db.UpdateAndSaveEntity(test1);

                    db.UpdateAndSaveEntityProperties(new SK_TEST() { ID = "217",NAME = new Random().Next(100, 200).ToString(), NUM = 13, DATETIME = DateTime.Today }, x=>x.NAME,x=>x.DATETIME);

                    var one = object.ReferenceEquals(test1, test2);
                    Debug.WriteLine(one);


                    db.DeleteAndSaveEntity(test1);

                    SK_TEST t1 = new SK_TEST() { ID = "xx1", NAME = "yyyy1", DATETIME = DateTime.Now, NUM = 99 };
                    SK_TEST t2 = new SK_TEST() { ID = "xx2", NAME = "yyyy1", DATETIME = DateTime.Now, NUM = 99 };
                    SK_TEST t3 = new SK_TEST() { ID = "xx3", NAME = "yyyy1", DATETIME = DateTime.Now, NUM = 99 };
                    db.ExecuteInTransaction(x=> {
                        x.InsertEntity(t1);
                        x.InsertEntity(t2);
                        x.InsertEntity(t3);
                    });

                    db.ExecuteInTransaction(x => {
                        x.DeleteEntity(t1);
                        x.DeleteEntity(t2);
                        x.DeleteEntity(t3);
                    });

                    return new string[] { "value1", "value2" };
                }
            }
            catch (Exception e)
            {
                StaticSink.SaveLog(e.Message + " " + e.StackTrace);
                throw e;
            }
        }

        [HttpGet]
        public IEnumerable<string> Get4()
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect("SKOracle") as OracleRepository)
                {
                    List<SK_TEST> sklist = db.QueryEntitiesBySql<SK_TEST>("select * from SK_TEST t WHERE T.ID LIKE :ID", OracleParameterFactory.Create("ID", "2%"));
                    DataTable dt = db.QueryEntitiesAsDataTable("select * from SK_TEST t WHERE T.ID LIKE :ID", OracleParameterFactory.Create("ID", "2%"));
                    db.ExecuteSql("DELETE FROM SK_TEST t WHERE T.ID = :ID ", OracleParameterFactory.Create("ID", "2557"));
                    db.ExecuteSql("INSERT INTO SK_TEST(ID,NAME) VALUES('2557','测速')");

                    return new string[] { "value1", "value2" };
                }
            }
            catch (Exception e)
            {
                StaticSink.SaveLog(e.Message + " " + e.StackTrace);
                throw e;
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect("SKOracle") as OracleRepository)
                {
                    SK_TEST one = new SK_TEST();
                    one.ID = Guid.NewGuid().ToString();
                    one.NAME = "123";
                    db.InsertAndSaveEntity(one);
                    return "value";
                }
            }
            catch (Exception e)
            {
                StaticSink.SaveLog(e.Message + " xxxa " + e.StackTrace);
                throw e;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public string Post([FromBody]JObject message)
        {
            FileLogHelper.Info(message.ToString());
            TM_MEASUREMAIN a1 = null;
            TM_ICCREATCARD a2 = null;
            using (OracleRepository db = new DatabaseConnect().DbConnect("WYOracle") as OracleRepository)
            {
                a1 = db.Query<TM_MEASUREMAIN>().Where(t => t.C_MEASUREDOCID == "WY2026081200029").FirstOrDefault();
                a2 = db.Query<TM_ICCREATCARD>().Where(x => x.C_NOTEID == "06a68ad0-b711-410e-be02-65bddb22fe3d").First();
            }
            
            return a1.C_MEASUREDOCID + a2.C_CARID;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Post2([FromBody] JObject message)
        {
            FileLogHelper.Info(message.ToString());
            TM_MEASUREMAIN a1 = null;
            TB_BARRIER_INFO a2 = null;
            using (OracleRepository db = new DatabaseConnect().DbConnect("WYOracle") as OracleRepository)
            {
                a2 = db.Query<TB_BARRIER_INFO>().Where(x => x.C_MAIND == "B0011").First();
            }

            return a2.C_IP;
        }


        [AllowAnonymous]
        [HttpPost]
        //[HttpGet]
        [Route("")] //根路径
        public string MainIntro()
        {
            string ip = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            FileLogHelper.Info(ip);
            return "akkk";
        }
    }

    
}