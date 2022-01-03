using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CustomerMetaData
    {
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string CenterID { get; set; }
        public string AadharNumber { get; set; }
        public int ActiveLoans { get; set; }
    }
}
