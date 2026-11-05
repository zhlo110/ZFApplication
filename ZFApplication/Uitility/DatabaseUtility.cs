using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Uitility
{
    public class DatabaseUtility
    {
        public static int getIntValue(DataRow row, string key, int defaultvalue)
        {
            int ret = defaultvalue;
            Object obj = row[key];
            if (obj != null && obj.ToString().Length != 0)
            {
                if (!int.TryParse(obj.ToString(), out ret))
                {
                    ret = defaultvalue;
                }
            }
            return ret;
        }


        public static double getDoubleValue(DataRow row, string key, double defaultvalue)
        {
            double ret = defaultvalue;
            Object obj = row[key];
            if (obj != null && obj.ToString().Length != 0)
            {
                if (!double.TryParse(obj.ToString(), out ret))
                {
                    ret = defaultvalue;
                }
            }
            return ret;
        }

        //得到里程标准格式 dk100+500
        //
        public static string getMileageText(string prefix, double value)
        {
            //得到千位
            double kilometer = value / 1000.0;
            int inkilo = (int)kilometer;
            double sub = value - inkilo * 1000; //百里范围内
            return prefix + inkilo.ToString() + "+" + sub.ToString("f2");
        }


        public static string getDatetimeValue(DataRow row, string key)
        {
            string ret = "";
            Object obj = row[key];
            if (obj != null && obj.ToString().Length != 0)
            {
                DateTime dt = (DateTime)obj;
                ret = dt.ToString("yyyy年MM月dd日");
            }
            return ret;
        }

        public static string getDatetimeValue(DataRow row, string key, string format)
        {
            string ret = "";
            Object obj = row[key];
            if (obj != null && obj.ToString().Length != 0)
            {
                DateTime dt = (DateTime)obj;
                ret = dt.ToString(format);
            }
            return ret;
        }

        public static string getStringValue(DataRow row, string key)
        {
            string ret = "";
            Object obj = row[key];
            if (obj != null && obj.ToString().Length != 0)
            {
                ret = obj.ToString();
            }
            return ret.Trim();
        }

        public static int stringtoint(string value, int defaultvalue)
        {
            if (value == null)
                value = "";
            int val = defaultvalue;
            if (!int.TryParse(value.Trim(), out val))
            {
                val = defaultvalue;
            }
            return val;
        }

        public static double stringtodouble(string value, double defaultvalue)
        {
            double val = defaultvalue;
            if (!double.TryParse(value.Trim(), out val))
            {
                val = defaultvalue;
            }
            return val;
        }

        //string 角度函数
        public static double palseDegree(string value)
        {
            double ret = -1000.0;
            if (!double.TryParse(value, out ret))
            {
                ret = -1000.0;
                string[] vec = value.Split(new char[] { '°', '′', '″' }, StringSplitOptions.RemoveEmptyEntries);
                if (vec.Length == 3)
                {
                    double degree = 0.0;
                    double minute = 0.0;
                    double second = 0.0;
                    double.TryParse(vec[0], out degree);
                    double.TryParse(vec[1], out minute);
                    double.TryParse(vec[2], out second);
                    ret = degree + minute / 60.0 + second / 3600.0;
                }
            }
            return ret;
        }

        public static string DegreeToString(double value)
        {
            int degree = (int)value;
            double rest = value - degree;
            rest *= 60.0;
            int minute = (int)rest;
            rest = rest - minute;
            rest *= 60.0;
            int second = (int)rest;
            return degree.ToString() + "°" + minute.ToString() + "′" + second.ToString() + "″";
        }

    }
}
