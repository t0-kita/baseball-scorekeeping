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
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Text;
using static System.Console;
using System.Data;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MMSS
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class DataPage : Page
	{
		public DataPage()
		{
			this.InitializeComponent();
			PlayerData.InitializeDB();
		}
		private DataTable dt;
		private int selectTeamId;
		private string selectTopBtmSt;

		private async void Serch_folder_Button_Clicked(object sender, RoutedEventArgs e)
		{
			Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker();
			picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
			picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
			picker.FileTypeFilter.Add(".txt");
			picker.FileTypeFilter.Add(".csv");

			Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
			if (file == null)
			{
				this.FileTextBox.Text = "Operation cancelled.";
				return;
			}
			string text = await Windows.Storage.FileIO.ReadTextAsync(file);
			List<string> textList = new List<string>();
			textList = text.Split("\r\n").ToList();
			dt = new DataTable();
			List<string> headerList = new List<string>();
			headerList = textList[0].Split(",").ToList();
			foreach (string st in headerList) 
			{
				dt.Columns.Add(st);
			}
			for(int i = 1; i < textList.Count - 1; i++) 
			{
				DataRow dr = dt.NewRow();
				List<string> tmp_list = new List<string>();
				tmp_list = textList[i].Split(",").ToList();
				dr[headerList[0]] = tmp_list[0];  // 打順
				dr[headerList[1]] = tmp_list[1];  // 名前
				dr[headerList[2]] = tmp_list[2];  // 守備
				dr[headerList[3]] = tmp_list[3];  // 投げ
				dr[headerList[4]] = tmp_list[4];  // 打席
				dr[headerList[5]] = tmp_list[5];  // 背番号
				dt.Rows.Add(dr);
			}

			this.FileTextBox.Text = text;
			if (text.Length != 0)
			{
				this.CSVfileReadResult_text.Text = "上記の内容を取得しました";
				CsvReaderComplition_btn.Visibility = Visibility.Visible;
			}
			else 
			{
				this.CSVfileReadResult_text.Text = "取得ができていません";
				CsvReaderComplition_btn.Visibility = Visibility.Collapsed;

			}
		}

		

		private void CsvFileReaderComplition(object sender, RoutedEventArgs e)
		{
			PlayOrderSub.InitializeDB();
			var tmp_player_id_Obj = PlayOrderSub.GetPlayerIdRecordsCount();
			int player_id = tmp_player_id_Obj[0].player_id -1;
			int game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			List<GameData.gameData> game = GameData.GetRecords(game_id);
			bool topFirstStartFlg = game[0].top_order_start_flg;
			bool btmFirstStartFlg = game[0].btm_order_start_flg;
			int topTeamId = game[0].bat_first_team_id;
			int btmTeamId = game[0].field_first_team_id;
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
			int team_count = PlayOrderSub.GetRecordsTeamCount(team_id:selectTeamId)[0].team_count;
			if (team_count != 0)
			{
				PlayOrderSub.deleteRecord(
									team_id: selectTeamId.ToString(),
									teamName: selectTopBtmSt);
			}
			foreach (DataRow row in dt.Rows) 
			{
				player_id++;
				string player_number = "0";
				if (row["背番号"].ToString().Length != 0) 
				{
					player_number = row["背番号"].ToString();
				}

				PlayOrderSub.addRecord(
									teamName: selectTopBtmSt,
									order_id: row["打順"].ToString(),
									name: row["名前"].ToString(),
									position: row["守備"].ToString(),
									bat: row["打"].ToString(),
									hand: row["投"].ToString(),
									player_num: player_number,
									player_id: player_id.ToString(),
									team_id: selectTeamId.ToString(),
									game_id: game_id.ToString()
									);
			}
			Frame.Navigate(typeof(OrderPage));
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

	}

}
