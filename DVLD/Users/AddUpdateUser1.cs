using DVLD.Controles;
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

namespace DVLD.Users
{
    public partial class AddUpdateUser1 : Form
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        private clsUser _User;
        private int _UserID;
        private int _PersonID;
        public AddUpdateUser1(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            if (_UserID == -1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;
        }

        void _LoadData()
        {
            tabLogininfo.Enabled = false;

            if (_Mode == enMode.AddNew)
            {
                _User = new clsUser();
               
                lblTitle.Text = "Add New User";
                return;
            }
            else
            {
                _User = clsUser.Find(_UserID);
                if (_User == null)
                {
                    MessageBox.Show("this form will be closed because No Contatc with ID " + _UserID + "");
                    this.Close();
                    return;
                }


                _FillInfopmation();




            }

        }
        void _FillInfopmation()
        {
            ctrPersonCardWithFilter1.FilterEnabled = false;  
            ctrPersonCardWithFilter1.LoadInformation1(_User.PersonID);
            txtUserName.Text = _User.UserName;
            txtPassWord.Text = _User.PassWord;
            txtConfirm.Text = _User.PassWord;
            checIsActive.Checked = _User.IsActive;
            lblUserID.Text = _User.UserID.ToString();
            lblTitle.Text = "Update New User";
            _PersonID = _User.PersonID;

        }

        private void AddUpdateUser1_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["tabLogininfo"];
                btnSave.Enabled = true;
                tabLogininfo.Enabled = true;
                return;
            }

            if (ctrPersonCardWithFilter1.PersonID!=-1)
            {
                if (clsUser.IsUserExistByPersonID(ctrPersonCardWithFilter1.PersonID)){

                    MessageBox.Show("this Person Is Alraedy A user choose Another one ..",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    tabControl1.SelectedTab = tabControl1.TabPages["tabLogininfo"];
                    btnSave.Enabled = true;
                    tabLogininfo.Enabled=true;  




                }
            }
            else

            MessageBox.Show("You have to Add A person First","Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);        



        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

      
            _User.UserID = _UserID;
            _User.PersonID=_PersonID; 
            _User.UserName = txtUserName.Text;
            _User.PassWord = txtPassWord.Text;
            if (checIsActive.Checked)
                _User.IsActive = true;
            else
                _User.IsActive = false;

            if (_User.Save())
            {
                MessageBox.Show("User Added Successfuly", "information");
                lblUserID.Text = _User.UserID.ToString();
            }
            else
                MessageBox.Show("User is not  Added Successfuly", "information");
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (clsUser.IsUserExist(txtUserName.Text))
            {

                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "This User Is Already in The System..");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");

            }
        }

        private void txtConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassWord.Text != txtConfirm.Text)
            {

                e.Cancel = true;
                txtConfirm.Focus();
                errorProvider1.SetError(txtConfirm, "the PassWord is not the Same Please Confirm" +
                    "The PassWod Correctly");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirm, "");

            }
        }

        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;    
        }

        private void ctrPersonCardWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}
