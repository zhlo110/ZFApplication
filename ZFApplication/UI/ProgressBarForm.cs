using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Fields;
using ZFApplication.Listeners;
using ZFApplication.Uitility;

namespace ZFApplication.UI
{
    public partial class ProgressBarForm : DevExpress.XtraEditors.XtraForm, ProgressBarListeners
    {
        private string mfolder = "";
        private string mmdbfile = "";
        public ProgressBarForm(string mdb, string folder)
        {
            InitializeComponent();
            mfolder = folder;
            mmdbfile = mdb;
        }

        

        private void ProgressBarForm_Load(object sender, EventArgs e)
        {
           
        }

        void ProgressBarListeners.progressend()
        {
            if(mmdbfile!=""&& mfolder != "")
            {
                SystemUtility.writecurrentfile(mmdbfile, "FilePath");
                SystemUtility.writecurrentfile(mfolder, "WorkplaceFolder");
            }
            
            this.DialogResult = DialogResult.OK;
        }

        public delegate void ShowCallback(string folder,string mdbfile,
            ProgressBarListeners listener);

        ShowCallback mShowCallback = null;
        public void setShowCallback(ShowCallback showCallback)
        {
            mShowCallback = showCallback;
        }

        private void ProgressBarForm_Shown(object sender, EventArgs e)
        {
            if (mShowCallback != null)
            {
                mShowCallback(mfolder, mmdbfile,this);
            }
            //ExcelReader.readerfolder(mfolder, mmdbfile, this);
        }

        public void settotalnumber(int totalnumber)
        {
            
            mProgressBar.Properties.Maximum = totalnumber;
            this.mProgressBar.EditValue = 0;
            Application.DoEvents();

        }

        public void setsubnumber(int totalnumber)
        {
            this.mSubProgressBar.Properties.Maximum = totalnumber;
            this.mSubProgressBar.EditValue = 0;
            Application.DoEvents();
        }

        public void settotalcurrent(int current, string files)
        {
            mLabel.Text = files;
            this.mProgressBar.EditValue = current;
            Application.DoEvents();
        }

        public void setsubcurrent(int current)
        {
            this.mSubProgressBar.EditValue = current;
            Application.DoEvents();
        }
    }
}