using MicroFinance.Modal;
using MicroFinance.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DenominationPage.xaml
    /// </summary>
    public partial class DenominationPage : Page
    {

        List<DenominationModel> Dlist = new List<DenominationModel>();
        ObservableCollection<CollectionEntryView> CollectionEntryDetails = new ObservableCollection<CollectionEntryView>();
        string CenterName = "";
        int initialAmt = 0;
        Button btn;
        public DenominationPage()
        {

        }
        public DenominationPage(int TotalCollectedAmount,string BranchName,string CenterName,DateTime Date,Button button)
        {
            btn = button;
            initialAmt = TotalCollectedAmount;
            InitializeComponent();
            TotalAmount.Text = initialAmt.ToString("C");
            BranchBlock.Text = BranchName;
            CenterBlock.Text = CenterName;
            DateBlock.Text = Date.ToString("yyyy-MM-dd");
            DayBlock.Text = Date.DayOfWeek.ToString();
            AddBasic();
            DenominationList.ItemsSource = Dlist;
        }
        public DenominationPage(ObservableCollection<CollectionEntryView> CollectionDetails,string centerName)
        {
           
           
            InitializeComponent();
            initialAmt = CollectionDetails.Select(temp => temp.Total).Sum();
            CenterName = centerName;
            CollectionEntryDetails = CollectionDetails;
            TotalAmount.Text = initialAmt.ToString("C");
           
            
            AddBasic();
            DenominationList.ItemsSource = Dlist;
        }

        void AddBasic()
        {
            Dlist.Add(new DenominationModel(2000, "0"));
            Dlist.Add(new DenominationModel(500, "0"));
            Dlist.Add(new DenominationModel(200, "0"));
            Dlist.Add(new DenominationModel(100, "0"));
            Dlist.Add(new DenominationModel(50, "0"));
            Dlist.Add(new DenominationModel(20, "0"));
            Dlist.Add(new DenominationModel(10, "0"));
            Dlist.Add(new DenominationModel(5, "0"));
            Dlist.Add(new DenominationModel(2, "0"));
            Dlist.Add(new DenominationModel(1, "0"));
        }
        bool _checkIsValid = false;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            long currentAmt = Total();

            TotalBox.Text = currentAmt.ToString("C0");
            if (initialAmt == currentAmt)
            {
                _checkIsValid = true;
                TotalBox.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                _checkIsValid = false;
                TotalBox.Background = new SolidColorBrush(Colors.Red);
            }

        }
        public long Total()
        {
            long total = 0;
            foreach (var x in Dlist)
            {
                long a = x.Amount;
                long parsed = 0;
                if (long.TryParse(x.Multiples, out parsed))
                {
                    long ans = a * parsed;
                    total += ans;
                }
            }
            return total;
        }
        public bool  AlreadyEntered = false;
        private async void SaveDenomination_Click(object sender, RoutedEventArgs e)
        {
            if(_checkIsValid)
            {
                //btn.IsEnabled = true;
                //AlreadyEntered = true;
                //NavigationService.GoBack();
                try
                {
                    GifPanel.Visibility = Visibility.Visible;
                    await SubmitCollection();
                    GifPanel.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Collection Added SuccessFully!...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new DashboardFieldOfficer());
                }
                catch(Exception)
                {
                    MessageBox.Show("Error Occur!...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            else
            {
                MainWindow.StatusMessageofPage(0, "Please Check Denomination and Enter Correct Denomination.....");
            }
        }
        public DenominationView InsertDenomination()
        {
            string _branchId = MainWindow.LoginDesignation.BranchId;
            string _regionName = MainWindow.LoginDesignation.RegionName;
            string _empId = MainWindow.LoginDesignation.EmpId;
            string _date = DateBlock.Text;
            string _twoThousand = Dlist[0].Multiples;
            string _fiveHundred = Dlist[1].Multiples;
            string _twoHundred = Dlist[2].Multiples;
            string _hundred = Dlist[3].Multiples;
            string _fifty = Dlist[4].Multiples;
            string _twenty = Dlist[5].Multiples;
            string _ten = Dlist[6].Multiples;
            string _five = Dlist[7].Multiples;
            string _two = Dlist[8].Multiples;
            string _one = Dlist[9].Multiples;
            //using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand();
            //    command.Connection = connection;
            //    command.CommandText = "insert into DenominationTable values('" + _regionName + "','" + _branchId + "','" + _date + "','" + _empId + "'," + _twoThousand + "," + _fiveHundred + "," + _twoHundred + "," + _hundred + "," + _fifty + "," + _twenty + "," + _ten + "," + _five + "," + _two + "," + _one + "," + initialAmt + ",'" + CenterBlock.Text + "',0)";
            //    command.ExecuteNonQuery();
            //}

            DenominationView denomination = new DenominationView();
            denomination.RegionName = _regionName;
            denomination.BId = _branchId;
            denomination.EmpId = _empId;
            denomination.CollectionDate = DateTime.Today;
            denomination.TwoThousand = int.Parse(_twoThousand);
            denomination.FiveHundred =int.Parse(_fiveHundred);
            denomination.TwoHundred = int.Parse(_twoHundred);
            denomination.Hundred = int.Parse(_hundred);
            denomination.Fifty = int.Parse(_fifty);
            denomination.Twenty = int.Parse(_twenty);
            denomination.Ten = int.Parse(_ten);
            denomination.Five = int.Parse(_five);
            denomination.Two = int.Parse(_two);
            denomination.One = int.Parse(_one);
            denomination.SHGName = CenterName;
            denomination.TotalCollection = initialAmt;
            return denomination;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



        public void AddCollectionDetails()
        {

        }

        async Task SubmitCollection()
        {
            List<CollectionEntryView> collectiondetaillist = new List<CollectionEntryView>();
            foreach (var item in CollectionEntryDetails)
            {
                collectiondetaillist.Add(new CollectionEntryView
                {
                    SecurityDeposite = item.SecurityDeposite,
                    ActualDue = item.ActualDue,
                    ActualPaymentDate = item.ActualPaymentDate,
                    CollectedBy = item.CollectedBy,
                    CollectedOn =  DateTime.Now.ToLocalTime(),
                    Attendance = item.Attendance,
                    Balance = item.Balance,
                    BranchId = item.BranchId,
                    CustId = item.CustId,
                    CustomerName = item.CustomerName,
                    Extras = item.Extras,
                    Principal = item.Principal,
                    Interest = item.Interest,
                    LoanId = item.LoanId,
                    PaidDue = item.PaidDue,
                    Total = item.Total
                });
            }
            var json = JsonConvert.SerializeObject(collectiondetaillist);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/CollectionEntry";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);
            

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();

                await SubmitDenamination(InsertDenomination());

            }
        }
        async Task SubmitDenamination(DenominationView denomination)
        {
            DenominationView Denomination = denomination;
            var jsondata = JsonConvert.SerializeObject(Denomination);
            var stringContent = new StringContent(jsondata, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/AddDenomination";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);


            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();


            }
        }
    }
}
