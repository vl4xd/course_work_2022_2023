﻿#pragma checksum "..\..\..\Views\AllergyWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6E91A4E5444A7049E746DA0B473AD727C38879F61DF13CE7EC9DC75A90224ABF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using medical_family_card.Views;


namespace medical_family_card.Views {
    
    
    /// <summary>
    /// AllergyWindow
    /// </summary>
    public partial class AllergyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menuChangeItems;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemSave;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemCancel;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemClose;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxAllergyName;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\Views\AllergyWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxComment;
        
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
            System.Uri resourceLocater = new System.Uri("/medical_family_card;component/views/allergywindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AllergyWindow.xaml"
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
            
            #line 19 "..\..\..\Views\AllergyWindow.xaml"
            ((medical_family_card.Views.AllergyWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.menuChangeItems = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            this.menuItemSave = ((System.Windows.Controls.MenuItem)(target));
            
            #line 51 "..\..\..\Views\AllergyWindow.xaml"
            this.menuItemSave.Click += new System.Windows.RoutedEventHandler(this.menuItemSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuItemCancel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 56 "..\..\..\Views\AllergyWindow.xaml"
            this.menuItemCancel.Click += new System.Windows.RoutedEventHandler(this.menuItemCancel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.menuItemClose = ((System.Windows.Controls.MenuItem)(target));
            
            #line 60 "..\..\..\Views\AllergyWindow.xaml"
            this.menuItemClose.Click += new System.Windows.RoutedEventHandler(this.menuItemClose_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.textBoxAllergyName = ((System.Windows.Controls.TextBox)(target));
            
            #line 107 "..\..\..\Views\AllergyWindow.xaml"
            this.textBoxAllergyName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBoxAllergyName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBoxComment = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

