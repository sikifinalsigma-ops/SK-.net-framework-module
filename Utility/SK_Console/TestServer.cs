using Server.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace SK_Console
{
    public class TestServer
    {
        public static void test() 
        {
            string aaa = JwtHelper.CreateToken("xuyan","测试");


        }
    }
}
