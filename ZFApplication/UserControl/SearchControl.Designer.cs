namespace ZFApplication.UserControl
{
    partial class SearchControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchControl));
            this.mCombox = new DevExpress.XtraEditors.ComboBoxEdit();
            this.mFilterComboxLabel = new DevExpress.XtraEditors.LabelControl();
            this.SearchText = new DevExpress.XtraEditors.TextEdit();
            this.Search = new DevExpress.XtraEditors.SimpleButton();
            this.ClearFilter = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mCombox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mCombox
            // 
            this.mCombox.Location = new System.Drawing.Point(106, 14);
            this.mCombox.Name = "mCombox";
            this.mCombox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mCombox.Properties.Items.AddRange(new object[] {
            "未选择",
            "姓名",
            "职业"});
            this.mCombox.Size = new System.Drawing.Size(152, 20);
            this.mCombox.TabIndex = 0;
            this.mCombox.SelectedIndexChanged += new System.EventHandler(this.mCombox_SelectedIndexChanged);
            // 
            // mFilterComboxLabel
            // 
            this.mFilterComboxLabel.Location = new System.Drawing.Point(50, 17);
            this.mFilterComboxLabel.Name = "mFilterComboxLabel";
            this.mFilterComboxLabel.Size = new System.Drawing.Size(40, 14);
            this.mFilterComboxLabel.TabIndex = 1;
            this.mFilterComboxLabel.Text = "查询列:";
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(287, 14);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(152, 20);
            this.SearchText.TabIndex = 2;
            // 
            // Search
            // 
            this.Search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Search.ImageOptions.Image")));
            this.Search.Location = new System.Drawing.Point(455, 12);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(62, 25);
            this.Search.TabIndex = 3;
            this.Search.Text = "查询";
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // ClearFilter
            // 
            this.ClearFilter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ClearFilter.ImageOptions.Image")));
            this.ClearFilter.Location = new System.Drawing.Point(619, 12);
            this.ClearFilter.Name = "ClearFilter";
            this.ClearFilter.Size = new System.Drawing.Size(109, 25);
            this.ClearFilter.TabIndex = 4;
            this.ClearFilter.Text = "清除查询条件";
            this.ClearFilter.Visible = false;
            this.ClearFilter.Click += new System.EventHandler(this.ClearFilter_Click);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ClearFilter);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.mFilterComboxLabel);
            this.Controls.Add(this.mCombox);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(778, 46);
            ((System.ComponentModel.ISupportInitialize)(this.mCombox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit mCombox;
        private DevExpress.XtraEditors.LabelControl mFilterComboxLabel;
        private DevExpress.XtraEditors.TextEdit SearchText;
        private DevExpress.XtraEditors.SimpleButton Search;
        private DevExpress.XtraEditors.SimpleButton ClearFilter;
    }
}
