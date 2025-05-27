using Driver_License_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer
{
    public partial class frmAddEditPersonInfo : Form
    {

        private int _PersonID = -1;
        private clsPeople _Person;
        enum enMode { AddNew = 0, Update = 1};
        enMode _Mode;

        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;

            if(PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void FillCountriesInComboBox()
        {
            DataTable CountriesDataTable = clsCountries.GetAllCountries();

            foreach(DataRow Row in  CountriesDataTable.Rows)
            {
                cbCountries.Items.Add(Row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            FillCountriesInComboBox();
            cbCountries.SelectedIndex = 168;
            rbMale.Checked = true;

            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPeople();
                return;
            }

            _Person = clsPeople.FindPerson(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }

            lblMode.Text = "Update Person";
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtlNationalNumber.Text = _Person.NationalNumber;
            dtpDataOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFeMale.Checked = true;

            if (_Person.ImagePath != "")
                picPersonImage.Load(_Person.ImagePath);

            llRemove.Visible = (_Person.ImagePath != "");

            cbCountries.SelectedIndex = cbCountries.FindString(clsCountries.Find(_Person.NationalityCountryID).CountryName);

        }

        private void AddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if(string.IsNullOrWhiteSpace(Temp.Text.Trim()))
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
            int CountryID = clsCountries.Find(cbCountries.Text).CountryID;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNumber  = txtlNationalNumber.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDataOfBirth.Value;
            _Person.NationalityCountryID = CountryID;

            if (rbMale.Checked)
                _Person.Gendor = 0;
            else
                _Person.Gendor = 1;

            if (picPersonImage.ImageLocation != null)
                _Person.ImagePath = picPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Set DialogResult to None to prevent the form from closing
                //this.DialogResult = DialogResult.None;

                return;
            }

            if (_Person.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lblMode.Text = "Update Person";
            lblPersonID.Text = _Person.PersonID.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //string FirstName = txtFirstName.Text;
            //string SecondName = txtSecondName.Text;
            //string ThirdName = txtThirdName.Text;
            //string LastName = txtSecondName.Text;
            //string Email = txtEmail.Text;
            //string Phone = txtPhone.Text;
            //string Address = txtAddress.Text;
            //string NationalNo = txtlNationalNumber.Text;
            //Byte Gendor = Convert.ToByte( rbMale.Checked ? 1 : 0);
            //DateTime DateOfBirth = dtpDataOfBirth.Value;
            //int CountryID = cbCountries.SelectedIndex;
            //string ImagePath = picPersonImage.ImageLocation.ToString();

            DataBack?.Invoke(this, _Person.PersonID);

            this.Close();

            

        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            picPersonImage.ImageLocation = null;
            llRemove.Visible = false;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                picPersonImage.Load(selectedFilePath);
                // ...
            }

            llRemove.Visible = true;
        }

        private void picPersonImage_Click(object sender, EventArgs e)
        {

        }

        public delegate void DataBackHandler(object sender ,int PersonID);
        public event DataBackHandler DataBack;

        private void frmAddEditPersonInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void frmAddEditPersonInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataBack?.Invoke(this ,_Person.PersonID);
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
