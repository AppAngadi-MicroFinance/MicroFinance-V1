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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanRequest.xaml
    /// </summary>
    public partial class LoanRequest : Page
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public string EmployeeId = MainWindow.LoginDesignation.EmpId;
        public string EmployeeDesignation = MainWindow.LoginDesignation.LoginDesignation;

        Branch_Shg_PgDetails GetDetails = new Branch_Shg_PgDetails();
        List<string> SelfHelpGroups = new List<string>();
        List<string> PeerGroups = new List<string>();
        public LoanRequest()
        {
            InitializeComponent();
            GetDetails.EmpId = EmployeeId;
            GetDetails.EmpDesignation = EmployeeDesignation;
            GetBranchDetailsofFieldOfficer();
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
            if (SelectPg.SelectedItem != null)
            {
                string _peerGroup = SelectPg.SelectedItem.ToString();
                List<string> CustomerIds = new List<string>();
                string _branchName = SelectBranch.SelectedItem.ToString();
                string _selfHelpGroup = SelectShg.SelectedItem.ToString();
                using (SqlConnection sql = new SqlConnection(ConnectionString))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select CustId from CustomerGroup where BranchName='" + _branchName + "' and SelfHelpGroup='" + _selfHelpGroup + "' and PeerGroup='" + _peerGroup + "'";
                    SqlDataReader sqlData = command.ExecuteReader();
                    while (sqlData.Read())
                    {
                        CustomerIds.Add(sqlData.GetString(0));
                    }
                    sqlData.Close();
                    string _customerName = "";
                    int _age = 0;
                    string _guarantorName = "";
                    string _nomineeName = "";
                    string _status = "";
                    int _pendingAmount = 0;
                    bool _isleader = false;
                    BitmapImage _profilePhoto;
                    foreach (string item in CustomerIds)
                    {
                        command.CommandText = "select CustomerDetails.Name,CustomerDetails.Age,CustomerDetails.ProfilePhoto,GuarenteeDetails.Name,NomineeDetails.Name,CustomerGroup.IsLeader from CustomerDetails join GuarenteeDetails on CustomerDetails.CustId = GuarenteeDetails.CustId join NomineeDetails on CustomerDetails.CustId = NomineeDetails.CustId join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId where CustomerDetails.CustId = '" + item + "'";
                        sqlData = command.ExecuteReader();
                        while (sqlData.Read())
                        {
                            _customerName = sqlData.GetString(0);
                            _age = sqlData.GetInt32(1);
                            _profilePhoto = ByteToBI((byte[])sqlData.GetValue(2));
                            _guarantorName = sqlData.GetString(3);
                            _nomineeName = sqlData.GetString(4);
                            _isleader = sqlData.GetBoolean(5);
                            MembersList.Add(new GroupMembers { CustomerName = _customerName, Age = _age, ProfilePhoto = _profilePhoto, GuarantorName = _guarantorName, NomineeName = _nomineeName, IsLeader = _isleader, PendingStatus = "Week 9", PendingAmount = 1200 });
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
        }
    }

    class GroupMembers
    {
        public string CustomerName { get; set; }
        public int Age { get; set; }
        public string GuarantorName { get; set; }
        public string NomineeName { get; set; }
        public int PendingAmount { get; set; }
        public string PendingStatus { get; set; }
        public BitmapImage ProfilePhoto { get; set; }
        public bool IsLeader { get; set; }
    }
}

