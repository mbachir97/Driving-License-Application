using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.SymbolStore;

namespace DVLD_DataAccess
{
    public static  class clsUserData
    {

        public static bool GetUserbyID(int ID, ref int PersonID, ref string UserName,
            ref string PassWord, ref bool IsActive
        )
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@UserID", ID);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserName = (string)reader["UserName"];
                    PassWord = (string)reader["PassWord"];
                    IsActive = (bool)reader["IsActive"];
                    PersonID = (int)reader["PersonID"];




                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }

        public static DataTable GetDataWithFilter(string Filter, string Like)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "Select * from (select Users.UserID,Users.PersonID,People.FirstName " +
                "+' '+ People.SecondName +' '+People.ThirdName +' '+ People.LastName " +
                "as FullName, Users.UserName,Users.IsActive from Users " +
                "join People on Users.PersonID=People.PersonID)R1 " +
                "Where " + Filter + " like @Like+'%' ";
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


        public static bool GetUserbyUserName(ref int ID, ref int PersonID, string UserName,
            ref string PassWord, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from Users where UserName=@UserName";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int)reader["UserID"];
                    PassWord = (string)reader["PassWord"];
                    IsActive = (bool)reader["IsActive"];
                    PersonID = (int)reader["PersonID"];


                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }

        public static bool GetUserbyUserNameandPassWord(ref int ID, ref int PersonID, string UserName,
            string PassWord, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select * from Users where UserName=@UserName and PassWord=@PassWord";
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PassWord", PassWord);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];



                }

            }
            catch (Exception ex)
            {

            }
            finally { Connection.Close(); }
            return IsFound;
        }





        public static int AddNewUsere(string UserName,
            string PassWord, bool IsActive, int PersonID)
        {

            int UsereID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"INSERT INTO Users (PersonID,UserName, 
                             PassWord,IsActive)
                                 VALUES (@PersonID,@UserName,@PassWord,@IsActive);
                                 SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PassWord", PassWord);
            command.Parameters.AddWithValue("@IsActive", IsActive);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UsereID = insertedID;
                }

            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }
            finally { connection.Close(); }

            return UsereID;



        }


        public static bool UpdateUsere(int ID, string UserName,
            string PassWord, bool IsActive, int PersonID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Update  Users  
                                set   UserName=@UserName,

                                      PassWord = @PassWord, 
                                      PersonID=@PersonID,
                                      IsActive=@IsActive

                                    where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", ID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PassWord", PassWord);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@IsActive", IsActive);


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

        public static bool DeleteUser(int UsereID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string query = @"Delete Users 
                                    where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UsereID);

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


        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select Users.UserID,Users.PersonID,People.FirstName " +
                "+' '+ People.SecondName +' '+People.ThirdName +' '+ People.LastName " +
                "as FullName, Users.UserName,Users.IsActive from Users " +
                "join People on Users.PersonID=People.PersonID";
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

        public static DataTable GetActiveUser(bool IaActive)
        {
            short isactive = Convert.ToInt16(IaActive);
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "Select * from (select Users.UserID,Users.PersonID,People.FirstName " +
                "+' '+ People.SecondName +' '+People.ThirdName +' '+ People.LastName " +
                "as FullName, Users.UserName,Users.IsActive from Users " +
                "join People on Users.PersonID=People.PersonID)R1 " +
                "Where IsActive = @IsActive ";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@IsActive", isactive);
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

        public static bool IsUsertExist(int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     Users where UserID=@UserID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUsertExistbyPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     Users where PersonID=@PersonID ";

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

        public static bool IsUsertExist(string UserName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     Users where UserName=@UserName ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

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

        public static bool IsUsertExist(string UserName, string PassWord)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string query = "select found=1 from     Users where UserName=@UserName " +
                "and PassWord=@PassWord ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PassWord", PassWord);

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
