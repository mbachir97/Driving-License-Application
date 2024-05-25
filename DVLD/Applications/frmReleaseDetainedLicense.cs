using DVLD.Global;
using DVLD.Licenses;
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

namespace DVLD.Applications
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _LicenseID;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
           
        }


        public frmReleaseDetainedLicense(int LincenseID)
        {

            InitializeComponent();
            _LicenseID = LincenseID;



            // Instructor Methode 
            ctrDriverLincenseInfoWithFilter1.LoadLicenseInfo(_LicenseID);
            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;


        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            





        }


          //My Methode
        private void btnDetain_Click(object sender, EventArgs e)
        {
        //    clsApplication application = new clsApplication();
        


        //    application.ApplicationStatus = 3;
        //    application.ApplicantPersonID = _License.DriverInfo.PersonID;
        //    application.ApplicationTypeID = clsApplicationType.
        //        Find((int)clsApplication.enApplicationType.enReleaseLicense).ApplicationTypeID;
        //    application.PaidFees = clsApplicationType.
        //        Find((int)clsApplication.enApplicationType.enReleaseLicense).ApplicationFees;
        //    application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
        //    application.ApplicationDate = DateTime.Now;
        //    application.LastStatusDate = DateTime.Now;

        //    if (MessageBox.Show("Are you sure you want to Release this Detained license ", "Confirm",
        //         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {

        //        if(application.Save())
        //        {

        //            _DetainLicense.IsReleased = true;
        //            _DetainLicense.ReleaseDate = DateTime.Now;  
        //            _DetainLicense.ReleaseApplicationID = application.ApplicationID;
        //            _DetainLicense.ReleasedByUserID=clsCurrentUser.CurrentUser.UserID;
        //            if (_DetainLicense.Save())
        //            {
        //                MessageBox.Show("Data Saved Successfuly", "Saved"
        //                    , MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                lblAppID.Text= _DetainLicense.ReleaseApplicationID.ToString();
        //                btnRelease.Enabled= false;  
        //                linkLabelShowIHistory.Enabled= true;
        //                linkLabelShowInfo.Enabled= true;   
        //                ctrDriverLincenseInfoWithFilter1.FilterEnable = false;
        //            }
        //            else
        //                MessageBox.Show("Fail to save Data You have An Error", "Error"
        //                   , MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        }

        //    }


          }

        private void linkLabelShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_LicenseID);
            frm.ShowDialog();
        }

        private void linkLabelShowIHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmshowPersonLicenseHistory frm =new frmshowPersonLicenseHistory
                (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();       
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {    
              
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;


            bool IsReleased = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
                ReleaseDetainedLicense(clsCurrentUser.CurrentUser.UserID, ref ApplicationID); 

            lblAppID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;
            linkLabelShowInfo.Enabled = true;
        }

        private void ctrDriverLincenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected_1(int obj)
        {
            _LicenseID = obj;

            if (_LicenseID == -1)
                return;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            lblLicenseID.Text = _LicenseID.ToString();



            if (!ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseDetained)
            {
                MessageBox.Show("This License is not Detained", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            btnRelease.Enabled = true;

            lblLicenseID.Text = _LicenseID.ToString();
            lblCreatedby.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
               DetainInfo.CreatedByUserID.ToString();
            lblDetainID.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
                DetainInfo.DetainID.ToString();
            lbDetainDate.Text = clsFormat.DateToShort
                (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DetainInfo.DetainDate);
            lblFineFees.Text = ctrDriverLincenseInfoWithFilter1.
                SellectedLicenseInfo.
                DetainInfo.FineFees.ToString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType
                .enReleaseLicense).ApplicationFees.ToString();
            lblTotalFees.Text = (Single.Parse(lblAppFees.Text) + Single.Parse(lblFineFees.Text)).ToString();

        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected_2(int obj)
        {
            _LicenseID = obj;

            if (_LicenseID == -1)
                return;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            lblLicenseID.Text = _LicenseID.ToString();



            if (!ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseDetained)
            {
                MessageBox.Show("This License is not Detained", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            btnRelease.Enabled = true;

            lblLicenseID.Text = _LicenseID.ToString();
            lblCreatedby.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
               DetainInfo.CreatedByUserID.ToString();
            lblDetainID.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
                DetainInfo.DetainID.ToString();
            lbDetainDate.Text = clsFormat.DateToShort
                (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DetainInfo.DetainDate);
            lblFineFees.Text = ctrDriverLincenseInfoWithFilter1.
                SellectedLicenseInfo.
                DetainInfo.FineFees.ToString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType
                .enReleaseLicense).ApplicationFees.ToString();
            lblTotalFees.Text = (Single.Parse(lblAppFees.Text) + Single.Parse(lblFineFees.Text)).ToString();

        }
    }
}
