using DevExpress.CodeParser;
using DevExpress.Pdf;
using DevExpress.Pdf.Native.BouncyCastle.Utilities.Collections;
using DevExpress.Utils.ScrollAnnotations;
using DevExpress.XtraLayout.Customization;
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
using System.Windows.Forms;
using ZFApplication.Database;
using ZFApplication.Listeners;

namespace ZFApplication.Uitility
{
    public class SplitUtility
    {
        public static void creatsubdir(string mdbfile,int parentid,string foldername, ProgressBarListeners listener)
        {
            string sql = "select [ID],[Name] from City where [ParentID]=" + parentid;
            DataSet ds = AccessDB.Query(mdbfile, sql, null);
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach(DataRow dr in dt.Rows)
                    {
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        if(id >= 0)
                        {
                            string name = DatabaseUtility.getStringValue(dr, "Name");
                            string newfolder = foldername + "\\" + name;
                            if(!Directory.Exists(newfolder))
                            {
                                Directory.CreateDirectory(newfolder);
                            }
                            createfiles(mdbfile, newfolder, id, listener);
                            //建立子文件夹
                            creatsubdir(mdbfile, id, newfolder, listener);
                        }
                    }
                }
            }
        }


        public static void writeEmailfolder(string mdbfile, string folder,int id,string objname,string no,string title,string name,string url,ProgressBarListeners listener)
        {
            listener.settotalcurrent(mcurrentNumber, "正在导出"+ SystemUtility.trimstring(objname) + "的"+title+"(" + SystemUtility.trimstring(name) +")的信息");
        //    Hashtable hash = new Hashtable();
            string urlname = Path.GetFileNameWithoutExtension(url);
            string[] strings = urlname.Split(new string[] { "-" }, StringSplitOptions.None);
            if (strings.Length >= 3)
            {
                urlname = strings[0] + "-" + strings[1] + "-" + strings[2];
            }
            string filename = folder + "\\" + urlname + ".xlsx";
            string destpath = "./template/police-template.xlsx";
            if (!File.Exists(filename))
            {
                //    File.Delete(filename);
                if (File.Exists(destpath))
                {
                    File.Copy(destpath, filename);
                }
            }
            listener.setsubnumber(100);
            listener.setsubcurrent(0);
            writefilebyPolice(mdbfile, filename, name, objname, id, title, no);
        }

        public static void writepolicefolder(string mdbfile,string folder, int cityid, string policename, ProgressBarListeners listener)
        {
            //读一个派出所的数据
            listener.settotalcurrent(mcurrentNumber, folder);

            //统计这个派出所要读多少条数据
            string sql = "select count(ID) from Person where [CityID]=@cityid and [PoliceAddr]=@policename and [IsDelete]=0 and [IsReal]=1";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@cityid", cityid));
            list.Add(new DictionaryEntry("@policename", policename));
            int total = AccessDB.QueryStatistic(mdbfile, sql, list);
            listener.setsubnumber(total);

            //第一步生成文件,每个考生一个文件
            sql = @"select [ID],[No],[ObjName],[Url],[Title],[Name] from [Person] where [CityID]=@cityid and 
                        [PoliceAddr]=@policename and [IsDelete]=0 and [IsReal]=1 order by [ObjName],[OrderNo] asc";
            int subnumber = 0;
            Hashtable hash = new Hashtable();

            // listener.setsubcurrent(subnumber);
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        listener.setsubcurrent(subnumber);
                        subnumber++;
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        string objname = DatabaseUtility.getStringValue(dr, "ObjName");
                        string url = DatabaseUtility.getStringValue(dr, "Url");
                        string no = DatabaseUtility.getStringValue(dr, "No");
                        string title = DatabaseUtility.getStringValue(dr, "Title");
                        string personname = DatabaseUtility.getStringValue(dr, "Name");

                        if (id >= 0)
                        {
                            //从url中获取文件名
                            string name = Path.GetFileNameWithoutExtension(url);
                            string urlname = name;
                            string[] strings = urlname.Split(new string[] { "-" }, StringSplitOptions.None);
                            if (strings.Length >= 3)
                            {
                                urlname = strings[0] + "-" + strings[1] + "-" + strings[2];
                            }
                            string filename = folder + "\\" + urlname + ".xlsx";
                            string destpath = "./template/police-template.xlsx";
                            if (!File.Exists(filename))
                            {
                                //    File.Delete(filename);
                                if (File.Exists(destpath))
                                {
                                    File.Copy(destpath, filename);
                                }
                            }
                            

                            string key = no +"_"+ objname +"_"+ title +"_"+ personname;
                            if (hash.ContainsKey(key)) //来三个，只处理一个，因为是同一个人，在Excel中是一列
                                continue;//已经处理过了
                            else
                            {
                                hash[key] = id;
                                writefilebyPolice(mdbfile, filename, personname, objname, id, title, no);
                            }

                            //获取设个人要读取数据的总数
                        }
                    }
                }
            }
            mcurrentNumber++;
        }
        //mdbfile: 数据库文件名
        //filename: 要写入的excel文件名
        //name:姓名
        //objname:考生姓名
        //id:name的id
        //title:与考生关系
        //no:考生编号
        public static void addkeyhash(Hashtable table,string key,string value)
        {
            if (!table.ContainsKey(key))
            {
                table[key] = value;
            }
        }
        private static void writefilebyPolice(string mdbfile, string filename, string name, string objname, int id, string title, string no)
        {
            if (!mHasWriteHead.ContainsKey(filename))//如果mHasWriteHead不存在这个文件名，表示之前没有写过，需要写头
            {
                writeExcelHead(mdbfile, filename, name, objname, id, title, no);  
                mHasWriteHead[filename] = true;
            }

            //写亲属
            if(!mWriteHeadLine.ContainsKey(filename))
            {
                //亲属是从第7行开始写
                mWriteHeadLine[filename] = 7;
            }
            if (title == "本人") //本人跳过，已经在头部写过了
                return;

            int line = (int)mWriteHeadLine[filename];
            writeExcelline(line, mdbfile, filename, name, objname, id, title, no);
            mWriteHeadLine[filename] = line + 1;//写下一行
        }
        private static void writeExcelline(int line,string mdbfile, string filename, string name, string objname, int id, string title, string no)
        {
            //mdbfile数据库文件名
            //filename:要写入Excel的文件名
            //name:亲属姓名
            //objname:考生姓名
            //id: Person表中的id对应亲属
            //title:与考生关系
            //no:考生编号
            string sql = @"select [No],[ObjName],[Title],[Name],[Sex],[Nation],[Political],[Marriage],[IDCard],[PhoneNum],[AddrType],[Address],[PoliceAddr],[Position],[AddressOrder],[Remark] 
                        from [Person] where [ObjName]=@objname and [Name]=@name and [Title]=@title and [No]=@no and [IsReal]=1 and [IsDelete]=0";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@objname", objname));
            list.Add(new DictionaryEntry("@name", name));
            list.Add(new DictionaryEntry("@title", title));
            list.Add(new DictionaryEntry("@no", no));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            bool success = false;
            Hashtable hash = new Hashtable();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        string no1 = DatabaseUtility.getStringValue(dr, "No");

                        string title1 = DatabaseUtility.getStringValue(dr, "Title"); //称谓
                      //  hash["@title"] = title1;
                        addkeyhash(hash, "@title", title1);//称谓

                        string name1 = DatabaseUtility.getStringValue(dr, "Name");
                        string[] lines = name1.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                        string oldername = "无";
                        if (lines.Length <= 1)
                        {
                            name1 = lines[0];
                        }
                        else
                        {
                            name1 = lines[0];
                            oldername = lines[1];
                        }
                        addkeyhash(hash, "@name", name1);//姓名
                        addkeyhash(hash, "@oldername", oldername); //曾用名

                        string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                        addkeyhash(hash, "@idcard", idcard);//身份证号

                        string marriage = DatabaseUtility.getStringValue(dr, "Marriage");
                        addkeyhash(hash, "@marriage", marriage);//婚姻状况

                        string nation = DatabaseUtility.getStringValue(dr, "Nation");
                        addkeyhash(hash, "@nation", nation); //民族
                        string political = DatabaseUtility.getStringValue(dr, "Political");
                        addkeyhash(hash, "@political", political);//政治面貌

                        string phonenum = DatabaseUtility.getStringValue(dr, "PhoneNum");
                        addkeyhash(hash, "@phone", phonenum);//手机号码

                        string position = DatabaseUtility.getStringValue(dr, "Position");
                        addkeyhash(hash, "@position", position);//职务

                        string remark = DatabaseUtility.getStringValue(dr, "Remark");
                        addkeyhash(hash, "@remark", remark);//备注

                        string addrtype = DatabaseUtility.getStringValue(dr, "AddrType");
                        string address = DatabaseUtility.getStringValue(dr, "Address");
                        string policename = DatabaseUtility.getStringValue(dr, "PoliceAddr");
              
                        if (addrtype == "户口所在地")
                        {
                            addkeyhash(hash, "@address", address);
                            addkeyhash(hash, "@policeaddress", policename);

                        }
                        else if (addrtype == "经常居住地")
                        {
                            addkeyhash(hash, "@oftenaddress", address);
                            addkeyhash(hash, "@oftenpoliceaddress", policename);
                        }
                        else if (addrtype == "工作单位及职务")
                        {
                            addkeyhash(hash, "@unit", address);
                        }


                        success = true;


                    }
                }
            }

            if(success)
            {
                //填入Excel表格
                fillexcelline(hash, filename, line);
            }


        }
        //写Excle的头
        private static void writeExcelHead(string mdbfile, string filename,string name,string objname,int id,string title,string no)
        {
            //写一个文件，文件名为派出所名称
            //1 写本人页面，通过填写的方式写
            string sql = @"select [No],[ObjName],[Title],[Name],[Sex],[Nation],[Political],[Marriage],[IDCard],[PhoneNum],[AddrType],[Address],[PoliceAddr],[Position],[AddressOrder],[Remark] 
                        from [Person] where [ObjName]=@name and [Title]=@title and [No]=@no and [IsReal]=1 and [IsDelete]=0";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@name", objname));
         //   list.Add(new DictionaryEntry("@objname", objname));
            list.Add(new DictionaryEntry("@title", "本人"));
            list.Add(new DictionaryEntry("@no", no));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            bool success = false;
            Hashtable hash = new Hashtable();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                     //   string no1 = DatabaseUtility.getStringValue(dr, "No");
                        
                       // string objname1 = DatabaseUtility.getStringValue(dr, "ObjName");
                       // string title1 = DatabaseUtility.getStringValue(dr, "Title");
                        string name1 = DatabaseUtility.getStringValue(dr, "Name");
                        //名字里可能有两个

                        string[] lines = name1.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                        string oldername = "无";
                        if (lines.Length <= 1)
                        {
                            name1 = lines[0];
                        }
                        else
                        {
                            name1 = lines[0];
                            oldername = lines[1];
                        }

                        //  name1.Split(new string[] { "\r\n" });
                        addkeyhash(hash,"@name", name1);
                        addkeyhash(hash, "@oldername", oldername);

                        string sex = DatabaseUtility.getStringValue(dr, "Sex");
                        addkeyhash(hash, "@sex", sex);
                       
                        string nation = DatabaseUtility.getStringValue(dr, "Nation");
                        addkeyhash(hash, "@nation", nation);
                        string political = DatabaseUtility.getStringValue(dr, "Political");
                        addkeyhash(hash, "@political", political);

                        string marriage = DatabaseUtility.getStringValue(dr, "Marriage");
                        string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                        addkeyhash(hash, "@idcard", idcard);

                        string phonenum = DatabaseUtility.getStringValue(dr, "PhoneNum");
                        addkeyhash(hash, "@phone", phonenum);

                        string addrtype = DatabaseUtility.getStringValue(dr, "AddrType");
                        string address = DatabaseUtility.getStringValue(dr, "Address");
                        string policename = DatabaseUtility.getStringValue(dr, "PoliceAddr");
                        if(addrtype == "户口所在地")
                        {
                            addkeyhash(hash, "@address", address);
                            addkeyhash(hash, "@policeaddress", policename);
                            
                        }
                        else if (addrtype == "经常居住地")
                        {
                            addkeyhash(hash, "@oftenaddress", address);
                            addkeyhash(hash, "@oftenpoliceaddress", policename);
                        }
                        else if (addrtype == "工作单位及职务")
                        {
                            addkeyhash(hash, "@schoolname", address);
                        }

                        string position = DatabaseUtility.getStringValue(dr, "Position");
                        addkeyhash(hash, "@classname", position);


                        string remark = DatabaseUtility.getStringValue(dr, "Remark");
                        addkeyhash(hash, "@parentphone", remark);
                        success = true;
                    }  
                }
            }
            if(success)
            {
                //填入Excel表格
                fillexcel(hash, filename);
            }


        }
        public static string gethashvalue(Hashtable table, string key)
        {
            if (table.ContainsKey(key))
            {
                string value = table[key].ToString();
                value = value.Trim();
                if (value == "无")
                    value = "";
                return value;
            }
            else
            {
                return "";
            }
        }

        public static void fillexcelline(Hashtable table, string filename,int line)
        {
            FileInfo fileInfo = new FileInfo(filename);
            if (!fileInfo.Exists)
            {
                LogUtility.Log("文件" + filename + "不存在");
            }
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                worksheet.Cells["A"+line.ToString()].Value = gethashvalue(table, "@title");//table["@title"] == null ? "" : table["@title"].ToString();
                worksheet.Cells["B"+line.ToString()].Value = gethashvalue(table, "@name");//table["@name"] == null ? "" : table["@name"].ToString();
                worksheet.Cells["C"+line.ToString()].Value = gethashvalue(table, "@idcard");//table["@idcard"] == null ? "" : table["@idcard"].ToString();
                worksheet.Cells["D"+line.ToString()].Value = gethashvalue(table, "@oldername");//table["@oldername"] == null ? "" : table["@oldername"].ToString();
                worksheet.Cells["E"+line.ToString()].Value = gethashvalue(table, "@marriage");//table["@marriage"] == null ? "" : table["@marriage"].ToString();
                worksheet.Cells["F"+line.ToString()].Value = gethashvalue(table, "@political");//table["@political"] == null ? "" : table["@political"].ToString();
                worksheet.Cells["G"+line.ToString()].Value = gethashvalue(table, "@nation");//table["@nation"] == null ? "" : table["@nation"].ToString();
                worksheet.Cells["H"+line.ToString()].Value = gethashvalue(table, "@phone");//table["@phone"] == null ? "" : table["@phone"].ToString();
                worksheet.Cells["I"+line.ToString()].Value = gethashvalue(table, "@address");//table["@address"] == null ? "" : table["@address"].ToString();
                worksheet.Cells["J"+line.ToString()].Value = gethashvalue(table, "@policeaddress");//table["@policeaddress"] == null ? "" : table["@policeaddress"].ToString();
                worksheet.Cells["K"+line.ToString()].Value = gethashvalue(table, "@oftenaddress");//table["@oftenaddress"] == null ? "" : table["@oftenaddress"].ToString();
                worksheet.Cells["L"+line.ToString()].Value = gethashvalue(table, "@oftenpoliceaddress");//table["@oftenpoliceaddress"] == null ? "" : table["@oftenpoliceaddress"].ToString();
                string unit = gethashvalue(table, "@unit");//table["@unit"] == null ? "" : table["@unit"].ToString();
                string position = gethashvalue(table, "@position");//table["@position"] == null ? "" : table["@position"].ToString();
                worksheet.Cells["M"+line.ToString()].Value = unit + position;
                worksheet.Cells["O"+line.ToString()].Value = gethashvalue(table, "@remark");//table["@remark"] == null ? "" : table["@remark"].ToString();

                // 设置边框
                using (var range = worksheet.Cells["A" + line + ":P" + line])
                {
                    range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                package.Save();
            }
        }
        public static void fillexcel(Hashtable table, string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            if (!fileInfo.Exists)
            {
                LogUtility.Log("文件" + filename + "不存在");
            }
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  // 也可以通过名称获取，如 package.Workbook.Worksheets["Sheet1"];
                worksheet.Cells["B3"].Value = table["@name"].ToString();
                worksheet.Cells["C3"].Value = table["@idcard"].ToString();
                worksheet.Cells["D3"].Value = table["@oldername"].ToString();
                worksheet.Cells["E3"].Value = table["@sex"].ToString();
                worksheet.Cells["F3"].Value = table["@political"].ToString();
                worksheet.Cells["G3"].Value = table["@nation"].ToString();
                worksheet.Cells["H3"].Value = table["@phone"].ToString();
                worksheet.Cells["I3"].Value = table["@address"].ToString();
                worksheet.Cells["J3"].Value = table["@policeaddress"].ToString();
                worksheet.Cells["K3"].Value = table["@oftenaddress"].ToString();
                worksheet.Cells["L3"].Value = table["@oftenpoliceaddress"].ToString();
                worksheet.Cells["M3"].Value = table["@schoolname"].ToString();
                worksheet.Cells["N3"].Value = table["@classname"].ToString();
                worksheet.Cells["O3"].Value = table["@parentphone"].ToString();
                package.Save();
            }
            // 替换第三行的标记
               
        }

        public static void writefile(string mdbfile, string filename, int cityid, string policename, SingleBarInterface listener)
        {
            string sql = @"select [ID],[ObjName],[Title],[Name] from [Person] where [CityID]=@cityid and 
                        [PoliceAddr]=@policename and [IsDelete]=0 and [IsReal]=1";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@cityid", cityid));
            list.Add(new DictionaryEntry("@policename", policename));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            string ids = "";
            Hashtable hash = new Hashtable();
            HashSet set = new HashSet();
            //查询真实信息
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                        string objname = DatabaseUtility.getStringValue(dr, "ObjName");
                        string title = DatabaseUtility.getStringValue(dr, "Title");
                        string name = DatabaseUtility.getStringValue(dr, "Name");
                        if (id >= 0)
                        {
                            ids+=id.ToString()+",";
                            if (hash.ContainsKey(id))
                            {
                                //有重复
                                hash.Remove(id);
                            }
                            if(!set.Contains(id))
                            {
                                set.Add(id);
                            }
                            
                            //查询关联信息
                            sql = "select [ID],[RealID] from [Person] where [RealID] <> @realid and [ObjName]=@objname and [Title]=@title and [Name]=@name";
                            list.Clear();
                            list.Add(new DictionaryEntry("@realid", id));
                            list.Add(new DictionaryEntry("@objname", objname));
                            list.Add(new DictionaryEntry("@title", title));
                            list.Add(new DictionaryEntry("@name", name));
                            DataSet ds1 = AccessDB.Query(mdbfile, sql, list);
                            if (ds1 != null)
                            {
                                if (ds1.Tables.Count > 0)
                                {
                                    DataTable dt1 = ds1.Tables[0];
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        int id1 = DatabaseUtility.getIntValue(dr1, "ID", -1);
                                        int realid = DatabaseUtility.getIntValue(dr1, "RealID", -1);
                                        if (id1 >= 0)
                                        {
                                            if (realid > 0) // 不在本县区
                                            {
                                                if (!set.Contains(realid)) //去掉重复
                                                {
                                                    if (!hash.ContainsKey(realid))
                                                    {
                                                        hash[realid] = id1;
                                                    }
                                                }
                                            }
                                            else //在本县区
                                            {
                                                if (!set.Contains(id1)) //去掉重复
                                                {
                                                    if (!hash.ContainsKey(id1))
                                                    {
                                                        hash[id1] = id1;
                                                    }
                                                }
                                            }
                                         //   ids += id1.ToString() + ",";
                                        }
                                    }
                                }
                            }
                         //   writefilebyid(mdbfile, filename, cityid, policename, listener);
                        }
                    }
                }
            }

            foreach (DictionaryEntry de in hash)
            {
                ids += de.Value.ToString() + ",";
            }

            if (ids.Length > 0)
            {
                
                ids = ids.Substring(0, ids.Length - 1);
                writefilebyid(mdbfile, filename, ids, listener);
            }

        }

        public static void writefilebyid(string mdbfile,string filename,string ids, SingleBarInterface listener)
        {
            //写一个文件，文件名为派出所名称


            string sql = @"select [No],[ObjName],[Title],[Name],[Nation],[Political],[Marriage],[IDCard],[PhoneNum],[AddrType],[Address],[PoliceAddr],[Position],[AddressOrder],[Remark] 
                        from [Person] where [ID] in (" + ids+ ") order by [No],[ObjName],[Title],[AddressOrder] asc";
          //  IList list = new ArrayList();
          //  list.Add(new DictionaryEntry("@cityid", cityid));
          //  list.Add(new DictionaryEntry("@policename", policename));

            DataSet ds = AccessDB.Query(mdbfile, sql, null);
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    //写文件
                        //将数据写入到文件中
                    FileInfo fileInfo = new FileInfo(filename);
                    if (!fileInfo.Exists)
                    {
                        LogUtility.Log("文件"+filename+"不存在");
                    }
                    // 假设我们操作第一个工作表
                    int rowNum = 2;
                    int mergecurrent = 0;
                    int mergetotal = -1;
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  // 也可以通过名称获取，如 package.Workbook.Worksheets["Sheet1"];
                        foreach (DataRow dr in dt.Rows)
                        {
                            mcurrentNumber++;
                            if(listener != null)
                            {
                                listener.settotalcurrent(mcurrentNumber);
                            }
                                
                            if (mergecurrent == 0 && mergetotal==-1) 
                            {
                                string no = DatabaseUtility.getStringValue(dr, "No");
                                string objname = DatabaseUtility.getStringValue(dr, "ObjName");
                                string title = DatabaseUtility.getStringValue(dr, "Title");
                                string name = DatabaseUtility.getStringValue(dr, "Name");
                                string nation = DatabaseUtility.getStringValue(dr, "Nation");
                                string political = DatabaseUtility.getStringValue(dr, "Political");
                                string marriage = DatabaseUtility.getStringValue(dr, "Marriage");
                                string idcard = DatabaseUtility.getStringValue(dr, "IDCard");
                                string phonenum = DatabaseUtility.getStringValue(dr, "PhoneNum");
                               
                                string remark = DatabaseUtility.getStringValue(dr, "Remark");

                                sql = "select count(ID) from Person where [ID] in ("+ids+ ") and [No]=@no and [Title]=@title and [ObjName]=@objname  and [IsDelete]=0";
                                IList list1 = new ArrayList();
                               // list1.Add(new DictionaryEntry("@cityid", cityid));
                               // list1.Add(new DictionaryEntry("@policename", policename));
                                list1.Add(new DictionaryEntry("@no", no));
                                list1.Add(new DictionaryEntry("@title", title));
                                list1.Add(new DictionaryEntry("@objname", objname));
                                mergetotal = AccessDB.QueryStatistic(mdbfile, sql, list1);
                                //合并单元格
                                if(mergetotal > 1)
                                {
                                     worksheet.Cells["A"+rowNum.ToString()+":A"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["B"+rowNum.ToString()+":B"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["C"+rowNum.ToString()+":C"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["D"+rowNum.ToString()+":D"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["E"+rowNum.ToString()+":E"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["F"+rowNum.ToString()+":F"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["G"+rowNum.ToString()+":G"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["H"+rowNum.ToString()+":H"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["I"+rowNum.ToString()+":I"+(rowNum+mergetotal-1).ToString()].Merge = true;
                                     worksheet.Cells["J"+rowNum.ToString()+":J"+(rowNum + mergetotal - 1).ToString()].Merge = false;
                                     worksheet.Cells["K"+rowNum.ToString()+":K"+(rowNum + mergetotal - 1).ToString()].Merge = false;
                                     worksheet.Cells["L"+rowNum.ToString()+":L"+(rowNum+mergetotal-1).ToString()].Merge = false;
                                     worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Merge = true;


                                    worksheet.Cells["A" + rowNum.ToString() + ":A" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["B" + rowNum.ToString() + ":B" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + rowNum.ToString() + ":C" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["D" + rowNum.ToString() + ":D" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["E" + rowNum.ToString() + ":E" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["F" + rowNum.ToString() + ":F" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["G" + rowNum.ToString() + ":G" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["H" + rowNum.ToString() + ":H" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["I" + rowNum.ToString() + ":I" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["J" + rowNum.ToString() + ":J" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["K" + rowNum.ToString() + ":K" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["L" + rowNum.ToString() + ":L" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    worksheet.Cells["A" + rowNum.ToString() + ":A" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["B" + rowNum.ToString() + ":B" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["C" + rowNum.ToString() + ":C" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["D" + rowNum.ToString() + ":D" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["E" + rowNum.ToString() + ":E" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["F" + rowNum.ToString() + ":F" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["G" + rowNum.ToString() + ":G" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["H" + rowNum.ToString() + ":H" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["I" + rowNum.ToString() + ":I" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["J" + rowNum.ToString() + ":J" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["K" + rowNum.ToString() + ":K" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["L" + rowNum.ToString() + ":L" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                else if(mergetotal == 1) //不合并
                                {
                                    worksheet.Cells["A" + rowNum.ToString() + ":A" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["B" + rowNum.ToString() + ":B" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + rowNum.ToString() + ":C" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["D" + rowNum.ToString() + ":D" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["E" + rowNum.ToString() + ":E" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["F" + rowNum.ToString() + ":F" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["G" + rowNum.ToString() + ":G" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["H" + rowNum.ToString() + ":H" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["I" + rowNum.ToString() + ":I" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["J" + rowNum.ToString() + ":J" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["K" + rowNum.ToString() + ":K" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["L" + rowNum.ToString() + ":L" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    worksheet.Cells["A" + rowNum.ToString() + ":A" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["B" + rowNum.ToString() + ":B" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["C" + rowNum.ToString() + ":C" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["D" + rowNum.ToString() + ":D" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["E" + rowNum.ToString() + ":E" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["F" + rowNum.ToString() + ":F" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["G" + rowNum.ToString() + ":G" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["H" + rowNum.ToString() + ":H" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["I" + rowNum.ToString() + ":I" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["J" + rowNum.ToString() + ":J" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["K" + rowNum.ToString() + ":K" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["L" + rowNum.ToString() + ":L" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                
                                worksheet.Cells["A" + rowNum.ToString() + ":A" + (rowNum + mergetotal - 1).ToString()].Value =no;
                                worksheet.Cells["B" + rowNum.ToString() + ":B" + (rowNum + mergetotal - 1).ToString()].Value =objname;
                                worksheet.Cells["C" + rowNum.ToString() + ":C" + (rowNum + mergetotal - 1).ToString()].Value =title;
                                worksheet.Cells["D" + rowNum.ToString() + ":D" + (rowNum + mergetotal - 1).ToString()].Value =name;
                                worksheet.Cells["E" + rowNum.ToString() + ":E" + (rowNum + mergetotal - 1).ToString()].Value =nation;
                                worksheet.Cells["F" + rowNum.ToString() + ":F" + (rowNum + mergetotal - 1).ToString()].Value =political;
                                worksheet.Cells["G" + rowNum.ToString() + ":G" + (rowNum + mergetotal - 1).ToString()].Value =marriage;
                                worksheet.Cells["H" + rowNum.ToString() + ":H" + (rowNum + mergetotal - 1).ToString()].Value =idcard;
                                worksheet.Cells["I" + rowNum.ToString() + ":I" + (rowNum + mergetotal - 1).ToString()].Value =phonenum;
                                worksheet.Cells["M" + rowNum.ToString() + ":M" + (rowNum + mergetotal - 1).ToString()].Value ="";
                                
                            }
                            if(mergetotal > 1)
                            {
                                if(mergecurrent >= mergetotal-1)
                                {
                                    mergecurrent = -1;
                                    mergetotal = -1;
                                }
                            }
                            else if(mergetotal == 1)
                            {
                                mergecurrent = -1;
                                mergetotal = -1;
                            }
                            //[Position],[AddressOrder]
                            string addrtype = DatabaseUtility.getStringValue(dr, "AddrType");
                            string address = DatabaseUtility.getStringValue(dr, "Address");
                            string policename = DatabaseUtility.getStringValue(dr, "PoliceAddr");
                            string position = DatabaseUtility.getStringValue(dr, "Position");
                            int order = DatabaseUtility.getIntValue(dr, "AddressOrder",-1);
                            string titles = DatabaseUtility.getStringValue(dr, "Title");
                            string positiontitle = "职务";
                            string addresstype = "工作单位";
                            if (titles == "本人")
                            {
                                addresstype = "学校";
                                positiontitle = "班级";
                            }
                            worksheet.Cells["J" + rowNum.ToString()].Value = addrtype;//不合并
                            
                            if (order != 3)
                            {
                                worksheet.Cells["K" + rowNum.ToString()].Value = address;//不合并
                                worksheet.Cells["L" + rowNum.ToString()].Value = policename;//不合并
                            }
                            else
                            {
                                worksheet.Cells["K" + rowNum.ToString()].Value = addresstype + "：" + address + "\r\n" + positiontitle + "：" + position;//不合并
                                worksheet.Cells["L" + rowNum.ToString()].Value = "";//不合并
                            }
                            worksheet.Row(rowNum).Height = 50;
                            rowNum++;
                            mergecurrent++;

                        }
                        package.Save();
                    }
                }
            }
        }
      

        public static void createfiles(string mdbfiles,string folder,int cityid, ProgressBarListeners listener)
        {
            //按派出所创建文件
            string sql = "select distinct [PoliceAddr] from Person where  [IsDelete]=0 and [IsReal]=1 and [CityID]=" + cityid;
            DataSet ds = AccessDB.Query(mdbfiles, sql, null);
            if(ds != null)
            {
                if(ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach(DataRow dr in dt.Rows)
                    {
                        string policeaddr = DatabaseUtility.getStringValue(dr, "PoliceAddr");
                        string olderpoliceaddr = policeaddr;
                        if (policeaddr != null)
                        {
                            //将policeaddr中的回车换行符替换为空值
                            policeaddr = policeaddr.Replace("\r\n", "");
                            policeaddr = policeaddr.Replace("\r", "");
                            policeaddr = policeaddr.Replace("\n", "");

                            string foldname = folder + "\\" + policeaddr;
                            //创建一个文件夹，文件夹名称为派出所名称
                            if (!Directory.Exists(foldname))
                            {
                                Directory.CreateDirectory(foldname);
                            }
                            writepolicefolder(mdbfiles, foldname,cityid, olderpoliceaddr, listener);
                            /*
                            //从模板中拷贝一个文件，并命名位派出所名称
                            string filename = folder + "\\" + policeaddr + ".xlsx";
                            string destpath = "./template/police-temp.xlsx";
                            if(File.Exists(filename))
                            {
                                File.Delete(filename);
                            }
                            if(File.Exists(destpath))
                            {
                                File.Copy(destpath, filename);
                            }*/

                            //写文件
                            //   writefile(mdbfiles, filename, cityid,olderpoliceaddr, listener);
                        }
                    }
                }
            }
            
        }


        public static int mcurrentNumber = 0;

        public static void splitEmail(string foldername, ProgressBarListeners listener)
        {
            mcurrentNumber = 0;
            mHasWriteHead.Clear();
            mWriteHeadLine.Clear();
            PathUtility.setcurrentpath();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfiles))
            {
                //统计公职人员的数量,IsPublic为公职
                string sql = "select count(ID) from Person where [IsDelete]=0 and [IsReal]=1 and [IsPublic]=1";
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
                sql = "select [ID],[No],[ObjName],[Name],[Title],[Url] from Person where [IsDelete]=0 and [IsReal]=1 and [IsPublic]=1";
                DataSet ds = AccessDB.Query(mdbfiles, sql, null);
                Hashtable hash = new Hashtable();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                            string no = DatabaseUtility.getStringValue(dr, "No");
                            string objname = DatabaseUtility.getStringValue(dr, "ObjName");
                            string name = DatabaseUtility.getStringValue(dr, "Name");
                            string title = DatabaseUtility.getStringValue(dr, "Title");
                            string url = DatabaseUtility.getStringValue(dr, "Url");
                            if (id >= 0 && no.Length >= 0 && objname.Length > 0 && name.Length > 0 && title.Length > 0)
                            {
                                string key = no + "_" + objname + "_" + title + "_" + name;
                                if (hash.ContainsKey(key)) //来三个，只处理一个，因为是同一个人，在Excel中是一列
                                {
                                    continue;//已经处理过了
                                }
                                writeEmailfolder(mdbfiles, foldername,id,objname,no,title, name,url, listener);
                                hash[key] = id;
                                //    writeExcelHead(mdbfiles, foldername + "\\" + objname + ".xlsx", name, objname, no, title, no.ToString());
                                //    mcurrentNumber++;
                                //    listener.settotalcurrent(mcurrentNumber);
                            }
                            mcurrentNumber++;
                        }
                    }
                }
            }
            listener.progressend();
        }


        // 下面两个表的key是一样的，都表示Excel文件所在的路径
        // mHasWriteHead 的Value是一个bool值，表示是否已经写了本人信息
        // mWriteHeadLine 的Value是一个int值，表示写了本人亲属信息的行数
        public static Hashtable mHasWriteHead = new Hashtable();
        public static Hashtable mWriteHeadLine = new Hashtable();
        public static void split(string foldername, ProgressBarListeners listener)
        {
            mcurrentNumber = 0;
            mHasWriteHead.Clear();
            mWriteHeadLine.Clear();
            PathUtility.setcurrentpath();
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            if (File.Exists(mdbfiles))
            {
                //统计派出所的数量

                string sql = "select count(*) from (select distinct [PoliceAddr],[CityID] from Person where [IsDelete]=0 and [IsReal]=1)";
                int cnt = AccessDB.QueryStatistic(mdbfiles, sql, null);
                if (cnt > 0)
                {
                    //本人由于有3条
                    listener.settotalnumber(cnt);
                }
                else
                {
                    listener.settotalnumber(0);
                }

                sql = "select [ID],[Name] from City where [ParentID]=0";
                DataSet ds  = AccessDB.Query(mdbfiles, sql,null);
                if(ds != null)
                {
                    if(ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        foreach(DataRow dr in dt.Rows)
                        {
                            int id = DatabaseUtility.getIntValue(dr, "ID", -1);
                            if(id >= 0)
                            {
                                string name = DatabaseUtility.getStringValue(dr, "Name");
                                string newfolder = foldername + "\\" + name;
                                if(!Directory.Exists(newfolder))
                                {
                                    Directory.CreateDirectory(newfolder);
                                }
                                createfiles(mdbfiles, newfolder, id, listener);
                                //建立子文件夹
                                creatsubdir(mdbfiles, id, newfolder, listener);
                            }
                            
                        }
                    }
                    

                }

            }
            listener.progressend();

        }
    }
}
