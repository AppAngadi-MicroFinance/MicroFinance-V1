using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class CollectionEntryModel
    {
        public MFOrigin OriginDetail { get; set; }


        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string LoanId { get; set; }

        public int PrincipleAmount { get; set; }
        public int InterestAmount { get; set; }
        public int SecurityDeposite { get; set; }

        public int ActualDue { get; set; }
        public int PaidDue { get; set; }


        public bool IsPresent { get; set; }
        public DateTime ActualPaymentDate { get; set; }
        public DateTime CollectedDate { get; set; }
        public string CollectedBy_EmpID { get; set; }
        public string CollectedBy_EmpName { get; set; }
        public  int TotalAmount { get; set; }

        public CollectionEntryModel()
        {
            OriginDetail = new MFOrigin();
        }
    }
}
