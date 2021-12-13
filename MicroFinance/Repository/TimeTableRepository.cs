using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;

namespace MicroFinance.Repository
{
    public class TimeTableRepository
    {
        public static string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;

        public static bool CreateShedule(TimeTable SheduleDetails)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into TimeTable(SHGId,CollectionTime,CollectionDay,EmpId) values('"+SheduleDetails.SHGID+"','"+SheduleDetails.CollectionTime+"','"+SheduleDetails.CollectionDay+"','"+SheduleDetails.EmployeeID+"')";
                    int res = sqlcomm.ExecuteNonQuery();
                    if (res > 0)
                        return true;
                }
            }
            return false;
        }
        public static bool UpdateShedule(TimeTable SheduleDetails)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update TimeTable set CollectionTime='"+SheduleDetails.CollectionTime+"' , CollectionDay='"+SheduleDetails.CollectionDay+"',EmpId='"+SheduleDetails.EmployeeID+"' where SHGId='"+SheduleDetails.SHGID+"'";
                    int res = sqlcomm.ExecuteNonQuery();
                    if (res > 0)
                        return true;
                }
            }
            return false;
        }
        public static bool DeleteShedule(string SHGID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "delete from TimeTable where SHGId='"+SHGID+"'";
                    int res = sqlcomm.ExecuteNonQuery();
                    if (res > 0)
                        return true;
                }
            }
            return false;
        }


        public static bool IsAlreadyAllocated(string EmployeeID, string CollectionDay, string CollectionTime)
        {
            int Count = 0;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select COUNT(SHGId) from TimeTable where EmpId = '" + EmployeeID + "' and CollectionDay = '" + CollectionDay + "' and CollectionTime = '" + CollectionTime + "'";
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


        public static ObservableCollection<TimeTable> GetShedules(string BranchID)
        {
            ObservableCollection<TimeTable> SheduleList = new ObservableCollection<TimeTable>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Employee.Name,SelfHelpGroup.SHGName,TimeTable.CollectionTime,TimeTable.SHGId,TimeTable.CollectionDay,TimeTable.EmpId from Employee,SelfHelpGroup,TimeTable where TimeTable.EmpId=Employee.EmpId and SelfHelpGroup.SHGId=TimeTable.SHGId and SelfHelpGroup.BranchId='"+BranchID+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            TimeTable Shedule = new TimeTable();
                            Shedule.EmpName = reader.GetString(0);
                            Shedule.CenterName = reader.GetString(1);
                            TimeSpan time = reader.GetTimeSpan(2);
                            Shedule.CollectionTime = (time.Hours+":"+time.Minutes).ToString();
                            Shedule.SHGID = reader.GetString(3);
                            Shedule.CollectionDay = reader.GetString(4);
                            Shedule.EmployeeID = reader.GetString(5);


                            SheduleList.Add(Shedule);

                        }
                        reader.Close();
                    }
                    sqlconn.Close();
                }
            }

            return SheduleList;
        }
    }
}
