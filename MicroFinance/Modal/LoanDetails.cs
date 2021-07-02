using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class LoanDetails : BindableBase
    {
        private string _loanID;
        public string LoanId
        {
            get
            {
                return _loanID;
            }
            set
            {
                _loanID = value;
                RaisedPropertyChanged("LoanID");
            }
        }
        private string _loantype;
        public string LoanType
        {
            get
            {
                return _loantype;
            }
            set
            {
                _loantype = value;
                RaisedPropertyChanged("LoanType");
            }
        }
        private int _loanamount;
        public int LoanAmount
        {
            get
            {
                return _loanamount;
            }
            set
            {
                _loanamount = value;
                RaisedPropertyChanged("LoanAmount");
            }
        }
        private int _loanperiod;
        public int LoanPeriod
        {
            get
            {
                return _loanperiod;
            }
            set
            {
                _loanperiod = value;
                RaisedPropertyChanged("LoanPeriod");
            }
        }
        private double _interestrate;
        public double InterestRate
        {
            get
            {
                return _interestrate;
            }
            set
            {
                _interestrate = value;
                RaisedPropertyChanged("InterestRate");
            }
        }
        private DateTime _enrolldate;
        public DateTime EnrollDate
        {
            get
            {
                return _enrolldate;
            }
            set
            {
                _enrolldate = value;
                RaisedPropertyChanged("EnrollDate");
            }
        }
    }
}
