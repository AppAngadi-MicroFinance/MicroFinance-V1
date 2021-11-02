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
using MicroFinance.ViewModel;
using MicroFinance.Repository;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CustomerSearch.xaml
    /// </summary>
    public partial class CustomerSearch : Page
    {
        public static ObservableCollection<BranchViewModel> BranchList = new ObservableCollection<BranchViewModel>();
        public static List<CenterViewModel> CenterList = new List<CenterViewModel>();
        public CustomerSearch()
        {
            InitializeComponent();

            BranchList = EmployeeRepository.GetBranches();
            CenterList = CustomerRepository.GetCenters();
            BranchCombo.ItemsSource = BranchList;
            SelectOptionPanel.IsOpen = false;
        }
        public CustomerSearch(string BranchID)
        {
            InitializeComponent();

            BranchList = EmployeeRepository.GetBranches();
            CenterList = CustomerRepository.GetCenters();
            BranchCombo.ItemsSource = BranchList;
            BranchCombo.SelectedIndex = SelectedBranch(BranchID);
            BranchCombo.IsEnabled = false;
            SelectOptionPanel.IsOpen = false;
        }

        int SelectedBranch(string branchID)
        {
            int count = 0;
            foreach(BranchViewModel Branch in BranchList)
            {
                if(Branch.BranchId==branchID)
                {
                    return count;
                }
                count++;
            }
            return 0;
        }

        private void CustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerViewModel SelectedCustomer = new CustomerViewModel();
            if(CustomerList.SelectedIndex!=-1)
            {
                SelectedCustomer = CustomerList.SelectedItem as CustomerViewModel;
                SelectOptionPanel.IsOpen = true;
                MainGrid.IsEnabled = false;
            }
            

            //this.NavigationService.Navigate(new GTrustCustomerProfile(SelectedCustomer.CustomerID));
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchViewModel SelecctedBranch = BranchCombo.SelectedItem as BranchViewModel;

            LoadData(SelecctedBranch.BranchId);

        }

        void LoadData(string BranchID)
        {
            CenterCombo.Items.Clear();
            foreach(CenterViewModel center in CenterList)
            {
                if(center.BranchId==BranchID)
                {
                    CenterCombo.Items.Add(center);
                }
            }
        }

        private void CenterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterViewModel selectedCenter = CenterCombo.SelectedItem as CenterViewModel;
            if(selectedCenter!=null)
            {
                List<CustomerViewModel> Customers = CustomerRepository.Customers(selectedCenter.CenterID);
                LoadCustomers(Customers);
                GroupMemberCount.Text =(Customers.Count<=9)?"0"+Customers.Count.ToString(): Customers.Count.ToString();
            }
           


        }


        void LoadCustomers(List<CustomerViewModel> Customers)
        {
            CustomerList.Items.Clear();
            foreach(CustomerViewModel customer in Customers)
            {
                CustomerList.Items.Add(customer);
            }
        }

        private void ViewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectOptionPanel.IsOpen = false;
            CustomerViewModel SelectedCustomer = CustomerList.SelectedItem as CustomerViewModel;
            this.NavigationService.Navigate(new GTrustCustomerProfile(SelectedCustomer.CustomerID));
        }

        private void PopUpCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectOptionPanel.IsOpen = false;
            MainGrid.IsEnabled = true;
        }

        private void CustomerEditBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectOptionPanel.IsOpen = false;
            CustomerViewModel SelectedCustomer = CustomerList.SelectedItem as CustomerViewModel;
            this.NavigationService.Navigate(new AddCustomer(SelectedCustomer.CustomerID));
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
           
        }
    }
}
