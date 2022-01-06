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
using MicroFinance.Modal;
using MicroFinance.ViewModel;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SDRecommendView.xaml
    /// </summary>
    public partial class SDRecommendView : Page
    {
        List<SavingsAccountRequestView> RequestDetailsList = new List<SavingsAccountRequestView>();
        public SDRecommendView()
        {
            InitializeComponent();
        }
        public SDRecommendView(List<SavingsAccountRequestView> RequestList)
        {
            InitializeComponent();
            RequestDetailsList = RequestList;
            RequestGrid.ItemsSource = RequestDetailsList;
        }

        private async void RecommedBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> IdList = RequestDetailsList.Select(temp => temp.RequestID).ToList();
            int CurrentCode = RequestDetailsList.Select(temp => temp.Code).FirstOrDefault();
            List<SavingAmountRequest_Log> LogDetails = FormlogDetails(CurrentCode + 1);
            try
            {
                await updateRequestDetails(IdList, CurrentCode + 1,LogDetails);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        List<SavingAmountRequest_Log> FormlogDetails(int Code)
        {
            List<SavingAmountRequest_Log> LogDetails = new List<SavingAmountRequest_Log>();
            foreach(SavingsAccountRequestView Request in RequestDetailsList)
            {
                SavingAmountRequest_Log Log = new SavingAmountRequest_Log();
                Log.RequestID = Request.RequestID;
                Log.EmployeeID = MainWindow.LoginDesignation.EmpId;
                Log.Code = Code;
                Log.TransactionDate = DateTime.Now.ToLocalTime();

                LogDetails.Add(Log);
            }
            return LogDetails;
        }

        async Task updateRequestDetails(List<string> IdList, int Code,List<SavingAmountRequest_Log> LogDetails)
        {
            UpdateRequestView Details = new UpdateRequestView { RequestIDList = IdList, StatusCode = Code,LogDetails=LogDetails };
            string url = "http://examsign-001-site4.itempurl.com/api/UpdateRequest/SA";
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response = new HttpResponseMessage();
            var json = JsonConvert.SerializeObject(Details);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            Response = await Client.PostAsync(url, stringContent);
            if(Response.IsSuccessStatusCode)
            {
                MessageBox.Show("Success");
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
            else
            {
                MessageBox.Show(Response.StatusCode.ToString());
            }
        }

        public class UpdateRequestView
        {
            public int StatusCode { get; set; }
            public List<string> RequestIDList { get; set; }
            public List<SavingAmountRequest_Log> LogDetails { get; set; }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardBranchManager());
        }
    }
}
