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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FH.Dispatch.Client.Manage;

namespace FH.Dispatch.Client.Controls
{
    /// <summary>
    /// TopMenu.xaml 的交互逻辑
    /// </summary>
    public partial class TopMenu : UserControl
    {
        public TopMenu()
        {
            InitializeComponent();
        }

        private void mi_exit_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.sp != null && MainWindow.sp.IsOpen)
            {
                MainWindow.sp.Close();
            }
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void mi_route_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowRouteIndex();
        }

        private void mi_dispath_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowDispathIndex();
        }

        private void mi_truck_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowTruckIndex();
        }

        private void mi_rack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowRackIndex();
        }

        private void mi_log_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowTruckRouteLog();
        }

        private void mi_showroute_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowRouteShow();
        }

        private void mi_showroutesize_Click(object sender, RoutedEventArgs e)
        {
            RouteShowSize rss = new RouteShowSize();
            rss.ShowDialog();
        }

        private void mi_closesp_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.sp != null && MainWindow.sp.IsOpen)
            {
                MainWindow.sp.Close();
            }
            MsgBox.Show("已关闭");
        }

        private void mi_opensp_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.sp == null)
            {
                MsgBox.Show("打开失败，端口尚未初始化，请从主界面启动调度端口");
                return;
            }
            else if (!MainWindow.sp.IsOpen)
            {
                MainWindow.sp.Open();                
            }
            MsgBox.Show("打开成功");
        }
         

         
    }
}
