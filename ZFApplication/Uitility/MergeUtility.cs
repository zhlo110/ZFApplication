using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ClipboardSource.SpreadsheetML;
using ZFApplication.Database;
using ZFApplication.Listeners;

namespace ZFApplication.Uitility
{
    public class MergeUtility
    {
        public static void merge(string dest,string ori, ProgressBarListeners listener)
        {
            if (File.Exists(dest) && File.Exists(ori))
            {
                listener.settotalnumber(3);//公共拷贝3个表
                listener.settotalcurrent(0,"合并City表");//City表

                string tablename = "City";
                string[] columns = new string[] { "ID", "ParentID", "Name", "ShortName" };
                string[] types = new string[] { "int", "int", "string", "string" };
                copyTable(dest, ori, tablename, columns, types, listener);

                listener.settotalcurrent(1, "合并Problem表");//合并Problem表
                tablename = "Problem";
                columns = new string[] { "StudentNo", "StudentName", "Description"};
                types = new string[] { "string", "string", "string" };
                copyTable(dest, ori, tablename, columns, types, listener);

                listener.settotalcurrent(2, "合并Person表");//Person
                tablename = "Person";
                columns = new string[] { "No", "CityID", "ObjName", "Title", "Sex", "Name", 
                                            "Nation", "Political", "Marriage", "IDCard", "PhoneNum", 
                                            "AddrType", "Address", "AddressOrder", "PoliceAddr", 
                                            "Remark", "Position", "IsPublic", "IsVisit", "IsDelete",
                                            "IsReal", "RealID", "OrderNo", "HasError", "Url" };
                types = new string[] { "string", "int", "string", "string", "string", "string",//25
                                        "string", "string", "string", "string", "string",
                                        "string", "string", "int","string",
                                        "string", "string", "int", "int", "int",
                                        "int", "int", "int", "int", "string" };//25
                //  copyproblem(dest, ori, listener);
                copyTable(dest, ori, tablename, columns, types, listener);
                listener.progressend();
            }
        }

        private static string changeurl(string workspace,string url)
        {
            string result = url;
            string filename = Path.GetFileName(url);
            if(workspace.EndsWith("\\"))
                result = workspace + filename;
            else
                result = workspace +"\\"+ filename;

            return result;

        }

        private static void copyTable(string dest, string ori,string tablename,string[] columns, string[] types,ProgressBarListeners listener)
        {
            string workspace = SystemUtility.GetSystemValues("WorkplaceFolder");
            string sql = "select count([ID]) from "+tablename;
            IList list = new ArrayList();
            int nCount = AccessDB.QueryStatistic(ori, sql,list);
            string columnsstr = "";
            foreach (string column in columns)
            {
                columnsstr += "[" + column + "],";
            }
            if (columnsstr.Length > 0)
            {
                columnsstr = columnsstr.Substring(0, columnsstr.Length - 1);
            }

            if (nCount > 0 && columnsstr.Length > 0)
            {
                listener.setsubnumber(nCount);
                sql = "select "+ columnsstr + " from "+tablename;
                list.Clear();
                DataSet ds = AccessDB.Query(ori, sql, list);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null)
                        {
                            int k = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                listener.setsubcurrent(k);
                                int jCount = columns.Length;
                                list.Clear();
                                string wherecondition = "";
                                string insertvalue = "";
                                for (int j = 0; j < jCount; j++)
                                {
                                    string column = columns[j];
                                    string type = types[j];
                                    if (type == "int")
                                    {
                                        int value = DatabaseUtility.getIntValue(dr, column, -1);
                                        list.Add(new DictionaryEntry("@" + column, value));
                                    }
                                    else if (type == "string")
                                    {
                                        string value = DatabaseUtility.getStringValue(dr, column);
                                        if(column == "Url")
                                        {
                                            value = changeurl(workspace, value);
                                        }
                                        list.Add(new DictionaryEntry("@" + column, value));
                                    }
                                    wherecondition += "[" + column + "]=@" + column + " and ";
                                    insertvalue += "@" + column + ",";
                                }
                                if (wherecondition.Length > 0)
                                {
                                    wherecondition = wherecondition.Substring(0, wherecondition.Length - 4);
                                }
                                if (insertvalue.Length > 0)
                                {
                                    insertvalue = insertvalue.Substring(0, insertvalue.Length - 1);
                                }
                                sql = "select count([ID]) from " + tablename + " where " + wherecondition;
                                int nCount1 = AccessDB.QueryStatistic(dest, sql, list);
                                if (nCount1 == 0)
                                {
                                    sql = "insert into " + tablename + " (" + columnsstr + ") values(" + insertvalue + ")";
                                    AccessDB.NoQuery(dest, sql, list);
                                }
                                k++;
                            }
                        }
                    }
                }
            }
        }
    }
}
