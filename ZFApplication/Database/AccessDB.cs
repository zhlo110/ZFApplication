using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFApplication.Uitility;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace ZFApplication.Database
{
    //修改成sqllite
    public class AccessDB
    {

        public static bool hasfile(string mdbfile)
        {
            bool success = false;
            try
            {
                if (File.Exists(mdbfile))
                {
                    success = true;
                } 
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            return success;
        }

        public static int HasCityTable(string mdbFilePath)
        {

            //   string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            int success = 0;
            if (!AccessDB.hasfile(mdbFilePath)) return success;

            // using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // 获取数据库的架构信息，指定获取表的信息
                    DataTable schemaTable = connection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
            
                    // 检查是否存在名为"City"的表
                    bool cityTableExists = schemaTable.Rows.Cast<DataRow>().Any(row => row["TABLE_NAME"].ToString() == "City");
                    if (cityTableExists)
                    {
                        success = 1;
                        LogUtility.Log("数据库中存在'City'表。");
                      //  Console.WriteLine("");
                    }
                    else
                    {
                        success = 0;
                        LogUtility.Log("数据库中不存在'City'表。");
                      //  Console.WriteLine("");
                    }
                }
                catch (Exception ex)
                {
                    success = -1;
                    LogUtility.Log($"发生错误: {ex.Message}");
                 //   Console.WriteLine();
                }
            }
            return success;
        
        }
        public static bool copyfile(string orifile, string destfile)
        {
            if(!Directory.Exists(destfile))
            {
                SQLiteConnection.CreateFile(destfile);
            }
            
            return true;
        }

        public static bool copyfile_bak(string orifile, string destfile)
        {
            bool success = false;
            try
            {
                // 确保源文件存在
                if (File.Exists(orifile))
                {
                    // 使用File.Copy方法复制文件。如果目标文件已存在，将会抛出异常。
                    // 若要覆盖已存在的文件，可将第三个参数设置为true。
                    File.Copy(orifile, destfile, true);
                    LogUtility.Log("文件复制成功");
                    success = true;
                }
                else
                {
                    LogUtility.Log("源文件不存在");
                }
            }
            catch (IOException ioEx)
            {
                LogUtility.Log($"发生I/O错误: {ioEx.Message}");
                // 捕获并处理可能的I/O错误
            }
            catch (Exception ex)
            {
                // 捕获并处理所有其他类型的错误
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            return success;
        }

        public static void gentable(string mdbFilePath)
        {
            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            // string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            try
            {
                // 创建表
                // using (var connection = new OleDbConnection(connectionString))
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"CREATE TABLE Person (
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                No TEXT,
                                CityID INTEGER,
                                ObjName TEXT,
                                Title TEXT,
                                Sex TEXT,
                                Name TEXT,
                                Nation TEXT,
                                Political TEXT,
                                Marriage TEXT,
                                IDCard TEXT,
                                PhoneNum TEXT,
                                AddrType TEXT,
                                Address TEXT,
                                AddressOrder INTEGER,
                                PoliceAddr TEXT,
                                Remark TEXT,
                                Position TEXT,
                                IsPublic INTEGER,
                                IsVisit INTEGER,
                                IsDelete INTEGER,
                                IsReal INTEGER,
                                RealID INTEGER,
                                OrderNo INTEGER,
                                HasError INTEGER,
                                Url TEXT
                            );
                            ";
                 //  command.CommandText = "";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = @"CREATE TABLE City (
                                            ID INTEGER,
                                            ParentID INTEGER,
                                            Name TEXT,
                                            ShortName TEXT
                                        );";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Problem (
                                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                            StudentNo TEXT,
                                            StudentName TEXT,
                                            Description TEXT
                                        )";
                    command.ExecuteNonQuery();


                    command.CommandText = @"CREATE TABLE UpdateList (
                                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                            CityID INTEGER
                                           )";
                    command.ExecuteNonQuery();

                    //[TIME] 文档更新日期，第一次按导入的时间算
                    /*
                    command.CommandText = @"CREATE TABLE ProjectInfo([ID] AUTOINCREMENT,
                                                              [TIME] VARCHAR(50),
                                                              [Other] MEMO)";
                    command.ExecuteNonQuery();*/

                    LogUtility.Log("创建表格成功");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
        }

        

       


        public static void NoQuery(string mdbFilePath, string sql, IList table)
        {
            if (!AccessDB.hasfile(mdbFilePath)) return;

            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            //  string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            try
            {
                // 创建表
                // using (var connection = new OleDbConnection(connectionString))
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //OleDbCommand cmd = new OleDbCommand(sql, connection);
                   // SqliteCommand cmd = new SQLiteCommand(sql, connection);
                    var cmd = new SQLiteCommand(sql, connection);
                    if (table != null)
                    {
                        foreach (DictionaryEntry de in table)
                        {
                            cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            
        }


        public static string QueryString(string mdbFilePath, string sql, IList table)
        {
            string result = "";
            if (!AccessDB.hasfile(mdbFilePath)) return result;
            // string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            try
            {
                // 创建表
                // using (var connection = new OleDbConnection(connectionString))
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //OleDbCommand cmd = new OleDbCommand(sql, connection);
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    if (table != null)
                    {
                        foreach (DictionaryEntry de in table)
                        {
                            cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                        }
                    }
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        result = obj.ToString();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            return result;
        }

        public static int QueryStatistic(string mdbFilePath, string sql, IList table)
        {
            int result = -1;
            if (!AccessDB.hasfile(mdbFilePath)) return result;
            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            //  string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            try
            {
                // 创建表
                // using (var connection = new OleDbConnection(connectionString))
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //OleDbCommand cmd = new OleDbCommand(sql, connection);
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                    if (table != null)
                    {
                        foreach (DictionaryEntry de in table)
                        {
                            cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                        }
                    }
                    object obj = cmd.ExecuteScalar();
                    if (obj == null || !int.TryParse(obj.ToString(), out result))
                    {
                        result = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            return result;
        }

        public static DataSet Query(string mdbFilePath,string sql, IList table)
        {
            if (!AccessDB.hasfile(mdbFilePath)) return null;
            DataSet OleDsExcel = new DataSet();
            string connectionString = $"Data Source={mdbFilePath};Version=3;";
            //  string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
            try
            {
                // 创建表
                // using (var connection = new OleDbConnection(connectionString))
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //   OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, connection);
                    SQLiteDataAdapter OleDaExcel = new SQLiteDataAdapter(sql, connection);

                    SQLiteCommand cmd = OleDaExcel.SelectCommand;
                    if (table != null)
                    {
                        foreach (DictionaryEntry de in table)
                        {
                            cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                        }
                    }
                    
                    OleDaExcel.Fill(OleDsExcel);       
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log($"发生错误: {ex.Message}");
            }
            return OleDsExcel;
        }

        public static void createCiyDb(string mdbFilePath)
        {
            // 数据库文件路径
            //  string mdbFilePath = "C:\\path\\to\\your\\database.mdb";
            if (!AccessDB.hasfile(mdbFilePath)) return;
            int success = HasCityTable(mdbFilePath);
            if (success == 0)
            {
                // 创建数据库的连接字符串
                // 注意：如果文件已存在，则会抛出异常
                //    string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={mdbFilePath}";
                string connectionString = $"Data Source={mdbFilePath};Version=3;";
                try
                {
                    // 创建表
                    //  using (var connection = new OleDbConnection(connectionString))
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        // 定义创建表的SQL命令
                        // 例如，创建一个包含ID和Name字段的表
                        command.CommandText = "CREATE TABLE City(ID AUTOINCREMENT,ParentID INT,Name VARCHAR(255),ShortName VARCHAR(255))";
                        command.ExecuteNonQuery();
                        

                        command.CommandText = "CREATE TABLE Workplace(ID AUTOINCREMENT, Name VARCHAR(255), [Values] MEMO,[TIME] VARCHAR(255))";
                        command.ExecuteNonQuery();

                        LogUtility.Log("创建City表格成功");
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Log($"发生错误: {ex.Message}");
                }
            }
        }
    }
}
