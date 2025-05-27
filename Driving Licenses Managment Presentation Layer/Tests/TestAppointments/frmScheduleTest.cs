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

namespace Driving_Licenses_Managment_Presentation_Layer.TestAppointments
{
    public partial class frmScheduleTest : Form
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID = -1;

        private clsTestsAppointments _TestAppointment;
        private int _TestAppointmentID = -1;

        private int _ApplicationMode = -1;
        enum enMode { Addnew = 0, Update = 1 }
        enMode Mode;

        private int _TestTypeID;
        public frmScheduleTest(int LDLApplicationID, int TestAppointmentID, int TestTypeID, int ApplicationMode)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
            _ApplicationMode = ApplicationMode;
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {

            ctrlScheduleTest1.LoadScheduleTest(_LDLApplicationID, _TestTypeID, _TestAppointmentID, _ApplicationMode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ctrlScheduleTest1.Save();
            btnClose.PerformClick();
        }
    }
}
