using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class DenominationView
    {
        public string RegionName { get; set; }
        public string BId { get; set; }
        public DateTime CollectionDate { get; set; }
        public string EmpId { get; set; }
        public int TwoThousand { get; set; }
        public int FiveHundred { get; set; }
        public int TwoHundred { get; set; }
        public int Hundred { get; set; }
        public int Fifty { get; set; }
        public int Twenty { get; set; }
        public int Ten { get; set; }
        public int Five { get; set; }
        public int Two { get; set; }
        public int One { get; set; }
        public int TotalCollection { get; set; }
        public string SHGName { get; set; }
        public bool IsVerified { get; set; }
    }
}
