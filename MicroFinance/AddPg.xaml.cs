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
using System.Windows.Shapes;
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddPg.xaml
    /// </summary>
    public partial class AddPg : Window
    {
        string BranchId = MainWindow.LoginDesignation.BranchId;
        string Region = MainWindow.LoginDesignation.RegionName;
        string SHGid = string.Empty;


        public string GetRegionNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
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
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
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
        public string GeneratePGID() // IDPattern 01002202106SHG-05 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountofShg+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "Select Count( GroupId ) from PeerGroup2";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "PG-" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }
        public AddPg()
        {
            InitializeComponent();
            xSHGcombo.ItemsSource = DatabaseMethods.GetSHG(BranchId);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchId != string.Empty && SHGid != string.Empty)
            {
                DatabaseMethods.InsertNewPeerGroup(SHGid, GeneratePGID(), GroupNameBox.Text);
            }
            this.Close();

            //AddCustomer addCustomer = new AddCustomer();
            //AddPeerGroup APG = new AddPeerGroup();
            //APG.Branch= addCustomer.SelectBranch.SelectedItem.ToString();
            //APG.FieldOfficer = addCustomer.SelectFO.SelectedItem.ToString();
            //APG.SelfHelpGroup = addCustomer.SelectSHG.SelectedItem.ToString();
            //APG.PeerGroup = GroupNameBox.Text;
            

        }

        private void xSHGcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelfHelpGroupModal selectedSHG = xSHGcombo.SelectedItem as SelfHelpGroupModal;
            SHGid = selectedSHG.SHGid;
        }
    }
}
