using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft = Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{
    public class HimarkResult : LoanProcess
    {
       
       public List<HimarkResult> himarkResultslist = new List<HimarkResult>();
       public List<string> CategoryList = new List<string>();
        private string _requestid;
        public string RequestID
        {
            get
            {
                return _requestid;
            }
            set
            {
                _requestid = value;
                GetCustomerDetails(_requestid);
            }
        }
        public string AadharNumber { get; set; }
        public string Name { get; set; }
        public int EligibleLoanAmount { get; set; }
        public string Status { get; set; }
        public string HiMarkRemark { get; set; }
        public int ActiveUnsecureLoan { get; set; }
        public int ActiveUnsecureLoanin6Months { get; set; }
        public int OutstandingAmount { get; set; }
        public string DPDSummary { get; set; }
        public string HIMarkScore { get; set; }
        public string ScoreCommend { get; set; }
        public string SheetName;
        public string ColumnNumber;

        public void GetDetails(string Path)
        {
            Excel.Application application;
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            StringBuilder sb = new StringBuilder();
            int SC;
            int RC = 0;
            int CC = 0;
            string Sheetname = null;

            application = new Excel.Application();
            workbook = application.Workbooks.Open(Path);

            SC = workbook.Worksheets.Count;
            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name.Equals("Analytics", StringComparison.CurrentCultureIgnoreCase))
                {
                    Excel.Range usedrange = sheet.UsedRange;
                    Sheetname = sheet.Name;
                    GetDataFromExcel(sheet);
                    break;
                }

            }
            workbook.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            application.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
           
        }


        public void GetCustomerDetails(string requestID)
        {
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.RequestId='"+requestID+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
                            _customerId = reader.GetString(0);
                            CustomerName = reader.GetString(1);
                            FatherName = reader.GetString(2);
                            MotherName = reader.GetString(3);
                            DateofBirth = reader.GetDateTime(4);
                            Age = reader.GetInt32(5);
                            Gender = reader.GetString(6);
                            ContactNumber = reader.GetString(7);
                            AadharNo = reader.GetString(8);
                            Religion = reader.GetString(9);
                            Caste = reader.GetString(10);
                            Community = reader.GetString(11);
                            Education = reader.GetString(12);
                            FamilyMembers = reader.GetInt32(13);
                            EarningMembers = reader.GetInt32(14);
                            Occupation = reader.GetString(15);
                            MonthlyIncome = reader.GetInt32(16);
                            MothlyExpenses = reader.GetInt32(17);
                            DoorNumber = _fullAdress[0];
                            StreetName = _fullAdress[1];
                            LocalityTown = _fullAdress[2];
                            City = _fullAdress[3];
                            State = _fullAdress[4];
                            Pincode = reader.GetInt32(19);
                            HousingType = reader.GetString(20);
                            NameofAddressProof = reader.GetString(21);
                            NameofPhotoProof = reader.GetString(22);
                            AccountHolder = reader.GetString(26);
                            AccountNumber = reader.GetString(27);
                            BankBranchName = reader.GetString(28);
                            IFSCCode = reader.GetString(29);
                            MICRCode = reader.GetString(30);
                            AddressProof = (reader.GetBoolean(23)) ? ByteToBI((byte[])reader.GetValue(31)) : null;
                            PhotoProof = (reader.GetBoolean(24)) ? ByteToBI((byte[])reader.GetValue(32)) : null;
                            ProfilePicture = (reader.GetBoolean(25)) ? ByteToBI((byte[])reader.GetValue(33)) : null;
                            LoanRequestID = reader.GetString(34);
                            LoanAmount = reader.GetInt32(35);
                            LoanType = reader.GetString(36);
                            LoanPeriod = reader.GetInt32(37);
                            LoanPurpose = reader.GetString(38);
                            EnrollDate = reader.GetDateTime(39);
                            EmployeeID = reader.GetString(41);
                            BranchID = reader.GetString(42);
                            BankName = reader.GetString(43);
                        }
                    }
                }
                sqlconn.Close();
            }
        }
       
        public void GetDataFromExcel(Excel.Worksheet worksheet)
        {
            Excel.Range userange = worksheet.UsedRange;
            int rowcount = userange.Rows.Count;
            int ColumnCount = userange.Columns.Count;
            int requestIDColumn = 0;
            int AadharNoColumn = 0;
            int NameColumn = 0;
            int EligibleLoanAmountColumn = 0;
            int HimarkStatusColumn = 0;
            int RemarkColumn = 0;
            int ActiveUnsecureLoanColumn = 0;
            int ActiveUnsecureLoan6MonthsColumn = 0;
            int outstandingamountcolumn = 0;
            int DPDSummaryColumn = 0;
            int HimarkScoreValuecolumn = 0;
            int ScoreCommend = 0;
            for (int i = 1; i <= ColumnCount; i++)
            {
                int row = 1;
                string value = (worksheet.Cells[row, i] as Excel.Range).Value;
                if (value.Equals("Report ID"))
                {
                    requestIDColumn = i;
                }
                else if (value.Equals("Aadhar"))
                {
                    AadharNoColumn = i;
                }
                else if (value.Equals("Name"))
                {
                    NameColumn = i;
                }
                else if (value.Equals("Eligible Loan Amount", StringComparison.CurrentCultureIgnoreCase))
                {
                    EligibleLoanAmountColumn = i;
                }
                else if (value.Equals("Status", StringComparison.CurrentCultureIgnoreCase))
                {
                    HimarkStatusColumn = i;
                }
                else if (value.Equals("Remarks", StringComparison.CurrentCultureIgnoreCase))
                {
                    RemarkColumn = i;
                }
                else if (value.Equals("Active Unsecured Loans", StringComparison.CurrentCultureIgnoreCase) || value.Equals("Active\nUnsecured Loans", StringComparison.CurrentCultureIgnoreCase))
                {
                    ActiveUnsecureLoanColumn = i;
                }
                else if (value.Equals("Active Unsecured Loans (last 6 month)", StringComparison.CurrentCultureIgnoreCase) || value.Equals("Active\nUnsecured Loans\n(last 6 months)", StringComparison.CurrentCultureIgnoreCase))
                {
                    ActiveUnsecureLoan6MonthsColumn = i;
                }
                else if (value.Equals("Outstanding Amount", StringComparison.CurrentCultureIgnoreCase) || value.Equals("Outstanding\nAmount", StringComparison.CurrentCultureIgnoreCase))
                {
                    outstandingamountcolumn = i;
                }
                else if (value.Equals("DPD Payment History", StringComparison.CurrentCultureIgnoreCase))
                {
                    DPDSummaryColumn = i;
                }
                else if (value.Equals("Highmark Score Value", StringComparison.CurrentCultureIgnoreCase))
                {
                    HimarkScoreValuecolumn = i;
                }
                else if (value.Equals("Highmark Score Comment", StringComparison.CurrentCultureIgnoreCase))
                {
                    ScoreCommend = i;
                }
            }
            for (int Rownumber = 2; Rownumber <= rowcount; Rownumber++)
            {
                string _reqID = (worksheet.Cells[Rownumber, requestIDColumn] as Excel.Range).Value;
                string _aadharno = (worksheet.Cells[Rownumber, AadharNoColumn] as Excel.Range).Value;
                string _name = (worksheet.Cells[Rownumber, NameColumn] as Excel.Range).Value;
                int _eligibleAmount = (int)(worksheet.Cells[Rownumber, EligibleLoanAmountColumn] as Excel.Range).Value;
                string _status = (worksheet.Cells[Rownumber, HimarkStatusColumn] as Excel.Range).Value;
                string _remark = (worksheet.Cells[Rownumber, RemarkColumn] as Excel.Range).Value;
                int _activeUnsecureLoan = (int)(worksheet.Cells[Rownumber, ActiveUnsecureLoanColumn] as Excel.Range).Value;
                int _activeunsecureloan6months = (int)(worksheet.Cells[Rownumber, ActiveUnsecureLoan6MonthsColumn] as Excel.Range).Value;
                int _outstandingamount = (int)(worksheet.Cells[Rownumber, outstandingamountcolumn] as Excel.Range).Value;
                string _dpdsummary = (worksheet.Cells[Rownumber, DPDSummaryColumn] as Excel.Range).Value;
                string _himarkscore = Convert.ToString((worksheet.Cells[Rownumber, HimarkScoreValuecolumn] as Excel.Range).Value);
                string _scoreCommend = (worksheet.Cells[Rownumber, ScoreCommend] as Excel.Range).Value;
                string CategoryValue = _status.ToUpper();
                if(!CategoryList.Contains(CategoryValue))
                {
                    CategoryList.Add(_status.ToUpper());
                }
                himarkResultslist.Add(
                    new HimarkResult
                    {
                        RequestID = _reqID,
                        AadharNumber = _aadharno,
                        Name = _name,
                        EligibleLoanAmount = _eligibleAmount,
                        Status = _status,
                        HiMarkRemark = _remark,
                        ActiveUnsecureLoan = _activeUnsecureLoan,
                        ActiveUnsecureLoanin6Months = _activeunsecureloan6months,
                        OutstandingAmount = _outstandingamount,
                        DPDSummary = _dpdsummary,
                        HIMarkScore = _himarkscore,
                        ScoreCommend = _scoreCommend,
                       
                    });
            }

        }


    }
}
