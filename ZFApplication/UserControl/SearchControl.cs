using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZFApplication.UserControl
{
    public partial class SearchControl : DevExpress.XtraEditors.XtraUserControl
    {
        IList mNameList = new ArrayList();
        IList mIDList = new ArrayList();
        IList mSearchList = new ArrayList();
        public void setList(IList namelist, IList idlist)
        {
            mNameList = namelist;
            mIDList = idlist;

            mCombox.Properties.Items.Clear();
            foreach (object item in mNameList)
            {
                mCombox.Properties.Items.Add(item);
            }
        }
        GridView mGridView = null;
        public void setGridView(GridView gridview)
        {
            mGridView = gridview;
        }

        public SearchControl()
        {
            InitializeComponent();
           

        }

        private void ClearFilter_Click(object sender, EventArgs e)
        {
            mSearchList.Clear();
            mGridView.ClearColumnsFilter();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            mSearchList.Clear();
            mGridView.ClearColumnsFilter();
            if (mIDList.Count <= 0) return;
            int index = mCombox.SelectedIndex;
            if (index < 0)
            {
              //  mSearchList.Clear();
            //    mGridView.ClearColumnsFilter();
                mGridView.GridControl.Tag = "";
                mGridView.GridControl.RefreshDataSource();
                return;
            }
            string colname = mIDList[index].ToString();
            string value = SearchText.Text.Trim();
            if (value == "" || value == null)
            {
               
            //    mGridView.ClearColumnsFilter();
                mGridView.GridControl.Tag = "";
                mGridView.GridControl.RefreshDataSource();
                return;
            }
            string key = "["+colname + "] like '%" + value + "%'";
            if (!mSearchList.Contains(key))
            {
                mSearchList.Add(key);
            }
          //  mSearchList.Add((colname + " like '%" + value + "%'"));
            string filter = "";
            foreach (string item in mSearchList)
            {
                filter += item + " AND ";
            }
            if(filter.Length>4)
            {
                filter = filter.Substring(0, filter.Length - 4);
            }
            // filter = filter.Substring(0, filter.Length - 4);

            //    CriteriaOperator criteria = CriteriaOperator.Parse(filter);
            mGridView.GridControl.Tag = filter;
            mGridView.GridControl.RefreshDataSource();
            
            //  mGridView.RefreshDataSource();
            //VirtualServerModeSource datasource = (VirtualServerModeSource)mGridView.DataSource;
            // datasource.Refresh();
            //mGridView.DataSource.s
            // mGridView.ActiveFilterCriteria = criteria;

            /*
            var criteria = CriteriaOperator.Parse("Name like '%高%' AND IDCard = '1212'");
            mGridView.ActiveFilterCriteria = criteria;*/

        }

        private void mCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchText.Text = "";
        }
    }
}
