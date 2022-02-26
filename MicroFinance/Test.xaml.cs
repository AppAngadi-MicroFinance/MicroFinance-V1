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
using MicroFinance.Repository;
using MicroFinance.ViewModel;

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
          
            //Custlist.ItemsSource = mylist;

        }

       
       

        private void sampleBtn_Click(object sender, RoutedEventArgs e)
        {
            string BranchId = "01202109001";
            DateTime Todaya = DateTime.Today;
            DateTime ToDate = Convert.ToDateTime("01-01-2021");
            DateModel Datedata = new DateModel { FromDate = ToDate, ToDate = Todaya };
            List<BranchReportEmployeeWise> result= BranchReportRepository.GetBranchLoanAmountEmployeewise(BranchId, Datedata);

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
