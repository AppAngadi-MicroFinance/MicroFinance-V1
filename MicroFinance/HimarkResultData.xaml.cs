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
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for HimarkResultData.xaml
    /// </summary>
    public partial class HimarkResultData : Page
    {
        HimarkResult HMResult = new HimarkResult();
        List<HimarkResult> HMResultList = new List<HimarkResult>();
        List<string> CategoryList = new List<string>();
        LoanProcess loanProcess = new LoanProcess();

        public HimarkResultData(string FilePath)
        {
            InitializeComponent();
            HMResult.GetDetails(FilePath);
            HMResultList = HMResult.himarkResultslist;
            CategoryList = HMResult.CategoryList;
            CategoryCombo.ItemsSource = CategoryList;
            CategoryCombo.SelectedIndex = 0;
            string selectedvalue = "ACCEPT";
            UpdateData(selectedvalue);

        }
        public HimarkResultData()
        {
            InitializeComponent();
        }

        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedValue = CategoryCombo.SelectedItem as string;
            UpdateData(SelectedValue);
        }

        void UpdateData(string CategoryType)
        {
            HimarkResultList.Items.Clear();
            foreach (HimarkResult hm in HMResultList)
            {
                if(hm.Status.Equals(CategoryType,StringComparison.CurrentCultureIgnoreCase))
                {
                    HimarkResultList.Items.Add(hm);
                }
            }
        }

        private void RetainBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.RetainFromHimark(ID);
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.ApproveLoanFromHimark(ID);
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.RejectFromHimark(ID);
        }
    }
}
