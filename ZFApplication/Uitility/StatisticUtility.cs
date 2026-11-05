using DevExpress.CodeParser;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.Utils;
using DevExpress.Utils.About;
using DevExpress.Utils.NonclientArea;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Forms;
using DevExpress.XtraRichEdit.Unicode;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFApplication.Database;
using ZFApplication.Listeners;
using ZFApplication.Model;

namespace ZFApplication.Uitility
{
    public class StatisticUtility
    {

        public static void LogError(string mdbfile,System.IO.FileInfo fileInfo)
        {
            string sql = "";
            IList list = new ArrayList();
            //本人
            sql = "select [No],[ObjName],[Title],[AddrType],[Address] from Person where [CityID]=-1 and [IsDelete]=0 and [IsReal]=1 and [OrderNo]<= 0";
            LogErrorImp(mdbfile,fileInfo, sql, list,1);

            //直系
            sql = "select [No],[ObjName],[Title],[AddrType],[Address] from Person where [CityID]=-1 and [IsDelete]=0 and [IsReal]=1 and [OrderNo]>= 1 and [OrderNo]<= 16";
            LogErrorImp(mdbfile,fileInfo, sql, list, 2);

            //旁系
            sql = "select [No],[ObjName],[Title],[AddrType],[Address] from Person where [CityID]=-1 and [IsDelete]=0 and [IsReal]=1 and [OrderNo]>= 17";
            LogErrorImp(mdbfile,fileInfo, sql, list, 3);

        }

        public static void LogErrorImp(string mdbfile, System.IO.FileInfo fileInfo, string sql, IList list, int excelNo)
        {
            DataSet ds = AccessDB.Query(mdbfile, sql, null);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                int current = 0;    
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[excelNo];
                   
                    foreach (DataRow row in dt.Rows)
                    {
                        string no = DatabaseUtility.getStringValue(row, "No");
                        string objname = DatabaseUtility.getStringValue(row, "ObjName");
                        string title = DatabaseUtility.getStringValue(row, "Title");
                        string addrtype = DatabaseUtility.getStringValue(row, "AddrType");
                        string addr = DatabaseUtility.getStringValue(row, "Address");

                        if (excelNo == 1)
                        {
                            worksheet.Cells["A" + (current + 2).ToString()].Value = no;
                            worksheet.Cells["B" + (current + 2).ToString()].Value = objname;
                            //   worksheet.Cells["B" + (current + 2).ToString()].Value = policeaddr;
                            worksheet.Cells["C" + (current + 2).ToString()].Value = addrtype;
                            worksheet.Cells["D" + (current + 2).ToString()].Value = addr;
                            // worksheet.Cells["E" + (current + 2).ToString()].Value = info.RelationCount;
                            string line = (current + 2).ToString();
                            using (var range = worksheet.Cells["A" + line + ":D" + line])
                            {
                                range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            }
                        }
                        else
                        {
                            worksheet.Cells["A" + (current + 2).ToString()].Value = no;
                            worksheet.Cells["B" + (current + 2).ToString()].Value = objname;
                            //   worksheet.Cells["B" + (current + 2).ToString()].Value = policeaddr;
                            worksheet.Cells["C" + (current + 2).ToString()].Value = title;
                            worksheet.Cells["D" + (current + 2).ToString()].Value = addrtype;
                            worksheet.Cells["E" + (current + 2).ToString()].Value = addr;
                            string line = (current + 2).ToString();
                            using (var range = worksheet.Cells["A" + line + ":E" + line])
                            {
                                range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            }
                        }
                        current += 1;   


                    }
                    package.Save();
                }
            }
        }

        //导出派出所统计数据，mdbfile是数据库文件，filename是导出
        // 按本人、旁系、直系导出
        public static void ExportStatistic(string mdbfile, string filename, SingleBarInterface listener)
        {
            string sql = "select count(*) from (select distinct [CityID] from Person where [IsDelete]=0 and [IsReal]=1)";
            int cnt = AccessDB.QueryStatistic(mdbfile, sql, null);
            int current = 0;
            listener.settotalcurrent(current);
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

            sql = "select distinct [CityID] from Person where [IsDelete]=0 and [IsReal]=1 order by [CityID]";
            DataSet ds = AccessDB.Query(mdbfile, sql, null);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                if (!fileInfo.Exists)
                {
                    LogUtility.Log("文件" + filename + "不存在");
                }

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    ArrayList provincelist = new ArrayList();
                    foreach (DataRow row in dt.Rows)
                    {
                        
                        int cityid = DatabaseUtility.getIntValue(row, "CityID", -1);
                        //本人
                        sql = "select count([No]) from (select distinct [No] from Person where [CityID]=@city and [IsDelete]=0 and [IsReal]=1 and [OrderNo]<= 0)";

                        IList list = new ArrayList();
                       // list.Add(new DictionaryEntry("@addr", policeaddr));
                        list.Add(new DictionaryEntry("@city", cityid));
                        int personcount = AccessDB.QueryStatistic(mdbfile, sql, list);
                        //旁系
                        sql = "select count([No]) from (select distinct [No] from Person where [CityID]=@city and [IsDelete]=0 and [IsReal]=1 and [OrderNo]>= 17)";
                        int relationcount = AccessDB.QueryStatistic(mdbfile, sql, list);
                        //直系
                        sql = "select count(*) from (select distinct [No],[Title] from Person where [CityID]=@city and [IsDelete]=0 and [IsReal]=1 and [OrderNo]>= 1 and [OrderNo]<= 16)";
                        int directcount = AccessDB.QueryStatistic(mdbfile, sql, list);

                        string cityname = CityUtility.GetCityName(mdbfile, cityid);
                        string province = CityUtility.GetProvinceName(mdbfile, cityid);
                        string region = CityUtility.GetSecondLevelName(mdbfile, cityid);
                        ProvinceInfo provinceInfo = new ProvinceInfo();
                        provinceInfo.CityName = cityname;
                        provinceInfo.PersonCount = personcount.ToString();
                        provinceInfo.DirectCount = directcount.ToString();
                        provinceInfo.RelationCount = relationcount.ToString();
                        provinceInfo.Province = province;
                        provinceInfo.Region = region;
                        provincelist.Add(provinceInfo);

                        current += 1;
                        listener.settotalcurrent(current);

                        /*
                        worksheet.Cells["A" + (current + 2).ToString()].Value = province;
                        worksheet.Cells["B" + (current + 2).ToString()].Value = cityname;
                     //   worksheet.Cells["B" + (current + 2).ToString()].Value = policeaddr;
                        worksheet.Cells["C" + (current + 2).ToString()].Value = personcount.ToString();
                        worksheet.Cells["D" + (current + 2).ToString()].Value = directcount.ToString();
                        worksheet.Cells["E" + (current + 2).ToString()].Value = relationcount.ToString();
                        
                        string line = (current + 2).ToString();
                        using (var range = worksheet.Cells["A" + line + ":E" + line])
                        {
                            range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }

                        current += 1;
                        listener.settotalcurrent(current);
                        //导出
                        // string[] data = new string[] { cityname, policeaddr, personcount.ToString(), relationcount.ToString(), directcount.ToString() };
                        //  FileUtility.WriteFile(filename, data);
                        */
                    }
                    current = 0;
                    //按省份、城市对数据进行排序
                    provincelist.Sort(new ProvinceInfoComparer());

                    foreach (ProvinceInfo info in provincelist)
                    {
                        worksheet.Cells["A" + (current + 2).ToString()].Value = info.Province;
                        worksheet.Cells["B" + (current + 2).ToString()].Value = info.region;
                        if (info.region == info.CityName)
                        {
                            worksheet.Cells["C" + (current + 2).ToString()].Value = "-";
                        }
                        else
                        {
                            worksheet.Cells["C" + (current + 2).ToString()].Value = info.CityName;
                        }
                        //   worksheet.Cells["B" + (current + 2).ToString()].Value = policeaddr;
                        worksheet.Cells["D" + (current + 2).ToString()].Value = info.PersonCount;
                        worksheet.Cells["E" + (current + 2).ToString()].Value = info.DirectCount;
                        worksheet.Cells["F" + (current + 2).ToString()].Value = info.RelationCount;
                        string line = (current + 2).ToString();
                        using (var range = worksheet.Cells["A" + line + ":F" + line])
                        {
                            range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }
                        current += 1;
                        listener.settotalcurrent(current);
                    }

                    package.Save();
                }

                LogError(mdbfile, fileInfo);


            }

           
            listener.progressend();
        }

        public static Hashtable Statistic(SingleBarInterface listener)
        {
            Hashtable namelist = new Hashtable();
            //   Console.WriteLine("统计功能正在开发中...");
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            if (!File.Exists(mdbfile))
            {
                listener.progressend();
                return namelist;
            }
            string sql = "";
            if (listener != null)
            {
                sql = "select count(ID) from City";
                int citycount = AccessDB.QueryStatistic(mdbfile, sql, null);
                listener.settotalnumber(citycount+1);
            }

            int total = 0;
            sql = "select count(ID) from Person where [IsDelete]=0 and [IsReal] = 1";
            StatisticInfo info= new StatisticInfo();
            int count = AccessDB.QueryStatistic(mdbfile, sql,null);
            info.Name = "全国";
            info.Number = count;
            total += 1;
            if (listener != null)
            {
                listener.settotalcurrent(total);
            }

            //IsVisit列累加,sum(Visit) as Visit
            int visitcount = 0;
            sql = "select count(ID)  from Person where [IsVisit]=1 and [IsDelete]=0 and [IsReal] = 1";
            visitcount = AccessDB.QueryStatistic(mdbfile, sql, null);
            info.VisitNumber = visitcount;
            // namelist.Add(new DictionaryEntry("全国", count));
            namelist["全国"] = info;

            sql = "select count([ID]) as Num,[CityID] from Person where [IsDelete]=0 and [IsReal] = 1 group by [CityID]";
            DataSet ds = AccessDB.Query(mdbfile, sql, null);
            if(ds!=null)
            {
                DataTable dt = ds.Tables[0];
                foreach(DataRow row in dt.Rows)
                {
                    if (listener != null)
                    {
                        listener.settotalcurrent(total);
                    }
                    total += 1;
                    int cityid = DatabaseUtility.getIntValue(row, "CityID", -1);
                    int number = DatabaseUtility.getIntValue(row, "Num", 0);

                    sql = "select count(ID) from Person where [CityID]=@id and [IsVisit]=1 and [IsDelete]=0 and [IsReal] = 1";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@id", cityid));
                    int visit = AccessDB.QueryStatistic(mdbfile, sql, list);
                    
                  //  int visit = DatabaseUtility.getIntValue(row, "VisCount", 0);
                   // int cityid = Convert.ToInt32(row["CityID"]);
                   // int number = Convert.ToInt32(row["Num"]);
                    //得到省名称
                    string cityname = CityUtility.GetProvinceName(mdbfile, cityid);
                    //累加
                    if (namelist.ContainsKey(cityname))
                    {
                        StatisticInfo info1 = (StatisticInfo)namelist[cityname];
                        info1.Number += number;
                        info1.VisitNumber += visit;
                        //int oldnumber = Convert.ToInt32(namelist[cityname]);
                        //namelist[cityname] = oldnumber + number;
                    }
                    else
                    {
                        StatisticInfo info1= new StatisticInfo();
                        info1.Name = cityname;
                        info1.Number = number;
                        info1.VisitNumber = visit;
                        namelist[cityname] = info1;
                    }
                    //  string cityname = AccessDB.GetProviceName(mdbfile, cityid);
                    //   namelist.Add(new DictionaryEntry(cityname, number));
                }
            }
            if (listener != null)
            {
                listener.progressend();
            }
            return namelist;

        }
    }
}
