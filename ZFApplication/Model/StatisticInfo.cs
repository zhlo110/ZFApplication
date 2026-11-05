using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Model
{
    public class StatisticInfo
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        int visitNumber;
        public int VisitNumber
        {
            get { return visitNumber; }
            set { visitNumber = value; }
        }

    }
}
