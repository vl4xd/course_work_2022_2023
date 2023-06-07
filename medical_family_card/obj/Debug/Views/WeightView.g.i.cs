﻿#pragma checksum "..\..\..\Views\WeightView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C095278ADC71C76CA41A074083BEF0DD6EA43806495E33BB242A97E4F55B6EA8"
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
    /// WeightView
    /// </summary>
    public partial class WeightView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menuChangeItems;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemSave;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemCancel;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemClose;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxWeightValue;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Views\WeightView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerWeightDate;
        
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
            System.Uri resourceLocater = new System.Uri("/medical_family_card;component/views/weightview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\WeightView.xaml"
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
            this.menuChangeItems = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            this.menuItemSave = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.menuItemCancel = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 4:
            this.menuItemClose = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            this.textBoxWeightValue = ((System.Windows.Controls.TextBox)(target));
            
            #line 88 "..\..\..\Views\WeightView.xaml"
            this.textBoxWeightValue.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBoxUsername_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.datePickerWeightDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 96 "..\..\..\Views\WeightView.xaml"
            this.datePickerWeightDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerDateOfBirth_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

