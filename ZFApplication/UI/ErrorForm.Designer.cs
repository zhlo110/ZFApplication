namespace ZFApplication.UI
{
    partial class ErrorForm
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
            this.mErrorText = new DevExpress.XtraRichEdit.RichEditControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.FinishButton = new DevExpress.XtraEditors.SimpleButton();
            this.IgnoreButton = new DevExpress.XtraEditors.SimpleButton();
            this.DeleteButton = new DevExpress.XtraEditors.SimpleButton();
            this.OpenButton = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // mErrorText
            // 
            this.mErrorText.Location = new System.Drawing.Point(12, 34);
            this.mErrorText.Name = "mErrorText";
            this.mErrorText.Size = new System.Drawing.Size(628, 293);
            this.mErrorText.TabIndex = 0;
            this.mErrorText.Views.SimpleView.WordWrap = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "详细信息：";
            // 
            // FinishButton
            // 
            this.FinishButton.Location = new System.Drawing.Point(49, 348);
            this.FinishButton.Name = "FinishButton";
            this.FinishButton.Size = new System.Drawing.Size(104, 23);
            this.FinishButton.TabIndex = 2;
            this.FinishButton.Text = "完成";
            this.FinishButton.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // IgnoreButton
            // 
            this.IgnoreButton.Location = new System.Drawing.Point(184, 348);
            this.IgnoreButton.Name = "IgnoreButton";
            this.IgnoreButton.Size = new System.Drawing.Size(118, 23);
            this.IgnoreButton.TabIndex = 3;
            this.IgnoreButton.Text = "忽略";
            this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(339, 348);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(118, 23);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "删除";
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(481, 348);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(118, 23);
            this.OpenButton.TabIndex = 5;
            this.OpenButton.Text = "打开";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 383);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.IgnoreButton);
            this.Controls.Add(this.FinishButton);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.mErrorText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误信息提示";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl mErrorText;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton FinishButton;
        private DevExpress.XtraEditors.SimpleButton IgnoreButton;
        private DevExpress.XtraEditors.SimpleButton DeleteButton;
        private DevExpress.XtraEditors.SimpleButton OpenButton;
    }
}