using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class HimarkRequestView
    {
        public string CustomerName { get; set; }
        public int   LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public string EmpName { get; set; }
        public string RequestID { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        private string _empid;
        public string EmpId
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;
                
            }
        }
        public string BranchName { get; set; }
        public string Collectionday { get; set; }
        public string CenterName { get; set; }

    }
}
