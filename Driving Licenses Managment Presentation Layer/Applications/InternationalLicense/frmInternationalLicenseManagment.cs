using Driver_License_Business_Layer;
using Driver_License_Business_Layer.International_License;
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

namespace Driving_Licenses_Managment_Presentation_Layer.InternationalLicense
{
    public partial class frmInternationalLicenseManagment : Form
    {
        private DataTable _dtInternationalLicenseApplications;
        public frmInternationalLicenseManagment()
        {
            InitializeComponent();
        }

        private void _RefreshInternationalLicenseList()
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbInternationalApplicationsFillter.SelectedIndex = 0;

            dgvInternationalLicense.DataSource = _dtInternationalLicenseApplications;
            lblRecordsNumber.Text = dgvInternationalLicense.Rows.Count.ToString();

            if (dgvInternationalLicense.Rows.Count > 0)
            {
                dgvInternationalLicense.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicense.Columns[0].Width = 160;

                dgvInternationalLicense.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicense.Columns[1].Width = 150;

                dgvInternationalLicense.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicense.Columns[2].Width = 130;

                dgvInternationalLicense.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicense.Columns[3].Width = 130;

                dgvInternationalLicense.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicense.Columns[4].Width = 180;

                dgvInternationalLicense.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicense.Columns[5].Width = 180;

                dgvInternationalLicense.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicense.Columns[6].Width = 120;

            }
        }


        private void frmInternationalLicenseManagment_Load(object sender, EventArgs e)
        {
            _RefreshInternationalLicenseList();
        }

        private void cbInternationalApplicationsFillter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbInternationalApplicationsFillter.Text == "Is Active")
            {
                txtFillter.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
                return;
            }
            else
            {
                txtFillter.Visible = (cbInternationalApplicationsFillter.Text != "None");
                cbIsActive.Visible = false;


                if (cbInternationalApplicationsFillter.Text == "None")
                {
                    txtFillter.Visible = false;
                    return;
                }
                else
                    txtFillter.Visible = true;

                txtFillter.Text = "";
                txtFillter.Focus();
            }
            txtFillter.Visible = true;
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {
            string FillterColumn = "";

            switch (cbInternationalApplicationsFillter.Text)
            {
                case "International License ID":
                    FillterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FillterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FillterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FillterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FillterColumn = "IsActive";
                    break;


                default:
                    FillterColumn = "None";
                    break;
            }


            if (txtFillter.Text.Trim() == "" || FillterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                _RefreshInternationalLicenseList();
                lblRecordsNumber.Text = dgvInternationalLicense.Rows.Count.ToString();
                return;
            }

            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FillterColumn, txtFillter.Text.Trim());

            lblRecordsNumber.Text = dgvInternationalLicense.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo((int)dgvInternationalLicense.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshInternationalLicenseList();
        }

        private void deActivateLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            InternationalLicense = clsInternationalLicense.FindInternationalLicense((int)dgvInternationalLicense.CurrentRow.Cells[0].Value);

            if(InternationalLicense.IsActive == true)
            {
                if (MessageBox.Show("Are You Sure You Want To Decativate This License?", "Confirme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    InternationalLicense.IsActive = false;
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Activate This License?", "Confirme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    InternationalLicense.IsActive = true;
            }

            InternationalLicense.UpdateLicense();

            _RefreshInternationalLicenseList();
        }

        private void picAddNewInternationalApplication_Click(object sender, EventArgs e)
        {
            frmInternationalLicense frm = new frmInternationalLicense();
            frm.ShowDialog();
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FillterColumn = "IsActive";
            string FillterValues = cbIsActive.Text;

            switch(FillterValues)
            {
                case "All":
                    break;
                case "Active":
                    FillterValues = "1";
                    break;
                case "Not Active":
                    FillterValues = "0";
                    break;
            }

            if (FillterValues == "All")
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FillterColumn, FillterValues);
                lblRecordsNumber.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();

            lblRecordsNumber.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmInternationalLicense frm = new frmInternationalLicense();
            frm.ShowDialog();
        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showPersonLicense_Click(object sender, EventArgs e)
        {
            int LicesneID = (int)dgvInternationalLicense.CurrentRow.Cells[0].Value;
            clsLicenses IntLicense = clsLicenses.FindLicense(LicesneID);
            int PersonID = IntLicense.DriverInfo.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicesneID = (int)dgvInternationalLicense.CurrentRow.Cells[0].Value;
            clsLicenses IntLicense = clsLicenses.FindLicense(LicesneID);
            int PersonID = IntLicense.DriverInfo.PersonID;

            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
