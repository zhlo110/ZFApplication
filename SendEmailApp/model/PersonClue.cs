using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailApp.model
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
            pc.isVisit = this.IsVisit;
            pc.Ispublic = this.Ispublic;
            pc.isdelete = this.Isdelete;
            pc.isreal = this.Isreal;
            pc.addressorder = this.Addressorder;
            pc.realid = this.Realid;
            pc.OrderNo = this.OrderNo;
            pc.Haserror = this.Haserror;

            return pc;
        }
        public string getName(string text)
        {
            string str = "";
            string[] lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length <= 1)
            {
                str = text;
            }
            else
            {
                str = lines[0];
            }
            return str;
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
            get { return sex; }
            set { sex = value; }
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
