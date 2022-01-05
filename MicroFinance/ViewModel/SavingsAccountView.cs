using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class SavingsAccountView
    {
        public string CustomerName { get; set; }
        public string CustId { get; set; }
        public string SavingAcId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsActive { get; set; }
        public int Debit { get; set; }
        public int Credit { get; set; }
        public int Balance { get; set; }
    }
}
