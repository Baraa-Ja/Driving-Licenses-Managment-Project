using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License;
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

namespace Driving_Licenses_Managment_Presentation_Layer.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenses : Form
    {
        int _SelectedLicenseID = -1;
        public frmReleaseDetainedLicenses()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenses(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;

            ctrlLicenseInfoWithFillter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlLicenseInfoWithFillter1.FilterEnabled = false;
        }
        private void ctrlLicenseInfoWithFillter1_OnLicenseSelected_1(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            if (_SelectedLicenseID == -1)
            {
                btnRelase.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Not Detained, Please Selecte Another License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnRelase.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Please Selecte Another License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnRelase.Enabled = false;
                return;
            }

            lblDetainID.Text = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseID.ToString();
            lblFineFees.Text = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblFineFees.Text) + Convert.ToDecimal(lblApplicationFees.Text)).ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            liblShowLicensesHistory.Enabled = true;
            btnRelase.Enabled = true;
        }

        private void btnRelase_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Relase This License ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsReleased = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblReleaseApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("License Couldn't Released.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelase.Enabled = false;
                return;
            }

            MessageBox.Show("Detained Licenses Successfully Released.", "Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelase.Enabled = false;
            liblShowNewLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFillter1.FilterEnabled = false;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void liblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void liblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
