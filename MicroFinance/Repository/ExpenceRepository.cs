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
                    sqlcomm.CommandText = "insert into ExpenceDetails(BranchID,EmployeeID,ExpenceType,Reason,ExpenceDate,Amount)values('"+Details.BranchID+"','"+Details.EmployeeID+"','"+Details.ExpenceType+"','"+Details.Reason+"','"+Details.ExpenceDate.ToString("yyyy-MM-dd")+"','"+Details.Amount+"')";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public static List<ExpenseDetailsView> GetExpenceDetails(string BranchId,DateTime FromDate,DateTime ToDate)
        {

            return new List<ExpenseDetailsView>();
        }
        public static List<ExpenseDetailsView> GetExpenceDetails(DateTime FromDate,DateTime ToDate)
        {
            List<ExpenseDetailsView> ExpenseDetails = new List<ExpenseDetailsView>();
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchDetails.BranchName,Employee.Name,ExpenceType,Reason,ExpenceDate,Amount from ExpenceDetails,BranchDetails,Employee where ExpenceDate between '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "' and BranchDetails.Bid=ExpenceDetails.BranchID and Employee.EmpId=ExpenceDetails.EmployeeID";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            ExpenseDetailsView Expense = new ExpenseDetailsView();
                            Expense.BranchName = reader.GetString(0);
                            Expense.EmployeeName = reader.GetString(1);
                            Expense.ExpenseType = reader.GetString(2);
                            Expense.Reason = reader.GetString(3);
                            Expense.ExpenceDate = reader.GetDateTime(4);
                            Expense.Amount = reader.GetInt32(5);


                            ExpenseDetails.Add(Expense);
                            
                        }
                    }

                }
            }
            return ExpenseDetails;
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
