using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;

namespace DVLD_Bisness
{
    //internal class clsLicense
    //{
    //}



    public class clsLicense
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        //1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.

        public enum enIssueReason { FirstTime = 1, Renew = 2 , ReplacementForDamaged = 3, ReplacementForLost=4 }

         
        

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }

        private clsLicenseClass _ClassInfo;

       

        public clsLicenseClass ClassInfo
        {
            get { return _ClassInfo; }
        }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }

        private clsDriver _driverInfo ;

        private clsUser _UserInfo;
        private clsDetainedLicense _detainInfo;

        public clsUser UserInfo {get { return _UserInfo; } }    

        public clsDriver DriverInfo
        {
            get { return _driverInfo; }
        }

        public int IssueReason { get; set; }

        string GetIssueReasonText(enIssueReason Reason)
        {
            switch(Reason)
            {
                case enIssueReason.FirstTime:
                    return "FirstTime";
                case enIssueReason.Renew:
                    return "Renew";

                case enIssueReason.ReplacementForDamaged:
                    return "ReplacementForDemaged";
                case enIssueReason.ReplacementForLost:
                    return "ReplacmentForLost";
                default:
                    return "FirstTime";

            }

        }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText((enIssueReason)this.IssueReason);
            }
        }

       public clsDetainedLicense DetainInfo
        {
            get; set;   

        }




        public bool IsLicenseDetained
        {
            get
            {
              return   clsDetainedLicense.IsLicenseDetained(this.LicenseID); 
            }
        }




        public int CreatedByUserID { get; set; }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Parse("01/01/1111");
            this.ExpirationDate = DateTime.Parse("01/01/1111");
            this.Notes = "";
            this.PaidFees = -1;
            this.IsActive = false;
            this.IssueReason = -1;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;

            _driverInfo = clsDriver.Find(DriverID);

            this.DetainInfo =clsDetainedLicense.FindbyLicenseID(LicenseID);  

            _ClassInfo = clsLicenseClass.Find(LicenseClass);
            this.CreatedByUserID = CreatedByUserID;
            _UserInfo = clsUser.Find(CreatedByUserID);
            _Mode = enMode.Update;
        }
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Parse("01/01/1111");
            DateTime ExpirationDate = DateTime.Parse("01/01/1111");
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            int IssueReason = -1;
            int CreatedByUserID = -1;
            if (clsLicenseData.GetLicenseByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else { return null; };
        }

        public static clsLicense FindByAppID(int ApplicationID)
        {
            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Parse("01/01/1111");
            DateTime ExpirationDate = DateTime.Parse("01/01/1111");
            string Notes = "";
            float PaidFees = -1;
            bool IsActive = false;
            int IssueReason = -1;
            int CreatedByUserID = -1;
            if (clsLicenseData.GetLicenseByAppplicatioID(ref LicenseID,  ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else { return null; };
        }
        bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, 
                this.LicenseClass, this.IssueDate,
                this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,
                this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }
        bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, 
                this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive, this.IssueReason,
                this.CreatedByUserID);
        }

        public bool IsDetained()
        {
            clsDetainedLicense Detained = clsDetainedLicense.FindbyLicenseID(this.LicenseID);

            if (Detained == null) {
            return false;
            }

            return (!Detained.IsReleased);
        }

        public bool DeActivateLicense()
        {
            this.IsActive = false;
            return this.Save();
        }
        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }

        public static bool IsLicenseExistbyAppID(int AppID)
        {
            return clsLicenseData.IsLicenseExistbyAppID(AppID);    
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateLicense();
                default: return false;

            }
        }


        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public Boolean IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool DeActivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        public bool ReleaseDetainedLicense(int RelaeseByUserID , ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();  

            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.enReleaseLicense;
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.CreatedByUserID = RelaeseByUserID;
            Application.ApplicationStatus = (int)clsApplication.enApplicationStatus.Complete;

            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.enReleaseLicense).ApplicationFees;

            if(!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID; 

            return this.DetainInfo.ReleaseDetainedLicense(RelaeseByUserID, ApplicationID);




        }

        public clsLicense RenewLicense(string Note,int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.enRenew;
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.CreatedByUserID = CreatedByUserID;
            Application.ApplicationStatus = (int)clsApplication.enApplicationStatus.Complete;

            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.enRenew)
                .ApplicationFees;
            if(!Application.Save()) { 
              return null ; 
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID= Application.ApplicationID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueReason = (int)clsLicense.enIssueReason.Renew;
            NewLicense.IssueDate = DateTime.Now;    
            NewLicense.IsActive= true;  
            NewLicense.Notes= Note; 
            NewLicense.DriverID= this.DriverID;
            NewLicense.ExpirationDate= DateTime.Now.AddYears(this.ClassInfo.DefaultValidityLength);
            NewLicense.CreatedByUserID= CreatedByUserID;    
            NewLicense.PaidFees = this.ClassInfo.ClassFees;
            
            if(!NewLicense.Save()) { return null ; }

            DeActivateCurrentLicense(); 

            return NewLicense ;


        }


        public clsLicense Replace(enIssueReason Issuereason, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (Issuereason == enIssueReason.ReplacementForDamaged) ?
                (int)clsApplication.enApplicationType.enReplacmentForDemaged :
                (int)clsApplication.enApplicationType.enReplacmentForLost;

            Application.ApplicationStatus = (int)clsApplication.enApplicationStatus.Complete;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = (int)Issuereason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if(!NewLicense.Save()) { return null; } 

            DeActivateCurrentLicense();
            return NewLicense;  


        }




    }
}
