using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Common.Utils
{
    public class TrimHelper
    {
        /// <summary>
        /// 去掉字符串前后的数字，并补足偶数位
        /// </summary>
        /// <param name="orignal"></param>
        /// <param name="trimstr"></param>
        /// <returns></returns>
        public static string Trim(string orignal, char trimstr)
        {
            if (!string.IsNullOrEmpty(orignal))
            {
                string tmp = orignal.TrimStart(trimstr);
                if (tmp.Length % 2 != 0)
                {
                    tmp = trimstr + tmp;
                }
                string newstr = tmp.TrimEnd(trimstr);
                if (newstr.Length % 2 != 0)
                {
                    newstr += trimstr;
                }
                return newstr;
            }
            return null;
        }
    }
}
