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
    /// Interaction logic for CollectionEntryBulk2.xaml
    /// </summary>
    public partial class CollectionEntryBulk2 : Page
    {
        public List<CollectionEntryView> CollectionDetailsList = new List<CollectionEntryView>();
        public CollectionEntryBulk2()
        {
            InitializeComponent();
        }
        public CollectionEntryBulk2(List<CollectionEntryView> CollectionDetails)
        {
            InitializeComponent();
            CollectionDetailsList = CollectionDetails;
            UpdateDate();
            CollectionDetailsGrid.ItemsSource = CollectionDetailsList;
        }

        void UpdateDate()
        {
            foreach(CollectionEntryView Collection in CollectionDetailsList)
            {
                Collection.CollectedOn = DateTime.Now.ToLocalTime();
            }
        }

        private async void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GifPanel.Visibility = Visibility.Visible;
                await InsertData();
                GifPanel.Visibility = Visibility.Collapsed;
                
            }
            catch(Exception ex)
            {
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message);
            }
           
        }

        async Task InsertData()
        {
            var json = JsonConvert.SerializeObject(CollectionDetailsList);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/CollectionEntry/1";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);


            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();

                MessageBox.Show("Insert Record SuccessFully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.Navigate(new CollectionEntryBulk(MainWindow.LoginDesignation.EmpId));

            }
            else
            {
                MessageBox.Show(response1.StatusCode.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
