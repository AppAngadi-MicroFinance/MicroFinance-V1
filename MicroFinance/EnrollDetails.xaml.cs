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
        public EnrollDetails(string BranchId)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollList = EnrollDetailsRepository.GetEnrollDetails(BranchId);
            Branches = EmployeeRepository.GetBranches();
            BranchCombo.ItemsSource = Branches;
            BranchCombo.SelectedItem = BranchId;
            BranchCombo.IsEnabled = false;
            BranchID = BranchId;
            //EnrollDetailGrid.ItemsSource = EnrollList;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
        }
        public EnrollDetails(string BranchId,string EmpId)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollList = EnrollDetailsRepository.GetEnrollDetails(BranchId,EmpId);
            Branches = EmployeeRepository.GetBranches();
            BranchCombo.ItemsSource = Branches;
            BranchCombo.SelectedItem = BranchId;
            BranchCombo.IsEnabled = false;
            BranchID = BranchId;
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
            BranchCombo.SelectedItem = BranchId;
            BranchCombo.IsEnabled = false;
            BranchID = BranchId;
            //EnrollDetailGrid.ItemsSource = EnrollList;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
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
    }
}
