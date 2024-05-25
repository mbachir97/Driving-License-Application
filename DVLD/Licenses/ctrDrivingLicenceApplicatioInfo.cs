using DVLD.Tests;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Licenses
{
    public partial class ctrDrivingLicenceApplicatioInfo : UserControl
    {
        public ctrDrivingLicenceApplicatioInfo()
        {
            InitializeComponent();
        }
       
        private int _LocalApp=-1;

        private int _LicenseID = -1;


        public int LocalDrivingApp
        {
            get { return _LocalApp; }
        }

       private clsLocalDrivingLicenseApplication _LocalDrivingApp ;

        private clsLicense _Lincense;
       //public  enum enTestMode { enVission = 1, enWritten = 2, enPractical = 3 }

       // enTestMode _TestMode = enTestMode.enVission;


        public void LoadLocalApplicationInfo(int LocalAppID)
        {
            _LocalApp = LocalAppID;

            _LocalDrivingApp = clsLocalDrivingLicenseApplication.FindByLocalAppID(_LocalApp);
            if (_LocalDrivingApp == null) {
                MessageBox.Show("there is no Application With LocalApplicationID "
                     + LocalAppID.ToString(), "Not Found", MessageBoxButtons.OK
                     , MessageBoxIcon.Error);
                ctrApplicationBasicInfo1._ResetValue();
                lbAppID.Text = "[????]";
                lblTitleClass.Text = "[???]";



                return;

            }
            else
            {

                _FillInformation();
            }

        }

        void _FillInformation()
        {
            _LicenseID=_LocalDrivingApp.GetActiveLicenseID();       

            linkLabel1.Enabled= (_LicenseID != -1);

            ctrApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingApp.ApplicationID);
            lbAppID.Text = _LocalDrivingApp.LocalDrivingLicenseApplicationID.ToString();
            lblTitleClass.Text = _LocalDrivingApp.LicenseClassInfo.ClassName;
            // lblTestPass.Text = clsLocalDrivingLicenseApplication.GetTestPass(_LocalApp).ToString() + "/3";
            lblTestPass.Text = _LocalDrivingApp.GetPassedTestCount().ToString();

           
           





        }

        private void ctrDrivingLicenceApplicatioInfo_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_LicenseID);
            frm.ShowDialog();   
        }
    }
}
