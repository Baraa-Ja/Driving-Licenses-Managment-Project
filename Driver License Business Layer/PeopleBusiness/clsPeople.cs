using Driver_License_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer
{
    public  class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1};
        enMode _Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName {  get; set; }
        public string ThirdName {  get; set; } 
        public string LastName {  get; set; }

        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public DateTime DateOfBirth {  get; set; }
        public short Gendor {  get; set; }
        public string Address {  get; set; }
        public string Phone { get; set; }   
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsCountries CountryInfo;

        public string PersonFullname
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public clsPeople()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.LastName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Email = "";
            this.Phone = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
          
            _Mode = enMode.AddNew;
        }

        private clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, short Gendor, string Address,
            string Email, string Phone, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Email = Email;
            this.Phone = Phone;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountries.Find(NationalityCountryID);

            _Mode = enMode.Update;
        }

        public static clsPeople FindPerson(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "",
            Address = "", Email = "", Phone = "", ImagePath = "";

            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool isfound = clsPeopleData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Email, ref Phone,
                ref NationalityCountryID, ref ImagePath);

            if(isfound)
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                    Gendor, Address, Email,
                    Phone, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPeople FindPerson(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "",
            Address = "", Email = "", Phone = "", ImagePath = "";

            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1, PersonID = -1;
            Byte Gendor = 0;

            bool isfound = clsPeopleData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName,
                ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Email, ref Phone,
                ref NationalityCountryID, ref ImagePath);

            if (isfound)
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Email,
                    Phone, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.Gendor, this.Email, this.Phone, this.Address, this.DateOfBirth,
                this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePersonInfo(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Email, this.Phone,
                this.NationalityCountryID, this.ImagePath);
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();

        }

        public static bool _DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

        public static bool _IsPersonExists(int PersonID)
        {
            return clsPeopleData.CheckPersonExists(PersonID);
        }

        public static bool _IsPersonExists(string NationalNo)
        {
            return clsPeopleData.IsPersonExist(NationalNo);
        }


        //public static bool _IsPersonConnectedWithUser(int PersonID)
        //{

        //}

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if(_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();
            }

            return false;
        }

    }
}
