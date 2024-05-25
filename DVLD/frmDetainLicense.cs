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


namespace DVLD
{
    public partial class frmDetainLicense : Form
    {

        private int _LicenseID = -1;

        private int _DetainID = -1;

        private clsLicense _License;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            if (_LicenseID == -1)
                return;
            _License = clsLicense.Find(_LicenseID);

            lblLicenseID.Text=_LicenseID.ToString();

            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseDetained)
            {
                MessageBox.Show("Selected License Already Detained Choose Another One"
                    ,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!_License.IsActive)
            {
                MessageBox.Show("This License is not Active , Select An Active One "
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            btnDetain.Enabled = true;
            txtFineFees.Focus();    
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lbDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedby.Text = clsCurrentUser.CurrentUser.UserName;


        }

        private void btnDetain_Click(object sender, EventArgs e)
        {


            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _DetainID = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), clsCurrentUser.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrDriverLincenseInfoWithFilter1.FilterEnable = false;
            txtFineFees.Enabled = false;
            linkLabelShowInfo.Enabled = true;

            //clsDetainedLicense DetainedLicense = new clsDetainedLicense();

            //DetainedLicense.LicenseID = _LicenseID; 
            //DetainedLicense.CreatedByUserID=clsCurrentUser.CurrentUser.UserID;
            //DetainedLicense.DetainDate = DateTime.Now;
            //DetainedLicense.FineFees = Single.Parse(txtFineFees.Text);
            //if (MessageBox.Show("Are you sure you want to Detain this license ", "Confirm",
            //    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    if (DetainedLicense.Save())
            //    {
            //        MessageBox.Show("license Detained Succefuly with ID = " + DetainedLicense.DetainID.ToString()
            //     , "Renew",
            //     MessageBoxButtons.OK, MessageBoxIcon.Information); 
            //        btnDetain.Enabled=false;
            //        linkLabelShowIHistory.Enabled=true;
            //        linkLabelShowInfo.Enabled=true;
            //        ctrDriverLincenseInfoWithFilter1.FilterEnable = false;

            //    }
            //    else
            //        MessageBox.Show("License is not detained Succefuly "
            //       , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
        }

        private void linkLabelShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(_LicenseID);
            frm.ShowDialog();
        }

        private void linkLabelShowIHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmshowPersonLicenseHistory frm =new 
                frmshowPersonLicenseHistory(_License.DriverInfo.PersonID);  
            frm.ShowDialog();   
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {

                e.Cancel = true;
                txtFineFees.Focus();
                errorProvider1.SetError(txtFineFees, "you must Enter the Fees");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, "");

            }
        }
    }
}
