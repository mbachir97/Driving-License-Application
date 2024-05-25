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
    public partial class frmSchudueldTest : Form
    {

        enum enMode {enAdd=1,enEdit=2 };

        enMode _Mode = enMode.enAdd;

        int _LocalID = -1;

        clsTestAppointment _TestAppointment;

        private int  _TestAppointmentID=-1; 

        bool _IsRetakeTest = false;     
        public frmSchudueldTest(int LocalAppID, bool isRetakeTest)
        {

            _LocalID=LocalAppID;    
            _Mode = enMode.enAdd;
            InitializeComponent();
            _IsRetakeTest = isRetakeTest;   
        }


        public frmSchudueldTest(int TestApointmentID)
        {
            _Mode = enMode.enEdit;

            _TestAppointmentID=TestApointmentID;    
            InitializeComponent();
            
        }

        void _LoadInformation()
        {
            if (_Mode == enMode.enAdd)
            {
               
                scheduelTest1.LoadInformation(_LocalID);
                scheduelTest1.RetakeTestEnable = _IsRetakeTest;

            }
            else { 
                
                _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
                scheduelTest1.LoadInformation(_TestAppointment);
                scheduelTest1.RetakeTestEnable=
                    (_TestAppointment.RetakeTestApplicationID!=-1)?true:false;
            }
        }
        private void frmSchudueldTest_Load(object sender, EventArgs e)
        {
            _LoadInformation();
        }
    }
}
