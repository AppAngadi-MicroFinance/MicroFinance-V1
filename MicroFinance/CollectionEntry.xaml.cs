using MicroFinance.Modal;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionEntry.xaml
    /// </summary>
    public partial class CollectionEntry : Page
    {
        List<GroupTotal> TotalofEachGroup = new List<GroupTotal>();
        List<string> GroupNames = new List<string> { "Group 1", "Group 2", "Group 3", "Group 4", "Group 5","Group 6" };
        List<DailyCollection> DailyCollectionsDetails = new List<DailyCollection>();
        public CollectionEntry()
        {
            InitializeComponent();
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count+1, GroupName = "Group 1",Name="Safdhar",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 1",Name="Safdhar",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 1",Name="Safdhar",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 1",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 1",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 2",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 2",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 2",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 2",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 2",Name="Ashraf",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 3",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 3",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 3",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 3",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 3",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 4",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 4",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 4",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 4",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 5",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 5",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 5",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 5",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            DailyCollectionsDetails.Add(new DailyCollection { SNo = DailyCollectionsDetails.Count + 1, GroupName = "Group 5",Name="Thalif",LoanType="General",Principal=600,Interest=80,Security=30,Attendance=1 });
            CollectionList.ItemsSource = DailyCollectionsDetails;
            GetTotalEachGroup();
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


        private void AddDenomination_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new DenominationPage(OverAlltotal));
        }
    }
    public class DailyCollection:BindableBase
    {
        public string GName { get; set; }
        public int Amount { get; set; }

        public int SNo { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string LoanType { get; set; }
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
        private int _security;
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
        public int Attendance { get; set; }

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
