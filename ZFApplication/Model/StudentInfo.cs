using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Model
{
    public class StudentInfo
    {
        string no;
        public string No
        {
            get { return no; }
            set { no = value; }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string idcard;
        public string IDCard
        {
            get { return idcard; }
            set { idcard = value; }
        }

        string sex;
        public string Sex
        {
            get { return sex; }
            set { sex = value; } 
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

        string phonenumber;
        public string Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }

        string grade;
        
        public string Grade
        {             
            get { return grade; }
            set { grade = value; }
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

        string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        int haserror = 0;
        public int HasError
        {
            get { return haserror; }
            set { haserror = value; }
        }


    }

}
