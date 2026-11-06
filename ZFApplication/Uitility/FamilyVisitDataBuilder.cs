using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace ZFApplication.Uitility
{
    internal sealed class FamilyVisitExportData
    {
        public List<FamilyVisitGroup> Groups { get; private set; }
        public List<string> Warnings { get; private set; }

        public FamilyVisitExportData()
        {
            Groups = new List<FamilyVisitGroup>();
            Warnings = new List<string>();
        }
    }

    internal sealed class FamilyVisitGroup
    {
        public string Province { get; set; }
        public string Region { get; set; }
        public string Number { get; set; }
        public string ObjectName { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string RegisteredAddress { get; set; }
        public string HomeAddress { get; set; }
        public string ParentWork { get; set; }
        public string Phone { get; set; }
    }

    // Keep selection and grouping independent of the database and Excel writer.
    internal static class FamilyVisitDataBuilder
    {
        private static readonly string[] Titles = { "本人", "父亲", "母亲", "继父", "继母" };

        internal static FamilyVisitExportData Build(DataTable people, DataTable cities)
        {
            if (people == null) throw new ArgumentNullException("people");
            if (cities == null) throw new ArgumentNullException("cities");

            FamilyVisitExportData result = new FamilyVisitExportData();
            HashSet<string> warnings = new HashSet<string>(StringComparer.Ordinal);
            CityLookup cityLookup = new CityLookup(cities);
            IEnumerable<IGrouping<Tuple<string, string>, DataRow>> families = people.Rows.Cast<DataRow>()
                .Where(row => GetInt(row, "IsReal", 0) == 1 && GetInt(row, "IsDelete", 1) == 0)
                .Where(row => Array.IndexOf(Titles, GetText(row, "Title")) >= 0)
                .GroupBy(row => Tuple.Create(GetText(row, "No"), GetText(row, "ObjName")))
                .OrderBy(family => family.Key.Item1, StringComparer.Ordinal)
                .ThenBy(family => family.Key.Item2, StringComparer.Ordinal);

            foreach (IGrouping<Tuple<string, string>, DataRow> family in families)
            {
                string number = family.Key.Item1;
                string name = FirstLine(family.Key.Item2);
                string context = "编号「" + (number.Length == 0 ? "空" : number) + "」招飞对象「" +
                    (name.Length == 0 ? "空" : name) + "」：";
                Action<string> warn = message =>
                {
                    string warning = context + message;
                    if (warnings.Add(warning)) result.Warnings.Add(warning);
                };

                if (number.Length == 0 || name.Length == 0)
                {
                    if (number.Length == 0) warn("线索编号为空，无法可靠识别招飞对象，未导出该组。");
                    if (name.Length == 0) warn("招飞对象 ObjName 为空，未导出该组。");
                    continue;
                }

                List<DataRow> rows = family.ToList();
                List<DataRow> studentHomes = Select(rows, "本人", "户口所在地").ToList();
                if (studentHomes.Count == 0)
                    warn("缺少本人真实户口所在地记录，将按已知父母户籍地点列出。");

                List<List<CityInfo>> homePaths = new List<List<CityInfo>>();
                foreach (DataRow home in studentHomes)
                {
                    string error;
                    List<CityInfo> path = cityLookup.GetPath(GetInt(home, "CityID", -1), out error);
                    if (path == null)
                        warn("本人户口所在地行政区无法确定（" + error + "），地址「" + GetText(home, "Address") + "」。");
                    else
                        homePaths.Add(path);
                }
                if (studentHomes.Count > 0 && homePaths.Count == 0)
                    warn("本人户口所在地没有可用的行政区，将按已知父母户籍地点列出。");

                List<IGrouping<string, List<CityInfo>>> locations = homePaths
                    .GroupBy(GetLocationKey)
                    .ToList();
                List<CityInfo> studentPath = null;
                if (locations.Count > 1)
                    warn("本人真实户口所在地指向多个省份或地区，无法确定本人唯一归属，将按已知父母户籍地点列出。");
                else if (homePaths.Count > 0)
                    studentPath = homePaths[0];

                List<List<CityInfo>> destinations = new List<List<CityInfo>>();
                if (studentPath != null)
                    destinations.Add(studentPath);
                if (studentPath != null && studentPath.Count == 1)
                    warn("本人户口所在地仅确定到省级，已放入“待确认地区”工作表。");

                foreach (DataRow parentHome in rows.Where(row => GetText(row, "Title") != "本人"
                    && GetText(row, "AddrType") == "户口所在地"))
                {
                    string error;
                    List<CityInfo> path = cityLookup.GetPath(GetInt(parentHome, "CityID", -1), out error);
                    if (path == null)
                    {
                        warn(GetText(parentHome, "Title") + "户口所在地无法确定导出省份（" + error
                            + "），原地址「" + GetText(parentHome, "Address") + "」。");
                        continue;
                    }
                    // 本人所在省仍按本人户籍地市分表；跨省父母按各自实际地市分表。
                    if (studentPath == null || path[0].Id != studentPath[0].Id)
                        destinations.Add(path);
                }

                if (destinations.Count == 0)
                {
                    warn("本人及父母均没有可用的户籍行政区，未导出该组。");
                    continue;
                }

                // 父母同地仅列一行；省级线索已有具体市州承接时，不再重复增加待确认行。
                IEnumerable<List<CityInfo>> targets = destinations.GroupBy(path => path[0].Id)
                    .SelectMany(provincePaths => provincePaths.Any(path => path.Count > 1)
                        ? provincePaths.Where(path => path.Count > 1) : provincePaths)
                    .GroupBy(GetLocationKey).Select(location => location.First());
                foreach (List<CityInfo> target in targets)
                {
                    string province = target[0].Name;
                    string region = target.Count > 1 ? target[1].Name : "待确认地区";
                    FamilyVisitGroup group = new FamilyVisitGroup
                    {
                        Province = province,
                        Region = region,
                        Number = number,
                        ObjectName = family.Key.Item2,
                        Name = name,
                        School = JoinDistinct(Select(rows, "本人", "工作单位及职务").Select(row =>
                        {
                            string school = GetText(row, "Address");
                            string district = GetDistrict(row, province, cityLookup, warn, "本人学校");
                            return school.Length == 0 ? district : school +
                                (district.Length == 0 || district == school ? "" : "\r\n" + district);
                        })),
                        RegisteredAddress = GetParentDistricts(rows, "户口所在地", province, cityLookup, warn),
                        HomeAddress = JoinDistinct(Select(rows, "本人", "经常居住地")
                            .Select(row => GetDistrict(row, province, cityLookup, warn, "本人经常居住地"))),
                        ParentWork = GetParentDistricts(rows, "工作单位及职务", province, cityLookup, warn),
                        Phone = GetParentPhones(rows)
                    };
                    result.Groups.Add(group);
                }
            }

            return result;
        }

        private static string GetLocationKey(List<CityInfo> path)
        {
            return path[0].Id.ToString(CultureInfo.InvariantCulture) + ":" +
                (path.Count > 1 ? path[1].Id.ToString(CultureInfo.InvariantCulture) : "省级");
        }

        private static IEnumerable<DataRow> Select(IEnumerable<DataRow> rows, string title, string addressType)
        {
            return rows.Where(row => GetText(row, "Title") == title && GetText(row, "AddrType") == addressType);
        }

        private static string GetParentDistricts(List<DataRow> rows, string addressType, string province,
            CityLookup lookup, Action<string> warn)
        {
            List<string> lines = new List<string>();
            foreach (string title in Titles.Skip(1))
            {
                foreach (string address in Select(rows, title, addressType)
                    .Select(row => GetDistrict(row, province, lookup, warn, title + addressType))
                    .Where(value => value.Length > 0).Distinct(StringComparer.Ordinal))
                    lines.Add(title + ":" + address);
            }
            return string.Join("\r\n", lines);
        }

        private static string GetParentPhones(List<DataRow> rows)
        {
            List<string> lines = new List<string>();
            foreach (string title in Titles.Skip(1))
            {
                foreach (string phone in rows.Where(row => GetText(row, "Title") == title)
                    .Select(row => GetText(row, "PhoneNum")).Where(value => value.Length > 0)
                    .Distinct(StringComparer.Ordinal))
                    lines.Add(title + ":" + phone);
            }
            return string.Join("\r\n", lines);
        }

        private static string GetDistrict(DataRow row, string province, CityLookup lookup,
            Action<string> warn, string description)
        {
            string address = GetText(row, "Address");
            int cityId = GetInt(row, "CityID", -1);
            string error;
            List<CityInfo> path = lookup.GetPath(cityId, out error);
            if (path == null)
            {
                warn(description + "行政区无法确定（" + error + "），" +
                    (address.Length > 0 ? "已保留原地址「" + address + "」。" : "原地址也为空。"));
                return address;
            }

            if (path.Count == 1)
                warn(description + "行政区仅确定到省级，已保留省名。");

            // Preserve the province for cross-province addresses and two-level paths,
            // including municipalities directly administering districts/counties.
            int start = path.Count >= 3 && path[0].Name == province ? 1 : 0;
            return string.Concat(path.Skip(start).Select(city => city.Name));
        }

        private static string JoinDistinct(IEnumerable<string> values)
        {
            return string.Join("\r\n", values.Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.Ordinal));
        }

        private static string FirstLine(string value)
        {
            return value.Split(new[] { '\r', '\n' }, StringSplitOptions.None)[0].Trim();
        }

        private static string GetText(DataRow row, string column)
        {
            return row.IsNull(column) ? "" : Convert.ToString(row[column], CultureInfo.InvariantCulture).Trim();
        }

        private static int GetInt(DataRow row, string column, int fallback)
        {
            int value;
            return int.TryParse(GetText(row, column), NumberStyles.Integer, CultureInfo.InvariantCulture, out value)
                ? value : fallback;
        }

        private sealed class CityInfo
        {
            internal int Id;
            internal int ParentId;
            internal string Name;
        }

        private sealed class CityLookup
        {
            private readonly Dictionary<int, CityInfo> cities = new Dictionary<int, CityInfo>();
            private readonly Dictionary<int, List<CityInfo>> paths = new Dictionary<int, List<CityInfo>>();
            private readonly Dictionary<int, string> errors = new Dictionary<int, string>();

            internal CityLookup(DataTable cityTable)
            {
                foreach (DataRow row in cityTable.Rows)
                {
                    int id = GetInt(row, "ID", -1);
                    if (id <= 0) continue;
                    cities[id] = new CityInfo
                    {
                        Id = id,
                        ParentId = GetInt(row, "ParentID", -1),
                        Name = GetText(row, "Name")
                    };
                }
            }

            internal List<CityInfo> GetPath(int id, out string error)
            {
                List<CityInfo> cached;
                if (paths.TryGetValue(id, out cached))
                {
                    error = null;
                    return cached;
                }
                if (errors.TryGetValue(id, out error)) return null;

                List<CityInfo> path = new List<CityInfo>();
                HashSet<int> visited = new HashSet<int>();
                int current = id;
                error = null;
                while (true)
                {
                    CityInfo city;
                    if (current <= 0 || !cities.TryGetValue(current, out city))
                    {
                        error = "CityID=" + id.ToString(CultureInfo.InvariantCulture) +
                            " 的行政区链缺少 ID=" + current.ToString(CultureInfo.InvariantCulture);
                        break;
                    }
                    if (!visited.Add(current))
                    {
                        error = "CityID=" + id.ToString(CultureInfo.InvariantCulture) + " 的行政区存在循环引用";
                        break;
                    }
                    if (city.Name.Length == 0)
                    {
                        error = "行政区 ID=" + current.ToString(CultureInfo.InvariantCulture) + " 名称为空";
                        break;
                    }
                    path.Insert(0, city);
                    if (city.ParentId == 0)
                    {
                        paths[id] = path;
                        return path;
                    }
                    current = city.ParentId;
                }
                errors[id] = error;
                return null;
            }
        }
    }
}
