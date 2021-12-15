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
using MicroFinance.APIModal;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for EnrollDataView.xaml
    /// </summary>
    public partial class EnrollDataView : Page
    {
        public List<CustomerEnrollMetaData> CustomerDataList = new List<CustomerEnrollMetaData>();
        public EnrollDataView(List<CustomerEnrollMetaData> CustomerData)
        {
            InitializeComponent();
            CustomerDataList = CustomerData;

            EnrollDataGrid.ItemsSource = CustomerDataList;
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            if(EnrollDataGrid.SelectedIndex!=-1)
            {
                CustomerEnrollMetaData SelectedCustomer = new CustomerEnrollMetaData();
                SelectedCustomer = EnrollDataGrid.SelectedItem as CustomerEnrollMetaData;
            }
        }
    }
}
