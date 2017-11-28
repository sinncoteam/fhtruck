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
using FH.Dispatch.Domain.Info;
using FH.Dispatch.Domain.Service;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RackIndex.xaml 的交互逻辑
    /// </summary>
    public partial class RackIndex : Window
    {
        public RackIndex()
        {
            InitializeComponent();
        }

        RackService x_rService = new RackService();
        RackEdit win_rackEdit;
        RackLayerIndex rlIndex;
        public MainWindow main;
        IList<RackInfo> rackList;
        public void InitData(int page = 1)
        {
            dataPager1.PageIndex = page;

            PagingInfo pi = new PagingInfo()
            {
                PageIndex = page,
                PageSize = dataPager1.PageSize,
                Conditions = " r.house_id ="+ MainWindow.HouseId
            };
            rackList = x_rService.GetByPage(pi);
            dataPager1.TotalCount = pi.RecordCount;
            this.dataGrid1.ItemsSource = rackList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void dataPager1_PageChanged(object sender, Controls.PageChangedEventArgs e)
        {
            InitData(e.CurrentPageIndex);
        }

        private void chk_all_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                RackInfo ri = item as RackInfo;
                ri.IsChecked = true;
            }
        }

        private void chk_all_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                RackInfo ri = item as RackInfo;
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
            foreach (RackInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    idList.Add(item.Id);
                }
            }
            if (idList.Count > 0)
            {
                int i = x_rService.UpdateToValid(idList, isvalid);
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
                    RackInfo ri = e.Row.Item as RackInfo;
                    if (ri != null)
                    {
                        x_rService.UpdateRow(e.Column.SortMemberPath, newValue, ri.Id);
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

        private void btn_newtruck_Click(object sender, RoutedEventArgs e)
        {
            if (win_rackEdit != null && win_rackEdit.IsVisible)
            {
                win_rackEdit.ShowDialog();
            }
            else
            {
                win_rackEdit = new RackEdit() { win_rkIndex = this };
                win_rackEdit.ShowDialog();
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            int id =　Convert.ToInt32( btn.Tag);
            
            if (rlIndex != null && rlIndex.IsVisible)
            {
                rlIndex.Show();
                rlIndex.Activate();
                rlIndex.WindowState = System.Windows.WindowState.Normal;
                rlIndex.RackId = id;
                rlIndex.InitData();
            }
            else
            {
                rlIndex = new RackLayerIndex();
                rlIndex.RackId = id;
                rlIndex.Show();
            }
        }

        private void c_id_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int id = Convert.ToInt32(cb.Tag);
            var item = rackList.Where(a => a.Id == id).FirstOrDefault();
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
