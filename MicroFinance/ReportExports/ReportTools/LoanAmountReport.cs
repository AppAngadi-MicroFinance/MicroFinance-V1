using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class LoanAmountReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanApplicationModel> LoanMetaMasterList = new List<LoanApplicationModel>();

        public List<ReportModel> CenterWise_AmountData { get; set; }
        public List<ReportModel> EmployeeWise_AmountData { get; set; }
        public List<ReportModel> RegionWise_AmountData { get; set; }
        LoanRepository LoanRepos;
        public LoanAmountReport(LoanRepository loanRepos, DateRange range)
        {
            this.LoanRepos = loanRepos;
            this.Range = range;
            this.MonthPeriods = LoanRepos.Get_MonthPeriods(this.Range);
            this.LoanMetaMasterList = LoanRepos.Get_AllApprovedLoans(this.Range);
            //
            this.CenterWise_AmountData = CenterWise();
            this.EmployeeWise_AmountData = EmployeeWise();
            this.RegionWise_AmountData = RegionWise();
        }
        List<ReportModel> RegionWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegionId = LoanMetaMasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();
            foreach (string regionId in distinctRegionId)
            {
                List<string> distinctBranch = LoanMetaMasterList.Where(o => o.OriginDetail.RegionId == regionId).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branchId in distinctBranch)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = regionId;
                    Item.Column_2 = branchId;
                    Item.Column_3 = LoanRepos.BranchDetailDICT[branchId].BranchName;
                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];
                        List<LoanApplicationModel> RegionAndBranch = LoanMetaMasterList.Where(o => o.OriginDetail.RegionId == regionId && o.OriginDetail.BranchId == branchId).ToList();
                        List<LoanApplicationModel> Final = RegionAndBranch.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Select(o => o.LoanAmount).Sum();
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
            List<string> distinctBranchId = LoanMetaMasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
            foreach (string branch in distinctBranchId)
            {
                List<string> distinctmEmpId = LoanMetaMasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.EmployeeId).Distinct().ToList();
                foreach (string empID in distinctmEmpId)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = empID;
                    Item.Column_3 = LoanRepos.EmployeeNameDICT[empID];
                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndEmployee = LoanMetaMasterList.Where(o => o.OriginDetail.BranchId == branch && o.EmployeeId == empID).ToList();
                        List<LoanApplicationModel> Final = BranchAndEmployee.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Select(o => o.LoanAmount).Sum();
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

            List<string> distinctBranchId = LoanMetaMasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
            foreach (string branch in distinctBranchId)
            {
                List<string> distinctCenterId = LoanMetaMasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in distinctCenterId)
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

                        List<LoanApplicationModel> BranchAndSHG = LoanMetaMasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<LoanApplicationModel> Final = BranchAndSHG.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Select(o => o.LoanAmount).Sum();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
