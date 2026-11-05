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
using DevExpress.XtraEditors;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.Model;
using ZFApplication.UI;
using ZFApplication.Uitility;

namespace ZFApplication.UserControl
{
    public partial class EmailControl : DevExpress.XtraEditors.XtraUserControl, TreeListClickListener
    {
        private int batchSize = 20; // 每次加载的数据量
        private int maxRowCount = 0; // 总数据量
        public EmailControl()
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
            idlist.Add("Address");
            idlist.Add("PoliceAddr");
            idlist.Add("Remark");

            mSearchControl.setList(namelist, idlist);
            mSearchControl.setGridView(mGridView);
            mGridControl.DataSource = mVirtualSource;

            mTresslistControl.setTreeListClickListener(this);
        }
        int mCityid = -1;

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
            if (mCityid != -1)
            {
                list.Add(new DictionaryEntry("@cityid", id));

                sql = @"select Count([ID]) from Person where [CityID] = @cityid 
                           and [IsPublic]=1 and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    maxRowCount = total;
                }
            }
            else
            {
                sql = @"select count([ID]) from Person where 
                        [IsPublic]=1  and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";

                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    maxRowCount = total;
                }
            }

            return true;
        }
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
            if (mCityid != -1)
            {
                list.Add(new DictionaryEntry("@cityid", id));

                sql = @"select Count([ID]) from Person where [CityID] = @cityid 
                           and [IsPublic]=1 and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";
                {
                    maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);
                }

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
                           [Position],[IsVisit],[Url] 
                        FROM Person
                        WHERE 
                           [CityID] = @cityid 
                           and [IsPublic]=1 
                           and [AddrType]='工作单位及职务' 
                           and [IsDelete]=0 
                           and [IsReal]=1                           
                           " + condtion + @"
                        ORDER BY [ObjName] asc,[Name] asc
                    )
                    ORDER BY [ObjName] desc,[Name] DESC
                )
                ORDER BY [ObjName] asc,[Name] asc;
                ";*/


                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT  
                           [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] 
                        FROM Person
                        WHERE 
                           [CityID] = @cityid 
                           and [IsPublic]=1 
                           and [AddrType]='工作单位及职务' 
                           and [IsDelete]=0 
                           and [IsReal]=1                           
                           " + condtion + @"
                        ORDER BY [ObjName] asc,[Name] asc
                        Limit " + endRowIndex.ToString() + @"
                    )
                    ORDER BY [ObjName] desc,[Name] DESC
                    Limit " + realSize.ToString() + @"  
                )
                ORDER BY [ObjName] asc,[Name] asc;
                ";

            }
            else
            {
                sql = @"select count([ID]) from Person where 
                        [IsPublic]=1  and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";

                {
                    maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);
               //     listener.settotalcurrent(total);
                }

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
                           [Position],[IsVisit],[Url] 
                        FROM Person
                        WHERE 
                           [IsPublic]=1 
                           and [AddrType]='工作单位及职务' 
                           and [IsDelete]=0 
                           and [IsReal]=1                           
                           " + condtion + @"
                        ORDER BY [ObjName] asc,[Name] asc
                    )
                    ORDER BY [ObjName] DESC,[Name] DESC
                )
                ORDER BY [ObjName] asc,[Name] asc;
                ";*/


                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT  
                           [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] 
                        FROM Person
                        WHERE 
                           [IsPublic]=1 
                           and [AddrType]='工作单位及职务' 
                           and [IsDelete]=0 
                           and [IsReal]=1                           
                           " + condtion + @"
                        ORDER BY [ObjName] asc,[Name] asc
                        Limit " + endRowIndex.ToString() + @"
                    )
                    ORDER BY [ObjName] DESC,[Name] DESC
                    Limit " + realSize.ToString() + @"
                )
                ORDER BY [ObjName] asc,[Name] asc;
                ";

            }


            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                if(ds.Tables.Count == 0) return null;
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
                //     mItemCheckEdit.CheckedChanged -= ItemCheckEdit_CheckedChanged;
                //     mItemCheckEdit.CheckedChanged += ItemCheckEdit_CheckedChanged;


            }
            else
            {
             
                return null;
            }
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
            if (mCityid != -1)
            {
                list.Add(new DictionaryEntry("@cityid", id));

                sql = @"select Count([ID]) from Person where [CityID] = @cityid 
                           and [IsPublic]=1 and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    listener.settotalcurrent(total);
                }



                sql = @"select [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] from Person where [CityID] = @cityid 
                           and [IsPublic]=1 and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1 order by [ObjName],[Name] asc";
                
            }
            else
            {
                sql = @"select count([ID]) from Person where 
                        [IsPublic]=1  and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1";

                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    listener.settotalcurrent(total);
                }

                sql = @"select [ID],[Name],[IDCard],[No],[ObjName],[Title],
                           [Political],[Nation],[Marriage],
                           [PhoneNum],[AddrType],[Address],[Remark],[PoliceAddr],[IsPublic],
                           [Position],[IsVisit],[Url] from Person where 
                           [IsPublic]=1  and [AddrType]='工作单位及职务' and [IsDelete]=0 and [IsReal]=1 order by [ObjName],[Name] asc";
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

                    if (listener != null)
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
                //     mItemCheckEdit.CheckedChanged -= ItemCheckEdit_CheckedChanged;
                //     mItemCheckEdit.CheckedChanged += ItemCheckEdit_CheckedChanged;


            }
            else
            {
                if (listener != null)
                {
                    listener.progressend();
                }
                return null;
            }
        }
        public void reload()
        {
            mTresslistControl.reLoad();
        }
        public void EndCallback(object obj)
        {
            if (obj != null)
            {
                mGridControl.RefreshDataSource();
                //  mGridControl.DataSource = obj;
                mButtonEdit.ButtonClick -= ButtonEdit_ButtonClick;
                mButtonEdit.ButtonClick += ButtonEdit_ButtonClick;

                mOpenButton.ButtonClick -= OpenButton_ButtonClick;
                mOpenButton.ButtonClick += OpenButton_ButtonClick;
            }
        }


        public void OnTreeListClick(int cityid)
        {
         //   mGridControl.DataSource = null;
            mCityid= cityid;
            SingleProgressBar progress = new SingleProgressBar();
            progress.setShowCallback(ShowCallback);
            progress.setEndCallback(EndCallback);
            progress.showwithoutwindows();

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
        private void ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            PersonInfo rowData = (PersonInfo)mGridView.GetRow(handle);

            SelectPrintForm selectPrintForm = new SelectPrintForm();
            if(selectPrintForm.ShowDialog()==DialogResult.OK)
            {
                string template = selectPrintForm.getTemplate();
                if (template.Length > 0)
                {
                    PrintForm frm = new PrintForm(template);
                    frm.setPerson(rowData);
                  //  frm.TemplatePath = template;
                    frm.ShowDialog();
                }
            }
            //打印发函
            //   PrintForm frm = new PrintForm();
            //  frm.setPerson(rowData);
            // frm.ShowDialog();

        }

        private void mGridView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            var name1 = mGridView.GetRowCellValue(e.RowHandle1, "Name").ToString();
            var name2 = mGridView.GetRowCellValue(e.RowHandle2, "Name").ToString();


            if (e.Column.FieldName != "IsVisit")
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

        private void EmailControl_Enter(object sender, EventArgs e)
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
