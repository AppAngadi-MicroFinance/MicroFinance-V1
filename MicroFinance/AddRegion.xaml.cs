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
using MicroFinance.ViewModel;
using MicroFinance.Utils;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddRegion.xaml
    /// </summary>
    public partial class AddRegion : Window
    {
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        Region region;
        public AddRegion()
        {
            InitializeComponent();
            region = new Region();
            RegionMainGrid.DataContext = region;
        }

        private void RegionSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!region.Isexist())
            {
                region.AddRegion();
                this.Close();
                message = language.translate(SystemFunction.IsTamil, "SA24");//Region Addeed Successfully...
                MainWindow.StatusMessageofPage(1, message);
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "AE11");//Region Is Already Exist... Please Check
                MainWindow.StatusMessageofPage(1, region.RegionName + message);
            }
        }
    }
}
