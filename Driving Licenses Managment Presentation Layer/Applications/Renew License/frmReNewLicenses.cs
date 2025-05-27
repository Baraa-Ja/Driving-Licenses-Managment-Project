using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer;
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
using Driving_Licenses_Managment_Presentation_Layer.Licenses;

namespace Driving_Licenses_Managment_Presentation_Layer.Applications.Renew_License
{
    public partial class frmReNewLicenses : Form
    {
        private int _NewLicenseID = -1;
        public frmReNewLicenses()
        {
            InitializeComponent();
        }

        private void frmReNewLicenses_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFillter1.txtLicenseIDFocus();


            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;

            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();

            liblShowLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)

            {
                btnRenew.Enabled = false;

                return;
            }

            int DefaultValidityLength = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassesInfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.LicenseClassesInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.Notes;


            //check the license is not Expired.
            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            //check the license is not Active.
            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }



            btnRenew.Enabled = true;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicenses NewLicense = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
                clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblRenewedLicenseApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlLicenseInfoWithFillter1.FilterEnabled = false;
            liblShowNewLicenseInfo.Enabled = true;
        }



        private void liblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void liblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //frmLicensesHistory frm = new frmLicensesHistory();
            //frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReNewLicenses_Activated(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFillter1.txtLicenseIDFocus();
        }

        private void lblLicenseFees_Click(object sender, EventArgs e)
        {

        }
    }
}
