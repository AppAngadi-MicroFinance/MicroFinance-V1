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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanAfterHimark.xaml
    /// </summary>
    public partial class LoanAfterHimark : Page
    {
        LoanProcess loanprocess = new LoanProcess();
        public List<LoanProcess> RecommendList = new List<LoanProcess>();
        public LoanAfterHimark()
        {
            InitializeComponent();
            loanprocess.GetRecommendList();
            RecommendList.Clear();
            RecommendList = loanprocess.RecommendList;
            Custlist.ItemsSource = RecommendList;
            setCount();
            setAmount();
        }
        void setCount()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (LoanProcess c in RecommendList)
            {

                if (c.LoanType == "General Loan")
                {
                    count1++;
                }
                else if (c.LoanType == "Special")
                {
                    count2++;
                }
            }
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
        }
        void setAmount()
        {
            int Amount1 = 0;
            int Amount2 = 0;
            foreach (LoanProcess c in RecommendList)
            {
                Amount1 += c.LoanAmount;
            }
            LoanAmountValue.Text = Amount1.ToString();
            BacklogValue.Text = Amount2.ToString();
        }

        private void Custlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ApproveLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loan = new LoanProcess();
            Button btn = sender as Button;
            string ID = btn.Uid.ToString();
            Custlist.Items.Refresh();
            if(loan.IsAlreadyApproved(ID))
            {
                MainWindow.StatusMessageofPage(1, "Loan Already Approved!...");
            }
            else
            {
                loan = GetRecommendDetails(ID);
                string ApprovedBy = MainWindow.LoginDesignation.EmpId;
                loan.ApprovedBy = ApprovedBy;
                loan.ApproveLoan(ID);
                MainWindow.StatusMessageofPage(1, "Loan Approved Successfully...");
            }
            
            
        }

        public LoanProcess GetRecommendDetails(string ID)
        {
            LoanProcess selectedLoan = new LoanProcess();
            int i = 0;
            foreach(LoanProcess process in RecommendList)
            {
                if(process.LoanRequestID==ID)
                {
                    selectedLoan = RecommendList.ElementAt(i);
                }
                else
                {
                    i++;
                }
            }
            return selectedLoan;
        }

        private void RejectResonOKBtn_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loan = new LoanProcess();
            Button button = sender as Button;
            string ID = button.Uid.ToString();
            loan = GetRecommendDetails(ID);
            string ApprovedBy = MainWindow.LoginDesignation.EmpId;
            loan.ApprovedBy = ApprovedBy;
            loan.RejectLoan(ID);
            Custlist.Items.Refresh();
        }
    }
}
