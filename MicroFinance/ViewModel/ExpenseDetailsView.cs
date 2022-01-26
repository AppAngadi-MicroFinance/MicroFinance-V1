using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class ExpenseDetailsView
    {
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
        public int Amount { get; set; }
        public string ExpenseType { get; set; }
        public string Reason { get; set; }
        public DateTime ExpenceDate { get; set; }
    }
}
