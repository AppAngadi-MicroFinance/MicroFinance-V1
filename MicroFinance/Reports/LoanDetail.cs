using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Reports
{
    public class LoanDetail
    {
        public int Attendance { get; set; }
        public string LoanId { get; set; }

        public int CurrentWeek { get; set; } // LoanId
        public int LoanAmount { get; set; } // LoanId
        public DateTime LoanDate { get; set; } // LoanId

        public int SecurityDepositeAmt { get; set; } // LoanId
        public int CumulativeSDAmount { get; set; } // LoanId


        public int PaidPrincipleAmount { get; set; } // LoanId


        public int PrincipleAmount { get; set; } // LoanId
        public int InterestAmount { get; set; } // LoanId
        public int TotalAmount { get; set; } // TotalAmount = PrincipleAmount + InterestAmount

        public int OutstandingAmount { get; set; } // LoanId


        public LoanDetail(string loanId)
        {
            this.LoanId = loanId;
            this.SecurityDepositeAmt = 60;
            SetLoanDetails(this.LoanId);
        }

        void SetLoanDetails(string loanId)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.db))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                // LoanAmount, LoadDate.
                cmd.CommandText = "select LoanAmount, ApproveDate from LoanDetails where LoanID = '" + loanId + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.LoanAmount = dr.GetInt32(0);
                    this.LoanDate = dr.GetDateTime(1);
                    break;
                }
                dr.Close();

                // Cuurent Week number.
                cmd.CommandText = "select COUNT(*) from LoanCollectionEntry where LoanId = '" + loanId + "'";
                this.CurrentWeek = (int)cmd.ExecuteScalar();

                // Principle , Interest amount.
                cmd.CommandText = "select Principal, Interest from LoanCollectionMaster where LoanId = '" + loanId + "' and WeekNo = " + (this.CurrentWeek + 1) + "";
                SqlDataReader dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    this.PrincipleAmount = dr2.GetInt32(0);
                    this.InterestAmount = dr2.GetInt32(1);
                    this.TotalAmount = this.PrincipleAmount + this.InterestAmount + this.SecurityDepositeAmt;
                    break;
                }
                dr2.Close();

                // Paid Principle amount.
                cmd.CommandText = "select SUM(Principal) from LoanCollectionEntry where LoanId = '" + loanId + "'";
                SqlDataReader dr3 = cmd.ExecuteReader();
                while (dr3.Read())
                {
                    if (dr3.IsDBNull(0))
                        this.PaidPrincipleAmount = 0;
                    else
                        this.PaidPrincipleAmount = dr3.GetInt32(0);
                    break;
                }
                dr3.Close();

                // OutstandingAmount / Balance amount.
                if (this.CurrentWeek == 0)
                    this.OutstandingAmount = this.LoanAmount;
                else
                {
                    cmd.CommandText = "select top 1 Balance from LoanCollectionEntry where LoanId = '" + loanId + "' order by Balance";
                    this.OutstandingAmount = (int)cmd.ExecuteScalar();
                }

                // Security Deposite amount. Default Value = 60;
                //cmd.CommandText = "select SecurityDeposite from LoanCollectionEntry where LoanId = '" + loanId + "'";
                //var res = (int)cmd.ExecuteScalar();

                // Cumulative sum of Security deposite.
                cmd.CommandText = "select sum(SecurityDeposite) from LoanCollectionEntry where LoanId = '" + loanId + "'";
                SqlDataReader dr5 = cmd.ExecuteReader();
                while (dr5.Read())
                {
                    if (dr5.IsDBNull(0))
                        this.CumulativeSDAmount = 0;
                    else
                        this.CumulativeSDAmount = dr5.GetInt32(0);
                    break;
                }
            }
        }
    }
}
