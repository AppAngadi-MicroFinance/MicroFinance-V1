using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.ViewModel;
using MicroFinance.Repository;
using System.Collections.ObjectModel;

namespace MicroFinance.Modal
{
    public class BasicDetailsStatic
    {
        public BasicDetailsStatic()
        {
            BranchList = EmployeeRepository.GetBranches();
            EmployeeList = EmployeeRepository.GetEmployees();
            CenterList = EmployeeRepository.GetTimeTable();
        }



        public ObservableCollection<BranchViewModel> BranchList = new ObservableCollection<BranchViewModel>();
        public ObservableCollection<EmployeeViewModel> EmployeeList = new ObservableCollection<EmployeeViewModel>();
        public List<TimeTableViewModel> CenterList = new List<TimeTableViewModel>();
    }
}
