using MicroFinance.Repository;
using MicroFinance.ViewModel;
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
    /// Interaction logic for ExpenceReportView.xaml
    /// </summary>
    public partial class ExpenceReportView : Page
    {
        public ExpenceReportView()
        {
            InitializeComponent();
            FromDate.SelectedDate = DateTime.Today;
            EndDate.SelectedDate = DateTime.Today;
        }
        public ExpenceReportView(List<ExpenseDetailsView> ExpenseDetails)
        {
            InitializeComponent();
           // ExpenceViewGrid.ItemsSource = ExpenseDetails;
            FromDate.SelectedDate = DateTime.Today;
            EndDate.SelectedDate = DateTime.Today;
        }

        private async void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if(FromDate.SelectedDate!=null && EndDate.SelectedDate!=null)
            {
                DateTime FDate = FromDate.SelectedDate.Value;
                DateTime TDate = EndDate.SelectedDate.Value;
                List<ExpenseDetailsView> Expenses = new List<ExpenseDetailsView>();
                GifPanel.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => Expenses = ExpenceRepository.GetExpenceDetails(FDate,TDate));
                GifPanel.Visibility = Visibility.Collapsed;
                if(Expenses.Count!=0)
                {
                    LoadDetails(Expenses);
                }
                else
                {
                    ExpenceViewGrid.Items.Clear();
                    Totaltext.Text = "";
                    MessageBox.Show("No Data Found");
                } 
            }
        }

        void LoadDetails(List<ExpenseDetailsView> Details)
        {
            ExpenceViewGrid.Items.Clear();
            foreach(ExpenseDetailsView expense in Details)
            {
                ExpenceViewGrid.Items.Add(expense);
            }

            Totaltext.Text = "Total : Rs-" + Details.Select(temp => temp.Amount).Sum().ToString() + "/-";
        }
    }
}
