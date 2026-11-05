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
using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.Model;
using ZFApplication.UI;
using ZFApplication.Uitility;
using static DevExpress.Utils.Diagnostics.GUIResources;

namespace ZFApplication.UserControl
{
    public partial class OriTableControl : DevExpress.XtraEditors.XtraUserControl, TreeListClickListener
    {

        private int batchSize = 20; // 每次加载的数据量
        private int maxRowCount = 0; // 总数据量

        public OriTableControl()
        {
            
            InitializeComponent();
            IList namelist = new ArrayList();
            IList idlist = new ArrayList();
            namelist.Add("姓名");
            namelist.Add("身份证号");
            namelist.Add("政治面貌");
            namelist.Add("民族");
            namelist.Add("本人电话");
            namelist.Add("就读学校");
            namelist.Add("班级");
            namelist.Add("父母电话");
            namelist.Add("是否错误");
            idlist.Add("Name");
            idlist.Add("IDCard");
            idlist.Add("Political");
            idlist.Add("Nation");
            idlist.Add("Phonenumber");
            idlist.Add("Address");
            idlist.Add("Grade");
            idlist.Add("Remark");
            idlist.Add("HasError");
            mSearchControl.setList(namelist, idlist);

            mSearchControl.setGridView(mGridView);
            //
            mGridControl.DataSource = mVirtualSource;
            /*
            var criteria = CriteriaOperator.Parse("Name like '%高%' AND IDCard = '1212'");
            mGridView.ActiveFilterCriteria = criteria;*/
            mTresslistControl.setTreeListClickListener(this);
        }

        int mCityid = -1;

        public object ShowCallback(SingleBarInterface listener)
        {

            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            string workspace = SystemUtility.GetSystemValues("WorkplaceFolder");
            if (!File.Exists(mdbfile) || !Directory.Exists(workspace))
            {
            
                return null;
            }
            string sql = "";
            IList list = new ArrayList();
            if (mCityid != -1)//本市的
            {
                list.Add(new DictionaryEntry("@id", id));
                sql = @"select count([ID])
                            from Person where [CityID]=@id 
                            and Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete]=0";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    if (listener != null)
                    {
                        maxRowCount = total;

                        //listener.settotalnumber(total);
                    }
                }
            }
            else //全国的
            {
                sql = @"select count([ID]) from Person where Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 and [IsReal]=1";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    if (listener != null)
                    {
                        maxRowCount = total;
                        // listener.settotalnumber(total);
                    }
                }
            }
            
            return true;
        }


        /*
        public object ShowCallback(SingleBarInterface listener)
        {

            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            string workspace = SystemUtility.GetSystemValues("WorkplaceFolder");
            if (!File.Exists(mdbfile) || !Directory.Exists(workspace))
            {
                if(listener != null)
                {
                    listener.progressend();
                }
                
                return null;
            }
            string sql = "";
            IList list = new ArrayList();
            if (mCityid != -1)//本市的
            {
                list.Add(new DictionaryEntry("@id", id));
                sql = @"select count([ID])
                            from Person where [CityID]=@id 
                            and Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete]=0";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    if (listener != null)
                    {
                        listener.settotalnumber(total);
                    }
                }


                 sql = @"select [No],[Name] as 姓名,[IDCard] as 身份证号,
                           [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                           [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                           [Position] as 职务,[Url],[HasError]
                            from Person where [CityID]=@id 
                            and Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 order by [Name]";
                
            }
            else //全国的
            {
                sql = @"select count([ID]) from Person where Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 and [IsReal]=1";
                if (listener != null)
                {
                    int total = AccessDB.QueryStatistic(mdbfile, sql, list);
                    if (listener != null)
                    {
                        listener.settotalnumber(total);
                    }
                }
                sql = @"select [No],[Name] as 姓名,[IDCard] as 身份证号,
                           [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                           [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                           [Position] as 职务,[Url],[HasError]
                            from Person where Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 and [IsReal]=1 order by [Name]";
            }

            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                //IList<StudentInfo> list1= new IList<StudentInfo>();
                BindingList<StudentInfo> list1 = new BindingList<StudentInfo>();
                //   BindingSource bs = new BindingSource();
                int nCount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    StudentInfo info = new StudentInfo();
                    info.Name = DatabaseUtility.getStringValue(dr, "姓名");
                    info.IDCard = DatabaseUtility.getStringValue(dr, "身份证号");
                    info.Phonenumber = DatabaseUtility.getStringValue(dr, "本人电话");
                    info.Sex = DatabaseUtility.getStringValue(dr, "性别");
                    info.Political = DatabaseUtility.getStringValue(dr, "政治面貌");
                    info.Nation = DatabaseUtility.getStringValue(dr, "民族");
                    info.Address = DatabaseUtility.getStringValue(dr, "就读学校和班级");
                    info.Remark = DatabaseUtility.getStringValue(dr, "父母电话");
                    info.Grade = DatabaseUtility.getStringValue(dr, "职务");
                    info.Url = DatabaseUtility.getStringValue(dr, "Url");
                    info.No = DatabaseUtility.getStringValue(dr, "No");
                    info.HasError = DatabaseUtility.getIntValue(dr, "HasError", 0);
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

            }
            else
            {
                if (listener != null)
                {
                    listener.progressend();
                }
                return null;
            }
        }*/
        public void EndCallback(object obj)
        {
            if (obj != null)
            {
                mGridControl.RefreshDataSource();
                //mGridControl.DataSource = obj;
                mOperaionButton.ButtonClick -= OperaionButton_ButtonClick;
                mOperaionButton.ButtonClick += OperaionButton_ButtonClick;
                DeleteItemButtonEdit.ButtonClick -= DeleteButton_ButtonClick;
                DeleteItemButtonEdit.ButtonClick += DeleteButton_ButtonClick;
            }
        }
         public void OnTreeListClick(int cityid)
        {
// mGridControl.DataSource = null;
            mCityid= cityid;

            SingleProgressBar progress = new SingleProgressBar();
            progress.setShowCallback(ShowCallback);
            progress.setEndCallback(EndCallback);
            progress.showwithoutwindows();
        }

        public void reload()
        {
            mTresslistControl.reLoad();
        }
        private void DeleteButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            StudentInfo rowData = (StudentInfo)mGridView.GetRow(handle);
            if (MessageBox.Show("删除所有与该考生关联的线索，确定要删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string mdbfile = SystemUtility.GetSystemValues("FilePath");
                if (!File.Exists(mdbfile))
                {
                    return;
                }
                string sql = @"update Person set [IsDelete]=1 where [No]=@No";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@No", rowData.No));
                AccessDB.NoQuery(mdbfile, sql, list);
// mGridControl.DataSource = null;
                SingleProgressBar progress = new SingleProgressBar();
                progress.setShowCallback(ShowCallback);
                progress.setEndCallback(EndCallback);
                progress.showwithoutwindows();
            }
        }
        private void OperaionButton_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            StudentInfo rowData = (StudentInfo)mGridView.GetRow(handle);
            if(File.Exists(rowData.Url))
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
                MessageBox.Show("数据文件路径更改了，之前的路径为‘"+ rowData.Url + "'");
                LogUtility.Log("数据文件路径更改了，之前的路径为‘" + rowData.Url + "'");
            }
           // MessageBox.Show("Button clicked! Row data: " + rowData.Url);
            //   throw new NotImplementedException();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void OriTableControl_Enter(object sender, EventArgs e)
        {
            OnTreeListClick(mCityid);
        }

        private void mGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                StudentInfo rowData = (StudentInfo)mGridView.GetRow(e.RowHandle);
                // 获取当前行的某个字段值
               
                if (rowData.HasError == 1)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
                else if (rowData.HasError == 2)
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.ForeColor = Color.White;
                }
                
            }

        }

        private void mGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void mGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = mGridView;

            if (view != null && view.FocusedRowHandle >= 0)
            {
                // 获取当前选择的行数据对象
                StudentInfo rowData = (StudentInfo)view.GetFocusedRow();
                if (rowData.HasError > 0)
                {

                    //有错误才能打开对话框
                    string mdbfile = SystemUtility.GetSystemValues("FilePath");
                    ErrorForm form = new ErrorForm(rowData, mdbfile);
                    form.ShowDialog();
                }

                //   StudentInfo rowData = (StudentInfo)mGridView.GetRow(e.RowHandle);
                // 获取某个列的值（例如 "Name" 列）
                // var haserror = view.GetFocusedRowCellValue("HasError");


            }
        }

        private void mGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "序号") // 替换为你实际的列名
            {
                e.DisplayText = (e.RowHandle + 1).ToString(); // 行号从 1 开始
            }
        }

        private void mVirtualSource_ConfigurationChanged(object sender, DevExpress.Data.VirtualServerModeRowsEventArgs e)
        {
            BindingList<StudentInfo> students = GetRows(0);
            
            e.UserData = students;
        }

        private BindingList<StudentInfo> GetRows(int startRowIndex)
        {

            int id = mCityid;
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            string workspace = SystemUtility.GetSystemValues("WorkplaceFolder");
            string sql = "";
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
            if (mCityid != -1)//本市的
            {
                list.Add(new DictionaryEntry("@id", id));

                /* 没有分页
                sql = @"select [No],[Name] as 姓名,[IDCard] as 身份证号,
                           [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                           [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                           [Position] as 职务,[Url],[HasError]
                            from Person where [CityID]=@id 
                            and Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 order by [Name]";
                */
                //if(condtion.Length > 0)
                {
                    sql = @"select count([ID])
                            from Person where [CityID]=@id 
                            and Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete]=0"+condtion;
                    maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);
                }


                //需要分页
                int endRowIndex = 0;
                int realSize = batchSize;
                SystemUtility.calpageresult(maxRowCount, startRowIndex, out endRowIndex, out realSize);
                Console.WriteLine(realSize.ToString() + "," + endRowIndex.ToString());
                /*
                sql = @"
                SELECT * FROM (
                    SELECT TOP " + realSize.ToString() + @" * FROM (
                        SELECT TOP " + endRowIndex.ToString() + @" 
                        [No],[Name] as 姓名,[IDCard] as 身份证号,
                        [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                        [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                        [Position] as 职务,[Url],[HasError]
                        FROM Person
                        WHERE [CityID] = @id
                          AND Title = '本人'
                          AND [AddrType] = '工作单位及职务'
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [Name]
                    )
                    ORDER BY [姓名] DESC
                )
                ORDER BY [姓名];
                ";*/

                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT  
                        [No],[Name] as 姓名,[IDCard] as 身份证号,
                        [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                        [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                        [Position] as 职务,[Url],[HasError]
                        FROM Person
                        WHERE [CityID] = @id
                          AND Title = '本人'
                          AND [AddrType] = '工作单位及职务'
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [Name]
                        LIMIT " + endRowIndex.ToString() + @"
                    )
                    ORDER BY [姓名] DESC
                    LIMIT " + realSize.ToString() + @"
                )
                ORDER BY [姓名];
                ";


            }
            else //全国的
            {
                /* 没有分页
                sql = @"select [No],[Name] as 姓名,[IDCard] as 身份证号,
                           [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                           [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                           [Position] as 职务,[Url],[HasError]
                            from Person where Title = '本人' and [AddrType]='工作单位及职务' and [IsDelete] = 0 and [IsReal]=1 order by [Name]";

                */

               // if (condtion.Length > 0)
                {
                    sql = @"select count([ID])
                            from Person where Title = '本人' and [AddrType]='工作单位及职务' AND [IsReal]=1 and [IsDelete]=0" + condtion;
                    maxRowCount = AccessDB.QueryStatistic(mdbfile, sql, list);
                }

                //需要分页

                int endRowIndex = 0;
                int realSize = batchSize;
                SystemUtility.calpageresult(maxRowCount, startRowIndex, out endRowIndex, out realSize);
                Console.WriteLine(realSize.ToString() + "," + endRowIndex.ToString());
                /*
                sql = @"
                SELECT * FROM (
                    SELECT TOP " + realSize.ToString() + @" * FROM (
                        SELECT TOP " + endRowIndex.ToString() + @" 
                        [No],[Name] as 姓名,[IDCard] as 身份证号,
                        [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                        [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                        [Position] as 职务,[Url],[HasError]
                        FROM Person
                        WHERE
                          Title = '本人'
                          AND [AddrType] = '工作单位及职务'
                          AND [IsReal]=1
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [Name]
                    )
                    ORDER BY [姓名] DESC
                )
                ORDER BY [姓名];
                ";*/


                sql = @"
                SELECT * FROM (
                    SELECT * FROM (
                        SELECT  
                        [No],[Name] as 姓名,[IDCard] as 身份证号,
                        [Sex] as 性别,[Political] as 政治面貌,[Nation] as 民族,
                        [PhoneNum] as 本人电话,[Address] as 就读学校和班级,[Remark] as 父母电话,
                        [Position] as 职务,[Url],[HasError]
                        FROM Person
                        WHERE
                          Title = '本人'
                          AND [AddrType] = '工作单位及职务'
                          AND [IsReal]=1
                          AND [IsDelete] = 0 " + condtion + @"
                        ORDER BY [Name]
                        LIMIT "+endRowIndex.ToString()+ @"
                    )
                    ORDER BY [姓名] DESC
                    LIMIT "+realSize.ToString()+@"
                )
                ORDER BY [姓名];
                ";
            }

            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    //IList<StudentInfo> list1= new IList<StudentInfo>();
                    BindingList<StudentInfo> list1 = new BindingList<StudentInfo>();
                    //   BindingSource bs = new BindingSource();
                    int nCount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentInfo info = new StudentInfo();
                        info.Name = DatabaseUtility.getStringValue(dr, "姓名");
                        info.IDCard = DatabaseUtility.getStringValue(dr, "身份证号");
                        info.Phonenumber = DatabaseUtility.getStringValue(dr, "本人电话");
                        info.Sex = DatabaseUtility.getStringValue(dr, "性别");
                        info.Political = DatabaseUtility.getStringValue(dr, "政治面貌");
                        info.Nation = DatabaseUtility.getStringValue(dr, "民族");
                        info.Address = DatabaseUtility.getStringValue(dr, "就读学校和班级");
                        info.Remark = DatabaseUtility.getStringValue(dr, "父母电话");
                        info.Grade = DatabaseUtility.getStringValue(dr, "职务");
                        info.Url = DatabaseUtility.getStringValue(dr, "Url");
                        info.No = DatabaseUtility.getStringValue(dr, "No");
                        info.HasError = DatabaseUtility.getIntValue(dr, "HasError", 0);
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
