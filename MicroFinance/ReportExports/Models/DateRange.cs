using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.Models
{
    public class DateRange
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string FromDate_String { get { return FromDate.ToString("yyyy-MM-dd"); } }
        public string ToDate_String { get { return ToDate.ToString("yyyy-MM-dd"); } }

        public string RangeString { get { return FromDate.ToString("dd-MM") + " to " + ToDate.ToString("dd-MM"); } }
        public int Days { get { return ToDate >= FromDate ? (ToDate - FromDate).Days : -1; } }

        public DateRange(DateTime from, DateTime to)
        {
            this.FromDate = from;
            this.ToDate = to;
        }
        public DateRange()
        {

        }
    }
}
