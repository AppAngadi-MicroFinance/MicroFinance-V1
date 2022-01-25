using MicroFinance.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class SALoanStatusView
    {
        public List<StatusModal> StatusDetails { get; set; }
    }

    public class StatusModal
    {
        private int _code;
        public int Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                Status= (string)Enum.GetName(typeof(LoanStatusCode), value);
            }
        }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
