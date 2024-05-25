using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace DVLD_DataAccess
{
    public static  class clsCountryData
    {


        public static bool GetCountryID(int ID,ref string CountryName)
       
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from Countries where CountryID=@CountryID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)reader["CountryName"];
                   




                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }

        public static bool GetCountryBYCountryName(ref int ID,  string CountryName)

        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from Countries where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int )reader["CountryID"];





                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "Select * from Countries ";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();

            }

            catch { }
            finally
            {
                connection.Close();
            }

            return dt;
        }



    }
}
