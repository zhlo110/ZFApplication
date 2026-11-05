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

namespace ZFApplication.UI
{
    public partial class NewCityForm : DevExpress.XtraEditors.XtraForm
    {
        public NewCityForm()
        {
            InitializeComponent();
        }
        int mParentID = -1;
        public NewCityForm(int parentID)
        {
            InitializeComponent();
            mParentID = parentID;
        }

        public void setParentID(int parentID)
        {
            mParentID = parentID;
        }

        public int getParentID()
        {
            return mParentID;
        } 

        public string getCityName()
        {
            return mCityNameEdit.Text;
        }


        public string getCityShortName()
        {
            string shortName = mCityShortNameEdit.Text;
            shortName = shortName.Trim();

            shortName = shortName.Replace("，", ",")
                                 .Replace("、", ",");

            return shortName;
        }

        public void setCityName(string name)
        {
            mCityNameEdit.Text = name;
        }

        public void setCityShortName(string name)
        {
            mCityShortNameEdit.Text = name;
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}