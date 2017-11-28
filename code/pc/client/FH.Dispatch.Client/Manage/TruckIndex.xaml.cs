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
using FH.Dispatch.Domain.Service;
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// TruckIndex.xaml 的交互逻辑
    /// </summary>
    public partial class TruckIndex : Window
    {
        public TruckIndex()
        {
            InitializeComponent();
        }

        TruckService x_tService = new TruckService();
        RouteService x_rtService = new RouteService();
        TruckEdit win_truckEdit;
        public MainWindow main;
        IList<TruckInfo> truckList;
        public void InitData(int page = 1)
        {
            dataPager1.PageIndex = page;

            PagingInfo pi = new PagingInfo()
            {
                PageIndex = page,
                PageSize = dataPager1.PageSize,
                Conditions = " r.house_id = "+ MainWindow.HouseId
            };
            truckList = x_tService.GetByPage(pi);
            dataPager1.TotalCount = pi.RecordCount;
            this.dataGrid1.ItemsSource = truckList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void dataPager1_PageChanged(object sender, Controls.PageChangedEventArgs e)
        {
            InitData(e.CurrentPageIndex);
        }

        private void btn_newtruck_Click(object sender, RoutedEventArgs e)
        {
            if (win_truckEdit != null && win_truckEdit.IsVisible)
            {
                win_truckEdit.ShowDialog();
            }
            else
            {
                win_truckEdit = new TruckEdit() { win_trIndex = this };
                win_truckEdit.ShowDialog();
            }
        }

        private void chk_all_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                TruckInfo ri = item as TruckInfo;
                ri.IsChecked = true;
            }
        }

        private void chk_all_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                TruckInfo ri = item as TruckInfo;
                ri.IsChecked = false;
            }
        }

        private void btn_isvalid_Click(object sender, RoutedEventArgs e)
        {
            setToValid(1);
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            setToValid(0);
        }

        void setToValid(int isvalid)
        {
            List<int> idList = new List<int>();
            foreach (TruckInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    idList.Add(item.Id);
                }
            }
            if (idList.Count > 0)
            {
                int i = x_tService.UpdateToValid(idList, isvalid);
                InitData(dataPager1.PageIndex);
                chk_all.IsChecked = false;
            }
        }

        string oldValue;
        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox)
            {
                string newValue = (e.EditingElement as TextBox).Text;
                if (oldValue != newValue)
                {
                    TruckInfo ri = e.Row.Item as TruckInfo;
                    if (ri != null)
                    {
                        x_tService.UpdateRow(e.Column.SortMemberPath, newValue, ri.Id);
                    }
                }
            }
        }
        private void dataGrid1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column is DataGridTextColumn)
            {
                oldValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
            }
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

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            var routeItem = x_rtService.Get(a => a.IsValid == 1 && a.Isstart == 2 && a.HouseId == MainWindow.HouseId).FirstOrDefault();
            string routecode = "";
            if (routeItem != null)
            {
                routecode = routeItem.Routecode;
            }
            foreach (TruckInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    i += x_tService.Update(() => new TruckInfo() { Truckstatus = 0, TruckrouteCode = routecode }, a => a.Id == item.Id);
                }
            }
            MsgBox.Show("成功复位了" + i + "个叉车");
            InitData(dataPager1.PageIndex);
        }

        private void c_id_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int id = Convert.ToInt32(cb.Tag);
            var item = truckList.Where(a => a.Id == id).FirstOrDefault();
            if (item != null)
            {
                if (cb.IsChecked.Value)
                {
                    item.IsChecked = true;
                }
                else
                {
                    item.IsChecked = false;
                }
            }
        }
    }
}
