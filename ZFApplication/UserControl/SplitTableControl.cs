using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Model;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.Model;
using ZFApplication.UI;
using ZFApplication.Uitility;

namespace ZFApplication.UserControl
{
    public partial class SplitTableControl : DevExpress.XtraEditors.XtraUserControl, TreeListClickListener
    {
        private int batchSize = 20; // 每次加载的数据量
        private int maxRowCount = 0; // 总数据量
        public SplitTableControl()
        {
            InitializeComponent();

            IList namelist = new ArrayList();
            IList idlist = new ArrayList();
            namelist.Add("编号");
            namelist.Add("招飞对象");
            namelist.Add("称谓");
            namelist.Add("姓名");
            namelist.Add("民族");

            namelist.Add("政治面貌");
            namelist.Add("婚姻状况");
            namelist.Add("身份证号");
            namelist.Add("电话号码");
            namelist.Add("信息类型");

            namelist.Add("详细地址");
            namelist.Add("所在派出所");
            namelist.Add("备注");


            idlist.Add("No");
            idlist.Add("ObjName");
            idlist.Add("Title");
            idlist.Add("Name");
            idlist.Add("Nation");

            idlist.Add("Political");
            idlist.Add("Marriage");
            idlist.Add("IDCard");
            idlist.Add("Phonenumber"); 
            idlist.Add("Addresstype");
            idlist.Add("Address");
            idlist.Add("PoliceAddr");
            idlist.Add("Remark");

            mSearchControl.setList(namelist, idlist);
            mSearchControl.setGridView(mGridView);

            mGridControl.DataSource = mVirtualSource;

            mTresslistControl.setTreeListClickListener(this);
        }
        int mCityid = -1;
        private BindingList<PersonInfo> GetRows(int startRowIndex)
        {
            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(mdbfile))
            {
                return null;
            }
            IList list = new ArrayList();

            string condtion = (string)mGridControl.Tag;
            if (condtion == null)
            {
                condtion = "";
            }
            condtion = condtion.Trim();
            if (condtion.Length > 0)
            {
                condtion = " and " + condtion;
                // list.Add(new DictionaryEntry("@condition", "%" + condtion + "%"));
            }


            string sql = "";
            
            
            if (mCityid != -1) //城市的
            {
                sql = "select  count([ID]) from Person where [CityID] = @cityid and [IsDelete]=0";
                list.Add(new DictionaryEntry("@cityid", id));
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);


                int endRowIndex = 0;
                int realSize = batchSize;
                SystemUtility.calpageresult(maxRowCount, startRowIndex, out endRowIndex, out realSize);
                Console.WriteLine(realSize.ToString() + "," + endRowIndex.ToString());

                /*
                sql = @"
                SELECT * FROM (
                    SELECT TOP " + realSize.ToString() + @" * FROM (
                        SELECT TOP " + endRowIndex.ToString() + @" 
                            [ID],[Name],[IDCard],[No],[ObjName],[Title],
                            [Political],[Nation],[Marriage],
                            [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                            [Position],[IsVisit],[Url],[OrderNo],[AddressOrder]
                        FROM Person
                        WHERE [CityID] = @cityid
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc
                    )
                    ORDER BY [ObjName] DESC,[OrderNo] DESC,[Name] DESC,[AddressOrder] DESC
                )
                ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc;
                ";*/


                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT 
                            [ID],[Name],[IDCard],[No],[ObjName],[Title],
                            [Political],[Nation],[Marriage],
                            [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                            [Position],[IsVisit],[Url],[OrderNo],[AddressOrder]
                        FROM Person
                        WHERE [CityID] = @cityid
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc
                        Limit " + endRowIndex.ToString() + @"
                    )
                    ORDER BY [ObjName] DESC,[OrderNo] DESC,[Name] DESC,[AddressOrder] DESC
                    Limit " + realSize.ToString() + @"
                )
                ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc;
                ";

            }
            else //全国的
            {
                sql = "select  count([ID]) from Person where [IsDelete]=0 and [IsReal]=1";
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);
               
                int endRowIndex = 0;
                int realSize = batchSize;
                SystemUtility.calpageresult(maxRowCount, startRowIndex, out endRowIndex, out realSize);
                Console.WriteLine(realSize.ToString() + ","+endRowIndex.ToString());

                /*
                sql = @"
                SELECT * FROM (
                    SELECT TOP " + realSize.ToString() + @" * FROM (
                        SELECT TOP " + endRowIndex.ToString() + @" 
                            [ID],[Name],[IDCard],[No],[ObjName],[Title],
                            [Political],[Nation],[Marriage],
                            [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                            [Position],[IsVisit],[Url],[OrderNo],[AddressOrder]
                        FROM Person
                        WHERE [IsReal]=1
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc
                    )
                    ORDER BY [ObjName] DESC,[OrderNo] DESC,[Name] DESC,[AddressOrder] DESC
                )
                ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc;
                ";*/

                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT 
                            [ID],[Name],[IDCard],[No],[ObjName],[Title],
                            [Political],[Nation],[Marriage],
                            [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                            [Position],[IsVisit],[Url],[OrderNo],[AddressOrder]
                        FROM Person
                        WHERE [IsReal]=1
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc
                        Limit " + endRowIndex.ToString() + @"
                    )
                    ORDER BY [ObjName] DESC,[OrderNo] DESC,[Name] DESC,[AddressOrder] DESC
                    Limit " + realSize.ToString() + @"
                )
                ORDER BY [ObjName] asc,[OrderNo] asc,[Name] asc,[AddressOrder] asc;
                ";

                /*
                sql = @"select [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] from Person where [IsDelete]=0 and [IsReal]=1 order by [ObjName],[OrderNo],[Name],[AddressOrder] asc";

                */
            }


            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                { 
                    DataTable dt = ds.Tables[0];

                    BindingList<PersonInfo> list1 = new BindingList<PersonInfo>();
                    //   BindingSource bs = new BindingSource();
                    int nCount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        PersonInfo info = new PersonInfo();
                        string svisit = DatabaseUtility.getStringValue(dr, "IsVisit");
                        info.IsVisit = svisit == "1";
                        info.ID = DatabaseUtility.getIntValue(dr, "ID", -1);
                        info.No = DatabaseUtility.getStringValue(dr, "No");
                        info.ObjName = DatabaseUtility.getStringValue(dr, "ObjName");
                        info.Title = DatabaseUtility.getStringValue(dr, "Title");
                        info.Name = DatabaseUtility.getStringValue(dr, "Name");
                        info.Nation = DatabaseUtility.getStringValue(dr, "Nation");
                        info.Political = DatabaseUtility.getStringValue(dr, "Political");
                        info.Marriage = DatabaseUtility.getStringValue(dr, "Marriage");
                        info.IDCard = DatabaseUtility.getStringValue(dr, "IDCard");
                        info.Phonenumber = DatabaseUtility.getStringValue(dr, "PhoneNum");
                        info.Addresstype = DatabaseUtility.getStringValue(dr, "AddrType");
                        info.PoliceAddr = DatabaseUtility.getStringValue(dr, "PoliceAddr");
                        info.Url = DatabaseUtility.getStringValue(dr, "Url");

                        if (info.Addresstype == "工作单位及职务")
                        {
                            if (info.Title != "本人")
                            {
                                info.Address = "工作单位:" + DatabaseUtility.getStringValue(dr, "Address")
                                    + "\r\n职务:" + DatabaseUtility.getStringValue(dr, "Position");
                            }
                            else
                            {
                                info.Address = "学校:" + DatabaseUtility.getStringValue(dr, "Address")
                                    + "\r\n班级:" + DatabaseUtility.getStringValue(dr, "Position");
                            }
                        }
                        else
                        {
                            info.Address = DatabaseUtility.getStringValue(dr, "Address");
                        }
                        info.Remark = DatabaseUtility.getStringValue(dr, "Remark");
                        string ispublic = DatabaseUtility.getStringValue(dr, "IsPublic");
                        info.IsPublic = ispublic.Equals("1") ? "是" : "否";
                        list1.Add(info);
                        nCount++;
                    }
                    return list1;
               }
               else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            //  return StatisticUtility.Statistic(listener);
        }
        public object ShowCallback(SingleBarInterface listener)
        {
            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(mdbfile))
            {
                return null;
            }
            IList list = new ArrayList();
            string sql = "";
            if (mCityid != -1) //城市的
            {
                sql = "select  count([ID]) from Person where [CityID] = @cityid and [IsDelete]=0";
                list.Add(new DictionaryEntry("@cityid", id));
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                if (listener != null)
                {
                    maxRowCount = total;
                }

            
            }
            else //全国的
            {
                sql = "select  count([ID]) from Person where [IsDelete]=0 and [IsReal]=1";
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                if (listener != null)
                {
                    maxRowCount = total;
                }
            }
            return true;
            //  return StatisticUtility.Statistic(listener);
        }

        public object ShowCallback1(SingleBarInterface listener)
        {
            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(mdbfile))
            {
                if (listener != null)
                {
                    listener.progressend();
                }
                return null;
            }
            IList list = new ArrayList();
            string sql = "";
            if (mCityid != -1) //城市的
            {
                sql = "select  count([ID]) from Person where [CityID] = @cityid and [IsDelete]=0";
                list.Add(new DictionaryEntry("@cityid", id));
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                if(listener!=null)
                {
                    listener.settotalnumber(total);
                }

                sql = @"select [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] from Person where [CityID] = @cityid and [IsDelete]=0 order by [ObjName],[OrderNo],[Name],[AddressOrder] asc";
                
            }
            else //全国的
            {
                sql = "select  count([ID]) from Person where [IsDelete]=0 and [IsReal]=1";            
                int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                if (listener != null)
                {
                    listener.settotalnumber(total);
                }

                sql = @"select [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] from Person where [IsDelete]=0 and [IsReal]=1 order by [ObjName],[OrderNo],[Name],[AddressOrder] asc";
            }


            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                BindingList<PersonInfo> list1 = new BindingList<PersonInfo>();
                //   BindingSource bs = new BindingSource();
                int nCount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    PersonInfo info = new PersonInfo();
                    string svisit = DatabaseUtility.getStringValue(dr, "IsVisit");
                    info.IsVisit = svisit == "1";
                    info.ID = DatabaseUtility.getIntValue(dr, "ID", -1);
                    info.No = DatabaseUtility.getStringValue(dr, "No");
                    info.ObjName = DatabaseUtility.getStringValue(dr, "ObjName");
                    info.Title = DatabaseUtility.getStringValue(dr, "Title");
                    info.Name = DatabaseUtility.getStringValue(dr, "Name");
                    info.Nation = DatabaseUtility.getStringValue(dr, "Nation");
                    info.Political = DatabaseUtility.getStringValue(dr, "Political");
                    info.Marriage = DatabaseUtility.getStringValue(dr, "Marriage");
                    info.IDCard = DatabaseUtility.getStringValue(dr, "IDCard");
                    info.Phonenumber = DatabaseUtility.getStringValue(dr, "PhoneNum");
                    info.Addresstype = DatabaseUtility.getStringValue(dr, "AddrType");
                    info.PoliceAddr = DatabaseUtility.getStringValue(dr, "PoliceAddr");
                    info.Url = DatabaseUtility.getStringValue(dr, "Url");

                    if (info.Addresstype == "工作单位及职务")
                    {
                        if (info.Title != "本人")
                        {
                            info.Address = "工作单位:" + DatabaseUtility.getStringValue(dr, "Address")
                                + "\r\n职务:" + DatabaseUtility.getStringValue(dr, "Position");
                        }
                        else
                        {
                            info.Address = "学校:" + DatabaseUtility.getStringValue(dr, "Address")
                                + "\r\n班级:" + DatabaseUtility.getStringValue(dr, "Position");
                        }
                    }
                    else
                    {
                        info.Address = DatabaseUtility.getStringValue(dr, "Address");
                    }
                    info.Remark = DatabaseUtility.getStringValue(dr, "Remark");
                    string ispublic = DatabaseUtility.getStringValue(dr, "IsPublic");
                    info.IsPublic = ispublic.Equals("1") ? "是" : "否";
                    list1.Add(info);
                    if(listener != null)
                    {
                        listener.settotalcurrent(nCount);
                    }
                    nCount++;
                }
                if (listener != null)
                {
                    listener.progressend();
                }
                return list1;


            }
            else
            {
                if (listener != null)
                {
                    listener.progressend();
                }
                return null;
            }
            //  return StatisticUtility.Statistic(listener);
        }
        public void reload()
        {
            mTresslistControl.reLoad();
        }

        public void export()
        {
            XlsxExportOptionsEx optionsEx= new XlsxExportOptionsEx();
            
            mGridControl.ExportToXlsx("D:\\aaa.xlsx");
        }
        public void EndCallback(object obj)
        {
            if (obj != null)
            {
                mGridControl.RefreshDataSource();
                //mGridControl.DataSource = obj;
                mItemCheckEdit.EditValueChanged -= ItemCheckEdit_CheckedChanged;
                mItemCheckEdit.EditValueChanged += ItemCheckEdit_CheckedChanged;
                mItemCheckEdit.Click -= mItemCheckEdit_Click;
                mItemCheckEdit.Click += mItemCheckEdit_Click;
            //    mItemCheckEdit.EditValueChanged

                mDeleteItemButtonEdit.ButtonClick -= DeleteButton_ButtonClick;
                mDeleteItemButtonEdit.ButtonClick += DeleteButton_ButtonClick;

                mOpenItemButtonEdit.ButtonClick -= OpenButton_ButtonClick;
                mOpenItemButtonEdit.ButtonClick += OpenButton_ButtonClick;
            }
        }

        private void mItemCheckEdit_Click(object sender, EventArgs e)
        {
            CheckEdit editor = sender as CheckEdit;
            if (editor != null)
            {
                bool newValue = editor.Checked;
                // 在这里处理复选框值更改逻辑
                editor.EditValue = !newValue;
                // throw new NotImplementedException();
            }
        }

        private void OpenButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            PersonInfo rowData = (PersonInfo)mGridView.GetRow(handle);
            if (rowData != null)
            {
                if (File.Exists(rowData.Url))
                {
                    try
                    {
                        // 指定.xlsx文件的完整路径
                        //    string filePath = @"C:\path\to\your\file.xlsx";

                        // 使用默认程序打开.xlsx文件，通常是Excel
                        Process.Start(new ProcessStartInfo(rowData.Url) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"无法打开文件: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("数据文件路径更改了，之前的路径为‘" + rowData.Url + "'");
                    LogUtility.Log("数据文件路径更改了，之前的路径为‘" + rowData.Url + "'");
                }
            }
        }


        private void DeleteButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            PersonInfo rowData = (PersonInfo)mGridView.GetRow(handle);

            DialogResult result = XtraMessageBox.Show("确认要删除这条记录吗？", "确认",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (rowData != null)
                {
                    string mdbfiles = SystemUtility.GetSystemValues("FilePath");
                    if (File.Exists(mdbfiles))
                    {
                        SystemUtility.selectRealIDFunction(itemDeletecallback, mdbfiles, rowData.ID, true);
                        /*
                        string sql = "update Person set [IsDelete]=1 where [ID]=@id";
                        IList list = new ArrayList();
                        list.Add(new DictionaryEntry("@id", rowData.ID));  
                        AccessDB.NoQuery(mdbfiles, sql, list);
                        */

                        //更新工作区
                        OnTreeListClick(mCityid);
                    }
                }
            }
        }
        public void OnTreeListClick(int cityid)
        {
          //  mGridControl.DataSource = null;
            mCityid = cityid;
            SingleProgressBar progress = new SingleProgressBar();
            progress.setShowCallback(ShowCallback);
            progress.setEndCallback(EndCallback);
            // progress.ShowDialog();
            progress.showwithoutwindows();
        }

        public void itemDeletecallback(string mdbfile, int realid, object newValue)
        {
            string sql = "update Person set [IsDelete]=1 where [ID]=@id or [RealID]=@rid";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@id", realid));
            list.Add(new DictionaryEntry("@rid", realid));
            AccessDB.NoQuery(mdbfile, sql, list);
        }

        public void itemCheckedChangedcallback(string mdbfile, int realid, object newValue)
        {
            IList list = new ArrayList();
            string sql = "update Person set [IsVisit]=@visit where [ID]=@id or [RealID]=@rid";//
            list.Add(new DictionaryEntry("@visit", (bool)newValue ? "1" : "0"));
            list.Add(new DictionaryEntry("@id", realid));
            list.Add(new DictionaryEntry("@rid", realid));
            AccessDB.NoQuery(mdbfile, sql, list);
        }

        

        private void ItemCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(mdbfile))
            {
                return;
            }
            var handle = mGridView.FocusedRowHandle;
            PersonInfo rowData = (PersonInfo)mGridView.GetRow(handle);
            //获取checkbox的状态

            CheckEdit editor = sender as CheckEdit;
            if (editor != null)
            {
                bool newValue = editor.Checked;
                // 在这里处理复选框值更改逻辑
                //写数据库
                ////////////////////////////////
                ///
                SystemUtility.selectRealIDFunction(itemCheckedChangedcallback,mdbfile, rowData.ID, newValue);
                /////////////////////////////
                // 可选：更新绑定的数据源
                mGridView.PostEditor(); // 确保编辑值被推送到数据源
                mGridView.UpdateCurrentRow(); // 更新当前行
            }
         
        }

        private void mGridView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            var name1 = mGridView.GetRowCellValue(e.RowHandle1, "Name").ToString();
            var name2 = mGridView.GetRowCellValue(e.RowHandle2, "Name").ToString();


            if (e.Column.FieldName != "IsVisit" && e.Column.FieldName != "Isdelete" && e.Column.FieldName != "IsOpen")
            {
                // 只有当“Name”列的值相同时，才进一步考虑合并
                if (name1 == name2 && name1 != null && Name.Length > 0)
                {
                    // 检查当前处理的列是否是我们希望根据“Name”列的值来合并的列
                    if (e.Column.FieldName != "Name")
                    {
                        // 对于除“Name”列外的列，检查它们的值是否相同
                        var value1 = mGridView.GetRowCellValue(e.RowHandle1, e.Column);
                        var value2 = mGridView.GetRowCellValue(e.RowHandle2, e.Column);

                        // 如果值相同，则允许合并
                        e.Merge = object.Equals(value1, value2);
                    }
                    else
                    {
                        // 如果是“Name”列，只需检查值是否相同即可决定是否合并
                        e.Merge = true;
                    }
                }
                else
                {
                    // 如果“Name”列的值不相同，则不允许合并
                    e.Merge = false;
                }
            }
            else
            {
                e.Merge = false;
            }

            e.Handled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            export();
        }

        private void SplitTableControl_Enter(object sender, EventArgs e)
        {
            OnTreeListClick(mCityid);
        }

        private void mVirtualSource_ConfigurationChanged(object sender, DevExpress.Data.VirtualServerModeRowsEventArgs e)
        {
            BindingList<PersonInfo> persons = GetRows(0);
            e.UserData = persons;
        }

        private void mVirtualSource_MoreRows(object sender, DevExpress.Data.VirtualServerModeRowsEventArgs e)
        {
            e.RowsTask = Task.Factory.StartNew(() =>
            {
                bool moreRows = e.CurrentRowCount < maxRowCount - batchSize;
                return new VirtualServerModeRowsTaskResult(GetRows(e.CurrentRowCount), moreRows, e.UserData);
            }, e.CancellationToken);
        }
    }
}
