using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CustomerEnrollMetaData
    {
        public string CustomerName { get; set; }
        public string AadharNumber { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollDate { get; set; }
    }
}
