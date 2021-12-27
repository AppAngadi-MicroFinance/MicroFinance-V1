using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;

namespace MicroFinance.ViewModel
{
    public class EnrollDetailsView:BindableBase
    {
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string CenterName { get; set; }
        public string CenterID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string AadharNumber { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        private int _loanCode;
        public int LoanStatusCode
        {
            get
            {
                return _loanCode;
            }
            set
            {
                _loanCode = value;
                LoanStatus = (string)Enum.GetName(typeof(LoanStatusCode), value);
            }
        }
        public string LoanStatus { get; set; }
        public string RequestID { get; set; }
    }
}
