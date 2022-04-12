using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Reports
{
    public class GroupSummaryRDLCModel
    {
        public string GroupName { get; set; }
        public int CollectionAmount { get; set; }
        public GroupSummaryRDLCModel()
        {

        }
        public GroupSummaryRDLCModel(string groupName, int collectionAmount)
        {
            this.GroupName = groupName;
            this.CollectionAmount = collectionAmount;
        }
    }
}
