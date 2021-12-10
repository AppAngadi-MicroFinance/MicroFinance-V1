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
    /// Interaction logic for AssignCenter.xaml
    /// </summary>
    public partial class AssignCenter : Page
    {
        public static List<TimeTableViewModel> TimeTableList = new List<TimeTableViewModel>();
        public static List<int> Hours = new List<int>() {6,7,8,9,10,11,12};
        public static List<int> Minutes = new List<int>() {0,30};
        public static ObservableCollection<EmployeeViewModel> EmployeeList = new ObservableCollection<EmployeeViewModel>();
        public static List<EmployeeViewModel> NewEmployees = new List<EmployeeViewModel>();
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        public static string EmpName = string.Empty;


        public static SHGView SHG = new SHGView();
        public static ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();

        public AssignCenter()
        {
            InitializeComponent();
        }
        
        public AssignCenter(string EmpId)
        {
            InitializeComponent();
            TimeTableList = EmployeeRepository.GetTimeTable(EmpId);
            xTimeHour.ItemsSource = Hours;
            xTimeMinute.ItemsSource = Minutes;
            Employees = EmployeeRepository.GetEmployees();
            string BranchId = Employees.Where(temp => temp.EmployeeId == EmpId).Select(temp => temp.BranchId).FirstOrDefault();
            EmpName = Employees.Where(temp => temp.EmployeeId == EmpId).Select(temp => temp.EmployeeName).FirstOrDefault();
            EmployeeNameText.Text = EmpName;
            EmployeeNameText1.Text = EmpName;
            NewEmployees = EmployeeRepository.GetNewEmployees(BranchId);
            NewEmployeeCombo1.ItemsSource = NewEmployees;
            LoadEmployee(BranchId, EmpId);
            TimeTableGrid.ItemsSource = TimeTableList;
            NewEmployeeCombo.ItemsSource = EmployeeList;

        }


        void LoadEmployee(string BranchId,string EmpId)
        {
            EmployeeList.Clear();
            foreach(EmployeeViewModel emp in Employees)
            {
                if (emp.BranchId ==BranchId && emp.Designation.ToLower() == "field officer" && emp.EmployeeId!=EmpId)
                {
                    EmployeeList.Add(emp);
                }
            }
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            TimeTableViewModel SelectedTimeTable = TimeTableGrid.SelectedItem as TimeTableViewModel;
            EmpNameBox.Text = EmpName;
            ShgAssignPanel.Visibility = Visibility.Visible;

             SHG = new SHGView() { SHGName=SelectedTimeTable.SHGName,SHGID=SelectedTimeTable.SHGId,CollectionDay=SelectedTimeTable.CollectionDay,CollectionTime=SelectedTimeTable.CollectionTime,CurrentEmployee=SelectedTimeTable.EmpId};
            ShgAssignPanel.DataContext = SHG;
           

        }

        private void AssignSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            bool Result = EmployeeRepository.IsAlreadyHaveShedule(SHG);
            if(Result)
            {
                message = language.translate(SystemFunction.IsTamil, "AE5");//This Time Already Assign To this Employee Select Another Time!...
                MessageBox.Show(message);
            }
            else
            {
                bool result = EmployeeRepository.AssignNewEmployeeToCenter(SHG);
                if(result)
                {
                    this.NavigationService.Navigate(new AssignCenter(SHG.CurrentEmployee));
                }
            }

        }

        private void NewEmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeViewModel SelectedEmployee = NewEmployeeCombo.SelectedItem as EmployeeViewModel;
            if(SelectedEmployee!=null)
            {
                SHG.NewEmployee = SelectedEmployee.EmployeeId;
            }
           
        }

        private void FullAssignBtn_Click(object sender, RoutedEventArgs e)
        {
            if(NewEmployeeCombo1.SelectedIndex!=-1)
            {
                EmployeeViewModel NewSelectedEmployee = NewEmployeeCombo1.SelectedItem as EmployeeViewModel;

                string Message= "Are You Sure You Want to Change All Center To "+NewSelectedEmployee.EmployeeName;
                MessageBoxResult result = MessageBox.Show(Message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result==MessageBoxResult.Yes)
                {
                    EmployeeRepository.AssignNewEmployeeToCenter(NewSelectedEmployee.EmployeeId, TimeTableList);
                    string Designation = MainWindow.LoginDesignation.LoginDesignation;
                    Designation = (Designation == null) ? "" : Designation;
                    LoadHomePage(Designation);
                }
                
                
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "W3");//Please Select The Employee
                MessageBox.Show(message);
            }
            
        }
        public void LoadHomePage(string Designation)
        {
            if (Designation.Equals("Field Officer"))
                this.NavigationService.Navigate(new DashboardFieldOfficer());
            else if (Designation.Equals("Accountant"))
                this.NavigationService.Navigate(new DashboardAccountant());
            else if (Designation.Equals("Branch Manager") || Designation.Equals("Manager"))
                this.NavigationService.Navigate(new DashboardBranchManager());
            else if (Designation.Equals("Region Manager"))
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            else
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectAllCenterCheck_Click(object sender, RoutedEventArgs e)
        {
           if(SelectAllCenterCheck.IsChecked==true)
            {
                AssignPanel1.Visibility = Visibility.Visible;
            }
           else if(SelectAllCenterCheck.IsChecked==false)
            {
                AssignPanel1.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            string Designation = MainWindow.LoginDesignation.LoginDesignation;
            Designation = (Designation == null) ? "" : Designation;
            LoadHomePage(Designation);
        }
    }

    public class SHGView
    {
        public string SHGName { get; set; }
        public string SHGID { get; set; }
        public string CollectionDay { get; set; }
        public TimeSpan CollectionTime { get; set; }
        public string CurrentEmployee { get; set; }
        public string NewEmployee { get; set; }

        public int Hour
        {
            get
            {
                return CollectionTime.Hours;
            }
            set
            {
                int h = value;
            }
        }
        public int Minute
        {
            get
            {
                return CollectionTime.Minutes;
            }
            set
            {
                int m = value;
            }
        }
    }
}
