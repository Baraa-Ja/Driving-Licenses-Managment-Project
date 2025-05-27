using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using System;
using System.Collections;
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
    public partial class frmAddEditLDLApplication : Form
    {
        private clsLDLApplication _LDLApplication;
        private int _LDLApplicationID = -1;
        private int _SelectedPersonID = -1;
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public frmAddEditLDLApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditLDLApplication(int LDLApplictionID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplictionID;
            _Mode = enMode.Update;

        }

        private void _FillClassNameInComboBox()
        {
            DataTable ClassesDataTable = clsLicenseClasses.GetAllClasses();

            foreach(DataRow Row in  ClassesDataTable.Rows)
            {
                cbClassNames.Items.Add(Row["ClassName"]);
            }

        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillClassNameInComboBox();


            if (_Mode == enMode.AddNew)
            {
                _LDLApplication = new clsLDLApplication();
                ctrlPersonInfoWithFillter1.FilterFocus();
                TpApplicationInfo.Enabled = false;

                cbClassNames.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationTypes.FindApplicationTypeByID((int)clsApplications.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                //we set the max date to 18 years from today, and set the default value the same.

                clsLicenseClasses LicenseClassInfo = clsLicenseClasses.FindLicenseClassByName(cbClassNames.Text);
                int LicenseClassID = LicenseClassInfo.LicenseClassID;

                dtpApplicationDate.MaxDate = DateTime.Now.AddYears(-LicenseClassInfo.MinimumAllowedAge);
                dtpApplicationDate.Value = (dtpApplicationDate.MaxDate);

                //should not allow adding age more than 100 years
                dtpApplicationDate.MinDate = DateTime.Now.AddYears(-100);

                lblCreateBy.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {

                TpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;

            }

            lblMode.Text = this.Text = ((_Mode == enMode.AddNew) ? "Add New" : "Update" ) + "Local Driving License Application";

        }

        private void _LoadData()
        {
            ctrlPersonInfoWithFillter1.FilterEnabled = false;
            _LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LDLApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonInfoWithFillter1.LoadPersonInfo(_LDLApplication.ApplicantPersonID);
            lblApplicationID.Text = _LDLApplication.LDLApplicationID.ToString();
            dtpApplicationDate.Text = clsFormat.DateToShort(_LDLApplication.ApplicationDate);
            cbClassNames.SelectedIndex = cbClassNames.FindString(clsLicenseClasses.FindLicenseClassByID(_LDLApplication.LicenseClassID).ClassName);
            lblApplicationFees.Text = _LDLApplication.PaidFees.ToString();
            lblCreateBy.Text = clsUsers.FindUser(_LDLApplication.CreatedByUserID).UserName;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonInfoWithFillter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                TpApplicationInfo.Enabled = true;
                tabControl1.SelectedTab = tabControl1.TabPages["TpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonInfoWithFillter1.FilterFocus();
            }
        }

        private void frmLDLApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode == enMode.Update)
                _LoadData();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsLicenseClasses LicenseClassInfo = clsLicenseClasses.FindLicenseClassByName(cbClassNames.Text);

            int LicenseClassID = clsLicenseClasses.FindLicenseClassByName(cbClassNames.Text).LicenseClassID;


            int ActiveApplicationID = clsApplications.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplications.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbClassNames.Focus();
                return;
            }


            //check if user already have issued license of the same driving  class.
            if (clsLicenses.IsLicenseExistByPersonID(ctrlPersonInfoWithFillter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte Years = 0;

            try
            {
                Years = Convert.ToByte(DateTime.Now.AddYears(-dtpApplicationDate.Value.Year).Year);
            }
            catch
            {
                MessageBox.Show("Use A Valid Date Please");
            }

            if (!clsLDLApplication.IsAgeAllowedForLicenseClass(LicenseClassID,Years))
            {
                MessageBox.Show($"Person Age Is Not Allowed For This License, MinimumeAge Should Be {LicenseClassInfo.MinimumAllowedAge}", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LDLApplication.ApplicantPersonID = ctrlPersonInfoWithFillter1.PersonID; ;
            _LDLApplication.ApplicationDate = DateTime.Now;
            _LDLApplication.ApplicationTypeID = 1;
            _LDLApplication.ApplicationStatus = (int)clsApplications.enStatus.New;
            _LDLApplication.LastStatusDate = DateTime.Now;
            _LDLApplication.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            _LDLApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LDLApplication.LicenseClassID = LicenseClassID;


            if (_LDLApplication.Save())
            {
                lblApplicationID.Text = _LDLApplication.LDLApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblMode.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditLDLApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonInfoWithFillter1.FilterFocus();
        }

        private void ctrlPersonInfoWithFillter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void cbClassNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsLicenseClasses LicenseClassInfo = clsLicenseClasses.FindLicenseClassByName(cbClassNames.Text);
            int LicenseClassID = LicenseClassInfo.LicenseClassID;

            dtpApplicationDate.MaxDate = DateTime.Now.AddYears(-LicenseClassInfo.MinimumAllowedAge);
            dtpApplicationDate.Value = (dtpApplicationDate.MaxDate);
        }
    }
}
