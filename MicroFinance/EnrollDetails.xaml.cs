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
        public static IEnumerable<EmployeeViewModel> FOList;
        string BranchID = "";
        string GEmployeeID = "";
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
           // BranchCombo.ItemsSource = Branches;
            //SetBranch(BranchId);
            BranchCombo.IsEnabled = false;
            EmployeeCombo.IsEnabled = false;
            BranchPanel.Visibility = Visibility.Collapsed;
            EmployeePanel.Visibility = Visibility.Collapsed;
            BranchID = BranchId;
            GEmployeeID = MainWindow.LoginDesignation.EmpId;
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
                FOList = MainWindow.BasicDetails.EmployeeList.Where(temp=>temp.Designation=="Field Officer").Select(temp=>new EmployeeViewModel {BranchId=temp.BranchId,EmployeeName=temp.EmployeeName,EmployeeId=temp.EmployeeId});
                string ManagerBranchID = MainWindow.LoginDesignation.BranchId;
                BranchID = ManagerBranchID;
                LoadEmployee(ManagerBranchID);
                SetBranch(ManagerBranchID);
                EmployeeCombo.SelectedIndex = 0;
                BranchCombo.IsEnabled = false;
                BranchPanel.Visibility = Visibility.Collapsed;
            }
            else if(MainWindow.LoginDesignation.LoginDesignation.Equals("Region Manager"))
            {
                FOList = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.Designation == "Field Officer").Select(temp => new EmployeeViewModel { BranchId = temp.BranchId, EmployeeName = temp.EmployeeName, EmployeeId = temp.EmployeeId });
                Branches = MainWindow.BasicDetails.BranchList;
                Employees = MainWindow.BasicDetails.EmployeeList;
                LoadBranch();
                //LoadEmployee();
                BranchCombo.SelectedIndex = 0;
                EmployeeCombo.SelectedIndex = 0;
            }
            //Branches = EmployeeRepository.GetBranches();
            //BranchCombo.ItemsSource = Branches;
            Loaddata();
            EnrollDetailGrid.ItemsSource = BindingData;
        }

        void LoadBranch()
        {
            BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "All", RegionId = "ALL" };
            BranchCombo.Items.Add(AllBranch);
            foreach(BranchViewModel Branch in Branches)
            {
                BranchCombo.Items.Add(Branch);
            }
        }
        void LoadEmployee()
        {
            EmployeeViewModel AllEmployee = new EmployeeViewModel { BranchId = "All", EmployeeId = "All", EmployeeName = "All" };
            EmployeeCombo.Items.Add(AllEmployee);
            foreach(EmployeeViewModel employee in FOList)
            {
                Employees.Add(employee);
                EmployeeCombo.Items.Add(employee);
            }
            
        }
        void LoadEmployee(string BranchID)
        {
            EmployeeCombo.Items.Clear();
            EmployeeViewModel AllEmployee = new EmployeeViewModel { BranchId = "All", EmployeeId = "All", EmployeeName = "All" };
            EmployeeCombo.Items.Add(AllEmployee);
            foreach (EmployeeViewModel employee in FOList)
            {
                if(employee.BranchId==BranchID)
                {
                   //Employees.Add(employee);
                    EmployeeCombo.Items.Add(employee);
                }
            }

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
            if(!BranchID.Equals("ALL",StringComparison.CurrentCultureIgnoreCase))
            {
                if(!GEmployeeID.Equals("ALL",StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (EnrollDetailsView enroll in EnrollList)
                    {
                        if (enroll.EnrollDate >= StartDate && enroll.EnrollDate <= EndDate && enroll.BranchId == BranchID && enroll.EmployeeId == GEmployeeID)
                        {
                            BindingData.Add(enroll);
                        }

                    }
                }
                else
                {
                    foreach (EnrollDetailsView enroll in EnrollList)
                    {
                        if (enroll.EnrollDate >= StartDate && enroll.EnrollDate <= EndDate && enroll.BranchId == BranchID)
                        {
                            BindingData.Add(enroll);
                        }

                    }
                }
                
            }
            else
            {
                if(!GEmployeeID.Equals("ALL",StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (EnrollDetailsView enroll in EnrollList)
                    {
                        if (enroll.EnrollDate >= StartDate && enroll.EnrollDate <= EndDate && enroll.EmployeeId == GEmployeeID)
                        {
                            BindingData.Add(enroll);
                        }

                    }
                }
                else
                {
                    foreach (EnrollDetailsView enroll in EnrollList)
                    {
                        if (enroll.EnrollDate >= StartDate && enroll.EnrollDate <= EndDate)
                        {
                            BindingData.Add(enroll);
                        }

                    }
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
            BindingData.Clear();
            foreach(EnrollDetailsView enroll in EnrollList)
            {
                if(enroll.EmployeeId==EmpID)
                {
                    BindingData.Add(enroll);
                }
            }
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
            LoadEmployee(BranchID);
            EmployeeCombo.SelectedIndex = 0;
        }

        private void AadharSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string AadharNumber = AadharSearchBox.Text;
            LoadData(AadharNumber);
        }

        private void EmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeViewModel SelectedEmployee = EmployeeCombo.SelectedItem as EmployeeViewModel;

            if (SelectedEmployee != null)
            {
                GEmployeeID = SelectedEmployee.EmployeeId;
                if (!SelectedEmployee.EmployeeId.Equals("ALL", StringComparison.InvariantCultureIgnoreCase))
                {
                    LoadDataByEmployee(SelectedEmployee.EmployeeId);
                }
                else
                {
                    BindingData.Clear();
                    if (!BranchID.Equals("ALL"))
                    {
                        foreach (EnrollDetailsView Enroll in EnrollList)
                        {
                            if (Enroll.BranchId == BranchID)
                            {
                                BindingData.Add(Enroll);
                            }
                        }
                    }
                    else
                    {
                        foreach (EnrollDetailsView Enroll in EnrollList)
                        {

                            BindingData.Add(Enroll);

                        }
                    }

                }
            }
           
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            BranchCombo.SelectedIndex = 0;
            EmployeeCombo.SelectedIndex = 0;
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            Loaddata();
            AadharSearchBox.Text = "";
        }
    }
}
