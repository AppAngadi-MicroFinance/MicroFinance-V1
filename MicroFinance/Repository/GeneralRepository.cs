using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Repository
{
    public class GeneralRepository
    {
        public static void AddLoanPurpose(string Purpose)
        {
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText="select count(LoanPurposeName) where LoanPurposeName='"+Purpose.ToUpper()+"'";
                    int count = (int)sqlcomm.ExecuteScalar();
                    if (count == 0)
                    {
                        sqlcomm.CommandText = "insert into LoanPurpose values('" + Purpose.ToUpper() + "')";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void AddBankName(string BankName)
        {
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(BankName) from BankNames where BankName='" + BankName + "'";
                    int count = (int)sqlcomm.ExecuteScalar();
                    if(count==0)
                    {
                        sqlcomm.CommandText = "insert into BankNames values('" + BankName.ToUpper() + "')";
                        sqlcomm.ExecuteNonQuery();
                    }
                    
                }
            }
        }

        public static void AddExpenseType(string Category)
        {
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Count(Category) from ExpenceType where Category='" + Category + "'";
                    int Count = (int)sqlcomm.ExecuteScalar();
                    if(Count==0)
                    {
                        sqlcomm.CommandText = "insert into ExpenceType values ('"+Category.ToUpper()+"')";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
