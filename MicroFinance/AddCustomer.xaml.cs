using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
using MicroFinance.Modal;
using MicroFinance.Repository;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public static Customer customer = new Customer();
        public static Guarantor guarantor = new Guarantor();
        public static Nominee nominee = new Nominee();
        public static StaticProperty CaptureImageMessage = new StaticProperty();
        public static LoanDetails Loan = new LoanDetails();

        public List<string> BankList = new List<string>();
        public List<string> PurposeList = new List<string>();


        public List<string> ResidencyList = new List<string> { "OWN HOUSE", "RENT HOUSE" };
        public List<string> ResidencyTypeList = new List<string> { "HUT HOUSE", "TILED ROOF HOUSE", "CONCRETE HOUSE" };
        public List<string> LandHoldingList = new List<string> { "YES", "NO" };
        public List<BankDetailsView> BankDetailsList = new List<BankDetailsView>();

        string WhichClassButtonClick;
        public AddCustomer()
        {
            InitializeComponent();
            IsEligible();
           // MainGrid.Height = MainWindow.MainHeight-50;
           // MainGrid.Width = MainWindow.MainWidth;

            //TempLoad();
            BranchAndGroupDetailsforFieldOfficer();
            Assign();
            BankList = BankRepository.GetAllBankNames();
            BankNameComboBox.ItemsSource = BankList;
            PurposeList = LoanRepository.GetAllPurposeNames();
            PurposeNameCombo.ItemsSource = PurposeList;

            ResidencyTypeCombo.ItemsSource = ResidencyList;
            HousingTypeCombo.ItemsSource = ResidencyTypeList;
            LandHoldingCombo.ItemsSource = LandHoldingList;

            BankDetailsList = BankRepository.BankDetailsList();
        }
        void TempLoad()
        {
            customer.CustomerName = "THENMOZHI";
            //CustomerNameBox.Text = "Thirisa";

            customer.Gender = "MALE";
            //Female.IsChecked = true;

            customer.DateofBirth = new DateTime(1990, 08, 12);
            //SelectDOB.SelectedDate = new DateTime(1990, 08, 12);

            customer.FatherName = "ARAVIND";
            //FatherNameBox.Text = "Aravind";

            customer.MotherName = "KAVIARASI";
            //MotherNameBox.Text = "Kaviarasi";

            customer.ContactNumber = "7897894564";
            //ContactBox.Text = "7897894564";

            customer.Religion = "HINDU";
            //SelectReligion.SelectedIndex = 1;

            customer.Caste = "DHRAVIDAR";
            //CasteBox.Text = "MBC";

            customer.Community = "MBC";
            //CommunityBox.Text = "Dhravidar";

            customer.Education = "10";
            //EducatinBox.Text = "10";

            customer.FamilyMembers = 5;
            //FamilyMemberBox.Text = "5";

            customer.EarningMembers = 3;
            //EarningMemberBox.Text = "3";

            customer.Occupation = "DAILY WAGES";
            //OccupationBox.Text = "Daily wages";

            customer.MonthlyIncome = 15000;
            //MonthlyIncomeBox.Text = "15000";

            customer.MothlyExpenses = 12000;

            customer.YearlyIncome = 200000;
            //MonthlyExpensesBox.Text = "12000";

            customer.DoorNumber = "77 / W3";
            //HouseNOBox.Text = "77 / W3";

            customer.StreetName = "THIRUNAGAR";
            //StreetNameBox.Text = "Thirunagar";

            customer.LocalityTown = "KARUMANDABAM";
            //LocalityBox.Text = "Karumandapam";

            customer.Pincode = 620020;
            //PincodeBox.Text = "620020";

            customer.City = "TRICHY";
            //CityBox.Text = "Trichy";

            customer.State = "TAMILNADU";
            //StateBox.Text = "Tamil Nadu";

            customer.HousingType = "OWN HOUSE";
            //HouseTypeBox.Text = "Own house";

            customer.HousingIndex = "10";
            //HouseIndexBox.Text = "10";

            customer.AadharNo = "987987987987";
            customer.HusbandName = "PRAKSH"
                ;
            customer.Taluk = "Kurinji";
            customer.PhotoProofNo = "823932893";
            customer.AddressProofNo = "8329823";



            guarantor.GuarantorName = "VICKY";
            guarantor.Gender = "MALE";
            guarantor.DateofBirth = new DateTime(1980, 08, 12);
            guarantor.ContactNumber = "7894564562";
            guarantor.Occupation = "DRIVER";
            guarantor.RelationShip = "HUSBAND";
            guarantor.IsNominee = true;

            guarantor.DoorNumber = "77 / W3";
            guarantor.StreetName = "THIRUNAGAR";
            guarantor.LocalityTown = "KARUMANDAPAM";
            guarantor.Pincode = "620020";
            guarantor.City = "TRIHCY";
            guarantor.State = "TAMILNADU";
            guarantor.AddressProofNo = "28391872323";
            guarantor.PhotoProofNo = "2314078923";
            

            nominee.NomineeName = "VICKY";
            nominee.Gender = "MALE";
            nominee.DateofBirth = new DateTime(1980, 08, 12);
            nominee.ContactNumber = "7894564562";
            nominee.Occupation = "DRIVER";
            nominee.RelationShip = "HUSBAND";

            nominee.DoorNumber = "77 / W3";
            nominee.StreetName = "THIRUNAGAR";
            nominee.LocalityTown = "KARUMANDAPAM";
            nominee.Pincode = "620020";
            nominee.City = "TRIHCY";
            nominee.State = "TAMILNADU";

            guarantor.IsGuarantorNull = true;
            nominee.IsNomineeNull = true;


            customer.AccountHolder = "Saffu";
            customer.AccountNumber = "222222";
            customer.BankName = "SBI";
            customer.BankBranchName = "DPI";
            customer.IFSCCode = "SBIN0032";
            customer.MICRCode = "SBIN00832";
            customer.HavingBankDetails = true;

            CustDetailsInputs.DataContext = customer;
            AddressDetailGrid.DataContext = customer;
            AddNomineepopup.DataContext = guarantor;
        }
        void Assign()
        {
            CustomerGrid.DataContext = customer;
            AddressGrid.DataContext = customer;
            AddressProofGrid.DataContext = customer;
            PhotoProofGrid.DataContext = customer;
            PhotoProfileGrid.DataContext = customer;
            BankGrid.DataContext = customer;
            AadharNoGrid.DataContext = customer;
            Male.DataContext = customer;
            Female.DataContext = customer;

            GurantorGrid.DataContext = guarantor;
            GuarantorAddressDetails.DataContext = guarantor;
            GuarantorOtherDetailsGrid.DataContext = guarantor;
            GuarnatorDetails.DataContext = guarantor;
            NomineeGrid.DataContext = nominee;
            NomineeAddressDetails.DataContext = nominee;
            NomineeOtherDetails.DataContext = nominee;
            NomineeDetails.DataContext = nominee;
            LoanRequestPanel.DataContext = customer;
        }
        string CustomerId;
        public AddCustomer(string CustomerID)
        {


            

            InitializeComponent();
            
            CustomerId = CustomerID;
            FillAllDetails();
            IsEligible();

            BranchAndGroupDetailsforFieldOfficer(1);
            SelectedBranchAndGroupDetails();
            CustomerGrid.DataContext = customer;
            AddressGrid.DataContext = customer;
            AddressProofGrid.DataContext = customer;
            PhotoProofGrid.DataContext = customer;
            PhotoProfileGrid.DataContext = customer;
            BankGrid.DataContext = customer;
            AadharNoGrid.DataContext = customer;
            GenderPanel.DataContext = customer;
            LandDetialsPanel.DataContext = customer;

            GurantorGrid.DataContext = guarantor;
            GuarantorAddressDetails.DataContext = guarantor;
            GuarantorOtherDetailsGrid.DataContext = guarantor;
            GuarnatorDetails.DataContext = guarantor;

            NomineeGrid.DataContext = nominee;
            NomineeAddressDetails.DataContext = nominee;
            NomineeOtherDetails.DataContext = nominee;
            NomineeDetails.DataContext = nominee;


            SaveCustomer.Content = "Update";
            LoanRequestText.Visibility = Visibility.Collapsed;
            LaonRequestSection.Visibility = Visibility.Collapsed;


            ResidencyTypeCombo.ItemsSource = ResidencyList;
            HousingTypeCombo.ItemsSource = ResidencyTypeList;
            LandHoldingCombo.ItemsSource = LandHoldingList;
            

            BankDetailsList = BankRepository.BankDetailsList();


            BankList = BankRepository.GetAllBankNames();
            BankNameComboBox.ItemsSource = BankList;
            AadharPassBox.Password = customer.AadharNo;


            if (customer.LandHolding!=null)
            {
                if (customer.LandHolding.Equals("YES", StringComparison.CurrentCultureIgnoreCase))
                {
                    LandDetialsPanel.Visibility = Visibility.Collapsed;
                    LandHoldingShowBtn.Visibility = Visibility.Visible;
                }
            }

        }
        void FillAllDetails()
        {
            customer._customerId = CustomerId;
            guarantor._customerId = CustomerId;
            nominee._customerId = CustomerId;
            customer.GetAllDetailsofCustomers();
            guarantor.GetGuranteeDetails();
            nominee.GetNomineeDetails();
        }
        public static void StatusMessageWhileCapturingImage(int Type, string Message)
        {
            CaptureImageMessage.MessageType = Type;
            CaptureImageMessage.StatusMessage = Message;
        }
        List<PeerGroup> PeerGroupDetails = new List<PeerGroup>();
        void BranchAndGroupDetailsforFieldOfficer()
        {
            string _officerBranchId = MainWindow.LoginDesignation.BranchId;
            string _officerEmpId = MainWindow.LoginDesignation.EmpId;
            string[] _branchName = new string[1];
            string[] _officerName = new string[1];
            string[] _regionName = new string[1];
            List<string> SelfHelpGroupList = new List<string>();

            Branch_Shg_PgDetails branch_Shg_Pg = new Branch_Shg_PgDetails();
            branch_Shg_Pg.EmpId = _officerEmpId;
            _branchName[0] = branch_Shg_Pg.GetBranchNameofEmployee();
            _regionName[0] = branch_Shg_Pg.GetRegionNameofEmployee();
            _officerName[0] = branch_Shg_Pg.GetEmployeeName();
            SelfHelpGroupList = branch_Shg_Pg.GetSelfHelpGroup();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                
                sqlCommand.CommandText = "select SelfHelpGroup.SHGName, PeerGroup.GroupName, PeerGroup.GroupId from PeerGroup join SelfHelpGroup on PeerGroup.SHGid=SelfHelpGroup.SHGId where SelfHelpGroup.SHGid in (select SHGId from TimeTable where EmpId='" + _officerEmpId + "')";
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    PeerGroupDetails.Add(new PeerGroup { SHGName = sqlDataReader1.GetString(0),Name=sqlDataReader1.GetString(1), PG_Id = sqlDataReader1.GetString(2)});
                }
                sqlConnection.Close();
            }
            SelectRegion.ItemsSource = _regionName; SelectRegion.SelectedIndex = 0;
            SelectBranch.ItemsSource = _branchName; SelectBranch.SelectedIndex = 0;
            SelectFO.ItemsSource = _officerName; SelectFO.SelectedIndex = 0;
            SelectSHG.ItemsSource = SelfHelpGroupList;
           // SelectSHG.SelectedIndex = SelfHelpGroupList.Count - 1;
        }
        void BranchAndGroupDetailsforFieldOfficer(int number=0)
        {
            string _officerBranchId = GetCustomerBranch(CustomerId);
            string _officerEmpId = GetCustomerOfficer(CustomerId);
            string[] _branchName = new string[1];
            string[] _officerName = new string[1];
            string[] _regionName = new string[1];
            List<string> SelfHelpGroupList = new List<string>();

            Branch_Shg_PgDetails branch_Shg_Pg = new Branch_Shg_PgDetails();
            branch_Shg_Pg.EmpId = _officerEmpId;
            _branchName[0] = branch_Shg_Pg.GetBranchNameofEmployee();
            _regionName[0] = branch_Shg_Pg.GetRegionNameofEmployee();
            _officerName[0] = branch_Shg_Pg.GetEmployeeName();
            SelfHelpGroupList = branch_Shg_Pg.GetSelfHelpGroup();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "select SelfHelpGroup.SHGName, PeerGroup.GroupName, PeerGroup.GroupId from PeerGroup join SelfHelpGroup on PeerGroup.SHGid=SelfHelpGroup.SHGId where SelfHelpGroup.SHGid in (select SHGId from TimeTable where EmpId='" + _officerEmpId + "')";
                SqlDataReader sqlDataReader1 = sqlCommand.ExecuteReader();
                while (sqlDataReader1.Read())
                {
                    PeerGroupDetails.Add(new PeerGroup { SHGName = sqlDataReader1.GetString(0), Name = sqlDataReader1.GetString(1), PG_Id = sqlDataReader1.GetString(2) });
                }
                sqlConnection.Close();
            }
            SelectRegion.ItemsSource = _regionName; SelectRegion.SelectedIndex = 0;
            SelectBranch.ItemsSource = _branchName; SelectBranch.SelectedIndex = 0;
            SelectFO.ItemsSource = _officerName; SelectFO.SelectedIndex = 0;
            SelectSHG.ItemsSource = SelfHelpGroupList; SelectSHG.SelectedIndex = SelfHelpGroupList.Count - 1;
        }

        string GetCustomerBranch(string CustomerID)
        {
            string BranchID = "";
            using(SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchId from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerID+"'))";
                    BranchID =(string) sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return BranchID;
        }

        string GetCustomerOfficer(string CustomerID)
        {
            string EmpId = "";
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select EmpId from TimeTable where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerID+"'))";
                    EmpId = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return EmpId;
        }
        void SelectedBranchAndGroupDetails()
        {
            string Selectedbranch = "";
            string SelectedSHg = "";
            string SelectedPg = "";
            string SelectedOfficerName = "";
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select GroupName from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerId+"')";
                SelectedPg = (string)sqlCommand.ExecuteScalar();
                sqlCommand.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerId+"'))";
                SelectedSHg = (string)sqlCommand.ExecuteScalar();
                sqlCommand.CommandText = "select BranchName from BranchDetails where Bid=(select BranchId from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerId+"')))";
                Selectedbranch=(string) sqlCommand.ExecuteScalar();
                sqlCommand.CommandText = "select Name from Employee where EmpId=(select EmpId from TimeTable where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+CustomerId+"')))";
                SelectedOfficerName = sqlCommand.ExecuteScalar().ToString();
            }
            SelectBranch.SelectedItem = Selectedbranch;
            SelectFO.SelectedItem = SelectedOfficerName;
            SelectSHG.SelectedItem = SelectedSHg;
            SelectPG.SelectedItem = SelectedPg;
        }

        private void SelectSHG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<PeerGroup> SelectedPG = new List<PeerGroup>();

            foreach (var item in PeerGroupDetails)
            {
                if (item.SHGName == SelectSHG.SelectedItem.ToString())
                {
                    if(customer.IsPeerGroupFull(item.PG_Id))
                    {
                        SelectedPG.Add(item);
                    }
                }
            }

            SelectPG.ItemsSource = SelectedPG;
            SelectPG.SelectedIndex = 0;
        }

        private void SelectPG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                if (SelectPG.SelectedItem != null)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select IsLeader from CustomerGroup where PeerGroupId='" + SelectPG.SelectedItem.ToString() + "'";

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0) == true)
                        {
                            IsLeaderCheck.IsEnabled = false;
                            break;
                        }
                        else
                        {
                            IsLeaderCheck.IsEnabled = true;
                        }
                    }
                }
            }

        }
        void IsEligible()
        {
            if (MainWindow.LoginDesignation.LoginDesignation == "Field Officer")
            {
                SelectBranch.IsEnabled = false;
                SelectFO.IsEnabled = false;
                SelectRegion.IsEnabled = false;
            }
            else if (MainWindow.LoginDesignation.LoginDesignation == "Region Manager")
            {
                SelectBranch.IsEnabled = true;
                SelectFO.IsEnabled = true;
                SelectRegion.IsEnabled = true;
            }
            else
            {
                SelectBranch.IsEnabled = false;
                SelectFO.IsEnabled = false;
                SelectRegion.IsEnabled = false;
            }

        }

        void EnableDisableBackground(bool Enable)
        {
            if (Enable)
            {
                BranchDetailsGrid.IsEnabled = true;
                CustomerGrid.IsEnabled = true;
                AddressGrid.IsEnabled = true;
                CustomerOtherDetails.IsEnabled = true;
            }
            else
            {
                BranchDetailsGrid.IsEnabled = false;
                CustomerGrid.IsEnabled = false;
                AddressGrid.IsEnabled = false;
                CustomerOtherDetails.IsEnabled = false;
            }
        }

        private void SelectReligion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustReligionError.Visibility == Visibility.Visible)
            {
                CustReligionError.Visibility = Visibility.Collapsed;
            }
            CommunityBox.IsEnabled = true;
            CasteBox.IsEnabled = true;
        }

        //add peer group
        private void AddPG_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.GetNavigationService(this).Navigate(new AddPg());
            AddPg APG = new AddPg();
            APG.ShowDialog();


        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //save customer
        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;

            string BtnContent = Btn.Content.ToString();
            if(BtnContent.Equals("Save"))
            {
                CheckAadharAlreadyExist();
                if (IsAadharAlreadeyExists)
                {
                    MessageBox.Show("Aadhar Card Already Exists..");
                }
                else if (CustomerValidation() == false)
                {
                    MainWindow.StatusMessageofPage(0, "Please Enter Require Fields....");
                    //Thread.Sleep(10000);
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
                else if(LoanDetailsValidation()==false)
                {
                    //MainWindow.StatusMessageofPage(0, "Please Enter Loan Details....");
                    //Thread.Sleep(2000);
                    //MainWindow.StatusMessageofPage(1, "Ready...");

                    MessageBox.Show("Please Complete Loan Details","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    PeerGroup SelectedPG = SelectPG.SelectedValue as PeerGroup;
                    
                        customer._customerId = customer.GetCustId(SelectBranch.Text, SelectRegion.Text);
                        CustomerVerified verified = new CustomerVerified(customer, guarantor, nominee, 0, SelectRegion.Text, SelectBranch.Text, SelectSHG.Text, SelectedPG.PG_Id);
                        customer = new Customer();
                        nominee = new Nominee();
                        guarantor = new Guarantor();
                        Assign();
                        NavigationService.GetNavigationService(this).Navigate(verified);
                   


                }
            }
            else if(BtnContent.Equals("Update"))
            {
                PeerGroup SelectedPG = SelectPG.SelectedValue as PeerGroup;
                customer.UpdateExistingDetails(SelectBranch.Text, SelectSHG.Text, SelectedPG.PG_Id, guarantor, nominee);
                string Designation = MainWindow.LoginDesignation.LoginDesignation;
                Designation = (Designation == null) ? "" : Designation;
                LoadHomePage(Designation);
            }
            
            
        }


        public bool LoanDetailsValidation()
        {
            if (LoanTypecombo.SelectedIndex != -1 && LoanAmountcombo.SelectedIndex != -1 && TimePeriodcombo.SelectedIndex!=-1 && PurposeNameCombo.SelectedIndex!=-1)
            {
                return true;
            }
            return false;
        }
        public void LoadHomePage(string Designation)
        {
            if (Designation.Equals("Field Officer"))
                this.NavigationService.Navigate(new DashboardFieldOfficer());
            else if (Designation.Equals("Accountant"))
                this.NavigationService.Navigate(new DashboardAccountant());
            else if (Designation.Equals("Branch Manager") || Designation.Equals("Manager"))
                this.NavigationService.Navigate(new DashboardBranchManager());
            else if (Designation.Equals("Region Manager"))
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            else
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void ImageSavebtn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Capture.SavedImage;
            if (image != null)
            {
                string txt = PhotoProofNametxt.Text;
                SetImage(image, txt);
                CaptureImage.Visibility = Visibility.Collapsed;
                Capture.SavedImage = null;
                //    MessageBox.Show("Photo Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                StatusMessageWhileCapturingImage(0, "Please Capture or Select Photo To Save....");
                //CaptureImageStatus.Text = "Please Capture or Select Photo";
            }
        }

        private void ImgCancelbtn_Click(object sender, RoutedEventArgs e)
        {
            CaptureImage.Visibility = Visibility.Collapsed;
        }

        void SetImage(BitmapImage image, string imagename)
        {

            if (WhichClassButtonClick.Equals("Customer"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        customer.AddressProof = image;
                        MainWindow.StatusMessageofPage(1, "Successfully Customer Address Proof Added...");
                        break;
                    case "Photo Proof":
                        customer.PhotoProof = image;
                        MainWindow.StatusMessageofPage(1, "Successfully Customer Photo Proof Added...");
                        break;
                    case "Profile Picture":
                        {
                            customer.ProfilePicture = image;
                            MainWindow.StatusMessageofPage(1, "Successfully Customer Profile Picture Added...");
                        }
                        break;
                }
            }
            else if (WhichClassButtonClick.Equals("Guarantor"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        guarantor.AddressProof = image;
                        GAddressProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Address Proof Added";
                        break;
                    case "Photo Proof":
                        guarantor.PhotoProof = image;
                        //MainWindow.StatusMsg.MessageType = 1;
                        GPhotoProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Photo Proof Added";
                        break;
                    case "Profile Picture":
                        guarantor.ProfilePicture = image;
                        GProfilePhotoError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Profile Picture Added";
                        break;
                }
            }
            else if (WhichClassButtonClick.Equals("Nominee"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        nominee.AddressProof = image;
                        NomineeAddressError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Address Proof Added";
                        break;
                    case "Photo Proof":
                        nominee.PhotoProof = image;
                        NomineePhotoError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Photo Proof Added";
                        break;
                    case "Profile Picture":
                        nominee.ProfilePicture = image;
                        NomineeProfileError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Profile Picture Added";
                        break;
                }
            }
        }

        private void ProfileImageOk_Click(object sender, RoutedEventArgs e)
        {
            ViewImagePopup.IsOpen = false;
        }

        //adding profile image 

        Capture CapturePhoto = new Capture();

        private void AddProfilePhoto_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ViewProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.ProfilePicture;
        }
        private void EditProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
        }
        //adding Address Proof image

        Capture AddressProofPhoto = new Capture();

        private void AddAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.AddressProof;
        }

        private void EditAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        //Adding Photo Proof Image

        private void AddPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.PhotoProof;
        }

        private void EditPhototProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }



        //Guarantor 

        private void AddGaurantor_Click(object sender, RoutedEventArgs e)
        {
            GuarantorWindow.Visibility = Visibility.Visible;
            EnableDisableBackground(false);
        }

        private void ViewGaurantor_Click(object sender, RoutedEventArgs e)
        {
            ViewGuarantorPopopup.IsOpen = true;
            EnableDisableBackground(false);
            ViewGuarantorGrid.DataContext = guarantor;

        }

        private void EditGaurantor_Click(object sender, RoutedEventArgs e)
        {
            GuarantorWindow.Visibility = Visibility.Visible;
            GuarantorProcess.Text = "Edit Guarantor";
            EnableDisableBackground(false);
        }

        private void SaveGuarantor_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckGuarantorValidation())
            {
                MainWindow.StatusMessageofPage(0, "Please Enter Required Fields....");
            }
            else
            {
                GuarantorErrorCheck.Visibility = Visibility.Collapsed;
                GuarantorWindow.Visibility = Visibility.Collapsed;
                guarantor.IsGuarantorNull = true;
                EnableDisableBackground(true);

                MainWindow.StatusMessageofPage(1, "Successfully Guarantor Added...");
                if (guarantor.IsNominee)
                {
                    NomineeErrorCheck.Visibility = Visibility.Collapsed;
                    NomineeNameBox.Text = guarantor.GuarantorName;
                    NomineeSelectDOB.SelectedDate = guarantor.DateofBirth;
                    if(guarantor.Gender=="Male")
                    {
                        NMale.IsChecked = true;
                    }
                    else
                    {
                        NFemale.IsChecked = true;
                    }
                    NomineeContactBox.Text = guarantor.ContactNumber;
                    NomineeOccupationBox.Text = guarantor.Occupation;
                    NomineeRelationshipBox.Text = guarantor.RelationShip;
                    NomineeHouseNOBox.Text = guarantor.DoorNumber;
                    NomineeStreetNameBox.Text = guarantor.StreetName;
                    NomineeLocalityBox.Text = guarantor.LocalityTown;
                    NomineeCityBox.Text = guarantor.City;
                    NomineePincodeBox.Text = guarantor.Pincode;
                    NomineeStateBox.Text = guarantor.State;
                    nominee.IsNomineeNull = true;
                    NomineeAddressProofBox.Text = guarantor.NameofAddressProof;
                    NomineePhotoProofBox.Text = guarantor.NameofPhotoProof;
                    SameAsCustomerAddressForNominee.IsChecked = SameAsCustomerAddress.IsChecked;
                    nominee.AddressProof = guarantor.AddressProof;
                    nominee.PhotoProof = guarantor.PhotoProof;
                    nominee.ProfilePicture = guarantor.ProfilePicture;
                    nominee.NameofAddressProof = guarantor.NameofAddressProof;
                    nominee.NameofPhotoProof = guarantor.NameofPhotoProof;

                    //nominee.NomineeName = guarantor.GuarantorName;
                    //nominee.DateofBirth = guarantor.DateofBirth;
                    //nominee.Age = guarantor.Age;
                    //nominee.ContactNumber = guarantor.ContactNumber;
                    //nominee.Occupation = guarantor.Occupation;
                    //nominee.RelationShip = guarantor.RelationShip;
                    //nominee.DoorNumber = guarantor.DoorNumber;
                    //nominee.StreetName = guarantor.StreetName;
                    //nominee.LocalityTown = guarantor.LocalityTown;
                    //nominee.Pincode = guarantor.Pincode;
                    //nominee.City = guarantor.City;
                    //nominee.State = guarantor.State;
                    //nominee.IsNomineeNull = true;
                    //nominee.AddressProof = guarantor.AddressProof;
                    //nominee.PhotoProof = guarantor.PhotoProof;
                    //nominee.ProfilePicture = guarantor.ProfilePicture;
                    //nominee.NameofAddressProof = guarantor.NameofAddressProof;
                    //nominee.NameofPhotoProof = guarantor.NameofPhotoProof;
                    //NomineeAddressProofBox.Text = guarantor.NameofAddressProof;
                    //NomineePhotoProofBox.Text = guarantor.NameofPhotoProof;
                    //SameAsCustomerAddressForNominee.IsChecked = SameAsCustomerAddress.IsChecked;
                    ////if (SameAsCustomerAddress.IsChecked == true)
                    ////{
                    ////    nominee.DoorNumber = guarantor.DoorNumber;
                    ////    nominee.StreetName = guarantor.StreetName;
                    ////    nominee.LocalityTown = guarantor.LocalityTown;
                    ////    nominee.Pincode = guarantor.Pincode;
                    ////    nominee.City = guarantor.City;
                    ////    nominee.State = guarantor.City;
                    ////}
                    MainWindow.StatusMessageofPage(1, "Successfully Guarantor and Nominee Added...");
                }
            }
        }

        private void GuarantorAddAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ViewAddressProofOfGuarantor_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.AddressProof;
        }

        private void EditAddressProofofGuarantor_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GuarantorAddPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GuarantorViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.PhotoProof;
        }

        private void GuarantorEditPhototProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GuarantorAddProfilePhoto_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GuarantorViewProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.ProfilePicture;
        }

        private void GuarantorEditProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }



        //Nominee Details

        private void AddNominee_Click(object sender, RoutedEventArgs e)
        {
            AddNomineepopup.Visibility = Visibility.Visible;
            EnableDisableBackground(false);

        }

        private void ViewNominee_Click(object sender, RoutedEventArgs e)
        {
            ViewNomineePopopup.IsOpen = true;
            EnableDisableBackground(false);
            ViewNomineeDetails.DataContext = nominee;
            ViewNomineeAddress.DataContext = nominee;
            ViewNomineeAddressProofGrid.DataContext = nominee;
            ViewNomineePhotoProofGrid.DataContext = nominee;
        }

        private void EditNominee_Click(object sender, RoutedEventArgs e)
        {
            AddNomineepopup.Visibility = Visibility.Visible;
            EnableDisableBackground(false);
            Nomineprocess.Text = "Edit Nominee";
        }

        private void NomineeSaveGuarantor_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNomineeValidation() == false)
            {
                MainWindow.StatusMessageofPage(2, "Please Enter Required Fields....");
            }
            else
            {
                NomineeErrorCheck.Visibility = Visibility.Collapsed;
                AddNomineepopup.Visibility = Visibility.Collapsed;
                EnableDisableBackground(true);
                nominee.IsNomineeNull = true;
                MainWindow.StatusMessageofPage(1, "Successfully Nominee Added...");
            }
        }

        private void NomineeCancel_Click(object sender, RoutedEventArgs e)
        {
            AddNomineepopup.Visibility = Visibility.Collapsed;
            EnableDisableBackground(true);
        }

        private void NomineeAddAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NomineeViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.AddressProof;
        }

        private void NomineeEditAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NomineeAddPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NomineeViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.PhotoProof;
        }

        private void NomineeEditPhototProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NomineeAddProfilePhoto_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NomineeViewProfilePicture_Click(object sender, RoutedEventArgs e)
        {

            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.ProfilePicture;
        }

        private void NomineeEditProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ViewGuarantorOk_Click(object sender, RoutedEventArgs e)
        {
            ViewGuarantorPopopup.IsOpen = false;
            EnableDisableBackground(true);
        }

        private void ViewNomineeok_Click(object sender, RoutedEventArgs e)
        {
            ViewNomineePopopup.IsOpen = false;
            EnableDisableBackground(true);
        }

        void ChangeReadOnly()
        {
            GuarantorHouseNOBox.IsReadOnly = true;
            GuarantorStreetNameBox.IsReadOnly = true;
            GuarantorLocalityBox.IsReadOnly = true;
            GuarantorPincodeBox.IsReadOnly = true;
            GuarantorCityBox.IsReadOnly = true;
            GuarantorStateBox.IsReadOnly = true;
        }
        void ChangeReadProperty()
        {
            GuarantorHouseNOBox.IsReadOnly = false;
            GuarantorStreetNameBox.IsReadOnly = false;
            GuarantorLocalityBox.IsReadOnly = false;
            GuarantorPincodeBox.IsReadOnly = false;
            GuarantorCityBox.IsReadOnly = false;
            GuarantorStateBox.IsReadOnly = false;
        }

        private void SameAsCustomerAddress_Click(object sender, RoutedEventArgs e)
        {
            if (SameAsCustomerAddress.IsChecked == true)
            {
                GuarantorAddressDetails.IsEnabled = true;
                ChangeReadOnly();

                GuarantorHouseNOBox.Text = customer.DoorNumber;
                GuarantorStreetNameBox.Text = customer.StreetName;
                GuarantorLocalityBox.Text = customer.LocalityTown;
                GuarantorPincodeBox.Text = customer.Pincode.ToString();
                GuarantorCityBox.Text = customer.City;
                GuarantorStateBox.Text = customer.State;
            }
            else
            {
                ChangeReadProperty();
                GuarantorAddressDetails.IsEnabled = true;
                GuarantorHouseNOBox.Text = "";
                GuarantorStreetNameBox.Text = "";
                GuarantorLocalityBox.Text = "";
                GuarantorPincodeBox.Text = "";
                GuarantorCityBox.Text = "";
                GuarantorStateBox.Text = "";
            }
        }

        private void SameAsCustomerAddressForNominee_Checked(object sender, RoutedEventArgs e)
        {
            NomineeAddressDetails.IsEnabled = false;

            NomineeHouseNOBox.Text = customer.DoorNumber;
            NomineeStreetNameBox.Text = customer.StreetName;
            NomineeLocalityBox.Text = customer.LocalityTown;
            NomineePincodeBox.Text = customer.Pincode.ToString();
            NomineeCityBox.Text = customer.City;
            NomineeStateBox.Text = customer.State;
        }

        private void SameAsCustomerAddressForNominee_Unchecked(object sender, RoutedEventArgs e)
        {
            NomineeAddressDetails.IsEnabled = true;

            NomineeHouseNOBox.Text = "";
            NomineeStreetNameBox.Text = "";
            NomineeLocalityBox.Text = "";
            NomineePincodeBox.Text = "";
            NomineeCityBox.Text = "";
            NomineeStateBox.Text = "";
        }

        private void GuarantorCancel_Click(object sender, RoutedEventArgs e)
        {
            EnableDisableBackground(true);
            GuarantorWindow.Visibility = Visibility.Collapsed;
        }

        private void BankDetails_Click(object sender, RoutedEventArgs e)
        {
            AccountdetailsPanel.IsOpen = true;
            EnableDisableBackground(false);
        }

        private void SampleCheck_Click(object sender, RoutedEventArgs e)
        {
            customer.HavingBankDetails = true;
            AccountdetailsPanel.IsOpen = false;
            EnableDisableBackground(true);
            MainWindow.StatusMessageofPage(1, "Successfully BankDetails Added...");
            BankErrorCheck.Visibility = Visibility.Collapsed;

            BankDetailsView NewBank = new BankDetailsView();
            if(IsExists(ifsccode.Text)==false)
            {
                NewBank.BankName = BankNameComboBox.Text;
                NewBank.BranchName = bankname.Text;
                NewBank.IFSCCode = ifsccode.Text;
                NewBank.MICRCode = micrcode.Text;

                BankRepository.AddBankDetails(NewBank);
                BankDetailsList.Add(NewBank);
            }
        }
        void BankFieldValidation()
        {

        }
        private void PanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            AccountdetailsPanel.IsOpen = false;
            EnableDisableBackground(true);
        }

        private void ViewBankDetails_Click(object sender, RoutedEventArgs e)
        {
            ViewAccountdetailsPanel.IsOpen = true;
            EnableDisableBackground(false);
        }

        private void EditBankDetails_Click(object sender, RoutedEventArgs e)
        {
            AccountdetailsPanel.IsOpen = true;
            EnableDisableBackground(false);
            customer.HavingBankDetails = true;
        }

        private void BankOk_Click(object sender, RoutedEventArgs e)
        {
            ViewAccountdetailsPanel.IsOpen = false;
            EnableDisableBackground(true);
        }
        private void MaleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CustGender.Visibility == Visibility.Visible)
            {
                CustGender.Visibility = Visibility.Collapsed;
            }
            customer.Gender = "Male";
        }

        private void FemalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CustGender.Visibility == Visibility.Visible)
            {
                CustGender.Visibility = Visibility.Collapsed;
            }
            customer.Gender = "Female";
        }


        bool NumberValidation(String pincode)
        {
            try
            {
                int _isNumber = Convert.ToInt32(pincode);
                return true;
            }
            catch
            {
                return false;
            }
        }
        bool NameValidation(String Word)
        {
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] != ' ')
                {
                    if (char.IsLetter(Word[i]) == false && Word[i] != '.')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        bool MoneyValidation(String Money)
        {
            StringBuilder sb = new StringBuilder();
            
            try
            {
                if (Money[0] == '₹')
                {
                    for (int i = 3; i < Money.Length; i++)
                    {
                        sb.Append(Money[i]);
                    }
                }
                int _isNumber = Convert.ToInt32(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GuarantorMaleBtn_Click(object sender, RoutedEventArgs e)
        {
            guarantor.Gender = "Male";
            GGender.Visibility = Visibility.Collapsed;
        }

        private void GuarantorFemalBtn_Click(object sender, RoutedEventArgs e)
        {
            guarantor.Gender = "Female";
            GGender.Visibility = Visibility.Collapsed;
        }

        private void NomineeMaleBtn_Click(object sender, RoutedEventArgs e)
        {
            nominee.Gender = "Male";
            NomineeGenderError.Visibility = Visibility.Collapsed;
        }

        private void NomineeFemalBtn_Click(object sender, RoutedEventArgs e)
        {
            nominee.Gender = "Female";
            NomineeGenderError.Visibility = Visibility.Collapsed;
        }

        BrushConverter bc = new BrushConverter();
        private bool CheckGuarantorValidation()
        {
            bool _chekcisValid = true;
            if (String.IsNullOrEmpty(GuarantorNameBox.Text))
            {
                NameErrorGuarantor.Visibility = Visibility.Visible;
                GuarantorNameBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                GuarantorNameBox.ToolTip = "Required Field";
                _chekcisValid = false;
            }
            if (Gmale.IsChecked == false && GFemale.IsChecked == false)
            {
                GGender.Visibility = Visibility.Visible;
                _chekcisValid = false;
            }
            if (GuarantorSelectDOB.SelectedDate.Equals(DateTime.Today) && CalculateAge(GuarantorSelectDOB.SelectedDate.Value)==0 && AgeValidation(guarantor.Age)==false)
            {
                DateError.Visibility = Visibility.Visible;
                GuarantorSelectDOB.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorContactBox.Text))
            {
                GuarantorContactError.Visibility = Visibility.Visible;
                GuarantorContactBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorOccupationBox.Text))
            {
                GuarantorOccupationError.Visibility = Visibility.Visible;
                GuarantorOccupationBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (string.IsNullOrEmpty(GuarantorRelationshipBox.Text))
            {
                GuarantorRelationshipError.Visibility = Visibility.Visible;
                GuarantorRelationshipBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorHouseNOBox.Text))
            {
                GuaratnorDoorError.Visibility = Visibility.Visible;
                GuarantorHouseNOBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorStreetNameBox.Text))
            {
                GStreetError.Visibility = Visibility.Visible;
                GuarantorStreetNameBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorLocalityBox.Text))
            {
                GuarantorLocalityError.Visibility = Visibility.Visible;
                GuarantorLocalityBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorPincodeBox.Text))
            {
                GuarantorPincodeError.Visibility = Visibility.Visible;
                GuarantorPincodeBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorCityBox.Text))
            {
                GuarantorCityError.Visibility = Visibility.Visible;
                GuarantorCityBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            if (String.IsNullOrEmpty(GuarantorStateBox.Text))
            {
                GuarantorStateError.Visibility = Visibility.Visible;
                GuarantorStateBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
                _chekcisValid = false;
            }
            //if (GuarantorAddressProofViewEditPanel.Visibility == Visibility.Hidden)
            //{
            //    GAddressProofError.Visibility = Visibility.Visible;
            //    _chekcisValid = false;
            //}
            //if (GuarantorPhotoProofViewEditPanel.Visibility == Visibility.Hidden)
            //{
            //    GPhotoProofError.Visibility = Visibility.Visible;
            //    _chekcisValid = false;
            //}
            //if (GuarantorProfilePictureViewEditPanel.Visibility == Visibility.Hidden)
            //{
            //    GProfilePhotoError.Visibility = Visibility.Visible;
            //    _chekcisValid = false;
            //}
            return _chekcisValid;
        }

        private void GuarantorNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameErrorGuarantor.Visibility == Visibility.Visible)
            {
                NameErrorGuarantor.Visibility = Visibility.Collapsed;
                GuarantorNameBox.BorderBrush = (Brush)bc.ConvertFrom("LightGray");
                GuarantorNameBox.ToolTip = "";
            }
        }

        private void GuarantorSelectDOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateError.Visibility == Visibility.Visible)
            {
                DateError.Visibility = Visibility.Collapsed;
                GuarantorSelectDOB.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            }
            DateTime SelectedDate = GuarantorSelectDOB.SelectedDate.Value;
            int Age = CalculateAge(SelectedDate);
            if(Age!=0)
            {
                if (Age <= 18 || Age >= 59)
                {
                    MessageBoxResult Result = MessageBox.Show("This User is Not Valid!...\n(The " +
                        "Age Range Between 18-65)\n Are You Sure You Want To Continue.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                
            
            
        }

        private void GuarantorContactBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PhoneNumberValidation(GuarantorContactBox.Text))
            {
                GuarantorContactError.Visibility = Visibility.Collapsed;
                GuarantorContactBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            }
            else
            {
                GuarantorContactBox.BorderBrush = (Brush)bc.ConvertFrom("Red");
            }
        }

        private void GuarantorHouseNOBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorHouseNOBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            GuaratnorDoorError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorStreetNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorStreetNameBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            GStreetError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorLocalityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorLocalityBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            GuarantorLocalityError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorCityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorCityBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            GuarantorCityError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorStateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorStateBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
            GuarantorStateError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorOccupationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GuarantorOccupationError.Visibility = Visibility.Collapsed;
            GuarantorOccupationBox.BorderBrush = (Brush)bc.ConvertFrom("Gray");
        }

        private void GuarantorRelationshipBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GuarantorRelationshipError.Visibility = Visibility.Collapsed;
        }

        Brush Redcolor = (Brush)new BrushConverter().ConvertFrom("Red");
        private bool CheckNomineeValidation()
        {
            bool _checkValid = true;
            if (string.IsNullOrEmpty(NomineeNameBox.Text))
            {
                NomineeNameBox.BorderBrush = Redcolor;
                NomineeNameError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (NomineeSelectDOB.SelectedDate == DateTime.Today && CalculateAge(NomineeSelectDOB.SelectedDate.Value)==0 && AgeValidation(nominee.Age)==false)
            {
                NomineeSelectDOB.BorderBrush = Redcolor;
                NomineeDoorError.Visibility = Visibility.Visible;
                _checkValid = false;

            }
            if (String.IsNullOrEmpty(NomineeContactBox.Text))
            {
                NomineeContactBox.BorderBrush = Redcolor;
                NomineeContactError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (string.IsNullOrEmpty(NomineeOccupationBox.Text))
            {
                NomineeOccupationBox.BorderBrush = Redcolor;
                NomineeOccuapationError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (string.IsNullOrEmpty(NomineeRelationshipBox.Text))
            {
                NomineeRelationError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeHouseNOBox.Text))
            {
                NomineeHouseNOBox.BorderBrush = Redcolor;
                NomineeDoorError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeStreetNameBox.Text))
            {
                NomineeStreetError.Visibility = Visibility.Visible;
                NomineeStreetNameBox.BorderBrush = Redcolor;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeLocalityBox.Text))
            {
                NomineeLocalityBox.BorderBrush = Redcolor;
                NomineeLocalityError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineePincodeBox.Text))
            {
                NomineePincodeBox.BorderBrush = Redcolor;
                NomineePincodeError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeCityBox.Text))
            {
                NomineeCityBox.BorderBrush = Redcolor;
                NomineeCityError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeStateBox.Text))
            {
                NomineeStateBox.BorderBrush = Redcolor;
                NomineeStateError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (String.IsNullOrEmpty(NomineeStateBox.Text))
            {
                NomineeStateBox.BorderBrush = Redcolor;
                NomineeStateError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            if (NomineeAddressProofViewEditPanel.Visibility == Visibility.Hidden)
            {
                NomineeAddressError.Visibility = Visibility.Visible;
                _checkValid = false;
            }
            //if (NMale.IsChecked == false && NFemale.IsChecked == false)
            //{
            //    NomineeGenderError.Visibility = Visibility.Visible;
            //    _checkValid = false;
            //}
            //if (NomineePhotoProofViewEditPanel.Visibility == Visibility.Hidden)
            //{
            //    NomineePhotoError.Visibility = Visibility.Visible;
            //    _checkValid = false;
            //}
            //if (NomineeProfilePictureViewEditPanel.Visibility == Visibility.Hidden)
            //{
            //    NomineeProfileError.Visibility = Visibility.Visible;
            //    _checkValid = false;
            //}
            return _checkValid;
        }
        Brush GrayColor = (Brush)new BrushConverter().ConvertFrom("Gray");
        private void NomineeNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeNameBox.BorderBrush = GrayColor;
            NomineeNameError.Visibility = Visibility.Collapsed;
        }

        private void NomineeSelectDOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            NomineeSelectDOB.BorderBrush = GrayColor;
            NomineeDateError.Visibility = Visibility.Collapsed;

            DateTime SelectedDate = GuarantorSelectDOB.SelectedDate.Value;
            int Age = CalculateAge(SelectedDate);
            if(Age!=0)
            {
                if (Age <= 18 || Age > 65)
                {
                    MessageBoxResult Result = MessageBox.Show("This User is Not Valid!...\n(The " +
                        "Age Range Between 18-65)\n Are You Sure You Want To Continue.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }
        bool PhoneNumberValidation(string Phonenumber)
        {
            if (Phonenumber.Length <= 10)
            {
                for (int i = 0; i < Phonenumber.Length; i++)
                {
                    if (char.IsDigit(Phonenumber[i]) == false)
                    {
                        return false;
                    }
                }
                if (Phonenumber.Length == 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        private void NomineeContactBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PhoneNumberValidation(NomineeContactBox.Text))
            {
                NomineeContactBox.BorderBrush = GrayColor;
                NomineeContactError.Visibility = Visibility.Collapsed;
            }
            else
            {
                NomineeContactBox.BorderBrush = (Brush)new BrushConverter().ConvertFrom("Red");
            }
        }

        private void NomineeOccupationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeOccupationBox.BorderBrush = GrayColor;
            NomineeOccuapationError.Visibility = Visibility.Collapsed;
        }

        private void NomineeRelationshipBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NomineeRelationError.Visibility = Visibility.Collapsed;
        }

        private void NomineeHouseNOBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeHouseNOBox.BorderBrush = GrayColor;
            NomineeDoorError.Visibility = Visibility.Collapsed;
        }

        private void NomineeStreetNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeStreetError.Visibility = Visibility.Collapsed;
            NomineeStreetNameBox.BorderBrush = GrayColor;
        }

        private void NomineeLocalityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeLocalityBox.BorderBrush = GrayColor;
            NomineeLocalityError.Visibility = Visibility.Collapsed;
        }
        private void NomineePincodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberValidation(NomineePincodeBox.Text))
            {
                NomineePincodeBox.BorderBrush = GrayColor;
                NomineePincodeError.Visibility = Visibility.Collapsed;
            }
            else
            {
                NomineePincodeBox.BorderBrush = (Brush)new BrushConverter().ConvertFrom("Red");
            }
        }

        private void NomineeCityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeCityBox.BorderBrush = GrayColor;
            NomineeCityError.Visibility = Visibility.Collapsed;
        }

        private void NomineeStateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomineeStateBox.BorderBrush = GrayColor;
            NomineeStateError.Visibility = Visibility.Collapsed;
        }

        private void GuarantorPincodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberValidation(GuarantorPincodeBox.Text))
            {
                GuarantorPincodeBox.BorderBrush = GrayColor;
                GuarantorPincodeError.Visibility = Visibility.Collapsed;
            }
            else
            {
                GuarantorPincodeBox.BorderBrush = (Brush)new BrushConverter().ConvertFrom("Red");

            }
        }

        bool CustomerValidation()
        {
            bool _check = true;
            if (String.IsNullOrEmpty(CustomerNameBox.Text))
            {
                CustomerNameBox.BorderBrush = Redcolor;
                CustNameError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (Male.IsChecked == false && Female.IsChecked == false)
            {
                CustGender.Visibility = Visibility.Visible;
                _check = false;
            }
            if (SelectDOB.SelectedDate == DateTime.Today && CalculateAge(SelectDOB.SelectedDate.Value)==0 && AgeValidation(customer.Age)==false)
            {
                CustDobError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(FatherNameBox.Text))
            {
                FatherNameBox.BorderBrush = Redcolor;
                CustFatherError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(MotherNameBox.Text))
            {
                MotherNameBox.BorderBrush = Redcolor;
                CustMotherError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(ContactBox.Text))
            {
                ContactBox.BorderBrush = Redcolor;
                CustContactError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(SelectReligion.Text))
            {
                CustReligionError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(CasteBox.Text))
            {
                CustCasteError.Visibility = Visibility.Visible;
                CasteBox.BorderBrush = Redcolor;
                _check = false;
            }
            if (String.IsNullOrEmpty(CommunityBox.Text))
            {
                CustCommunityError.Visibility = Visibility.Visible;
                CommunityBox.BorderBrush = Redcolor;
                _check = false;
            }
            if (String.IsNullOrEmpty(EducatinBox.Text))
            {
                EducatinBox.BorderBrush = Redcolor;
                CustEducationError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(FamilyMemberBox.Text) || FamilyMemberBox.Text == "0")
            {
                FamilyMemberBox.BorderBrush = Redcolor;
                CustFamilyMemberError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(EarningMemberBox.Text) || EarningMemberBox.Text == "0")
            {
                EarningMemberBox.BorderBrush = Redcolor;
                CustEarningError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(OccupationBox.Text))
            {
                OccupationBox.BorderBrush = Redcolor;
                CustOccupationError.Visibility = Visibility;
                _check = false;
            }
            if (String.IsNullOrEmpty(MonthlyIncomeBox.Text) || MonthlyIncomeBox.Text == "₹. 0")
            {
                MonthlyIncomeBox.BorderBrush = Redcolor;
                CustMonthlyIncomeError.Visibility = Visibility.Visible;
                _check = false;
            }
            if (String.IsNullOrEmpty(MonthlyExpensesBox.Text) || MonthlyExpensesBox.Text == "₹. 0")
            {
                MonthlyExpensesBox.BorderBrush = Redcolor;
                CustMonthlyExpensesError.Visibility = Visibility.Visible;
                _check = false;
            }


            //if(String.IsNullOrEmpty(HouseNOBox.Text))
            //{
            //    HouseNOBox.BorderBrush = Redcolor;
            //    CustDoorError.Visibility = Visibility.Visible;
            //    _check = false;
            //}



            if(String.IsNullOrEmpty(StreetNameBox.Text))
            {
                StreetNameBox.BorderBrush = Redcolor;
                CustStreetError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(LocalityBox.Text))
            {
                LocalityBox.BorderBrush = Redcolor;
                CustLocalityError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(CityBox.Text))
            {
                CityBox.BorderBrush = Redcolor;
                CustCityError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(PincodeBox.Text))
            {
                PincodeBox.BorderBrush = Redcolor;
                CustPincodeError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(StateBox.Text))
            {
                StateBox.BorderBrush = Redcolor;
                CustStateError.Visibility = Visibility.Visible;
                _check = false;
            }
            //if(String.IsNullOrEmpty(HouseTypeBox.Text))
            //{
            //    HouseTypeBox.BorderBrush = Redcolor;
            //    CustHousingTypeError.Visibility = Visibility.Visible;
            //    _check = false;
            //}
            if(guarantor.IsGuarantorNull==false)
            {
                GuarantorErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(nominee.IsNomineeNull==false)
            {
                NomineeErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(customer.HavingBankDetails==false)
            {
                BankErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(AadharNo.Text))
            {
                aadharErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(string.IsNullOrEmpty(HusbandBox.Text))
            {
                CustHusbandError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(customer.YearlyIncome<=150000)
            {
                CustYearlyExpensesError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(string.IsNullOrEmpty(customer.Taluk))
            {
                CustTalukError.Visibility = Visibility.Visible;
                _check = false;
            }
            if(PhotoProofNoBox.SelectedItem==null)
            {
                photoProofNoErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(AddressProofNoBox.SelectedItem==null)
            {
                photoProofNoErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(string.IsNullOrEmpty(PhotoProofNo.Text))
            {
                addressNoErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            if(String.IsNullOrEmpty(AddressProofNoTextBox.Text))
            {
                addressNoErrorCheck.Visibility = Visibility.Visible;
                _check = false;
            }
            return _check;
        }
        private void CustomerNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(CustNameError.Visibility==Visibility.Visible )
            {
                CustNameError.Visibility = Visibility.Collapsed;
                CustomerNameBox.BorderBrush = GrayColor;
            }
            if(NameValidation(CustomerNameBox.Text)==false)
            {
                CustomerNameBox.BorderBrush = Redcolor;
            }
            else
            {
                CustomerNameBox.BorderBrush = GrayColor;
            }
        }

        private void SelectDOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustDobError.Visibility == Visibility.Visible)
            {
                CustDobError.Visibility = Visibility.Collapsed;
            }
            DateTime SelectedDate = SelectDOB.SelectedDate.Value;
            int Age = CalculateAge(SelectedDate);
            if(Age!=0)
            {
                if (Age <= 18 || Age > 59)
                {
                    MessageBoxResult Result = MessageBox.Show("This User is Not Valid!...\n(The " +
                        "Age Range Between 18-59)\n Are You Sure You Want To Continue.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
                
            
        }

        private void FatherNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustFatherError.Visibility == Visibility.Visible)
            {
                CustFatherError.Visibility = Visibility.Collapsed;
                FatherNameBox.BorderBrush = GrayColor;
            }
            if (NameValidation(CustomerNameBox.Text) == false)
            {
                FatherNameBox.BorderBrush = Redcolor;
            }
            else
            {
                FatherNameBox.BorderBrush = GrayColor;
            }
        }

        private void MotherNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustMotherError.Visibility == Visibility.Visible)
            {
                CustMotherError.Visibility = Visibility.Collapsed;
                MotherNameBox.BorderBrush = GrayColor;
            }
            if (NameValidation(MotherNameBox.Text) == false)
            {
                MotherNameBox.BorderBrush = Redcolor;
            }
            else
            {
                MotherNameBox.BorderBrush = GrayColor;
            }
        }

        private void ContactBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustContactError.Visibility == Visibility.Visible)
            {
                CustContactError.Visibility = Visibility.Collapsed;
                ContactBox.BorderBrush = GrayColor;
            }
            if(PhoneNumberValidation(ContactBox.Text))
            {
                ContactBox.BorderBrush = GrayColor;
            }
            else
            {
                ContactBox.BorderBrush = Redcolor;
            }
        }

        private void CasteBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustCasteError.Visibility == Visibility.Visible)
            {
                CustCasteError.Visibility = Visibility.Collapsed;
                CasteBox.BorderBrush = GrayColor;
            }
        }

        private void CommunityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustCommunityError.Visibility == Visibility.Visible)
            {
                CustCommunityError.Visibility = Visibility.Collapsed;
                CommunityBox.BorderBrush = GrayColor;
            }
        }

        private void EducatinBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustEducationError.Visibility == Visibility.Visible)
            {
                CustEducationError.Visibility = Visibility.Collapsed;
                EducatinBox.BorderBrush = GrayColor;
            }
        }

        private void FamilyMemberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustFamilyMemberError.Visibility == Visibility.Visible)
            {
                CustFamilyMemberError.Visibility = Visibility.Collapsed;
                FamilyMemberBox.BorderBrush = GrayColor;
            }
            if(NumberValidation(FamilyMemberBox.Text))
            {
                FamilyMemberBox.BorderBrush = GrayColor;
            }
            else
            {
                FamilyMemberBox.BorderBrush = Redcolor;
            }
        }

        private void EarningMemberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustEarningError.Visibility == Visibility.Visible)
            {
                CustEarningError.Visibility = Visibility.Collapsed;
                EarningMemberBox.BorderBrush = GrayColor;
            }
            if (NumberValidation(EarningMemberBox.Text))
            {
                EarningMemberBox.BorderBrush = GrayColor;
            }
            else
            {
                EarningMemberBox.BorderBrush = Redcolor;
            }
        }
       
        private void OccupationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustOccupationError.Visibility == Visibility.Visible)
            {
                CustOccupationError.Visibility = Visibility.Collapsed;
                OccupationBox.BorderBrush = GrayColor;
            }
        }

        private void MonthlyIncomeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustMonthlyIncomeError.Visibility == Visibility.Visible)
            {
                CustMonthlyIncomeError.Visibility = Visibility.Collapsed;
                MonthlyIncomeBox.BorderBrush = GrayColor;
            }
            if(MoneyValidation(MonthlyIncomeBox.Text))
            {
                MonthlyIncomeBox.BorderBrush = GrayColor;
            }
            else
            {
                MonthlyIncomeBox.BorderBrush = Redcolor;
            }
        }


        private void MonthlyExpensesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustMonthlyExpensesError.Visibility == Visibility.Visible)
            {
                CustMonthlyExpensesError.Visibility = Visibility.Collapsed;
                MonthlyExpensesBox.BorderBrush = GrayColor;
            }
            if (MoneyValidation(MonthlyExpensesBox.Text))
            {
                MonthlyExpensesBox.BorderBrush = GrayColor;
            }
            else
            {
                MonthlyExpensesBox.BorderBrush = Redcolor;
            }
        }

        private void HouseNOBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (CustDoorError.Visibility == Visibility.Visible)
            //{
            //    CustDoorError.Visibility = Visibility.Collapsed;
            //    HouseNOBox.BorderBrush = GrayColor;
            //}
        }

        private void StreetNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustStreetError.Visibility == Visibility.Visible)
            {
                CustStreetError.Visibility = Visibility.Collapsed;
                StreetNameBox.BorderBrush = GrayColor;
            }
        }

        private void LocalityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustLocalityError.Visibility == Visibility.Visible)
            {
                CustLocalityError.Visibility = Visibility.Collapsed;
                LocalityBox.BorderBrush = GrayColor;
            }
        }

        private void PincodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustPincodeError.Visibility == Visibility.Visible)
            {
                CustPincodeError.Visibility = Visibility.Collapsed;
                PincodeBox.BorderBrush = GrayColor;
            }
        }

        private void CityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustCityError.Visibility == Visibility.Visible)
            {
                CustCityError.Visibility = Visibility.Collapsed;
                CityBox.BorderBrush = GrayColor;
            }
        }

        private void StateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CustStateError.Visibility == Visibility.Visible)
            {
                CustStateError.Visibility = Visibility.Collapsed;
                StateBox.BorderBrush = GrayColor;
            }
        }

        //private void HouseTypeBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (CustHousingTypeError.Visibility == Visibility.Visible)
        //    {
        //        CustHousingTypeError.Visibility = Visibility.Collapsed;
        //        HouseTypeBox.BorderBrush = GrayColor;
        //    }
        //}

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }
        bool AadharValidatin(string Aadhar)
        {
            if (Aadhar.Length <= 12)
            {
                for (int i = 0; i < Aadhar.Length; i++)
                {
                    if (char.IsDigit(Aadhar[i]) == false)
                    {
                        return false;
                    }
                }
                if (Aadhar.Length == 12)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        public bool IsAadharAlreadeyExists;
        void CheckAadharAlreadyExist()
        {
            using(SqlConnection sql =new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Count(AadharNumber) from CustomerDetails where AadharNumber = '" + AadharNo.Text + "'";
                int getCount =(int)command.ExecuteScalar();
                if(getCount==0)
                {
                    IsAadharAlreadeyExists = false;
                }
                else
                {
                    IsAadharAlreadeyExists = true;
                }
            }
        }
        private void AadharNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckAadharAlreadyExist();
            if(IsAadharAlreadeyExists)
            {
                MessageBox.Show("Aadhar Number is Already Exist..");
            }
            if(AadharValidatin(AadharNo.Text))
            {
                aadharErrorCheck.Visibility = Visibility.Collapsed;
            }
            else
            {
                aadharErrorCheck.Visibility = Visibility.Visible;
            }
        }

        private void HusbandBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(NameValidation(HusbandBox.Text))
            {
                CustHusbandError.Visibility = Visibility.Collapsed;
            }
            else
            {
                CustHusbandError.Visibility = Visibility.Visible;
            }
        }

        private void CommunityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustCommunityError.Visibility == Visibility.Visible)
            {
                CustCommunityError.Visibility = Visibility.Collapsed;
                CommunityBox.BorderBrush = GrayColor;
            }
        }

        private void FamilyYearIncomeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(MoneyValidation(FamilyYearIncomeBox.Text))
            {
                CustYearlyExpensesError.Visibility = Visibility.Collapsed;
            }
            else
            {
                CustYearlyExpensesError.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddPG_Click(object sender, RoutedEventArgs e)
        {
            AddPg APG = new AddPg();
            APG.ShowDialog();
        }

        private void LandDetailsOKBtn_Click(object sender, RoutedEventArgs e)
        {
            customer.LandType = LantypeBox.Text.ToUpper();
            customer.LandVolume = LandVolumeBox.Text.ToString();
            LandDetialsPanel.Visibility = Visibility.Collapsed;
            LandHoldingShowBtn.Visibility = Visibility.Visible;
        }

        private void LandHoldingShowBtn_Click(object sender, RoutedEventArgs e)
        {
            LandDetialsPanel.Visibility = Visibility.Visible;
        }

        private void LandHoldingCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             string value = LandHoldingCombo.SelectedValue as string;
            if(value!=null)
            {
                if (value == "YES")
                {
                    LandDetialsPanel.Visibility = Visibility.Visible;
                }
                else if (value == "NO")
                {
                    LandHoldingShowBtn.Visibility = Visibility.Collapsed;
                }
            }

           


        }

        private void ifsccode_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox IFSCCode = sender as TextBox;
            string ifsctext = IFSCCode.Text;
            if(ifsctext.Length==11)
            {
                if(IsExists(ifsctext))
                {
                    BankDetailsView bank = Bankdetail(ifsctext);
                    setBankDetails(bank);
                }
                else
                {
                    unsetBankDetails();
                }
            }
            else
            {
                unsetBankDetails();
            }
        }


        void unsetBankDetails()
        {
            //ifsccode.Text = "";
            micrcode.Text = "";
            BankNameComboBox.SelectedIndex = -1;
            bankname.Text = "";
            micrcode.IsReadOnly = false;
            BankNameComboBox.IsReadOnly = false;
            bankname.IsReadOnly = false;
        }

        void setBankDetails(BankDetailsView bank)
        {
            ifsccode.Text = bank.IFSCCode;
            micrcode.Text = bank.MICRCode;
            BankNameComboBox.SelectedValue = bank.BankName;
            bankname.Text = bank.BranchName;

            micrcode.IsReadOnly = true;
            BankNameComboBox.IsReadOnly = true;
            bankname.IsReadOnly = true;
        }

        public  bool IsExists(string IFSCCode)
        {
            bool result = false;
            foreach (BankDetailsView b in BankDetailsList)
            {
                if (b.IFSCCode == IFSCCode)
                {
                    return true;
                }
            }
            return result;
        }

        BankDetailsView Bankdetail(string IFSCCode)
        {
            BankDetailsView Bank = new BankDetailsView();
            foreach (BankDetailsView b in BankDetailsList)
            {
                if (b.IFSCCode == IFSCCode)
                {
                    Bank.BankName = b.BankName;
                    Bank.BranchName = b.BranchName;
                    Bank.IFSCCode = b.IFSCCode;
                    Bank.MICRCode = b.MICRCode;
                }
            }
            return Bank;

        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerGrid.Visibility = Visibility.Collapsed;
            AddressGrid.Visibility = Visibility.Collapsed;

            CustomerOtherDetails.Visibility = Visibility.Visible;
        }

        private void AadharPassBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AadharNo.IsEnabled = true;
        }

        private void AadharNo_LostFocus(object sender, RoutedEventArgs e)
        {
            string AadharPass = AadharPassBox.Password;
            string AadharNumber = AadharNo.Text;
            if(AadharPass.Length==12 && AadharNumber.Length==12)
            {
                if(!AadharPass.Equals(AadharNumber))
                {
                    MessageBox.Show("Please Enter Valid Aadhar Number!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    AadharPassBox.Clear();
                    AadharNo.Clear();
                    AadharPassBox.Focus();
                    AadharNo.IsEnabled = false;
                    
                }
                else
                {
                    PhotoProofNoBox.SelectedIndex = 1;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Valid Aadhar Number!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                AadharPassBox.Clear();
                AadharNo.Clear();
                AadharPassBox.Focus();
                AadharNo.IsEnabled = false;
            }
        }

        private void PhotoProofNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if(AadharNo.Text!=PhotoProofNo.Text)
            {
                MessageBox.Show("Please Enter Valid Aadhar Number!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                PhotoProofNo.Text = "";
            }
        }

       

        public int CalculateAge(DateTime date)
        {
            int age = 0;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            age = year - date.Year;
            if (date.Month > month)
            {
                age -= 1;
            }
            return age;
        }

        public bool AgeValidation(int Age)
        {
            bool Result = false;
            if(Age>18&&Age<60)
            {
                return true;
            }
            return Result;
        }
    }
}
