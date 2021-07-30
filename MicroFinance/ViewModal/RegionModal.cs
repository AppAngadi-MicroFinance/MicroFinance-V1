using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModal
{
    public class RegionModal
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public RegionModal()
        {

        }
        public override string ToString()
        {
            return RegionName;
        }
    }
}
