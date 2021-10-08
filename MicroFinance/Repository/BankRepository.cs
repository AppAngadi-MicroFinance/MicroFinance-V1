using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.ViewModel;

namespace MicroFinance.Repository
{
    public class BankRepository
    {
        public static List<string> GetAllBankNames()
        {
            List<string> BankList = new List<string>();
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BankName from banknames ORDER BY BankName ASC";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            BankList.Add(reader.GetString(0));
                        }
                    }
                    reader.Close();

                }
                sqlconn.Close();
            }

            return BankList;
        }



        public static List<BankDetailsView> BankDetailsList()
        {
            List<BankDetailsView> BankList = new List<BankDetailsView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BankName,BranchName,IFSCCode,MICRCode from BankDetails ORDER BY BankName ASC";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BankList.Add(new BankDetailsView { BankName=reader.GetString(0),BranchName=reader.GetString(1),IFSCCode=reader.GetString(2),MICRCode=reader.GetString(3)});
                        }
                    }
                    reader.Close();

                }
                sqlconn.Close();
            }

            return BankList;
        }


        public static void AddBankDetails(BankDetailsView bank)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into BankDetails(BankName,BranchName,IFSCCode,MICRCode)values('"+bank.BankName+"','"+bank.BranchName+"','"+bank.IFSCCode+"','"+bank.MICRCode+"')";
                    sqlcomm.ExecuteNonQuery();
                }
            }

        }
    }
}
