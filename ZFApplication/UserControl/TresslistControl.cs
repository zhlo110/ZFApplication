using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.Uitility;

namespace ZFApplication.UserControl
{
    public partial class TresslistControl : DevExpress.XtraEditors.XtraUserControl
    {
        public TresslistControl()
        {
            InitializeComponent();
            mSearchText.Visible = false;


        }
        TreeListClickListener mTreelistListener = null;
        public void setTreeListClickListener(TreeListClickListener listener)
        {
            mTreelistListener = listener;
        }
        public void LoadTree()
        {
            mTreeList.Enabled = false;
            mTreeList.ClearNodes();
            mTreeList.AppendNode(new object[] { "全国", -1 }, null);
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfile))
            {

                string sql = "select [ID],[Name] from City where [ParentID]=0 order by [Name] asc";
                DataSet ds = AccessDB.Query(mdbfile, sql, null);
                if (ds != null)
                {
                    if(ds.Tables.Count == 0) return;
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        string name = DatabaseUtility.getStringValue(dr, "Name");
                        DevExpress.XtraTreeList.Nodes.TreeListNode node = mTreeList.AppendNode(new object[] { name, id }, null);
                        node.Tag = id;
                        node.HasChildren = true;
                        //LoadChildTree(mdbfile, node, id);
                    }
                }
            }
            mTreeList.Enabled = true;
        }

        private bool haschild(string mdbfile, int parentid)
        {
            bool haschild = false;
            string sql = "select count(ID) from City where [ParentID]=@parentid";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@parentid", parentid));
            int nCount = AccessDB.QueryStatistic(mdbfile, sql, list);
            if (nCount > 0)
            {
                haschild = true;
            }
            return haschild;
        }


        private void LoadChildTree(string mdbfile, DevExpress.XtraTreeList.Nodes.TreeListNode node, int parentid)
        {
            string sql = "select [ID],[Name] from City where [ParentID]=@parentid  order by [Name] asc";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@parentid", parentid));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                    string name = DatabaseUtility.getStringValue(dr, "Name");
                    DevExpress.XtraTreeList.Nodes.TreeListNode childnode = mTreeList.AppendNode(new object[] { name, id }, node);
                    node.Tag = id;
                    childnode.HasChildren = haschild(mdbfile, id);
                    LoadChildTree(mdbfile, childnode, id);
                }
            }
        }

        void setNodeVisible(TreeListNode node, bool visible)
        {
            node.Visible = visible;
            foreach (TreeListNode child in node.Nodes)
            {
                setNodeVisible(child, visible);
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            string searchtext = mSearchText.Text;
            TreeListNodes list = mTreeList.Nodes;
            if (string.IsNullOrEmpty(searchtext))
            {
                foreach (TreeListNode node in list)
                {
                    setNodeVisible(node, true);
                }
                return;
            }
            TreeListNode[] collection = mTreeList.FindNodes(x => x.GetDisplayText(0).Contains(searchtext));
            //  mTreeList.Nodes

            foreach (TreeListNode node in list)
            {
                setNodeVisible(node, false);
                //for(node.Nodes)

            }

            foreach (TreeListNode node in collection)
            {
                node.Visible = true;
                TreeListNode parentNode = node.ParentNode;
                while (parentNode != null)
                {
                    parentNode.Visible = true;
                    parentNode = parentNode.ParentNode;
                }
            }
            mTreeList.Refresh();


        }

        private void TresslistControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                /*
                if (!mBackgroundWorker.IsBusy)
                {
                    mBackgroundWorker.RunWorkerAsync();
                }*/
                LoadTree();
            //    mTreeList.ExpandAll();
            }
        }

        public void reLoad()
        {
            LoadTree();
            /*
            if (!mBackgroundWorker.IsBusy)
            {
                mBackgroundWorker.RunWorkerAsync();
            }*/
            // 
            //  mTreeList.ExpandAll();
        }

        private void mTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            // mGridControl.DataSource = null;
            if (e.Node != null)
            {
                string id = e.Node.GetDisplayText("treeListColumn2");
                if (mTreelistListener != null)
                {
                    mTreelistListener.OnTreeListClick(Convert.ToInt32(id));
                }

            }
        }

        private void TresslistControl_Enter(object sender, EventArgs e)
        {
            LoadTree();
        }

        private void mBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void mBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void mBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void mTreeList_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            
            
        }

        private void mTreeList_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            mTreeList.Enabled = false;
            TreeListNode parentNode = e.Node;
            // 检查父节点是否有子节点
            if (parentNode.HasChildren)
            {
                if (parentNode.Nodes.Count == 0)
                {
                    string mdbfile = SystemUtility.GetSystemValues("FilePath");
                    LoadChildTree(mdbfile, e.Node, (int)e.Node.Tag);
                }
            }
            mTreeList.Enabled = true;
        }
    }
}
