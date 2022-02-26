using MicroFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Repository
{
    public class BranchReportRepository
    {
        public static BranchReportCentervice GetBranchLoanCountCenterWise(string BranchId,DateModel DateData)
        {
            BranchReportCentervice LoanDetails = new BranchReportCentervice();
            List<CenterLoanDetail> CenterDetails = new List<CenterLoanDetail>();
            List<string> BranchEmployees = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.BranchId == BranchId).Select(temp => temp.EmployeeId).ToList();
            List<string> Centers = MainWindow.BasicDetails.CenterList.Where(temp => BranchEmployees.Contains(temp.EmpId) == true).Select(temp => temp.SHGId).ToList();
            //01202109001
            LoanDetails.BranchCode = BranchId.Substring(8);
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                   foreach(string Center in Centers)
                    {
                        CenterLoanDetail Detail = GetCenterDetails(Center, DateData, sqlconn);
                        CenterDetails.Add(Detail);
                    }
                }
            }
            LoanDetails.CenterLoanDetailsData = CenterDetails.Where(temp=>temp.IsValidData==true).ToList();
            return LoanDetails;
        }

        public static List<BranchReportEmployeeWise> GetBranchLoanCountEmployeeWise(string BranchID,DateModel DateData)
        {
            List<BranchReportEmployeeWise> EmployeeDetails = new List<BranchReportEmployeeWise>();
            List<string> Employees = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.BranchId == BranchID && temp.Designation=="Field Officer").Select(temp => temp.EmployeeId).ToList();
            string Branchname = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == BranchID).Select(temp => temp.BranchName).FirstOrDefault();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    foreach (string emp in Employees)
                    {
                        BranchReportEmployeeWise EmpDetails = GetEmployeeDetails(emp, DateData, sqlconn);
                        EmpDetails.BranchName = Branchname;

                        EmployeeDetails.Add(EmpDetails);

                    }
                }
            }

            return EmployeeDetails;
        }


        public static List<BranchReportEmployeeWise> GetBranchLoanAmountEmployeewise(string BranchID,DateModel DateData)
        {
            List<BranchReportEmployeeWise> LoanAmountData = new List<BranchReportEmployeeWise>();
            List<BranchReportEmployeeWise> EmployeeDetails = GetBranchLoanCountEmployeeWise(BranchID, DateData);
            foreach(BranchReportEmployeeWise Det in EmployeeDetails)
            {
                List<MonthDetails> LoanAmountDetails = new List<MonthDetails>();
                foreach(MonthDetails month in Det.LoanCountDetails)
                {
                    int amount = month.MonthValue * 30000;
                    MonthDetails details = new MonthDetails { MonthName = month.MonthName, MonthValue = amount };
                    LoanAmountDetails.Add(details);
                }
                BranchReportEmployeeWise EmpDet = new BranchReportEmployeeWise { BranchName = Det.EmployeeName,EmployeeName=Det.EmployeeName,LoanCountDetails=LoanAmountDetails };
                LoanAmountData.Add(EmpDet);
            }
            return LoanAmountData;
        }
        static BranchReportEmployeeWise GetEmployeeDetails(string EmpID,DateModel DateData,SqlConnection _sqlConnection)
        {
            BranchReportEmployeeWise EmployeDetails = new BranchReportEmployeeWise();
            List<MonthDetails> LoanData = new List<MonthDetails>();
            int month = DateData.FromDate.Month;
            int year = DateData.FromDate.Year;
            int MonthDifferece = GetMonthsBetween(DateData.FromDate, DateData.ToDate);
            SqlConnection sqlconn = _sqlConnection;
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;

            
            for(int i=0;i<MonthDifferece;i++)
            {
                DateTime FDate = Convert.ToDateTime("1-" + month.ToString() + "-" + year.ToString());
                DateTime TDate = Convert.ToDateTime(DateTime.DaysInMonth(year, month).ToString() + "-" + month + "-" + year);
                sqlcomm.CommandText = "select Count(*) from LoanDetails where RequestedBy='"+EmpID+"' and ApproveDate Between '"+FDate.ToString("yyyy-MM-dd")+"' and '"+TDate.ToString("yyyy-MM-dd")+"'";
                int Count = (int)sqlcomm.ExecuteScalar();
                MonthDetails details = new MonthDetails { MonthName = FDate.ToString("MMM-yyyy"), MonthValue = Count };
                LoanData.Add(details);
                if(month==12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }
            }
            EmployeDetails.EmployeeName = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == EmpID).Select(temp => temp.EmployeeName).FirstOrDefault();
            EmployeDetails.LoanCountDetails = LoanData;
            return EmployeDetails;
        }

        static CenterLoanDetail GetCenterDetails(string CenterId,DateModel DateData,SqlConnection _sqlConnection)
        {
            CenterLoanDetail CenterData = new CenterLoanDetail();
            List<MonthDetails> LoanData = new List<MonthDetails>();
            int month = DateData.FromDate.Month;
            int year = DateData.FromDate.Year;
            int MonthDifferece = GetMonthsBetween(DateData.FromDate, DateData.ToDate);
            CenterData.IsValidData = false;
            SqlConnection sqlconn = _sqlConnection;
            SqlCommand sqlcomm = new SqlCommand();
            sqlcomm.Connection = sqlconn;
            for (int i=0;i<MonthDifferece;i++)
            {
                DateTime FDate =Convert.ToDateTime("1-"+month.ToString()+"-"+year.ToString());
                DateTime TDate =Convert.ToDateTime(DateTime.DaysInMonth(year,month).ToString()+"-"+month+"-"+year);
                sqlcomm.CommandText = "select Count(*) from LoanDetails where CustomerID in (select CustId from CustomerGroup where SHGID = '"+CenterId+"') and ApproveDate between '"+FDate.ToString("yyyy-MM-dd")+"' and '"+TDate.ToString("yyyy-MM-dd")+"'";
                int Count = (int)sqlcomm.ExecuteScalar();
                if(Count>0)
                {
                    CenterData.IsValidData = true;
                }
                LoanData.Add(new MonthDetails { MonthName = FDate.ToString("MMM-yyyy"),MonthValue=Count });
                if(month==12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }
            }
            string Centername = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == CenterId).Select(temp => temp.SHGName).FirstOrDefault();
            CenterData.CenterName = Centername;
            CenterData.LoanDetails = LoanData;
            return CenterData;
        }

         static int GetMonthsBetween(DateTime FromDate, DateTime ToDate)
        {
            if (FromDate > ToDate)
                return GetMonthsBetween(ToDate, FromDate);

            var MonthDiff = Math.Abs((ToDate.Year * 12 + (ToDate.Month - 1)) - (FromDate.Year * 12 + (FromDate.Month - 1)));

            if (FromDate.AddMonths(MonthDiff) > ToDate || ToDate.Day < FromDate.Day)
            {
                return MonthDiff - 1;
            }
            else
            {
                return MonthDiff;
            }
        }
    }
    public struct BranchReportCentervice
    {
        public string BranchCode { get; set; }
        public List<CenterLoanDetail> CenterLoanDetailsData { get; set; }
    }
    public struct BranchReportEmployeeWise
    {
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
        public List<MonthDetails> LoanCountDetails { get; set; }
    }
    public struct CenterLoanDetail
    {
        public bool IsValidData { get; set; }
        public string CenterName { get; set; }
        public List<MonthDetails> LoanDetails { get; set; }
    }
    public struct MonthDetails
    {
        public string MonthName { get; set; }
        public int MonthValue { get; set; }
    }
}
