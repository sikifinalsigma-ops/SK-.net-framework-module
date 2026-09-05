using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace SK_Excel
{
    /// <summary>
    /// 仅限Excel 小于等于200行，sheet数目小于3
    /// </summary>
    public class ExcelHandle
    {
        private IWorkbook workbook = null;
        private ISheet sheet = null;

        /// <summary>
        /// 1.1 从零创建一个全新的 Excel (.xlsx) 文件
        /// </summary>
        /// <param name="sheetName">默认创建的工作表名称</param>
        public void createFile(string sheetName = "Sheet1")
        {
            workbook = new XSSFWorkbook(); // 内存中新建 .xlsx 工作簿
            sheet = workbook.CreateSheet(sheetName); // 创建默认 Sheet
        }

        public void loadFile(string templatePath)
        {
            using (FileStream fs = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fs);
            }

            // 默认加载第一个 Sheet，防止外部忘记调用 setSheet 导致空指针
            if (workbook.NumberOfSheets > 0)
            {
                sheet = workbook.GetSheetAt(0);
            }
        }

        public void writeFile(string outputPath) 
        {
            if (workbook == null)
                throw new InvalidOperationException("Workbook 未初始化，请先加载或创建 Excel 文件。");

            using (FileStream outFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(outFs);
            }            

        }

        

        public void setSheet(int sheetNum) 
        {
            if (workbook == null)
                throw new InvalidOperationException("请先调用 loadFile 加载文件。");
            sheet = workbook.GetSheetAt(sheetNum);
        }

        public ICell getCell(int rowIndex, int cellIndex) 
        {
            if (sheet == null)
                throw new InvalidOperationException("Sheet 未初始化，请先设置工作表。");

            // 行索引从 0 开始 (1 代表第 2 行)
            IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            ICell cell = row.GetCell(cellIndex) ?? row.CreateCell(cellIndex);
            return cell;
        }

        public void setCellValue(int rowIndex, int cellIndex, object cellValue)
        {
            

            ICell cell = getCell(rowIndex, cellIndex);

            if (cellValue == null || cellValue == DBNull.Value)
            {
                cell.SetCellValue(string.Empty);
                return;
            }

            if (cellValue is int || cellValue is double || cellValue is decimal || cellValue is float || cellValue is long)
                cell.SetCellValue(Convert.ToDouble(cellValue));
            else if (cellValue is DateTime)
                cell.SetCellValue((DateTime)cellValue);
            else if (cellValue is bool)
                cell.SetCellValue((bool)cellValue);
            else
                cell.SetCellValue(cellValue.ToString());
        }


        public void setCellFormula(int rowIndex, int cellIndex, string formulaValue)
        {
            ICell cell = getCell(rowIndex, cellIndex);
            // 自动容错：如果外部传了带 "=" 开头的公式，NPOI 会报错，这里自动截取掉
            if (!string.IsNullOrEmpty(formulaValue) && formulaValue.StartsWith("="))
            {
                formulaValue = formulaValue.Substring(1);
            }
            cell.SetCellFormula(formulaValue);
        }

        public void applyFormula() 
        {
            // 4. 强制触发表内公式重新计算（重要！）
            //XSSFFormulaEvaluator.EvaluateAllFormulaCells(workbook);
            if (workbook != null)
            {
                // 使用通用求值器
                XSSFFormulaEvaluator.EvaluateAllFormulaCells(workbook);
            }
        }

        
    }

}

    