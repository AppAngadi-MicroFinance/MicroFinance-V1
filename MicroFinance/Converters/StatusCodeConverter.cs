using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    class StatusCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string State = (string)value;
            return GetStatus(State);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



        public string GetStatus(string Value)
        {
            string Result = "";

            switch(Value)
            {
                case "Requested":
                    return "Requested";
                case "WaithingForHimarkResult":
                    return "Waiting For Himark Result";
                case "HimarkApprove":
                    return "Himark-Approve";
                case "HimarkRejecct":
                    return "Himark-Reject";
                case "HimarkRetain":
                    return "Himark-Retain";
                case "FODocumentVerify":
                    return "Document-Verify (FO)";
                case "ACDoucumentVerify":
                    return "Document-Verify (AC)";
                case "BMDoucumentVerify":
                    return "Document-Verify (BM)";
                case "BMRecommend":
                    return "Recommend (BM)";
                case "RMRecommend":
                    return "Recommend (RM)";
                case "SAMURequest":
                    return "Waiting For Samu Approval";
                case "LoanApproval":
                    return "Loan-Appproved";
                case "LoanReject":
                    return "Loan-Reject";
                case "LoanDisbursment":
                    return "Loan-Disposed";
            }

            return Result;
        }
    }
}
