using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanSummaryModel
    {
        public MFOrigin OriginDetail { get; set; }
        public string LoanId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int LoanAmount { get; set; }
        public int PrincipleAmount { get; set; }
        public List<LoanCollectionEntryModel> CollectionEntryData { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int CollectedAmount { get; set; }

        public int WeeksPaid { get; set; }
        public DateTime AccountClosedOn { get; set; }
        public string AccountClosedBY { get; set; }
        public LoanSummaryModel()
        {
            OriginDetail = new MFOrigin();
            CollectionEntryData = new List<LoanCollectionEntryModel>();
        }
    }
}
