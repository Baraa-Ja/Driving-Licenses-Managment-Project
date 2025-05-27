using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_Business_Layer.Tests;
using Driver_License_Business_Layer.VisionTestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.IssueLicenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Licenses_Replacment
{
    public partial class frmLicenseReplacment : Form
    {
        private clsLicenses _License;
        private int _LicenseID = 0;

        private clsDrivers _Driver;
        private clsLDLApplication _LDLApplication;
        private clsLicenses NewLicense;
        private clsLDLApplication NewLDLApplication;
        private clsApplications NewApplication;
        private clsTests NewTest1;
        private clsTestsAppointments NewTestAppointment1;
        private clsTests NewTest2;
        private clsTestsAppointments NewTestAppointment2;
        private clsTests NewTest3;
        private clsTestsAppointments NewTestAppointment3;
        public frmLicenseReplacment()
        {
            InitializeComponent();
        }

        private void ReplacmentForChoices()
        {
            if(rbDamagedLicense.Checked)
            {
                Text = "Replaced For Damaged License";
                lblTitle.Text = "Replaced For Damaged License";
                lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaDamagedDrivingLicense).ApplicationFees.ToString();
            }

            else if(rbLostLicense.Checked)
            {
                Text = "Replaced For Lost License";
                lblTitle.Text = "Replaced For Lost License";
                lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaLostDrivingLicense).ApplicationFees.ToString();
            }
        }
        private void frmLicenseReplacment_Load(object sender, EventArgs e)
        {
            ReplacmentForChoices();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUserName.Text = clsUsers.FindUser(1).UserName;

        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                //Temp.Focus();
                errorProvider1.SetError(Temp, "This Field Is Required");
            }
            else
            {
                errorProvider1.SetError(Temp, "");
            }
        }

        private void pictFind_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtLicenseID.Text != "")
                {
                    _License = clsLicenses.FindLicense(int.Parse(txtLicenseID.Text));

                    ctrlLicenseInfo1.LoadData(_License.LicenseID, _License.IssueReason);
                    lblOldLicenseID.Text = _License.LicenseID.ToString();
                    btnIssueReplacment.Enabled = true;
                    liblShowLicensesHistory.Enabled = true;

                    if (_License == null)
                    {
                        MessageBox.Show("No License With LicenseID : " + txtLicenseID.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        btnIssueReplacment.Enabled = false;
                        liblShowLicensesHistory.Enabled = false;

                        return;
                    }


                    if (_License.IsActive == false)
                    {
                        MessageBox.Show("Selected License Is Not Active, Choose An Active One Please.",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        btnIssueReplacment.Enabled = false;

                        return;
                    }

                }
                else
                {
                    this.ValidateChildren();
                    MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

            }
            catch
            {
                MessageBox.Show("No License With LicenseID : " + txtLicenseID.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssueReplacment.Enabled = false;
                liblShowLicensesHistory.Enabled = false;

                return;
            }
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtLicenseID.Text.ToString().Trim(), out int result))
            {
                errorProvider1.SetError(txtLicenseID, "Enter Numeric Type please");
            }
        }

        private void liblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _License = clsLicenses.FindLicense(int.Parse(txtLicenseID.Text));
            clsLDLApplication LDLApplication =  clsLDLApplication.FindLDLApplicationByApplicationID(_License.ApplicationID);

            //frmLicensesHistory frm = new frmLicensesHistory(LDLApplication.LDLApplicationID);
            //frm.ShowDialog();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            Text = "Replaced For Lost License";
            lblTitle.Text = "Replaced For Lost License";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaLostDrivingLicense).ApplicationFees.ToString();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            Text = "Replaced For Damaged License";
            lblTitle.Text = "Replaced For Damaged License";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaDamagedDrivingLicense).ApplicationFees.ToString();
        }

        private void rbLostLicense_Click(object sender, EventArgs e)
        {

        }

        private void btnIssueReplacment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Issue A Replacment For This License ? ", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                clsApplications Application = clsApplications.FindApplication(_License.ApplicationID);
                _LDLApplication = clsLDLApplication.FindLDLApplicationByApplicationID(Application.ApplicationID);

                NewApplication = new clsApplications();
                NewApplication.ApplicantPersonID = Application.ApplicantPersonID;
                NewApplication.ApplicationDate = DateTime.Now;

                NewApplication.ApplicationTypeID = rbLostLicense.Checked ? (int)clsApplicationTypes.EnApplicationTypes.ReplacementforaLostDrivingLicense
                    : (int)clsApplicationTypes.EnApplicationTypes.ReplacementforaDamagedDrivingLicense;

                NewApplication.ApplicationStatus = Application.ApplicationStatus;
                NewApplication.LastStatusDate = Application.LastStatusDate;

                NewApplication.PaidFees = rbLostLicense.Checked ? clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaLostDrivingLicense).ApplicationFees
                    : clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaDamagedDrivingLicense).ApplicationFees;

                NewApplication.CreatedByUserID = Application.CreatedByUserID;
                NewApplication.Save();

                NewLDLApplication = new clsLDLApplication();
                NewLDLApplication.ApplicationID = NewApplication.ApplicationID;
                NewLDLApplication.LicenseClassID = _LDLApplication.LicenseClassID;
                NewLDLApplication.Save();

                NewTestAppointment1 = new clsTestsAppointments();
                NewTestAppointment1.TestTypeID = 1;
                NewTestAppointment1.LocalDrivingLicenseApplicationID = NewLDLApplication.LDLApplicationID;
                NewTestAppointment1.AppointmentDate = DateTime.Now;
                NewTestAppointment1.PaidFees = clsTestTypes.FindTestByID(1).TestTypeFees;
                NewTestAppointment1.CreatedByUserID = Application.CreatedByUserID;
                NewTestAppointment1.IsLocked = true;
                NewTestAppointment1.Save();

                NewTest1 = new clsTests();
                NewTest1.TestAppointmentID = NewTestAppointment1.TestAppointmentID;
                NewTest1.TestResult = true;
                NewTest1.Notes = "";
                NewTest1.CreatedByUserID = NewTestAppointment1.CreatedByUserID;
                NewTest1.AddNewTest();

                NewTestAppointment2 = new clsTestsAppointments();
                NewTestAppointment2.TestTypeID = 2;
                NewTestAppointment2.LocalDrivingLicenseApplicationID = NewLDLApplication.LDLApplicationID;
                NewTestAppointment2.AppointmentDate = DateTime.Now;
                NewTestAppointment2.PaidFees = clsTestTypes.FindTestByID(2).TestTypeFees;
                NewTestAppointment2.CreatedByUserID = Application.CreatedByUserID;
                NewTestAppointment2.IsLocked = true;
                NewTestAppointment2.Save();
                                  
                NewTest2 = new clsTests();
                NewTest2.TestAppointmentID = NewTestAppointment2.TestAppointmentID;
                NewTest2.TestResult = true;
                NewTest2.Notes = "";
                NewTest2.CreatedByUserID = NewTestAppointment2.CreatedByUserID;
                NewTest2.AddNewTest();

                NewTestAppointment3 = new clsTestsAppointments();
                NewTestAppointment3.TestTypeID = 3;
                NewTestAppointment3.LocalDrivingLicenseApplicationID = NewLDLApplication.LDLApplicationID;
                NewTestAppointment3.AppointmentDate = DateTime.Now;
                NewTestAppointment3.PaidFees = clsTestTypes.FindTestByID(3).TestTypeFees;
                NewTestAppointment3.CreatedByUserID = Application.CreatedByUserID;
                NewTestAppointment3.IsLocked = true;
                NewTestAppointment3.Save();

                NewTest3 = new clsTests();
                NewTest3.TestAppointmentID = NewTestAppointment3.TestAppointmentID;
                NewTest3.TestResult = true;
                NewTest3.Notes = "";
                NewTest3.CreatedByUserID = NewTestAppointment3.CreatedByUserID;
                NewTest3.AddNewTest();


                if (clsDrivers.IsDriverExists(NewApplication.ApplicantPersonID) == false)
                {
                    _Driver = new clsDrivers();
                    _Driver.PersonID = NewApplication.ApplicantPersonID;
                    _Driver.CreatedByUserID = NewApplication.CreatedByUserID;
                    _Driver.CreatedDate = DateTime.Now;
                    _Driver.AddNewDriver();
                }
                else
                {
                    _Driver = clsDrivers.FindDriverByPersonID(NewApplication.ApplicantPersonID);
                }

                NewLicense = new clsLicenses();
                NewLicense.ApplicationID = NewApplication.ApplicationID;
                NewLicense.DriverID = _Driver.DriverID;
                NewLicense.LicenseClassID = NewLDLApplication.LicenseClassID;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.ExpirationDate = NewLicense.IssueDate.AddYears(clsLicenseClasses.FindLicenseClassByID(_License.LicenseClassID).DefaultValidityLength);
                NewLicense.Notes = "";
                NewLicense.PaidFees = clsLicenseClasses.FindLicenseClassByID(_License.LicenseClassID).ClassFees;
                NewLicense.IsActive = true;
                NewLicense.IssueReason = Convert.ToByte((rbLostLicense.Checked) ? 4 : 3);
                NewLicense.CreateByUserID = NewApplication.CreatedByUserID;

                if (NewLicense.AddNewLicense())
                {
                    MessageBox.Show($"License Added Successfully with license ID = {NewLicense.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    NewApplication.ApplicationStatus = 3;
                    NewApplication.Save();

                    _License.IsActive = false;
                    _License.UpdateLicense();

                    btnIssueReplacment.Enabled = false;
                    liblShowNewLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Licsense Not Added");
                }
            }
        }

        private void liblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplicationByApplicationID(NewLicense.ApplicationID);

            frmLicenseInfo frm = new frmLicenseInfo(NewLDLApplication.LDLApplicationID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
