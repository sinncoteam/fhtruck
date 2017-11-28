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
using System.Data;
using FH.Dispatch.Domain.Info;
using ViData;

namespace FH.Dispatch.Client.Manage
{
    /// <summary>
    /// RouteIndex.xaml 的交互逻辑
    /// </summary>
    public partial class RouteIndex : Window
    {
        public RouteIndex()
        {
            InitializeComponent();
            
        }
        public MainWindow main { get; set; }
        public bool OnlyClose { get; set; }
        RouteService x_rService = new RouteService();
        int houseId = MainWindow.HouseId;
        IList<RouteInfo> routeList;
        public void InitData(int page = 1)
        {
            dataPager1.PageIndex = page;
            
            PagingInfo pi = new PagingInfo()
            {
                PageIndex = page,
                 PageSize = dataPager1.PageSize,
                  Conditions = " r.house_id = "+ houseId
            };
            routeList = x_rService.GetByPage(pi);
            dataPager1.TotalCount = pi.RecordCount;
            //dataPager1.PageCount = pi.PageCount;
            this.dataGrid1.ItemsSource = routeList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
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

        private void btn_newroute_Click(object sender, RoutedEventArgs e)
        {
            RouteEdit edit = new RouteEdit(this);
            edit.ShowDialog();
        }

        private void dataPager1_PageChanged(object sender, Controls.PageChangedEventArgs e)
        {
            InitData(e.CurrentPageIndex);
        }

        private void dataPager1_PageChanging(object sender, Controls.PageChangingEventArgs e)
        {

        }

        private void DataGridHyperlinkColumn_Click(object sender, RoutedEventArgs e)
        {
             
        }

        //RouteInfo RouteEdit;
        //private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //    RouteEdit = e.Row.Item as RouteInfo;
        //    MessageBox.Show(RouteEdit.Routecode + " : " + RouteEdit.IsValid.ToString());
        //}

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            InitData(dataPager1.PageIndex);
        }

        string oldValue;
        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox)
            {
                string newValue = (e.EditingElement as TextBox).Text;
                if (oldValue != newValue)
                {
                    RouteInfo ri = e.Row.Item as RouteInfo;
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

        private void chk_all_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                RouteInfo ri = item as RouteInfo;
                ri.IsChecked = true;
            }
        }

        private void chk_all_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in dataGrid1.Items)
            {
                RouteInfo ri = item as RouteInfo;
                ri.IsChecked = false;
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            List<int> idList = new List<int>();
            foreach (RouteInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    idList.Add(item.Id);
                }
            }
            if (idList.Count > 0)
            {
                int i = x_rService.UpdateToValid(idList, 0);
                InitData(dataPager1.PageIndex);
                chk_all.IsChecked = false;
            }
        }

        private void btn_isvalid_Click(object sender, RoutedEventArgs e)
        {
            List<int> idList = new List<int>();
            foreach (RouteInfo item in dataGrid1.Items)
            {
                if (item.IsChecked)
                {
                    idList.Add(item.Id);
                }
            }
            if (idList.Count > 0)
            {
                int i = x_rService.UpdateToValid(idList, 1);
                InitData(dataPager1.PageIndex);
                chk_all.IsChecked = false;
            }
        }
        private void btn_showpath_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowRouteShow();
        }

        private void c_id_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int id = Convert.ToInt32(cb.Tag);
            var item = routeList.Where(a => a.Id == id).FirstOrDefault();
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
