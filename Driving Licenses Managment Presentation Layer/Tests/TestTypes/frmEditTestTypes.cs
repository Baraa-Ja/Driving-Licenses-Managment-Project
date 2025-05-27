using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.TestTypes
{
    public partial class frmEditTestTypes : Form
    {

        private clsTestTypes _TestType;
        private int _TestTypID;
        public frmEditTestTypes(int TestType)
        {
            InitializeComponent();

            _TestTypID = TestType;
        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            _TestType = clsTestTypes.FindTestByID(_TestTypID);

            if (_TestType == null)
            {
                MessageBox.Show("This form will be closed because No _TestTypID with ID = " + _TestTypID);
                this.Close();

                return;
            }

            lblTestTypeID.Text = _TestTypID.ToString();
            txtTestTypeTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtTestTypeFees.Text = _TestType.TestTypeFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeTitle = txtTestTypeTitle.Text;
            _TestType.TestTypeFees = Convert.ToDecimal(txtTestTypeFees.Text);
            _TestType.TestTypeDescription = txtDescription.Text;

            if (_TestType.UpdateTestTypes())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);

            };


            if (!clsValidatoin.IsNumber(txtTestTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);
            };
        }
    }
}
