using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bisness
{
    //internal class clsDetainedLicense
    //{
    //}

    public class clsDetainedLicense
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Parse("01/01/1111");
            this.FineFees = -1;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.Parse("01/01/1111");
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            _Mode = enMode.AddNew;
        }
        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            _Mode = enMode.Update;
        }
        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.Parse("01/01/1111");
            float FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Parse("01/01/1111");
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            if (clsDetainedLicenseData.GetDetainedLicenseByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else { return null; };
        }


        public static clsDetainedLicense FindbyLicenseID(int LicenseID)
        {

            int DetainID = -1;
           
            DateTime DetainDate = DateTime.Parse("01/01/1111");
            float FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Parse("01/01/1111");
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            if (clsDetainedLicenseData.GetDetainedLicenseByLicenseID(ref DetainID, LicenseID, ref DetainDate, 
                ref FineFees, ref CreatedByUserID, ref IsReleased, 
                ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, 
                    CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, 
                    ReleaseApplicationID);
            }
            else { return null; };
        }

        bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
            return (this.DetainID != -1);
        }
        bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }
        public static bool DeleteDetainedLicense(int DetainID)
        {
            return clsDetainedLicenseData.DeleteDetainedLicense(DetainID);
        }
        public static DataTable GetAllDetainedLicense()
        {
            return clsDetainedLicenseData.GetAllDetainedLicense();
        }
        public static bool IsDetainedLicenseExist(int DetainID)
        {
            return clsDetainedLicenseData.IsDetainedLicenseExist(DetainID);
        }


        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDetainedLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateDetainedLicense();
                default: return false;

            }
        }

        public  bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {

            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                ReleasedByUserID, ReleaseApplicationID);

                

        }
    }
}
