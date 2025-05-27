using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.International_License;
using Driver_License_Business_Layer.LicenseClasses;
using Driver_License_Business_Layer.Tests;
using Driver_License_Business_Layer.VisionTestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License;
using Driving_Licenses_Managment_Presentation_Layer.PeoplePresentationLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.InternationalLicense
{
    public partial class frmInternationalLicense : Form
    {
        private int _InternationalLicenseId = -1;
        private int _SelectedLicenseID = -1;
        public frmInternationalLicense()
        {
            InitializeComponent();
        }

        public frmInternationalLicense(int InternationalLicenseId)
        {
            InitializeComponent();
            _InternationalLicenseId = InternationalLicenseId;

            ctrlLicenseInfoWithFillter1.LoadLicenseInfo(_InternationalLicenseId);
            ctrlLicenseInfoWithFillter1.FilterEnabled = false;
        }
        private void frmInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(Convert.ToDateTime(lblIssueDate.Text).AddYears(1));
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.NewInternationalLicense).ApplicationFees.ToString();
        }
        private void liblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Issue An International License ? ", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                clsInternationalLicense InternationalLicense = new clsInternationalLicense();
                //those are the information for the base application, because it inhirts from application, they are part of the sub class.

                InternationalLicense.ApplicantPersonID = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID;
                InternationalLicense.ApplicationDate = DateTime.Now;
                InternationalLicense.ApplicationStatus = (byte)clsApplications.enStatus.Completed;
                InternationalLicense.LastStatusDate = DateTime.Now;
                InternationalLicense.PaidFees = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.NewInternationalLicense).ApplicationFees;
                InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


                InternationalLicense.DriverID = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DriverID;
                InternationalLicense.IssuedUsingLocalLicenseID = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseID;
                InternationalLicense.IssueDate = DateTime.Now;
                // Hard Coded International License Validaty Length
                InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

                InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!InternationalLicense.Save())
                {
                    MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                lblinternationalLicenseApplicationID.Text = InternationalLicense.ApplicationID.ToString();
                _InternationalLicenseId = InternationalLicense.InternationalLicenseID;
                lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnIssue.Enabled = false;
                ctrlLicenseInfoWithFillter1.FilterEnabled = false;
                liblShowNewLicenseInfo.Enabled = true;
            }
        }

        private void liblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(_InternationalLicenseId);
            frm.ShowDialog();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if(_SelectedLicenseID == -1)
            {
                btnIssue.Enabled = false;
                return;
            }

            lblLocalLicenseID.Text = _SelectedLicenseID.ToString();
            liblShowLicensesHistory.Enabled = (_SelectedLicenseID != -1);

            //check the license class, person could not issue international license without having
            //normal license of class 3.

            if (ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                liblShowNewLicenseInfo.Enabled = true;
                return;
            }
            //check if person already have an active international license

            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                liblShowNewLicenseInfo.Enabled = true;
                _InternationalLicenseId = ActiveInternaionalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }

    }
}
