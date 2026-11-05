using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Mvvm.Native;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZFApplication.Database;

namespace ZFApplication.Uitility
{
    public class CityUtility
    {
        public static string getRightCityName(string input)
        {
            //  string input = "红河州弥勒市";
            if(input.Length <= 3)
            {
                return input;
            }
            string result = "";
            // 匹配这些区域性字
            string pattern = @"(州|盟|旗|区|县|市)";

            // 查找第一个匹配的字
            System.Text.RegularExpressions.Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                // 获取第一个匹配的字
                string regionPart = match.Value;
                int regionPos = match.Index;

                // 截取到该字的位置
                result = input.Substring(0, regionPos + regionPart.Length);

               // Console.WriteLine($"截取到的字符串: {result}");
            }
            else
            {
                result = input;
            //    Console.WriteLine("没有找到匹配的区域性字");
            }
            return result;
        }   

        public static int ParseCityWithoutProvince(string str)
        {
            int cityid = -1;
            string addr = str.Trim(); 
                //有些记录没有省
           // 成都市青羊区清水滨河路104号1栋2单元16楼1号
           string regexPattern = SystemUtility.GetSystemValues("cityRegex");
           System.Text.RegularExpressions.Match match = Regex.Match(addr, regexPattern);
           if (match.Success)
           {
               string city = match.Groups["city"].Value.Trim();
               city = getRightCityName(city);
               Console.WriteLine("市：" + city);
               int citylength = 0;
               int regionid = GetCitywithoutparent(city, out citylength);
               if (regionid >= 0 && addr.Length - citylength > 0)
               {
                   addr = addr.Substring(citylength, addr.Length - citylength);
                   //    regexPattern = @"(?<district>[^区]+区|[^县]+县|[^市]+市)";
                   //   match = Regex.Match(addr, regexPattern);
                   // regexPattern = @"(?<district>[^市]+市|[^自治州]+自治州|
                   //    [^州]+州|[^盟]+盟|[^旗]+旗|[^县]+县|[^特别行政区]+特别行政区|[^区]+区)";
                   regexPattern = SystemUtility.GetSystemValues("districtRegex");
                   match = Regex.Match(addr, regexPattern);
                   if (match.Success)
                   {
                       string district = match.Groups["district"].Value.Trim();
                       district = getRightCityName(district);
                       Console.WriteLine("县或区：" + district);
                       int districtlength = 0;
                       int districtid = GetCity(regionid, district, out districtlength);
                       if (districtid >= 0)
                       {
                           cityid = districtid;
                       }
                       else
                       {
                           cityid = regionid;
                           LogUtility.Log("地址：'" + str + "'没有解析到县级单位");
                       }
                   }
                   else
                   {
                       cityid = regionid;
                       LogUtility.Log("地址：'" + str + "'没有解析到县级单位");
                   }
               }
               else
               {
                   LogUtility.Log("地址：'" + str + "'没有解析到区级单位");
               }
           }
           else
           {
               LogUtility.Log("地址：'" + str + "'没有解析到区级单位");
           }

            return cityid;
           //LogUtility.Log("地址：'" + str + "'没有解析到省级单位");
            
        }

        private static bool getotherprovince(string str,out string provincename,out int length)
        {
            bool success = false;
            provincename = "";
            length = 0;
            string[] strings = new string[] { "宁夏", "新疆", "西藏", "香港", "澳门", "香港", "澳门","台湾", "黑龙江","四川","重庆","云南","贵州" };
            foreach (string s in strings)
            {
                if (str.IndexOf(s) == 0)
                {
                    success = true;
                    provincename = s;
                    length = s.Length;
                    break;
                }
            }
            return success;
        }
        public static int ParseCity(string str)
        {
         //   string 重庆市开州区三汇口乡小江村1组100号
            int cityid = -1;
     //       str = "新疆伊犁州伊宁市开发区上海路公园大地25-2-501";
            Console.WriteLine("原始地址：" + str);
            string[] vec = str.Split(new char[] {':','：'}, StringSplitOptions.RemoveEmptyEntries);
            if(vec.Length > 0)
            {
                str = vec[vec.Length - 1];
            }

            //getRightCityName("贵州省");
            //getRightCityName("重庆市");


            //str = "贵州省遵义市桐梓县官仓镇小何村联合组";
            // str = "重庆市武隆区羊角街道碑垭村大土组15号";
            string addr = str.Trim();
            Console.WriteLine("地址：" + addr);

            //正则表达式

            //     string regexPattern = @"(?<province>[^省]+省|[^自治区]+自治区|.*重庆市|.*北京市|
            //                           .*上海市|.*天津市|.*重庆|.*北京|.*上海|.*天津|.*广西|.*内蒙古|.*宁夏|.*新疆|.*西藏|.*香港|.*澳门|.*香港特别行政区|.*澳门特别行政区)";
            string regexPattern = SystemUtility.GetSystemValues("provinceRegex");
            if (!String.IsNullOrEmpty(addr))
            {
                System.Text.RegularExpressions.Match match = Regex.Match(addr, regexPattern);
                //bool success = false;
                //string brief_provincename = "";
               // int brief_provincelength = 0;
                
               // success = getotherprovince(addr, out  brief_provincename, out  brief_provincelength);
                

                if (match.Success)
                {
                    string province = match.Groups["province"].Value.Trim();
                    
                    //     province = getRightCityName(province);
                    int provincelength = 0;
                    int provinceid = GetCity(0, province,out provincelength);
                

                    Console.WriteLine("省：" + province);
                    if (provinceid >= 0 && addr.Length - provincelength > 0)
                    {
                        addr = addr.Substring(provincelength, addr.Length - provincelength);
                        // regexPattern = @"(?<city>[^市]+市|[^自治州]+自治州|
                        //            [^州]+州|[^盟]+盟|[^旗]+旗|[^县]+县|[^特别行政区]+特别行政区|[^区]+区)";
                        regexPattern = SystemUtility.GetSystemValues("cityRegex");
                        match = Regex.Match(addr, regexPattern);
                        if (match.Success)
                        {
                            string city = match.Groups["city"].Value.Trim();
                            city = getRightCityName(city);
                            Console.WriteLine("市：" + city);
                            int citylength = 0;
                            int regionid = GetCity(provinceid, city, out citylength);
                            if (regionid >= 0 && addr.Length - citylength > 0)
                            {
                                addr = addr.Substring(citylength, addr.Length - citylength);
                                //    regexPattern = @"(?<district>[^区]+区|[^县]+县|[^市]+市)";
                                //   match = Regex.Match(addr, regexPattern);
                                // regexPattern = @"(?<district>[^市]+市|[^自治州]+自治州|
                                //    [^州]+州|[^盟]+盟|[^旗]+旗|[^县]+县|[^特别行政区]+特别行政区|[^区]+区)";
                                regexPattern = SystemUtility.GetSystemValues("districtRegex");
                                match = Regex.Match(addr, regexPattern);
                                if (match.Success)
                                {
                                    string district = match.Groups["district"].Value.Trim();
                                    district = getRightCityName(district);
                                    Console.WriteLine("县或区：" + district);
                                    int districtlength = 0;
                                    int districtid = GetCity(regionid, district, out districtlength);
                                    if (districtid >= 0)
                                    {
                                        cityid = districtid;
                                    }
                                    else
                                    {
                                        cityid = regionid;
                                        LogUtility.Log("地址：'" + str + "'没有解析到县级单位");
                                    }
                                }
                                else
                                {
                                    cityid = regionid;
                                    LogUtility.Log("地址：'" + str + "'没有解析到县级单位");
                                }
                            }
                            else
                            {
                                LogUtility.Log("地址：'" + str + "'没有解析到区级单位");
                            }
                        }
                        else
                        {
                            LogUtility.Log("地址：'" + str + "'没有解析到区级单位");
                        }
                    }
                    else //没有省
                    {
                        //有些记录没有省
                        // 成都市青羊区清水滨河路104号1栋2单元16楼1号
                        return ParseCityWithoutProvince(str);
                    }

                }
                else //没有省
                {
                    return ParseCityWithoutProvince(str);
                }
            }

            

            return cityid;
        }

        public static string getupdatelist()
        {
            string content = "";
            string mdbfile = SystemUtility.GetSystemValues("FilePath");
            string sql = "select [CityID] from UpdateList order by [CityID]";
            IList list = new ArrayList();
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if(ds != null) 
            {
                
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            int cityid = DatabaseUtility.getIntValue(row, "CityID", -2);
                            if(cityid > -2)
                            {
                                content += cityid.ToString() + ",";
                            }
                        }
                    }
                }
            }
            if(content.Length > 0)
            {
                content = content.Substring(0, content.Length - 1);
            }

            return content;
        }
        public static void CopytoLocalCity(string mdbfile,int id)
        {
            string sysdb = "./ds/City.mdb";
            string sql = "select [ID],[ParentID],[Name],[ShortName] from City where ID=@id";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@id", id));
            DataSet ds = AccessDB.Query(sysdb, sql, list);
            if(ds != null)
            {
                DataTable dt = ds.Tables[0];
                foreach(DataRow row in dt.Rows)
                {
                    int ID = DatabaseUtility.getIntValue(row, "ID", -1);
                    int parentid = DatabaseUtility.getIntValue(row, "ParentID", -1);
                    string name = DatabaseUtility.getStringValue(row, "Name");
                    string shortname = DatabaseUtility.getStringValue(row, "ShortName");
                    if(ID >= 0)
                    {
                        sql = "select count(ID) from City where ID=@id";
                        list.Clear();
                        list.Add(new DictionaryEntry("@id", ID));
                        int count = AccessDB.QueryStatistic(mdbfile, sql, list);
                        if(count == 0)
                        {
                            sql = "insert into City([ID],[ParentID],[Name],[ShortName]) values(@id,@pid,@name,@shortname)";
                            list.Clear();
                            list.Add(new DictionaryEntry("@id", ID));
                            list.Add(new DictionaryEntry("@pid", parentid));
                            list.Add(new DictionaryEntry("@name", name));
                            list.Add(new DictionaryEntry("@shortname", shortname));
                            AccessDB.NoQuery(mdbfile, sql, list);
                        }
                        if(parentid > 0)
                        {
                            CopytoLocalCity(mdbfile, parentid);
                        }
                    }
                    
                }
            }

        }

        public static string GetCityName(string mdbfile, int cityid)
        {
            string result = "";
            string sql = "select [Name] from City where ID=@id";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@id", cityid));
            result = AccessDB.QueryString(mdbfile, sql, list);
            return result;
        }

        public static string GetProvinceName(string mdbfile,int cityid)
        {
            string result = "";
            //获取根节点的ID
            int id = cityid;
            int parentid = -1;
            while(id > 0)
            {
                string sql = "select [ParentID] from City where ID=@id";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@id", id));
                parentid = id;
                id = AccessDB.QueryStatistic(mdbfile, sql, list);
            }
            if(parentid > 0)
            {
                string sql = "select [Name] from City where ID=@id";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@id", parentid));
                result = AccessDB.QueryString(mdbfile, sql, list);
            }
            
            return result;
        }

        //获取第二级名称（省下面的名称）
        public static string GetSecondLevelName(string mdbfile, int cityid)
        {
            string result = "";
            //获取根节点的ID
            int id = cityid;
           // int parentid = -1;
            IList Zonelist = new ArrayList();
            Zonelist.Add(cityid);
            while (id > 0)
            {
                string sql = "select [ParentID] from City where ID=@id";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@id", id));
                id = AccessDB.QueryStatistic(mdbfile, sql, list);
                Zonelist.Add(id);
            }
            if (Zonelist.Count >= 3)
            {
                string sql = "select [Name] from City where ID=@id";
                IList list = new ArrayList();
                list.Add(new DictionaryEntry("@id", Zonelist[Zonelist.Count-3]));
                result = AccessDB.QueryString(mdbfile, sql, list);
            }

            return result;
        }
        //获取parentid,根据当前id
        public static int GetParentID(string mdbfile, int cityid)
        {
            string sql = "select top 1 [ParentID] from City where ID=@id";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@id", cityid));
            return AccessDB.QueryStatistic(mdbfile, sql, list);
        }
        public static string[] GetALLRegionName(string mdbfile, int cityid)
        {
            List<string> resultList = new List<string>();
            string sql = "select [ParentID],[Name] from City where ID=@id";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@id", cityid));
            DataSet ds = AccessDB.Query(mdbfile, sql, list);
            if(ds != null)
            {
                DataTable dt = ds.Tables[0];
                foreach(DataRow row in dt.Rows)
                {
                    int parentid = DatabaseUtility.getIntValue(row, "ParentID", -1);
                    string name = DatabaseUtility.getStringValue(row, "Name");
                    resultList.Insert(0, name);
                    if(parentid > 0)
                    {
                        string parentname = GetCityName(mdbfile, parentid);
                        resultList.Insert(0, parentname);
                        parentid = GetParentID(mdbfile, parentid);
                        if(parentid > 0)
                        {
                            string parentname2 = GetCityName(mdbfile, parentid);
                            resultList.Insert(0, parentname2);
                        }
                    }
                }
            }
            return resultList.ToArray();
        }

        public static bool getCityRelationship(int parentid,int cityid,IList list)
        {
            list.Add(cityid);
            string sql = "select [ParentID] from City where ID=@id";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@id", cityid));
            int pid = AccessDB.QueryStatistic("./ds/City.mdb", sql, list1);
            if (pid == parentid)
            {
                return true;
            }
            else if (pid > 0)
            {
                return getCityRelationship(parentid, pid, list);
            }
            else
            {
                return false;
            }
        }

        public static int GetCitywithoutparent(string str, out int length)
        {
            //不能解析到省
            int result = -1;
            string sql = "select [ParentID],[ID],[Name],[ShortName] from City";
            IList list = new ArrayList();
           // list.Add(new DictionaryEntry("@pid", parentid));
            DataSet ds = AccessDB.Query("./ds/City.mdb", sql, list);
            length = str.Length;
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    int id = DatabaseUtility.getIntValue(row, "ID", -1);
                    int parentid = DatabaseUtility.getIntValue(row, "ParentID", -1);
                    string name = DatabaseUtility.getStringValue(row, "Name");
                    
                    string shortname = DatabaseUtility.getStringValue(row, "ShortName");
                    if (parentid > 0)
                    {
                        //  if (str.IndexOf(shortname) >= 0 || str.IndexOf(name) >= 0)
                        if (shortname.IndexOf(str) >= 0 || name.IndexOf(str) >= 0 || str.IndexOf(shortname) >= 0 || str.IndexOf(name) >= 0)
                        {
                            result = id;
                            break;
                        }
                    }
                }
            }
            if(result > 0)
            {
                //必须是第二级
                //IList relist = new ArrayList();
                //str的结尾是市
                if(!str.EndsWith("市"))
                {
                    result = -1;
                }
            }

            //

            return result;
        }

        public static int GetCity(int parentid,string str,out int length) 
        {
            int result = -1;
            string sql = "select [ID],[Name],[ShortName] from City where ParentID=@pid";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@pid", parentid));
            DataSet ds = AccessDB.Query("./ds/City.mdb",sql,list);
            length = str.Length;
            if(ds != null)
            {
                DataTable dt = ds.Tables[0];
                foreach(DataRow row in dt.Rows)
                {
                    int id = DatabaseUtility.getIntValue(row, "ID",-1);
                    string name = DatabaseUtility.getStringValue(row, "Name");
                    string shortname = DatabaseUtility.getStringValue(row, "ShortName");
                    if (shortname.IndexOf(str) >= 0 || name.IndexOf(str) >= 0 || str.IndexOf(shortname) >= 0 || str.IndexOf(name) >= 0)
                    {
                        result = id;
                        break;
                    }
                }
            }
            if(result == -1)//没有找到，肯能是下个节点
            {
                //陕西省清涧县店则沟镇崔家岔村三组03号，没有区或者市，可能隔了几个级别
                sql = "select [ID],[Name],[ShortName] from City where INSTR(@strname,ShortName)>0";
                list.Clear();
                list.Add(new DictionaryEntry("@strname", str));
                ds = AccessDB.Query("./ds/City.mdb", sql, list);
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        int id = DatabaseUtility.getIntValue(row, "ID", -1);
                     //   string name = DatabaseUtility.getStringValue(row, "Name");
                     //   string shortname = DatabaseUtility.getStringValue(row, "ShortName");
                        if(id > 0) // 查找于parentid是否有关系
                        {
                            IList relist = new ArrayList();
                            if(getCityRelationship(parentid,id,relist))
                            {
                                if(relist.Count>1)
                                {
                                    result = (int)relist[relist.Count-1];
                                    break;
                                }
                               // result = id;
                                
                            }
                        }
                    }
                }
                length = 0;

            }

            return result;
        }
    }
}
