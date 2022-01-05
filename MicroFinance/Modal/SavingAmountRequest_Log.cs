using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class SavingAmountRequest_Log
    {
        
        public string RequestID { get; set; }
        public int Code { get; set; }
        public string EmployeeID { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
