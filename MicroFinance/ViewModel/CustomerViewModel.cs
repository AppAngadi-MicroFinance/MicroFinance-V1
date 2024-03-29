﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CustomerViewModel
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string GroupID { get; set; }
        public string CenterId { get; set; }
        public string CenterName { get; set; }
        public int ActiveLoans { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BranchName { get; set; }
        public string PhoneNumber { get; set; }
        public Bitmap ProfilePhoto { get; set; }
    }
}
