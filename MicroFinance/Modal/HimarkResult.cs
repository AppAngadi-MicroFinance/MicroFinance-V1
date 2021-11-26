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
using System.Diagnostics;

namespace MicroFinance.Modal
{
    public class HimarkResult
    {
       public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
       public List<HimarkResultModel> himarkResultslist = new List<HimarkResultModel>();
       public List<string> CategoryList = new List<string>();

        Stopwatch SW = new Stopwatch();
        public List<HimarkResultModel> GetFileDetails(string Path)
        {
            List<HimarkResultModel> ResultList = new List<HimarkResultModel>();
           
            SW.Start();
            MainWindow.TimeBuilder.Append("\n GetSheet Name : " + SW.Elapsed.Ticks.ToString());
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
            MainWindow.TimeBuilder.Append("\n open Application time start : " + SW.Elapsed.Ticks.ToString());
            application = new Excel.Application();
            workbook = application.Workbooks.Open(Path);
            MainWindow.TimeBuilder.Append("\n application Opened Time : " + SW.Elapsed.Ticks.ToString());
            try
            {
                SC = workbook.Worksheets.Count;
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name.Equals("Analytics", StringComparison.CurrentCultureIgnoreCase))
                    {
                        MainWindow.TimeBuilder.Append("\n time When Sheet Found : " + SW.Elapsed.Ticks.ToString());
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
               MainWindow.TimeBuilder.Append("\n time for xl App Close start : " + SW.Elapsed.Ticks.ToString());
                workbook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
               /// GC.Collect();
                MainWindow.TimeBuilder.Append("\n time for xl App Close end : " + SW.Elapsed.Ticks.ToString());
                SW.Stop();
            }
            return ResultList;
        }



        public List<HimarkResultExcelModel> BulkInsertData(string Path,int Value)
        {

            List<HimarkResultExcelModel> ResultList = new List<HimarkResultExcelModel>();
            //SW.Start();
            //MainWindow.TimeBuilder.Append("\n GetSheet Name : " + SW.Elapsed.Ticks.ToString());
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
            //MainWindow.TimeBuilder.Append("\n open Application time start : " + SW.Elapsed.Ticks.ToString());
            application = new Excel.Application();
            workbook = application.Workbooks.Open(Path);
            //MainWindow.TimeBuilder.Append("\n application Opened Time : " + SW.Elapsed.Ticks.ToString());
            try
            {
                SC = workbook.Worksheets.Count;
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name.Equals("Analytics", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //MainWindow.TimeBuilder.Append("\n time When Sheet Found : " + SW.Elapsed.Ticks.ToString());
                        Excel.Range usedrange = sheet.UsedRange;
                        Sheetname = sheet.Name;
                        ResultList= GetDataFromExcel(sheet, FileName);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                workbook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);   
            }

            return ResultList;
            
        }

        public static bool IsAlreadyUpload(string InputFile)
        {
            bool Result = false;
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
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
        public void InsertHimarkDate(List<HimarkResultModel> ResultList)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    foreach(HimarkResultModel result in ResultList)
                    {
                        sqlcomm.CommandText = "insert into HimarkResult(RequestID,ReportID,ReportDate,EligibleAmount,Status,Remark,ActiveUnsecureLoan,ActiveUnsecureLoaninLast6Months,OutstandingAmount,DPDPaymentHistory,HimarkScore,ScoreCommend,DPDAmount,BranchID,FileName)values('" + result.GetRequestId(result.AadharNumber) + "','" + result.ReportID + "','" + result.ReportDate.ToString("MM-dd-yyyy") + "','" + result.EligibleLoanAmount + "','" + result.Status + "','" + result.HiMarkRemark + "','" + result.ActiveUnsecureLoan + "','" + result.ActiveUnsecureLoanin6Months + "','" + result.OutstandingAmount + "','" + result.DPDSummary + "','" + result.HIMarkScore + "','" + result.ScoreCommend + "','" + result.DPDAmount + "','" + result.BName + "','" + result.FileName + "')";
                        sqlcomm.ExecuteNonQuery();
                    }
                    
                }
                sqlconn.Close();
            }
        }

        public void InsertHimarkDate(HimarkResultModel result)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    
                        sqlcomm.CommandText = "insert into HimarkResult(RequestID,ReportID,ReportDate,EligibleAmount,Status,Remark,ActiveUnsecureLoan,ActiveUnsecureLoaninLast6Months,OutstandingAmount,DPDPaymentHistory,HimarkScore,ScoreCommend,DPDAmount,BranchID,FileName)values('" + result.GetRequestId(result.AadharNumber) + "','" + result.ReportID + "','" + result.ReportDate.ToString("MM-dd-yyyy") + "','" + result.EligibleLoanAmount + "','" + result.Status + "','" + result.HiMarkRemark + "','" + result.ActiveUnsecureLoan + "','" + result.ActiveUnsecureLoanin6Months + "','" + result.OutstandingAmount + "','" + result.DPDSummary + "','" + result.HIMarkScore + "','" + result.ScoreCommend + "','" + result.DPDAmount + "','" + result.BName + "','" + result.FileName + "')";
                        sqlcomm.ExecuteNonQuery();
               

                }
                sqlconn.Close();
            }
        }
        public List<HimarkResultExcelModel> GetDataFromExcel(Excel.Worksheet worksheet,string Filename)
        {
            List<HimarkResultExcelModel> HimarkResultList = new List<HimarkResultExcelModel>();
          
            
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

                        var IsNull = (worksheet.Cells[Rownumber, ReportDateColumn] as Excel.Range);
                        if (IsNull.Text == "")
                        {
                            break;
                        }
                        else
                        {
                            DateTime _reportDate = (DateTime)(worksheet.Cells[Rownumber, ReportDateColumn] as Excel.Range).Value;
                            string _reqID = ((worksheet.Cells[Rownumber, requestIDColumn] as Excel.Range).Value);
                            var AadharString = (worksheet.Cells[Rownumber, AadharNoColumn] as Excel.Range).Value;
                            string _aadharno =Convert.ToString(AadharString).Trim();
                            string BranchString = (worksheet.Cells[Rownumber, BranchNameColumn] as Excel.Range).Value;
                            string _branchname = BranchString.Trim();
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
                    HimarkResultExcelModel result = new HimarkResultExcelModel();
                            result.ReportID = (_reqID).Trim();
                            result.Name = (_name).Trim();
                            result.AadharNumber = _aadharno.Trim();
                            result.EligibleLoanAmount = _eligibleAmount;
                            result.Status = _status;
                            result.HiMarkRemark = _remark;
                            result.ActiveUnsecureLoan = _activeUnsecureLoan;
                            result.ActiveUnsecureLoanin6Months = _activeunsecureloan6months;
                            result.OutstandingAmount = _outstandingamount;
                            result.DPDSummary = _dpdsummary;
                            result.HIMarkScore = _himarkscore;
                            result.ScoreCommend = _scoreCommend;
                            result.BName = _branchname.Trim();
                            result.DPDAmount = _dpdamount;
                            result.ReportDate = _reportDate;
                            result.FileName = Filename;


                    HimarkResultList.Add(result);
                        }
                    }
            return HimarkResultList;
        }
        public List<HimarkResultModel> GetBranchRequest(string BranchId)
        {
            List<HimarkResultModel> RequestList = new List<HimarkResultModel>();
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RequestID,ReportID,ReportDate,EligibleAmount,Status,Remark,ActiveUnsecureLoan,ActiveUnsecureLoaninLast6Months,OutstandingAmount,DPDPaymentHistory,HimarkScore,ScoreCommend,DPDAmount,BranchID from HimarkResult where RequestID in (select LoanApplication.RequestId from LoanApplication where LoanStatus='2') and BranchID='"+ BranchId + "'";
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
                                new HimarkResultModel
                                {
                                    //Change Value assign ReportId to RequestID
                                    RequestID = reader.GetString(0),
                                    ReportID = reader.GetString(1),
                                    ReportDate = (DateTime)reader.GetDateTime(2),
                                    EligibleLoanAmount = reader.GetInt32(3),
                                    Status = _category,
                                    HiMarkRemark = reader.GetString(5),
                                    ActiveUnsecureLoan = reader.GetInt32(6),
                                    ActiveUnsecureLoanin6Months = reader.GetInt32(7),
                                    OutstandingAmount = reader.GetInt32(8),
                                    DPDSummary = _dpdsummary,
                                    HIMarkScore = (reader.GetInt32(10)).ToString(),
                                    ScoreCommend = reader.GetString(11),
                                    DPDAmount = reader.GetInt32(12),
                                    IsRecommend = true
                                }
                                ); ;
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



    public class HimarkResultModel:BindableBase
    {
        public int SNo { get; set; }
        private bool _isRecommend;
        public bool IsRecommend
        {
            get
            {
                return _isRecommend;
            }
            set
            {
                _isRecommend = value;
                RaisedPropertyChanged("IsRecommend");
            }
        }
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
        public string BranchName { get; set; }
        public string BName
        {
            get
            {
                return _bname;
            }
            set
            {
                string id = value;
                _bname = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchName == id).Select(temp => temp.BranchId).FirstOrDefault();
                BranchName = id;
            }
        }
        public string FOName { get; set; }
        public string CustomerName { get; set; }
        public string GroupName { get; set; }
        public DateTime ReportDate { get; set; }
        public int DPDAmount { get; set; }
        public string FileName { get; set; }
        public string ReportID { get; set; }
        public string CustomerID { get; set; }
        public void GetCustomerDetails(string RequestID)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId from LoanApplication where RequestId='" + RequestID + "'";
                    CustomerID = (string)sqlcomm.ExecuteScalar();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name from CustomerDetails where CustId='" + CustomerID + "'";
                    CustomerName = (string)sqlcomm.ExecuteScalar();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + CustomerID + "'))";
                    GroupName = (string)sqlcomm.ExecuteScalar();
                    sqlcomm.CommandText = "select Name from Employee where EmpId=(select EmpId from TimeTable where SHGId=(select SHGid from PeerGroup  where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerID+"')))";
                    FOName = (string)sqlcomm.ExecuteScalar();

                }
            }
        }
        public string GetRequestId(string Aadharnumber)
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select LoanApplication.RequestId from LoanApplication where CustId=(select CustId from CustomerDetails where AadharNumber='"+Aadharnumber+"') and LoanStatus='2'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }
    }
}
