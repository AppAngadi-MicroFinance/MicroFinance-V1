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
using Microsoft.Office.Interop.Excel;

namespace MicroFinance.Modal
{
    public class HimarkResult
    {
       public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
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
                GetCustomerDetails(value);
            }
        }
        private string _aadharnumber;
        public string AadharNumber
        {
            get
            {
                return _aadharnumber;
            }
            set
            {
                _aadharnumber = value;
                //GetCustomerDetails(_aadharnumber, Name);
            }
        }
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
        private string _bname;
        public string BName
        {
            get
            {
                return _bname;
            }
            set
            {
                string id = value;
                using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if(sqlconn.State==ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select Bid from BranchDetails where BranchName='"+id+"'";
                        _bname = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
            }
        }
        public string CustomerName { get; set; }
        public string GroupName { get; set; }
        public DateTime ReportDate { get; set; }
        public int DPDAmount { get; set; }
        public string FileName { get; set; }
        public string ReportID { get; set; }
        public string CustomerID { get; set; }
        public string SheetName;
        public string ColumnNumber;

        public void GetCustomerDetails(string RequestID)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId from LoanApplication where RequestId='" + RequestID + "'";
                    CustomerID = (string)sqlcomm.ExecuteScalar();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name from CustomerDetails where CustId='"+ CustomerID + "'";
                    CustomerName = (string)sqlcomm.ExecuteScalar();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerID+"'))";
                    GroupName = (string)sqlcomm.ExecuteScalar();
                }
            }
        }
        public string GetRequestId()
        {
            string Result = "";
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select LoanApplication.RequestId from LoanApplication,CustomerDetails where LoanApplication.CustId=CustomerDetails.CustId and CustomerDetails.AadharNumber='"+ _aadharnumber + "' and CustomerDetails.Name='"+ Name + "' and LoanApplication.LoanStatus=1";
                    Result =(string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }
        public void GetFileDetails(string Path)
        {
            var FilePath = Path.Split('\\');
            string FileName = FilePath[FilePath.Length - 1];
            Excel.Application application;
            Excel.Workbook workbook;
           // Excel.Worksheet worksheet;
            StringBuilder sb = new StringBuilder();
            int SC;
           // int RC = 0;
           // int CC = 0;
            string Sheetname = null;

            application = new Excel.Application();
            workbook = application.Workbooks.Open(Path);
            try
            {
                SC = workbook.Worksheets.Count;
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name.Equals("Analytics", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Excel.Range usedrange = sheet.UsedRange;
                        Sheetname = sheet.Name;
                        GetDataFromExcel(sheet, FileName);
                        break;
                    }
                }
            }
            catch
            {

            }
            finally
            {
                workbook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
            }
        }
        //public void GetCustomerDetails(string AadharNumber,string CustName)
        //{
        //    using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
        //    {
        //        sqlconn.Open();
        //        if(sqlconn.State==ConnectionState.Open)
        //        {
        //            SqlCommand sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlconn;
        //            sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and CustomerDetails.AadharNumber='"+AadharNumber+"'and CustomerDetails.Name='"+CustName+"' and LoanStatus=1";
        //            SqlDataReader reader = sqlcomm.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    string[] _fullAdress = reader.GetString(18).Split('|', '~');
        //                    _customerId = reader.GetString(0);
        //                    CustomerName = reader.GetString(1);
        //                    FatherName = reader.GetString(2);
        //                    MotherName = reader.GetString(3);
        //                    DateofBirth = reader.GetDateTime(4);
        //                    Age = reader.GetInt32(5);
        //                    Gender = reader.GetString(6);
        //                    ContactNumber = reader.GetString(7);
        //                    AadharNo = reader.GetString(8);
        //                    Religion = reader.GetString(9);
        //                    Caste = reader.GetString(10);
        //                    Community = reader.GetString(11);
        //                    Education = reader.GetString(12);
        //                    FamilyMembers = reader.GetInt32(13);
        //                    EarningMembers = reader.GetInt32(14);
        //                    Occupation = reader.GetString(15);
        //                    MonthlyIncome = reader.GetInt32(16);
        //                    MothlyExpenses = reader.GetInt32(17);
        //                    DoorNumber = _fullAdress[0];
        //                    StreetName = _fullAdress[1];
        //                    LocalityTown = _fullAdress[2];
        //                    City = _fullAdress[3];
        //                    State = _fullAdress[4];
        //                    Pincode = reader.GetInt32(19);
        //                    HousingType = reader.GetString(20);
        //                    NameofAddressProof = (DBNull.Value.Equals(reader["AddressProofName"])) ? "" : reader.GetString(21);
        //                    NameofPhotoProof = (DBNull.Value.Equals(reader["PhotoProofName"])) ? "" : reader.GetString(22);
        //                    AccountHolder = reader.GetString(26);
        //                    AccountNumber = reader.GetString(27);
        //                    BankBranchName = reader.GetString(28);
        //                    IFSCCode = reader.GetString(29);
        //                    MICRCode = reader.GetString(30);
        //                    AddressProof = (reader.GetBoolean(23)) ? ByteToBI((byte[])reader.GetValue(31)) : null;
        //                    PhotoProof = (reader.GetBoolean(24)) ? ByteToBI((byte[])reader.GetValue(32)) : null;
        //                    ProfilePicture = (reader.GetBoolean(25)) ? ByteToBI((byte[])reader.GetValue(33)) : null;
        //                    LoanRequestID = reader.GetString(34);
        //                    LoanAmount = reader.GetInt32(35);
        //                    LoanType = reader.GetString(36);
        //                    LoanPeriod = reader.GetInt32(37);
        //                    LoanPurpose = reader.GetString(38);
        //                    EnrollDate = reader.GetDateTime(39);
        //                    EmployeeID = reader.GetString(41);
        //                    BranchID = reader.GetString(42);
        //                    BankName = reader.GetString(43);
        //                }
        //            }
        //        }
        //        sqlconn.Close();
        //    }
        //}
        public bool IsAlreadyUpload(string InputFile)
        {
            bool Result = false;
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select distinct FileName from HimarkResult where FileName='" + InputFile + "'";
                    string Res =(string) sqlcomm.ExecuteScalar();
                    if (!InputFile.Equals(Res))
                        Result = true;
                }
                sqlconn.Close();
            }
            return Result;
        }
        public void InsertHimarkDate(HimarkResult result)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into HimarkResult(RequestID,ReportID,ReportDate,EligibleAmount,Status,Remark,ActiveUnsecureLoan,ActiveUnsecureLoaninLast6Months,OutstandingAmount,DPDPaymentHistory,HimarkScore,ScoreCommend,DPDAmount,BranchID,FileName)values('"+result.GetRequestId() +"','"+result.ReportID+"','"+result.ReportDate.ToString("MM-dd-yyyy")+"','"+result.EligibleLoanAmount+"','"+result.Status+"','"+result.HiMarkRemark+"','"+result.ActiveUnsecureLoan+"','"+result.ActiveUnsecureLoanin6Months+"','"+result.OutstandingAmount+"','"+result.DPDSummary+"','"+result.HIMarkScore+"','"+result.ScoreCommend+"','"+result.DPDAmount+"','"+result.BName+"','"+result.FileName+"')";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
        }
        public void GetDataFromExcel(Excel.Worksheet worksheet,string Filename)
        {
            Excel.Range userange = worksheet.UsedRange;
            int rowcount = userange.Rows.Count;
            int ColumnCount = userange.Columns.Count;
            int ReportDateColumn=0;
            int BranchNameColumn = 0;
            int requestIDColumn = 0;
            int AadharNoColumn = 0;
            int NameColumn = 0;
            int DPDColumn=0;
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
                if(value.Equals("Report Date"))
                {
                    ReportDateColumn = i;
                }
                else if (value.Equals("Report ID"))
                {
                    requestIDColumn = i;
                }
                else if(value.Equals("DPD"))
                {
                    DPDColumn = i;
                }
                else if(value.Equals("Branch"))
                {
                    BranchNameColumn = i;
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
                var IsNull=(worksheet.Cells[Rownumber, ReportDateColumn] as Excel.Range);
                if (IsNull.Text=="")
                {
                    break;
                }
                else
                {
                    DateTime _reportDate = (DateTime)(worksheet.Cells[Rownumber, ReportDateColumn] as Excel.Range).Value;
                    string _reqID = ((worksheet.Cells[Rownumber, requestIDColumn] as Excel.Range).Value);
                    string _aadharno = (worksheet.Cells[Rownumber, AadharNoColumn] as Excel.Range).Value;
                    string _branchname = (worksheet.Cells[Rownumber, BranchNameColumn] as Excel.Range).Value;
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
                    int _dpdamount = (int)(worksheet.Cells[Rownumber, DPDColumn] as Excel.Range).Value;
                    if (!CategoryList.Contains(CategoryValue))
                    {
                        CategoryList.Add(_status.ToUpper());
                    }
                    himarkResultslist.Add(
                        new HimarkResult
                        {
                            ReportID = (_reqID).Trim(),
                            Name = (_name).Trim(),
                            AadharNumber = _aadharno.Trim(),
                            EligibleLoanAmount = _eligibleAmount,
                            Status = _status,
                            HiMarkRemark = _remark,
                            ActiveUnsecureLoan = _activeUnsecureLoan,
                            ActiveUnsecureLoanin6Months = _activeunsecureloan6months,
                            OutstandingAmount = _outstandingamount,
                            DPDSummary = _dpdsummary,
                            HIMarkScore = _himarkscore,
                            ScoreCommend = _scoreCommend,
                            BName = _branchname.Trim(),
                            DPDAmount = _dpdamount,
                            ReportDate = _reportDate,
                            FileName = Filename
                        });
                }
            }
        }
        public List<HimarkResult> GetBranchRequest(string BranchId)
        {
            List<HimarkResult> RequestList = new List<HimarkResult>();
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RequestID,ReportID,ReportDate,EligibleAmount,Status,Remark,ActiveUnsecureLoan,ActiveUnsecureLoaninLast6Months,OutstandingAmount,DPDPaymentHistory,HimarkScore,ScoreCommend,DPDAmount,BranchID from HimarkResult where RequestID in (select LoanApplication.RequestId from LoanApplication where LoanStatus='1') and BranchID='"+ BranchId + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            string _dpdsummary = (DBNull.Value.Equals(reader["DPDPaymentHistory"])) ? "" : reader.GetString(9);
                            _dpdsummary =(string.IsNullOrEmpty(_dpdsummary)? "NO Payment History":HimarkSummaryFormat(_dpdsummary));
                            string _category = reader.GetString(4);
                            if (!CategoryList.Contains(_category))
                            {
                                CategoryList.Add(_category);
                            }
                            RequestList.Add(
                                new HimarkResult
                                {
                                    //Change Value assign ReportId to RequestID
                                    RequestID = reader.GetString(0),
                                    ReportID=reader.GetString(1),
                                    ReportDate=(DateTime)reader.GetDateTime(2),
                                    EligibleLoanAmount=reader.GetInt32(3),
                                    Status=_category,
                                    HiMarkRemark=reader.GetString(5),
                                    ActiveUnsecureLoan=reader.GetInt32(6),
                                    ActiveUnsecureLoanin6Months=reader.GetInt32(7),
                                    OutstandingAmount=reader.GetInt32(8),
                                    DPDSummary=_dpdsummary,
                                    HIMarkScore=(reader.GetInt32(10)).ToString(),
                                    ScoreCommend=reader.GetString(11),
                                    DPDAmount=reader.GetInt32(12),
                                }
                                );
                        }
                    }
                }
            }
            return RequestList;
        }
        public string HimarkSummaryFormat(string Value)
        {
            string[] SummaryValue = Value.Split('|').ToArray();
            List<string> SummaryList = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (string s in SummaryValue)
            {
                if(!string.IsNullOrEmpty(s))
                {
                    if (s[s.Length - 1] == 'X')
                    {
                        if (s[s.Length - 1] == 'X')
                        {
                            string v = s.Substring(0, (s.Length - 4));
                            int Res = 0;
                            bool result = int.TryParse(v.Substring((v.Length - 3)), out Res);
                            if(result)
                            {
                                if (Res > 0)
                                {
                                    //SummaryList.Add(v);
                                    sb.Append(v + "\n");
                                }
                            }
                            else
                            {
                                sb.Append(v + "\n");
                            }
                           
                        }
                        else
                        {
                            //SummaryList.Add(s);
                            sb.Append(s + "\n");
                        }
                    }
                    else
                    {
                        string[] arr = s.Split(' ').ToArray();
                        foreach (string s1 in arr)
                        {
                            if(!string.IsNullOrEmpty(s1))
                            {
                                if (s1[s1.Length - 1] == 'X')
                                {
                                    string v = s1.Substring(0, (s1.Length - 4));
                                    int Res = 0;
                                    bool result = int.TryParse(v.Substring((v.Length - 3)), out Res);
                                    if (result)
                                    {
                                        if (Res > 0)
                                        {
                                            //SummaryList.Add(v);
                                            sb.Append(v + "\n");
                                        }
                                    }
                                    else
                                    {
                                        sb.Append(v + "\n");
                                    }
                                }
                                else
                                {
                                    //SummaryList.Add(s1);
                                    sb.Append(s1 + "\n");
                                }

                            }
                           
                        }

                    }

                }

            }
            return sb.ToString();
        }



    }
}
