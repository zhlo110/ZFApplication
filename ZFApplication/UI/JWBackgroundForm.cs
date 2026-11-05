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
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;
using ZFApplication.Database;
using ZFApplication.Uitility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace ZFApplication.UI
{
    public partial class JWBackgroundForm : DevExpress.XtraEditors.XtraForm
    {
        public JWBackgroundForm()
        {
            InitializeComponent();
        }

        private void BrowerButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mTextEdit.Text = dialog.SelectedPath;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //判断目录存在
            if (System.IO.Directory.Exists(mTextEdit.Text))
            {
                if (mStuType.Text.Length > 0)
                {
                    Export(mTextEdit.Text, mStuType.Text);
                }
                else
                {
                    XtraMessageBox.Show("请选择招生类别");
                }
            }
            else
            {
                XtraMessageBox.Show("目录不存在，请检查");
            }

        }
        public void ReplaceNameInDocument(RichEditControl richEditControl, string sign, string newName)
        {

            // 获取文档的 API 接口
            Document document = richEditControl.Document;
            // 开始文档更新
            document.BeginUpdate();
            try
            {
                // 创建一个搜索参数对象
                ISearchResult searchResult = document.StartSearch(sign, SearchOptions.None, SearchDirection.Forward, document.Range);

                // 遍历文档中的所有匹配项并替换它们
                while (searchResult.FindNext())
                {
                    document.Replace(searchResult.CurrentResult, newName);
                }
            }
            finally
            {
                // 结束文档更新
                document.EndUpdate();
            }
        }

        public static string GetBirthDateStringFromIdCard(string idCard)
        {
            if (string.IsNullOrWhiteSpace(idCard))
                return idCard;

            try
            {
                DateTime birthDate;

                if (idCard.Length == 18)
                {
                    string birth = idCard.Substring(6, 8); // YYYYMMDD
                    birthDate = DateTime.ParseExact(birth, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (idCard.Length == 15)
                {
                    string birth = idCard.Substring(6, 6); // YYMMDD
                    birthDate = DateTime.ParseExact(birth, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    return idCard;
                }

                return birthDate.ToString("yyyy年MM月dd日");
            }
            catch
            {
                return idCard;
            }
        }

        private void Export(string path, string type)
        {
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            //导出文件
            //string path = mTextEdit.Text;
            //string type = mStuType.Text;
            //string fileName = System.IO.Path.Combine(path, type + ".xlsx");
            //if (System.IO.File.Exists(fileName))
            //{
            //    System.IO.File.Delete(fileName);
            //}
            //System.IO.File.Create(fileName);
            //名称，身份证号，出生日期（从身份证号里面得到），政治面貌、籍贯(从户籍地址里面得到)，
            //文化程度（type）
            //，性别(从sex和称谓中得到)，民族，户籍地址、手机号码，

            string sql = @"select [Name],[ObjName],[IDCard],[Political],[Sex],[CityID],
                           [Title],[Nation],[Address],[PhoneNum] from Person 
                           where [AddrType]=@add and [IsReal]=1 and [IsDelete]=0
                           Order by [No] asc";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@add", "户口所在地"));
            //list.Add(new DictionaryEntry("@type", type));
            DataSet ds = AccessDB.Query(mdbfiles, sql, list);
            string template = "./template/jwbjsc.docx";
            DateTime now = DateTime.Now;
            int foldercount = 50;
            if (ds != null)
            {
                
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        mProgressBar.Properties.Maximum = dt.Rows.Count;
                        mProgressBar.Properties.Minimum = 0;
                        mProgressBar.EditValue = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mProgressBar.EditValue = i;
                            Application.DoEvents();
                            string name = DatabaseUtility.getStringValue(dt.Rows[i], "Name");
                            string objname = DatabaseUtility.getStringValue(dt.Rows[i], "ObjName");
                            string idcard = DatabaseUtility.getStringValue(dt.Rows[i], "IDCard");
                            string political = DatabaseUtility.getStringValue(dt.Rows[i], "Political");
                            string sex = DatabaseUtility.getStringValue(dt.Rows[i], "Sex");
                            string title = DatabaseUtility.getStringValue(dt.Rows[i], "Title");
                            string nation = DatabaseUtility.getStringValue(dt.Rows[i], "Nation");
                            string address = DatabaseUtility.getStringValue(dt.Rows[i], "Address");
                            string phonenum = DatabaseUtility.getStringValue(dt.Rows[i], "PhoneNum");
                            int cityid = DatabaseUtility.getIntValue(dt.Rows[i], "CityID", -1);

                            //从身份证号中得到出生日期
                            string birthday = GetBirthDateStringFromIdCard(idcard);
                            string cityname = "";
                            if (cityid > 0)
                            {
                                cityname = ExportIDNameExcelUtility.getRealAddress(mdbfiles, cityid);
                            }
                         //   ExportIDNameExcelUtility
                            mRichEditControl.LoadDocument(template);

                            string sexx = TitleOrderUtility.GetGenderFromTitle(title);
                            if(sexx == "未知")
                                sexx = sex;
                            //替换模板中的内容
                            ReplaceNameInDocument(mRichEditControl, "@name", name);
                            ReplaceNameInDocument(mRichEditControl, "@idcard", idcard);
                            ReplaceNameInDocument(mRichEditControl, "@birthday", birthday);
                            ReplaceNameInDocument(mRichEditControl, "@political", political);
                            ReplaceNameInDocument(mRichEditControl, "@addr", cityname);
                            ReplaceNameInDocument(mRichEditControl, "@school", type);
                            ReplaceNameInDocument(mRichEditControl, "@sex", sexx);
                            ReplaceNameInDocument(mRichEditControl, "@nation", nation);
                            ReplaceNameInDocument(mRichEditControl, "@hj_address", address);
                            ReplaceNameInDocument(mRichEditControl, "@phone", phonenum);

                            ReplaceNameInDocument(mRichEditControl, "@year", now.Year.ToString());
                            ReplaceNameInDocument(mRichEditControl, "@month", now.Month.ToString());
                            ReplaceNameInDocument(mRichEditControl, "@day", now.Day.ToString());

                           // DateTime birthday = GetBirthDateFromIdCard(idcard);


                            int count = (i / foldercount)+1;
                            string foldername = System.IO.Path.Combine(path, count.ToString());
                            int No = i % foldercount + 1;
                            string[] namevec = name.Split(new[] { "\r\n" }, StringSplitOptions.None);
                            string[] objvec  = objname.Split(new[] { "\r\n" }, StringSplitOptions.None);

                            //保存文件
                            string filename = System.IO.Path.Combine(foldername, No.ToString()+"-"+ objvec[0] + "-"+ namevec[0] + ".doc");
                            if (!System.IO.Directory.Exists(foldername))
                            {
                                System.IO.Directory.CreateDirectory(foldername);
                            }
                            mRichEditControl.SaveDocument(filename, DocumentFormat.Dotx);
                            //string fileName = System.IO.Path.Combine(path, name + ".docx");
                        }
                        this.DialogResult = DialogResult.OK;
                       // XtraMessageBox.Show("导出成功");
                    }
                    else
                    {
                        XtraMessageBox.Show("没有符合条件的数据");
                    }
                }
            }


        }
    }
}