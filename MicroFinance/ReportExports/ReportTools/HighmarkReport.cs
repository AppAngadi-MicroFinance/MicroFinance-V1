using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class HighmarkReport
    {
        DateRange Range = new DateRange();
        public List<DateTime> MonthPeriods = new List<DateTime>();

        List<LoanApplicationModel> ApplicationHighmarkRejected = new List<LoanApplicationModel>();
        List<LoanApplicationModel> ApplicationHighmarkApproved = new List<LoanApplicationModel>();

        public List<ReportModel> CenterWise_RejectionData { get; set; }
        public List<ReportModel> CenterWise_ApprovedData { get; set; }

        public List<ReportModel> EmployeeWise_RejectionData { get; set; }
        public List<ReportModel> EmployeeWise_ApprovedData { get; set; }

        public List<ReportModel> RegionWise_RejectionData { get; set; }
        public List<ReportModel> RegionWise_ApprovedData { get; set; }
        LoanRepository LoanRepos;
        public HighmarkReport(LoanRepository loanRepos, DateRange range,bool isaccept)
        {
            this.Range = range;
            this.LoanRepos = loanRepos;
            this.MonthPeriods = LoanRepos.Get_MonthPeriods(this.Range);
            if (isaccept==true)
            {
                this.ApplicationHighmarkApproved = LoanRepos.Get_AllHighmarkApproved(this.Range);
                this.CenterWise_ApprovedData = CenterWise_Approved();
                this.EmployeeWise_ApprovedData = EmployeeWise_Approved();
                this.RegionWise_ApprovedData = RegionWise_Approved();
            }
            else
            {
                this.ApplicationHighmarkRejected = LoanRepos.Get_AllHighmarkRejected(this.Range);
                this.CenterWise_RejectionData = CenterWise_Rejection();
                this.EmployeeWise_RejectionData = EmployeeWise_Rejected();
                this.RegionWise_RejectionData = RegionWise_Rejected();
            }
        }
        
        List<ReportModel> RegionWise_Approved()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegionId = ApplicationHighmarkApproved.Select(o => o.OriginDetail.RegionId).Distinct().ToList();
            foreach (string regionID in distinctRegionId)
            {
                List<string> distinctBranchId = ApplicationHighmarkApproved.Where(o => o.OriginDetail.RegionId == regionID).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branchId in distinctBranchId)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = regionID;
                    Item.C2 = branchId;
                    Item.C3 = LoanRepos.BranchDetailDICT[branchId].BranchName;

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> RegionAndBranch = ApplicationHighmarkApproved.Where(o => o.OriginDetail.RegionId == regionID && o.OriginDetail.BranchId == branchId).ToList();
                        List<LoanApplicationModel> Final = RegionAndBranch.Where
                            (o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> RegionWise_Rejected()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctRegionId = ApplicationHighmarkRejected.Select(o => o.OriginDetail.RegionId).Distinct().ToList();
            foreach (string regionID in distinctRegionId)
            {
                List<string> distinctBranchId = ApplicationHighmarkRejected.Where(o => o.OriginDetail.RegionId == regionID).Select(o => o.OriginDetail.BranchId).Distinct().ToList();
                foreach (string branchId in distinctBranchId)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = regionID;
                    Item.C2 = branchId;
                    Item.C3 = LoanRepos.BranchDetailDICT[branchId].BranchName;

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> RegionAndBranch = ApplicationHighmarkRejected.Where(o => o.OriginDetail.RegionId == regionID && o.OriginDetail.BranchId == branchId).ToList();
                        List<LoanApplicationModel> Final = RegionAndBranch.Where
                            (o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> EmployeeWise_Approved()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = ApplicationHighmarkApproved.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branchId in distinctBranchId)
            {
                List<string> distinctEmployeeID = ApplicationHighmarkApproved.Where(o => o.OriginDetail.BranchId == branchId && o.EmployeeId.Length > 0).Select(o => o.EmployeeId).Distinct().ToList();
                foreach (string employee in distinctEmployeeID)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branchId;
                    Item.C2 = employee;
                    Item.C3 = LoanRepos.EmployeeNameDICT[employee];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndEmployee = ApplicationHighmarkApproved.Where(o => o.OriginDetail.BranchId == branchId && o.EmployeeId == employee).ToList();
                        List<LoanApplicationModel> Final = BranchAndEmployee.Where
                            (o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> EmployeeWise_Rejected()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = ApplicationHighmarkRejected.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branchId in distinctBranchId)
            {
                List<string> distinctEmployeeID = ApplicationHighmarkRejected.Where(o => o.OriginDetail.BranchId == branchId && o.EmployeeId.Length > 0).Select(o => o.EmployeeId).Distinct().ToList();

                foreach (string employee in distinctEmployeeID)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branchId;
                    Item.C2 = employee;
                    Item.C3 = LoanRepos.EmployeeNameDICT[employee];

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndEmployee = ApplicationHighmarkRejected.Where(o => o.OriginDetail.BranchId == branchId && o.EmployeeId == employee).ToList();
                        List<LoanApplicationModel> Final = BranchAndEmployee.Where
                            (o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

                        obj.Amount = Final.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> CenterWise_Approved()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = ApplicationHighmarkApproved.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branchId in distinctBranchId)
            {
                List<string> distinctCenterID = ApplicationHighmarkApproved.Where(o => o.OriginDetail.BranchId == branchId).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string centerID in distinctCenterID)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branchId;
                    Item.C2 = centerID;
                    Item.C3 = ApplicationHighmarkApproved.Where(o => o.OriginDetail.BranchId == branchId).Select(o => o.OriginDetail.BranchName).FirstOrDefault();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndCenter = ApplicationHighmarkApproved.Where(o => o.OriginDetail.BranchId == branchId & o.OriginDetail.SHGId == centerID).ToList();
                       

                        obj.Amount = BranchAndCenter.Count();
                        Item.DataList.Add(obj);
                    }
                    FinalData.Add(Item);
                }
            }
            return FinalData;
        }
        List<ReportModel> CenterWise_Rejection()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            List<string> distinctBranchId = ApplicationHighmarkRejected.Select(o => o.OriginDetail.BranchId).Distinct().ToList();

            foreach (string branchId in distinctBranchId)
            {
                List<string> distinctCenterID = ApplicationHighmarkRejected.Where(o => o.OriginDetail.BranchId == branchId).Select(o => o.OriginDetail.SHGId).Distinct().ToList();
                foreach (string centerID in distinctCenterID)
                {
                    ReportModel Item = new ReportModel();
                    Item.C1 = branchId;
                    Item.C2 = centerID;
                    Item.C3 = ApplicationHighmarkRejected.Where(o => o.OriginDetail.BranchId == branchId).Select(o => o.OriginDetail.BranchName).FirstOrDefault();

                    for (int i = 0; i < MonthPeriods.Count; i++)
                    {
                        DateAndData obj = new DateAndData();
                        obj.FromDate = MonthPeriods[i].AddMonths(-1);
                        obj.ToDate = MonthPeriods[i];

                        List<LoanApplicationModel> BranchAndCenter = ApplicationHighmarkRejected.Where(o => o.OriginDetail.BranchId == branchId & o.OriginDetail.SHGId == centerID).ToList();
                        List<LoanApplicationModel> Final = BranchAndCenter.Where
                            (o => o.RequestedDate > obj.FromDate && o.RequestedDate <= obj.ToDate).ToList();

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
