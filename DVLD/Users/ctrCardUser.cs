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

namespace DVLD.Controles
{
    public partial class ctrCardUser : UserControl
    {
        private clsUser _User;
        private int _UserID = -1;

        public ctrCardUser()
        {
            InitializeComponent();
        }

        private void ctrPersonCard1_Load(object sender, EventArgs e)
        {

        }
        public void LoadUserInfo(int UserID)
        {
            _User=clsUser.Find(UserID);
            if (_User == null)
            {
                MessageBox.Show("No Person With PersonID =" + UserID.ToString()
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _FillUserInfo();
                ctrPersonCard1.LoadInfoPerson(_User.PersonID);
            }

        }


        public void _FillUserInfo()
        {
            _UserID=_User.UserID;
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive)
                lblIsAcive.Text = "Yes";
            else lblIsAcive.Text = "N0"; 
            
        }

        private void ctrUserCard_Load(object sender, EventArgs e)
        {

        }

        private void ctrPersonCard1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
