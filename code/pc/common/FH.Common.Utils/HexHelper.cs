using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FH.Common.Utils
{
    public class HexHelper
    {
        /// <summary>
        /// 16 to string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = "";

            if (bytes != null)
            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            } 
            return hexString;
        }

        /// <summary>
        /// string to byte 16
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToHexByte(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] bytes = new byte[str.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                }
                return bytes;
            }
            return null;
        }

    }
}
