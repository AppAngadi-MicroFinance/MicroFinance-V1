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
using MicroFinance.ViewModel;
using MicroFinance.Repository;
using System.Collections.ObjectModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AssignSHG.xaml
    /// </summary>
    public partial class AssignSHG : Page
    {
        SHGModal CenterDetails = new SHGModal();
        ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        string CenterID = "";
        public AssignSHG()
        {
            InitializeComponent();
            List<int> TimeHour = new List<int> { 6, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6 };
            xTimeHour.ItemsSource = TimeHour;

            List<int> TimeMinute = new List<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            xTimeMinute.ItemsSource = TimeMinute;
        }
        public AssignSHG(SHGModal SHGDetails)
        {
            InitializeComponent();
            CenterDetails = SHGDetails;
            CenterName.Text = SHGDetails.SHGName;
            LoadEmployee();
            EmployeeNameCombo.ItemsSource = Employees;
            List<int> TimeHour = new List<int> { 6, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6 };
            xTimeHour.ItemsSource = TimeHour;

            List<int> TimeMinute = new List<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            xTimeMinute.ItemsSource = TimeMinute;
        }
        void LoadEmployee()
        {
            foreach(EmployeeViewModel employee in MainWindow.BasicDetails.EmployeeList)
            {
                if(employee.BranchId==MainWindow.LoginDesignation.BranchId&&employee.Designation.Equals("Field Officer"))
                {
                    Employees.Add(employee);
                }
            }
        }
        List<int> TimeHour = new List<int> { 6, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6 };
        List<int> TimeMinute = new List<int> { 00, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
        List<string> WeekDays = new List<string> { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };
        
        public AssignSHG(TimeTable SheduleDetails)
        {
            InitializeComponent();
            CenterID = SheduleDetails.SHGID;
            LoadEmployee();
            EmployeeNameCombo.ItemsSource = Employees;

           
            xTimeHour.ItemsSource = TimeHour;

            
            xTimeMinute.ItemsSource = TimeMinute;
            LoadCenterData(SheduleDetails);
            AssignBtn.Visibility = Visibility.Collapsed;
        }
        void LoadCenterData(TimeTable Shedule)
        {
            CenterName.Text = Shedule.CenterName;
            EmployeeNameCombo.SelectedIndex = SelectedEmployee(Shedule.EmployeeID);
            string[] timearr = Shedule.CollectionTime.Split(':');
            xTimeHour.SelectedIndex = selectedHour(timearr[0]);
            xTimeMinute.SelectedIndex =selectedMin(timearr[1]);
            CollectionDayCombo.SelectedIndex =selectedDay(Shedule.CollectionDay);

        }

        int selectedHour(string hour)
        {
            int Count = 0;
            foreach(int s in TimeHour)
            {
                if(s==Convert.ToInt32(hour))
                {
                    return Count;
                }
                Count++;
            }
            return -1;
        }
        int selectedMin(string Min)
        {
            int Count = 0;
            foreach (int s in TimeMinute)
            {
                if (s == Convert.ToInt32(Min))
                {
                    return Count;
                }
                Count++;
            }
            return -1;
        }


        int SelectedEmployee(string EmpID)
        {
            int Count = 0;
            foreach(EmployeeViewModel employee in Employees)
            {
                if(employee.EmployeeId==EmpID)
                {
                    return Count;
                }
                Count++;
            }
            return -1;
        }
        int selectedDay(string Day)
        {
            int Count = 0;
            foreach(string s in WeekDays)
            {
                if(s.Equals(Day))
                {
                    return Count;
                }
                Count++;
            }
            return -1;
        }

        private void AssignBtn_Click(object sender, RoutedEventArgs e)
        {
            TimeTable Shedule = new TimeTable();
            Shedule.SHGID = CenterDetails.SHGId;
            Shedule.CollectionTime =GetSheduleTime();
            if(CollectionDayCombo.SelectedIndex!=-1)
            {
                ComboBoxItem SelectedItem = CollectionDayCombo.SelectedItem as ComboBoxItem;
                Shedule.CollectionDay = SelectedItem.Content.ToString();
            }
            else
            {
                MessageBox.Show("Select CollectionDay!...","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            EmployeeViewModel SelectedEmployee = EmployeeNameCombo.SelectedItem as EmployeeViewModel;
            Shedule.EmployeeID = SelectedEmployee.EmployeeId;


            if(!TimeTableRepository.IsAlreadyAllocated(Shedule.EmployeeID,Shedule.CollectionDay,Shedule.CollectionTime))
            {
                bool Res = TimeTableRepository.CreateShedule(Shedule);
                if (Res)
                {
                    MessageBox.Show("Shedule Created SuccessFully!...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new DashboardBranchManager());
                }
                else
                {
                    MessageBox.Show("Error!..\nTry Again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Shedule Time Already Allocated!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            
        }

        public string GetSheduleTime()
        {
            string Hour =xTimeHour.SelectedItem==null?"00":  xTimeHour.SelectedItem.ToString();
            string Minute = xTimeMinute.SelectedItem == null ? "00" : xTimeMinute.SelectedItem.ToString();
            Minute = Minute == string.Empty ? "00" : Minute;
            string Time = Hour + ":" + Minute;

            return Time;

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            TimeTable Shedule = new TimeTable();
            Shedule.SHGID = CenterID;
            Shedule.CollectionTime = GetSheduleTime();
            if (CollectionDayCombo.SelectedIndex != -1)
            {
                ComboBoxItem SelectedItem = CollectionDayCombo.SelectedItem as ComboBoxItem;
                Shedule.CollectionDay = SelectedItem.Content.ToString();
            }
            else
            {
                MessageBox.Show("Select CollectionDay!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            EmployeeViewModel SelectedEmployee = EmployeeNameCombo.SelectedItem as EmployeeViewModel;
            Shedule.EmployeeID = SelectedEmployee.EmployeeId;

            bool Res = TimeTableRepository.UpdateShedule(Shedule);
            if (Res)
            {
                MessageBox.Show("Shedule Updated SuccessFully!...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
            else
            {
                MessageBox.Show("Error!..\nTry Again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardBranchManager());
        }
    }
}
