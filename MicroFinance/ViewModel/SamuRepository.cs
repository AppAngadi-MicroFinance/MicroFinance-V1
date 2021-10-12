using MicroFinance.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using MicroFinance.ViewModel;
using System.Collections.ObjectModel;

namespace MicroFinance.ViewModel
{
    public class SamuRepository
    {
        public static List<HimarkRequestView> GetSamuRequestList()
        {
            List<HimarkRequestView> ResultView = new List<HimarkRequestView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='10') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HimarkRequestView HMRequestCustomer = new HimarkRequestView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (HimarkRequestView Hm in ResultView)
                    {
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.Collectionday = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }




        public static List<SamuReportView> GetSamuRequest(string Path)
        {
            List<SamuReportView> RequestList = ImportSamuFile(Path);

            List<SamuReportView> ReqList = new List<SamuReportView>();

            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    foreach (SamuReportView sm in RequestList)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select CustId from CustomerDetails where AadharNumber='" + sm.AadharNumber + "'";
                        sm.CustomerID = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText= "select RequestID from LoanApplication where CustId = (select CustId from CustomerDetails where AadharNumber = '"+sm.AadharNumber+"') and LoanStatus = 11";
                        sm.RequestID = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select Name from Employee where EmpId=(select EmpId from TimeTable where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + sm.CustomerID + "')))";
                        sm.EmpName = (string)sqlcomm.ExecuteScalar();
                        if (!string.IsNullOrEmpty(sm.RequestID))
                        {
                            ReqList.Add(sm);
                           
                        }
                        
                        

                    }
                }
                sqlconn.Close();
            }

            return ReqList;
        }


        private static List<SamuReportView> ImportSamuFile(string Path)
        {
            List<SamuReportView> SamuList = new List<SamuReportView>();
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
            SC = workbook.Worksheets.Count;
            try
            {
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name.Equals("Loan", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Excel.Range usedrange = sheet.UsedRange;
                        Sheetname = sheet.Name;
                        SamuList = GetDataFromExcel(sheet, FileName);
                        break;
                    }
                }
                if (Sheetname == null)
                {
                    throw new ArgumentException("Sheet Not Found");
                }
            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
            finally
            {
                workbook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
            }
            return SamuList;
        }
        private static List<SamuReportView> GetDataFromExcel(Excel.Worksheet worksheet, string Filename)
        {
            List<SamuReportView> SamuList = new List<SamuReportView>();
            Excel.Range userange = worksheet.UsedRange;
            int rowcount = userange.Rows.Count;
            int ColumnCount = userange.Columns.Count;
            int AadharColumn = 0;
            int LoanAcNoColumn = 0;
            int CustomerNameColumn = 0;
            int DisbursementColumn = 0;
            int DateColumn = 0;
            int GroupNameColumn = 0;
            for (int i = 1; i <= ColumnCount; i++)
            {
                int row = 1;
                string value = (worksheet.Cells[row, i] as Excel.Range).Value;
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Trim();
                    if (value.Equals("Aadhar"))
                    {
                        AadharColumn = i;
                    }
                    else if (value.Equals("Date"))
                    {
                        DateColumn = i;
                    }
                    else if (value.Equals("Loan Acc No"))
                    {
                        LoanAcNoColumn = i;
                    }
                    else if (value.Equals("Customer Name"))
                    {
                        CustomerNameColumn = i;
                    }
                    else if (value.Equals("Disbursement"))
                    {
                        DisbursementColumn = i;
                    }
                    else if(value.Equals("Group Name"))
                    {
                        GroupNameColumn = i;
                    }
                }

            }
            for (int Rownumber = 2; Rownumber <= rowcount; Rownumber++)
            {
                var IsNull = (worksheet.Cells[Rownumber, DateColumn] as Excel.Range);
                if (IsNull.Text == "")
                {
                    break;
                }
                else
                {
                    DateTime _reportDate = (DateTime)(worksheet.Cells[Rownumber, DateColumn] as Excel.Range).Value;
                    var aadhar = (worksheet.Cells[Rownumber, AadharColumn] as Excel.Range).Value;
                    string _aadharNumber = aadhar.ToString();
                    //string _aadharNumber = (worksheet.Cells[Rownumber, AadharColumn] as Excel.Range).Value;
                    var  loan= (worksheet.Cells[Rownumber, LoanAcNoColumn] as Excel.Range).Value;
                    string _loanacno = loan.ToString();
                    string _customername = (worksheet.Cells[Rownumber, CustomerNameColumn] as Excel.Range).Value;
                    int _disbursement = (int)(worksheet.Cells[Rownumber, DisbursementColumn] as Excel.Range).Value;
                    string GroupNameData = (string)(worksheet.Cells[Rownumber, GroupNameColumn] as Excel.Range).Value;
                    string[] groups = GroupNameData.Split(',').ToArray();
                    string _centerName = groups[0].ToString();
                    string _branchName = groups[1].ToString();
                    string _fileName = Filename;

                    SamuList.Add(new SamuReportView
                    {
                        ApproveDate = _reportDate,
                        AadharNumber = _aadharNumber,
                        LoanAcNo = _loanacno,
                        CustomerName = _customername,
                        Disbursement = _disbursement,
                        FileName = _fileName,
                        BranchName = _branchName,
                        CenterName=_centerName
                    }); ;

                }
            }
            return SamuList;
        }


        public static void InsertSamuData(List<SamuReportView> samuReportViews)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    foreach(SamuReportView sm in samuReportViews)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "insert into DisbursementFromSAMU(ApprovedDate,LoanAcNo,RequestID,FileName)values('" + sm.ApproveDate.ToString("MM-dd-yyyy") + "','" + sm.LoanAcNo + "','" + sm.RequestID + "','" + sm.FileName + "')";
                        sqlcomm.ExecuteNonQuery();
                    }
                    
                }
            }
        }

        public static void InsertSamuData(ObservableCollection<SamuReportView> samuReportViews)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    foreach (SamuReportView sm in samuReportViews)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        try
                        {
                            sqlcomm.CommandText = "insert into DisbursementFromSAMU(ApprovedDate,LoanAcNo,RequestID,FileName)values('" + sm.ApproveDate.ToString("MM-dd-yyyy") + "','" + sm.LoanAcNo + "','" + sm.RequestID + "','" + sm.FileName + "')";
                            sqlcomm.ExecuteNonQuery();
                        }
                        catch
                        {

                        }
                        
                    }

                }
            }
        }

        public static bool IsFileAlreadyExists(string Filename)
        {
            bool Result = false;
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandText = "select distinct count(FileName) from DisbursementFromSAMU where FileName='"+Filename+"'";
                int count = (int)sqlcomm.ExecuteScalar();
                if (count == 1)
                    Result = true;
            }

            return Result;
        }
    }
}
