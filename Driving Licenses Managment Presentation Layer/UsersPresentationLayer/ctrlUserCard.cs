using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Properties;
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
    public partial class ctrlUserCard : UserControl
    {
        private clsUsers _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUsers.FindUser(UserID);

            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show("No User With UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK);
            }
            _UserID = UserID;

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            _UserID = _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsACtive.ToString();
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
        }

        private void _ResetUserInfo()
        {
            _UserID = -1;

            lblUserID.Text = "????";
            lblUserName.Text = "????";
            lblIsActive.Text = "????";

        }

    }
}
