﻿#pragma checksum "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CE7142AF27A677D70823C9FAFFC74856C19A4C3E"
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
using application.InteractWindow.ForSparePart;


namespace application.InteractWindow.ForSparePart {
    
    
    /// <summary>
    /// AddSparePart
    /// </summary>
    public partial class AddSparePart : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox label_text;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox labelModelBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description_text;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox quantity_text;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox price_text;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox status_text;
        
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
            System.Uri resourceLocater = new System.Uri("/application;component/interactwindow/forsparepart/addsparepart.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
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
            this.label_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.labelModelBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.description_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.quantity_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.price_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.status_text = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            
            #line 32 "..\..\..\..\..\InteractWindow\ForSparePart\AddSparePart.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.add_car);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
