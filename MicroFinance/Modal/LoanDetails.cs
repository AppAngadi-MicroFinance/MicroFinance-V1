using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{
   
    public class LoanDetails : BindableBase
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        private string _customerID;
        public string CustomerID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
                RaisedPropertyChanged("CustomerID");
            }
        }
        private string _employeeID;
        public string EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
                RaisedPropertyChanged("EmployeeID");
            }
        }
        private string _loanrequestID;
        public string LoanRequestID
        {
            get
            {
                return _loanrequestID;
            }
            set
            {
                _loanrequestID = value;
                RaisedPropertyChanged("LoanRequestID");
            }
        }
        private string _loantype;
        public string LoanType
        {
            get
            {
                return _loantype;
            }
            set
            {
                _loantype = value;
                RaisedPropertyChanged("LoanType");
            }
        }
        private int _loanamount;
        public int LoanAmount
        {
            get
            {
                return _loanamount;
            }
            set
            {
                _loanamount = value;
                RaisedPropertyChanged("LoanAmount");
            }
        }
        private int _loanperiod;
        public int LoanPeriod
        {
            get
            {
                return _loanperiod;
            }
            set
            {
                _loanperiod = value;
                RaisedPropertyChanged("LoanPeriod");
            }
        }
        private double _interestrate;
        public double InterestRate
        {
            get
            {
                return _interestrate;
            }
            set
            {
                _interestrate = value;
                RaisedPropertyChanged("InterestRate");
            }
        }
        private DateTime _enrolldate;
        public DateTime EnrollDate
        {
            get
            {
                return _enrolldate;
            }
            set
            {
                _enrolldate = value;
                RaisedPropertyChanged("EnrollDate");
            }
        }
        private string _loanPurpose;
        public string LoanPurpose
        {
            get
            {
                return _loanPurpose;
            }
            set
            {
                _loanPurpose = value;
                RaisedPropertyChanged("LoanPurpose");
            }
        }
        private string _branchname;
        private string _regionname;
        public void SendRequest(string Region,String Branch)
        {
            this._regionname = Region;
            this._branchname = Branch;
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into LoanRequest(RequestID,CustomerID,EmpID,LoanType,LoanAmount,LoanPeriod,LoanPurpose,EnrollDate,StatusCode,Status,Remark) values ('"+GenerateLoanRequestID()+"','"+_customerID+"','"+_employeeID+"','"+_loantype+"',"+_loanamount+","+_loanperiod+",'"+_loanPurpose+"','"+DateTime.Now.ToString("MM-dd-yyyy")+"','1','Requested','')";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
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
                    sqlcomm.CommandText = "select SNo from Region where RegionName='" + _regionname + "'";
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
                    sqlcomm.CommandText = "select SNo from BranchDetails where BranchName='" + _branchname + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
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
        public string GenerateLoanRequestID() // IDPattern 02001202106SL05 (02-Region/001-Branch/2021-CurrentYear/06-CurrentMonth/SL-Loantype(Special)/(No.of Loan given in currentYear+1))
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
                    sqlcomm.CommandText = "select Count(RequestID) from LoanRequest where RequestID like '%" + year + "%'";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "R"+ ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }



        public override string ToString()
        {
            return _customerID + " |" + _employeeID + " | " + _loantype + "|" + LoanAmount + "|" + _loanperiod + "|" + LoanPurpose;
        }
    }
}
