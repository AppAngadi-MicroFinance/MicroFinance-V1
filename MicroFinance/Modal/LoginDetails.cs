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
            GetDetailsOfEmployee();
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
                    command.CommandText = "select BranchDetails.RegionName,BranchEmployees.Bid,BranchEmployees.Empid,BranchEmployees.Designation from BranchEmployees join BranchDetails on BranchEmployees.Bid=BranchDetails.Bid where Empid=(select EmpId from Employee where Name='" + _userName + "')";
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
            catch
            {

            }
        }
    }
}
