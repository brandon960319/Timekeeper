﻿#pragma checksum "..\..\DatabaseConnect.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "167D52ABF49E3D577F04B97392259A10C95132BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BrandonVanLoggerenberg_TimeKeeper;
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


namespace BrandonVanLoggerenberg_TimeKeeper {
    
    
    /// <summary>
    /// DatabaseConnect
    /// </summary>
    public partial class DatabaseConnect : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridLogin;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_welcome;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_XML;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_SQL;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander expander_MoreLess;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Register;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\DatabaseConnect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_ForgotPassword;
        
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
            System.Uri resourceLocater = new System.Uri("/BrandonVanLoggerenberg_TimeKeeper;component/databaseconnect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DatabaseConnect.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GridLogin = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.label_welcome = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.button_XML = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\DatabaseConnect.xaml"
            this.button_XML.Click += new System.Windows.RoutedEventHandler(this.button_Login_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.button_SQL = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\DatabaseConnect.xaml"
            this.button_SQL.Click += new System.Windows.RoutedEventHandler(this.button_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.expander_MoreLess = ((System.Windows.Controls.Expander)(target));
            
            #line 29 "..\..\DatabaseConnect.xaml"
            this.expander_MoreLess.Expanded += new System.Windows.RoutedEventHandler(this.expander_Expanded);
            
            #line default
            #line hidden
            
            #line 29 "..\..\DatabaseConnect.xaml"
            this.expander_MoreLess.Collapsed += new System.Windows.RoutedEventHandler(this.expander_Collapsed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label_Register = ((System.Windows.Controls.Label)(target));
            
            #line 31 "..\..\DatabaseConnect.xaml"
            this.label_Register.MouseEnter += new System.Windows.Input.MouseEventHandler(this.label_Generic_MouseEnter);
            
            #line default
            #line hidden
            
            #line 31 "..\..\DatabaseConnect.xaml"
            this.label_Register.MouseLeave += new System.Windows.Input.MouseEventHandler(this.label_Generic_MouseLeave);
            
            #line default
            #line hidden
            
            #line 31 "..\..\DatabaseConnect.xaml"
            this.label_Register.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.label_Generic_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.label_ForgotPassword = ((System.Windows.Controls.Label)(target));
            
            #line 32 "..\..\DatabaseConnect.xaml"
            this.label_ForgotPassword.MouseEnter += new System.Windows.Input.MouseEventHandler(this.label_Generic_MouseEnter);
            
            #line default
            #line hidden
            
            #line 32 "..\..\DatabaseConnect.xaml"
            this.label_ForgotPassword.MouseLeave += new System.Windows.Input.MouseEventHandler(this.label_Generic_MouseLeave);
            
            #line default
            #line hidden
            
            #line 32 "..\..\DatabaseConnect.xaml"
            this.label_ForgotPassword.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.label_Generic_MouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

