using System;
using System.Collections;
using ZFApplication.Model;

namespace ZFApplication.Uitility
{
    public class ProvinceInfoComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            ProvinceInfo p1 = x as ProvinceInfo;
            ProvinceInfo p2 = y as ProvinceInfo;
            if (p1 == null || p2 == null)
            {
                return 0;
                // throw new ArgumentException("Objects are not of type ProvinceInfo");
            }
            int provinceComparison = string.Compare(p1.Province, p2.Province, StringComparison.Ordinal);
            if (provinceComparison == 0)
            {
                return string.Compare(p1.CityName, p2.CityName, StringComparison.Ordinal);
            }
            return provinceComparison;
        }
    }
}