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
using SendEmailApp.model;

namespace SendEmailApp.UI
{
    public partial class SelectPrintForm : DevExpress.XtraEditors.XtraForm
    {
        public SelectPrintForm()
        {
            InitializeComponent();
        }
        

        string mtemplate = "";
        public string getTemplate()
        {
            return  mtemplate;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            mtemplate = "./template/mail.doc";
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            mtemplate = "./template/mail1.doc";
            this.DialogResult = DialogResult.OK;
            
        }
    }
}