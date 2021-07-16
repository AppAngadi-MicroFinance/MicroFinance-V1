using System;
using System.Collections.Generic;
using System.Data;
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
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddNewSelfHelpGroup.xaml
    /// </summary>
    public partial class AddNewSelfHelpGroup : Page
    {
        string BranchId = "01202106002";
        string SHGname = string.Empty;
        string Day = string.Empty;
        string Time = string.Empty;
        string OfficerId = string.Empty;
        string ConnectionString = "Data Source=.;Initial Catalog=MicroFinance;Integrated Security=True";
        public AddNewSelfHelpGroup()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            List<string> DaysOfWeeks = new List<string>();
            DaysOfWeeks.Add("MONDAY");
            DaysOfWeeks.Add("TUESDAY");
            DaysOfWeeks.Add("WEDNESSDAY");
            DaysOfWeeks.Add("THURSDAY");
            DaysOfWeeks.Add("FRIDAY");
            DaysOfWeeks.Add("SATURDAY");

            xSHGname.Text = "Periyakulam";
            xDayOfWeek.SelectedIndex = 2;
            xTimeBox.Text = "8:00";
            xOfficerSelect.SelectedIndex = 1;

            xDayOfWeek.ItemsSource = DaysOfWeeks;
            xOfficerSelect.ItemsSource = GetFieldOfficers(BranchId);
        }

        private void xCreateNewSHG_Click(object sender, RoutedEventArgs e)
        {
            SHGname = xSHGname.Text;
            Day = xDayOfWeek.SelectedItem.ToString();
            Time = xTimeBox.Text;
            OfficerModal officer = xOfficerSelect.SelectedItem as OfficerModal;
            if(InsertIntoSHG(SHGname, Day, Time, officer.FoID))
            {
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
        }

        List<OfficerModal> GetFieldOfficers(string branchId)
        {
            List<OfficerModal> FieldOfficerNames = new List<OfficerModal>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select Name,EmpId from Employee where Designation = 'Field Officer' and Bid = '" + branchId + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FieldOfficerNames.Add(new OfficerModal { Name = reader.GetString(0), FoID = reader.GetString(1) });
                }
                con.Close();
            }
            return FieldOfficerNames;
        }


        bool InsertIntoSHG(string name, string day, string time, string officer)
        {
            string Query = "insert Into SelfHelpGroup2(BranchId,SHGId, SHGName, CollectionDay, CollectionTime, Foid) values('" + BranchId + "', '"+ GenerateSHGID()+ "', '" + name + "', '" + day + "','" + time + "','" + officer + "')";
            List<OfficerModal> FieldOfficerNames = new List<OfficerModal>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = Query;
                if(cmd.ExecuteNonQuery() == 1)
                {
                    return true;                    
                }
                else
                {
                    return false;
                }
            }
        }



        string BranchName = MainWindow.LoginDesignation.BranchId;
        string Region = MainWindow.LoginDesignation.RegionName;
        string Designation = string.Empty;

        public string GetRegionNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from Region where RegionName='" + Region + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }
        public string GetBranchNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from BranchDetails where Bid='" + BranchId + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }
        public string GenerateSHGID() // IDPattern 01002202106SHG-05 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountofShg+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "Select Count(SHGId) from SelfHelpGroup2";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "SHG-" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }

        public string DigitConvert(string digit, int place = 3)
        {
            StringBuilder sb = new StringBuilder();
            string number = digit;
            string Result = "";
            if (number.Length < place)
            {
                for (int i = 0; i < (place - (number.Length)); i++)
                {
                    sb.Append(0);
                }
                Result = sb.ToString() + number;
            }
            else
            {
                Result = number;
            }

            return Result;
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }
    }
}
