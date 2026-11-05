
namespace ZFApplication.UI
{
    partial class ProgressBarForm
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
            this.mProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.mLabel = new DevExpress.XtraEditors.LabelControl();
            this.mSubProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mProgressBar
            // 
            this.mProgressBar.EditValue = 10;
            this.mProgressBar.Location = new System.Drawing.Point(26, 32);
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.Properties.Maximum = 1000;
            this.mProgressBar.Properties.ShowTitle = true;
            this.mProgressBar.Size = new System.Drawing.Size(582, 22);
            this.mProgressBar.TabIndex = 0;
            // 
            // mLabel
            // 
            this.mLabel.Location = new System.Drawing.Point(26, 8);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(0, 14);
            this.mLabel.TabIndex = 1;
            // 
            // mSubProgressBar
            // 
            this.mSubProgressBar.EditValue = 10;
            this.mSubProgressBar.Location = new System.Drawing.Point(26, 66);
            this.mSubProgressBar.Name = "mSubProgressBar";
            this.mSubProgressBar.Properties.Maximum = 1000;
            this.mSubProgressBar.Properties.ShowTitle = true;
            this.mSubProgressBar.Size = new System.Drawing.Size(582, 22);
            this.mSubProgressBar.TabIndex = 2;
            // 
            // ProgressBarForm
            // 
            this.Appearance.Options.UseImage = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 100);
            this.ControlBox = false;
            this.Controls.Add(this.mSubProgressBar);
            this.Controls.Add(this.mLabel);
            this.Controls.Add(this.mProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProgressBarForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProgressBarForm_Load);
            this.Shown += new System.EventHandler(this.ProgressBarForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl mProgressBar;
        private DevExpress.XtraEditors.LabelControl mLabel;
        private DevExpress.XtraEditors.ProgressBarControl mSubProgressBar;
    }
}