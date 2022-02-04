using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public struct LoanApplication
    {
        public string BranchID { get; set; }
        public string CustomerID { get; set; }
        public string RequestID { get; set; }
        public string EmpId { get; set; }
        public int LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public int LoanStatus { get; set; }
        public string CenterID { get; set; }
        public DateTime EnrollDate { get; set; }
        public string LoanPurpose { get; set; }
        public string LoanType { get; set; }
        public string Remark { get; set; }
    }
}
