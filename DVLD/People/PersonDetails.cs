using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class PersonDetails : Form
    {
        private int _PersonID;
        public PersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID= PersonID;    

        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {
            ctrPersonCard1.LoadInfoPerson(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
