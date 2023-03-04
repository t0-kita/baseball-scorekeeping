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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MMSS
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class GameResultPage : Page
	{
		public GameResultPage()
		{
			this.InitializeComponent();
			SetGameListView();
			SetGameResultBatters();
			SetGameResultPitcher();
			DisplayChangeBatterOrPitcher();
			SetBattingResultSum();
			SetPitchingResultSum();
		}

		private int game_id = 0;
		private const int TOP_CODE = 0;
		private const int BTM_CODE = 1;
		private bool Pitcher_Flg= false;

		private void SetBattingResultSum() 
		{
			SetBattingResultSumTop();
			SetBattingResultSumBtm();
		}
		private void SetBattingResultSumTop() 
		{
			BattingResult.battingResultSum battingResultSum = BattingResult.GetSumRecords(game_id, TOP_CODE)[0];
			TopBatBATSum.Text= battingResultSum.bat_count.ToString();
			TopBatHITSum.Text = battingResultSum.hit_count.ToString();
			TopBatHITSum.Text = battingResultSum.hit_count.ToString();
			TopBatRBISum.Text = battingResultSum.run_batted_in_count.ToString();
			TopBatHRSum.Text = battingResultSum.homerun_count.ToString();
			TopBatIBBSum.Text = battingResultSum.fourball_count.ToString();
			TopBatDBBSum.Text = battingResultSum.deadball_count.ToString();
			TopBatRUNSum.Text = battingResultSum.runs_count.ToString();
			TopBatSOSum.Text = battingResultSum.strike_out_count.ToString();
			TopBatSACRIFICESum.Text = battingResultSum.sacrifice_count.ToString();
			TopBatSTEALSum.Text = battingResultSum.steal_count.ToString();
		}
		private void SetBattingResultSumBtm() 
		{
			BattingResult.battingResultSum battingResultSum = BattingResult.GetSumRecords(game_id, BTM_CODE)[0];
			BtmBatBATSum.Text = battingResultSum.bat_count.ToString();
			BtmBatHITSum.Text = battingResultSum.hit_count.ToString();
			BtmBatHITSum.Text = battingResultSum.hit_count.ToString();
			BtmBatRBISum.Text = battingResultSum.run_batted_in_count.ToString();
			BtmBatHRSum.Text = battingResultSum.homerun_count.ToString();
			BtmBatIBBSum.Text = battingResultSum.fourball_count.ToString();
			BtmBatDBBSum.Text = battingResultSum.deadball_count.ToString();
			BtmBatRUNSum.Text = battingResultSum.runs_count.ToString();
			BtmBatSOSum.Text = battingResultSum.strike_out_count.ToString();
			BtmBatSACRIFICESum.Text = battingResultSum.sacrifice_count.ToString();
			BtmBatSTEALSum.Text = battingResultSum.steal_count.ToString();
		}

		private void SetPitchingResultSum()
		{
			SetPitchingResultSumTop();
			SetPitchingResultSumBtm();
		}
		private void SetPitchingResultSumTop()
		{
			PitchingResult.pitchingResultSum pitchingResultSum = PitchingResult.GetSumRecords(game_id, TOP_CODE)[0];
			TopPitINNSum.Text = pitchingResultSum.ining_count.ToString();
			TopPitHITSum.Text = pitchingResultSum.hit_count.ToString();
			TopPitBALLSum.Text = pitchingResultSum.ball_count.ToString();
			TopPitHRSum.Text = pitchingResultSum.homerun_count.ToString();
			TopPitIBBSum.Text = pitchingResultSum.fourball_count.ToString();
			TopPitDBBSum.Text = pitchingResultSum.deadball_count.ToString();
			TopPitLostRunSum.Text = pitchingResultSum.lost_runs.ToString();
			TopPitSOSum.Text = pitchingResultSum.strike_out_count.ToString();
			TopPitEarnedSum.Text = pitchingResultSum.earned_runs.ToString();
		}
		private void SetPitchingResultSumBtm()
		{
			PitchingResult.pitchingResultSum pitchingResultSum = PitchingResult.GetSumRecords(game_id, BTM_CODE)[0];
			BtmPitINNSum.Text = pitchingResultSum.ining_count.ToString();
			BtmPitHITSum.Text = pitchingResultSum.hit_count.ToString();
			BtmPitBALLSum.Text = pitchingResultSum.ball_count.ToString();
			BtmPitHRSum.Text = pitchingResultSum.homerun_count.ToString();
			BtmPitIBBSum.Text = pitchingResultSum.fourball_count.ToString();
			BtmPitDBBSum.Text = pitchingResultSum.deadball_count.ToString();
			BtmPitLostRunSum.Text = pitchingResultSum.lost_runs.ToString();
			BtmPitSOSum.Text = pitchingResultSum.strike_out_count.ToString();
			BtmPitEarnedSum.Text = pitchingResultSum.earned_runs.ToString();
		}

		private void SetGameListView() 
		{
			GameListView.ItemsSource = GameData.GetGameResultData();
			GameData.gameTeamData gameTeamData = GameData.GetGameResultData()[0];
			game_id = gameTeamData.game_id;
			//gameData = GameData.GetRecords(game_id)[0];
			TopTeamNameTextBlock.Text = gameTeamData.top_team_name;
			BtmTeamNameTextBlock.Text = gameTeamData.btm_team_name;
		}

		private void SetGameResultBatters() 
		{
			TopTeamOrderIdListView.ItemsSource = BattingResult.GetGameResultDisplayRecords(game_id,TOP_CODE);
			BtmTeamOrderIdListView.ItemsSource = BattingResult.GetGameResultDisplayRecords(game_id, BTM_CODE);
		}
		private void SetGameResultPitcher()
		{
			TopTeamPitcherListView.ItemsSource = PitchingResult.GetRecordsDisplay(game_id: game_id, top_btm_cd: TOP_CODE);
			BtmTeamPitcherListView.ItemsSource = PitchingResult.GetRecordsDisplay(game_id: game_id, top_btm_cd: BTM_CODE);
		}

		private void GameListView_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			GameData.gameTeamData gameSelectData = (GameData.gameTeamData)listView.SelectedValue;
			int selectedGameId = gameSelectData.game_id;
			game_id = gameSelectData.game_id;
			TopTeamNameTextBlock.Text = gameSelectData.top_team_name;
			BtmTeamNameTextBlock.Text = gameSelectData.btm_team_name;
			TopTeamOrderIdListView.ItemsSource = BattingResult.GetGameResultDisplayRecords(selectedGameId, TOP_CODE);
			BtmTeamOrderIdListView.ItemsSource = BattingResult.GetGameResultDisplayRecords(selectedGameId, BTM_CODE);
			TopTeamPitcherListView.ItemsSource = PitchingResult.GetRecordsDisplay(game_id: selectedGameId, top_btm_cd: TOP_CODE);
			BtmTeamPitcherListView.ItemsSource = PitchingResult.GetRecordsDisplay(game_id: selectedGameId, top_btm_cd: BTM_CODE);

			SetBattingResultSum();
			SetPitchingResultSum();
		}

		private bool CheckPitcherDisplayFlg() 
		{
			if (Pitcher_Flg) { return true; }
			return false;
		}

		private void DisplayChangeBatterOrPitcher() 
		{
			if (CheckPitcherDisplayFlg()) 
			{
				TopTeamOrderIdListView.Visibility = Visibility.Collapsed;
				BtmTeamOrderIdListView.Visibility = Visibility.Collapsed;
				TopTeamPitcherListView.Visibility = Visibility.Visible;
				BtmTeamPitcherListView.Visibility = Visibility.Visible;
				BattingResultSum.Visibility = Visibility.Collapsed;
				PitchingResultSum.Visibility = Visibility.Visible;
				return;
			}
			TopTeamOrderIdListView.Visibility = Visibility.Visible;
			BtmTeamOrderIdListView.Visibility = Visibility.Visible;
			TopTeamPitcherListView.Visibility = Visibility.Collapsed;
			BtmTeamPitcherListView.Visibility = Visibility.Collapsed;
			BattingResultSum.Visibility = Visibility.Visible;
			PitchingResultSum.Visibility = Visibility.Collapsed;

		}


		private void PitcherToggleSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = (ToggleSwitch)sender;
			Pitcher_Flg = (bool)toggleSwitch.IsOn;
			DisplayChangeBatterOrPitcher();
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
					//Frame.Navigate(typeof(DataPage));
					Frame.Navigate(typeof(DataAnalysisPage));
					break;
			}
		}
	}
}
