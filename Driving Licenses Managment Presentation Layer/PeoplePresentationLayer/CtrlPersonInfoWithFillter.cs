using Driver_License_Business_Layer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.PeoplePresentationLayer
{
    public partial class CtrlPersonInfoWithFillter : UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFillter.Enabled = _FilterEnabled;
            }
        }

        public CtrlPersonInfoWithFillter()
        {
            InitializeComponent();
        }

        private int _PersonID = -1;

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPeople SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFillterPersonInfo.SelectedIndex = 1;
            txtFillter.Text = PersonID.ToString();
            FindNow();

        }
        private void FindNow()
        {
            switch (cbFillterPersonInfo.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFillter.Text));

                    break;

                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFillter.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void cbFillterPersonInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFillter.Text = "";
            txtFillter.Focus();
        }

        public void btnFindPerson_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();

        }

        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                //Temp.Focus();
                errorProvider1.SetError(Temp, "This Field Is Required");
            }
            else
            {
                errorProvider1.SetError(Temp, "");
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.DataBack += Form_DataBack;
            frm.ShowDialog();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(ctrlPersonCard1.PersonID);
            frm.ShowDialog();
        }

        private void Form_DataBack(object sender, int PersonID)
        {

            // Handle the data received

            cbFillterPersonInfo.SelectedIndex = 1;
            txtFillter.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);

        }

        private void txtFillter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFindPerson.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFillterPersonInfo.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void CtrlPersonInfoWithFillter_Load(object sender, EventArgs e)
        {
            cbFillterPersonInfo.SelectedIndex = 0;
            txtFillter.Focus();
        }

        public void FilterFocus()
        {
            txtFillter.Focus();
        }
    }
}
