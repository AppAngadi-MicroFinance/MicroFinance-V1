using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class SelfHeadGroupModal : BindableBase
    {
        string _shgName;
        public string SHGName
        {
            get
            {
                return _shgName;
            }
            set
            {
                _shgName = value;
                RaisedPropertyChanged("SHGName");
            }
        }
        bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisedPropertyChanged("IsSelected");
            }
        }

        public SelfHeadGroupModal(string groupName, bool state)
        {
            SHGName = groupName;
            IsSelected = state;
        }
        public SelfHeadGroupModal()
        {

        }
    }
}
