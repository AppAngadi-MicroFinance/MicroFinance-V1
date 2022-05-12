using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class DisbursementReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanApplicationModel> MasterList = new List<LoanApplicationModel>();

        public List<ReportModel> CenterWise_DisbursmentData { get; set; }
        public List<ReportModel> EmployeeWise_DisbursmentData { get; set; }
        public List<ReportModel> RegionWise_DisbursmentData { get; set; }

        LoanRepository LoanRepo;
        public DisbursementReport(LoanRepository loanRepos, DateRange dateRange)
        {
            this.Range = dateRange;
            this.LoanRepo = loanRepos;
            //
            this.MonthPeriods = LoanRepo.Get_MonthPeriods(Range);
            this.MasterList = LoanRepo.Get_AllApprovedLoans(Range);
            //
            this.CenterWise_DisbursmentData = CenterWise();
            this.EmployeeWise_DisbursmentData = StaffWise();
            this.RegionWise_DisbursmentData = RegionWise();
        }
        List<ReportModel> StaffWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branchId in DistinctBranchId)
            {
                List<string> DistinctEmpId = MasterList.Where(o => o.OriginDetail.BranchId == branchId).Select(o => o.EmployeeId).Distinct().ToList();
                foreach (string employee in DistinctEmpId)
                {
                    ReportModel item = new ReportModel();
                    item.C1 = branchId;
                    item.C2 = employee;
                    item.C3 = MasterList.Where(o => o.EmployeeId == employee).Select(o => o.EmployeeName).FirstOrDefault();
                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branchId && o.EmployeeId == employee).ToList();
                        List<LoanApplicationModel> Final = BranchAndSHG.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();
                        obj.Amount = Final.Count();
                        item.DataList.Add(obj);
                    }
                    FinalData.Add(item);
                }
            }
            return FinalData;
        }
        List<ReportModel> CenterWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            // Distinct BranchID list.
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in DistinctBranchId)
            {
                List<string> DistinctCenterId = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in DistinctCenterId)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branch;
                    Item.C2 = center;
                    Item.C3 = MasterList.Where(o => o.OriginDetail.SHGId == center).Select(o => o.OriginDetail.SHGName).FirstOrDefault();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<LoanApplicationModel> Final = BranchAndSHG.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> RegionWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> DistinctRegionId = MasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();

            foreach (string region in DistinctRegionId)
            {
                List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branch in DistinctBranchId)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = region;
                    Item.C2 = branch;
                    Item.C3 = MasterList.Where(o => o.OriginDetail.BranchName == branch).Select(o => o.OriginDetail.BranchName).FirstOrDefault();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> RegionAndBranch = MasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                        List<LoanApplicationModel> Final = RegionAndBranch.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
