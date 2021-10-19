using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public enum LoanStatusCode
    {
        Requested=1,
        WaithingForHimarkResult=2,
        HimarkApprove=3,
        HimarkRejecct=4,
        HimarkRetain=5,
        FODocumentVerify=6,
        ACDoucumentVerify=7,
        BMDoucumentVerify=8,
        BMRecommend=9,
        RMRecommend=10,
        SAMURequest=11,
        LoanApproval=12,
        LoanReject=13,
        LoanDisbursment=14
    }
}
