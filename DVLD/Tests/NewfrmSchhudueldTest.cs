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

namespace DVLD.Tests
{
    public partial class NewfrmSchhudueldTest : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VissionTest;
        private int _AppointmentID = -1;
        public NewfrmSchhudueldTest(int LocalAppID,clsTestType.enTestType TestType,int AppointmentID=-1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID= LocalAppID;  
            _TestTypeID= TestType;  
            _AppointmentID= AppointmentID;  
        }

        private void NewfrmSchhudueldTest_Load(object sender, EventArgs e)
        {
            newSchudeldTest1.TestTypeID = _TestTypeID;
            newSchudeldTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
