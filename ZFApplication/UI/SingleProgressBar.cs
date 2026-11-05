using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZFApplication.Listeners;

namespace ZFApplication.UI
{
    public partial class SingleProgressBar : DevExpress.XtraEditors.XtraForm, SingleBarInterface
    {
        public SingleProgressBar()
        {
            InitializeComponent();
        }

        public void progressend()
        {
            this.DialogResult = DialogResult.OK;
        }

        public void settotalcurrent(int current)
        {
            this.mProgressBar.EditValue = current;
            Application.DoEvents();
        }

        public void settotalnumber(int totalnumber)
        {
            mProgressBar.Properties.Maximum = totalnumber;
            this.mProgressBar.EditValue = 0;
            Application.DoEvents();
        }
        public delegate object ShowCallback(SingleBarInterface listener);
        public delegate void EndCallback(object table);
        //设置回调函数，并在SingleProgressBar_Shown调用
        ShowCallback mShowCallback = null;

        public void setShowCallback(ShowCallback call)
        {
            mShowCallback = call;
        }

        EndCallback mEndCallback= null;
        public void setEndCallback(EndCallback call)
        {
            mEndCallback = call;
        }

        public void showwithoutwindows()
        {
            object table = mShowCallback(this);
            if (table != null)
            {
                mEndCallback(table);
            }
        }

        private void SingleProgressBar_Shown(object sender, EventArgs e)
        {
            object table = mShowCallback(this);
            if (table != null)
            {
                mEndCallback(table);
            }
            
            //mCallback.inver
        }

        
    }
}