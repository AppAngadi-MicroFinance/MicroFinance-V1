using MicroFinance.Repository;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanPurposeView.xaml
    /// </summary>
    public partial class LoanPurposeView : Page
    {
        public LoanPurposeView()
        {
            InitializeComponent();
        }
        public int StateCode;
        public LoanPurposeView(int Code)
        {
            InitializeComponent();
            StateCode = Code;
            if(Code==0)
            {
                TitleName.Text = "Add Expense";
                SubTitleName.Text = "Category";
            }
            else if(Code==1)
            {
                TitleName.Text = "Add Bank Name";
                SubTitleName.Text = "Bank Name";
            }
            else if(Code==2)
            {
                TitleName.Text = "Add Loan Purpose";
                SubTitleName.Text = "Purpose Name";
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string Value = ValueBox.Text;
            if(!string.IsNullOrEmpty(Value))
            {
                AddDetails(StateCode, Value);
                ValueBox.Text = "";
            }
            else
            {
                MessageBox.Show("Enter the Value", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        void AddDetails(int Code,string Value)
        {
            if(Code==0)//Add Expence Type
            {
                GeneralRepository.AddExpenseType(Value);
                MessageBox.Show("Expense Type Added SuccessFully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(Code==1)//Add Bank Bank
            {
                GeneralRepository.AddBankName(Value);
                MessageBox.Show("Bank Name Added SuccessFully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(Code==2)//Add Loan Purpose
            {
                GeneralRepository.AddLoanPurpose(Value);
                MessageBox.Show("Loan Purpose Type Added SuccessFully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
