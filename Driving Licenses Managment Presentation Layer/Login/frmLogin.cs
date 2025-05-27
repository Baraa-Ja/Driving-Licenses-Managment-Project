using Driver_License_Business_Layer;
using Driving_Licenses_Managment_Presentation_Layer.Global;
using Driving_Licenses_Managment_Presentation_Layer.MainFormsPresentationLayer;
using System;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer.Login
{
    public partial class frmLogin : Form
    {

        private clsUsers _LoginUser;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentialUsingRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                ckbRemmeberMe.Checked = true;
            }
            else
                ckbRemmeberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsers user = clsUsers.FindUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {

                if (ckbRemmeberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry("", "");

                }

                //incase the user is not active
                if (!user.IsACtive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMainForm frm = new frmMainForm(this);
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void ckbRemmeberMe_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
