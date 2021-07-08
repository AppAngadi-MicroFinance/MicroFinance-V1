using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MicroFinance.Modal;
using MicroFinance.Converters;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        public List<LoanProcess> mylist = new List<LoanProcess>();
        public Test()
        {
            InitializeComponent();
            add();
            //Custlist.ItemsSource = mylist;

        }

        public void add()
        {
            
        }

        private void ApprovetoHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
    public class Cust:BindableBase
    {
        public string CustName { get; set; }
        public string EmployeeName { get; set; }
        public int MonthlyIncome { get; set; }
        public string SHGName { get; set; }
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
        
        public string LoanPeriod { get; set; }
        

        public string CustomerID { get; set; }
        
        public string FieldOfficerName { get; set; }
        

    }
    
}
