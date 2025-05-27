using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Licenses
{
    public partial class frmLicenseInfo : Form
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID = -1;

        private int _LicenseID;
        //public frmLicenseInfo(int LDLApplicationID)
        //{
        //    InitializeComponent();

        //    _LDLApplicationID = LDLApplicationID;
        //}

        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();

            _LicenseID = LicenseID;
        }

        private void ctrlLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void frmLicenseInfo_Load(object sender, EventArgs e)
        {
            //_LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);
            //clsApplications Application = clsApplications.FindApplication(_LDLApplication.ApplicationID);
            //clsLicenses License = clsLicenses.FindLicenseByApplicationID(Application.ApplicationID);

            //ctrlLicenseInfo1.LoadData(License.LicenseID, (byte)clsLicenses.enIssueReasons.First_Time);

            ctrlLicenseInfo1.LoadData(_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
