using SK_DataAccess;
using SK_DataEntity.ZLEnity;
using SK_Excel;
using SK_Tool;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SK_Console
{
    public static class TestExcel
    {
        public static void test()
        {
            //new class1().FillExcelTemplate();
            ExcelHandle eh = new ExcelHandle();
            eh.loadFile("Template.xlsx");
            eh.setCellValue(0, 0, "kkk1123");
            eh.writeFile("sktest.xlsx");
            ExcelTool.DirectPrintExcel("sktest.xlsx", "Hewlett-Packard HP LaserJet 3050");


        }

        public static void test2()
        {
            ExcelTool.DirectPrintExcel("Output_Order2026.xlsx", "HP Universal Printing PS (v7.9.0)");
        }

        public static void test3()
        {
            using (OracleRepository db = new DatabaseConnect().DbConnect("ZLOracle") as OracleRepository)
            {

                MES_DISPATCH_MEASURE line = db.Query<MES_DISPATCH_MEASURE>().Where(x => x.ID == "2085165912998055938").FirstOrDefault();
                ExcelHandle eh = new ExcelHandle();
                eh.loadFile("Template2.xlsx");

                eh.setCellValue(1, 1, line.THUNDERING_DATE);
                eh.setCellValue(2, 1, line.CAR_NUM);
                eh.setCellValue(3, 1, "综利公司");
                eh.setCellValue(4, 1, line.CREATE_BY);

                eh.setCellValue(1, 3, line.ENTRUST_ID);
                eh.setCellValue(2, 3, line.MATERIAL);
                eh.setCellValue(3, 3, line.RECEIVE);
                eh.setCellValue(4, 3, FormatTime.formatDateTime(DateTime.Now));

                long id = SnowflakeIdUtil.NextId();
                eh.writeFile($"{id}.xlsx");

                ExcelTool.DirectPrintExcel($"{id}.xlsx", "Hewlett-Packard HP LaserJet 3050");

            }
        }
    }
}
