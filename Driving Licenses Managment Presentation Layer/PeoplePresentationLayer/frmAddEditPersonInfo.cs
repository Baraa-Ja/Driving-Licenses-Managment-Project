using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Driving_Licenses_Managment_Presentation_Layer
{
    public partial class frmAddEditPersonInfo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        private int _PersonID = -1;
        private clsPeople _Person;

        public enum enGendor { Male = 0, Female = 1 };
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPeople();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            //set default image for the person.
            if (rbMale.Checked)
                picPersonImage.Image = Resources.Male_512;
            else
                picPersonImage.Image = Resources.Female_512;

            //hide/show the remove linke incase there is no image for the person.
            llRemove.Visible = (picPersonImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDataOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDataOfBirth.Value = dtpDataOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDataOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            cbCountries.SelectedIndex = cbCountries.FindString("Syria");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtlNationalNumber.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void FillCountriesInComboBox()
        {
            DataTable CountriesDataTable = clsCountries.GetAllCountries();

            foreach (DataRow Row in CountriesDataTable.Rows)
            {
                cbCountries.Items.Add(Row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            _Person = clsPeople.FindPerson(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtlNationalNumber.Text = _Person.NationalNumber;
            dtpDataOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFeMale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);


            //load person image incase it was set.
            if (_Person.ImagePath != "")
            {
                picPersonImage.ImageLocation = _Person.ImagePath;

            }

            //hide/show the remove linke incase there is no image for the person.
            llRemove.Visible = (_Person.ImagePath != "");

        }

        private void AddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it is changed then we copy the new image
            if (_Person.ImagePath != picPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (picPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = picPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        picPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = clsCountries.Find(cbCountries.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNumber = txtlNationalNumber.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDataOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            _Person.NationalityCountryID = NationalityCountryID;

            if (picPersonImage.ImageLocation != null)
                _Person.ImagePath = picPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblMode.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            picPersonImage.ImageLocation = null;


            if (rbMale.Checked)
                picPersonImage.Image = Resources.Male_512;
            else
                picPersonImage.Image = Resources.Female_512;

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
                picPersonImage.Load(selectedFilePath);
                llRemove.Visible = true;
                // ...
            }

        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (picPersonImage.ImageLocation == null)
                picPersonImage.Image = Resources.Male_512;

        }

        private void rbFeMale_Click(object sender, EventArgs e)
        {
            if (picPersonImage.ImageLocation == null)
                picPersonImage.Image = Resources.Female_512;
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
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;              
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
                Temp.Focus();
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtlNationalNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtlNationalNumber, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtlNationalNumber, null);
            }

            //Make sure the national number is not used by another person
            if (txtlNationalNumber.Text.Trim() != _Person.NationalNumber && clsPeople._IsPersonExists(txtlNationalNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtlNationalNumber, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtlNationalNumber, null);
            }
        }

    }
}
