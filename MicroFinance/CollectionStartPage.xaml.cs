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
    /// Interaction logic for CollectionStartPage.xaml
    /// </summary>
    public partial class CollectionStartPage : Page
    {
        public CollectionStartPage()
        {
            InitializeComponent();
            GetDetails();
        }
        void GetDetails()
        {
            string _officerBranchId = MainWindow.LoginDesignation.BranchId;
            string _officerEmpId = MainWindow.LoginDesignation.EmpId;
            string[] _branchName = new string[1];
            string[] _officerName = new string[1];
            string[] _regionName = new string[1];
            List<string> SelfHelpGroupList = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select BranchName,RegionName from BranchDetails where BranchDetails.Bid='" + _officerBranchId + "'";
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                while (sqlData.Read())
                {
                    _branchName[0] = sqlData.GetString(0);
                    _regionName[0] = sqlData.GetString(1);
                }
                sqlData.Close();
                sqlCommand.CommandText = "select Name from Employee where EmpId='" + _officerEmpId + "'";
                _officerName[0] = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.CommandText = "select distinct(SHGName) from SelfHelpGroup where EmpId='" + _officerEmpId + "'";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    SelfHelpGroupList.Add(sqlDataReader.GetString(0));
                }
                sqlDataReader.Close();
                sqlConnection.Close();
            }
            BranchBox.ItemsSource = _branchName; BranchBox.SelectedIndex = 0;
            FieldOfficerBox.ItemsSource = _officerName; FieldOfficerBox.SelectedIndex = 0;
            SHGBox.ItemsSource = SelfHelpGroupList;
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            string _branchName = BranchBox.Text;
            string _shgName = SHGBox.Text;
            DateTime _date =(DateTime)DateBox.SelectedDate;
            string _day = _date.DayOfWeek.ToString();
            
            NavigationService.GetNavigationService(this).Navigate(new CollectionEntry(_branchName,_shgName,_date.ToShortDateString(),_day));
        }
    }
}
