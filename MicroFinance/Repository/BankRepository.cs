using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
