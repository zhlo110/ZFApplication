namespace ZFApplication.UI
{
    partial class SingleProgressBar
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(339, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "正在加载数据...";
            // 
            // mProgressBar
            // 
            this.mProgressBar.EditValue = 60;
            this.mProgressBar.Location = new System.Drawing.Point(27, 35);
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.mProgressBar.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mProgressBar.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mProgressBar.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.mProgressBar.Properties.Appearance.ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mProgressBar.Properties.ShowTitle = true;
            this.mProgressBar.Size = new System.Drawing.Size(733, 24);
            this.mProgressBar.TabIndex = 1;
            // 
            // SingleProgressBar
            // 
            this.Appearance.Options.UseImage = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 70);
            this.ControlBox = false;
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleProgressBar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SingleProgressBar";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.SingleProgressBar_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ProgressBarControl mProgressBar;
    }
}