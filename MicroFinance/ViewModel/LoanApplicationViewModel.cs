using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class LoanApplicationViewModel
    {
        public string  CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string LoanStatus { get; set; }
        public string LoanType { get; set; }
        public int StatusCode { get; set; }
        public int LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime EnrollDate { get; set; }
        public string LoanPurpose { get; set; }
        public string RequestedBy { get; set; }
    }
}
