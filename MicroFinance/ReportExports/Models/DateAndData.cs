using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class DateAndData
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Value { get; set; }

        public DateAndData(DateTime dateOn, int value)
        {
            this.FromDate = dateOn;
            this.Value = value;
        }
        public DateAndData()
        {

        }
    }
}
