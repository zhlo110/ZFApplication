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
using DevExpress.XtraEditors;
using ZFApplication.Uitility;

namespace ZFApplication.UI
{
    public partial class NewProjectForm : DevExpress.XtraEditors.XtraForm
    {
        string mProjectName = "";
        string mFolderName = "";
        public NewProjectForm()
        {
            InitializeComponent();
            mProjectName = "";
            mFolderName = "";
        }

        public string getProjectName()
        {
            return mProjectName;
        }

        public string getFolderName()
        {
            return mFolderName;
        }
        private void OKButton_Click(object sender, EventArgs e)
        {

            string savepath = Path.GetDirectoryName(projecttext.Text);
            string folderpath = foldertext.Text;
            if(Directory.Exists(savepath)&&Directory.Exists(folderpath))
            {
                mProjectName = projecttext.Text;
                mFolderName = foldertext.Text;
                LogUtility.Log("项目路径或文件夹验证正确");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                LogUtility.Log("项目路径或文件夹输入错误");
                MessageBox.Show("项目路径或文件夹输入错误");
            }

            
        }

        private void projectbutton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "mdb文件|*.mdb";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                projecttext.Text = sfd.FileName;
            }
        }

        private void folderbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                foldertext.Text = fbd.SelectedPath;
            }
        }
    }
}