#pragma checksum "C:\Users\tomok\source\repos\MMSS\GameMatchTeamSelect.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B03BD048436832DC5D35FA3DE242369AC94530992597390DAD856E29F20B188A"
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
    partial class GameMatchTeamSelect : 
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
            case 2: // GameMatchTeamSelect.xaml line 12
                {
                    this.Navigations = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Navigations).ItemInvoked += this.NavView_ItemInvoked;
                }
                break;
            case 3: // GameMatchTeamSelect.xaml line 132
                {
                    this.FirstFieldTeam = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.FirstFieldTeam).Tapped += this.FieldFirstTeamListView_Item_Tapped;
                }
                break;
            case 4: // GameMatchTeamSelect.xaml line 163
                {
                    this.FilterByFieldFirstName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.FilterByFieldFirstName).TextChanged += this.OnFilterChanged;
                }
                break;
            case 5: // GameMatchTeamSelect.xaml line 168
                {
                    this.FilterByFieldLastName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.FilterByFieldLastName).TextChanged += this.OnFilterChanged;
                }
                break;
            case 6: // GameMatchTeamSelect.xaml line 174
                {
                    this.CreateFirstFieldTeamName_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CreateFirstFieldTeamName_btn).Click += this.CreateNewTeamName;
                }
                break;
            case 8: // GameMatchTeamSelect.xaml line 80
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.GameTeamEnter_Button_Clicked;
                }
                break;
            case 9: // GameMatchTeamSelect.xaml line 85
                {
                    this.CreateFirstBatTeamName_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.CreateFirstBatTeamName_btn).Click += this.CreateNewTeamName;
                }
                break;
            case 10: // GameMatchTeamSelect.xaml line 117
                {
                    this.NewTeamCreate_btn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.NewTeamCreate_btn).Click += this.NewTeamCreate_btn_Clicked;
                }
                break;
            case 11: // GameMatchTeamSelect.xaml line 105
                {
                    this.NewTeamAreaName = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.NewTeamAreaName).SelectionChanged += this.NewTeamCreate_SelectionChanged;
                }
                break;
            case 13: // GameMatchTeamSelect.xaml line 98
                {
                    this.NewTeamNameCreate = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.NewTeamNameCreate).TextChanged += this.NewTeamCreate_TextChanging;
                }
                break;
            case 14: // GameMatchTeamSelect.xaml line 32
                {
                    this.FirstBatTeam = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.FirstBatTeam).Tapped += this.BatFirstTeamListView_Item_Tapped;
                }
                break;
            case 15: // GameMatchTeamSelect.xaml line 64
                {
                    this.FilterByBatFirstName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.FilterByBatFirstName).TextChanged += this.OnFilterChanged;
                }
                break;
            case 16: // GameMatchTeamSelect.xaml line 70
                {
                    this.FilterByBatLastName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.FilterByBatLastName).TextChanged += this.OnFilterChanged;
                }
                break;
            case 18: // GameMatchTeamSelect.xaml line 18
                {
                    this.Main = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 19: // GameMatchTeamSelect.xaml line 19
                {
                    this.Score = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 20: // GameMatchTeamSelect.xaml line 20
                {
                    this.Data = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 21: // GameMatchTeamSelect.xaml line 21
                {
                    this.Order = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 22: // GameMatchTeamSelect.xaml line 22
                {
                    this.Opt = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 23: // GameMatchTeamSelect.xaml line 23
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

