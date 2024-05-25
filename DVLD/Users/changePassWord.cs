using DVLD.Global;
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
    public partial class changePassWord : Form
    {
        int _UserID;
        clsUser _User;
        public changePassWord(int UserID)
        {
            InitializeComponent();
            _UserID=UserID;
            _User = clsUser.Find(_UserID);
        }

        private void changePassWord_Load(object sender, EventArgs e)
        {
            if (_User == null)
            {
                MessageBox.Show("this form will be closed because No User with ID " + _UserID + "");
                this.Close();
                return;
            }
            ctrCardUser1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassWord_Validating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrEmpty(txtCurrentPassWord.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassWord, "Username cannot be blank");
                return;
            }
            if (_User != null)
            {
                if (txtCurrentPassWord.Text!= _User.PassWord)
                {

                    e.Cancel = true;
                    txtCurrentPassWord.Focus();
                    errorProvider1.SetError(txtCurrentPassWord, "This PassWord is not for the Current User..");

                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtCurrentPassWord, "");

                }
            }
           
        }

        private void txtConfirm_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirm.Text != txtNewPassWord.Text)
            {

                e.Cancel = true;
                txtConfirm.Focus();
                errorProvider1.SetError(txtConfirm, "Confirm The PassWord Correctly..");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirm, "");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        
            _User.PassWord = txtNewPassWord.Text;
            if (!clsGlobal.RememberUserNameAndPassWord(_User.UserName, txtNewPassWord.Text.Trim()))
            {
                MessageBox.Show("We could not Update The PassWord In The Registery",
                   "Update  Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return    ;
            }

            if (_User.Save()) {

                MessageBox.Show("the PassWord is Changed Successfuly", "Information");
            }
            else
            {
                MessageBox.Show("the PassWord is not  Changed Successfuly", "Error "
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void txtNewPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassWord.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassWord, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtNewPassWord, null);
            };
        }
    }
}
