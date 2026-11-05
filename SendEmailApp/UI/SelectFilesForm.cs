using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using SendEmailApp.model;
using SendEmailApp.Utility;

namespace SendEmailApp.UI
{
    public partial class SelectFilesForm : DevExpress.XtraEditors.XtraForm
    {
        public SelectFilesForm()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            loadall();
        }

        public void loadall()
        {
            mListBox.Items.Clear();
            string workspace = SystemUtility.workspace;
            if(string.IsNullOrEmpty(workspace) || !Directory.Exists(workspace))
            {
                return;
            }
            //搜索文件夹下的所有xlsx文件
            string[] files = System.IO.Directory.GetFiles(workspace, "*.xlsx", System.IO.SearchOption.AllDirectories);
            //将文件名添加到列表框中
            foreach (string file in files)
            {
                //将file写到列表的Tag中
                string fileName = System.IO.Path.GetFileName(file);

                FilesList filesList = new FilesList(fileName, file);
                mListBox.Items.Add(filesList);
            }
        }

        public void search(string text)
        {
            mListBox.Items.Clear();
            string workspace = SystemUtility.workspace;
            if (string.IsNullOrEmpty(workspace) || !Directory.Exists(workspace))
            {
                return;
            }
            //搜索文件夹下的所有xlsx文件
            string[] files = System.IO.Directory.GetFiles(workspace, "*.xlsx", System.IO.SearchOption.AllDirectories);
            //将文件名添加到列表框中
            foreach (string file in files)
            {
                //将file写到列表的Tag中
                string fileName = System.IO.Path.GetFileName(file);
                if (fileName.Contains(text))
                {
                    FilesList filesList = new FilesList(fileName, file);
                    mListBox.Items.Add(filesList);
                }
            }
        }

        private void onTextChange(object sender, EventArgs e)
        {
            //获取文本框中的文本
            string searchText = mSearchText.Text.Trim();
            if (searchText.Length > 0)
            {
                search(searchText);
            }
            else
            {
                loadall();
            }
        }

        FilesList mSelectFile = null;
        public FilesList getSelectFile()
        {
            return mSelectFile;
        }
        private void mListBox_DoubleClick(object sender, EventArgs e)
        {
            //获取选中的文件
            FilesList filesList = (FilesList)mListBox.SelectedItem;
            if (filesList != null)
            {
                mSelectFile = filesList;
                //打开文件
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}