using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.Detain;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Driver_License_Business_Layer.clsLicenses;

namespace Driver_License_Business_Layer
{
    public class clsLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReasons { First_Time = 1, Renew = 2, Replacement_for_Damaged  = 3, Replacement_for_Lost = 4 };
        public enIssueReasons IReason;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassesInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public Boolean IsActive { get; set; }
        public byte IssueReason {  get; set; }
        public int CreateByUserID { get; set; }
        public enIssueReasons eIssueReason { set; get; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public clsDetain DetainedInfo { set; get; }
        public bool IsDetained
        {
            get { return clsDetain.IsLicenseDetained(this.LicenseID); }
        }

        public clsDrivers DriverInfo;
        public clsLicenses()
        {
            this.LicenseID = 0;
            this.ApplicationID = 0;
            this.DriverID = 0;
            this.LicenseClassID = 0;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreateByUserID = 0;
        }

        private clsLicenses(int LicenseID, int ApplicationID, int DriverID, int licenseClassID, DateTime IssueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason, int createByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = licenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreateByUserID = createByUserID;

            this.DriverInfo = clsDrivers.FindDriver(this.DriverID);
            this.LicenseClassesInfo = clsLicenseClasses.FindLicenseClassByID(this.LicenseClassID);
            this.DetainedInfo = clsDetain.FindDetainedLicenseByLicenseID(this.LicenseID);
        }

        public static clsLicenses FindLicense(int LicenseID)
        {
            int ApplicationID = 0, DriverID = 0, LicenseClassID = 0, CreateByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            Boolean isActive = false;
            byte IssueReason = 0;

            bool Isfound = clsLicensesData.GetLicenseByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref isActive, ref IssueReason, ref CreateByUserID);

            if (Isfound)
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees,
                    isActive, IssueReason, CreateByUserID);
            else
                return null;
        }

        public static clsLicenses FindLicenseByApplicationID(int ApplicationID)
        {
            int LicenseID = 0, DriverID = 0, LicenseClassID = 0, CreateByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            Boolean isActive = false;
            byte IssueReason = 0;

            bool Isfound = clsLicensesData.GetLicenseByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref isActive, ref IssueReason, ref CreateByUserID);

            if (Isfound)
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees,
                    isActive, IssueReason, CreateByUserID);
            else
                return null;
        }

        public static clsLicenses FindLicenseByDriverID(int DriverID)
        {
            int LicenseID = 0, ApplicationID = 0, LicenseClassID = 0, CreateByUserID = 0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            Boolean isActive = false;
            byte IssueReason = 0;

            bool Isfound = clsLicensesData.GetLicenseByDriverID(DriverID, ref ApplicationID , ref LicenseID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref isActive, ref IssueReason, ref CreateByUserID);

            if (Isfound)
                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees,
                    isActive, IssueReason, CreateByUserID);
            else
                return null;
        }
        public bool AddNewLicense()
        {
            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreateByUserID);

            return LicenseID != 0;
        }
        public bool UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(this.LicenseID, this.IsActive);
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicensesData.GetDriverLicense(DriverID);
        }

        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicensesData.DeactivateLicense(this.LicenseID));
        }

        public static string GetIssueReasonText(byte IssueReason)
        {

            switch (IssueReason)
            {
                case (byte)enIssueReasons.First_Time:
                    return "First Time";
                case (byte)enIssueReasons.Renew:
                    return "Renew";
                case (byte)enIssueReasons.Replacement_for_Damaged:
                    return "Replacement for Damaged";
                case (byte)enIssueReasons.Replacement_for_Lost:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public int Detain(decimal FineFees, int CreatedByUserID)
        {
            clsDetain detainedLicense = new clsDetain();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = Convert.ToDecimal(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = (byte)clsApplications.enStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;


            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }

        public clsLicenses RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = (byte)clsApplications.enStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassesInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassesInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = (byte)clsLicenses.enIssueReasons.Renew;
            NewLicense.CreateByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicenses Replace(enIssueReasons IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReasons.Replacement_for_Damaged) ?
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = (byte)clsApplications.enStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = (byte)IssueReason;
            NewLicense.CreateByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }
        public static DataTable LDLDataTable(int PersonID)
        {
            return clsLicensesData.GetLDLDataTable(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewLicense())
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
    }
}
