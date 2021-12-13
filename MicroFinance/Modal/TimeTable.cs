using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class TimeTable : BindableBase
    {
        private string _shgid;
        public string SHGID
        {
            get
            {
                return _shgid;
            }
            set
            {
                _shgid = value;
                RaisedPropertyChanged("SHGID");
            }
        }
        private string _empname;
        public string EmpName
        {
            get
            {
                return _empname;
            }
            set
            {
                _empname = value;
                RaisedPropertyChanged("EmpName");
            }
        }
        private string _collectiontime;
        public string CollectionTime
        {
            get
            {
                return _collectiontime;
            }
            set
            {
                _collectiontime = value;
                RaisedPropertyChanged("CollectionTime");
            }
        }
        private string _collectionday;
        public string CollectionDay
        {
            get
            {
                return _collectionday;
            }
            set
            {
                _collectionday = value;
                RaisedPropertyChanged("CollectionDay");
            }
        }
        private string _empid;
        public string EmployeeID
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;
                RaisedPropertyChanged("EmployeeID");
            }
        }

        private string _centername;
        public string CenterName
        {
            get
            {
                return _centername;
            }
            set
            {
                _centername = value;
                RaisedPropertyChanged("CenterName");
            }
        }

    }
}
