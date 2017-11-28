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
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Domain.Service;
using FH.Common.Utils;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RouteEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RouteEdit : Window
    {
        public RouteInfo MyRoute { get; set; }
        public RouteIndex IndexView { get; set; }
        public RouteEdit(RouteIndex index)
        {
            InitializeComponent();
            InitData();
            IndexView = index;
        }
        int houseId = MainWindow.HouseId;
        void InitData()
        {
            if (MyRoute != null)
            {
                tb_routecode.Text = MyRoute.Routecode;
                tb_next.Text = MyRoute.Nextroutecode;
                tb_next_value.Text = MyRoute.Nextroutevalue.ToString();
                tb_left.Text = MyRoute.Leftroutecode;
                tb_left_value.Text = MyRoute.Leftroutevalue.ToString();
                tb_right.Text = MyRoute.Rightroutecode;
                tb_right_value.Text = MyRoute.Rightroutevalue.ToString();
                tb_back.Text = MyRoute.Backroutecode;
                tb_back_value.Text = MyRoute.Backroutevalue.ToString();
            }
        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            int temp = 0;
            if (!string.IsNullOrEmpty(tb_next.Text) && !int.TryParse(tb_next_value.Text, out temp))
            {
                MsgBox.Show("前节点权值必须为整数");
                tb_next_value.Focus();
                return;
            }
            if(!string.IsNullOrEmpty(tb_left.Text) && !int.TryParse(tb_left_value.Text, out temp))
            {
                MsgBox.Show("左节点权值必须为整数");
                tb_left_value.Focus();
                return;
            }
            if( !string.IsNullOrEmpty(tb_right.Text) && !int.TryParse(tb_right_value.Text, out temp))
            {
                MsgBox.Show("右节点权值必须为整数");
                tb_right_value.Focus();
                return;
            }
            if( !string.IsNullOrEmpty(tb_back.Text) && !int.TryParse(tb_back_value.Text, out temp))
            {
                MsgBox.Show("后节点权值必须为整数");
                tb_back_value.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(tb_x.Text) && !int.TryParse(tb_x.Text, out temp))
            {
                MsgBox.Show("坐标必须为整数");
                tb_x.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(tb_y.Text) && !int.TryParse(tb_y.Text, out temp))
            {
                MsgBox.Show("坐标必须为整数");
                tb_y.Focus();
                return;
            }

            RouteInfo myInfo = new RouteInfo();
            myInfo.Routecode = tb_routecode.Text;
            myInfo.Isstart = 0;
            int way = 0;            
            myInfo.Nextroutecode = tb_next.Text;
            myInfo.Nextroutevalue = TypeHelper.StrToInt(tb_next_value.Text);
            if (!string.IsNullOrEmpty(myInfo.Nextroutecode))
            {
                way++;
            }
            myInfo.Leftroutecode = tb_left.Text;

            myInfo.Leftroutevalue = TypeHelper.StrToInt(tb_left_value.Text);
            if (!string.IsNullOrEmpty(myInfo.Leftroutecode))
            {
                way++;
            }
            myInfo.Rightroutecode = tb_right.Text;
            myInfo.Rightroutevalue = TypeHelper.StrToInt(tb_right_value.Text);
            if (!string.IsNullOrEmpty(myInfo.Rightroutecode))
            {
                way++;
            }
            myInfo.Backroutecode = tb_back.Text;
            myInfo.Backroutevalue = TypeHelper.StrToInt(tb_back_value.Text);
            if (!string.IsNullOrEmpty(myInfo.Backroutecode))
            {
                way++;
            }
            

            myInfo.Routewaycount = way;
            myInfo.Createtime = DateTime.Now;
            myInfo.Updatetime = DateTime.Now;
            myInfo.IsValid = 1;
            myInfo.X = TypeHelper.StrToInt(tb_x.Text);
            myInfo.Y = TypeHelper.StrToInt(tb_y.Text);
            myInfo.HouseId = houseId;
            RouteService x_rService = new RouteService();
            if (MyRoute != null)
            {

            }
            else
            {
                x_rService.Insert(myInfo);
            }
            MyRoute = null;
            IndexView.InitData();
            this.Close();
        }
    }
}
