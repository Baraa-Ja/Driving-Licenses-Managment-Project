using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_Business_Layer.Tests;
using Driver_License_Business_Layer.VisionTestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.LDLApplication;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Tests
{
    public partial class ctrlTakeTest : UserControl
    {
        private clsTestsAppointments _TestAppointment;
        private int _TestAppointmentID;

        private clsTests _Test;

        private int _TestTypeID;
        private clsTestTypes.enTestTypes _enTestTypeID = clsTestTypes.enTestTypes.VisionTest;
        public ctrlTakeTest()
        {
            InitializeComponent();
        }

        private void TestAppointmentType(int TestTypeID)
        {
            clsTestTypes TestType = clsTestTypes.FindTestByID(TestTypeID);

            switch (TestTypeID)
            {
                case 1:
                    picbTest.BackgroundImage = Resources.Vision;
                    groupBox1.Text = "Vision Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.VisionTest;
                    break;
                case 2:
                    picbTest.BackgroundImage = Resources.Writing_PNG_Download_Image;
                    groupBox1.Text = "Written Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.WrittentTest;
                    break;
                case 3:
                    picbTest.BackgroundImage = Resources.Street;
                    groupBox1.Text = "Street Test";
                    _enTestTypeID = clsTestTypes.enTestTypes.StreetTest;
                    break;
            }

        }

        public void _LoadTestData(int TestAppointmentID, int TestTypeID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
            _TestAppointment = clsTestsAppointments.FindTestAppointment(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            clsTestTypes TestType = clsTestTypes.FindTestByID(TestTypeID);

            TestAppointmentType(TestTypeID);

            lblLDLApplicationID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();

            //clsApplications Application = clsApplications.FindApplication(clsLDLApplication.FindLDLApplication(_TestAppointment.LocalDrivingLicenseApplicationID).ApplicationID);
            lblDrivingClassName.Text = clsLicenseClasses.FindLicenseClassByID(clsLDLApplication.FindLDLApplication(_TestAppointment.LocalDrivingLicenseApplicationID).LicenseClassID).ClassName;
            lblApplicantPersonName.Text = clsPeople.FindPerson(_TestAppointment.LDLApplicationInfo.ApplicantPersonID).FullName();
            lblTrailsCount.Text = _TestAppointment.LDLApplicationInfo.TotalTrialsPerTest(_enTestTypeID).ToString();
            lbltestDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);

            lblTestFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = "????";
            rbPass.Checked = true;

            _Test = new clsTests();
            return;

        }

        int PassTestCount = 0;
        public void Save()
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
            "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _TestAppointmentID;

            if (rbPass.Checked)
                _Test.TestResult = true;
            else
                _Test.TestResult = false;

            _Test.Notes = txtNotes.Text;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            clsTestTypes TestType = clsTestTypes.FindTestByID(_TestTypeID);

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
                if (_Test.TestResult == false)
                {
                    clsApplications Application = clsApplications.FindApplication(_TestAppointment.LDLApplicationInfo.ApplicationID);
                    Application.ApplicationTypeID = (int)clsApplicationTypes.EnApplicationTypes.ReTakeTest;
                    Application.Save();
                }
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }

            //_TestAppointment = clsTestsAppointments.FindTestAppointment(_TestAppointmentID);
            //_TestAppointment.IsLocked = true;
            //_TestAppointment._UpdateTestAppointment();

        }
    }
}
