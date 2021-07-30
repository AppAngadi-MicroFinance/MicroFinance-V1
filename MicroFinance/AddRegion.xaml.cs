using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddRegion.xaml
    /// </summary>
    public partial class AddRegion : Window
    {
        Region region;
        public AddRegion()
        {
            InitializeComponent();
            region = new Region();
            RegionMainGrid.DataContext = region;
        }

        private void RegionSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string regionName = xRegionnameBox.Text;
            if (DatabaseMethods.IsExixtRegion(regionName) == false)
            {
                region.AddRegion();
                this.Close();
                MainWindow.StatusMessageofPage(1, "Region Addeed Successfully");
            }
            else
            {
                MainWindow.StatusMessageofPage(1, "The " + region.RegionName + " Region Is Already Exist... Please Check");
            }
        }

        public void AddNewRegion(string regionName)
        {
            using (SqlConnection sqlconn = new SqlConnection(MainWindow.NewConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into Region (RegionId, RegionName) values('" + region.GenerateRegionID()+"','"+ regionName + "')";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }
        }
    }
}
