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

namespace Driving_Licenses_Managment_Presentation_Layer.UsersPresentationLayer
{
    public partial class frmManageUsers : Form
    {
        private static DataTable _dtAllUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUsers.GetAllUsers();
            dgvUsersInfo.DataSource = _dtAllUsers;
            cbFillterUsers.SelectedIndex = 0;
            lblNumberOfRecords.Text = dgvUsersInfo.Rows.Count.ToString();

            dgvUsersInfo.Columns[0].HeaderText = "User ID";
            dgvUsersInfo.Columns[0].Width = 110;

            dgvUsersInfo.Columns[1].HeaderText = "Person ID";
            dgvUsersInfo.Columns[1].Width = 120;

            dgvUsersInfo.Columns[2].HeaderText = "Full Name";
            dgvUsersInfo.Columns[2].Width = 350;

            dgvUsersInfo.Columns[3].HeaderText = "User Name";
            dgvUsersInfo.Columns[3].Width = 120;

            dgvUsersInfo.Columns[4].HeaderText = "Is Active";
            dgvUsersInfo.Columns[4].Width = 120;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }

        private void txtFillter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFillterUsers.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFillter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvUsersInfo.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFillter.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFillter.Text.Trim());

            lblNumberOfRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void cbFillterUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFillterUsers.Text == "Is Active")
            {
                txtFillter.Visible = false;
                cbIsActiveList.Visible = true;
                cbIsActiveList.Focus();
                cbIsActiveList.SelectedIndex = 0;
            }

            else

            {

                txtFillter.Visible = (cbFillterUsers.Text != "None");
                cbIsActiveList.Visible = false;

                txtFillter.Text = "";
                txtFillter.Focus();
            }

        }
    

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard frm = new frmUserCard((int)dgvUsersInfo.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsersInfo.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This User with ID: [" + dgvUsersInfo.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUsers._DeleteUser((int)dgvUsersInfo.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _RefreshUsersList();
                }

                else
                    MessageBox.Show("User is not deleted,Because The Is A Data Connected To This Person.");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsersInfo.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void cbIsActiveList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActiveList.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblNumberOfRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFillterUsers.Text == "Person ID" || cbFillterUsers.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
