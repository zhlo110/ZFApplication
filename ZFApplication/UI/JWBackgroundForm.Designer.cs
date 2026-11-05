namespace ZFApplication.UI
{
    partial class JWBackgroundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JWBackgroundForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mStuType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.OKButton = new DevExpress.XtraEditors.SimpleButton();
            this.mProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.mTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.BrowerButton = new DevExpress.XtraEditors.SimpleButton();
            this.mRichEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            ((System.ComponentModel.ISupportInitialize)(this.mStuType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(50, 91);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "招生类别：";
            // 
            // mStuType
            // 
            this.mStuType.EditValue = "高中生";
            this.mStuType.Location = new System.Drawing.Point(120, 89);
            this.mStuType.Name = "mStuType";
            this.mStuType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mStuType.Properties.Items.AddRange(new object[] {
            "初中生",
            "高中生",
            "本科",
            "硕士",
            "博士"});
            this.mStuType.Size = new System.Drawing.Size(250, 20);
            this.mStuType.TabIndex = 2;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(178, 187);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "确    定";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // mProgressBar
            // 
            this.mProgressBar.Location = new System.Drawing.Point(12, 152);
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.Size = new System.Drawing.Size(403, 18);
            this.mProgressBar.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(49, 38);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "选择目录：";
            // 
            // mTextEdit
            // 
            this.mTextEdit.EditValue = "";
            this.mTextEdit.Location = new System.Drawing.Point(120, 36);
            this.mTextEdit.Name = "mTextEdit";
            this.mTextEdit.Size = new System.Drawing.Size(208, 20);
            this.mTextEdit.TabIndex = 7;
            // 
            // BrowerButton
            // 
            this.BrowerButton.Location = new System.Drawing.Point(334, 34);
            this.BrowerButton.Name = "BrowerButton";
            this.BrowerButton.Size = new System.Drawing.Size(36, 23);
            this.BrowerButton.TabIndex = 8;
            this.BrowerButton.Text = "...";
            this.BrowerButton.Click += new System.EventHandler(this.BrowerButton_Click);
            // 
            // mRichEditControl
            // 
            this.mRichEditControl.Location = new System.Drawing.Point(15, 286);
            this.mRichEditControl.Name = "mRichEditControl";
            this.mRichEditControl.Size = new System.Drawing.Size(400, 76);
            this.mRichEditControl.TabIndex = 9;
            this.mRichEditControl.Text = "richEditControl1";
            this.mRichEditControl.Visible = false;
            // 
            // JWBackgroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 241);
            this.Controls.Add(this.mRichEditControl);
            this.Controls.Add(this.BrowerButton);
            this.Controls.Add(this.mTextEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.mStuType);
            this.Controls.Add(this.labelControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("JWBackgroundForm.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JWBackgroundForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "军网背景审查";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.mStuType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mProgressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit mStuType;
        private DevExpress.XtraEditors.SimpleButton OKButton;
        private DevExpress.XtraEditors.ProgressBarControl mProgressBar;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit mTextEdit;
        private DevExpress.XtraEditors.SimpleButton BrowerButton;
        private DevExpress.XtraRichEdit.RichEditControl mRichEditControl;
    }
}