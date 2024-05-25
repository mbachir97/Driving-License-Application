using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global;
using DVLD_Bisness;

namespace DVLD.Licenses
{
    public partial class frmIssueLincense : Form
    {
        private int _LocalAppID = -1;
        

        private clsLocalDrivingLicenseApplication _LocalAppLincense;


        public frmIssueLincense(int LocalLincenseID)
        {
            _LocalAppID = LocalLincenseID; 

            InitializeComponent();
        }

        private void IssueLincense_Load(object sender, EventArgs e)
        {
            _LocalAppLincense = clsLocalDrivingLicenseApplication.FindByLocalAppID(_LocalAppID);

            if (_LocalAppLincense == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalAppID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LocalAppLincense.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalAppLincense.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrDrivingLicenceApplicatioInfo1.LoadLocalApplicationInfo(_LocalAppID);

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            txtNote.Focus();


            int LicenseID = _LocalAppLincense.IssueLicenseForTheFirtTime(txtNote.Text.Trim(),clsCurrentUser.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //clsDriver Driver1 = clsDriver.FindByPerson(_LocalAppLincense.ApplicationInfo
            //    .PersonInfo.PersonID);



            //clsLicense License =new clsLicense();

            //if(Driver1== null) {

            //    Driver1=new clsDriver();
            //    Driver1.PersonID = _LocalAppLincense.ApplicationInfo.PersonInfo.PersonID;
            //    Driver1.CreatedDate = DateTime.Now;
            //    Driver1.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;


            //}



            //if (Driver1.Save())
            //{
            //    License.DriverID=Driver1.DriverID;
            //    License.ApplicationID = _LocalAppLincense.ApplicationInfo.ApplicationID;
            //    License.IssueDate=DateTime.Now;
            //    License.ExpirationDate = License.IssueDate.AddYears(_LocalAppLincense.LicenseClassInfo.DefaultValidityLength);
            //    License.IssueReason = 1;
            //    License.LicenseClass = _LocalAppLincense.LicenseClassID;
            //    License.Notes = textBox1.Text;
            //    License.IsActive= true; 
            //    License.CreatedByUserID=clsCurrentUser.CurrentUser.UserID;
            //    License.PaidFees = _LocalAppLincense.LicenseClassInfo.ClassFees;

            //    if (License.Save())
            //    {

            //        _LocalAppLincense.ApplicationInfo.CompleteAppication();
            //        MessageBox.Show("License Issued Succefuly with LicenseID = " + License.LicenseID,
            //            "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }

            //    else
            //    {
            //        MessageBox.Show("You Have An Error",
            //          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    }
        }



        
    }
}
