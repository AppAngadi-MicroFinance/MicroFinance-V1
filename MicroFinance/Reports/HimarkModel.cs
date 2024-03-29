﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;

namespace MicroFinance.Reports
{
    public class HimarkModel : BindableBase
    {
        private string _CBOname;
        public string CBOName
        {
            get
            {
                return _CBOname;
            }
            set
            {
                _CBOname = value;
                RaisedPropertyChanged("CBOName");

            }
        }
        private string _FIGname = "G Trust";
        public string FIGName
        {
            get
            {
                return _FIGname;
            }
            set
            {
                _FIGname = value;
                RaisedPropertyChanged("FIGName");
            }
        }
        private string _FIGformationdate;
        public string FIGFormationDate
        {
            get
            {
                return _FIGformationdate;
            }
            set
            {
                _FIGformationdate = value;
                RaisedPropertyChanged("FIGFormationDate");
            }
        }
        private string _FIGvillage;
        public string FIGVillage
        {
            get
            {
                return _FIGvillage;
            }
            set
            {
                _FIGvillage = value;
                RaisedPropertyChanged("FIGVillage");
            }
        }
        private string _FIGtaluk;
        public string FIGTaluk
        {
            get
            {
                return _FIGtaluk;
            }
            set
            {
                _FIGtaluk = value;
                RaisedPropertyChanged("FIGTaluk");
            }
        }
        private string _FIGdistrict;
        public string FIGDistrict
        {
            get
            {
                return _FIGdistrict;
            }
            set
            {
                _FIGdistrict = value;
                RaisedPropertyChanged("FIGDistrict");
            }
        }
        private string _FIGstate;
        public string FIGState
        {
            get
            {
                return _FIGstate;
            }
            set
            {
                _FIGstate = value;
                RaisedPropertyChanged("FIGState");
            }
        }
        private string _FIGsaving;
        public string FIGSavings
        {
            get
            {
                return _FIGsaving;
            }
            set
            {
                _FIGsaving = value;
                RaisedPropertyChanged("FIGSavings");
            }
        }
        private string _FIGbankname;
        public string FIGBankName
        {
            get
            {
                return _FIGbankname;
            }
            set
            {
                _FIGbankname = value;
                RaisedPropertyChanged("FIGBankName");
            }
        }
        private string _FIGaccountno;
        public string FIGAccountNO
        {
            get
            {
                return _FIGaccountno;
            }
            set
            {
                _FIGaccountno = value;
                RaisedPropertyChanged("FIGAccountNO");
            }
        }
        private string _FIGbankIFSC;
        public string FIGBankIFSC
        {
            get
            {
                return _FIGbankIFSC;
            }
            set
            {
                _FIGbankIFSC = value;
                RaisedPropertyChanged("FIGBankIFSC");
            }
        }
        private string _FIGbranchname;
        public string FIGBranchName
        {
            get
            {
                return _FIGbranchname;
            }
            set
            {
                _FIGbranchname = value;
                RaisedPropertyChanged("FIGBranchName");
            }
        }
        private string _eligibleloanamount;
        public string EligibleLoanAmount
        {
            get
            {
                return _eligibleloanamount;
            }
            set
            {
                _eligibleloanamount = value;
                RaisedPropertyChanged("EligibleLoanAmount");
            }
        }
        private string _applicationno;
        public string ApplicationNo
        {
            get
            {
                return _applicationno;
            }
            set
            {
                _applicationno = value;
                RaisedPropertyChanged("ApplicationNo");
            }
        }
        private string _empid;
        public string EmpID
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;
                RaisedPropertyChanged("EmpID");
            }
        }
        private string _RMname;
        public string RMName
        {
            get
            {
                return _RMname;
            }
            set
            {
                _RMname = value;
                RaisedPropertyChanged("RMName");
            }
        }
        private DateTime _dateofapplication;
        public DateTime DateOfApplication
        {
            get
            {
                return _dateofapplication;
            }
            set
            {
                _dateofapplication = value;
                RaisedPropertyChanged("DateOfApplication");
            }
        }
        private string _member;
        public string Member
        {
            get
            {
                return _member;
            }
            set
            {
                _member = value;
                RaisedPropertyChanged("Member");
            }
        }
        private string _applicantname;
        public string ApplicantName
        {
            get
            {
                return _applicantname;
            }
            set
            {
                _applicantname = value;
                RaisedPropertyChanged("ApplicantName");
            }
        }
        private string _applicantgender;
        public string ApplicantGender
        {
            get
            {
                return _applicantgender;
            }
            set
            {
                _applicantgender = value;
                RaisedPropertyChanged("ApplicantGender");
            }
        }
        private string _applicantmobile;
        public string ApplicantMobile
        {
            get
            {
                return _applicantmobile;
            }
            set
            {
                _applicantmobile = value;
                RaisedPropertyChanged("ApplicantMobile");
            }
        }
        private string _applicantDOB;
        public string ApplicantDOB
        {
            get
            {
                return _applicantDOB;
            }
            set
            {
                _applicantDOB = value;
                RaisedPropertyChanged("ApplicantDOB");
            }
        }
        private int _applicantage;
        public int ApplicantAge
        {
            get
            {
                return _applicantage;
            }
            set
            {
                _applicantage = value;
                RaisedPropertyChanged("ApplicantAge");
            }
        }
        private string _applicantIDprooftype;
        public string ApplicantIDProofType
        {
            get
            {
                return _applicantIDprooftype;
            }
            set
            {
                _applicantIDprooftype = value;
                RaisedPropertyChanged("ApplicantIDProofType");
            }
        }
        private string _Applicantidproofno;
        public string ApplicantIDProofNo
        {
            get
            {
                return _Applicantidproofno;
            }
            set
            {
                _Applicantidproofno = value;
                RaisedPropertyChanged("ApplicantIDProofNo");
            }
        }
        private string _applicantAddressprooftype;
        public string ApplicantAddressProofType
        {
            get
            {
                return _applicantAddressprooftype;
            }
            set
            {
                _applicantAddressprooftype = value;
                RaisedPropertyChanged("ApplicantAddressProofType");
            }
        }
        private string _ApplicantAddressproofno;
        public string ApplicantAddressProofNo
        {
            get
            {
                return _ApplicantAddressproofno;
            }
            set
            {
                _ApplicantAddressproofno = value;
                RaisedPropertyChanged("ApplicantAddressProofNo");
            }
        }
        private string _coapplicantname;
        public string COapplicantName
        {
            get
            {
                return _coapplicantname;
            }
            set
            {
                _coapplicantname = value;
                RaisedPropertyChanged("COapplicantName");
            }
        }
        private string _coapplicantgender;
        public string COapplicantGender
        {
            get
            {
                return _coapplicantgender;
            }
            set
            {
                _coapplicantgender = value;
                RaisedPropertyChanged("COapplicantGender");
            }
        }
        private string _coapplicantDOB;
        public string COapplicantDOB
        {
            get
            {
                return _coapplicantDOB;
            }
            set
            {
                _coapplicantDOB = value;
                RaisedPropertyChanged("COapplicantDOB");
            }
        }
        private int _coapplicantage;
        public int COapplicantAge
        {
            get
            {
                return _coapplicantage;
            }
            set
            {
                _coapplicantage = value;
                RaisedPropertyChanged("COapplicantAge");
            }
        }
        private string _coapplicantIDprooftype;
        public string COapplicantIDProofType
        {
            get
            {
                return _coapplicantIDprooftype;
            }
            set
            {
                _coapplicantIDprooftype = value;
                RaisedPropertyChanged("COapplicantIDProofType");
            }
        }
        private string _coapplicantidproofno;
        public string COapplicantIDProofNo
        {
            get
            {
                return _coapplicantidproofno;
            }
            set
            {
                _coapplicantidproofno = value;
                RaisedPropertyChanged("COapplicantIDProofNo");
            }
        }
        private string _coapplicantaddressprooftype;
        public string COapplicantAddressProofType
        {
            get
            {
                return _coapplicantaddressprooftype;
            }
            set
            {
                _coapplicantaddressprooftype = value;
            }
        }
        private string _coapplicantaddressproofno;
        public string COapplicantAddressProofNo
        {
            get
            {
                return _coapplicantaddressproofno;
            }
            set
            {
                _coapplicantaddressproofno = value;
                RaisedPropertyChanged("COapplicantAddressProofNo");
            }
        }
        private string _relationshipWithCOapplicant;
        public string RelationshipWithCOapplicant
        {
            get
            {
                return _relationshipWithCOapplicant;
            }
            set
            {
                _relationshipWithCOapplicant = value;
                RaisedPropertyChanged("RelationshipWithCOapplicant");
            }
        }
        private string _relationshipWithApplicant;
        public string RelationshipWithApplicant
        {
            get
            {
                return _relationshipWithApplicant;
            }
            set
            {
                _relationshipWithApplicant = value;
                RaisedPropertyChanged("RelationshipWithApplicant");
            }
        }
        private string _applicantFatherName;
        public string ApplicantFatherName
        {
            get
            {
                return _applicantFatherName;
            }
            set
            {
                _applicantFatherName = value;
                RaisedPropertyChanged("ApplicantFatherName");
            }
        }
        private string _applicantMotherName;
        public string ApplicantMotherName
        {
            get
            {
                return _applicantMotherName;
            }
            set
            {
                _applicantMotherName = value;
                RaisedPropertyChanged("ApplicantMotherName");
            }
        }
        private string _doorNo;
        public string DoorNO
        {
            get
            {
                return _doorNo;
            }
            set
            {
                _doorNo = value;
                RaisedPropertyChanged("DoorNO");
            }
        }
        private string _streetname;
        public string StreetName
        {
            get
            {
                return _streetname;
            }
            set
            {
                _streetname = value;
                RaisedPropertyChanged("StreetName");
            }
        }
        private string _village;
        public string Village
        {
            get
            {
                return _village;
            }
            set
            {
                _village = value;
                RaisedPropertyChanged("Village");
            }
        }
        private string _taluk;
        public string Taluk
        {
            get
            {
                return _taluk;
            }
            set
            {
                _taluk = value;
                RaisedPropertyChanged("Taluk");
            }
        }
        private string _district;
        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
                RaisedPropertyChanged("District");
            }
        }
        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                RaisedPropertyChanged("State");
            }
        }
        private string _pincode;
        public string Pincode
        {
            get
            {
                return _pincode;
            }
            set
            {
                _pincode = value;
                RaisedPropertyChanged("Pincode");
            }
        }
        private string _occupation;
        public string Occupation
        {
            get
            {
                return _occupation;
            }
            set
            {
                _occupation = value;
                RaisedPropertyChanged("Occupation");
            }
        }
        private string _education;
        public string Education
        {
            get
            {
                return _education;
            }
            set
            {
                _education = value;
                RaisedPropertyChanged("Education");
            }
        }
        private string _place;
        public string Place
        {
            get
            {
                return _place;
            }
            set
            {
                _place = value;
                RaisedPropertyChanged("Place");
            }
        }
        private string _residence;
        public string Residence
        {
            get
            {
                return _residence;
            }
            set
            {
                _residence = value;
                RaisedPropertyChanged("Residence");

            }
        }
        private string _typeofResidence;
        public string TypeOFResidence
        {
            get
            {
                return _typeofResidence;
            }
            set
            {
                _typeofResidence = value;
                RaisedPropertyChanged("TypeOFResidence");
            }
        }
        private string _landholding;
        public string LandHolding
        {
            get
            {
                return _landholding;
            }
            set
            {
                _landholding = value;
                RaisedPropertyChanged("LandHolding");
            }
        }
        private string _caste;
        public string Caste
        {
            get
            {
                return _caste;
            }
            set
            {
                _caste = value;
                RaisedPropertyChanged("Caste");
            }
        }
        private string _religion;
        public string Religion
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                RaisedPropertyChanged("Religion");
            }
        }
        private string _monthlyincome;
        public string MonthlyIncome
        {
            get
            {
                return _monthlyincome;
            }
            set
            {
                _monthlyincome = value;
                RaisedPropertyChanged("MonthlyIncome");
            }
        }
        private string _monthlyexpenses;
        public string MonthlyExpenses
        {
            get
            {
                return _monthlyexpenses;
            }
            set
            {
                _monthlyexpenses = value;
                RaisedPropertyChanged("MonthlyExpenses");
            }
        }
        private string _appBankName;
        public string ApplicantBankName
        {
            get
            {
                return _appBankName;
            }
            set
            {
                _appBankName = value;
                RaisedPropertyChanged("ApplicantBankName");
            }
        }
        private string _appAccountNo;
        public string ApplicantAccountNO
        {
            get
            {
                return _appAccountNo;
            }
            set
            {
                _appAccountNo = value;
                RaisedPropertyChanged("ApplicantAccountNO");

            }
        }
        private string _appIFSCcode;
        public string ApplicantIFSCcode
        {
            get
            {
                return _appIFSCcode;
            }
            set
            {
                _appIFSCcode = value;
                RaisedPropertyChanged("ApplicantIFSCcode");

            }
        }
        private string _appBranchName;
        public string ApplicantBranchName
        {
            get
            {
                return _appBranchName;
            }
            set
            {
                _appBranchName = value;
                RaisedPropertyChanged("ApplicantBranchName");
            }
        }
        private string _loanpurpose;
        public string LoanPurpose
        {
            get
            {
                return _loanpurpose;
            }
            set
            {
                _loanpurpose = value;
                RaisedPropertyChanged("LoanPurpose");
            }
        }
        private string _loantenure;
        public string LoanTenure
        {
            get
            {
                return _loantenure;
            }
            set
            {
                _loantenure = value;
                RaisedPropertyChanged("LoanTenure");
            }
        }
        private string _samfinBranch;
        public string SamFinBranch
        {
            get
            {
                return _samfinBranch;
            }
            set
            {
                _samfinBranch = value;
                RaisedPropertyChanged("SamFinBranch");
            }
        }
        private string _repaymentSchedule;
        public string RepaymentSchedule
        {
            get
            {
                return _repaymentSchedule;
            }
            set
            {
                _repaymentSchedule = value;
                RaisedPropertyChanged("RepaymentSchedule");
            }
        }
        private string _security;
        public string Security
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
                RaisedPropertyChanged("Security");
            }
        }
        private string _fulladdress;
        public string FullAddress
        {
            get
            {
                return _fulladdress;
            }
            set
            {
                _fulladdress = value;
                RaisedPropertyChanged("FullAddress");
            }
        }
        private string _monthlysurplus;
        public string MonthlySurplus
        {
            get
            {
                return _monthlysurplus;
            }
            set
            {
                _monthlysurplus = value;
                RaisedPropertyChanged("MonthlySurplus");
            }
        }
        private string _intrestrate;
        public string IntrestRate
        {
            get
            {
                return _intrestrate;
            }
            set
            {
                _intrestrate = value;
                RaisedPropertyChanged("IntrestRate");
            }
        }
        private string _Lpf;
        public string LPF
        {
            get
            {
                return _Lpf;
            }
            set
            {
                _Lpf = value;
                RaisedPropertyChanged("LPF");
            }
        }
        private string _documentationCharges;
        public string DocumentationCharges
        {
            get
            {
                return _documentationCharges;
            }
            set
            {
                _documentationCharges = value;
                RaisedPropertyChanged("DocumentationCharges");
            }
        }
        private string _penalCharges;
        public string PenalChanges
        {
            get
            {
                return _penalCharges;
            }
            set
            {
                _penalCharges = value;
                RaisedPropertyChanged("PenalChanges");
            }
        }
        private string _guarantee;
        public string Guarantee
        {
            get
            {
                return _guarantee;
            }
            set
            {
                _guarantee = value;
                RaisedPropertyChanged("Guarantee");
            }
        }
        private string _gtliPremiumAmount;
        public string GTLIPremiumAmount
        {
            get
            {
                return _gtliPremiumAmount;
            }
            set
            {
                _gtliPremiumAmount = value;
                RaisedPropertyChanged("GTLIPremiumAmount");
            }
        }
        private string _paiPremiumAmount;
        public string PAIPermiumAmount
        {
            get
            {
                return _paiPremiumAmount;
            }
            set
            {
                _paiPremiumAmount = value;
                RaisedPropertyChanged("PAIPermiumAmount");
            }
        }
        private string _insuranceAmount;
        public string InsuranceAmount
        {
            get
            {
                return _insuranceAmount;
            }
            set
            {
                _insuranceAmount = value;
                RaisedPropertyChanged("InsuranceAmount");
            }
        }
        private string _hmReferenceNo;
        public string HMreferenceNo
        {
            get
            {
                return _hmReferenceNo;
            }
            set
            {
                _hmReferenceNo = value;
                RaisedPropertyChanged("HMreferenceNo");
            }
        }
        private string _slRefNo;
        public string SLreferenceNo
        {
            get
            {
                return _slRefNo;
            }
            set
            {
                _slRefNo = value;
                RaisedPropertyChanged("SLreferenceNo");
            }
        }
        private string _slDated;
        public string SLdated
        {
            get
            {
                return _slDated;
            }
            set
            {
                _slDated = value;
                RaisedPropertyChanged("SLdated");
            }
        }
    }
}
