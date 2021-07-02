using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class Notification
    {
        public Guarantor GuarantorObj { get; set; }
        public Nominee NomineeObj { get; set; }
        public Customer CustomerObj { get; set; }

        public string CustomerId { get; set; }
        public string EmpId { get; set; }
        
        public string NotificationFrom { get; set; }
        public string BranchName { get; set; }
        public string NotificationPurpose { get; set; }
        public string CustomerStatus { get; set; }
        public string NotificationDate { get; set; }


       

    }
}
