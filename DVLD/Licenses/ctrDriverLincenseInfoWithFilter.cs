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

namespace DVLD.Licenses
{
    public partial class ctrDriverLincenseInfoWithFilter : UserControl
    {

        private int _LicenseID = -1;

        private bool _FilterEnable=true;

        public int  LicenseID
        {
            get
            {
                return ctrDriverLicenseInfo1.LincenseID;  
            }
        }

        public clsLicense SellectedLicenseInfo
        {
            get
            {
                return ctrDriverLicenseInfo1.SellectedLicenseInfo;
            }
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        public void LoadLicenseInfo(int LicenseID)
        {


            txtLicenseID.Text = LicenseID.ToString();
            ctrDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            _LicenseID = ctrDriverLicenseInfo1.LincenseID;
            if (OnLicenseSelected != null && FilterEnable==true)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);


        }

        public bool FilterEnable
        {
            get { return _FilterEnable; }   

            set {
                _FilterEnable = value;
                gbFilter.Enabled = value;
                   
                     }
        }
        public ctrDriverLincenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            
        }


        public event Action<int> OnLicenseSelected;

        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnLicenseSelected;

            if (handler != null)
            {
                handler(personID);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = Convert.ToInt32(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
           

        }

        private void Find()
        {
            //if (string.IsNullOrEmpty(textBox1.Text))
            //{

            //    MessageBox.Show("Enter LincenseID First ", "Information"
            //        , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            _LicenseID = Convert.ToInt32(txtLicenseID.Text);
            if (clsLicense.IsLicenseExist(_LicenseID))
            {
                ctrDriverLicenseInfo1.LoadLicenseInfo(_LicenseID);
                PersonSelected(_LicenseID);

            }
            else
                MessageBox.Show("this License is not Exist", "Error"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public void LoadInfo(int LincenseID)
        {
            
            FilterEnable = false;
            txtLicenseID.Text = LincenseID.ToString();  
            Find();

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if( string.IsNullOrEmpty(txtLicenseID.Text))
            {
                e.Cancel= true;
                errorProvider1.SetError(txtLicenseID, "This field is requered");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID,null);
            }

        }
    }
}
