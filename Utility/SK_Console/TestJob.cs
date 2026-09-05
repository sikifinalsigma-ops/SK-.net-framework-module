using SaveLog;
using SK_DataAccess;
using SK_DataEntity.Entity;
using SK_Task;
using System;
using System.Collections.Generic;

namespace SK_Console
{
    public class TestJob
    {
        public void testMethod() 
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] DemoJob 执行");


            QuartzManager.Instance.InitializeAsync().Wait();
            QuartzManager.Instance.StartAsync().Wait();
            QuartzManager.Instance.addSimplejob("job1", "jg1", "tr1", "trg1", 5, new Dictionary<string, object>() { { "Type", "SK_Console.TestJob,SK_Console" }, { "Method", "testInsert" } }).Wait();
        }

        public void testInsert()
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect("SKOracle") as OracleRepository)
                {
                    SK_TEST test1 = new SK_TEST() { ID = new Random().Next(10000, 99999).ToString(), NAME = new Random().Next(100000, 999999).ToString(), DATETIME = DateTime.Now, NUM = 12 };
                    db.InsertAndSaveEntity(test1);

                    FileLogHelper.Info(test1.ID);
                }
            }
            catch (Exception e)
            {
                FileLogHelper.Error(e);
            }
        }

    }
}
