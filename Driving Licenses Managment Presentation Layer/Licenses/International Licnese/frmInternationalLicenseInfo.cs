using Driver_License_Business_Layer;
using Driver_License_Business_Layer.International_License;
using Driving_Licenses_Managment_Presentation_Layer.Global;
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

namespace Driving_Licenses_Managment_Presentation_Layer.InternationalLicense
{
    public partial class frmInternationalLicenseInfo : Form
    {
        private clsInternationalLicense _InternationalLicense;
        private int _InternationalLicenseID = -1;
        public frmInternationalLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = internationalLicenseID;
        }

        private void _LoadData()
        {
            _InternationalLicense = clsInternationalLicense.FindInternationalLicense(_InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("No License With LicenseID = " + _InternationalLicenseID.ToString(), "Error", MessageBoxButtons.OK);
            }

            _FillLicense();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.Driverinfo.PersonInfo.Gendor == 0)
                picDriver.Image = Resources.Male_512;
            else
                picDriver.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.Driverinfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    picDriver.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _FillLicense()
        {
            _InternationalLicense = clsInternationalLicense.FindInternationalLicense(_InternationalLicenseID);

            lblDriverName.Text = _InternationalLicense.Driverinfo.PersonInfo.PersonFullname;
            lblInternatiolLicenseID.Text = _InternationalLicenseID.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.Driverinfo.PersonInfo.NationalNumber;
            lblGendor.Text = _InternationalLicense.Driverinfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive.ToString();
            lblIsActive.Text = (_InternationalLicense.IsActive == true) ? "Yes" : "false";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.Driverinfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExperationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
