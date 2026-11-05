namespace ZFApplication.UserControl
{
    partial class TresslistControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TresslistControl));
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.mTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.mImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.Search = new DevExpress.XtraEditors.SimpleButton();
            this.mSearchText = new DevExpress.XtraEditors.TextEdit();
            this.mBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mImageCollection)).BeginInit();
            this.sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mSearchText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.mTreeList);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel2.Location = new System.Drawing.Point(0, 36);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(313, 555);
            this.sidePanel2.TabIndex = 2;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // mTreeList
            // 
            this.mTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.mTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTreeList.Location = new System.Drawing.Point(0, 0);
            this.mTreeList.Name = "mTreeList";
            this.mTreeList.BeginUnboundLoad();
            this.mTreeList.AppendNode(new object[] {
            "1",
            null}, -1, 0, 1, 1);
            this.mTreeList.AppendNode(new object[] {
            "2",
            null}, 0, 0, 1, 1);
            this.mTreeList.AppendNode(new object[] {
            "3",
            null}, -1, 0, 1, 2);
            this.mTreeList.EndUnboundLoad();
            this.mTreeList.OptionsBehavior.Editable = false;
            this.mTreeList.SelectImageList = this.mImageCollection;
            this.mTreeList.Size = new System.Drawing.Size(313, 555);
            this.mTreeList.TabIndex = 0;
            this.mTreeList.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.mTreeList_BeforeExpand);
            this.mTreeList.AfterExpand += new DevExpress.XtraTreeList.NodeEventHandler(this.mTreeList_AfterExpand);
            this.mTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.mTreeList_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "行政区划";
            this.treeListColumn1.FieldName = "treeListColumn1";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowSort = false;
            this.treeListColumn1.OptionsFilter.AllowAutoFilter = false;
            this.treeListColumn1.OptionsFilter.AllowFilter = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "treeListColumn2";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // mImageCollection
            // 
            this.mImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("mImageCollection.ImageStream")));
            this.mImageCollection.Images.SetKeyName(0, "content_16x16.png");
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.Search);
            this.sidePanel1.Controls.Add(this.mSearchText);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.sidePanel1.Location = new System.Drawing.Point(0, 0);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(313, 36);
            this.sidePanel1.TabIndex = 1;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // Search
            // 
            this.Search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Search.ImageOptions.Image")));
            this.Search.Location = new System.Drawing.Point(194, 7);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(24, 23);
            this.Search.TabIndex = 1;
            this.Search.Visible = false;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // mSearchText
            // 
            this.mSearchText.Location = new System.Drawing.Point(32, 8);
            this.mSearchText.Name = "mSearchText";
            this.mSearchText.Size = new System.Drawing.Size(152, 20);
            this.mSearchText.TabIndex = 0;
            // 
            // mBackgroundWorker
            // 
            this.mBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mBackgroundWorker_DoWork);
            this.mBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mBackgroundWorker_ProgressChanged);
            this.mBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mBackgroundWorker_RunWorkerCompleted);
            // 
            // TresslistControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
            this.Name = "TresslistControl";
            this.Size = new System.Drawing.Size(313, 591);
            this.Load += new System.EventHandler(this.TresslistControl_Load);
            this.Enter += new System.EventHandler(this.TresslistControl_Enter);
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mImageCollection)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mSearchText.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraTreeList.TreeList mTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SimpleButton Search;
        private DevExpress.XtraEditors.TextEdit mSearchText;
        private DevExpress.Utils.ImageCollection mImageCollection;
        private System.ComponentModel.BackgroundWorker mBackgroundWorker;
    }
}
