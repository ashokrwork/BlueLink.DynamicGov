﻿#pragma checksum "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C4D5662345008056BEC0BD4390D9AD3C736F4ED5AD3A6A5CBF09D20A9F21EC0A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using MoonPdfLib;
using OneHub360.WPF.Register.App.Controls.General;
using OneHub360.WPF.Register.App.Controls.Outgoing;
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


namespace OneHub360.WPF.Register.App.Controls.Outgoing {
    
    
    /// <summary>
    /// ViewOutgoingLetter
    /// </summary>
    public partial class ViewOutgoingLetter : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 13 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrint;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHandToMessanger;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPageNumber;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextPage;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MoonPdfLib.MoonPdfPanel moonPdfPanel;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblOutgoingNumber;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblOutgoingDate;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OneHub360.WPF.Register.App.Controls.General.ExternalUnitDisplay toExternalOrganization;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OneHub360.WPF.Register.App.Controls.General.UserDisplay fromUser;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMainDocumentView;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl listLetterAttachements;
        
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
            System.Uri resourceLocater = new System.Uri("/OneHub360.WPF.Register.App;component/controls/outgoing/viewoutgoingletter.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
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
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.btnPrint = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.btnHandToMessanger = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            this.btnHandToMessanger.Click += new System.Windows.RoutedEventHandler(this.btnHandToMessanger_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 26 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnBack_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnPreviousPage = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            this.btnPreviousPage.Click += new System.Windows.RoutedEventHandler(this.btnPreviousPage_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblPageNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnNextPage = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            this.btnNextPage.Click += new System.Windows.RoutedEventHandler(this.btnNextPage_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.moonPdfPanel = ((MoonPdfLib.MoonPdfPanel)(target));
            return;
            case 10:
            this.lblOutgoingNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.lblOutgoingDate = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.toExternalOrganization = ((OneHub360.WPF.Register.App.Controls.General.ExternalUnitDisplay)(target));
            return;
            case 13:
            this.fromUser = ((OneHub360.WPF.Register.App.Controls.General.UserDisplay)(target));
            return;
            case 14:
            this.btnMainDocumentView = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            this.btnMainDocumentView.Click += new System.Windows.RoutedEventHandler(this.btnMainDocumentView_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.listLetterAttachements = ((System.Windows.Controls.ItemsControl)(target));
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
            case 16:
            
            #line 83 "..\..\..\..\Controls\Outgoing\ViewOutgoingLetter.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
