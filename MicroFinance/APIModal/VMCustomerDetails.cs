using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.APIModal
{
    public class VMCustomerDetails
    {
        public string CustId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string AadharNumber { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string Community { get; set; }
        public string Education { get; set; }
        public int FamilyMembers { get; set; }
        public int EarningMembers { get; set; }
        public string Occupation { get; set; }
        public int MonthlyIncome { get; set; }
        public int MonthlyExpenses { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string HousingType { get; set; }
        public string AddressProofName { get; set; }
        public string PhotoProofName { get; set; }
        public bool IsBankDetails { get; set; }
        public bool IsAddressProof { get; set; }
        public bool IsPhotoProof { get; set; }
        public bool IsProfilePhoto { get; set; }
        public string BankACHolderName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }
        public byte[] AddressProof { get; set; }
        public byte[] PhotoProof { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public bool GuarenteeStatus { get; set; }
        public bool NomineeStatus { get; set; }
        public int CustomerStatus { get; set; }
        public bool IsActive { get; set; }
        public string HusbandName { get; set; }
        public int YearlyIncome { get; set; }
        public bool IsCombinePhoto { get; set; }
        public byte[] CombinePhoto { get; set; }
        public string PhotoProofNo { get; set; }
        public string AddressProofNo { get; set; }
        public string Residency { get; set; }
        public string LandHolding { get; set; }
        public string LandVolume { get; set; }

    }
}
