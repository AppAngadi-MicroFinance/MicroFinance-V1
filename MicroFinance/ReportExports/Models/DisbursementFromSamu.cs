using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class DisbursementFromSamu
    {
        public DateTime ApproveDate { get; set; }
        public string SamLoanAcNumber { get; set; }
        public string GTLoanId { get; set; }
        public string RequestId { get; set; }
    }
}
