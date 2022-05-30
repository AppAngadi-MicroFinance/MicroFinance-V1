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
        public string Column_4 { get; set; }
        public string Column_5 { get; set; }
        public string Column_6 { get; set; }
        public string Column_7 { get; set; }
        public string Column_8 { get; set; }
        public string Column_9 { get; set; }
        public string Column_10 { get; set; }
        public string Column_11 { get; set; }
        public string Column_12 { get; set; }
        public string Column_13 { get; set; }
        public string Column_14 { get; set; }
        public string Column_15 { get; set; }
        public string Column_16 { get; set; }
        public string Column_17 { get; set; }

        public List<DateAndData> DataList { get; set; }

        public ReportModel()
        {
            this.DataList = new List<DateAndData>();
        }
    }
}
