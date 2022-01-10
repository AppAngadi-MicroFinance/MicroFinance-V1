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
using MicroFinance.Reports;
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
        public SDRecommendView(List<SavingsAccountRequestView> RequestList,int Code)
        {
            InitializeComponent();
            RequestDetailsList = RequestList;
            RequestGrid.ItemsSource = RequestDetailsList;
            if(Code==2)
            {
                RecommedBtn.Content = "Approve";
            }
            else if(Code==3)
            {
                RecommendPanel.Visibility = Visibility.Collapsed;
                NeftPanel.Visibility = Visibility.Visible;
            }
        }

        private async void RecommedBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> IdList = RequestDetailsList.Select(temp => temp.RequestID).ToList();
            int CurrentCode = RequestDetailsList.Select(temp => temp.Code).FirstOrDefault();
            List<SavingAmountRequest_Log> LogDetails = FormlogDetails(CurrentCode + 1);
            try
            {
                await updateRequestDetails(IdList, CurrentCode + 1,LogDetails);
                string Designation = MainWindow.LoginDesignation.LoginDesignation;
                Designation = (Designation == null) ? "" : Designation;
                LoadHomePage(Designation);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        List<SavingAmountRequest_Log> FormlogDetails(int Code)
        {
            List<SavingAmountRequest_Log> LogDetails = new List<SavingAmountRequest_Log>();
            string EmpId = (MainWindow.LoginDesignation.EmpId != null) ? MainWindow.LoginDesignation.EmpId : "Admin";
            foreach (SavingsAccountRequestView Request in RequestDetailsList)
            {
                SavingAmountRequest_Log Log = new SavingAmountRequest_Log();
                Log.RequestID = Request.RequestID;
                Log.EmployeeID = EmpId;
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
            string Designation = MainWindow.LoginDesignation.LoginDesignation;
            Designation = (Designation == null) ? "" : Designation;
            LoadHomePage(Designation);
        }

        private async void NeftBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> IdList = RequestDetailsList.Select(temp => temp.RequestID).ToList();
                int CurrentCode = RequestDetailsList.Select(temp => temp.Code).FirstOrDefault();
                List<SavingAmountRequest_Log> LogDetails = FormlogDetails(CurrentCode + 1);
                try
                {
                    await updateRequestDetails(IdList, CurrentCode + 1, LogDetails);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                List<NeftRequestView> NeftDetails = RequestDetailsList.Select(temp => new NeftRequestView { CustomerID = temp.CustomerID, Amount = temp.Amount }).ToList();
                await GetNeftDetails(NeftDetails);
                NEFT neft_Generator = new NEFT();
                neft_Generator.GenerateNEFT_File_SD(BankDetails);
                string Designation = MainWindow.LoginDesignation.LoginDesignation;
                Designation = (Designation == null) ? "" : Designation;
                LoadHomePage(Designation);
            }
            catch
            {

            }
           
            
        }


        List<CustomerBankDetailView> BankDetails = new List<CustomerBankDetailView>();
        async Task GetNeftDetails(List<NeftRequestView> Details)
        {
            NeftDetails neft = new NeftDetails { RequestDetails = Details };
            string url = "http://examsign-001-site4.itempurl.com/api/NeftRequest";
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response = new HttpResponseMessage();
            var json = JsonConvert.SerializeObject(neft);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            Response = await Client.PostAsync(url, stringContent);

            if(Response.IsSuccessStatusCode)
            {
                var result = await Response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CustomerBankDetailView>>(result);
                if(result!=null)
                {
                    BankDetails = status;
                }
            }
            else
            {
                MessageBox.Show(Response.RequestMessage.ToString());
            }

                                            
        }


        


        public class NeftDetails
        {
            public List<NeftRequestView> RequestDetails { get; set; }
        }

        public void LoadHomePage(string Designation)
        {
            if (Designation.Equals("Field Officer"))
                this.NavigationService.Navigate(new DashboardFieldOfficer());
            else if (Designation.Equals("Accountant"))
                this.NavigationService.Navigate(new DashboardAccountant());
            else if (Designation.Equals("Branch Manager") || Designation.Equals("Manager"))
                this.NavigationService.Navigate(new DashboardBranchManager());
            else if (Designation.Equals("Region Manager"))
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            else
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }
    }
}
