using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZFApplication.UI
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void mTimer_Tick(object sender, EventArgs e)
        {
            mTimer.Stop();
            this.DialogResult= DialogResult.OK;
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            //围绕窗体绘制边框
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (Pen pen = new Pen(Color.FromArgb(0, 0, 128), 1))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}