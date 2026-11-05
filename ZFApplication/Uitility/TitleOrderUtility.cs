using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZFApplication.Uitility
{
    public class TitleOrderUtility
    {
        public static string orderstring = "本人," +
            "父亲|爸爸|父|继父,母亲|妈妈|母|继母," +
            "祖父|爷爷|爷|继祖父,祖母|奶奶|奶|继祖母,外祖父|外公|继外祖父,外祖母|外婆|继外祖母," +
            "儿子|儿|继子,女儿|女|继女," +
            "哥哥|哥|兄|兄长|继兄,嫂子|嫂|继嫂,弟弟|弟|继弟,弟媳|继弟媳," +
            "姐姐|姐|继姐,姐夫|继姐夫,妹妹|妹|继妹,妹夫|继妹夫," +
            "伯父|伯伯|伯|继伯父,叔父|叔叔|叔|继叔父,姑母|姑妈|姑|继姑母," + //17
            "舅父|舅舅|舅|继舅父,姨|姨母|继姨母";

        public static string mergetitel =
            "本人:本人," +
            "配偶:妻子|妻|老婆|老公|丈夫|夫," +
            "子女:儿子|儿|女儿|女," +
            "养子女:养儿|养女," +
            "孙子女:孙子|孙女|孙," +
            "继子女:继子|继女," +
            "外孙子女:外孙|外孙女," +
            "父母:父亲|爸爸|父|母亲|妈妈|母," +
            "养父母:养父|养母," +
            "继父母:继父|继母," +
            "祖母:祖父|爷爷|爷," +
            "祖父:祖母|奶奶|奶," +
            "外祖父:外祖父|外公," +
            "外祖母:外祖母|外婆," +
            "兄弟姐妹:哥哥|哥|兄|兄长|继兄|弟弟|弟|继弟|姐姐|姐|继姐|妹妹|妹|继妹," +
            "堂兄弟姐妹:堂兄|堂弟|堂哥|堂姐|堂妹," +
            "表兄弟姐妹:表兄|表弟|表哥|表姐|表妹," +
            "公公:公公," +
            "婆婆:婆婆," +
            "岳父:岳父," +
            "岳母:岳母," +
            "嫂:嫂|唐嫂|表嫂|嫂子|继嫂" +
            "弟妹:弟妹," +
            "妹夫:妹夫," +
            "姐夫:姐夫";

        private static Dictionary<string, string> titleMap = BuildMap();
        private static Dictionary<string, string> BuildMap()
        {
            var map = new Dictionary<string, string>();
            string[] entries = mergetitel.Split(',');

            foreach (string entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry)) continue;
                var parts = entry.Split(':');
                if (parts.Length != 2) continue;

                string standard = parts[0];
                string[] synonyms = parts[1].Split('|');
                foreach (string word in synonyms)
                {
                    if (!map.ContainsKey(word))
                        map[word] = standard;
                }
            }

            return map;
        }
        public static string NormalizeRelation(string input)
        {
            return titleMap.ContainsKey(input) ? titleMap[input] : "其他亲属";
        }

        public static int getOrder(string title)
        {
            string orderstring = TitleOrderUtility.orderstring;
            string[] orders = orderstring.Split(',');
            int order = 100;
            for (int i = 0; i < orders.Length; i++)
            {
                string[] titles = orders[i].Split('|');
                for (int j = 0; j < titles.Length; j++)
                {
                    if (title == titles[j])
                    {
                        order = i;
                        break;
                    }
                }
            }
            return order;
        }
        public static string GetGenderFromTitle(string title)
        {
            string orderstring = TitleOrderUtility.orderstring;

            // 拆分为每一组关系
            var groups = orderstring.Split(',');

            // 男性关键字集合
            var maleKeywords = new HashSet<string> {
                "父亲", "爸爸", "父", "继父",
                "祖父", "爷爷", "爷", "继祖父",
                "外祖父", "外公", "继外祖父",
                "儿子", "儿", "继子",
                "哥哥", "哥", "兄", "兄长", "继兄",
                "姐夫", "继姐夫",
                "弟弟", "弟", "继弟",
                "妹夫", "继妹夫",
                "伯父", "伯伯", "伯", "继伯父",
                "叔父", "叔叔", "叔", "继叔父",
                "舅父", "舅舅", "舅", "继舅父"
            };

                    // 女性关键字集合
            var femaleKeywords = new HashSet<string> {
                "母亲", "妈妈", "母", "继母",
                "祖母", "奶奶", "奶", "继祖母",
                "外祖母", "外婆", "继外祖母",
                "女儿", "女", "继女",
                "姐姐", "姐", "继姐",
                "嫂子", "嫂", "继嫂",
                "弟媳", "继弟媳",
                "妹妹", "妹", "继妹",
                "姑母", "姑妈", "姑", "继姑母",
                "姨", "姨母", "继姨母"
            };

            if (maleKeywords.Contains(title)) return "男";
            if (femaleKeywords.Contains(title)) return "女";
            if (title == "本人") return "未知"; // 或可根据其他字段判断本人性别
            return "未知";
        }

    }
}
