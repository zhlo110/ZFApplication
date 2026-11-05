using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailApp.model
{
    public class FilesList
    {
        public FilesList(string filename, string filepath)
        {
            mfilename = filename;
            mfilepath = filepath;
        }
        string mfilename = "";
        public string getFileName()
        {
            return mfilename;
        }
        public void setFileName(string filename)
        {
            mfilename = filename;
        }
        string mfilepath = "";
        public string getFilePath()
        {
            return mfilepath;
        }
        public void setFilePath(string filepath)
        {
            mfilepath = filepath;
        }
        //重新定义ToString方法
        public override string ToString()
        {
            return mfilename;
        }
    }
}
