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
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for ModifyEmployee.xaml
    /// </summary>
    public partial class ModifyEmployee : Page
    {
        Employee employee1 = new Employee();
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public List<Employee> emplist;
        public ModifyEmployee()
        {
            InitializeComponent();
            AddEmployees();
        }

        public void AddEmployees()
        {
            emplist = new List<Employee>();
            employee1.GetEmployeeList();
            emplist = employee1.EmployeeList;
        }

        private void searchbntn_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(serachtxt.Text))
            {
                Employeelist.Items.Clear();
                ResultedEmployee(serachtxt.Text);
            }
            else
            {
                MessageBox.Show("Please Enter the Name");
            }

        }
        public void ResultedEmployee(string name)
        {
            foreach(var v in emplist)
            {
                if(v.EmployeeName!="")
                {
                    if ((v.EmployeeName).StartsWith(serachtxt.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Employeelist.Items.Add(v);
                    }
                }
                
            }
           
        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = Employeelist.SelectedItem as Employee;
            this.NavigationService.Navigate(new AddEmployee(employee));
        }

        private void serachtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Employeelist.Items.Clear();
            ResultedEmployee(serachtxt.Text);
        }

        private void Employeelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = Employeelist.SelectedItem as Employee;
            this.NavigationService.Navigate(new AddEmployee(employee));
        }

        

       
       
    }

   
}
