﻿#pragma checksum "..\..\..\..\Manage\RackIndex.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E81BBB81DC1D4752FAE666FE05A365B8"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FH.Dispatch.Client.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FH.Dispatch.Client.Manage {
    
    
    /// <summary>
    /// RackIndex
    /// </summary>
    public partial class RackIndex : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 6 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FH.Dispatch.Client.Controls.TopMenu my_topmenu;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chk_all;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_isvalid;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_del;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_newtruck;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid1;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Manage\RackIndex.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FH.Dispatch.Client.Controls.Pager dataPager1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FH.Dispatch.Client;component/manage/rackindex.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Manage\RackIndex.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\..\Manage\RackIndex.xaml"
            ((FH.Dispatch.Client.Manage.RackIndex)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 4 "..\..\..\..\Manage\RackIndex.xaml"
            ((FH.Dispatch.Client.Manage.RackIndex)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.my_topmenu = ((FH.Dispatch.Client.Controls.TopMenu)(target));
            return;
            case 3:
            this.chk_all = ((System.Windows.Controls.CheckBox)(target));
            
            #line 7 "..\..\..\..\Manage\RackIndex.xaml"
            this.chk_all.Unchecked += new System.Windows.RoutedEventHandler(this.chk_all_Unchecked);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\..\Manage\RackIndex.xaml"
            this.chk_all.Checked += new System.Windows.RoutedEventHandler(this.chk_all_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_isvalid = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\..\Manage\RackIndex.xaml"
            this.btn_isvalid.Click += new System.Windows.RoutedEventHandler(this.btn_isvalid_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_del = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\..\Manage\RackIndex.xaml"
            this.btn_del.Click += new System.Windows.RoutedEventHandler(this.btn_del_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_newtruck = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\..\Manage\RackIndex.xaml"
            this.btn_newtruck.Click += new System.Windows.RoutedEventHandler(this.btn_newtruck_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dataGrid1 = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\..\..\Manage\RackIndex.xaml"
            this.dataGrid1.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.dataGrid1_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\Manage\RackIndex.xaml"
            this.dataGrid1.BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.dataGrid1_BeginningEdit);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dataPager1 = ((FH.Dispatch.Client.Controls.Pager)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 16 "..\..\..\..\Manage\RackIndex.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Click += new System.Windows.RoutedEventHandler(this.c_id_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 34 "..\..\..\..\Manage\RackIndex.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
