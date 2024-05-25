using DVLD_Bisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class ManageUsers : Form
    {

        DataTable _dtAllUsers;
        public ManageUsers()
        {
            InitializeComponent();
        }

        void _RefrechListUser()
        {
             _dtAllUsers = clsUser.GetAllUser();

            dgvUsers.DataSource = _dtAllUsers;
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;   
            cbIsActive.Visible = false;
            _dtAllUsers = clsUser.GetAllUser();

            dgvUsers.DataSource = _dtAllUsers;
            cbxFindBy.SelectedIndex= 0; 

        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
                
                  
                if (cbxFindBy.Text.ToLower() == "isactive")
                {
                    cbIsActive.Visible = true;
                    textBox1.Visible = false;
                return;
                }
                else if(cbxFindBy.Text.ToLower()!="none")
                {
                    cbIsActive.Visible = false;
                    textBox1.Visible = true;
                return;
                }

            cbIsActive.Visible = false;
            textBox1.Visible = false;


        }
          
         
        //My Methode
        private void _SearchWithFilter()
        {
            DataTable dt = new DataTable();

            switch (cbxFindBy.Text.ToLower())
            {
                case "personid":

                    dt = clsUser.GetDataWithFilter(cbxFindBy.Text, textBox1.Text );
                    dgvUsers.DataSource = dt;
                    break;
                case "userid":

                    dt = clsUser.GetDataWithFilter(cbxFindBy.Text, textBox1.Text );
                    dgvUsers.DataSource = dt;
                    break;
                case "fullname":
                    dt = clsUser.GetDataWithFilter(cbxFindBy.Text, textBox1.Text );
                    dgvUsers.DataSource = dt;
                    break;


            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbxFindBy.Text)
            {
                case "PersonID":
                    FilterColumn = "PersonID";  
                    break;
                case "UserID":
                    FilterColumn = "UserID";
                    break;
                case "FullName":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;


            }

            if(textBox1.Text=="" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                return;
            }


            if(FilterColumn =="PersonID" || FilterColumn == "UserID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, textBox1.Text.Trim());

            }
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} like '{1}%'", 
                    FilterColumn, textBox1.Text.Trim());


        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dataTable = new DataTable();
            //switch (cbIsActive.Text.ToLower())
            //{

            //    case "all":
            //        dataTable = clsUser.GetAllUser();
            //        dgvUsers.DataSource= dataTable;
            //        break;
            //    case "yes":
            //        dataTable = clsUser.GetActiveUser(true);
            //        dgvUsers.DataSource = dataTable;
            //        break;
            //    case "no":
            //        dataTable = clsUser.GetActiveUser(false);
            //        dgvUsers.DataSource = dataTable;
            //        break;



            //}

            string FilterColumn = "IsActive";
            string FilterValue = "";
            switch (cbIsActive.Text.ToLower())
            {
                case "yes":
                    FilterValue = "1";
                    break;
                case "no":
                    FilterValue = "0";
                    break;  
                default:
                    FilterValue = "";
                    break;
            }
            if (FilterColumn == "")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                return; 
            }
            else
            _dtAllUsers.DefaultView.RowFilter =  string.Format("{0} ={1}",
                FilterColumn, FilterValue);


        }

            private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUpdateUser1 addUpateUser = new AddUpdateUser1(-1);
            addUpateUser.ShowDialog();
            _RefrechListUser();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUser1 addUpateUser = new AddUpdateUser1(-1);
            addUpateUser.ShowDialog();
            _RefrechListUser();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUser1 addUpateUser = new AddUpdateUser1((int)dgvUsers.CurrentRow.Cells[0].Value);
            addUpateUser.ShowDialog();
            _RefrechListUser();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails((int)dgvUsers.CurrentRow.Cells[0].Value);    
            userDetails.ShowDialog();   
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            changePassWord ch = new changePassWord((int)dgvUsers.CurrentRow.Cells[0].Value);
            ch.ShowDialog();    
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (cbxFindBy.Text.ToLower() == "personid" || cbxFindBy.Text.ToLower()=="userid")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
        }
    }
}
