using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class ReportModel
    {
        public string Column_1 { get; set; }
        public string Column_2 { get; set; }
        public string Column_3 { get; set; }

        public List<DateAndData> DataList { get; set; }

        public ReportModel()
        {
            this.DataList = new List<DateAndData>();
        }
    }
}
