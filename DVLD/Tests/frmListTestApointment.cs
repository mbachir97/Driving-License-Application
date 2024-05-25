using DVLD.Properties;
using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestApointment : Form
    {

        private DataTable _dtAllApointment;

        //private DataTable _dtAllApointment = clsTestAppointment.GetAppointment();


        //My Methode
        public  enum enTestMode {enVission=1,enWritten=2,enPractical=3}
        //My Methode
       public static  enTestMode _TestMode = enTestMode.enVission;

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VissionTest;

        static private int _LocalAppID = -1;

       static  private int _MyTestType = -1;

        private int _TestPass = -1;
        enTestMode _ReturnMode(int PasedTest)
        {
            switch (PasedTest)
            {
                case 0:
                    return enTestMode.enVission;
                    case 1:
                    return enTestMode.enWritten;
                    case 2: 
                    return enTestMode.enPractical;  

                    default: return enTestMode.enVission;   
            }

           
        }

        //My Methode
        public frmListTestApointment(int LocalAppID, int PassedTest)
        {
            _TestMode = _ReturnMode(PassedTest);
            _TestPass=PassedTest;   
            _LocalAppID = LocalAppID;
            InitializeComponent();
        }


        public frmListTestApointment(int LocalAppID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _TestType = TestType;
            //_TestPass = PassedTest;
            _LocalAppID = LocalAppID;
            
        }

        void IniatialValue()
        {
            switch (_TestMode) {
            
                case enTestMode.enVission:
                    pbTest.Image = Resources.Vision_512;
                    lblTestTitle.Text = "Vission Test Appointment";
                    _MyTestType = 1;
                    break;
                case enTestMode.enWritten:
                    pbTest.Image = Resources.Written_Test_512;
                    lblTestTitle.Text = "Written Test Appointment";
                    _MyTestType = 2;  
                    break;
                case enTestMode.enPractical:
                    pbTest.Image = Resources.driving_test_512;
                    lblTestTitle.Text = "Practical Test Appointment";
                    _MyTestType = 3;  
                    break;



            }
        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VissionTest:
                    {
                        lblTestTitle.Text = "Vision Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTest.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTestTitle.Text = "Written Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTest.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTestTitle.Text = "Street Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTest.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }
        void _LooadInformation()
        { 
            //My Methoed
            //IniatialValue();

            _LoadTestTypeImageAndTitle();
            ctrDrivingLicenceApplicatioInfo1.LoadLocalApplicationInfo(_LocalAppID);
            if(ctrDrivingLicenceApplicatioInfo1.LocalDrivingApp==-1)
                this.Close();       
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            //this is my methode with the frmSchudueledTest
            //frmSchudueldTest frmSchudueldTest = new frmSchudueldTest(_LocalAppID, true);

            //if (clsTestAppointment.IsTestAppointmentExist(_MyTestType, _LocalAppID))
            //{
            //    if(clsTestAppointment.IsTestAppointmentExist(_MyTestType, _LocalAppID, false))
            //    {
            //        MessageBox.Show("this Application  is Already have an active Appointment "
            //            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    if(clsTestAppointment.IsTestPassed(_MyTestType, _LocalAppID))
            //    {
            //        MessageBox.Show("this Application  is Passed the Test  "
            //           , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        return;
            //    }


            //    frmSchudueldTest.ShowDialog();
            //    _Refresh();


            //}

            //else
            //{
            //    frmSchudueldTest = new frmSchudueldTest(_LocalAppID, false);
            //    frmSchudueldTest.ShowDialog();
            //    _Refresh();


            //}

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalAppID(_LocalAppID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //---
            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                NewfrmSchhudueldTest frm1 = new NewfrmSchhudueldTest(_LocalAppID, _TestType);
                frm1.ShowDialog();
                frmListTestApointment_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewfrmSchhudueldTest frm2 = new NewfrmSchhudueldTest
                (LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
            frm2.ShowDialog();
            frmListTestApointment_Load(null, null);



        }

        void _Refresh()
        {
            _dtAllApointment = clsTestAppointment.GetAllTestAppointment(_MyTestType, _LocalAppID);
            //_dtAllApointment = clsTestAppointment.GetAppointment();


            dgvAppointment.DataSource = _dtAllApointment;

            lblRecordsCount.Text=dgvAppointment.Rows.Count.ToString();  
    }

        private void frmListTestApointment_Load(object sender, EventArgs e)
        {
           
           

            _LooadInformation();
            //My Mehode
            //_dtAllApointment = clsTestAppointment.GetAllTestAppointment(_MyTestType, _LocalAppID);
            _dtAllApointment = clsTestAppointment.GetAllTestAppointment((int)_TestType, _LocalAppID);

            dgvAppointment.DataSource = _dtAllApointment;

            lblRecordsCount.Text= dgvAppointment.Rows.Count.ToString();     
        }

        private void ctrDrivingLicenceApplicatioInfo1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvAppointment.CurrentRow.Cells[0].Value;


            NewfrmSchhudueldTest frm = new NewfrmSchhudueldTest(_LocalAppID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmListTestApointment_Load(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvAppointment.CurrentRow.Cells[0].Value;

            NewTakeTest frm = new NewTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmListTestApointment_Load(null, null);
        }
    }
}
