using SaveLog;
using SK_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using YT_DataEntity.Entity;

namespace SK_WinService
{
    public class YTjob
    {
        public void TransSaleReturn()
        {
            try
            {
                using (OracleRepository db = new DatabaseConnect().DbConnect("YTOracle") as OracleRepository,db2 = new DatabaseConnect().DbConnect("YTOracleOld") as OracleRepository)
                {
                    //List<TM_MEASUREMAIN> list = db.Query<TM_MEASUREMAIN>().Where(x=>x.C_TRANSFLAG == null && x.MSRFINISHFLAG == "1" && x.NETDATETIME.CompareTo("2026-05-07") > 0).ToList();

                    List<TM_MEASUREMAIN> list = db.Query<TM_MEASUREMAIN>().Where(x => x.MEASUREDOCID == "JL202604270153").ToList();

                    foreach (var one in list) 
                    {

                        one.C_TRANSFLAG = "3";
                        one.C_TRANSTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        one.C_TRANSERPID = "123";
                        db.UpdateAndSaveEntity(one);
                    }
                                        
                }
            }
            catch (Exception e)
            {
                FileLogHelper.Error(e);
            }

        }
    }
}
