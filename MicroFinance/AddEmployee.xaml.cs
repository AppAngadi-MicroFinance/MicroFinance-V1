using MicroFinance.Modal;
using Microsoft.Win32;
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
using MicroFinance.Utils;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public bool Isnew;
        public List<string> ProofTypes = new List<string> { "Aadhar Proof", "Family Card", "Licence", "VoterID" };
        public List<string> DesignationList = new List<string> { "Manager", "Region Manager","Accountant","Field Officer"};
        public List<string> DesignationList1=new List<string> {"Accountant", "Field Officer" };
        public List<Branch> Branchlist;
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        public List<string> Religionlist = new List<string> { "Muslim", "Hindu", "Christianity" };
        Branch branch = new Branch();
        Employee addemployee = new Employee();
        StringBuilder RequiredFields = new StringBuilder();
        StringBuilder Emptyfields = new StringBuilder();
        Employee tempemployee;
        public AddEmployee(Employee emp)
        {
            InitializeComponent();
            branch.GetBranchList();
            branch.GetRegionList();
            Branchlist = branch.BranchList;
            AddressProofcombo.ItemsSource = ProofTypes;
            PhotoproofCombo.ItemsSource = ProofTypes;
            RegionCombo.ItemsSource = branch.RegionList;
            if(MainWindow.LoginDesignation.LoginDesignation=="Manager")
            {
                DesignationCombo.ItemsSource = DesignationList1;
            }
            else
            {
                DesignationCombo.ItemsSource = DesignationList;
            }
            Religioncombo.ItemsSource = Religionlist;
            BranchCombo.Text = emp.BranchName;
            EmployeeMainGrid.DataContext = emp;
            emp.Region = branch.GetRegionName(emp.BranchID);
            emp.BranchName = branch.GetBranchName(emp.BranchID);
            DesignationGrid.DataContext = emp;
            addemployee = emp;
            tempemployee = emp;
            EmployeeSaveBtn.Content = "Update";
            capturepanel.Visibility = Visibility.Collapsed;
            Captureframe.NavigationService.Navigate(new Capture());
            Isnew = false;
            CancelBtn.Visibility = Visibility.Collapsed;
            DeacativeBtn.Visibility = Visibility.Visible;
            BackBtn.Visibility = Visibility.Visible;
        }
        public AddEmployee()
        {
            InitializeComponent();
            addemployee = new Employee();
            Isnew = true;
            branch.GetBranchList();
            branch.GetRegionList();
            EmployeeMainGrid.DataContext = addemployee;
            capturepanel.Visibility = Visibility.Collapsed;
            Branchlist = branch.BranchList;
           
            Captureframe.NavigationService.Navigate(new Capture());
            AddressProofcombo.ItemsSource = ProofTypes;
            PhotoproofCombo.ItemsSource = ProofTypes;
            if (MainWindow.LoginDesignation.LoginDesignation == "Manager")
            {
                DesignationCombo.ItemsSource = DesignationList1;
                RegionCombo.ItemsSource = branch.RegionList;
                addemployee.Region = MainWindow.LoginDesignation.RegionName;
                addemployee.BranchName = MainWindow.LoginDesignation.BranchName;
                RegionCombo.IsEnabled = false;
                BranchCombo.IsEnabled = false;
               
            }
            else
            {
                DesignationCombo.ItemsSource = DesignationList;
                RegionCombo.ItemsSource = branch.RegionList;
            }
            Religioncombo.ItemsSource = Religionlist;
        }

        private void BankDetails_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAccountdetailsPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;

        }

        private void PanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAccountdetailsPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void AddressproofBtn_Click(object sender, RoutedEventArgs e)
        { 
            //Captureframe.NavigationService.Navigate(new Capture());
            PhotoProofNametxt.Text = "Address Proof";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;
        }

        private void SampleCheck_Click(object sender, RoutedEventArgs e)
        {
            addemployee.HavingBankDetails = true;
            EmployeeAccountdetailsPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void ImageSavebtn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Capture.SavedImage;
            string txt = PhotoProofNametxt.Text;
            SetImage(image, txt);
            capturepanel.Visibility = Visibility.Collapsed;
            EmployeeDetailsGrid.IsEnabled = true;
        }

        private void CaptureModelCloseButton_Click(object sender, RoutedEventArgs e)
        {
            capturepanel.IsEnabled = false;
            capturepanel.Visibility = Visibility.Collapsed;
            EmployeeDetailsGrid.IsEnabled = true;
        }

        public void SetImage(BitmapImage image,string imagename)
        {
            switch(imagename)
            {
                case "Address Proof":
                    addemployee.AddressProofImage = image;
                    addemployee.IsAddressProof = true;
                    message = language.translate(SystemFunction.IsTamil, "SA8");//Address Proof Added
                    MainWindow.StatusMessageofPage(1, message);
                    break;
                case "Photo Proof":
                    addemployee.PhotoProofImage = image;
                    addemployee.IsPhotoProof = true;
                    message = language.translate(SystemFunction.IsTamil, "SA9");//Photo Proof Added
                    MainWindow.StatusMessageofPage(1, message);
                    break;
                case "Profile Picture":
                    addemployee.ProfileImage = image;
                    addemployee.IsProfilePicture = true;
                    message = language.translate(SystemFunction.IsTamil, "SA10");//Profile Picture Added
                    MainWindow.StatusMessageofPage(1, message);
                    break;
            }
        }

        private void PhotoProofBtn_Click(object sender, RoutedEventArgs e)
        {
            Captureframe.NavigationService.Navigate(new Capture());
            PhotoProofNametxt.Text = "Photo Proof";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;

        }

        private void ProfilePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            Captureframe.NavigationService.Navigate(new Capture());
            PhotoProofNametxt.Text = "Profile Picture";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;

        }

        private void EmployeeSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            IsemptyCheck();
            if (RequiredFields.Length == 0)
            {
                if (Emptyfields.Length == 0)
                {
                    ConfirmationPanel.IsOpen = true;
                    EmployeeMainGrid.IsEnabled = false;
                }
                else
                {
                    message = language.translate(SystemFunction.IsTamil, "W21");//Check These fields are Empty\n
                    string messagetwo = language.translate(SystemFunction.IsTamil, "W22");//Are you sure You want to create Employee Without These Information
                    if (MessageBox.Show(message + Emptyfields.ToString() + messagetwo, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        ConfirmationPanel.IsOpen = true;
                        EmployeeMainGrid.IsEnabled = false;
                    }
                }
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "M4");//These Fields are Mandatory Please Fill All these Fields\n
                MessageBox.Show(message+"\n"+ RequiredFields.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BankDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            //proofDetailsBtn.IsEnabled = false;
            BankDetailsPanel.IsOpen = true;
        }

        private void proofDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationPanel.IsEnabled = false;
            ProofDetailsPanel.IsOpen = true;
        }

        private void bankdetailspanelclosebtn_Click(object sender, RoutedEventArgs e)
        {
            BankDetailsPanel.IsOpen = false;
            ConfirmationPanel.IsEnabled = true;
        }

        private void ProofDetailscloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ProofDetailsPanel.IsOpen = false;
            ConfirmationPanel.IsEnabled = true;
        }
        private void ConfirmationPanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmployee());
        }

        private void EmpAdd_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
            // addemployee.EmployeeAdd();
            
            try
            {
                
                if(EmployeeSaveBtn.Content.ToString()=="Save")
                {
                    bool Result = EmployeeRepository.AddEmployee(addemployee);
                    message = language.translate(SystemFunction.IsTamil, "SA22");//Employee Added Successfully
                    MainWindow.StatusMessageofPage(1, message);
                    this.NavigationService.Navigate(new AddEmployee());
                }
                else if(EmployeeSaveBtn.Content.ToString()=="Update")
                {
                   
                    ConfirmationPanel.IsOpen = false;
                    EmployeeMainGrid.IsEnabled = true;
                    addemployee.EmployeeAdd();
                    message = language.translate(SystemFunction.IsTamil, "SA23");//Employee Updated Successfully
                    MainWindow.StatusMessageofPage(1, message);
                    this.NavigationService.Navigate(new AddEmployee());
                }
                
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, ex.Message);
            }
        }
        public void IsemptyCheck()
        {
            RequiredFields = new StringBuilder();
            Emptyfields = new StringBuilder();
            if (BranchCombo.Text == "")
            {
                RequiredFields.Append("Branch Name*\n");
            }
            if (DesignationCombo.Text == "")
            {
                RequiredFields.Append("Designation*\n");
            }
            if (Employeenamebox.Text == "")
            {
                RequiredFields.Append("Employee Name*\n");
            }
            if (Employeedob.Text == ""||Employeedob.Text==DateTime.Now.ToString("dd/MM/yyyy"))
            {
                RequiredFields.Append("Employee DOB*\n");
            }
            if (Employeeage.Text == ""|| Employeeage.Text=="0")
            {
                RequiredFields.Append("Employee Age*\n");
            }
            if (EmployeeContactnumber.Text == "")
            {
                RequiredFields.Append("Employee Number*\n");
            }
            if (EmployeeEmail.Text == "")
            {
                RequiredFields.Append("Employee Email*\n");
            }
            if (EmployeeAadhar.Text == "")
            {
                RequiredFields.Append("Employee Aadhar*\n");
            }
            if (EmployeeEducation.Text == "")
            {
                RequiredFields.Append("Employee Education*\n");
            }
            if (Employeejoiningdate.Text == "")
            {
                RequiredFields.Append("Date of Joining*\n");
            }
            if (houseno.Text == "")
            {
                Emptyfields.Append("HouseNo\n");
            }
            if (townname.Text == "")
            {
                Emptyfields.Append("Town Name\n");
            }
            if (district.Text == "")
            {
                Emptyfields.Append("District\n");
            }
            if (pincode.Text == "")
            {
                Emptyfields.Append("Pincode\n");
            }
            if (AddressProofcombo.Text == "")
            {
                RequiredFields.Append("Address Proof*\n");
            }
            if (PhotoproofCombo.Text == "")
            {
                RequiredFields.Append("Photo Proof*\n");
            }
            if (accountholdername.Text == "")
            {
                Emptyfields.Append("Accout Holder Name\n");
            }
            if (accountnumber.Text == "")
            {
                Emptyfields.Append("Account Number\n");
            }
            if (bankname.Text == "")
            {
                Emptyfields.Append("Bank Name\n");
            }
            if (bankbranchname.Text == "")
            {
                Emptyfields.Append("Bank Branch Name\n");
            }
            if (ifsccode.Text == "")
            {
                Emptyfields.Append("IFSC Code\n");
            }
            if (micrcode.Text == "")
            {
                Emptyfields.Append("MICR Code\n");
            }
        }

        private void Religioncombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Isnew==true&&addemployee.IsExists()==true)
            {
                message = language.translate(SystemFunction.IsTamil, "AE10");//The Employee Already Exists.... Please Check!.
                MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.NavigationService.Navigate(new AddEmployee());
            }
        }

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchCombo.Items.Clear();
            string regionname = RegionCombo.SelectedValue as string;
            FetchBranch(regionname);
        }

        public void FetchBranch(string regionname)
        {
            foreach(Branch b in Branchlist)
            {
                if(b.RegionName==regionname)
                {
                    BranchCombo.Items.Add(b.BranchName);
                }
            }
        }

        private void ProofDetailsviewcloseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddressProofViewPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void ViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            AddressProofViewPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;
        }

        private void EditAddressProof_Click(object sender, RoutedEventArgs e)
        {
            PhotoProofNametxt.Text = "Address Proof";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;
        }

        private void ViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            PhotoProofViewPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;
        }

        private void AddressDetailsviewcloseBtn_Click(object sender, RoutedEventArgs e)
        {
            PhotoProofViewPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void EditPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            Captureframe.NavigationService.Navigate(new Capture());
            PhotoProofNametxt.Text = "Photo Proof";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;
        }

        private void ProfilePictureviewcloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePictureViewPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void ViewProfileProof_Click(object sender, RoutedEventArgs e)
        {
            ProfilePictureViewPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;
        }

        private void EditProfileProof_Click(object sender, RoutedEventArgs e)
        {
            Captureframe.NavigationService.Navigate(new Capture());
            PhotoProofNametxt.Text = "Profile Picture";
            capturepanel.Visibility = Visibility.Visible;
            EmployeeDetailsGrid.IsEnabled = false;
            capturepanel.IsEnabled = true;
        }

        private void bankdetailsviewpanelclosebtn_Click(object sender, RoutedEventArgs e)
        {
            BankDetailsviewPanel.IsOpen = false;
            EmployeeMainGrid.IsEnabled = true;
        }

        private void ViewBank_Click(object sender, RoutedEventArgs e)
        {
            BankDetailsviewPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;
        }

        private void EditBank_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAccountdetailsPanel.IsOpen = true;
            EmployeeMainGrid.IsEnabled = false;
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void DecativeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackNtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }
    }
}
