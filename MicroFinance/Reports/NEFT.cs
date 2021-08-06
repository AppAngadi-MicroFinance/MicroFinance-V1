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
    public class NEFT : BindableBase
    {
        private string _remitteraccno= "09876554as";
        public string RemitterAccNo
        {
            get
            {
                return _remitteraccno;
            }
            set
            {
                _remitteraccno = "09876554as";
            }
        }
        private string _remittername = "G-Trust";
        public string RemitterName
        {
            get
            {
                return _remittername;
            }
            set
            {
                _remittername = "G-Trust";
            }
        }
        private string _remitteraddress="Trichy";
        public string RemitterAddress
        {
            get
            {
                return _remitteraddress;
            }
            set
            {
                _remitteraddress = value;
            }
        }
        private string _benificiaryacc;
        public string BenificiaryAcc
        {
            get
            {
                return _benificiaryacc;
            }
            set
            {
                _benificiaryacc = value;

            }
        }
        private string _benificiaryname;
        public string BenificiaryName
        {
            get
            {
                return _benificiaryname;
            }
            set
            {
                _benificiaryname = value;
            }
        }
        private string _benificiaryaddress;
        public string BenificiaryAddress
        {
            get
            {
                return _benificiaryaddress;
            }
            set
            {
                _benificiaryaddress = value;
            }
        }
        private string _benificiaryifsc;
        public string BenificiaryIFSC
        {
            get
            {
                return _benificiaryifsc;
            }
            set
            {
                _benificiaryifsc = value;
            }
        }
        private string _paymentdetails;
        public string PaymentDetails
        {
            get
            {
                return _paymentdetails;
            }
            set
            {
                _paymentdetails = value;
            }
        }
        private string _recievercode;
        public string RecieverCode
        {
            get
            {
                return _recievercode;
            }
            set
            {
                _recievercode = value;
            }
        }
        private string _remitteremail;
        public string RemitterEmail
        {
            get
            {
                return _remitteremail;
            }
            set
            {
                _remitteremail = value;
            }
        }
        private string _refno;
        public string RefNo
        {
            get
            {
                return _refno;
            }
            set
            {
                _refno = value;
            }
        }
        private string _amount;
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        public NEFT() { }
        public NEFT( string benificiaryacc, string benificiaryname,
            string benificiaryifsc, string paymentdetails, string recievercode, string refno, string amount)
        {
            BenificiaryAcc = benificiaryacc;
            BenificiaryName = benificiaryname;
            BenificiaryIFSC = benificiaryifsc;
            PaymentDetails = paymentdetails;
            RecieverCode = recievercode;
            RefNo = refno;
            Amount = amount;
        }
        LoanProcess loandetails = new LoanProcess();


        public void GenerateNEFT_File(List<LoanProcess> loanList)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            //S.No.
            var no = xlWorkSheet.Cells.Range["A1"];
            no.Value = "S.NO.";
            no.Font.Bold = true;
            no.Font.Size = 11;
            no.RowHeight = 46;
            no.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            no.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Remitter Accoutn no
            var remitterAccNo = xlWorkSheet.Cells.Range["B1"];
            remitterAccNo.Value = "Remitter Account No.";
            remitterAccNo.Font.Bold = true;
            remitterAccNo.Font.Size = 11;
            remitterAccNo.RowHeight = 46;
            remitterAccNo.ColumnWidth = 24;
            remitterAccNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            remitterAccNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Remitter Name
            var remitterName = xlWorkSheet.Cells.Range["C1"];
            remitterName.Value = "Remitter Name ";
            remitterName.Font.Bold = true;
            remitterName.Font.Size = 11;
            remitterName.RowHeight = 46;
            remitterName.ColumnWidth = 24;
            remitterName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            remitterName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Remitter Address
            var remitteraddress = xlWorkSheet.Cells.Range["D1"];
            remitteraddress.Value = "Remitter Address";
            remitteraddress.Font.Bold = true;
            remitteraddress.Font.Size = 11;
            remitteraddress.RowHeight = 46;
            remitteraddress.ColumnWidth = 24;
            remitteraddress.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            remitteraddress.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Benificiary Account NO
            var BenificiaryaccNO = xlWorkSheet.Cells.Range["E1"];
            BenificiaryaccNO.Value = "Benificiary Account No";
            BenificiaryaccNO.Font.Bold = true;
            BenificiaryaccNO.Font.Size = 11;
            BenificiaryaccNO.RowHeight = 46;
            BenificiaryaccNO.ColumnWidth = 24;
            BenificiaryaccNO.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BenificiaryaccNO.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Benificiary Name
            var Benificiaryname = xlWorkSheet.Cells.Range["F1"];
            Benificiaryname.Value = "Benificiary Name";
            Benificiaryname.Font.Bold = true;
            Benificiaryname.Font.Size = 11;
            Benificiaryname.RowHeight = 46;
            Benificiaryname.ColumnWidth = 24;
            Benificiaryname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Benificiaryname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Benificiary Address
            var Benificiaryaddress = xlWorkSheet.Cells.Range["G1"];
            Benificiaryaddress.Value = "Benificiary Address";
            Benificiaryaddress.Font.Bold = true;
            Benificiaryaddress.Font.Size = 11;
            Benificiaryaddress.RowHeight = 46;
            Benificiaryaddress.ColumnWidth = 24;
            Benificiaryaddress.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Benificiaryaddress.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Benificiary IFSC
            var BenificiaryIFSC = xlWorkSheet.Cells.Range["H1"];
            BenificiaryIFSC.Value = "Benificiary IFSC";
            BenificiaryIFSC.Font.Bold = true;
            BenificiaryIFSC.Font.Size = 11;
            BenificiaryIFSC.RowHeight = 46;
            BenificiaryIFSC.ColumnWidth = 24;
            BenificiaryIFSC.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BenificiaryIFSC.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Payment details
            var Paymentdetails = xlWorkSheet.Cells.Range["I1"];
            Paymentdetails.Value = "Payment Details";
            Paymentdetails.Font.Bold = true;
            Paymentdetails.Font.Size = 11;
            Paymentdetails.RowHeight = 46;
            Paymentdetails.ColumnWidth = 24;
            Paymentdetails.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Paymentdetails.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Sender to Receiver Code
            var ReceiverCode = xlWorkSheet.Cells.Range["J1"];
            ReceiverCode.Value = " Sender to Receiver Code(ATTN/FAST/URGENT/DETAIL/NREAC) ";
            ReceiverCode.Font.Bold = true;
            ReceiverCode.Font.Size = 11;
            ReceiverCode.RowHeight = 46;
            ReceiverCode.ColumnWidth = 24;
            ReceiverCode.WrapText = true;
            ReceiverCode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ReceiverCode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Remitter Email
            var RemitterMail = xlWorkSheet.Cells.Range["K1"];
            RemitterMail.Value = "Remitter Email";
            RemitterMail.Font.Bold = true;
            RemitterMail.Font.Size = 11;
            RemitterMail.RowHeight = 46;
            RemitterMail.ColumnWidth = 24;
            RemitterMail.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            RemitterMail.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            // Ref No
            var Refno = xlWorkSheet.Cells.Range["L1"];
            Refno.Value = "Ref No";
            Refno.Font.Bold = true;
            Refno.Font.Size = 11;
            Refno.RowHeight = 46;
            Refno.ColumnWidth = 24;
            Refno.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Refno.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            // Amount
            var amount = xlWorkSheet.Cells.Range["M1"];
            amount.Value = "Amount";
            amount.Font.Bold = true;
            amount.Font.Size = 11;
            amount.RowHeight = 46;
            amount.ColumnWidth = 24;
            amount.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            amount.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            FillNEFT(xlWorkSheet, loanList);
            try
            {
                string dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report\\NEFT Report");
                if (Directory.Exists(dir))
                {
                    string FileName = dir + "\\NEFT_" + DateTime.Now.ToString("dd-MM-yyyy hh-mm") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    string FileName = dir + "\\NEFT_" + DateTime.Now.ToString() + ".xlsx";
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

        double DoucumentationCharge = 2;
        int Insurence = 50;
        double InsurenCharge = 3;

        public void FillNEFT(Worksheet xlWorkSheet, List<LoanProcess> loans)
        {
            int i = 1;
            int RowStart = 2;
            foreach (LoanProcess x in loans)
            {
                // Gtrust Account Details
                xlWorkSheet.Cells[RowStart, 1] = i;
                xlWorkSheet.Cells[RowStart, 2] = RemitterAccNo;
                xlWorkSheet.Cells[RowStart, 3] = RemitterName;
                xlWorkSheet.Cells[RowStart, 4] = RemitterAddress;

                //Customer Account Detials
                xlWorkSheet.Cells[RowStart, 5] = x.AccountNumber;
                xlWorkSheet.Cells[RowStart, 6] = x.AccountHolder;
                xlWorkSheet.Cells[RowStart, 7] = x.DoorNumber + "," + x.StreetName+x.City+","+x.State;
                xlWorkSheet.Cells[RowStart, 8] = x.IFSCCode;
                xlWorkSheet.Cells[RowStart, 10] =  "Fast";
                xlWorkSheet.Cells[RowStart, 11] = "gtrust@gmail.com";
                xlWorkSheet.Cells[RowStart, 12] = RefNo;
                xlWorkSheet.Cells[RowStart, 13] =NetAmountCal(x.LoanAmount,DoucumentationCharge,Insurence,InsurenCharge);
                RowStart++;
                i++;
            }
        }


        public double NetAmountCal(int amount,double Doc_Charge,int insurence,double Ins_Charge)
        {
            return (amount - (amount * Doc_Charge / 100) - (insurence * Ins_Charge / 100)) - insurence;
        }
        public static void Cusid()
        {
            SqlConnection sqlcon = new SqlConnection(MicroFinance.Properties.Settings.Default.db);
            sqlcon.Open();
            if (sqlcon.State == ConnectionState.Open)
            { 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlcon;
                cmd.CommandText = "select BankAccountNo,BankACHolderName,Address,IFSCCode from CustomerDetails where CustId in (select CustId from LoanApplication where LoanStatus = 10)";
                SqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    //CusID.Add();
                }

            }
            sqlcon.Close();
        }
    }
}


