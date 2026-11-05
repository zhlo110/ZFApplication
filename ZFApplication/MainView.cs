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
using DevExpress.Mvvm.Native;
using DevExpress.Utils.CommonDialogs;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using OfficeOpenXml;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.UI;
using ZFApplication.Uitility;
using ZFApplication.UserControl;
using static ZFApplication.UI.SingleProgressBar;

namespace ZFApplication
{
    public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //  XtraUserControl employeesUserControl;
        //  XtraUserControl customersUserControl;

        OriTableControl mOriTableControl = new OriTableControl();
        SplitTableControl mSplitTableControl = new SplitTableControl();
        EmailControl mEmailControl = new EmailControl();
        StatisticBar mStatisticBar = new StatisticBar();
        StatisticTableControl mStatisticTableControl = new StatisticTableControl();
        DeleteControl mDeleteControl = new DeleteControl();
        public MainView()
        {
            InitializeComponent();
            //      return;
            string baseurl = AppDomain.CurrentDomain.BaseDirectory;
            LogUtility.init(baseurl);
            LogUtility.Log("--------------------------------------------------------------------");
            LogUtility.Log("空军招飞局成都选拔中心-政审系统-程序开始");

            mOriTableControl.Name = "mOriTableControl";
            mOriTableControl.Text = "考生表";

            mSplitTableControl.Name = "mSplitTableControl";
            mSplitTableControl.Text = "线索表";

            mEmailControl.Name = "mEmailControl";
            mEmailControl.Text = "发函表";

            mStatisticBar.Name = "mStatisticBar";
            mStatisticBar.Text = "统计表";

            mStatisticTableControl.Name = "mStatisticTableControl";
            mStatisticTableControl.Text = "统计图";

            mDeleteControl.Name = "mDeleteControl";
            mDeleteControl.Text = "删除表";

            //return;




            PathUtility.setcurrentpath();
            Database.AccessDB.createCiyDb("./ds/City.mdb");
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            updatetitle();


            //   CityUtility.ParseCity("四川省攀枝花市盐边县红果乡红果村2组146号");
            //      
        }
        public void updatetitle()
        {
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            string workspace = SystemUtility.GetSystemValues("WorkplaceFolder");
            if (!File.Exists(mdbfiles) || !Directory.Exists(workspace))
            {
                this.Text = "空军招飞局成都选拔中心-政审系统";
            }
            else
            {
                this.Text = "空军招飞局成都选拔中心-政审系统(文件路径：" + mdbfiles + ")";
            }

            if (Directory.Exists(workspace))
            {
                this.Text += ",(工作空间：" + workspace + ")";
            }
        }

        void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null) return;
            switch (e.Element.Text)
            {
                case "考生表":
                    tabbedView.AddDocument(mOriTableControl);
                    tabbedView.ActivateDocument(mOriTableControl);
                    break;
                case "线索表":

                    tabbedView.AddDocument(mSplitTableControl);
                    tabbedView.ActivateDocument(mSplitTableControl);
                    break;
                case "发函表":

                    tabbedView.AddDocument(mEmailControl);
                    tabbedView.ActivateDocument(mEmailControl);
                    break;
                case "统计表":
                    tabbedView.AddDocument(mStatisticBar);
                    tabbedView.ActivateDocument(mStatisticBar);
                    break;
                case "统计图":
                    tabbedView.AddDocument(mStatisticTableControl);
                    tabbedView.ActivateDocument(mStatisticTableControl);
                    break;
                case "删除表":
                    tabbedView.AddDocument(mDeleteControl);
                    tabbedView.ActivateDocument(mDeleteControl);
                    break;
            }


            //   
            //   XtraUserControl userControl = e.Element.Text == "Employees" ? employeesUserControl : customersUserControl;
            //  tabbedView.AddDocument(userControl);
            //   tabbedView.ActivateDocument(userControl);
        }
        void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            RecreateUserControls(e);
            SetAccordionSelectedElement();
        }
        void SetAccordionSelectedElement()
        {
            if (tabbedView.Documents.Count != 0)
            {
                switch (tabbedView.Documents[0].Caption)
                {
                    case "考生表":
                        accordionControl.SelectedElement = OriTableItem;
                        break;
                    case "线索表":
                        accordionControl.SelectedElement = SplitTableItem;
                        break;
                    case "发函表":
                        accordionControl.SelectedElement = EmailTableItem;
                        break;
                    case "统计表":
                        accordionControl.SelectedElement = StatisticBarItem;
                        break;
                    case "统计图":
                        accordionControl.SelectedElement = StatisticTableItem;
                        break;
                }

                /*
                if (e.Document.Caption == "Employees") 
                    accordionControl.SelectedElement = customersAccordionControlElement;
                else 
                    accordionControl.SelectedElement = employeesAccordionControlElement;*/
            }
            else
            {
                accordionControl.SelectedElement = null;
            }
        }


        void SelectAccordionSelectedElement(string text)
        {
            if (tabbedView.Documents.Count != 0)
            {
                switch (text)
                {
                    case "考生表":
                        accordionControl.SelectedElement = OriTableItem;
                        break;
                    case "线索表":
                        accordionControl.SelectedElement = SplitTableItem;
                        break;
                    case "发函表":
                        accordionControl.SelectedElement = EmailTableItem;
                        break;
                    case "统计表":
                        accordionControl.SelectedElement = StatisticBarItem;
                        break;
                    case "统计图":
                        accordionControl.SelectedElement = StatisticTableItem;
                        break;
                }

                /*
                if (e.Document.Caption == "Employees") 
                    accordionControl.SelectedElement = customersAccordionControlElement;
                else 
                    accordionControl.SelectedElement = employeesAccordionControlElement;*/
            }
            else
            {
                accordionControl.SelectedElement = null;
            }
        }
        void RecreateUserControls(DocumentEventArgs e)
        {

            switch (e.Document.Caption)
            {
                case "考生表":
                    mOriTableControl = new OriTableControl();
                    mOriTableControl.Name = "mOriTableControl";
                    mOriTableControl.Text = "考生表";
                    break;
                case "线索表":
                    mSplitTableControl = new SplitTableControl();
                    mSplitTableControl.Name = "mSplitTableControl";
                    mSplitTableControl.Text = "线索表";
                    break;
                case "发函表":
                    mEmailControl = new EmailControl();
                    mEmailControl.Name = "mEmailControl";
                    mEmailControl.Text = "发函表";
                    break;
                case "统计表":
                    mStatisticBar = new StatisticBar();
                    mStatisticBar.Name = "mStatisticBar";
                    mStatisticBar.Text = "统计表";
                    break;
                case "统计图":
                    mStatisticTableControl = new StatisticTableControl();
                    mStatisticTableControl.Name = "mStatisticTableControl";
                    mStatisticTableControl.Text = "统计图";
                    break;
                case "删除表":
                    mDeleteControl = new DeleteControl();
                    mDeleteControl.Name = "mDeleteControl";
                    mDeleteControl.Text = "删除表";
                    break;
            }



            //    if (e.Document.Caption == "Employees") employeesUserControl = CreateUserControl("Employees");
            //     else customersUserControl = CreateUserControl("Customers");
        }

        private void tabbedView_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {

            e.Control = mOriTableControl;
            //     e.Control = new OriTable();
        }

        private void dockPanel_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            e.Cancel = true;
            // mNavPanel.

        }

        private void OriButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mOriTableControl);
            tabbedView.ActivateDocument(mOriTableControl);
            SelectAccordionSelectedElement("考生表");

        }

        private void SplitButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mSplitTableControl);
            tabbedView.ActivateDocument(mSplitTableControl);
            SelectAccordionSelectedElement("线索表");
        }

        private void EmailButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mEmailControl);
            tabbedView.ActivateDocument(mEmailControl);
            SelectAccordionSelectedElement("发函表");
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mStatisticBar);
            tabbedView.ActivateDocument(mStatisticBar);
            SelectAccordionSelectedElement("统计表");
        }

        private void DeleteBarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mDeleteControl);
            tabbedView.ActivateDocument(mDeleteControl);
            SelectAccordionSelectedElement("删除表");
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.AddDocument(mStatisticTableControl);
            tabbedView.ActivateDocument(mStatisticTableControl);
            SelectAccordionSelectedElement("统计图");
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogUtility.Log("空军招飞局成都选拔中心-政审系统-程序结束");
            LogUtility.Log("--------------------------------------------------------------------");
        }

        private void NewProjectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            LogUtility.Log("点击新建项目");
            NewProjectForm form = new NewProjectForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                PathUtility.setcurrentpath();
                LogUtility.Log("新建项目验证正确");

                LogUtility.Log("拷贝新的数据库到指定的文件中");
                if (AccessDB.copyfile("./ds/empty.mdb", form.getProjectName()))
                {
                    LogUtility.Log("生成表格");
                    AccessDB.gentable(form.getProjectName());

                    LogUtility.Log("将文件写入到系统数据库中");
                    SystemUtility.writecurrentfile(form.getProjectName(), "FilePath");
                    SystemUtility.writecurrentfile(form.getFolderName(), "WorkplaceFolder");

                    LogUtility.Log("读Excel并导入数据");

                    //要有进度条
                    ProgressBarForm progressform = new ProgressBarForm(form.getProjectName(), form.getFolderName());
                    progressform.setShowCallback(this.ShowCallback);
                    if (progressform.ShowDialog() == DialogResult.OK)
                    {
                        reload();

                        //      mOriTableControl.reload();
                        //      mEmailControl.reload();
                        //      mSplitTableControl.reload();
                    }
                    //     ExcelReader.readerfolder(form.getFolderName());

                }
            }
        }
        public void reload()
        {
            BaseDocument doc = tabbedView.ActiveDocument;
            // 判断doc 的类型是不是OriTableControl
            if (doc.Control is OriTableControl)
            {
                // OriTableControl control = (OriTableControl)doc;
                mOriTableControl.reload();
            }
            else if (doc.Control is SplitTableControl)
            {
                //  SplitTableControl control = (SplitTableControl)doc;
                mSplitTableControl.reload();
            }
            else if (doc.Control is EmailControl)
            {
                //  EmailControl control = (EmailControl)doc;
                mEmailControl.reload();
            }
            updatetitle();
        }
        IList mNeedlist = new ArrayList();
        public void ShowCallback(string folder, string mdbfile,
            ProgressBarListeners listener)
        {
            ExcelReader.readerfolder(folder, mdbfile, mNeedlist, listener);
        }

        public void UpdateCallback(string folder, string mdbfile,
            ProgressBarListeners listener)
        {
            ExcelReader.updatefolder(folder, mdbfile, mNeedlist, listener);
        }

        private void BarUpdate_ItemClickImp()
        {

            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            string folder = SystemUtility.GetSystemValues("WorkplaceFolder");
            if (File.Exists(mdbfiles) && Directory.Exists(folder))
            {
                // string time = SystemUtility.GetSystemTime("FilePath");
                ProgressBarForm progressform = new ProgressBarForm(mdbfiles, folder);
                progressform.setShowCallback(this.UpdateCallback);
                if (progressform.ShowDialog() == DialogResult.OK)
                {
                    reload();
                }
            }
        }

        private void BarUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarUpdate_ItemClickImp();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            tabbedView.AddDocument(mOriTableControl);
            tabbedView.ActivateDocument(mOriTableControl);
            mNeedlist.Clear();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            string need = SystemUtility.needupdate(mdbfiles);
            if (need != null && need.Length > 0)
            {
                need = need.Trim();
                if (need.Equals("true"))
                {
                    string content = CityUtility.getupdatelist();
                    if (content.Length > 0)
                    {
                        string sql = "select distinct [Url] from Person where [CityID] in (" + content + ")";
                        DataSet ds = AccessDB.Query(mdbfiles, sql, null);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                DataTable dt = ds.Tables[0];
                                foreach (DataRow row in dt.Rows)
                                {
                                    string url = DatabaseUtility.getStringValue(row, "Url");
                                    mNeedlist.Add(url);
                                }
                                BarUpdate_ItemClickImp();
                            }
                        }
                    }

                    SystemUtility.MustUpdate(mdbfiles, "false");

                }
            }
            mNeedlist.Clear();

        }


        string mSplitFileName = "";
        public void SplitBeginCallback(string folder, string mdbfile, ProgressBarListeners listener)
        {
            SplitUtility.split(mSplitFileName, listener);
            // return true;
            //return StatisticUtility.Statistic(listener);
        }

        public void SplitEndCallback(object obj)
        {
            mSplitFileName = "";
        }



        private void SaveAsButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfile))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "mdb文件|*.mdb";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(mdbfile, saveFileDialog.FileName);
                    //
                    // SystemUtility.writecurrentfile();
                }
            }
            else
            {
                MessageBox.Show("另存的文件不存在");
            }
        }

        private void OpenProjectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "mdb文件|*.mdb";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SystemUtility.writecurrentfile(dlg.FileName, "FilePath");
                SystemUtility.updatedatabaseurl();
                reload();
            }
        }

        private void FromFolderButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = folderBrowserDialog.SelectedPath;
                SystemUtility.writecurrentfile(filename, "WorkplaceFolder");
                SystemUtility.updatedatabaseurl();
                reload();

            }
        }


        string mOutputExcelName = "";
        public object OutputExcelCallback(SingleBarInterface listener)
        {
            ExportIDNameExcelUtility.export(mOutputExcelName, listener);
            return true;
            //return StatisticUtility.Statistic(listener);
        }

        public void OutputExcelEndCallback(object obj)
        {
            mOutputExcelName = "";
        }

        private void OutputExcelButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.Filter = "Excel Files|*xlsx|All Files|*.*";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                string filename = savefiledialog.FileName;
                mOutputExcelName = filename + ".xlsx";
                SingleProgressBar progressbar = new SingleProgressBar();
                progressbar.setShowCallback(OutputExcelCallback);
                progressbar.setEndCallback(OutputExcelEndCallback);
                progressbar.ShowDialog();
            }
        }

        private void SplitbarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            // SplitUtility.split("D:\\招飞系统\\folder\\");

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = folderBrowserDialog.SelectedPath;
                //   string filename = "E:\\招飞系统\\Police\\";
                mSplitFileName = filename;
                ProgressBarForm progressbar = new ProgressBarForm("", "");
                //  SingleProgressBar progressbar = new SingleProgressBar();
                progressbar.setShowCallback(SplitBeginCallback);
                //   progressbar.setEndCallback(SplitEndCallback);
                progressbar.ShowDialog();

            }
        }

        string mSendEmailFolderName = "";


        public void SendEmailEndCallback(object obj)
        {
            mSendEmailFolderName = "";
        }

        private void StatisticTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            //导出发函表，导出到一个文件夹里面
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = folderBrowserDialog.SelectedPath;
                mSendEmailFolderName = filename;
                ProgressBarForm progressbar = new ProgressBarForm("", "");
                progressbar.setShowCallback(SendEmailBeginCallback);
                progressbar.ShowDialog();

            }
        }
        public void StaticEndCallback(object obj)
        {

        }
        public object StaticShowCallback(SingleBarInterface listener)
        {
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            StatisticUtility.ExportStatistic(mdbfiles, mStatisticFileName, listener);
            return null;
        }

        string mStatisticFileName = "";

        private void TableNumBut_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                string filename = savefiledialog.FileName;
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                string destpath = "./template/police-statistic.xlsx";
                if (!File.Exists(filename))
                {
                    if (File.Exists(destpath))
                    {
                        File.Copy(destpath, filename);
                    }
                }
                mStatisticFileName = filename;
                SingleProgressBar progress = new SingleProgressBar();
                progress.setShowCallback(StaticShowCallback);
                progress.setEndCallback(StaticEndCallback);
                progress.ShowDialog();
                //按照派出所统计
                //      string mdbfiles = SystemUtility.GetSystemValues("FilePath");
                //     StatisticUtility.ExportStatistic(mdbfiles, filename, "aa");

            }
        }

        private void CityButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            CityForm cityForm = new CityForm();
            cityForm.ShowDialog();
        }

        string mMergeOri = "";
        string mMergeDes = "";
        public void MergeBeginCallback(string folder, string mdbfile, ProgressBarListeners listener)
        {
            if (mMergeDes.Length > 0 && mMergeOri.Length > 0)
            {
                MergeUtility.merge(mMergeDes, mMergeOri, listener);
            }
            //     SplitUtility.split(mSplitFileName, listener);
            // return true;
            //return StatisticUtility.Statistic(listener);
        }

        private void ProjectMerge_ItemClick(object sender, ItemClickEventArgs e)
        {
            //项目合格
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mdb文件|*.mdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PathUtility.setcurrentpath();
                mMergeOri = ofd.FileName;
                mMergeDes = SystemUtility.GetSystemValues("FilePath");
                ProgressBarForm progressbar = new ProgressBarForm("", "");
                progressbar.setShowCallback(MergeBeginCallback);
                progressbar.ShowDialog();
            }
        }

        string mOutputExExcelName = "";
        public object OutputEXExcelCallback(SingleBarInterface listener)
        {
            ExportIDNameExcelUtility.exportEX(mOutputExExcelName, listener);
            return true;
            //return StatisticUtility.Statistic(listener);
        }

        public void OutputEXExcelEndCallback(object obj)
        {
            mOutputExExcelName = "";
        }

        private void OutputCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.Filter = "Excel Files|*xlsx|All Files|*.*";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                string filename = savefiledialog.FileName;
                mOutputExExcelName = filename + ".xlsx";
                SingleProgressBar progressbar = new SingleProgressBar();
                progressbar.setShowCallback(OutputEXExcelCallback);
                progressbar.setEndCallback(OutputEXExcelEndCallback);
                progressbar.ShowDialog();
            }
        }

        private void JNetExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            //军网背景审查
            JWBackgroundForm form = new JWBackgroundForm();
            form.ShowDialog();
        }

        public void SendEmailBeginCallback(string folder, string mdbfile, ProgressBarListeners listener)
        {
            SplitUtility.splitEmail(mSendEmailFolderName, listener);
            // return true;
            //return StatisticUtility.Statistic(listener);
        }

        string mNewNetFolderName = "";

        public void NetSplitCallback(string folder, string mdbfile, ProgressBarListeners listener)
        {
            NetSplitExcelUtility.split(mNewNetFolderName, listener);
            // return true;
            //return StatisticUtility.Statistic(listener);
        }
        private void JNetExport_New_ItemClick(object sender, ItemClickEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = folderBrowserDialog.SelectedPath;
                mNewNetFolderName = filename;
                ProgressBarForm progressbar = new ProgressBarForm("", "");
                progressbar.setShowCallback(NetSplitCallback);
                progressbar.ShowDialog();

            }
        }

        private void PicReName_ItemClick(object sender, ItemClickEventArgs e)
        {
            PicReName form = new PicReName();
            if (form.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private async void FamilyButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            PathUtility.setcurrentpath();
            string databasePath = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(databasePath))
            {
                XtraMessageBox.Show(this, "请先打开有效的招飞项目。", "导出家访表格");
                return;
            }

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择家访表格的保存文件夹（每个省份一个 Excel 文件）";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                string outputFolder = dialog.SelectedPath;
                int year = DateTime.Now.Year;
                FamilyButtonItem.Enabled = false;
                UseWaitCursor = true;
                try
                {
                    FamilyVisitExportResult result = await Task.Run(() =>
                    {
                        const string personSql = @"select [No],[ObjName],[Name],[Title],[AddrType],
                            [Address],[CityID],[PhoneNum],[IsReal],[IsDelete] from Person
                            where [IsDelete]=0 and [IsReal]=1
                            and [Title] in ('本人','父亲','母亲','继父','继母') order by [ID]";
                        using (DataSet people = AccessDB.Query(databasePath, personSql, null))
                        using (DataSet cities = AccessDB.Query(databasePath,
                            "select [ID],[ParentID],[Name],[ShortName] from City", null))
                        {
                            if (people == null || people.Tables.Count == 0 || cities == null || cities.Tables.Count == 0)
                                throw new InvalidOperationException("无法读取线索表或行政区数据，请检查项目数据库。");
                            return FamilyVisitExportUtility.Export(people.Tables[0], cities.Tables[0], outputFolder, year);
                        }
                    });

                    string message = result.Files.Count == 0
                        ? "没有可导出的家访数据。"
                        : "家访表格导出完成，共 " + result.Files.Count + " 个省份、" + result.StudentCount
                            + " 名招飞对象。\r\n保存位置：" + outputFolder;
                    if (result.Warnings.Count > 0)
                    {
                        foreach (string warning in result.Warnings)
                            LogUtility.Log("家访导出：" + warning);
                        message += "\r\n\r\n请核对以下数据（共 " + result.Warnings.Count + " 项）：\r\n"
                            + string.Join("\r\n", result.Warnings.Take(8));
                        if (result.Warnings.Count > 8)
                            message += "\r\n其余问题已记录到程序日志。";
                    }
                    XtraMessageBox.Show(this, message, "导出家访表格", MessageBoxButtons.OK,
                        result.Warnings.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                catch (Exception exception)
                {
                    LogUtility.Log("家访表格导出失败：" + exception);
                    XtraMessageBox.Show(this, "家访表格导出失败：" + exception.Message
                        + "\r\n部分省份可能已导出，请检查保存目录：" + outputFolder,
                        "导出家访表格", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    UseWaitCursor = false;
                    FamilyButtonItem.Enabled = true;
                }
            }
        }
    }
}
