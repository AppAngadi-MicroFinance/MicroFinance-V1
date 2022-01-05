using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class SavingsAmountRequest
    {
        public string RequestID { get; set; }
        public string AccountNumber { get; set; }
        public string BranchID { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
        public int Code { get; set; }
        public string CustomerID { get; set; }
        public int RequestAmount { get; set; }
    }
}
