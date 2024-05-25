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
    public partial class frmRenewLicense : Form
    {

        clsLicense _License;
        private int _LicenseID = -1;

        private int _NewLicenseID = -1;  
        public frmRenewLicense()
        {
            InitializeComponent();
        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            if (_LicenseID == -1)
                return;

            //My Old Methode
            //clsLicense _License = clsLicense.Find(_LicenseID);
            lblOldlicenseID.Text = _LicenseID.ToString();
            lblLicenFees.Text = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.ClassInfo.ClassFees.ToString();
            lblAppFees.Text= clsApplicationType.
                Find((int)clsApplication.enApplicationType.enRenew).ApplicationFees.ToString();
            lblExpDate.Text = clsFormat.DateToShort(ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.ExpirationDate);

            lblTotalFees.Text= (Convert.ToSingle(lblLicenFees.Text)+ Convert.ToSingle(lblAppFees.Text)).ToString();
            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsActive == false)
            {
                MessageBox.Show("this License is not Active", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseExpired())
            {

                MessageBox.Show("Selected License is not yet Expired , it will Expire On "+
                    clsFormat.DateToShort(ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.ExpirationDate),"Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);    return; 
            }

            btnRenue.Enabled = true;

        





        }

        private void btnRenue_Click(object sender, EventArgs e)
        {


            clsLicense NewLicense =
             ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.RenewLicense(txtNote.Text.Trim(),
             clsCurrentUser.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblRenewAppID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblNewLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenue.Enabled = false;
            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;
            linkLabelShowInfo.Enabled = true;
      
            //clsApplication application = new clsApplication();
            //clsLicense _license =new clsLicense();


            //application.ApplicationStatus = 3;
            //application.ApplicantPersonID = _License.DriverInfo.PersonID;
            //application.ApplicationTypeID = clsApplicationType.
            //    Find((int)clsApplication.enApplicationType.enRenew).ApplicationTypeID;
            //application.PaidFees = clsApplicationType.
            //    Find((int)clsApplication.enApplicationType.enRenew).ApplicationFees;
            //application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            //application.ApplicationDate = DateTime.Now;
            //application.LastStatusDate = DateTime.Now;

            //if (MessageBox.Show("Are you sure you want to Renew this license ", "Confirm",
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
            //        _license.IssueReason = (int)clsLicense.enIssueReason.Renew;
            //        _license.IsActive = true;
            //        _license.PaidFees = _License.PaidFees;
            //        _license.Notes = txtNote.Text;
            //        _license.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;

            //        if (_license.Save())
            //        {
            //            _NewLicenseID = _license.LicenseID;
            //            lblNewLicenseID.Text=_license.LicenseID.ToString(); 
            //            lblRenewAppID.Text=_license.ApplicationID.ToString();
            //            MessageBox.Show("license Renew Succefuly with ID = "+ _NewLicenseID.ToString()
            //            , "Renew",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information); 

            //            lblRenewAppID.Text = _license.ApplicationID.ToString();

            //            linkLabelShowInfo.Enabled=true;

            //            btnRenue.Enabled = false;
            //        }
            //        else
            //        {
            //            MessageBox.Show("license was not renew you have an error " , "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            //        }

            //    }

            //}
        }

        private void linkLabelShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_NewLicenseID);
            frm.ShowDialog();   
        }

        private void linkLabelShowIHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmshowPersonLicenseHistory frm = new frmshowPersonLicenseHistory(ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            lblIssueDate.Text = clsFormat.DateToShort( DateTime.Now);
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
        }
    }
}
