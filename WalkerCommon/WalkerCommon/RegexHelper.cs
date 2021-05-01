using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WalkerCommon
{
    public class RegexHelper
    {
        /// <summary>
        /// 获取第一个匹配项
        /// </summary>
        /// <param name="str">要匹配的字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns></returns>
        public static string GetFirstMatchValue(string str,string regex)
        {
            Regex reg = new Regex(regex);
            //例如我想提取记录中的NAME值
            Match match = reg.Match(str);
            string value = match.Groups[1].Value;

            return value;
        }
    }
}
