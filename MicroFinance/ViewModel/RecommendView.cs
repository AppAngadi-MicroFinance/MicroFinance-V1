using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
namespace MicroFinance.ViewModel
{
    class RecommendView:BindableBase
    {
        public string CustomerName { get; set; }
        public int LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public string EmpName { get; set; }
        public string RequestID { get; set; }
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        private string _empid;
        public string EmpId
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;

            }
        }
        public string BranchName { get; set; }
        public string CenterName { get; set; }
        public string  CollectionDay { get; set; }
        private bool _isrecommend;
        public bool IsRecommend
        {
            get
            {
                return _isrecommend;
            }
            set
            {
                _isrecommend = value;
                RaisedPropertyChanged("IsRecommend");
            }
        }
    }
}
