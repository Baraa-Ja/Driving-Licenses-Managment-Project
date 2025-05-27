using Driver_License_Business_Layer.International_License;
using Driver_License_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer
{
    public class clsDrivers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsPeople PersonInfo;

        public clsDrivers()
        {
            this.DriverID = 0;
            this.PersonID = 0;
            this.CreatedByUserID = 0;
            this.CreatedDate = DateTime.Now;

        }

        private clsDrivers(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;
            this.PersonInfo = clsPeople.FindPerson(PersonID);
        }

        public static clsDrivers FindDriver(int DriverID)
        {
            int PersonID = 0 , CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.Now;

            bool isfound = clsDriversData.GetDriverByID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);

            if (isfound)
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public static clsDrivers FindDriverByPersonID(int PersonID)
        {
            int DriverID = 0,  CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.Now;

            bool isfound = clsDriversData.GetDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate);

            if (isfound)
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public bool AddNewDriver()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return DriverID != 0;
        }

        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return clsDriversData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static bool IsDriverExists(int PersonID)
        {
            bool isfound = clsDriversData.IsDriverExists(PersonID);

            return isfound;
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicenses.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
    }
}
