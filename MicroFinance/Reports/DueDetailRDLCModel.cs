using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Reports
{
    public class DueDetailRDLCModel
    {
        public string SNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CenterName { get; set; }
        public string BranchName { get; set; }

        public int Attendance { get; set; }
        public string LoanId { get; set; }

        public int CurrentWeek { get; set; }
        public int LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }


        public int SecurityDepositeAmt { get; set; }
        public int CumulativeSDAmount { get; set; }

        public int PaidPrincipleAmount { get; set; }
        public int PrincipleAmount { get; set; }
        public int InterestAmount { get; set; }
        public int TotalAmount { get; set; }

        public int OutstandingAmount { get; set; }

        public DateTime TodayDate { get; set; }
        public string Day { get; set; }

        public DueDetailRDLCModel(CustomerDueDetail obj)
        {
            this.SNo = obj.SNo;
            this.CustomerName = obj.CustomerName;
            this.CenterName = obj.CenterName;
            this.BranchName = obj.BranchName;

            this.LoanDate = obj.CuurentLoanDetail.LoanDate;
            this.CurrentWeek = obj.CuurentLoanDetail.CurrentWeek;
            this.LoanAmount = obj.CuurentLoanDetail.LoanAmount;

            this.SecurityDepositeAmt = obj.CuurentLoanDetail.SecurityDepositeAmt;
            this.CumulativeSDAmount = obj.CuurentLoanDetail.CumulativeSDAmount;

            this.PrincipleAmount = obj.CuurentLoanDetail.PrincipleAmount;
            this.PaidPrincipleAmount = obj.CuurentLoanDetail.PaidPrincipleAmount;

            this.InterestAmount = obj.CuurentLoanDetail.InterestAmount;
            this.TotalAmount = obj.CuurentLoanDetail.TotalAmount;
            this.OutstandingAmount = obj.CuurentLoanDetail.OutstandingAmount;

            this.TodayDate = obj.TodayDate;
            this.Day = obj.Day;
        }
    }
}
