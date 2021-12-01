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
        string BranchId = MainWindow.LoginDesignation.BranchId;
        string SHGname = string.Empty;
        string Day = string.Empty;
        string Time = string.Empty;
        string OfficerId = string.Empty;
        string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public AddNewSelfHelpGroup()
        {
            InitializeComponent();
            xBranchName.Text = GetBranchName(BranchId);
            LoadData();
        }

        void LoadData()
        {
            List<string> DaysOfWeeks = new List<string>();
            DaysOfWeeks.Add("MONDAY");
            DaysOfWeeks.Add("TUESDAY");
            DaysOfWeeks.Add("WEDNESDAY");
            DaysOfWeeks.Add("THURSDAY");
            DaysOfWeeks.Add("FRIDAY");
            DaysOfWeeks.Add("SATURDAY");
            DaysOfWeeks.Add("SUNDAY");

            List<int> TimeHour = new List<int> { 6, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5,6 };
            xTimeHour.ItemsSource = TimeHour;

            List<int> TimeMinute = new List<int> {0,5,10,15,20,25,30,35,40,45,50,55 };
            xTimeMinute.ItemsSource = TimeMinute;

            xDayOfWeek.SelectedIndex = 2;
            xOfficerSelect.SelectedIndex = 1;

            xDayOfWeek.ItemsSource = DaysOfWeeks;
            xOfficerSelect.ItemsSource = GetFieldOfficers(BranchId);
        }

        private void xCreateNewSHG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SHGname = xSHGname.Text;
                Day = xDayOfWeek.SelectedItem.ToString();
                string Hour = xTimeHour.SelectedItem.ToString();
                string Minute = xTimeMinute.SelectedItem == null ? "00" : xTimeMinute.SelectedItem.ToString();
                Minute = Minute == string.Empty ? "00" : Minute;
                Time = Hour + ":" + Minute;
                OfficerModal officer = xOfficerSelect.SelectedItem as OfficerModal;

                string Taluk = xSHGTaluk.Text;
                string District = xSHGRegion.Text;
                if (InsertIntoSHG(SHGname, Taluk, District, Day, Time, officer.FoID))
                {
                    this.NavigationService.Navigate(new DashboardBranchManager());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something you did wrong. Check all fields..!");
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
                cmd.CommandText = "select Employee.EmpId, Employee.Name from Employee join EmployeeBranch on Employee.EmpId = EmployeeBranch.Empid where EmployeeBranch.BranchId = '" + branchId + "' and EmployeeBranch.Designation = 'Field Officer'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FieldOfficerNames.Add(new OfficerModal { Name = reader.GetString(1), FoID = reader.GetString(0) });
                }
                con.Close();
            }
            return FieldOfficerNames;
        }

        string GetBranchName(string branchId)
        {
            string branchName = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select BranchName from BranchDetails where Bid = '" + branchId + "'";
                branchName = (string)cmd.ExecuteScalar();
                con.Close();
            }
            return branchName;
        }

        bool IsFreeTimeForEmp(string empID,string day, string time)
        {
            int Count = 0;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select COUNT(SHGId) from TimeTable where EmpId = '" + empID + "' and CollectionDay = '" + day + "' and CollectionTime = '" + time + "'";
                Count = (int)cmd.ExecuteScalar();
                con.Close();
                if (Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }



        public bool InsertIntoSHG(string shgName,string taluk, string district, string day, string time, string officer)
        {
            List<OfficerModal> FieldOfficerNames = new List<OfficerModal>();

            try
            {
                if(IsFreeTimeForEmp(officer, day, time))
                {
                    string SHGidGenerated = GenerateSHGID();
                    using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "insert into SelfHelpGroup(BranchId, SHGId, SHGName,Taluk, District, DateOfCreation)values(@BranchId, @SHGId, @SHGName,@Taluk, @District, @DateOfCreation)";
                        cmd.Parameters.AddWithValue("@branchId", BranchId);
                        cmd.Parameters.AddWithValue("@shgID", SHGidGenerated);
                        cmd.Parameters.AddWithValue("@SHGName", shgName);
                        cmd.Parameters.AddWithValue("@Taluk", taluk);
                        cmd.Parameters.AddWithValue("@District", district);
                        cmd.Parameters.AddWithValue("@DateOfCreation", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "insert into TimeTable(SHGId, CollectionTime, CollectionDay,EmpId)values(@shgId,@cTime,@cDay,@empId)";
                        cmd.Parameters.AddWithValue("@shgId", SHGidGenerated);
                        cmd.Parameters.AddWithValue("@cTime", time);
                        cmd.Parameters.AddWithValue("@cDay", day);
                        cmd.Parameters.AddWithValue("@empId", officer);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Already time alloted for this employee..!");
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        //string BranchName = MainWindow.LoginDesignation.BranchId;
        //string Region = MainWindow.LoginDesignation.RegionName;
        //string Designation = string.Empty;

        public string GetRegionNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionCode from region where RegionName=(select RegionName from BranchDetails where Bid='"+BranchId+"')";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }
        public string GetBranchNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchCode from BranchDetails where Bid='" + BranchId + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
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
                    sqlcomm.CommandText = "Select Count(SHGId) from SelfHelpGroup";
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

        private void xOfficerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OfficerModal obj = xOfficerSelect.SelectedItem as OfficerModal;
            List<FieldOfficerWorkload> FOworkLoad = GetDaysForFieldOfficer(obj.FoID);
            xSHGRegion.Text = GetDistrictName(obj.FoID).ToUpper();
            //xFOworkLoadList.DataContext = FOworkLoad;
            xOfficerName.Text = obj.Name;
            xFOworkLoadList.ItemsSource = FOworkLoad;
        }

        List<string> GetAllDistrinct()
        {
            List<string> distrinctlist = new List<string>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select distinct(District) from DistrictTaluk";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    distrinctlist.Add(dr.GetString(0));
                }
                dr.Close();
            }

            return distrinctlist;
        }
        List<string> GetTalukByDistrict(string District)
        {
            List<string> taluklist = new List<string>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select distinct(Taluk) from DistrictTaluk where district='"+ District+"' ";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    taluklist.Add(dr.GetString(0));
                }
                dr.Close();
            }
            return taluklist;

        }

        public string GetDistrictName(string empID)
        {
            string Result = string.Empty;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionName from BranchDetails where Bid = (select BranchId from EmployeeBranch where EmpId = '"+ empID+"')";
                    Result = (string)sqlcomm.ExecuteScalar();
                    sqlconn.Close();
                }
                return Result;
            }
        }

        List<FieldOfficerWorkload> GetDaysForFieldOfficer(string empId)
        {
            List<string> ListOfDays = new List<string>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select CollectionDay from TimeTable where EmpId = '"+empId+"' group by CollectionDay";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListOfDays.Add(reader.GetString(0));
                }
                con.Close();
            }

            List<FieldOfficerWorkload> ListToBind = new List<FieldOfficerWorkload>();
            foreach (string item in ListOfDays)
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "select CollectionTime from TimeTable where EmpId = '" + empId + "' and CollectionDay = '" + item + "'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    FieldOfficerWorkload obj = new FieldOfficerWorkload();
                    obj.TimeList = new List<Timestring>();
                    while (reader.Read())
                    {   
                        obj.WeekDay = item;
                        obj.TimeList.Add(new Timestring(reader.GetTimeSpan(0).ToString(@"hh\:mm")));
                    }
                    ListToBind.Add(obj);
                    con.Close();
                }
            }
            return ListToBind;
        }
    }

    public class FieldOfficerWorkload
    {
        public string WeekDay { get; set; }
        public List<Timestring> TimeList { get; set; }

        public FieldOfficerWorkload()
        {
            
        }
    }

    public class Timestring
    {
        public string Time { get; set; }

        public Timestring(string time)
        {
            Time = time;
        }
    }

}
