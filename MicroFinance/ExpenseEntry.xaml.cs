using MicroFinance.Modal;
using MicroFinance.Repository;
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
    /// Interaction logic for ExpenseEntry.xaml
    /// </summary>
    public partial class ExpenseEntry : Page
    {

        public ExpenseEntry()
        {
            InitializeComponent();
            ExpenceTypeCombo.ItemsSource = ExpenceRepository.GetExpenceTypes();
            ExpenceDate.SelectedDate = DateTime.Today;
        }

        private void SumbitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValidEntry())
                {
                    ExpenceDetails Expence = new ExpenceDetails();
                    Expence.BranchID = (MainWindow.LoginDesignation.BranchId != null) ? MainWindow.LoginDesignation.BranchId : "Admin";
                    Expence.EmployeeID = (MainWindow.LoginDesignation.EmpId != null) ? MainWindow.LoginDesignation.EmpId : "Admin";
                    Expence.ExpenceType = ExpenceTypeCombo.SelectedItem as string;
                    Expence.Amount = Convert.ToInt32(AmountBox.Text);
                    Expence.ExpenceDate = ExpenceDate.SelectedDate.Value;
                    Expence.Reason = ReasonBox.Text;
                    ExpenceRepository.AddExpence(Expence);
                    this.NavigationService.Navigate(new DashboardBranchManager());
                }
                else
                {
                    MessageBox.Show("Please Enter All Fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardBranchManager());
        }



        bool IsValidEntry()
        {
            if(ExpenceTypeCombo.SelectedItem!=null&&!string.IsNullOrEmpty(AmountBox.Text)&&!string.IsNullOrEmpty(ReasonBox.Text)&&ExpenceDate.SelectedDate!=null)
            {
                return true;
            }

            return false;
        }
    }
}
