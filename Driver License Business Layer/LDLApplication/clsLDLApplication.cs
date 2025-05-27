using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_Business_Layer.Tests;
using Driver_License_DataAccess_Layer;
using Driver_License_DataAccess_Layer.ApplicationsDataAccess;
using static Driver_License_Business_Layer.clsTestTypes;

namespace Driver_License_Business_Layer
{
    public class clsLDLApplication : clsApplications
    {
        private enum enMode { AddNew = 0, Update = 1};
        enMode _Mode;
        public int LDLApplicationID {  get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClasses LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return base.ApplicantInfo.FullName();
            }
        }
        public clsLDLApplication()
        {
            this.LDLApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }

        private clsLDLApplication(int lDLApplicationID, int licenseClassID, int applicationID, int applicantPersonID,
            DateTime applicationDate, int applicationTypeID, Byte ApplicationStatus, DateTime lastStatusDate, decimal paidFees,
            int createdByUserID) : base(applicationID, applicantPersonID, applicationDate, applicationTypeID, ApplicationStatus,
                lastStatusDate, paidFees, createdByUserID)
        
        {
            this.LDLApplicationID = lDLApplicationID;
            this.LicenseClassID = licenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClassByID(licenseClassID);
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;

            _Mode = enMode.Update;
        }
        public static clsLDLApplication FindLDLApplication(int lDLApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLDLApplicationData.GetApplicationByApplicationID
                (lDLApplicationID, ref LicenseClassID, ref ApplicationID);


            if (IsFound)
            {
                //now we find the base application
                clsApplications Application = clsApplications.FindApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLDLApplication(
                    lDLApplicationID, LicenseClassID, Application.ApplicationID, Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate,Application.PaidFees, Application.CreatedByUserID);
            }
            else
                return null;
        }

        public static clsLDLApplication FindLDLApplicationByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLDLApplicationData.FindLDLApplicationByApplicationID
                (ApplicationID, ref LicenseClassID, ref LocalDrivingLicenseApplicationID);


            if (IsFound)
            {
                //now we find the base application
                clsApplications Application = clsApplications.FindApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLDLApplication(
                    LocalDrivingLicenseApplicationID, LicenseClassID, Application.ApplicationID,Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID);
            }
            else
                return null;
        }
        public clsApplications FindApp(int ApplicationID)
        {

            return clsApplications.FindApplication(ApplicationID);
        }

        //public bool AddNEwApplication()
        //{
        //    this.ApplicationID = clsLDLApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
        //        this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        //    return ApplicationID != -1;
        //}

        //public bool UpdateApplication()
        //{
        //    return clsLDLApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
        //        this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        //}

        private bool _AddNewLDLApplication()
        {
            this.LDLApplicationID = clsLDLApplicationData.AddNewLDLApplication(this.LicenseClassID, this.ApplicationID);
            return (LDLApplicationID != -1);

        }

        private bool _UpdateLDLApplication()
        {
            return clsLDLApplicationData.UpdateLDLApplication(this.LDLApplicationID, this.LicenseClassID, this.ApplicationID);
        }

        public  bool DeleteLDLApplication()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLDLApplicationData.DeleteLDLApplication(this.LDLApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.DeleteApplication();
            return IsBaseApplicationDeleted;
        }

        public static DataTable GetLDLApplications()
        {
            return clsLDLApplicationData.GetLDLApplications();
        }

        public int PassedTestsCount(int LDLApplicationID)
        {
            int Count = -1;
            
            Count = clsLDLApplicationData.GetPassedTestcount(LDLApplicationID);

            return Count;
        }

        public static bool CheckLicenseAlreadyExists(int PersonID, int LicenseClassID)
        {
            return clsLDLApplicationData.CheckLicenseAlreadyExists(PersonID, LicenseClassID);
        }
        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplications.enMode)_Mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLDLApplication())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLDLApplication();

            }

            return false;
        }

        public bool DoesPassTestType(clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.DoesPassTestType(this.LDLApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestTypes.enTestTypes CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypes.enTestTypes.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestTypes.enTestTypes.WrittentTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestTypes.enTestTypes.VisionTest);


                case clsTestTypes.enTestTypes.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestTypes.enTestTypes.WrittentTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.DoesAttendTestType(this.LDLApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsLDLApplicationData.TotalTrialsPerTest(this.LDLApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLDLApplicationData.TotalTrialsPerTest(this.LDLApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {

            return clsLDLApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestTypes TestTypeID)

        {

            return clsLDLApplicationData.IsThereAnActiveScheduledTest(this.LDLApplicationID, (int)TestTypeID);
        }

        public clsTests GetLastTestPerTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTests.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTests.GetPassedTestCount(this.LDLApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTests.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTests.PassedAllTests(this.LDLApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTests.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDrivers Driver = clsDrivers.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDrivers();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now Our diver is there, so we add new licesnse

            clsLicenses License = new clsLicenses();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = (int)clsLicenses.enIssueReasons.First_Time;
            License.CreateByUserID = CreatedByUserID;

            if (License.Save()) 
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicenses.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public static bool IsAgeAllowedForLicenseClass(int LicenseClassID, byte Age)
        {
            byte MinimumAllowedAge = clsLicenseClasses.FindLicenseClassByID(LicenseClassID).MinimumAllowedAge;

            if (Age < MinimumAllowedAge)
                return false;
            else
                return true;
            
        }
        //public bool IsAgeAllowedForLicenseClass()
        //{
        //    return IsAgeAllowedForLicenseClass(this.LicenseClassInfo.MinimumAllowedAge, this.LicenseClassID, this.);
        //}
    }
}
