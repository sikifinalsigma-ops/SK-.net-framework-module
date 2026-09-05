using Spire.Xls;
using System.Drawing.Printing;

namespace SK_Excel
{
    ///免费版（FreeSpire.XLS）前 3 页和 200 行的限制。
    public class ExcelTool
    {
        public static void ConvertExcelToPdf(string excelPath, string pdfPath)
        {
            // 1. 实例化 Workbook 对象
            Workbook workbook = new Workbook();

            // 2. 加载 Excel 文件
            workbook.LoadFromFile(excelPath);

            // 3. 设置 PDF 页面自适应（可选，防止单元格被截断）
            workbook.ConverterSetting.SheetFitToPage = true;

            // 4. 保存为 PDF
            workbook.SaveToFile(pdfPath, FileFormat.PDF);
        }

        public static void DirectPrintExcel(string excelPath, string printerName)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(excelPath);

            // 获取打印对象
            PrintDocument printDoc = workbook.PrintDocument;
            printDoc.PrinterSettings.PrinterName = printerName;
            printDoc.PrintController = new StandardPrintController(); // 静默打印

            printDoc.Print();
        }
    }


    
}
