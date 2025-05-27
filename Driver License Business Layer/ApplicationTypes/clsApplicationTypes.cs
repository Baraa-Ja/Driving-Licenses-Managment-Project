using Driver_License_DataAccess_Layer;
using Driver_License_DataAccess_Layer.ApplicationsDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer
{
    public class clsApplicationTypes
    {

        public enum EnApplicationTypes { NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2, ReplacementforaLostDrivingLicense = 3,
            ReplacementforaDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, ReTakeTest = 8 };

        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;
        public int ApplicationTypeID { get; set; }
        public string ApplicationTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTitle = "";
            this.ApplicationFees  = 0;
        }

        public clsApplicationTypes(int applicationTypeID, string applicationTitle, decimal applicationFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTitle = applicationTitle;
            ApplicationFees = applicationFees;
        }

        public static clsApplicationTypes FindApplicationTypeByID(int ApplicationTypeID)
        {
            string ApplicationTitle = "";
            decimal ApplicationFees = 0;

            bool isfound = clsApplicationTypeDataAccess.GetApplicationTypeIDByID(ApplicationTypeID, ref ApplicationTitle, ref ApplicationFees);

            if (isfound)
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTitle, ApplicationFees);
            else
                return null;

        }

        public static clsApplicationTypes FindApplicationTypeByTitle(string ApplicationTitle)
        {
            int ApplicationTypeID = -1;
            decimal ApplicationFees = 0;

            bool isfound = clsApplicationTypeDataAccess.GetApplicationTypeIDByTitle(ApplicationTitle, ref ApplicationTypeID, ref ApplicationFees);

            if (isfound)
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTitle, ApplicationFees);
            else
                return null;

        }

        public bool _UpdateApplicationType()
        {
            return clsApplicationTypeDataAccess.UpdateApplicationType(ApplicationTypeID, ApplicationTitle, ApplicationFees);
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplicationTypeDataAccess.AddNewApplicationType(this.ApplicationTitle, this.ApplicationFees);


            return (this.ApplicationTypeID != -1);
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeDataAccess.GetAllApplicationTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }

    }
}
