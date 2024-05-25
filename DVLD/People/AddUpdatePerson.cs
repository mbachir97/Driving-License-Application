using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global;
using DVLD.Properties;
using DVLD_Bisness;

namespace DVLD.People
{
    public partial class AddUpdatePerson : Form
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;

        private int _PersonID = 0;

        private clsPerson _Person;

        public AddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;

        }
        void _FillCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow dr in dt.Rows)
            {
                cbxCountry.Items.Add(dr["CountryName"]);

            }
        }
        public delegate void DataBackEventHandler(object Sender, int PersonID);
        public event DataBackEventHandler DataBack;
        void _FillInformation()
        {
            lblPersonID.Text = _PersonID.ToString();
            txtNationalNo.Text = _Person.NationalNO;
            txtFirsName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            dateTimePicker1.Value = _Person.DateOfBirth;
            if (_Person.Gendor == 0)
            {
                rbMail.Checked = true;
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_boy.png");


            }

            if (_Person.Gendor == 1)
            {
                rbFemail.Checked = true;
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_girl.png");

            }

            cbxCountry.SelectedIndex = cbxCountry.FindString(clsCountry.
                Find(_Person.CountryID).CountryName);

            if (_Person.ImagePath != "")
            {
                pbxPersonImage.ImageLocation = _Person.ImagePath;

            }
            else
                pbxPersonImage.ImageLocation = null;
            linkRemove.Visible = (_Person.ImagePath != "");



        }

        void _ResetValue()
        {
            if (rbFemail.Checked)
                pbxPersonImage.Image = Resources.Female_512;
            if(rbMail.Checked)  
                pbxPersonImage.Image= Resources.Male_512; 

            linkRemove.Visible = (pbxPersonImage.ImageLocation !=null);

            dateTimePicker1.MaxDate =DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate= DateTime.Now.AddYears(-100);

            cbxCountry.SelectedIndex = cbxCountry.FindString("Algeria");

            txtFirsName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMail.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        void _LoadData()
        {
            _FillCountries();
            _ResetValue();
            if (_Mode == enMode.AddNew)
            {
                _Person = new clsPerson();
                lblTitle.Text = "Add New Person";
                cbxCountry.SelectedIndex = cbxCountry.FindString("Jordan");  
                 dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);    

                return;


            }
            else if (_Mode == enMode.Update)
            {
                _Person = clsPerson.Find(_PersonID);
                if (_Person == null)
                {
                    MessageBox.Show("this form will be closed because No Contatc with ID " + _PersonID + "");
                    this.Close();
                    return;
                }
                lblTitle.Text = "Update Person";
                
                _FillInformation();

                txtFirsName.Focus();    

            }

           
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AddUpdatePerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private bool _HandelPersonImmage()
        {
            if (pbxPersonImage.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != "")
                {
                    // delete the old immage
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }

                    catch {
                    //Error
                    }   

                }
                if(pbxPersonImage.ImageLocation != null)
                {
                    string sourceFile = pbxPersonImage.ImageLocation.ToString();
                    if(clsUtil.CopyImageToProjectImagesFolder(ref sourceFile)){
                        pbxPersonImage.ImageLocation= sourceFile;       
                        return true;    
                    }
                    else {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false; }  
                }
                return true;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandelPersonImmage())
                return;


            _Person.PersonID = _PersonID;
            _Person.NationalNO = txtNationalNo.Text;
            _Person.FirstName = txtFirsName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            if (rbMail.Checked)
                _Person.Gendor = 0;
            if (rbFemail.Checked)
                _Person.Gendor = 1;

            _Person.CountryID = clsCountry.Find(cbxCountry.Text).CountryID;
            if (pbxPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbxPersonImage.ImageLocation;
            }
            else
                _Person.ImagePath = "";
            if (_Person.Save())
            {
                MessageBox.Show("Data Added Successfuly ");
                _PersonID = _Person.PersonID;
                lblPersonID.Text = _PersonID.ToString();
                DataBack?.Invoke(this, _PersonID);

            }
            else
            {
                MessageBox.Show("Error Data is not  saved successfuly");
            }

           
        }

        private void SetImageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbxPersonImage.ImageLocation=selectedFilePath;
                linkRemove.Visible = true;
                // ...
            }


        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }


            if (txtNationalNo.Text.Trim() != _Person.NationalNO && clsPerson.IsPersonExist(txtNationalNo.Text)){

                e.Cancel= true; 
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "This NationalNO is Alredy Exist!");

            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtNationalNo, "");

            }
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void rbMail_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMail.Checked)
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_boy.png");
            if (rbFemail.Checked)
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_girl.png");

        }

        private void rbFemail_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMail.Checked)
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_boy.png");
            if (rbFemail.Checked)
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_girl.png");

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
                errorProvider1.SetError(Temp, null);

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void linkRemove_Click(object sender, EventArgs e)
        {
           
        }

        private void linkRemove_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbxPersonImage.ImageLocation = null;
            if (rbMail.Checked)
                pbxPersonImage.Image = Resources.Male_512;
            else
                pbxPersonImage.Image = Resources.Female_512;

            linkRemove.Visible = false;
        }
    }
}