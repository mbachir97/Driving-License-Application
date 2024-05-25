using DVLD.People;
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
using System.Diagnostics.Contracts;

namespace DVLD.Users
{
    public partial class AddUpateUser : Form
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        private clsUser _User;
        private int _UserID;
        private int _PersonID; 
        public AddUpateUser(int UserID)
        {

            InitializeComponent();
            _UserID= UserID;    
            if(_UserID==-1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;


        }

        void _FillInfopmation()
        {
            ctrPersonCard1.LoadInfoPerson(_User.PersonID);
            txtUserName.Text = _User.UserName;  
            txtPassWord.Text = _User.PassWord;
            txtConfirm.Text = _User.PassWord;
            checIsActive.Checked = _User.IsActive;
            lblUserID.Text = _User.UserID.ToString();

        }
        void _LoadData()
        {
            if(_Mode== enMode.AddNew)
            {
                _User= new clsUser();   
                btnUpdatePerson.Visible = false;
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
                lblTitle.Text = "Update New User";
                btnUpdatePerson.Visible = true;
                _FillInfopmation();

            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FindPerson findPerson = new FindPerson();

            //findPerson.DataBack += FindPerson_DataBack;

            //findPerson.ShowDialog();
            //if (_PersonID != 0)
            //{
            //    ctrPersonCard1.LoadInfoPerson(_PersonID);
            //    _User.PersonID = _PersonID;
            //}
           
        }

        private void FindPerson_DataBack(object Sender, int PersonID)
        {
            _PersonID = PersonID;
           
        }

        private void AddUpateUser_Load(object sender, EventArgs e)
        {
            _LoadData();
            
        }

        private void btnUpdatePerson_Click(object sender, EventArgs e)
        {

            AddUpdatePerson add =new AddUpdatePerson(_User.PersonID);
            //we can Subscribe to delegate in AddUpdatePerson
            add.ShowDialog();   
            ctrPersonCard1.LoadInfoPerson(_User.PersonID);
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            if(!clsUser.IsUserExistByPersonID(_PersonID))
            tabControl1.SelectedIndex = 1;   
            else
                MessageBox.Show("this Person Is Already User choose Onother Person ","Error"
                    ,MessageBoxButtons.OK, MessageBoxIcon.Error);   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsUser.IsUserExistByPersonID(_PersonID) && _Mode==enMode.AddNew)
            {
                MessageBox.Show(" we can not Save , this Person Is Already User choose Onother Person ", "Error"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if(txtConfirm.Text!=txtPassWord.Text)
            {
                MessageBox.Show("Confirm The PassWord Correctly", "Error"
                     , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.UserID = _UserID;
            
            _User.UserName = txtUserName.Text;
            _User.PassWord = txtPassWord.Text; 
            if(checIsActive.Checked)
            _User.IsActive=true;
            else
            _User.IsActive=false;   

            if (_User.Save())
            {
                MessageBox.Show("User Added Successfuly", "information");
                lblUserID.Text = clsUser.Find(txtUserName.Text).UserID.ToString();
            }
            else
                MessageBox.Show("User is not  Added Successfuly", "information");

           
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

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
            if (txtPassWord.Text!=txtConfirm.Text)
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
    }
}
