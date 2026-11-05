
namespace ZFApplication.UI
{
    partial class NewProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectForm));
            this.projecttext = new DevExpress.XtraEditors.TextEdit();
            this.foldertext = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.projectbutton = new DevExpress.XtraEditors.SimpleButton();
            this.folderbutton = new DevExpress.XtraEditors.SimpleButton();
            this.OKButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.projecttext.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foldertext.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // projecttext
            // 
            this.projecttext.EditValue = "";
            this.projecttext.Location = new System.Drawing.Point(112, 32);
            this.projecttext.Name = "projecttext";
            this.projecttext.Size = new System.Drawing.Size(185, 20);
            this.projecttext.TabIndex = 0;
            // 
            // foldertext
            // 
            this.foldertext.EditValue = "";
            this.foldertext.Location = new System.Drawing.Point(112, 73);
            this.foldertext.Name = "foldertext";
            this.foldertext.Size = new System.Drawing.Size(185, 20);
            this.foldertext.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "项目路径：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(47, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "文件夹:";
            // 
            // projectbutton
            // 
            this.projectbutton.Location = new System.Drawing.Point(309, 30);
            this.projectbutton.Name = "projectbutton";
            this.projectbutton.Size = new System.Drawing.Size(43, 23);
            this.projectbutton.TabIndex = 4;
            this.projectbutton.Text = "...";
            this.projectbutton.Click += new System.EventHandler(this.projectbutton_Click);
            // 
            // folderbutton
            // 
            this.folderbutton.Location = new System.Drawing.Point(309, 72);
            this.folderbutton.Name = "folderbutton";
            this.folderbutton.Size = new System.Drawing.Size(43, 23);
            this.folderbutton.TabIndex = 5;
            this.folderbutton.Text = "...";
            this.folderbutton.Click += new System.EventHandler(this.folderbutton_Click);
            // 
            // OKButton
            // 
            this.OKButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OKButton.ImageOptions.Image")));
            this.OKButton.Location = new System.Drawing.Point(156, 122);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(83, 26);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "确定";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 170);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.folderbutton);
            this.Controls.Add(this.projectbutton);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.foldertext);
            this.Controls.Add(this.projecttext);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("NewProjectForm.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建项目";
            ((System.ComponentModel.ISupportInitialize)(this.projecttext.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foldertext.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit projecttext;
        private DevExpress.XtraEditors.TextEdit foldertext;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton projectbutton;
        private DevExpress.XtraEditors.SimpleButton folderbutton;
        private DevExpress.XtraEditors.SimpleButton OKButton;
    }
}