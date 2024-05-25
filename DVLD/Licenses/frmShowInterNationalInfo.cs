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
    public partial class frmShowInterNationalInfo : Form
    {
        private int _InterNationalLicenseID = -1;

        public frmShowInterNationalInfo(int interNationalLicenseID)
        {
            InitializeComponent();
            _InterNationalLicenseID = interNationalLicenseID;   
        }

        private void frmShowInterNationalInfo_Load(object sender, EventArgs e)
        {
            ctrInterNationalDriverInfo1.LoadLicenseInfo(_InterNationalLicenseID);
        }
    }
}
