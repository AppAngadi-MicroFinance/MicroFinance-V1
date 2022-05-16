using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class CollectionReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<CollectionEntryModel> MasterList = new List<CollectionEntryModel>();

        public List<ReportModel> CenterWise_CollectedData { get; set; }
        public List<ReportModel> EmployeeWise_CollectedData { get; set; }
        public List<ReportModel> DistrictWise_CollectedData { get; set; }
        LoanRepository LoanRepos;
        public CollectionReport(LoanRepository loanRepos, DateRange range)
        {
            this.Range = range;
            this.LoanRepos = loanRepos;
            this.MonthPeriods = LoanRepos.Get_MonthPeriods(this.Range);
            this.MasterList = LoanRepos.Get_AllCollectionEntries(this.Range);
            //
            this.CenterWise_CollectedData = CenterWise();
            this.EmployeeWise_CollectedData = EmployeeWise();
            this.DistrictWise_CollectedData = DistrictWise();
        }


        List<ReportModel> DistrictWise()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegionId = MasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();

            foreach (string region in distinctRegionId)
            {
                List<string> distinctBranch = MasterList.Where(o => o.OriginDetail.RegionId == region).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branch in distinctBranch)
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

                        List<CollectionEntryModel> regionList = MasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                        List<CollectionEntryModel> FinalList = regionList.Where
                            (o => o.CollectedDate > obj.FromDate && o.CollectedDate <= obj.ToDate).ToList();

                        obj.Value = FinalList.Select(o => o.PrincipleAmount).Sum();
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
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in DistinctBranchId)
            {
                List<string> distinctEmployee = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.CollectedBy_EmpID).Distinct().ToList();
                foreach (string empID in distinctEmployee)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = LoanRepos.BranchDetailDICT[branch].BranchName;
                    Item.Column_3 = empID;
                    Item.Column_4 = LoanRepos.EmployeeNameDICT[empID];

                    for (int i = 0; i < MonthPeriods.Count(); i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<CollectionEntryModel> BranchAndEmployee = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.CollectedBy_EmpID == empID).ToList();
                        List<CollectionEntryModel> Final = BranchAndEmployee.Where
                            (o => o.CollectedDate > obj.FromDate && o.CollectedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Select(o => o.PrincipleAmount).Sum();
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
            List<string> DistinctBranchId = MasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branch in DistinctBranchId)
            {
                List<string> distinctCenterId = MasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in distinctCenterId)
                {
                    ReportModel Item = new ReportModel();
                    Item.Column_1 = branch;
                    Item.Column_2 = LoanRepos.BranchDetailDICT[branch].BranchName;
                    Item.Column_3 = center;
                    Item.Column_4 = LoanRepos.SHGNameDICT[center];

                    for (int i = 0; i < MonthPeriods.Count(); i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<CollectionEntryModel> BranchAndSHG = MasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                        List<CollectionEntryModel> Final = BranchAndSHG.Where
                            (o => o.CollectedDate > obj.FromDate && o.CollectedDate <= obj.ToDate).ToList();

                        obj.Value = Final.Select(o => o.PrincipleAmount).Sum();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
