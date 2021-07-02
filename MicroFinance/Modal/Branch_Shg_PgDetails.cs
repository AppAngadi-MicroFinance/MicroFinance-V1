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
        //        sqlCommand.CommandText = "select BranchName from BranchDetails where RegionName = (select RegionName from BranchDetails where Bid = (select Bid from BranchEmployees where Empid = '"+EmpId+"'))";
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
                sqlCommand.CommandText = "select RegionName from BranchDetails where Bid = (select Bid from BranchEmployees where Empid = '"+EmpId+"')";
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
                sqlCommand.CommandText = "select BranchName from BranchDetails where Bid = (select Bid from BranchEmployees where Empid = '" + EmpId + "')";
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
        public List<string> GetSelfHelpGroup()
        {
            List<String> SHG = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select SHGName from SelfHelpGroup where EmpId='" + EmpId + "'";
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while(dataReader.Read())
                {
                    SHG.Add(dataReader.GetString(0));
                }
            }
            return SHG;
        }
        public List<string> GetPeerGroup(string SHGName)
        {
            List<String> PG = new List<string>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select PeerGroup.PeerGroupName from PeerGroup join SelfHelpGroup on PeerGroup.PGId = SelfHelpGroup.PGId where SHGName = '"+SHGName+"'";
                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    PG.Add(dataReader.GetString(0));
                }
            }
            return PG;
        }
    }
}
