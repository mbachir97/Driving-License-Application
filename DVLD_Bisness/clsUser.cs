using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Bisness
{
    public  class clsUser
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool IsActive { get; set; }  

        public clsUser() 
        {
            this.UserID = -1;
            this.UserName = "";
            this.PassWord = "";
            PersonID = -1;  
            IsActive = false;
            _Mode = enMode.AddNew;

        }

        private clsUser(int userID, string userName, string passWord,bool IsActive,int Personid)
        {
            UserID = userID;
            PersonID = Personid;
            UserName = userName;
            PassWord = passWord;
            this.IsActive = IsActive;   
            _Mode = enMode.Update;
        }

        public static clsUser Find(int UserID)
        {
            string UserName = "", PassWord = "";
            bool IsActive = false;
            int PersonID = -1;

            if(clsUserData.GetUserbyID(UserID,ref PersonID,ref UserName,
                ref PassWord,ref IsActive))
            {
                return new  clsUser(UserID,UserName,PassWord,IsActive,PersonID); 

            }

            else
            {
                return null;    
            }
        }


        public static clsUser Find(string  UserName)
        {
            int UserID = -1;string  PassWord = "";
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.GetUserbyUserName(ref UserID, ref PersonID,  UserName,
                ref PassWord, ref IsActive))
            {
                return new clsUser(UserID, UserName, PassWord, IsActive, PersonID);

            }

            else
            {
                return null;
            }
        }


        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);  
        }

        public static clsUser Find(string UserName,string PassWord)
        {
            int UserID = -1; 
            bool IsActive = false;
            int PersonID = -1;

            if (clsUserData.GetUserbyUserNameandPassWord(ref UserID, ref PersonID, UserName,
                 PassWord, ref IsActive))
            {
                return new clsUser(UserID, UserName, PassWord, IsActive, PersonID);

            }

            else
            {
                return null;
            }
        }


        bool _AddNewUser()
        {
            this.UserID=clsUserData.AddNewUsere(this.UserName,this.PassWord
                ,this.IsActive,this.PersonID);

            return (this.UserID != -1);
        }

        bool _UpdateUser()
        {
            return clsUserData.UpdateUsere(this.UserID, this.UserName, this.PassWord
                , this.IsActive, this.PersonID);
        }

        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateUser();
                default: return false;

            }
        }

        public static DataTable GetAllUser()
        {
            return clsUserData.GetAllUsers();
        }

        public static DataTable GetDataWithFilter(string Filter, string Like)
        {
            return clsUserData.GetDataWithFilter(Filter, Like);
        }

        public static  DataTable GetActiveUser(bool IsActive)
        {
            return clsUserData.GetActiveUser(IsActive);
        }


        public static  bool IsUserExist(string UserName)
        {
            return clsUserData.IsUsertExist(UserName);
        }
        public static bool IsUserExist(string UserName, string PassWord)
        {
            return clsUserData.IsUsertExist(UserName,PassWord);
        }
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUsertExist(UserID);
        }


        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsUsertExistbyPersonID(PersonID);    
        }

    }
}
