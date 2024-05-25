using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bisness
{
    //internal class clsInternationalLincense
    //{
    //}



    public class clsInternationalLicense
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        private clsLicense _LicenseInfo;

        public clsLicense LicenseInfo { get { return _LicenseInfo; } }  

        public int CreatedByUserID { get; set; }
        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Parse("01/01/1111");
            this.ExpirationDate = DateTime.Parse("01/01/1111");
            this.IsActive = false;
            this.CreatedByUserID = -1;
            _Mode = enMode.AddNew;
        }
        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            _LicenseInfo = clsLicense.Find(IssuedUsingLocalLicenseID);
            _Mode = enMode.Update;
        }
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Parse("01/01/1111");
            DateTime ExpirationDate = DateTime.Parse("01/01/1111");
            bool IsActive = false;
            int CreatedByUserID = -1;
            if (clsInternationalLicenseData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else { return null; };
        }


        public static clsInternationalLicense FindByDriverID(int DriverID )
        {
            int ApplicationID = -1;
            int IssuedUsingLocalLicenseID = -1;
            int InternationalLicenseID = -1;
            DateTime IssueDate = DateTime.Parse("01/01/1111");
            DateTime ExpirationDate = DateTime.Parse("01/01/1111");
            bool IsActive = false;
            int CreatedByUserID = -1;
            if (clsInternationalLicenseData.GetInternationalLicenseByDreiverID
                (ref InternationalLicenseID, ref ApplicationID,  DriverID, ref  IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else { return null; };
        }

        bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseID != -1);
        }
        bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteInternationalLicense(InternationalLicenseID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }
        public static DataTable GetAllInternationalLicense(int DriverID)
        {
            return clsInternationalLicenseData.GetAllInternationalLicense(DriverID);
        }

        public static DataTable GetAllInternationalLicense()
        {
            return clsInternationalLicenseData.GetAllInternationalLicense();
        }
        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseExist(InternationalLicenseID);
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewInternationalLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateInternationalLicense();
                default: return false;

            }
        }
    }
}
