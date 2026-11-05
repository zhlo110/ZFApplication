namespace SendEmailApp.UI
{
    partial class SelectFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFilesForm));
            this.mListBox = new DevExpress.XtraEditors.ListBoxControl();
            this.mSearchText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mListBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSearchText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mListBox
            // 
            this.mListBox.Location = new System.Drawing.Point(12, 42);
            this.mListBox.Name = "mListBox";
            this.mListBox.Size = new System.Drawing.Size(323, 554);
            this.mListBox.TabIndex = 0;
            this.mListBox.DoubleClick += new System.EventHandler(this.mListBox_DoubleClick);
            // 
            // mSearchText
            // 
            this.mSearchText.Location = new System.Drawing.Point(65, 12);
            this.mSearchText.Name = "mSearchText";
            this.mSearchText.Size = new System.Drawing.Size(270, 20);
            this.mSearchText.TabIndex = 1;
            this.mSearchText.EditValueChanged += new System.EventHandler(this.onTextChange);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "搜索:";
            // 
            // SelectFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 608);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.mSearchText);
            this.Controls.Add(this.mListBox);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("SelectFilesForm.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectFilesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择文件";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mListBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSearchText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl mListBox;
        private DevExpress.XtraEditors.TextEdit mSearchText;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}