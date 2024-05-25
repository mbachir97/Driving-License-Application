using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.People;
using DVLD_Bisness;

namespace DVLD.Controles
{

    public partial class ctrPersonCardWithFilter : UserControl
    {

        private int _PersonID=-1;


        //Expose A Property
        public int PersonID
        {

            get
            {

                return ctrPersonCard1.PersonID;
            }
        




        }


        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }


        public clsPerson Person
        {
            get { return ctrPersonCard1.SelectPerson; }
        }



        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;

            if(handler != null)
            {
                handler(personID);
            } 
        }
        public ctrPersonCardWithFilter()
        {
            InitializeComponent();
        }


        //My Methode
        public void LoadInformation(int PersonID)
        {
            ctrPersonCard1.LoadInfoPerson(PersonID);
            cbxFindBy.SelectedIndex = 0;
            txtFilterValue.Text=PersonID.ToString();
            gbFilter.Enabled = false;
        }


        public void LoadInformation1(int PersonID)
        {
            txtFilterValue.Text= PersonID.ToString();       
            cbxFindBy.SelectedIndex = 0;
            FindNow();
        }
        private void ctrPersonCardWithFilter_Load(object sender, EventArgs e)
        {
           
           
             
        }



        private void FindNow()
        {
            switch (cbxFindBy.Text)
            {
                case "Person ID":
                    ctrPersonCard1.LoadInfoPerson(int.Parse(txtFilterValue.Text));

                    break;

                case "National No":
                    ctrPersonCard1.LoadInfoPerson(txtFilterValue.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                PersonSelected(ctrPersonCard1.PersonID);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clsPerson _Person;

            if (cbxFindBy.SelectedIndex >=0)
            {
                
                if(cbxFindBy.Text.ToLower()=="personid")
                {
                   int  id =Convert.ToInt32(txtFilterValue.Text);    
                     
                        _Person = clsPerson.Find(id);
                        ctrPersonCard1.LoadInfoPerson(id);
                    if (_Person != null)
                    {
                        _PersonID = _Person.PersonID;
                    }
                    else
                        return;
                        //_PersonID = Convert.ToInt32(textBox1.Text);
                   
                }

                if (cbxFindBy.Text.ToLower() == "nationalno")
                {
                    _Person = clsPerson.Find(txtFilterValue.Text);
                    ctrPersonCard1.LoadInfoPerson(txtFilterValue.Text);
                    if (_Person != null)
                    {
                        _PersonID =_Person.PersonID ;
                    }
                   
                     else 
                        return; 
                    
                    
                }

                if(OnPersonSelected != null)
                    OnPersonSelected(_PersonID);
            }

            else
            {
                MessageBox.Show("Select A Filter First");
            }
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {
               
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddUpdatePerson add = new AddUpdatePerson(-1);
            add.DataBack += Add_DataBack;
            add.ShowDialog();
         
        }

        private void Add_DataBack(object Sender, int PersonID)
        {
            _PersonID=PersonID;
            ctrPersonCard1.LoadInfoPerson(_PersonID);
            txtFilterValue.Text = _PersonID.ToString();
            cbxFindBy.SelectedIndex = 0;
            OnPersonSelected(PersonID);
            //we can also call the methode LoadInformation1(PersonID) it makes the same think
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (cbxFindBy.Text.ToLower() == "person id")
            {
                if (!char.IsDigit(ch) && ch != 8 && ch != 46)

                {
                    e.Handled = true;
                }
            }
        }


        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            FindNow();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            AddUpdatePerson add = new AddUpdatePerson(-1);
            add.DataBack += Add_DataBack;
            add.ShowDialog();
        }
    }
}
