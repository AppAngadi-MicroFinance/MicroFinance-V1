using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
using MicroFinance.ViewModel;

namespace MicroFinance.Repository
{
    public class ExpenceRepository
    {
        public static void AddExpence(ExpenceDetails Details)
        {
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into ExpenceDetails(BranchID,EmployeeID,ExpenceType,Reason,ExpenceDate)values('"+Details.BranchID+"','"+Details.EmployeeID+"','"+Details.ExpenceType+"','"+Details.Reason+"','"+Details.ExpenceDate.ToString("yyyy-MM-dd")+"')";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public static List<ExpenseDetailsView> GetExpenceDetails(string BranchId,DateTime FromDate,DateTime ToDate)
        {

            return new List<ExpenseDetailsView>();
        }
        public static List<ExpenceDetails> GetExpenceDetails(DateTime FromDate,DateTime ToDate)
        {
            return new List<ExpenceDetails>();
        }

        public static List<string> GetExpenceTypes()
        {
            List<string> Types = new List<string>();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select * from Expencetype";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Types.Add(reader.GetString(0));
                        }
                    }

                }
            }

            return Types;
        }
    }


    
}
