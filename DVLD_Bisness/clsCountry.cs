using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Bisness
{
    public  class clsCountry
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;
        public int CountryID { get; set; }
       public string CountryName { get; set; }

        public clsCountry() {
            CountryID = -1;

            CountryName = "";

            _Mode = enMode.AddNew;


        }

         private  clsCountry(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
            _Mode = enMode.Update;
        }

        public static clsCountry Find(int CountryID)
        {
           

          string   CountryName = "";

            if (clsCountryData.GetCountryID( CountryID,ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);

            }

            else
            {
                return null;
            }
        }

        public static clsCountry Find(string  CountryName)
        {


            int  CountryID =-1;

            if (clsCountryData.GetCountryBYCountryName(ref CountryID,  CountryName))
            {
                return new clsCountry(CountryID, CountryName);

            }

            else
            {
                return null;
            }
        }

        public   static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }



    }
}
