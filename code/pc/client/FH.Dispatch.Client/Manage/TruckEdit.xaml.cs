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
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// TruckEdit.xaml 的交互逻辑
    /// </summary>
    public partial class TruckEdit : Window
    {
        public TruckEdit()
        {
            InitializeComponent();
        }
        public TruckIndex win_trIndex;
        public int truckId;
        TruckService x_tService = new TruckService();
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string trcode = tb_code.Text;
            string trname = tb_name.Text;
            if (truckId == 0)
            {
                TruckInfo trInfo = new TruckInfo()
                {
                    Createtime = DateTime.Now,
                    Isvalid = 1,
                    Updatetime = DateTime.Now
                };
                trInfo.Truckcode = trcode;
                trInfo.Truckname = trname;                
                x_tService.Insert(trInfo);                
            }
            else
            {
                x_tService.Update(() => new TruckInfo() { Truckcode = trcode, Truckname = trname }, a => a.Id == truckId);
            }
            tb_code.Text = "";
            tb_name.Text = "";
            MsgBox.Show("保存成功");
            if (win_trIndex != null && win_trIndex.IsVisible)
            {
                win_trIndex.InitData();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var info = x_tService.GetById(truckId);
            if (info != null)
            {
                tb_code.Text = info.Truckcode;
                tb_name.Text = info.Truckname;
            }
        }
    }
}
