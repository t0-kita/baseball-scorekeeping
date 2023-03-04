﻿#pragma checksum "C:\Users\tomok\source\repos\MMSS\GameOptions.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4E43A7C18265F999F0257FAD2F1ACFE40B6013A6411D2F7AD89C55396064A3E1"
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
    partial class GameOptions : 
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
            case 2: // GameOptions.xaml line 12
                {
                    this.Navigations = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Navigations).ItemInvoked += this.NavView_ItemInvoked;
                }
                break;
            case 3: // GameOptions.xaml line 202
                {
                    this.GameOptionsEnter = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.GameOptionsEnter).Click += this.ToGameMatchTeamSelect_Clicked;
                }
                break;
            case 4: // GameOptions.xaml line 156
                {
                    this.UmpireNameSelect = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.UmpireNameSelect).Tapped += this.UmpireNameSelect_Tapped;
                }
                break;
            case 5: // GameOptions.xaml line 172
                {
                    this.CreateUmpireName_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CreateUmpireName_btn).Click += this.CreateNewUmpireName;
                }
                break;
            case 6: // GameOptions.xaml line 190
                {
                    this.NewUmpireCreate_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewUmpireCreate_btn).Click += this.NewUmpireNameCreate_Button_Clicked;
                }
                break;
            case 7: // GameOptions.xaml line 185
                {
                    this.NewUmpireNameCreate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewUmpireNameCreate).TextChanged += this.NewUmpireCreate_TextChanging;
                }
                break;
            case 9: // GameOptions.xaml line 109
                {
                    this.WeatherNameSelect = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.WeatherNameSelect).Tapped += this.WeatherNameSelect_Tapped;
                }
                break;
            case 10: // GameOptions.xaml line 125
                {
                    this.CreateWeatherName_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CreateWeatherName_btn).Click += this.CreateNewWeatherName;
                }
                break;
            case 11: // GameOptions.xaml line 141
                {
                    this.NewWeatherCreate_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewWeatherCreate_btn).Click += this.NewCreateWeatherName_Button_Clicked;
                }
                break;
            case 12: // GameOptions.xaml line 136
                {
                    this.NewWeatherNameCreate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewWeatherNameCreate).TextChanged += this.NewWeatherCreate_TextChanging;
                }
                break;
            case 14: // GameOptions.xaml line 45
                {
                    this.FieldNameSelect = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.FieldNameSelect).Tapped += this.FieldNameSelect_Tapped;
                }
                break;
            case 15: // GameOptions.xaml line 74
                {
                    this.CreateFieldName_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CreateFieldName_btn).Click += this.CreateNewFieldName;
                }
                break;
            case 16: // GameOptions.xaml line 96
                {
                    this.NewFieldCreate_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewFieldCreate_btn).Click += this.NeWBallParkCreate_Button_Clicked;
                }
                break;
            case 17: // GameOptions.xaml line 92
                {
                    this.NewFiledAreaName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewFiledAreaName).TextChanged += this.NewFieldCreate_TextChanging;
                }
                break;
            case 18: // GameOptions.xaml line 86
                {
                    this.NewFieldNameCreate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewFieldNameCreate).TextChanged += this.NewFieldCreate_TextChanging;
                }
                break;
            case 20: // GameOptions.xaml line 32
                {
                    this.GameStartDatePicker = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                    ((global::Windows.UI.Xaml.Controls.CalendarDatePicker)this.GameStartDatePicker).DateChanged += this.GameStartDatePicker_DateChanged;
                }
                break;
            case 21: // GameOptions.xaml line 36
                {
                    this.GameStartTimePicker = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                    ((global::Windows.UI.Xaml.Controls.TimePicker)this.GameStartTimePicker).SelectedTimeChanged += this.GameStartTimePicker_SelectedTimeChanged;
                }
                break;
            case 22: // GameOptions.xaml line 18
                {
                    this.Main = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 23: // GameOptions.xaml line 19
                {
                    this.Score = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 24: // GameOptions.xaml line 20
                {
                    this.Data = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 25: // GameOptions.xaml line 22
                {
                    this.Order = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 26: // GameOptions.xaml line 23
                {
                    this.Opt = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 27: // GameOptions.xaml line 24
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

