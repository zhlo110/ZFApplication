
namespace ZFApplication.UserControl
{
    partial class StatisticBar
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
            this.mGridControl = new DevExpress.XtraGrid.GridControl();
            this.mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridVisit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.GenButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mGridControl
            // 
            this.mGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mGridControl.Location = new System.Drawing.Point(0, 0);
            this.mGridControl.MainView = this.mGridView;
            this.mGridControl.Name = "mGridControl";
            this.mGridControl.Size = new System.Drawing.Size(684, 384);
            this.mGridControl.TabIndex = 0;
            this.mGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mGridView});
            // 
            // mGridView
            // 
            this.mGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.GridName,
            this.GridNumber,
            this.GridVisit});
            this.mGridView.GridControl = this.mGridControl;
            this.mGridView.Name = "mGridView";
            this.mGridView.OptionsView.ShowGroupPanel = false;
            this.mGridView.RowHeight = 40;
            this.mGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.GridNumber, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // GridName
            // 
            this.GridName.AppearanceCell.Options.UseTextOptions = true;
            this.GridName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridName.AppearanceHeader.Options.UseTextOptions = true;
            this.GridName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridName.Caption = "名称";
            this.GridName.FieldName = "Name";
            this.GridName.Name = "GridName";
            this.GridName.OptionsFilter.AllowAutoFilter = false;
            this.GridName.OptionsFilter.AllowFilter = false;
            this.GridName.Visible = true;
            this.GridName.VisibleIndex = 0;
            // 
            // GridNumber
            // 
            this.GridNumber.AppearanceCell.Options.UseTextOptions = true;
            this.GridNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.GridNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNumber.Caption = "线索总数量";
            this.GridNumber.FieldName = "Number";
            this.GridNumber.Name = "GridNumber";
            this.GridNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.GridNumber.OptionsFilter.AllowAutoFilter = false;
            this.GridNumber.OptionsFilter.AllowFilter = false;
            this.GridNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.GridNumber.Visible = true;
            this.GridNumber.VisibleIndex = 1;
            // 
            // GridVisit
            // 
            this.GridVisit.AppearanceCell.Options.UseTextOptions = true;
            this.GridVisit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridVisit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridVisit.AppearanceHeader.Options.UseTextOptions = true;
            this.GridVisit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridVisit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridVisit.Caption = "走访的线索数量";
            this.GridVisit.FieldName = "VisitNumber";
            this.GridVisit.Name = "GridVisit";
            this.GridVisit.OptionsFilter.AllowAutoFilter = false;
            this.GridVisit.OptionsFilter.AllowFilter = false;
            this.GridVisit.Visible = true;
            this.GridVisit.VisibleIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.GenButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 384);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(684, 37);
            this.panelControl1.TabIndex = 1;
            // 
            // GenButton
            // 
            this.GenButton.Location = new System.Drawing.Point(290, 9);
            this.GenButton.Name = "GenButton";
            this.GenButton.Size = new System.Drawing.Size(75, 23);
            this.GenButton.TabIndex = 0;
            this.GenButton.Text = "生成表格";
            this.GenButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // StatisticBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mGridControl);
            this.Controls.Add(this.panelControl1);
            this.Name = "StatisticBar";
            this.Size = new System.Drawing.Size(684, 421);
            this.Load += new System.EventHandler(this.StatisticBar_Load);
            this.Enter += new System.EventHandler(this.StatisticBar_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl mGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mGridView;
        private DevExpress.XtraGrid.Columns.GridColumn GridName;
        private DevExpress.XtraGrid.Columns.GridColumn GridNumber;
        private DevExpress.XtraGrid.Columns.GridColumn GridVisit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton GenButton;
    }
}
