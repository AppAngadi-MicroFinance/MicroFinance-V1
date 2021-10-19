using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class LoanViewModel
    {
        public string CustomerID { get; set; }
        public string LoanType { get; set; }
        public int LoanAmount{ get; set; }
        public string LoanId { get; set; }
        public int LoanPeriod { get; set; }
        public int InterestRate { get; set; }
        public DateTime ApproveDate { get; set; }
        public string RequestedBY{ get; set; }
        public string ApprovedBy { get; set; }
        public bool IsActive { get; set; }

        public int PaidedAmount { get; set; }
        public int BalanceAmount
        { 
            get
            {
                return LoanAmount - PaidedAmount;
            }
        }
    }
}
