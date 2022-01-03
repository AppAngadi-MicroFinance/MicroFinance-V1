using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CollectionDetailView
    {
        public string BranchId { get; set; }
        public string CustId { get; set; }
        public string CustomerName { get; set; }
        public string LoanId { get; set; }
        public int Principal { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }
        public int SecurityDeposite { get; set; }
        public int ActualDue { get; set; }
        public int PaidDue { get; set; }
        public int Balance { get; set; }
        public DateTime ActualPaymentDate { get; set; }
        public DateTime CollectedOn { get; set; }
        public int Attendance { get; set; }
        public int Extras { get; set; }
        public string CollectedBy { get; set; }
        public string CenterID { get; set; }
        public string CenterName { get; set; }
    }
}
