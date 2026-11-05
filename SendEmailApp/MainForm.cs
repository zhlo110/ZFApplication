using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Controls;
using SendEmailApp.model;
using SendEmailApp.UI;
using SendEmailApp.Utility;

namespace SendEmailApp
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void workspace_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            workspace.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            e.Cancel = true;
        }

        private  async void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (SystemUtility.IsFileLocked(e.FullPath))
            {
                await Task.Delay(2000); // 延迟 2 秒
            }

            // 延时处理文件
            if (e.FullPath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !Path.GetFileName(e.FullPath).StartsWith("~$", StringComparison.OrdinalIgnoreCase))
            {
                this.loadfile(CheckNewFiles.loadonefilefromworkspace());
            }

            
            //  Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");
        }

        // 文件被重命名时触发
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            Task.Run(async () =>
            {
                if (SystemUtility.IsFileLocked(e.FullPath))
                {
                    await Task.Delay(2000); // 延迟 2 秒
                }
                if (e.FullPath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !Path.GetFileName(e.FullPath).StartsWith("~$", StringComparison.OrdinalIgnoreCase))
                {
                    this.loadfile(CheckNewFiles.loadonefilefromworkspace());
                }
            });
        }

        public void loadfile(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return;
            }
           // mFileSystemWatcher.Path = SystemUtility.workspace;
           // mFileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
          //  mFileSystemWatcher.Filter = "*.xlsx";
        //    mFileSystemWatcher.Changed += OnFileChanged;
         //   mFileSystemWatcher.Created += OnFileChanged;
          //  mFileSystemWatcher.Deleted += OnFileChanged;
           // mFileSystemWatcher.Renamed += OnFileRenamed;
           this.currentfile.EditValue = filepath;

            IList list = ExcelReader.readexcel(filepath);

            Console.WriteLine("list count:" + list.Count);
            if (list.Count > 0)
            {
                mGridControl.DataSource = list;

                mButtonEdit.ButtonClick -= ButtonEdit_ButtonClick;
                mButtonEdit.ButtonClick += ButtonEdit_ButtonClick;

                mOpenButtonEdit.ButtonClick -= OpenButton_ButtonClick;
                mOpenButtonEdit.ButtonClick += OpenButton_ButtonClick;
            }
            else
            {
                mGridControl.DataSource = null;
            }
        }

        private void onLoad(object sender, EventArgs e)
        {
           // CheckNewFiles.setMainForm(this);
          //  loadfile("E:\\招飞系统\\定选全部\\C12-王乐頔-重庆市九龙坡区-A.xlsx");

            string value = SystemUtility.ReadValueFromFile("workspace.txt", "WorkSpace");
            string value1 = SystemUtility.ReadValueFromFile("workspace.txt", "Areas");
            if (value == null)
            {
                value = "";
            }
            if (value1 == null)
            {
                value1 = "";
            }
            this.Text = "政审发函平台-("+value+")";
            SystemUtility.areas = value1.Split(new char[] { ',', '，' });
            SystemUtility.workspace = value;
            //从工作区间中选择一个文件,修改日期比较晚的
            this.loadfile(CheckNewFiles.loadonefilefromworkspace());
            

        }

        

        private void OpenButton_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            PersonClue rowData = (PersonClue)mGridView.GetRow(handle);
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
                    ///LogUtility.Log("数据文件路径更改了，之前的路径为‘" + rowData.Url + "'");
                }
            }
        }

        private void ButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var handle = mGridView.FocusedRowHandle;
            PersonClue rowData = (PersonClue)mGridView.GetRow(handle);

            SelectPrintForm selectPrintForm = new SelectPrintForm();
            if (selectPrintForm.ShowDialog() == DialogResult.OK)
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
        }

        private void SetWorkSpace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //选择工作区间
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                mGridControl.DataSource = null;
                PathUtility.setcurrentpath();
                string folderPath = folderBrowserDialog.SelectedPath+ "\\";
                SystemUtility.UpdateFolderPathInFile("workspace.txt", "WorkSpace", folderPath);
                SystemUtility.workspace = folderPath;
                this.Text = "政审发函平台-(" + folderPath + ")";

                this.loadfile(CheckNewFiles.loadonefilefromworkspace());

                //将folderPath写入配置文件
                //ConfigurationManager.AppSettings["WorkSpace"] = folderPath;
                //Console.WriteLine("Selected folder: " + folderPath);
                //将folderPath写入txt文件
                //File.WriteAllText("workspace.txt", folderPath);
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExceptForm exceptForm = new ExceptForm();

            if(exceptForm.ShowDialog()==DialogResult.OK)
            {
                this.loadfile(CheckNewFiles.loadonefilefromworkspace());
            }
        }

        private void SelectFiles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SelectFilesForm selectFilesForm = new SelectFilesForm();
            if (selectFilesForm.ShowDialog() == DialogResult.OK)
            {
                FilesList select = selectFilesForm.getSelectFile();
                if(select != null)
                {
                    CheckNewFiles.currentfile = select.getFilePath();
                    loadfile(select.getFilePath());
                }
            }
        }

        private void mTimer_Tick(object sender, EventArgs e)
        {
            this.loadfile(CheckNewFiles.loadonefilefromworkspace());
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.loadfile(CheckNewFiles.loadonefilefromworkspace());
        }

        public void setCurrentfile(string file)
        {
            currentfile.EditValue = file;
        }

        private void OpenCurrentFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (File.Exists(CheckNewFiles.currentfile))
            {
                try
                {
                    // 指定.xlsx文件的完整路径
                    //    string filePath = @"C:\path\to\your\file.xlsx";

                    // 使用默认程序打开.xlsx文件，通常是Excel
                    Process.Start(new ProcessStartInfo(CheckNewFiles.currentfile) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法打开文件: {ex.Message}");
                }
            }
        }
    }
}
