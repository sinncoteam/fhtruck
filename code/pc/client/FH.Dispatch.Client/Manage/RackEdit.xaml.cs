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

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RackEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RackEdit : Window
    {
        public RackEdit()
        {
            InitializeComponent();
        }
        public RackIndex win_rkIndex;
        public int rackId;
        RackService x_rkService = new RackService();
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string trcode = tb_code.Text;
            string trname = tb_name.Text;
            string trcount = cb_layercount.Text;
            string trroute = cb_routelist.Text;
            if (rackId == 0)
            {
                RackInfo trInfo = new RackInfo()
                {
                    Createtime = DateTime.Now,
                    Isvalid = 1,
                    Updatetime = DateTime.Now
                };
                trInfo.Rackcode = trcode;
                trInfo.Rackname = trname;
                trInfo.RacklayerCount = Convert.ToInt32(trcount);
                trInfo.RouteCode = trroute;
                trInfo.HouseId = MainWindow.HouseId;
                x_rkService.Insert(trInfo);
            }
            else
            {
                x_rkService.Update(() => new RackInfo() { Rackcode = trcode, Rackname = trname, RacklayerCount = Convert.ToInt32(trcount), RouteCode = trroute }, a => a.Id == rackId);
            }
            tb_code.Text = "";
            tb_name.Text = "";
            MsgBox.Show("保存成功");
            if (win_rkIndex != null && win_rkIndex.IsVisible)
            {
                win_rkIndex.InitData();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RouteService x_rService = new RouteService();
            var list = x_rService.Get(a => a.IsValid == 1);
            cb_routelist.ItemsSource = list;
            cb_routelist.DisplayMemberPath = "Routecode";

            var info = x_rkService.GetById(rackId);
            if (info != null)
            {
                tb_code.Text = info.Rackcode;
                tb_name.Text = info.Rackname;
                foreach (ComboBoxItem item in cb_routelist.Items)
                {
                    if (item.Content.ToString() == info.RouteCode)
                    {
                        item.IsSelected = true;
                        break;
                    }
                }
                foreach (ComboBoxItem itemm in cb_layercount.Items)
                {
                    if (itemm.Content.ToString() == info.RacklayerCount.ToString())
                    {
                        itemm.IsSelected = true;
                        break;
                    }
                }
            }
            else
            {
                cb_routelist.SelectedIndex = 0;
                cb_routelist.SelectedIndex = 2;
            }
        }
    }
}
