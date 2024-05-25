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
    public partial class frmLocalAppInfo : Form
    {

        private int _LocalAppID = -1;
        public frmLocalAppInfo(int LocalAppID)
        {
            _LocalAppID = LocalAppID;   
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalAppInfo_Load(object sender, EventArgs e)
        {
            ctrDrivingLicenceApplicatioInfo1.LoadLocalApplicationInfo(_LocalAppID);
        }
    }
}
