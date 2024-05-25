using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Bisness
{
    //internal class clsApplication
    //{
    //}



    public class clsApplication
    {
       public enum enMode { AddNew = 0, Update = 1 };
         public  enMode _Mode = enMode.AddNew;

        public  enum enApplicationType { enNewLocalDrivingApp=1,enRenew=2,
        enReplacmentForLost=3,enReplacmentForDemaged=4,
        enReleaseLicense=5, enInternationalApp = 6,
            enRetakeTest = 8
        }

        public enum enApplicationStatus { New=1,Cancel=2,Complete=3 }
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public string StutusText
        {
            get
            {
                switch (ApplicationStatus)
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
        }


        private clsPerson _PersonInfo;

        private clsApplicationType _ApplicationTypeInfo;    
        public clsPerson PersonInfo { 
         
            get { return _PersonInfo; } 
          
        }

        public string PersonFullName
        {
            get
            {
                return PersonInfo.FullName();
            }
        }

       

        private clsUser _UserInfo;  

        public clsUser UserInfo { get { return _UserInfo; } }

        public clsApplicationType ApplicationTypeInfo
        {

            get { return _ApplicationTypeInfo; }

        }
        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Parse("01/01/1111");
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = -1;
            this.LastStatusDate = DateTime.Parse("01/01/1111");
            this.PaidFees = -1;
            this.CreatedByUserID = -1;

            _Mode = enMode.AddNew;
        }
        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;

            _ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);

            _UserInfo=clsUser.Find(CreatedByUserID);

            _PersonInfo = clsPerson.Find(ApplicantPersonID);
            _Mode = enMode.Update;
        }
        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Parse("01/01/1111");
            int ApplicationTypeID = -1;
            int ApplicationStatus = -1;
            DateTime LastStatusDate = DateTime.Parse("01/01/1111");
            float PaidFees = -1;
            int CreatedByUserID = -1;
            if (clsApplicationData.GetApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else { return null; };
        }
        bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }
        bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static DataTable GetAllApplication()
        {
            return clsApplicationData.GetAllApplication();
        }

        

         public static DataTable GetAllApplicationInMyView()
        {
            return clsApplicationData.GetAllApplicationInMyView();
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

      

        public static bool IsApplicationExist(int ApplicantPersonID, int ApplicationTypeID, int ApplicationStatus)
        {
            return clsApplicationData.IsApplicationExist(ApplicantPersonID, ApplicationTypeID, ApplicationStatus);  
        }

        public bool CompleteAppication()
        {
            this.ApplicationStatus = 3;

            return this.Save();
        }
        public static int GetActiveApplicaionIDForLicence(int PersonID, enApplicationType
            ApplicationTypeID, int LiceseClassID)
        {
            return clsApplicationData.GetActiveApplicaionIDForLicence(PersonID, (int)ApplicationTypeID, LiceseClassID);
        }

         public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }



        public static bool DoesPersonHaveAnActiveApplication(int PersonID, enApplicationType ApplicationTypeID, int LiceseClassID)
        {
            return clsApplicationData.DoesPersonHaveAnActiveApplication(PersonID, (int)ApplicationTypeID, LiceseClassID);    
        }

        public bool DoesPersonHaveAnActiveApplication(enApplicationType ApplicationTypeID, int LiceseClassID)
        {
            return DoesPersonHaveAnActiveApplication(this.ApplicantPersonID, ApplicationTypeID, LiceseClassID);
        }





        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateApplication();
                default: return false;

            }
        }
    }

}
