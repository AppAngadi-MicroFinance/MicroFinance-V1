using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.ViewModel;
using MicroFinance.Repository;
using System.Data.SqlClient;
using System.Data;

namespace MicroFinance.Repository
{
    public class EnrollDetailsRepository
    {
        public static List<EnrollDetailsView> GetEnrollDetails(string BranchId,DateModel DateData)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,CustomerDetails.AadharNumber,CustomerDetails.CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication,CustomerDetails where BranchId='"+BranchId+ "' and CustomerDetails.CustId=LoanApplication.CustId and LoanStatus<14 and LoanApplication.EnrollDate between '" + DateData.FromDate.ToString("MM-dd-yyyy") + "' and '" + DateData.EndDate.ToString("MM-dd-yyyy") + "' order by EnrollDate DESC";

                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            EnrollDetailsView Details = new EnrollDetailsView();
                            Details.CustomerName = reader.GetString(0);
                            Details.AadharNumber = reader.GetString(1);
                            Details.CustomerID = reader.GetString(2);
                            Details.EnrollDate = reader.GetDateTime(3);
                            Details.EmployeeId = reader.GetString(4);
                            Details.BranchId = reader.GetString(5);
                            Details.LoanStatusCode = reader.GetInt32(6);

                            EnrollDetails.Add(Details);
                        }
                    }
                    reader.Close();


                    foreach(EnrollDetailsView EnrollCust in EnrollDetails)
                    {
                        sqlcomm.CommandText = "select SHGId from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+EnrollCust.CustomerID+"'))";



                        EnrollCust.CenterID = (string)sqlcomm.ExecuteScalar();
                        EnrollCust.CenterName = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == EnrollCust.CenterID).Select(temp => temp.SHGName).FirstOrDefault();
                        EnrollCust.EmployeeName = MainWindow.BasicDetails.EmployeeList.Where(temp=>temp.EmployeeId==EnrollCust.EmployeeId).Select(temp=>temp.EmployeeName).FirstOrDefault();
                        EnrollCust.BranchName = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == EnrollCust.BranchId).Select(temp => temp.BranchName).FirstOrDefault();
                    }
                }
            }
            return EnrollDetails;
        }
        public static List<EnrollDetailsView> GetEnrollDetails(string BranchId, string EmpId,DateModel DateData)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,CustomerDetails.AadharNumber,CustomerDetails.CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication,CustomerDetails where EmployeeId='"+EmpId+"' and BranchId='"+BranchId+"' and LoanStatus<14 and LoanApplication.CustId=CustomerDetails.CustId and LoanApplication.EnrollDate between '"+DateData.FromDate.ToString("MM-dd-yyyy")+"' and '"+DateData.EndDate.ToString("MM-dd-yyyy")+"' order by EnrollDate DESC";

                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EnrollDetailsView Details = new EnrollDetailsView();
                            Details.CustomerName = reader.GetString(0);
                            Details.AadharNumber = reader.GetString(1);
                            Details.CustomerID = reader.GetString(2);
                            Details.EnrollDate = reader.GetDateTime(3);
                            Details.EmployeeId = reader.GetString(4);
                            Details.BranchId = reader.GetString(5);
                            Details.LoanStatusCode = reader.GetInt32(6);

                            EnrollDetails.Add(Details);
                        }
                    }
                    reader.Close();


                    foreach (EnrollDetailsView EnrollCust in EnrollDetails)
                    {
                        sqlcomm.CommandText = "select SHGId from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + EnrollCust.CustomerID + "'))";



                        EnrollCust.CenterID = (string)sqlcomm.ExecuteScalar();
                        EnrollCust.CenterName = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == EnrollCust.CenterID).Select(temp => temp.SHGName).FirstOrDefault();
                        EnrollCust.EmployeeName = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == EnrollCust.EmployeeId).Select(temp => temp.EmployeeName).FirstOrDefault();
                        EnrollCust.BranchName = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == EnrollCust.BranchId).Select(temp => temp.BranchName).FirstOrDefault();
                    }
                }
            }
            return EnrollDetails;
        }
        public static List<EnrollDetailsView> GetEnrollDetails(DateModel DateData)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,CustomerDetails.AadharNumber,CustomerDetails.CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication,CustomerDetails where CustomerDetails.CustId=LoanApplication.CustId and LoanStatus<14 and LoanApplication.EnrollDate between '" + DateData.FromDate.ToString("MM-dd-yyyy") + "' and '" + DateData.EndDate.ToString("MM-dd-yyyy") + "' order by EnrollDate DESC";

                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EnrollDetailsView Details = new EnrollDetailsView();
                            Details.CustomerName = reader.GetString(0);
                            Details.AadharNumber = reader.GetString(1);
                            Details.CustomerID = reader.GetString(2);
                            Details.EnrollDate = reader.GetDateTime(3);
                            Details.EmployeeId = reader.GetString(4);
                            Details.BranchId = reader.GetString(5);
                            Details.LoanStatusCode = reader.GetInt32(6);

                            EnrollDetails.Add(Details);
                        }
                    }
                    reader.Close();


                    foreach (EnrollDetailsView EnrollCust in EnrollDetails)
                    {
                        sqlcomm.CommandText = "select SHGId from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + EnrollCust.CustomerID + "'))";



                        EnrollCust.CenterID = (string)sqlcomm.ExecuteScalar();
                        EnrollCust.CenterName = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == EnrollCust.CenterID).Select(temp => temp.SHGName).FirstOrDefault();
                        EnrollCust.EmployeeName = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == EnrollCust.EmployeeId).Select(temp => temp.EmployeeName).FirstOrDefault();
                        EnrollCust.BranchName = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == EnrollCust.BranchId).Select(temp => temp.BranchName).FirstOrDefault();
                    }
                }
            }
            return EnrollDetails;
        }
        public static List<EnrollDetailsView> GetEnrollDetails()
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication LoanStatus<14 order by EnrollDate DESC";

                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EnrollDetailsView Details = new EnrollDetailsView();
                            Details.CustomerID = reader.GetString(0);
                            Details.EnrollDate = reader.GetDateTime(1);
                            Details.EmployeeId = reader.GetString(2);
                            Details.BranchId = reader.GetString(3);
                            Details.LoanStatusCode = reader.GetInt32(4);

                            EnrollDetails.Add(Details);
                        }
                    }
                    reader.Close();


                    foreach (EnrollDetailsView EnrollCust in EnrollDetails)
                    {
                        sqlcomm.CommandText = "select Name from CustomerDetails where CustId='" + EnrollCust.CustomerID + "'";
                        EnrollCust.CustomerName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select AadharNumber from CustomerDetails where CustId='" + EnrollCust.CustomerID + "'";
                        EnrollCust.AadharNumber = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGId from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + EnrollCust.CustomerID + "'))";
                        EnrollCust.CenterID = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGName from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + EnrollCust.CustomerID + "'))";
                        EnrollCust.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select Name from Employee where EmpId='" + EnrollCust.EmployeeId + "'";
                        EnrollCust.EmployeeName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select BranchName from BranchDetails where Bid='" + EnrollCust.BranchId + "'";
                        EnrollCust.BranchName = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return EnrollDetails;
        }
    }
}
