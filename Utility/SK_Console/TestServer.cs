using Server.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using SK_Encrypt;

namespace SK_Console
{
    public class TestServer
    {
        public static void test() 
        {
            //password = cyberdragonnova
            //sha256password = cffcd4294c8d31997f6283eb6cd775d6953c33c6d0be0b3e8925db643e4a800c
            //token = eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMTIyMzMiLCJqdGkiOiIwZDgxNDQyNy0yZjQxLTRlYTQtYjBiMS01ODNiYTBhM2M1ZmYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6InNrdGVzdCIsIm5iZiI6MTc5MzU4NzgxNywiZXhwIjoyNDI0MzA3ODE3LCJpc3MiOiJTS3dlYmFwaSIsImF1ZCI6IlNLdXNlciJ9.uEyLQsKeBXTxBeOvOQkPcVi9MoY_Oane23PzFM1kG_4
            string aaa = JwtHelper.CreateToken("112233", "sktest", "测试用户",null,60*24*365*20);


        }
    }
}
