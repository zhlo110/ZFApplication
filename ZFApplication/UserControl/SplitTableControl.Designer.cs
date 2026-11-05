
namespace ZFApplication.UserControl
{
    partial class SplitTableControl
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitTableControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.mDockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mTresslistControl = new ZFApplication.UserControl.TresslistControl();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.sidePanel3 = new DevExpress.XtraEditors.SidePanel();
            this.mGridControl = new DevExpress.XtraGrid.GridControl();
            this.mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridObjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.GridTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridNation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridPolitical = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridMarrage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridPublic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridIDCard = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridPhonenumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridAddresstyoe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridVisit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.GridAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridPoliceAddr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mDeleteItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.GridOpen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mOpenItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.mSearchControl = new ZFApplication.UserControl.SearchControl();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.mVirtualSource = new DevExpress.Data.VirtualServerModeSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mDockManager)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.sidePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mItemCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeleteItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenItemButtonEdit)).BeginInit();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mVirtualSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(45, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "线索表";
            // 
            // mDockManager
            // 
            this.mDockManager.Form = this;
            this.mDockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel2});
            this.mDockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("680c4632-fa2d-4f3c-85e3-e4765dc8e889");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(284, 200);
            this.dockPanel1.Size = new System.Drawing.Size(284, 629);
            this.dockPanel1.Text = "省市区";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.mTresslistControl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(277, 600);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // mTresslistControl
            // 
            this.mTresslistControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTresslistControl.Location = new System.Drawing.Point(0, 0);
            this.mTresslistControl.Name = "mTresslistControl";
            this.mTresslistControl.Size = new System.Drawing.Size(277, 600);
            this.mTresslistControl.TabIndex = 0;
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("395d3a12-5e2e-441f-a26d-cbe0deaeba1b");
            this.dockPanel2.Location = new System.Drawing.Point(284, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Options.ShowCloseButton = false;
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(828, 629);
            this.dockPanel2.Text = "线索表";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.sidePanel3);
            this.dockPanel2_Container.Controls.Add(this.sidePanel2);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(822, 599);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // sidePanel3
            // 
            this.sidePanel3.Controls.Add(this.mGridControl);
            this.sidePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel3.Location = new System.Drawing.Point(0, 47);
            this.sidePanel3.Name = "sidePanel3";
            this.sidePanel3.Size = new System.Drawing.Size(822, 552);
            this.sidePanel3.TabIndex = 1;
            this.sidePanel3.Text = "sidePanel3";
            // 
            // mGridControl
            // 
            this.mGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mGridControl.Location = new System.Drawing.Point(0, 0);
            this.mGridControl.MainView = this.mGridView;
            this.mGridControl.Name = "mGridControl";
            this.mGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1,
            this.mItemCheckEdit,
            this.mDeleteItemButtonEdit,
            this.mOpenItemButtonEdit});
            this.mGridControl.Size = new System.Drawing.Size(822, 552);
            this.mGridControl.TabIndex = 0;
            this.mGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mGridView});
            // 
            // mGridView
            // 
            this.mGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.GridNo,
            this.GridObjectName,
            this.GridTitle,
            this.GridName,
            this.GridNation,
            this.GridPolitical,
            this.GridMarrage,
            this.GridPublic,
            this.GridIDCard,
            this.GridPhonenumber,
            this.GridAddresstyoe,
            this.GridVisit,
            this.GridAddress,
            this.GridPoliceAddr,
            this.GridRemark,
            this.GridDelete,
            this.GridOpen});
            this.mGridView.GridControl = this.mGridControl;
            this.mGridView.Name = "mGridView";
            this.mGridView.OptionsView.AllowCellMerge = true;
            this.mGridView.OptionsView.ShowGroupPanel = false;
            this.mGridView.RowHeight = 60;
            this.mGridView.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.mGridView_CellMerge);
            // 
            // GridNo
            // 
            this.GridNo.AppearanceCell.Options.UseTextOptions = true;
            this.GridNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNo.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNo.AppearanceHeader.Options.UseTextOptions = true;
            this.GridNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNo.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNo.Caption = "编号";
            this.GridNo.FieldName = "No";
            this.GridNo.Name = "GridNo";
            this.GridNo.Visible = true;
            this.GridNo.VisibleIndex = 0;
            this.GridNo.Width = 20;
            // 
            // GridObjectName
            // 
            this.GridObjectName.AppearanceCell.Options.UseTextOptions = true;
            this.GridObjectName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridObjectName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridObjectName.AppearanceHeader.Options.UseTextOptions = true;
            this.GridObjectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridObjectName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridObjectName.Caption = "招飞对象";
            this.GridObjectName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.GridObjectName.FieldName = "ObjName";
            this.GridObjectName.Name = "GridObjectName";
            this.GridObjectName.Visible = true;
            this.GridObjectName.VisibleIndex = 1;
            this.GridObjectName.Width = 20;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // GridTitle
            // 
            this.GridTitle.AppearanceCell.Options.UseTextOptions = true;
            this.GridTitle.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.GridTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridTitle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridTitle.Caption = "称谓";
            this.GridTitle.FieldName = "Title";
            this.GridTitle.Name = "GridTitle";
            this.GridTitle.Visible = true;
            this.GridTitle.VisibleIndex = 2;
            this.GridTitle.Width = 20;
            // 
            // GridName
            // 
            this.GridName.AppearanceCell.Options.UseTextOptions = true;
            this.GridName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridName.AppearanceHeader.Options.UseTextOptions = true;
            this.GridName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridName.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridName.Caption = "姓名";
            this.GridName.ColumnEdit = this.repositoryItemMemoEdit1;
            this.GridName.FieldName = "Name";
            this.GridName.Name = "GridName";
            this.GridName.Visible = true;
            this.GridName.VisibleIndex = 3;
            this.GridName.Width = 20;
            // 
            // GridNation
            // 
            this.GridNation.AppearanceCell.Options.UseTextOptions = true;
            this.GridNation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNation.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNation.AppearanceHeader.Options.UseTextOptions = true;
            this.GridNation.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridNation.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridNation.Caption = "民族";
            this.GridNation.FieldName = "Nation";
            this.GridNation.Name = "GridNation";
            this.GridNation.Visible = true;
            this.GridNation.VisibleIndex = 4;
            this.GridNation.Width = 20;
            // 
            // GridPolitical
            // 
            this.GridPolitical.AppearanceCell.Options.UseTextOptions = true;
            this.GridPolitical.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPolitical.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPolitical.AppearanceHeader.Options.UseTextOptions = true;
            this.GridPolitical.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPolitical.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPolitical.Caption = "政治面貌";
            this.GridPolitical.FieldName = "Political";
            this.GridPolitical.Name = "GridPolitical";
            this.GridPolitical.Visible = true;
            this.GridPolitical.VisibleIndex = 5;
            this.GridPolitical.Width = 20;
            // 
            // GridMarrage
            // 
            this.GridMarrage.AppearanceCell.Options.UseTextOptions = true;
            this.GridMarrage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridMarrage.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridMarrage.AppearanceHeader.Options.UseTextOptions = true;
            this.GridMarrage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridMarrage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridMarrage.Caption = "婚姻状况";
            this.GridMarrage.FieldName = "Marriage";
            this.GridMarrage.Name = "GridMarrage";
            this.GridMarrage.Visible = true;
            this.GridMarrage.VisibleIndex = 6;
            this.GridMarrage.Width = 20;
            // 
            // GridPublic
            // 
            this.GridPublic.AppearanceCell.Options.UseTextOptions = true;
            this.GridPublic.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPublic.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPublic.AppearanceHeader.Options.UseTextOptions = true;
            this.GridPublic.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPublic.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPublic.Caption = "是否有公职";
            this.GridPublic.FieldName = "IsPublic";
            this.GridPublic.Name = "GridPublic";
            this.GridPublic.Visible = true;
            this.GridPublic.VisibleIndex = 7;
            this.GridPublic.Width = 23;
            // 
            // GridIDCard
            // 
            this.GridIDCard.AppearanceCell.Options.UseTextOptions = true;
            this.GridIDCard.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridIDCard.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridIDCard.AppearanceHeader.Options.UseTextOptions = true;
            this.GridIDCard.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridIDCard.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridIDCard.Caption = "身份证号";
            this.GridIDCard.FieldName = "IDCard";
            this.GridIDCard.Name = "GridIDCard";
            this.GridIDCard.Visible = true;
            this.GridIDCard.VisibleIndex = 8;
            this.GridIDCard.Width = 59;
            // 
            // GridPhonenumber
            // 
            this.GridPhonenumber.AppearanceCell.Options.UseTextOptions = true;
            this.GridPhonenumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPhonenumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPhonenumber.AppearanceHeader.Options.UseTextOptions = true;
            this.GridPhonenumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPhonenumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPhonenumber.Caption = "电话号码";
            this.GridPhonenumber.FieldName = "Phonenumber";
            this.GridPhonenumber.Name = "GridPhonenumber";
            this.GridPhonenumber.Visible = true;
            this.GridPhonenumber.VisibleIndex = 9;
            this.GridPhonenumber.Width = 28;
            // 
            // GridAddresstyoe
            // 
            this.GridAddresstyoe.AppearanceCell.Options.UseTextOptions = true;
            this.GridAddresstyoe.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridAddresstyoe.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridAddresstyoe.AppearanceHeader.Options.UseTextOptions = true;
            this.GridAddresstyoe.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridAddresstyoe.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridAddresstyoe.Caption = "信息类别";
            this.GridAddresstyoe.FieldName = "Addresstype";
            this.GridAddresstyoe.Name = "GridAddresstyoe";
            this.GridAddresstyoe.Visible = true;
            this.GridAddresstyoe.VisibleIndex = 11;
            this.GridAddresstyoe.Width = 33;
            // 
            // GridVisit
            // 
            this.GridVisit.AppearanceCell.Options.UseTextOptions = true;
            this.GridVisit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridVisit.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridVisit.AppearanceHeader.Options.UseTextOptions = true;
            this.GridVisit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridVisit.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridVisit.Caption = "已经走访";
            this.GridVisit.ColumnEdit = this.mItemCheckEdit;
            this.GridVisit.FieldName = "IsVisit";
            this.GridVisit.Name = "GridVisit";
            this.GridVisit.Visible = true;
            this.GridVisit.VisibleIndex = 10;
            this.GridVisit.Width = 21;
            // 
            // mItemCheckEdit
            // 
            this.mItemCheckEdit.AutoHeight = false;
            this.mItemCheckEdit.Name = "mItemCheckEdit";
            // 
            // GridAddress
            // 
            this.GridAddress.AppearanceCell.Options.UseTextOptions = true;
            this.GridAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.GridAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.GridAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridAddress.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridAddress.Caption = "详细信息（地址、工作单位及职务）";
            this.GridAddress.ColumnEdit = this.repositoryItemMemoEdit1;
            this.GridAddress.FieldName = "Address";
            this.GridAddress.Name = "GridAddress";
            this.GridAddress.Visible = true;
            this.GridAddress.VisibleIndex = 12;
            this.GridAddress.Width = 101;
            // 
            // GridPoliceAddr
            // 
            this.GridPoliceAddr.AppearanceCell.Options.UseTextOptions = true;
            this.GridPoliceAddr.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPoliceAddr.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPoliceAddr.AppearanceHeader.Options.UseTextOptions = true;
            this.GridPoliceAddr.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridPoliceAddr.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridPoliceAddr.Caption = "所在地派出所";
            this.GridPoliceAddr.FieldName = "PoliceAddr";
            this.GridPoliceAddr.Name = "GridPoliceAddr";
            this.GridPoliceAddr.Visible = true;
            this.GridPoliceAddr.VisibleIndex = 13;
            this.GridPoliceAddr.Width = 71;
            // 
            // GridRemark
            // 
            this.GridRemark.AppearanceCell.Options.UseTextOptions = true;
            this.GridRemark.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.GridRemark.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.GridRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridRemark.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridRemark.Caption = "备注";
            this.GridRemark.ColumnEdit = this.repositoryItemMemoEdit1;
            this.GridRemark.FieldName = "Remark";
            this.GridRemark.Name = "GridRemark";
            this.GridRemark.Visible = true;
            this.GridRemark.VisibleIndex = 14;
            this.GridRemark.Width = 52;
            // 
            // GridDelete
            // 
            this.GridDelete.AppearanceCell.Options.UseTextOptions = true;
            this.GridDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridDelete.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.GridDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridDelete.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridDelete.Caption = "删除";
            this.GridDelete.ColumnEdit = this.mDeleteItemButtonEdit;
            this.GridDelete.FieldName = "Isdelete";
            this.GridDelete.Name = "GridDelete";
            this.GridDelete.OptionsFilter.AllowFilter = false;
            this.GridDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.GridDelete.Visible = true;
            this.GridDelete.VisibleIndex = 15;
            this.GridDelete.Width = 23;
            // 
            // mDeleteItemButtonEdit
            // 
            this.mDeleteItemButtonEdit.AutoHeight = false;
            this.mDeleteItemButtonEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.mDeleteItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.mDeleteItemButtonEdit.Name = "mDeleteItemButtonEdit";
            this.mDeleteItemButtonEdit.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.mDeleteItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // GridOpen
            // 
            this.GridOpen.AppearanceCell.Options.UseTextOptions = true;
            this.GridOpen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridOpen.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridOpen.AppearanceHeader.Options.UseTextOptions = true;
            this.GridOpen.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridOpen.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridOpen.Caption = "打开";
            this.GridOpen.ColumnEdit = this.mOpenItemButtonEdit;
            this.GridOpen.FieldName = "IsOpen";
            this.GridOpen.Name = "GridOpen";
            this.GridOpen.Visible = true;
            this.GridOpen.VisibleIndex = 16;
            this.GridOpen.Width = 24;
            // 
            // mOpenItemButtonEdit
            // 
            this.mOpenItemButtonEdit.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.mOpenItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.mOpenItemButtonEdit.Name = "mOpenItemButtonEdit";
            this.mOpenItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.mSearchControl);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel2.Location = new System.Drawing.Point(0, 0);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(822, 47);
            this.sidePanel2.TabIndex = 0;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // mSearchControl
            // 
            this.mSearchControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mSearchControl.Location = new System.Drawing.Point(0, 0);
            this.mSearchControl.Name = "mSearchControl";
            this.mSearchControl.Size = new System.Drawing.Size(822, 46);
            this.mSearchControl.TabIndex = 0;
            // 
            // sidePanel1
            // 
            this.sidePanel1.Location = new System.Drawing.Point(292, 8);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(75, 23);
            this.sidePanel1.TabIndex = 3;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // mVirtualSource
            // 
            this.mVirtualSource.RowType = typeof(ZFApplication.Model.PersonInfo);
            this.mVirtualSource.ConfigurationChanged += new System.EventHandler<DevExpress.Data.VirtualServerModeRowsEventArgs>(this.mVirtualSource_ConfigurationChanged);
            this.mVirtualSource.MoreRows += new System.EventHandler<DevExpress.Data.VirtualServerModeRowsEventArgs>(this.mVirtualSource_MoreRows);
            // 
            // SplitTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.dockPanel1);
            this.Name = "SplitTableControl";
            this.Size = new System.Drawing.Size(1112, 629);
            this.Enter += new System.EventHandler(this.SplitTableControl_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.mDockManager)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.sidePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mItemCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeleteItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenItemButtonEdit)).EndInit();
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mVirtualSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.Docking.DockManager mDockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private TresslistControl mTresslistControl;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel sidePanel3;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private SearchControl mSearchControl;
        private DevExpress.XtraGrid.GridControl mGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mGridView;
        private DevExpress.XtraGrid.Columns.GridColumn GridNo;
        private DevExpress.XtraGrid.Columns.GridColumn GridObjectName;
        private DevExpress.XtraGrid.Columns.GridColumn GridTitle;
        private DevExpress.XtraGrid.Columns.GridColumn GridName;
        private DevExpress.XtraGrid.Columns.GridColumn GridNation;
        private DevExpress.XtraGrid.Columns.GridColumn GridPolitical;
        private DevExpress.XtraGrid.Columns.GridColumn GridMarrage;
        private DevExpress.XtraGrid.Columns.GridColumn GridIDCard;
        private DevExpress.XtraGrid.Columns.GridColumn GridPhonenumber;
        private DevExpress.XtraGrid.Columns.GridColumn GridAddresstyoe;
        private DevExpress.XtraGrid.Columns.GridColumn GridAddress;
        private DevExpress.XtraGrid.Columns.GridColumn GridRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn GridPublic;
        private DevExpress.XtraGrid.Columns.GridColumn GridVisit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit mItemCheckEdit;
        private DevExpress.XtraGrid.Columns.GridColumn GridPoliceAddr;
        private DevExpress.XtraGrid.Columns.GridColumn GridDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit mDeleteItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn GridOpen;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit mOpenItemButtonEdit;
        private DevExpress.Data.VirtualServerModeSource mVirtualSource;
    }
}
