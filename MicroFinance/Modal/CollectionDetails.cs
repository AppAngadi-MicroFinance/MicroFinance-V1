using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class CollectionDetails : BindableBase
    {
        string _custGroupId;
        public string CustGroupId 
        {
            get
            {
                return _custGroupId;
            } 
            set
            {
                _custGroupId = value;
                RaisedPropertyChanged("CustGroupId");
            }
        }
        int _attendance;
        public int Attendance
        {
            get
            {
                return _attendance;
            }
            set
            {
                _attendance = value;
                RaisedPropertyChanged("Attendance");
            }
        }
        string _custID;
        public string CustID
        {
            get
            {
                return _custID;
            }
            set
            {
                _custID = value;
                RaisedPropertyChanged("CustID");
            }
        }

        string _customerName;
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                RaisedPropertyChanged("CustomerName");
            }
        }

    
        string _loanId;
        public string LoanId
        {
            get
            {
                return _loanId;
            }
            set
            {
                _loanId = value;
                GetPaidDetails(_loanId);
                GetNextDueDetails(_loanId);
                GetCustomerName(_loanId);
                GetExtraBalance(_loanId);
                GetGroupId(CustID);
            }
        }

        DateTime _approvedDate;
        public DateTime ApprovedDate 
        {
            get
            {
                return _approvedDate;
            }
            set
            {
                _approvedDate = value;
                RaisedPropertyChanged("ApprovedDate");
            }
        }

        string _loanType;
        public string LoanType
        {
            get
            {
                return _loanType;
            }
            set
            {
                _loanType = value;
                RaisedPropertyChanged("LoanType");
            }
        }

        int _loanAmount;
        public int LoanAmount
        {
            get
            {
                return _loanAmount ;
            }
            set
            {
                _loanAmount = value;
                RaisedPropertyChanged("LoanAmount");
            }
        }
        int _paymentCount = 1;
        public int PaymentCount
        {
            get
            {
                return _paymentCount;
            }
            set
            {
                _paymentCount = value;
                Principal = _defaultPrincipal * _paymentCount;
                Interest = +_defaultInterest * _paymentCount;
                RaisedPropertyChanged("PaymentCount");
            }
        }


        int _noOfPaymentPaid;
        public int NoOfPaymentPaid
        {
            get
            {
                return _noOfPaymentPaid;
            }
            set
            {
                _noOfPaymentPaid = value;
                RaisedPropertyChanged("NoOfPaymentPaid");
            }
        }


        int _paidPrincipal;
        public int PaidPrincipal
        {
            get
            {
                return _paidPrincipal;
            }
            set
            {
                _paidPrincipal = value;
                RaisedPropertyChanged("PaidPrincipal");
            }
        }

        int _remaingLoanAmount;
        public int RemaingLoanAmount
        {
            get
            {
                return _remaingLoanAmount;
            }
            set
            {
                _remaingLoanAmount = value;
                RaisedPropertyChanged("RemaingLoanAmount");
            }
        }


        int _defaultPrincipal = 0;
        int _defaultInterest = 0;
        int _principal;
        public int Principal
        {
            get
            {
                return _principal;
            }
            set
           {
                _principal = value;
                if (_defaultPrincipal == 0)
                    _defaultPrincipal = _principal;

                Total = Principal + Interest + Security;
                RaisedPropertyChanged("Principal");
            }
        }


        int _interest;
        public int Interest
        {
            get
            {
                return _interest;
            }
            set
            {
                _interest = value;
                if (_defaultInterest == 0)
                    _defaultInterest = _interest;

                Total = Principal + Interest + Security;
                RaisedPropertyChanged("Interest");
            }
        }
        private int _security = 30;
        public int Security
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
                RaisedPropertyChanged("Security");
            }
        }

        int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                RaisedPropertyChanged("Total");
            }
        }
        int _extra;
        public int Extras
        {
            get
            {
                return _extra;
            }
            set
            {
                _extra = value;
                RaisedPropertyChanged("Extras");
            }
        }

        public CollectionDetails()
        {

        }
        public CollectionDetails(string loanId)
        {
            LoanId = loanId;
            
        }
        void GetPaidDetails(string loanId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Count(Principal), SUM(Principal), MIN(Balance) from LoanCollectionEntry where LoanId = '" + loanId + "' and Attendance > 0";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NoOfPaymentPaid = reader.GetInt32(0);
                    if (NoOfPaymentPaid == 0)
                    {
                        PaidPrincipal = 0;
                        RemaingLoanAmount = LoanAmount;
                    }
                    else
                    {
                        PaidPrincipal = reader.GetInt32(1);
                        RemaingLoanAmount = reader.GetInt32(2);
                    }

                }
                sql.Close();
            }
        }
        void GetNextDueDetails(string loanID)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select CustId, Principal, Interest from LoanCollectionMaster where WeekNo = ((select count(CustId) from LoanCollectionEntry where LoanId = '" + loanID + "' and Attendance > 0) + 1) and LoanId = '" + loanID + "'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustID = reader.GetString(0);
                    Principal = reader.GetInt32(1);
                    Interest = reader.GetInt32(2);
                }
                sql.Close();
            }
        }
        void GetCustomerName(string loanID)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select CustomerDetails.Name from CustomerDetails join LoanDetails on LoanDetails.CustomerID = CustomerDetails.CustId where LoanDetails.LoanID ='"+ loanID + "'";
                CustomerName = (string)command.ExecuteScalar();
                sql.Close();
            }
        }

        void GetExtraBalance(string loanID)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Sum(Extras) from LoanCollectionEntry where LoanId = '"+loanID+"'";
                var res = command.ExecuteScalar();
                if(DBNull.Value.Equals(res))
                {
                    Extras = 0; 
                }
                else
                {
                    Extras = (int)res;
                }
                sql.Close();
            }
        }

        void GetGroupId(string custId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select PeerGroupId from CustomerGroup where CustId = '"+ custId + "'";
                _custGroupId = (string)command.ExecuteScalar();
                sql.Close();
            }
        }
    }
}
