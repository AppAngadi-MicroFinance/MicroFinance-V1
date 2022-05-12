using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanRepoModel
    {
        public string LoanId { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedBy_EmpId { get; set; }
        public string ApprovedBy_EmpName { get; set; }
        public bool IsActive { get; set; }
    }
}
