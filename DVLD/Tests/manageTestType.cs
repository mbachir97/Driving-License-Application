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

namespace DVLD
{
    public partial class manageTestType : Form
    {
        public manageTestType()
        {
            InitializeComponent();
        }

        void _Refresh()
        {
            DataTable dataTable = clsTestType.GetAllTestType();
            dgvTestType.DataSource = dataTable;
        }
        private void manageTestType_Load(object sender, EventArgs e)
        {
            DataTable dataTable = clsTestType.GetAllTestType();
            dgvTestType.DataSource = dataTable;
            laNumberOfRecord.Text = laNumberOfRecord.Text + dataTable.Rows.Count.ToString();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTestType manageTestType = new UpdateTestType((int)dgvTestType.CurrentRow.Cells[0].Value);
            manageTestType.ShowDialog();
            _Refresh();
        }
    }
}
