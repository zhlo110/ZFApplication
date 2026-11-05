using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using ZFApplication.Database;
using ZFApplication.Uitility;
using static ZFApplication.UI.CityForm;

namespace ZFApplication.UI
{
    public partial class UpdateListForm : DevExpress.XtraEditors.XtraForm
    {
        public UpdateListForm()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            PathUtility.setcurrentpath();
            string mdbfile = SystemUtility.GetSystemValues("FilePath");

            string cityfile = "./ds/City.mdb";

            string sql = "select [CityID] from UpdateList order by [CityID]";
            IList list = new ArrayList();
            DataSet ds = AccessDB.Query(mdbfile, sql, list);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            int cityid = DatabaseUtility.getIntValue(row, "CityID", -2);
                            if (cityid > -2)
                            {
                                if (cityid == -1)
                                {
                                    this.mListBox.Items.Add(new ListItem { Text = "地址解析错误的文件", Tag = -1 });
                                    //全国
                                    //this.mListBox.Items.Add("地址解析错误的我文件");
                                }
                                else
                                {
                                    this.mListBox.Items.Add(new ListItem { Text = CityUtility.GetCityName(cityfile, cityid), Tag = cityid });

                                 //   this.mListBox.Items.Add(CityUtility.GetCityName(cityfile, cityid));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            //刪除
            ListItem item = (ListItem)mListBox.SelectedItem;
            if (item == null) return;

            int currentID = (int)item.Tag;

            string sql = "";

            PathUtility.setcurrentpath();
            string mdbfile = SystemUtility.GetSystemValues("FilePath");

            if (MessageBox.Show("确定要从更新列表中删除吗？，是否删除？", "删除", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            else
            {
                //删除City表中的数据
                sql = "delete from UpdateList where [CityID] = @cid";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@cid", currentID));
                AccessDB.NoQuery(mdbfile, sql, list);
                //更新界面
                mListBox.Items.Remove(item);
            }


        }
    }
}