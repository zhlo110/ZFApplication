using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Excel;
using OfficeOpenXml;
using ZFApplication.Listeners;
using ZFApplication.Model;

namespace ZFApplication.Uitility
{
    public class ExcelReader
    {

        public static string[] FindDeletedXlsxFiles(string directoryPath,ProgressBarListeners listeners)
        {
            List<string> filesList = new List<string>();
            //  DateTime targetDate = new DateTime(2024, 4, 8); // 设置目标日期为2024年4月8日
            string sql = "select count(*) from (select distinct [Url] from Person)";
            string mdbfiles = SystemUtility.GetSystemValues("FilePath");
            int count = Database.AccessDB.QueryStatistic(mdbfiles, sql, null);

            listeners.settotalnumber(100);
            listeners.settotalcurrent(0, "正在查找删除的文件");
            listeners.setsubnumber(count);

            sql = "select distinct [Url] from Person";
            int nCount = 0;
            DataSet ds = Database.AccessDB.Query(mdbfiles, sql, null);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string url = DatabaseUtility.getStringValue(row, "Url");
                            if (url != null && url.Length > 0)
                            {
                                if (!File.Exists(url))
                                {
                                    filesList.Add(url);
                                }
                            }
                            listeners.setsubcurrent(nCount);
                            nCount++;
                        }
                    }
                }
            }
            return filesList.ToArray();
        }

        public static string[] FindModifiedXlsxFiles(string directoryPath,string time,
            ProgressBarListeners listeners)
        {
            List<string> filesList = new List<string>();
            //  DateTime targetDate = new DateTime(2024, 4, 8); // 设置目标日期为2024年4月8日

            DateTime targetDate = DateTime.Parse(time);

             DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            // 使用“SearchOption.AllDirectories”来搜索所有子目录
            FileInfo[] files = dirInfo.GetFiles("*.xlsx", SearchOption.AllDirectories);
            listeners.settotalnumber(100);
            listeners.settotalcurrent(0, "正在查找最新修改的文件");
            listeners.setsubnumber(files.Length);
            int nCount = 0;
            foreach (FileInfo file in files)
            {
                if(file.FullName.Contains("~$"))
                {
                    continue;
                }
             
                
                // 检查文件的最后修改时间
                DateTime dt = file.CreationTime> file.LastWriteTime ? file.CreationTime: file.LastWriteTime;
             
                if (dt > targetDate)
                {
                    filesList.Add(file.FullName);
                }
                //如果文件系统中没有这个文件，就不要加入到列表中
                string sql = "select count(ID) from Person where [Url]=@url and [Title]='本人'";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@url", file.FullName));

                string mdbfiles = SystemUtility.GetSystemValues("FilePath");
                int count = Database.AccessDB.QueryStatistic(mdbfiles, sql, list);
                if (count == 0)//没有读到这个文件
                {
                    filesList.Add(file.FullName);
                }


                listeners.setsubcurrent(nCount);
                //Thread.Sleep(3000);
                nCount++;
            }

            return filesList.ToArray();
        }

        public static void updatefolder(string rootPath, 
            string mdbfile, IList needlist, ProgressBarListeners listeners)
        {
            string time = SystemUtility.GetSystemTime("FilePath");
            try
            {
                // 使用"*.xls"作为搜索模式来匹配所有的.xls文件
                // SearchOption.AllDirectories选项使得搜索包含所有子目录
                //  string[] xlsFiles = Directory.GetFiles(rootPath, "*.xlsx", SearchOption.AllDirectories);
                string[] xlsFiles = 
                    ExcelReader.FindModifiedXlsxFiles(rootPath, time,listeners);
                foreach (string updatefile in needlist)
                {
                    if (!xlsFiles.Contains(updatefile))
                    {
                        xlsFiles = xlsFiles.Concat(new string[] { updatefile }).ToArray();
                    }
                }
                LogUtility.Log($"找到 {xlsFiles.Length} 个.xlsx。");
                listeners.settotalnumber(xlsFiles.Length);
                // Console.WriteLine($"找到 {xlsFiles.Length} 个.xls文件。");

                // 遍历找到的文件
                // 进度条
                int nCount = 0;
                foreach (string file in xlsFiles)
                {
                    // 在这里，你可以添加代码来处理每个文件
                    // Console.WriteLine(file);
                    listeners.settotalcurrent(nCount, file);
                    try
                    {
                        readexcel(file, mdbfile, listeners);
                    }
                    catch (Exception ex)
                    {
                        LogUtility.Log($"发生错误:文件损坏'"+file+"'{ex.Message}");
                    }
                    nCount++;
                }

                string[] deletedFiles = ExcelReader.FindDeletedXlsxFiles(rootPath, listeners);
                listeners.settotalnumber(deletedFiles.Length);
                nCount = 0;
                foreach (string file in deletedFiles)
                {
                    listeners.settotalcurrent(nCount, file);
                    string sql = "delete from Person where [Url]=@url";
                    IList list = new ArrayList();
                    list.Add(new DictionaryEntry("@url", file));
                    Database.AccessDB.NoQuery(mdbfile, sql, list);
                    //readexcel(file, mdbfile, listeners);
                    nCount++;
                }

            }
            catch (Exception ex)
            {
                // 捕获并显示任何错误
                //Console.WriteLine($"发生错误: {ex.Message}");
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            listeners.progressend();
        }
        public static void readerfolder(string rootPath,string mdbfile,IList needlist,ProgressBarListeners listeners)
        {
            try
            {
                // 使用"*.xls"作为搜索模式来匹配所有的.xls文件
                // SearchOption.AllDirectories选项使得搜索包含所有子目录
                string[] xlsFiles = Directory.GetFiles(rootPath, "*.xlsx", SearchOption.AllDirectories);

                foreach (string updatefile in needlist)
                {
                    if (!xlsFiles.Contains(updatefile))
                    {
                        xlsFiles = xlsFiles.Concat(new string[] { updatefile }).ToArray();
                    }
                }


                LogUtility.Log($"找到 {xlsFiles.Length} 个.xlsx。");
                listeners.settotalnumber(xlsFiles.Length);
                // Console.WriteLine($"找到 {xlsFiles.Length} 个.xls文件。");

                // 遍历找到的文件
                // 进度条
                int nCount = 0;
                foreach (string file in xlsFiles)
                {
                    // 在这里，你可以添加代码来处理每个文件
                    // Console.WriteLine(file);
                    listeners.settotalcurrent(nCount,file);
                    readexcel(file, mdbfile,listeners);
                    nCount++;
                }
            }
            catch (Exception ex)
            {
                // 捕获并显示任何错误
                //Console.WriteLine($"发生错误: {ex.Message}");
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            listeners.progressend();
        }
        
        public static void logError(string mdbfile,string studentno,string studentname,string error,bool save = true)
        {
            //在Person表中更新HasError字段
            if (save)
            {
                string sql = "update Person set HasError=1 where [No]=@no and [Name]=@name";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@no", studentno));
                list.Add(new DictionaryEntry("@name", studentname));
                Database.AccessDB.NoQuery(mdbfile, sql, list);
            }
            ProblemInfo problem = new ProblemInfo();
            problem.StudentNo = studentno;
            problem.StudentName = studentname;
            problem.Descriptin = error;
            problem.save(mdbfile);
            
            /*
            string path = SystemUtility.GetSystemTime("FilePath");
            string filename = path + "error.txt";
            FileStream fs = new FileStream(filename, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(studentno + " " + studentname + " " + error);
            sw.Close();
            fs.Close();*/
        }

        public static void readexcel(string path,string mdbfile, ProgressBarListeners listeners)
        {

            var fileInfo = new FileStream(path,FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            LogUtility.Log($"正在读文件：'"+ path + "'");
            using (var package = new ExcelPackage(fileInfo))
            {
                var workbook = package.Workbook;
                var dataset = new DataSet();
                bool success = false;
                foreach (var worksheet in workbook.Worksheets)
                {
                    string sheetname = worksheet.Name.Trim();
                    if (sheetname.IndexOf("线索")>0||sheetname.IndexOf("政治考核线索表")>0
                        ||sheetname.IndexOf("政考信息采集表")>0 || sheetname.IndexOf("采集") > 0)
                    {
                        success = true;
                       //
                       //var dataTable = new DataTable(worksheet.Name);
                       listeners.setsubnumber(worksheet.Dimension.End.Row);
                       if(worksheet.Dimension.End.Row >=7)
                       {
                            var rowNum = 3;//读本人的
                            listeners.setsubcurrent(4);
                            var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                            var cell1 = wsRow[rowNum, 2];
                            string studentname = "";
                            string studentno = "";
                            
                            if (cell1.Text.Trim().Length > 0)
                            {
                                PersonClue person = new PersonClue();
                                person.IsVisit = 0;
                                person.Isdelete= 0;
                                person.Haserror = 0;
                                string filename = Path.GetFileName(path);
                                string[] names = filename.Split('-');
                                if(names.Length > 0)
                                {
                                    person.No = names[0].Trim();
                                    studentno = names[0].Trim();
                                }
                                person.Title = "本人";
                                person.OrderNo = 0;
                                string name = cell1.Text.Trim(); //姓名
                               
                                
                                cell1 = wsRow[rowNum, 3];
                                string IDCard = cell1.Text.Trim(); //身份证
                                person.Idcard = IDCard;

                                
                             //   person.deleteALLClue();


                                cell1 = wsRow[rowNum, 4];
                                string othername = cell1.Text.Trim(); //曾用名

                                if(othername != null && othername.Length > 0 && othername != "无" && othername != "无曾用名" && othername != "无曾用名")
                                {
                                    name += "\r\n" + othername + "";
                                }
                                //删除原来的数据
                                ProblemInfo problemInfo = new ProblemInfo();
                                problemInfo.StudentNo = studentno;
                                problemInfo.StudentName = name;
                                problemInfo.delete(mdbfile);

                                person.Objname = name;
                                person.Name = name;
                                studentname = name;
                                //删除原来的数据
                                person.deleteALLClue(mdbfile);

                                cell1 = wsRow[rowNum, 5];
                                string sex = cell1.Text.Trim(); //性别
                                person.Sex = sex;

                                cell1 = wsRow[rowNum, 6];
                                string political = cell1.Text.Trim(); //政治面貌
                                person.Political = political;

                                cell1 = wsRow[rowNum, 7];
                                string nation = cell1.Text.Trim(); //民族
                                person.Nation = nation;

                                cell1 = wsRow[rowNum, 8];
                                string phonenumber = cell1.Text.Trim(); //本人电话
                                person.Phone = phonenumber;

                                cell1 = wsRow[rowNum, 9];
                                string cardAddr = cell1.Text.Trim(); //户口所在地
                                person.Addresstype = "户口所在地";
                                person.Address = cardAddr;
                                int cardcityid = CityUtility.ParseCity(cardAddr);
                                person.Cityid= cardcityid;//如果为-1，表示没有找到
                                if(cardcityid == -1)
                                {
                                    person.Haserror = 1;
                                    logError(mdbfile,studentno, studentname, "学生（"+studentname+"）的户口所在地没有找到",false);
                                }


                                cell1 = wsRow[rowNum, 10];
                                string cardPoliceAddr = cell1.Text.Trim(); //户口公安局地址
                                person.Policeaddress = cardPoliceAddr;
                                


                                cell1 = wsRow[rowNum, 11];
                                string Addr = cell1.Text.Trim(); //经常居住地
                                int addresscityid = CityUtility.ParseCity(Addr);
                                if(addresscityid == -1)
                                {
                                    person.Haserror = 1;
                                    logError(mdbfile,studentno, studentname, "学生("+studentname+")的经常居住地没有找到",false);
                                }

                                cell1 = wsRow[rowNum, 12];
                                string PoliceAddr = cell1.Text.Trim(); //经常居住地公安局


                                //就读学校，工作单位
                                cell1 = wsRow[rowNum, 13];
                                string unit = cell1.Text.Trim(); //就读学校，工作单位

                                //职位
                                cell1 = wsRow[rowNum, 14];
                                string position = cell1.Text.Trim(); //职位
                                person.Position = position;


                                //备注，父母电话
                                cell1 = wsRow[rowNum, 15];
                                string remark = cell1.Text.Trim(); //备注
                                person.Remark = remark;

                                //是否有公职

                                person.Ispublic = 0;
                                person.Url = path;
                                person.Marriage = "未婚";

                                person.Isreal= 1;
                                person.Addressorder= 1;
                                person.Realid = -1;
                                int hjrealid = person.save(mdbfile);


                                PersonClue jc = person.clone();
                                jc.Addresstype = "经常居住地";
                                jc.Addressorder = 2;
                                jc.Address = Addr;
                                jc.Policeaddress = PoliceAddr;
                                jc.Cityid = addresscityid;
                                jc.Isreal = 1;
                                int jcrealid = jc.save(mdbfile);


                                PersonClue gz = person.clone();
                                gz.Addresstype = "工作单位及职务";
                                gz.Address = unit;
                                gz.Policeaddress = PoliceAddr;
                                gz.Cityid = addresscityid;
                                gz.Addressorder = 3;
                                gz.Isreal = 1;
                                int gzrealid = gz.save(mdbfile);


                                PersonClue jc_to_hj = person.clone();
                                jc_to_hj.Cityid = addresscityid;
                                if (addresscityid != cardcityid)
                                {
                                    jc_to_hj.Realid = hjrealid;
                                    jc_to_hj.Isreal = 0;
                                }
                                jc_to_hj.save(mdbfile);// if (addresscityid == cardcityid) 条件相等，只是更新


                                PersonClue hj_to_jc = person.clone();
                                //   otherperson.Url = "";
                                hj_to_jc.Addresstype = "经常居住地";
                                hj_to_jc.Address = Addr;
                                hj_to_jc.Policeaddress = PoliceAddr;
                                hj_to_jc.Addressorder= 2;
                                if (addresscityid != cardcityid)
                                {
                                    hj_to_jc.Isreal = 0;
                                    hj_to_jc.Realid = jcrealid;
                                }
                                hj_to_jc.save(mdbfile);


                                PersonClue hj_to_gz = person.clone();
                                hj_to_gz.Addresstype = "工作单位及职务";
                                hj_to_gz.Address = unit;
                                hj_to_gz.Policeaddress = PoliceAddr;
                                hj_to_gz.Addressorder = 3;
                                if (addresscityid != cardcityid)
                                {
                                    hj_to_gz.Isreal = 0;
                                    hj_to_gz.Realid = gzrealid;
                                }
                                
                                hj_to_gz.save(mdbfile);

                                if (cardcityid > 0)
                                {
                                    CityUtility.CopytoLocalCity(mdbfile,cardcityid);
                                }

                                if(addresscityid > 0)
                                {
                                    CityUtility.CopytoLocalCity(mdbfile,addresscityid);
                                }

                            }
                            if(studentname.Length > 0 && studentno.Length > 0)
                            {
                                rowNum = 7; //读亲属的
                                for(;rowNum < worksheet.Dimension.End.Row;rowNum++)
                                {
                                    listeners.setsubcurrent(rowNum);
                                    wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                                    cell1 = wsRow[rowNum, 1];
                                    if (cell1.Text.Trim().Length > 0)
                                    {
                                        PersonClue person = new PersonClue();
                                        person.IsVisit = 0;
                                        person.Isdelete = 0;
                                        person.Haserror = 0;
                                        person.No = studentno;
                                        string title = cell1.Text.Trim();//称谓不能为
                                        if(title.Length > 5)
                                        {
                                            //需要说明的问题等，不操作
                                            continue;
                                        }

                                        if(title.Length == 0)
                                        {
                                            logError(mdbfile,studentno, studentname, "亲属称谓为空");
                                            continue;
                                        }

                                        person.Title = title; //称谓
                                        person.OrderNo = TitleOrderUtility.getOrder(title);
                                        cell1 = wsRow[rowNum, 2]; //姓名
                                        string name = cell1.Text.Trim(); //姓名 必须的，没有是错的

                                        if(name.Length == 0)
                                        {
                                            
                                            //
                                            logError(mdbfile, studentno, studentname, "亲属("+ title + ")姓名为空");
                                            continue;
                                        }
                                                                        

                                        cell1 = wsRow[rowNum, 3]; //身份证号 没有是错的
                                        string idcard = cell1.Text.Trim(); //姓名
                                        if(idcard.Length == 0)
                                        {
                                            logError(mdbfile, studentno, studentname, "亲属("+ title + ","+name+")身份证为空");
                                         //   continue;
                                        }
                                        
                                        person.Idcard = idcard;


                                        cell1 = wsRow[rowNum, 4]; //曾用名
                                        string othername = cell1.Text.Trim(); 
                                        if(othername != null && othername.Length > 0 && othername != "无" && othername != "无曾用名" && othername != "无曾用名")
                                        {
                                            name += "\r\n" + othername + "";
                                        }
                                        person.Name = name;
                                        person.Objname = studentname;

                                        cell1 = wsRow[rowNum, 5]; //婚姻状况  没有是错的
                                        string marriage = cell1.Text.Trim();
                                        if(marriage.Length == 0)
                                        {
                                            logError(mdbfile, studentno, studentname, "亲属("+title+"," + name + ")婚姻状况为空");
                                          //  continue;
                                        } 
                                        person.Marriage = marriage;

                                        cell1 = wsRow[rowNum, 6]; //政治面貌 没有是错的
                                        string political = cell1.Text.Trim(); 
                                        if(political.Length == 0)
                                        {
                                            logError(mdbfile, studentno, studentname, "亲属("+ title + ","+ name + ")政治面貌为空");
                                       //     continue;
                                        }
                                        person.Political = political;

                                        //民族
                                        cell1 = wsRow[rowNum, 7]; //民族,没有是错的
                                        string nation = cell1.Text.Trim();
                                        if(nation.Length == 0)
                                        {
                                            logError(mdbfile, studentno, studentname, "亲属("+ title + ","+ name + ")民族为空");
                                        //    continue;
                                        }
                                        person.Nation = nation;

                                        //电话
                                        cell1 = wsRow[rowNum, 8]; //电话
                                        string phone = cell1.Text.Trim();
                                        person.Phone = phone;

                                        //户籍所在地地址
                                        cell1 = wsRow[rowNum, 9]; //户籍所在地地址
                                        string cardAddr = cell1.Text.Trim();
                                        person.Addresstype = "户口所在地";
                                        person.Address = cardAddr;

                                       // cardAddr = "陕西省清涧县店则沟镇崔家岔村三组03号";
                                        int cardcityid = CityUtility.ParseCity(cardAddr);
                                        if(cardcityid == -1)
                                        {
                                            logError(mdbfile, studentno, studentname, "学生（"+studentname+"）的"+title+"户口所在地没有找到");
                                        }
                                        person.Cityid= cardcityid;

                                        //户籍所在地公安局地址
                                        cell1 = wsRow[rowNum, 10]; //户籍所在地公安局地址
                                        string cardPoliceAddr = cell1.Text.Trim();
                                        person.Policeaddress = cardPoliceAddr;

                                        //现居住地地址
                                        cell1 = wsRow[rowNum, 11]; //现居住地地址
                                        string Addr = cell1.Text.Trim();
                                        int addresscityid = CityUtility.ParseCity(Addr);
                                        if(addresscityid == -1)
                                        {
                                            logError(mdbfile, studentno, studentname, "学生（"+studentname+"）的"+title+"现居住地没有找到");
                                        }


                                        //现居住地公安局地址
                                        cell1 = wsRow[rowNum, 12]; //现居住地公安局地址
                                        string PoliceAddr = cell1.Text.Trim();

                                        //工作单位
                                        cell1 = wsRow[rowNum, 13]; //工作单位
                                        string unit = cell1.Text.Trim();

                                        //职务
                                        cell1 = wsRow[rowNum, 14]; //职务
                                        string position = cell1.Text.Trim();
                                        person.Position = position;

                                        //是否有公职
                                        cell1 = wsRow[rowNum, 15]; //是否有公职
                                        string ispublic = cell1.Text.Trim();
                                        if(ispublic == "是")
                                        {
                                            person.Ispublic = 1;
                                        }
                                        else
                                        {
                                            person.Ispublic = 0;
                                        }

                                        //备注
                                        cell1 = wsRow[rowNum, 16]; //备注
                                        string remark = cell1.Text.Trim();
                                        person.Remark = remark;
                                        person.Sex = "";
                                        person.Url = path;

                                        person.Isreal = 1;
                                        person.Addressorder= 1;
                                        person.Realid = -1;

                                        int hjrealid  = person.save(mdbfile);

                                        if (cardcityid > 0)
                                        {
                                            // 把cardcityid的这条记录拷贝到本地数据库中
                                            CityUtility.CopytoLocalCity(mdbfile, cardcityid);
                                        }

                                        if (addresscityid > 0)
                                        {
                                            CityUtility.CopytoLocalCity(mdbfile, addresscityid);
                                        }

                                        if (Addr.Trim().Length != 0) //有经常居住地
                                        {
                                            //经常居住地地址
                                            PersonClue jc = person.clone();
                                            jc.Addresstype = "经常居住地";
                                            
                                            jc.Cityid = addresscityid;
                                            

                                            jc.Address = Addr;

                                            if (addresscityid <= 1)
                                            {
                                                jc.Cityid = CityUtility.ParseCity(Addr);
                                            }

                                            jc.Addressorder = 2;
                                            jc.Isreal = 1;
                                            jc.Policeaddress = PoliceAddr;
                                            int jcrealid = jc.save(mdbfile);

                                            PersonClue gz = person.clone();
                                            gz.Addresstype = "工作单位及职务";
                                            gz.Address = unit;
                                            gz.Policeaddress = PoliceAddr;
                                            gz.Cityid = addresscityid;
                                            if (addresscityid <= 1)
                                            {
                                                jc.Cityid = CityUtility.ParseCity(unit);
                                            }

                                            gz.Addressorder = 3;
                                            gz.Isreal = 1;
                                            int gzrealid = gz.save(mdbfile);


                                            //经常居住地写入户籍所在地的地址
                                            PersonClue hj_to_jc = person.clone();
                                            hj_to_jc.Addresstype = "经常居住地";
                                            hj_to_jc.Addressorder = 2;
                                            hj_to_jc.Address = Addr;
                                            hj_to_jc.Policeaddress = PoliceAddr;
                                            if(addresscityid != cardcityid)
                                            {
                                                hj_to_jc.Realid = jcrealid;
                                                hj_to_jc.Isreal = 0;
                                            }
                                            hj_to_jc.save(mdbfile);

                                            //工作单位及职务写入户籍所在地的地址

                                            PersonClue gz_to_hj = person.clone();
                                            gz_to_hj.Addresstype = "工作单位及职务";
                                            gz_to_hj.Address = unit;
                                            gz_to_hj.Addressorder = 3;
                                            gz_to_hj.Policeaddress = PoliceAddr;
                                            if (addresscityid != cardcityid)
                                            {
                                                gz_to_hj.Isreal = 0;
                                                gz_to_hj.Realid = gzrealid;
                                            }
                                            gz_to_hj.save(mdbfile);

                                            //户籍所在地写入经常居住地的地址
                                            PersonClue jc_to_hj = person.clone();
                                            jc_to_hj.Cityid = addresscityid;
                                            
                                            if (addresscityid != cardcityid)
                                            {
                                                jc_to_hj.Isreal = 0;
                                                jc_to_hj.Realid = hjrealid;
                                            }
                                            jc_to_hj.save(mdbfile);



                                        }
                                        else //没有经常居住地
                                        {
                                            if(unit.Length != 0) //有工作单位
                                            {
                                                PersonClue otherperson1 = person.clone();
                                                otherperson1.Addresstype = "工作单位及职务";
                                                otherperson1.Address = unit;
                                                otherperson1.Policeaddress = cardPoliceAddr;
                                                otherperson1.Cityid = cardcityid;
                                                otherperson1.Addressorder = 3;
                                                otherperson1.Isreal= 1;
                                                otherperson1.Realid= -1;
                                                otherperson1.save(mdbfile);

                                            }

                                        }

                                        
                                     

                                    }
                                }
                            }

                       }

                    /*
                        for (var rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                            var row = dataTable.Rows.Add();
                            foreach (var cell in wsRow)
                            {

                                row[cell.Start.Column - 1] = cell.Text;
                            }
                        }*/
                       // dataset.Tables.Add(dataTable);
                    }
                    
                }

                if (!success)
                {
                    // 没有找到线索表或者政治考核线索表
                    LogUtility.Log(path+"中没有找到线索表或者政治考核线索表");
                    //dataset.Tables.Add(dataTable);
                }
                // 现在，dataset 包含了Excel文件的所有数据
            }
            fileInfo.Close();
        }
    }
}
