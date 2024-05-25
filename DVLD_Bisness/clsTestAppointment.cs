using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DVLD_DataAccess;

namespace DVLD_Bisness
{
    //internal class clsTestAppointment
    //{
    //}

    public class clsTestAppointment
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseInfo;

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseInfo
        {

            get { return _LocalDrivingLicenseInfo;}

        }

        public clsApplication RetakeTestAppInfo;
        public int TestID
        {
            get { return _GetTestID(); }

        }


        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Parse("01/01/1111");
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            _Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            RetakeTestAppInfo = clsApplication.Find(RetakeTestApplicationID);

            _LocalDrivingLicenseInfo = clsLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            _Mode = enMode.Update;
        }
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Parse("01/01/1111");
            float PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;
            if (clsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else { return null; };
        }
        bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }
        bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }
        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }
        public static DataTable GetAllTestAppointment(int TestTypeID,int LocalAppID)
        {
            return clsTestAppointmentData.GetAllTestAppointment(TestTypeID, LocalAppID);
        }


        public static DataTable GetAppointment()
        {
            return clsTestAppointmentData.GetAppointment(); 
        }

        public bool LockedAppointment()
        {
            this.IsLocked = true;
            return this.Save();
        }
        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(TestAppointmentID);
        }

        public static bool IsTestAppointmentExist(int TestTypeID,int LocalAppID)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(TestTypeID, LocalAppID);
        }

        public static bool IsTestAppointmentExist(int TestTypeID, int LocalAppID,bool IsLocked)
        {
            return clsTestAppointmentData.IsTestAppointmentExist(TestTypeID, LocalAppID, IsLocked);
        }

        public static bool  IsTestPassed(int TestTypeID, int LocalAppID)
        {
            return clsTestAppointmentData.IsTestPass(TestTypeID, LocalAppID);
        }


        public static int ReturnTrial(int TestTypeID, int LocalAppID)
        {
           return clsTestAppointmentData.ReturnTrial(TestTypeID, LocalAppID);   
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateTestAppointment();
                default: return false;

            }
        }

          private int  _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }

}
