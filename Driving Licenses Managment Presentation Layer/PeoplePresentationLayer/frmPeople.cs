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


namespace Driving_Licenses_Managment_Presentation_Layer
{
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();

        }

        private static DataTable _dtAllPeople = clsPeople.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNumber",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "Gendor", "DateOfBirth", "Nationality",
                                                         "Phone", "Email");
        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPeople.GetAllPeople();
            _dtPeople= _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNumber",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "Gendor", "DateOfBirth", "Nationality",
                                                         "Phone", "Email");

            dgvPeopleInfo.DataSource = _dtPeople;
            lblRecordsNumber.Text = dgvPeopleInfo.Rows.Count.ToString();
        }

        private void People_Load(object sender, EventArgs e)
        {

            dgvPeopleInfo.DataSource = _dtPeople;
            cbFillterPeopleList .SelectedIndex = 0;
            lblRecordsNumber.Text = dgvPeopleInfo.Rows.Count.ToString();
            if (dgvPeopleInfo.Rows.Count > 0)
            {

                dgvPeopleInfo.Columns[0].HeaderText = "Person ID";
                dgvPeopleInfo.Columns[0].Width = 100;

                dgvPeopleInfo.Columns[1].HeaderText = "National No.";
                dgvPeopleInfo.Columns[1].Width = 110;


                dgvPeopleInfo.Columns[2].HeaderText = "First Name";
                dgvPeopleInfo.Columns[2].Width = 110;

                dgvPeopleInfo.Columns[3].HeaderText = "Second Name";
                dgvPeopleInfo.Columns[3].Width = 130;


                dgvPeopleInfo.Columns[4].HeaderText = "Third Name";
                dgvPeopleInfo.Columns[4].Width = 110;

                dgvPeopleInfo.Columns[5].HeaderText = "Last Name";
                dgvPeopleInfo.Columns[5].Width = 110;

                dgvPeopleInfo.Columns[6].HeaderText = "Gendor";
                dgvPeopleInfo.Columns[6].Width = 110;

                dgvPeopleInfo.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleInfo.Columns[7].Width = 130;

                dgvPeopleInfo.Columns[8].HeaderText = "Nationality";
                dgvPeopleInfo.Columns[8].Width = 110;


                dgvPeopleInfo.Columns[9].HeaderText = "Phone";
                dgvPeopleInfo.Columns[9].Width = 110;


                dgvPeopleInfo.Columns[10].HeaderText = "Email";
                dgvPeopleInfo.Columns[10].Width = 160;
            }

        }

        private void cbFillterPeopleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFillter.Visible = (cbFillterPeopleList.Text != "None");

            if (txtFillter.Visible)
            {
                txtFillter.Text = "";
                txtFillter.Focus();
            }
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFillterPeopleList.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNumber";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Gendor":
                    FilterColumn = "Gendor";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFillter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvPeopleInfo.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFillter.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFillter.Text.Trim());

            lblRecordsNumber.Text = dgvPeopleInfo.Rows.Count.ToString();
        }

        private void txtFillter_Validating(object sender, CancelEventArgs e)
        {

        }

        private void ShowDetailstoolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvPeopleInfo.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void btnAddNewPerson_Click_1(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)dgvPeopleInfo.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Are You Sure You Want To Delete This Person with ID: [" + dgvPeopleInfo.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPeople._DeletePerson((int)dgvPeopleInfo.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshPeopleList();
                }

                else
                    MessageBox.Show("Contact is not deleted,Because There are Data Connected To This Person.");
            }

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFillterPeopleList.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
