namespace ZFApplication.UI
{
    partial class NewCityForm
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.mCityNameEdit = new DevExpress.XtraEditors.TextEdit();
            this.mCityShortNameEdit = new DevExpress.XtraEditors.TextEdit();
            this.OKButton = new DevExpress.XtraEditors.SimpleButton();
            this.CancelButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mCityNameEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCityShortNameEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(45, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "城市名称:";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(45, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "别      名:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(36, 108);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(295, 19);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "注意:如果有多个别名，别名间请加逗号";
            // 
            // mCityNameEdit
            // 
            this.mCityNameEdit.Location = new System.Drawing.Point(123, 26);
            this.mCityNameEdit.Name = "mCityNameEdit";
            this.mCityNameEdit.Size = new System.Drawing.Size(197, 20);
            this.mCityNameEdit.TabIndex = 3;
            // 
            // mCityShortNameEdit
            // 
            this.mCityShortNameEdit.Location = new System.Drawing.Point(123, 68);
            this.mCityShortNameEdit.Name = "mCityShortNameEdit";
            this.mCityShortNameEdit.Size = new System.Drawing.Size(197, 20);
            this.mCityShortNameEdit.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(85, 172);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "确  定";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.Location = new System.Drawing.Point(228, 172);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 6;
            this.CancelButton1.Text = "取  消";
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.mCityNameEdit);
            this.panelControl1.Controls.Add(this.mCityShortNameEdit);
            this.panelControl1.Location = new System.Drawing.Point(21, 10);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(352, 149);
            this.panelControl1.TabIndex = 7;
            // 
            // NewCityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 215);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.OKButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewCityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建";
            ((System.ComponentModel.ISupportInitialize)(this.mCityNameEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCityShortNameEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit mCityNameEdit;
        private DevExpress.XtraEditors.TextEdit mCityShortNameEdit;
        private DevExpress.XtraEditors.SimpleButton OKButton;
        private DevExpress.XtraEditors.SimpleButton CancelButton1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}