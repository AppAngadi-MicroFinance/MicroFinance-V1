using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CollectionReportView
    {
        public string BranchId { get; set; }
        public string CustomerID { get; set; }
        public string LoanID { get; set; }
        public string CollectedBy { get; set; }



        public string BranchName { get; set; }
        public string CenterName { get; set; }
        public string FoName { get; set; }
        public string CustomerName { get; set; }
        public int Amount { get; set; }
    }
}
