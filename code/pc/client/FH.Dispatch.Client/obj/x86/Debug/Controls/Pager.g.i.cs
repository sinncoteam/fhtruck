﻿#pragma checksum "..\..\..\..\Controls\Pager.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DE05AEC71AB241237547AAFDB88BF486"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace FH.Dispatch.Client.Controls {
    
    
    /// <summary>
    /// Pager
    /// </summary>
    public partial class Pager : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FH.Dispatch.Client.Controls.Pager userControl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFirst;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrev;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLast;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPageIndex;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Controls\Pager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGoTo;
        
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
            System.Uri resourceLocater = new System.Uri("/FH.Dispatch.Client;component/controls/pager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\Pager.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.userControl = ((FH.Dispatch.Client.Controls.Pager)(target));
            return;
            case 2:
            this.btnFirst = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\Controls\Pager.xaml"
            this.btnFirst.Click += new System.Windows.RoutedEventHandler(this.btnFirst_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnPrev = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Controls\Pager.xaml"
            this.btnPrev.Click += new System.Windows.RoutedEventHandler(this.btnPrev_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Controls\Pager.xaml"
            this.btnNext.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnLast = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Controls\Pager.xaml"
            this.btnLast.Click += new System.Windows.RoutedEventHandler(this.btnLast_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtPageIndex = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnGoTo = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\Controls\Pager.xaml"
            this.btnGoTo.Click += new System.Windows.RoutedEventHandler(this.btnGoTo_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

