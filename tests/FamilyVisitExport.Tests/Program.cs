using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using ZFApplication.Uitility;

internal static class Program
{
    private static int assertions;
    private static int nextId;
    private static readonly List<ExcelPackage> packages = new List<ExcelPackage>();

    private static void Main()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        string output = Path.Combine(Path.GetTempPath(), "zf-family-test", "exports-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(output);
        try
        {
            TestMainExport(output);
            TestSafeNames(Path.Combine(output, "names"));
            TestEmpty(Path.Combine(output, "empty"));
            TestDataQuality(Path.Combine(output, "quality"));
            TestCrossProvinceParents(Path.Combine(output, "cross-province"));
            TestParentLocationsWithoutStudentHome(Path.Combine(output, "parent-locations"));
            Console.WriteLine("PASS: " + assertions + " assertions; production exporter compiled and exercised with synthetic data.");
        }
        finally
        {
            foreach (ExcelPackage package in packages) package.Dispose();
            Directory.Delete(output, true);
        }
    }

    private static void TestMainExport(string folder)
    {
        DataTable people = NewPeople();
        DataTable cities = NewCities();
        Add(people, "0001", "同名考生", "本人", "户口所在地", "贵州省贵阳市南明区甲街", 111);
        Add(people, "0001", "同名考生", "本人", "工作单位及职务", "第一中学", 111, phone: "13000000001");
        Add(people, "0001", "同名考生", "本人", "经常居住地", "贵州省贵阳市南明区甲家", 111);
        Add(people, "0001", "同名考生", "父亲", "户口所在地", "贵州省贵阳市南明区父户", 111, phone: "13000000002");
        Add(people, "0001", "同名考生", "父亲", "户口所在地", "贵州省贵阳市南明区父户", 111, phone: "13000000002");
        Add(people, "0001", "同名考生", "母亲", "户口所在地", "四川省成都市青羊区母户", 211, phone: "13000000003");
        Add(people, "0001", "同名考生", "继父", "户口所在地", "贵州省遵义市红花岗区继父户", 121);
        Add(people, "0001", "同名考生", "继母", "户口所在地", "贵州省贵阳市南明区继母户", 111);
        Add(people, "0001", "同名考生", "父亲", "经常居住地", "贵州省贵阳市南明区父家", 111);
        Add(people, "0001", "同名考生", "母亲", "经常居住地", "四川省成都市青羊区母家", 211);
        Add(people, "0001", "同名考生", "父亲", "工作单位及职务", "父单位", 111, position: "工程师");
        Add(people, "0001", "同名考生", "母亲", "工作单位及职务", "母单位", 211, position: "教师");
        Add(people, "0001", "同名考生", "叔父", "户口所在地", "不应出现的叔父地址", 211, phone: "19999999999");
        Add(people, "0001", "同名考生", "父亲", "户口所在地", "不应出现的复制地址", 211, isReal: 0);
        Add(people, "0001", "同名考生", "母亲", "户口所在地", "不应出现的删除地址", 211, isDelete: 1);

        Add(people, "0002", "同名考生", "本人", "户口所在地", "贵州省遵义市红花岗区乙街", 121);
        Add(people, "0002", "同名考生", "本人", "工作单位及职务", "第二中学", 121);
        Add(people, "0001", "另一考生", "本人", "户口所在地", "贵州省贵阳市南明区丙街", 111);
        Add(people, "0001", "另一考生", "本人", "工作单位及职务", "第三中学", 111);
        Add(people, "0030", "重庆考生", "本人", "户口所在地", "重庆市渝中区丁街", 31);
        Add(people, "0030", "重庆考生", "父亲", "户口所在地", "重庆市渝中区父户", 31);
        Add(people, "0030", "重庆考生", "本人", "工作单位及职务", "重庆中学", 31);
        Add(people, "0040", "四川考生", "本人", "户口所在地", "四川省成都市青羊区戊街", 211);
        Add(people, "0050", "缺户籍考生", "本人", "工作单位及职务", "缺户籍学校", 111);
        Add(people, "0050", "缺户籍考生", "父亲", "户口所在地", "贵州省贵阳市南明区缺户籍父户", 111);
        Add(people, "0060", "省级考生", "本人", "户口所在地", "贵州省", 1);
        Add(people, "0070", "复制考生", "本人", "户口所在地", "不应出现的复制考生", 111, isReal: 0);
        Add(people, "0080", "删除考生", "本人", "户口所在地", "不应出现的删除考生", 111, isDelete: 1);
        Add(people, "0090", "亲属线索", "叔父", "户口所在地", "贵州省贵阳市南明区不应成组", 111);

        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(result.Files.Count == 3, "one file per student/parent registration province");
        Assert(result.Warnings.Count > 0, "missing student household registration is reported");
        Assert(result.Files.All(File.Exists), "all reported files exist");
        List<ExcelWorksheet> sheets = OpenSheets(result.Files);
        ExcelWorksheet guiYang = sheets.Single(s => s.Name == "贵阳市");
        ExcelWorksheet zunYi = sheets.Single(s => s.Name == "遵义市");
        Assert(CountRows(guiYang) == 3, "different student groups and known parent location are retained");
        Assert(CountRows(zunYi) == 1, "second municipality has one student");
        Assert(guiYang.Cells[1, 1].Text.Contains("家访3人"), "title counts student groups");
        Assert(guiYang.MergedCells[1, 1] == "A1:I1", "sample title merge is preserved");
        string[] headers = { "序号", "姓名", "学校名称及所在区县", "户籍所在区县", "家庭所在区县", "父母单位所在区县", "联系电话", "政审编号", "备注" };
        for (int c = 1; c <= headers.Length; c++) Assert(guiYang.Cells[2, c].Text == headers[c - 1], "template header " + c);
        int target = FindRow(guiYang, "同名考生", "0001");
        Assert(guiYang.Cells[target, 3].Text.Contains("第一中学"), "school comes from the student's work/school row");
        Assert(guiYang.Cells[target, 3].Text.Contains("南明区"), "school location included");
        string registered = guiYang.Cells[target, 4].Text;
        Assert(registered.Contains("父亲") && registered.Contains("母亲") && registered.Contains("继父") && registered.Contains("继母"), "all four parent relationships included");
        Assert(registered.Contains("青羊区"), "out-of-province parent registration retained on student's row");
        Assert(registered.Split('\n').Count(s => s.Contains("父亲")) == 1, "duplicate parent rows deduplicated");
        Assert(guiYang.Cells[target, 5].Text.Contains("南明区"), "family residence populated");
        Assert(guiYang.Cells[target, 6].Text.Contains("父亲") && guiYang.Cells[target, 6].Text.Contains("母亲"), "both parents' workplaces populated");
        Assert(guiYang.Cells[target, 7].Text.Contains("13000000002") && guiYang.Cells[target, 7].Text.Contains("13000000003"), "both parents' phone numbers included");
        Assert(!guiYang.Cells[target, 7].Text.Contains("13000000001"), "phone column matches template parent-only convention");
        Assert(guiYang.Cells[target, 8].Value is string && guiYang.Cells[target, 8].Text == "0001", "leading-zero political review number remains text");
        Assert(guiYang.Cells[target, 4].Style.WrapText, "multiline parent addresses wrap");
        Assert(guiYang.PrinterSettings.RepeatRows.Address.Replace("$", "") == "1:2", "print title repeats full header rows: " + guiYang.PrinterSettings.RepeatRows.Address);
        Assert(guiYang.PrinterSettings.FitToWidth == 1 && guiYang.PrinterSettings.FitToHeight == 0, "printing fits one page wide without shrinking all rows to one page");
        string allText = string.Join("\n", sheets.SelectMany(s => s.Cells.Select(c => c.Text)));
        Assert(!allText.Contains("不应出现") && !allText.Contains("19999999999"), "deleted/copy/unapproved relationships excluded");
        Assert(sheets.Any(s => FindRowOrZero(s, "重庆考生", "0030") > 0), "direct-administered municipality exports");
        ExcelWorksheet chengDu = sheets.Single(s => s.Name == "成都市");
        Assert(FindRowOrZero(chengDu, "同名考生", "0001") > 0, "cross-province mother also adds student in her province");
        Assert(FindRowOrZero(guiYang, "缺户籍考生", "0050") > 0, "known parent registration retained when student registration is absent");
        Assert(FindRowOrZero(zunYi, "同名考生", "0001") == 0, "same-province parent in another municipality does not duplicate student");
        foreach (ExcelWorksheet sheet in sheets)
        {
            int count = CountRows(sheet);
            for (int r = 3; r < count + 3; r++) Assert(sheet.Cells[r, 1].GetValue<int>() == r - 2, "sequence starts at one in " + sheet.Name);
        }
        int actualRows = sheets.Sum(CountRows);
        Assert(result.RowCount == actualRows && actualRows == 8, "summary matches all cross-province output rows");
        Assert(result.StudentCount == 7, "summary counts unique student keys across all provinces");

        Dictionary<string, byte[]> originalFiles = result.Files.ToDictionary(f => f, File.ReadAllBytes);
        FamilyVisitExportResult repeated = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(repeated.Files.All(f => !originalFiles.ContainsKey(f)), "repeat export chooses new file names");
        Assert(originalFiles.All(p => File.ReadAllBytes(p.Key).SequenceEqual(p.Value)), "repeat export preserves existing workbooks");
    }

    private static void TestSafeNames(string folder)
    {
        DataTable cities = NewCities();
        cities.Rows.Add(4, 0, "测试/省:名称", "测试");
        cities.Rows.Add(41, 4, "同名/行政区", "同名");
        cities.Rows.Add(42, 4, "同名:行政区", "同名");
        cities.Rows.Add(43, 4, "这是一个超过三十一个字符的省份下一级行政区名称用来验证工作表名称能够截断且保存", "长名");
        DataTable people = NewPeople();
        Add(people, "1", "安全名称甲", "本人", "户口所在地", "测试甲", 41);
        Add(people, "2", "安全名称乙", "本人", "户口所在地", "测试乙", 42);
        Add(people, "3", "安全名称丙", "本人", "户口所在地", "测试丙", 43);
        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(result.Files.Count == 1, "unsafe province names still produce one workbook");
        string fileName = Path.GetFileName(result.Files.Single());
        Assert(!fileName.Any(c => "<>:\"/\\|?*".Contains(c)), "Windows-safe workbook filename");
        List<ExcelWorksheet> sheets = OpenSheets(result.Files);
        Assert(sheets.Count == 3, "sanitized sheet-name collisions preserve all municipalities");
        Assert(sheets.Select(s => s.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() == 3, "worksheet names unique");
        Assert(sheets.All(s => s.Name.Length <= 31 && !s.Name.Any(c => "[]:*?/\\".Contains(c))), "Excel-safe worksheet names");
    }

    private static void TestEmpty(string folder)
    {
        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(NewPeople(), NewCities(), folder, 2026);
        Assert(result.Files.Count == 0 && result.StudentCount == 0 && result.RowCount == 0, "empty clues do not create empty workbook");
    }

    private static void TestDataQuality(string folder)
    {
        DataTable cities = NewCities();
        cities.Rows.Add(91, 92, "循环地区甲", "循环甲");
        cities.Rows.Add(92, 91, "循环地区乙", "循环乙");
        cities.Rows.Add(93, 999, "缺父行政区", "缺父");
        DataTable people = NewPeople();
        Add(people, "1000", "正常考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "1000", "正常考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "1000", "正常考生", "本人", "工作单位及职务", "=SUM(1,2)", 111);
        Add(people, "1000", "正常考生", "父亲", "户口所在地", "无法识别的父亲户籍原地址", -1);
        Add(people, "1001", "循环行政区考生", "本人", "户口所在地", "循环地区甲地址", 91);
        Add(people, "1002", "缺父行政区考生", "本人", "户口所在地", "缺父地区地址", 93);
        Add(people, "1003", "冲突户籍考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "1003", "冲突户籍考生", "本人", "户口所在地", "四川省成都市青羊区", 211);
        Add(people, "", "空编号考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "1004", "", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(result.Files.Count == 1 && result.StudentCount == 1, "unresolvable and conflicting student registration is not silently assigned");
        string warnings = string.Join("\n", result.Warnings);
        Assert(warnings.Contains("循环"), "cyclic administrative hierarchy reported without infinite loop");
        Assert(warnings.Contains("999"), "missing administrative ancestor reported");
        Assert(warnings.Contains("冲突户籍考生"), "conflicting real student registration reported");
        Assert(warnings.Contains("空编号考生") && warnings.Contains("1004"), "missing grouping identifiers reported");
        ExcelWorksheet sheet = OpenSheets(result.Files).Single();
        Assert(CountRows(sheet) == 1, "duplicate real student registration produces one row");
        Assert(sheet.Cells[3, 4].Text.Contains("无法识别的父亲户籍原地址"), "unknown parent administrative district retains original source address");
        Assert(sheet.Cells[3, 3].Formula.Length == 0 && sheet.Cells[3, 3].Text.Contains("=SUM(1,2)"), "formula-like school source is exported as plain text");
    }

    private static void TestCrossProvinceParents(string folder)
    {
        DataTable cities = NewCities();
        cities.Rows.Add(22, 2, "绵阳市", "绵阳");
        cities.Rows.Add(221, 22, "涪城区", "涪城");
        cities.Rows.Add(4, 0, "云南省", "云南");
        cities.Rows.Add(41, 4, "昆明市", "昆明");
        cities.Rows.Add(411, 41, "五华区", "五华");
        cities.Rows.Add(5, 0, "吉林省", "吉林");
        cities.Rows.Add(51, 5, "长春市", "长春");
        DataTable people = NewPeople();
        Add(people, "2000", "跨省考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "2000", "跨省考生", "本人", "工作单位及职务", "省际中学", 111);
        Add(people, "2000", "跨省考生", "本人", "经常居住地", "贵州省贵阳市南明区", 111);
        Add(people, "2000", "跨省考生", "父亲", "户口所在地", "四川省成都市青羊区父户", 211);
        Add(people, "2000", "跨省考生", "父亲", "户口所在地", "四川省成都市青羊区父户", 211);
        Add(people, "2000", "跨省考生", "母亲", "户口所在地", "四川省成都市青羊区母户", 211);
        Add(people, "2000", "跨省考生", "父亲", "户口所在地", "贵州省遵义市红花岗区父另户", 121);
        Add(people, "2000", "跨省考生", "继父", "户口所在地", "四川省绵阳市涪城区继父户", 221);
        Add(people, "2000", "跨省考生", "继父", "户口所在地", "重庆市渝中区继父另户", 31);
        Add(people, "2000", "跨省考生", "继母", "户口所在地", "云南省昆明市五华区继母户", 411);
        Add(people, "2000", "跨省考生", "父亲", "工作单位及职务", "贵州父单位", 111);
        Add(people, "2000", "跨省考生", "母亲", "工作单位及职务", "四川母单位", 211);
        Add(people, "2000", "跨省考生", "叔父", "户口所在地", "吉林省长春市叔父户", 51);
        Add(people, "2000", "跨省考生", "父亲", "户口所在地", "吉林省长春市复制父户", 51, isReal: 0);
        Add(people, "2000", "跨省考生", "母亲", "户口所在地", "吉林省长春市已删母户", 51, isDelete: 1);
        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(result.Files.Count == 4, "student, parents, and step-parents all trigger their real registration provinces");
        Assert(result.StudentCount == 1 && result.RowCount == 5, "one student is counted once despite five province/city rows");
        List<ExcelWorksheet> sheets = OpenSheets(result.Files);
        Assert(sheets.Count == 5 && sheets.Sum(CountRows) == 5, "each province/municipality/student key appears once");
        Assert(sheets.All(s => CountRows(s) == 1 && FindRowOrZero(s, "跨省考生", "2000") == 3), "parent and duplicate clues do not add repeated student rows in one sheet");
        Assert(!sheets.Any(s => s.Name == "遵义市" || s.Name == "长春市"), "same student province uses student's city; unrelated/deleted/copied relatives trigger no city");
        Assert(sheets.Any(s => s.Name == "绵阳市") && sheets.Any(s => s.Name == "渝中区") && sheets.Any(s => s.Name == "昆明市"), "step-parent cross-province and multiple external cities are retained");
        ExcelWorksheet guiYang = sheets.Single(s => s.Name == "贵阳市");
        ExcelWorksheet chengDu = sheets.Single(s => s.Name == "成都市");
        Assert(guiYang.Cells[3, 4].Text.Contains("四川省成都市") && guiYang.Cells[3, 4].Text.Contains("云南省昆明市"), "student province row retains all out-of-province parent registration with province names");
        Assert(chengDu.Cells[3, 4].Text.Contains("父亲:成都市青羊区") && !chengDu.Cells[3, 4].Text.Contains("父亲:四川省成都市"), "parent province row formats its local parent registration relative to that province");
        Assert(chengDu.Cells[3, 3].Text.Contains("贵州省贵阳市南明区") && chengDu.Cells[3, 5].Text.Contains("贵州省贵阳市南明区"), "external province row includes student's school and home province");
        Assert(chengDu.Cells[3, 6].Text.Contains("父亲:贵州省贵阳市南明区") && chengDu.Cells[3, 6].Text.Contains("母亲:成都市青羊区"), "parent workplace districts are reformatted for each destination province");
        Assert(sheets.All(s => s.Cells[1, 1].Text.Contains("家访1人") && s.Cells[3, 1].GetValue<int>() == 1), "each external city title and sequence reflect one local row");
    }

    private static void TestParentLocationsWithoutStudentHome(string folder)
    {
        DataTable people = NewPeople();
        DataTable cities = NewCities();
        Add(people, "2001", "无本人户籍考生", "本人", "工作单位及职务", "未知户籍中学", 111);
        Add(people, "2001", "无本人户籍考生", "父亲", "户口所在地", "四川省成都市青羊区父户", 211);
        Add(people, "2001", "无本人户籍考生", "母亲", "户口所在地", "四川省成都市青羊区母户", 211);
        Add(people, "2002", "本人户籍冲突考生", "本人", "户口所在地", "贵州省贵阳市南明区", 111);
        Add(people, "2002", "本人户籍冲突考生", "本人", "户口所在地", "四川省成都市青羊区", 211);
        Add(people, "2002", "本人户籍冲突考生", "父亲", "户口所在地", "重庆市渝中区父户", 31);
        Add(people, "2003", "仅省父母考生", "父亲", "户口所在地", "四川省", 2);
        Add(people, "2004", "父母省市并存考生", "父亲", "户口所在地", "四川省", 2);
        Add(people, "2004", "父母省市并存考生", "母亲", "户口所在地", "四川省成都市青羊区母户", 211);
        Add(people, "3000", "同名当前\r\n曾用甲", "父亲", "户口所在地", "四川省成都市青羊区父户甲", 211);
        Add(people, "3000", "同名当前\r\n曾用甲", "母亲", "户口所在地", "重庆市渝中区母户甲", 31);
        Add(people, "3000", "同名当前\r\n曾用乙", "父亲", "户口所在地", "四川省成都市青羊区父户乙", 211);
        Add(people, "3000", "同名当前\r\n曾用乙", "母亲", "户口所在地", "重庆市渝中区母户乙", 31);
        FamilyVisitExportResult result = FamilyVisitExportUtility.Export(people, cities, folder, 2026);
        Assert(result.Files.Count == 2 && result.RowCount == 8, "known parent locations survive missing/conflicting student home");
        Assert(result.StudentCount == 6, "unique student count uses original ObjName rather than displayed first line");
        List<ExcelWorksheet> sheets = OpenSheets(result.Files);
        ExcelWorksheet chengDu = sheets.Single(s => s.Name == "成都市");
        ExcelWorksheet pending = sheets.Single(s => s.Name == "待确认地区");
        ExcelWorksheet chongQing = sheets.Single(s => s.Name == "渝中区");
        Assert(FindRowOrZero(chengDu, "无本人户籍考生", "2001") > 0, "parents' shared city retains student once without own home");
        Assert(FindRowOrZero(chongQing, "本人户籍冲突考生", "2002") > 0 && FindRowOrZero(chengDu, "本人户籍冲突考生", "2002") == 0, "conflicting own home is not guessed while known parent location remains");
        Assert(FindRowOrZero(pending, "仅省父母考生", "2003") > 0, "province-only parent registration enters pending city");
        Assert(FindRowOrZero(chengDu, "父母省市并存考生", "2004") > 0 && FindRowOrZero(pending, "父母省市并存考生", "2004") == 0, "known parent city avoids an additional province-only pending row");
        Assert(Enumerable.Range(3, chengDu.Dimension.End.Row - 2).Count(r => chengDu.Cells[r, 2].Text == "同名当前" && chengDu.Cells[r, 8].Text == "3000") == 2, "same display name and number retain groups distinguished by original ObjName");
        Assert(Enumerable.Range(3, chongQing.Dimension.End.Row - 2).Count(r => chongQing.Cells[r, 2].Text == "同名当前" && chongQing.Cells[r, 8].Text == "3000") == 2, "raw-ObjName groups both repeat in external province without increasing unique student count");
        string warnings = string.Join("\n", result.Warnings);
        Assert(warnings.Contains("无本人户籍考生") && warnings.Contains("本人户籍冲突考生") && warnings.Contains("仅省父母考生"), "partial locations and parent-only groups remain visibly warned");
    }

    private static List<ExcelWorksheet> OpenSheets(IEnumerable<string> files)
    {
        List<ExcelWorksheet> sheets = new List<ExcelWorksheet>();
        foreach (string file in files)
        {
            ExcelPackage package = new ExcelPackage(new FileInfo(file));
            packages.Add(package);
            sheets.AddRange(package.Workbook.Worksheets);
        }
        return sheets;
    }

    private static int CountRows(ExcelWorksheet sheet)
    {
        return Enumerable.Range(3, Math.Max(0, sheet.Dimension.End.Row - 2)).Count(r => sheet.Cells[r, 2].Text.Length > 0);
    }

    private static int FindRow(ExcelWorksheet sheet, string name, string number)
    {
        int row = FindRowOrZero(sheet, name, number);
        Assert(row > 0, "expected student " + name + "/" + number);
        return row;
    }

    private static int FindRowOrZero(ExcelWorksheet sheet, string name, string number)
    {
        return Enumerable.Range(3, Math.Max(0, sheet.Dimension.End.Row - 2)).FirstOrDefault(r => sheet.Cells[r, 2].Text == name && sheet.Cells[r, 8].Text == number);
    }

    private static DataTable NewPeople()
    {
        DataTable table = new DataTable("Person");
        string[] textColumns = { "No", "ObjName", "Title", "Name", "IDCard", "PhoneNum", "AddrType", "Address", "Position", "Remark" };
        foreach (string name in textColumns) table.Columns.Add(name, typeof(string));
        string[] numberColumns = { "ID", "CityID", "AddressOrder", "IsDelete", "IsReal", "RealID", "OrderNo" };
        foreach (string name in numberColumns) table.Columns.Add(name, typeof(int));
        return table;
    }

    private static DataTable NewCities()
    {
        DataTable table = new DataTable("City");
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("ParentID", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("ShortName", typeof(string));
        table.Rows.Add(1, 0, "贵州省", "贵州");
        table.Rows.Add(11, 1, "贵阳市", "贵阳");
        table.Rows.Add(111, 11, "南明区", "南明");
        table.Rows.Add(12, 1, "遵义市", "遵义");
        table.Rows.Add(121, 12, "红花岗区", "红花岗");
        table.Rows.Add(2, 0, "四川省", "四川");
        table.Rows.Add(21, 2, "成都市", "成都");
        table.Rows.Add(211, 21, "青羊区", "青羊");
        table.Rows.Add(3, 0, "重庆市", "重庆");
        table.Rows.Add(31, 3, "渝中区", "渝中");
        return table;
    }

    private static void Add(DataTable table, string number, string student, string title, string addressType,
        string address, int cityId, string phone = "", string position = "", int isReal = 1, int isDelete = 0)
    {
        DataRow row = table.NewRow();
        row["ID"] = ++nextId;
        row["No"] = number;
        row["ObjName"] = student;
        row["Title"] = title;
        row["Name"] = title == "本人" ? student : student + title;
        row["IDCard"] = "510000200001010000";
        row["PhoneNum"] = phone;
        row["AddrType"] = addressType;
        row["Address"] = address;
        row["CityID"] = cityId;
        row["Position"] = position;
        row["IsDelete"] = isDelete;
        row["IsReal"] = isReal;
        row["AddressOrder"] = 0;
        row["OrderNo"] = nextId;
        table.Rows.Add(row);
    }

    private static void Assert(bool condition, string description)
    {
        assertions++;
        if (!condition) throw new InvalidOperationException("FAIL: " + description);
    }
}
