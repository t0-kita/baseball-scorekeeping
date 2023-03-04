using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public sealed partial class SettingPlayerDataPage : Page
	{
		public SettingPlayerDataPage()
		{
			this.InitializeComponent();
			TeamName.ItemsSource = TeamData.GetRecords();

			//PlayerName.ItemsSource = PlayerData.GetRecordsPitchers();
			EntryWedgetsIsEnable(false);
			LangInitialize();
			TextBoesInitialize();
			GetPlayerDataList();
		}
		private string player_name = "";
		private int player_number = -1;
		private int team_id = -1;
		private int selected_team_id = -1;
		private int player_id = -1;
		private int hand_id = -1;
		private int bat_id = -1;
		private int selected_id = -1;
		private PlayerData.playerData player = new PlayerData.playerData();
		private int selectedLang = UseLanguage.GetRecords(selected: 1)[0].id;
		private List<UseDisplay.display> disp_throw;
		private List<UseDisplay.display> disp_batbox;
		private string thr_r;
		private string thr_l;
		private string batbox_r;
		private string batbox_l;
		private string batbox_sw;
		ObservableCollection<Throw> Throws = new ObservableCollection<Throw>();
		ObservableCollection<BatBox> BatBoxes = new ObservableCollection<BatBox>();
		ObservableCollection<Selected> Selecteds = new ObservableCollection<Selected>();
		//ObservableCollection<TeamData> Teams = new ObservableCollection<TeamData>();
		List<TeamData.teamData> Teams = TeamData.GetRecords();

		private void LangInitialize()
		{
			LangClassNameGetRecords();
			LangThrowInitialize();
			LangBatBoxInitialize();

		}

		private void LangClassNameGetRecords()
		{
			disp_throw = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "Throw", lang_k: selectedLang);
			disp_batbox = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "BatBox", lang_k: selectedLang);
		}

		private void LangThrowInitialize()
		{
			thr_r = disp_throw[0].text;
			thr_l = disp_throw[1].text;
		}

		private void LangBatBoxInitialize()
		{
			batbox_r = disp_batbox[0].text;
			batbox_l = disp_batbox[1].text;
			batbox_sw = disp_batbox[2].text;
		}

		private void TextBoesInitialize()
		{
			Throws.Add(new Throw(0, thr_r));
			Throws.Add(new Throw(1, thr_l));

			BatBoxes.Add(new BatBox(0, batbox_r));
			BatBoxes.Add(new BatBox(1, batbox_l));
			BatBoxes.Add(new BatBox(2, batbox_sw));

			Selecteds.Add(new Selected(0, "Entry Player"));
			Selecteds.Add(new Selected(1, "Not Entry"));
		}

		private void ComboBoxBinding()
		{
			//PlayerHandComboBox.ItemsSource = Throws;
			//PlayerBatComboBox.ItemsSource = BatBoxes;
		}

		private void SetComboBoxBatValue()
		{
			PlayerBatComboBox.SelectedItem = BatBoxes.Where(p => p.id == bat_id).First();
		}
		private void SetComboBoxHandValue()
		{
			PlayerHandComboBox.SelectedItem = Throws.Where(p => p.id == hand_id).First();
		}

		private void SetComboBoxSelectValue()
		{
			PlayerSelectedComboBox.SelectedItem = Selecteds.Where(p => p.id == selected_id).First();
		}

		private void GetComboBoxTeamsValue()
		{
			TeamData.teamData teamData = (TeamData.teamData)PlayerTeamComboBox.SelectedItem;
			selected_team_id = teamData.team_id;
		}

		private void GetComboBoxBatValue()
		{
			BatBox batBox = (BatBox)PlayerBatComboBox.SelectedItem;
			bat_id = batBox.id;
		}
		private void GetComboBoxHandValue()
		{
			Throw throwData = (Throw)PlayerHandComboBox.SelectedItem;
			hand_id = throwData.id;
		}

		private void GetComboBoxSelectValue()
		{
			Selected selected = (Selected)PlayerSelectedComboBox.SelectedItem;
			selected_id = selected.id;
		}

		private void GetComboBoxes() 
		{
			GetComboBoxTeamsValue();
			GetComboBoxBatValue();
			GetComboBoxHandValue();
			GetComboBoxSelectValue();
		}

		private void EntryWedgetsIsEnable(bool visible = true) 
		{
			PlayerNameTextBox.IsEnabled = visible;
			PlayerHandComboBox.IsEnabled = visible;
			PlayerBatComboBox.IsEnabled = visible;
			PlayerTeamComboBox.IsEnabled = visible;
			PlayerNumberTextBox.IsEnabled = visible;
			PlayerSelectedComboBox.IsEnabled = visible;
			EntryBottn.IsEnabled = visible;
		}

		private void SetComboBoxTeamsValue()
		{
			PlayerTeamComboBox.SelectedItem = Teams.Where(p => p.team_id == team_id).First();
		}



		private bool GetPlayerDataList(int team_id = -1)
		{
			try
			{
				if (team_id < 0)
				{
					PlayerName.ItemsSource = PlayerData.GetRecords();
				}
				if (team_id >= 0)
				{
					PlayerName.ItemsSource = PlayerData.GetRecords(team_id: team_id);
				}
			}
			catch { return false; }
			return true;
		}

		private void GetPlayerName()
		{
			try
			{
				PlayerNameTextBox.Text = player.name ?? "";
				player_name = player.name;
			}
			catch (Exception err)
			{ Console.WriteLine(err.ToString()); }
		}

		private void GetPlayerNumber()
		{
			try
			{
				PlayerNumberTextBox.Text = player.player_num.ToString();
				player_number = player.player_num;
			}
			catch (Exception err)
			{ Console.WriteLine(err.ToString()); }
		}

		private void GetPlayerBat()
		{
			bat_id = player.bat_id;
		}

		private void GetPlayerHand()
		{
			hand_id = player.hand_id;
		}

		private void GetPlayerTeam()
		{
			team_id = player.team_id;
		}

		private void GetPlayerSelected()
		{
			selected_id = player.selected;
		}


		private void TeamName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			TeamData.teamData teamDatas = (TeamData.teamData)lv.SelectedValue;
			team_id = teamDatas.team_id;
			GetPlayerDataList(team_id: team_id);
		}

		private void TeamNameListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			TeamData.teamData teamDatas = (TeamData.teamData)lv.SelectedValue;
			team_id = teamDatas.team_id;
			GetPlayerDataList(team_id: team_id);
		}

		private void PlayerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			try
			{
				player = (PlayerData.playerData)lv.SelectedValue;
				GetPlayerDatas();

			}
			catch (Exception err)
			{ Console.WriteLine(err.ToString()); }
		}

		private bool CheckPlayerDataNull(PlayerData.playerData player)
		{
			if (player != null)
			{
				EntryWedgetsIsEnable(true);
				return true; 
			}
			EntryWedgetsIsEnable(false);
			return false;
		}

		private void PlayerNameListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			player = (PlayerData.playerData)lv.SelectedValue;
			GetPlayerDatas();

		}

		private void GetPlayerDatas()
		{
			if (!CheckPlayerDataNull(player))
			{ return; }
			player_id = player.player_id;
			GetPlayerName();
			GetPlayerTeam();
			GetPlayerNumber();
			GetPlayerBat();
			GetPlayerHand();
			GetPlayerSelected();
			SetComboBoxBatValue();
			SetComboBoxHandValue();
			SetComboBoxSelectValue();
			SetComboBoxTeamsValue();
		}

		private void EntryButton_Click(object sender, RoutedEventArgs e)
		{
			GetComboBoxes();
			PlayerData.SettingPlayerDataUpdate(
				player_id: player_id,
				//name:player_name,
				name: PlayerNameTextBox.Text,
				hand_id: hand_id,
				bat_id: bat_id,
				team_id: selected_team_id,
				selected: selected_id,
				player_num: Convert.ToInt32(PlayerNumberTextBox.Text)
				);

			GetPlayerDataList(team_id);

			GetPlayerDatas();

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

	}
}
