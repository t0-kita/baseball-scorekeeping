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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MMSS
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class GameOptions : Page
	{
		ObservableCollection<BallPark> BallParks = new ObservableCollection<BallPark>();
		public GameOptions()
		{
			this.InitializeComponent();
			//BallPark.InitializeDB();
			//Umpire.InitializeDB();
			//Weather.InitializeDB();
			//GameData.InitializeDB();
			//TmpGameData.InitializeDB();
			//BallData.InitializeDB();
			//BallAction.InitializeDB();
			//BallType.InitializeDB();
			//BoxData.InitializeDB();
			//BallCollor.InitializeDB();
			//BallCourse.InitializeDB();
			//HitType.InitializeDB();


			ballPark = BallPark.GetRecords();
			umpire = Umpire.GetRecords();
			weather = Weather.GetRecords();
			FieldNameSelect.ItemsSource = BallPark.GetRecords();
			UmpireNameSelect.ItemsSource = Umpire.GetRecords();
			WeatherNameSelect.ItemsSource = Weather.GetRecords();

			if (!CheckRunResultRows())
			{
				AddRunResultRows();
			}
		}

		private List<BallPark.ballPark> ballPark;
		private List<Umpire.umpire> umpire;
		private List<Weather.weather> weather;

		private DateTime GameStartDate;
		private string GameStartDateSt;
		private DateTime GameStartTime;
		private string GameStartTimeSt;
		private DateTime GameStartDateTime;
		private int park_id=0;
		private int ump_id=0;
		private int weather_id=0;


		private void NewFieldCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewFieldNameCreate.Text != "" & NewFiledAreaName.Text != "")
			{
				NewFieldCreate_btn.IsEnabled = true;
			}
			else
			{
				NewFieldCreate_btn.IsEnabled = false;
			}
		}

		private void CreateNewFieldName(object sender, RoutedEventArgs e)
		{

		}

		private void CreateNewUmpireName(object sender, RoutedEventArgs e)
		{

		}

		private bool CheckRunResultRows()
		{
			RunResult runResult = new RunResult();
			int rowsCount = RunResult.GetRecords().Count;
			if (rowsCount > 0) { return true; }
			return false;
		}

		private void AddRunResultRows()
		{
			RunResult runResult = new RunResult();
			runResult.SetRunCode(1);
			runResult.SetLanguageId(1);
			runResult.SetRunResultName("インプレー");
			runResult.addRecord();
			runResult.SetLanguageId(0);
			runResult.SetRunResultName("in_play");
			runResult.addRecord();

			runResult.SetRunCode(2);
			runResult.SetLanguageId(1);
			runResult.SetRunResultName("牽制");
			runResult.addRecord();
			runResult.SetLanguageId(0);
			runResult.SetRunResultName("pickoff_attempt");
			runResult.addRecord();

			runResult.SetRunCode(3);
			runResult.SetLanguageId(1);
			runResult.SetRunResultName("牽制死");
			runResult.addRecord();
			runResult.SetLanguageId(0);
			runResult.SetRunResultName("pickoff_out");
			runResult.addRecord();

			runResult.SetRunCode(4);
			runResult.SetLanguageId(1);
			runResult.SetRunResultName("盗塁");
			runResult.addRecord();

			runResult.SetLanguageId(0);
			runResult.SetRunResultName("steal_base");
			runResult.addRecord();

			runResult.SetRunCode(5);
			runResult.SetLanguageId(1);
			runResult.SetRunResultName("盗塁死");
			runResult.addRecord();

			runResult.SetLanguageId(0);
			runResult.SetRunResultName("caught_steal");
			runResult.addRecord();
		}
		private void NewUmpireCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewUmpireNameCreate.Text != "")
			{
				NewUmpireCreate_btn.IsEnabled = true;
			}
			else
			{
				NewUmpireCreate_btn.IsEnabled = false;
			}
		}

		private void CreateNewWeatherName(object sender, RoutedEventArgs e)
		{

		}

		private void NewWeatherCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewWeatherNameCreate.Text != "")
			{
				NewWeatherCreate_btn.IsEnabled = true;
			}
			else
			{
				NewWeatherCreate_btn.IsEnabled = false;
			}
		}

		private void ToGameMatchTeamSelect_Clicked(object sender, RoutedEventArgs e)
		{
			//GameData.InitializeDB();
			//List<GameData.gameDataCount> gameDataCounts = GameData.GetRecordsCount();
			//int gameCount = gameDataCounts[0].game_count + 1;

			int game_id = GameData.GetGameIdRecord()[0].game_id;
			//List<GameData.gameData> game = GameData.GetRecords(game_id);

			DateTime GameStartDateTime = DateTime.Now;
			if (GameStartDateSt != null && GameStartTimeSt != null) 
			{
				string tmpGameStartDateTime =
									GameStartDateSt
									+ " "
									+ GameStartTimeSt;
				GameStartDateTime = Convert.ToDateTime(tmpGameStartDateTime);
			}
			GameData.addRecord(
						game_id: game_id, 
						park_id: park_id, ump_id: ump_id, 
						weather_id: weather_id, 
						start_datetime: GameStartDateTime);
			/// 試合中に修正する際に、現在時点へ戻るためのレコードを作成
			TmpGameData.addRecord(
						game_id: game_id,
						park_id: park_id, ump_id: ump_id,
						weather_id: weather_id,
						start_datetime: GameStartDateTime);
			Frame.Navigate(typeof(GameMatchTeamSelect));
		}

		private void NeWBallParkCreate_Button_Clicked(object sender, RoutedEventArgs e)
		{
			string NewBallPark_name = NewFieldNameCreate.Text;
			string NewBallPark_area = NewFiledAreaName.Text;
			
			BallPark.addRecord(name: NewBallPark_name,area_nm: NewBallPark_area);
			FieldNameSelect.ItemsSource = BallPark.GetRecords();
			ballPark = BallPark.GetRecords();

		}

		private void NewUmpireNameCreate_Button_Clicked(object sender, RoutedEventArgs e)
		{
			string NewUmpireName = NewUmpireNameCreate.Text;
			Umpire.addRecord(name: NewUmpireName);
			UmpireNameSelect.ItemsSource = Umpire.GetRecords();
			umpire = Umpire.GetRecords();
			
		}

		private void NewCreateWeatherName_Button_Clicked(object sender, RoutedEventArgs e)
		{
			string NewWeatherName = NewWeatherNameCreate.Text;
			Weather.addRecord(name: NewWeatherName);
			WeatherNameSelect.ItemsSource = Weather.GetRecords();
			weather = Weather.GetRecords();
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
					//case "Order":
					//	Frame.Navigate(typeof(OrderPage));
					//	//Frame.Navigate(typeof(GameOrders));
					//	break;
					//case "Opt":
					//	Frame.Navigate(typeof(GameOptions));
					//	break;
					//case "Team":
					//	Frame.Navigate(typeof(GameMatchTeamSelect));
					//	break;
			}
		}

		private void FieldNameSelect_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			int index = listView.SelectedIndex;
			park_id = ballPark[index].park_id;
		}

		private void WeatherNameSelect_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			int index = listView.SelectedIndex;
			weather_id = weather[index].weather_id;
		}

		private void UmpireNameSelect_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			int index = listView.SelectedIndex;
			ump_id = umpire[index].ump_id;
		}

		private void GameStartTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
		{
			TimePicker timePicker = (TimePicker)sender;
		}

		private void GameStartTimePicker_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
		{
			GameStartTimeSt = sender.Time.ToString();
		}

		private void GameStartDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
		{
			GameStartDateSt = sender.Date.Value.Date.ToString("yyyy/MM/dd");
		}
	}
}
