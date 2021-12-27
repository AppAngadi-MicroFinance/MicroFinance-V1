using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.APIModal
{
    public class CustomerEnrollDataViewModel
    {
        //Basic Details
        public string CustomerID { get; set; }
        public string BranchID { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GuardianName { get; set; }
        public string ContactNumber { get; set; }
        public string Religion { get; set; }
        public string Community { get; set; }
        public string Caste { get; set; }
        public string Education { get; set; }
        //Family Details
        public int FamilyMembers { get; set; }
        public int EarningMembers { get; set; }
        public string Occupation { get; set; }
        public int MonthlyIncome { get; set; }
        public int MonthlyExpence { get; set; }
        public int FamilyYearlyIncome { get; set; }
        //Address Details
        public string DoorNo { get; set; }
        public string StreetName { get; set; }
        public string Village { get; set; }
        public string Taluk { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

        //Proof Details

        public string AadharNumber { get; set; }
        public string AddressProofName { get; set; }
        public string AddressProofNumber { get; set; }
        public string PhotoProofName { get; set; }
        public string PhotoProofNumber { get; set; }
        //Land Details
        public string HousingType { get; set; }
        public string LandHolding { get; set; }
        public string LandType { get; set; }
        public string LandVolume { get; set; }
        //Customer Account Details
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }
        public string AccountNumber { get; set; }

        //Proof Details
        public byte[] AddressProof { get; set; }
        public byte[] PhotoProof { get; set; }
        public byte[] CombinePhoto { get; set; }

        //Guarantor Details
        public string GName { get; set; }
        public string GGender { get; set; }
        public DateTime GDateOfBirth { get; set; }
        public int GAge { get; set; }
        public string GContactNumber { get; set; }
        public string GOccupation { get; set; }
        public string GRelationShip { get; set; }
        public string GDoorNo { get; set; }
        public string GStreetName { get; set; }
        public string GVillage { get; set; }
        public string GTaluk { get; set; }
        public string GCity { get; set; }
        public string GState { get; set; }
        public string GPinCode { get; set; }
        public string GAddressProofName { get; set; }
        public string GAddressProofNumber { get; set; }
        public string GPhotoProofName { get; set; }
        public string GPhotoProofNumber { get; set; }

        //Nominee Details
        public string NName { get; set; }
        public string NGender { get; set; }
        public DateTime NDateOfBirth { get; set; }
        public int NAge { get; set; }
        public string NContactNumber { get; set; }
        public string NOccupation { get; set; }
        public string NRelationShip { get; set; }
        public string NDoorNo { get; set; }
        public string NStreetName { get; set; }
        public string NVillage { get; set; }
        public string NTaluk { get; set; }
        public string NCity { get; set; }
        public string NState { get; set; }
        public string NPinCode { get; set; }
        public string NAddressProofName { get; set; }
        public string nAddressProofNumber { get; set; }
        public string NPhotoProofName { get; set; }
        public string NPhotoProofNumber { get; set; }

        //RequestDetails
        public string EnrollBy { get; set; }
        public DateTime CustEnrollDate { get; set; }
        public bool IsEnrolled { get; set; }

        //CustomerGroupDetails
        public string PeerGroupID { get; set; }
        public string SHGId { get; set; }
        public bool Isleader { get; set; }
        public int CPid { get; set; }


        //LoanDetails
        public string RequestID { get; set; }
        public int LoanAmount { get; set; }
        public string LoanType { get; set; }
        public int LoanPeriod { get; set; }
        public string Purpose { get; set; }
        public DateTime LoanEnrollDate { get; set; }
        public int LoanStatus { get; set; }
        public string Remark { get; set; }
        public string EmployeeID { get; set; }

    }
}
