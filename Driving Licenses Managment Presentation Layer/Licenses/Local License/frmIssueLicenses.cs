using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Driving_Licenses_Managment_Presentation_Layer.IssueLicenses
{
    public partial class frmIssueLicenses : Form
    {

        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID;

        private clsDrivers _Driver;
        private clsLicenses _License;
        public frmIssueLicenses(int LDLApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID; 
        }

        private void frmIssueLicenses_Load(object sender, EventArgs e)
        {

            txtNotes.Focus();
            _LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);

            if (_LDLApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LDLApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LDLApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LDLApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlLDLApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
        }

        public void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LDLApplication.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //_LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);
            //clsApplications Application = clsApplications.FindApplication(_LDLApplication.ApplicationID);

            //if(clsDrivers.IsDriverExists(Application.ApplicantPersonID) == false)
            //{
            //    _Driver = new clsDrivers();
            //    _Driver.PersonID = Application.ApplicantPersonID;
            //    _Driver.CreatedByUserID = Application.CreatedByUserID;
            //    _Driver.CreatedDate = DateTime.Now;
            //    _Driver.AddNewDriver();
            //}
            //else
            //{
            //    _Driver = clsDrivers.FindDriverByPersonID(Application.ApplicantPersonID);
            //}

            //_License = new clsLicenses();
            //_License.ApplicationID = Application.ApplicationID;
            //_License.DriverID = _Driver.DriverID;
            //_License.LicenseClassID = _LDLApplication.LicenseClassID;
            //_License.IssueDate = DateTime.Now;
            //_License.ExpirationDate = _License.IssueDate.AddYears(clsLicenseClasses.FindLicenseClassByID(_License.LicenseClassID).DefaultValidityLength);
            //_License.Notes = txtNotes.Text;
            //_License.PaidFees = clsLicenseClasses.FindLicenseClassByID(_License.LicenseClassID).ClassFees;
            //_License.IsActive = true;
            //_License.IssueReason = 1;
            //_License.CreateByUserID = Application.CreatedByUserID;

            //if(_License.AddNewLicense())
            //{
            //    MessageBox.Show($"License Added Successfully with license ID = {_License.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    Application.ApplicationStatus = 3;
            //    Application.Save();
            //}
            //else
            //{
            //    MessageBox.Show("Licsense Not Added");
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
