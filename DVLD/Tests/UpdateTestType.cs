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
    public partial class UpdateTestType : Form
    {
        int _TestTypeID = -1;
        clsTestType _TestType;
        public UpdateTestType(int TestTypeId)
        {
            _TestTypeID = TestTypeId;
            _TestType = clsTestType.Find(_TestTypeID);
            InitializeComponent();
        }

        void _LodInformation()
        {
            lableID.Text = _TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle.ToString();
            txtFees.Text = _TestType.TestTypeFees.ToString();
            txtDiscription.Text= _TestType.TestTypeDescription.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestType clsTestType = clsTestType.Find(_TestTypeID); ;
            clsTestType.TestTypeTitle = txtTitle.Text;
            clsTestType.TestTypeFees = Convert.ToSingle(txtFees.Text);
            clsTestType.TestTypeDescription=txtDiscription.Text;

            if (clsTestType.Save())
            {
                MessageBox.Show("Data Saved Succefuly ", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("fail to Save Data  ", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
        }

        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            _LodInformation();  
        }
    }
}
