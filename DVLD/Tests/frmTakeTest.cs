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
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;

        clsTestAppointment _TestAppointment;

        public frmTakeTest(int TestAppointment)
        {

            _TestAppointmentID = TestAppointment;
            InitializeComponent();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            schudueledTest1.LoadLocalAppInformation(_TestAppointment.LocalDrivingLicenseInfo
                .LocalDrivingLicenseApplicationID);



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest Test1 = new clsTest();

            Test1.TestAppointmentID= _TestAppointmentID;   
            Test1.TestResult = rbPass.Checked;
            Test1.Notes=txtNote.Text;   
            Test1.CreatedByUserID=clsCurrentUser.CurrentUser.UserID;

                       
            
                if(MessageBox.Show("Are you Shure you want to passs or Fail this Person"
                    ,"Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)
                    == DialogResult.Yes)
                {

                    if (Test1.Save())
                    {

                    _TestAppointment.LockedAppointment();
                    MessageBox.Show("Data Saved Succesfuly ","Saved",
                        MessageBoxButtons.OK,MessageBoxIcon.Information);

                    }
                else
                {
                    MessageBox.Show("Data is not Saved ", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
                

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
