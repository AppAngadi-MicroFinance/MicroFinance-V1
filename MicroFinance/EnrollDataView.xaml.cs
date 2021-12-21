using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
using MicroFinance.APIModal;
using MicroFinance.ViewModel;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for EnrollDataView.xaml
    /// </summary>
    public partial class EnrollDataView : Page
    {
       
        public ObservableCollection<CustomerEnrollMetaData> CustomerDataList = new ObservableCollection<CustomerEnrollMetaData>();
        public VMCustomerEnrollData CustomerData = new VMCustomerEnrollData();
       
        public EnrollDataView(ObservableCollection<CustomerEnrollMetaData> CustomerData)
        {
            InitializeComponent();
            CustomerDataList = CustomerData;

            EnrollDataGrid.ItemsSource = CustomerDataList;
        }

        private async void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            if(EnrollDataGrid.SelectedIndex!=-1)
            {
                try
                {
                    CustomerEnrollMetaData SelectedCustomer = new CustomerEnrollMetaData();
                    SelectedCustomer = EnrollDataGrid.SelectedItem as CustomerEnrollMetaData;
                    await GetCustomerData(SelectedCustomer.AadharNumber, SelectedCustomer.ContactNumber);
                    this.NavigationService.Navigate(new CustomerEnrollDataView(CustomerData));
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        async Task GetCustomerData(string AadharNumber,string ContactNumber)
        {
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCustomerEnrollData/" + AadharNumber+"/"+ContactNumber;

            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, null);
            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<VMCustomerEnrollData>(result);

                if(status!=null)
                {
                    CustomerData = status;
                }
                else
                {
                    throw new Exception("false");
                }
                

            }
            else
            {
                throw new Exception("false");
            }
        }
    }
}
