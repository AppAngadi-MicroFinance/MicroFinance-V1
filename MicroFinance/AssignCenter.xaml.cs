﻿using System;
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
                MessageBox.Show("This Time Already Assign To this Employee Select Another Time!...");
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
