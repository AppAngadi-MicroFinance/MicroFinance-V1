using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    class DenominationModel:BindableBase
    {
        long _amount;
        string _multiples;
        long _answer;
       

        public long Amount
        {
            get 
            {
                return _amount;
            }
            set
            {
                _amount = value;
                RaisedPropertyChanged("Amount");
            }
        }

        public string Multiples
        {
            get
            {
                return _multiples;
            }
            set
            {
                _multiples = value;
                long parsed = 0;
                if(long.TryParse(Multiples, out parsed))
                {
                    Answer = (Amount * parsed);
                }
                if (_multiples.ToString() == "")
                {
                    _multiples = "0";
                }
                
                RaisedPropertyChanged("Multiples");
            }
        }

        public long Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                RaisedPropertyChanged("Answer");
            }
        }

      


        public DenominationModel(int Key, string Value)
        {                                                       
            this.Amount = Key;
            this.Multiples = Value;
        }

        public DenominationModel()
        {
            
        }
    }
}
