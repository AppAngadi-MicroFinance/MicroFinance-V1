using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class LoanApplicationModel
    {
        public MFOrigin OriginDetail { get; set; }

        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string RequestId { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime AccountCosedOn { get; set; }

        public string LoanId { get; set; }
        public string LoanType { get; set; }
        public string LoanPurpose { get; set; }
        public int LoanAmount { get; set; }
        public int LoanStatus { get; set; }

        public ApplicationStatusModel LoanApplicationStatus { get; set; }
        public LoanApplicationModel()
        {
            OriginDetail = new MFOrigin();
            LoanApplicationStatus = new ApplicationStatusModel();
        }
    }
}
