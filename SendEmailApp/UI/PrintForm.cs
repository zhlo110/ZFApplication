using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;
using SendEmailApp.Utility;
using SendEmailApp.model;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using Document = DevExpress.XtraRichEdit.API.Native.Document;
using System.Drawing.Printing;

namespace SendEmailApp.UI
{
    public partial class PrintForm : DevExpress.XtraEditors.XtraForm
    {
        public PrintForm(string template)
        {
            mTemplatePath = template;
            InitializeComponent();
            SetA4PageSize(mRichEditControl);
            PathUtility.setcurrentpath();
            RichEditControlCompatibility.EnableDirectWriteMeasurement = false;

            //string aas = mRichEditControl.Text;

            mRichEditControl.LoadDocument(mTemplatePath);
         //   mRichEditControl.SaveDocument(mTemplatePath, DocumentFormat.WordML);

        }
        public void ReplaceNameInDocument(RichEditControl richEditControl,string sign,string newName)
        {
            try
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
            catch(Exception ex)
            {
                // 处理异常
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        PersonClue mPersonInfo = null;

        public string getFullName(string pname)
        {
            string name = "";
            string[] vec = pname.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (vec.Length == 2)
            {
                name = vec[0] + "(曾用名:" + vec[1] + ")";
            }
            else
            {
                name = pname;
            }
            return name;
        }

        public void setPerson(PersonClue info)
        {
            mPersonInfo = info;


            if (mPersonInfo != null)
            {
                
                string name = getFullName(mPersonInfo.Name);

                string[] vec = mPersonInfo.Address.Split(new[] { "\r\n" }, StringSplitOptions.None);
                string position = "";
                string company = "";
                if(vec.Length == 2)
                {
                    //工作单位: 航空工业自控所
                    //职务:退休职工
                    company = vec[0].Substring(5);
                //    position = vec[1].Substring(3);
                }
                else
                {
                    company = mPersonInfo.Address;
                }

                position = getFullName(mPersonInfo.Objname);

                ReplaceNameInDocument(mRichEditControl, "@company", company);
                ReplaceNameInDocument(mRichEditControl, "@position", name);
                ReplaceNameInDocument(mRichEditControl, "@name", position);
                ReplaceNameInDocument(mRichEditControl, "@number", "1");
                DateTime now = DateTime.Now;
                //+7天的时间
                DateTime dateTime= now.AddDays(7);
                ReplaceNameInDocument(mRichEditControl, "@month", dateTime.Month.ToString());
                ReplaceNameInDocument(mRichEditControl, "@day", dateTime.Day.ToString());


                ReplaceNameInDocument(mRichEditControl, "@nowy", now.Year.ToString());
                ReplaceNameInDocument(mRichEditControl, "@mo", now.Month.ToString("D2"));
                ReplaceNameInDocument(mRichEditControl, "@dy", now.Day.ToString("D2"));
            }
        }

        public void SetA4PageSize(RichEditControl richEdit)
        {
            // 确保传入的控件已经初始化
            if (richEdit == null)
                throw new ArgumentNullException("richEdit");

            // 获取文档对象



            Document document = richEdit.Document;

            // 开始更新文档，以防止在设置过程中进行渲染
            document.BeginUpdate();
            try
            {
                // 获取文档的第一个节（Section）
                Section firstSection = document.Sections[0];

                // 设置纸张大小为 A4
                firstSection.Page.Width = Units.MillimetersToDocumentsF(210);
                firstSection.Page.Height = Units.MillimetersToDocumentsF(297);

                // 设置边距
                firstSection.Margins.Left = Units.MillimetersToDocumentsF(20);   // 20mm 左边距
                firstSection.Margins.Right = Units.MillimetersToDocumentsF(20);  // 20mm 右边距
                firstSection.Margins.Top = Units.MillimetersToDocumentsF(20);    // 20mm 上边距
                firstSection.Margins.Bottom = Units.MillimetersToDocumentsF(20); // 20mm 下边距
            }
            finally
            {
                // 完成更新
                document.EndUpdate();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            PrintRichEditContent(mRichEditControl);
        }

        public void PrintRichEditContent(RichEditControl richEditControl)
        {
            // 确保 RichEditControl 实例已经被初始化
            if (richEditControl == null)
            {

            }
           //     LogUtility.Log("打印错误");
            // 创建打印系统和组件链接
            //richEditControl.Print();
            ConfigureAndPrint(richEditControl);


        }

        public void ConfigureAndPrint(RichEditControl richEditControl)
        {
            // 创建 PrintDocument 对象
            PrintDialog printDialog = new PrintDialog();
            printDialog.UseEXDialog = true;        // 对于较新的 Windows 版本，使用扩展对话框

            // 显示打印设置对话框
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // 用户点击打印后，使用用户选择的打印机设置来打印
                richEditControl.Print(printDialog.PrinterSettings);
            }
        }

        private void mRichEditControl_Click(object sender, EventArgs e)
        {

        }

        string mTemplatePath = "./template/mail.doc";
        public string TemplatePath
        {
            get { return mTemplatePath; }
            set { mTemplatePath = value; }
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}