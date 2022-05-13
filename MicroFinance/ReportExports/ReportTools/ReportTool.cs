using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using MicroFinance.ReportExports.Models;

namespace MicroFinance.ReportExports.ReportTools
{
    public class ReportTool
    {
        public static void ReportFormat1(string filePath, string p1, string p2, string p3, List<DateTime> MonthPeriods, List<ReportModel> DataList)
        {
            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null) { }
            Excel.Workbook xl_WorkBook;
            Excel.Worksheet xl_WorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xl_WorkBook = xlApp.Workbooks.Add(misValue);
            xl_WorkSheet = (Excel.Worksheet)xl_WorkBook.Worksheets.get_Item(1);

            //======================================================================================================================

            int col = 1;
            xl_WorkSheet.Cells[1, col++] = p1;
            xl_WorkSheet.Cells[1, col++] = p2;
            xl_WorkSheet.Cells[1, col++] = p3;

            // Write Columns of period.
            int i = 0;
            while (i < MonthPeriods.Count)
            {
                string monthOf = MonthPeriods[i++].ToString("MMM-yy");
                xl_WorkSheet.Cells[1, col] = monthOf;
                col++;
            }
            // Text decoration.
            Excel.Range thisRange = xl_WorkSheet.Cells[1, col] as Excel.Range;
            thisRange.EntireRow.Font.Bold = true;
            thisRange.EntireRow.Font.Size = 12;
            thisRange.EntireRow.Interior.Color = ColorTranslator.ToOle(Color.LightGray);


            for (int j = 0; j < DataList.Count; j++)
            {
                col = 1;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_1;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_2;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_3;

                int k = 0;
                while (k < DataList[j].DataList.Count)
                {
                    xl_WorkSheet.Cells[j + 2, col] = DataList[j].DataList[k].Value;
                    col++;
                    k++;
                }
            }

            //======================================================================================================================

            xl_WorkBook.SaveAs(filePath, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xl_WorkBook.Close(true, misValue, misValue);
            Marshal.ReleaseComObject(xl_WorkBook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            Marshal.ReleaseComObject(xl_WorkSheet);
            xlApp = null;
            xl_WorkBook = null;
        }
    }
}
