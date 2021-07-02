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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanAfterHimark.xaml
    /// </summary>
    public partial class LoanAfterHimark : Page
    {
        public LoanAfterHimark()
        {
            InitializeComponent();
            setCount();
            setAmount();
            Custlist.ItemsSource = LoanRecommend.RecommenedList;
        }
        void setCount()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (Cust c in LoanRecommend.RecommenedList)
            {

                if (c.LoanType == "General")
                {
                    count1++;
                }
                else if (c.LoanType == "Special")
                {
                    count2++;
                }
            }
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
        }
        void setAmount()
        {
            int Amount1 = 0;
            int Amount2 = 0;
            foreach (Cust c in LoanRecommend.RecommenedList)
            {

                Amount1 += c.LoanAmount;
            }
            LoanAmountValue.Text = Amount1.ToString();
            BacklogValue.Text = Amount2.ToString();
        }

        private void Custlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
