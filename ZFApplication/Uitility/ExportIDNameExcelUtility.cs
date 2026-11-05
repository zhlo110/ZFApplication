using DevExpress.Pdf.Native.BouncyCastle.Utilities.Collections;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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

namespace ZFApplication.Uitility
{
    public class ExportIDNameExcelUtility
    {
        public static int mcurrentNumber = 0;

        public static string getPhoneNumbers(string mdbfiles,string No,string objname,string[] titlelist)
        {
            string phone = "";
            string sql = "";
            if(titlelist.Length > 0)
            {
                foreach(string title in titlelist)
                {
                    sql = @"select [PhoneNum] from Person 
                        where [IsDelete]=0 and [IsReal]=1 and Title=@title and [No]=@no and [ObjName]=@name limit 1";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@title", title));
                    list.Add(new DictionaryEntry("@no", No));
                    list.Add(new DictionaryEntry("@name", objname));

                    DataSet ds = AccessDB.Query(mdbfiles, sql, list);
                    if(ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        if(dt.Rows.Count > 0)
                        {
                            phone += title + ":" + DatabaseUtility.getStringValue(dt.Rows[0], "PhoneNum")+"\r\n";
                        }
                    }
                }
            }
        
            return phone;
        }
        public static string getRealAddress(string mdbfiles,int cityid)
        {
            string address = "";
            string sql = "";
            if(cityid > 0)
            {
                string[] regionnames = CityUtility.GetALLRegionName(mdbfiles, cityid);
                //如果regionnames是2个元素，则将这两个元素合并，如果三个元素，则将后两个元素合并
                if(regionnames.Length == 2)
                {
                    address = regionnames[0] + regionnames[1];
                }
                else if(regionnames.Length == 3)
                {
                    address = regionnames[1] + regionnames[2];
                }
               // address = string.Join(" ", regionnames);
            }
            return address;
        }

        public static string getParentAddress(string mdbfiles,string No,string objname,string addresstype,string[] titlelist)
        {
            string address = "";
            string sql = "";
            if(titlelist.Length > 0)
            {
                foreach(string title in titlelist)
                {
                    sql = @"select [CityID] from Person 
                        where [IsDelete]=0 and [IsReal]=1 and Title=@title and [No]=@no and [ObjName]=@name and [AddrType]=@addresstype limit 1";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@title", title));
                    list.Add(new DictionaryEntry("@no", No));
                    list.Add(new DictionaryEntry("@name", objname));
                    list.Add(new DictionaryEntry("@addresstype", addresstype));

                    DataSet ds = AccessDB.Query(mdbfiles, sql, list);
                    if(ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        if(dt.Rows.Count > 0)
                        {
                            int cityid = DatabaseUtility.getIntValue(dt.Rows[0], "CityID", -1);
                            address += title + ":" + getRealAddress(mdbfiles, cityid)+"\r\n";
                        }
                    }
                }
            }
            return address;
        }
        //得到父母单位
        public static string getParentUnit(string mdbfiles,string No,string objname,string[] titlelist)
        {
            string address = "";
            string sql = "";
            if(titlelist.Length > 0)
            {
                foreach(string title in titlelist)
                {
                    sql = @"select [Position],[Address] from Person 
                        where [IsDelete]=0 and 
                        [IsReal]=1 and Title=@title and [No]=@no and [ObjName]=@name and [AddrType]='工作单位及职务' limit 1";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@title", title));
                    list.Add(new DictionaryEntry("@no", No));
                    list.Add(new DictionaryEntry("@name", objname));

                    DataSet ds = AccessDB.Query(mdbfiles, sql, list);
                    if(ds != null)
                    {
                        DataTable dt = ds.Tables[0];
                        if(dt.Rows.Count > 0)
                        {
                            string position = DatabaseUtility.getStringValue(dt.Rows[0], "Position");
                            string address1 = DatabaseUtility.getStringValue(dt.Rows[0], "Address");
                            //如果position为空或为居民或村民，不填写
                            if(string.IsNullOrEmpty(position) || position.IndexOf("居民") > -1 || position.IndexOf("村民") > -1)
                            {
                                continue;
                            }
                            address = title + ":"+address1+position+"\r\n";
                            //address = title + ":" + DatabaseUtility.getStringValue(dt.Rows[0], "Position") + ":" + DatabaseUtility.getStringValue(dt.Rows[0], "Address");
                        }
                    }
                }
            }
            return address;
        }

        public static void exportEX(string filename, SingleBarInterface listener)
        {
            mcurrentNumber = 0;
            PathUtility.setcurrentpath();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfiles))
            {
                string destpath = "./template/scwd.xlsx";
                //新建文件
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                if (File.Exists(destpath))
                {
                    File.Copy(destpath, filename);
                }

                FileInfo fileInfo = new FileInfo(filename);
                if (!fileInfo.Exists)
                {
                    LogUtility.Log("文件" + filename + "不存在");
                }

                int rowNum = 2;
                string sql = @"select distinct [No],[IDCard],[Name],[CityID],[Nation],[Address] from Person 
                             where [IsDelete]=0 and [IsReal]=1 and Title='本人' and AddrType ='工作单位及职务'";
                DataSet ds = AccessDB.Query(mdbfiles, sql, null);
                Hashtable hashtable = new Hashtable();
                HashSet hashset = new HashSet();
                string[] titlelist = new string[] {"父亲","母亲","继父","继母"};
            //    int rowNum = 2;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            listener.settotalnumber(dt.Rows.Count);

                            foreach (DataRow dr in dt.Rows)
                            {
                                string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                                string name = DatabaseUtility.getStringValue(dr, "Name");
                                string no = DatabaseUtility.getStringValue(dr, "No");//政审编号
                                int cityid = DatabaseUtility.getIntValue(dr, "CityID", -1);
                                string nation = DatabaseUtility.getStringValue(dr, "Nation");
                                string address = DatabaseUtility.getStringValue(dr, "Address");//学校

                                //获取所有地区名称
                                string realaddress = getRealAddress(mdbfiles, cityid);
                                string parentaddress = getParentAddress(mdbfiles, no, name, "户口所在地", titlelist);
                                string parentOftenaddress = getParentAddress(mdbfiles, no, name, "经常居住地", titlelist);
                                string parentUnit = getParentUnit(mdbfiles, no, name, titlelist);
                                string phone = getPhoneNumbers(mdbfiles, no, name, titlelist);

                                string key = idcard + "_" + name + "_" + no;
                                listener.settotalcurrent(mcurrentNumber++);
                                if (hashset.Contains(key))
                                {
                                    continue;
                                }
                                hashset.Add(key);

                                worksheet.Cells["A" + rowNum.ToString()].Value = (rowNum-1).ToString(); //序号
                                worksheet.Cells["B" + rowNum.ToString()].Value = name;
                                worksheet.Cells["C" + rowNum.ToString()].Value = address;
                                worksheet.Cells["D" + rowNum.ToString()].Value = parentaddress;
                                worksheet.Cells["E" + rowNum.ToString()].Value = parentOftenaddress;
                                worksheet.Cells["F" + rowNum.ToString()].Value = realaddress;
                                worksheet.Cells["G" + rowNum.ToString()].Value = parentUnit;
                                worksheet.Cells["H" + rowNum.ToString()].Value = phone;
                                worksheet.Cells["I" + rowNum.ToString()].Value = no;
                                rowNum++;
                               // if (rowNum >= 10) break;
                            }

                            /*
                            /// 保存
                            /// 
                            int k = 0;
                            rowNum = 2;
                            using (ExcelPackage package = new ExcelPackage(fileInfo))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                                foreach (string province in hashtable.Keys)
                                {
                                    IList list = (IList)hashtable[province];
                                    int count = list.Count;
                                    if (count > 0)
                                    {
                                        int No = 1;
                                        foreach (var person in list)
                                        {
                                            var tuple = (Tuple<string, string, string, string>)person;
                                            worksheet.Cells["A" + rowNum.ToString()].Value = province;
                                            worksheet.Cells["B" + rowNum.ToString()].Value = No.ToString();
                                            worksheet.Cells["C" + rowNum.ToString()].Value = tuple.Item1;
                                            worksheet.Cells["D" + rowNum.ToString()].Value = tuple.Item2;
                                            worksheet.Cells["E" + rowNum.ToString()].Value = tuple.Item4;
                                            worksheet.Cells["F" + rowNum.ToString()].Value = tuple.Item3;
                                            rowNum++;
                                            listener.settotalcurrent(k);
                                            k++;
                                            No++;
                                        }
                                    }
                                }
                                worksheet.Rows[2, rowNum + 1].Height = 40;
                                package.Save();
                            }*/

                        }
                    }
                    if(rowNum > 2)
                    {
                        //A列设置居中
                        worksheet.Cells["A:A"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //B列设置居中
                        worksheet.Cells["B:B"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //C列设置左对齐
                        worksheet.Cells["C:C"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //D列设置左对齐
                        worksheet.Cells["D:D"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //E列设置左居中
                        worksheet.Cells["E:E"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        //F列设置左对齐
                        worksheet.Cells["F:F"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //G列设置左对齐
                        worksheet.Cells["G:G"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //H列设置左对齐
                        worksheet.Cells["H:H"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        //I列设置左对齐
                        worksheet.Cells["I:I"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        worksheet.Rows[2, rowNum + 1].Height = 50;    
                    }
                    package.Save();
                }

            }

            listener.progressend();
        }

        public static void export(string filename, SingleBarInterface listener)
        {
            mcurrentNumber = 0;
            PathUtility.setcurrentpath();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfiles))
            {
                string destpath = "./template/name-idcard.xlsx";
                //新建文件
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                if (File.Exists(destpath))
                {
                    File.Copy(destpath, filename);
                }

                FileInfo fileInfo = new FileInfo(filename);
                if (!fileInfo.Exists)
                {
                    LogUtility.Log("文件" + filename + "不存在");
                }

                int rowNum = 2;
                string sql = @"select distinct [No],[IDCard],[Name],[CityID],[Nation],[Address] from Person 
                             where [IsDelete]=0 and [IsReal]=1";
                DataSet ds = AccessDB.Query(mdbfiles, sql, null);
                Hashtable hashtable = new Hashtable();
                HashSet hashset = new HashSet();
              //  string[] titlelist = new string[] {"父亲","母亲","继父","继母"};
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        listener.settotalnumber(dt.Rows.Count);
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                            string name = DatabaseUtility.getStringValue(dr, "Name");
                            string no = DatabaseUtility.getStringValue(dr, "No");//政审编号
                            int cityid = DatabaseUtility.getIntValue(dr, "CityID", -1);
                            string nation = DatabaseUtility.getStringValue(dr, "Nation");
                            string address = DatabaseUtility.getStringValue(dr, "Address");//学校

                            //获取所有地区名称
                         //   string realaddress = getRealAddress(mdbfiles, cityid);
                        //    string parentaddress = getParentAddress(mdbfiles, no, name, "户口所在地", titlelist);
                        //    string parentOftenaddress = getParentAddress(mdbfiles, no, name, "经常居住地", titlelist);
                       //     string parentUnit = getParentUnit(mdbfiles, no, name, titlelist);  
                            
                            string key = idcard +"_"+name +"_"+ no;
                            listener.settotalcurrent(mcurrentNumber++);
                            if (hashset.Contains(key))
                            {
                                continue;
                            }
                            if(idcard.Contains('年')|| idcard.Contains('月')|| idcard.Contains('日')|| idcard.Contains('.')|| idcard.Length < 15)
                            {
                              //不是身份证
                                continue;
                            }

                            hashset.Add(key);
                            string cardtype = "居民身份证";
                           
                            idcard = idcard.Trim();
                            name = name.Trim();
                            string province = "未找到";
                            if(cityid > 0)
                            {
                                province = CityUtility.GetProvinceName(mdbfiles, cityid);
                            }
                            //string GetProvinceName
                            if (idcard.Length > 0 && name.Length > 0 && nation.Length > 0)
                            {
                                if(hashtable.ContainsKey(province))
                                {
                                    var person = new Tuple<string, string, string, string>(name, idcard, nation, cardtype);
                                    IList list = (IList)hashtable[province];
                                    list.Add(person);
                                }
                                else
                                {
                                    IList list = new ArrayList();
                                    var person = new Tuple<string,string,string,string>(name, idcard, nation, cardtype);
                                    list.Add(person);

                                    hashtable[province] = list;
                                }
                                //  worksheet.Cells["A" + rowNum.ToString()].Value = name;//不合并
                                // worksheet.Cells["B" + rowNum.ToString()].Value = idcard;//不合并
                                rowNum++;
                            }

                        }
                        


                    /// 保存
                    /// 
                        int k = 0;
                        rowNum = 2;
                        using (ExcelPackage package = new ExcelPackage(fileInfo))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            
                            foreach (string province in hashtable.Keys)
                            {
                                IList list = (IList)hashtable[province];
                                int count = list.Count;
                                if (count > 0)
                                {
                                    int No = 1;
                                    foreach (var person in list)
                                    {
                                        var tuple = (Tuple<string, string, string, string>)person;
                                        worksheet.Cells["A" + rowNum.ToString()].Value = province;
                                        worksheet.Cells["B" + rowNum.ToString()].Value = No.ToString();
                                        worksheet.Cells["C" + rowNum.ToString()].Value = tuple.Item1;
                                        worksheet.Cells["D" + rowNum.ToString()].Value = tuple.Item2;
                                        worksheet.Cells["E" + rowNum.ToString()].Value = tuple.Item4;
                                        worksheet.Cells["F" + rowNum.ToString()].Value = tuple.Item3;
                                        rowNum++;
                                        listener.settotalcurrent(k);
                                        k++;
                                        No++;
                                    }
                                }
                            }
                            worksheet.Rows[2, rowNum+1].Height = 40;
                            package.Save();
                        }

                    }


                }


                

            }

            listener.progressend();
        }
    }
}
