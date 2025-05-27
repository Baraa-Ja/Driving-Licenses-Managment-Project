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

namespace Driving_Licenses_Managment_Presentation_Layer.TestTypes
{
    public partial class frmTestTypes : Form
    {
        public frmTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            DataView TestTypesView = clsTestTypes.GetAllTestTypes().DefaultView;
            dgvTestTypes.DataSource = TestTypesView;
            lblRecordsNumber.Text = dgvTestTypes.Rows.Count.ToString();


            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 120;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }


        private void frmTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
        }

        private void EditTestTypes_Click(object sender, EventArgs e)
        {
            frmEditTestTypes frm = new frmEditTestTypes((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshTestTypesList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
