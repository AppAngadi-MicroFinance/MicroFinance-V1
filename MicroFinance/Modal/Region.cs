﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{
    class Region:BindableBase
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        private string _regionname;
        public string RegionName
        {
            get
            {
                return _regionname;
            }
            set
            {
                _regionname = value;
                RaisedPropertyChanged("RegionName");
            }
        }
        public int GetRegionCount()
        {
            int number = 1;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = sqlconn;
                    sqlComm.CommandText = "select count(RegionName) from Region";
                    int n = (int)sqlComm.ExecuteScalar();
                    number += n;
                }
                sqlconn.Close();
            }
            return number;
        }
        public void AddRegion()
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into Region (RegionCode,RegionId,RegionName)values(" + GetRegionCount() + ",'" + GenerateRegionID() + "','" + _regionname + "')";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
        }
        public string GenerateRegionID()
        {
            int count = GetRegionCount();
            string Result = "R"+DigitConvert(count.ToString(),2);
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

        public bool Isexist()
        {
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText="Select RegionName from Region Where RegionName='"+_regionname+"'";
                    string result=(string) sqlcomm.ExecuteScalar();
                    if(result!=null)
                    {
                        if (result.Equals(RegionName, StringComparison.CurrentCultureIgnoreCase) == true)
                        {
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }
    }
}
