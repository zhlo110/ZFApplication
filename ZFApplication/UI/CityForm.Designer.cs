namespace ZFApplication.UI
{
    partial class CityForm
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
            this.components = new System.ComponentModel.Container();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.ThirdBoxControl = new DevExpress.XtraEditors.ListBoxControl();
            this.mMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SecondBoxControl = new DevExpress.XtraEditors.ListBoxControl();
            this.FirstBoxControl = new DevExpress.XtraEditors.ListBoxControl();
            this.addupdatelist = new System.Windows.Forms.ToolStripMenuItem();
            this.showupdatelist = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThirdBoxControl)).BeginInit();
            this.mMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecondBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstBoxControl)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.33F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.33F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.34F)});
            this.tablePanel1.Controls.Add(this.ThirdBoxControl);
            this.tablePanel1.Controls.Add(this.SecondBoxControl);
            this.tablePanel1.Controls.Add(this.FirstBoxControl);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Size = new System.Drawing.Size(701, 509);
            this.tablePanel1.TabIndex = 0;
            // 
            // ThirdBoxControl
            // 
            this.ThirdBoxControl.Appearance.Options.UseTextOptions = true;
            this.ThirdBoxControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ThirdBoxControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel1.SetColumn(this.ThirdBoxControl, 2);
            this.ThirdBoxControl.ContextMenuStrip = this.mMenuStrip;
            this.ThirdBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThirdBoxControl.Location = new System.Drawing.Point(470, 3);
            this.ThirdBoxControl.Name = "ThirdBoxControl";
            this.tablePanel1.SetRow(this.ThirdBoxControl, 0);
            this.ThirdBoxControl.Size = new System.Drawing.Size(228, 503);
            this.ThirdBoxControl.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.ThirdBoxControl.TabIndex = 2;
            // 
            // mMenuStrip
            // 
            this.mMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenuItem,
            this.UpdateMenuItem,
            this.DeleteMenuItem,
            this.addupdatelist,
            this.showupdatelist});
            this.mMenuStrip.Name = "mMenuStrip";
            this.mMenuStrip.Size = new System.Drawing.Size(181, 136);
            this.mMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.mMenuStrip_Opening);
            // 
            // NewMenuItem
            // 
            this.NewMenuItem.Name = "NewMenuItem";
            this.NewMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NewMenuItem.Text = "新建";
            this.NewMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // UpdateMenuItem
            // 
            this.UpdateMenuItem.Name = "UpdateMenuItem";
            this.UpdateMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UpdateMenuItem.Text = "修改";
            this.UpdateMenuItem.Click += new System.EventHandler(this.UpdateMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DeleteMenuItem.Text = "刪除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // SecondBoxControl
            // 
            this.SecondBoxControl.Appearance.Options.UseTextOptions = true;
            this.SecondBoxControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SecondBoxControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel1.SetColumn(this.SecondBoxControl, 1);
            this.SecondBoxControl.ContextMenuStrip = this.mMenuStrip;
            this.SecondBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecondBoxControl.Location = new System.Drawing.Point(237, 3);
            this.SecondBoxControl.Name = "SecondBoxControl";
            this.tablePanel1.SetRow(this.SecondBoxControl, 0);
            this.SecondBoxControl.Size = new System.Drawing.Size(228, 503);
            this.SecondBoxControl.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.SecondBoxControl.TabIndex = 1;
            this.SecondBoxControl.SelectedIndexChanged += new System.EventHandler(this.SecondBoxControl_SelectedIndexChanged);
            // 
            // FirstBoxControl
            // 
            this.FirstBoxControl.Appearance.Options.UseTextOptions = true;
            this.FirstBoxControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FirstBoxControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel1.SetColumn(this.FirstBoxControl, 0);
            this.FirstBoxControl.ContextMenuStrip = this.mMenuStrip;
            this.FirstBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirstBoxControl.Items.AddRange(new object[] {
            "{11}"});
            this.FirstBoxControl.Location = new System.Drawing.Point(3, 3);
            this.FirstBoxControl.Name = "FirstBoxControl";
            this.tablePanel1.SetRow(this.FirstBoxControl, 0);
            this.FirstBoxControl.Size = new System.Drawing.Size(228, 503);
            this.FirstBoxControl.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.FirstBoxControl.TabIndex = 0;
            this.FirstBoxControl.SelectedIndexChanged += new System.EventHandler(this.FirstBoxControl_SelectedIndexChanged);
            // 
            // addupdatelist
            // 
            this.addupdatelist.Name = "addupdatelist";
            this.addupdatelist.Size = new System.Drawing.Size(180, 22);
            this.addupdatelist.Text = "添加到更新列表";
            this.addupdatelist.Click += new System.EventHandler(this.addupdatelist_Click);
            // 
            // showupdatelist
            // 
            this.showupdatelist.Name = "showupdatelist";
            this.showupdatelist.Size = new System.Drawing.Size(180, 22);
            this.showupdatelist.Text = "显示更新列表";
            this.showupdatelist.Click += new System.EventHandler(this.showupdatelist_Click);
            // 
            // CityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 509);
            this.Controls.Add(this.tablePanel1);
            this.Name = "CityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地名管理(City.mdb)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CityForm_FormClosed);
            this.Load += new System.EventHandler(this.CityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ThirdBoxControl)).EndInit();
            this.mMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecondBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstBoxControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.ListBoxControl FirstBoxControl;
        private DevExpress.XtraEditors.ListBoxControl ThirdBoxControl;
        private DevExpress.XtraEditors.ListBoxControl SecondBoxControl;
        private System.Windows.Forms.ContextMenuStrip mMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem NewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addupdatelist;
        private System.Windows.Forms.ToolStripMenuItem showupdatelist;
    }
}