using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class SavingsAccountRequestView
    {
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string CenterName { get; set; }
        public string RequestID { get; set; }
        public int Code { get; set; }
        public string BranchName { get; set; }
        public int Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public int Balance { get; set; }
    }
}
