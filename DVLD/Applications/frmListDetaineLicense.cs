using DVLD.Licenses;
using DVLD.People;
using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmListDetaineLicense : Form
    {
        DataTable _dtLicense;    

        public frmListDetaineLicense()
        {
            InitializeComponent();
        }

        private void frmListDetaineLicense_Load(object sender, EventArgs e)
        {
            _dtLicense=clsDetainedLicense.GetAllDetainedLicense();  
            dgvDetainLicense.DataSource = _dtLicense;

            dgvDetainLicense.Columns[1].HeaderText = "L.ID";
            dgvDetainLicense.Columns[0].HeaderText = "D.ID";
            dgvDetainLicense.Columns[2].HeaderText = "D.Date";
            dgvDetainLicense.Columns[3].HeaderText = "Is Released";
            dgvDetainLicense.Columns[6].HeaderText = "N.No";
            dgvDetainLicense.Columns[8].HeaderText = "Releaes AppID";


            lblRecordsCount.Text = _dtLicense.Rows.Count.ToString();





        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (cbxFindBy.Text.ToLower() == "detainid" || cbxFindBy.Text.ToLower() == "licenseid")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";
            switch (cbxFindBy.Text.ToLower())
            {

                case "detainid":
                    FilterColumn = "DetainID";
                    break;
                case "licenseid":
                    FilterColumn = "LicenseID";
                    break;
                case "fullname":
                    FilterColumn = "FullName";
                    break;
                case "nationalno":
                    FilterColumn = "NationalNo";
                    break;

                default:
                    FilterColumn = "None";
                    break;



            }


            if (FilterColumn == "None" || txtFilter.Text == "")
            {
                _dtLicense.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDetainLicense.Rows.Count.
                    ToString();

                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "LicenseID")
                _dtLicense.DefaultView.RowFilter = string.Format("{0} = {1} ", FilterColumn, txtFilter.Text.Trim());

            else
                _dtLicense.DefaultView.RowFilter = string.Format("{0} like '{1}%' ", FilterColumn, txtFilter.Text.Trim());



            lblRecordsCount.Text = dgvDetainLicense.Rows.Count.ToString();
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = (!(bool)dgvDetainLicense.CurrentRow
                .Cells[3].Value)?true:false;
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {


            PersonDetails Person = new PersonDetails(clsLicense.Find((int)dgvDetainLicense
                .CurrentRow.Cells[1].Value).DriverInfo.PersonID);

            Person.ShowDialog();    
        }

        private void showLicenseDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicense frm = new frmShowLicense(clsLicense.Find((int)dgvDetainLicense
                .CurrentRow.Cells[1].Value).LicenseID);
            frm.ShowDialog();   
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmshowPersonLicenseHistory frm = new frmshowPersonLicenseHistory(clsLicense.
                Find((int)dgvDetainLicense
                .CurrentRow.Cells[1].Value).DriverInfo.PersonID);
            frm.ShowDialog();   
        }

        void _Refresh()
        {
            _dtLicense = clsDetainedLicense.GetAllDetainedLicense();
            dgvDetainLicense.DataSource = _dtLicense;
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dgvDetainLicense
                .CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            _Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();  
            frm.ShowDialog();
            _Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
                frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
                frm.ShowDialog();
                _Refresh();
           
              
           
        }
    }
}
