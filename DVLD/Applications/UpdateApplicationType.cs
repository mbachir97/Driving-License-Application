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
    public partial class UpdateApplicationType : Form
    {
        int _ApplicationTypeID=-1;  
        clsApplicationType _ApplicationType;    
        public UpdateApplicationType(int ApplicationTypeID)
        {
            _ApplicationTypeID=ApplicationTypeID;
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            InitializeComponent();
        }

        void _LodInformation()
        {
            lableID.Text = _ApplicationTypeID.ToString();   
            txtTitle.Text=_ApplicationType.ApplicationTypeTitle.ToString(); 
            txtFees.Text=_ApplicationType.ApplicationFees.ToString();
        }

        private void UpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LodInformation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplicationType clsApplicationType = clsApplicationType.Find(_ApplicationTypeID); ;
            clsApplicationType.ApplicationTypeTitle = txtTitle.Text;
            clsApplicationType.ApplicationFees = Convert.ToSingle(txtFees.Text);

            if (clsApplicationType.Save())
            {
                MessageBox.Show("Data Saved Succefuly ","Saved",MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("fail to Save Data  ", "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
