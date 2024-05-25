using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonByID(int ID, ref string NationalNo, ref string FirstName, ref string SecondName,
              ref string ThirdName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth,
         ref short Gendor, ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from People where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    CountryID = (int)reader["NationalityCountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                        ImagePath = "";



                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }




        public static bool GetPersonbyNationalNO(ref int ID, string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth,
        ref short Gendor, ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from People where NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];

                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    Gendor = Convert.ToInt16(reader["Gendor"]);
                    CountryID = (int)reader["NationalityCountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                        ImagePath = "";



                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }






        public static int AddNewPersone(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, string Email,
           short Gendor, int CountryID, string Phone, string Address, DateTime DateOfBirth, string ImagePath)
        {

            int PersoneID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"INSERT INTO People (NationalNo,FirstName, 
                         SecondName,ThirdName,LastName, DateOfBirth,Gendor,Address, Phone, 
                     Email ,NationalityCountryID,ImagePath)
                             VALUES (@NationalNo,@FirstName,@SecondName,@ThirdName, @LastName,
                                   @DateOfBirth,@Gendor,@Address, @Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersoneID = insertedID;
                }

            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally { connection.Close(); }

            return PersoneID;



        }


        public static bool UpdatePersone(int ID, string NationalNo, string FirstName,
                                  string SecondName, string ThirdName, string LastName, string Email,
                                   short Gendor, int CountryID, string Phone,
                                   string Address, DateTime DateOfBirth, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  People  
                            set   NationalNo=@NationalNo,

                                  FirstName = @FirstName, 
                                  SecondName=@SecondName,
                                  ThirdName=@ThirdName,
                                  LastName = @LastName, 
                                  DateOfBirth =@DateOfBirth, 
                                  Gendor=@Gendor,
                                  Address = @Address, 

                                  Phone = @Phone,
                                  Email = @Email, 
                                  NationalityCountryID=@NationalityCountryID,

                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);


            if (ImagePath != "")
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePerson(int PersoneID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersoneID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }


        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "SELECT People.PersonID , People.NationalNo, People.FirstName, " +
                "People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, " +
                "Gendor= CASE  " +
                " WHEN Gendor=0 THEN 'Male'   " +
                " WHEN Gendor=1 THEN 'Female'    ELSE 'Unknown' END, People.Phone, People.Email," +
                " Countries.CountryName as Nationality FROM   Countries INNER JOIN " +
                " People ON Countries.CountryID = People.NationalityCountryID";
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

        public static DataTable GetDataWithFilter(string Filter, string Like)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "Select * from (SELECT People.PersonID , People.NationalNo, People.FirstName, " +
                "People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, " +
                "Gendor= CASE  " +
                " WHEN Gendor=0 THEN 'Male'   " +
                " WHEN Gendor=1 THEN 'Female'     ELSE 'Unknown' END , People.Phone, People.Email," +
                " Countries.CountryName as Nationality FROM   Countries INNER JOIN " +
                " People ON Countries.CountryID = People.NationalityCountryID)R1 " +
                "Where " + Filter + " like  @Like+'%' ";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@Like", Like);
            //Command.Parameters.AddWithValue("@Filter", Filter);


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


        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     People where PersonID=@PersonID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     People where NationalNo=@NationalNo ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }





    }

}
