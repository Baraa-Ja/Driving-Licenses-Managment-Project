using Driver_License_DataAccess_Layer.ApplicationsDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer.ApplicationsBusiness
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1, Retake = 2};
        public enMode Mode;

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 8
        };

        public enum enStatus { New = 1, Canceled = 2, Completed = 3};
        enStatus Status;

        public int ApplicationID {  get; set; }
        public int ApplicantPersonID {  get; set; }
        public clsPeople ApplicantInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPeople.FindPerson(ApplicantPersonID).FullName();
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID {  get; set; }
        public clsApplicationTypes ApplicationTypeInfo;
        public Byte ApplicationStatus {  get; set; }
        public enStatus eApplicationStatus { set; get; }
        public string StatusText
        {
            get
            {

                switch (eApplicationStatus)
                {
                    case enStatus.New:
                        return "New";
                    case enStatus.Canceled:
                        return "Cancelled";
                    case enStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime LastStatusDate {  get; set; }
        public decimal PaidFees {  get; set; }
        public int CreatedByUserID { get; set; }

        public clsUsers CreatedByUserInfo;



        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsApplications(int applicationID, int applicantPersonID, DateTime applicationDate,
            int applicationTypeID, Byte ApplicationStatus, DateTime lastStatusDate,
            decimal paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicantInfo = clsPeople.FindPerson(applicantPersonID);
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.FindUser(createdByUserID);

            Mode = enMode.Update;
        }

        public static clsApplications FindApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            Byte ApplicationStatus = 1;
            DateTime ApplicationDate = new DateTime(), LastStatusDate = new DateTime();
            decimal PaidFees = 0;

            bool IsFound = clsApplicationsData.GetApplicationByApplicationID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if(IsFound )
            {
                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus,
                    LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public static clsApplications FindApplicatipnByPersonID(int PersonID)
        {
            int ApplicationID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            Byte ApplicationStatus = 1;
            DateTime ApplicationDate = new DateTime(), LastStatusDate = new DateTime();
            decimal PaidFees = 0;

            bool IsFound = clsApplicationsData.GetApplicationByPersonID(PersonID, ref ApplicationID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (IsFound)
            {
                return new clsApplications(ApplicationID, PersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus,
                    LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,
                this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool DeleteApplication()
        {
            return clsApplicationsData.DeleteApplication(this.ApplicationID);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }

        public bool Cancel()

        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 3);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplications.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplications.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplications.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:

                    if(_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;

        }
    }
}
