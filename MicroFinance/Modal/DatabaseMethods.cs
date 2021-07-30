using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class DatabaseMethods
    {
        static public List<SelfHelpGroupModal> GetSHG(string branchId)
        {
            List<SelfHelpGroupModal> toReturn = new List<SelfHelpGroupModal>();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select SHGid, SHGName from SelfHelpGroup2 where BranchId = '" + branchId + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    toReturn.Add(new SelfHelpGroupModal(reader.GetString(0), reader.GetString(1)));
                }
                con.Close();
            }
            return toReturn;
        }
        static public void InsertNewPeerGroup(string shgId, string groupId, string groupName)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "insert into PeerGroup2(SHGid, GroupId, GroupName) values ('"+shgId+"','"+groupId+"','"+groupName+"')";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //Region
        public static bool IsExixtRegion(string regionName)
        {
            int Count = int.MaxValue;
            using (SqlConnection sqlcon = new SqlConnection(MainWindow.NewConnectionString))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select count(RegionId) from Region where RegionName = '"+regionName+"'";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            if (Count <= 0)
                return false;
            else
                return true;
        }
        public static string GetRegionsCount()
        {
            int Count = 0;
            using (SqlConnection sqlcon = new SqlConnection(MainWindow.NewConnectionString))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "select count(RegionID) from Region";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }

            if (Count < 10)
                return "0" + (Count+1);
            else
            {
                return Count.ToString();
            }
        }


        //Branch
        public static int GetBranchCount()
        {
            int number = 1;
            using (SqlConnection sqlconn = new SqlConnection(MainWindow.NewConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = sqlconn;
                    sqlComm.CommandText = "select count(BranchName) from BranchDetails";
                    int n = (int)sqlComm.ExecuteScalar();
                    number += n;
                }
                sqlconn.Close();
            }
            return number;
        }

    }

    public class SelfHelpGroupModal
    {
        public string SHGid { get; set; }
        public string SHGName{ get; set; }
        public SelfHelpGroupModal(string shgId, string SHGName)
        {
            this.SHGid = shgId;
            this.SHGName = SHGName;
        }
        public SelfHelpGroupModal()
        {

        }

        public override string ToString()
        {
            return this.SHGName;
        }
    }


}
