using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.DPI;
using ZFApplication.Database;
using ZFApplication.Uitility;

namespace ZFApplication.Model
{
    public class PersonClue
    {
        public PersonClue clone()
        {
            //克隆一个一样的PersonClue，并返回；
            PersonClue pc = new PersonClue();
            pc.No = this.No;
            pc.Cityid = this.Cityid;
            pc.Objname = this.Objname; 
            pc.Title = this.Title;
            pc.Sex = this.Sex;
            pc.Name = this.Name;
            pc.Nation = this.Nation;
            pc.Political = this.Political;
            pc.Marriage = this.Marriage;
            pc.Idcard = this.Idcard;
            pc.Phone = this.Phone;
            pc.Addresstype = this.Addresstype;
            pc.Address = this.Address;
            pc.Policeaddress = this.Policeaddress;
            pc.Position = this.getSortPosition(this.Position);


            pc.Url = this.Url;
            pc.Remark = this.Remark;
            pc.isVisit= this.IsVisit;
            pc.Ispublic = this.Ispublic;
            pc.isdelete = this.Isdelete;
            pc.isreal= this.Isreal;
            pc.addressorder= this.Addressorder;
            pc.realid= this.Realid;
            pc.OrderNo = this.OrderNo;
            pc.Haserror = this.Haserror;

            return pc;
        }
        public string getName(string text)
        {
            string str = "";
            string[] lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if(lines.Length <= 1)
            {
                str = text;
            }
            else
            {
                str = lines[0];
            }
            return str;
        }

        public  int loadID(string mdbfile)
        {
            int id = -1;
            string sql = "";
            sql = @"select [ID],[Name],[ObjName] from Person where [No]=@no and [CityID]=@cityid and 
                   [Title]=@title
                   and [AddrType]=@addresstype and [AddressOrder]=@addrorder 
                   and [IsReal]=@isreal and [RealID]=@realid";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@no", this.No));
            list1.Add(new DictionaryEntry("@cityid", this.Cityid));
            list1.Add(new DictionaryEntry("@title", this.Title));
    //        list1.Add(new DictionaryEntry("@name", this.Name));
       //     list1.Add(new DictionaryEntry("@name1", this.Name));
            list1.Add(new DictionaryEntry("@addresstype", this.Addresstype));
            list1.Add(new DictionaryEntry("@addrorder", this.Addressorder));
            list1.Add(new DictionaryEntry("@isreal", this.Isreal));
            list1.Add(new DictionaryEntry("@realid", this.Realid));

            DataSet ds = AccessDB.Query(mdbfile, sql, list1);
            if (ds != null)
            {
                if(ds.Tables.Count>0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int i;
                        for(i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int nid = DatabaseUtility.getIntValue(ds.Tables[0].Rows[i], "ID", -1);
                            string name = DatabaseUtility.getStringValue(ds.Tables[0].Rows[i], "Name");
                            string first = getName(name);
                            string second = getName(this.Name);

                            string objname = DatabaseUtility.getStringValue(ds.Tables[0].Rows[i], "ObjName");
                            string objfirst = getName(objname);
                            string objsecond = getName(this.Objname);

                            if (first == second && objfirst == objsecond) //去掉曾用名的干扰
                            {
                                id = nid;
                                break;
                            }    
                        }

                        //id = DatabaseUtility.getIntValue(ds.Tables[0].Rows[0], "ID", -1);
                    }
                }
            }
            return id;
        }

        public void deleteALLClue(string mdbfile)
        {
            string sql = "delete from Person where [No]=@no and [ObjName]=@name";
            IList list1 = new ArrayList();
            list1.Add(new DictionaryEntry("@no", this.No));
            list1.Add(new DictionaryEntry("@name", this.Objname));
            AccessDB.NoQuery(mdbfile, sql, list1);
        }

        public int save(string mdbfile)
        {
            
            int id = loadID(mdbfile);

            if(this.cityid <= 0)
            {
                this.cityid = CityUtility.ParseCity(this.Address);
            }

            if (id > 0)
            {
                updateImp(id, mdbfile);
            }
            else
            {
                id = saveImp(mdbfile);
            }
            return id;
        }
        public void updateImp(int id,string mdbfile)
        {
            if (this.Objname == null || this.Objname == "")
                return;
            this.position = this.getSortPosition(this.Position);
            string sql = "";
            sql = @"update Person set [No]=@no,[CityID]=@cityid,[ObjName]=@objname,[Title]=@title,
                [Sex]=@sex,[Name]=@name,[Nation]=@nation,[Political]=@political,[Marriage]=@marriage,
                [IDCard]=@idcard,[PhoneNum]=@phone,[AddrType]=@addresstype,[Address]=@address,[AddressOrder]=@order,
                [PoliceAddr]=@policeaddress,[Remark]=@remark,[Position]=@position,[IsPublic]=@ispublic,[IsReal]=@real,
                [RealID]=@realid,[Url]=@url,[OrderNo]=@orderno, [HasError]=@haserror
                where [ID]=@id";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@no", this.No));
            list.Add(new DictionaryEntry("@cityid", this.Cityid));
            list.Add(new DictionaryEntry("@objname", this.Objname));
            list.Add(new DictionaryEntry("@title", this.Title));
            list.Add(new DictionaryEntry("@sex", this.Sex));
            list.Add(new DictionaryEntry("@name", this.Name));
            list.Add(new DictionaryEntry("@nation", this.Nation));
            list.Add(new DictionaryEntry("@political", this.Political));
            list.Add(new DictionaryEntry("@marriage", this.Marriage));
            list.Add(new DictionaryEntry("@idcard", this.Idcard));
            list.Add(new DictionaryEntry("@phone", this.Phone));
            list.Add(new DictionaryEntry("@addresstype", this.Addresstype));
            list.Add(new DictionaryEntry("@address", this.Address));
            list.Add(new DictionaryEntry("@order", this.Addressorder));
            list.Add(new DictionaryEntry("@policeaddress", this.Policeaddress));
            list.Add(new DictionaryEntry("@remark", this.Remark));
            list.Add(new DictionaryEntry("@position", this.Position));
            list.Add(new DictionaryEntry("@ispublic", this.Ispublic));
            list.Add(new DictionaryEntry("@real", this.isreal));
            list.Add(new DictionaryEntry("@realid", this.Realid));
            list.Add(new DictionaryEntry("@url", this.Url));
            list.Add(new DictionaryEntry("@orderno", this.OrderNo));
            list.Add(new DictionaryEntry("@haserror", this.Haserror));
            list.Add(new DictionaryEntry("@id", id));
            AccessDB.NoQuery(mdbfile, sql, list);                         
        }
       public int saveImp(string mdbfile)
       {
            //[No] [CityID],[Title], [Name],[AddrType]
            //查找是否有是重复的，重复的更新，否则插入
            

            if (this.Objname == null || this.Objname == "")
                return -1;
            

            this.position = this.getSortPosition(this.Position);
            //  int id = AccessDB.QueryID(mdbfile, sql, list1);
            string  sql="";
            //将PersonClue保存到mdbfile中
            sql = @"insert into Person([No],[CityID],[ObjName],[Title],[Sex],[Name],[Nation],[Political],
                            [Marriage],[IDCard],[PhoneNum],[AddrType],[Address],[AddressOrder],
                            [PoliceAddr],[Remark],[Position],[IsPublic],[Url],[IsVisit],[IsDelete],
                            [IsReal],[RealID],[OrderNo],[HasError])
                            values(@no,@cityid,@objname,@title,@sex,@name,@nation,@political,
                            @marriage,@idcard,@phone,@addresstype,@address,@order,@policeaddress,
                            @remark,@position,@ispublic,@url,@visit,@delete,@real,@realid,@orderno,@haserror)";
            IList list = new ArrayList();
            list.Add(new DictionaryEntry("@no", this.No));
            list.Add(new DictionaryEntry("@cityid", this.Cityid));
            list.Add(new DictionaryEntry("@objname", this.Objname));
            list.Add(new DictionaryEntry("@title", this.Title));
            list.Add(new DictionaryEntry("@sex", this.Sex));
            list.Add(new DictionaryEntry("@name", this.Name));
            list.Add(new DictionaryEntry("@nation", this.Nation));
            list.Add(new DictionaryEntry("@political", this.Political));
            list.Add(new DictionaryEntry("@marriage", this.Marriage));
            list.Add(new DictionaryEntry("@idcard", this.Idcard));
            list.Add(new DictionaryEntry("@phone", this.Phone));
            list.Add(new DictionaryEntry("@addresstype", this.Addresstype));
            list.Add(new DictionaryEntry("@address", this.Address));
            list.Add(new DictionaryEntry("@order", this.Addressorder));
            list.Add(new DictionaryEntry("@policeaddress", this.Policeaddress));
            list.Add(new DictionaryEntry("@remark", this.Remark));
            list.Add(new DictionaryEntry("@position", this.Position));
            list.Add(new DictionaryEntry("@ispublic", this.Ispublic));
            list.Add(new DictionaryEntry("@url", this.Url));
            list.Add(new DictionaryEntry("@visit", this.IsVisit));
            list.Add(new DictionaryEntry("@delete", this.Isdelete));
            list.Add(new DictionaryEntry("@real", this.Isreal));
            list.Add(new DictionaryEntry("@realid", this.Realid));
            list.Add(new DictionaryEntry("@orderno", this.OrderNo));
            list.Add(new DictionaryEntry("@haserror", this.Haserror));

            AccessDB.NoQuery(mdbfile, sql, list);
            return loadID(mdbfile);

       }

        // 线索编号
        string no;
        public string No
        {
            get { return no; }
            set { no = value; }
        }

        // 外键，城市编号
        int cityid;
        public int Cityid
        {
            get { return cityid; }
            set { cityid = value; }
        }

        int isVisit;
        public int IsVisit
        {
            get { return isVisit; }
            set { isVisit = value; }
        }

        // 对象名称,考生名称
        string objname;
        public string Objname
        {
            get { return objname; }
            set { objname = value; }
        }
        //称谓
        string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        // 性别
        string sex;
        public string Sex
        {
            get{ return sex;}
            set{ sex=value;}
        }

        // 姓名，包括曾用名
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // 民族
        string nation;
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        // 政治面貌
        string political;
        public string Political
        {
            get { return political; }
            set { political = value; }
        }

        // 婚姻状况
        string marriage;
        public string Marriage
        {
            get { return marriage; }
            set { marriage = value; }
        }

        // 身份证号
        string idcard;
        public string Idcard
        {
            get { return idcard; }
            set { idcard = value; }
        }

        // 联系电话
        string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        //地址类型
        string addresstype;
        public string Addresstype
        {
            get { return addresstype; }
            set { addresstype = value; }
        }

        // 地址
        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        // 公安局地址
        string policeaddress;
        public string Policeaddress
        {
            get { return policeaddress; }
            set { policeaddress = value; }
        }   

        // 职位
        string position;
        public string Position
        {
            get { return position; }
            set { position = this.getSortPosition(value); }
        }

        public string getSortPosition(string position)
        {
            int len = position.Length;
            if (len > 10)
            {
                string[] vec = position.Split(new char[] { '、', '，', '。', '；', ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (vec.Length > 0)
                {
                    return vec[vec.Length - 1];
                }
                else
                {
                    return position.Substring(0, 10);
                }
            }
            else
            {
                return position;
            }
        }

        // url
        string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        // 备注
        string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        //是否有公职
        int ispublic;
        public int Ispublic
        {
            get { return ispublic; }
            set { ispublic = value; }
        }

        //是否删除了
        int isdelete;
        public int Isdelete
        {
            get { return isdelete; }
            set { isdelete = value; }
        }

        int addressorder;
        public int Addressorder
        {
            get { return addressorder; }
            set { addressorder = value; }
        }

        int isreal;
        public int Isreal
        {
            get { return isreal; }
            set { isreal = value; }
        }

        int realid;
        public int Realid
        {
            get { return realid; }
            set { realid = value; }
        }

        int orderno;
        public int OrderNo
        {
            get { return orderno; }
            set { orderno = value; }
        }

        int haserror;
        public int Haserror
        {
            get { return haserror; }
            set { haserror = value; }
        }


    }
}
