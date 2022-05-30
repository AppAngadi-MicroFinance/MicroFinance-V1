using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class POSReport
    {
        public string Region { get; set; }
        public string Branch { get; set; }
        public string Center { get; set; }
        public string FieldManager { get; set; }
        public string CustName { get; set; }
        public string CustId { get; set; }
        public string AadhaarNumber { get; set; }
        public string AccountNo { get; set; }
        public string SamAccountNo { get; set; }
        public DateTime DisbursementDate { get; set; }
        public int DisbursementAmount { get; set; }
        public DateTime MaturityDate { get; set; }
        public int PrincipalRepaid { get; set; }
        public int InterestRepaid { get; set; }
        public int TotalRepaid { get; set; }
        public int ODDays { get; set; }
        public DateTime AccountCloseDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public int LedgerBalance { get; set; }
    }
}
