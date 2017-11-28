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
using ViData;

namespace FH.Dispatch.Client.Dispatch
{
    /// <summary>
    /// TruckRouteList.xaml 的交互逻辑
    /// </summary>
    public partial class TruckRouteList : Window
    {
        public TruckRouteList()
        {
            InitializeComponent();
        }
        string truckCode;
        TruckLogService x_tlService = new TruckLogService();
        IList<TruckLogInfo> logList;
        void InitData(int page = 1)
        {
            PagingInfo pi = new PagingInfo()
            {
                PageSize = dataPager1.PageSize,
                 PageIndex = page
            };
            if (!string.IsNullOrEmpty(truckCode))
            {
                pi.Conditions = " tl.truckcode = @code";
                pi.Parameters.Add("code", truckCode);
            }
            logList = x_tlService.GetByPage(pi);
            dataPager1.TotalCount = pi.RecordCount;
            this.dataGrid1.ItemsSource = logList;
        }

        private void dataPager1_PageChanged(object sender, Controls.PageChangedEventArgs e)
        {
            InitData(e.CurrentPageIndex);
        }

        private void dataPager1_PageChanging(object sender, Controls.PageChangingEventArgs e)
        {

        }

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            truckCode = tb_truck.Text;
            InitData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}
