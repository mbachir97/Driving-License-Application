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
using DVLD_Bisness;
using DVLD.Licenses;

namespace DVLD.Drivers
{
    public partial class ctrDriverLicense : UserControl
    {


        private int _DriverID = -1;
        public ctrDriverLicense()
        {
            InitializeComponent();
           
        }

        public void LoadLocalInformation(int DriverID)
        {

          
            _DriverID = DriverID;

            DataTable dt = clsLicense.GetDriverLicenses(_DriverID);

            if(0 == dt.Rows.Count) {

                return;
            }
            dgvLocalLicense.DataSource = dt;

         
            dgvLocalLicense.Columns[0].HeaderText = "L.LicenID";
            dgvLocalLicense.Columns[1].HeaderText = "AppID";          

            lblRecordsCount.Text= dgvLocalLicense.Rows.Count.ToString();
        }



        public void LoadInternationalInformation(int DriverID)
        {


            _DriverID = DriverID;

           
       
            DataTable _AllInterLicense = clsInternationalLicense.GetAllInternationalLicense(_DriverID);
            if(0 == _AllInterLicense.Rows.Count)
            {
                return;
            }
            dgvInterNational.DataSource = _AllInterLicense.DefaultView.
                ToTable(false, "InternationalLicenseID", "ApplicationID", "IssuedUsingLocalLicenseID"
                , "IssueDate", "ExpirationDate", "IsActive");
           
            dgvInterNational.Columns[0].HeaderText = "Int.LicenseID";

            dgvInterNational.Columns[2].HeaderText = "L.LicenseID";

            lblRecordsCount.Text = dgvLocalLicense.Rows.Count.ToString();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense frm = new frmShowLicense((int)dgvLocalLicense.CurrentRow.Cells[0]
                .Value);

            frm.ShowDialog();   
        }
    }
}
