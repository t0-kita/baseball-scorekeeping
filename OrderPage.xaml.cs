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
	public sealed partial class OrderPage : Page
	{
		public OrderPage()
		{
			this.InitializeComponent();
			PlayOrderSub.InitializeDB();
			PlayerData.InitializeDB();

			//int game_id = GameData.GetRecordsCount()[0].game_count;
			//List<GameData.gameData> game = GameData.GetRecords(game_id);
			int game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			List<GameData.gameData> game = GameData.GetRecords(game_id);
			topFirstStartFlg = game[0].top_order_start_flg;
			btmFirstStartFlg = game[0].btm_order_start_flg;
			topTeamId = game[0].bat_first_team_id;
			btmTeamId = game[0].field_first_team_id;
			if (!topFirstStartFlg && !btmFirstStartFlg)
			{
				selectTeamId = topTeamId;
				selectTopBtmSt = "先攻チーム";
			}
			else if (topFirstStartFlg && !btmFirstStartFlg)
			{
				selectTeamId = btmTeamId;
				selectTopBtmSt = "後攻チーム";
			}
			CsvSelectedRows = PlayOrderSub.GetRecordsAllMember(
												team_id: selectTeamId.ToString());
			TeamOrderIdListView.ItemsSource = CsvSelectedRows;
			//TeamOrderIdListView.ItemsSource = PlayOrderSub.GetRecordsAllMember(
			//												team_id:selectTeamId.ToString());
			//CsvSelectedRows = PlayOrderSub.GetRecordsScvFile(team_id:"0");
			PlayOrder.InitializeDB();
			//TeamPlayerDataListView.ItemsSource = PlayerData.GetRecords(team_id: selectTeamId);
			TeamPlayerDataListView.ItemsSource = PlayerData.GetRecordsAllMember(team_id: selectTeamId);
			PlayerData.InitializeDB();
			players = PlayerData.GetRecords(selectTeamId);

		}

		private List<PlayOrderSub.playOrder> CsvSelectedRows;
		//private List<PlayOrder.playOrder> CsvSelectedRows;
		private int selectTeamId;
		private int topTeamId;
		private int btmTeamId;
		private bool topFirstStartFlg;
		private bool btmFirstStartFlg;

		private string selectTopBtmSt;

		private List<PlayerData.playerData> players;
		private List<PlayerData.playerData> gamePlayers;
		private List<PlayerData.playerData> reservePlayers;


		private void CsvFileReader_Button_Clicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(DataPage));
		}

		private void ToMainPage_Button_Clicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
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
				//case "Order":
				//	Frame.Navigate(typeof(OrderPage));
				//	//Frame.Navigate(typeof(GameOrders));
				//	break;
				case "Score":
					Frame.Navigate(typeof(ScorePage));
					break;
				case "Data":
					//Frame.Navigate(typeof(DataPage));
					Frame.Navigate(typeof(DataAnalysisPage));
					break;
				//case "Opt":
				//	Frame.Navigate(typeof(GameOptions));
				//	break;
				//case "Team":
				//	Frame.Navigate(typeof(GameMatchTeamSelect));
				//	break;
			}
		}

		private void TeamOrderName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var lv = (ListView)sender;
			object lvitem = lv.ItemsSource;

		}

		private void CsvfileAddPlayer_Checked(object sender, RoutedEventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			bool checkBoxFlg = (bool)checkBox.IsChecked;
			PlayOrderSub.playOrder Row = (PlayOrderSub.playOrder)checkBox.DataContext;
			if (checkBoxFlg)
			{
				//PlayOrder.playOrder Row = (PlayOrder.playOrder)checkBox.DataContext;
				CsvSelectedRows.Add(Row);
			}
			else 
			{
				CsvSelectedRows.Remove(Row);
			}
	}

		private void CsvSelectPlayerData_btn_Clicked(object sender, RoutedEventArgs e)
		{
			var tmp_player_id_Obj = PlayOrderSub.GetPlayerIdRecordsCount();
			int player_id = tmp_player_id_Obj[0].player_id - 1;
			player_id = PlayerData.GetPlayerIdRecordsCount()[0].player_id;
			foreach (PlayOrderSub.playOrder Row in CsvSelectedRows) 
			{
				
				bool flgOrder = PlayerData.SearchExistingOrderId(
											team_id:Convert.ToInt32(Row.team_id),
											order_id:Row.order_id
											);
				bool flgPosition = PlayerData.SearchExistingPosition(
											team_id: Convert.ToInt32(Row.team_id),
											positionST: Row.position
											);

				LangClass lang = new LangClass(
										position: Row.position,
										hand: Row.hand,
										bat: Row.bat
										);
				int position = lang.GetPosition();
				int hand_id = lang.GetHand_id();
				int bat_id = lang.GetBat_id();

				if (flgOrder && flgPosition)
				{
					PlayerData.addRecord(
						player_id: player_id,
						team_id: selectTeamId,
						etc_str2: Row.order_id,
						player_name: Row.name,
						//etc_str1: Row.position,
						//bat: Row.bat,
						//hand: Row.hand,
						position: position,
						hand_id: hand_id,
						bat_id: bat_id,
						player_num:Convert.ToInt32(Row.player_num),
						update_date: DateTime.Now
						);
				}
				else 
				{
					PlayerData.addRecord(
						player_id: player_id,
						team_id: selectTeamId,
						//etc_str2: "0",
						etc_str2: "R",
						player_name: Row.name,
						//etc_str1: "",
						//bat: Row.bat,
						//hand: Row.hand,
						position: position,
						hand_id: hand_id,
						bat_id: bat_id,
						player_num: Convert.ToInt32(Row.player_num),
						update_date: DateTime.Now
						);
				}


				player_id++;
			}
			Frame.Navigate(typeof(OrderListPage));

		}
	}
}
