using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer
{
    public partial class ctrlPersonCard : UserControl
    {
        private  clsPeople _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPeople.FindPerson(PersonID);

            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person With PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationaNumber)
        {
            _Person = clsPeople.FindPerson(NationaNumber);

            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person With NationalNo = " + NationaNumber.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonPhoto.Image = Resources.Male_512;
            else
                pbPersonPhoto.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonPhoto.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblName.Text = _Person.FullName();
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountries.Find(_Person.NationalityCountryID).CountryName;
            txtAddress.Text = _Person.Address;
            _LoadPersonImage();

        }

        private void _ResetPersonInfo()
        {
            _PersonID = -1;

            lblPersonID.Text = "????";
            lblName.Text = "????";
            lblNationalNo.Text = "????";
            lblGendor.Text = "????";
            pbGendorPhoto.Image = Resources.Man_32;
            lblEmail.Text = "????";
            lblPhone.Text = "????";
            txtAddress.Text = "????";
            lblPersonID.Text = "????";
            lblDateOfBirth.Text = "????";
            lblCountry.Text = "????";
            pbPersonPhoto.Image = Resources.Male_512;

        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            //frm.DataBack += Form_DataBack;
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }
    }
}
