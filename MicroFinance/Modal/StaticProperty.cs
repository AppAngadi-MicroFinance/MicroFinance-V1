using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class StaticProperty : BindableBase
    {
        public StaticProperty()
        {

        }

        private int _messageType = 1;

        public int MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                _messageType = value;
                RaisedPropertyChanged("MessageType");
            }
        }

        private string _statusMessage = "Ready";

        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                RaisedPropertyChanged("StatusMessage");
            }
        }

    }
}
