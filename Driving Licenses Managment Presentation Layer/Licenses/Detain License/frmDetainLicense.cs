using Driver_License_Business_Layer;
using Driver_License_Business_Layer.Detain;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License;
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

namespace Driving_Licenses_Managment_Presentation_Layer.Detain
{
    public partial class frmDetainLicense : Form
    {
        private clsLicenses _License;
        private int _licenseID;
        private clsDetain _DetainLicense;
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (txtFineFees.Text == "")
            {
                MessageBox.Show("Fees Cannot Be Empty", "Wornging", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            
            if (MessageBox.Show("Are You Sure You Want To Detain This License ? ", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _DetainID = ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text), clsGlobal.CurrentUser.UserID);

                if(_DetainID == -1)
                {
                    MessageBox.Show("Failed To Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblDetainID.Text = _DetainID.ToString();

                MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnDetain.Enabled = false;
                liblShowNewLicenseInfo.Enabled = true;
                ctrlLicenseInfoWithFillter1.Enabled = false;
                txtFineFees.Enabled = false;

            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseInfoWithFillter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            if (_SelectedLicenseID == -1)
            {
                btnDetain.Enabled = false;
                return;
            }

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            liblShowLicensesHistory.Enabled = true;
            txtFineFees.Focus();

            if (!ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Please Selecte Another License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnDetain.Enabled = false;
                return;
            }

            if (ctrlLicenseInfoWithFillter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Already Detained, Please Select Another One.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            //ctrlLicenseInfoWithFillter1.txtLicenseIDFocus();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }
    }
}
