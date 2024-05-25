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
using DVLD_Bisness;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD.People
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }
        static DataTable dt = clsPerson.GetAllPeople();

      static   private DataTable _dtAllPeople = dt.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gendor", "DateOfBirth", "Nationality",
                                                       "Phone", "Email");



          
         BindingSource BindingSource = new BindingSource(_dtAllPeople, "");
      

        void GetSommData1( string Filter, string Value)
        {


            if (Filter.ToLower() == "personid")
            {
                dt.DefaultView.RowFilter = $"[{Filter}] = {Value}";

                return;
            }


            dt.DefaultView.RowFilter = $"{Filter} like \'{Value}%\'";

            


        }
       
        //My beautiful Methode keep going..dont give up...
        private void _SearchWithFilter()
        {
           
            if(string.IsNullOrEmpty (cbxFindBy.Text) || txtFilterValue.Text=="")
            {
                dt.DefaultView.RowFilter = "";

                return;
            }
            switch (cbxFindBy.Text.ToLower())
            {

                case "personid":

                   // dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, textBox1.Text );
                    //dgvPeople.DataSource = GetSommData(dt, cbxFindBy.Text, textBox1.Text);
                    break;
                case "nationalno":

                     GetSommData1( cbxFindBy.Text, txtFilterValue.Text);
                    // dgvPeople.DataSource = GetSommData(dt, cbxFindBy.Text, textBox1.Text); 
                    break;
                case "firstname":
                   // GetSommData1(cbxFindBy.Text, textBox1.Text);
                    // dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, textBox1.Text );
                    //dgvPeople.DataSource = GetSommData(dt, cbxFindBy.Text, textBox1.Text);
                    break;
                case "secondname":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
                    break;
                case "thirdname":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
                    break;
                case "lastname":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text);
                    dgvPeople.DataSource = dt;
                    break;
                case "email":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
                    break;
                case "phone":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
                    break;
                case "gendor":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
                    break;
                case "nationality":

                    dt = clsPerson.GetDataWithFilter(cbxFindBy.Text, txtFilterValue.Text );
                    dgvPeople.DataSource = dt;
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
                case "NationalNo":
                    FilterColumn = "NationalNo";
                    break;
                case "FirstName":
                    FilterColumn = "FirstName";
                    break;
                case "SecondName":
                    FilterColumn = "SecondName";
                    break;
                case "ThirdName":
                    FilterColumn = "ThirdName";
                    break;
                case "LastName":
                    FilterColumn = "LastName";
                    break;
                case "Nationality":
                    FilterColumn = "Nationality";
                    break;
                case "Gendor":
                    FilterColumn = "Gendor";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if(txtFilterValue.Text=="" || FilterColumn=="None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                lblcountPeople.Text=dgvPeople.Rows.Count.ToString();    
                return;
            }

            if (FilterColumn == "PersonID")
            {
                _dtAllPeople.DefaultView.RowFilter = string.Format("{0} = {1}",FilterColumn,txtFilterValue.Text.Trim());
            }
            else

            _dtAllPeople.DefaultView.RowFilter = string.Format("{0} like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblcountPeople.Text= dgvPeople.Rows.Count.ToString();   


        }
        void _RefrechListPerson()
        {
            dgvPeople.DataSource = clsPerson.GetAllPeople();
        }

        void _RefreshPeopleList()
        {
             dt = clsPerson.GetAllPeople();

         _dtAllPeople = dt.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gendor", "DateOfBirth", "Nationality",
                                                       "Phone", "Email");
            dgvPeople.DataSource = _dtAllPeople;

            lblcountPeople.Text=dgvPeople.Rows.Count.ToString();    

    }

    private void ManagePeople_Load(object sender, EventArgs e)
        {
         

            dgvPeople.DataSource = BindingSource;
            cbxFindBy.SelectedIndex = 0;
            lblcountPeople.Text = dgvPeople.Rows.Count.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            AddUpdatePerson addUpdatePerson = new AddUpdatePerson(-1);
            addUpdatePerson.ShowDialog();

            _RefreshPeopleList(); 



        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePerson frm1 = new AddUpdatePerson(-1);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdatePerson frm1 = new AddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails PersonDetails = new PersonDetails((int)dgvPeople.CurrentRow.Cells[0].Value);
            PersonDetails.ShowDialog(); 
        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbxFindBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch =e.KeyChar;

            if (cbxFindBy.Text.ToLower() == "personid")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           BindingSource.Clear();   
        }
    }
}
