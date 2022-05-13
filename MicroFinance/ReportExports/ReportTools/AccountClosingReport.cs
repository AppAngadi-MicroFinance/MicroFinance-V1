using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class AccountClosingReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanSummaryModel> LoanMasterList = new List<LoanSummaryModel>();

        public List<ReportModel> EmployeeWise_Data { get; set; }
        public List<ReportModel> CenterWise_Data { get; set; }
        public List<ReportModel> BranchWise_Data { get; set; }

        LoanRepository LoanRepo;
        public AccountClosingReport(LoanRepository loanRepos, DateRange range)
        {
            this.Range = range;
            this.LoanRepo = loanRepos;
            this.MonthPeriods = this.LoanRepo.Get_MonthPeriods(this.Range);
            this.LoanMasterList = this.LoanRepo.Get_InActiveLoan(this.Range);
            this.EmployeeWise_Data = EmployeeeWise_ReportData();
            this.CenterWise_Data = CenterWise_ReportData();
            this.BranchWise_Data = BranchWise_ReportData();
        }

        List<ReportModel> EmployeeeWise_ReportData()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranch = LoanMasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
            foreach (string branch in distinctBranch)
            {
                List<string> distinctEmployee = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.AccountClosedBY).Distinct().ToList().Where(o => o != null && o.Length > 0).ToList();
                foreach (string employee in distinctEmployee)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = employee;
                    Item.Column_3 = LoanRepo.EmployeeNameDICT[employee];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanSummaryModel> branchAndCenter = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch && o.AccountClosedBY == employee).ToList();
                        List<LoanSummaryModel> final = branchAndCenter.Where(o => o.AccountClosedOn > obj.FromDate && o.AccountClosedOn <= obj.ToDate).ToList();

                        obj.Value = final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> BranchWise_ReportData()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegion = LoanMasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();
            foreach (string region in distinctRegion)
            {
                List<string> distinctCenter = LoanMasterList.Where(o => o.OriginDetail.RegionId == region).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branch in distinctCenter)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = region;
                    Item.Column_2 = branch;
                    Item.Column_3 = LoanRepo.BranchDetailDICT[branch].BranchName;

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanSummaryModel> branchAndCenter = LoanMasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                        List<LoanSummaryModel> final = branchAndCenter.Where(o => o.AccountClosedOn > obj.FromDate && o.AccountClosedOn <= obj.ToDate).ToList();

                        obj.Value = final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> CenterWise_ReportData()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranch = LoanMasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
            foreach (string branch in distinctBranch)
            {
                List<string> distinctCenter = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList().Where(o => o != null).ToList();
                foreach (string center in distinctCenter)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = center;
                    Item.Column_3 = LoanRepo.SHGNameDICT[center];
                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanSummaryModel> branchAndCenter = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<LoanSummaryModel> final = branchAndCenter.Where(o => o.AccountClosedOn > obj.FromDate && o.AccountClosedOn <= obj.ToDate).ToList();

                        obj.Value = final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
