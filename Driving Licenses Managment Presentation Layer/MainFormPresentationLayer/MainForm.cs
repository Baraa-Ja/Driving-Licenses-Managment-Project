using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Applications.Licenses_Replacment;
using Driving_Licenses_Managment_Presentation_Layer.Applications.Release_Detained_License;
using Driving_Licenses_Managment_Presentation_Layer.Applications.Renew_License;
using Driving_Licenses_Managment_Presentation_Layer.Detain;
using Driving_Licenses_Managment_Presentation_Layer.Drivers;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.InternationalLicense;
using Driving_Licenses_Managment_Presentation_Layer.LDLApplication;
using Driving_Licenses_Managment_Presentation_Layer.Licenses;
using Driving_Licenses_Managment_Presentation_Layer.Licenses_Replacment;
using Driving_Licenses_Managment_Presentation_Layer.Login;
using Driving_Licenses_Managment_Presentation_Layer.TestTypes;
using Driving_Licenses_Managment_Presentation_Layer.UsersPresentationLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.MainFormsPresentationLayer
{
    public partial class frmMainForm : Form
    {
        frmLogin _frmLogin;
        public frmMainForm(frmLogin frm )
        {
            InitializeComponent();

            _frmLogin = frm;
        }
        private void peoepleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeople frm = new frmPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            lblUserLogged.Text = clsGlobal.CurrentUser.UserName.ToString();
            lblLoggedUser.Text = clsGlobal.CurrentUser.UserID.ToString();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frm = new frmUserCard(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void singOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLDLApplication frm = new frmAddEditLDLApplication();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void replaceForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicensesReplacment frm = new frmLicensesReplacment();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicense frm = new frmInternationalLicense();
            frm.ShowDialog();
        }

        private void renewDrivingLicesnseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReNewLicenses frm = new frmReNewLicenses();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void manageTestsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypes frm = new frmTestTypes();
            frm.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseManagment frm = new frmInternationalLicenseManagment();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenses frm = new frmReleaseDetainedLicenses();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainedLicenseList frm = new frmDetainedLicenseList();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications frm = new frmLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void releaseDetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainedLicenseList frm = new frmDetainedLicenseList();
            frm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void lblUserLogged_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
