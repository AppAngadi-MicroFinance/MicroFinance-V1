using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class LoanDetailsView
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string LoanID { get; set; }
        public string LoanType { get; set; }
        public int LoanAmount { get; set; }
        public int PaidAmount { get; set; }
        public int Balance { get; set; }
    }
}
