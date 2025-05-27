using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_DataAccess_Layer;
using Driver_License_DataAccess_Layer.VisionTestAppointmentsDataAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer.VisionTestAppointments
{
    public class clsTestsAppointments
    {
        public int VisionTrailesCount = 0;
        public int writtenTrailesCount = 0;
        public int StreetTrailsCount = 0;
        private enum enMode { AddNew = 0, Update = 1 }
        enMode _Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public Boolean IsLocked { get; set; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplications RetakeTestAppInfo { set; get; }

        public clsLDLApplication LDLApplicationInfo;
        public int TestID
        {
            get { return _GetTestID(); }

        }

        public clsTestsAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;

            _Mode = enMode.AddNew;
        }

        private clsTestsAppointments(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, decimal paidFees, int createdByUserID, Boolean isLocked)
        {
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLocked = isLocked;
            this.LDLApplicationInfo = clsLDLApplication.FindLDLApplication(LocalDrivingLicenseApplicationID);

            _Mode = enMode.Update;
        }

        public static clsTestsAppointments FindTestAppointment(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            Boolean IsLocked = false;

            bool IsFound = clsTestsAppointmentsData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked);

            if (IsFound)
            {
                return new clsTestsAppointments(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked);
            }
            else
            {
                return null;
            }
        }

        public static clsTestsAppointments GetLastTestAppointment(int LDLApplicationID, clsTestTypes.enTestTypes TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; decimal PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; 

            if (clsTestsAppointmentsData.GetLastTestAppointment(LDLApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))

                return new clsTestsAppointments(TestAppointmentID, (int)TestTypeID, LDLApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            else
                return null;

        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestsAppointmentsData.AddNewTestAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);

            return (TestAppointmentID != -1);
        }

        public bool _UpdateTestAppointment()
        {
            return clsTestsAppointmentsData.UpdateTestAppointment(this.TestAppointmentID, this.AppointmentDate, this.IsLocked);
        }

        public static DataTable TestAppointmentsTable(int TestTypeID, int LDLApplicationID)
        {
            return clsTestsAppointmentsData.TestAppointmentsList(TestTypeID, LDLApplicationID);
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestsAppointmentsData.GetAllTestAppointments();

        }

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTestsAppointmentsData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTestsAppointmentsData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static bool CheckAppointmentAlreadyExists(int TestTypeID, int LDLApplicationID, bool IsLocked)
        {
            return clsTestsAppointmentsData.CheckAppointmentAlreadyExists(TestTypeID, LDLApplicationID, IsLocked);
        }

        public static bool CheckPersonAlreadyPassedTest(int TestTypeID, int LDLApplicationID, bool TestResult)
        {
            return clsTestsAppointmentsData.CheckPersonAlreadyPassedTest(TestTypeID, LDLApplicationID, TestResult);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateTestAppointment();
            }

            return false;
        }

        private int _GetTestID()
        {
            return clsTestsAppointmentsData.GetTestID(TestAppointmentID);
        }
    }
}
