using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Reports
{
    public class CustomerDueDetail
    {
        public string SNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CenterName { get; set; }
        public string BranchName { get; set; }

        public DateTime TodayDate { get; set; }
        public string Day { get; set; }
        public string GroupName { get; set; }
        public LoanDetail CuurentLoanDetail { get; set; }

        public CustomerDueDetail(string loanId)
        {
            TodayDate = DateTime.Today;
            Day = TodayDate.DayOfWeek.ToString();
            SetOriginDetails(loanId);
            CuurentLoanDetail = new LoanDetail(loanId);
        }
        void SetOriginDetails(string loanId)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.db))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select CustId from CustomerDetails where CustId = (select CustomerID from LoanDetails where LoanID = '" + loanId + "')";
                this.CustomerId = (string)cmd.ExecuteScalar();

                cmd.CommandText = "select Name from CustomerDetails where CustId = '" + this.CustomerId + "'";
                this.CustomerName = (string)cmd.ExecuteScalar();

                cmd.CommandText = "select GroupName from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + this.CustomerId + "')";
                this.GroupName = (string)cmd.ExecuteScalar();

                cmd.CommandText = "select SHGName from SelfHelpGroup where SHGid = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + this.CustomerId + "'))";
                this.CenterName = (string)cmd.ExecuteScalar();

                cmd.CommandText = "select BranchName from BranchDetails where Bid = (select BranchId from SelfHelpGroup where SHGid = (select SHGid from PeerGroup where GroupId = (select PeerGroupId from CustomerGroup where CustId = '" + this.CustomerId + "')))";
                this.BranchName = (string)cmd.ExecuteScalar();
            }
        }
    }
}
