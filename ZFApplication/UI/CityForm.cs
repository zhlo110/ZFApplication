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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraRichEdit.Import.Html;
using ZFApplication.Database;
using ZFApplication.Model;
using ZFApplication.Uitility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ZFApplication.UI
{
    public partial class CityForm : DevExpress.XtraEditors.XtraForm
    {
        public class ListItem
        {
            public string Text { get; set; }
            public object Tag { get; set; }

            public override string ToString()
            {
                return Text; // ListBoxControl 会调用此方法来显示文本
            }
        }

        string mdbfile = "./ds/City.mdb";
        public CityForm()
        {
            InitializeComponent();
        }

        private void CityForm_Load(object sender, EventArgs e)
        {
            //first load
            FirstBoxControl.Items.Clear();
            string sql = "select [ID],[Name],[ShortName] from City where ParentID=0";
            LoadData(sql, null, FirstBoxControl);

        }
        public void LoadData(string sql, IList list, ListBoxControl control)
        {
            //load data

            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                int nCount = dt.Rows.Count;
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        string name = DatabaseUtility.getStringValue(dr, "Name");
                        string shortName = DatabaseUtility.getStringValue(dr, "ShortName");
                        control.Items.Add(new ListItem{ Text = name + "," + shortName, Tag = id });
                    }
                    control.SortOrder = SortOrder.Ascending;
                }
            }
            // gridControl1.DataSource = dt;
        }

        private void FirstBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //second load
            //   string sql = "select [Name],[ShortName] from City where ParentID=@ParentID";
            SecondBoxControl.Items.Clear();
            if (FirstBoxControl.SelectedItem == null) return;

            ListItem item = (ListItem)FirstBoxControl.SelectedItem;
            string name = item.Text;
            if (name == null) return;
            string[] strings = name.Split(',');
            if (strings.Length > 0)
            {
                int parentid = (int)item.Tag;
                string sql = "select [ID],[Name],[ShortName] from City where ParentID =@parentid";
                IList list = new ArrayList();
             //   list.Add(new DictionaryEntry("@name", strings[0]));
                list.Add(new DictionaryEntry("@parentid", parentid));
                LoadData(sql, list, SecondBoxControl);
            }
            //MessageBox.Show(name);
        }

        private void SecondBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThirdBoxControl.Items.Clear();
            if (SecondBoxControl.SelectedItem == null) return;

            ListItem item = (ListItem)SecondBoxControl.SelectedItem;

            string name = item.Text.Trim();
            if (name == null) return;
            string[] strings = name.Split(',');
            if (strings.Length > 0)
            {
                int parentid = (int)item.Tag;  
                string sql = "select [ID],[Name],[ShortName] from City where ParentID = @parentid";
                IList list = new ArrayList();
                //list.Add(new DictionaryEntry("@name", strings[0]));
                list.Add(new DictionaryEntry("@parentid", parentid));
                LoadData(sql, list, ThirdBoxControl);

            }
        }

        private int getParentID(ListBoxControl control)
        {
            int parentID = -1;
            if (control.SelectedItem == null) return parentID;
            ListItem item = (ListItem)control.SelectedItem;
            string name = item.Text.Trim();
            if (name != null)
            {
                int parentid = (int)item.Tag;
                parentID = parentid;

                /*
                string[] strings = name.Split(',');
                if (strings.Length > 0)
                {
                    string sql = "select [ID] from City where [Name]=@name";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@name", strings[0].Trim()));
                    DataSet ds = AccessDB.Query(mdbfile, sql, list);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            parentID = DatabaseUtility.stringtoint(dr["ID"].ToString(), -1);
                        }
                    }
                }
                */

            }
            return parentID;
        }
        int mParentID = -1;
        ListBoxControl mControl = null;
        private void mMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //获取当前选中的ListBoxControl
            ListBoxControl control = null;
            //鼠标在哪个控件上点击，就获取哪个控件
            Point mousePosition = Control.MousePosition;
            mousePosition = this.PointToClient(mousePosition);
            int parentID = 0;
            mParentID = -1;
            if (FirstBoxControl.Bounds.Contains(mousePosition))
            {
                parentID = 0;
                control = FirstBoxControl;
              //  Console.WriteLine("FirstBoxControl");
            }
            else if (SecondBoxControl.Bounds.Contains(mousePosition))
            {

                control = SecondBoxControl;
                parentID = getParentID(FirstBoxControl);
                //Console.WriteLine("SecondBoxControl");
            }
            else if (ThirdBoxControl.Bounds.Contains(mousePosition))
            {
                control = ThirdBoxControl;
                parentID = getParentID(SecondBoxControl);
                //Console.WriteLine("ThirdBoxControl");
            }

            if (control != null && parentID >= 0)
            {
                mParentID = parentID;
                mControl = control;
                // Do something with the control
            }
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            if (mParentID < 0)
            {
                MessageBox.Show("请选择一个城市");
                return;
            }
            NewCityForm cityForm = new NewCityForm();
            cityForm.Text = "新建";
            cityForm.setParentID(mParentID);
            // cityForm.mParentID = mParentID;
            if (cityForm.ShowDialog() == DialogResult.OK)
            {
              //  mParentID = 0;
                string sql = "";
                sql = "select count(ID) from City where [Name]=@name and [ParentID]=@parentID";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@name", cityForm.getCityName()));
                list.Add(new DictionaryEntry("@parentID", mParentID));
                int count = AccessDB.QueryStatistic(mdbfile, sql, list);
                if (count > 0)
                {
                    MessageBox.Show("该城市已经存在,不能添加");
                    return;
                }


                sql = "insert into City([Name],[ShortName],[ParentID]) values(@name,@shortName,@parentID)";
                list.Clear();
                list.Add(new DictionaryEntry("@name", cityForm.getCityName()));
                list.Add(new DictionaryEntry("@shortName", cityForm.getCityShortName()));
                list.Add(new DictionaryEntry("@parentID", mParentID));
                AccessDB.NoQuery(mdbfile, sql, list);

                sql = "select [ID] from City where [Name]=@name and [ShortName]=@shortName and [ParentID] = @parentID";
                DataSet ds = AccessDB.Query(mdbfile, sql, list);
                if (ds != null) {

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        mControl.Items.Add(new ListItem { Text = cityForm.getCityName() + "," + cityForm.getCityShortName(), Tag = id });
                    }
                }

                //string projmdb = SystemUtility.GetSystemValues("FilePath");
                //AccessDB.NoQuery(projmdb, sql, list);
                //更新界面
             //   mControl.Items.Add(new ListItem(Text = cityForm.getCityName() + "," + cityForm.getCityShortName(),Tag=));
            }
        }

        private void UpdateMenuItem_Click(object sender, EventArgs e)
        {
            if (mParentID < 0)
            {
                MessageBox.Show("请选择一个城市");
                return;
            }

            NewCityForm cityForm = new NewCityForm();
            cityForm.Text = "修改";
            cityForm.setParentID(mParentID);
            ListItem item = (ListItem)mControl.SelectedItem;
            string text = item.Text.Trim();
            text = text.Replace("，", ",");
            text = text.Trim();


            string[] strings = text.Split(',');
            // string[] strings = text.IndexOf(',') > 0 ? text.Split(',') : new string[] { text };
            if (strings.Length > 0)
            {
                cityForm.setCityName(strings[0]);
                if (strings.Length > 1)
                {
                    string shortName = text.Remove(0, text.IndexOf(',') + 1);
                    cityForm.setCityShortName(shortName);
                }
                //cityForm.setCityShortName(strings[1]);
            }
            if (cityForm.ShowDialog() == DialogResult.OK)
            {
                int currentID = (int)item.Tag;
                string sql = "select count(ID) from City where [Name]=@name and [ParentID]=@parentID and [ID] <> @currentid";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@name", cityForm.getCityName()));
                list.Add(new DictionaryEntry("@parentID", mParentID));
                list.Add(new DictionaryEntry("@currentid", currentID));
                int count = AccessDB.QueryStatistic(mdbfile, sql, list);
                if (count > 0)
                {
                    MessageBox.Show("该城市已经存在,不能修改");
                    return;
                }


                //更新数据库City.mdb
                sql = "update City set [Name]=@name,[ShortName]=@shortName where [ParentID]=@parentID and [Name]=@oldName and [ID]=@currentid";
                list.Clear();
                list.Add(new DictionaryEntry("@name", cityForm.getCityName()));
                list.Add(new DictionaryEntry("@shortName", cityForm.getCityShortName()));
                list.Add(new DictionaryEntry("@parentID", mParentID));
                list.Add(new DictionaryEntry("@oldName", strings[0]));
                list.Add(new DictionaryEntry("@currentid", currentID));
                AccessDB.NoQuery(mdbfile, sql, list);

                string projmdb = SystemUtility.GetSystemValues("FilePath");
                AccessDB.NoQuery(projmdb, sql, list);
                //更新界面
                mControl.Items[mControl.SelectedIndex] = new ListItem { Text = cityForm.getCityName() + "," + cityForm.getCityShortName(),Tag = currentID };

               // mControl.Items.Add(new ListItem { Text = cityForm.getCityName() + "," + cityForm.getCityShortName(), Tag = id });
            }
        }

        private void CityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string projectname = SystemUtility.GetSystemValues("FilePath");
            if (projectname != null && projectname.Length > 0)
            {
                SystemUtility.MustUpdate(projectname, "true");
            }
            MessageBox.Show("添加，修改，删除后需要重新启动政审系统，地址才能生效！");
        }

        private void findallCity(int id, IList result)
        {
            string sql = "select [ID] from City where ParentID=@parentID";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@parentID", id));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                int nCount = dt.Rows.Count;
                if (nCount > 0)
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int currentID = DatabaseUtility.getIntValue(dr, "ID", -1);
                        result.Add(currentID);
                        findallCity(currentID, result);
                    }
                }
            }
        }
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            //刪除
            ListItem item = (ListItem)mControl.SelectedItem;
            if (item == null) return;

            int currentID = (int)item.Tag;
            IList IDList = new ArrayList();
            IDList.Add(currentID);
            findallCity(currentID, IDList);

            //在Person表中查找是否有关联，CityID为关联字段，如果在IDList中有值，说明有关联
            string condition = "";
            foreach (int id in IDList)
            {
                condition += id.ToString() + ",";
            }

            if (condition.Length > 0)
            {
                condition = condition.Remove(condition.Length - 1);

                string sql = "select count(ID) from Person where CityID in ("+condition+")";
                IList list = new ArrayList();
                string mdbfiles = SystemUtility.GetSystemValues("FilePath");
                int count = AccessDB.QueryStatistic(mdbfiles, sql, list);
                if (count > 0)
                {
                    MessageBox.Show("该城市已经被使用，不能删除");
                    return;
                }
                else
                {
                    if (MessageBox.Show("确定要删除这个城市吗？，是否删除？", "删除", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        //删除City表中的数据
                        sql = "delete from City where ID in (" + condition + ")";
                        AccessDB.NoQuery(mdbfile, sql, null);
                        //删除Person表中的数据
                        sql = "delete from City where ID in (" + condition + ")";
                        AccessDB.NoQuery(mdbfiles, sql, null);
                        //更新界面
                        mControl.Items.Remove(item);
                    }
                }
            }
        }

        private void addupdatelist_Click(object sender, EventArgs e)
        {
            if (mParentID < 0)
            {
                MessageBox.Show("请选择一个城市");
                return;
            }
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            ListItem item = (ListItem)mControl.SelectedItem;
            int currentID = (int)item.Tag;
            IList list = new ArrayList();
            string sql = "select count(ID) from UpdateList where [CityID]=-1";
            int count = AccessDB.QueryStatistic(mdbfiles, sql, list);
            if (count == 0)//添加一个默认值
            {
                sql = "insert into UpdateList([CityID]) values(-1)";
                AccessDB.NoQuery(mdbfiles, sql, list);
            }

            sql = "select count(ID) from UpdateList where [CityID]=@id";
            list.Add(new DictionaryEntry("@id", currentID));
            count = AccessDB.QueryStatistic(mdbfiles, sql, list);
            if(count == 0)
            {
                sql = "insert into UpdateList([CityID]) values(@id)";
                AccessDB.NoQuery(mdbfiles, sql, list);
                MessageBox.Show("添加成功");
            }

        }

        private void showupdatelist_Click(object sender, EventArgs e)
        {
            UpdateListForm updateListForm = new UpdateListForm();
            updateListForm.ShowDialog();
        }
    }
}