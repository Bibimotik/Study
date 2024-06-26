﻿#pragma checksum "..\..\..\..\UsersWindow\UserWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A8F4D764D360E512530A1D7C6B6031D698D50F02"
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
using application;


namespace application {
    
    
    /// <summary>
    /// UserWindow
    /// </summary>
    public partial class UserWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
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
            System.Uri resourceLocater = new System.Uri("/application;component/userswindow/userwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UsersWindow\UserWindow.xaml"
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
            
            #line 16 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_ShowCarsSpareParts);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 17 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_SearchCarsSpareParts);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 18 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_ShowAllReviews);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 19 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_CreateOrderCar);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 20 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_CreateOrderSparePart);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 21 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_CreateServiceSheet);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 22 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_CreateReview);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 23 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_ShowDataStatusOrders);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 24 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_ShowHistory);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 25 "..\..\..\..\UsersWindow\UserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.open_ShowServiceSheet);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

