using DVLD.Licenses;
using DVLD.Tests;
using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Applications
{
    public partial class frmListLocalDrivingLicense : Form
    {

        private static DataTable _AllLocalApplication;
        public frmListLocalDrivingLicense()
        {
            InitializeComponent();
        }


        void _ResteContextMenue()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = true;

            edditApplicationToolStripMenuItem.Enabled = true;

            deleteApplicationToolStripMenuItem.Enabled = true;

            cancelApplicationToolStripMenuItem.Enabled = true;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

            showLicenseToolStripMenuItem.Enabled = false;

            writtenTestToolStripMenuItem.Enabled = false;

            practicalTestToolStripMenuItem.Enabled = false;

            scheduleTestsToolStripMenuItem.Enabled = true; 

            vissiionTestsToolStripMenuItem.Enabled = true; 
         showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
        }


        void _Canceled()
        {
            showApplicationDetailsToolStripMenuItem.Enabled = false;

            edditApplicationToolStripMenuItem.Enabled = false;

            deleteApplicationToolStripMenuItem.Enabled = true;

            cancelApplicationToolStripMenuItem.Enabled = false;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

            showLicenseToolStripMenuItem.Enabled = false;

            writtenTestToolStripMenuItem.Enabled = false;

            practicalTestToolStripMenuItem.Enabled = false;

            scheduleTestsToolStripMenuItem.Enabled = false;

            vissiionTestsToolStripMenuItem.Enabled = false;
            showPersonLicenseHistoryToolStripMenuItem.Enabled = false;
        }

        void _Complete()
        {
            edditApplicationToolStripMenuItem.Enabled = false;

            deleteApplicationToolStripMenuItem.Enabled = false;

            cancelApplicationToolStripMenuItem.Enabled = false;

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;

            showLicenseToolStripMenuItem.Enabled = true;
        }
        private void frmListLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _AllLocalApplication=clsApplication.GetAllApplicationInMyView();    

            dgvListLocalApp.DataSource = _AllLocalApplication;

            lblRecordsCount.Text = dgvListLocalApp.Rows.Count.ToString();


            cbxFindBy.SelectedIndex = 0;
            _ResteContextMenue();



            dgvListLocalApp.Columns[0].HeaderText = "L.ApplicationID";


            //dgvListLocalApp.Columns[2].HeaderText = "National No";

            //dgvListLocalApp.Columns[4].HeaderText = "Application Date";

            //dgvListLocalApp.Columns[5].HeaderText = "Passed Test";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewLocalDrivingApplication newLocalDrivingApplication = new NewLocalDrivingApplication();
            newLocalDrivingApplication.ShowDialog();

            frmListLocalDrivingLicense_Load(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";



            switch (cbxFindBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "FullName":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;

                default:

                    FilterColumn = "None";
                    break;

            }

            if(textBox1.Text=="" || FilterColumn == "None")
            {

                _AllLocalApplication.DefaultView.RowFilter = "";
                lblRecordsCount.Text= dgvListLocalApp.Rows.Count.ToString();

                return;

            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _AllLocalApplication.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, textBox1.Text.Trim());
            else
                _AllLocalApplication.DefaultView.RowFilter = string.Format("{0} like '{1}%' ", FilterColumn, textBox1.Text.Trim());


            lblRecordsCount.Text = dgvListLocalApp.Rows.Count.ToString();

        }




        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (cbxFindBy.Text == "L.D.L.AppID")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (cbxFindBy.Text != "None");
            textBox1.Text = "";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            


        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            switch (dgvListLocalApp.CurrentRow.Cells[5].Value)
            {
                case 0:
                    _ResteContextMenue();
                    break;
                case 1:
                    _ResteContextMenue();
                    writtenTestToolStripMenuItem.Enabled = true;

                    practicalTestToolStripMenuItem.Enabled = false;

                    vissiionTestsToolStripMenuItem.Enabled = false;
                    break;

                case 2:
                    _ResteContextMenue();
                    writtenTestToolStripMenuItem.Enabled = false;

                    practicalTestToolStripMenuItem.Enabled = true;

                    vissiionTestsToolStripMenuItem.Enabled = false;
                    break;

                case 3:
                    _ResteContextMenue();
                    scheduleTestsToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;

                    break;

            }

            if (dgvListLocalApp.CurrentRow.Cells[6].Value.ToString() == "Completed")
            {

                _Complete();


            }

            if(dgvListLocalApp.CurrentRow.Cells[6].Value.ToString() == "Cancelled")
            {
                _Canceled();

            }

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication
                .FindByLocalAppID
                ((int)dgvListLocalApp.CurrentRow.Cells[0].Value);
            if(LocalApp != null ) {


              


                if (LocalApp.Cancel())
                {
                    MessageBox.Show("This Application is  Canceled Successfuly", "Cancel",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                    MessageBox.Show("Faild To Canceld This Applicarion", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);


                frmListLocalDrivingLicense_Load(null, null  );

            }
            else
            {
                MessageBox.Show("This Application is not Found","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }


        }

        private void vissiionTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListTestApointment Appointment = new frmListTestApointment
            //    ((int)dgvListLocalApp.CurrentRow.Cells[0].Value, (int)dgvListLocalApp.CurrentRow.Cells[5].Value);


            //Appointment.ShowDialog();

            //frmListLocalDrivingLicense_Load(null, null  );

            ScheduelTest(clsTestType.enTestType.VissionTest);
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmListTestApointment Appointment = new frmListTestApointment
            //  ((int)dgvListLocalApp.CurrentRow.Cells[0].Value, (int)dgvListLocalApp.CurrentRow.Cells[5].Value);


            //Appointment.ShowDialog();

            //frmListLocalDrivingLicense_Load(null, null  );

            ScheduelTest(clsTestType.enTestType.WrittenTest);
        }

        private void practicalTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  frmListTestApointment Appointment = new frmListTestApointment
            //((int)dgvListLocalApp.CurrentRow.Cells[0].Value, (int)dgvListLocalApp.CurrentRow.Cells[5].Value);


            //  Appointment.ShowDialog();

            //  frmListLocalDrivingLicense_Load(null, null  );

            ScheduelTest(clsTestType.enTestType.StreetTest);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueLincense Isuue1 = new frmIssueLincense((int)dgvListLocalApp.CurrentRow.Cells[0].Value);

            Isuue1.ShowDialog();
            frmListLocalDrivingLicense_Load(null, null  );
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clsLocalDrivingLicenseApplication Lincense = clsLocalDrivingLicenseApplication.Find(
            //    (int)dgvListLocalApp.CurrentRow.Cells[0].Value);
            //frmShowLicense ShowLincense = new frmShowLicense(clsLicense.
            //    FindByAppID(Lincense.ApplicationInfo.ApplicationID).LicenseID);


            //ShowLincense.ShowDialog();  

            int LocalAppID = (int)dgvListLocalApp.CurrentRow.Cells[0].Value;

            int LicenseID= clsLocalDrivingLicenseApplication.FindByLocalAppID(LocalAppID)
                .GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicense frm = new frmShowLicense(LicenseID);
                frm.ShowDialog();   
            }

            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication Lincense = clsLocalDrivingLicenseApplication.Find(
                (int)dgvListLocalApp.CurrentRow.Cells[0].Value);

            frmshowPersonLicenseHistory History = new frmshowPersonLicenseHistory
                (Lincense.ApplicationInfo.PersonInfo.PersonID);
            History.ShowDialog();   

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalAppInfo frm =new frmLocalAppInfo((int)dgvListLocalApp.CurrentRow.Cells[0].Value); 
            frm.ShowDialog();   
        }

        private void edditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingApplication newLocalDrivingApplication 
                = new NewLocalDrivingApplication((int)dgvListLocalApp.CurrentRow.Cells[0].Value);

            newLocalDrivingApplication.ShowDialog();
            frmListLocalDrivingLicense_Load(null,null);

        }


        void ScheduelTest(clsTestType.enTestType TestType)
        {
            frmListTestApointment Appointment = new frmListTestApointment
       ((int)dgvListLocalApp.CurrentRow.Cells[0].Value, TestType);


            Appointment.ShowDialog();

            frmListLocalDrivingLicense_Load(null, null);
        }
        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvListLocalApp.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalAppID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicense_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
