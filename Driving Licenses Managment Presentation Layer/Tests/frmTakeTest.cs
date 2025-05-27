using Driver_License_Business_Layer;
using Driver_License_Business_Layer.VisionTestAppointments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Tests
{
    public partial class frmTakeTest : Form
    {
        private clsTestsAppointments _TestAppointment;
        private int _TestAppointmentID = -1;

        private int _TestTypeID = -1;
        public frmTakeTest(int TestAppointmentID, int TestTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ctrlTakeTest1.Save();
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTakeTest1._LoadTestData(_TestAppointmentID, _TestTypeID);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlTakeTest1_Load(object sender, EventArgs e)
        {

        }
    }
}
