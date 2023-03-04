using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Toolkit.Uwp.UI.Controls;



// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MMSS
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class GameMatchTeamSelect : Page
	{
		public GameMatchTeamSelect()
		{
			this.InitializeComponent();
			Prefectures.InitializeDB();
			TeamData.InitializeDB();

			FirstBatTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect();
			FirstFieldTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect();
			teamDatasBat = TeamData.GetRecordsGameMatchTeamSelect();
			teamDatasField = TeamData.GetRecordsGameMatchTeamSelect();
			NewTeamAreaName.ItemsSource = Prefectures.GetRecords();

		}

		private List<TeamData.teamDataTeamSelect> teamDatas;
		private TeamData.teamDataTeamSelect batFirstteamData;
		private TeamData.teamDataTeamSelect fieldFirstteamData;
		private List<TeamData.teamDataTeamSelect> teamDatasBat;
		private List<TeamData.teamDataTeamSelect> teamDatasField;
		private bool batSelFlg = false;
		private bool fieldSelFlg = false;
		private int distinct_id =0;

		private void OnFilterChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			string input_text = textBox.Text;
			switch (textBox.Name) 
			{
				case "FilterByBatFirstName":
					FirstBatTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect(teamName: input_text);
					teamDatasBat = TeamData.GetRecordsGameMatchTeamSelect(teamName: input_text);
					break;
				case "FilterByBatLastName":
					FirstBatTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect(distinctName: input_text);
					teamDatasBat = TeamData.GetRecordsGameMatchTeamSelect(distinctName: input_text);
					break;
				case "FilterByFieldFirstName":
					FirstFieldTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect(teamName: input_text);
					teamDatasField = TeamData.GetRecordsGameMatchTeamSelect(teamName: input_text);
					break;
				case "FilterByFieldLastName":
					FirstFieldTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect(distinctName: input_text);
					teamDatasField = TeamData.GetRecordsGameMatchTeamSelect(distinctName: input_text);
					break;
			}
		}

		/// <summary>
		/// 新規チームの作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateNewTeamName(object sender, RoutedEventArgs e)
		{

		}

		private void NewTeamCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewTeamNameCreate.Text != "" & distinct_id != 0) 
			{
				//NewTeamCreate_btn.Visibility = Visibility.Visible;
				NewTeamCreate_btn.IsEnabled = true;
			}
			else 
			{
				//NewTeamCreate_btn.Visibility = Visibility.Collapsed;
				NewTeamCreate_btn.IsEnabled = false;
			}
		}

		private void GameTeamEnter_Button_Clicked(object sender, RoutedEventArgs e)
		{
			if (batSelFlg && fieldSelFlg)
			{
				//if (batFirstteamData != fieldFirstteamData)
				//{
				//	/// 試合前に交代選手フラグがあれば、リセットする
				//	PlayerData.resetReserveFlgRecord(team_id:batFirstteamData.team_id);
				//	PlayerData.resetReserveFlgRecord(team_id: fieldFirstteamData.team_id);

				//	int game_id = GameData.GetGameIdRecord()[0].game_id - 1;
				//	GameData.updateRecord(
				//				game_id: game_id,
				//				bat_first_team_id: batFirstteamData.team_id,
				//				field_first_team_id: fieldFirstteamData.team_id);
				//	Frame.Navigate(typeof(OrderListPage));
				//}
				//else { MessageText_NotSelectedTeams(msg:"チームが重複しています"); }

				/// 2022.05.12 試合中選手交代した選手フラグを元に戻す
				PlayerData.BeforeGameStartReSetUpdate();

				/// 紅白戦を考慮して、チーム選択を重複可能にしている 2022.01.26
				PlayerData.resetReserveFlgRecord(team_id: batFirstteamData.team_id);
				PlayerData.resetReserveFlgRecord(team_id: fieldFirstteamData.team_id);

				int game_id = GameData.GetGameIdRecord()[0].game_id - 1;
				GameData.updateRecord(
							game_id: game_id,
							bat_first_team_id: batFirstteamData.team_id,
							field_first_team_id: fieldFirstteamData.team_id);
				Frame.Navigate(typeof(OrderListPage));

			}
			else 
			{
				MessageText_NotSelectedTeams();
			}
		}

		private async void MessageText_NotSelectedTeams(string msg="")
		{
			if (msg.Length == 0)
			{
				msg = "チームが選択されていません";
			}
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "チーム選択";
			dialog.Content = msg;
			dialog.CloseButtonText = "閉じる";
			dialog.DefaultButton = ContentDialogButton.None;
			var result = await dialog.ShowAsync();

		}

			private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args.IsSettingsInvoked)
			{
				Frame.Navigate(typeof(SettingsPage));
			}
			else
			{
				var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
				NavView_Navigate(item as NavigationViewItem);
			}
		}


		private void NavView_Navigate(NavigationViewItem item)
		{
			switch (item.Tag)
			{
				case "Top":
					Frame.Navigate(typeof(MainPage));
					break;
				case "Score":
					Frame.Navigate(typeof(ScorePage));
					break;
				case "Data":
					Frame.Navigate(typeof(DataAnalysisPage));
					break;

			}
		}

		/// <summary>
		/// 先攻チームListViewの選択時、
		/// 先攻チームの情報を取得する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BatFirstTeamListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			int index = listView.SelectedIndex;
			//batFirstteamData = teamDatas[index];
			try
			{
				batFirstteamData = teamDatasBat[index];
				batSelFlg = true;
			}
			catch { }
		}

		private void FieldFirstTeamListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			int index = listView.SelectedIndex;
			//fieldFirstteamData = teamDatas[index];
			try
			{
				fieldFirstteamData = teamDatasField[index];
				fieldSelFlg = true;
			}
			catch { }
			}

		private void NewTeamCreate_btn_Clicked(object sender, RoutedEventArgs e)
		{
			string name = NewTeamNameCreate.Text;
			int teamCount = TeamData.GetRecordCount()[0].count + 1;
			//int distinct_id = 26;  // 26:京都
			TeamData.addRecord(
						team_id: teamCount,
						teamName: name,
						distinct_id: distinct_id,
						update_dt: DateTime.Now);

			FirstBatTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect();
			FirstFieldTeam.ItemsSource = TeamData.GetRecordsGameMatchTeamSelect();
			//teamDatas = TeamData.GetRecordsGameMatchTeamSelect();
			teamDatasBat = TeamData.GetRecordsGameMatchTeamSelect();
			teamDatasField = TeamData.GetRecordsGameMatchTeamSelect();

		}

		private void NewTeamCreate_SelectionChanged(object sender, RoutedEventArgs e)
		{
			ComboBox combo = (ComboBox)sender;
			distinct_id = combo.SelectedIndex + 1;
			if (NewTeamNameCreate.Text != "" & distinct_id != 0)
			{
				NewTeamCreate_btn.IsEnabled = true;
			}
			else
			{
				NewTeamCreate_btn.IsEnabled = false;
			}
		}
	}
}
