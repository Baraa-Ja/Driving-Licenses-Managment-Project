using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
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
    public partial class ctrlLDLApplicationInfo : UserControl
    {
        private clsLDLApplication _LDLApplciation;
        private int _LDLApplicationID = -1;

        private int _LicenseID;

        public int LDLApplicationID
        {
            get { return _LDLApplicationID; }
        }
        public ctrlLDLApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LDLApplciation = clsLDLApplication.FindLDLApplication(LocalDrivingLicenseApplicationID);
            _LDLApplicationID = _LDLApplciation.LDLApplicationID;

            if (_LDLApplciation == null)
            {
                _ResetLDLApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LDLApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLDLApplication();
        }

        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LDLApplciation = clsLDLApplication.FindLDLApplicationByApplicationID(ApplicationID);
            _LDLApplicationID = _LDLApplciation.LDLApplicationID;

            if (_LDLApplciation == null)
            {
                _ResetLDLApplicationInfo();


                MessageBox.Show("No Application with ApplicationID = " + LDLApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLDLApplication();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            clsApplications Application = _LDLApplciation.FindApp(_LDLApplciation.ApplicationID);

            lblApplicationID.Text = _LDLApplciation.ApplicationID.ToString();
            lblStatus.Text = Application.StatusText;
            lblFees.Text = Application.PaidFees.ToString();
            lblType.Text = Application.ApplicationTypeInfo.ApplicationTitle;
            lblApplicant.Text = Application.ApplicantFullName;
            lblApplicationDate.Text = clsFormat.DateToShort(Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(Application.LastStatusDate);
            lblCreatedByUser.Text = clsUsers.FindUser(Application.CreatedByUserID).UserName;
        }
        private void _FillLDLApplication()
        {
            _LDLApplicationID = _LDLApplciation.LDLApplicationID;
            lblLDLApplicationID.Text = _LDLApplicationID.ToString();
            lblClassLicense.Text = clsLicenseClasses.FindLicenseClassByID(_LDLApplciation.LicenseClassID).ClassName;
            lblPassedTestsCount.Text = _LDLApplciation.PassedTestsCount(_LDLApplicationID).ToString();

            LoadApplicationInfo(_LDLApplciation.ApplicationID);

        }

        private void _ResetLDLApplicationInfo()
        {
            _LDLApplicationID = -1;

            lblLDLApplicationID.Text = "????";
            lblClassLicense.Text = "????";
            lblPassedTestsCount.Text = "????";
            lblApplicationID.Text = "????";
            lblStatus.Text = "????";
            lblFees.Text = "????";
            lblType.Text = "????";
            lblApplicant.Text = "????";
            lblApplicationDate.Text = "????";
            lblStatusDate.Text = "????";
            lblCreatedByUser.Text = "????";

        }

        private void lklShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_LDLApplciation.FindApp(_LDLApplciation.ApplicationID).ApplicantPersonID);
            frm.ShowDialog();

            LoadApplicationInfo(_LDLApplciation.ApplicationID);
        }

        private void lklShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmLicenseInfo frm = new frmLicenseInfo(_LDLApplciation.GetActiveLicenseID());
            frm.ShowDialog();
        }
    }
}
