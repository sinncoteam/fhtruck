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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FH.Dispatch.Client.Controls
{
    /// <summary>
    /// FootBar.xaml 的交互逻辑
    /// </summary>
    public partial class FootBar : UserControl
    {
        public FootBar()
        {
            InitializeComponent();
        }

        public void ShowText(string text)
        {
            x_sbitem.Content = text;
        }
    }
}
