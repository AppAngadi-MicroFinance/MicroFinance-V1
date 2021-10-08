using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class TimeTableViewModel
    {
        public string SHGName { get; set; }
        public string SHGId { get; set; }
        public TimeSpan CollectionTime { get; set; }
        public string CollectionDay { get; set; }
        public string EmpId { get; set; }
    }
}
