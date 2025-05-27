using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer
{
    public partial class frmUpdateApplicationType : Form
    {
        private clsApplicationTypes _ApplicationType;
        private int _ApplcationTypeID;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();

            _ApplcationTypeID = ApplicationTypeID;

        }


        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _ApplicationType = clsApplicationTypes.FindApplicationTypeByID(_ApplcationTypeID);

            if (_ApplicationType == null)
            {
                MessageBox.Show("This form will be closed because No ApplicationType with ID = " + _ApplcationTypeID);
                this.Close();

                return;
            }

            lblApplicationTypeID.Text = _ApplcationTypeID.ToString();
            txtApplicationTitle.Text = _ApplicationType.ApplicationTitle;
            txtApplicationTypeFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds Are Not Valied, Put The Mouse Over The Red Icon To See The Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTitle = txtApplicationTitle.Text;
            _ApplicationType.ApplicationFees = Convert.ToDecimal(txtApplicationTypeFees.Text);

            if (_ApplicationType._UpdateApplicationType())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtApplicationTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtApplicationTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTitle, "Title Can't be Empty");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTitle, null);
            }
        }

        private void txtApplicationTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApplicationTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTypeFees, "Title Can't be Empty");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, null);
            }

            if(!clsValidatoin.IsNumber(txtApplicationTypeFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApplicationTypeFees, "Enter A Number Please");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, null);
            }
        }
    }
}
