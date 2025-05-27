using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.VisionTestAppointments;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
using Driving_Licenses_Managment_Presentation_Layer.TestAppointments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Tests.TestAppointments
{
    public partial class frmTestAppointmenTable : Form
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID;

        private int _TestTypeID;
        public frmTestAppointmenTable(int LDLApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void TestAppointmentType(int TestTypeID)
        {
            switch (TestTypeID)
            {
                case 1:
                    pictureBox1.Image = Resources.Vision;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Vision Test Appointments";
                    break;
                case 2:
                    pictureBox1.Image = Resources.Writing_PNG_Download_Image;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Written Test Appointments";
                    break;
                case 3:
                    pictureBox1.Image = Resources.Street;
                    this.Text = lblTitle.Text;
                    lblTitle.Text = "Street Test Appointments";
                    break;
            }
        }

        private void _RefreshTestAppointmentsList()
        {
            DataView TestAppointmentsView = clsTestsAppointments.TestAppointmentsTable(_TestTypeID, _LDLApplicationID).DefaultView;
            dgvTestAppointments.DataSource = TestAppointmentsView;
            lblRecordCount.Text = dgvTestAppointments.Rows.Count.ToString();
        }

        public void LoadTestAppointmentsInfo()
        {
            TestAppointmentType(_TestTypeID);
            ctrlLDLApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
            _RefreshTestAppointmentsList();
        }

        private void frmTestAppointmenTable_Load(object sender, EventArgs e)
        {
            Text = clsTestTypes.FindTestByID(_TestTypeID).TestTypeTitle;
            LoadTestAppointmentsInfo();
        }

        private void picAddNewTestAppointment_Click_1(object sender, EventArgs e)
        {
            if (clsTestsAppointments.CheckAppointmentAlreadyExists(_TestTypeID, _LDLApplicationID, false))
            {
                MessageBox.Show("Person Already Has An Active Appointment For This Test You Can't Add More Appointments", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestsAppointments.CheckPersonAlreadyPassedTest(_TestTypeID, _LDLApplicationID, true))
            {
                MessageBox.Show("Person Already Passed This Test Befor, You Can Only Retake Failed Test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestsAppointments.CheckPersonAlreadyPassedTest(_TestTypeID, _LDLApplicationID, false))
            {
                frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID, -1, _TestTypeID, (int)clsApplications.enMode.Retake);
                frm.ShowDialog();

                _RefreshTestAppointmentsList();

                return;
            }
            else
            {
                frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID, -1, _TestTypeID, (int)clsApplicationTypes.EnApplicationTypes.NewLocalDrivingLicenseService);
                frm.ShowDialog();
            }

            _RefreshTestAppointmentsList();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[3].Value == true)
            {
                MessageBox.Show("You Can't Access This Appointment Because IT's Locked.");
            }
            else
            {
                frmScheduleTest frm = new frmScheduleTest(_LDLApplicationID, (int)dgvTestAppointments.CurrentRow.Cells[0].Value, _TestTypeID, (int)clsApplicationTypes.EnApplicationTypes.NewLocalDrivingLicenseService);
                frm.ShowDialog();
            }

            _RefreshTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvTestAppointments.CurrentRow.Cells[3].Value == true)
            {
                MessageBox.Show("You Can't Access This Appointment Because IT's Locked.");
            }
            else
            {
                frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.CurrentRow.Cells[0].Value, _TestTypeID);

                frm.ShowDialog();
            }

            _RefreshTestAppointmentsList();
        }
    }
}
