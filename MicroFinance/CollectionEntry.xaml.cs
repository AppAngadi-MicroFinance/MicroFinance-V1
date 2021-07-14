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


        public CollectionEntry(String BranchName,String SelfHelpGroupName,String Date,String Day)
        {
            InitializeComponent();
            BranchNameBlock.Text = BranchName;
            CenterBlck.Text = SelfHelpGroupName;
            DateBlck.Text = Date;
            DayBlck.Text = Day;
            GroupNameUnderShg(SelfHelpGroupName);
            GetActiveCustomers(SelfHelpGroupName);
            GetLoanDetails();
            CollectionDetails();
            CollectionList.ItemsSource = DailyCollectionsDetails;
            GetTotalEachGroup();
        }


        void GroupNameUnderShg(String SHGName)
        {
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                string BranchId = MainWindow.LoginDesignation.BranchId;
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select PeerGroupName from PeerGroup join SelfHelpGroup on PeerGroup.PGId = SelfHelpGroup.PGId where SHGName = '" + SHGName + "' and BId='"+BranchId+"'";
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
                denomination = new DenominationPage(10000, BranchNameBlock.Text, CenterBlck.Text, Convert.ToDateTime(DateBlck.Text), SaveCollection);
            }
            NavigationService.GetNavigationService(this).Navigate(denomination);
        }

        private void SaveCollection_Click(object sender, RoutedEventArgs e)
        {
            InsertCollections();
        }
        void InsertCollections()
        {
            foreach (DailyCollection loan in DailyCollectionsDetails)
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection; 
                    command.CommandText = "select sum(Principal) from LoanCollection where LoanId='" + loan.LoanId + "'";
                    int _remainingDue = 0;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        if (!dataReader.IsDBNull(0))
                        {
                            _remainingDue = dataReader.GetInt32(0);
                        }
                    }
                    dataReader.Close();
                    _remainingDue = loan.Amount - _remainingDue-loan.Principal;
                    string _todayDate = Convert.ToDateTime(DateBlck.Text).ToString("yyyy-MM-dd");
                    command.CommandText = "insert into LoanCollection values('" + loan.CustId + "','" + loan.LoanId + "'," + loan.Principal + "," + loan.Interest + "," + loan.Security + "," + loan.Total + "," + loan.Attendance + ",'" +_todayDate+ "',"+loan.NOOfPayment+","+_remainingDue+")";
                    command.ExecuteNonQuery();
                    if(_remainingDue<=0)
                    {
                        command.CommandText = "update LoanDisposement set Active = 'False', EndDate = '" + _todayDate + "'  where LoanID = '" + loan.LoanId + "'";
                        command.ExecuteNonQuery();
                    }
                    command.CommandText = "select COUNT(LoanID) from LoanDisposement where CustID='" + loan.CustId + "' and Active='True'";
                    int _activeLoans = (int)command.ExecuteScalar();
                    if(_activeLoans==0)
                    {
                        command.CommandText = "UPDATE CustomerDetails SET IsActive='FALSE' WHERE CustId='" + loan.CustId + "'";
                        command.ExecuteNonQuery();
                    }
                    denomination.InsertDenomination();
                }
            }
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
                RaisedPropertyChanged("Amount");
            }
        }
    }
}
