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
using ViData;
using FH.Dispatch.Domain.Info;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RackLayerIndex.xaml 的交互逻辑
    /// </summary>
    public partial class RackLayerIndex : Window
    {
        public RackLayerIndex()
        {
            InitializeComponent();
        }
        RackLayerService x_rlService = new RackLayerService();
        RackLayerEdit win_rackLayerEdit;
        public int RackId;
        IList<RackLayerInfo> layerList;
        public void InitData(int page = 1)
        {
            dataPager1.PageIndex = page;

            PagingInfo pi = new PagingInfo()
            {
                PageIndex = page,
                PageSize = dataPager1.PageSize
            };
            if (RackId > 0)
            {
                pi.Conditions = " r.ID = "+ RackId;
            }
            layerList = x_rlService.GetByPage(pi);
            dataPager1.TotalCount = pi.RecordCount;
            this.dataGrid1.ItemsSource = layerList;
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
                RackLayerInfo ri = item as RackLayerInfo;
                ri.IsChecked = true;
            }
        }

        private void chk_all_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                RackLayerInfo ri = item as RackLayerInfo;
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
            foreach (RackLayerInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    idList.Add(item.ID);
                }
            }
            if (idList.Count > 0)
            {
                int i = x_rlService.UpdateToValid(idList, isvalid);
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
                    RackLayerInfo ri = e.Row.Item as RackLayerInfo;
                    if (ri != null)
                    {
                        x_rlService.UpdateRow(e.Column.SortMemberPath, newValue, ri.ID);
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
            
        }

        private void btn_newtruck_Click(object sender, RoutedEventArgs e)
        {
            if (win_rackLayerEdit != null && win_rackLayerEdit.IsVisible)
            {
                win_rackLayerEdit.rackId = RackId;
                win_rackLayerEdit.ShowDialog();
            }
            else
            {

                win_rackLayerEdit = new RackLayerEdit() { win_rklIndex = this , rackId = RackId};
                win_rackLayerEdit.ShowDialog();
            }
        }

        private void c_id_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int id = Convert.ToInt32(cb.Tag);
            var item = layerList.Where(a => a.ID == id).FirstOrDefault();
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
