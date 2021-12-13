using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class SHGModal:BindableBase
    {
        private string _branchid;
        public string BranchID
        {
            get
            {
                return _branchid;
            }
            set
            {
                _branchid = value;
                RaisedPropertyChanged("BranchID");
            }
        }
        private string _shgid;
        public string SHGId
        {
            get
            {
                return _shgid;
            }
            set
            {
                _shgid = value;
                RaisedPropertyChanged("SHGId");
            }
        }
        private string _shgname;
        public string SHGName
        {
            get
            {
                return _shgname;
            }
            set
            {
                _shgname = value;
                RaisedPropertyChanged("SHGName");
            }
        }
        private DateTime _creationdate;
        public DateTime CreationDate
        {
            get
            {
                return _creationdate;
            }
            set
            {
                _creationdate = value;
                RaisedPropertyChanged("CreationDate");
            }
        }
        private string _taluk;
        public string Taluk
        {
            get
            {
                return _taluk;
            }
            set
            {
                _taluk = value;
                RaisedPropertyChanged("Taluk");
            }
        }
        private string _district;
        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
                RaisedPropertyChanged("District");
            }
        }

        private bool _isactive;
        public bool IsActive
        {
            get
            {
                return _isactive;
            }
            set
            {
                _isactive = value;
                RaisedPropertyChanged("IsActive");
            }
        }
     }
}
