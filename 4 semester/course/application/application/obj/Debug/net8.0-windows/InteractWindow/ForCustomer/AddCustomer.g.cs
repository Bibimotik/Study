﻿#pragma checksum "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C8E52C433893A266AC9E201E656753A556EEA176"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using application.InteractWindow.ForCustomer;


namespace application.InteractWindow.ForCustomer {
    
    
    /// <summary>
    /// AddCustomer
    /// </summary>
    public partial class AddCustomer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox secondName_text;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstName_text;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox thirdName_text;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mail_text;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phone_text;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox country_text;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox address_text;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox requisites_text;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/application;component/interactwindow/forcustomer/addcustomer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.secondName_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.firstName_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.thirdName_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.mail_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.phone_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.country_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.address_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.requisites_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 36 "..\..\..\..\..\InteractWindow\ForCustomer\AddCustomer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

