using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailApp.Utility
{
    public class TitleOrderUtility
    {
        public static int getOrder(string title)
        {
            string orderstring = "本人," +
            "父亲|爸爸|父|继父,母亲|妈妈|母|继母," +
            "祖父|爷爷|爷|继祖父,祖母|奶奶|奶|继祖母,外祖父|外公|继外祖父,外祖母|外婆|继外祖母," +
            "儿子|儿|继子,女儿|女|继女," +
            "哥哥|哥|兄|兄长|继兄,嫂子|嫂|继嫂,弟弟|弟|继弟,弟媳|继弟媳," +
            "姐姐|姐|继姐,姐夫|继姐夫,妹妹|妹|继妹,妹夫|继妹夫," +
            "伯父|伯伯|伯|继伯父,叔父|叔叔|叔|继叔父,姑母|姑妈|姑|继姑母," + //17
            "舅父|舅舅|舅|继舅父,姨|姨母|继姨母";
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
    }
}
