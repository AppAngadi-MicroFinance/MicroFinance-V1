using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class LoginDetails
    {
        public string LoginDesignation { get; set; }
        public string EmpId { get; set; }
        public string BranchId { get; set; }
        public string RegionName { get; set; }
        string _userName;
        public LoginDetails(String UserName)
        {
            _userName = UserName;
            GetEmployeeID(_userName);
            GetBranchAndRegionNameForEmployee(_userName);
            GetDesignation(_userName);
        }
        void GetBranchAndRegionNameForEmployee(string userName)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select RegionName,Bid ,BranchName from BranchDetails where Bid = (select BranchId from EmployeeBranch where EmpId = (select EmpId from Employee where Name = '" + userName + "'))";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    RegionName = dataReader.GetString(0);
                    BranchId = dataReader.GetString(1);
                }
                dataReader.Close();
            }
        }

        void GetDesignation(string userName)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Designation from EmployeeBranch where EmpId = (select EmpId from Employee where Name = '" + userName + "')";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    LoginDesignation = dataReader.GetString(0);
                }
                dataReader.Close();
            }
        }

        void GetEmployeeID(string empName)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select EmpId from Employee where Name = '" + empName + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    EmpId = dataReader.GetString(0);
                }
                dataReader.Close();
            }
        }
        void GetDetailsOfEmployee()
        {
            try
            { 
                using(SqlConnection sql=new SqlConnection(Properties.Settings.Default.DBConnection))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select BranchDetails.RegionName,EmployeeBranch.Bid,EmployeeBranch.Empid,EmployeeBranch.Designation from EmployeeBranch join BranchDetails on EmployeeBranch.Bid=BranchDetails.Bid where Empid=(select EmpId from Employee where Name='" + _userName + "')";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        RegionName = dataReader.GetString(0);
                        BranchId = dataReader.GetString(1);
                        EmpId = dataReader.GetString(2);
                        LoginDesignation = dataReader.GetString(3);
                    }
                    dataReader.Close();
                }
            }
            catch(Exception EX)
            {
                throw new Exception(EX.Message);
            }
        }
    }
}
