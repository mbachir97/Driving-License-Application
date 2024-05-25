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

namespace DVLD.Drivers
{
    public partial class frmDriversList : Form
    {

        DataTable _AllDrivers;
        public frmDriversList()
        {
            InitializeComponent();
        }

        private void frmDriversList_Load(object sender, EventArgs e)
        {
            _AllDrivers = clsDriver.GetAllDriver();
            dgvDrivers.DataSource = _AllDrivers;

            dgvDrivers.Columns[0].HeaderText = "DriverID";
            dgvDrivers.Columns[1].HeaderText = "PersonID";
            dgvDrivers.Columns[2].HeaderText = "NationalNo";
            dgvDrivers.Columns[3].HeaderText = "FullName";
            dgvDrivers.Columns[4].HeaderText = "Date";
            dgvDrivers.Columns[5].HeaderText = "ActiveLicenses";

            lblRecordsCount.Text=dgvDrivers.Rows.Count.ToString();  


        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = "";
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbxFindBy.Text.ToLower()) {

                case "driverid":
                    FilterColumn = "DriverID";
                    break;
                case "personid":
                    FilterColumn = "PersonID";
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
                _AllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text=dgvDrivers.Rows.Count.
                    ToString();

                return;
            }

            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                _AllDrivers.DefaultView.RowFilter = string.Format("{0} = {1} ", FilterColumn, txtFilter.Text.Trim());

            else
                _AllDrivers.DefaultView.RowFilter = string.Format("{0} like '{1}%' ", FilterColumn, txtFilter.Text.Trim());



            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();    
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (cbxFindBy.Text.ToLower() == "personid" || cbxFindBy.Text.ToLower()=="driverid")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
