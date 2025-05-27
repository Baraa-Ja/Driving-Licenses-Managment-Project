using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driving_Licenses_Managment_Presentation_Layer.IssueLicenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License;
using Driving_Licenses_Managment_Presentation_Layer.TestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.Tests.TestAppointments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.LDLApplication
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        private DataTable _LDLApplicationsDataTable;
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshLDLApplicationsList()
        {
            _LDLApplicationsDataTable = clsLDLApplication.GetLDLApplications();
            dgvLDLApplications.DataSource = _LDLApplicationsDataTable;
            lblRecordsNumber.Text = dgvLDLApplications.Rows.Count.ToString();

            if(dgvLDLApplications.Rows.Count > 0)
            {
                dgvLDLApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLDLApplications.Columns[0].Width = 90;

                dgvLDLApplications.Columns[1].HeaderText = "Driving Class";
                dgvLDLApplications.Columns[1].Width = 260;

                dgvLDLApplications.Columns[2].HeaderText = "National No.";
                dgvLDLApplications.Columns[2].Width = 110;

                dgvLDLApplications.Columns[3].HeaderText = "Full Name";
                dgvLDLApplications.Columns[3].Width = 305;

                dgvLDLApplications.Columns[4].HeaderText = "Application Date";
                dgvLDLApplications.Columns[4].Width = 160;

                dgvLDLApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLDLApplications.Columns[5].Width = 65;

                dgvLDLApplications.Columns[6].HeaderText = "Status";
                dgvLDLApplications.Columns[6].Width = 90;

            }

            cbLDLApplicationsFillter.SelectedIndex = 0;
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLDLApplicationsList();

        }

        private void cbLDLApplicationsFillter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFillter.Visible = cbLDLApplicationsFillter.Text != "None";

            if(txtFillter.Visible)
            {
                txtFillter.Text = "";
                txtFillter.Focus();
            }

            _LDLApplicationsDataTable.DefaultView.RowFilter = "";
            lblRecordsNumber.Text = dgvLDLApplications.Rows.Count.ToString();
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {
            string FillterColumn = "";

            switch (cbLDLApplicationsFillter.Text)
            {
                case "L.D.L.Applicaion ID":
                    FillterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "Driving Class":
                    FillterColumn = "ClassName";
                    break;
                case "National Number":
                    FillterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FillterColumn = "FullName";
                    break;
                case "Passed Tests":
                    FillterColumn = "PassedTestCount";
                    break;
                case "Status":
                    FillterColumn = "Status";
                    break;
                default:
                    FillterColumn = "None";
                    break;

            }


            if (txtFillter.Text.Trim() == "" || FillterColumn == "None")
            {
                _LDLApplicationsDataTable.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvLDLApplications.Rows.Count.ToString();
                return;
            }


            if (FillterColumn == "LocalDrivingLicenseApplicationID" || FillterColumn == "PassedTestCount")
            {
                _LDLApplicationsDataTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", FillterColumn, txtFillter.Text.Trim());
            }
            else
            {
                _LDLApplicationsDataTable.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FillterColumn, txtFillter.Text.Trim());
            }

            lblRecordsNumber.Text = dgvLDLApplications.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showApplicationDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLDLApplicationShowInfo frm = new frmLDLApplicationShowInfo((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            //frmLocalDrivingLicenseApplications_Load(null, null);
            _RefreshLDLApplicationsList();

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication frm = new frmAddEditLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        private void canceleApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clsLDLApplication LDLApplication = clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value);

            //clsApplications Application = LDLApplication.FindApp(LDLApplication.ApplicationID);

            //if(Application.ApplicationStatus == 2)
            //{
            //    Application.ApplicationStatus = 1;
            //}
            //else
            //{
            //    Application.ApplicationStatus = 2;

            //}

            //Application.Save();


            //_RefreshLDLApplicationsList();

            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LDLApplicationID = (int)dgvLDLApplications.CurrentRow.Cells[0].Value;

            clsLDLApplication LDLApplications =clsLDLApplication.FindLDLApplication(LDLApplicationID);

            if (LDLApplications != null)
            {
                if (LDLApplications.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin, Because of Connected Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int LDLApplicationID = (int)dgvLDLApplications.CurrentRow.Cells[0].Value;

            clsLDLApplication LDLApplications = clsLDLApplication.FindLDLApplication(LDLApplicationID);

            if (LDLApplications != null)
            {
                if (LDLApplications.DeleteLDLApplication())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //public void EnableVisionTestAndDisableWrittenTest()
        //{
        //    scheduleAVisionTestToolStripMenuItem.Enabled = true;
        //    scheduleAWrittenTestToolStripMenuItem.Enabled = false;
        //}
        //public void EnableAndDisableVisionAndWrittenTests()
        //{
        //    scheduleAVisionTestToolStripMenuItem.Enabled = false;
        //    scheduleAWrittenTestToolStripMenuItem.Enabled = true;
        //    scheduleAStreetTestToolStripMenuItem.Enabled = false;
        //}
        private void scheduleAVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) > 0)
            //    EnableAndDisableVisionAndWrittenTests();

            //frmTestAppointments frm = new frmTestAppointments((int)dgvLDLApplications.CurrentRow.Cells[0].Value, (int)clsTestTypes.enTestTypes.VisionTest);
            //frm.ShowDialog();

            //_RefreshLDLApplicationsList();

            frmTestAppointmenTable frm = new frmTestAppointmenTable((int)dgvLDLApplications.CurrentRow.Cells[0].Value, (int)clsTestTypes.enTestTypes.VisionTest);
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        //private void scheduleAVisionTestToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{

        //}

        //private void scheduleAVisionTestToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        //{

        //}

        //public  void EnableAndDisableWrittentAndStreetTests()
        //{
        //    scheduleAVisionTestToolStripMenuItem.Enabled = false;
        //    scheduleAWrittenTestToolStripMenuItem.Enabled = false;
        //    scheduleAStreetTestToolStripMenuItem.Enabled = true;
        //}

        //private void sechduelATestToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //}

        //private void dgvLDLApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void dgvLDLApplications_Click(object sender, EventArgs e)
        //{
        //    //issueDrivingLicenseFirstTimeToolStripMenuItem.PerformClick();
        //}

        //private void dgvLDLApplications_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    clsApplications Application = clsApplications.FindApplication(clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).ApplicationID);

        //    if(clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).ApplicationStatus == 2)
        //    {
        //        editApplicationToolStripMenuItem.Enabled = false;
        //        sechduelATestToolStripMenuItem.Enabled = false;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //        showLicenseToolStripMenuItem.Enabled = false;
        //    }


        //    if (clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) == 0)
        //    {
        //        sechduelATestToolStripMenuItem.Enabled = true;
        //        editApplicationToolStripMenuItem.Enabled = true;
        //        deleteApplicationToolStripMenuItem.Enabled = true;
        //        canceleApplicationToolStripMenuItem.Enabled = true;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //        showLicenseToolStripMenuItem.Enabled = false;
        //        EnableVisionTestAndDisableWrittenTest();
        //    }
        //    else if (clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) == 1)
        //    {
        //        sechduelATestToolStripMenuItem.Enabled = true;
        //        editApplicationToolStripMenuItem.Enabled = true;
        //        deleteApplicationToolStripMenuItem.Enabled = true;
        //        canceleApplicationToolStripMenuItem.Enabled = true;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //        showLicenseToolStripMenuItem.Enabled = false;
        //        EnableAndDisableVisionAndWrittenTests();
        //    }
        //    else if (clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) == 2)
        //    {
        //        sechduelATestToolStripMenuItem.Enabled = true;
        //        editApplicationToolStripMenuItem.Enabled = true;
        //        deleteApplicationToolStripMenuItem.Enabled = true;
        //        canceleApplicationToolStripMenuItem.Enabled = true;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //        showLicenseToolStripMenuItem.Enabled = false;
        //        EnableAndDisableWrittentAndStreetTests();
        //    }
        //    else if ((clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) == 3) &&
        //    (Application.ApplicationStatus == 3))
        //    {
        //        sechduelATestToolStripMenuItem.Enabled = false;
        //        editApplicationToolStripMenuItem.Enabled = false;
        //        deleteApplicationToolStripMenuItem.Enabled = false;
        //        canceleApplicationToolStripMenuItem.Enabled = false;
        //        showLicenseToolStripMenuItem.Enabled = true;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
        //    }
        //    else if (clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value).PassedTestsCount((int)dgvLDLApplications.CurrentRow.Cells[0].Value) == 3)
        //    {
        //        sechduelATestToolStripMenuItem.Enabled = false;
        //        editApplicationToolStripMenuItem.Enabled = false;
        //        deleteApplicationToolStripMenuItem.Enabled = false;
        //        canceleApplicationToolStripMenuItem.Enabled = false;
        //        issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
        //        showLicenseToolStripMenuItem.Enabled = false;
        //    }

        //}

        private void scheduleAWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointmenTable frm = new frmTestAppointmenTable((int)dgvLDLApplications.CurrentRow.Cells[0].Value, (int)clsTestTypes.enTestTypes.WrittentTest);
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        private void scheduleAStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestAppointmenTable frm = new frmTestAppointmenTable((int)dgvLDLApplications.CurrentRow.Cells[0].Value, (int)clsTestTypes.enTestTypes.StreetTest);
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueLicenses frm = new frmIssueLicenses((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLDLApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLDLApplication.FindLDLApplication(
               LDLApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmLicenseInfo frm = new frmLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLApplication LDLApplciation = clsLDLApplication.FindLDLApplication((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(LDLApplciation.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbLDLApplicationsFillter.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication frm = new frmAddEditLDLApplication();
            frm.ShowDialog();

            _RefreshLDLApplicationsList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LDLApplicationID = (int)dgvLDLApplications.CurrentRow.Cells[0].Value;
            clsLDLApplication LDLApplications =clsLDLApplication.FindLDLApplication(LDLApplicationID);

            int TotalPassedTests = (int)dgvLDLApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LDLApplications.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && (LDLApplications.ApplicationStatus == (byte)clsApplications.enStatus.New);
            sechduelATestToolStripMenuItem.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            canceleApplicationToolStripMenuItem.Enabled = (LDLApplications.ApplicationStatus == (byte)clsApplications.enStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            deleteApplicationToolStripMenuItem.Enabled = (LDLApplications.ApplicationStatus == (byte)clsApplications.enStatus.New ||
               LDLApplications.ApplicationStatus == (byte)clsApplications.enStatus.Canceled);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LDLApplications.DoesPassTestType(clsTestTypes.enTestTypes.VisionTest); ;
            bool PassedWrittenTest = LDLApplications.DoesPassTestType(clsTestTypes.enTestTypes.WrittentTest);
            bool PassedStreetTest = LDLApplications.DoesPassTestType(clsTestTypes.enTestTypes.StreetTest);

            sechduelATestToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LDLApplications.ApplicationStatus == (byte)clsApplications.enStatus.New);

            if (sechduelATestToolStripMenuItem.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                scheduleAVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                scheduleAWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                scheduleAStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }
    }
}
