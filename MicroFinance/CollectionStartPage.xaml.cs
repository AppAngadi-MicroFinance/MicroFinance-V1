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
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionStartPage.xaml
    /// </summary>
    public partial class CollectionStartPage : Page
    {
        ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        List<TimeTableViewModel> CenterList = new List<TimeTableViewModel>();
        ObservableCollection<CollectionEntryView> CollectionDetails = new ObservableCollection<CollectionEntryView>();
        ObservableCollection<TimeTableViewModel> BindingCenterList = new ObservableCollection<TimeTableViewModel>();
        public CollectionStartPage()
        {
            InitializeComponent();

            LoadData();
            EmployeeNameCombo.SelectedIndex = SelectedEmployee();
            CenterNameCombo.ItemsSource = BindingCenterList;
        }


        void LoadData()
        {
            LoadEmployee();
            EmployeeNameCombo.ItemsSource = Employees;
            SelectedEmployee();
            CenterList = MainWindow.BasicDetails.CenterList;
        }


        void LoadCenters(string EmpId,string CollectionDay)
        {
            BindingCenterList.Clear();
            foreach(TimeTableViewModel center in CenterList)
            {
                if(center.EmpId==EmpId&&center.CollectionDay==CollectionDay)
                {
                    BindingCenterList.Add(center);
                }
            }
        }


        void LoadEmployee()
        {
            foreach(EmployeeViewModel employee in MainWindow.BasicDetails.EmployeeList)
            {
                if(employee.BranchId==MainWindow.LoginDesignation.BranchId && employee.Designation=="Field Officer")
                {
                    Employees.Add(employee);
                }
            }
        }

        int SelectedEmployee()
        {
            int Count = 0;
            foreach (EmployeeViewModel employee in Employees)
            {
                if(employee.EmployeeId==MainWindow.LoginDesignation.EmpId)
                {
                    return Count; 
                }
                Count++;
            }
            return -1;
        }

        

        
        


        async Task LoadCollectionlist(string EmpId, string collectionday, string groupid)
        {
            
            string url1 = "http://examsign-001-site4.itempurl.com/api/Collectiondetails";
           // string url1 = "http://localhost:44357/api/Collectiondetails";
            var values1 = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("empid",EmpId),
                    new KeyValuePair<string, string>("collectionday",collectionday),
                    new KeyValuePair<string, string>("groupid",groupid),
                };
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, new FormUrlEncodedContent(values1));
            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<ObservableCollection<CollectionEntryView>>(result);

                CollectionDetails = status;

               
            }
            else
            {
                MessageBox.Show(response1.ReasonPhrase, "Warninng", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }

        private async void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            if(EmployeeNameCombo.SelectedIndex!=-1 && CollectionDayCombo.SelectedIndex!=-1&&CenterNameCombo.SelectedIndex!=-1)
            {
                ComboBoxItem SelectedItem = CollectionDayCombo.SelectedItem as ComboBoxItem;
                string CollectionDay = SelectedItem.Content.ToString();
                EmployeeViewModel SelectedEmployee = EmployeeNameCombo.SelectedItem as EmployeeViewModel;
                string EmpId = SelectedEmployee.EmployeeId;
                TimeTableViewModel SelectedSHG = CenterNameCombo.SelectedItem as TimeTableViewModel;
                string CenterID = SelectedSHG.SHGId;
                try
                {
                    GifPanel.Visibility = Visibility.Visible;
                    await LoadCollectionlist(EmpId, CollectionDay, CenterID);
                    GifPanel.Visibility = Visibility.Collapsed;
                    if(CollectionDetails.Count!=0)
                    {
                        this.NavigationService.Navigate(new CollectionEntryNew(CollectionDetails, SelectedSHG.SHGName));
                    }
                    else
                    {
                        MessageBox.Show("No Collection Available in this Center", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                   
                }
                catch
                {

                }

                

               
            }
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardFieldOfficer());
        }

        private void CollectionDayCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CollectionDayCombo.SelectedIndex!=-1)
            {
                ComboBoxItem SelectedItem = CollectionDayCombo.SelectedItem as ComboBoxItem;
                string CollectionDay = SelectedItem.Content.ToString();
                EmployeeViewModel SelectedEmployee = EmployeeNameCombo.SelectedItem as EmployeeViewModel;
                string EmpId = SelectedEmployee.EmployeeId;

                LoadCenters(EmpId, CollectionDay);
            }
        }
    }
}
