using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaCrafts.Gateway.Common.Helper
{
   public class TextHelper
    {
        /// <summary>
        /// 格式化字符串长度,超出部分显示省略号,区分汉字和字母。
        /// </summary>
        /// <param name="rawstr">要截取的字符串</param>
        /// <param name="n">截取长度,多出部分用...代替</param>
        /// <returns></returns>
        public static string StringCut(string rawstr, int n)
        {
            string temp = string.Empty;
            //如果原字符串长度比需要的长度n小,直接返回原字符串
            if (System.Text.Encoding.Default.GetByteCount(rawstr) <= n)
            {
                return rawstr;
            }
            else
            {
                int t = 0;
                char[] q = rawstr.ToCharArray();
                for (int i = 0; i < q.Length && t < n; i++)
                {
                    if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
                    {
                        temp += q[i];
                        t += 2;
                    }
                    else
                    {
                        temp += q[i];
                        t++;
                    }
                }
                return (temp + "...");
            }
        }
    }

}
