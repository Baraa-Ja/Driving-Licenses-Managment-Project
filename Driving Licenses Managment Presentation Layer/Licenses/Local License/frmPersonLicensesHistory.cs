using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Licenses.Local_License
{
    public partial class frmPersonLicensesHistory : Form
    {
        private int _PersonID = -1;
        public frmPersonLicensesHistory()
        {
            InitializeComponent();
        }

        public frmPersonLicensesHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmPersonLicensesHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID != -1)
            {
                ctrlPersonInfoWithFillter1.LoadPersonInfo(_PersonID);
                ctrlPersonInfoWithFillter1.FilterEnabled = false;

                ctrlLicensesHistory1.LoadInfoByPersonID(_PersonID);

            }
            else
            {
                ctrlPersonInfoWithFillter1.FilterFocus();
                ctrlLicensesHistory1.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonLicensesHistory_Activated(object sender, EventArgs e)
        {
            btnClose.Focus();
        }
    }
}
