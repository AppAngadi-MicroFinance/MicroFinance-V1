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
        public List<string> months = new List<string> { "12 Months", "24 months" };
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
        private string _approvedby;
        public string ApprovedBy
        {
            get
            {
                return _approvedby;
            }
            set
            {
                _approvedby = value;
                RaisedPropertyChanged("ApprovedBy");
            }
        }
        private int _loanstatus;
        public int LoanStatus
        {
            get
            {
                return _loanstatus;
            }
            set
            {
                _loanstatus = value;
                RaisedPropertyChanged("LoanStatus");
            }
        }
        private string _branchId;
        public string BranchID
        {
            get
            {
                return _branchId;
            }
            set
            {
                _branchId = value;
                RaisedPropertyChanged("BranchID");
            }
        }
        private string _remark;
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                RaisedPropertyChanged("Remark");
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
                    sqlcomm.CommandText = "insert into LoanApplication(RequestID,CustId,EmployeeId,LoanType,LoanAmount,LoanPeriod,Purpose,EnrollDate,LoanStatus,Remark,BranchID) values  ('" + GenerateLoanRequestID()+"','"+_customerID+"','"+_employeeID+"','"+_loantype+"',"+_loanamount+","+_loanperiod+",'"+_loanPurpose+"','"+DateTime.Now.ToString("MM-dd-yyyy")+"','1','','"+_branchId+"')";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
        }
        public void RecommendLoan(string RequestId)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update LoanApplication set LoanStatus='8' where RequestID='" + RequestId + "'";
                    sqlcomm.ExecuteNonQuery();
                }
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
                    sqlcomm.CommandText = "select RegionCode from Region where RegionName='" + _regionname + "'";
                    Result = ((int)sqlcomm.ExecuteScalar()).ToString();
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
                    sqlcomm.CommandText = "select BranchCode,Bid from BranchDetails where BranchName='" + _branchname + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Result = (reader.GetInt32(0)).ToString();
                            _branchId = reader.GetString(1);
                        }
                        reader.Close();
                    }
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
        public string GenerateLoanRequestID() // IDPattern 02001202106R05 (02-Region/001-Branch/2021-CurrentYear/06-CurrentMonth/R-Request(Spe)/(No.of Loan given in currentYear+1))
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
                    sqlcomm.CommandText = "select Count(RequestID) from LoanApplication where RequestID like '%" + year + "%'";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "R"+ ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }
        public string GenerateLoanID() // IDPattern 02001202106R05 (02-Region/001-Branch/2021-CurrentYear/06-CurrentMonth/R-Request(Spe)/(No.of Loan given in currentYear+1))
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
                    sqlcomm.CommandText = "select Count(LoanID) from LoanDetails where LoanID like '%" + year + "%'";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = BranchID.Substring(0, 2);
            string branch = BranchID.Substring(8);
            Result = region + branch + year + month + "GL" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }

        public void GetRequestDetails(string ID)
        {
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchID from LoanApplication where RequestId='"+ID+"'";
                    _branchId = (string)sqlcomm.ExecuteScalar();
                    sqlconn.Close();
                }
                _interestrate = 12;
            }
        }
        //public void ApproveLoan(string ID)
        //{
        //    GetRequestDetails(ID);
        //    RequestApproval(ID);
        //    using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
        //    {
        //        sqlconn.Open();
        //        if(sqlconn.State==ConnectionState.Open)
        //        {
        //            SqlCommand sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlconn;
        //            sqlcomm.CommandText = "insert into LoanDetails(LoanID,CustomerID,LoanType,LoanPeriod,InterestRate,RequestedBY,ApprovedBy,ApproveDate,LoanAmount)values('" + GenerateLoanID() + "','"+_customerID+ "','"+_loantype+ "',"+_loanperiod+","+_interestrate+ ",'"+_employeeID+ "','"+_approvedby+"','"+DateTime.Now.ToString("MM-dd-yyyy")+ "'," + _loanamount + ")";
        //            sqlcomm.ExecuteNonQuery();
        //        }
        //    }

        //}
        public void RequestApproval(string ID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update LoanApplication set LoanAmount=" + LoanAmount + ",LoanStatus='9' where RequestId='" + ID + "'";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
        }

        public bool IsAlreadyRecommend(string ID)
        {
            bool result = false;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select LoanStatus from LoanApplication where RequestID='" + ID + "'";
                    sqlcomm.ExecuteNonQuery();
                    int status = (int)sqlcomm.ExecuteScalar();
                    if(status==8)
                    {
                        result = true;
                    }
                }
                sqlconn.Close();
            }
            return result;
        }
        public bool IsAlreadyApproved(string ID)
        {
            bool result = false;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select LoanStatus from LoanApplication where RequestId='" + ID + "'";
                    sqlcomm.ExecuteNonQuery();
                    int status = (int)sqlcomm.ExecuteScalar();
                    if (status==9)
                    {
                        result = true;
                    }
                }
                sqlconn.Close();
            }
            return result;
        }
        public override string ToString()
        {
            return _customerID + " |" + _employeeID + " | " + _loantype + "|" + LoanAmount + "|" + _loanperiod + "|" + LoanPurpose;
        }
    }
}
