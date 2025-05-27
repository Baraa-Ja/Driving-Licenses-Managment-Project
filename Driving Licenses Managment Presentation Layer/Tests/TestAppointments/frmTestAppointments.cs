using Driver_License_Business_Layer;
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
    public partial class frmTestAppointments : Form
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID;

        private int _TestTypeID;
        public frmTestAppointments(int LDLApplicationID, int TestTypeID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            Text = clsTestTypes.FindTestByID(_TestTypeID).TestTypeTitle;
            ctrlTestAppointments1.LoadTestAppointmentsInfo(_LDLApplicationID,_TestTypeID);
        }

        private void ctrlTestAppointments1_Load(object sender, EventArgs e)
        {

        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
