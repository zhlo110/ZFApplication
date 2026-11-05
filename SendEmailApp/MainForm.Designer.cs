using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

namespace SendEmailApp
{
    partial class MainForm
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.mDockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.workspace = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mGridControl = new DevExpress.XtraGrid.GridControl();
            this.mGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NoColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ObjNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TitleColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PoliticalColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MarigeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IDCardColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhoneColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UnitColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.AddrColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.RemarkColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.SendColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.OpenColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mOpenButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.SetWorkSpace = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.SelectFiles = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.OpenCurrentFile = new DevExpress.XtraBars.BarButtonItem();
            this.currentfile = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMemoEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.mFileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.mDockManager)).BeginInit();
            this.workspace.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // mDockManager
            // 
            this.mDockManager.Form = this;
            this.mDockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.workspace});
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
            // workspace
            // 
            this.workspace.Controls.Add(this.dockPanel2_Container);
            this.workspace.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.workspace.ID = new System.Guid("0533bd4d-8311-47d8-8d80-ff14b1f4370b");
            this.workspace.Location = new System.Drawing.Point(0, 160);
            this.workspace.Name = "workspace";
            this.workspace.OriginalSize = new System.Drawing.Size(618, 200);
            this.workspace.Size = new System.Drawing.Size(1113, 445);
            this.workspace.Text = "工作区";
            this.workspace.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.workspace_ClosingPanel);
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.mGridControl);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(1107, 415);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // mGridControl
            // 
            this.mGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mGridControl.Location = new System.Drawing.Point(0, 0);
            this.mGridControl.MainView = this.mGridView;
            this.mGridControl.MenuManager = this.ribbonControl1;
            this.mGridControl.Name = "mGridControl";
            this.mGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.mButtonEdit,
            this.mOpenButtonEdit,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3});
            this.mGridControl.Size = new System.Drawing.Size(1107, 415);
            this.mGridControl.TabIndex = 0;
            this.mGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mGridView});
            // 
            // mGridView
            // 
            this.mGridView.Appearance.Row.Options.UseTextOptions = true;
            this.mGridView.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.mGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NoColumn,
            this.ObjNameColumn,
            this.TitleColumn,
            this.NameColumn,
            this.PoliticalColumn,
            this.MarigeColumn,
            this.IDCardColumn,
            this.PhoneColumn,
            this.UnitColumn,
            this.AddrColumn,
            this.RemarkColumn,
            this.SendColumn,
            this.OpenColumn});
            this.mGridView.GridControl = this.mGridControl;
            this.mGridView.Name = "mGridView";
            this.mGridView.OptionsView.ShowGroupPanel = false;
            this.mGridView.RowHeight = 70;
            // 
            // NoColumn
            // 
            this.NoColumn.AppearanceCell.Options.UseTextOptions = true;
            this.NoColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NoColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NoColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.NoColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NoColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NoColumn.Caption = "编号";
            this.NoColumn.FieldName = "No";
            this.NoColumn.Name = "NoColumn";
            this.NoColumn.Visible = true;
            this.NoColumn.VisibleIndex = 0;
            this.NoColumn.Width = 50;
            // 
            // ObjNameColumn
            // 
            this.ObjNameColumn.AppearanceCell.Options.UseTextOptions = true;
            this.ObjNameColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ObjNameColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ObjNameColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.ObjNameColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ObjNameColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ObjNameColumn.Caption = "招飞对象";
            this.ObjNameColumn.FieldName = "Objname";
            this.ObjNameColumn.Name = "ObjNameColumn";
            this.ObjNameColumn.Visible = true;
            this.ObjNameColumn.VisibleIndex = 1;
            this.ObjNameColumn.Width = 50;
            // 
            // TitleColumn
            // 
            this.TitleColumn.AppearanceCell.Options.UseTextOptions = true;
            this.TitleColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TitleColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TitleColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.TitleColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TitleColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TitleColumn.Caption = "称谓";
            this.TitleColumn.FieldName = "Title";
            this.TitleColumn.Name = "TitleColumn";
            this.TitleColumn.Visible = true;
            this.TitleColumn.VisibleIndex = 2;
            this.TitleColumn.Width = 50;
            // 
            // NameColumn
            // 
            this.NameColumn.AppearanceCell.Options.UseTextOptions = true;
            this.NameColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NameColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NameColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.NameColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NameColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.NameColumn.Caption = "姓名";
            this.NameColumn.FieldName = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.Visible = true;
            this.NameColumn.VisibleIndex = 3;
            this.NameColumn.Width = 50;
            // 
            // PoliticalColumn
            // 
            this.PoliticalColumn.AppearanceCell.Options.UseTextOptions = true;
            this.PoliticalColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PoliticalColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PoliticalColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.PoliticalColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PoliticalColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PoliticalColumn.Caption = "政治面貌";
            this.PoliticalColumn.FieldName = "Political";
            this.PoliticalColumn.Name = "PoliticalColumn";
            this.PoliticalColumn.Visible = true;
            this.PoliticalColumn.VisibleIndex = 4;
            this.PoliticalColumn.Width = 50;
            // 
            // MarigeColumn
            // 
            this.MarigeColumn.AppearanceCell.Options.UseTextOptions = true;
            this.MarigeColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MarigeColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MarigeColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.MarigeColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MarigeColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MarigeColumn.Caption = "婚姻状况";
            this.MarigeColumn.FieldName = "Marriage";
            this.MarigeColumn.Name = "MarigeColumn";
            this.MarigeColumn.Visible = true;
            this.MarigeColumn.VisibleIndex = 5;
            this.MarigeColumn.Width = 50;
            // 
            // IDCardColumn
            // 
            this.IDCardColumn.AppearanceCell.Options.UseTextOptions = true;
            this.IDCardColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDCardColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.IDCardColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.IDCardColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IDCardColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.IDCardColumn.Caption = "身份证号";
            this.IDCardColumn.FieldName = "Idcard";
            this.IDCardColumn.Name = "IDCardColumn";
            this.IDCardColumn.Visible = true;
            this.IDCardColumn.VisibleIndex = 6;
            this.IDCardColumn.Width = 101;
            // 
            // PhoneColumn
            // 
            this.PhoneColumn.AppearanceCell.Options.UseTextOptions = true;
            this.PhoneColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PhoneColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PhoneColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.PhoneColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.PhoneColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.PhoneColumn.Caption = "电话号码";
            this.PhoneColumn.FieldName = "Phone";
            this.PhoneColumn.Name = "PhoneColumn";
            this.PhoneColumn.Visible = true;
            this.PhoneColumn.VisibleIndex = 7;
            this.PhoneColumn.Width = 107;
            // 
            // UnitColumn
            // 
            this.UnitColumn.AppearanceCell.Options.UseTextOptions = true;
            this.UnitColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UnitColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.UnitColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.UnitColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.UnitColumn.Caption = "工作单位&辖区派出所";
            this.UnitColumn.ColumnEdit = this.repositoryItemMemoEdit3;
            this.UnitColumn.FieldName = "Address";
            this.UnitColumn.Name = "UnitColumn";
            this.UnitColumn.Visible = true;
            this.UnitColumn.VisibleIndex = 8;
            this.UnitColumn.Width = 198;
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // AddrColumn
            // 
            this.AddrColumn.AppearanceCell.Options.UseTextOptions = true;
            this.AddrColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AddrColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.AddrColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.AddrColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.AddrColumn.Caption = "地址";
            this.AddrColumn.ColumnEdit = this.repositoryItemMemoEdit2;
            this.AddrColumn.FieldName = "Policeaddress";
            this.AddrColumn.Name = "AddrColumn";
            this.AddrColumn.Visible = true;
            this.AddrColumn.VisibleIndex = 9;
            this.AddrColumn.Width = 125;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // RemarkColumn
            // 
            this.RemarkColumn.AppearanceCell.Options.UseTextOptions = true;
            this.RemarkColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.RemarkColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.RemarkColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.RemarkColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.RemarkColumn.Caption = "备注";
            this.RemarkColumn.ColumnEdit = this.repositoryItemMemoEdit1;
            this.RemarkColumn.FieldName = "Remark";
            this.RemarkColumn.Name = "RemarkColumn";
            this.RemarkColumn.Visible = true;
            this.RemarkColumn.VisibleIndex = 10;
            this.RemarkColumn.Width = 146;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // SendColumn
            // 
            this.SendColumn.AppearanceCell.Options.UseTextOptions = true;
            this.SendColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SendColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SendColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.SendColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SendColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SendColumn.Caption = "发函";
            this.SendColumn.ColumnEdit = this.mButtonEdit;
            this.SendColumn.Name = "SendColumn";
            this.SendColumn.Visible = true;
            this.SendColumn.VisibleIndex = 11;
            this.SendColumn.Width = 61;
            // 
            // mButtonEdit
            // 
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.mButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.mButtonEdit.Name = "mButtonEdit";
            this.mButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // OpenColumn
            // 
            this.OpenColumn.AppearanceCell.Options.UseTextOptions = true;
            this.OpenColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OpenColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OpenColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.OpenColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OpenColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OpenColumn.Caption = "打开";
            this.OpenColumn.ColumnEdit = this.mOpenButtonEdit;
            this.OpenColumn.Name = "OpenColumn";
            this.OpenColumn.Visible = true;
            this.OpenColumn.VisibleIndex = 12;
            this.OpenColumn.Width = 62;
            // 
            // mOpenButtonEdit
            // 
            this.mOpenButtonEdit.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.mOpenButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.mOpenButtonEdit.Name = "mOpenButtonEdit";
            this.mOpenButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.SetWorkSpace,
            this.barButtonItem1,
            this.SelectFiles,
            this.barButtonItem2,
            this.OpenCurrentFile,
            this.currentfile});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit4});
            this.ribbonControl1.Size = new System.Drawing.Size(1113, 160);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // SetWorkSpace
            // 
            this.SetWorkSpace.Caption = "设置工作目录";
            this.SetWorkSpace.Id = 1;
            this.SetWorkSpace.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("SetWorkSpace.ImageOptions.Image")));
            this.SetWorkSpace.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("SetWorkSpace.ImageOptions.LargeImage")));
            this.SetWorkSpace.Name = "SetWorkSpace";
            this.SetWorkSpace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SetWorkSpace_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "除去的省份";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // SelectFiles
            // 
            this.SelectFiles.Caption = "选择文件";
            this.SelectFiles.Id = 3;
            this.SelectFiles.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("SelectFiles.ImageOptions.Image")));
            this.SelectFiles.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("SelectFiles.ImageOptions.LargeImage")));
            this.SelectFiles.Name = "SelectFiles";
            this.SelectFiles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SelectFiles_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "刷新";
            this.barButtonItem2.Id = 4;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // OpenCurrentFile
            // 
            this.OpenCurrentFile.Caption = "打开当前文件";
            this.OpenCurrentFile.Id = 5;
            this.OpenCurrentFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OpenCurrentFile.ImageOptions.Image")));
            this.OpenCurrentFile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("OpenCurrentFile.ImageOptions.LargeImage")));
            this.OpenCurrentFile.Name = "OpenCurrentFile";
            this.OpenCurrentFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenCurrentFile_ItemClick);
            // 
            // currentfile
            // 
            this.currentfile.Caption = "当前文件";
            this.currentfile.Edit = this.repositoryItemMemoEdit4;
            this.currentfile.EditValue = "";
            this.currentfile.EditWidth = 1000;
            this.currentfile.Id = 6;
            this.currentfile.Name = "currentfile";
            // 
            // repositoryItemMemoEdit4
            // 
            this.repositoryItemMemoEdit4.Name = "repositoryItemMemoEdit4";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "工程目录";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.SetWorkSpace);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.ItemLinks.Add(this.SelectFiles);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem2);
            this.ribbonPageGroup1.ItemLinks.Add(this.OpenCurrentFile);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "工程目录设置";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.currentfile);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 605);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1113, 24);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // mFileSystemWatcher
            // 
            this.mFileSystemWatcher.EnableRaisingEvents = true;
            this.mFileSystemWatcher.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 629);
            this.Controls.Add(this.workspace);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "政审发函平台";
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mDockManager)).EndInit();
            this.workspace.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager mDockManager;
        private DevExpress.XtraBars.Docking.DockPanel workspace;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem SetWorkSpace;
        private DevExpress.XtraGrid.GridControl mGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView mGridView;
        private DevExpress.XtraGrid.Columns.GridColumn NoColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ObjNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn TitleColumn;
        private DevExpress.XtraGrid.Columns.GridColumn NameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PoliticalColumn;
        private DevExpress.XtraGrid.Columns.GridColumn MarigeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn IDCardColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PhoneColumn;
        private DevExpress.XtraGrid.Columns.GridColumn UnitColumn;
        private DevExpress.XtraGrid.Columns.GridColumn RemarkColumn;
        private DevExpress.XtraGrid.Columns.GridColumn SendColumn;
        private DevExpress.XtraGrid.Columns.GridColumn OpenColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit mButtonEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit mOpenButtonEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem SelectFiles;
        private System.IO.FileSystemWatcher mFileSystemWatcher;
        private DevExpress.XtraGrid.Columns.GridColumn AddrColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem OpenCurrentFile;
        private DevExpress.XtraBars.BarEditItem currentfile;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit4;
    }
}

