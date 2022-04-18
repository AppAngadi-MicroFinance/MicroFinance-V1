using MicroFinance.Modal;
using MicroFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using Humanizer;
namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanCollectionView.xaml
    /// </summary>
    public partial class LoanCollectionView : Page
    {
        LoginDetails LoginDesignation;
        List<LoanCollectionEntryView> CollectionEntryList = new List<LoanCollectionEntryView>();
        BranchViewModel BranchDetail = new BranchViewModel();
        EmployeeViewModel EmployeeDetail = new EmployeeViewModel();

        ObservableCollection<BranchViewModel> ContextBranchList = new ObservableCollection<BranchViewModel>();
        ObservableCollection<EmployeeViewModel> ContextEmployeeList = new ObservableCollection<EmployeeViewModel>();

        
        DateTime SelectedFromDate;
        DateTime SelectedToDate;
        public LoanCollectionView(LoginDetails loginDesignation)
        {
            // FieldOfficer login.
            InitializeComponent();
            this.LoginDesignation = loginDesignation;
            this.BranchDetail.BranchId = this.LoginDesignation.BranchId;
            this.BranchDetail.BranchName = this.LoginDesignation.BranchName;

            this.EmployeeDetail.EmployeeId = this.LoginDesignation.EmpId;
            this.EmployeeDetail.EmployeeName = EmployeeRepository.GetEmployeeName(this.EmployeeDetail.EmployeeId);

            this.SelectedFromDate = new DateTime(2022, 04, 09);
            this.SelectedToDate = new DateTime(2022, 04, 10);

            FieldOfficerLogin();
        }
        public LoanCollectionView()
        {
            //Admin login
            InitializeComponent();

            xBranchCmb.ItemsSource = EmployeeRepository.GetBranches();

        }

        void FieldOfficerLogin()
        {
            xBranchCmb.Visibility = Visibility.Collapsed;
            xBranchNamePanel.Visibility = Visibility.Visible;
            xBranchNameTB.Text = this.BranchDetail.BranchName;
            //
            xEmployeeCmb.Visibility = Visibility.Collapsed;
            xEmployeeNamePanel.Visibility = Visibility.Visible;
            xEmployeeNameTB.Text = this.EmployeeDetail.EmployeeName;
            //
            LoadData(BranchDetail, EmployeeDetail, SelectedFromDate, SelectedToDate);
        }

        private void xBranchCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xEmployeeCmb.ItemsSource = null;
            BranchDetail = xBranchCmb.SelectedValue as BranchViewModel;
            ContextEmployeeList = new ObservableCollection<EmployeeViewModel>(EmployeeRepository.GetEmployees(BranchDetail.BranchId));
            ContextEmployeeList.Insert(0, new EmployeeViewModel() { EmployeeName = "ALL" });

            xEmployeeCmb.ItemsSource = ContextEmployeeList;
        }
        private void xEmployeeCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (xEmployeeCmb.SelectedIndex >= 0)
            {
                EmployeeDetail = xEmployeeCmb.SelectedItem as EmployeeViewModel;
            }
        }


        async void LoadData(BranchViewModel branchDetail, EmployeeViewModel employeeDetail, DateTime fromDate, DateTime toDate)
        {
            xCollectionEntryView.ItemsSource = null;
            xCollectionCount.Text = string.Empty;
            xTotalCollectionAmount.Text = string.Empty;
            xTotalDays.Text = string.Empty;
            xAmountInWords.Text = string.Empty;

            CollectionEntryList = null;
            //
            xLoadingGif.Visibility = Visibility.Visible;

            if(EmployeeDetail.EmployeeName == "ALL")
            {
                await Task.Run(() =>
                {
                    CollectionEntryList = LoanRepository.GetCollectedList(branchDetail.BranchId, fromDate, toDate);
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    CollectionEntryList = LoanRepository.GetCollectedList(branchDetail.BranchId, employeeDetail.EmployeeId, fromDate, toDate);
                });
            }
            

            xCollectionCount.Text = CollectionEntryList.Count().ToString();
            int SumValue = CollectionEntryList.Select(o => o.PaidAmount).Sum();
            xTotalCollectionAmount.Text = string.Format(new CultureInfo("en-IN"), "{0:c}", SumValue);

            xTotalDays.Text = (toDate - fromDate).Days.ToString() + " Days";
            xAmountInWords.Text = SumValue.ToWords(new CultureInfo("en-IN")).Humanize(LetterCasing.Sentence);

            xLoadingGif.Visibility = Visibility.Collapsed;
            //
            xCollectionEntryView.ItemsSource = CollectionEntryList;
        }
        private void xFromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFromDate = (DateTime)xFromDate.SelectedDate;
            if (SelectedFromDate > DateTime.Today)
                SelectedFromDate = DateTime.Today;
            xFromDateTB.Text = SelectedFromDate.ToShortDateString();
        }

        private void xToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedToDate = (DateTime)xToDate.SelectedDate;
            if (SelectedToDate < SelectedFromDate)
            {
                SelectedToDate = SelectedFromDate;
                MessageBox.Show("Select valid date.");
            }
            else if (SelectedToDate > DateTime.Today)
                SelectedToDate = DateTime.Today;

            xToDateTB.Text = SelectedToDate.ToShortDateString();
        }

        private void xFetch_Click(object sender, RoutedEventArgs e)
        {
            LoadData(BranchDetail, EmployeeDetail, SelectedFromDate, SelectedToDate);
        }
    }    
}
