﻿#pragma checksum "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34947091977A03E9870A574C77D6C9A5A248FFEC"
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
using application.InteractWindow.ForOrders;


namespace application.InteractWindow.ForOrders {
    
    
    /// <summary>
    /// ChangeStatusOrderCar
    /// </summary>
    public partial class ChangeStatusOrderCar : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox managerBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ordersBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock secondnameCustomer_text;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock car_text;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox status_text;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
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
            System.Uri resourceLocater = new System.Uri("/application;component/interactwindow/fororders/changestatusordercar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
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
            this.managerBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 12 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
            this.managerBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.getIdManager_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ordersBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
            this.ordersBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.getIdOrder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.secondnameCustomer_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.car_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.status_text = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\..\InteractWindow\ForOrders\ChangeStatusOrderCar.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

