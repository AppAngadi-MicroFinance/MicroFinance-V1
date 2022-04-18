using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class LoanCollectionEntryView
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int IsPresent { get; set; }
        public int PrincipleAmount { get; set; }
        public int InterestAmount { get; set; }
        public int SecurityDeposite { get; set; }
        public int ActualPayment { get; set; }
        public int PaidAmount { get; set; }
        public int OutstandingAmount { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime PaidDate { get; set; }

        public bool IsOnDateCollected
        {
            get { return ActualDate == PaidDate; }
        }

        public bool IsFullAmountPaid
        {
            get { return ActualPayment == PaidAmount; }
        }

        public string ActualDateString
        {
            get { return this.ActualDate.ToString("yyyy-MM-dd"); }
        }
        public string PaidDateString
        {
            get { return this.PaidDate.ToString("yyyy-MM-dd"); }
        }

    }
}
