using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class CollectionSheetModelPDF : CollectionShceduleSheet
    {
        string EmpID;
        public string Branch { get; set; }
        public string Center { get; set; }
        public DateTime Date { get; set; }
        public string Day
        {
            get
            {
                return Date.DayOfWeek.ToString().ToUpper();
            }
        }

        public List<CollectionShceduleSheet> CollectionList { get; set; }
        public List<GroupWiseDetails> GroupWiseTotal = new List<GroupWiseDetails>();

        public CollectionSheetModelPDF(string empID, string day)
        {
            CollectionList = GetActiveLoanCustomer(empID, day);
            GetGroupSUM();
        }

        void GetGroupSUM()
        {
            List<string> PeerGroups = CollectionList.Select(o => o.GroupId).Distinct().ToList();

            foreach(string item in PeerGroups)
            {
                int sum = 0;
                foreach (CollectionShceduleSheet cItem in CollectionList)
                {
                    if(cItem.GroupId == item)
                    {
                        sum += cItem.TotalAmonut;
                    }
                }
                GroupWiseDetails obj = new GroupWiseDetails(item, sum);
                GroupWiseTotal.Add(obj);
            }
        }
    }
}
