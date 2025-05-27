using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.Detain;
using Driving_Licenses_Managment_Presentation_Layer.Applications.Release_Detained_License;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Detain
{
    public partial class frmDetainedLicenseList : Form
    {
        private DataTable _dtDetainedLicenses;
        public frmDetainedLicenseList()
        {
            InitializeComponent();
        }
        private void _RefreshDetainedLicenseList()
        {
            cbDetainedLicenseFillter.SelectedIndex = 0;

            _dtDetainedLicenses = clsDetain.DetainedLicenseTable();

            dgvDetainedLicense.DataSource = _dtDetainedLicenses;
            lblRecordsNumber.Text = dgvDetainedLicense.Rows.Count.ToString();

            if (dgvDetainedLicense.Rows.Count > 0)
            {
                dgvDetainedLicense.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicense.Columns[0].Width = 90;

                dgvDetainedLicense.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicense.Columns[1].Width = 90;

                dgvDetainedLicense.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicense.Columns[2].Width = 160;

                dgvDetainedLicense.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicense.Columns[3].Width = 110;

                dgvDetainedLicense.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicense.Columns[4].Width = 110;

                dgvDetainedLicense.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicense.Columns[5].Width = 160;

                dgvDetainedLicense.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicense.Columns[6].Width = 90;

                dgvDetainedLicense.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicense.Columns[7].Width = 330;

                dgvDetainedLicense.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicense.Columns[8].Width = 150;
            }
        }
        private void frmDetainedLicenseList_Load(object sender, EventArgs e)
        {
            cbDetainedLicenseFillter.SelectedIndex = 0;
            _RefreshDetainedLicenseList();
        }

        private void cbLDLApplicationsFillter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDetainedLicenseFillter.Text == "Is Released")
            {
                txtFillter.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFillter.Visible = (cbIsReleased.Text != "None");
                cbIsReleased.Visible = false;

                if (cbDetainedLicenseFillter.Text == "None")
                {
                    txtFillter.Visible = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFillter.Enabled = true;

                txtFillter.Text = "";
                txtFillter.Focus();
            }
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {
            DataView DetainedLicenseView = clsDetain.DetainedLicenseTable().DefaultView;
            dgvDetainedLicense.DataSource = DetainedLicenseView;

            string FillterColumn = "";

            switch (cbDetainedLicenseFillter.Text)
            {
                case "Detain ID":
                    FillterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FillterColumn = "IsReleased";
                        break;
                    };

                case "National No.":
                    FillterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FillterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FillterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FillterColumn = "None";
                    break;
            }


            if (txtFillter.Text.Trim() == "" || FillterColumn == "None")
            {
                DetainedLicenseView.RowFilter = "";
                lblRecordsNumber.Text = dgvDetainedLicense.Rows.Count.ToString();
                return;
            }


            if (FillterColumn == "DetainID" || FillterColumn == "ReleaseApplicationID")
            {
                if (!int.TryParse(txtFillter.Text.ToString().Trim(), out int result))
                {
                    errorProvider1.SetError(txtFillter, "Enter Numeric Type please");
                }
                else
                {
                    DetainedLicenseView.RowFilter = string.Format("[{0}] = {1}", FillterColumn, txtFillter.Text.Trim());
                    errorProvider1.SetError(txtFillter, "");
                }

            }
            else
            {
                DetainedLicenseView.RowFilter = string.Format("[{0}] like '{1}%'", FillterColumn, txtFillter.Text.Trim());
                errorProvider1.SetError(txtFillter, "");
            }

            lblRecordsNumber.Text = dgvDetainedLicense.Rows.Count.ToString();
        }

        private void picRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenses frm = new frmReleaseDetainedLicenses();
            frm.ShowDialog();
        }

        private void picDetaine_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void showPersonDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.FindLicense(LicenseID).DriverInfo.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.FindLicense(LicenseID).DriverInfo.PersonID;

            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicenses frm = new frmReleaseDetainedLicenses(LicenseID);
            frm.ShowDialog();

            _RefreshDetainedLicenseList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicense.CurrentRow.Cells[3].Value;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsNumber.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbIsReleased.Text == "Detain ID" || cbIsReleased.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
