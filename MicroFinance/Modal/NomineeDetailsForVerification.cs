using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class NomineeDetailsForVerification:BindableBase
    {

        private bool _nomineeName;
        public bool NName
        {
            get
            {
                return _nomineeName;
            }
            set
            {
                _nomineeName = value;
                RaisedPropertyChanged("NName");
            }
        }

        private bool _nomineeGender;
        public bool NomineeGender
        {
            get
            {
                return _nomineeGender;
            }
            set
            {
                _nomineeGender = value;
                RaisedPropertyChanged("NomineeGender");
            }
        }

        private bool _nomineeDOB;
        public bool NomineeDOB
        {
            get
            {
                return _nomineeDOB;
            }
            set
            {
                _nomineeDOB = value;
                RaisedPropertyChanged("NomineeDOB");
            }
        }

        private bool _nomineeContact;
        public bool NomineeContact
        {
            get
            {
                return _nomineeContact;
            }
            set
            {
                _nomineeContact = value;
                RaisedPropertyChanged("NomineeContact");
            }
        }

        private bool _nomineeOccupation;
        public bool NomineeOccupation
        {
            get
            {
                return _nomineeOccupation;
            }
            set
            {
                _nomineeOccupation = value;
                RaisedPropertyChanged("NomineeOccupation");
            }
        }

        private bool _nomineeRelationship;
        public bool NomineeRelationship
        {
            get
            {
                return _nomineeRelationship;
            }
            set
            {
                _nomineeRelationship = value;
                RaisedPropertyChanged("NomineeRelationship");
            }
        }

        private bool _nomineeDoorNo;
        public bool NomineeDoorNo
        {
            get
            {
                return _nomineeDoorNo;
            }
            set
            {
                _nomineeDoorNo = value;
                RaisedPropertyChanged("NomineeDoorNo");
            }
        }

        private bool _nomineeStreet;
        public bool NomineeStreet
        {
            get
            {
                return _nomineeStreet;
            }
            set
            {
                _nomineeStreet = value;
                RaisedPropertyChanged("NomineeStreet");
            }
        }

        private bool _nomineeLocality;
        public bool NomineeLocality
        {
            get
            {
                return _nomineeLocality;
            }
            set
            {
                _nomineeLocality = value;
                RaisedPropertyChanged("NomineeLocality");
            }
        }

        private bool _nomineeCity;
        public bool NomineeCity
        {
            get
            {
                return _nomineeCity;
            }
            set
            {
                _nomineeCity = value;
                RaisedPropertyChanged("NomineeCity");
            }
        }

        private bool _nomineeState;
        public bool NomineeState
        {
            get
            {
                return _nomineeState;
            }
            set
            {
                _nomineeState = value;
                RaisedPropertyChanged("NomineeState");
            }
        }

        private bool _nomineePincode;
        public bool NomineePincode
        {
            get
            {
                return _nomineePincode;
            }
            set
            {
                _nomineePincode = value;
                RaisedPropertyChanged("NomineePincode");
            }
        }

        private bool _nomineeAddressProof;
        public bool NomineeAddressProof
        {
            get
            {
                return _nomineeAddressProof;
            }
            set
            {
                _nomineeAddressProof = value;
                RaisedPropertyChanged("NomineeAddressProof");
            }
        }

        private bool _nomineePhotoProof;
        public bool NomineePhotoProof
        {
            get
            {
                return _nomineePhotoProof;
            }
            set
            {
                _nomineePhotoProof = value;
                RaisedPropertyChanged("NomineePhotoProof");
            }
        }

        private bool _nomineeprofilePicture;
        public bool NomineeProfilePicture
        {
            get
            {
                return _nomineeprofilePicture;
            }
            set
            {
                _nomineeprofilePicture = value;
                RaisedPropertyChanged("NomineeProfilePicture");
            }
        }

        private bool _overAllBasicDetailsofNominee;
        public bool OverAllBasicDetailsofNominee
        {
            get
            {
                return _overAllBasicDetailsofNominee;
            }
            set
            {
                _overAllBasicDetailsofNominee = value;
                RaisedPropertyChanged("OverAllBasicDetailsofNominee");
            }
        }

        private bool _overAllPhotoVerification;
        public bool OverAllNomineePhotoVerification
        {
            get
            {
                return _overAllPhotoVerification;
            }
            set
            {
                _overAllPhotoVerification = value;
                RaisedPropertyChanged("OverAllNomineePhotoVerification");
            }
        }

    }
}
