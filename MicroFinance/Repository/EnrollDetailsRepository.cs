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
        public static List<EnrollDetailsView> GetEnrollDetails(string BranchId,string EmpId)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication where EmployeeId='" + EmpId+"' and BranchId='"+BranchId+"' and LoanStatus<14 order by EnrollDate DESC";

                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
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


                    foreach(EnrollDetailsView EnrollCust in EnrollDetails)
                    {
                        sqlcomm.CommandText = "select Name from CustomerDetails where CustId='" + EnrollCust.CustomerID + "'";
                        EnrollCust.CustomerName =(string) sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select AadharNumber from CustomerDetails where CustId='" + EnrollCust.CustomerID + "'";
                        EnrollCust.AadharNumber = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGId from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+EnrollCust.CustomerID+"'))";
                        EnrollCust.CenterID = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGName from selfhelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + EnrollCust.CustomerID + "'))";
                        EnrollCust.CenterName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select Name from Employee where EmpId='"+EnrollCust.EmployeeId+"'";
                        EnrollCust.EmployeeName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select BranchName from BranchDetails where Bid='" + EnrollCust.BranchId + "'";
                        EnrollCust.BranchName = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return EnrollDetails;
        }
        public static List<EnrollDetailsView> GetEnrollDetails(string BranchID)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId,EnrollDate,EmployeeId,BranchId,LoanStatus from LoanApplication where BranchId='" + BranchID + "' and LoanStatus<14 order by EnrollDate DESC";

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
