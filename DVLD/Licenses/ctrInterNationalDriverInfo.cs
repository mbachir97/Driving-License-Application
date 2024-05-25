using DVLD.Properties;
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
    public partial class ctrInterNationalDriverInfo : UserControl
    {


        private int _InterNatianalLincenseID = -1;
        clsInternationalLicense _InterNaLicense;
        public ctrInterNationalDriverInfo()
        {
            InitializeComponent();
        }


        void _FillInformation()
        {
           
            lblDriverID.Text = _InterNaLicense.DriverID.ToString();
            lblDateOfBirth.Text = _InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblExpritionDate.Text = _InterNaLicense.ExpirationDate.ToShortDateString();
            lblGendor.Text = (_InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.Gendor== 0) ? "Male" : "Femail";
            lblIssueDate.Text = _InterNaLicense.IssueDate.ToShortDateString();
            lblIsActive.Text = (_InterNaLicense.IsActive == true) ? "Yes" : "No";
            lblAppID.Text = _InterNaLicense.ApplicationID.ToString();
            lblInterLicenseID.Text = _InterNaLicense.InternationalLicenseID.ToString();   

            lblLocalLicenseID.Text = _InterNaLicense.IssuedUsingLocalLicenseID.ToString();
        
            lblNatonalNo.Text = _InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.NationalNO;
            lblName.Text = _InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.FullName();
            pbDriverImage.Image = (_InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.Gendor == 0) ? Resources.Male_512
                : Resources.Female_512;
            if (_InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.ImagePath != "")
                pbDriverImage.Load(_InterNaLicense.LicenseInfo.DriverInfo.PersonInfo.ImagePath);

        }

        public void LoadLicenseInfo(int LincenseID)
        {
            _InterNatianalLincenseID = LincenseID;


            _InterNaLicense = clsInternationalLicense.Find(_InterNatianalLincenseID);

            if (_InterNaLicense == null)
            {
                MessageBox.Show("There is no Lincense with LicenseID= " + _InterNatianalLincenseID, "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            else
                _FillInformation();
        }
        }
    }
