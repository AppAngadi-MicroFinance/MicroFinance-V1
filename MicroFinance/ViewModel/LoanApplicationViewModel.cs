using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class LoanApplicationViewModel
    {
        public string  CustomerId { get; set; }
        public string CustomerName { get; set; }
        
        public string LoanType { get; set; }
        public int StatusCode { get; set; }
        public int LoanAmount { get; set; }
        public int LoanPeriod { get; set; }
        public DateTime EnrollDate { get; set; }
        public string LoanPurpose { get; set; }
        public string RequestedBy { get; set; }
        public string LoanStatus
        { 
            get
            {
                return GetLoanStatus(StatusCode);
            }
        }


        private string GetLoanStatus(int StatusCode)
        {
            string Result = "";

            switch(StatusCode)
            {
                case 1:
                    Result = "REQUESTED";
                    break;
                case 2:
                    Result = "Himark Result";
                    break;
                case 3:
                    Result = "FO Doc. Verification";
                    break;
                case 4:
                    Result = "Reject From Himark";
                    break;
                case 5:
                    Result = "Retain from Himark";
                    break;
                case 6:
                    Result = "AC Doc. Verification";
                    break;
                case 7:
                    Result = "BM Doc. Verification";
                    break;
                case 8:
                    Result = "BM Recommend";
                    break;
                case 9:
                    Result = "RM Recommend";
                    break;
                case 10:
                    Result = "SAMU Waiting";
                    break;
                case 11:
                    Result = "SAMU Result";
                    break;
                case 12:
                    Result = "Loan Approved";
                    break;
                case 13:
                    Result = "Loan Reject";
                    break;
                case 14:
                    Result = "Loan Disbursment";
                    break;
                default:
                    Result = "UNKNOWN";
                    break;
            }
            return Result;
        }
    }
}
