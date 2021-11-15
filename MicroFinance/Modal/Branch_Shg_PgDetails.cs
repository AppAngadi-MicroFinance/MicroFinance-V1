using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    class Branch_Shg_PgDetails
    {
        public string EmpId { get; set; }
        public string EmpDesignation { get; set; }
        public string ConnectionString = Properties.Settings.Default.DBConnection;

        //public List<string> GetBranchNames()
        //{
        //    List<String> BN = new List<string>();

        //    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        sqlConnection.Open();
        //        SqlCommand sqlCommand = new SqlCommand();
        //        sqlCommand.Connection = sqlConnection;
        //        sqlCommand.CommandText = "select BranchName from BranchDetails where RegionName = (select RegionName from BranchDetails where Bid = (select Bid from BranchEmployees where Empid = '" + EmpId + "'))";
        //        SqlDataReader dataReader = sqlCommand.ExecuteReader();
        //        while (dataReader.Read())
        //        {
        //            BN.Add(dataReader.GetString(0));
        //        }
        //    }
        //    return BN;
        //}

        public string GetRegionNameofEmployee()
        {
            string RegionName = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select RegionName from BranchDetails where Bid = (select BranchId from EmployeeBranch where Empid = '"+EmpId+"')";
                RegionName = sqlCommand.ExecuteScalar().ToString();
            }
            return RegionName;
        }
        public string GetBranchNameofEmployee()
        {
            string Name = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select BranchName from BranchDetails where Bid = (select BranchId from EmployeeBranch where Empid = '" + EmpId + "')";
                Name = sqlCommand.ExecuteScalar().ToString();
            }
            return Name;
        }
        public string GetEmployeeName()
        {
            string Name = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select Name from Employee where EmpId='"+EmpId+"'";
                Name = sqlCommand.ExecuteScalar().ToString();
            }
            return Name;
        }

        public string GetBranchID()
        {
            string ID = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select BranchID from EmployeeBranch where EmpId='"+EmpId+"'";
                ID = sqlCommand.ExecuteScalar().ToString();
            }
            return ID;
        }
        public List<string> GetSelfHelpGroup()
        {
            List<String> SHG = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select SHGName from SelfHelpGroup where SHGId in(select SHGId from TimeTable where EmpId='"+EmpId+"')";
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while(dataReader.Read())
                {
                    SHG.Add(dataReader.GetString(0));
                }
            }
            return SHG;
        }
        public List<PGView> GetPeerGroup(string SHGName)
        {
            List<PGView> PGList = new List<PGView>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select GroupID,GroupName from PeerGroup where SHGid=(select distinct SHGId from SelfHelpGroup where SHGName='" + SHGName+"' and BranchId='"+GetBranchID()+"')";
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    PGView PG = new PGView();
                    PG.GroupID = dataReader.GetString(0);
                    PG.GroupName = dataReader.GetString(1);

                    PGList.Add(PG);
                    
                }
            }
            return PGList;
        }

        public string GetCustomerPG(string CustId)
        {
            string PgName = "";
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select PeerGroupId from CustomerGroup where CustId='"+CustId+"'";
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    PgName = dataReader.GetString(0);
                }
            }
            return PgName;
        }
    }
    public class PGView
    {
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }
}
