using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FH.Common.Utils;
using FH.Dispatch.Client.Base;
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Domain.Service;
using ViCore.Caching;
using ViCore.Logging;
using ViData;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initIcon();
        }
        
        #region 初始化窗体
        public static int HouseId = Convert.ToInt32(ConfigurationManager.AppSettings["houseid"]);
        public static Dispatch.Index dispatchIndex;
        public static Manage.RouteIndex routeIndex;
        public static Dispatch.TruckRouteList dispathTrList;
        public static Manage.TruckIndex truckIndex;
        public static Manage.RackIndex rackIndex;
        public static Dispatch.TruckRouteList trRouteList;
        public static Manage.RouteShow routeShow;
        /// <summary>
        /// 打开路径可视化窗体
        /// </summary>
        public static void ShowRouteShow(bool forselect = false)
        {
            if (routeShow != null && routeShow.IsVisible)
            {
                routeShow.isForSelect = forselect;
                routeShow.Show();
                routeShow.Activate();
                routeShow.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                routeShow = new Manage.RouteShow();
                routeShow.isForSelect = forselect;
                routeShow.Show();
            }
        }
        /// <summary>
        /// 打开日志窗体
        /// </summary>
        public static void ShowTruckRouteLog()
        {
            if (trRouteList != null && trRouteList.IsVisible)
            {
                trRouteList.Show();
                trRouteList.Activate();
                trRouteList.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                trRouteList = new Dispatch.TruckRouteList();
                trRouteList.Show();
            }
        }
        /// <summary>
        /// 打开仓位管理窗体
        /// </summary>
        public static void ShowRackIndex()
        {
            if (rackIndex != null && rackIndex.IsVisible)
            {
                rackIndex.Show();
                rackIndex.Activate();
                rackIndex.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                rackIndex = new Manage.RackIndex();
                rackIndex.Show();
            }
        }
        /// <summary>
        /// 打开叉车管理窗体
        /// </summary>
        public static void ShowTruckIndex()
        {
            if (truckIndex != null && truckIndex.IsVisible)
            {
                truckIndex.Show();
                truckIndex.Activate();
                truckIndex.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                truckIndex = new Manage.TruckIndex();
                truckIndex.Show();
            }
        }
        /// <summary>
        /// 打开调度窗体
        /// </summary>
        public static void ShowDispathIndex()
        {
            if (dispatchIndex != null && dispatchIndex.IsVisible)
            {
                dispatchIndex.Show();
                dispatchIndex.Activate();
                dispatchIndex.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                dispatchIndex = new Dispatch.Index();
                dispatchIndex.Show();
            }
        }
        /// <summary>
        /// 开打路径管理窗体
        /// </summary>
        public static void ShowRouteIndex()
        {
            if (routeIndex != null && routeIndex.IsVisible)
            {
                routeIndex.Show();
                routeIndex.Activate();
                routeIndex.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                routeIndex = new Manage.RouteIndex();
                routeIndex.Show();
            }
        }
        #endregion
        CRCHelper crcHelper = new CRCHelper();
        public static SerialPort sp;
        //Thread listenThread;
        System.Windows.Forms.NotifyIcon notifyIcon;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ShowDispathIndex();
            dispatchIndex.main = this;
            this.WindowState = System.Windows.WindowState.Minimized;            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //listenThread.Abort();
            if (sp != null && sp.IsOpen)
            {
                sp.Close();
            }
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ShowRouteIndex();
            routeIndex.main = this;
            this.WindowState = System.Windows.WindowState.Minimized;         
        }

        TruckService x_tkService = new TruckService();
        TruckLogService x_tlService = new TruckLogService();
        string sp_buffer;
        public const string sp_start_buf = "AA";
        /// <summary>
        /// 返回指令长度，前2位为起始位
        /// </summary>
        int sp_buffer_len = 14;
        int sp_read_len = 100;
        public void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sp_buffer != null && (sp_buffer.Length >= sp_buffer_len || !sp_buffer.StartsWith("AA")))  //清理漏洞数据
            {
                sp_buffer = null;
            }
            else if(sp_buffer != null && !sp_buffer.StartsWith("AAAA") && sp_buffer.Length > 2)
            {
                sp_buffer = null;
            }
            byte[] readBuffer = new byte[sp_read_len];
            sp.Read(readBuffer, 0, readBuffer.Length);
            string readstr = HexHelper.ToHexString(readBuffer);
            readstr = TrimHelper.Trim(readstr, '0');
            showDispatchText("info.原始指令: " + readstr);
            if (!CheckCRC(readstr))
            {
                bool cangoon = false;
                string newstr = readstr.Substring(0, readstr.Length);

                for (int i = 0; i < newstr.Length; i += 2)
                {
                    char a = newstr[i];
                    char b = newstr[i + 1];
                    if (a != '0' || b != '0')
                    {
                        string wd = string.Concat(a, b);
                        if (sp_buffer != null && sp_buffer.StartsWith(sp_start_buf))
                        {
                            if (sp_buffer.StartsWith(sp_start_buf + sp_start_buf))
                            {
                                sp_buffer += wd;
                            }
                            else if (wd == sp_start_buf)
                            {
                                sp_buffer += wd;
                            }
                            else
                            {
                                sp_buffer = null;
                            }
                        }
                        else if (wd == sp_start_buf)
                        {
                            sp_buffer = wd;
                        }
                        if (!string.IsNullOrEmpty(sp_buffer))
                        {
                            if (CheckCRC(sp_buffer))
                            {
                                cangoon = true;
                                readstr = sp_buffer;
                                sp_buffer = null;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (!cangoon)
                {
                    showDispatchText("error：反馈指令错误..." + readstr);
                    return;
                }
            }

            TruckLogInfo log = new TruckLogInfo()
            {
                Createtime = DateTime.Now,
                FullCode = readstr
            };
            List<string> list = InstructionHelper.getFromInstuct(readstr);
            log.Routecode = list[4];    //路径点
            log.RunStatus = list[3];    //运行状态
            log.Truckcode = list[2];    //叉车编号
            bool needlog = false;
            #region 重复处理
            string cacheKey = "listen_route_key" + log.FullCode;
            object obj = CacheManager.Instance.Get(cacheKey);
            if (obj != null)
            {
                List<TruckLogInfo> tList = obj as List<TruckLogInfo>;
                var item = tList.Where(a => a.FullCode == readstr).LastOrDefault();
                if (item == null)
                {
                    needlog = true;
                }
            }
            else
            {
                List<TruckLogInfo> tList = new List<TruckLogInfo>();
                tList.Add(log);
                CacheManager.Instance.Set(cacheKey, tList, 0, 15);
                needlog = true;
            }
            #endregion
            if (routeShow != null && routeShow.IsVisible)
            {
                Dispatcher.Invoke(new Action(() =>
                    {
                        routeShow.ShowTruckPosition(log.Routecode);
                    }));
            }
            if (needlog)
            {
                x_tlService.Insert(log);
                if (log.RunStatus != "AA")  //叉车接收到启动指令后的返回消息
                {
                    //将叉车更新为运行状态
                    x_tkService.Update(() => new TruckInfo() { Truckstatus = 1, TruckrouteCode =  log.Routecode }, a => a.Truckcode == log.Truckcode);
                }
                else
                {
                    int status = 1;
                    if (log.RunStatus == "02")  //停止指令
                    {
                        status = 0; //叉车已停止运行，恢复为可用状态（暂定）
                    }
                    x_tkService.Update(() => new TruckInfo() { Truckstatus = status, TruckrouteCode = log.Routecode }, a => a.Truckcode == log.Truckcode);
                }
            }
            if (log.RunStatus == "AA")  //叉车接收到启动指令后的返回消息
            {
                showDispatchText("start.叉车已启动：" + readstr);
            }
            else
            {
                if (log.RunStatus == "01")
                {
                    showDispatchText("runing.叉车运行到：" + readstr);
                }
                else
                {
                    showDispatchText("stop.叉车已停止运行：" + readstr);
                }
            }
        }

        void showDispatchText(string text)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (dispatchIndex != null && dispatchIndex.IsVisible)
                {
                    dispatchIndex.ShowListenEvent(text);
                }
            }));
        }

        bool CheckCRC(string str)
        {
            if (str.Length == sp_buffer_len)
            {
                string data = str.Substring(0, sp_buffer_len-4);
                string crc = str.Substring(sp_buffer_len-4, 4);
                if (crcHelper.CRCResult_ModBus(data) == crc)
                {
                    return true;
                }
            }
            return false;
        }

        public void initListener()
        {
            if (sp == null)
            {
                sp = new SerialPort();
                sp.PortName = ConfigurationManager.AppSettings["PortName"];
                sp.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
                sp.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["DataBits"]);
                sp.StopBits = StopBits.One;
                sp.Parity = Parity.None;
                sp.Encoding = Encoding.UTF8;
                sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                sp.Open();
                
                //byte[] bytes = new byte[] { 0x01, 0x01, 0x0f, 0x0f };
                //sp.Write(bytes, 0, bytes.Length);
                //if (listenThread == null)
                //{
                //    ThreadStart ts = new ThreadStart(DispathListener);
                //    listenThread = new Thread(ts);
                //    listenThread.Start();
                //}
            }
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_start.IsEnabled = false;
                initListener();                
                btn_start.Content = "启动成功";
            }
            catch (Exception ex)
            {
                MsgBox.Show("启动失败，请检查串口连接是否正常");
                btn_start.IsEnabled = true;
                sp.Dispose();
                sp = null;
                Logging4net.WriteError(ex, "串口监听启动失败");
            }
        }

        private void btn_truck_Click(object sender, RoutedEventArgs e)
        {
            ShowTruckIndex();
            truckIndex.main = this;
            this.WindowState = System.Windows.WindowState.Minimized;   
        }

        private void btn_rack_Click(object sender, RoutedEventArgs e)
        {
            ShowRackIndex();
            rackIndex.main = this;
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void initIcon()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.BalloonTipText = "应用程序已启动"; //设置程序启动时显示的文本
            this.notifyIcon.Text = "智能叉车中控程序";//最小化到托盘时，鼠标点击时显示的文本
            this.notifyIcon.Icon = new System.Drawing.Icon(Environment.CurrentDirectory+"\\resource\\image\\notify.ico");//程序图标
            this.notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseDoubleClick);
            this.notifyIcon.ShowBalloonTip(500);
        }

        void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 写入数据到串口
        /// </summary>
        /// <param name="route"></param>
        public static void WriteToPort(string route)
        {
            byte[] bys = HexHelper.ToHexByte(route);
            if (sp != null && sp.IsOpen)
            {
                for (int i = 0; i < 3; i++)
                {
                    sp.Write(bys, 0, bys.Length);
                    Thread.Sleep(50);
                }
            }
        }

        //void DispathListener()
        //{
        //    while (true)
        //    {
        //        var pathList = x_dService.GetTopItems(1);
        //        if (pathList.Count > 0)
        //        {
        //            var truckList = x_tkService.GetTopTrucks(1);

        //        }
        //        Thread.Sleep(1000);
        //    }
        //}
    }
}
