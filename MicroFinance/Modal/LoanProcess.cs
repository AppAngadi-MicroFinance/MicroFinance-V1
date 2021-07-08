using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{
    public class LoanProcess:Customer
    {
        private string[] _guaranterDetails = new string[2];
       // string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public string CustomerID { get; set; }
        private LoanDetails _loandetails;
        public LoanDetails loanDetails
        {
            get
            {
                return loanDetails;
            }
            set
            {
                _loandetails = value;
                RaisedPropertyChanged("Loan Details");
            }
        }
        private string _guarantername;
        public string GuaranterName
        {
            get
            {
                return _guarantername; 
            }
        }
        private string _guaranterrelatioship;
        private string GuaranterRelatioShip
        {
            get
            {
                return _guaranterrelatioship;
            }
            
        }

        public string SHGName
        {
            get
            {
                string Result="";
                using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if(sqlconn.State==ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select SelfHelpGroup from CustomerGroup where CustId='"+_customerId+"'";
                        Result = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
                return Result;
            }
        }

        public string Address
        {
            get
            {
                return DoorNumber + ", " + StreetName + ", " + Pincode;
            }
        }

        public void GetGuaranterDetails()
        {
            
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                
                _guaranterDetails = new string[2];
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    // Pending Check if Guarnter is True
                    sqlcomm.CommandText = "select Name,RelationShip from GuarenteeDetails where CustId='" + _customerId + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        _guarantername = reader.GetString(0);
                        _guaranterrelatioship = reader.GetString(1);
                    }
                }
                sqlconn.Close();
            }
          
        }
    }
}
