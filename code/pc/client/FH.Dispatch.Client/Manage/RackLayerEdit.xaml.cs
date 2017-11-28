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
    /// RackLayerEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RackLayerEdit : Window
    {
        public RackLayerEdit()
        {
            InitializeComponent();
        }

        public RackLayerIndex win_rklIndex;
        public int layerId;
        public int rackId;
        RackLayerService x_rkService = new RackLayerService();
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string trcode = tb_code.Text;
            string rackid = cb_rack.SelectedValue.ToString();
            string trcount = cb_layercount.Text;
            if (layerId == 0)
            {
                RackLayerInfo trInfo = new RackLayerInfo()
                {
                    Createtime = DateTime.Now,
                    IsValid = 1,
                    Updatetime = DateTime.Now
                };
                trInfo.RackLayerCode = trcode;
                trInfo.RackLayerNum = Convert.ToInt32(trcount);
                trInfo.RackId = Convert.ToInt32(rackid);
                x_rkService.Insert(trInfo);
            }
            else
            {
                x_rkService.Update(() => new RackLayerInfo() { RackLayerCode = trcode, RackLayerNum = Convert.ToInt32(trcount), RackId = Convert.ToInt32(rackid) }, a => a.ID == layerId);
            }
            tb_code.Text = "";
            MsgBox.Show("保存成功");
            if (win_rklIndex != null && win_rklIndex.IsVisible)
            {
                win_rklIndex.InitData();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RackService x_rService = new RackService();
            var list = x_rService.Get(a => a.Id == rackId && a.HouseId == MainWindow.HouseId);
            cb_rack.ItemsSource = list;
            cb_rack.DisplayMemberPath = "Rackcode";
            cb_rack.SelectedValuePath = "Id";

            if (layerId > 0)
            {
                var info = x_rkService.GetById(layerId);
                if (info != null)
                {
                    tb_code.Text = info.RackLayerCode;

                    foreach (ComboBoxItem item in cb_rack.Items)
                    {
                        if (item.Tag.ToString() == info.RackId.ToString())
                        {
                            item.IsSelected = true;
                            break;
                        }
                    }
                    foreach (ComboBoxItem itemm in cb_layercount.Items)
                    {
                        if (itemm.Content.ToString() == info.RackLayerNum.ToString())
                        {
                            itemm.IsSelected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                cb_rack.SelectedIndex = 0;
                cb_layercount.SelectedIndex = 0;
            }
        }
    }
}
