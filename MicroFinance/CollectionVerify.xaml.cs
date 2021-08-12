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
    /// Interaction logic for CollectionVerify.xaml
    /// </summary>
    public partial class CollectionVerify : Page
    {

        public CollectionVerify()
        {
            InitializeComponent();
            FillDetails();
        }
        void FillDetails()
        {
            RegionName.Text = MainWindow.LoginDesignation.RegionName;
            BranchName.Text = GetBranchName();
        }
        string GetBranchName()
        {
            string _BranchName = "";
            using (SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select BranchName from BranchDetails where Bid = '" + MainWindow.LoginDesignation.BranchId + "'";
                _BranchName = command.ExecuteScalar().ToString();
            }
            return _BranchName;
        }


        private void VerifyDenomination_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "update DenominationTable set IsVerified=1 from DenominationTable where BId=(select BId from BranchDetails where BranchName='" + BranchName.Text + "' and RegionName='" + RegionName.Text + "') and CollectionDate='" + Convert.ToDateTime(DatePic.Text).ToString("yyyy-MM-dd") + "' and IsVerified=0 and SHGName='" + ShgBox.Text + "' ";
                command.ExecuteNonQuery();
            }
            Dlist = new List<DenominationModel>();
            DenominationList.ItemsSource = Dlist;
            DenominationList.Items.Refresh();
            //FieldOfficerIdAndName = new Dictionary<string, string>();
            //SHGNameList = new List<string>();

            //FieldOfficerBox.ItemsSource = FieldOfficerIdAndName;
            //ShgBox.ItemsSource = SHGNameList;
            //FieldOfficerBox.Items.Refresh();
            //ShgBox.Items.Refresh();

            DateChanges();
            DenominationGrid.Visibility = Visibility.Hidden;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DenominationList.Items.Clear();
            DenominationGrid.Visibility = Visibility.Hidden;

        }

        Dictionary<string, string> FieldOfficerIdAndName = new Dictionary<string, string>();
        List<string> FOName = new List<string>();
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateChanges();
        }

        void DateChanges()
        {
            DayText.Text = Convert.ToDateTime(DatePic.SelectedDate).DayOfWeek.ToString();
            GetFieldOfficers();
            FieldOfficerBox.ItemsSource = FieldOfficerIdAndName.Values;
            FieldOfficerBox.Items.Refresh();
            FieldOfficerBox.IsEnabled = true;
            DenominationGrid.Visibility = Visibility.Hidden;
        }

        void GetFieldOfficers()
        {
            FieldOfficerIdAndName.Clear();
            Dictionary<string, string> ShgAndBranchId = new Dictionary<string, string>();
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select SHGName,BId from DenominationTable where BId=(select BId from BranchDetails where BranchName='" + BranchName.Text + "' and RegionName='" + RegionName.Text + "') and CollectionDate='" + Convert.ToDateTime( DatePic.SelectedDate).ToString("yyyy-MM-dd") + "' and IsVerified=0";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    ShgAndBranchId.Add(dataReader.GetString(1), dataReader.GetString(0));
                }
                dataReader.Close();
                foreach(var item in ShgAndBranchId)
                {
                    command.CommandText = "select EmpId, Name from Employee where EmpId = (select distinct EmpId from SelfHelpGroup where SHGName = '" + item.Value + "' and Bid = '" + item.Key + "')";
                    dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        string _Temp = dataReader.GetString(1);
                        FieldOfficerIdAndName.Add(dataReader.GetString(0), _Temp);
                    }
                    dataReader.Close();
                }
            }
        }
        List<string> SHGNameList = new List<string>();
        private void FieldOfficerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SHGNameList.Clear();
            string _empId = "";
             foreach(var item in FieldOfficerIdAndName)
            {
                if(item.Value==FieldOfficerBox.SelectedItem)
                {
                    _empId = item.Key;
                }
            }
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select SHGName from DenominationTable where BId=(select BId from BranchDetails where BranchName='" + BranchName.Text + "' and RegionName='" + RegionName.Text + "') and CollectionDate='" + Convert.ToDateTime(DatePic.Text).ToString("yyyy-MM-dd") + "' and IsVerified=0 and EmpId='" + _empId + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    SHGNameList.Add(dataReader.GetString(0));
                }
                dataReader.Close();
            }
            ShgBox.ItemsSource = SHGNameList;
            ShgBox.Items.Refresh();
            ShgBox.IsEnabled = true;
            DenominationGrid.Visibility = Visibility.Hidden;
        }
        List<DenominationModel> Dlist = new List<DenominationModel>();
        void AddBasic()
        {
            Dlist.Add(new DenominationModel(2000, "0"));
            Dlist.Add(new DenominationModel(500, "0"));
            Dlist.Add(new DenominationModel(200, "0"));
            Dlist.Add(new DenominationModel(100, "0"));
            Dlist.Add(new DenominationModel(50, "0"));
            Dlist.Add(new DenominationModel(20, "0"));
            Dlist.Add(new DenominationModel(10, "0"));
            Dlist.Add(new DenominationModel(5, "0"));
            Dlist.Add(new DenominationModel(2, "0"));
            Dlist.Add(new DenominationModel(1, "0"));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBasic();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select TwoThousand,FiveHundred,TwoHundred,Hundred,Fifty,Twenty,Ten,Five,Two,One,TotalCollectin from DenominationTable where BId=(select BId from BranchDetails where BranchName='" + BranchName.Text + "' and RegionName='" + RegionName.Text + "') and CollectionDate='" + Convert.ToDateTime(DatePic.Text).ToString("yyyy-MM-dd") + "' and IsVerified=0 and SHGName='" + ShgBox.Text + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    Dlist[0].Multiples = dataReader.GetInt32(0).ToString();
                    Dlist[1].Multiples = dataReader.GetInt32(1).ToString();
                    Dlist[2].Multiples = dataReader.GetInt32(2).ToString();
                    Dlist[3].Multiples = dataReader.GetInt32(3).ToString();
                    Dlist[4].Multiples = dataReader.GetInt32(4).ToString();
                    Dlist[5].Multiples = dataReader.GetInt32(5).ToString();
                    Dlist[6].Multiples = dataReader.GetInt32(6).ToString();
                    Dlist[7].Multiples = dataReader.GetInt32(7).ToString();
                    Dlist[8].Multiples = dataReader.GetInt32(8).ToString();
                    Dlist[9].Multiples = dataReader.GetInt32(9).ToString();
                    TotalBox.Text = dataReader.GetInt32(10).ToString();
                }
                dataReader.Close();
            }
            DenominationList.ItemsSource = Dlist;
            DenominationList.Items.Refresh();
            DenominationGrid.Visibility = Visibility.Visible;

        }

        private void CncBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
