using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ZFApplication.Uitility
{
    public sealed class FamilyVisitExportResult
    {
        public List<string> Files { get; private set; } = new List<string>();
        public List<string> Warnings { get; private set; } = new List<string>();
        public int StudentCount { get; internal set; }
        public int RowCount { get; internal set; }
    }

    public static class FamilyVisitExportUtility
    {
        private static readonly string[] Headers =
        {
            "序号", "姓名", "学校名称及所在区县", "户籍所在区县", "家庭所在区县",
            "父母单位所在区县", "联系电话", "政审编号", "备注"
        };

        public static FamilyVisitExportResult Export(DataTable people, DataTable cities,
            string outputFolder, int year)
        {
            if (string.IsNullOrWhiteSpace(outputFolder))
                throw new ArgumentException("请选择家访表格的保存文件夹。", "outputFolder");

            FamilyVisitExportData data = FamilyVisitDataBuilder.Build(people, cities);
            FamilyVisitExportResult result = new FamilyVisitExportResult();
            result.Warnings.AddRange(data.Warnings);
            if (data.Groups.Count == 0)
                return result;

            result.StudentCount = data.Groups.Select(group => Tuple.Create(group.Number, group.ObjectName))
                .Distinct().Count();
            result.RowCount = data.Groups.Count;

            Directory.CreateDirectory(outputFolder);
            StringComparer nameComparer = StringComparer.Create(CultureInfo.GetCultureInfo("zh-CN"), false);
            foreach (var province in data.Groups.GroupBy(group => group.Province)
                .OrderBy(group => group.Key, nameComparer))
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    HashSet<string> sheetNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var region in province.GroupBy(group => group.Region)
                        .OrderBy(group => group.Key, nameComparer))
                    {
                        List<FamilyVisitGroup> students = region.OrderBy(group => group.Number, StringComparer.Ordinal)
                            .ThenBy(group => group.Name, nameComparer).ToList();
                        ExcelWorksheet sheet = package.Workbook.Worksheets.Add(GetSheetName(region.Key, sheetNames));
                        WriteSheet(sheet, region.Key, students);
                    }

                    string fileName = year.ToString(CultureInfo.InvariantCulture) + "年度" + province.Key + "人调分组.xlsx";
                    string path = GetAvailablePath(outputFolder, fileName);
                    // CreateNew 防止覆盖已有家访表中的手工备注。
                    using (FileStream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                    {
                        try
                        {
                            package.SaveAs(stream);
                        }
                        catch
                        {
                            stream.Close();
                            File.Delete(path);
                            throw;
                        }
                    }
                    result.Files.Add(path);
                }
            }
            return result;
        }

        private static void WriteSheet(ExcelWorksheet sheet, string region, IList<FamilyVisitGroup> students)
        {
            int lastRow = students.Count + 2;
            ExcelRange table = sheet.Cells[1, 1, lastRow, Headers.Length];
            table.Style.Font.Name = "宋体";
            table.Style.Font.Size = 11;
            table.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            table.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            table.Style.WrapText = true;
            table.Style.Numberformat.Format = "@";
            table.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            table.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            sheet.Cells[1, 1, 1, Headers.Length].Merge = true;
            sheet.Cells[1, 1].Value = region + "（家访" + students.Count + "人）";
            sheet.Cells[1, 1].Style.Font.Size = 28;
            sheet.Row(1).Height = 44;
            sheet.Row(2).Height = 36;
            double[] widths = { 3.75, 8.25, 20.56, 23.81, 18.19, 22.47, 21.44, 8.73, 15.44 };
            for (int column = 1; column <= Headers.Length; column++)
            {
                sheet.Cells[2, column].Value = Headers[column - 1];
                sheet.Column(column).Width = widths[column - 1];
            }

            for (int index = 0; index < students.Count; index++)
            {
                int row = index + 3;
                FamilyVisitGroup student = students[index];
                sheet.Cells[row, 1].Style.Numberformat.Format = "0";
                sheet.Cells[row, 1].Value = index + 1;
                string[] values =
                {
                    student.Name, student.School, student.RegisteredAddress, student.HomeAddress,
                    student.ParentWork, student.Phone, student.Number, ""
                };
                int lines = 1;
                for (int column = 2; column <= Headers.Length; column++)
                {
                    string value = (values[column - 2] ?? "").Replace("\r\n", "\n").Replace('\r', '\n');
                    // 标识符、电话及以 = 开头的源文本均作为文本写入。
                    sheet.Cells[row, column].Value = value.Length == 0 ? null : (object)value;
                    lines = Math.Max(lines, GetWrappedLineCount(value, widths[column - 1]));
                }
                sheet.Row(row).Height = Math.Min(409, Math.Max(48, lines * 16 + 8));
            }

            sheet.View.FreezePanes(3, 1);
            sheet.PrinterSettings.PaperSize = ePaperSize.A4;
            sheet.PrinterSettings.Orientation = eOrientation.Landscape;
            sheet.PrinterSettings.FitToPage = true;
            sheet.PrinterSettings.FitToWidth = 1;
            sheet.PrinterSettings.FitToHeight = 0;
            sheet.PrinterSettings.RepeatRows = sheet.Cells["1:2"];
            sheet.PrinterSettings.PrintArea = table;
            sheet.PrinterSettings.LeftMargin = 0.75M;
            sheet.PrinterSettings.RightMargin = 0.75M;
            sheet.PrinterSettings.TopMargin = 1M;
            sheet.PrinterSettings.BottomMargin = 1M;
            sheet.PrinterSettings.HeaderMargin = 0.5M;
            sheet.PrinterSettings.FooterMargin = 0.5M;
        }

        private static int GetWrappedLineCount(string value, double width)
        {
            int lines = 0;
            foreach (string line in value.Replace("\r", "").Split('\n'))
            {
                int units = line.Sum(character => character > 127 ? 2 : 1);
                lines += Math.Max(1, (int)Math.Ceiling(units / Math.Max(1, width - 1)));
            }
            return lines;
        }

        private static string GetSheetName(string name, ISet<string> usedNames)
        {
            string clean = new string(name.Where(character => character >= ' '
                && ":\\/?*[]".IndexOf(character) < 0).ToArray()).Trim().Trim('\'');
            if (clean.Length == 0)
                clean = "待确认地区";
            if (clean.Length > 31)
                clean = clean.Substring(0, 31);
            string candidate = clean;
            for (int suffix = 2; !usedNames.Add(candidate); suffix++)
            {
                string ending = "(" + suffix + ")";
                candidate = clean.Substring(0, Math.Min(clean.Length, 31 - ending.Length)) + ending;
            }
            return candidate;
        }

        private static string GetAvailablePath(string folder, string fileName)
        {
            string safeName = new string(fileName.Select(character => character < ' '
                || "<>:\"/\\|?*".IndexOf(character) >= 0 ? '_' : character).ToArray());
            string path = Path.Combine(folder, safeName);
            for (int suffix = 2; File.Exists(path) || Directory.Exists(path); suffix++)
            {
                path = Path.Combine(folder, Path.GetFileNameWithoutExtension(safeName)
                    + "(" + suffix + ")" + Path.GetExtension(safeName));
            }
            return path;
        }
    }
}
