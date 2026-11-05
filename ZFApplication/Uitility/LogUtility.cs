using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Uitility
{
    public static class LogUtility
    {

        public static string mBaseUrl = "";
        private const int MaxLines = 10000;
        private static LinkedList<string> _lines = new LinkedList<string>();
        public static string mCurrentFileName = "";
        public static readonly string dateformat = "yyyy-MM-dd";

        public static void init(string baseurl)
        {
            mBaseUrl = baseurl;

            //读之前的log文件
            DateTime now = DateTime.Now;
            string filename = "Log//" + now.ToString(dateformat) + ".log";
            string filePath = Path.Combine(mBaseUrl, filename);
            if (File.Exists(filePath))
            {
                mCurrentFileName = filePath;
                _lines = new LinkedList<string>(File.ReadAllLines(filePath));
            }

            // mWriter = new StreamWriter(baseurl + "log.log", true);
        }

        public static void AddLine(string line)
        {

            _lines.AddFirst(line);
            while (_lines.Count > MaxLines)
            {
                _lines.RemoveLast();
            }
        }

        public static void SaveToFile(string _filePath)
        {
            try
            {
                using (var writer = new StreamWriter(_filePath, false)) // false means overwrite
                {
                    foreach (var line in _lines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public static void Log(string str)
        {
            // return;
            //LinkLog(str);\
            Console.WriteLine(str);
            FlushLog(str);
        }

        public static void FlushLog(string str)
        {
            DateTime now = DateTime.Now;
            string filename = "Log//" + now.ToString(dateformat) + ".log";
            string filePath = Path.Combine(mBaseUrl, filename); // 构造文件路径

            string head = now.ToString("yyyy-MM-dd HH:mm:ss");
            string line = head + " : " + str;

            try
            {
                using (var writer = new StreamWriter(filePath, true)) // false means overwrite
                {
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }
            catch (Exception)
            {

            }

        }

        //日期从前到后
        public static void LinkLog(string str)
        {
            // return;

            DateTime now = DateTime.Now;
            string filename = "Log//" + now.ToString(dateformat) + ".log";
            string filePath = Path.Combine(mBaseUrl, filename); // 构造文件路径

            if (mCurrentFileName != filePath)
            {
                mCurrentFileName = filePath;
                _lines.Clear();
            }

            string head = now.ToString("yyyy-MM-dd HH:mm:ss");
            string line = head + " : " + str;
            AddLine(line);
            SaveToFile(filePath);
        }

        public static void close()
        {
        }
    }
}
