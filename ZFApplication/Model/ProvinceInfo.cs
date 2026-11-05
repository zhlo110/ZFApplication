using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Model
{
    public class ProvinceInfo
    {
        string cityname = "";
        public string CityName
        {
            get { return cityname; }
            set { cityname = value; }
        }
        string personcount = "";
        string directcount = "";
        string relationcount = "";

        public string PersonCount
        {
            get { return personcount; }
            set { personcount = value; }
        }

        public string DirectCount
        {
            get { return directcount; }
            set { directcount = value; }
        }

        public string RelationCount
        {
            get { return relationcount; }
            set { relationcount = value; }
        }

        public string province = "";
        public string Province
        {
            get { return province; }
            set { province = value; }
        }

        public string region = "";
        public string Region
        {
            get { return region; }
            set { region = value; }
        }
    }
}
