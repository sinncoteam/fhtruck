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
using FH.Dispatch.Domain.Service;
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Client.Controls;
using FH.Common.Utils;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RouteShow.xaml 的交互逻辑
    /// </summary>
    public partial class RouteShow : Window
    {
        public RouteShow()
        {
            InitializeComponent();
        }
        public Dispatch.Index dpIndex;
        RouteService x_rService = new RouteService();
        RackService x_rkService = new RackService();
        TruckService x_tkService = new TruckService();
        IList<RouteInfo> routeList;
        IList<RackInfo> rackList;
        IList<TruckInfo> truckList;
        Dictionary<string, List<string>> pathDict;
        public int pSize = 70;
        public int xPoint = 200;
        public int yPoint = 400;
        int houseId = MainWindow.HouseId;
        public bool isForSelect;
        public void initData()
        {
            pathDict = new Dictionary<string, List<string>>();
            foreach (ComboBoxItem item in cb_house.Items)
            {
                if (item.Content.ToString() == houseId.ToString())
                {
                    item.IsSelected = true;
                    break;
                }
            }
            x_path_grid.Children.Clear();
            tb_size.Text = pSize.ToString();
            tb_x.Text = xPoint.ToString();
            tb_y.Text = yPoint.ToString();
            routeList = x_rService.Get(a => a.IsValid == 1 && a.HouseId == houseId).OrderBy(a => a.Routecode).ToList();

            
            var first_item = routeList.Where(a => a.Isstart == 1).FirstOrDefault();
            if (first_item == null)
            {
                return;
            }

            List<RouteInfo> singleList = new List<RouteInfo>();
            //绘制路线
            LineRouteList(singleList, first_item.Routecode);
            DrawLines(singleList);                

            //绘制仓位点
            rackList = x_rkService.GetListByHouse(houseId);
            foreach (var item in rackList)
            {
                int orgx = 0;
                int orgy = 0;
                var route_item = routeList.Where(a => a.Routecode == item.RouteCode).FirstOrDefault();
                if (route_item != null)
                {
                    orgx = route_item.X * pSize + xPoint;
                    orgy = 0 - route_item.Y * pSize + yPoint;
                }
                double x = item.X * pSize + xPoint;
                double y = (0 - item.Y * pSize + yPoint);

                Path line_path = new Path() { Stroke = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#FF8C00"), Opacity = 0.4 } };
                x_path_grid.Children.Add(line_path);
                PathGeometry pg = new PathGeometry();
                var xlist = new List<PathSegment>();
                xlist.Add(new LineSegment(new Point(x, y), true));
                PathFigure pf = new PathFigure(new Point(orgx, orgy), xlist, false);
                pg.Figures.Add(pf);
                line_path.Data = pg;

                Path xPath = new Path() { Fill = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#00cc00") }, Tag = item.Rackcode };
                xPath.Data = new RectangleGeometry() { Rect = new Rect(x - 10, y - 10, 20, 20) };
                x_path_grid.Children.Add(xPath);
                Label lb = new Label()
                {
                    Content = item.Rackname,
                    Margin = new Thickness(x - 15, y + 4, 0, 0)
                };
                x_path_grid.Children.Add(lb);

            }
            //绘制路径点
            foreach (var item in routeList)
            {
                double x = item.X * pSize + xPoint;
                double y = (0 - item.Y * pSize + yPoint);
                Path xPath = new Path() { Fill = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#666") } };
                xPath.Data = new EllipseGeometry() { Center = new Point(x, y), RadiusX = 12, RadiusY = 12 };
                x_path_grid.Children.Add(xPath);

                Label lb = new Label()
                {
                    Content = item.Routecode,
                    Margin = new Thickness(x - 12, y - 13, 0, 0),
                    Foreground = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#ffffff") }
                };
                x_path_grid.Children.Add(lb);
            }

            //绘制叉车
            truckList = x_tkService.Get(a => a.HouseId == houseId && a.Isvalid == 1);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("Resource/Image/truck.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            bi.Freeze();
            foreach (var truckItem in truckList)
            {
                var routeItem = x_rService.Get(a => a.Routecode == truckItem.TruckrouteCode && a.HouseId == houseId).FirstOrDefault();
                if (routeItem != null)
                {
                    double x = routeItem.X * pSize + xPoint;
                    double y = (0 - routeItem.Y * pSize + yPoint);
                    Image image = new Image() { Width = 24, Height = 24 };
                    image.Source = bi;
                    image.Margin = new Thickness(x + 5, y + 5, 0, 0);
                    x_path_grid.Children.Add(image);
                }
            }
        }

        private void DrawLines(List<RouteInfo> rList)
        {

            Path line_path = new Path() { Stroke = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#999") } };
            x_path_grid.Children.Add(line_path);

            PathGeometry pg = new PathGeometry();
            var xlist = new List<PathSegment>();
            int i = 0;
            double orgx = xPoint;
            double orgy = yPoint;
            if (rList.Count > 0)
            {
                orgx = rList[0].X * pSize + xPoint;
                orgy = (0 - rList[0].Y * pSize + yPoint);
            }
            foreach (var item in rList)
            {
                if (!string.IsNullOrEmpty(item.Leftroutecode) && i > 0 )
                {
                    if (!isLinked(item.Routecode, item.Leftroutecode))
                    {
                        List<RouteInfo> singleList = new List<RouteInfo>();
                        singleList.Add(item);
                        LineRouteList(singleList, item.Leftroutecode);
                        DrawLines(singleList);
                    }
                }

                if (!string.IsNullOrEmpty(item.Rightroutecode) && i > 0)
                {
                    if (!isLinked(item.Routecode, item.Rightroutecode))
                    {
                        List<RouteInfo> singleList = new List<RouteInfo>();
                        singleList.Add(item);
                        LineRouteList(singleList, item.Rightroutecode);
                        DrawLines(singleList);
                    }
                }
                
                double x = item.X * pSize + xPoint;
                double y = (0 - item.Y * pSize + yPoint);
                xlist.Add(new LineSegment(new Point(x, y), true));
                i++;
            }
            PathFigure pf = new PathFigure(new Point(orgx, orgy), xlist, false);
            pg.Figures.Add(pf);

            line_path.Data = pg;    
        }

        /// <summary>
        /// 递归路径信息
        /// </summary>
        /// <param name="xList"></param>
        /// <param name="startCode"></param>
        private void LineRouteList(List<RouteInfo> xList, string startCode)
        {
            var x_item = xList.FirstOrDefault();
            if (x_item != null)
            {
                if (!pathDict.ContainsKey(x_item.Routecode))
                {
                    List<string> li = new List<string>();
                    li.Add(startCode);
                    pathDict.Add(x_item.Routecode, li);
                }
                else
                {
                    List<string> li = pathDict[x_item.Routecode];
                    if (!li.Contains(startCode))
                    {
                        li.Add(startCode);
                    }
                }
            }

            var item = routeList.Where(a => a.Routecode == startCode).FirstOrDefault();
            
            if (item != null )                
            {
                xList.Add(item);
                if (!string.IsNullOrEmpty(item.Nextroutecode))
                {
                    var next = routeList.Where(a => a.Routecode == item.Nextroutecode).FirstOrDefault();
                    if (next != null)
                    {
                        if (!pathDict.ContainsKey(item.Routecode))
                        {
                            List<string> li = new List<string>();
                            li.Add(item.Nextroutecode);
                            pathDict.Add(item.Routecode, li);
                        }
                        else
                        {
                            List<string> li = pathDict[item.Routecode];
                            if (!li.Contains(item.Nextroutecode))
                            {
                                li.Add(item.Nextroutecode);
                            }
                        }
                        LineRouteList(xList, next.Routecode);
                    }
                }
            }
        }

        double lastX = double.MinValue;
        double lastY = double.MinValue;
        double lastXT = double.MinValue;
        double lastYT = double.MinValue;
        public void ClearTruckLine()
        {
            foreach (var item in x_path_grid.Children)
            {
                if (item is Path)
                {
                    Path pt = item as Path;
                    if (pt.Tag != null && pt.Tag.ToString() == "truckline")
                    {
                        pt.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
            lastX = double.MinValue;
            lastY = double.MinValue;
        }
        /// <summary>
        /// 显示叉车运行路径
        /// </summary>
        /// <param name="routeCode"></param>
        public void ShowTruckLine(string routeCode)
        {            
            Path line_path = new Path() { Stroke = new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#ff3300") }, Tag = "truckline" };
            x_path_grid.Children.Add(line_path);
            var routeItem = x_rService.Get(a => a.Routecode == routeCode && a.HouseId == houseId).FirstOrDefault();
            if (routeItem != null)
            {
                if (lastX == double.MinValue || lastY == double.MinValue)
                {
                    lastX = routeItem.X * pSize + xPoint;
                    lastY = 0 - routeItem.Y * pSize + yPoint;
                }
                PathGeometry pg = new PathGeometry();
                var xlist = new List<PathSegment>();
                double orgx = lastX;
                double orgy = lastY;

                double x = routeItem.X * pSize + xPoint;
                double y = (0 - routeItem.Y * pSize + yPoint);
                xlist.Add(new LineSegment(new Point(x, y), true));
                lastX = x;
                lastY = y;
                PathFigure pf = new PathFigure(new Point(orgx, orgy), xlist, false);
                pg.Figures.Add(pf);
                line_path.Data = pg;
            }
        }
        /// <summary>
        /// 显示叉车位置
        /// </summary>
        /// <param name="routeCode"></param>
        public void ShowTruckPosition(string routeCode)
        {
            var routeItem = x_rService.Get(a => a.Routecode == routeCode && a.HouseId == houseId).FirstOrDefault();
            if (routeItem != null)
            {
                if (lastXT == double.MinValue || lastYT == double.MinValue)
                {
                    lastXT = routeItem.X * pSize + xPoint;
                    lastYT = 0 - routeItem.Y * pSize + yPoint;
                }
                PathGeometry pg = new PathGeometry();
                var xlist = new List<PathSegment>();
                double orgx = lastXT;
                double orgy = lastYT;

                double x = routeItem.X * pSize + xPoint;
                double y = (0 - routeItem.Y * pSize + yPoint);
                xlist.Add(new LineSegment(new Point(x, y), true));
                lastXT = x;
                lastYT = y;
                foreach (var item in x_path_grid.Children)
                {
                    if (item is Image)
                    {
                        UIElement ue = item as Image;
                        ue.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("Resource/Image/truck.png", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                bi.Freeze();
                Image image = new Image() { Width = 26, Height = 26 };
                image.Source = bi;
                image.Margin = new Thickness(x - 13, y - 13, 0, 0);
                x_path_grid.Children.Add(image);
            }
        }

        int psize_temp;
        int xpoint_temp;
        int ypoint_temp;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IniHelper ini = new IniHelper();
            string p_size = ini.Read("psize");
            string p_left = ini.Read("pleft");
            string p_up = ini.Read("pup");
            if (!string.IsNullOrEmpty(p_size) && int.TryParse(p_size, out pSize)) { }
            if (!string.IsNullOrEmpty(p_size) && int.TryParse(p_left, out xPoint)) { }
            if (!string.IsNullOrEmpty(p_size) && int.TryParse(p_up, out yPoint)) { }
            initData();
            psize_temp = pSize;
            xpoint_temp = xPoint;
            ypoint_temp = yPoint;
            sl_psize.AddHandler(Slider.MouseLeftButtonUpEvent, new MouseButtonEventHandler(sl_psize_MouseLeftButtonUp), true);
            sl_left.AddHandler(Slider.MouseLeftButtonUpEvent, new MouseButtonEventHandler(sl_left_MouseLeftButtonUp), true);
            sl_up.AddHandler(Slider.MouseLeftButtonUpEvent, new MouseButtonEventHandler(sl_up_MouseLeftButtonUp), true);
        }
        
        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            string p = tb_size.Text;
            if (!int.TryParse(p, out pSize))
            {
                MsgBox.Show("请输入正整数倍数");
                tb_size.Focus();
                return;
            }
            string xp = tb_x.Text;
            if( !int.TryParse(xp, out xPoint))
            {
                MsgBox.Show("请输入正整数左偏移量");
                tb_x.Focus();
                return;
            }
            string yp = tb_y.Text;
            if (!int.TryParse(yp, out yPoint))
            {
                MsgBox.Show("请输入正整数下偏移量");
                tb_y.Focus();
                return;
            }
            string hid = cb_house.Text;
            if (!int.TryParse(hid, out houseId))
            {

            }
            initData();
        }

        /// <summary>
        /// 是否存在路径
        /// </summary>
        /// <param name="route1"></param>
        /// <param name="route2"></param>
        /// <returns></returns>
        private bool isLinked(string route1, string route2)
        {
            foreach (var item in pathDict)
            {
                var li = item.Value;
                if (item.Key == route1 && li.Contains(route2))
                {
                        return true;
                }
                else if (item.Key == route2 && li.Contains(route1))
                {
                        return true;
                }
            }
            return false;
        }
        double oldsize = 5;
        private void sl_psize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double newsize = sl_psize.Value;
            double nvl = newsize - oldsize;
            oldsize = newsize;
            pSize += Convert.ToInt32(nvl * psize_temp);   
            initData();
        }
        double oldleft = 5;
        public void sl_left_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double newleft = sl_left.Value;
            double nvl = newleft - oldleft;
            oldleft = newleft;
            xPoint += Convert.ToInt32(nvl * xpoint_temp);            
            initData();
        }
        double oldup = 5;
        public void sl_up_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double newup = sl_up.Value;
            double nvl = newup - oldup;
            oldup = newup;
            yPoint += Convert.ToInt32(nvl * ypoint_temp);    
            initData();
        }


        RackInfo selectRack;
        private void x_path_grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (true)
            {
                var p = e.GetPosition(x_path_grid);
                foreach (var itemm in x_path_grid.Children)
                {
                    if (itemm is Path)
                    {
                        Path pitem = itemm as Path;
                        if (pitem.Tag != null)
                        {
                            if (pitem.Data is RectangleGeometry)
                            {
                                RectangleGeometry rect = pitem.Data as RectangleGeometry;
                                if (rect.Bounds.Contains(p))
                                {
                                    string rackcode = pitem.Tag.ToString();
                                    selectRack = rackList.Where(a => a.Rackcode == rackcode).FirstOrDefault();
                                    SetSelectLayer("1");
                                    //RouteShowSelect rss = new RouteShowSelect()
                                    //{
                                    //    selectRack = selectRack,
                                    //    pRouteShow = this
                                    //};
                                    //rss.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置选中的仓位
        /// </summary>
        /// <param name="num"></param>
        public void SetSelectLayer(string num)
        {
            if (dpIndex != null)
            {
                dpIndex.SetSelectRack(selectRack.Rackname, selectRack.Rackcode, num);
                this.Close();
            }
        }
    }
}
