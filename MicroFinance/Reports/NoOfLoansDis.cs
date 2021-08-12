using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using MicroFinance.Modal;
using Microsoft.Office.Interop.Excel;

namespace MicroFinance.Reports
{
    class NoOfLoansDis
    {
        public void GenerateLoanDis_File()
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Cells.WrapText = true;
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            //Date
            var date = xlWorkSheet.Cells.Range["A1"];
            date.Value = "Date";
            date.Font.Bold = true;
            date.Font.Size = 11;
            date.RowHeight = 46;
            date.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            date.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //No.of Loan
            var noofloans = xlWorkSheet.Cells.Range["B1"];
            noofloans.Value = "No.of Loans";
            noofloans.Font.Bold = true;
            noofloans.Font.Size = 11;
            noofloans.RowHeight = 46;
            noofloans.ColumnWidth = 6;
            noofloans.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            noofloans.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Amount
            var loanAmount = xlWorkSheet.Cells.Range["C1"];
            loanAmount.Value = "Loan Amount";
            loanAmount.Font.Bold = true;
            loanAmount.Font.Size = 11;
            loanAmount.RowHeight = 46;
            loanAmount.ColumnWidth = 11;
            loanAmount.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanAmount.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Total Loan Amount
            var TotalAmount = xlWorkSheet.Cells.Range["D1"];
            TotalAmount.Value = "Total Loan Amount";
            TotalAmount.Font.Bold = true;
            TotalAmount.Font.Size = 11;
            TotalAmount.RowHeight = 46;
            TotalAmount.ColumnWidth = 11;
            TotalAmount.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            TotalAmount.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //1% LLP
            var LLP = xlWorkSheet.Cells.Range["E1"];
            LLP.Value = "1% LLP";
            LLP.Font.Bold = true;
            LLP.Font.Size = 11;
            LLP.RowHeight = 46;
            LLP.ColumnWidth = 7;
            LLP.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            LLP.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //GST
            var GST = xlWorkSheet.Cells.Range["F1"];
            GST.Value = "GST";
            GST.Font.Bold = true;
            GST.Font.Size = 11;
            GST.RowHeight = 46;
            GST.ColumnWidth = 7;
            GST.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            GST.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //To Samunnati 
            var toSamunnati = xlWorkSheet.Cells.Range["G1"];
            toSamunnati.Value = "To Samunnati";
            toSamunnati.Font.Bold = true;
            toSamunnati.Font.Size = 11;
            toSamunnati.RowHeight = 46;
            toSamunnati.ColumnWidth = 10;
            toSamunnati.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            toSamunnati.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Samunnati To GT
            var SamTogGT = xlWorkSheet.Cells.Range["H1"];
            SamTogGT.Value = "Samunnati To GT";
            SamTogGT.Font.Bold = true;
            SamTogGT.Font.Size = 11;
            SamTogGT.RowHeight = 46;
            SamTogGT.ColumnWidth = 11;
            SamTogGT.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            SamTogGT.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance 225
            var Ins = xlWorkSheet.Cells.Range["I1"];
            Ins.Value = "Insurance 225";
            Ins.Font.Bold = true;
            Ins.Font.Size = 11;
            Ins.RowHeight = 46;
            Ins.ColumnWidth = 9;
            Ins.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Ins.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Member Subscription 300
            var sub = xlWorkSheet.Cells.Range["J1"];
            sub.Value = "Member Subscription 300";
            sub.Font.Bold = true;
            sub.Font.Size = 11;
            sub.RowHeight = 46;
            sub.ColumnWidth = 11;
            sub.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            sub.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Disbursement NEFT
            var disb = xlWorkSheet.Cells.Range["K1"];
            disb.Value = "Loan Disburdement NEFT(SBI)";
            disb.Font.Bold = true;
            disb.Font.Size = 11;
            disb.RowHeight = 46;
            disb.ColumnWidth = 14;
            disb.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            disb.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Trf to from sbi GT BOM
            var BOM = xlWorkSheet.Cells.Range["L1"];
            BOM.Value = "Trf to from SBI GT BOM";
            BOM.Font.Bold = true;
            BOM.Font.Size = 11;
            BOM.RowHeight = 46;
            BOM.ColumnWidth = 11;
            BOM.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BOM.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            try
            {
                string dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report\\Loan Disbursement Report");
                if (Directory.Exists(dir))
                {
                    string FileName = dir + "\\LoanDis_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    string FileName = dir + "\\LoanDis_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                xlWorkBook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
                xlWorkBook = null;
                File.Delete(@"D:\temp.xlsx");
            }

        }

        public void FillLoanDis(Worksheet xlWorkSheet, List<SamunnatiResult> loans)
        {
            int LLP = 1;
            int GST = 18;
            int RowStart = 2;
            foreach (SamunnatiResult x in loans)
            {
                xlWorkSheet.Cells[RowStart, 1] = x.LoanStartDate;
                xlWorkSheet.Cells[RowStart, 2] = "";
                xlWorkSheet.Cells[RowStart, 3] = "";
                xlWorkSheet.Cells[RowStart, 4] = xlWorkSheet.Cells[RowStart, 2].Value * xlWorkSheet.Cells[RowStart, 3].Value;
                xlWorkSheet.Cells[RowStart, 5] = (xlWorkSheet.Cells[RowStart, 4].Value * LLP / 100);
                xlWorkSheet.Cells[RowStart, 6] = (xlWorkSheet.Cells[RowStart, 5].Value * GST / 100);
                xlWorkSheet.Cells[RowStart, 7] = (xlWorkSheet.Cells[RowStart, 5].Value + xlWorkSheet.Cells[RowStart, 6].Value);
                xlWorkSheet.Cells[RowStart, 8] = xlWorkSheet.Cells[RowStart, 4].Value - xlWorkSheet.Cells[RowStart, 7].Value;
                xlWorkSheet.Cells[RowStart, 9] = xlWorkSheet.Cells[RowStart, 2].Value * 225;
                xlWorkSheet.Cells[RowStart, 10] = xlWorkSheet.Cells[RowStart, 2].Value * 300;
                xlWorkSheet.Cells[RowStart, 11] = xlWorkSheet.Cells[RowStart, 8].Value - xlWorkSheet.Cells[RowStart, 9].Value - xlWorkSheet.Cells[RowStart, 10].Value;
                xlWorkSheet.Cells[RowStart, 12] = 0;
                RowStart++;
            }
        }


     

    }
}