using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_DataAccess_Layer;
using Driver_License_DataAccess_Layer.International_LicensesData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Driver_License_Business_Layer.International_License
{
    public class clsInternationalLicense: clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Boolean IsActive { get; set; }
        public clsDrivers Driverinfo;


        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.ApplicationTypeID = (int) clsApplications.enApplicationType.NewInternationalLicense;
            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int internationalLicenseID, int applicationID, int driverID, int issuedUsingLocalLicenseID
            ,DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, Byte ApplicationStatus, DateTime lastStatusDate, decimal paidFees)
        {
            base.ApplicationID = applicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = lastStatusDate;
            base.PaidFees = paidFees;
            base.CreatedByUserID = createdByUserID;

            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;

            this.Driverinfo = clsDrivers.FindDriver(this.DriverID);

            Mode = enMode.Update;
        }

        public static clsInternationalLicense FindInternationalLicense(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = true;


            bool IsFound = clsinternationalLicenseData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID);

            if (IsFound)
            {
                clsApplications Application = clsApplications.FindApplication(ApplicationID);

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive,
                    CreatedByUserID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID,
                    Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees);
            }
            else
            {
                return null;
            }
        }

        public bool AddNewInternationalApplication()
        {
            this.InternationalLicenseID = clsinternationalLicenseData.AddNewApplication(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return InternationalLicenseID != -1;
        }

        public static bool CheckLicenseAlreadyExists(int PersonID)
        {
            return clsinternationalLicenseData.CheckLicenseAlreadyExists(PersonID);
        }

        public static DataTable GetAllInternationalLicense(int PersonID)
        {
            return clsinternationalLicenseData.GetInternationalLicenseTable(PersonID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsinternationalLicenseData.GetAllInternationalLicenses();

        }
        public static DataTable InterNationalLicenseList()
        {
            return clsinternationalLicenseData.InterNationalLicenseList();
        }

        public bool UpdateLicense()
        {
            return clsinternationalLicenseData.UpdateLicense(this.InternationalLicenseID, this.IsActive);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsinternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }

        public bool Save()
        {
            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;

            switch(Mode)
            {
                case enMode.AddNew:
                    if(AddNewInternationalApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                    else
                        {
                            return false;
                        }
                case enMode.Update:
                    return UpdateLicense();
            }

            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsinternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }
    }
}
