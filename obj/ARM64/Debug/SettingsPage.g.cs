﻿#pragma checksum "C:\Users\tomok\source\repos\MMSS\SettingsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6FA45D23B1B284A9FFD01426B576945665DA7C0C51A8BA9F53E74E7BE7A5011D"
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
    partial class SettingsPage : 
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
            case 2: // SettingsPage.xaml line 12
                {
                    this.Navigations = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Navigations).ItemInvoked += this.NavView_ItemInvoked;
                }
                break;
            case 3: // SettingsPage.xaml line 186
                {
                    this.LanguageComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.LanguageComboBox).SelectionChanged += this.LanguageComboBox_SelectionChanged;
                }
                break;
            case 5: // SettingsPage.xaml line 37
                {
                    this.BallTypeSTackPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                    ((global::Windows.UI.Xaml.Controls.StackPanel)this.BallTypeSTackPanel).Tapped += this.BallTypeAddButton_Tapped;
                }
                break;
            case 6: // SettingsPage.xaml line 92
                {
                    this.BallTypeAddButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BallTypeAddButton).Click += this.BallTypeAddButton_Click;
                }
                break;
            case 7: // SettingsPage.xaml line 174
                {
                    this.BallImgGrid_8 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 8: // SettingsPage.xaml line 170
                {
                    this.BallImgGrid_7 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 9: // SettingsPage.xaml line 166
                {
                    this.BallImgGrid_6 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 10: // SettingsPage.xaml line 159
                {
                    this.BallImgGrid_5 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 11: // SettingsPage.xaml line 155
                {
                    this.BallImgGrid_4 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 12: // SettingsPage.xaml line 151
                {
                    this.BallImgGrid_3 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 13: // SettingsPage.xaml line 144
                {
                    this.BallImgGrid_2 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 14: // SettingsPage.xaml line 140
                {
                    this.BallImgGrid_1 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 15: // SettingsPage.xaml line 136
                {
                    this.BallImgGrid_0 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 16: // SettingsPage.xaml line 121
                {
                    this.NewBallTypeCreate_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewBallTypeCreate_btn).Click += this.NewBallTypeCreate_btn_Clicked;
                }
                break;
            case 17: // SettingsPage.xaml line 109
                {
                    this.NewBallTypeImgCode = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.NewBallTypeImgCode).SelectionChanged += this.NewBallTypeImgCode_SelectionChanged;
                }
                break;
            case 19: // SettingsPage.xaml line 102
                {
                    this.NewBallTypeNameCreate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewBallTypeNameCreate).TextChanged += this.NewBallTypeNameCreate_TextChanging;
                }
                break;
            case 20: // SettingsPage.xaml line 39
                {
                    this.BallTypeListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.BallTypeListView).Tapped += this.BallTypeListView_Tapped;
                }
                break;
            case 24: // SettingsPage.xaml line 78
                {
                    global::Windows.UI.Xaml.Controls.TextBox element24 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)element24).GotFocus += this.BallTypeImg_GotFocus;
                }
                break;
            case 25: // SettingsPage.xaml line 19
                {
                    this.Main = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 26: // SettingsPage.xaml line 20
                {
                    this.Score = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 27: // SettingsPage.xaml line 21
                {
                    this.Data = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 28: // SettingsPage.xaml line 23
                {
                    this.Order = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 29: // SettingsPage.xaml line 24
                {
                    this.Opt = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 30: // SettingsPage.xaml line 25
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
