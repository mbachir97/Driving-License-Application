using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class scheduelTest : UserControl
    {

        enum enMode { enAdd = 1, enEdit = 2 }

        enMode  _Mode= enMode.enAdd;    

        private bool _RetakeTestEnable = false;

        private int _LocalAppID = -1;
        clsTestAppointment _TestAppointment;

        private float _ReTakeAppFees = clsApplicationType.Find((int)clsApplication.
            enApplicationType.enRetakeTest).ApplicationFees;

        public   bool RetakeTestEnable
        {
            get { return _RetakeTestEnable; }
            
            set 
            {
                _RetakeTestEnable = value;

                gbRetakeTest.Enabled = _RetakeTestEnable;
                    ;}
        }

      public   void LoadInformation(int LocalAppID)
        {
            _Mode= enMode.enAdd;    
            _LocalAppID =LocalAppID; 
            schudueledTest1.LoadLocalAppInformation(_LocalAppID);
            //schudueledTest1.Title = (_RetakeTestEnable == false) ? "Schudueled Test" : "Schudueled ReTake Test";


            _TestAppointment =new clsTestAppointment();
            _FillReTakeTestInformation();

        }

        void _FillReTakeTestInformation()
        {
           lblRetakeAppID.Text= (_TestAppointment.RetakeTestApplicationID==-1)? "N/A": _TestAppointment.RetakeTestApplicationID.ToString();
           lbAppFees.Text=(gbRetakeTest.Enabled==true)?_ReTakeAppFees.ToString():"0";
           lblTotalFees.Text = (gbRetakeTest.Enabled == true)?(_ReTakeAppFees+schudueledTest1.TestFees).ToString(): _ReTakeAppFees.ToString();  
       
        }

        public void LoadInformation(clsTestAppointment TestAppointment)
        {

            _Mode=enMode.enEdit;
            if (TestAppointment == null)
            {
                MessageBox.Show("This Appointment is not Exist","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else
            {
                _TestAppointment = TestAppointment;
                _LocalAppID =
                    _TestAppointment.LocalDrivingLicenseInfo.LocalDrivingLicenseApplicationID;
                schudueledTest1.LoadLocalAppInformation
                (_TestAppointment.LocalDrivingLicenseInfo.LocalDrivingLicenseApplicationID);
                schudueledTest1.IsLockedLableVisible = (_TestAppointment.IsLocked);
                schudueledTest1.IsDateTimePickerEnable = (!_TestAppointment.IsLocked);
                btnSave.Enabled = (!_TestAppointment.IsLocked);
                _FillReTakeTestInformation();
            }
          

        }
        public scheduelTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_Mode==enMode.enEdit)
            {
                _TestAppointment.AppointmentDate = schudueledTest1.AppointmentDate;
                if (_TestAppointment.Save())
                    MessageBox.Show("Appointment Update Succesfuly", "Save",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else

                    MessageBox.Show("Appointment is not  Update yuo have an Error ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            clsApplication application = new clsApplication();

            if(_Mode==enMode.enAdd && _RetakeTestEnable==true) 
              {
                application.ApplicationDate = DateTime.Now;
                application.LastStatusDate = DateTime.Now;
                application.ApplicantPersonID = clsLocalDrivingLicenseApplication
                    .Find(_LocalAppID).ApplicationInfo.PersonInfo.PersonID;
                application.PaidFees = _ReTakeAppFees;
                application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
                application.ApplicationTypeID = 8;
                application.ApplicationStatus = 3;

                application.Save();
               }
              
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalAppID;
            _TestAppointment.AppointmentDate = schudueledTest1.AppointmentDate;
            _TestAppointment.PaidFees = schudueledTest1.TestFees;
            _TestAppointment.CreatedByUserID=clsCurrentUser.CurrentUser.UserID;
            _TestAppointment.TestTypeID = schudueledTest1.TestType;
            _TestAppointment.IsLocked = false;
            _TestAppointment.RetakeTestApplicationID = application.ApplicationID;


            if (_TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Succesfuly", "Save",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblRetakeAppID.Text = application.ApplicationID.ToString();
            }
            else

                MessageBox.Show("Data is not Save you  have an Error ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
