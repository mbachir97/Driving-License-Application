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
using DVLD_Bisness;
using DVLD.Properties;

namespace DVLD.Tests
{
    public partial class SchudueledTest : UserControl
    {

        clsLocalDrivingLicenseApplication _LocalLicenceApp;
        public SchudueledTest()
        {
            InitializeComponent();
        }


        private int  _LocalApp=-1;

        private bool _IsLockedLableVisible=false;

        public bool IsLockedLableVisible
        {
            get { return _IsLockedLableVisible; }

            set { _IsLockedLableVisible = value;
            
            lblIsLocked.Visible = _IsLockedLableVisible;    
            }
        }

        private int _TestType = -1;

        private string _Title;

        public string Title
        {

            get { return _Title; }  

            set { _Title = value;
            lblTestTitle.Text = _Title; 
            
            }
        }

        public int TestType
        {
            get {return _TestType;} 
        }

            


        private bool _IsDateTimePickerEnable = true;

       

       public  DateTime AppointmentDate
        {
            get
            {
                return dateTimePicker1.Value;

            }
        }

        public bool IsDateTimePickerEnable
        {
            get {return _IsDateTimePickerEnable;
             }


            set {
                _IsDateTimePickerEnable = value;
            
            dateTimePicker1.Enabled = _IsDateTimePickerEnable;
            }
        }

       public float   TestFees
        {
            get { return  Convert.ToSingle( lblFees.Text); }
        }


        void _InitialValue()
        {
            switch (frmListTestApointment._TestMode)
            {
                case frmListTestApointment.enTestMode.enVission:
                    pbTestPicture.Image = Resources.Vision_512;
                    _TestType = 1;
                    lblFees.Text=clsTestType.Find(1).TestTypeFees.ToString();
                    break;

                case frmListTestApointment.enTestMode.enWritten:
                    pbTestPicture.Image = Resources.Written_Test_512;
                    _TestType = 2;
                    lblFees.Text = clsTestType.Find(2).TestTypeFees.ToString();
                    break;

                case frmListTestApointment.enTestMode.enPractical:
                    pbTestPicture.Image = Resources.driving_test_512;
                    _TestType=3;    
                    lblFees.Text = clsTestType.Find(3).TestTypeFees.ToString();
                    break;


            }
        }
        void _FillInformation()
        {
            _InitialValue();
            lblID.Text=_LocalLicenceApp.LocalDrivingLicenseApplicationID.ToString();
            lblClass.Text = _LocalLicenceApp.LicenseClassInfo.ClassName;
            lblName.Text = _LocalLicenceApp.ApplicationInfo.PersonInfo.FullName();
            lblTrial.Text = clsTestAppointment.ReturnTrial(_TestType, _LocalApp).ToString();
           lblTestTitle.Text = (lblTrial.Text == "0") ? "Schudueled Test " : "Schudueled Retake Test";
           
        }

        public void LoadLocalAppInformation(int LocalApp)
        {
            _LocalApp= LocalApp;    
            _LocalLicenceApp = clsLocalDrivingLicenseApplication.Find(LocalApp);

            if (_LocalLicenceApp == null)
            {
                MessageBox.Show("this LocalApp Is not found ","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);    

                return;

            }
            else
            {
                _FillInformation();
                dateTimePicker1.Enabled = true;

            }

        }




    }
}
