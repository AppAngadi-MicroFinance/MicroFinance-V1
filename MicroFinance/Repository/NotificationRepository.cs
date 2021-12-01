using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Repository
{
    public class NotificationRepository
    {
        public static string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;

        public static int GetVerifyDocumentNotifyCount(string EmpID)
        {
            int Count = 0;

            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(*) from LoanApplication where EmployeeId='E0100120210904' and LoanStatus=3";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Count;

        }
       
    }
}
