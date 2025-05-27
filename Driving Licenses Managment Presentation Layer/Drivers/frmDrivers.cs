using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
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

namespace Driving_Licenses_Managment_Presentation_Layer.Drivers
{
    public partial class frmDrivers : Form
    {
        private clsDrivers _Driver;
        private clsLicenses _License;
        private clsApplications _Application;
        private clsLDLApplication _LDLApplication;
        private DataTable _dtAlldrivers;
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void _RefreshDriversList()
        {
            _dtAlldrivers = clsDrivers.GetAllDrivers();
            dgvDrivers.DataSource = _dtAlldrivers;

            cbFillterDriversList.SelectedIndex = 0;
            lblRecordsNumber.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {

            string FillterColumn = "";

            switch (cbFillterDriversList.Text)
            {
                case "Driver ID":
                    FillterColumn = "DriverID";
                    break;
                case "Person ID":
                    FillterColumn = "PersonID";
                    break;
                case "National No":
                    FillterColumn = "National No";
                    break;
                case "FullName":
                    FillterColumn = "FullName";
                    break;
                case "Date":
                    FillterColumn = "Date";
                    break;
                case "Active Licenses":
                    FillterColumn = "ActiveLicenses";
                    break;
                default:
                    FillterColumn = "None";
                    break;
            }

            if (txtFillter.Text.Trim() == "" || FillterColumn == "None")
            {
                _dtAlldrivers.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (FillterColumn == "PersonID" || FillterColumn == "DriverID")
            {
                if (!int.TryParse(txtFillter.Text.ToString().Trim(), out int result))
                {
                    errorProvider1.SetError(txtFillter, "Enter Numeric Type please");
                }
                else
                {
                    _dtAlldrivers.DefaultView.RowFilter = string.Format("{0} = {1}", FillterColumn, txtFillter.Text.Trim());
                    errorProvider1.SetError(txtFillter, "");
                }

            }

            else
            {
                _dtAlldrivers.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FillterColumn, txtFillter.Text.Trim());
            }

            lblRecordsNumber.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void cbFillterDriversList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFillter.Visible = true;

            if (cbFillterDriversList.Text == "Active Licenses")
            {
                txtFillter.Visible = false;
                cbLicenseActive.Visible = true;
                cbLicenseActive.Focus();
                cbLicenseActive.SelectedIndex = 0;
            }

            else

            {

                txtFillter.Visible = (cbFillterDriversList.Text != "None");
                cbLicenseActive.Visible = false;

                txtFillter.Text = "";
                txtFillter.Focus();
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Driver = clsDrivers.FindDriver((int)dgvDrivers.CurrentRow.Cells[0].Value);
            
            frmPersonDetails frm = new frmPersonDetails(_Driver.PersonID);
            frm.ShowDialog();
        }

        private void showPersonLiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(_Driver.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbLicenseActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "ActiveLicenses";
            string FilterValue = cbLicenseActive.Text;

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
                _dtAlldrivers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAlldrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsNumber.Text = _dtAlldrivers.Rows.Count.ToString();
        }
    }
}
