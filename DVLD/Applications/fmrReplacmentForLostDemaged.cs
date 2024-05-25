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
    public partial class fmrReplacmentForLostDemaged : Form
    {

        private int _LicenseID = -1;

        private int _ReplacmentLicenseID = -1;

        private clsLicense _License;

        private float _AppFeesForDemaged = clsApplicationType.Find((int)clsApplication.
                enApplicationType.enReplacmentForDemaged).ApplicationFees;

        private float _AppFeesForLost = clsApplicationType.Find((int)clsApplication.
                enApplicationType.enReplacmentForLost).ApplicationFees;

        private int _IssueReason = (int)clsLicense.enIssueReason.ReplacementForDamaged;



        public fmrReplacmentForLostDemaged()
        {
            InitializeComponent();
        }

        private void ctrDriverLincenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            if (_LicenseID == -1)
                return;
            

            lblOldlicenseID.Text = _LicenseID.ToString();
            lblCreatedby.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.UserInfo.UserName;
            if (!ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsActive)
            {

                MessageBox.Show("this License is not Active", "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            btnRPlacment.Enabled = true;


        }

        private void fmrReplacmentForLostDemaged_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);

            lblAppFees.Text = _AppFeesForDemaged
                .ToString();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDemaged.Checked)
            {
                lblAppFees.Text = _AppFeesForDemaged.ToString();
                lblTitle.Text = "Replacment for Damaged License";
                _IssueReason = (int)clsLicense.enIssueReason.ReplacementForDamaged;

            }
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLost.Checked)
            {
                lblAppFees.Text = _AppFeesForLost.ToString();
                lblTitle.Text = "Replacment for Lost License";
                _IssueReason = (int)clsLicense.enIssueReason.ReplacementForLost;

            }
        }

        private void btnRPlacment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
               ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.
               Replace((clsLicense.enIssueReason)_IssueReason,
               clsCurrentUser.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblReplaceAppID.Text = NewLicense.ApplicationID.ToString();
            _ReplacmentLicenseID = NewLicense.LicenseID;

            lblReplacmentLicenseID.Text = _ReplacmentLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _ReplacmentLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRPlacment.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;
             linkLabelShowInfo.Enabled = true;



            //clsApplication application = new clsApplication();
            //clsLicense _license = new clsLicense();


            //application.ApplicationStatus = 3;
            //application.ApplicantPersonID = _License.DriverInfo.PersonID;
            //if (rbDemaged.Checked)
            //{
            //    application.ApplicationTypeID = clsApplicationType.
            //  Find((int)clsApplication.enApplicationType.enReplacmentForDemaged).ApplicationTypeID;

            //    application.PaidFees = _AppFeesForDemaged;
            //}



            //if (rbLost.Checked)
            //{
            //    application.ApplicationTypeID = clsApplicationType.
            //        Find((int)clsApplication.enApplicationType.
            //        enReplacmentForLost).ApplicationTypeID;
            //    application.PaidFees = _AppFeesForLost;
            //}




            //application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            //application.ApplicationDate = DateTime.Now;
            //application.LastStatusDate = DateTime.Now;

            //if (MessageBox.Show("Are you sure you want to Replace this license ", "Confirm",
            //     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    if (application.Save())
            //    {
            //        _License.DeActivateLicense();
            //        _license.DriverID = _License.DriverID;
            //        _license.ApplicationID = application.ApplicationID;
            //        _license.IssueDate = DateTime.Now;
            //        _license.ExpirationDate = DateTime.Now.AddYears(_License.ClassInfo.DefaultValidityLength);
            //        _license.LicenseClass = _License.LicenseClass;
            //        _license.IssueReason = _IssueReason;
            //        _license.IsActive = true;
            //        _license.PaidFees = _License.PaidFees;
            //        _license.Notes = _License.Notes;
            //        _license.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;

            //        if (_license.Save())
            //        {
            //            _ReplacmentLicenseID = _license.LicenseID;
            //            lblReplacmentLicenseID.Text = _license.LicenseID.ToString();
            //            lblReplaceAppID.Text = _license.ApplicationID.ToString();
            //            MessageBox.Show("license is  Replaced Succefuly with ID = " + _ReplacmentLicenseID.ToString()
            //            , "Renew",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information); 
            //            lblReplaceAppID.Text = _License.ApplicationID.ToString();   

            //            linkLabelShowInfo.Enabled = true;

            //            btnRPlacment.Enabled = false;

            //            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;

            //            linkLabelShowIHistory.Enabled = true;
            //        }
            //        else
            //        {
            //            MessageBox.Show("license was not renew you have an error ", "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            //        }

            //    }
            //}
        }

        private void linkLabelShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_ReplacmentLicenseID);

            frm.ShowDialog();   
        }

        private void linkLabelShowIHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmshowPersonLicenseHistory frm = new frmshowPersonLicenseHistory(ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();       
        }
    }
}
