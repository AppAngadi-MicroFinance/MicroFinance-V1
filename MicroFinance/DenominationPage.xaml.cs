using MicroFinance.Modal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DenominationPage.xaml
    /// </summary>
    public partial class DenominationPage : Page
    {

        List<DenominationModel> Dlist = new List<DenominationModel>();
        int initialAmt = 0;
        Button btn;
        public DenominationPage()
        {

        }
        public DenominationPage(int TotalCollectedAmount,string BranchName,string CenterName,DateTime Date,Button button)
        {
            btn = button;
            initialAmt = TotalCollectedAmount;
            InitializeComponent();
            TotalAmount.Text = initialAmt.ToString("C");
            BranchBlock.Text = BranchName;
            CenterBlock.Text = CenterName;
            DateBlock.Text = Date.ToString("yyyy-MM-dd");
            DayBlock.Text = Date.DayOfWeek.ToString();
            AddBasic();
            DenominationList.ItemsSource = Dlist;
        }

        void AddBasic()
        {
            Dlist.Add(new DenominationModel(2000, "0"));
            Dlist.Add(new DenominationModel(500, "0"));
            Dlist.Add(new DenominationModel(200, "0"));
            Dlist.Add(new DenominationModel(100, "0"));
            Dlist.Add(new DenominationModel(50, "0"));
            Dlist.Add(new DenominationModel(20, "0"));
            Dlist.Add(new DenominationModel(10, "0"));
            Dlist.Add(new DenominationModel(5, "0"));
            Dlist.Add(new DenominationModel(2, "0"));
            Dlist.Add(new DenominationModel(1, "0"));
        }
        bool _checkIsValid = false;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        //    TextBox textBox = sender as TextBox;
        //    Int32 selectionStart = textBox.SelectionStart;
        //    Int32 selectionLength = textBox.SelectionLength;
        //    String newText = String.Empty;
        //    foreach (Char c in textBox.Text.ToCharArray())
        //    {
        //        if (Char.IsDigit(c) || Char.IsControl(c))
        //        {
        //            newText += c;
        //        }
        //    }

        //    textBox.Text = newText;
        //    textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;

            long currentAmt = Total();

            Totalamount.Text = currentAmt.ToString("C0");
            if (initialAmt == currentAmt)
            {
                _checkIsValid = true;
                TotalPanel.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                _checkIsValid = false;
                TotalPanel.Background = new SolidColorBrush(Colors.Red);
            }

        }
        public long Total()
        {
            long total = 0;
            foreach (var x in Dlist)
            {
                long a = x.Amount;
                long parsed = 0;
                if (long.TryParse(x.Multiples, out parsed))
                {
                    long ans = a * parsed;
                    total += ans;
                }
            }
            return total;
        }
        public bool  AlreadyEntered = false;
        private void SaveDenomination_Click(object sender, RoutedEventArgs e)
        {
            if(_checkIsValid)
            {
                btn.IsEnabled = true;
                AlreadyEntered = true;
                NavigationService.GoBack();
            }
            else
            {
                MainWindow.StatusMessageofPage(0, "Please Check Denomination and Enter Correct Denomination.....");
            }
        }
        public void InsertDenomination()
        {
            string _branchId = MainWindow.LoginDesignation.BranchId;
            string _regionName = MainWindow.LoginDesignation.RegionName;
            string _empId = MainWindow.LoginDesignation.EmpId;
            string _date = DateBlock.Text;
            string _twoThousand = Dlist[0].Multiples;
            string _fiveHundred = Dlist[1].Multiples;
            string _twoHundred = Dlist[2].Multiples;
            string _hundred = Dlist[3].Multiples;
            string _fifty = Dlist[4].Multiples;
            string _twenty = Dlist[5].Multiples;
            string _ten = Dlist[6].Multiples;
            string _five = Dlist[7].Multiples;
            string _two = Dlist[8].Multiples;
            string _one = Dlist[9].Multiples;
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "insert into DenominationTable values('" + _regionName + "','" + _branchId + "','" + _date + "','" + _empId + "'," + _twoThousand + "," + _fiveHundred + "," + _twoHundred + "," + _hundred + "," + _fifty + "," + _twenty + "," + _ten + "," + _five + "," + _two + "," + _one + ")";
                command.ExecuteNonQuery();
            }
        }
    }
}
