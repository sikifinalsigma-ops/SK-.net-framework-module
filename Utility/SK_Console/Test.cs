using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK_FileStream;
namespace SK_Console
{
    public class Test
    {
        public static void test()
        {
            for (int i=0;i<20;i++) 
            {
                FileTool.writeTxtToFile($"{AppDomain.CurrentDomain.BaseDirectory}/test.txt", $"{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}");
            }
            
        }
    }
}
