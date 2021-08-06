using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class CollectionShceduleSheet
    {
        public string CustGroupId { get; set; }

        public string Attendance { get; set; }
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
                GetCustomerName(_custID);
            }
        }
        public string CustomerName { get; set; }

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
            }
        }
        public DateTime SanctionDate { get; set; }
        public string LoanType { get; set; }
        public int LoanAmount { get; set; }

        public int NoOfPayments { get; set; }
        public int PaidPrincipal { get; set; }
        public int RemaingLoanAmount { get; set; }

        public int Principal { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }


        public CollectionShceduleSheet()
        {

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
                    NoOfPayments = reader.GetInt32(0);
                    if (NoOfPayments == 0)
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
                command.CommandText = "select Principal, Interest,Total from LoanCollectionMaster where WeekNo = ((select count(CustId) from LoanCollectionEntry where LoanId = '" + loanID + "' and Attendance > 0) + 1) and LoanId = '" + loanID + "'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Principal = reader.GetInt32(0);
                    Interest = reader.GetInt32(1);
                    Total = reader.GetInt32(2);
                }
                sql.Close();
            }
        }
        void GetCustomerName(string custId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Name from CustomerDetails where CustId = '" + custId + "'";
                CustomerName = (string)command.ExecuteScalar();
                sql.Close();
            }
        }
    }
}
