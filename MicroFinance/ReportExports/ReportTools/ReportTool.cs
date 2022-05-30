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
        public static void ReportFormat1(string filePath, string p1, string p2, string p3, string p4, List<DateTime> MonthPeriods, List<ReportModel> DataList)
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
            xl_WorkSheet.Cells[1, col++] = p4;

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
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_4;

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

        public static void ReportFormat2(string filePath, List<ReportModel> DataList, string branch, string center, string customerId, string customerName, string aadhar, string GT_AC_no, 
            string samuAC, string disbursedDate, string disbursedAmount, string maturityDate, string principlePaid, string interestPaid, string ODday, string accountClosingDate, string lastPayment, string Ledgerbalance )
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
            xl_WorkSheet.Cells[1, col++] = branch;
            xl_WorkSheet.Cells[1, col++] = center;
            xl_WorkSheet.Cells[1, col++] = customerId;
            xl_WorkSheet.Cells[1, col++] = customerName;
            xl_WorkSheet.Cells[1, col++] = aadhar;
            xl_WorkSheet.Cells[1, col++] = GT_AC_no;
            xl_WorkSheet.Cells[1, col++] = samuAC;
            xl_WorkSheet.Cells[1, col++] = disbursedDate;
            xl_WorkSheet.Cells[1, col++] = disbursedAmount;
            xl_WorkSheet.Cells[1, col++] = maturityDate;
            xl_WorkSheet.Cells[1, col++] = principlePaid;
            xl_WorkSheet.Cells[1, col++] = interestPaid;
            xl_WorkSheet.Cells[1, col++] = ODday;
            xl_WorkSheet.Cells[1, col++] = accountClosingDate;
            xl_WorkSheet.Cells[1, col++] = lastPayment;
            xl_WorkSheet.Cells[1, col++] = Ledgerbalance;
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
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_4;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_5;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_6;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_7;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_8;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_9;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_10;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_11;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_12;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_13;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_14;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_15;
                xl_WorkSheet.Cells[j + 2, col++] = DataList[j].Column_16;

                int k = 0;
                while (k < DataList[j].DataList.Count)
                {
                    xl_WorkSheet.Cells[j + 2, col] = DataList[j].DataList[k].Value;
                    col++;
                    k++;
                }
            }
        }
    }
}
