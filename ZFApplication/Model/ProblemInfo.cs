using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFApplication.Database;
using ZFApplication.Uitility;

namespace ZFApplication.Model
{
    public class ProblemInfo
    {
        string studentname = "";
        public string StudentName
        {
            get { return studentname; }
            set { studentname = value; }
        }

        string studentno = "";
        public string StudentNo
        {
            get { return studentno; }
            set { studentno = value; }
        }

        string descriptin = "";
        public string Descriptin
        {
            get { return descriptin; }
            set { descriptin = value; }
        }

        public void save(string mdbfile)
        {
            int id = loadID(mdbfile);
            if (id == -1)
            {
                saveimp(mdbfile);
            }
            else
            {
                updateimp(mdbfile, id);
            }
        }

        public void delete(string mdbfile)
        {
            string sql = "";
            sql = @"delete from Problem where [StudentNo]=@stno and [StudentName]=@name";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@stno", this.studentno));
            list1.Add(new DictionaryEntry("@name", this.studentname));
            AccessDB.NoQuery(mdbfile, sql, list1);
        }

        public void saveimp(string mdbfile)
        {
            string sql = "";
            sql = @"insert into Problem([StudentNo],[StudentName],[Description]) values(@stno,@name,@des)";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@stno", this.studentno));
            list1.Add(new DictionaryEntry("@name", this.studentname));
            list1.Add(new DictionaryEntry("@des", this.descriptin));
            AccessDB.NoQuery(mdbfile, sql, list1);
        }

        public void updateimp(string mdbfile,int id)
        {
            string sql = "";
            string des = loadDescription(mdbfile, id);
            des = des + "\r\n" + this.descriptin;
            sql = @"update Problem set [Description]=@des where [ID]=@id";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@des", des));
            list1.Add(new DictionaryEntry("@id", id));
            AccessDB.NoQuery(mdbfile, sql, list1);
        }
        public string loadDescription(string mdbfile,int id)
        {
            string result = "";
            string sql = "";
            sql = @"select [Description] from Problem where [ID]=@id";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@id", id));
            DataSet ds = AccessDB.Query(mdbfile, sql, list1);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = DatabaseUtility.getStringValue(ds.Tables[0].Rows[0], "Description");
                    }
                }
            }
            return result;
        }

        public int loadID(string mdbfile)
        {
            int id = -1;
            string sql = "";
            sql = @"select [ID],[StudentNo],[StudentName] from Problem where [StudentNo]=@stno and 
                   [StudentName]=@name";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@stno", this.studentno));
            list1.Add(new DictionaryEntry("@name", this.studentname));
            DataSet ds = AccessDB.Query(mdbfile, sql, list1);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int nid = DatabaseUtility.getIntValue(ds.Tables[0].Rows[0], "ID", -1);
                        id = nid;
                    }
                }
            }
            return id;
        }


     }
}
