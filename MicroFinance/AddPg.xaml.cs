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
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionCode from Region where RegionName='" + Region + "'";
                    Result = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result.ToString();
            }
        }
        public string GetBranchNumber()
        {
            int Result = 0;
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
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
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());

            int count = GetPeerGroupCount();

            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + "PG-" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }
        public AddPg()
        {
            InitializeComponent();
            xSHGcombo.ItemsSource = GetSHG(BranchId);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public int GetPeerGroupCount()
        {
            int count = 1;
            using (SqlConnection sqlcon = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select count(SHGid) from PeerGroup";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            return count;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchId != string.Empty && SHGid != string.Empty)
            {
                if (!CheckGroupNameExists(SHGid,GroupNameBox.Text))
                {
                    if (GroupNameBox.Text != string.Empty)
                        InsertNewPeerGroup(SHGid, GeneratePGID(), GroupNameBox.Text);
                    else
                        MessageBox.Show("Please enter group name before click.");
                }
                else
                {
                    MessageBox.Show("Group name already exist in this Selfhelpgroup..!");
                }
                
            }
            this.Close();

            //AddCustomer addCustomer = new AddCustomer();

            //AddPeerGroup APG = new AddPeerGroup();
            //APG.Branch= addCustomer.SelectBranch.SelectedItem.ToString();
            //APG.FieldOfficer = addCustomer.SelectFO.SelectedItem.ToString();
            //APG.SelfHelpGroup = addCustomer.SelectSHG.SelectedItem.ToString();
            //APG.PeerGroup = GroupNameBox.Text;
            
        }
        bool CheckGroupNameExists(string shg, string grpName)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select Count(GroupName) from PeerGroup where SHGid = '" + shg + "' and GroupName = '" + grpName + "'";
                var c = cmd.ExecuteScalar();
                con.Close();

                if (int.Parse(c.ToString()) > 0)
                    return true;
                else
                    return false;
            }
        }

        public void InsertNewPeerGroup(string shgId, string groupId, string groupName)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "insert into PeerGroup(SHGid, GroupId, GroupName, ActiveCustomers) values ('" + shgId + "','" + groupId + "','" + groupName + "'," + 0 + ")";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        static public List<SelfHelpGroupModal> GetSHG(string branchId)
        {
            List<SelfHelpGroupModal> toReturn = new List<SelfHelpGroupModal>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select SHGId, SHGName from SelfHelpGroup where BranchId = '"+branchId+"'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    toReturn.Add(new SelfHelpGroupModal(reader.GetString(0), reader.GetString(1)));
                }
                con.Close();
            }
            return toReturn;
        }
        private void xSHGcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelfHelpGroupModal selectedSHG = xSHGcombo.SelectedItem as SelfHelpGroupModal;
            SHGid = selectedSHG.SHGid;

            if(SHGid.Length > 0)
                GroupNameBox.IsEnabled = true;
            else
                GroupNameBox.IsEnabled = false;
        }
    }
}
