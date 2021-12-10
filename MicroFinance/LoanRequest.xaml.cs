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
using MicroFinance.Modal;
using System.Data;
using System.Data.SqlClient;
using MicroFinance.Repository;
using MicroFinance.ViewModel;
using MicroFinance.Utils;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanRequest.xaml
    /// </summary>
    public partial class LoanRequest : Page
    {
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public string EmployeeId = MainWindow.LoginDesignation.EmpId;
        public string EmployeeDesignation = MainWindow.LoginDesignation.LoginDesignation;

        Branch_Shg_PgDetails GetDetails = new Branch_Shg_PgDetails();
        LoanDetails loanRequest = new LoanDetails();
        List<int> Months = new List<int> { 25,50,100 };
        List<int> Amountlist = new List<int> { 10000, 15000, 20000, 30000 };
        List<string> SelfHelpGroups = new List<string>();
        List<PGView> PeerGroups = new List<PGView>();
        public List<string> PurposeList = new List<string>();
        public LoanRequest()
        {
            InitializeComponent();
            GetDetails.EmpId = EmployeeId;
            GetDetails.EmpDesignation = EmployeeDesignation;
            GetBranchDetailsofFieldOfficer();
            LoanAmountcombo.ItemsSource = Amountlist;
            TimePeriodcombo.ItemsSource = Months;
            LoanRequestPanel.DataContext = loanRequest;
            PurposeList = LoanRepository.GetAllPurposeNames();
            LoanPurposeCombo.ItemsSource = PurposeList;
        }
        void GetBranchDetailsofFieldOfficer()
        {
            string[] RegionName = new string[1];
            string[] BranchName = new string[1];
            string[] FieldOfficer = new string[1];
            RegionName[0] = GetDetails.GetRegionNameofEmployee();
            BranchName[0] = GetDetails.GetBranchNameofEmployee();
            FieldOfficer[0] = GetDetails.GetEmployeeName();
            SelfHelpGroups = GetDetails.GetSelfHelpGroup();
            SelectShg.IsEnabled = true;
            SelectShg.ItemsSource = SelfHelpGroups;
            SelectRegion.ItemsSource = RegionName; SelectRegion.SelectedIndex = 0;
            SelectBranch.ItemsSource = BranchName; SelectBranch.SelectedIndex = 0;
            SelectOfficer.ItemsSource = FieldOfficer; SelectOfficer.SelectedIndex = 0;
        }
        void ResetPeergroup()
        {
            PeerGroups.Clear();
            SelectPg.ItemsSource = PeerGroups; SelectPg.SelectedIndex = 0;
        }

        private void SelectShg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PeerGroups.Clear();
            string ShgName = SelectShg.SelectedItem.ToString();
            PeerGroups = GetDetails.GetPeerGroup(ShgName);
            SelectPg.ItemsSource = PeerGroups;
            SelectPg.IsEnabled = true;
            

        }
        public BitmapImage ByteToBI(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }


        List<GroupMembers> MembersList = new List<GroupMembers>();

        void GroupMembersDetails()
        {
            MembersList.Clear();
            if (SelectPg.SelectedItem != null)
            {
                PGView SelectedPg = SelectPg.SelectedItem as PGView;
                string _peerGroup = SelectedPg.GroupID;
                string _regionName = MainWindow.LoginDesignation.RegionName;
                List<string> CustomerIds = new List<string>();
                string _branchName = SelectBranch.SelectedItem.ToString();
                string _selfHelpGroup = SelectShg.SelectedItem.ToString();
                using (SqlConnection sql = new SqlConnection(ConnectionString))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select CustId from CustomerGroup where PeerGroupId='"+_peerGroup+"'";
                    SqlDataReader sqlData = command.ExecuteReader();
                    while (sqlData.Read())
                    {
                        if(!sqlData.IsDBNull(0))
                        {
                            CustomerIds.Add(sqlData.GetString(0));
                        }
                    }
                    sqlData.Close();
                    List<string> ActiveAndEligibleCustomerId = new List<string>();
                    foreach(string id in CustomerIds)
                    {
                        command.CommandText = "(select Count(CustomerID) from LoanDetails where IsActive='1' and CustomerID='"+id+"')";
                        int Count =(int) command.ExecuteScalar();
                        if(Count<2)
                        {
                            ActiveAndEligibleCustomerId.Add(id);
                        }
                    }
                    string _customerID="";
                    string _customerName = "";
                    int _age = 0;
                    string _guarantorName = "";
                    string _nomineeName = "";
                    //string _status = "";
                    //int _pendingAmount = 0;
                    bool _isleader = false;
                    BitmapImage _profilePhoto;
                    foreach (string item in ActiveAndEligibleCustomerId)
                    {
                        command.CommandText = "select a.Name,a.Age,a.ProfilePhoto,b.IsLeader,c.Name,d.Name from (select Name,Age,ProfilePhoto,CustId from  CustomerDetails where CustId='" + item + "') as a join (select isleader,CustId from CustomerGroup where CustId='" + item+ "') as b on a.CustId=b.CustId join (select Name,CustId from GuarenteeDetails where CustId='" + item + "') as c on a.CustId=c.CustId join (select Name,CustId from NomineeDetails where CustId='" + item + "') as d on a.CustId=d.CustId";
                        sqlData = command.ExecuteReader();
                        while (sqlData.Read())
                        {
                            _customerID = item;
                            _customerName = sqlData.GetString(0);
                            _age = sqlData.GetInt32(1);
                            _profilePhoto = null;
                            _isleader = sqlData.GetBoolean(3);
                            _guarantorName = sqlData.GetString(4);
                            _nomineeName = sqlData.GetString(5);
                            
                            MembersList.Add(new GroupMembers { CustomerID=_customerID, CustomerName = _customerName, Age = _age, ProfilePhoto = _profilePhoto, GuarantorName = _guarantorName, NomineeName = _nomineeName, IsLeader = _isleader, PendingStatus = "Week 9", PendingAmount = 1200 });
                        }
                        sqlData.Close();
                    }
                    //MembersList.Clear();
                    if (MembersList.Count > 0)
                    {
                        LoanRequestGrid.IsEnabled = true;
                    }
                }
            }
            else
            {
                MembersList.Clear();
                MembersListView.ItemsSource = MembersList;
                MembersListView.Items.Refresh();
                LoanRequestGrid.IsEnabled = false;
            }

        }

        private void SelectPg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupMembersDetails();
            MembersListView.ItemsSource = MembersList;
            MembersListView.Items.Refresh();
        }

        private void LoanRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            GroupMembers SelectedMemeber = MembersListView.SelectedValue as GroupMembers;
            if(LoanRepository.IsAlreadyInApplicationProcess(SelectedMemeber.CustomerID)==false)
            {
                if (MembersListView.SelectedItem != null && SelectedMemeber.IsRequested != true && LoanDetailsValid() == true)
                {
                    string BranchName = SelectBranch.SelectedValue as string;
                    string RegionName = SelectRegion.SelectedValue as string;
                    GroupMembers groupMembers = MembersListView.SelectedValue as GroupMembers;
                    groupMembers.IsRequested = true;
                    loanRequest.CustomerID = groupMembers.CustomerID;
                    loanRequest.EmployeeID = EmployeeId;
                    loanRequest.SendRequest(RegionName, BranchName);
                    MembersListView.Items.Refresh();
                    loanRequest = new LoanDetails();
                    LoanRequestPanel.DataContext = new LoanDetails();
                }
                else
                {
                    message = language.translate(SystemFunction.IsTamil, "W17");//1.Please Check Loan Details\n2.Select Customer
                    MessageBox.Show(message,"Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                    loanRequest = new LoanDetails();
                    LoanRequestPanel.DataContext = new LoanDetails();
                }
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "SW3");//This Customer Already in Application Process!...
                MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
            
            
        }

        public bool LoanDetailsValid()
        {
            if(LoanTypecombo.SelectedIndex!=-1)
            {
                return true;
            }
            return false;
        }

        private void MembersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }
    }

    class GroupMembers
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Age { get; set; }
        public string GuarantorName { get; set; }
        public string NomineeName { get; set; }
        public int PendingAmount { get; set; }
        public string PendingStatus { get; set; }
        public BitmapImage ProfilePhoto { get; set; }
        public bool IsLeader { get; set; }
        public bool IsRequested { get; set; }
    }
}

