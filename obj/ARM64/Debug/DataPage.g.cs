﻿#pragma checksum "C:\Users\tomok\source\repos\MMSS\DataPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "86342B86C4F29B13578877C7C3E873E19B7458A309A23E46595BFAC79D0E0871"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMSS
{
    partial class DataPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // DataPage.xaml line 12
                {
                    this.Navigations = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Navigations).ItemInvoked += this.NavView_ItemInvoked;
                }
                break;
            case 3: // DataPage.xaml line 44
                {
                    this.FileTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // DataPage.xaml line 48
                {
                    this.CSVfileReadResult_text = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // DataPage.xaml line 49
                {
                    this.CsvReaderComplition_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CsvReaderComplition_btn).Click += this.CsvFileReaderComplition;
                }
                break;
            case 6: // DataPage.xaml line 40
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.Serch_folder_Button_Clicked;
                }
                break;
            case 7: // DataPage.xaml line 32
                {
                    this.ReadCSVTeamName_text = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // DataPage.xaml line 18
                {
                    this.Main = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 9: // DataPage.xaml line 19
                {
                    this.Score = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 10: // DataPage.xaml line 20
                {
                    this.Data = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 11: // DataPage.xaml line 22
                {
                    this.Order = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 12: // DataPage.xaml line 23
                {
                    this.Opt = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 13: // DataPage.xaml line 24
                {
                    this.Team = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
