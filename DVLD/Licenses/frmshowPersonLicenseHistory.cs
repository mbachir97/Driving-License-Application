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

namespace DVLD.Licenses
{
    public partial class frmshowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmshowPersonLicenseHistory(int PersonID)
        {
            _PersonID=PersonID; 
            InitializeComponent();
        }

        private void showPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrPersonCardWithFilter1.FilterEnabled = false;
            ctrPersonCardWithFilter1.LoadInformation1(_PersonID);
            
            clsDriver Driver = clsDriver.FindByPerson(_PersonID);

            if (Driver != null)
            {

                ctrDriverLicense1.LoadLocalInformation(Driver.DriverID);

                ctrDriverLicense1.LoadInternationalInformation(Driver.DriverID);

            }
        }
    }
}
