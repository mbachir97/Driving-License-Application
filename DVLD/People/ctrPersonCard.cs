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
using DVLD.People;
using DVLD.Properties;
using DVLD_Bisness;

namespace DVLD.Controles
{
    public partial class ctrPersonCard : UserControl
    {
        private clsPerson _Person;

        public clsPerson SelectPerson
        {
            get { return _Person; }
        }

        private int  _PersonID=-1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public ctrPersonCard()
        {
            InitializeComponent();
        }

        
        private void ctrPersonCard_Load(object sender, EventArgs e)
        {

        }

        public void LoadInfoPerson(int PersonID)
        {
            
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person With PersonID ="+PersonID.ToString()
                    ,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            else
            {
                _PersonID = PersonID;

                _FillPersonInfo();
            }

        }

        public void LoadInfoPerson(string NationalNo)
        {
           
           
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                MessageBox.Show("No Person With NationalNo =" + NationalNo.ToString()
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                linkLabel1.Enabled = true;
                _PersonID = _Person.PersonID;
                _FillPersonInfo();
            }

        }

        private void _FillPersonInfo()
        {
            linkLabel1.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString(); 
            lblNationalNO.Text= _Person.NationalNO;
            lblName.Text = _Person.FullName();
            if (_Person.Gendor == 0)
            {
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_boy.png");
                lblGendor.Text = "Male";
            }
            else
            {
                pbxPersonImage.Image = Image.FromFile(@"C:\Users\Win\Desktop\Pro19\Picture And Icones\person_girl.png");

                lblGendor.Text = "FeMale";
            }

            lblEmail.Text = _Person.Email;  
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text=_Person.DateOfBirth.ToShortDateString();

            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblPhone.Text = _Person.Phone;  

            if(_Person.ImagePath != "") {
                if (File.Exists(_Person.ImagePath))
                {
                    pbxPersonImage.ImageLocation=_Person.ImagePath;
                }

            }
            
           

            

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddUpdatePerson Add = new AddUpdatePerson(_PersonID);
            Add.ShowDialog();
            LoadInfoPerson(_PersonID);

        }
    }
}
