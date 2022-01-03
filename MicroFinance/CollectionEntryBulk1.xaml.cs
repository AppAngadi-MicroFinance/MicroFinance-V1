using System;
using System.Collections.Generic;
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
using MicroFinance.ViewModel;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionEntryBulk1.xaml
    /// </summary>
    public partial class CollectionEntryBulk1 : Page
    {
        public List<CollectionEntryView> CollectionDetailsList = new List<CollectionEntryView>();
        public CollectionEntryBulk1()
        {
            InitializeComponent();
        }
        public CollectionEntryBulk1(List<LoanDetailsView> loanDetails)
        {
            InitializeComponent();
            CustomerNameText.Text = loanDetails.Select(temp => temp.CustomerName).FirstOrDefault();
            LoanDetailsGrid.ItemsSource = loanDetails;
        }

        private void AddCollectionBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            LoanDetailsView SelectedLoan = LoanDetailsGrid.SelectedItem as LoanDetailsView;

            CustNameText.Text = SelectedLoan.CustomerName;
            LoanIDText.Text = SelectedLoan.LoanID;
            LoanDetailsGrid.IsEnabled = false;
            ConfirmationPanel.Visibility = Visibility.Visible;
        }

        private async void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            if(NoOfEntriesBox.Text!=null)
            {
                string LoanID = LoanIDText.Text;
                int NoofEntry= 0;
                bool res = int.TryParse(NoOfEntriesBox.Text, out NoofEntry);
                if(res && NoofEntry<=50)
                {
                    await GetCollections(LoanID, NoofEntry);
                    this.NavigationService.Navigate(new CollectionEntryBulk2(CollectionDetailsList));
                }
                else
                {
                    MessageBox.Show("Enter Proper Entry Value\n Value should be 1 to 50");
                }
            }
            
            
        }

        public class CollectionLoanView
        {
            public string LoanID { get; set; }
            public int NoOfWeeks { get; set; }
        }

        async Task GetCollections(string LoanID,int NoOfEntries)
        {
            CollectionLoanView Loan = new CollectionLoanView { LoanID = LoanID, NoOfWeeks = NoOfEntries };
            var json = JsonConvert.SerializeObject(Loan);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCollectionDetails/Loan";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CollectionEntryView>>(result);

                if (status != null)
                {
                    CollectionDetailsList = status;
                }
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            LoanDetailsGrid.IsEnabled = true;
            ConfirmationPanel.Visibility = Visibility.Collapsed;
        }
    }
}
