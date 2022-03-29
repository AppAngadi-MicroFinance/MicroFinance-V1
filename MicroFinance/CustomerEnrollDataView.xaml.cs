using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MicroFinance.ViewModel;
using MicroFinance.APIModal;
using System.Collections.ObjectModel;
using MicroFinance.Repository;
using System.Net.Http;
using Newtonsoft.Json;
using MicroFinance.Modal;
using System.Data.SqlClient;
using System.Data;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CustomerEnrollDataView.xaml
    /// </summary>
    public partial class CustomerEnrollDataView : Page
    {
        public VMCustomerEnrollData _customerdata = new VMCustomerEnrollData();
        private VMCustomerDetails CustomerDetails = new VMCustomerDetails();
        private VMCustomerGroup CustomerGroup = new VMCustomerGroup();
        private VMGuarenteeDetails GuarenteeDetails = new VMGuarenteeDetails();
        private VMNomineeDetails NomineeDetails = new VMNomineeDetails();
        private List<string> GenderList = new List<string> { "Male", "Female" };
        private List<string> ResidencyList = new List<string> { "OWN HOUSE", "RENT HOUSE" };
        private List<string> ResidencyTypeList = new List<string> { "HUT HOUSE", "TILED ROOF HOUSE", "CONCRETE HOUSE" };
        private List<string> LandHoldingList = new List<string> { "YES", "NO" };
        private List<string> ReligionList = new List<string> { "CHRISTIAN", "HINDU", "MUSLIM" };
        
        private List<string> RelationshipList = new List<string> { "FATHER", "MOTHER", "SISTER", "WIFE", "HUSBAND", "SON", "DAUGHTER" };
        private List<string> LoanPurposeList = new List<string> { "AGRI" };
        public List<string> CommunityList = new List<string> { "BCM", "BC", "MBC", "SC", "ST", "Others" };

        private List<string> RegionList = new List<string>();
        private ObservableCollection<EmployeeViewModel> EmployeeList = new ObservableCollection<EmployeeViewModel>();
        private List<TimeTableViewModel> CenterList = new List<TimeTableViewModel>();
        private List<PeerGroupViewModal> PeerGroups = new List<PeerGroupViewModal>();
        private List<BranchViewModel> BranchList = new List<BranchViewModel>();
        public ObservableCollection<CustomerEnrollMetaData> CustomerEnrollMetaDataList = new ObservableCollection<CustomerEnrollMetaData>();

        public CustomerEnrollDataView()
        {
            InitializeComponent();
           
        }

        
        public CustomerEnrollDataView(VMCustomerEnrollData CustomerData)
        {
            InitializeComponent();
            _customerdata = CustomerData;
            FormDataForBinding();
            GenderCombo.ItemsSource = GenderList;
            GGenderCombo.ItemsSource = GenderList;
            ReligionCombo.ItemsSource = ReligionList;
            ComminityCombo.ItemsSource = CommunityList;
            HousingTypeCombo.ItemsSource = ResidencyTypeList;
            RelationShipCombo.ItemsSource = RelationshipList;
            
            NGenderCombo.ItemsSource = GenderList;
            NRelationShipCombo.ItemsSource = RelationshipList;
            CustomerDetailsGrid.DataContext = CustomerDetails;
            GuarenteeDetailsGrid.DataContext = GuarenteeDetails;
            NomineeDetailsGrid.DataContext = NomineeDetails;
            LoanPurposeCombo.ItemsSource = LoanPurposeList;
            LoadData();
        }

        void LoadData()
        {
            string EmpID = MainWindow.LoginDesignation.EmpId;
            PeerGroups = CustomerRepository.GetPeerGroups(EmpID);
            BranchList = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == MainWindow.LoginDesignation.BranchId).ToList();
            CenterList = MainWindow.BasicDetails.CenterList.Where(temp => temp.EmpId == EmpID).Select(temp=>new TimeTableViewModel {SHGName=temp.SHGName,SHGId=temp.SHGId,CollectionDay=temp.CollectionDay,CollectionTime=temp.CollectionTime }).OrderBy(temp=>temp.SHGName).ToList();
            
            BranchCombo.ItemsSource = BranchList;BranchCombo.SelectedIndex = 0;BranchCombo.IsEnabled = false;
            string RegionName= MainWindow.LoginDesignation.RegionName;
            RegionCombo.Items.Add(RegionName);
            RegionCombo.SelectedIndex = 0;RegionCombo.IsEnabled = false;
            CenterNameCombo.ItemsSource = CenterList;
        }
        void FormDataForBinding()
        {
            //Customer Object
            CustomerDetails.Name = _customerdata.CustomerName;
            CustomerDetails.FatherName = _customerdata.FatherName;
            CustomerDetails.MotherName = _customerdata.MotherName;
            CustomerDetails.Dob = _customerdata.DateOfBirth;
            CustomerDetails.Age = _customerdata.Age;
            CustomerDetails.Gender = _customerdata.Gender;
            CustomerDetails.Mobile = _customerdata.ContactNumber;
            CustomerDetails.AadharNumber = _customerdata.AadharNumber;
            CustomerDetails.Religion = _customerdata.Religion;
            CustomerDetails.Caste = _customerdata.Caste;
            CustomerDetails.Community = _customerdata.Community;
            CustomerDetails.Education = _customerdata.Education;
            CustomerDetails.FamilyMembers = _customerdata.FamilyMembers;
            CustomerDetails.EarningMembers = _customerdata.EarningMembers;
            CustomerDetails.Occupation = _customerdata.Occupation;
            CustomerDetails.MonthlyIncome = _customerdata.MonthlyIncome;
            CustomerDetails.MonthlyExpenses = _customerdata.MonthlyExpence;
            CustomerDetails.DoorNo = _customerdata.DoorNo;
            CustomerDetails.StreetName = _customerdata.StreetName;
            CustomerDetails.Village = _customerdata.Village;
            CustomerDetails.City = _customerdata.City;
            CustomerDetails.State = _customerdata.GState;
            CustomerDetails.Pincode =Convert.ToInt32( _customerdata.PinCode);
            CustomerDetails.HousingType = _customerdata.HousingType;
            CustomerDetails.AddressProofName = _customerdata.AddressProofName;
            CustomerDetails.PhotoProofName = _customerdata.PhotoProofName;
            CustomerDetails.IsBankDetails = true;
            CustomerDetails.IsAddressProof = (_customerdata.AddressProof != null) ? true : false;
            CustomerDetails.IsPhotoProof = (_customerdata.PhotoProof != null) ? true : false;
            CustomerDetails.IsCombinePhoto = (_customerdata.CombinePhoto != null) ? true : false;
            CustomerDetails.BankACHolderName = _customerdata.AccountHolderName;
            CustomerDetails.BankAccountNo = _customerdata.AccountNumber;
            CustomerDetails.BankName = _customerdata.BankName;
            CustomerDetails.BankBranchName = _customerdata.BranchName;
            CustomerDetails.IFSCCode = _customerdata.IFSCCode;
            CustomerDetails.MICRCode = _customerdata.MICRCode;
            CustomerDetails.AddressProof = _customerdata.AddressProof;
            CustomerDetails.PhotoProof = _customerdata.PhotoProof;
            CustomerDetails.ProfilePhoto = _customerdata.CombinePhoto;
            CustomerDetails.GuarenteeStatus = true;
            CustomerDetails.NomineeStatus = true;
            CustomerDetails.CustomerStatus =0;
            CustomerDetails.IsActive = false;
            CustomerDetails.HusbandName = _customerdata.GuardianName;
            CustomerDetails.YearlyIncome = _customerdata.FamilyYearlyIncome;
            CustomerDetails.IsCombinePhoto = true;
            CustomerDetails.CombinePhoto = _customerdata.CombinePhoto;
            CustomerDetails.PhotoProofNo = _customerdata.PhotoProofNumber;
            CustomerDetails.AddressProofNo = _customerdata.AddressProofNumber;
            CustomerDetails.Residency = _customerdata.LandType;
            CustomerDetails.LandHolding = _customerdata.LandHolding;
            CustomerDetails.LandVolume = _customerdata.LandVolume;

            //GuarenteeObject

            GuarenteeDetails.Name = _customerdata.GName;
            GuarenteeDetails.Dob = _customerdata.GDateOfBirth;
            GuarenteeDetails.Age = _customerdata.GAge;
            GuarenteeDetails.Mobile = _customerdata.GContactNumber;
            GuarenteeDetails.Occupation = _customerdata.GOccupation;
            GuarenteeDetails.RelationShip = _customerdata.GRelationShip;
            GuarenteeDetails.DoorNo = _customerdata.GDoorNo;
            GuarenteeDetails.StreetName = _customerdata.GStreetName;
            GuarenteeDetails.Village = _customerdata.GVillage;
            GuarenteeDetails.City = _customerdata.GCity;
            GuarenteeDetails.State = _customerdata.GState;
            int intres= 0;
            bool res = int.TryParse(_customerdata.PinCode, out intres);
            GuarenteeDetails.Pincode = (res) ? intres : 0;
            GuarenteeDetails.AddressProofName = _customerdata.GAddressProofName;
            GuarenteeDetails.AddressProofNumber = _customerdata.GAddressProofNumber;
            GuarenteeDetails.PhotoProofName = _customerdata.GPhotoProofName;
            GuarenteeDetails.PhotoProofNumber = _customerdata.GPhotoProofNumber;
            GuarenteeDetails.IsAddressProof = false;
            GuarenteeDetails.IsPhotoProof = false;
            GuarenteeDetails.IsProfilePhoto = false;
            GuarenteeDetails.Gender = _customerdata.GGender;


            //Nominee Details
            NomineeDetails.Name = _customerdata.NName;
            NomineeDetails.Dob = _customerdata.NDateOfBirth;
            NomineeDetails.Age = _customerdata.NAge;
            NomineeDetails.Mobile = _customerdata.NContactNumber;
            NomineeDetails.Occupation = _customerdata.NOccupation;
            NomineeDetails.RelationShip = _customerdata.NRelationShip;
            NomineeDetails.DoorNo = _customerdata.NDoorNo;
            NomineeDetails.StreetName = _customerdata.NStreetName;
            NomineeDetails.Village = _customerdata.NVillage;
            NomineeDetails.City = _customerdata.NCity;
            NomineeDetails.State = _customerdata.NState;
            int intres1 = 0;
            bool res1 = int.TryParse(_customerdata.PinCode, out intres);
            NomineeDetails.Pincode = (res1) ? intres1 : 0;
            NomineeDetails.AddressProofName = _customerdata.NAddressProofName;
            NomineeDetails.PhotoProofName = _customerdata.NPhotoProofName;
            NomineeDetails.AddressProofNumber = _customerdata.nAddressProofNumber;
            NomineeDetails.PhotoProofNumber = _customerdata.NPhotoProofNumber;
            NomineeDetails.IsAddressProof = false;
            NomineeDetails.IsPhotoProof = false;
            NomineeDetails.IsProfilePhoto = false;
            NomineeDetails.Gender = _customerdata.NGender;





        }

        private void CenterNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CenterNameCombo.SelectedIndex!=-1)
            {
                TimeTableViewModel SelectedCenter = CenterNameCombo.SelectedItem as TimeTableViewModel;
                LoadPeerGroup(SelectedCenter.SHGId);
                
            }
        }

        void LoadPeerGroup(string CenterId)
        {
            PeerGroupCombo.Items.Clear();
            foreach(PeerGroupViewModal Group in PeerGroups)
            {
                if(Group.SHGID==CenterId)
                {
                    PeerGroupCombo.Items.Add(Group); 
                }
            }
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerVerifyPanel.Visibility = Visibility.Visible;
            PanelGrid.IsEnabled = false;
            ButtonPanel.IsEnabled = false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerVerifyPanel.Visibility = Visibility.Collapsed;
            PanelGrid.IsEnabled = true;
            ButtonPanel.IsEnabled = true;
        }
        string BranchName = "";

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValidEntry())
                {
                    BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                    string BranchID = SelectedBranch.BranchId;
                    BranchName = SelectedBranch.BranchName;
                    TimeTableViewModel SelectedCenter = CenterNameCombo.SelectedItem as TimeTableViewModel;
                    string CenterID = SelectedCenter.SHGId;
                    string EmpId = MainWindow.LoginDesignation.EmpId;
                    PeerGroupViewModal SelectedGroup = PeerGroupCombo.SelectedItem as PeerGroupViewModal;
                    string GroupID = SelectedGroup.GroupID;
                    GifPanel.Visibility = Visibility.Visible;
                    await insertData(BranchID, CenterID, GroupID, EmpId);
                    GifPanel.Visibility = Visibility.Collapsed;
                    this.NavigationService.Navigate(new DashboardFieldOfficer());

                }
                else
                {
                    MessageBox.Show("Check All Fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


         void AddCustomerImages(string FileName)
         {
            string BasePath = MainWindow.DriveBasePath + "\\TRICHY\\" + BranchName+"\\Customer";

            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    if (CustomerDetails.AddressProof != null)
                    {
                        SaveImageToDrive drive = new SaveImageToDrive(BasePath + "\\Address Proof", FileName, CustomerDetails.AddressProof);
                        sqlcomm.CommandText = "update CustomerEnrollData set AddressProof=null where AadharNumber='" + CustomerDetails.AadharNumber + "'";
                        sqlcomm.ExecuteNonQuery();
                    }
                    if (CustomerDetails.PhotoProof != null)
                    {
                        SaveImageToDrive drive = new SaveImageToDrive(BasePath + "\\Photo Proof", FileName, CustomerDetails.PhotoProof);
                        sqlcomm.CommandText = "update CustomerEnrollData set PhotoProof=null where AadharNumber='" + CustomerDetails.AadharNumber + "'";
                        sqlcomm.ExecuteNonQuery();
                    }
                    if (CustomerDetails.ProfilePhoto != null)
                    {
                        SaveImageToDrive drive = new SaveImageToDrive(BasePath + "\\Profile Picture", FileName, CustomerDetails.ProfilePhoto);
                    }
                    if (CustomerDetails.CombinePhoto != null)
                    {
                        SaveImageToDrive drive = new SaveImageToDrive(BasePath + "\\Combine Photo", FileName, CustomerDetails.CombinePhoto);
                        sqlcomm.CommandText = "update CustomerEnrollData set CombinePhoto=null where AadharNumber='" + CustomerDetails.AadharNumber + "'";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
            }
            
         }

        async Task insertData(string BranchID,string CenterID,string GroupID,string EmpID)
        {
            CustomerEnrollDataViewModel Customer = FormCustomerModel(BranchID, EmpID, CenterID, GroupID);
            string jsondata = JsonConvert.SerializeObject(Customer);
            var stringContent = new StringContent(jsondata, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/AddCustomerData";
            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);
            if (response1.IsSuccessStatusCode)
            {
                AddCustomerImages(Customer.CustomerID);
                MessageBox.Show("Customer Details Added SuccessFully...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Reloaddata();
            }
            else
            {
                MessageBox.Show(response1.ReasonPhrase, "Warninng", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public async void Reloaddata()
        {
                try
                {
                    GifPanel.Visibility = Visibility.Visible;
                   
                    await GetCustomerEnrollMetaData(MainWindow.LoginDesignation.EmpId);
                    
                    GifPanel.Visibility = Visibility.Collapsed;
                    ObservableCollection<CustomerEnrollMetaData> CustomerData = CustomerEnrollMetaDataList;
                    this.NavigationService.Navigate(new EnrollDataView(CustomerData));
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    GifPanel.Visibility = Visibility.Collapsed;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    GifPanel.Visibility = Visibility.Collapsed;
                   
                }   
        }

        async Task GetCustomerEnrollMetaData(string EmpId)
        {

            string url1 = "http://examsign-001-site4.itempurl.com/api/GetEnrollMetaDetails/" + EmpId;

            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, null);
            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<ObservableCollection<CustomerEnrollMetaData>>(result);
                CustomerEnrollMetaDataList = status;
            }
        }

        CustomerEnrollDataViewModel FormCustomerModel(string BranchId,string EmpID,string CenterID,string GroupID)
        {
            CustomerEnrollDataViewModel Customerdata = new CustomerEnrollDataViewModel();
            Customerdata.CustomerID = CustomerRepository.GenerateCustomerID(BranchId);
            Customerdata.CustomerName = CustomerDetails.Name;
            Customerdata.FatherName = CustomerDetails.FatherName;
            Customerdata.MotherName = CustomerDetails.MotherName;
            Customerdata.DateOfBirth = CustomerDetails.Dob;
            Customerdata.Age = CustomerDetails.Age;
            Customerdata.Gender = CustomerDetails.Gender;
            Customerdata.ContactNumber = CustomerDetails.Mobile;
            Customerdata.AadharNumber = CustomerDetails.AadharNumber;
            Customerdata.Religion = CustomerDetails.Religion;
            Customerdata.Community = CustomerDetails.Community;
            Customerdata.Caste = CustomerDetails.Caste;
            Customerdata.Education = CustomerDetails.Education;
            Customerdata.FamilyMembers = CustomerDetails.FamilyMembers;
            Customerdata.EarningMembers = CustomerDetails.EarningMembers;
            Customerdata.Occupation = CustomerDetails.Occupation;
            Customerdata.MonthlyIncome = CustomerDetails.MonthlyIncome;
            Customerdata.MonthlyExpence = CustomerDetails.MonthlyExpenses;
            Customerdata.DoorNo = CustomerDetails.DoorNo;
            Customerdata.StreetName = CustomerDetails.StreetName;
            Customerdata.Village = CustomerDetails.Village;
            Customerdata.City = CustomerDetails.City;
            Customerdata.State = "Tamil Nadu";
            Customerdata.PinCode = CustomerDetails.Pincode.ToString();
            Customerdata.HousingType = CustomerDetails.HousingType;
            Customerdata.AddressProofName = CustomerDetails.AddressProofName;
            Customerdata.AddressProofNumber = CustomerDetails.AddressProofNo;
            Customerdata.PhotoProofName = CustomerDetails.PhotoProofName;
            Customerdata.PhotoProofNumber = CustomerDetails.PhotoProofNo;

            //Add Photos to Drive

            //

            Customerdata.AccountHolderName = CustomerDetails.BankACHolderName;
            Customerdata.AccountNumber = CustomerDetails.BankAccountNo;
            Customerdata.BranchName = CustomerDetails.BankBranchName;
            Customerdata.IFSCCode = CustomerDetails.IFSCCode;
            Customerdata.MICRCode = CustomerDetails.MICRCode;
            Customerdata.BankName = CustomerDetails.BankName;

            Customerdata.GuardianName = CustomerDetails.HusbandName;
            Customerdata.FamilyYearlyIncome = CustomerDetails.YearlyIncome;
            Customerdata.HousingType = CustomerDetails.Residency;
            Customerdata.LandHolding = CustomerDetails.LandHolding;
            Customerdata.LandType = "";
            Customerdata.LandVolume = CustomerDetails.LandVolume;

            //Guarentee Details
            Customerdata.GName = GuarenteeDetails.Name;
            Customerdata.GDateOfBirth = GuarenteeDetails.Dob;
            Customerdata.GAge = CustomerDetails.Age;
            Customerdata.GContactNumber = GuarenteeDetails.Mobile;
            Customerdata.GGender = GuarenteeDetails.Gender;
            Customerdata.GOccupation = GuarenteeDetails.Occupation;
            Customerdata.GRelationShip = GuarenteeDetails.RelationShip;
            Customerdata.GDoorNo = GuarenteeDetails.DoorNo;
            Customerdata.GStreetName = GuarenteeDetails.StreetName;
            Customerdata.GVillage = GuarenteeDetails.Village;
            Customerdata.GCity = GuarenteeDetails.City;
            Customerdata.GState = GuarenteeDetails.State;
            Customerdata.GPinCode = GuarenteeDetails.Pincode.ToString();
            Customerdata.GAddressProofName = GuarenteeDetails.AddressProofName;
            Customerdata.GAddressProofNumber = GuarenteeDetails.AddressProofNumber;
            Customerdata.GPhotoProofName = GuarenteeDetails.PhotoProofName;
            Customerdata.GPhotoProofNumber = GuarenteeDetails.PhotoProofNumber;
            //Customerdata.Gender = GuarenteeDetails.Gender;


            // NomineeDetails

            Customerdata.NName = NomineeDetails.Name;
            Customerdata.NDateOfBirth = NomineeDetails.Dob;
            Customerdata.NAge = NomineeDetails.Age;
            Customerdata.NContactNumber = NomineeDetails.Mobile;
            Customerdata.NOccupation = NomineeDetails.Occupation;
            Customerdata.NRelationShip = NomineeDetails.RelationShip;
            Customerdata.NDoorNo = NomineeDetails.DoorNo;
            Customerdata.NStreetName = NomineeDetails.StreetName;
            Customerdata.NVillage = NomineeDetails.Village;
            Customerdata.NCity = NomineeDetails.City;
            Customerdata.NState = NomineeDetails.State;
            Customerdata.NPinCode = NomineeDetails.Pincode.ToString();
            Customerdata.NAddressProofName = NomineeDetails.AddressProofName;
           // Customerdata.NAddressProofNumber = NomineeDetails.AddressProofNumber;
            Customerdata.NPhotoProofName = NomineeDetails.PhotoProofName;
            Customerdata.NPhotoProofNumber = NomineeDetails.PhotoProofNumber;
            Customerdata.NGender = NomineeDetails.Gender;

            //CustomerGroup
            Customerdata.PeerGroupID = GroupID;
            Customerdata.BranchID = BranchId;
            Customerdata.SHGId = CenterID;
            Customerdata.Isleader = false;

            //LoanDetails
            Customerdata.RequestID = LoanRepository.GenerateLoanRequestID(BranchId);
            ComboBoxItem Selectedamount = LoanAmountCombo.SelectedItem as ComboBoxItem;
            Customerdata.LoanAmount =Convert.ToInt32(Selectedamount.Content.ToString());
            ComboBoxItem SelectedLoanType = LoanTypeCombo.SelectedItem as ComboBoxItem;
            Customerdata.LoanType = (string)SelectedLoanType.Content;
            ComboBoxItem SelectedPeriod = LoanPeriodCombo.SelectedItem as ComboBoxItem;
            Customerdata.LoanPeriod = Convert.ToInt32(SelectedPeriod.Content.ToString());
            ComboBoxItem SelectedPurpose = LoanPurposeCombo.SelectedItem as ComboBoxItem;
            Customerdata.Purpose =LoanPurposeCombo.SelectedItem.ToString();
            Customerdata.LoanEnrollDate = DateTime.Today;
            Customerdata.LoanStatus = 1;
            Customerdata.EmployeeID = EmpID;
            


            return Customerdata;
        }



        bool IsValidEntry ()
        {
            if(CenterNameCombo.SelectedIndex!=-1 && PeerGroupCombo.SelectedIndex!=-1&&LoanTypeCombo.SelectedIndex!=-1&&LoanAmountCombo.SelectedIndex!=-1&&LoanPeriodCombo.SelectedIndex!=-1&&LoanPurposeCombo.SelectedIndex!=-1)
            {
                return true;
            }
            return false;
        }
        
    }
}
