using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class ExpenceDetails
    {
        public string BranchID { get; set; }
        public string EmployeeID { get; set; }
        public string ExpenceType { get; set; }
        public int Amount { get; set; }
        public DateTime ExpenceDate { get; set; }
        public string Reason { get; set; }
    }
}
