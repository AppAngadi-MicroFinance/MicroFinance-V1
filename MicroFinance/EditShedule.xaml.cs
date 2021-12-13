using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditShedule.xaml
    /// </summary>
    public partial class EditShedule : Page
    {
        ObservableCollection<TimeTable> OverAllList = new ObservableCollection<TimeTable>();
        ObservableCollection<TimeTable> BindingList = new ObservableCollection<TimeTable>();
        public EditShedule(ObservableCollection<TimeTable> SheduleList)
        {
            InitializeComponent();
            OverAllList = SheduleList;
            LoadData();
            
            SheduleGridView.ItemsSource = BindingList;
        }

        void LoadData()
        {
            BindingList.Clear();
            foreach(TimeTable Shedule in OverAllList)
            {
                BindingList.Add(Shedule);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditShedule_Click(object sender, RoutedEventArgs e)
        {
            TimeTable Shedule = new TimeTable();
            Shedule = SheduleGridView.SelectedItem as TimeTable;
            this.NavigationService.Navigate(new AssignSHG(Shedule));
        }
    }
}
