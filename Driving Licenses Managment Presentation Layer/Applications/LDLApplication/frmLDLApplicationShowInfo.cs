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
    public partial class frmLDLApplicationShowInfo : Form
    {
        private int _LDLApplicationID = -1;
        public frmLDLApplicationShowInfo(int LDLApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmLDLApplicationShowInfo_Load(object sender, EventArgs e)
        {
            ctrlLDLApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LDLApplicationID);
        }

        private void ctrlLDLApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
