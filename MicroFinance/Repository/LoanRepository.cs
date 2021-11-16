using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
using MicroFinance.Reports;
using MicroFinance.ViewModel;

namespace MicroFinance.ViewModel
{
    class LoanRepository
    {


        public static ObservableCollection<RecommendView> GetRecommendList(int StatusCode)
        {
            ObservableCollection<RecommendView> ResultView = new ObservableCollection<RecommendView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='" + StatusCode+"') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and LoanApplication.BranchId='"+MainWindow.LoginDesignation.BranchId+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RecommendView HMRequestCustomer = new RecommendView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            HMRequestCustomer.IsRecommend = true;
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (RecommendView Hm in ResultView)
                    {
                        
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '"+Hm.CustomerID+"'))";
                        Hm.CollectionDay = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }

        public static ObservableCollection<RecommendView> GetRecommendList(int StatusCode,string EmpId)
        {
            ObservableCollection<RecommendView> ResultView = new ObservableCollection<RecommendView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='" + StatusCode + "') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and LoanApplication.BranchId='" + MainWindow.LoginDesignation.BranchId + "' and LoanApplication.EmployeeId='"+EmpId+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RecommendView HMRequestCustomer = new RecommendView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            HMRequestCustomer.IsRecommend = true;
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (RecommendView Hm in ResultView)
                    {

                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + Hm.CustomerID + "'))";
                        Hm.CollectionDay = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }
        public static ObservableCollection<RecommendView> GetRecommendListForRM(int StatusCode)
        {
            ObservableCollection<RecommendView> ResultView = new ObservableCollection<RecommendView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='" + StatusCode + "') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RecommendView HMRequestCustomer = new RecommendView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            HMRequestCustomer.IsRecommend = true;
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (RecommendView Hm in ResultView)
                    {

                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + Hm.CustomerID + "'))";
                        Hm.CollectionDay = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }


        public static ObservableCollection<RecommendView> GetRecommendList(int StatusCode,bool value=true)
        {
            ObservableCollection<RecommendView> ResultView = new ObservableCollection<RecommendView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.LoanType,LoanApplication.EnrollDate,DisbursementFromSAMU.ApprovedDate from CustomerDetails,LoanApplication,BranchDetails,Employee,DisbursementFromSAMU where LoanApplication.RequestId in(select RequestId from LoanApplication where LoanStatus='" + StatusCode+"') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and DisbursementFromSAMU.RequestID=LoanApplication.RequestId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RecommendView HMRequestCustomer = new RecommendView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.LoanType = reader.GetString(9);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(10);
                            HMRequestCustomer.SamuApproveDate = reader.GetDateTime(11);
                            HMRequestCustomer.IsRecommend = true;
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (RecommendView Hm in ResultView)
                    {

                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + Hm.CustomerID + "'))";
                        Hm.CollectionDay = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }


        public static ObservableCollection<RecommendView> GetApproveList(int StatusCode, bool value = true)
        {
            ObservableCollection<RecommendView> ResultView = new ObservableCollection<RecommendView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.LoanType,LoanApplication.EnrollDate,DisbursementFromSAMU.ApprovedDate from CustomerDetails,LoanApplication,BranchDetails,Employee,DisbursementFromSAMU where LoanApplication.RequestId in(select RequestId from LoanApplication where LoanStatus='" + StatusCode + "') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and DisbursementFromSAMU.RequestID=LoanApplication.RequestId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RecommendView HMRequestCustomer = new RecommendView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.LoanType = reader.GetString(9);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(11);

                            HMRequestCustomer.IsRecommend = true;
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (RecommendView Hm in ResultView)
                    {

                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + Hm.CustomerID + "'))";
                        Hm.CollectionDay = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }



        public static int RecommendLoans(ObservableCollection<RecommendView> recommends,int StatusCode)
        {
            int count = 0;
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    foreach(RecommendView view in recommends)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        if (view.IsRecommend==true)
                        {
                            sqlcomm.CommandText = "update LoanApplication set LoanStatus='" + StatusCode + "' where RequestId='" + view.RequestID + "'";
                            sqlcomm.ExecuteNonQuery();
                            count++;
                        }
                    }
                }
                sqlconn.Close();
            }
            return count;
        }


        public static bool IsAlreadyInApplicationProcess(string CustomerID)
        {
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(*) from LoanApplication where LoanStatus not in(0,3,13,14) and CustId='"+CustomerID+"'"; ;
                    int Count =(int) sqlcomm.ExecuteScalar();
                    if (Count != 0)
                        return true;
                }
                sqlconn.Close();
            }
            return false;
        }



        public static void RejectLoan(string ReqID)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='13' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }





        public static int ApproveLoan()
        {
            return 0;
        }



        public static string GenerateLoanID(string BranchID) // IDPattern 02001202106R05 (02-Region/001-Branch/2021-CurrentYear/06-CurrentMonth/R-Request(Spe)/(No.of Loan given in currentYear+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select Count(LoanID) from LoanDetails where LoanID like '%" + year + "%'";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = BranchID.Substring(0, 2);
            string branch = BranchID.Substring(8);
            Result = region + branch + year + month + "GL" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }



        public static void ApproveLoans(ObservableCollection<RecommendView> recommends)
        {
           // GetRequestDetails(ID);
           // ChangeLoanStatus(ID, 12);
            
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;

                    foreach(RecommendView rm in recommends)
                    {
                        if(rm.IsRecommend==true)
                        {
                            
                            string LoanId = GenerateLoanID(rm.BranchID);
                            sqlcomm.CommandText = "insert into LoanDetails(LoanID,CustomerID,LoanType,LoanPeriod,InterestRate,RequestedBY,ApprovedBy,ApproveDate,LoanAmount,IsActive)values('" + LoanId + "','" + rm.CustomerID + "','" + rm.LoanType + "'," + rm.LoanPeriod + ",'12','" + rm.EmpId + "','','" + rm.SamuApproveDate.ToString("MM-dd-yyyy") + "'," + rm.LoanAmount + ",'true')";
                            sqlcomm.ExecuteNonQuery();
                            sqlcomm.CommandText = "select EmpId from EmployeeBranch where BranchId=(select BranchId from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + rm.CustomerID + "'))) and Designation='Manager'";
                            string EmpId = (string)sqlcomm.ExecuteScalar();
                            sqlcomm.CommandText = "update LoanDetails set ApprovedBY='" + EmpId + "' where LoanID='" + LoanId + "'";
                            sqlcomm.ExecuteNonQuery();
                            sqlcomm = new SqlCommand();
                            sqlcomm.Connection = sqlconn;
                            sqlcomm.CommandText = "update CustomerDetails set IsActive='true' where CustId='" + rm.CustomerID + "'";
                            int Result = (int)sqlcomm.ExecuteNonQuery();
                            sqlcomm.CommandText = "update DisbursementFromSAMU set LoanID='" + LoanId + "' where RequestID='" + rm.RequestID + "'";
                            sqlcomm.ExecuteNonQuery();
                            if (Result == 1)
                            {
                                LoadData1(LoanId, rm.CustomerID, rm.LoanAmount, rm.LoanPeriod, rm.BranchID, rm.CollectionDay,rm.SamuApproveDate);
                                NewSavingAcc(rm.CustomerID, rm.BranchID);

                                //Suma.InsertData(SUMAObj, LoanId);

                            }
                        }
                        
                    }
                   
                }
                sqlconn.Close();
            }

        }
        static void LoadData1(string LoanID,string CustomerID,int LoanAmount,int LoanPeroid,string BranchID,string Collectionday,DateTime SamuapprovalDate)
        {
            // GetLoanDetails(CustId);
            DateTime ApproveDate = SamuapprovalDate;
            DayOfWeek CollectionDay = WeekDay(Collectionday);
            DateTime NextCollectionDate = CollectionDate(CollectionDay);
            List<Loan> LoanCollectionList = Interestcc(LoanAmount, LoanPeroid, ApproveDate, NextCollectionDate);
            foreach (Loan item in LoanCollectionList)
            {
                InsertIntoLoanMaster(BranchID, CustomerID, LoanID, item.WeekNo, item.DueDate, item.Amount, item.Interest, item.Total);
            }
        }

        
        public static List<Loan> Interestcc(int amount, int weeksCount, DateTime loanIssuedDate, DateTime nextDueDate)
        {
            //int days = (nextDueDate - loanIssuedDate).Days;
            //if (days >= excuseDays)
            //    nextDueDate = nextDueDate.AddDays(7);

            int[] InterestSeq = new int[] { 5, 4, 2, 1, 0 };
            int interval = weeksCount / 5;
            List<Loan> Collection = new List<Loan>();

            int SinglePayment = amount / weeksCount;

            int PaymentCount = 0;
            int periodd = 0;
            for (int i = 0; i < weeksCount; i++)
            {
                PaymentCount++;
                Collection.Add(new Loan((i + 1), nextDueDate, SinglePayment, AmountForPercent(amount, InterestSeq[periodd]) / interval));
                nextDueDate = nextDueDate.AddDays(7);
                if (PaymentCount == interval)
                {
                    PaymentCount = 0;
                    periodd++;
                }
            }
            return Collection;
        }
        static void InsertIntoLoanMaster(string branchId, string custId, string LoanId, int weekNo, DateTime dueDate, int principal, int interest, int total)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "insert into LoanCollectionMaster(BranchId, CustId, LoanId, WeekNo, DueDate, Principal, Interest, Total) values('" + branchId + "','" + custId + "','" + LoanId + "'," + weekNo + ",'" + dueDate.ToString("yyyy-MM-dd") + "'," + principal + "," + interest + "," + total + ")";
                command.ExecuteNonQuery();
                sql.Close();
            }
        }
        public static int AmountForPercent(int amount, int percent)
        {
            decimal cal = amount / 100;
            decimal cal2 = cal * percent;
            return Convert.ToInt32(cal2);
        }
        public static DateTime CollectionDate(DayOfWeek day)
        {
            DateTime ResultDate = new DateTime();
            DayOfWeek Today = DateTime.Now.DayOfWeek;
            int calValue = ((int)day - (int)Today);
            if (calValue == 0)
            {
                ResultDate = DateTime.Now.AddDays(7);
            }
            else if (calValue > 3)
            {
                ResultDate = DateTime.Now.AddDays(calValue);
            }
            else if (calValue <= 3 && calValue > 0)
            {
                ResultDate = DateTime.Now.AddDays(calValue + 7);
            }
            else if (calValue < 0)
            {
                int a = 7 - Math.Abs(calValue);
                if (Math.Abs(calValue) > 3)
                {
                    ResultDate = DateTime.Now.AddDays(7 + a);
                }
                else
                {
                    ResultDate = DateTime.Now.AddDays(a);
                }


            }
            return ResultDate;
        }
        public static DayOfWeek WeekDay(string Value)
        {
            DayOfWeek result = new DayOfWeek();
            Value = Value.ToLower();
            switch (Value)
            {
                case "monday":
                    result = DayOfWeek.Monday;
                    break;
                case "tuesday":
                    result = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                    result = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                    result = DayOfWeek.Thursday;
                    break;
                case "friday":
                    result = DayOfWeek.Friday;
                    break;
                case "saturday":
                    result = DayOfWeek.Saturday;
                    break;
                case "sunday":
                    result = DayOfWeek.Sunday;
                    break;
            }
            return result;
        }

        public static void NewSavingAcc(string id,string BranchID)
        {
            string regionCode = BranchID.Substring(0, 2);
            string branchCode = BranchID.Substring(8);
            GenerateSavingsAccID SA = new GenerateSavingsAccID();
            int res = 0;
            var date = DateTime.Now.ToString("MM-dd-yyyy");
            using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "IF (EXISTS (SELECT CustId FROM SavingsAccount WHERE CustId = '" + id + "' ))SELECT 1 AS res ELSE SELECT 0 AS res;";
                    res = (int)sqlcomm.ExecuteScalar();
                    if (res == 0)
                    {
                        sqlcomm.CommandText = "insert into SavingsAccount values ('" + id + "','" + SA.GenerateSavingAccID(regionCode, branchCode) + "','" + date + "'," + 1 + ")";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
                sqlcon.Close();
            }
        }



        public static List<string> GetAllPurposeNames()
        {
            List<string> PurposeList = new List<string>();

            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select LoanPurposeName from LoanPurpose ORDER BY LoanPurposeName ASC";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            PurposeList.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                    sqlconn.Close();
                }
            }
            return PurposeList;
        }


        public static List<LoanApplicationViewModel> LoanApplicationDetails(string CustomerID)
        {
            List<LoanApplicationViewModel> Applications = new List<LoanApplicationViewModel>();
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select LoanAmount,LoanType,LoanPeriod,EnrollDate,EmployeeId,LoanStatus,Purpose from LoanApplication where CustId='" + CustomerID+ "' order by EnrollDate DESC";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            LoanApplicationViewModel LoanDetail = new LoanApplicationViewModel();
                            LoanDetail.LoanAmount = reader.GetInt32(0);
                            LoanDetail.LoanType = reader.GetString(1);
                            LoanDetail.LoanPeriod = reader.GetInt32(2);
                            LoanDetail.EnrollDate = reader.GetDateTime(3);
                            LoanDetail.RequestedBy = reader.GetString(4);
                            LoanDetail.StatusCode = reader.GetInt32(5);
                            LoanDetail.LoanPurpose = reader.GetString(6);

                            Applications.Add(LoanDetail);
                        }
                    }
                }
            }

                return Applications;
        }

        public static List<LoanViewModel> LoanDetails(string CustomerID)
        {
            List<LoanViewModel> Loans = new List<LoanViewModel>();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select LoanID,LoanType,LoanAmount,LoanPeriod,InterestRate,ApproveDate,RequestedBY,ApprovedBy,IsActive from LoanDetails where CustomerID='" + CustomerID + "' order by ApproveDate DESC";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            LoanViewModel Loan = new LoanViewModel();
                            Loan.LoanId = reader.GetString(0);
                            Loan.LoanType = reader.GetString(1);
                            Loan.LoanAmount = reader.GetInt32(2);
                            Loan.LoanPeriod = reader.GetInt32(3);
                            Loan.InterestRate = reader.GetInt32(4);
                            Loan.ApproveDate = reader.GetDateTime(5);
                            Loan.RequestedBY = reader.GetString(6);
                            Loan.ApprovedBy = reader.GetString(7);
                            Loan.IsActive = reader.GetBoolean(8);
                            Loans.Add(Loan);
                        }
                    }
                    reader.Close();
                    foreach(LoanViewModel loan in Loans)
                    {
                        sqlcomm.CommandText = "select count(Principal) as CollectedAmount from loanCollectionentry where loanid='" + loan.LoanId + "'";
                        int Result = (int)sqlcomm.ExecuteScalar();
                        if(Result==0)
                        {
                            loan.PaidedAmount = 0;
                        }
                        else
                        {
                            sqlcomm.CommandText= "select sum(Principal) as CollectedAmount from loanCollectionentry where loanid='" + loan.LoanId + "'";
                            loan.PaidedAmount = (int)sqlcomm.ExecuteScalar();
                        }
                    }
                }
            }
            return Loans;
        }



    }
    public class Loan
    {
        public int WeekNo { get; set; }
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }
        public Loan(int weekNo, DateTime date, int amount, int interest)
        {
            WeekNo = weekNo;
            DueDate = date;
            Amount = amount;
            Interest = interest;
            Total = amount + interest;
        }
    }
}
