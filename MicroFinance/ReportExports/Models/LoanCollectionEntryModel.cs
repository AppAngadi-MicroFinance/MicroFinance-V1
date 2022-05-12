using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanCollectionEntryModel
    {
        public int PrincipleAmount { get; set; }
        public DateTime CollectedOn { get; set; }
        public string CollectedBy_EmpID { get; set; }
        public string CollectedBy_EmpName { get; set; }

    }
}
