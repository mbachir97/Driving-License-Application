using DVLD.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void ctrPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void test_Load(object sender, EventArgs e)
        {
           
        }

        private void ctrUserCa1_Load(object sender, EventArgs e)
        {
           
        }

         void DevidNumbers(int X,int Y)
        {
            MessageBox.Show((X / Y).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DevidNumbers(12, 0);
            }

            catch (Exception ex) 
            {
                 
                clsUtil.Log("DVLD", "Application", ex.Message, EventLogEntryType.Error);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
