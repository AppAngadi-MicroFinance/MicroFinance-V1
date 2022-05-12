using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class ReportModel
    {
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }

        public List<DateAndData> DataList { get; set; }

        public ReportModel()
        {
            this.DataList = new List<DateAndData>();
        }
    }
}
