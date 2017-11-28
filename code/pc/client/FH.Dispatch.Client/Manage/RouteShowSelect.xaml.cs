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

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RouteShowSelect.xaml 的交互逻辑
    /// </summary>
    public partial class RouteShowSelect : Window
    {
        public RouteShowSelect()
        {
            InitializeComponent();
        }

        public RouteShow pRouteShow;
        public RackInfo selectRack;        
        RackLayerService x_rkService = new RackLayerService();
        RackService x_rService = new RackService();
        IList<RackLayerInfo> rlList;
        void initData()
        {
            lb_layername.Content = selectRack.Rackname;

            rlList = x_rkService.Get(a => a.RackId == selectRack.Id && a.IsValid == 1);
            cb_racklayer.ItemsSource = rlList;
            cb_racklayer.DisplayMemberPath = "RackLayerNum";
            cb_racklayer.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initData();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string layer = cb_racklayer.Text;
            pRouteShow.SetSelectLayer(layer);
            this.Close();
        }
    }
}
