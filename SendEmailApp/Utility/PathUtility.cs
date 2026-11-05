using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailApp.Utility
{
    public class PathUtility
    {
        public static void setcurrentpath()
        {
            try
            {
                // 获取当前执行的exe文件的完整路径
                string exePath = Assembly.GetExecutingAssembly().Location;

                // 从完整路径中解析出目录路径
                string exeDirectory = Path.GetDirectoryName(exePath);

                // 将工作目录设置为exe文件所在的目录
                Environment.CurrentDirectory = exeDirectory;

                Console.WriteLine($"当前工作目录已设置为: {Environment.CurrentDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"设置工作目录时发生错误: {ex.Message}");
            }
        }
    }
}
