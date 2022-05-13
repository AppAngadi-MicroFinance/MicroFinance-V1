using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class OutStandingReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanSummaryModel> LoanMasterList = new List<LoanSummaryModel>();
        List<CollectionEntryData> CollectionMaster = new List<CollectionEntryData>();


        public List<ReportModel> CenterWise_OData { get; set; }
        public List<ReportModel> EmployeeWise_OData { get; set; }
        public List<ReportModel> BranchWise_OData { get; set; }
        LoanRepository LoanRepo;
        public OutStandingReport(LoanRepository loanRepos, DateRange range)
        {
            this.Range = range;
            this.LoanRepo = loanRepos;
            this.MonthPeriods = LoanRepo.Get_MonthPeriods(this.Range);
            this.LoanMasterList = LoanRepo.Get_LoanSummaryDetails(this.Range);
            this.CollectionMaster = LoanRepo.Get_CollectionEntry(this.Range);
            //
            this.EmployeeWise_OData = EmployeeWise_ReportData();
            this.CenterWise_OData = CenterWise_OutStanding();
            this.BranchWise_OData = BranchWise_Outstanding();
        }

        List<ReportModel> EmployeeWise_ReportData()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            Dictionary<string, List<string>> ApprovedLoans = LoanRepo.Get_AllApprovedLoanId(this.Range);
            List<LoanOutstandingModel> empCollectionList = new List<LoanOutstandingModel>();
            foreach (KeyValuePair<string, List<string>> loanItem in ApprovedLoans)
            {
                foreach (string empId in loanItem.Value)
                {
                    LoanOutstandingModel obj = new LoanOutstandingModel();
                    obj.LoanId = loanItem.Key;
                    obj.EmployeeID = empId;
                    obj.BranchDetail = LoanRepo.BranchDetailDICT[LoanRepo.EmployeeBranchDICT[empId]];
                    obj.LoanAmount = LoanMasterList.Where(o => o.LoanId == loanItem.Key).Select(o => o.LoanAmount).FirstOrDefault();
                    obj.PrincipleAmount = LoanMasterList.Where(o => o.LoanId == loanItem.Key).Select(o => o.PrincipleAmount).FirstOrDefault();
                    obj.CollectionEntries = CollectionMaster.Where(o => o.LoanId == loanItem.Key && o.CollectedBy_EmpID == empId).ToList();
                    empCollectionList.Add(obj);
                }
            }

            List<string> empIdList = empCollectionList.Select(o => o.EmployeeID).Distinct().ToList();
            foreach (string emp in empIdList)
            {
                ReportModel Item = new ReportModel();
                Item.C1 = empCollectionList.Where(o => o.EmployeeID == emp).Select(o => o.BranchDetail.BranchId).FirstOrDefault();
                Item.C2 = emp;
                Item.C3 = LoanRepo.EmployeeNameDICT[Item.C2];
                for (int i = 0; i < MonthPeriods.Count; i++)
                {
                    DateAndData obj = new DateAndData();
                    obj.FromDate = MonthPeriods[i].AddMonths(-1);
                    obj.ToDate = MonthPeriods[i];

                    List<string> loanId = empCollectionList.Where(o => o.EmployeeID == emp).Select(o => o.LoanId).Distinct().ToList();
                    int collectionForPeriodforLoan = 0;

                    foreach (string loan in loanId)
                    {
                        List<CollectionEntryData> collectionData = empCollectionList.Where(o => o.EmployeeID == emp && o.LoanId == loan).Select(o => o.CollectionEntries).FirstOrDefault();

                        int TotalLoanAmount = empCollectionList.Where(o => o.EmployeeID == emp && o.LoanId == loan).Select(o => o.LoanAmount).FirstOrDefault();
                        int principle = empCollectionList.Where(o => o.EmployeeID == emp && o.LoanId == loan).Select(o => o.PrincipleAmount).FirstOrDefault();

                        int collectionCount = collectionData.Where(o => o.CollectedOn <= obj.ToDate).Count();
                        collectionForPeriodforLoan += TotalLoanAmount - (principle * collectionCount);
                    }
                    obj.Amount = collectionForPeriodforLoan;
                    Item.DataList.Add(obj);
                }
                FinalData.Add(Item);
            }

            List<ReportModel> FinalData2 = new List<ReportModel>();
            List<string> distinctBranch = FinalData.Select(o => o.C1).Distinct().ToList();
            foreach (string branch in distinctBranch)
            {
                List<string> distinctEmployee = FinalData.Where(o => o.C1 == branch).Select(o => o.C2).Distinct().ToList();
                foreach (string employee in distinctEmployee)
                {
                    ReportModel Item = FinalData.Where(o => o.C1 == branch && o.C2 == employee).Select(o => o).FirstOrDefault();
                    FinalData2.Add(Item);
                }
            }
            return FinalData2;
        }
        List<ReportModel> CenterWise_OutStanding()
        {
            List<ReportModel> FinalData = new List<ReportModel>();

            List<string> distinctBranch = LoanMasterList.Select(o => o.OriginDetail.BranchId).Distinct().ToList();
            foreach (string branch in distinctBranch)
            {
                List<string> distinctCenter = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string center in distinctCenter)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branch;
                    Item.C2 = center;
                    Item.C3 = LoanRepo.SHGNameDICT[center];

                    List<LoanSummaryModel> BranchAndSHG = LoanMasterList.Where(o => o.OriginDetail.BranchId == branch && o.OriginDetail.SHGId == center).ToList();
                    List<string> distinctLoanID = BranchAndSHG.Select(o => o.LoanId).Distinct().ToList();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        int loanAmountSum = 0;
                        int principleSum = 0;
                        foreach (string loanId in distinctLoanID)
                        {
                            loanAmountSum += LoanMasterList.Where(o => o.LoanId == loanId).Select(o => o.LoanAmount).FirstOrDefault();
                            int principle = LoanMasterList.Where(o => o.LoanId == loanId).Select(o => o.PrincipleAmount).FirstOrDefault();
                            int count = CollectionMaster.Where(o => o.LoanId == loanId && o.CollectedOn < obj.ToDate).Count();
                            principleSum += (principle * count);
                        }

                        obj.Amount = loanAmountSum - principleSum;
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> BranchWise_Outstanding()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegion = LoanMasterList.Select(o => o.OriginDetail.RegionId).Distinct().ToList();
            foreach (string region in distinctRegion)
            {
                List<string> distinctBranch = LoanMasterList.Where(o => o.OriginDetail.RegionId == region).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branch in distinctBranch)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = region;
                    Item.C2 = branch;
                    Item.C3 = LoanRepo.BranchDetailDICT[branch].BranchName;

                    List<LoanSummaryModel> RegionAndBranch = LoanMasterList.Where(o => o.OriginDetail.RegionId == region && o.OriginDetail.BranchId == branch).ToList();
                    List<string> distinctLoanID = RegionAndBranch.Select(o => o.LoanId).Distinct().ToList();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        int loanAmountSum = 0;
                        int principleSum = 0;
                        foreach (string loanId in distinctLoanID)
                        {
                            loanAmountSum += LoanMasterList.Where(o => o.LoanId == loanId).Select(o => o.LoanAmount).FirstOrDefault();
                            int principle = LoanMasterList.Where(o => o.LoanId == loanId).Select(o => o.PrincipleAmount).FirstOrDefault();
                            int count = CollectionMaster.Where(o => o.LoanId == loanId && o.CollectedOn < obj.ToDate).Count();
                            principleSum += (principle * count);
                        }
                        obj.Amount = loanAmountSum - principleSum;
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
    }
}
