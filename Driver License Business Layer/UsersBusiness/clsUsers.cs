using Driver_License_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean IsACtive { get; set; }
        public int PersonID { get; set; }

        public clsPeople PersonInfo;

        public clsUsers()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsACtive = false;
            this.PersonID = -1;

            _Mode = enMode.AddNew;
        }

        private clsUsers(int UserID, string UserName, string PassWord, Boolean IsActive, int PersonID)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = PassWord;
            this.IsACtive = IsActive;
            this.PersonID = PersonID;
            this.PersonInfo = clsPeople.FindPerson(PersonID);

            _Mode = enMode.Update;
        }

        public static clsUsers FindUser(int UserID)
        {
            string UserName = "", PassWord = "";
            int PersonID = -1;
            Boolean IsActive = false;

            bool isfound = clsUsersData.GetUserInfoByID(UserID, ref UserName, ref PassWord, ref IsActive, ref PersonID);

            if (isfound)
            {
                return new clsUsers(UserID, UserName, PassWord, IsActive, PersonID);
            }
            else
            {
                return null;
            }
        }

        public static clsUsers FindUser(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            Boolean IsActive = false;

            bool Isfound = clsUsersData.GetUserInfoByUserNameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive);

            if (Isfound)
            {
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            }
            else
            {
                return null;
            }

        }

        public static clsUsers FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = clsUsersData.GetUserInfoByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUsers(UserID, UserName, Password, IsActive, PersonID);
            else
                return null;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(this.UserName, this.Password, this.IsACtive, this.PersonID);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUserInfo(this.UserID, this.UserName, this.Password, this.IsACtive, this.PersonID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();

        }

        public static bool _DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static bool _IsUserExists(int UserID)
        {
            return clsUsersData.CheckUserExists(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUsersData.IsUserExist(UserName);
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
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();
            }

            return false;
        }

        public static int IsUserConnectedWithPerson(int PersonID)
        {
           int UserID =  clsUsersData.isUserConnectedWithPersonData(PersonID);
           return UserID;
        }

        public bool ChangePassword(string NewPassword)
        {
            return clsUsersData.ChangePassword(this.UserID, NewPassword);
           
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistForPersonID(PersonID);
        }

    }
}