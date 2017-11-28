using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Common.Utils
{
    public class InstructionHelper
    {
        /// <summary>
        /// 将指令解析为数组
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static List<string> getFromInstuct(string instruction)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < instruction.Length; i += 2)
                {
                    list.Add(instruction.Substring(i, 2));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("非法指令", ex);
            }
        }
    }
}
