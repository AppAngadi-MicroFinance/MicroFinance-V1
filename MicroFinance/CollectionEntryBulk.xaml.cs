using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MicroFinance.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionEntryBulk.xaml
    /// </summary>
    public partial class CollectionEntryBulk : Page
    {
        public string _EmployeeID = "";
        List<TimeTableViewModel> Centers = new List<TimeTableViewModel>();
        List<EmployeeViewModel> Employees = new List<EmployeeViewModel>();
        List<CustomerMetaData> Customers = new List<CustomerMetaData>();
        List<LoanDetailsView> CustomerLoanDetailsList = new List<LoanDetailsView>();
        SavingsAccountView CustoemrAccountDetail = new SavingsAccountView();
        public CollectionEntryBulk()
        {
            InitializeComponent();
        }
        public CollectionEntryBulk(string EmpID)
        {
            InitializeComponent();
            _EmployeeID = EmpID;
            Employees = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == EmpID).Select(temp => new EmployeeViewModel { BranchId = temp.BranchId, EmployeeId = temp.EmployeeId, EmployeeName = temp.EmployeeName, Designation = temp.Designation }).ToList();
            Centers = MainWindow.BasicDetails.CenterList.OrderBy(temp=>temp.SHGName).ToList();
            EmployeeCombo.ItemsSource = Employees;
            EmployeeCombo.SelectedIndex = 0;
            
            
        }

        private void EmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EmployeeCombo.SelectedItem!=null)
            {
                EmployeeViewModel SelectedEmployee = EmployeeCombo.SelectedItem as EmployeeViewModel;
                LoadCenter(SelectedEmployee.EmployeeId);
                
            }
        }

        void LoadCenter(string EmpID)
        {
            CenterCombo.Items.Clear();
            foreach(TimeTableViewModel Center in Centers)
            {
                if(Center.EmpId==EmpID)
                {
                    CenterCombo.Items.Add(Center);
                }
            }
        }

        private void CustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MenuPanel.Visibility = Visibility.Visible;
            CustomerList.IsEnabled = false;
        }
        async Task GetLoanDetails(string CustomerID)
        {
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetLoanDetails/" + CustomerID;
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, null);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<LoanDetailsView>>(result);

                if (status != null)
                {
                    CustomerLoanDetailsList.Clear();
                    CustomerLoanDetailsList = status;
                }
            }
            else
            {
                string message = response1.StatusCode.ToString();
                MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private async void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeTableViewModel SelectedCenter = CenterCombo.SelectedItem as TimeTableViewModel;
                CustomerList.Items.Clear();
                GifPanel.Visibility = Visibility.Visible;
                await GetCustomers(SelectedCenter.SHGId);
                LoadCustomer();
                GifPanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        async Task GetAccountDetails(string CustomerId)
        {
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetAccountDetails/" + CustomerId;
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, null);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<SavingsAccountView>(result);

                if (status != null)
                {
                    CustoemrAccountDetail = new SavingsAccountView();
                    CustoemrAccountDetail = status;
                }
            }
            else
            {
                string message = response1.StatusCode.ToString();
                throw new ArgumentException(message);
            }
        }
        async Task GetCustomers(string CenterID)
        {
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCustomerMetaDetails/"+CenterID;
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1,null);

            if(response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CustomerMetaData>>(result);

                if(status!=null)
                {
                    Customers.Clear();
                    Customers = status;
                    LoadCustomer();
                }
            }
            else
            {
                string message = response1.StatusCode.ToString();
                MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        void LoadCustomer()
        {
            CustomerList.Items.Clear();
            foreach (CustomerMetaData Customer in Customers)
            {
                CustomerList.Items.Add(Customer);
            }
        }
        private async void withdrawnRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerMetaData SelectedCustomer = CustomerList.SelectedItem as CustomerMetaData;
                GifPanel.Visibility = Visibility.Visible;
                await GetAccountDetails(SelectedCustomer.CustomerID);
                GifPanel.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new SavingsAmountWithdrawRequest(CustoemrAccountDetail));

            }
            catch(Exception ex)
            {
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message);
            }
            
        }
        private async void ColletionEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerMetaData SelectedCustomer = CustomerList.SelectedItem as CustomerMetaData;
                if (SelectedCustomer.ActiveLoans != 0)
                {
                    GifPanel.Visibility = Visibility.Visible;
                    await GetLoanDetails(SelectedCustomer.CustomerID);
                    GifPanel.Visibility = Visibility.Collapsed;
                    if (CustomerLoanDetailsList.Count != 0)
                    {
                        this.NavigationService.Navigate(new CollectionEntryBulk1(CustomerLoanDetailsList));
                    }
                    else
                    {
                        MessageBox.Show("No Data Found");
                    }
                }
                else
                {
                    MessageBox.Show("No Active Loans for " + SelectedCustomer.CustomerName, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            CustomerList.IsEnabled = true;
        }
        private void LoanRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerMetaData SelectedCustomer = CustomerList.SelectedItem as CustomerMetaData;

            LoanApplicationMeta MetaData = new LoanApplicationMeta();
            MetaData.CustomerName = SelectedCustomer.CustomerName;
            MetaData.CustomerID = SelectedCustomer.CustomerID;
            MetaData.CenterID = SelectedCustomer.CenterID;
            MetaData.EmpID = _EmployeeID;
            MetaData.BranchID = MainWindow.LoginDesignation.BranchId;

            bool res = LoanRepository.IsAlreadyInApplicationProcess(MetaData.CustomerID);
            if(res==false)
            {
                this.NavigationService.Navigate(new LoanRequestNew(MetaData));
            }
            else
            {
                MessageBox.Show("This Customer Already in Application Process", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
               
            
        }
    }


    public class LoanApplicationMeta:CustomerMetaData
    {
        public string EmpID { get; set; }
        public string BranchID { get; set; }
    }
}
