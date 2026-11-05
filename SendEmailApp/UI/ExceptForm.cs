using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Behaviors;
using DevExpress.XtraEditors;
using SendEmailApp.Utility;

namespace SendEmailApp.UI
{
    public partial class ExceptForm : DevExpress.XtraEditors.XtraForm
    {
        public ExceptForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            string value = SystemUtility.ReadValueFromFile("workspace.txt", "Areas");
            if (value == null)
            {
                value = "";
            }
            mTextEdit.Text = value;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            PathUtility.setcurrentpath();
            string text = mTextEdit.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                SystemUtility.UpdateFolderPathInFile("workspace.txt", "Areas", text);
                SystemUtility.areas = text.Split(new char[] { ',','，'});
                // SystemUtility.UpdateFolderPathInFile("workspace.txt", "Areas", text);
                //  SystemUtility.workspace = folderPath;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}