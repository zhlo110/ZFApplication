using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SendEmailApp.Utility
{
    public class SystemUtility
    {
        //public static void WriteFolderPathToFile(string filePath, string folderPath)
        public static string workspace = "";
        public static string[] areas = new string[0];

        public static bool contentExcept(string addr)
        {
            if (string.IsNullOrEmpty(addr))
            {
                return false;
            }
            string[] except = areas;
            if (except.Length == 0)
            {
                return false;
            }
            foreach (string item in except)
            {
                if (addr.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        public static void UpdateFolderPathInFile(string filePath, string key, string value)
        {
            try
            {
                // 如果文件不存在，创建文件
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                // 读取现有内容
                string[] lines = File.ReadAllLines(filePath);

                bool folderPathUpdated = false;
                string updatedContent = "";

                foreach (var line in lines)
                {
                    // 如果找到包含 "folderPath:" 的行，更新它
                    if (line.StartsWith(key+":"))
                    {
                        updatedContent += key+$":{value}{Environment.NewLine}";
                        folderPathUpdated = true;
                    }
                    else
                    {
                        updatedContent += line + Environment.NewLine;
                    }
                }

                // 如果文件中没有 folderPath: 行，添加它
                if (!folderPathUpdated)
                {
                    updatedContent += key+$":{value}{Environment.NewLine}";
                }

                // 写回更新后的内容到文件
                File.WriteAllText(filePath, updatedContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }

        public static bool IsFileLocked(string filePath)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // 文件被占用，无法访问
                return true;
            }
            finally
            {
                stream?.Close();
            }

            return false;
        }
        public static string ReadValueFromFile(string filePath, string key)
        {
            try
            {
                // 检查文件是否存在
                if (!File.Exists(filePath))
                {
                    return null; // 如果文件不存在，返回 null
                }

                // 读取文件中的所有行
                string[] lines = File.ReadAllLines(filePath);

                // 遍历每一行，查找包含 key 的行
                foreach (var line in lines)
                {
                    if (line.StartsWith(key + ":"))
                    {
                        // 提取 key 后面的值并返回
                        return line.Substring(key.Length + 1); // 返回冒号后的部分
                    }
                }

                return null; // 如果找不到 key，则返回 null
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
                return null;
            }
        }
    }
    /*
    public delegate void RealIDCallback(string mdbfile, int realid, object newValue);

    public static string trimstring(string name)
    {
        string ret = name;
        if (name != null)
        {
            string[] lines = name.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length >= 2)
            {
                ret = lines[0] + "(" + lines[1] + ")";
            }
        }

        return ret;
    }
    public static void selectRealIDFunction(RealIDCallback callback, string mdbfile, int itemid, object newValue)
    {
        string sql = "";
        sql = "select [IsReal],[RealID] from Person where [ID]=@id";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@id", itemid));
        DataSet ds = AccessDB.Query(mdbfile, sql, list);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    int isreal = DatabaseUtility.getIntValue(dt.Rows[0], "IsReal", -1);
                    int realid = DatabaseUtility.getIntValue(dt.Rows[0], "RealID", -1);
                    if (isreal == 1)// 该条是真实条目
                    {
                        //rowData.ID
                        callback(mdbfile, itemid, newValue);
                    }
                    else if (realid > 0) //假的条目
                    {
                        //realid
                        callback(mdbfile, realid, newValue);
                    }
                    // string realid = DatabaseUtility.getStringValue(dt.Rows[0], "RealID");
                }
            }
        }
    }

    public static string GetSystemValues(string name)
    {
        string sql = "select [Values] from Workplace where Name=@name";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@name", name));
        return AccessDB.QueryString("./ds/City.mdb", sql, list);
    }

    public static string GetSystemTime(string name)
    {
        string sql = "select [TIME] from Workplace where Name=@name";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@name", name));
        return AccessDB.QueryString("./ds/City.mdb", sql, list);
    }
    public static int pagesize = 20;

    public static void calpageresult(int maxRowCount, int startRowIndex, out int endRowIndex, out int batchSize)
    {
        batchSize = pagesize;
        endRowIndex = 0;

        endRowIndex = startRowIndex + batchSize;

        if (endRowIndex > maxRowCount)//最后一页
        {
            batchSize = maxRowCount - startRowIndex;
        }
    }

    public static string needupdate(string projectname)
    {
        string sql = "select [Values] from Workplace where Name=@name";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@name", "MustUpdate"));
        DataSet ds = AccessDB.Query("./ds/City.mdb", sql, list);
        string ret = "false";
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string values = DatabaseUtility.getStringValue(dt.Rows[0], "Values");
                    string[] items = values.Split('#');
                    if (items.Length == 2)
                    {
                        string projectfilevalue = items[0];
                        string update = items[1];
                        if (projectfilevalue != null || projectfilevalue.Trim().Equals(projectname.Trim()))
                        {
                            ret = update;
                        }
                    }

                }
            }
        }
        return ret;
    }

    public static void MustUpdate(string projectfile, string update)
    {
        string sql = "select count(ID) from Workplace where Name=@name";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@name", "MustUpdate"));
        int nCount = AccessDB.QueryStatistic("./ds/City.mdb", sql, list);
        if (nCount > 0)
        {
            sql = "update Workplace set [Values] = @val,[TIME]=@time where [Name] =  @name";
            list.Clear();
            list.Add(new DictionaryEntry("@val", projectfile + "#" + update));
            list.Add(new DictionaryEntry("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            list.Add(new DictionaryEntry("@name", "MustUpdate"));
            AccessDB.NoQuery("./ds/City.mdb", sql, list);
        }
        else
        {
            sql = "insert into Workplace([Name],[Values],[TIME]) values(@name,@val,@time)";
            list.Clear();
            list.Add(new DictionaryEntry("@name", "MustUpdate"));
            list.Add(new DictionaryEntry("@val", projectfile + "#" + update));
            list.Add(new DictionaryEntry("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            AccessDB.NoQuery("./ds/City.mdb", sql, list);
        }

    }
    //移动文件的时候，更新数据库




    public static void writecurrentfile(string mdbfile, string fieldname)
    {
        string localfile = "./ds/City.mdb";
        string sql = "select count(ID) from Workplace where [Name] = @name";
        IList list = new ArrayList();
        list.Add(new DictionaryEntry("@name", fieldname));
        int nCount = AccessDB.QueryStatistic(localfile, sql, list);
        DateTime dt = DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd HH:mm:ss");
        if (nCount == 0)
        {
            list.Clear();
            sql = "insert into Workplace([Name],[Values],[TIME]) values(@name,@val,@time)";
            list.Add(new DictionaryEntry("@name", fieldname));
            list.Add(new DictionaryEntry("@val", mdbfile));
            list.Add(new DictionaryEntry("@time", time));
            AccessDB.NoQuery(localfile, sql, list);
        }
        else if (nCount > 0)
        {
            list.Clear();
            sql = "update Workplace set [Values] = @val,[TIME]=@time where [Name] =  @name";
            list.Add(new DictionaryEntry("@val", mdbfile));
            list.Add(new DictionaryEntry("@time", time));
            list.Add(new DictionaryEntry("@name", fieldname));
            AccessDB.NoQuery(localfile, sql, list);
        }
    }

    */


}
