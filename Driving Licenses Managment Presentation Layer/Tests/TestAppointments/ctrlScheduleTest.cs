using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_Business_Layer.VisionTestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.TestAppointments
{
    public partial class ctrlScheduleTest : UserControl
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID = -1;

        private clsTestsAppointments _TestAppointment;
        private int _TestAppointmentID = -1;

        private clsApplications _ReTakeTestApplication;
        private int _ApplicationMode = -1;
        enum enMode { Addnew = 0, Update = 1 }
        enMode Mode;

        private int _TestTypeID;
        private clsTestTypes.enTestTypes _enTestTypeID = clsTestTypes.enTestTypes.VisionTest; 

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void TestAppointmentType(int TestTypeID)
        {
            clsTestTypes TestType = clsTestTypes.FindTestByID(TestTypeID);

            switch(TestTypeID)
            {
                case 1:
                    gpRetakeTestInfo.Enabled = false;
                    PicbTest.Image = Resources.Vision;
                    groupBox1.Text = "Vision Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.VisionTest;

                    break;
                case 2:
                    gpRetakeTestInfo.Enabled = false;
                    PicbTest.Image = Resources.Writing_PNG_Download_Image;
                    groupBox1.Text = "Written Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.WrittentTest;

                    break;
                case 3:
                    gpRetakeTestInfo.Enabled = false;
                    PicbTest.Image = Resources.Street;
                    groupBox1.Text = "Street Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.StreetTest;

                    break;
            }
        }
        public void LoadScheduleTest(int LDLApplicationID, int TestTypeID, int TestAppointmentID, int ApplicationMode)
        {
            if (TestAppointmentID == -1)
                Mode = enMode.Addnew;
            else
                Mode = enMode.Update;

            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
            _LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);
            _TestAppointment = clsTestsAppointments.FindTestAppointment(_TestAppointmentID);
            _ApplicationMode = ApplicationMode;

            TestAppointmentType(TestTypeID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LDLApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLDLApplicationID.Text = _LDLApplicationID.ToString();
            lblDrivingClassName.Text = _LDLApplication.LicenseClassInfo.ClassName;
            lblApplicantPersonName.Text = _LDLApplication.PersonFullName;

            //this will show the trials for this test
            lblTrailsCount.Text = _LDLApplication.TotalTrialsPerTest(_enTestTypeID).ToString();

            dtpAppointmentDate.Value = DateTime.Now;
            dtpAppointmentDate.MinDate = DateTime.Now;

            lblTestFees.Text = clsTestTypes.FindTestByID(TestTypeID).TestTypeFees.ToString();

            if (Mode == enMode.Addnew && ApplicationMode == (int)clsApplications.enMode.Retake)
            {
                _TestAppointment = new clsTestsAppointments();
                lblRetakeTestApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReTakeTest).ApplicationFees.ToString();
                lblTotalFees.Text = (clsTestTypes.FindTestByID(TestTypeID).TestTypeFees +
    clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReTakeTest).ApplicationFees).ToString();
                lblMode.Text = "Schedule Retake Test";
                gpRetakeTestInfo.Enabled = true;
                return;
            }

            if (Mode == enMode.Addnew)
            {
                _TestAppointment = new clsTestsAppointments();
                Mode = enMode.Update;
                return;
            }

            if (Mode == enMode.Update)
            {
                if(!_LoadTestAppointmentData(ApplicationMode))
                    return;
                else
                {
                    lblTotalFees.Text = (clsTestTypes.FindTestByID(TestTypeID).TestTypeFees +
    clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReTakeTest).ApplicationFees).ToString();

                    lblMode.Text = "Update Test Appointment";
                }
            }

        }
        private bool _LoadTestAppointmentData(int ApplicationMode)
        {
            _TestAppointment = clsTestsAppointments.FindTestAppointment(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            lblTestFees.Text = _TestAppointment.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpAppointmentDate.MinDate = DateTime.Now;
            else
                dtpAppointmentDate.MinDate = _TestAppointment.AppointmentDate;

            dtpAppointmentDate.Value = _TestAppointment.AppointmentDate;

            return true;
        }
        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (Mode == enMode.Addnew && _ApplicationMode == (int)clsApplications.enMode.Retake)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LDLApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RetakeTest;
                Application.ApplicationStatus = (byte)clsApplications.enStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            return true;
        }

        public void Save()
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLApplicationID;
            _TestAppointment.AppointmentDate = dtpAppointmentDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblTestFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _TestAppointment.IsLocked = false;

            if (_TestAppointment.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            Mode = enMode.Update;
        }
    }
}
