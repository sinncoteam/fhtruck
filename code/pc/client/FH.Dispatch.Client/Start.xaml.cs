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
using ViData;
using System.Threading;

namespace FH.Dispatch.Client
{
    /// <summary>
    /// Start.xaml 的交互逻辑
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        void Init()
        {
            Thread.Sleep(500);
            ShowText("加载：正在加载日志配置......");
            log4net.Config.XmlConfigurator.Configure();
            Thread.Sleep(500);
            ShowText("加载：正在加载数据连接配置......");
            DMHelper.Instance.ExportMapping();
            Thread.Sleep(500);
            ShowText("完成：正在启动控制程序......");
            Thread.Sleep(1000);

            Dispatcher.Invoke(new Action(() =>
            {
                this.Hide();
                MainWindow mw = new MainWindow();
                mw.Show();
            }));  
            
        }

        void ShowText(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                this.lb_staus.Content = text;
            }));           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadStart ts = new ThreadStart(() => { Init(); });
            Thread th = new Thread(ts);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
