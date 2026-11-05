using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using ZFApplication.UI;

namespace ZFApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm fr= new LoginForm();
            if(fr.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainView());
            }
            
        }
    }
}
