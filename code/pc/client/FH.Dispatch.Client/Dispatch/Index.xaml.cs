using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using FH.Common.Utils.Dijkstra;
using FH.Dispatch.Domain.Service;
using FH.Dispatch.Domain.Info;
using System.Configuration;
using System.Threading;
using FH.Dispatch.Client.Manage;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client.Dispatch
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();            
        }
        public MainWindow main { get; set; }
        RouteService x_rService = new RouteService();
        TruckService x_tService = new TruckService();
        RackService x_rkService = new RackService();
        RackLayerService x_rlService = new RackLayerService();
        IList<RouteInfo> routelist;
        IList<RackInfo> rackList;
        int houseId = MainWindow.HouseId;
        void InitData()
        {
           
            var tList = x_tService.Get(a => a.Isvalid == 1 && a.HouseId == houseId).OrderBy(a => a.Truckcode).ToList();
            cb_trucklist.ItemsSource = tList;
            cb_trucklist.DisplayMemberPath = "Truckcode";                     

            routelist = x_rService.Get(a => a.IsValid == 1 && a.HouseId ==  houseId);
            //cb_start_route.ItemsSource = routelist.OrderByDescending(a => a.Isstart).OrderBy(a => a.Routecode);
            //cb_start_route.DisplayMemberPath = "Routecode";
            //if (tList.Count > 0 )
            //{
            //    string curoute = tList[0].TruckrouteCode;
            //    if (!string.IsNullOrEmpty(curoute))
            //    {
            //        foreach (var ritem in routelist)
            //        {
            //            if (ritem.Routecode == curoute)
            //            {
            //                cb_start_route.Text = curoute;
            //                break;
            //            }
            //        }                    
            //    }
            //}
            cb_trucklist.SelectedIndex = 0;   


            rackList = x_rkService.Get(a => a.Isvalid == 1 && a.HouseId == houseId);
            cb_pack_route.ItemsSource = rackList.OrderBy(a => a.Rackcode);
            cb_pack_route.DisplayMemberPath = "Rackname";
            cb_pack_route.SelectedValuePath = "Rackcode";

            int r = 0, r2 = 0;
            Random rnd = new Random();
            if (rackList.Count > 1)
            {
                r = rnd.Next(0, rackList.Count);
            }
            if (rackList.Count > 1)
            {
                r2 = rnd.Next(1, rackList.Count);
            }
            cb_pack_route.SelectedIndex = r;
            var rack_item = rackList.Where(a => a.Rackcode == cb_pack_route.SelectedValue.ToString()).FirstOrDefault();
            var rlList = x_rlService.Get(a => a.RackId == rack_item.Id);
            cb_pack_layer.ItemsSource = rlList;
            cb_pack_layer.DisplayMemberPath = "RackLayerNum";
            cb_pack_layer.SelectedValuePath = "RackLayerCode";
            cb_pack_layer.SelectedIndex = 0;

            cb_downpack_route.ItemsSource = rackList.OrderBy(a => a.Rackcode);
            cb_downpack_route.DisplayMemberPath = "Rackname";
            cb_downpack_route.SelectedValuePath = "Rackcode";
            cb_downpack_route.SelectedIndex = r2;
            var rack_item2 = rackList.Where(a => a.Rackcode == cb_downpack_route.SelectedValue.ToString()).FirstOrDefault();
            var rlList2 = x_rlService.Get(a => a.RackId == rack_item2.Id);
            cb_downpack_layer.ItemsSource = rlList2;
            cb_downpack_layer.DisplayMemberPath = "RackLayerNum";
            cb_downpack_layer.SelectedValuePath = "RackLayerCode";
            cb_downpack_layer.SelectedIndex = 0;


            //cb_end_route.ItemsSource = routelist;
            //cb_end_route.DisplayMemberPath = "Routecode";
            //cb_end_route.SelectedIndex = 0;
            //var item = routelist.Where(a => a.Isstart == 2).FirstOrDefault();
            //if (item != null && !string.IsNullOrEmpty(item.Routecode))
            //{
            //    cb_end_route.Text = item.Routecode;
            //}
        }

        public void ShowListenEvent(string text)
        {
            this.Dispatcher.BeginInvoke(new Action(() => 
                {
                    this.x_sbar.ShowText("叉车状态 "+ text);
                    if (text.StartsWith("start."))
                    {
                        this.lb_truck_status.Content = "运行中";
                        ShowText("接收到反馈指令，叉车已启动：" + text);
                    }
                    else if (text.StartsWith("stop."))
                    {
                        this.lb_truck_status.Content = "待命";
                        ShowText("接收到反馈指令，叉车已停止：" + text);
                    }
                    else
                    {
                        this.lb_truck_status.Content = "经过RF点";
                        ShowText(text);
                    }
                }
                ));
        }

        void ShowText(string text)
        {
            this.rtb_result.AppendText(text+"\r");
            rtb_result.ScrollToEnd();
        }

        /// <summary>
        /// 取货点
        /// </summary>
        string inpackroute;
        /// <summary>
        /// 卸货点
        /// </summary>
        string outpackroute;
        /// <summary>
        /// 开始调度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            direction = DirectionWay.None;
            ShowText("正在发送运行指令...");
            x_sbar.ShowText("正在处理...");
            #region old method
            //InstructionInfo insInfo = new InstructionInfo();

            //insInfo.TruckCode = cb_trucklist.Text;//叉车
            //insInfo.StartCode = "01";

            //var truck_item = x_tService.Get(a => a.Truckcode == insInfo.TruckCode && a.HouseId == houseId).FirstOrDefault();
            //if (truck_item != null && truck_item.Truckstatus != 0)
            //{
            //    MsgBox.Show("叉车"+ insInfo.TruckCode +"正在运行中，请等待叉车运行完成。");
            //    return;
            //}

            //string startcode = cb_start_route.Text;
            //if (string.IsNullOrEmpty(startcode))
            //{
            //    MsgBox.Show("叉车暂无停车位信息，请选择叉车出发点");
            //    return;
            //}
            //string rackcode = cb_pack_route.SelectedValue.ToString();
            //var rack_item = x_rkService.Get(a => a.Rackcode == rackcode).FirstOrDefault();
         
            //inpackroute = insInfo.InPackCode = rack_item.RouteCode; //取货点
            //string ulayer = cb_pack_layer.Text;
            //if (string.IsNullOrEmpty(ulayer))
            //{
            //    MsgBox.Show("取货点没有层位数据，请先完善层位信息");
            //    return;
            //}
            //insInfo.InPackLayer = Convert.ToInt32(cb_pack_layer.Text).ToString("x2");

            //ComboBoxItem cb_item = cb_packtype.SelectedItem as ComboBoxItem;
            //insInfo.InOut = cb_item.Tag.ToString(); //出库入库

            //rackcode = cb_downpack_route.SelectedValue.ToString();
            //var out_rack_item = x_rkService.Get(a => a.Rackcode == rackcode).FirstOrDefault();
            //outpackroute = insInfo.OutPackCode = out_rack_item.RouteCode;
          
            //string dlayer = cb_downpack_layer.Text;
            //if (string.IsNullOrEmpty(dlayer))
            //{
            //    MsgBox.Show("卸货点没有层位数据，请先完善层位信息");
            //    return;
            //}
            //insInfo.OutPackLayer = Convert.ToInt32(dlayer).ToString("x2");
           
            //string end = cb_end_route.Text;
            //var end_item = routelist.Where(a => a.Routecode == end).FirstOrDefault();
           
            //Dictionary<string, List<DNode>> dict = SetToNodes();
            //PathMathHelper.InitRouteNodes(dict);
            //string path1 = startcode + PathMathHelper.Start(startcode, inpackroute);   //取货路径

            //PathMathHelper.InitRouteNodes(dict);
            //string path2 = PathMathHelper.Start(inpackroute, outpackroute);     //卸货路径

            //PathMathHelper.InitRouteNodes(dict);
            //string path3 = PathMathHelper.Start(outpackroute, end);      //停车路径

            
            //ComputePath(insInfo, path1, path2, rack_item);
            //ComputePath(insInfo, path2, path3, out_rack_item);
            //ComputePath(insInfo, path3);
            //if (end_item != null && end_item.Isstart == 2)  //如果终点是停车位，则反转停车位的转向
            //{
            //    if (insInfo.RouteWay.Count > 2)
            //    {
            //        var wayinfo = insInfo.RouteWay.Last();
            //        if (wayinfo.Way == TurnWay.Left)
            //        {
            //            wayinfo.Way = TurnWay.Right;
            //            insInfo.AddItem(wayinfo.RouteCode, TurnWay.Back);
            //        }
            //        else if (wayinfo.Way == TurnWay.Right)
            //        {
            //            wayinfo.Way = TurnWay.Left;
            //            insInfo.AddItem(wayinfo.RouteCode, TurnWay.Back);
            //        }
            //    }
            //}
            //insInfo.AddItem(end, TurnWay.Stop);
            #endregion
            ComboBoxItem cb_item = cb_packtype.SelectedItem as ComboBoxItem;
            string iotype = cb_item.Tag.ToString(); //出库入库
            InOutType intype = InOutType.Out;
            if (iotype == "02")
            {
                intype = InOutType.In;
            }
            string rackcode = cb_pack_route.SelectedValue.ToString();
            var rack_item = x_rkService.Get(a => a.Rackcode == rackcode).FirstOrDefault();

            inpackroute = rack_item.RouteCode; //取货点
            string ulayer = cb_pack_layer.SelectedValue.ToString();

            string rackcode2 = cb_downpack_route.SelectedValue.ToString();
            var out_rack_item = x_rkService.Get(a => a.Rackcode == rackcode2).FirstOrDefault();
            outpackroute =  out_rack_item.RouteCode;

            string dlayer = cb_downpack_layer.SelectedValue.ToString();
            InstructionInfo insInfo = new DispathService(houseId).SetToMap(intype, ulayer, dlayer, "");
            string path1 = insInfo.PathList[0];
            string path2 = insInfo.PathList[1];
            string path3 = insInfo.PathList[2];
            
            #region 显示路径
            string[] pathArr = (path1 + path2 + path3).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (MainWindow.routeShow != null && MainWindow.routeShow.IsVisible)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    MainWindow.routeShow.ClearTruckLine();
                    foreach (string p in pathArr)
                    {
                        MainWindow.routeShow.ShowTruckLine(p);
                    }
                }));
            }
            #endregion
            string route = insInfo.ResetInstruction(rackList);
            MainWindow.WriteToPort(route);
            #region 显示算法结果
            string route1 = route.Substring(0, 14);
            string route2 = route.Substring(14, route.Length - 14);
            ShowText(route1);
            ShowText(route2);
            
            ShowText("已向叉车" + insInfo.TruckCode + "发送运行指令，等待启动……");
            string showpath = "运行路径："  +  path1 + ", 取货 " + path2 + " 卸货 "+ path3 +" 停车";

            ShowText(showpath);
            //ShowText("叉车" + cb_trucklist.Text + "已运行结束");
            ShowText("----------------------------------------");
            x_sbar.ShowText("处理完成.");
            #endregion
        }

        #region old method
        DirectionWay direction = DirectionWay.None;
        string lastRoute = "";
        /// <summary>
        /// 计算路径以及转向逻辑
        /// </summary>
        /// <param name="insInfo"></param>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <param name="rackItem"></param>
        private void ComputePath(InstructionInfo insInfo, string path1, string path2 = null, RackInfo rackItem = null)
        {
            string[] path1Arr = path1.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int path1Length = path1Arr.Length;
            for (int i = 0; i < path1Length; i++)
            {
                string path = path1Arr[i];
                if (i == 0 && !string.IsNullOrEmpty(lastRoute)) //如果取/卸货仓位是紧邻的仓位，则指令需要跳过下一位置
                {
                    var rackInfo = rackList.Where(a => a.RouteCode == path).FirstOrDefault();
                    if (rackInfo != null)
                    {
                        var lastInfo = rackList.Where(a => a.RouteCode == lastRoute).FirstOrDefault();
                        if (lastInfo != null)
                        {
                            continue;
                        }
                    }        
                }
                lastRoute = path;
                var item = routelist.Where(a => a.Routecode == path1Arr[i]).FirstOrDefault();
                if (i + 1 < path1Length)    //还有下一节点
                {
                    string next = path1Arr[i + 1];
                    var nextItem = routelist.Where(a => a.Routecode == next).FirstOrDefault();
                    int xsub = item.X - nextItem.X;
                    int ysub = item.Y - nextItem.Y;
                    if (xsub == 0)
                    {
                        if (ysub > 0)   //向南
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.South));
                            direction = DirectionWay.South;

                        }
                        else  //向北
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.North));

                            direction = DirectionWay.North;
                        }
                    }
                    else if (ysub == 0)
                    {
                        if (xsub > 0)  //向西
                        {
                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.West));

                            direction = DirectionWay.West;
                        }
                        else  //向东
                        {

                            insInfo.AddItem(path, insInfo.getWay(direction, DirectionWay.Easet));

                            direction = DirectionWay.Easet;
                        }
                    }

                }
                else if (!string.IsNullOrEmpty(path2))  //一期默认货架在右侧，即X正，Y负
                {
                    setRouteByRack(insInfo, path2, path, item, rackItem);
                }
            }
        }

        /// <summary>
        /// 计算仓位的转向逻辑
        /// </summary>
        /// <param name="insInfo"></param>
        /// <param name="nextPathArray"></param>
        /// <param name="lastPath"></param>
        /// <param name="item"></param>
        /// <param name="rackItem"></param>
        private void setRouteByRack(InstructionInfo insInfo, string nextPathArray, string lastPath, RouteInfo item, RackInfo rackItem)
        {
            string[] path2Arr = nextPathArray.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string next = path2Arr[0];
            var nextItem = routelist.Where(a => a.Routecode == next).FirstOrDefault();
            int xsub = item.X - nextItem.X;
            int ysub = item.Y - nextItem.Y;
            int xrack = 0;
            int yrack = 0;
            if (rackItem != null)
            {
                xrack = item.X - rackItem.X;
                yrack = item.Y - rackItem.Y;
            }
            if (xsub == 0)
            {
                if (ysub > 0)   //向南
                {
                    if (lastPath == inpackroute || lastPath == outpackroute)
                    {
                        if (direction == DirectionWay.South)
                        {
                            if (xrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.North)
                        {
                            if (xrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                    }
                    direction = DirectionWay.South;
                }
                else  //向北
                {
                    if (lastPath == inpackroute || lastPath == outpackroute)
                    {
                        if (direction == DirectionWay.South)
                        {
                            if (xrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.North)
                        {
                            if (xrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.Easet)
                        {

                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Next);
                        }
                    }
                    direction = DirectionWay.North;
                }
            }
            else if (ysub == 0)
            {
                if (xsub > 0)  //向西
                {
                    if (lastPath == inpackroute || lastPath == outpackroute)
                    {
                        if (direction == DirectionWay.South)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Left);
                        }
                        else if (direction == DirectionWay.Easet)
                        {
                            if (yrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            if (yrack > 0) //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }                            
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Right);
                        }
                    }
                    direction = DirectionWay.West;
                }
                else  //向东
                {
                    if (lastPath == inpackroute || lastPath == outpackroute)
                    {
                        if (direction == DirectionWay.South)
                        {
                            insInfo.AddItem(lastPath, TurnWay.Left);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Right);
                        }
                        else if (direction == DirectionWay.Easet)
                        {
                            if (yrack > 0)  //右手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else if (direction == DirectionWay.West)
                        {
                            if (yrack > 0)  //左手
                            {
                                insInfo.AddItem(lastPath, TurnWay.Right);
                                insInfo.AddItem(lastPath, TurnWay.Right);
                            }
                            else
                            {
                                insInfo.AddItem(lastPath, TurnWay.Left);
                                insInfo.AddItem(lastPath, TurnWay.Left);
                            }
                        }
                        else
                        {
                            insInfo.AddItem(lastPath, TurnWay.Right);
                            //insInfo.AddItem(lastPath, TurnWay.Back);
                            //insInfo.AddItem(lastPath, TurnWay.Left);
                        }
                    }
                    direction = DirectionWay.Easet;
                }
            }
        }

        private Dictionary<string, List<DNode>> SetToNodes()
        {
            Dictionary<string, List<DNode>> dict = new Dictionary<string, List<DNode>>();
            foreach (var item in routelist)
            {
                List<DNode> dnList = new List<DNode>();
                if (!string.IsNullOrEmpty(item.Nextroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Nextroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Nextroutecode, item.Nextroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Leftroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Leftroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Leftroutecode, item.Leftroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Rightroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Rightroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Rightroutecode, item.Rightroutevalue);
                        dnList.Add(dn);
                    }
                }
                if (!string.IsNullOrEmpty(item.Backroutecode))
                {
                    var tmp = routelist.Where(a => a.Routecode == item.Backroutecode && a.IsValid == 1).FirstOrDefault();
                    if (tmp != null)
                    {
                        DNode dn = new DNode(item.Backroutecode, item.Backroutevalue);
                        dnList.Add(dn);
                    }
                }
                dict.Add(item.Routecode, dnList);
            }
            return dict;
        }
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void btn_showroute_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowRouteShow();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (main != null)
            {
                main.Show();
                main.WindowState = System.Windows.WindowState.Normal;
                main.Activate();
            }
        }

        private void btn_showtruck_Click(object sender, RoutedEventArgs e)
        {
            Dispatch.TruckRouteList trList = new TruckRouteList();
            trList.Show();
        }

        private void cb_pack_route_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            if (cb.SelectedValue != null)
            {
                string rackcode = cb.SelectedValue.ToString();
                var item = x_rkService.Get(a => a.Rackcode == rackcode).FirstOrDefault();
                if (item != null)
                {
                    var layerList = x_rlService.Get(a => a.RackId == item.Id);
                    cb_pack_layer.ItemsSource = layerList;
                    cb_pack_layer.SelectedIndex = 0;
                }
            }
        }

        private void cb_downpack_route_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = e.Source as ComboBox;
            if (cb.SelectedValue != null)
            {
                string rackcode = cb.SelectedValue.ToString();
                var item = x_rkService.Get(a => a.Rackcode == rackcode).FirstOrDefault();
                if (item != null)
                {
                    var layerList = x_rlService.Get(a => a.RackId == item.Id);
                    cb_downpack_layer.ItemsSource = layerList;
                    cb_downpack_layer.SelectedIndex = 0;
                }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            rtb_result.Document.Blocks.Clear();
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            InstructionInfo ins = new InstructionInfo()
            {
                StartCode = "02",   //停车
                InPackCode = inpackroute,
                OutPackCode = outpackroute,
                InOut = "01",
                InPackLayer = "01",
                OutPackLayer = "01",
                TruckCode = cb_trucklist.Text
            };
            string route = ins.ResetInstruction();
            MainWindow.WriteToPort(route);
        }

        int btn_select_type = 0;
        private void btn_select1_Click(object sender, RoutedEventArgs e)
        {
            btn_select_type = 0;
            RouteShow rs = new RouteShow()
            {
                isForSelect = true,
                dpIndex = this
            };
            rs.ShowDialog();
        }
        private void btn_select2_Click(object sender, RoutedEventArgs e)
        {
            btn_select_type = 1;
            RouteShow rs = new RouteShow()
            {
                isForSelect = true,
                dpIndex = this
            };
            rs.ShowDialog();
        }

        /// <summary>
        /// 设置选中的仓位
        /// </summary>
        /// <param name="rackname"></param>
        /// <param name="rackcode"></param>
        /// <param name="layernum"></param>
        public void SetSelectRack(string rackname, string rackcode,string layernum)
        {
            if (btn_select_type == 0)
            {
                cb_pack_route.ItemsSource = rackList;
                for (int i = 0; i < rackList.Count; i++)
                {
                    RackInfo ri = rackList[i];
                    if (ri.Rackname == rackname)
                    {
                        cb_pack_route.SelectedIndex = i;
                        break;
                    }
                }               
            }
            else
            {
                cb_downpack_route.ItemsSource = rackList;
                for (int i = 0; i < rackList.Count; i++)
                {
                    RackInfo ri = rackList[i];
                    if (ri.Rackname == rackname)
                    {
                        cb_downpack_route.SelectedIndex = i;
                        break;
                    }
                }    
            }
        }

        //private void cb_trucklist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox cb = sender as ComboBox;
        //    TruckInfo cbitem = (cb.SelectedItem as TruckInfo);
        //    if (cbitem != null)
        //    {
        //        string vl = cbitem.Truckcode;
        //        TruckInfo tk = x_tService.Get(a => a.Truckcode == vl).FirstOrDefault();
        //        if (tk != null && !string.IsNullOrEmpty(tk.TruckrouteCode))
        //        {
        //            cb_start_route.Text = tk.TruckrouteCode;
        //        }
        //    }
        //}

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void cb_packtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_inout_status != null)
            {
                ComboBox cb = sender as ComboBox;
                ComboBoxItem item = cb.SelectedItem as ComboBoxItem;
                if (item != null)
                {
                    lb_inout_status.Content = item.Content;
                }
            }
        }

        
    }
}
