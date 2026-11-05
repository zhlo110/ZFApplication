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
using DevExpress.XtraEditors;
using ZFApplication.Database;
using ZFApplication.Model;
using ZFApplication.Uitility;

namespace ZFApplication.UI
{
    public partial class ErrorForm : DevExpress.XtraEditors.XtraForm
    {
        public ErrorForm(StudentInfo info, string mdbfile)
        {
            InitializeComponent();
            mStudentInfo= info;
            this.mdbfile = mdbfile;
        }

     

        StudentInfo mStudentInfo = null;
        string mdbfile = "";
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            mErrorText.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            mErrorText.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;

            if (mStudentInfo == null) return;

            string sql = "SELECT [Description] FROM Problem WHERE StudentNo =@no and StudentName=@name";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@no", mStudentInfo.No));
            list1.Add(new DictionaryEntry("@name", mStudentInfo.Name));

            DataSet ds = AccessDB.Query(mdbfile,sql, list1);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                { 
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        mErrorText.Text =DatabaseUtility.getStringValue(dr, "Description");
                    }
                }
            }

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (mStudentInfo == null) return;
            if (File.Exists(mStudentInfo.Url))
            {
                try
                {
                    // 指定.xlsx文件的完整路径
                    //    string filePath = @"C:\path\to\your\file.xlsx";

                    // 使用默认程序打开.xlsx文件，通常是Excel
                    Process.Start(new ProcessStartInfo(mStudentInfo.Url) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"无法打开文件: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("数据文件路径更改了，之前的路径为‘" + mStudentInfo.Url + "'");
                LogUtility.Log("数据文件路径更改了，之前的路径为‘" + mStudentInfo.Url + "'");
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (mStudentInfo == null) return;
            string sql = "delete FROM Problem WHERE StudentNo =@no and StudentName=@name";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@no", mStudentInfo.No));
            list1.Add(new DictionaryEntry("@name", mStudentInfo.Name));
            AccessDB.NoQuery(mdbfile, sql, list1);

            sql = "update Person set HasError=0 where [No] =@no and [Name]=@name and [Title]=@title";
            list1.Clear();
            list1.Add(new DictionaryEntry("@no", mStudentInfo.No));
            list1.Add(new DictionaryEntry("@name", mStudentInfo.Name));
            list1.Add(new DictionaryEntry("@title", "本人"));
            AccessDB.NoQuery(mdbfile, sql, list1);

            mStudentInfo.HasError = 0;
        }

        private void IgnoreButton_Click(object sender, EventArgs e)
        {
            if (mStudentInfo == null) return;

            string sql = "update Person set HasError=2 where [No] =@no and [Name]=@name and [Title]=@title";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@no", mStudentInfo.No));
            list1.Add(new DictionaryEntry("@name", mStudentInfo.Name));
            list1.Add(new DictionaryEntry("@title","本人"));
            AccessDB.NoQuery(mdbfile, sql, list1);


            mStudentInfo.HasError = 2;
        }
    }
}