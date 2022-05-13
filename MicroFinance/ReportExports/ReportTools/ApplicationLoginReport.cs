using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class ApplicationLoginReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanApplicationModel> MasterList = new List<LoanApplicationModel>();

        public List<ReportModel> CenterWise_Applications = new List<ReportModel>();
        public List<ReportModel> EmployeeWise_Applications = new List<ReportModel>();
        public List<ReportModel> RegionWise_Applications = new List<ReportModel>();
        LoanRepository LoanRepos;
        public ApplicationLoginReport(LoanRepository loanRepos, DateRange range)
        {
            this.Range = range;
            this.LoanRepos = loanRepos;
            this.MonthPeriods = this.LoanRepos.Get_MonthPeriods(this.Range);
            this.MasterList = this.LoanRepos.Get_AllLoanApplications(this.Range);
            this.CenterWise_Applications = CenterWise();
            this.EmployeeWise_Applications = EmployeeWise();
            this.RegionWise_Applications = RegionWise();
        }
        List<ReportModel> RegionWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegionId = MasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();

            foreach (string region in distinctRegionId)
            {
                List<string> distinctBranchId = MasterList.Where(o => o.OriginDetail.RegionId == region).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branch in distinctBranchId)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = region;
                    Item.Column_2 = branch;
                    Item.Column_3 = LoanRepos.BranchDetailDICT[branch].BranchName;

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> regionAndBranch = MasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                        List<LoanApplicationModel> Final = regionAndBranch.Where(o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> EmployeeWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in distinctBranchId)
            {
                List<string> distinctEmployee = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.EmployeeId).Distinct().ToList();
                foreach (string employee in distinctEmployee)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = employee;
                    Item.Column_3 = LoanRepos.EmployeeNameDICT[employee];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> branchAndEmployee = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.EmployeeId == employee).ToList();
                        List<LoanApplicationModel> Final = branchAndEmployee.Where(o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> CenterWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in distinctBranchId)
            {
                List<string> distinctCenter = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in distinctCenter)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = center;
                    Item.Column_3 = LoanRepos.SHGNameDICT[center];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> branchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<LoanApplicationModel> Final = branchAndSHG.Where(o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
