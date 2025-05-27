using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Driver_License_Business_Layer.clsLicenses;

namespace Driving_Licenses_Managment_Presentation_Layer.Applications.Licenses_Replacment
{
    public partial class frmLicensesReplacment : Form
    {
        private int _ReplacedLicenseID = -1;
        public frmLicensesReplacment()
        {
            InitializeComponent();

        }
        private int _GetApplicationTypeID()
        {
            //this will decide which application type to use accirding 
            // to user selection.

            if (rbDamagedLicense.Checked)

                return (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReasons _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamagedLicense.Checked)

                return enIssueReasons.Replacement_for_Damaged;
            else
                return enIssueReasons.Replacement_for_Lost;
        }

        private void frmLicensesReplacment_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFillter1.txtLicenseIDFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUserName.Text = clsGlobal.CurrentUser.UserName;

            rbDamagedLicense.Checked = true;
        }

        private void ctrlLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            if(SelectedLicenseID == -1)
            {
                btnIssueReplacment.Enabled = false;
                return;
            }

            lblOldLicenseID.Text = SelectedLicenseID.ToString();

            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacment.Enabled = false;
                return;
            }

            btnIssueReplacment.Enabled = true;
        }

        private void btnIssueReplacment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicenses NewLicese = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);

            if (NewLicese == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _ReplacedLicenseID = NewLicese.LicenseID;

            lblReplacedLicenseID.Text = _ReplacedLicenseID.ToString();
            lblLRApplicationID.Text = NewLicese.ApplicationID.ToString();

            MessageBox.Show("Licensed Replaced Successfully with ID=" + _ReplacedLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacment.Enabled = false;
            gbReplacmentChoices.Enabled = false;
            ctrlLicenseInfoWithFillter1.FilterEnabled = false;
            liblShowNewLicenseInfo.Enabled = true;


        }

        private void frmLicensesReplacment_Activated(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFillter1.txtLicenseIDFocus();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replaced For Damaged License";
            lblTitle.Text = "Replaced For Damaged License";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaDamagedDrivingLicense).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            Text = "Replaced For Lost License";
            lblTitle.Text = "Replaced For Lost License";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplicationTypes.EnApplicationTypes.ReplacementforaLostDrivingLicense).ApplicationFees.ToString();
        }

        private void liblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(_ReplacedLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
