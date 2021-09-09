using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    class GenerateCustomerId
    {
        public string BranchName { get; set; }
        public string Region { get; set; }


        public string GetRegionNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionCode from Region where RegionName='" + Region + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }
        public string GetBranchNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchCode from BranchDetails where BranchName='" + BranchName + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }
        public string GenerateCustomerID() // IDPattern 0100220210605 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountOfCustomers+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "Select Count(Name) from CustomerDetails";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }

        public string DigitConvert(string digit, int place = 3)
        {
            StringBuilder sb = new StringBuilder();
            string number = digit;
            string Result = "";
            if (number.Length < place)
            {
                for (int i = 0; i < (place - (number.Length)); i++)
                {
                    sb.Append(0);
                }
                Result = sb.ToString() + number;
            }
            else
            {
                Result = number;
            }

            return Result;
        }
    }
}
