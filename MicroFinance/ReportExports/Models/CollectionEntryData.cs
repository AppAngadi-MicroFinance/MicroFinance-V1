using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class CollectionEntryData
    {
        public MFOrigin OriginDetail { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CollectedBy_EmpID { get; set; }
        public string CollectedBy_EmpName { get; set; }

        public string LoanId { get; set; }
        public int LoanAmount { get; set; }
        public int PrincipleAmount { get; set; }
        public DateTime CollectedOn { get; set; }

        public CollectionEntryData()
        {
            this.OriginDetail = new MFOrigin();
        }
    }
}
