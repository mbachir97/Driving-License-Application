using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Bisness;

namespace DVLD.Applications
{
    public partial class NewLocalDrivingApplication : Form
    {

        private int _PersonID = -1;


        int _LocalApplicationID;

        clsLocalDrivingLicenseApplication _LocalDrivingLicense;
        enum enApplicationStatus {enNew=1,enCanceled=2,enCompleted=3}   


        public NewLocalDrivingApplication()
        {
            InitializeComponent();
            Mode = enMode.AddMode;
        }


        public NewLocalDrivingApplication(int LocalApplicationID)
        {
            InitializeComponent();
            _LocalApplicationID=LocalApplicationID; 

            Mode = enMode.UpdateMode;

        }
        enum enMode { AddMode=1,UpdateMode=2}
        enMode Mode;

        void _FillClassCombo()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClass();
            foreach (DataRow dr in dt.Rows) {
                cbLicensesClass.Items.Add(dr["ClassName"]);
            
            }

        }
        void ResetDefaultValue()
        {
            _FillClassCombo();
            if (Mode == enMode.AddMode)
            {
                lblAppDate.Text = DateTime.Now.ToShortDateString();
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType
                    .enNewLocalDrivingApp).ApplicationFees.ToString();
                   
                lblCreatedby.Text = clsCurrentUser.CurrentUser.UserName;
                btnSave.Enabled = false;
                tabAppInfo.Enabled = false;
                cbLicensesClass.SelectedIndex = 2;
                lblTitle.Text = "New Local Driving License Application ";
                ctrPersonCardWithFilter1.FilterFocus();
                _LocalDrivingLicense= new clsLocalDrivingLicenseApplication();


            }
            else
            {

                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tabAppInfo.Enabled = true;
                btnSave.Enabled = true;


            }



        }

        void LoadData()
        {
            _LocalDrivingLicense = clsLocalDrivingLicenseApplication.FindByLocalAppID(_LocalApplicationID);
            ctrPersonCardWithFilter1.FilterEnabled = false;
            if(_LocalDrivingLicense==null)
            {

                MessageBox.Show("No Application with ID = " + _LocalApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;

            }

            ctrPersonCardWithFilter1.LoadInformation1(_LocalDrivingLicense.ApplicantPersonID);
            cbLicensesClass.SelectedIndex = cbLicensesClass.FindString
                (clsLicenseClass.Find(_LocalDrivingLicense.LicenseClassID)
                .ClassName);

            lblFees.Text = _LocalDrivingLicense.PaidFees.ToString();
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);  
            lblCreatedby.Text = _LocalDrivingLicense.CreatedByUserID.ToString();  
            lblLocalApp.Text=_LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();
            lblLocalApp.Text = _LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();

        }

        private void NewLocalDrivingApplication_Load(object sender, EventArgs e)
        {
           
            ResetDefaultValue();    
            if(Mode == enMode.UpdateMode)
                LoadData(); 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ctrPersonCardWithFilter1.PersonID != -1)
            {
                btnSave.Enabled = true;
                tabAppInfo.Enabled=true;
                tabControle.SelectedTab = tabControle.TabPages["tabAppInfo"];
            }

            else
            {
                MessageBox.Show("Please Select A Person First ","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            //clsPerson Person = ctrPersonCardWithFilter1.Person;

            //clsApplication Application = new clsApplication();

            //clsLocalDrivingLicenseApplication LocalLicenses = new clsLocalDrivingLicenseApplication();

            //Application.ApplicantPersonID = _PersonID;
            //Application.ApplicationDate = DateTime.Now;
            //Application.ApplicationTypeID = 1;
            //Application.ApplicationStatus = 1;
            //Application.PaidFees = 15;
            //Application.LastStatusDate = DateTime.Now;
            //Application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;

            //if (clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(cbLicensesClass.Text, Person.NationalNO, "Completed")
            //    || clsLocalDrivingLicenseApplication.IsLocalDrivingLicenseApplicationExist(cbLicensesClass.Text, Person.NationalNO, "New"
            //    ))
            //{

            //    MessageBox.Show("this Person Already has an Application whith Application ID", "Error",
            //         MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}


            int LicenseClassID = clsLicenseClass.Find(cbLicensesClass.Text).LicenseClassID;

            int ActiveAppID = clsApplication.GetActiveApplicaionIDForLicence(ctrPersonCardWithFilter1.PersonID
                , clsApplication.enApplicationType.enNewLocalDrivingApp, LicenseClassID);

            if(ActiveAppID != -1)
            {
                 MessageBox.Show("Choose another License Class, the selected Person Already have an active application " +
                     "for the selected class with id=" + ActiveAppID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicensesClass.Focus();
                return;
            }


            if(clsLicense.IsLicenseExistByPersonID(ctrPersonCardWithFilter1.PersonID,
                LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicense.ApplicantPersonID= ctrPersonCardWithFilter1.PersonID;
            _LocalDrivingLicense.ApplicationStatus = (int)clsApplication.enApplicationStatus.New;
            _LocalDrivingLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.enNewLocalDrivingApp;
            _LocalDrivingLicense.ApplicationDate = DateTime.Now;    
            _LocalDrivingLicense.LastStatusDate=DateTime.Now;   
            _LocalDrivingLicense.LicenseClassID = LicenseClassID;
            _LocalDrivingLicense.PaidFees = Convert.ToSingle(lblFees.Text);
               
            _LocalDrivingLicense.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;


            if (_LocalDrivingLicense.Save1())
            {
                lblLocalApp.Text = _LocalDrivingLicense.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                Mode = enMode.UpdateMode;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            //if (Application.Save())
            //{
            //    LocalLicenses.ApplicationID = Application.ApplicationID;
            //    LocalLicenses.LicenseClassID = clsLicenseClass.Find(cbLicensesClass.Text).LicenseClassID;

            //    if (LocalLicenses.Save())
            //    {
            //        MessageBox.Show("Application Added Succefuly ", "Saved",
            //           MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        lblLocalApp.Text = LocalLicenses.LocalDrivingLicenseApplicationID.ToString();
            //    }

            //    else
            //        MessageBox.Show("Faild to ADD LocalApplication", "Faild",
            //          MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
            //else
            //{
            //    MessageBox.Show("Faild to ADD Application", "Faild",
            //          MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}






        }
        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;    
        }
    }
}
