using Driving_Licenses_Managment_Presentation_Layer.Login;
using Driving_Licenses_Managment_Presentation_Layer.MainFormsPresentationLayer;
using Driving_Licenses_Managment_Presentation_Layer.UsersPresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_Licenses_Managment_Presentation_Layer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmUserCard());
            //Application.Run(new frmPeople());
            //Application.Run(new frmAddEditPersonInfo());
            //Application.Run(new frmManageUsers());
            //Application.Run(new frmMainForm());
            Application.Run(new frmLogin());
        }
    }
}
