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
using MicroFinance.ViewModel;
using MicroFinance.Modal;
using System.Net.Http;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SavingsAmountWithdrawRequest.xaml
    /// </summary>
    public partial class SavingsAmountWithdrawRequest : Page
    {

        SavingsAccountView Dummy = new SavingsAccountView { CustomerName = "RAJAMALLI SIVA", CustId = "010012021112086", SavingAcId = "SA01001202112214", DateOfCreation = Convert.ToDateTime("2021-12-03T00:00:00"), IsActive = true, Debit = 0, Credit = 3300, Balance = 3300 };
        SavingsAccountView AccountDetails = new SavingsAccountView();
        public int RequestCount = 0;
        public SavingsAmountWithdrawRequest()
        {
            InitializeComponent();
        }
        public SavingsAmountWithdrawRequest(SavingsAccountView Accountdetails)
        {
            InitializeComponent();
            AccountDetails = Accountdetails;
            LoadData();
        }

        void LoadData()
        {
            CustomerNameBox.Text = AccountDetails.CustomerName;
            AccountNumberBox.Text = AccountDetails.SavingAcId;
            BalanceBox.Text = AccountDetails.Balance.ToString();
        }


        private async void RequestBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(WithdrawnAmountBox.Text)==false)
            {
                int RequiredAmount = Convert.ToInt32(WithdrawnAmountBox.Text);
                if(CheckAmount(AccountDetails.Balance,RequiredAmount))
                {
                    await GetRequestCount();
                    string BranchID = MainWindow.LoginDesignation.BranchId;
                    string EmpID = MainWindow.LoginDesignation.EmpId;
                    string RequestID = GenerateRequestID(BranchID, RequestCount);
                    SavingsAmountRequest RequestDetails = new SavingsAmountRequest {RequestID=RequestID,AccountNumber=AccountDetails.SavingAcId,BranchID=BranchID,RequestedBy=EmpID,RequestDate=DateTime.Now.ToLocalTime(),Code=1,CustomerID=AccountDetails.CustId,RequestAmount=RequiredAmount};
                    SavingAmountRequest_Log LogDetails = new SavingAmountRequest_Log { RequestID = RequestID, Code = 1, EmployeeID = EmpID, TransactionDate = DateTime.Now.ToLocalTime() };

                    try
                    {
                        await SendWithDrawRequest(RequestDetails,LogDetails);
                        this.NavigationService.Navigate(new DashboardFieldOfficer());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Amount");
                }
            }
            else
            {
                MessageBox.Show("Enter Amount to withdrawal", "Warning");
            }
        }

        bool CheckAmount(int BalanceAmount,int RequiredAmount)
        {
            return (RequiredAmount <= BalanceAmount && RequiredAmount!=0) ? true : false;
            
        }
        async Task GetRequestCount()
        {
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetRequestCount";
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, null);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<int>(result);

                RequestCount = status;
                
            }
            else
            {
                string message = response1.StatusCode.ToString();
                MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        async Task SendWithDrawRequest(SavingsAmountRequest RequestDetails,SavingAmountRequest_Log LogDetails)
        {
            WithdrawnRequestView Requesview = new WithdrawnRequestView { RequestDetails = RequestDetails, LogDetails = LogDetails };
            string url = "http://examsign-001-site4.itempurl.com/api/WithdrawnRequest";
            HttpClient Client = new HttpClient();
            var json = JsonConvert.SerializeObject(Requesview);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage Response = new HttpResponseMessage();
            Response = await Client.PostAsync(url,stringContent);

            if(Response.IsSuccessStatusCode)
            {
                MessageBox.Show("Request Send SuccessFully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(Response.StatusCode.ToString());
            }
        }

        public class WithdrawnRequestView
        {
            public SavingsAmountRequest RequestDetails { get; set; }
            public SavingAmountRequest_Log LogDetails { get; set; }
        }




        string GenerateRequestID(string BranchId,int Count)
        {
            string RequestID = "";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            string RegionCode = BranchId.Substring(0, 2);
            string BranchCode = BranchId.Substring(8);
            RequestID = RegionCode + BranchCode + year + ((month < 10) ? "0" + month.ToString() : month.ToString())+"SDR"+ ((Count < 100) ? "00" + (Count+1).ToString() : (Count+1).ToString());
            return RequestID;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardFieldOfficer());
        }
    }
}
