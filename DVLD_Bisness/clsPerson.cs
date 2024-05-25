using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccess;

namespace DVLD_Bisness
{
    public class clsPerson
    {

        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;

        public int PersonID { get; set; }

        public string NationalNO { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short Gendor { get; set; }
        public int CountryID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }

        public clsCountry CountryInfo;




        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }


        public clsPerson()
        {

            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.CountryID = -1;
            this.Gendor = -1;
            this.DateOfBirth = DateTime.Now;
            this.ImagePath = "";
            _Mode = enMode.AddNew;

        }

        private clsPerson(int personID, string nationalNO, string firstName,
            string secondName, string thirdName,
            string lastName, string email, short gendor, int countryID, string phone,
            string address, DateTime dateOfBirth, string imagePath)
        {

            PersonID = personID;
            NationalNO = nationalNO;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Email = email;
            Gendor = gendor;
            CountryID = countryID;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            ImagePath = imagePath;
            CountryInfo = clsCountry.Find(countryID);

            _Mode = enMode.Update;
        }



        public static clsPerson Find(int PersonID)
        {
            string nationalNO = "", firstName = "";
            string secondName = "", thirdName = "";
            string lastName = "", email = ""; short gendor = -1;
            int countryID = -1; string phone = "";
            string address = ""; DateTime dateOfBirth = DateTime.Now;
            string imagePath = "";

            if (clsPersonData.GetPersonByID(PersonID, ref nationalNO, ref firstName,
                ref secondName, ref thirdName, ref lastName, ref email, ref phone, ref address, ref dateOfBirth, ref gendor,
               ref countryID, ref imagePath))
            {
                return new clsPerson(PersonID, nationalNO, firstName, secondName,
                    thirdName, lastName, email, gendor, countryID, phone,
            address, dateOfBirth, imagePath);
            }
            else { return null; }


        }

        public static clsPerson Find(string nationalNO)
        {
            int PersonID = -1; string firstName = "";
            string secondName = "", thirdName = "";
            string lastName = "", email = ""; short gendor = -1;
            int countryID = -1; string phone = "";
            string address = ""; DateTime dateOfBirth = DateTime.Now;
            string imagePath = "";

            if (clsPersonData.GetPersonbyNationalNO(ref PersonID, nationalNO, ref firstName,
                ref secondName, ref thirdName, ref lastName, ref email, ref phone, ref address, ref dateOfBirth, ref gendor,
               ref countryID, ref imagePath))
            {
                return new clsPerson(PersonID, nationalNO, firstName, secondName,
                    thirdName, lastName, email, gendor, countryID, phone,
            address, dateOfBirth, imagePath);
            }
            else { return null; }


        }

        bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPersone(this.NationalNO, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.Email, this.Gendor,
                this.CountryID, this.Phone, this.Address, this.DateOfBirth, this.ImagePath
                   );
            return (this.PersonID != -1);

        }

        bool _UpdatePerson()
        {

            return clsPersonData.UpdatePersone(this.PersonID, this.NationalNO, this.FirstName,
                this.SecondName, this.ThirdName, this.LastName, this.Email, this.Gendor,
                this.CountryID, this.Phone, this.Address, this.DateOfBirth, this.ImagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {

            return clsPersonData.GetAllPeople();
        }
        public static DataTable GetDataWithFilter(string Filter, string Like)
        {
            return clsPersonData.GetDataWithFilter(Filter, Like);
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdatePerson();
                default: return false;

            }
        }


        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }









    }
    //public class clsPerson
    //{
    //    enum enMode { AddNew = 0, Update = 1 };
    //    enMode _Mode = enMode.AddNew;
    //    public int PersonID { get; set; }
    //    public string NationalNo { get; set; }
    //    public string FirstName { get; set; }
    //    public string SecondName { get; set; }
    //    public string ThirdName { get; set; }
    //    public string LastName { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //    public int Gendor { get; set; }
    //    public string Address { get; set; }
    //    public string Phone { get; set; }
    //    public string Email { get; set; }
    //    public int NationalityCountryID { get; set; }
    //    public string ImagePath { get; set; }
    //    public clsPerson()
    //    {
    //        this.PersonID = -1;
    //        this.NationalNo = "";
    //        this.FirstName = "";
    //        this.SecondName = "";
    //        this.ThirdName = "";
    //        this.LastName = "";
    //        this.DateOfBirth = DateTime.Now;
    //        this.Gendor = -1;
    //        this.Address = "";
    //        this.Phone = "";
    //        this.Email = "";
    //        this.NationalityCountryID = -1;
    //        this.ImagePath = "";
    //        _Mode = enMode.AddNew;
    //    }
    //    private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, int Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
    //    {
    //        this.PersonID = PersonID;
    //        this.NationalNo = NationalNo;
    //        this.FirstName = FirstName;
    //        this.SecondName = SecondName;
    //        this.ThirdName = ThirdName;
    //        this.LastName = LastName;
    //        this.DateOfBirth = DateOfBirth;
    //        this.Gendor = Gendor;
    //        this.Address = Address;
    //        this.Phone = Phone;
    //        this.Email = Email;
    //        this.NationalityCountryID = NationalityCountryID;
    //        this.ImagePath = ImagePath;
    //        _Mode = enMode.Update;
    //    }
    //    public static clsPerson Find(int PersonID)
    //    {
    //        string NationalNo = "";
    //        string FirstName = "";
    //        string SecondName = "";
    //        string ThirdName = "";
    //        string LastName = "";
    //        DateTime DateOfBirth = DateTime.Now;
    //        int Gendor = -1;
    //        string Address = "";
    //        string Phone = "";
    //        string Email = "";
    //        int NationalityCountryID = -1;
    //        string ImagePath = "";
    //        if (clsPersonData.GetPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
    //        {
    //            return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
    //        }
    //        else { return null; };
    //    }
    //    bool _AddNewPerson()
    //    {
    //        this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
    //        return (this.PersonID != -1);
    //    }
    //    bool _UpdatePerson()
    //    {
    //        return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
    //    }
    //    public static bool DeletePerson(int PersonID)
    //    {
    //        return clsPersonData.DeletePerson(PersonID);
    //    }
    //    public static DataTable GetAllPerson()
    //    {
    //        return clsPersonData.GetAllPerson();
    //    }
    //    public static bool IsPersonExist(int PersonID)
    //    {
    //        return clsPersonData.IsPersonExist(PersonID);
    //    }
    //    public bool Save()
    //    {

    //        switch (_Mode)
    //        {
    //            case enMode.AddNew:

    //                if (_AddNewPerson())
    //                {
    //                    _Mode = enMode.Update;
    //                    return true;
    //                }
    //                else return false;
    //            case enMode.Update:
    //                return _UpdatePerson();
    //            default: return false;

    //        }
    //    }
    //}
}
        

        
