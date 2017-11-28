using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FH.Common.Utils;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RouteShowSize.xaml 的交互逻辑
    /// </summary>
    public partial class RouteShowSize : Window
    {
        public RouteShowSize()
        {
            InitializeComponent();
        }
        IniHelper ini = new IniHelper();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string psize = ini.Read("psize");
            string pleft = ini.Read("pleft");
            string pup = ini.Read("pup");
            tb_psize.Text = psize;
            tb_left.Text = pleft;
            tb_up.Text = pup;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string psize = tb_psize.Text;
            string pleft = tb_left.Text;
            string pup = tb_up.Text;
            int tmp;
            if (int.TryParse(psize, out tmp) && int.TryParse(pleft, out tmp) && int.TryParse(pup, out tmp))
            {
                ini.Write("psize", psize);
                ini.Write("pleft", pleft);
                ini.Write("pup", pup);
                MsgBox.Show("保存成功");
                this.Close();
                return;
            }
            MsgBox.Show("输入必须为整型数字");
        }
    }
}
