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

namespace DVLD.Licenses
{
    public partial class frmNewInternationalLicenseApp : Form
    {
        public frmNewInternationalLicenseApp()
        {
            InitializeComponent();
        }

        clsLicense _License;
        private int _LicenseID = -1;

        private int _InterNationalID=-1;    
        private void ctrDriverLincenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID=obj;

            linkLabelShowIHistory.Enabled = (_LicenseID != -1);
            if (_LicenseID == -1)
            {
                return;
            }

           
            lblLocalLincenseID.Text = _LicenseID.ToString();
            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsActive==false)
            {
                MessageBox.Show("this License is not Active", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("the Lincense must be from Class 3", "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseExpired())
            {

                 MessageBox.Show("the License is Expired", "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.IsLicenseDetained)
            {
                MessageBox.Show("the Licens is Detained Releas it First", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInterNationalId = clsInternationalLicense.
                GetActiveInternationalLicenseIDByDriverID(ctrDriverLincenseInfoWithFilter1.
                SellectedLicenseInfo.DriverInfo.DriverID);

            if (ActiveInterNationalId != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInterNationalId.ToString(), 
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                linkLabelShowInfo.Enabled = true;
                _InterNationalID = ActiveInterNationalId;
                btnIssue.Enabled = false;
                return;
            }


            btnIssue.Enabled = true;


        }

        private void ctrDriverLincenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void frmNewInternationalLicenseApp_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text=clsFormat.DateToShort(DateTime.Now);
           lblFees.Text=clsApplicationType.Find((int)clsApplication.enApplicationType.enInternationalApp).ApplicationFees.ToString();
            lblExpDate.Text=clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedby.Text = clsCurrentUser.CurrentUser.UserName;
           

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            //clsInternationalLicense internationalLicense = clsInternationalLicense.FindByDriverID(_License.DriverID);
            //if (internationalLicense != null) {
            
            //   MessageBox.Show("Person Already have An Active InterNationalLicense " +
            //       "with ID ="+ internationalLicense.InternationalLicenseID.ToString(), "Error", MessageBoxButtons.OK,
            //       MessageBoxIcon.Error);

            //    return;
            
            //}

            clsInternationalLicense internationalLicense=new clsInternationalLicense();

            clsApplication App = new clsApplication();

            App.ApplicantPersonID = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo
                .DriverInfo.PersonID;
            App.ApplicationTypeID = clsApplicationType.Find((int)clsApplication.
            enApplicationType.enInternationalApp).ApplicationTypeID;
            App.ApplicationStatus = 3;
            App.ApplicationDate = DateTime.Now;     
            App.LastStatusDate = DateTime.Now;  
            App.CreatedByUserID=clsCurrentUser.CurrentUser.UserID;
            App.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType
                .enInternationalApp).ApplicationFees;

            if (MessageBox.Show("Are you sure you want to issue this license ", "Confirm",
                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (App.Save())
                {
                    internationalLicense.ApplicationID = App.ApplicationID;
                    internationalLicense.DriverID = ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DriverID;
              ; ;
                    internationalLicense.IssuedUsingLocalLicenseID = _LicenseID;
                    internationalLicense.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
                    internationalLicense.IssueDate = DateTime.Now;
                    internationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
                    internationalLicense.IsActive = true;


                    if (internationalLicense.Save())
                    {
                        MessageBox.Show("International License Issued Succefuly with LicenseID = " +
                            internationalLicense.InternationalLicenseID.ToString(), "Lincense Issued",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _InterNationalID = internationalLicense.InternationalLicenseID;
                        lblnternationalLicenseID.Text = internationalLicense.InternationalLicenseID.ToString();
                        ILAppID.Text = internationalLicense.ApplicationID.ToString();
                        linkLabelShowInfo.Enabled = true;
                        btnIssue.Enabled = false;
                    }
                    else

                        MessageBox.Show(" you have An Error  ", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);







                }




            }



            




        }

        private void linkLabelShowInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInterNationalInfo frm = new frmShowInterNationalInfo(_InterNationalID);
            frm.ShowDialog();   
        }

        private void linkLabelShowIHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmshowPersonLicenseHistory frm = new frmshowPersonLicenseHistory(ctrDriverLincenseInfoWithFilter1.SellectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();   
        }
    }
}
