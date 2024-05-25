using DVLD.People;
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
using System.Windows.Forms.VisualStyles;

namespace DVLD.Controles
{
    public partial class ctrApplicationBasicInfo : UserControl
    {

      

        private clsApplication _Application;


        public ctrApplicationBasicInfo()
        {
            InitializeComponent();
        }


        private int _ApplicationID=-1;
      public   void _ResetValue()
        {
            lblID.Text = "[????]";
            lblDate.Text = "[????]";
            lblApplicant.Text = "[????]";
           lblCreatedby.Text = "[????]";
            lblFees.Text = "[????]";
            lblStatus.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblType.Text = "[????]";
            

        }

        string _ReturnStatus(int Status)
        {
            switch (Status)
            {
                case 1:
                    return "New";
                case 2:
                    return "Canceled";
                case 3:
                    return "Completed";

                default:
                    return "not Found";
            }
        }
        void _FillInformation()
        {
            lblID.Text = _Application.ApplicationID.ToString();
            lblDate.Text = _Application.ApplicationDate.ToShortDateString();
            lblApplicant.Text = _Application.PersonInfo.FullName();
            lblCreatedby.Text = _Application.UserInfo.UserName;
            lblFees.Text = _Application.ApplicationTypeInfo.ApplicationFees.ToString();
            lblStatus.Text = _ReturnStatus(_Application.ApplicationStatus);
          //  lblStatus.Text = _Application.StutusText; instructor way 
            lblStatusDate.Text =_Application.LastStatusDate.ToShortDateString() ;
            lblType.Text = _Application.ApplicationTypeInfo.ApplicationTypeTitle ;
        }
        public void LoadApplicationInfo(int ApplicationID)
        {

            _ApplicationID=ApplicationID;   
            _Application =clsApplication.Find(_ApplicationID);    

            if (_Application == null)
            {
                _ResetValue();
                MessageBox.Show("there is no Applicationnn With ApplicationID "
                    + ApplicationID.ToString(), "Not Found", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);

                return ;    

            }
            else
            {
             
                _FillInformation();
            }
          

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetails Detail = new PersonDetails(_Application.ApplicantPersonID);
          
            Detail.ShowDialog();
            LoadApplicationInfo(_Application.ApplicationID);

        }
    }
}
