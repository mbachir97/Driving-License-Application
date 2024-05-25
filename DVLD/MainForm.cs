using DVLD.People;
using DVLD.Users;
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
using DVLD_Bisness;
using DVLD.Applications;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.Global;

namespace DVLD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Login _Login;
            


        public MainForm(Login obj)
        {
            InitializeComponent();

            _Login=obj;
        }
      
        private void newDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

       

        private void peapleToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            ManagePeople managePeople = new ManagePeople();
        
            managePeople.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {




            ManageUsers manageUsers = new ManageUsers();

            manageUsers.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsCurrentUser.CurrentUser=null;

            _Login.Show();
            this.Close();   
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails User = new UserDetails(clsCurrentUser.CurrentUser.UserID);
            User.ShowDialog();
        }

        private void changePassWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePassWord ch =new changePassWord(clsCurrentUser.CurrentUser.UserID);
            ch.ShowDialog();    
        }

        private void manageAplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manageAplicationTypes manageAplicationTypes = new manageAplicationTypes();
            manageAplicationTypes.ShowDialog();
        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manageTestType manageTestType = new manageTestType();   
            manageTestType.ShowDialog();    
            
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingApplication newLocalDrivingApplication = new NewLocalDrivingApplication();
            newLocalDrivingApplication.ShowDialog();    
        }

        private void localDrivingLicensesApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicense frmListLocalDrivingLicense = new frmListLocalDrivingLicense();
            frmListLocalDrivingLicense.ShowDialog();    
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmDriversList Driverlist= new frmDriversList();    
            Driverlist.ShowDialog();    


        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApp frmNewInternationalLicenseApp = new frmNewInternationalLicenseApp();


            frmNewInternationalLicenseApp.ShowDialog(); 
        }

        private void newDrivingLicensesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRenewLicense frmRenewLicense = new frmRenewLicense();
            frmRenewLicense.ShowDialog();   


        }

        private void internationalLicensesApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagmentInterLicenseApp frm = new frmManagmentInterLicenseApp();    
            frm.ShowDialog();   
        }

        private void demmagedDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmrReplacmentForLostDemaged frm = new fmrReplacmentForLostDemaged();    

            frm.ShowDialog();
        }

        private void detainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense Detain = new frmDetainLicense();
            
            Detain.ShowDialog();    
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetaineLicense frm   = new frmListDetaineLicense();  
            frm.ShowDialog();   
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Login.Close(); 
        }

        private void releaseDetainedDrivingLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();   
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicense frm = new frmListLocalDrivingLicense();  
            frm.ShowDialog();   
        }
    }
}
