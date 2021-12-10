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
using MicroFinance.Utils;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for TransferEmployee.xaml
    /// </summary>
    public partial class TransferEmployee : Page
    {
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        public static ObservableCollection<BranchViewModel> BranchList = new ObservableCollection<BranchViewModel>();
        public static ObservableCollection<EmployeeViewModel> EmployeeList = new ObservableCollection<EmployeeViewModel>();
        public static List<RegionViewModel> RegionList = new List<RegionViewModel>();
        public static List<string> DesignationList = new List<string>();
        public static ObservableCollection<string> NewDesignationList = new ObservableCollection<string>();

        public static List<string> Designations = new List<string>() {"Field Officer", "Accountant", "Region Manager","Manager"};
        public static ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        public static ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();

        TransferEmployeeView Transfer_Employee = new TransferEmployeeView();
        public TransferEmployee()
        {
            InitializeComponent();

            RegionList = EmployeeRepository.GetRegions();
            Branches = EmployeeRepository.GetBranches();
            Employees = EmployeeRepository.GetEmployees();
            LoadDesignation();

            RegionCombo.ItemsSource = RegionList;
            BranchCombo.ItemsSource = BranchList;
            EmployeeCombo.ItemsSource = EmployeeList;
            DesignationCombo.ItemsSource = Designations;



            NewBranchCombo.ItemsSource = BranchList;
            NewDesinationCombo.ItemsSource = NewDesignationList;


        }



        void LoadDesignation()
        {
            foreach(string s in Designations)
            {
                DesignationList.Add(s);
            }
        }
        void LoadDesignation(int value)
        {
            NewDesignationList.Clear();
            foreach (string s in Designations)
            {
                NewDesignationList.Add(s);
            }
        }

        void LoadDesignation(string CurrentDesignation)
        {
            NewDesignationList.Clear();
            foreach(string s in Designations)
            {
                if(!s.Equals(CurrentDesignation))
                {
                    NewDesignationList.Add(s);
                }
            }
        }

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchList.Clear();
            EmployeeList.Clear();
            TransferPanel.Visibility = Visibility.Collapsed;
            Transfer_Employee = new TransferEmployeeView();
            RegionViewModel SelectedRegion = RegionCombo.SelectedItem as RegionViewModel;

            Transfer_Employee.RegionID = SelectedRegion.RegionId;
            Transfer_Employee.RegionName = SelectedRegion.RegionName;
            SelectedBranches(SelectedRegion.RegionId);
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TransferPanel.Visibility = Visibility.Collapsed;
            NewBranchCombo.SelectedIndex = -1;
            NewDesinationCombo.SelectedIndex = -1;
            DesignationCombo.SelectedIndex = -1;
            EmployeeList.Clear();

        }

        private void DesignationCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TransferPanel.Visibility = Visibility.Collapsed;
            NewBranchCombo.SelectedIndex = -1;
            NewDesinationCombo.SelectedIndex = -1;

            EmployeeList.Clear();
            BranchViewModel selectedBranch = BranchCombo.SelectedItem as BranchViewModel;
            if(selectedBranch!=null)
            {
                string Designation = DesignationCombo.SelectedValue as string;
                Transfer_Employee.BranchName = selectedBranch.BranchName;
                Transfer_Employee.BranchId = selectedBranch.BranchId;
                Transfer_Employee.OldDesignation = Designation;
                SelectedEmployees(selectedBranch.BranchId, Designation);
            }
            
        }

        private void EmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TransferPanel.Visibility = Visibility.Collapsed;
            NewBranchCombo.SelectedIndex = -1;
            NewDesinationCombo.SelectedIndex = -1;
        }



        void SelectedBranches(string RegionId)
        {
            BranchList.Clear();
            foreach(BranchViewModel branch in Branches)
            {
                if(branch.RegionId==RegionId)
                {
                    BranchList.Add(branch);
                }
            }
        }

        void SelectedEmployees(string BranchId,string Designation)
        {
            foreach(EmployeeViewModel employee in Employees)
            {
                if(employee.BranchId==BranchId && employee.Designation==Designation)
                {
                    EmployeeList.Add(employee);
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            

            EmployeeViewModel selectedEmployee = EmployeeCombo.SelectedItem as EmployeeViewModel;

            Transfer_Employee.EmployeeId = selectedEmployee.EmployeeId;
            Transfer_Employee.EmployeeName = selectedEmployee.EmployeeName;

            List<TimeTableViewModel> TimeTableList = EmployeeRepository.GetTimeTable(selectedEmployee.EmployeeId);
            if(TimeTableList.Count==0)
            {
                TransferPanel.Visibility = Visibility.Visible;
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "W26");//This Employee Assign in Various Group Please Check
                string message1 = language.translate(SystemFunction.IsTamil, "W27");//Click -Yes- to Check"
                MessageBoxResult result= MessageBox.Show(message+"\n"+ message1 ,"Warning",MessageBoxButton.YesNo,MessageBoxImage.Warning);
                if(MessageBoxResult.Yes==result)
                {
                    this.NavigationService.Navigate(new AssignCenter(selectedEmployee.EmployeeId));
                }
            }
        }

        private void RegionCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BranchCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DesignationCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewBranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                BranchViewModel NewSelectedBranch = NewBranchCombo.SelectedItem as BranchViewModel;
                BranchViewModel selectedBranch = BranchCombo.SelectedItem as BranchViewModel;

                Transfer_Employee.NewBranchId = NewSelectedBranch.BranchId;
                Transfer_Employee.NewBranchName = NewSelectedBranch.BranchName;
                if (NewSelectedBranch.BranchId == selectedBranch.BranchId)
                {
                    string value = DesignationCombo.SelectedValue as string;
                    LoadDesignation(DesignationCombo.SelectedValue as string);
                }
                else
                {
                    LoadDesignation(1);
                }
            }
            catch(Exception ex)
            {

            }
           

        }

        private void NewDesinationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string NewDesination = NewDesinationCombo.SelectedValue as string;
            Transfer_Employee.NewDesignation = NewDesination;
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            bool Res= EmployeeRepository.TransferEmployee(Transfer_Employee);
            if(Res)
            {
                message = language.translate(SystemFunction.IsTamil, "AE7");//"Employee Transfered Successfully!..."
                MainWindow.StatusMessageofPage(1, message);
                System.Threading.Thread.Sleep(2000);
                MainWindow.StatusMessageofPage(1, "Ready");
                this.NavigationService.Navigate(new TransferEmployee());

            }
            else
            {

            }

        }
    }


    public class TransferEmployeeView
    {
        public string RegionName { get; set; }
        public string RegionID { get; set; }
        public string BranchName { get; set; }
        public string BranchId { get; set; }
        public string OldDesignation { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string NewBranchName { get; set; }
        public string NewBranchId { get; set; }
        public string NewDesignation { get; set; }
    }
}
