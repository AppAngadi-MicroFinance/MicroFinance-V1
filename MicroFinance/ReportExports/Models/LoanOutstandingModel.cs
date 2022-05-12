using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanOutstandingModel
    {
        public MFOrigin BranchDetail { get; set; }
        public string LoanId { get; set; }
        public string EmployeeID { get; set; }
        public int LoanAmount { get; set; }
        public int PrincipleAmount { get; set; }
        public List<CollectionEntryData> CollectionEntries { get; set; }
    }
}
