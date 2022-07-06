using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using MicroFinance.Modal;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Collections;

namespace MicroFinance.Reports
{
    public class HiMark : BindableBase
    {
        public static StringBuilder TimeBuilder = new StringBuilder();
        public HiMark()
        {

        }
        
        LoanProcess loandetails = new LoanProcess();
        Guarantor Guarantordetails = new Guarantor();
        public List<HiMark> hiMarksList = new List<HiMark>();
        public HiMark(LoanProcess loan)
        {
            this.loandetails = loan;
            Guarantordetails._customerId = loandetails._customerId;
            Guarantordetails.GetGuranteeDetails();
        }
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
            private DateTime _FIGformationdate;
            public DateTime FIGFormationDate
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
            private string _dateofapplication;
            public string DateOfApplication
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
            private DateTime _applicantDOB;
            public DateTime ApplicantDOB
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
            private DateTime _coapplicantDOB;
            public DateTime COapplicantDOB
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
            //private string _streetname;
            //public string StreetName
            //{
            //    get
            //    {
            //        return _streetname;
            //    }
            //    set
            //    {
            //        _streetname = value;
            //    RaisedPropertyChanged("StreetName");
            //    }
            //}
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
            //private string _state;
            //public string State
            //{
            //    get
            //    {
            //        return _state;
            //    }
            //    set
            //    {
            //        _state = value;
            //    RaisedPropertyChanged("State");
            //    }
            //}
            //private string _pincode;
            //public string Pincode
            //{
            //    get
            //    {
            //        return _pincode;
            //    }
            //    set
            //    {
            //        _pincode = value;
            //    RaisedPropertyChanged("Pincode");
            //    }
            //}
            //private string _occupation;
            //public string Occupation
            //{
            //    get
            //    {
            //        return _occupation;
            //    }
            //    set
            //    {
            //        _occupation = value;
            //    RaisedPropertyChanged("Occupation");
            //    }
            //}
            //private string _education;
            //public string Education
            //{
            //    get
            //    {
            //        return _education;
            //    }
            //    set
            //    {
            //        _education = value;
            //    RaisedPropertyChanged("Education");
            //    }
            //}
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
            //private string _caste;
            //public string Caste
            //{
            //    get
            //    {
            //        return _caste;
            //    }
            //    set
            //    {
            //        _caste = value;
            //    RaisedPropertyChanged("Caste");
            //    }
            //}
            //private string _religion;
            //public string Religion
            //{
            //    get
            //    {
            //        return _religion;
            //    }
            //    set
            //    {
            //        _religion = value;
            //    RaisedPropertyChanged("Religion");
            //    }
            //}
            //private string _monthlyincome;
            //public string MonthlyIncome
            //{
            //    get
            //    {
            //        return _monthlyincome;
            //    }
            //    set
            //    {
            //        _monthlyincome = value;
            //    RaisedPropertyChanged("MonthlyIncome");
            //    }
            //}
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
            //private string _loanpurpose;
            //public string LoanPurpose
            //{
            //    get
            //    {
            //        return _loanpurpose;
            //    }
            //    set
            //    {
            //        _loanpurpose = value;
            //    RaisedPropertyChanged("LoanPurpose");
            //    }
            //}
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
        //public HiMark(string cboname, string figname, DateTime figformationdate, string figvillage, string figtaluk, string figdistrict, string figstate,
        //    string figsavings, string figbankname, string figaccountno, string figbankifsc, string figbranchname, string eligibleloanamount, string applicationno, string empid,
        //    string rmname, string dateofapp, string member, string applicantname, string applicantgender, string appmobileno, string applicantdob, string applicantage,
        //    string appidprooftype, string appidproofno, string appaddressprooftype, string appaddressproofno, string coappname, string coappgender, string coappdob,
        //    string coappage, string coappidprooftype, string coappidproofno, string coappaddressprofftype, string coappaddressproofno, string apprelationtype,
        //    string coapprelationshiptype, string appfathername, string appmothername, string doorno, string streetname, string village, string taluk, string district,
        //    string state, string pincode, string occupation, string education, string place, string residence, string typeeofresidence, string landholding, string caste,
        //    string religion, string monthlyincome, string monthlyexpenses, string appbankname, string appaccountno, string appifsccodde, string appbranchname,
        //    string loanpurpose, string loantenure, string samfinbranch, string repaymentschedule, string security, string fulladdress, string monthlysurplus, string intrestrate,
        //    string lpf, string documentationcharges, string penalcharges, string guarantee, string gtlipremiumamt, string paipremiumamt, string insuranceamt, string hmreference,
        //    string slrefno, string sldated)
        //{
        //    CBOName = cboname;
        //    FIGName = figname;
        //    FIGFormationDate = figformationdate;
        //    FIGVillage = figvillage;
        //    FIGTaluk = figtaluk;
        //    FIGDistrict = figdistrict;
        //    FIGState = figstate;
        //    FIGSavings = figsavings;
        //    FIGBankName = figbankname;
        //    FIGAccountNO = figaccountno;
        //    FIGBankIFSC = figbankifsc;
        //    FIGBranchName = figbranchname;
        //    EligibleLoanAmount = eligibleloanamount;
        //    ApplicationNo = applicationno;
        //    EmpID = empid;
        //    RMName = rmname;
        //    DateOfApplication = dateofapp;
        //    Member = member;
        //    ApplicantName = applicantname;
        //    ApplicantGender = applicantgender;
        //    ApplicantMobile = appmobileno;
        //    ApplicantDOB = applicantdob;
        //    ApplicantAge = applicantage;
        //    ApplicantIDProofType = appidprooftype;
        //    ApplicantIDProofNo = appidproofno;
        //    ApplicantAddressProofType = appaddressprooftype;
        //    ApplicantAddressProofNo = appaddressprooftype;
        //    COapplicantName = coappname;
        //    COapplicantGender = coappgender;
        //    COapplicantDOB = coappdob;
        //    COapplicantAge = coappage;
        //    COapplicantIDProofType = coappidprooftype;
        //    COapplicantIDProofNo = coappidproofno;
        //    COapplicantAddressProofType = coappaddressprofftype;
        //    COapplicantAddressProofNo = coappaddressproofno;
        //    RelationshipWithApplicant = apprelationtype;
        //    RelationshipWithCOapplicant = coapprelationshiptype;
        //    ApplicantFatherName = appfathername;
        //    ApplicantMotherName = appmothername;
        //    DoorNO = doorno;
        //    StreetName = streetname;
        //    Village = village;
        //    Taluk = taluk;
        //    District = district;
        //    State = state;
        //    Pincode = pincode;
        //    Occupation = occupation;
        //    Education = education;
        //    Place = place;
        //    Residence = residence;
        //    TypeOFResidence = typeeofresidence;
        //    LandHolding = landholding;
        //    Caste = caste;
        //    Religion = religion;
        //    MonthlyIncome = monthlyincome;
        //    MonthlyExpenses = monthlyexpenses;
        //    ApplicantBankName = appbankname;
        //    ApplicantAccountNO = appaccountno;
        //    ApplicantIFSCcode = appifsccodde;
        //    ApplicantBranchName = appbranchname;
        //    LoanPurpose = loanpurpose;
        //    LoanTenure = loantenure;
        //    SamFinBranh = samfinbranch;
        //    RepaymentSchedule = repaymentschedule;
        //    Security = security;
        //    FullAddress = fulladdress;
        //    MonthlySurplus = monthlysurplus;
        //    IntrestRate = intrestrate;
        //    LPF = lpf;
        //    DocumentationCharges = documentationcharges;
        //    PenalChanges = penalcharges;
        //    Guarantee = guarantee;
        //    GTLIPremiumAmount = gtlipremiumamt;
        //    PAIPermiumAmount = paipremiumamt;
        //    InsuranceAmount = insuranceamt;
        //    HMreferenceNo = hmreference;
        //    SLreferenceNo = slrefno;
        //    SLdated = slrefno;

        //}
        //private static List<HiMark> Members()
        //{
        //    List<HiMark> ac = new List<HiMark>();
        //    SqlConnection sql = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection);
        //    SqlDataAdapter sqlData = new SqlDataAdapter("", sql);
        //    DataTable data = new DataTable();
        //    sqlData.Fill(data);
        //    foreach (DataRow row in data.Rows)
        //    {

        //    }
        //    return ac;
        //}
        public void createHimarkXls(List<HimarkModel> RequestList)
        {
           
           
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            //S.No.
            var no = xlWorkSheet.Cells.Range["A1"];
            no.Value = "S.NO.";
            no.Font.Bold = true;
            no.Font.Size = 11;
            no.RowHeight = 46;
            no.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            no.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //cbo Name
            var cbo = xlWorkSheet.Cells.Range["B1"];
            cbo.Value = "CBO/FPO/NGO/SP/BC Name";
            cbo.Font.Bold = true;
            cbo.Font.Size = 11;
            cbo.RowHeight = 46;
            cbo.EntireColumn.ColumnWidth = 50;
            cbo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cbo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG name
            var figname = xlWorkSheet.Cells.Range["C1"];
            figname.Value = "FIG / CENTRE Name";
            figname.Font.Bold = true;
            figname.Font.Size = 11;
            figname.RowHeight = 46;
            figname.ColumnWidth = 27;
            figname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG date
            var figdate = xlWorkSheet.Cells.Range["D1"];
            figdate.Value = "FIG / CENTRE Formation date";
            figdate.Font.Bold = true;
            figdate.Font.Size = 11;
            figdate.RowHeight = 46;
            figdate.ColumnWidth = 16;
            figdate.WrapText = true;
            figdate.Interior.ColorIndex = 34;
            figdate.Borders.ColorIndex = 15;
            figdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG village
            var figvillage = xlWorkSheet.Cells.Range["E1"];
            figvillage.Value = "FIG / CENTRE village";
            figvillage.Font.Bold = true;
            figvillage.Font.Size = 11;
            figvillage.RowHeight = 46;
            figvillage.ColumnWidth = 12;
            figvillage.WrapText = true;
            figvillage.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figvillage.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG taluk
            var figtaluk = xlWorkSheet.Cells.Range["F1"];
            figtaluk.Value = "FIG / CENTRE Taluk";
            figtaluk.Font.Bold = true;
            figtaluk.Font.Size = 11;
            figtaluk.RowHeight = 46;
            figtaluk.ColumnWidth = 12;
            figtaluk.WrapText = true;
            figtaluk.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figtaluk.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG district
            var figdistrict = xlWorkSheet.Cells.Range["G1"];
            figdistrict.Value = "FIG / CENTRE District";
            figdistrict.Font.Bold = true;
            figdistrict.Font.Size = 11;
            figdistrict.RowHeight = 46;
            figdistrict.ColumnWidth = 12;
            figdistrict.WrapText = true;
            figdistrict.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figdistrict.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Fig state
            var figstate = xlWorkSheet.Cells.Range["H1"];
            figstate.Value = "FIG / CENTRE State";
            figstate.Font.Bold = true;
            figstate.Font.Size = 11;
            figstate.RowHeight = 46;
            figstate.ColumnWidth = 11;
            figstate.WrapText = true;
            figstate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figstate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG saving
            var figsaving = xlWorkSheet.Cells.Range["I1"];
            figsaving.Value = "FIG Savings in Rs/-";
            figsaving.Font.Bold = true;
            figsaving.Font.Size = 11;
            figsaving.RowHeight = 46;
            figsaving.ColumnWidth = 21;
            figsaving.WrapText = true;
            figsaving.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figsaving.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG bank name
            var figbankname = xlWorkSheet.Cells.Range["J1"];
            figbankname.Value = "FIG/ CENTRE Bank Name";
            figbankname.Font.Bold = true;
            figbankname.Font.Size = 11;
            figbankname.RowHeight = 46;
            figbankname.ColumnWidth = 16;
            figbankname.WrapText = true;
            figbankname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figbankname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG account No
            var figaacountname = xlWorkSheet.Cells.Range["K1"];
            figaacountname.Value = "FIG/ CENTRE Account no.";
            figaacountname.Font.Bold = true;
            figaacountname.Font.Size = 11;
            figaacountname.RowHeight = 46;
            figaacountname.ColumnWidth = 16;
            figaacountname.WrapText = true;
            figaacountname.EntireColumn.NumberFormat = "@";
            figaacountname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figaacountname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG ifsc
            var figifsc = xlWorkSheet.Cells.Range["L1"];
            figifsc.Value = "FIG/ CENTRE Bank IFSC";
            figifsc.Font.Bold = true;
            figifsc.Font.Size = 11;
            figifsc.RowHeight = 46;
            figifsc.ColumnWidth = 15;
            figifsc.WrapText = true;
            figifsc.Interior.ColorIndex = 34;
            figifsc.Borders.ColorIndex = 15;
            figifsc.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figifsc.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG branch name
            var figbranchname = xlWorkSheet.Cells.Range["M1"];
            figbranchname.Value = "FIG/ CENTRE Branch Name";
            figbranchname.Font.Bold = true;
            figbranchname.Font.Size = 11;
            figbranchname.RowHeight = 46;
            figbranchname.ColumnWidth = 18;
            figbranchname.WrapText = true;
            figbranchname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figbranchname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Eligible loan Amount
            var eligibleloanamt = xlWorkSheet.Cells.Range["N1"];
            eligibleloanamt.Value = "Requested Loan Amount";
            eligibleloanamt.Font.Bold = true;
            eligibleloanamt.Font.Size = 11;
            eligibleloanamt.RowHeight = 46;
            eligibleloanamt.ColumnWidth = 24;
            eligibleloanamt.Interior.ColorIndex = 41;
            eligibleloanamt.Borders.ColorIndex = 15;
            eligibleloanamt.WrapText = true;
            eligibleloanamt.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            eligibleloanamt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //applicant number
            var applicantNo = xlWorkSheet.Cells.Range["O1"];
            applicantNo.Value = "Applicant NO.";
            applicantNo.Font.Bold = true;
            applicantNo.Font.Size = 11;
            applicantNo.RowHeight = 46;
            applicantNo.ColumnWidth = 17;
            applicantNo.WrapText = true;
            applicantNo.EntireColumn.NumberFormat = "@";
            applicantNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            applicantNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //emp id
            var EmpID = xlWorkSheet.Cells.Range["P1"];
            EmpID.Value = "EMP ID";
            EmpID.Font.Bold = true;
            EmpID.Font.Size = 11;
            EmpID.RowHeight = 46;
            EmpID.ColumnWidth = 15;
            EmpID.EntireColumn.NumberFormat = "@";
            EmpID.WrapText = true;
            EmpID.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            EmpID.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //RM name
            var RMName = xlWorkSheet.Cells.Range["Q1"];
            RMName.Value = "RM Name";
            RMName.Font.Bold = true;
            RMName.Font.Size = 11;
            RMName.RowHeight = 46;
            RMName.ColumnWidth = 12;
            RMName.WrapText = true;
            RMName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            RMName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Date of application
            var dateofapplication = xlWorkSheet.Cells.Range["R1"];
            dateofapplication.Value = "Date of Application";
            dateofapplication.Font.Bold = true;
            dateofapplication.Font.Size = 11;
            dateofapplication.RowHeight = 46;
            dateofapplication.ColumnWidth = 12;
            dateofapplication.Cells.Interior.ColorIndex = 6;
            dateofapplication.Borders.ColorIndex = 15;
            dateofapplication.WrapText = true;
            dateofapplication.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            dateofapplication.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //leader or member
            var Leader = xlWorkSheet.Cells.Range["S1"];
            Leader.Value = "Leader/Member";
            Leader.Font.Bold = true;
            Leader.Font.Size = 11;
            Leader.RowHeight = 46;
            Leader.ColumnWidth = 18;
            Leader.Interior.ColorIndex = 34;
            Leader.Borders.ColorIndex = 15;
            Leader.WrapText = true;
            Leader.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Leader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Name
            var ApplicantName = xlWorkSheet.Cells.Range["T1"];
            ApplicantName.Value = "Applicant Name";
            ApplicantName.Font.Bold = true;
            ApplicantName.Font.Size = 11;
            ApplicantName.RowHeight = 46;
            ApplicantName.ColumnWidth = 18;
            ApplicantName.WrapText = true;
            ApplicantName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Gender
            var ApplicantGender = xlWorkSheet.Cells.Range["U1"];
            ApplicantGender.Value = "Applicant Gender";
            ApplicantGender.Font.Bold = true;
            ApplicantGender.Font.Size = 11;
            ApplicantGender.RowHeight = 46;
            ApplicantGender.ColumnWidth = 11;
            ApplicantGender.WrapText = true;
            ApplicantGender.Font.ColorIndex = 3;
            ApplicantGender.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantGender.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //applicant mobile no
            var ApplicantMobileNo = xlWorkSheet.Cells.Range["V1"];
            ApplicantMobileNo.Value = "Applicant Contact No";
            ApplicantMobileNo.Font.Bold = true;
            ApplicantMobileNo.Font.Size = 11;
            ApplicantMobileNo.RowHeight = 46;
            ApplicantMobileNo.ColumnWidth = 14.30;
            ApplicantMobileNo.Interior.ColorIndex = 34;
            ApplicantMobileNo.Borders.ColorIndex = 15;
            ApplicantMobileNo.WrapText = true;
            ApplicantMobileNo.EntireColumn.NumberFormat = "@";
            ApplicantMobileNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantMobileNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant DOB
            var ApplicantDOB = xlWorkSheet.Cells.Range["W1"];
            ApplicantDOB.Value = "Applicant DOB";
            ApplicantDOB.Font.Bold = true;
            ApplicantDOB.Font.Size = 11;
            ApplicantDOB.RowHeight = 46;
            ApplicantDOB.ColumnWidth = 18;
            ApplicantDOB.WrapText = true;
            ApplicantDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Age
            var ApplicantAge = xlWorkSheet.Cells.Range["X1"];
            ApplicantAge.Value = "Applicant Age";
            ApplicantAge.Font.Bold = true;
            ApplicantAge.Font.Size = 11;
            ApplicantAge.RowHeight = 46;
            ApplicantAge.ColumnWidth = 18;
            ApplicantAge.WrapText = true;
            ApplicantAge.Cells.Interior.ColorIndex = 6;
            ApplicantAge.Borders.ColorIndex = 15;
            ApplicantAge.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantAge.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //id proof type
            var idprooftype = xlWorkSheet.Cells.Range["Y1"];
            idprooftype.Value = "ID Proof Type";
            idprooftype.Font.Bold = true;
            idprooftype.Font.Size = 11;
            idprooftype.RowHeight = 46;
            idprooftype.ColumnWidth = 14;
            idprooftype.Interior.ColorIndex = 34;
            idprooftype.Borders.ColorIndex = 15;
            idprooftype.WrapText = true;
            idprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            idprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant iD proof No
            var AppidproofNo = xlWorkSheet.Cells.Range["Z1"];
            AppidproofNo.Value = "Applicant ID Proof No.";
            AppidproofNo.Font.Bold = true;
            AppidproofNo.Font.Size = 11;
            AppidproofNo.RowHeight = 46;
            AppidproofNo.ColumnWidth = 19;
            AppidproofNo.WrapText = true;
            AppidproofNo.NumberFormat = "@";
            AppidproofNo.EntireColumn.NumberFormat = "@";
            AppidproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppidproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Address Proof Type
            var Addressprooftype = xlWorkSheet.Cells.Range["AA1"];
            Addressprooftype.Value = "Address Proof Type";
            Addressprooftype.Font.Bold = true;
            Addressprooftype.Font.Size = 11;
            Addressprooftype.RowHeight = 46;
            Addressprooftype.ColumnWidth = 16;
            Addressprooftype.Interior.ColorIndex = 34;
            Addressprooftype.Borders.ColorIndex = 15;
            Addressprooftype.WrapText = true;
            Addressprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Addressprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Address proof Number
            var AppAddressproofNo = xlWorkSheet.Cells.Range["AB1"];
            AppAddressproofNo.Value = "Applicant Address Proof ";
            AppAddressproofNo.Font.Bold = true;
            AppAddressproofNo.Font.Size = 11;
            AppAddressproofNo.RowHeight = 46;
            AppAddressproofNo.ColumnWidth = 26;
            AppAddressproofNo.Interior.ColorIndex = 34;
            AppAddressproofNo.Borders.ColorIndex = 15;
            AppAddressproofNo.WrapText = true;
            AppAddressproofNo.EntireColumn.NumberFormat = "@";
            AppAddressproofNo.NumberFormat = "@";
            AppAddressproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAddressproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Name
            var CoappName = xlWorkSheet.Cells.Range["AC1"];
            CoappName.Value = "Co-Applicant Name";
            CoappName.Font.Bold = true;
            CoappName.Font.Size = 11;
            CoappName.RowHeight = 46;
            CoappName.ColumnWidth = 20;
            CoappName.WrapText = true;
            CoappName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Gender
            var CoappGendder = xlWorkSheet.Cells.Range["AD1"];
            CoappGendder.Value = "Co-Applicant Gender";
            CoappGendder.Font.Bold = true;
            CoappGendder.Font.Size = 11;
            CoappGendder.RowHeight = 46;
            CoappGendder.ColumnWidth = 14;
            CoappGendder.WrapText = true;
            CoappGendder.Font.ColorIndex = 3;
            CoappGendder.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappGendder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Mobile Number
            var Coappnumber = xlWorkSheet.Cells.Range["AE1"];
            Coappnumber.Value = "Co-Applicant Mobile No.";
            Coappnumber.Font.Bold = true;
            Coappnumber.Font.Size = 11;
            Coappnumber.RowHeight = 46;
            Coappnumber.ColumnWidth = 14;
            Coappnumber.Interior.ColorIndex = 34;
            Coappnumber.Borders.ColorIndex = 15;
            Coappnumber.WrapText = true;
            Coappnumber.EntireColumn.NumberFormat = "@";
            Coappnumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Coappnumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant DOB
            var CoappDOB = xlWorkSheet.Cells.Range["AF1"];
            CoappDOB.Value = "Co-Applicant DOB";
            CoappDOB.Font.Bold = true;
            CoappDOB.Font.Size = 11;
            CoappDOB.RowHeight = 46;
            CoappDOB.ColumnWidth = 18;
            CoappDOB.Interior.ColorIndex = 34;
            CoappDOB.Borders.ColorIndex = 15;
            CoappDOB.WrapText = true;
            CoappDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Age
            var CoappAge = xlWorkSheet.Cells.Range["AG1"];
            CoappAge.Value = "Co-Applicant Age";
            CoappAge.Font.Bold = true;
            CoappAge.Font.Size = 11;
            CoappAge.RowHeight = 46;
            CoappAge.ColumnWidth = 20;
            CoappAge.Cells.Interior.ColorIndex = 6;
            CoappAge.WrapText = true;
            CoappAge.Borders.ColorIndex = 15;
            CoappAge.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappAge.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-app ID proof type
            var CoappIDproofType = xlWorkSheet.Cells.Range["AH1"];
            CoappIDproofType.Value = "ID Proof Type";
            CoappIDproofType.Font.Bold = true;
            CoappIDproofType.Font.Size = 11;
            CoappIDproofType.RowHeight = 46;
            CoappIDproofType.ColumnWidth = 16;
            CoappIDproofType.Interior.ColorIndex = 34;
            CoappIDproofType.Borders.ColorIndex = 15;
            CoappIDproofType.WrapText = true;
            CoappIDproofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappIDproofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-app Id proof Number
            var CoappIDproofNumber = xlWorkSheet.Cells.Range["AI1"];
            CoappIDproofNumber.Value = "Co-Applicant ID Proof No.";
            CoappIDproofNumber.Font.Bold = true;
            CoappIDproofNumber.Font.Size = 11;
            CoappIDproofNumber.RowHeight = 46;
            CoappIDproofNumber.ColumnWidth = 26;
            CoappIDproofNumber.WrapText = true;
            CoappIDproofNumber.NumberFormat = "@";
            CoappIDproofNumber.EntireColumn.NumberFormat = "@";
            CoappIDproofNumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappIDproofNumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address Proof Type
            var CoappAddressProoftype = xlWorkSheet.Cells.Range["AJ1"];
            CoappAddressProoftype.Value = "Address Proof Type";
            CoappAddressProoftype.Font.Bold = true;
            CoappAddressProoftype.Font.Size = 11;
            CoappAddressProoftype.RowHeight = 46;
            CoappAddressProoftype.ColumnWidth = 16;
            CoappAddressProoftype.Interior.ColorIndex = 34;
            CoappAddressProoftype.Borders.ColorIndex = 15;
            CoappAddressProoftype.WrapText = true;
            CoappAddressProoftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Address Proof No
            var CoappAddressproofNumber = xlWorkSheet.Cells.Range["AK1"];
            CoappAddressproofNumber.Value = "Co-Applicant Adddress Proof No.";
            CoappAddressproofNumber.Font.Bold = true;
            CoappAddressproofNumber.Font.Size = 11;
            CoappAddressproofNumber.RowHeight = 46;
            CoappAddressproofNumber.ColumnWidth = 26;
            CoappAddressproofNumber.WrapText = true;
            CoappAddressproofNumber.Interior.ColorIndex = 34;
            CoappAddressproofNumber.Borders.ColorIndex = 15;
            CoappAddressproofNumber.EntireColumn.NumberFormat = "@";
            CoappAddressproofNumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappAddressproofNumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Relationship with Co-Applicant
            var CoappRelationship = xlWorkSheet.Cells.Range["AL1"];
            CoappRelationship.Value = "Relationship with Co-Applicant";
            CoappRelationship.Font.Bold = true;
            CoappRelationship.Font.Size = 11;
            CoappRelationship.RowHeight = 46;
            CoappRelationship.ColumnWidth = 22;
            CoappRelationship.WrapText = true;
            CoappRelationship.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappRelationship.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Relationship wiht Applicant
            var AppRelationship = xlWorkSheet.Cells.Range["AM1"];
            AppRelationship.Value = "Relationship with Co-Applicant";
            AppRelationship.Font.Bold = true;
            AppRelationship.Font.Size = 11;
            AppRelationship.RowHeight = 46;
            AppRelationship.ColumnWidth = 19;
            AppRelationship.WrapText = true;
            AppRelationship.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppRelationship.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Father Name
            var AppFatherName = xlWorkSheet.Cells.Range["AN1"];
            AppFatherName.Value = "Applicant Father Name";
            AppFatherName.Font.Bold = true;
            AppFatherName.Font.Size = 11;
            AppFatherName.RowHeight = 46;
            AppFatherName.ColumnWidth = 24;
            AppFatherName.WrapText = true;
            AppFatherName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppFatherName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Mother Name
            var AppMotherName = xlWorkSheet.Cells.Range["AO1"];
            AppMotherName.Value = "Applicant Mother Name";
            AppMotherName.Font.Bold = true;
            AppMotherName.Font.Size = 11;
            AppMotherName.RowHeight = 46;
            AppMotherName.ColumnWidth = 24;
            AppMotherName.WrapText = true;
            AppMotherName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppMotherName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //DoorNo
            var DoorNo = xlWorkSheet.Cells.Range["AP1"];
            DoorNo.Value = "Door No.";
            DoorNo.Font.Bold = true;
            DoorNo.Font.Size = 11;
            DoorNo.RowHeight = 46;
            DoorNo.ColumnWidth = 11;
            DoorNo.WrapText = true;
            DoorNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            DoorNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Street Name
            var streetname = xlWorkSheet.Cells.Range["AQ1"];
            streetname.Value = "Street Name";
            streetname.Font.Bold = true;
            streetname.Font.Size = 11;
            streetname.RowHeight = 46;
            streetname.ColumnWidth = 18;
            streetname.WrapText = true;
            streetname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            streetname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Village
            var Village = xlWorkSheet.Cells.Range["AR1"];
            Village.Value = "Village";
            Village.Font.Bold = true;
            Village.Font.Size = 11;
            Village.RowHeight = 46;
            Village.ColumnWidth = 12;
            Village.WrapText = true;
            Village.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Village.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Taluk
            var Taluk = xlWorkSheet.Cells.Range["AS1"];
            Taluk.Value = "Taluk";
            Taluk.Font.Bold = true;
            Taluk.Font.Size = 11;
            Taluk.RowHeight = 46;
            Taluk.ColumnWidth = 11;
            Taluk.WrapText = true;
            Taluk.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Taluk.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //District
            var District = xlWorkSheet.Cells.Range["AT1"];
            District.Value = "District";
            District.Font.Bold = true;
            District.Font.Size = 11;
            District.RowHeight = 46;
            District.ColumnWidth = 11;
            District.WrapText = true;
            District.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            District.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //State
            var State = xlWorkSheet.Cells.Range["AU1"];
            State.Value = "State";
            State.Font.Bold = true;
            State.Font.Size = 11;
            State.RowHeight = 46;
            State.ColumnWidth = 11;
            State.WrapText = true;
            State.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            State.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Pincode
            var pincode = xlWorkSheet.Cells.Range["AV1"];
            pincode.Value = "Pincode";
            pincode.Font.Bold = true;
            pincode.Font.Size = 11;
            pincode.RowHeight = 46;
            pincode.ColumnWidth = 11;
            pincode.Interior.ColorIndex = 34;
            pincode.Borders.ColorIndex = 15;
            pincode.WrapText = true;
            pincode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            pincode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Occupation
            var occupation = xlWorkSheet.Cells.Range["AW1"];
            occupation.Value = "Occupation";
            occupation.Font.Bold = true;
            occupation.Font.Size = 11;
            occupation.RowHeight = 46;
            occupation.ColumnWidth = 13;
            occupation.WrapText = true;
            occupation.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            occupation.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Education
            var Education = xlWorkSheet.Cells.Range["AX1"];
            Education.Value = "Education";
            Education.Font.Bold = true;
            Education.Font.Size = 11;
            Education.RowHeight = 46;
            Education.ColumnWidth = 12;
            Education.WrapText = true;
            Education.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Education.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Place
            var place = xlWorkSheet.Cells.Range["AY1"];
            place.Value = "Place";
            place.Font.Bold = true;
            place.Font.Size = 11;
            place.RowHeight = 46;
            place.ColumnWidth = 8;
            place.Interior.ColorIndex = 34;
            place.Borders.ColorIndex = 15;
            place.WrapText = true;
            place.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            place.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Residence
            var Residence = xlWorkSheet.Cells.Range["AZ1"];
            Residence.Value = "Residence";
            Residence.Font.Bold = true;
            Residence.Font.Size = 11;
            Residence.RowHeight = 46;
            Residence.ColumnWidth = 12;
            Residence.Interior.ColorIndex = 34;
            Residence.Borders.ColorIndex = 15;
            Residence.WrapText = true;
            Residence.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Residence.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Type of Residence
            var TypeofResidence = xlWorkSheet.Cells.Range["BA1"];
            TypeofResidence.Value = "Type of Residence";
            TypeofResidence.Font.Bold = true;
            TypeofResidence.Font.Size = 11;
            TypeofResidence.RowHeight = 46;
            TypeofResidence.ColumnWidth = 12;
            TypeofResidence.WrapText = true;
            TypeofResidence.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            TypeofResidence.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Land Holding
            var landholding = xlWorkSheet.Cells.Range["BB1"];
            landholding.Value = "Land Holding";
            landholding.Font.Bold = true;
            landholding.Font.Size = 11;
            landholding.RowHeight = 46;
            landholding.ColumnWidth = 12;
            landholding.WrapText = true;
            landholding.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            landholding.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Caste
            var caste = xlWorkSheet.Cells.Range["BC1"];
            caste.Value = "Caste";
            caste.Font.Bold = true;
            caste.Font.Size = 11;
            caste.RowHeight = 46;
            caste.ColumnWidth = 8;
            caste.WrapText = true;
            caste.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            caste.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Religion
            var Religion = xlWorkSheet.Cells.Range["BD1"];
            Religion.Value = "Religion";
            Religion.Font.Bold = true;
            Religion.Font.Size = 11;
            Religion.RowHeight = 46;
            Religion.ColumnWidth = 10;
            Religion.Interior.ColorIndex = 34;
            Religion.Borders.ColorIndex = 15;
            Religion.WrapText = true;
            Religion.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Religion.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Monthly HH Income
            var MonthlyIncome = xlWorkSheet.Cells.Range["BE1"];
            MonthlyIncome.Value = "Monthly HH Income";
            MonthlyIncome.Font.Bold = true;
            MonthlyIncome.Font.Size = 11;
            MonthlyIncome.RowHeight = 46;
            MonthlyIncome.ColumnWidth = 21;
            MonthlyIncome.WrapText = true;
            MonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            MonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Monthly HH Expenses
            var MonthlyExpenses = xlWorkSheet.Cells.Range["BF1"];
            MonthlyExpenses.Value = "Monthly HH Expenses";
            MonthlyExpenses.Font.Bold = true;
            MonthlyExpenses.Font.Size = 11;
            MonthlyExpenses.RowHeight = 46;
            MonthlyExpenses.ColumnWidth = 21;
            MonthlyExpenses.WrapText = true;
            MonthlyExpenses.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            MonthlyExpenses.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Bank Name
            var AppBankName = xlWorkSheet.Cells.Range["BG1"];
            AppBankName.Value = "Applicant Bank Name";
            AppBankName.Font.Bold = true;
            AppBankName.Font.Size = 11;
            AppBankName.RowHeight = 46;
            AppBankName.ColumnWidth = 22;
            AppBankName.WrapText = true;
            AppBankName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppBankName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Account No.
            var AppAccNo = xlWorkSheet.Cells.Range["BH1"];
            AppAccNo.Value = "Applicant Account Number";
            AppAccNo.Font.Bold = true;
            AppAccNo.Font.Size = 11;
            AppAccNo.RowHeight = 46;
            AppAccNo.ColumnWidth = 22;
            AppAccNo.WrapText = true;
            AppAccNo.EntireColumn.NumberFormat = "@";
            AppAccNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAccNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant IFSC code
            var AppIFSCcode = xlWorkSheet.Cells.Range["BI1"];
            AppIFSCcode.Value = "Applicant IFSC Code";
            AppIFSCcode.Font.Bold = true;
            AppIFSCcode.Font.Size = 11;
            AppIFSCcode.RowHeight = 46;
            AppIFSCcode.ColumnWidth = 22;
            AppIFSCcode.Interior.ColorIndex = 34;
            AppIFSCcode.Borders.ColorIndex = 15;
            AppIFSCcode.WrapText = true;
            
            AppIFSCcode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppIFSCcode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Branch Name
            var AppBranchName = xlWorkSheet.Cells.Range["BJ1"];
            AppBranchName.Value = "Applicant Branch Name";
            AppBranchName.Font.Bold = true;
            AppBranchName.Font.Size = 11;
            AppBranchName.RowHeight = 46;
            AppBranchName.ColumnWidth = 22;
            AppBranchName.Interior.ColorIndex = 34;
            AppBranchName.Borders.ColorIndex = 15;
            AppBranchName.WrapText = true;
            AppBranchName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppBranchName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Purpose
            var Loanpurpose = xlWorkSheet.Cells.Range["BK1"];
            Loanpurpose.Value = "Loan Purpose";
            Loanpurpose.Font.Bold = true;
            Loanpurpose.Font.Size = 11;
            Loanpurpose.RowHeight = 46;
            Loanpurpose.ColumnWidth = 15;
            Loanpurpose.WrapText = true;
            Loanpurpose.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Loanpurpose.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan tenure
            var LoanTenure = xlWorkSheet.Cells.Range["BL1"];
            LoanTenure.Value = "Loan Tenure";
            LoanTenure.Font.Bold = true;
            LoanTenure.Font.Size = 11;
            LoanTenure.RowHeight = 46;
            LoanTenure.ColumnWidth = 15;
            LoanTenure.WrapText = true;
            LoanTenure.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            LoanTenure.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //SamFin Branch 
            var SamFin = xlWorkSheet.Cells.Range["BM1"];
            SamFin.Value = "SamFin Branch";
            SamFin.Font.Bold = true;
            SamFin.Font.Size = 11;
            SamFin.RowHeight = 46;
            SamFin.ColumnWidth = 9;
            SamFin.WrapText = true;
            SamFin.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            SamFin.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Repayment Schedule
            var Repayment = xlWorkSheet.Cells.Range["BN1"];
            Repayment.Value = "Repayment Schedule";
            Repayment.Font.Bold = true;
            Repayment.Font.Size = 11;
            Repayment.RowHeight = 46;
            Repayment.ColumnWidth = 12;
            Repayment.Font.ColorIndex = 37;
            Repayment.WrapText = true;
            Repayment.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Repayment.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Security
            var Security = xlWorkSheet.Cells.Range["BO1"];
            Security.Value = "Security";
            Security.Font.Bold = true;
            Security.Font.Size = 11;
            Security.RowHeight = 46;
            Security.ColumnWidth = 10;
            Security.Font.ColorIndex = 37;
            Security.WrapText = true;
            Security.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Security.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Full Address
            var FullAddress = xlWorkSheet.Cells.Range["BP1"];
            FullAddress.Value = "Full Address";
            FullAddress.Font.Bold = true;
            FullAddress.Font.Size = 11;
            FullAddress.RowHeight = 46;
            FullAddress.ColumnWidth = 74;
            FullAddress.Font.ColorIndex = 37;
            FullAddress.WrapText = true;
            FullAddress.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            FullAddress.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Month Surplus
            var Surplus = xlWorkSheet.Cells.Range["BQ1"];
            Surplus.Value = "Monthly Surplus";
            Surplus.Font.Bold = true;
            Surplus.Font.Size = 11;
            Surplus.RowHeight = 46;
            Surplus.ColumnWidth = 18;
            Surplus.Font.ColorIndex = 37;
            Surplus.WrapText = true;
            Surplus.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Surplus.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Intrest Rate
            var Intrestrate = xlWorkSheet.Cells.Range["BR1"];
            Intrestrate.Value = "Intrest Rate";
            Intrestrate.Font.Bold = true;
            Intrestrate.Font.Size = 11;
            Intrestrate.RowHeight = 46;
            Intrestrate.ColumnWidth = 14;
            Intrestrate.WrapText = true;
            Intrestrate.Font.ColorIndex = 3;
            Intrestrate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Intrestrate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //LPF
            var LPF = xlWorkSheet.Cells.Range["BS1"];
            LPF.Value = "LPF";
            LPF.Font.Bold = true;
            LPF.Font.Size = 11;
            LPF.RowHeight = 46;
            LPF.ColumnWidth = 6;
            LPF.WrapText = true;
            LPF.Font.ColorIndex = 3;
            LPF.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            LPF.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Documentation Charges
            var DocCharges = xlWorkSheet.Cells.Range["BT1"];
            DocCharges.Value = "Documentation Charges";
            DocCharges.Font.Bold = true;
            DocCharges.Font.Size = 11;
            DocCharges.RowHeight = 46;
            DocCharges.ColumnWidth = 14.14;
            DocCharges.Font.ColorIndex = 37;
            DocCharges.WrapText = true;
            DocCharges.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            DocCharges.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Penal Charges
            var PenalCharges = xlWorkSheet.Cells.Range["BU1"];
            PenalCharges.Value = "Penal Charges";
            PenalCharges.Font.Bold = true;
            PenalCharges.Font.Size = 11;
            PenalCharges.RowHeight = 46;
            PenalCharges.ColumnWidth = 15.14;
            PenalCharges.WrapText = true;
            PenalCharges.Font.ColorIndex = 3;
            PenalCharges.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            PenalCharges.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Guarantee
            var Guarantee = xlWorkSheet.Cells.Range["BV1"];
            Guarantee.Value = "Guarantee";
            Guarantee.Font.Bold = true;
            Guarantee.Font.Size = 11;
            Guarantee.RowHeight = 46;
            Guarantee.ColumnWidth = 19;
            Guarantee.WrapText = true;
            Guarantee.Font.ColorIndex = 37;
            Guarantee.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Guarantee.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //GTLI Premium Amount
            var GTLI = xlWorkSheet.Cells.Range["BW1"];
            GTLI.Value = "GTLI Premium Amount";
            GTLI.Font.Bold = true;
            GTLI.Font.Size = 11;
            GTLI.RowHeight = 46;
            GTLI.ColumnWidth = 13;
            GTLI.WrapText = true;
            GTLI.Font.ColorIndex = 37;
            GTLI.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            GTLI.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //PAI Premium Amount
            var PAI = xlWorkSheet.Cells.Range["BX1"];
            PAI.Value = "PAI Premium Amount";
            PAI.Font.Bold = true;
            PAI.Font.Size = 11;
            PAI.RowHeight = 46;
            PAI.ColumnWidth = 13;
            PAI.WrapText = true;
            PAI.Font.ColorIndex = 37;
            PAI.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            PAI.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance Amt
            var Insurance = xlWorkSheet.Cells.Range["BY1"];
            Insurance.Value = "Insurance Amt";
            Insurance.Font.Bold = true;
            Insurance.Font.Size = 11;
            Insurance.RowHeight = 46;
            Insurance.ColumnWidth = 14;
            Insurance.Font.ColorIndex = 37;
            Insurance.WrapText = true;
            Insurance.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Insurance.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //H.M Reference No
            var HMReference = xlWorkSheet.Cells.Range["BZ1"];
            HMReference.Value = "H.M Reference No.";
            HMReference.Font.Bold = true;
            HMReference.Font.Size = 11;
            HMReference.RowHeight = 46;
            HMReference.ColumnWidth = 14;
            HMReference.WrapText = true;
            HMReference.Font.ColorIndex = 3;
            HMReference.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            HMReference.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Age/DOB
            var AppAgeDOB = xlWorkSheet.Cells.Range["CA1"];
            AppAgeDOB.Value = "Applicant Age/DOB";
            AppAgeDOB.Font.Bold = true;
            AppAgeDOB.Font.Size = 11;
            AppAgeDOB.RowHeight = 46;
            AppAgeDOB.ColumnWidth = 23;
            AppAgeDOB.WrapText = true;
            AppAgeDOB.Font.ColorIndex = 37;
            AppAgeDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAgeDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Age/DOB
            var CoAppAgeDOB = xlWorkSheet.Cells.Range["CB1"];
            CoAppAgeDOB.Value = "Co-Applicant Age/DOB";
            CoAppAgeDOB.Font.Bold = true;
            CoAppAgeDOB.Font.Size = 11;
            CoAppAgeDOB.RowHeight = 46;
            CoAppAgeDOB.ColumnWidth = 23;
            CoAppAgeDOB.WrapText = true;
            CoAppAgeDOB.Font.ColorIndex = 37;
            AppAgeDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAgeDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //SL Ref No.
            var SLRef = xlWorkSheet.Cells.Range["CC1"];
            SLRef.Value = "SL Ref No.";
            SLRef.Font.Bold = true;
            SLRef.Font.Size = 11;
            SLRef.RowHeight = 46;
            SLRef.ColumnWidth = 16;
            SLRef.WrapText = true;
            SLRef.Cells.Interior.ColorIndex = 6;
            SLRef.Borders.ColorIndex = 15;
            SLRef.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            SLRef.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Sl Dated
            var SLDated = xlWorkSheet.Cells.Range["CD1"];
            SLDated.Value = "SL Dated";
            SLDated.Font.Bold = true;
            SLDated.Font.Size = 11;
            SLDated.RowHeight = 46;
            SLDated.ColumnWidth = 16;
            SLDated.Cells.Interior.ColorIndex = 6;
            SLDated.Borders.ColorIndex = 15;
            SLDated.WrapText = true;
            SLDated.EntireColumn.NumberFormat = "@";
            SLDated.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            SLDated.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
          
            FillHimarkDate(xlWorkSheet, RequestList);
            string dir = "";
            try
            {
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Report\\Hi-Mark Reports");
                if(Directory.Exists(dir))
                {
                    string FileName = dir+"\\Hi-Mark_" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    string FileName = dir + "\\Hi-Mark_" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);

                }
                
                
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                xlWorkBook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
                xlWorkBook = null;
                File.Delete(dir+"\\temp.xlsx");
            }
        }

        public void createHimarkXls1(List<HimarkModel> RequestList)
        {


            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            //S.No.
            var no = xlWorkSheet.Cells.Range["A1"];
            no.Value = "S.NO.";
            no.Font.Bold = true;
            no.Font.Size = 11;
            no.RowHeight = 46;
            no.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            no.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //cbo Name
            var cbo = xlWorkSheet.Cells.Range["B1"];
            cbo.Value = "CBO/FPO/NGO/SP/BC Name";
            cbo.Font.Bold = true;
            cbo.Font.Size = 11;
            cbo.RowHeight = 46;
            cbo.EntireColumn.ColumnWidth = 50;
            cbo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cbo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG name
            var figname = xlWorkSheet.Cells.Range["C1"];
            figname.Value = "FIG / CENTRE Name";
            figname.Font.Bold = true;
            figname.Font.Size = 11;
            figname.RowHeight = 46;
            figname.ColumnWidth = 27;
            figname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG date
            var figdate = xlWorkSheet.Cells.Range["D1"];
            figdate.Value = "FIG / CENTRE Formation date";
            figdate.Font.Bold = true;
            figdate.Font.Size = 11;
            figdate.RowHeight = 46;
            figdate.ColumnWidth = 16;
            figdate.WrapText = true;
            figdate.Interior.ColorIndex = 34;
            figdate.Borders.ColorIndex = 15;
            figdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG village
            var figvillage = xlWorkSheet.Cells.Range["E1"];
            figvillage.Value = "FIG / CENTRE village";
            figvillage.Font.Bold = true;
            figvillage.Font.Size = 11;
            figvillage.RowHeight = 46;
            figvillage.ColumnWidth = 12;
            figvillage.WrapText = true;
            figvillage.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figvillage.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG taluk
            var figtaluk = xlWorkSheet.Cells.Range["F1"];
            figtaluk.Value = "FIG / CENTRE Taluk";
            figtaluk.Font.Bold = true;
            figtaluk.Font.Size = 11;
            figtaluk.RowHeight = 46;
            figtaluk.ColumnWidth = 12;
            figtaluk.WrapText = true;
            figtaluk.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figtaluk.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG district
            var figdistrict = xlWorkSheet.Cells.Range["G1"];
            figdistrict.Value = "FIG / CENTRE District";
            figdistrict.Font.Bold = true;
            figdistrict.Font.Size = 11;
            figdistrict.RowHeight = 46;
            figdistrict.ColumnWidth = 12;
            figdistrict.WrapText = true;
            figdistrict.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figdistrict.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Fig state
            var figstate = xlWorkSheet.Cells.Range["H1"];
            figstate.Value = "FIG / CENTRE State";
            figstate.Font.Bold = true;
            figstate.Font.Size = 11;
            figstate.RowHeight = 46;
            figstate.ColumnWidth = 11;
            figstate.WrapText = true;
            figstate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figstate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG saving
            var figsaving = xlWorkSheet.Cells.Range["I1"];
            figsaving.Value = "FIG Savings in Rs/-";
            figsaving.Font.Bold = true;
            figsaving.Font.Size = 11;
            figsaving.RowHeight = 46;
            figsaving.ColumnWidth = 21;
            figsaving.WrapText = true;
            figsaving.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figsaving.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG bank name
            var figbankname = xlWorkSheet.Cells.Range["J1"];
            figbankname.Value = "FIG/ CENTRE Bank Name";
            figbankname.Font.Bold = true;
            figbankname.Font.Size = 11;
            figbankname.RowHeight = 46;
            figbankname.ColumnWidth = 16;
            figbankname.WrapText = true;
            figbankname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figbankname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG account No
            var figaacountname = xlWorkSheet.Cells.Range["K1"];
            figaacountname.Value = "FIG/ CENTRE Account no.";
            figaacountname.Font.Bold = true;
            figaacountname.Font.Size = 11;
            figaacountname.RowHeight = 46;
            figaacountname.ColumnWidth = 16;
            figaacountname.WrapText = true;
            figaacountname.EntireColumn.NumberFormat = "@";
            figaacountname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figaacountname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG ifsc
            var figifsc = xlWorkSheet.Cells.Range["L1"];
            figifsc.Value = "FIG/ CENTRE Bank IFSC";
            figifsc.Font.Bold = true;
            figifsc.Font.Size = 11;
            figifsc.RowHeight = 46;
            figifsc.ColumnWidth = 15;
            figifsc.WrapText = true;
            figifsc.Interior.ColorIndex = 34;
            figifsc.Borders.ColorIndex = 15;
            figifsc.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figifsc.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FIG branch name
            var figbranchname = xlWorkSheet.Cells.Range["M1"];
            figbranchname.Value = "FIG/ CENTRE Branch Name";
            figbranchname.Font.Bold = true;
            figbranchname.Font.Size = 11;
            figbranchname.RowHeight = 46;
            figbranchname.ColumnWidth = 18;
            figbranchname.WrapText = true;
            figbranchname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            figbranchname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Eligible loan Amount
            var eligibleloanamt = xlWorkSheet.Cells.Range["N1"];
            eligibleloanamt.Value = "Requested Loan Amount";
            eligibleloanamt.Font.Bold = true;
            eligibleloanamt.Font.Size = 11;
            eligibleloanamt.RowHeight = 46;
            eligibleloanamt.ColumnWidth = 24;
            eligibleloanamt.Interior.ColorIndex = 41;
            eligibleloanamt.Borders.ColorIndex = 15;
            eligibleloanamt.WrapText = true;
            eligibleloanamt.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            eligibleloanamt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //applicant number
            var applicantNo = xlWorkSheet.Cells.Range["O1"];
            applicantNo.Value = "Applicant NO.";
            applicantNo.Font.Bold = true;
            applicantNo.Font.Size = 11;
            applicantNo.RowHeight = 46;
            applicantNo.ColumnWidth = 17;
            applicantNo.WrapText = true;
            applicantNo.EntireColumn.NumberFormat = "@";
            applicantNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            applicantNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //emp id
            var EmpID = xlWorkSheet.Cells.Range["P1"];
            EmpID.Value = "EMP ID";
            EmpID.Font.Bold = true;
            EmpID.Font.Size = 11;
            EmpID.RowHeight = 46;
            EmpID.ColumnWidth = 15;
            EmpID.EntireColumn.NumberFormat = "@";
            EmpID.WrapText = true;
            EmpID.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            EmpID.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //RM name
            var RMName = xlWorkSheet.Cells.Range["Q1"];
            RMName.Value = "RM Name";
            RMName.Font.Bold = true;
            RMName.Font.Size = 11;
            RMName.RowHeight = 46;
            RMName.ColumnWidth = 12;
            RMName.WrapText = true;
            RMName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            RMName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Date of application
            var dateofapplication = xlWorkSheet.Cells.Range["R1"];
            dateofapplication.Value = "Date of Application";
            dateofapplication.Font.Bold = true;
            dateofapplication.Font.Size = 11;
            dateofapplication.RowHeight = 46;
            dateofapplication.ColumnWidth = 12;
            dateofapplication.Cells.Interior.ColorIndex = 6;
            dateofapplication.Borders.ColorIndex = 15;
            dateofapplication.WrapText = true;
            dateofapplication.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            dateofapplication.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //leader or member
            var Leader = xlWorkSheet.Cells.Range["S1"];
            Leader.Value = "Leader/Member";
            Leader.Font.Bold = true;
            Leader.Font.Size = 11;
            Leader.RowHeight = 46;
            Leader.ColumnWidth = 18;
            Leader.Interior.ColorIndex = 34;
            Leader.Borders.ColorIndex = 15;
            Leader.WrapText = true;
            Leader.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Leader.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Name
            var ApplicantName = xlWorkSheet.Cells.Range["T1"];
            ApplicantName.Value = "Applicant Name";
            ApplicantName.Font.Bold = true;
            ApplicantName.Font.Size = 11;
            ApplicantName.RowHeight = 46;
            ApplicantName.ColumnWidth = 18;
            ApplicantName.WrapText = true;
            ApplicantName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Applicant Father Name
            var AppFatherName = xlWorkSheet.Cells.Range["U1"];
            AppFatherName.Value = "Applicant Father Name";
            AppFatherName.Font.Bold = true;
            AppFatherName.Font.Size = 11;
            AppFatherName.RowHeight = 46;
            AppFatherName.ColumnWidth = 24;
            AppFatherName.WrapText = true;
            AppFatherName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppFatherName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Mother Name
            var AppMotherName = xlWorkSheet.Cells.Range["V1"];
            AppMotherName.Value = "Applicant Mother Name";
            AppMotherName.Font.Bold = true;
            AppMotherName.Font.Size = 11;
            AppMotherName.RowHeight = 46;
            AppMotherName.ColumnWidth = 24;
            AppMotherName.WrapText = true;
            AppMotherName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppMotherName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //DoorNo
            var DoorNo = xlWorkSheet.Cells.Range["W1"];
            DoorNo.Value = "Door No.";
            DoorNo.Font.Bold = true;
            DoorNo.Font.Size = 11;
            DoorNo.RowHeight = 46;
            DoorNo.ColumnWidth = 11;
            DoorNo.WrapText = true;
            DoorNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            DoorNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Street Name
            var streetname = xlWorkSheet.Cells.Range["X1"];
            streetname.Value = "Street Name";
            streetname.Font.Bold = true;
            streetname.Font.Size = 11;
            streetname.RowHeight = 46;
            streetname.ColumnWidth = 18;
            streetname.WrapText = true;
            streetname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            streetname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Village
            var Village = xlWorkSheet.Cells.Range["Y1"];
            Village.Value = "Village";
            Village.Font.Bold = true;
            Village.Font.Size = 11;
            Village.RowHeight = 46;
            Village.ColumnWidth = 12;
            Village.WrapText = true;
            Village.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Village.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Taluk
            var Taluk = xlWorkSheet.Cells.Range["Z1"];
            Taluk.Value = "Taluk";
            Taluk.Font.Bold = true;
            Taluk.Font.Size = 11;
            Taluk.RowHeight = 46;
            Taluk.ColumnWidth = 11;
            Taluk.WrapText = true;
            Taluk.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Taluk.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //District
            var District = xlWorkSheet.Cells.Range["AA1"];
            District.Value = "District";
            District.Font.Bold = true;
            District.Font.Size = 11;
            District.RowHeight = 46;
            District.ColumnWidth = 11;
            District.WrapText = true;
            District.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            District.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //State
            var State = xlWorkSheet.Cells.Range["AB1"];
            State.Value = "State";
            State.Font.Bold = true;
            State.Font.Size = 11;
            State.RowHeight = 46;
            State.ColumnWidth = 11;
            State.WrapText = true;
            State.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            State.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Pincode
            var pincode = xlWorkSheet.Cells.Range["AC1"];
            pincode.Value = "Pincode";
            pincode.Font.Bold = true;
            pincode.Font.Size = 11;
            pincode.RowHeight = 46;
            pincode.ColumnWidth = 11;
            pincode.Interior.ColorIndex = 34;
            pincode.Borders.ColorIndex = 15;
            pincode.WrapText = true;
            pincode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            pincode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //applicant mobile no
            var ApplicantMobileNo = xlWorkSheet.Cells.Range["AD1"];
            ApplicantMobileNo.Value = "Applicant Contact No";
            ApplicantMobileNo.Font.Bold = true;
            ApplicantMobileNo.Font.Size = 11;
            ApplicantMobileNo.RowHeight = 46;
            ApplicantMobileNo.ColumnWidth = 14.30;
            ApplicantMobileNo.Interior.ColorIndex = 34;
            ApplicantMobileNo.Borders.ColorIndex = 15;
            ApplicantMobileNo.WrapText = true;
            ApplicantMobileNo.EntireColumn.NumberFormat = "@";
            ApplicantMobileNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantMobileNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            var ApplicantMailid = xlWorkSheet.Cells.Range["AE1"];
            ApplicantMailid.Value = "Applicant Mail ID";
            ApplicantMailid.Font.Bold = true;
            ApplicantMailid.Font.Size = 11;
            ApplicantMailid.RowHeight = 46;
            ApplicantMailid.ColumnWidth = 14.30;
            ApplicantMailid.Interior.ColorIndex = 34;
            ApplicantMailid.Borders.ColorIndex = 15;
            ApplicantMailid.WrapText = true;
            ApplicantMailid.EntireColumn.NumberFormat = "@";
            ApplicantMailid.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantMailid.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Occupation
            var occupation = xlWorkSheet.Cells.Range["AF1"];
            occupation.Value = "Occupation";
            occupation.Font.Bold = true;
            occupation.Font.Size = 11;
            occupation.RowHeight = 46;
            occupation.ColumnWidth = 13;
            occupation.WrapText = true;
            occupation.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            occupation.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Education
            var Education = xlWorkSheet.Cells.Range["AG1"];
            Education.Value = "Education";
            Education.Font.Bold = true;
            Education.Font.Size = 11;
            Education.RowHeight = 46;
            Education.ColumnWidth = 12;
            Education.WrapText = true;
            Education.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Education.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Place
            var place = xlWorkSheet.Cells.Range["AH1"];
            place.Value = "Place";
            place.Font.Bold = true;
            place.Font.Size = 11;
            place.RowHeight = 46;
            place.ColumnWidth = 8;
            place.Interior.ColorIndex = 34;
            place.Borders.ColorIndex = 15;
            place.WrapText = true;
            place.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            place.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Residence
            var Residence = xlWorkSheet.Cells.Range["AI1"];
            Residence.Value = "Residence";
            Residence.Font.Bold = true;
            Residence.Font.Size = 11;
            Residence.RowHeight = 46;
            Residence.ColumnWidth = 12;
            Residence.Interior.ColorIndex = 34;
            Residence.Borders.ColorIndex = 15;
            Residence.WrapText = true;
            Residence.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Residence.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Type of Residence
            var TypeofResidence = xlWorkSheet.Cells.Range["AJ1"];
            TypeofResidence.Value = "Type of Residence";
            TypeofResidence.Font.Bold = true;
            TypeofResidence.Font.Size = 11;
            TypeofResidence.RowHeight = 46;
            TypeofResidence.ColumnWidth = 12;
            TypeofResidence.WrapText = true;
            TypeofResidence.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            TypeofResidence.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Land Holding
            var landholding = xlWorkSheet.Cells.Range["AK1"];
            landholding.Value = "Land Holding";
            landholding.Font.Bold = true;
            landholding.Font.Size = 11;
            landholding.RowHeight = 46;
            landholding.ColumnWidth = 12;
            landholding.WrapText = true;
            landholding.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            landholding.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Caste
            var caste = xlWorkSheet.Cells.Range["AL1"];
            caste.Value = "Caste";
            caste.Font.Bold = true;
            caste.Font.Size = 11;
            caste.RowHeight = 46;
            caste.ColumnWidth = 8;
            caste.WrapText = true;
            caste.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            caste.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Religion
            var Religion = xlWorkSheet.Cells.Range["AM1"];
            Religion.Value = "Religion";
            Religion.Font.Bold = true;
            Religion.Font.Size = 11;
            Religion.RowHeight = 46;
            Religion.ColumnWidth = 10;
            Religion.Interior.ColorIndex = 34;
            Religion.Borders.ColorIndex = 15;
            Religion.WrapText = true;
            Religion.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Religion.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Bank Name
            var AppBankName = xlWorkSheet.Cells.Range["AN1"];
            AppBankName.Value = "Applicant Bank Name";
            AppBankName.Font.Bold = true;
            AppBankName.Font.Size = 11;
            AppBankName.RowHeight = 46;
            AppBankName.ColumnWidth = 22;
            AppBankName.WrapText = true;
            AppBankName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppBankName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Account No.
            var AppAccNo = xlWorkSheet.Cells.Range["AO1"];
            AppAccNo.Value = "Applicant Account Number";
            AppAccNo.Font.Bold = true;
            AppAccNo.Font.Size = 11;
            AppAccNo.RowHeight = 46;
            AppAccNo.ColumnWidth = 22;
            AppAccNo.WrapText = true;
            AppAccNo.EntireColumn.NumberFormat = "@";
            AppAccNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAccNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant IFSC code
            var AppIFSCcode = xlWorkSheet.Cells.Range["AP1"];
            AppIFSCcode.Value = "Applicant IFSC Code";
            AppIFSCcode.Font.Bold = true;
            AppIFSCcode.Font.Size = 11;
            AppIFSCcode.RowHeight = 46;
            AppIFSCcode.ColumnWidth = 22;
            AppIFSCcode.Interior.ColorIndex = 34;
            AppIFSCcode.Borders.ColorIndex = 15;
            AppIFSCcode.WrapText = true;

            AppIFSCcode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppIFSCcode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Branch Name
            var AppBranchName = xlWorkSheet.Cells.Range["AQ1"];
            AppBranchName.Value = "Applicant Branch Name";
            AppBranchName.Font.Bold = true;
            AppBranchName.Font.Size = 11;
            AppBranchName.RowHeight = 46;
            AppBranchName.ColumnWidth = 22;
            AppBranchName.Interior.ColorIndex = 34;
            AppBranchName.Borders.ColorIndex = 15;
            AppBranchName.WrapText = true;
            AppBranchName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppBranchName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant House Hold Expenses
            var AppHouseHoldExpense = xlWorkSheet.Cells.Range["AR1"];
            AppHouseHoldExpense.Value = "Applicant House Hold Expenses";
            AppHouseHoldExpense.Font.Bold = true;
            AppHouseHoldExpense.Font.Size = 11;
            AppHouseHoldExpense.RowHeight = 46;
            AppHouseHoldExpense.ColumnWidth = 22;
            AppHouseHoldExpense.Interior.ColorIndex = 34;
            AppHouseHoldExpense.Borders.ColorIndex = 15;
            AppHouseHoldExpense.WrapText = true;
            AppHouseHoldExpense.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppHouseHoldExpense.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Loan Purpose
            var Loanpurpose = xlWorkSheet.Cells.Range["AS1"];
            Loanpurpose.Value = "Loan Purpose";
            Loanpurpose.Font.Bold = true;
            Loanpurpose.Font.Size = 11;
            Loanpurpose.RowHeight = 46;
            Loanpurpose.ColumnWidth = 15;
            Loanpurpose.WrapText = true;
            Loanpurpose.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Loanpurpose.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan tenure
            var LoanTenure = xlWorkSheet.Cells.Range["AT1"];
            LoanTenure.Value = "Loan Tenure";
            LoanTenure.Font.Bold = true;
            LoanTenure.Font.Size = 11;
            LoanTenure.RowHeight = 46;
            LoanTenure.ColumnWidth = 15;
            LoanTenure.WrapText = true;
            LoanTenure.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            LoanTenure.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //SamFin Branch 
            var SamFin = xlWorkSheet.Cells.Range["AU1"];
            SamFin.Value = "SamFin Branch";
            SamFin.Font.Bold = true;
            SamFin.Font.Size = 11;
            SamFin.RowHeight = 46;
            SamFin.ColumnWidth = 9;
            SamFin.WrapText = true;
            SamFin.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            SamFin.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Gender
            var ApplicantGender = xlWorkSheet.Cells.Range["AV1"];
            ApplicantGender.Value = "Applicant Gender";
            ApplicantGender.Font.Bold = true;
            ApplicantGender.Font.Size = 11;
            ApplicantGender.RowHeight = 46;
            ApplicantGender.ColumnWidth = 11;
            ApplicantGender.WrapText = true;
            ApplicantGender.Font.ColorIndex = 3;
            ApplicantGender.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantGender.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //applicant mobile no
            var ApplicantMobileNo1 = xlWorkSheet.Cells.Range["AW1"];
            ApplicantMobileNo1.Value = "Applicant Contact No";
            ApplicantMobileNo1.Font.Bold = true;
            ApplicantMobileNo1.Font.Size = 11;
            ApplicantMobileNo1.RowHeight = 46;
            ApplicantMobileNo1.ColumnWidth = 14.30;
            ApplicantMobileNo1.Interior.ColorIndex = 34;
            ApplicantMobileNo1.Borders.ColorIndex = 15;
            ApplicantMobileNo1.WrapText = true;
            ApplicantMobileNo1.EntireColumn.NumberFormat = "@";
            ApplicantMobileNo1.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantMobileNo1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant DOB
            var ApplicantDOB = xlWorkSheet.Cells.Range["AX1"];
            ApplicantDOB.Value = "Applicant DOB";
            ApplicantDOB.Font.Bold = true;
            ApplicantDOB.Font.Size = 11;
            ApplicantDOB.RowHeight = 46;
            ApplicantDOB.ColumnWidth = 18;
            ApplicantDOB.WrapText = true;
            ApplicantDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Age
            var ApplicantAge = xlWorkSheet.Cells.Range["AY1"];
            ApplicantAge.Value = "Applicant Age";
            ApplicantAge.Font.Bold = true;
            ApplicantAge.Font.Size = 11;
            ApplicantAge.RowHeight = 46;
            ApplicantAge.ColumnWidth = 18;
            ApplicantAge.WrapText = true;
            ApplicantAge.Cells.Interior.ColorIndex = 6;
            ApplicantAge.Borders.ColorIndex = 15;
            ApplicantAge.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantAge.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //id proof type
            var idprooftype = xlWorkSheet.Cells.Range["AZ1"];
            idprooftype.Value = "ID Proof Type";
            idprooftype.Font.Bold = true;
            idprooftype.Font.Size = 11;
            idprooftype.RowHeight = 46;
            idprooftype.ColumnWidth = 14;
            idprooftype.Interior.ColorIndex = 34;
            idprooftype.Borders.ColorIndex = 15;
            idprooftype.WrapText = true;
            idprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            idprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant iD proof No
            var AppidproofNo = xlWorkSheet.Cells.Range["BA1"];
            AppidproofNo.Value = "Applicant ID Proof No.";
            AppidproofNo.Font.Bold = true;
            AppidproofNo.Font.Size = 11;
            AppidproofNo.RowHeight = 46;
            AppidproofNo.ColumnWidth = 19;
            AppidproofNo.WrapText = true;
            AppidproofNo.NumberFormat = "@";
            AppidproofNo.EntireColumn.NumberFormat = "@";
            AppidproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppidproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Address Proof Type
            var Addressprooftype = xlWorkSheet.Cells.Range["BB1"];
            Addressprooftype.Value = "Address Proof Type";
            Addressprooftype.Font.Bold = true;
            Addressprooftype.Font.Size = 11;
            Addressprooftype.RowHeight = 46;
            Addressprooftype.ColumnWidth = 16;
            Addressprooftype.Interior.ColorIndex = 34;
            Addressprooftype.Borders.ColorIndex = 15;
            Addressprooftype.WrapText = true;
            Addressprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Addressprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Address proof Number
            var AppAddressproofNo = xlWorkSheet.Cells.Range["BC1"];
            AppAddressproofNo.Value = "Applicant Address Proof ";
            AppAddressproofNo.Font.Bold = true;
            AppAddressproofNo.Font.Size = 11;
            AppAddressproofNo.RowHeight = 46;
            AppAddressproofNo.ColumnWidth = 26;
            AppAddressproofNo.Interior.ColorIndex = 34;
            AppAddressproofNo.Borders.ColorIndex = 15;
            AppAddressproofNo.WrapText = true;
            AppAddressproofNo.EntireColumn.NumberFormat = "@";
            AppAddressproofNo.NumberFormat = "@";
            AppAddressproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAddressproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Relationship with Co-Applicant
            var CoappRelationship = xlWorkSheet.Cells.Range["BD1"];
            CoappRelationship.Value = "Relationship with Co-Applicant";
            CoappRelationship.Font.Bold = true;
            CoappRelationship.Font.Size = 11;
            CoappRelationship.RowHeight = 46;
            CoappRelationship.ColumnWidth = 22;
            CoappRelationship.WrapText = true;
            CoappRelationship.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappRelationship.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Applicant Monthly Income
            var ApplicantMonthlyIncome = xlWorkSheet.Cells.Range["BE1"];
            ApplicantMonthlyIncome.Value = "Applicant Monthly Income";
            ApplicantMonthlyIncome.Font.Bold = true;
            ApplicantMonthlyIncome.Font.Size = 11;
            ApplicantMonthlyIncome.RowHeight = 46;
            ApplicantMonthlyIncome.ColumnWidth = 22;
            ApplicantMonthlyIncome.WrapText = true;
            AppAddressproofNo.NumberFormat = "@";
            ApplicantMonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantMonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Name
            var CoappName = xlWorkSheet.Cells.Range["BF1"];
            CoappName.Value = "Co-Applicant Name";
            CoappName.Font.Bold = true;
            CoappName.Font.Size = 11;
            CoappName.RowHeight = 46;
            CoappName.ColumnWidth = 20;
            CoappName.WrapText = true;
            CoappName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Gender
            var CoappGendder = xlWorkSheet.Cells.Range["BG1"];
            CoappGendder.Value = "Co-Applicant Gender";
            CoappGendder.Font.Bold = true;
            CoappGendder.Font.Size = 11;
            CoappGendder.RowHeight = 46;
            CoappGendder.ColumnWidth = 14;
            CoappGendder.WrapText = true;
            CoappGendder.Font.ColorIndex = 3;
            CoappGendder.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappGendder.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Mobile Number
            var Coappnumber = xlWorkSheet.Cells.Range["BH1"];
            Coappnumber.Value = "Co-Applicant Mobile No.";
            Coappnumber.Font.Bold = true;
            Coappnumber.Font.Size = 11;
            Coappnumber.RowHeight = 46;
            Coappnumber.ColumnWidth = 14;
            Coappnumber.Interior.ColorIndex = 34;
            Coappnumber.Borders.ColorIndex = 15;
            Coappnumber.WrapText = true;
            Coappnumber.EntireColumn.NumberFormat = "@";
            Coappnumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Coappnumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant DOB
            var CoappDOB = xlWorkSheet.Cells.Range["BI1"];
            CoappDOB.Value = "Co-Applicant DOB";
            CoappDOB.Font.Bold = true;
            CoappDOB.Font.Size = 11;
            CoappDOB.RowHeight = 46;
            CoappDOB.ColumnWidth = 18;
            CoappDOB.Interior.ColorIndex = 34;
            CoappDOB.Borders.ColorIndex = 15;
            CoappDOB.WrapText = true;
            CoappDOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappDOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Age
            var CoappAge = xlWorkSheet.Cells.Range["BJ1"];
            CoappAge.Value = "Co-Applicant Age";
            CoappAge.Font.Bold = true;
            CoappAge.Font.Size = 11;
            CoappAge.RowHeight = 46;
            CoappAge.ColumnWidth = 20;
            CoappAge.Cells.Interior.ColorIndex = 6;
            CoappAge.WrapText = true;
            CoappAge.Borders.ColorIndex = 15;
            CoappAge.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappAge.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-app ID proof type
            var CoappIDproofType = xlWorkSheet.Cells.Range["BK1"];
            CoappIDproofType.Value = "ID Proof Type";
            CoappIDproofType.Font.Bold = true;
            CoappIDproofType.Font.Size = 11;
            CoappIDproofType.RowHeight = 46;
            CoappIDproofType.ColumnWidth = 16;
            CoappIDproofType.Interior.ColorIndex = 34;
            CoappIDproofType.Borders.ColorIndex = 15;
            CoappIDproofType.WrapText = true;
            CoappIDproofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappIDproofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-app Id proof Number
            var CoappIDproofNumber = xlWorkSheet.Cells.Range["BL1"];
            CoappIDproofNumber.Value = "Co-Applicant ID Proof No.";
            CoappIDproofNumber.Font.Bold = true;
            CoappIDproofNumber.Font.Size = 11;
            CoappIDproofNumber.RowHeight = 46;
            CoappIDproofNumber.ColumnWidth = 26;
            CoappIDproofNumber.WrapText = true;
            CoappIDproofNumber.NumberFormat = "@";
            CoappIDproofNumber.EntireColumn.NumberFormat = "@";
            CoappIDproofNumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappIDproofNumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address Proof Type
            var CoappAddressProoftype = xlWorkSheet.Cells.Range["BM1"];
            CoappAddressProoftype.Value = "Co-Applicant Address Proof Type";
            CoappAddressProoftype.Font.Bold = true;
            CoappAddressProoftype.Font.Size = 11;
            CoappAddressProoftype.RowHeight = 46;
            CoappAddressProoftype.ColumnWidth = 16;
            CoappAddressProoftype.Interior.ColorIndex = 34;
            CoappAddressProoftype.Borders.ColorIndex = 15;
            CoappAddressProoftype.WrapText = true;
            CoappAddressProoftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Address Proof No
            var CoappAddressproofNumber = xlWorkSheet.Cells.Range["BN1"];
            CoappAddressproofNumber.Value = "Co-Applicant Adddress Proof No.";
            CoappAddressproofNumber.Font.Bold = true;
            CoappAddressproofNumber.Font.Size = 11;
            CoappAddressproofNumber.RowHeight = 46;
            CoappAddressproofNumber.ColumnWidth = 26;
            CoappAddressproofNumber.WrapText = true;
            CoappAddressproofNumber.Interior.ColorIndex = 34;
            CoappAddressproofNumber.Borders.ColorIndex = 15;
            CoappAddressproofNumber.EntireColumn.NumberFormat = "@";
            CoappAddressproofNumber.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoappAddressproofNumber.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Relationship wiht Applicant
            var AppRelationship = xlWorkSheet.Cells.Range["BO1"];
            AppRelationship.Value = "Relationship with Co-Applicant";
            AppRelationship.Font.Bold = true;
            AppRelationship.Font.Size = 11;
            AppRelationship.RowHeight = 46;
            AppRelationship.ColumnWidth = 19;
            AppRelationship.WrapText = true;
            AppRelationship.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppRelationship.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Co-Applicant Monthly Income
            var CoApplicantMonthlyIncome = xlWorkSheet.Cells.Range["BP1"];
            CoApplicantMonthlyIncome.Value = "Co-Applicant Monthly HH Income";
            CoApplicantMonthlyIncome.Font.Bold = true;
            CoApplicantMonthlyIncome.Font.Size = 11;
            CoApplicantMonthlyIncome.RowHeight = 46;
            CoApplicantMonthlyIncome.ColumnWidth = 21;
            CoApplicantMonthlyIncome.WrapText = true;
            Coappnumber.EntireColumn.NumberFormat = "@";
            CoApplicantMonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CoApplicantMonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Applicant and Co-Applicant Monthly HH Income
            var ApplicantCoAppMonthlyHHIncome = xlWorkSheet.Cells.Range["BQ1"];
            ApplicantCoAppMonthlyHHIncome.Value = "Applicant and Co-Applicant Monthly Household Income";
            ApplicantCoAppMonthlyHHIncome.Font.Bold = true;
            ApplicantCoAppMonthlyHHIncome.Font.Size = 11;
            ApplicantCoAppMonthlyHHIncome.RowHeight = 46;
            ApplicantCoAppMonthlyHHIncome.ColumnWidth = 21;
            ApplicantCoAppMonthlyHHIncome.WrapText = true;
            ApplicantCoAppMonthlyHHIncome.EntireColumn.NumberFormat = "@";
            ApplicantCoAppMonthlyHHIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ApplicantCoAppMonthlyHHIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Unmarried Earning-I Child Name
            var UnmarriedC1Name = xlWorkSheet.Cells.Range["BR1"];
            UnmarriedC1Name.Value = "Unmarried Earning-I Child Name";
            UnmarriedC1Name.Font.Bold = true;
            UnmarriedC1Name.Font.Size = 11;
            UnmarriedC1Name.RowHeight = 46;
            UnmarriedC1Name.ColumnWidth = 21;
            UnmarriedC1Name.WrapText = true;
            UnmarriedC1Name.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1Name.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Unmarried Earning I Child Gender
            var UnmarriedC1Gender = xlWorkSheet.Cells.Range["BS1"];
            UnmarriedC1Gender.Value = "Unmarried Earning I Child Gender";
            UnmarriedC1Gender.Font.Bold = true;
            UnmarriedC1Gender.Font.Size = 11;
            UnmarriedC1Gender.RowHeight = 46;
            UnmarriedC1Gender.ColumnWidth = 21;
            UnmarriedC1Gender.WrapText = true;
            UnmarriedC1Gender.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1Gender.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Mobile No
            var UnmarriedC1MobileNo = xlWorkSheet.Cells.Range["BT1"];
            UnmarriedC1MobileNo.Value = "Unmarried Earning I Child Mobile No";
            UnmarriedC1MobileNo.Font.Bold = true;
            UnmarriedC1MobileNo.Font.Size = 11;
            UnmarriedC1MobileNo.RowHeight = 46;
            UnmarriedC1MobileNo.ColumnWidth = 21;
            UnmarriedC1MobileNo.WrapText = true;
            UnmarriedC1MobileNo.EntireColumn.NumberFormat = "@";
            UnmarriedC1MobileNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1MobileNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child's DOB
            var UnmarriedC1DOB = xlWorkSheet.Cells.Range["BU1"];
            UnmarriedC1DOB.Value = "Unmarried Earning I Child's DOB";
            UnmarriedC1DOB.Font.Bold = true;
            UnmarriedC1DOB.Font.Size = 11;
            UnmarriedC1DOB.RowHeight = 46;
            UnmarriedC1DOB.ColumnWidth = 21;
            UnmarriedC1DOB.WrapText = true;
            UnmarriedC1DOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1DOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child's Age
            var UnmarriedC1Age = xlWorkSheet.Cells.Range["BV1"];
            UnmarriedC1Age.Value = "Unmarried Earning I Child's Age";
            UnmarriedC1Age.Font.Bold = true;
            UnmarriedC1Age.Font.Size = 11;
            UnmarriedC1Age.RowHeight = 46;
            UnmarriedC1Age.ColumnWidth = 21;
            UnmarriedC1Age.EntireColumn.NumberFormat = "@";
            UnmarriedC1Age.WrapText = true;
            UnmarriedC1Age.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1Age.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Id proof type
            var UnmarriedC1IdProofType = xlWorkSheet.Cells.Range["BW1"];
            UnmarriedC1IdProofType.Value = "Unmarried Earning I Id proof type";
            UnmarriedC1IdProofType.Font.Bold = true;
            UnmarriedC1IdProofType.Font.Size = 11;
            UnmarriedC1IdProofType.RowHeight = 46;
            UnmarriedC1IdProofType.ColumnWidth = 21;
            UnmarriedC1IdProofType.WrapText = true;
            UnmarriedC1IdProofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1IdProofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Id Proof No.
            var UnmarriedC1IdProofNo = xlWorkSheet.Cells.Range["BX1"];
            UnmarriedC1IdProofNo.Value = "Unmarried Earning I Child Id Proof No.";
            UnmarriedC1IdProofNo.Font.Bold = true;
            UnmarriedC1IdProofNo.Font.Size = 11;
            UnmarriedC1IdProofNo.RowHeight = 46;
            UnmarriedC1IdProofNo.ColumnWidth = 21;
            UnmarriedC1IdProofNo.WrapText = true;
            UnmarriedC1IdProofNo.EntireColumn.NumberFormat = "@";
            UnmarriedC1IdProofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1IdProofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Address Proof Type
            var UnmarriedC1AddressProofType = xlWorkSheet.Cells.Range["BY1"];
            UnmarriedC1AddressProofType.Value = "Unmarried Earning I Child Address Proof Type";
            UnmarriedC1AddressProofType.Font.Bold = true;
            UnmarriedC1AddressProofType.Font.Size = 11;
            UnmarriedC1AddressProofType.RowHeight = 46;
            UnmarriedC1AddressProofType.ColumnWidth = 21;
            UnmarriedC1AddressProofType.WrapText = true;
            UnmarriedC1AddressProofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1AddressProofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Address Proof No. 
            var UnmarriedC1AddressProofNo = xlWorkSheet.Cells.Range["BZ1"];
            UnmarriedC1AddressProofNo.Value = "Unmarried Earning I Child Address Proof No.";
            UnmarriedC1AddressProofNo.Font.Bold = true;
            UnmarriedC1AddressProofNo.Font.Size = 11;
            UnmarriedC1AddressProofNo.RowHeight = 46;
            UnmarriedC1AddressProofNo.ColumnWidth = 21;
            UnmarriedC1AddressProofNo.WrapText = true;
            UnmarriedC1AddressProofNo.EntireColumn.NumberFormat = "@";
            UnmarriedC1AddressProofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1AddressProofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Monthly Income
            var UnmarriedC1MonthlyIncome = xlWorkSheet.Cells.Range["CA1"];
            UnmarriedC1MonthlyIncome.Value = "Unmarried Earning I Child Monthly Income";
            UnmarriedC1MonthlyIncome.Font.Bold = true;
            UnmarriedC1MonthlyIncome.Font.Size = 11;
            UnmarriedC1MonthlyIncome.RowHeight = 46;
            UnmarriedC1MonthlyIncome.ColumnWidth = 21;
            UnmarriedC1MonthlyIncome.WrapText = true;
            UnmarriedC1MonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC1MonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


            //Unmarried Earning-II Child Name
            var UnmarriedC2Name = xlWorkSheet.Cells.Range["CB1"];
            UnmarriedC2Name.Value = "Unmarried Earning - II Child Name";
            UnmarriedC2Name.Font.Bold = true;
            UnmarriedC2Name.Font.Size = 11;
            UnmarriedC2Name.RowHeight = 46;
            UnmarriedC2Name.ColumnWidth = 21;
            UnmarriedC2Name.WrapText = true;
            UnmarriedC2Name.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2Name.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Unmarried Earning I Child Gender
            var UnmarriedC2Gender = xlWorkSheet.Cells.Range["CC1"];
            UnmarriedC2Gender.Value = "Unmarried Earning II Child Gender";
            UnmarriedC2Gender.Font.Bold = true;
            UnmarriedC2Gender.Font.Size = 11;
            UnmarriedC2Gender.RowHeight = 46;
            UnmarriedC2Gender.ColumnWidth = 21;
            UnmarriedC2Gender.WrapText = true;
            UnmarriedC2Gender.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2Gender.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Mobile No
            var UnmarriedC2MobileNo = xlWorkSheet.Cells.Range["CD1"];
            UnmarriedC2MobileNo.Value = "Unmarried Earning II Child Mobile No";
            UnmarriedC2MobileNo.Font.Bold = true;
            UnmarriedC2MobileNo.Font.Size = 11;
            UnmarriedC2MobileNo.RowHeight = 46;
            UnmarriedC2MobileNo.ColumnWidth = 21;
            UnmarriedC2MobileNo.EntireColumn.NumberFormat = "@";
            UnmarriedC2MobileNo.WrapText = true;
            UnmarriedC2MobileNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2MobileNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child's DOB
            var UnmarriedC2DOB = xlWorkSheet.Cells.Range["CE1"];
            UnmarriedC2DOB.Value = "Unmarried Earning II Child's DOB";
            UnmarriedC2DOB.Font.Bold = true;
            UnmarriedC2DOB.Font.Size = 11;
            UnmarriedC2DOB.RowHeight = 46;
            UnmarriedC2DOB.ColumnWidth = 21;
            UnmarriedC2DOB.WrapText = true;
            UnmarriedC2DOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2DOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child's Age
            var UnmarriedC2Age = xlWorkSheet.Cells.Range["CF1"];
            UnmarriedC2Age.Value = "Unmarried Earning II Child's Age";
            UnmarriedC2Age.Font.Bold = true;
            UnmarriedC2Age.Font.Size = 11;
            UnmarriedC2Age.RowHeight = 46;
            UnmarriedC2Age.ColumnWidth = 21;
            UnmarriedC2Age.WrapText = true;
            UnmarriedC2Age.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2Age.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Id proof type
            var UnmarriedC2IdProofType = xlWorkSheet.Cells.Range["CG1"];
            UnmarriedC2IdProofType.Value = "Unmarried Earning II Id proof type";
            UnmarriedC2IdProofType.Font.Bold = true;
            UnmarriedC2IdProofType.Font.Size = 11;
            UnmarriedC2IdProofType.RowHeight = 46;
            UnmarriedC2IdProofType.ColumnWidth = 21;
            UnmarriedC2IdProofType.WrapText = true;
            UnmarriedC2IdProofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2IdProofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Id Proof No.
            var UnmarriedC2IdProofNo = xlWorkSheet.Cells.Range["CH1"];
            UnmarriedC2IdProofNo.Value = "Unmarried Earning II Child Id Proof No.";
            UnmarriedC2IdProofNo.Font.Bold = true;
            UnmarriedC2IdProofNo.Font.Size = 11;
            UnmarriedC2IdProofNo.RowHeight = 46;
            UnmarriedC2IdProofNo.ColumnWidth = 21;
            UnmarriedC2IdProofNo.WrapText = true;
            UnmarriedC2IdProofNo.EntireColumn.NumberFormat = "@";
            UnmarriedC2IdProofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2IdProofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Address Proof Type
            var UnmarriedC2AddressProofType = xlWorkSheet.Cells.Range["CI1"];
            UnmarriedC2AddressProofType.Value = "Unmarried Earning II Child Address Proof Type";
            UnmarriedC2AddressProofType.Font.Bold = true;
            UnmarriedC2AddressProofType.Font.Size = 11;
            UnmarriedC2AddressProofType.RowHeight = 46;
            UnmarriedC2AddressProofType.ColumnWidth = 21;
            UnmarriedC2AddressProofType.WrapText = true;
            UnmarriedC2AddressProofType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2AddressProofType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Address Proof No. 
            var UnmarriedC2AddressProofNo = xlWorkSheet.Cells.Range["CJ1"];
            UnmarriedC2AddressProofNo.Value = "Unmarried Earning II Child Address Proof No.";
            UnmarriedC2AddressProofNo.Font.Bold = true;
            UnmarriedC2AddressProofNo.Font.Size = 11;
            UnmarriedC2AddressProofNo.RowHeight = 46;
            UnmarriedC2AddressProofNo.ColumnWidth = 21;
            UnmarriedC2AddressProofNo.WrapText = true;
            UnmarriedC2AddressProofNo.EntireColumn.NumberFormat = "@";
            UnmarriedC2AddressProofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2AddressProofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Unmarried Earning I Child Monthly Income
            var UnmarriedC2MonthlyIncome = xlWorkSheet.Cells.Range["CK1"];
            UnmarriedC2MonthlyIncome.Value = "Unmarried Earning II Child Monthly Income";
            UnmarriedC2MonthlyIncome.Font.Bold = true;
            UnmarriedC2MonthlyIncome.Font.Size = 11;
            UnmarriedC2MonthlyIncome.RowHeight = 46;
            UnmarriedC2MonthlyIncome.ColumnWidth = 21;
            UnmarriedC2MonthlyIncome.WrapText = true;
            UnmarriedC2MonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            UnmarriedC2MonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Intrest Rate
            var Intrestrate = xlWorkSheet.Cells.Range["CL1"];
            Intrestrate.Value = "Intrest Rate";
            Intrestrate.Font.Bold = true;
            Intrestrate.Font.Size = 11;
            Intrestrate.RowHeight = 46;
            Intrestrate.ColumnWidth = 14;
            Intrestrate.WrapText = true;
            Intrestrate.Font.ColorIndex = 3;
            Intrestrate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Intrestrate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
           
           
            //Land Holding Proof
            var Guarantee = xlWorkSheet.Cells.Range["CM1"];
            Guarantee.Value = "Land Holding Proof";
            Guarantee.Font.Bold = true;
            Guarantee.Font.Size = 11;
            Guarantee.RowHeight = 46;
            Guarantee.ColumnWidth = 19;
            Guarantee.WrapText = true;
            Guarantee.Font.ColorIndex = 37;
            Guarantee.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Guarantee.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            

            FillHimarkDate(xlWorkSheet, RequestList);
            string dir = "";
            try
            {
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report\\Hi-Mark Reports");
                if (Directory.Exists(dir))
                {
                    string FileName = dir + "\\Hi-Mark_" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    string FileName = dir + "\\Hi-Mark_" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
               
            }
            finally
            {
                xlWorkBook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
                xlWorkBook = null;
                File.Delete(dir + "\\temp.xlsx");
            }
        }

        private void FillHimarkDate(Worksheet xlWorkSheet, List<HimarkModel> himarkdate)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            MainWindow.TimeBuilder.Append("\nStarting Time for Write HimarkFile : " + stopwatch1.Elapsed.Milliseconds.ToString());
            int i = 1;
            int RowStart = 2;
            foreach(HimarkModel hm in himarkdate)
            {
                
                xlWorkSheet.Cells[RowStart, 1] = i;
                xlWorkSheet.Cells[RowStart, 2] = hm.CBOName;
                xlWorkSheet.Cells[RowStart, 3] = hm.FIGName;
                xlWorkSheet.Cells[RowStart, 4] = hm.FIGFormationDate;
                //xlWorkSheet.Cells[RowStart, 5] = hm.loandetails.SHGName.ToUpper();
                xlWorkSheet.Cells[RowStart, 5] = hm.FIGVillage;
                xlWorkSheet.Cells[RowStart, 6] = hm.FIGTaluk;
                xlWorkSheet.Cells[RowStart, 7] = hm.FIGDistrict;
                xlWorkSheet.Cells[RowStart, 8] = hm.FIGState;
                //End Self Help Group Details
                xlWorkSheet.Cells[RowStart, 9] = "";//fig savings
                xlWorkSheet.Cells[RowStart, 10] = "";// fig bank name
                xlWorkSheet.Cells[RowStart, 11] = "";// fig account no
                xlWorkSheet.Cells[RowStart, 12] = ""; // fig ifsc code
                xlWorkSheet.Cells[RowStart, 13] = ""; // fig branch name
                xlWorkSheet.Cells[RowStart, 14] = hm.EligibleLoanAmount;
                xlWorkSheet.Cells[RowStart, 15] = ""; // application no
                xlWorkSheet.Cells[RowStart, 16] = ""; //empid
                xlWorkSheet.Cells[RowStart, 17] = "";// hm.RMName; 
                xlWorkSheet.Cells[RowStart, 18] = "";// hm.DateOfApplication.ToString("dd-MMM-yyyy");
                xlWorkSheet.Cells[RowStart, 19] = hm.Member;
                xlWorkSheet.Cells[RowStart, 20] = hm.ApplicantName;
                xlWorkSheet.Cells[RowStart, 21] = hm.ApplicantFatherName;
                xlWorkSheet.Cells[RowStart, 22] = hm.ApplicantMotherName;
                xlWorkSheet.Cells[RowStart, 23] = hm.DoorNO;
                xlWorkSheet.Cells[RowStart, 24] = hm.StreetName;
                xlWorkSheet.Cells[RowStart, 25] = hm.Village;
                xlWorkSheet.Cells[RowStart, 26] = hm.Taluk;
                xlWorkSheet.Cells[RowStart, 27] = hm.District;
                xlWorkSheet.Cells[RowStart, 28] = hm.State;
                xlWorkSheet.Cells[RowStart, 29] = hm.Pincode;
                xlWorkSheet.Cells[RowStart, 30] = hm.ApplicantMobile;
                xlWorkSheet.Cells[RowStart, 31] = ""; //applicant emailid
                xlWorkSheet.Cells[RowStart, 32] = hm.Occupation;
                xlWorkSheet.Cells[RowStart, 33] = hm.Education;
                xlWorkSheet.Cells[RowStart, 34] = hm.Taluk;
                xlWorkSheet.Cells[RowStart, 35] = hm.Residence;
                xlWorkSheet.Cells[RowStart, 36] = hm.TypeOFResidence;
                xlWorkSheet.Cells[RowStart, 37] = hm.LandHolding;
                xlWorkSheet.Cells[RowStart, 38] = hm.Caste;
                xlWorkSheet.Cells[RowStart, 39] = hm.Religion;
                xlWorkSheet.Cells[RowStart, 40] = hm.ApplicantBankName;
                xlWorkSheet.Cells[RowStart, 41] = hm.ApplicantAccountNO;
                xlWorkSheet.Cells[RowStart, 42] = hm.ApplicantIFSCcode;
                //applicant household expense
                xlWorkSheet.Cells[RowStart, 43] = hm.ApplicantBranchName;
                xlWorkSheet.Cells[RowStart, 44] = hm.ApplicantHHExpense;
                xlWorkSheet.Cells[RowStart, 45] = hm.LoanPurpose;
                xlWorkSheet.Cells[RowStart, 46] = hm.LoanTenure;
                xlWorkSheet.Cells[RowStart, 47] = hm.SamFinBranch;
                xlWorkSheet.Cells[RowStart, 48] = hm.ApplicantGender;
                xlWorkSheet.Cells[RowStart, 49] = hm.ApplicantMobile;
                xlWorkSheet.Cells[RowStart, 50] = hm.ApplicantDOB;
                xlWorkSheet.Cells[RowStart, 51] = hm.ApplicantAge;
                xlWorkSheet.Cells[RowStart, 52] = hm.ApplicantIDProofType;
                xlWorkSheet.Cells[RowStart, 53] = hm.ApplicantIDProofNo;
                xlWorkSheet.Cells[RowStart, 54] = hm.ApplicantAddressProofType;
                xlWorkSheet.Cells[RowStart, 55] = hm.ApplicantAddressProofNo;
                xlWorkSheet.Cells[RowStart, 56] = hm.RelationshipWithCOapplicant;
                xlWorkSheet.Cells[RowStart, 57] = hm.MonthlyIncome;
                xlWorkSheet.Cells[RowStart, 58] = hm.COapplicantName;
                xlWorkSheet.Cells[RowStart, 59] = hm.COapplicantGender;
                xlWorkSheet.Cells[RowStart, 60] = hm.CoApplicantMobileNo; // co applicant mobile number
                xlWorkSheet.Cells[RowStart, 61] = hm.COapplicantDOB;
                xlWorkSheet.Cells[RowStart, 62] = hm.COapplicantAge;
                xlWorkSheet.Cells[RowStart, 63] = hm.COapplicantIDProofType;
                xlWorkSheet.Cells[RowStart, 64] = hm.COapplicantIDProofNo;
                xlWorkSheet.Cells[RowStart, 65] = hm.COapplicantAddressProofType;
                xlWorkSheet.Cells[RowStart, 66] = hm.COapplicantAddressProofNo;
                xlWorkSheet.Cells[RowStart, 67] = hm.RelationshipWithApplicant;
                xlWorkSheet.Cells[RowStart, 68] = hm.GMonthlyIncome; // Co applicant monthly hh Income
                xlWorkSheet.Cells[RowStart, 69] = hm.GMonthlyIncome + Convert.ToInt32(hm.MonthlyIncome); // Applicant and co applicant monthly HH Income
                xlWorkSheet.Cells[RowStart, 70] = hm.C1Name; // Unmarried earning child1 name
                xlWorkSheet.Cells[RowStart, 71] = hm.C1Gender; // Unmarried Earning I Child Gender
                xlWorkSheet.Cells[RowStart, 72] = hm.C1Mobile; // Unmarried Earning I Child Mobile No
                xlWorkSheet.Cells[RowStart, 73] = hm.C1DOB; // Unmarried Earning I Child's DOB
                xlWorkSheet.Cells[RowStart, 74] = hm.C1Age; // Unmarried Earning I Child's Age
                xlWorkSheet.Cells[RowStart, 75] = hm.C1IdProofType; // Unmarried Earning I Id proof type
                xlWorkSheet.Cells[RowStart, 76] = hm.C1IdProofNo; // Unmarried Earning I Child Id Proof No.
                xlWorkSheet.Cells[RowStart, 77] = hm.C1AddressProofType; // Unmarried Earning I Child Address Proof Type
                xlWorkSheet.Cells[RowStart, 78] = hm.C1AddressProofNo; // Unmarried Earning I Child Address Proof No.
                xlWorkSheet.Cells[RowStart, 79] = hm.C1MonthlyIncome; // Unmarried Earning I Child Monthly Income
                xlWorkSheet.Cells[RowStart, 80] = hm.C2Name; // unmarried Earning child2 name
                xlWorkSheet.Cells[RowStart, 81] = hm.C2Gender; // Unmarried Earning II Child Gender
                xlWorkSheet.Cells[RowStart, 82] = hm.C2Mobile; // Unmarried Earning II Child Mobile No
                xlWorkSheet.Cells[RowStart, 83] = hm.C2DOB; // Unmarried Earning I Child's DOB
                xlWorkSheet.Cells[RowStart, 84] = hm.C2Age; // Unmarried Earning II Child's Age
                xlWorkSheet.Cells[RowStart, 85] = hm.C2IdProofType; // Unmarried Earning II Id proof type
                xlWorkSheet.Cells[RowStart, 86] = hm.C2IdProofNo; // Unmarried Earning II Child Id Proof No.
                xlWorkSheet.Cells[RowStart, 87] = hm.C2AddressProofType; // Unmarried Earning II Child Address Proof Type
                xlWorkSheet.Cells[RowStart, 88] = hm.C2AddressProofNo; // Unmarried Earning II Child Address Proof No.
                xlWorkSheet.Cells[RowStart, 89] = hm.C2MonthlyIncome; // Unmarried Earning II Child Monthly Income
                xlWorkSheet.Cells[RowStart, 90] = ""; // 
                xlWorkSheet.Cells[RowStart, 91] = hm.LandHoldingProof; // 



                //xlWorkSheet.Cells[RowStart, 58] = hm.MonthlyExpenses;
                // xlWorkSheet.Cells[RowStart, 68] = hm.DoorNO+","+hm.StreetName+","+hm.Village+","+hm.Taluk+","+hm.District+","+hm.State+".";

                RowStart++;
                i++;
            }
            MainWindow.TimeBuilder.Append("\nStarting Time for CreateHimarkFile : " + stopwatch1.Elapsed.Milliseconds.ToString());
            stopwatch1.Stop();
        }






    }
}
