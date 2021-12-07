using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;

namespace MicroFinance.Repository
{
    public class SHGRepository
    {
        public static string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;


        public static string AddSHG(SHGModal SHGDetails)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    string SHGId = GenerateSHGID(SHGDetails.BranchID);
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into SelfHelpGroup(BranchId,SHGId,SHGName,DateOfCreation,Taluk,District,IsActive) values('"+SHGDetails.BranchID.ToString()+"','"+SHGId+"','"+SHGDetails.SHGName+"','"+DateTime.Today.ToString("MM-dd-yyyy")+"','"+SHGDetails.Taluk+"','"+SHGDetails.District+"','1')";
                    int res = sqlcomm.ExecuteNonQuery();
                    if(res>0)
                    {
                        return SHGId;
                    }
                }
            }
            return null;
        }

        public static bool UpdateSHG(SHGModal SHGDetails)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "";
                    int res = sqlcomm.ExecuteNonQuery();
                    if (res > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool DeActiveSHG(string SHGID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update SelfHelpGroup set IsActive=0 where SHGId='"+SHGID+"'";
                    int res = sqlcomm.ExecuteNonQuery();
                    if (res > 0)
                    {
                        TimeTableRepository.DeleteShedule(SHGID);
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GenerateSHGID(string BranchID)
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
            string region = BranchID.Substring(0,2);
            string branch = BranchID.Substring(8);
            Result = region + branch + year + month + "SHG-" + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }

        public static List<SHGModal> GetCenters(bool IsSheduled,string BranchId)
        {
            List<SHGModal> Centers = new List<SHGModal>();
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SHGId,SHGName,DateOfCreation,Taluk,District,IsActive from SelfHelpGroup where SHGId not in(select SHGId from TimeTable) and BranchId='"+BranchId+"' and IsActive='"+IsSheduled+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            SHGModal Center = new SHGModal();
                            Center.SHGId = reader.GetString(0);
                            Center.SHGName = reader.GetString(1);
                            Center.CreationDate = reader.GetDateTime(2);
                            Center.Taluk = reader.GetString(3);
                            Center.District = reader.GetString(4);

                            Centers.Add(Center);
                        }
                        reader.Close();
                    }

                }
                sqlconn.Close();
            }
            return Centers;
        }

        public static List<SHGModal> GetCenters(string BranchId)
        {
            List<SHGModal> Centers = new List<SHGModal>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SHGId,SHGName,DateOfCreation,Taluk,District,IsActive from SelfHelpGroup where BranchId='"+BranchId+"' and IsActive=1";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SHGModal Center = new SHGModal();
                            Center.SHGId = reader.GetString(0);
                            Center.SHGName = reader.GetString(1);
                            Center.CreationDate = reader.GetDateTime(2);
                            Center.Taluk = reader.GetString(3);
                            Center.District = reader.GetString(4);

                            Centers.Add(Center);
                        }
                        reader.Close();
                    }

                }
                sqlconn.Close();
            }
            return Centers;
        }
    }
}
