using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ViCore.Logging;
using FH.Dispatch.Client.Controls;

namespace FH.Dispatch.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    Logging4net.WriteError(exception, "系统异常");
                }
            }
            catch (Exception ex)
            {
                Logging4net.WriteError(ex, "系统异常");
                MsgBox.Show("系统出现错误，即将退出；"+ ex.Message);
            }
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MsgBox.Show("系统出错，" + e.Exception.Message);
            e.Handled = true;
        }
    }
}
