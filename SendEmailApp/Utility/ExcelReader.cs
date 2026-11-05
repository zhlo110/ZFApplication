using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using SendEmailApp.model;
//using OfficeOpenXml;
namespace SendEmailApp.Utility
{
    public class ExcelReader
    {


        /*
                public static string[] FindModifiedXlsxFiles(string directoryPath, string time,
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
                        if (file.FullName.Contains("~$"))
                        {
                            continue;
                        }


                        // 检查文件的最后修改时间
                        DateTime dt = file.CreationTime > file.LastWriteTime ? file.CreationTime : file.LastWriteTime;

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
                }*/



        

        public static IList readexcel(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            IList retlist = new ArrayList();
            FileStream fileInfo = null;
            try
            {
                fileInfo = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                // LogUtility.Log($"正在读文件：'" + path + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件'"+path+"'正在使用，请先关闭！");
                return retlist;
            }
            using (var package = new ExcelPackage(fileInfo))
            {
                var workbook = package.Workbook;
                var dataset = new DataSet();
                bool success = false;
                foreach (var worksheet in workbook.Worksheets)
                {
                    string sheetname = worksheet.Name.Trim();
                    if (sheetname == "线索表" || sheetname == "政治考核线索表")
                    {
                        success = true;
                        //
                        //var dataTable = new DataTable(worksheet.Name);
                       // listeners.setsubnumber(worksheet.Dimension.End.Row);
                        if (worksheet.Dimension.End.Row >= 7)
                        {
                            var rowNum = 3;//读本人的
                          //  listeners.setsubcurrent(4);
                            var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                            var cell1 = wsRow[rowNum, 2];
                            string studentname = "";
                            string studentno = "";

                            if (cell1.Text.Trim().Length > 0)
                            {
                                PersonClue person = new PersonClue();
                                string filename = Path.GetFileName(path);
                                string[] names = filename.Split('-');
                                if (names.Length > 0)
                                {
                                    studentno = names[0].Trim();
                                    person.No = names[0].Trim();
                                }

                                person.Title = "本人";
                                person.OrderNo = 0;
                                string name = cell1.Text.Trim(); //姓名

                                cell1 = wsRow[rowNum, 3];
                                string IDCard = cell1.Text.Trim(); //身份证
                                person.Idcard = IDCard;

                                cell1 = wsRow[rowNum, 4];
                                string othername = cell1.Text.Trim(); //曾用名

                                if (othername != null && othername.Length > 0 && othername != "无" && othername != "无曾用名" && othername != "无曾用名")
                                {
                                    name += "\r\n" + othername + "";
                                }
                                studentname = name;
                                person.Objname = name;
                                person.Name = name;

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

                                cell1 = wsRow[rowNum, 10];
                                string cardPoliceAddr = cell1.Text.Trim(); //户口公安局地址
                                person.Policeaddress = cardPoliceAddr;
                                
                                person.Marriage = "未婚"; //婚姻状况



                                cell1 = wsRow[rowNum, 11];
                                string Addr = cell1.Text.Trim(); //经常居住地

                                cell1 = wsRow[rowNum, 12];
                                string PoliceAddr = cell1.Text.Trim(); //经常居住地公安局


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
                                person.Url = path; //文件路径

                                if (!SystemUtility.contentExcept(cardAddr))
                                {
                                    person.Address = cardPoliceAddr;
                                    person.Policeaddress = cardAddr;
                                    retlist.Add(person);
                                }

                                if (!SystemUtility.contentExcept(Addr))
                                {
                                    //如果现居住地和户籍所在地不一样，添加
                                    if (cardPoliceAddr.Trim() != PoliceAddr.Trim())
                                    {
                                        PersonClue jc = person.clone();
                                        jc.Address = PoliceAddr;
                                        jc.Policeaddress = Addr;
                                        retlist.Add(jc);
                                    }
                                }


                                

                            }


                            if (studentname.Length > 0 && studentno.Length > 0)
                            {
                                rowNum = 7; //读亲属的
                                for (; rowNum < worksheet.Dimension.End.Row; rowNum++)
                                {
                                    wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                                    
                                   

                                    cell1 = wsRow[rowNum, 1];
                                    if (cell1.Text.Trim().Length > 0)
                                    {
                                        PersonClue person = new PersonClue();
                                        person.IsVisit = 0;
                                        person.Isdelete = 0;
                                        person.Haserror = 0;
                                        person.No = studentno;//1、线索编号

                                        string title = cell1.Text.Trim();//称谓不能为
                                        if (title.Length > 5)
                                        {
                                            //需要说明的问题等，不操作
                                            continue;
                                        }

                                        if (title.Length == 0)
                                        {
                                         //   logError(mdbfile, studentno, studentname, "亲属称谓为空");
                                          //  continue;
                                        }
                                        person.Objname = studentname;//2、对象名称,考生名称
                                        person.Title = title; //3、称谓


                                        person.OrderNo = TitleOrderUtility.getOrder(title);
                                        cell1 = wsRow[rowNum, 2]; //姓名
                                        string name = cell1.Text.Trim(); //姓名 必须的，没有是错的

                                        if (name.Length == 0)
                                        {
                                            //
                                         //  logError(mdbfile, studentno, studentname, "亲属(" + title + ")姓名为空");
                                           // continue;
                                        }


                                        cell1 = wsRow[rowNum, 3]; //身份证号 没有是错的

                                        string idcard = cell1.Text.Trim(); //姓名
                                        if (idcard.Length == 0)
                                        {
                                           // logError(mdbfile, studentno, studentname, "亲属(" + title + "," + name + ")身份证为空");
                                            //   continue;
                                        }

                                        person.Idcard = idcard; //身份证号


                                        cell1 = wsRow[rowNum, 4]; //曾用名
                                        string othername = cell1.Text.Trim();
                                        if (othername != null && othername.Length > 0 && othername != "无" && othername != "无曾用名" && othername != "无曾用名")
                                        {
                                            name += "\r\n" + othername + "";
                                        }
                                        person.Name = name; //4、姓名


                                        cell1 = wsRow[rowNum, 5]; //婚姻状况  没有是错的
                                        string marriage = cell1.Text.Trim();
                                        if (marriage.Length == 0)
                                        {
                                           // logError(mdbfile, studentno, studentname, "亲属(" + title + "," + name + ")婚姻状况为空");
                                            //  continue;
                                        }
                                        person.Marriage = marriage;//7、婚姻状况

                                        cell1 = wsRow[rowNum, 6]; //政治面貌 没有是错的
                                        string political = cell1.Text.Trim();
                                        if (political.Length == 0)
                                        {
                                           // logError(mdbfile, studentno, studentname, "亲属(" + title + "," + name + ")政治面貌为空");
                                            //     continue;
                                        }
                                        person.Political = political;//6.政治面貌

                                        //民族
                                        cell1 = wsRow[rowNum, 7]; //民族,没有是错的
                                        string nation = cell1.Text.Trim();
                                        if (nation.Length == 0)
                                        {
                                            //logError(mdbfile, studentno, studentname, "亲属(" + title + "," + name + ")民族为空");
                                            //    continue;
                                        }
                                        person.Nation = nation;//、民族

                                        //电话
                                        cell1 = wsRow[rowNum, 8]; //8、电话
                                        string phone = cell1.Text.Trim();
                                        person.Phone = phone;


                                        

                                        //户籍所在地地址
                                        cell1 = wsRow[rowNum, 9]; //户籍所在地地址
                                        string cardAddr = cell1.Text.Trim();
                                        person.Addresstype = "户口所在地";
                                        person.Address = cardAddr;

                                       

                                        //户籍所在地公安局地址
                                        cell1 = wsRow[rowNum, 10]; //户籍所在地公安局地址
                                        string cardPoliceAddr = cell1.Text.Trim();
                                        person.Policeaddress = cardPoliceAddr;
                                        


                                        //现居住地地址
                                        cell1 = wsRow[rowNum, 11]; //现居住地地址
                                        string Addr = cell1.Text.Trim();
                                        // int addresscityid = CityUtility.ParseCity(Addr);
                                        // if (addresscityid == -1)
                                        //{
                                        //  logError(mdbfile, studentno, studentname, "学生（" + studentname + "）的" + title + "现居住地没有找到");
                                        //}

                                        //现居住地公安局地址
                                        cell1 = wsRow[rowNum, 12]; //现居住地公安局地址
                                        string PoliceAddr = cell1.Text.Trim();


                                        //工作单位
                                        cell1 = wsRow[rowNum, 13]; //工作单位
                                        string unit = cell1.Text.Trim();
                                      //  person.Address=unit; //9、工作单位和职务

                                        //职务
                                        cell1 = wsRow[rowNum, 14]; //职务
                                        string position = cell1.Text.Trim();
                                        person.Position = position;
                                        person.Address = unit + position; //9、工作单位和职务

                                        //是否有公职
                                        cell1 = wsRow[rowNum, 15]; //是否有公职
                                        string ispublic = cell1.Text.Trim();
                                        if (ispublic != "是")
                                        {
                                            //continue;
                                        }

                                        //备注
                                        cell1 = wsRow[rowNum, 16]; //10\备注
                                        string remark = cell1.Text.Trim();
                                        person.Remark = remark;
                                        person.Url = path; //文件路径

                                        //cardAddr
                                        if (!SystemUtility.contentExcept(cardAddr))
                                        {
                                            person.Address = cardPoliceAddr;
                                            person.Policeaddress = cardAddr;
                                            retlist.Add(person);
                                        }
                                        if (!SystemUtility.contentExcept(Addr))
                                        {
                                            if (cardPoliceAddr.Trim() != PoliceAddr.Trim())
                                            {
                                                PersonClue jc = person.clone();
                                                jc.Address = PoliceAddr;
                                                jc.Policeaddress = Addr;
                                                retlist.Add(jc);
                                            }
                                        }
                                        if (ispublic == "是")
                                        {
                                            PersonClue gz = person.clone();
                                            gz.Address = unit;
                                            gz.Policeaddress = "";
                                            retlist.Add(gz);
                                        }
                                        //Addr
                                        //ispublic

                                        //  retlist.Add(person);


                                    }
                                }
                            }

                        }

                      
                        // dataset.Tables.Add(dataTable);
                    }

                }

                if (!success)
                {
                    // 没有找到线索表或者政治考核线索表
                    //LogUtility.Log(path + "中没有找到线索表或者政治考核线索表");
                    //dataset.Tables.Add(dataTable);
                }
                // 现在，dataset 包含了Excel文件的所有数据
            }
            fileInfo.Close();
            return retlist;
        }
    }
}
