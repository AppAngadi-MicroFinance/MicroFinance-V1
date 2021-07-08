using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Converters
{
    class GuaranterName : IValueConverter
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";
            string CustID = (string)value;
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Name from GuarenteeDetails where CustId='" + CustID + "'";
                    Result =(string) sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
