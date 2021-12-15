using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.APIModal
{
    class VMCustomerGroup
    {
        public string CustId { get; set; }
        public string PeerGroupId { get; set; }
        public bool IsLeader { get; set; }
        public int CPid { get; set; }
        public string SHGID { get; set; }
    }
}
