using MicroFinance.Modal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionEntry.xaml
    /// </summary>
    public partial class CollectionEntry : Page
    {
        List<GroupTotal> TotalofEachGroup = new List<GroupTotal>();
        List<String> ActiveCustomersId = new List<string>();
        List<DailyCollection> LoanDetailsofCustomers = new List<DailyCollection>();
        List<string> GroupNames = new List<string>();
        List<DailyCollection> DailyCollectionsDetails = new List<DailyCollection>();


        List<string> ActiveLoanList = new List<string>();
        List<CollectionDetails> LoanCollectionDetailList = new List<CollectionDetails>();
        public CollectionEntry(String BranchName,String SelfHelpGroupName,String Date,String Day)
        {
            InitializeComponent();
            BranchNameBlock.Text = BranchName;
            CenterBlck.Text = SelfHelpGroupName;
            DateBlck.Text = Date;
            DayBlck.Text = Day;

            GroupNameUnderShg(SelfHelpGroupName);

            foreach(string item in GroupNames)
            {
                GetActiveLoanIdForGroupId(item);
                foreach (string loanId in ActiveLoanList)
                {
                    LoanCollectionDetailList.Add(LoadLoanDetails(loanId));
                }
                ActiveLoanList.Clear();
            }
            FILL_GroupData();

            
            CollectionList.ItemsSource = LoanCollectionDetailList;
            

            //GetLoanDetails();
            //CollectionDetails();
            //CollectionList.ItemsSource = DailyCollectionsDetails;
            //GetTotalEachGroup();
            //if(DailyCollectionsDetails.Count>0)
            //{
            //    AddDenomination.IsEnabled = true;
            //}
        }
        void FILL_GroupData()
        {
            ///Get GroupName and Total
            List<GroupTotal> ALLGroupsAndAmount = new List<GroupTotal>();
            foreach (string item in GroupNames)
            {
                var sumForGroup = LoanCollectionDetailList.Where(o => o.CustGroupId == item).Select(o => o.Total).Sum();
                GroupTotal oobj = new GroupTotal();
                oobj.GName = item;
                oobj.Amount = (int)sumForGroup;
                ALLGroupsAndAmount.Add(oobj);
            }
            
            int TotalAmountToCollect = 0;
            foreach (GroupTotal item in ALLGroupsAndAmount)
            {
                TotalAmountToCollect += item.Amount;
            }
            OverAllCollectionList.ItemsSource = ALLGroupsAndAmount;
            TotalAmountAll.Text = TotalAmountToCollect.ToString();
        }
        List<CollectionDetails> GetSingleGroupRecord(string groupID)
        {
            List<CollectionDetails> SingleGroupCollectionList = new List<CollectionDetails>();
            var ooo = LoanCollectionDetailList.Where(o => o.CustGroupId == groupID).Select(o => o);
            foreach (CollectionDetails ii in ooo)
            {
                SingleGroupCollectionList.Add(ii);
            }
            return SingleGroupCollectionList;
        }
        void GetGroupsUnderEmployee(string groupName)
        {
            GroupTotal obj = new GroupTotal();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select CustId from CustomerGroup where PeerGroupId = '" + groupName + "'";
                SqlDataReader reader2 = command.ExecuteReader();
                List<string> CustomerIDlist = new List<string>();
                while (reader2.Read())
                {
                    CustomerIDlist.Add(reader2.GetString(0));
                }

                int TotalCollectionAmountForGroup = 0;
                foreach (string custId in CustomerIDlist)
                {
                    TotalCollectionAmountForGroup += LoanCollectionDetailList.Where(m => m.CustID == custId).Select(o => o.Total).FirstOrDefault();
                }
                obj.Amount = TotalCollectionAmountForGroup;
            }
        }
        CollectionDetails LoadLoanDetails(string loanID)
        {
            CollectionDetails obj = new CollectionDetails();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select CustomerID, LoanType, LoanAmount,ApproveDate from LoanDetails where LoanID = '"+loanID+"'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obj.LoanId = loanID;
                    obj.CustID = reader.GetString(0);
                    obj.LoanType = reader.GetString(1);
                    obj.LoanAmount = reader.GetInt32(2);
                    obj.ApprovedDate = reader.GetDateTime(3);
                }
                reader.Close();
            }
            return obj;
        }

        void GetActiveLoanIdForGroupId(string groupId)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select LoanDetails.LoanID from LoanDetails join CustomerGroup on CustomerGroup.CustId = LoanDetails.CustomerID where CustomerGroup.PeerGroupId = '"+groupId+"' and LoanDetails.IsActive = 1";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    ActiveLoanList.Add(dataReader.GetString(0));
                }
                dataReader.Close();
            }
        }


        void GroupNameUnderShg(String SHGName)
        {
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select GroupId from PeerGroup join SelfHelpGroup on SelfHelpGroup.SHGId = PeerGroup.SHGid where SelfHelpGroup.SHGName = '"+SHGName+"'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    GroupNames.Add(dataReader.GetString(0));
                }
                dataReader.Close();
            }
        }
        void GetActiveCustomers(String SelfHelpGroupName)
        {
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection; string BranchId = MainWindow.LoginDesignation.BranchId;
                command.CommandText = "SELECT distinct CustomerDetails.CustId FROM CustomerDetails join CustomerGroup on CustomerDetails.CustId=CustomerGroup.CustId WHERE CustomerGroup.BranchId = '"+BranchId+"' and CustomerGroup.SelfHelpGroup = '"+SelfHelpGroupName+"' and IsActive='True'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    ActiveCustomersId.Add(dataReader.GetString(0));
                }
                dataReader.Close();
                connection.Close();
            }
        }
        void GetLoanDetails()
        {
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                foreach(String Customers in ActiveCustomersId)
                {
                    command.CommandText = "select LoanID, CustID, DurationInWeeks, LoanType, LoanAmount  from LoanDisposement where CustID = '" + Customers + "' and Active='True'";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        LoanDetailsofCustomers.Add(new DailyCollection { LoanId = dataReader.GetString(0), CustId = dataReader.GetString(1), Amount = dataReader.GetInt32(4), LoanType = dataReader.GetString(3), LoanDuration = dataReader.GetInt32(2) });
                    }
                    dataReader.Close();
                }
            }
        }
        int PaymentDoneTillNow(string LoanId,SqlConnection connection)
        {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                
                    command.CommandText = "select sum(NoOfPaymentDone) from LoanCollection where LoanID='" + LoanId + "'";
                    SqlDataReader dataReader = command.ExecuteReader();
                    int _noOfPaymentDoneTill = 0;
                    while (dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0))
                        {
                            _noOfPaymentDoneTill = dataReader.GetInt32(0);
                        }
                    }
                dataReader.Close();
            return _noOfPaymentDoneTill;
        }
        void CollectionDetails()
        {
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                foreach(DailyCollection loan in LoanDetailsofCustomers)
                {
                    int _noOfPaymentDoneTill = PaymentDoneTillNow(loan.LoanId, connection);
                    _noOfPaymentDoneTill = _noOfPaymentDoneTill / 10;
                    command.CommandText = "select InterestRate from LoanInterestRate where WeekNo > " + _noOfPaymentDoneTill + " and WeekNo<="+_noOfPaymentDoneTill+10+" ";
                    int _loanInterest = (int)command.ExecuteScalar();
                    command.CommandText = "select CustomerDetails.Name,CustomerGroup.PeerGroup,CustomerGroup.SelfHelpGroup from CustomerDetails join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId where CustomerDetails.CustId = '" + loan.CustId + "' ";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        DailyCollection lm = new DailyCollection();
                        lm = loan;
                        lm.InterestRate =_loanInterest;
                        lm.SNo = DailyCollectionsDetails.Count + 1;
                        lm.GroupName = dataReader.GetString(1);
                        lm.Name = dataReader.GetString(0);
                        lm.Attendance = 1;
                        lm.Calculate();
                        lm.NOOfPayment = 1;
                        DailyCollectionsDetails.Add(lm);
                    }
                    dataReader.Close();
                }
            }
        }
        int OverAlltotal = 0;
        void GetTotalEachGroup()
        {
            TotalofEachGroup.Clear();
            OverAlltotal = 0;
            foreach(string groups in GroupNames)
            {
                int total = 0;
                foreach(DailyCollection collection in DailyCollectionsDetails)
                {
                    if(groups.Equals(collection.GroupName))
                    {
                        total += collection.Total;
                    }
                }
                OverAlltotal += total;
                TotalofEachGroup.Add(new GroupTotal { GName = groups, Amount = total });
            }
            OverAllCollectionList.ItemsSource = TotalofEachGroup;
            OverAllCollectionList.Items.Refresh();
            TotalAmountAll.Text = OverAlltotal.ToString();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetTotalEachGroup();
        }

        DenominationPage denomination=new DenominationPage();
        private void AddDenomination_Click(object sender, RoutedEventArgs e)
        {
            if(!denomination.AlreadyEntered)
            {
                denomination = new DenominationPage(Convert.ToInt32(TotalAmountAll.Text), BranchNameBlock.Text, CenterBlck.Text, Convert.ToDateTime(DateBlck.Text), SaveCollection);
            }
            NavigationService.GetNavigationService(this).Navigate(denomination);
        }

        private void SaveCollection_Click(object sender, RoutedEventArgs e)
        {
            InsertCollections();
        }
        void InsertCollections()
        {
            foreach(CollectionDetails item in LoanCollectionDetailList)
            {
                DateTime actualDueDate = new DateTime();
                int actualDueAmount = 0;
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
                {
                    connection.Open();
                    string BranchId = MainWindow.LoginDesignation.BranchId;
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select DueDate,Total from LoanCollectionMaster where WeekNo = ((select count(CustId) from LoanCollectionEntry where LoanId = '"+item.LoanId+"' and Attendance > 0) + 1) and LoanId = '"+item.LoanId+"'";
                    SqlDataReader reader3 = command.ExecuteReader();
                    while(reader3.Read())
                    {
                        actualDueDate = reader3.GetDateTime(0);
                        actualDueAmount = (int)reader3.GetInt32(1);
                    }
                    reader3.Close();
                    actualDueAmount += item.Security;
                    actualDueAmount -= item.Extras;
                    if(actualDueAmount == item.Total && actualDueAmount < 0)
                    {
                        item.Extras = actualDueAmount;
                    }
                    else
                    {
                        item.Extras = 0;
                    }
                    item.ActualDueAmount = actualDueAmount;
                }
                int balance = GetBalanceForLoanId(item.LoanId);
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection; string BranchId = MainWindow.LoginDesignation.BranchId;
                    command.CommandText = "insert into LoanCollectionEntry(BranchId, CustId, LoanId, Principal, Interest, Total, SecurityDeposite, ActualDue, PaidDue, Balance, ActualPaymentDate, CollectedOn, Attendance,Extras) values(@BranchId, @CustId, @LoanId, @Principal, @Interest, @Total, @SecurityDeposite, @ActualDue, @PaidDue, @Balance, @ActualPaymentDate, @CollectedOn, @Attendance, @Extras)";
                    command.Parameters.AddWithValue("@BranchId", BranchId);
                    command.Parameters.AddWithValue("@CustId", item.CustID);
                    command.Parameters.AddWithValue("@LoanId", item.LoanId);
                    command.Parameters.AddWithValue("@Principal", item.Principal);
                    command.Parameters.AddWithValue("@Interest", item.Interest);
                    command.Parameters.AddWithValue("@Total", item.ActualDueAmount);
                    

                    command.Parameters.AddWithValue("@SecurityDeposite", item.Security);
                    command.Parameters.AddWithValue("@ActualDue", item.ActualDueAmount);

                    command.Parameters.AddWithValue("@PaidDue", item.Total);
                    if((item.Total - item.ActualDueAmount) != 0)
                    {
                        item.Extras = item.Total - item.ActualDueAmount;
                    }
                    command.Parameters.AddWithValue("@Balance", (balance - item.Total));
                    if (balance - item.Total <= 0)
                    {
                        DeactivateLoan(item.LoanId);
                    }

                    command.Parameters.AddWithValue("@ActualPaymentDate", actualDueDate);

                    command.Parameters.AddWithValue("@CollectedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@Attendance", item.Attendance);
                    
                    
                    command.Parameters.AddWithValue("@Extras", item.Extras);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            NavigationService.GetNavigationService(this).Navigate(new CollectionStartPage());
            denomination.InsertDenomination();

        }
        void DeactivateLoan(string loanId)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "update LoanDetails set IsActive = 0 where LoanID = '"+loanId+"'";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        int GetBalanceForLoanId(string loanID)
        {
            int balance = 0;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select min(Balance) from LoanCollectionEntry where LoanId = '"+ loanID + "'";
                var ball = command.ExecuteScalar();
                if(DBNull.Value.Equals(ball))
                {
                    command.CommandText = "select LoanAmount from LoanDetails where LoanID = '"+loanID+"'";
                    balance = (int)command.ExecuteScalar();
                }
                else
                {
                    balance = (int)ball;
                }
            }
            return balance;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void xCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardFieldOfficer());
        }

        private void xPrincipal_TextChanged(object sender, TextChangedEventArgs e)
        {
            FILL_GroupData();
        }

        private void xInterest_TextChanged(object sender, TextChangedEventArgs e)
        {
            FILL_GroupData();
        }


        private void xTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            FILL_GroupData();
        }
    }



    public class DailyCollection: BindableBase
    {
        public string CustId { get; set; }
        public string LoanId { get; set; }
        public string LoanType { get; set; }
        private int _amount;
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }
        public int LoanDuration { get; set; }
        private int _interestRate;
        public int InterestRate
        {
            get
            {
                return _interestRate;
            }
            set
            {
                _interestRate = value;
            }
        }
        private int _defaultPrincipal;
        private int _defaultInterestAmount;
        public void Calculate()
        {
            Principal = Amount / LoanDuration;
            Interest = (Amount * InterestRate / 100) / 10;
            _defaultInterestAmount = Interest;
            _defaultPrincipal = Principal;
        }
        public int SNo { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }

        private int _principal;
        public int Principal
        {
            get
            {
                return _principal;
            }
            set
            {
                _principal = value;
                RaisedPropertyChanged("Principal");
                Total = Principal + Interest + Security;
            }
        }
        private int _interest;
        public int Interest
        {
            get
            {
                return _interest;
            }
            set
            {
                _interest = value;
                RaisedPropertyChanged("Interest");
                Total = Principal + Interest + Security;
            }
        }
        private int _security = 30;
        public int Security
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
                RaisedPropertyChanged("Security");
                Total = Principal + Interest + Security;
            }
        }
        private int _numberOfPayment;
        public int NOOfPayment
        {
            get
            {
                return _numberOfPayment;
            }
            set
            {
                _numberOfPayment = value;
                RaisedPropertyChanged("NOOfPayment");
                Principal = _defaultPrincipal * _numberOfPayment;
                Interest = _defaultInterestAmount * _numberOfPayment;
            }
        }
        private int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                RaisedPropertyChanged("Total");
            }
        }
        private int _attendance;
        public int Attendance
        {
            get
            {
                return _attendance;
            }
            set
            {
                _attendance = value;
                if(_attendance==0)
                {
                    NOOfPayment = 0;
                    Security = 0;
                }
            }
        }
    }
    public class GroupTotal:BindableBase
    {
        private string _gName;
        public string GName
        {
            get
            {
                return _gName;
            }
            set
            {
                _gName = value;
            }
        }
        private int _amount;
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                //RaisedPropertyChanged("Amount");
            }
        }

        void GetgroupTotal()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select BranchName from BranchDetails where Bid = '" + MainWindow.LoginDesignation.BranchId + "'";
                //_BranchName = command.ExecuteScalar().ToString();
            }
        }
    }
}
