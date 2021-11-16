using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MicroFinance.Repository;
using MicroFinance.ViewModel;
using System.Text.RegularExpressions;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for EnrollDetails.xaml
    /// </summary>
    public partial class EnrollDetails : Page
    {
        public static List<EnrollDetailsView> EnrollList = new List<EnrollDetailsView>();
        public static ObservableCollection<EnrollDetailsView> BindingData = new ObservableCollection<EnrollDetailsView>();
        public static ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        public static ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        string BranchID = "";
        public EnrollDetails()
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollList = EnrollDetailsRepository.GetEnrollDetails();
            Branches = EmployeeRepository.GetBranches();
            BranchCombo.ItemsSource = Branches;
            //EnrollDetailGrid.ItemsSource = EnrollList;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
        }
        public EnrollDetails(List<EnrollDetailsView> EnrollDetailsList,string BranchId)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollList =EnrollDetailsList;
            Branches = EmployeeRepository.GetBranches();
            BranchCombo.ItemsSource = Branches;
            SetBranch(BranchId);
            BranchCombo.IsEnabled = true;
            BranchID = BranchId;
            //EnrollDetailGrid.ItemsSource = EnrollList;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
        }
        public EnrollDetails(List<EnrollDetailsView> EnrollDetailsList)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollList = EnrollDetailsList;
            if(MainWindow.LoginDesignation.LoginDesignation.Equals("Manager"))
            {
                Employees = MainWindow.BasicDetails.EmployeeList;
                Branches = MainWindow.BasicDetails.BranchList;
            }
            Branches = EmployeeRepository.GetBranches();
            BranchCombo.ItemsSource = Branches;

            BranchCombo.IsEnabled = true;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
        }

        void SetBranch(string BranchId)
        {
            int Index = 0;
            foreach(BranchViewModel Branch in Branches)
            {
                if(Branch.BranchId.Equals(BranchId))
                {
                    BranchCombo.SelectedIndex=Index;
                    break;
                }
                Index++;
            }
        }
        void Loaddata()
        {
            BindingData.Clear();
            foreach(EnrollDetailsView enroll in EnrollList)
            {
                BindingData.Add(enroll);
            }
        }
        void LoadData(DateTime StartDate,DateTime EndDate)
        { 
            BindingData.Clear();
            foreach (EnrollDetailsView enroll in EnrollList)
            {
                if(enroll.EnrollDate>=StartDate && enroll.EnrollDate<=EndDate && enroll.BranchId==BranchID)
                {
                    BindingData.Add(enroll);
                }
                
            }
        }
        void LoadData(string AadharNumber)
        {
            
            if(!string.IsNullOrEmpty(AadharNumber))
            {
                BindingData.Clear();
                foreach (EnrollDetailsView enroll in EnrollList)
                {
                    if (enroll.AadharNumber.StartsWith(AadharNumber, StringComparison.InvariantCulture))
                    {
                        BindingData.Add(enroll);
                    }

                }
            }
            else
            {
                Loaddata();
            }
            
        }

        void LoadDataByEmployee(string EmpID)
        {

        }

        void LoanDataByBranch(string BranchID)
        {

        }



        private void FilterSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            LoadData(StartDate, EndDate);

        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
            BranchID = SelectedBranch.BranchId;
        }

        private void AadharSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string AadharNumber = AadharSearchBox.Text;
            LoadData(AadharNumber);
        }
    }
}
