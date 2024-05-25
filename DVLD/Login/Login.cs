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
using System.IO;
using DVLD.Global;

namespace DVLD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
          
                
               

           
            clsUser User =clsUser.Find(txtUserName.Text,txtPassWord.Text);
            if (User != null )
            {
                if (User.IsActive)
                {
                   
                    if (checkBox1.Checked)
                    {
                        if (!clsGlobal.RememberUserNameAndPassWord(txtUserName.Text, txtPassWord.Text))
                                return;
                            
                      
                        
                    }
                    else
                    {
                        if (!clsGlobal.RememberUserNameAndPassWord("", ""))
                            return;
                    }
                    clsCurrentUser.CurrentUser = User;
                    MainForm mainForm = new MainForm(this);
                     this.Hide();
                    mainForm.ShowDialog();    
                    return;
                }
                MessageBox.Show("This User is not Active ","Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            else
            MessageBox.Show("Wrong PassWord And UserName ", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
               

        }

        private void Login_Load(object sender, EventArgs e)
        {
            string UserName = "";
            string PassWord = "";
            
            if(clsGlobal.GetStoredCredential(ref UserName,ref PassWord))
            {
                txtPassWord.Text = PassWord;
                txtUserName.Text = UserName;    
                checkBox1.Checked = true;   
            }
            else
            {
                checkBox1.Checked = false;
            }
         
           

        }
    }
}
