using MicroFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CollectionEntryNew.xaml
    /// </summary>
    public partial class CollectionEntryNew : Page
    {
        string _centername = "";
        ObservableCollection<CollectionEntryView> CollectionEntries = new ObservableCollection<CollectionEntryView>();
        ObservableCollection<CollectionEntryBindingModal> BindingModalList = new ObservableCollection<CollectionEntryBindingModal>();
        public CollectionEntryNew()
        {
            InitializeComponent();
        }
        public CollectionEntryNew(ObservableCollection<CollectionEntryView> collectionEntries,string CenterName)
        {
            InitializeComponent();
            _centername = CenterName;
            CenterNameText.Text = _centername;
            CollectionEntries = collectionEntries;
            LoadData();
            CollectionGrid.ItemsSource = BindingModalList ;
            TotalAmountText.Text = BindingModalList.Select(temp => temp.Total).Sum().ToString();
        }

        void LoadData()
        {
            int SNo = 0;
            foreach(CollectionEntryView Collection in CollectionEntries)
            {
                CollectionEntryBindingModal Coll = new CollectionEntryBindingModal();
                Coll.SNO = ++SNo;
                Coll.BranchId = Collection.BranchId;
                Coll.CustId = Collection.CustId;
                Coll.CustomerName = Collection.CustomerName;
                Coll.LoanId = Collection.LoanId;
                Coll.Principal = Collection.Principal;
                Coll.Interest = Collection.Interest;
                Coll.Total = Collection.Total;
                Coll.SecurityDeposite = Collection.SecurityDeposite;
                Coll.ActualDue = Collection.SecurityDeposite;
                Coll.PaidDue = Collection.PaidDue;
                Coll.Balance = Collection.Balance;
                Coll.ActualPaymentDate = Collection.ActualPaymentDate;
                Coll.CollectedOn = Collection.CollectedOn;
                Coll.Attendance = Collection.Attendance;
                Coll.Extras = Collection.Extras;
                Coll.CollectedBy = Collection.CollectedBy;

                BindingModalList.Add(Coll);
            }
        }

        private void AddDenominationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DenominationPage(FormCollectionData(),_centername));
        }


        ObservableCollection<CollectionEntryView> FormCollectionData()
        {
            ObservableCollection<CollectionEntryView> CollectionList = new ObservableCollection<CollectionEntryView>();

            foreach (CollectionEntryBindingModal Collection in BindingModalList)
            {
                CollectionEntryView Coll = new CollectionEntryView();
              
                Coll.BranchId = Collection.BranchId;
                Coll.CustId = Collection.CustId;
                Coll.CustomerName = Collection.CustomerName;
                Coll.LoanId = Collection.LoanId;
                Coll.Principal = Collection.Principal;
                Coll.Interest = Collection.Interest;
                Coll.Total = Collection.Total;
                Coll.SecurityDeposite = Collection.SecurityDeposite;
                Coll.ActualDue = Collection.SecurityDeposite;
                Coll.PaidDue = Collection.PaidDue;
                Coll.Balance = Collection.Balance;
                Coll.ActualPaymentDate = Collection.ActualPaymentDate;
                Coll.CollectedOn = Collection.CollectedOn;
                Coll.Attendance = Collection.Attendance;
                Coll.Extras = Collection.Extras;
                Coll.CollectedBy = Collection.CollectedBy;

                CollectionList.Add(Coll);
            }

            return CollectionList;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardFieldOfficer());
        }
    }
}
