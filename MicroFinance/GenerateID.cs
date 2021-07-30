using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;

namespace MicroFinance
{
    public class GenerateID
    {
        public static string GenerateRegionID()
        {
            string finalID = "R" + DatabaseMethods.GetRegionsCount();
            return finalID;
        }

        public string GenerateBranchID(string regionId)// IDPattern 01202106001 (01-RegionNumber/2021-CurrentYear/06-CurrentMonth/001-(CountOfBranch+1))
        {
            string Result = "";
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            Result = regionId + year + "" + month + "" + day + DatabaseMethods.GetBranchCount();
            return Result;
        }
        public string DigitConvert(string digit, int place = 3)
        {
            StringBuilder sb = new StringBuilder();
            string number = digit;
            string Result = "";
            if (number.Length < place)
            {
                for (int i = 0; i < (place - (number.Length)); i++)
                {
                    sb.Append(0);
                }
                Result = sb.ToString() + number;
            }
            else
            {
                Result = number;
            }

            return Result;
        }
    }
}
