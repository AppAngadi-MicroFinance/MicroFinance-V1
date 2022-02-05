using MicroFinance.Modal;
using MicroFinance.ViewModel;
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
    /// Interaction logic for LoanRequestNew.xaml
    /// </summary>
    public partial class LoanRequestNew : Page
    {
        public List<string> LoanTypeList = new List<string> { "General Loan" };
        public List<string> LoanPurposeList = new List<string> { "AGRI" };
        public List<int> LoanAmountList = new List<int> { 30000 };
        public List<int> LoanPeriodList = new List<int> { 50 };
        LoanApplicationMeta MetaDetails = new LoanApplicationMeta();
        public LoanRequestNew()
        {
            InitializeComponent();
        }
        public LoanRequestNew(LoanApplicationMeta ApplicatioMetaDetails)
        {
            InitializeComponent();
            MetaDetails = ApplicatioMetaDetails;
            LoadData();
            
        }


        void LoadData()
        {
            CustomerNameBox.Text = MetaDetails.CustomerName;
            EnrollDatePicker.Text = DateTime.Today.ToString("dd-MM-yyyy");
            LoanPeriodCombo.ItemsSource = LoanPurposeList;
            LoanTypeCombo.ItemsSource = LoanTypeList;
            LoanAmountCombo.ItemsSource = LoanAmountList;
            LoanPeriodCombo.ItemsSource = LoanPeriodList;
            LoanPurposeCombo.ItemsSource = LoanPurposeList;
        }

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsValidEntry())
            {
                try
                {
                    GifPanel.Visibility = Visibility.Visible;
                    int loantypeind = LoanTypeCombo.SelectedIndex;
                    int loanpurposeind = LoanPurposeCombo.SelectedIndex;
                    int loanamtind = LoanAmountCombo.SelectedIndex;
                    int loanperind = LoanPeriodCombo.SelectedIndex;
                    await System.Threading.Tasks.Task.Run(() => AddAppliction(loanamtind,loanpurposeind,loantypeind,loanperind));
                    GifPanel.Visibility = Visibility.Collapsed;

                    this.NavigationService.Navigate(new CollectionEntryBulk(MetaDetails.EmpID));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Check the All Fields in Application", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        void AddAppliction(int LAmountInd,int LPurposeInd,int LTypeInd,int LPeriodInd)
        {
            LoanApplication application = new LoanApplication();
            application.CustomerID = MetaDetails.CustomerID;
            application.BranchID = MetaDetails.BranchID;
            application.RequestID = LoanRepository.GenerateLoanRequestID(MetaDetails.BranchID);
            application.LoanAmount = LoanAmountList[LAmountInd];
            application.LoanType = LoanTypeList[LTypeInd];
            application.LoanPeriod = LoanPeriodList[LPeriodInd];
            application.LoanPurpose = LoanPurposeList[LPurposeInd];
            application.EnrollDate = DateTime.Today;
            application.LoanStatus = 1;
            application.Remark = "";
            application.EmpId = MetaDetails.EmpID;
            application.CenterID = MetaDetails.CenterID;
            LoanRepository.AddLoanApplication(application);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }



        bool IsValidEntry()
        {

            if(LoanPeriodCombo.SelectedIndex!=-1&&LoanTypeCombo.SelectedIndex!=-1&&LoanAmountCombo.SelectedIndex!=-1&&LoanPurposeCombo.SelectedIndex!=-1)
            {
                return true;
            }
            return false;
        }
    }
}
