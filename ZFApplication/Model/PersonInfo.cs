using DevExpress.Office.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Model
{
    public class PersonInfo
    {
        int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        string no;
        public string No
        {
            get { return no; }
            set { no = value; }
        }

        string objname;
        public string ObjName
        {
            get { return objname; }
            set { objname = value; }
        }

        string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string nation;
        public string Nation
        {
            get { return nation; }
            set { nation = value; }
        }

        string political;
        public string Political
        {
            get { return political; }
            set { political = value; }
        }

        //婚姻状况
        string marriage;
        public string Marriage
        {
            get { return marriage; }
            set { marriage = value; }
        }

        string idcard;
        public string IDCard
        {
            get { return idcard; }
            set { idcard = value; }
        }

        string phonenumber;
        public string Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }

        string addresstype;
        public string Addresstype
        {
            get { return addresstype; }
            set { addresstype = value; }
        }

        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        string ispublic;
        public string IsPublic
        {
            get { return ispublic; }
            set { ispublic = value; }
        }

        string policeAddr;
        public string PoliceAddr
        {
            get { return policeAddr; }
            set { policeAddr = value; }
        }

        bool isvisit;
        public bool IsVisit
        {          
            get { return isvisit; }
            set { isvisit = value; }
        }

        public string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        
    }
}
