using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class GuarantorDetailsForVerification:BindableBase
    {
      
        private bool _guarantorName;
        public bool GName
        {
            get
            {
                return _guarantorName;
            }
            set
            {
                _guarantorName = value;
                RaisedPropertyChanged("GName");
            }
        }

        private bool _guarantorGender;
        public bool GuarantorGender
        {
            get
            {
                return _guarantorGender;
            }
            set
            {
                _guarantorGender = value;
                RaisedPropertyChanged("GuarantorGender");
            }
        }

        private bool _guarantorDOB;
        public bool GuarantorDOB
        {
            get
            {
                return _guarantorDOB;
            }
            set
            {
                _guarantorDOB = value;
                RaisedPropertyChanged("GuarantorDOB");
            }
        }

        private bool _guarantorContact;
        public bool GuarantorContact
        {
            get
            {
                return _guarantorContact;
            }
            set
            {
                _guarantorContact = value;
                RaisedPropertyChanged("GuarantorContact");
            }
        }

        private bool _guarantorOccupation;
        public bool GuarantorOccupation
        {
            get
            {
                return _guarantorOccupation;
            }
            set
            {
                _guarantorOccupation = value;
                RaisedPropertyChanged("GuarantorOccupation");
            }
        }

        private bool _guarantorRelationship;
        public bool GuarantorRelationship
        {
            get
            {
                return _guarantorRelationship;
            }
            set
            {
                _guarantorRelationship = value;
                RaisedPropertyChanged("GuarantorRelationship");
            }
        }

        private bool _guarantorDoorNumber;
        public bool GuarantorDoorNumber
        {
            get
            {
                return _guarantorDoorNumber;
            }
            set
            {
                _guarantorDoorNumber = value;
                RaisedPropertyChanged("GuarantorDoorNumber");
            }
        }

        private bool _guarantorStreet;
        public bool GuarantorStreet
        {
            get
            {
                return _guarantorStreet;
            }
            set
            {
                _guarantorStreet = value;
                RaisedPropertyChanged("GuarantorStreet");
            }
        }

        private bool _guarantorLocality;
        public bool GuarantorLocality
        {
            get
            {
                return _guarantorLocality;
            }
            set
            {
                _guarantorLocality = value;
                RaisedPropertyChanged("GuarantorLocality");
            }
        }

        private bool _guarantorCity;
        public bool GuarantorCity
        {
            get
            {
                return _guarantorCity;
            }
            set
            {
                _guarantorCity = value;
                RaisedPropertyChanged("GuarantorCity");
            }
        }

        private bool _guarantorState;
        public bool GuarantorState
        {
            get
            {
                return _guarantorState;
            }
            set
            {
                _guarantorState = value;
                RaisedPropertyChanged("GuarantorState");
            }
        }

        private bool _guarantorPincode;
        public bool GuarantorPincode
        {
            get
            {
                return _guarantorPincode ;
            }
            set
            {
                _guarantorPincode = value;
                RaisedPropertyChanged("GuarantorPincode");
            }
        }

       

        private bool _guarantorAddressProof;
        public bool GuarantorAddressProof
        {
            get
            {
                return _guarantorAddressProof;
            }
            set
            {
                _guarantorAddressProof = value;
                RaisedPropertyChanged("GuarantorAddressProof");
            }
        }

        private bool _guarantorPhtoProof;
        public bool GuarantorPhotoProof
        {
            get
            {
                return _guarantorPhtoProof;
            }
            set
            {
                _guarantorPhtoProof = value;
                RaisedPropertyChanged("GuarantorPhotoProof");
            }
        }

        private bool _guarantorProfilePicture;
        public bool GuarantorProfilePicture
        {
            get
            {
                return _guarantorProfilePicture;
            }
            set
            {
                _guarantorProfilePicture = value;
                RaisedPropertyChanged("GuarantorProfilePicture");
            }
        }

      

    }
}
