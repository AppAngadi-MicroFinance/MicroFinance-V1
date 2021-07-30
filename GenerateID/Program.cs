using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateID
{
    class Program
    {
        string ConnectionString = "Data Source=.;Initial Catalog=MicroFinance;Integrated Security=True";
        public string BranchID { get; set; }
        public string Region { get; set; }
        static void Main(string[] args)
        {
            //GenerateRegionID();
        }

        public string GetRegionNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from Region where RegionName='" + Region + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }
        public string GetBranchNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from BranchDetails where BranchName='" + BranchID + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }

        public string GenerateRegionID()
        {
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            string finalID = "R"+year+""+month+""+GetRegionsCount();
            return finalID;
        }
        static string GetRegionsCount()
        {
            int Count = 0;
            using (SqlConnection sqlcon = new SqlConnection("Data Source=.;Initial Catalog=MicroFinance3;Integrated Security=True"))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select count(RegionID) from Region";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            if (Count == 0)
                Count = 1;

            if (Count < 10)
                return "00" + Count;
            else if (Count < 100)
                return "0" + Count;
            else
            {
                return Count.ToString();
            }
        }

        public string GeneratePGID() // IDPattern 01002202106SHG-05 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountofShg+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "Select Count( Field_Name ) from (Table Name)";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "PG-" + ((count < 10) ? "0" + count : count.ToString());
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
