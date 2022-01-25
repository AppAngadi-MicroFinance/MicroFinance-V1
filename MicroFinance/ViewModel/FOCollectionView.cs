using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class FOCollectionView
    {
        public string  EmployeeName { get; set; }
        public string BranchName { get; set; }
        public int CollectionAmount { get; set; }
        public DateTime CollectedDate { get; set; }
    }
}
