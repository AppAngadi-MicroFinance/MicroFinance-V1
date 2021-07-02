using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class LoanProcess:BindableBase
    {
        private Customer _customer;
        public Customer customerDetails
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                RaisedPropertyChanged("Customer");
            }
        }
        private LoanDetails _loandetails;
        public LoanDetails loanDetails
        {
            get
            {
                return loanDetails;
            }
            set
            {
                _loandetails = value;
                RaisedPropertyChanged("Loan Details");
            }
        }

    }
}
