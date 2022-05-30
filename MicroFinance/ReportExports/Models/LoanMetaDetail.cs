using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanMetaDetail
    {
        public string LoanId { get; set; }
        public string CustomerId { get; set; }
        public int LoanAmount { get; set; }
        public DateTime approveddate { get; set; }
        public bool IsActive { get; set; }
    }
}
