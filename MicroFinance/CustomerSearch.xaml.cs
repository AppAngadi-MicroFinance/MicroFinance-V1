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
        }
        public CustomerSearch(string BranchID)
        {
            InitializeComponent();

            BranchList = EmployeeRepository.GetBranches();
            CenterList = CustomerRepository.GetCenters();
            BranchCombo.ItemsSource = BranchList;
            BranchCombo.SelectedIndex = SelectedBranch(BranchID);
            BranchCombo.IsEnabled = false;
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
            CustomerViewModel SelectedCustomer = CustomerList.SelectedItem as CustomerViewModel;

            List<LoanApplicationViewModel> Applications= LoanRepository.LoanApplicationDetails(SelectedCustomer.CustomerID);
            List<LoanViewModel> Laons= LoanRepository.LoanDetails(SelectedCustomer.CustomerID);
            // MessageBox.Show(SelectedCustomer.CustomerID+" | "+SelectedCustomer.CustomerName);
            this.NavigationService.Navigate(new GTrustCustomerProfile(SelectedCustomer.CustomerID));
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
    }
}
