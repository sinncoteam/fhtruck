using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FH.Dispatch.Client.Controls
{
    public class MsgBox
    {
        /// <summary>
        /// 显示弹框
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MessageBoxResult Show(string content)
        {
            return MessageBox.Show(content, "提示");
        }

        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool Confirm(string content)
        {
            MessageBoxResult result = MessageBox.Show(content, "提示", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                return true;
            }
            return false;
        }
    }
}
