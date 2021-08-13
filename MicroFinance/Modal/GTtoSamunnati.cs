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

namespace MicroFinance.Modal
{
    class GTtoSamunnati
    {
        private string _operatingUnit;
        public string OperatingUnit
        {
            get
            {
                return OperatingUnit;
            }
            set
            {
                _operatingUnit = value;
            }
        }
        private string _customernumber;
        public string CustomerNumber
        {
            get
            {
                return _customernumber;
            }
            set
            {
                _customernumber = value;
            }
        }
        private string _reportid;
        public string ReportID
        {
            get
            {
                return _reportid;
            }
            set
            {
                _reportid = value;
            }
        }
        private string _hmreferenceNo;
        public string HMRefernceNo
        {
            get
            {
               return _hmreferenceNo;
            }
            set
            {
                _hmreferenceNo = value;
            }
        }
        private string _customername;
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                _customername = value;
            }
        }
        private string _samunnatiBranchMapping;
        public string SamunnatiBranchMapping
        {
            get
            {
                return _samunnatiBranchMapping;
            }
            set
            {
                _samunnatiBranchMapping = value;
            }
        }
        private string _customerType;
        public string CustomerType
        {
            get
            {
                return _customerType;
            }
            set
            {
                _customerType = value;
            }
        }
        private string _customerSubtype;
        public string CustomerSubtype
        {
            get
            {
                return _customerSubtype;
            }
            set
            {
                _customerSubtype = value;
            }
        }
        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }
        private string _dob;
        public string DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
            }
        }
        private string _businessCategory;
        public string BusinessCategory
        {
            get
            {
                return _businessCategory;
            }
            set
            {
                _businessCategory = value;
            }
        }
        private string _busniessIndustryType;
        public string BusinessIndustryType
        {
            get
            {
                return _busniessIndustryType;
            }
            set
            {
                _busniessIndustryType = value;
            }
        }
        private string _sector;
        public string Sector
        {
            get
            {
                return _sector;
            }
            set
            {
                _sector = value;
            }
        }
        private string _primaryValueChain;
        public string PrimaryValueChain
        {
            get
            {
                return _primaryValueChain;
            }
            set
            {
                _primaryValueChain = value;
            }
        }
        private string _secondaryValueChain;
        public string SecondaryValueChain
        {
            get
            {
                return _secondaryValueChain;
            }
            set
            {
                _secondaryValueChain = value;
            }
        }
        private string _phoneNo;
        public string Phone
        {
            get
            {
                return _phoneNo;
            }
            set
            {
                _phoneNo = value;
            }
        }
        private string _address1;
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
            }
        }
        private string _address2;
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value; 
            }
        }
        private string _address3;
        public string Address3
        {
            get
            {
                return _address3;
            }
            set
            {
                _address3 = value;
            }
        }
        private string _address4;
        public string Address4
        {
            get
            {
                return _address4;
            }
            set
            {
                _address4 = value;
            }
        }
        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }
        private string _postalCode;
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                _postalCode = value;
            }
        }
        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        private string _emailID;
        public string EmailID
        {
            get
            {
                return _emailID;
            }
            set
            {
                _emailID = value;
            }
        }
        private string _constitution;
        public string Constitution
        {
            get
            {
                return _constitution;
            }
            set
            {
                _constitution = value;
            }
        }
        private string _gstinNO;
        public string GSTINno
        {
            get
            {
                return _gstinNO;
            }
            set
            {
                _gstinNO = value;
            }
        }
        private string _panTanNo;
        public string PANTANno
        {
            get
            {
                return _panTanNo;
            }
            set
            {
                _panTanNo = value;
            }
        }
        private string _aadharNO;
        public string AadharNo
        {
            get
            {
                return _aadharNO;
            }
            set
            {
                _aadharNO = value;
            }
        }
        private string _existingRelationshipValue;
        public string ExistingRelationshipValue
        {
            get
            {
                return _existingRelationshipValue;
            }
            set
            {
                _existingRelationshipValue = value;
            }
        }
        private string _newCustomerAcquisition;
        public string NewCustomerAcquisition
        {
            get
            {
                return _newCustomerAcquisition;
            }
            set
            {
                _newCustomerAcquisition = value;
            }
        }
        private string _bankName;
        public string BankName
        {
            get
            {
                return _bankName;
            }
            set
            {
                _bankName = value;
            }
        }
        private string _bankbranchname;
        public string BankBranchName
        {
            get
            {
                return _bankbranchname;
            }
            set
            {
                _bankbranchname = value;
            }
        }
        private string _ifscCode;
        public string IFSCcode
        {
            get
            {
                return _ifscCode;
            }
            set
            {
                _ifscCode = value;
            }
        }
        private string _bankaccName;
        public string BankAccName
        {
            get
            {
                return _bankaccName;
            }
            set
            {
                _bankaccName = value;
            }
        }
        private string _bankacclevel;
        public string BankAccLevel
        {
            get
            {
                return _bankacclevel;
            }
            set
            {
                _bankacclevel = value;
            }
        }
        private string _bankAccNO;
        public string BankAccNo
        {
            get
            {
                return _bankAccNO;
            }
            set
            {
                _bankAccNO = value;
            }
        }
        private string _salespersonName;
        public string SalesPesonName
        {
            get
            {
                return _salespersonName;
            }
            set
            {
                _salespersonName = value;
            }
        }
        private string _loanProduct;
        public string LoanProduct
        {
            get
            {
                return _loanProduct;
            }
            set
            {
                _loanProduct = value;
            }
        }
        private int _loanamount;
        public int LoanAmount
        {
            get
            {
                return _loanamount;
            }
            set
            {
                _loanamount = value;
            }
        }
        private string _loantenure;
        public string LoanTenure
        {
            get
            {
                return _loantenure;
            }
            set
            {
                _loantenure = value;
            }
        }
        private string _loantermperiod;
        public string LoanTermPeriod
        {
            get
            {
                return _loantermperiod;
            }
            set
            {
                _loantermperiod = value;
            }
        }
        private string _loantype;
        public string LoanType
        {
            get
            {
                return _loantype;
            }
            set
            {
                _loantype = value;
            }
        }
        private string _loanstartdate;
        public string LoanStartDate
        {
            get
            {
                return _loanstartdate;
            }
            set
            {
                _loanstartdate = value;
            }
        }
        private string _amortizationMethod;
        public string AmortizationMethod
        {
            get
            {
                return _amortizationMethod;
            }
            set
            {
                _amortizationMethod = value;
            }
        }
        private string _paymentStatedate;
        public string PaymentStartDate
        {
            get
            {
                return _paymentStatedate;
            }
            set
            {
                _paymentStatedate = value;
            }
        }
        private string _paymentfrequency;
        public string PaymentFrequency
        {
            get
            {
                return _paymentfrequency;
            }
            set
            {
                _paymentfrequency = value;
            }
        }
        private string _intreststartdate;
        public string IntrestStartDate
        {
            get
            {
                return _intreststartdate;
            }
            set
            {
                _intreststartdate = value;
            }
        }
        private string _intrestfrequency;
        public string IntrestFrequency
        {
            get
            {
                return _intrestfrequency;
            }
            set
            {
                _intrestfrequency = value;
            }
        }
        private string _loanintrestrate;
        public string LoanIntrestRate
        {
            get
            {
                return _loanintrestrate;
            }
            set
            {
                _loanintrestrate = value;
            }
        }
        private string _processingFeesPercentage;
        public string ProcessingFeesPercentage
        {
            get
            {
                return _processingFeesPercentage;
            }
            set
            {
                _processingFeesPercentage = value;
            }
        }
        private string _insuranceFees1;
        public string InsuranceFees1
        {
            get
            {
                return _insuranceFees1;
            }
            set
            {
                _insuranceFees1 = value;
            }
        }
        private string _insuranceFees1amt;
        public string InsuranceFees1Amt
        {
            get
            {
                return _insuranceFees1amt;
            }
            set
            {
                _insuranceFees1amt = value;
            }
        }
        private string _insuranceFees2;
        public string InsuranceFees2
        {
            get
            {
                return _insuranceFees2;
            }
            set
            {
                _insuranceFees2 = value;
            }
        }
        private string _insuranceFees2amt;
        public string InsuranceFees2Amt
        {
            get
            {
                return _insuranceFees2amt;
            }
            set
            {
                _insuranceFees2amt = value;
            }
        }
        private string _penalinterestrate;
        private string PenalIntrestRate
        {
            get
            {
                return _penalinterestrate;
            }
            set
            {
                _penalinterestrate = value;
            }
        }




        //public List<SamunnatiResult> samunnatiList = new List<SamunnatiResult>();
        //public List<String> dates = new List<String>();
        //public List<int> Amt = new List<int>();

        //public void GetDetails(string Path)
        //{
        //    Excel.Application application;
        //    Excel.Workbook workbook;
        //    StringBuilder sb = new StringBuilder();
        //    int SC;
        //    string Sheetname = null;

        //    application = new Excel.Application();
        //    workbook = application.Workbooks.Open(Path);

        //    SC = workbook.Worksheets.Count;
        //    foreach (Excel.Worksheet sheet in workbook.Worksheets)
        //    {
        //        if (sheet.Name.Equals("Sheet2", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            Excel.Range usedrange = sheet.UsedRange;
        //            Sheetname = sheet.Name;
        //            //GetDataFromExcel(sheet);
        //            break;
        //        }

        //    }
        //    workbook.Close();
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //    application.Quit();
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

        //}

        //public void GetDataFromExcel(Excel.Worksheet worksheet)
        //{
        //    Excel.Range userange = worksheet.UsedRange;
        //    int rowcount = userange.Rows.Count;
        //    int loandatecolumn = 0;
        //    foreach (DataRow Row in worksheet.Rows)
        //    {
        //        loandatecolumn++;
        //        if (Row["Loan Start Date"].ToString() == "Loan Start Date")
        //        {
        //            break;
        //        }
        //    }

        //    for (int Rownumber = 2; Rownumber <= rowcount; Rownumber++)
        //    {
        //        string _date = (worksheet.Cells[Rownumber, loandatecolumn] as Excel.Range).Value;
        //        int _amount = (int)(worksheet.Cells[Rownumber, 39] as Excel.Range).Value;
        //        samunnatiList.Add(
        //            new SamunnatiResult
        //            {
        //                OperatingUnit = (worksheet.Cells[Rownumber, 1] as Excel.Range).Value,
        //                CustomerNumber = (worksheet.Cells[Rownumber, 2] as Excel.Range).Value,
        //                ReportID = (worksheet.Cells[Rownumber, 3] as Excel.Range).Value,
        //                HMRefernceNo = (worksheet.Cells[Rownumber, 4] as Excel.Range).Value,
        //                CustomerName = (worksheet.Cells[Rownumber, 5] as Excel.Range).Value,
        //                SamunnatiBranchMapping = (worksheet.Cells[Rownumber, 6] as Excel.Range).Value,
        //                CustomerType = (worksheet.Cells[Rownumber, 7] as Excel.Range).Value,
        //                CustomerSubtype = (worksheet.Cells[Rownumber, 8] as Excel.Range).Value,
        //                Gender = (worksheet.Cells[Rownumber, 9] as Excel.Range).Value,
        //                DOB = (worksheet.Cells[Rownumber, 10] as Excel.Range).Value,
        //                BusinessCategory = (worksheet.Cells[Rownumber, 11] as Excel.Range).Value,
        //                BusinessIndustryType = (worksheet.Cells[Rownumber, 12] as Excel.Range).Value,
        //                Sector = (worksheet.Cells[Rownumber, 13] as Excel.Range).Value,
        //                PrimaryValueChain = (worksheet.Cells[Rownumber, 14] as Excel.Range).Value,
        //                SecondaryValueChain = (worksheet.Cells[Rownumber, 15] as Excel.Range).Value,
        //                Phone = (worksheet.Cells[Rownumber, 16] as Excel.Range).Value,
        //                Address1 = (worksheet.Cells[Rownumber, 17] as Excel.Range).Value,
        //                Address2 = (worksheet.Cells[Rownumber, 18] as Excel.Range).Value,
        //                Address3 = (worksheet.Cells[Rownumber, 19] as Excel.Range).Value,
        //                Address4 = (worksheet.Cells[Rownumber, 20] as Excel.Range).Value,
        //                City = (worksheet.Cells[Rownumber, 21] as Excel.Range).Value,
        //                PostalCode = (worksheet.Cells[Rownumber, 22] as Excel.Range).Value,
        //                State = (worksheet.Cells[Rownumber, 23] as Excel.Range).Value,
        //                EmailID = (worksheet.Cells[Rownumber, 24] as Excel.Range).Value,
        //                Constitution = (worksheet.Cells[Rownumber, 25] as Excel.Range).Value,
        //                GSTINno = (worksheet.Cells[Rownumber, 26] as Excel.Range).Value,
        //                PANTANno = (worksheet.Cells[Rownumber, 27] as Excel.Range).Value,
        //                AadharNo = (worksheet.Cells[Rownumber, 28] as Excel.Range).Value,
        //                ExistingRelationshipValue = (worksheet.Cells[Rownumber, 29] as Excel.Range).Value,
        //                NewCustomerAcquisition = (worksheet.Cells[Rownumber, 30] as Excel.Range).Value,
        //                BankName = (worksheet.Cells[Rownumber, 31] as Excel.Range).Value,
        //                BankBranchName = (worksheet.Cells[Rownumber, 32] as Excel.Range).Value,
        //                IFSCcode = (worksheet.Cells[Rownumber, 33] as Excel.Range).Value,
        //                BankAccName = (worksheet.Cells[Rownumber, 34] as Excel.Range).Value,
        //                BankAccLevel = (worksheet.Cells[Rownumber, 35] as Excel.Range).Value,
        //                BankAccNo = (worksheet.Cells[Rownumber, 36] as Excel.Range).Value,
        //                SalesPesonName = (worksheet.Cells[Rownumber, 37] as Excel.Range).Value,
        //                LoanProduct = (worksheet.Cells[Rownumber, 38] as Excel.Range).Value,
        //                LoanAmount = (int)(worksheet.Cells[Rownumber, 39] as Excel.Range).Value,
        //                LoanTenure = (worksheet.Cells[Rownumber, 40] as Excel.Range).Value,
        //                LoanTermPeriod = (worksheet.Cells[Rownumber, 41] as Excel.Range).Value,
        //                LoanType = (worksheet.Cells[Rownumber, 42] as Excel.Range).Value,
        //                LoanStartDate = (worksheet.Cells[Rownumber, 43] as Excel.Range).Value,
        //                AmortizationMethod = (worksheet.Cells[Rownumber, 44] as Excel.Range).Value,
        //                PaymentStartDate = (worksheet.Cells[Rownumber, 45] as Excel.Range).Value,
        //                PaymentFrequency = (worksheet.Cells[Rownumber, 46] as Excel.Range).Value,
        //                IntrestStartDate = (worksheet.Cells[Rownumber, 47] as Excel.Range).Value,
        //                IntrestFrequency = (worksheet.Cells[Rownumber, 48] as Excel.Range).Value,
        //                LoanIntrestRate = (worksheet.Cells[Rownumber, 49] as Excel.Range).Value,
        //                ProcessingFeesPercentage = (worksheet.Cells[Rownumber, 50] as Excel.Range).Value,
        //                InsuranceFees1 = (worksheet.Cells[Rownumber, 51] as Excel.Range).Value,
        //                InsuranceFees1Amt = (worksheet.Cells[Rownumber, 52] as Excel.Range).Value,
        //                InsuranceFees2 = (worksheet.Cells[Rownumber, 53] as Excel.Range).Value,
        //                InsuranceFees2Amt = (worksheet.Cells[Rownumber, 54] as Excel.Range).Value,
        //                PenalIntrestRate = (worksheet.Cells[Rownumber, 55] as Excel.Range).Value,
        //            }); 
        //        if (!dates.Contains(_date))
        //        {
        //            dates.Add(_date);
        //        }
        //        if (!Amt.Contains(_amount))
        //        {
        //            Amt.Add(Convert.ToInt32(_amount));
        //        }
        //    }
        //}



        public void GenerateSamunnati_File()
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
            //Operating Unit
            var operatingunit = xlWorkSheet.Cells.Range["A1"];
            operatingunit.Value = "Operating Unit";
            operatingunit.Font.Bold = true;
            operatingunit.Font.Size = 11;
            operatingunit.RowHeight = 46;
            operatingunit.Interior.Color = 6;
            operatingunit.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            operatingunit.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Number
            var CusNo = xlWorkSheet.Cells.Range["B1"];
            CusNo.Value = "Customer Number";
            CusNo.Font.Bold = true;
            CusNo.Font.Size = 11;
            CusNo.RowHeight = 15;
            CusNo.Interior.Color = 6;
            CusNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CusNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Report ID
            var reportid = xlWorkSheet.Cells.Range["C1"];
            reportid.Value = "Report ID";
            reportid.Font.Bold = true;
            reportid.Font.Size = 11;
            reportid.RowHeight = 15;
            reportid.Interior.Color = 6;
            reportid.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            reportid.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //H.M Reference no
            var hmrefernce = xlWorkSheet.Cells.Range["D1"];
            hmrefernce.Value = "H.M Reference No";
            hmrefernce.Font.Bold = true;
            hmrefernce.Font.Size = 11;
            hmrefernce.RowHeight = 15;
            hmrefernce.Interior.Color = 6;
            hmrefernce.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            hmrefernce.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Name
            var cusname = xlWorkSheet.Cells.Range["E1"];
            cusname.Value = "Customer Name";
            cusname.Font.Bold = true;
            cusname.Font.Size = 11;
            cusname.RowHeight = 15;
            cusname.Interior.Color = 6;
            cusname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cusname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Samunnati Branch Mapping
            var Samunnatibranchmapping = xlWorkSheet.Cells.Range["E1"];
            cusname.Value = "Customer Name";
            cusname.Font.Bold = true;
            cusname.Font.Size = 11;
            cusname.RowHeight = 15;
            cusname.Interior.Color = 6;
            cusname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cusname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


        }

















    }
}
