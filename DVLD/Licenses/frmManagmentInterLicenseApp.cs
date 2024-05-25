using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People;
using DVLD_Bisness;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Licenses
{
    public partial class frmManagmentInterLicenseApp : Form
    {

        DataTable AllInterNational;
        public frmManagmentInterLicenseApp()
        {
            InitializeComponent();
        }

        void _Refresh()
        {
            AllInterNational = clsInternationalLicense.GetAllInternationalLicense()
               .DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID", "DriverID", "IssuedUsingLocalLicenseID",
               "IssueDate", "ExpirationDate", "IsActive");
            //dgvListInternationalApp.DataSource = AllInterNational;
        }

        private void dgvListLocalApp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmManagmentInterLicenseApp_Load(object sender, EventArgs e)
        {
             AllInterNational= clsInternationalLicense.GetAllInternationalLicense()
                .DefaultView.ToTable(false, "InternationalLicenseID", "ApplicationID", "DriverID", "IssuedUsingLocalLicenseID", 
                "IssueDate", "ExpirationDate", "IsActive");
            dgvListInternationalApp.DataSource = AllInterNational;
            dgvListInternationalApp.Columns[3].HeaderText = "L.LicenseID";
            dgvListInternationalApp.Columns[0].HeaderText = "IL.LicenseID";

            lblRecordsCount.Text=dgvListInternationalApp.Rows.Count.ToString();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbxFindBy.Text.ToLower())
            {

                case "driverid":
                    FilterColumn = "DriverID";
                    break;
                case "applicationid":
                    FilterColumn = "ApplicationID";
                    break;
                case "illicenseid":
                    FilterColumn = "InternationalLicenseID";
                    break;
             

                default:
                    FilterColumn = "None";
                    break;



            }


            if (FilterColumn == "None" || txtFilter.Text == "")
            {
                AllInterNational.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListInternationalApp.Rows.Count.
                    ToString();

                return;
            }

           
                AllInterNational.DefaultView.RowFilter = string.Format("{0} = {1} ", FilterColumn, txtFilter.Text.Trim());




            lblRecordsCount.Text = dgvListInternationalApp.Rows.Count.ToString();
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApp frm = new frmNewInternationalLicenseApp();
            frm.ShowDialog();
            _Refresh();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Personid = clsDriver.Find((int)dgvListInternationalApp.CurrentRow
                .Cells[2].Value).PersonInfo.PersonID;

            PersonDetails frm = new PersonDetails(Personid);    
            frm.ShowDialog();
        }

        private void edditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInterNationalInfo frm = new frmShowInterNationalInfo((int)dgvListInternationalApp.CurrentRow
                .Cells[0].Value);
            frm.ShowDialog();   
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Personid = clsDriver.Find((int)dgvListInternationalApp.CurrentRow
               .Cells[2].Value).PersonInfo.PersonID;

            frmshowPersonLicenseHistory frm = new frmshowPersonLicenseHistory(Personid);
            frm.ShowDialog();   
        }
    }
}
