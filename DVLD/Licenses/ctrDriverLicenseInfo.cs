using DVLD.Properties;
using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrDriverLicenseInfo : UserControl
    {
        private int _LincenseID = -1;

        private clsLicense _Lincense;
        public ctrDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LincenseID
        {
            get { return _LincenseID; }
        }
            
        public clsLicense SellectedLicenseInfo
        {
            get { return  _Lincense; }  
        }
        

        string _ReturnIssueReason(int IssueReason)
        {
            switch (IssueReason) {
                case 1:
                    return "First Time";

                case 2:
                    return "Renew";
                case 3:
                    return "Replacment For Demaged";
                case 4:
                    return "Replacment For Lost";

                default:
                    return "";
            }

        }

        void _FillInformation()
        {


            lblClassName.Text= _Lincense.ClassInfo.ClassName;
            lblDriverID.Text=_Lincense.DriverID.ToString();
            lblDateOfBirth.Text=_Lincense.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblExpritionDate.Text=_Lincense.ExpirationDate.ToShortDateString() ;    
            lblGendor.Text=(_Lincense.DriverInfo.PersonInfo.Gendor==0)?"Male":"Femail";
            lblIssueDate.Text=_Lincense.IssueDate.ToShortDateString() ;
            lblIsActive.Text=(_Lincense.IsActive==true)?"Yes":"No";
            //  lblIssueReason.Text = _ReturnIssueReason(_Lincense.IssueReason);
            lblIssueReason.Text = _Lincense.IssueReasonText;
            lblLicenseID.Text= _Lincense.LicenseID.ToString();
            // lblIsDetained.Text = clsDetainedLicense.IsLicenseDetained(_LincenseID) ?"Yes":"No";
            lblIsDetained.Text = _Lincense.IsLicenseDetained ? "Yes" : "No";
            lblNote.Text =(_Lincense.Notes=="")?"No Note":_Lincense.Notes;
            lblNatonalNo.Text=_Lincense.DriverInfo.PersonInfo.NationalNO;
            lblName.Text = _Lincense.DriverInfo.PersonInfo.FullName();
            pbDriverImage.Image=(_Lincense.DriverInfo.PersonInfo.Gendor==0)?Resources.Male_512
                : Resources.Female_512;   
            if( _Lincense.DriverInfo.PersonInfo.ImagePath!="")
                if(File.Exists(_Lincense.DriverInfo.PersonInfo.ImagePath))
                pbDriverImage.Load(_Lincense.DriverInfo.PersonInfo.ImagePath);

        }

        public void ReseteValue()
        {
            lblClassName.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblExpritionDate.Text = "[???]";
            lblGendor.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIsActive.Text = "[???]";
            //  lblIssueReason.Tex
            lblIssueReason.Text = "[???]";
            lblLicenseID.Text = "[???]";
            // lblIsDetained.Text  if (_Lincense == null)
            lblIsDetained.Text = "[???]";
            lblNote.Text = "[???]";
            lblNatonalNo.Text = "[???]";
            lblName.Text = "[???]";

            return;
        }

        public void LoadLicenseInfo(int LincenseID)
        {
            _LincenseID=LincenseID;


            _Lincense = clsLicense.Find(_LincenseID);

            if (_Lincense == null)
            {
                ReseteValue();
                _LincenseID = -1;
                MessageBox.Show("There is no Lincense with LicenseID= " + _LincenseID, "Error" 
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _FillInformation();

        }
    }
}
