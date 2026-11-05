namespace ZFApplication.UI
{
    partial class PicReName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicReName));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dddfasd = new DevExpress.XtraEditors.LabelControl();
            this.mOriPath = new DevExpress.XtraEditors.TextEdit();
            this.mDestPath = new DevExpress.XtraEditors.TextEdit();
            this.OKButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mOriPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDestPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(57, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "原照片文件夹:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(46, 114);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "目标照片文件夹:";
            // 
            // dddfasd
            // 
            this.dddfasd.Location = new System.Drawing.Point(80, 25);
            this.dddfasd.Name = "dddfasd";
            this.dddfasd.Size = new System.Drawing.Size(252, 14);
            this.dddfasd.TabIndex = 2;
            this.dddfasd.Text = "该功能将以姓名命名的图片改成以身份证号命名";
            // 
            // mOriPath
            // 
            this.mOriPath.Location = new System.Drawing.Point(166, 66);
            this.mOriPath.Name = "mOriPath";
            this.mOriPath.Size = new System.Drawing.Size(166, 20);
            this.mOriPath.TabIndex = 3;
            // 
            // mDestPath
            // 
            this.mDestPath.Location = new System.Drawing.Point(166, 112);
            this.mDestPath.Name = "mDestPath";
            this.mDestPath.Size = new System.Drawing.Size(166, 20);
            this.mDestPath.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OKButton.ImageOptions.Image")));
            this.OKButton.Location = new System.Drawing.Point(147, 162);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(109, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "确定";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PicReName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 220);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.mDestPath);
            this.Controls.Add(this.mOriPath);
            this.Controls.Add(this.dddfasd);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("PicReName.IconOptions.LargeImage")));
            this.Name = "PicReName";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "照片改名";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.mOriPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDestPath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl dddfasd;
        private DevExpress.XtraEditors.TextEdit mOriPath;
        private DevExpress.XtraEditors.TextEdit mDestPath;
        private DevExpress.XtraEditors.SimpleButton OKButton;
    }
}