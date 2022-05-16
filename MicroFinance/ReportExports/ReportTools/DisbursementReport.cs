using MicroFinance.Modal;
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
        

        LoanRepository LoanRepos;
        public DisbursementReport(LoanRepository loanRepos, DateRange dateRange)
        {
            this.Range = dateRange;
            this.LoanRepos = loanRepos;
            //
            this.MonthPeriods = LoanRepos.Get_MonthPeriods(Range);
            this.MasterList = LoanRepos.Get_AllApprovedLoans(Range);
            //
            this.CenterWise_DisbursmentData = CenterWise();
            this.EmployeeWise_DisbursmentData = EmployeeWise();
            this.RegionWise_DisbursmentData = RegionWise();
        }
        List<ReportModel> EmployeeWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in DistinctBranchId)
            {
                List<string> DistinctEmpId = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.EmployeeId).Distinct().ToList();
                foreach (string employee in DistinctEmpId)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = LoanRepos.BranchDetailDICT[branch].BranchName;
                    Item.Column_3 = employee;
                    Item.Column_4 = LoanRepos.EmployeeNameDICT[employee];
                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.EmployeeId == employee).ToList();
                        List<LoanApplicationModel> Final = BranchAndSHG.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();
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
            // Distinct BranchID list.
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in DistinctBranchId)
            {
                List<string> DistinctCenterId = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in DistinctCenterId)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = LoanRepos.BranchDetailDICT[branch].BranchName;
                    Item.Column_3 = center;
                    Item.Column_4 = LoanRepos.SHGNameDICT[center];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<LoanApplicationModel> Final = BranchAndSHG.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Count();
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
                    Item.Column_1 = region;
                    Item.Column_2 = LoanRepos.BranchDetailDICT[branch].RegionName;
                    Item.Column_3 = branch;
                    Item.Column_4 = LoanRepos.BranchDetailDICT[branch].BranchName;

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> RegionAndBranch = MasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                        List<LoanApplicationModel> Final = RegionAndBranch.Where
                            (o => o.LoanApplicationStatus.ApprovedDate > obj.FromDate && o.LoanApplicationStatus.ApprovedDate <= obj.ToDate).ToList();

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
