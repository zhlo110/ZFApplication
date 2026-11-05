using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailApp.Utility
{
    public class CheckNewFiles
    {
        private static IList Filelist = new ArrayList();
        public static string currentfile ="";
        public static MainForm mainForm = null;
        public void setMainForm(MainForm form)
        {
            mainForm = form;
        }
        public static string loadonefilefromworkspace()
        {
            string result = "";
            string workspace = SystemUtility.workspace;
            if (workspace.Length == 0 || !Directory.Exists(workspace))
            {
                currentfile = "";
                return result;
            }
            string[] files = System.IO.Directory.GetFiles(workspace, "*.xlsx", System.IO.SearchOption.AllDirectories);
            //将文件名添加到列表框中

         //   Filelist.Clear();
            foreach (string file in files)
            {
                //将file写到列表的Tag中
                // string fileName = System.IO.Path.GetFileName(file);
                if (Filelist.Contains(file))//如果已经存在,则跳过
                {
                    continue;
                }
                Filelist.Add(file);//新增的加载后面
            }
            int nCount = Filelist.Count;
            int i;
            for (i = 0; i < nCount; i++)
            {
                string file = Filelist[i].ToString();
                if (files.Contains(file))
                {
                    continue;
                }
                else
                {
                    Filelist.Remove(file);//删除不存在的文件
                    i--;
                    nCount--;

                }
            }


            if (currentfile.Length == 0) //空值
            {
                if (Filelist.Count > 0)
                {
                    result = Filelist[0].ToString();
                    currentfile = result;
                }
            }
            else
            {
                //说明不是第一次加载,找到Filelist中没有的文件
                nCount = Filelist.Count;
                if (nCount > 0)
                {
                    int index = Filelist.IndexOf(currentfile);
                    if (index >= 0)
                    {
                        index = (index + 1) % nCount;
                        result = Filelist[index].ToString();
                        currentfile = result;
                    }
                    else
                    {
                        //选择第一个文件
                        result = Filelist[0].ToString();
                        currentfile = result;
                    }
                }
                else
                {
                    //说明没有文件了
                    currentfile = "";
                    result = "";
                }
            }
            return result;
        }
        public static string loadonefilefromworkspace1()
        {
            string result = "";
            
            if(Filelist.Count == 0)//说明是第一次加载
            {
                string workspace = SystemUtility.workspace;
                if (workspace.Length == 0 || !Directory.Exists(workspace))
                {
                    currentfile = "";
                    return result;
                }

                //搜索文件夹下的所有xlsx文件
                string[] files = System.IO.Directory.GetFiles(workspace, "*.xlsx", System.IO.SearchOption.AllDirectories);
                //将文件名添加到列表框中
                
                /*
                foreach (string file in files)
                {
                    //将file写到列表的Tag中
                    result = file;
                    break;
                }*/

                foreach (string file in files)
                {
                    //将file写到列表的Tag中
                   // string fileName = System.IO.Path.GetFileName(file);
                    Filelist.Add(file);
                }
            }
            else
            {
                //说明不是第一次加载,找到Filelist中没有的文件
                string workspace = SystemUtility.workspace;
                //搜索文件夹下的所有xlsx文件
                string[] files = System.IO.Directory.GetFiles(workspace, "*.xlsx", System.IO.SearchOption.AllDirectories);
                //将文件名添加到列表框中
                foreach (string file in files)
                {
                    //将file写到列表的Tag中
                    string fileName = System.IO.Path.GetFileName(file);
                    if (!Filelist.Contains(fileName))
                    {
                        result = file;
                        //Filelist.Add(fileName);
                        break;
                    }
                }
                Filelist.Clear();
                foreach (string file in files)
                {
                    //将file写到列表的Tag中
                    string fileName = System.IO.Path.GetFileName(file);
                    if(result.Length == 0)
                    {
                      //  result = file;
                    }
                    Filelist.Add(fileName);
                }


            }
            Console.WriteLine("load file:" + result);
            currentfile = result;
            return result;
        }
    }
}
