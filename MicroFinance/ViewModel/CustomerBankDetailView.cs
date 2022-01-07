using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CustomerBankDetailView
    {
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string IFSCCode { get; set; }
        public int Amount { get; set; }
    }
}
