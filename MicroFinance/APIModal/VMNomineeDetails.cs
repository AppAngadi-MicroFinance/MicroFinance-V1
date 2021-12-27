using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.APIModal
{
    public class VMNomineeDetails
    {
        public string CustId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public string Occupation { get; set; }
        public string RelationShip { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string AddressProofName { get; set; }
        public string PhotoProofName { get; set; }
        public bool IsAddressProof { get; set; }
        public bool IsPhotoProof { get; set; }
        public bool IsProfilePhoto { get; set; }
        public byte[] AddressProof { get; set; }
        public byte[] PhotoProof { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string Gender { get; set; }
        public string AddressProofNumber { get; set; }
        public string PhotoProofNumber { get; set; }


        //Address Details

        public string DoorNo { get; set; }
        public string StreetName { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
