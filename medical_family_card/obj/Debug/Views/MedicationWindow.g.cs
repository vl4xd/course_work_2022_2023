﻿#pragma checksum "..\..\..\Views\MedicationWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A77A6E1BBC4B1425142B255B0F5BA32BBB21FBF30D6A7C27AAE0D07CEFA0E329"
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
    /// MedicationWindow
    /// </summary>
    public partial class MedicationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menuChangeItems;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemSave;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemCancel;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemClose;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxMedicationName;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerStDate;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerEndDate;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxComment;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock addPhoto;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\Views\MedicationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackPanelPhoto;
        
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
            System.Uri resourceLocater = new System.Uri("/medical_family_card;component/views/medicationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MedicationWindow.xaml"
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
            
            #line 19 "..\..\..\Views\MedicationWindow.xaml"
            ((medical_family_card.Views.MedicationWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.menuChangeItems = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            this.menuItemSave = ((System.Windows.Controls.MenuItem)(target));
            
            #line 53 "..\..\..\Views\MedicationWindow.xaml"
            this.menuItemSave.Click += new System.Windows.RoutedEventHandler(this.menuItemSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuItemCancel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 58 "..\..\..\Views\MedicationWindow.xaml"
            this.menuItemCancel.Click += new System.Windows.RoutedEventHandler(this.menuItemCancel_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.menuItemClose = ((System.Windows.Controls.MenuItem)(target));
            
            #line 62 "..\..\..\Views\MedicationWindow.xaml"
            this.menuItemClose.Click += new System.Windows.RoutedEventHandler(this.menuItemClose_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.textBoxMedicationName = ((System.Windows.Controls.TextBox)(target));
            
            #line 125 "..\..\..\Views\MedicationWindow.xaml"
            this.textBoxMedicationName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textBoxMedicationName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.datePickerStDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 134 "..\..\..\Views\MedicationWindow.xaml"
            this.datePickerStDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerStDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.datePickerEndDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 143 "..\..\..\Views\MedicationWindow.xaml"
            this.datePickerEndDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerEndDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.textBoxComment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.addPhoto = ((System.Windows.Controls.TextBlock)(target));
            
            #line 182 "..\..\..\Views\MedicationWindow.xaml"
            this.addPhoto.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.addPhoto_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 11:
            this.stackPanelPhoto = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

