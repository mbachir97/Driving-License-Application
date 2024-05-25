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

namespace DVLD
{
    public partial class manageAplicationTypes : Form
    {
        public manageAplicationTypes()
        {
            InitializeComponent();
        }

        void _Refresh()
        {
            DataTable dataTable = clsApplicationType.GetAllApplicationType();
            dgvApplication.DataSource = dataTable;
        }
        private void manageAplicationTypes_Load(object sender, EventArgs e)
        {
            DataTable dataTable = clsApplicationType.GetAllApplicationType();
            dgvApplication.DataSource = dataTable;
            laNumberOfRecord.Text = laNumberOfRecord.Text + dataTable.Rows.Count.ToString();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationType updateApplicationType = new UpdateApplicationType((int)dgvApplication.CurrentRow.Cells[0].Value);  
            updateApplicationType.ShowDialog();
            _Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
