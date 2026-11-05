using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ZFApplication.Database;
using ZFApplication.Uitility;

namespace ZFApplication.UI
{
    public partial class PicReName : DevExpress.XtraEditors.XtraForm
    {
        public PicReName()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string originalPath = mOriPath.Text;
            string newPath = mDestPath.Text;

            // 检查路径是否存在

            if (originalPath != newPath)
            {
                if (System.IO.Directory.Exists(originalPath)
                    && System.IO.Directory.Exists(newPath))
                {
                    // 获取所有图片文件
                    string[] files = System.IO.Directory.GetFiles(originalPath, "*.*");
                    foreach (string file in files)
                    {
                        if (file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                        {
                            // 获取文件名
                            string fileName = System.IO.Path.GetFileName(file);
                            string name = System.IO.Path.GetFileNameWithoutExtension(fileName);

                            //获取身份证号
                            string sql = "select [IDCard] from Person where [IsDelete]=0 and [IsReal]=1 and  Title = '本人' and [Name] Like '%" + name + "%'";
                            string mbdfiles = SystemUtility.GetSystemValues("FilePath");

                            DataSet ds = AccessDB.Query(mbdfiles, sql, null);
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    DataTable dt = ds.Tables[0];
                                    if (dt.Rows.Count > 0)
                                    {
                                        DataRow row = dt.Rows[0];
                                        string idcard = DatabaseUtility.getStringValue(row, "IDCard");
                                        if (idcard.Length == 18 || idcard.Length == 15)
                                        {
                                            // 重命名文件
                                            string newFileName = System.IO.Path.Combine(newPath, idcard + System.IO.Path.GetExtension(fileName));
                                            if (!System.IO.File.Exists(newFileName))
                                            {
                                                // 复制文件到新路径
                                                System.IO.File.Copy(file, newFileName);
                                            }
                                        }
                                    }
                                }


                                // 生成新的文件名
                                //string newFileName = System.IO.Path.Combine(newPath, fileName);
                                // 重命名文件
                                //System.IO.File.Move(file, newFileName);
                            }
                        }

                    }
                }
            }
            this.DialogResult = DialogResult.OK;

        }
    }
}