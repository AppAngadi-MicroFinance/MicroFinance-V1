using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MicroFinance.Modal
{
    class GenerateSavingsAccID
    {
        LoginDetails ld = new LoginDetails();

        public string GetRegionNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionCode from Region where RegionName='" + ld.RegionName + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }
        public string GetBranchNumber()
        {

            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchCode from BranchDetails where BranchName='" + ld.BranchId + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }

        public string GenerateSavingAccID() // Savings Account IDPattern SA0100220210605 (SA-SavingsAccount + 01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountOfCustomers+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select count(CustId) from SavingsAccount";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = "SA" + region + branch + year + month + ((count < 10) ? "0" + count : count.ToString());
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

        //public void NewSavingAcc(string id)
        //{
        //    int res = 0;
        //    var date = DateTime.Now;
        //    using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.db))
        //    {
        //        sqlcon.Open();
        //        if (sqlcon.State == ConnectionState.Open)
        //        {
        //            SqlCommand sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlcon;
        //            sqlcomm.CommandText = "IF (EXISTS (SELECT * FROM SavingsAccount WHERE CustId = '"+id+"' ))SELECT 1 AS res ELSE SELECT 0 AS res;";
        //            res = (int)sqlcomm.ExecuteScalar();
        //            if(res == 0)
        //            {
        //                sqlcomm.CommandText = "insert into SavingsAccount values ('" + id + "','" + GenerateSavingAccID() + "','" + date + "',"+1+")";
        //            }
        //        }
        //        sqlcon.Close();
        //    }
        //}
    }
}
