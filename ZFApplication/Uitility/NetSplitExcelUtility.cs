using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ClipboardSource.SpreadsheetML;
using OfficeOpenXml;
using ZFApplication.Database;
using ZFApplication.Listeners;

namespace ZFApplication.Uitility
{
    public class NetSplitExcelUtility
    {
        public static int mcurrentNumber = 0;
        public static void export_excel(string foldername,string no,string name,string idcard)
        {
            if(Directory.Exists(foldername))
            {
                //string template = SystemUtility.GetSystemValues("ExcelTemplate");
                string newfilename = foldername + "\\" + no + "_" + SystemUtility.trimstring(name) + ".xlsx";
                string template = "./template/net-student.xlsx";
                if (File.Exists(template))
                {
                    try
                    {
                        File.Copy(template, newfilename, true);
                        FileInfo fileInfo = new FileInfo(newfilename);
                        if (!fileInfo.Exists)
                        {
                            LogUtility.Log("文件" + newfilename + "不存在");
                        }
                        using (ExcelPackage package = new ExcelPackage(fileInfo))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            string sql = "select [IDCard],[Name],[Title],[PhoneNum],[Address],[Position] from Person where [IsDelete]=0 and [IsReal]=1 and  AddrType = '工作单位及职务' and No=@no order by OrderNo asc";
                            IList list = new ArrayList();
                            list.Add(new DictionaryEntry("@no", no));
                            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
                            DataSet ds = AccessDB.Query(mdbfiles, sql, list);
                            int line = 2; //从第二行开始写入数据
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    DataTable dt = ds.Tables[0];
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        DataRow row = dr;
                                        string personidcard = DatabaseUtility.getStringValue(row, "IDCard");

                                        if (personidcard.Length != 18 && personidcard.Length != 15)
                                        {
                                            continue;
                                        }

                                        string personname = DatabaseUtility.getStringValue(row, "Name");
                                        string title = DatabaseUtility.getStringValue(row, "Title");
                                     //   string political = DatabaseUtility.getStringValue(row, "Political");
                                        string phone = DatabaseUtility.getStringValue(row, "PhoneNum");
                                        string address = DatabaseUtility.getStringValue(row, "Address");
                                        string position = DatabaseUtility.getStringValue(row, "Position");
                                        string stridcard = idcard; // 考生身份证

                                        if(title == "本人")
                                        {
                                            position = "学生";
                                            stridcard = "";
                                        }

                                        worksheet.Cells["A" + line.ToString()].Value = line-1; // 序号
                                        worksheet.Cells["B" + line.ToString()].Value = personname; // No
                                        worksheet.Cells["C" + line.ToString()].Value = personidcard; // 身份证号
                                        worksheet.Cells["D" + line.ToString()].Value = phone; // 手机号
                                        worksheet.Cells["E" + line.ToString()].Value = address; // 工作单位
                                        worksheet.Cells["F" + line.ToString()].Value = position; // 职务
                                        worksheet.Cells["G" + line.ToString()].Value = TitleOrderUtility.NormalizeRelation(title); // 关系
                                        worksheet.Cells["H" + line.ToString()].Value = stridcard; // 考生身份证

                                        // 设置边框
                                        using (var range = worksheet.Cells["A" + line + ":H" + line])
                                        {
                                            range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                            range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                            range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                            range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                        }

                                        package.Save();
                                        line++;

                                    }
                                }


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Error copying file: " + ex.Message);
                    }
                }
                else
                {
                  //  Console.WriteLine("Template file not found: " + template);
                }
            }
        }
        public static void split(string foldername, ProgressBarListeners listener)
        {
            PathUtility.setcurrentpath();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfiles))
            {
                string sql = "select count(ID) from Person where [IsDelete]=0 and [IsReal]=1 and [Title]='本人' and  AddrType = '工作单位及职务'";
                int cnt = AccessDB.QueryStatistic(mdbfiles, sql, null);
                if (cnt > 0)
                {
                    //本人由于有3条
                    listener.settotalnumber(cnt);
                }
                else
                {
                    listener.settotalnumber(0);
                    return;
                }
                sql = "select [No],[IDCard],[Name] from Person where [IsDelete]=0 and [IsReal]=1 and [Title]='本人' and  AddrType = '工作单位及职务'";
                DataSet ds = AccessDB.Query(mdbfiles, sql, null);
                Hashtable hash = new Hashtable();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        int No = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string no = DatabaseUtility.getStringValue(dr, "No");
                            string name = DatabaseUtility.getStringValue(dr, "Name");
                            string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                            //判断是不是有效身份证
                            
                            if (no.Length == 0 || name.Length == 0)
                            {
                                continue;
                            }
                            export_excel(foldername, no, name, idcard);

                            listener.settotalcurrent(mcurrentNumber, name);
                            mcurrentNumber++;
                        }
                    }
                }
            }
            listener.progressend();
        }
    }
}
