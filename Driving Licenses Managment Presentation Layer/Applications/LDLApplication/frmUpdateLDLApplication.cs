using Driver_License_Business_Layer;
using Driver_License_Business_Layer.ApplicationsBusiness;
using Driver_License_Business_Layer.LicenseClasses;
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
    public partial class frmUpdateLDLApplication : Form
    {
        int _LDLApplicationID = -1;
        clsLDLApplication _LDLApplication;
        public frmUpdateLDLApplication(int LDLApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;

        }

        private void _FillClassNameInComboBox()
        {
            DataTable ClassesDataTable = clsLicenseClasses.GetAllClasses();

            foreach (DataRow Row in ClassesDataTable.Rows)
            {
                cbClassNames.Items.Add(Row["ClassName"]);
            }

        }

        private void frmUpdateLDLApplication_Load(object sender, EventArgs e)
        {
            _FillClassNameInComboBox();

            _LDLApplication = clsLDLApplication.FindLDLApplication(_LDLApplicationID);
            cbClassNames.SelectedIndex = 2;

            clsApplications Application = _LDLApplication.FindApp(_LDLApplication.ApplicationID);

            if (_LDLApplication == null)
            {
                MessageBox.Show("This form will be closed because No LDLApplication with ID = " + _LDLApplicationID);
                this.Close();

                return;
            }

            //ctrlPersonInfoWithFillter1.changetxtamdfillter(Application.ApplicantPersonID.ToString(), "Person ID");
            ctrlPersonInfoWithFillter1.btnFindPerson_Click(sender, e);
            ctrlPersonInfoWithFillter1.Enabled = false;

            lblApplicationID.Text  = _LDLApplication.LDLApplicationID.ToString();
            dtpApplicationDate.Value = Application.ApplicationDate;
            cbClassNames.SelectedIndex = cbClassNames.FindString(clsLicenseClasses.FindLicenseClassByID(_LDLApplication.LicenseClassID).ClassName.ToString());
            lblApplicationFees.Text = Application.PaidFees.ToString();
            lblCreateBy.Text = Application.CreatedByUserID.ToString();
        }

        private bool _ValidatePerson(int PersonID)
        {
            if (ctrlPersonInfoWithFillter1.PersonID == 0)
            {
                this.ValidateChildren();
                MessageBox.Show("Some fields are not valid!, Please Select a Person To Link it With User",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                tabControl1.SelectedIndex = 0;
                return false;
            }

            return true;
        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(Temp.Text.Trim()))
            {
                e.Cancel = true;
                Temp.Focus();
                errorProvider1.SetError(Temp, "This Field Is Required");
            }
            else
            {
                errorProvider1.SetError(Temp, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplications Application = _LDLApplication.FindApp(_LDLApplication.ApplicationID);

            //_LDLApplication.ApplicantPersonID = ctrlPersonInfoWithFillter1.NewPersonID;
            _LDLApplication.ApplicationDate = dtpApplicationDate.Value;
            _LDLApplication.LicenseClassID = cbClassNames.SelectedIndex + 1;
            _LDLApplication.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            _LDLApplication.CreatedByUserID = int.Parse(lblCreateBy.Text);

            if (_LDLApplication.Save())
            {
                MessageBox.Show("Date Saved Successfully");
            }
            else
            {
                MessageBox.Show("Date Is Not Saved Successfully");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ctrlPersonInfoWithFillter1.PersonID == 0)
            {
                MessageBox.Show("Some fields are not valid!, Please Select a Person To Link it With User",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.ValidateChildren();
                //tabControl1.SelectedIndex = 0;
                e.Cancel = true;
            }
            else
            {
                tabControl1.SelectedIndex = 1;
                btnSave.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_ValidatePerson(ctrlPersonInfoWithFillter1.PersonID))
            {
                tabControl1.SelectedIndex = 1;
                btnSave.Enabled = true;
            }
        }
    }
}
