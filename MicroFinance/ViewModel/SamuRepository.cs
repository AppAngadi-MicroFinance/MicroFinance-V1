using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='9') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId";
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

    }
}
