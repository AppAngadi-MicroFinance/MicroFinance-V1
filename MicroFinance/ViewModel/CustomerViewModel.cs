using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CustomerViewModel
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string GroupID { get; set; }
        public string CenterId { get; set; }
        public string CenterName { get; set; }
        public int ActiveLoans { get; set; }
    }
}
