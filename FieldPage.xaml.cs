using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
	public sealed partial class FieldPage : Page
	{
		public FieldPage()
		{
			this.InitializeComponent();
			InitializePosition();

			game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);
			game_source = GameData.GetRecords(game_id);     // リセット用

			/// 走塁の登録のため
			run_source = GameData.GetRecords(game_id)[0];
			run_source.player_id = 0;
			run_source.run_1_player_id = 0;
			run_source.run_2_player_id = 0;
			run_source.run_3_player_id = 0;
			run_source.run_1 = false;
			run_source.run_2 = false;
			run_source.run_3 = false;

			/// 守備IDの取得
			def_id = DefensiveData.GetRecordsCount()[0].def_count;

			top_order_id = game[0].top_order_id;
			btm_order_id = game[0].btm_order_id;

			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);


			top_btmFlg = game[0].top_btm_flg;   // 裏表フラグの取得

			count_b = game[0].count_b;
			count_s = game[0].count_s;
			count_o = game[0].count_o;
			initialize_out_count = game[0].count_o;

			FirstRunFlg = game[0].run_1;
			SecondRunFlg = game[0].run_2;
			ThirdRunFlg = game[0].run_3;
			runner_1 = game[0].run_1;
			runner_2 = game[0].run_2;
			runner_3 = game[0].run_3;

			runner_bat_player_id = game[0].player_id;
			runner_1_player_id = game[0].run_1_player_id;
			runner_2_player_id = game[0].run_2_player_id;
			runner_3_player_id = game[0].run_3_player_id;


			ining_box_id = game[0].ining_box_id;
			Ining = game[0].ining;
			run_play_id = game[0].run_play_id;

			var tmp = BoxData.GetRecordsCount();
			box_id = tmp[0].box_count - 1;
			box_template = BoxData.GetRecords(box_id);

			ball_id = BallData.GetCountRecords(game_id)[0].ball_id - 1;

			inPlayBallData = BallData.GetRecordsInPlay(box_id);
			HitTypeComboBox.ItemsSource = HitType.GetRecords();
			HitResultInt = 0;

			if (run_play_id > 0)  // 走塁のみ
			{
				SafeOut_Visible();          // ランナーセーフイメージの表示
				RunnerImgVisible();         // ランナーイメージの表示
			}
			if (!top_btmFlg)
			{
				List<PlayerData.playerData> btm_players = PlayerData.GetRecords(team_id: game[0].field_first_team_id, selected: 0);
				selectedPitcherPlayerId = btm_players.Find(x => x.position == 1).player_id;
			}
			if (top_btmFlg)
			{
				List<PlayerData.playerData> top_players = PlayerData.GetRecords(team_id: game[0].bat_first_team_id, selected: 0);
				selectedPitcherPlayerId = top_players.Find(x => x.position == 1).player_id;
			}
			top_btm_cd = GetTopBtmCode();
			pitchingResult = new PitchingResult(game_id, selectedPitcherPlayerId, top_btm_cd);
			pitchingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			player_id = game[0].player_id;

			order_id = GetOrderId();
			battingResult = new BattingResult(game_id, player_id, order_id, top_btm_cd);
			battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();

			/// ver.1.0.0.4
			CheckRunnerOnBase();

		}
		private int ball_id = 0;
		private int box_id = 0;
		private int game_id = 0;
		List<GameData.gameData> game;
		List<GameData.gameData> game_source;
		GameData.gameData run_source;
		private double zone_x;
		private double zone_y;
		private int zone_x_int;
		private int zone_y_int;
		private int in_play_result = 0;
		private int[] position_th_order;
		private List<int> position_list = new List<int>();
		private Dictionary<int, string> Position_dict = new Dictionary<int, string>();
		private Windows.UI.Xaml.Media.Brush defaultColor_btn;
		private Windows.UI.Xaml.Media.Brush defaultColor_font;
		private bool PositionSelectedFlg = false;
		private bool ResultSelectFlg = false;
		private bool BatterRunFlg = false;
		private bool FirstRunFlg = false;
		private bool SecondRunFlg = false;
		private bool ThirdRunFlg = false;
		private bool runner_1 = false;
		private bool runner_2 = false;
		private bool runner_3 = false;
		private string LastDragItemName = "";
		private Dictionary<string, string> Runner_dict = new Dictionary<string, string>();
		private List<BoxData.boxData> box_template;
		private int HitResultInt = 0;
		private List<BallData.ballData> inPlayBallData;
		private string top_order_id = "1";
		private string btm_order_id = "1";
		private int selectHitTypeIndex = 0;


		private int topScore = 0;
		private int btmScore = 0;

		private int count_b;
		private int count_s;
		private int count_o;
		private int initialize_out_count;
		private int IningScore = 0;
		private int tmpIningScore = 0;
		private string DragImgName = "";
		private int ining_box_id = 1;
		private string Ining = "1";
		private int run_play_id = 0;
		private int def_id = 0;
		private bool field_tapped_flg = false;
		private DefensiveData.defData defData;
		private bool errorFlg = false;
		private bool fielderchiceFlg = false;
		private bool deadFlg = false;
		private bool swingSoFlg = false;
		private bool s_buntFlg = false;
		private bool s_flyFlg = false;

		private int runner_bat_player_id = 0;
		private int runner_1_player_id = 0;
		private int runner_2_player_id = 0;
		private int runner_3_player_id = 0;


		private bool StealFirstFlg = false;
		private bool StealSecondFlg = false;
		private bool StealThirdFlg = false;

		private RunnerData runnerDataBatter;
		private RunnerData runnerDataFirst;
		private RunnerData runnerDataSecond;
		private RunnerData runnerDataThird;
		private List<RunnerData> runnerList = new List<RunnerData>();

		private bool top_btmFlg = false;   // 裏表フラグの取得

		private bool IningChangeFlg = false;

		private int selectedPitcherPlayerId;
		private PitchingResult pitchingResult;

		private int order_id;
		private int top_btm_cd;
		private int player_id;
		private BattingResult battingResult;


		private void EntryRunnerData(int base_cd = 0)
		{
			if (base_cd == 1)
			{
				runnerDataFirst = CreateRunnerInstance(runner_1_player_id);
				runnerList.Add(runnerDataFirst);
				return;
			}
			if (base_cd == 2)
			{
				runnerDataSecond = CreateRunnerInstance(runner_2_player_id);
				runnerList.Add(runnerDataSecond);
				return;
			}
			if (base_cd == 3)
			{
				runnerDataThird = CreateRunnerInstance(runner_3_player_id);
				runnerList.Add(runnerDataThird);
				return;
			}
			runnerDataBatter = CreateRunnerInstance(player_id);
			runnerList.Add(runnerDataBatter);
		}

		private RunnerData CreateRunnerInstance(int runner_id)
		{
			return new RunnerData(
					game_id: game_id,
					top_btm_cd: top_btm_cd,
					ining: Convert.ToInt32(Ining),
					box_id: box_id,
					ball_id: ball_id,
					run_player_id: runner_id
					);
		}

		private void CheckRunnerOnBase()
		{
			if (run_play_id > 0) { return; }
			if (CheckRunnerFirst()) { EntryRunnerData(1); }
			if (CheckRunnerSecond()) { EntryRunnerData(2); }
			if (CheckRunnerThird()) { EntryRunnerData(3); }
			EntryRunnerData(0);
			SetAddRunnersDataInitialize();
		}
		private void SetAddRunnersDataInitialize(int run_code = 1)
		{
			foreach (RunnerData runner in runnerList)
			{
				runner.SetCountBall(count_b);
				runner.SetCountStrike(count_s);
				runner.SetCountOut(count_o);
				runner.SetRunCode(run_code);
			}
		}

		private bool CheckRunnerFirst()
		{
			if (runner_1) { return true; }
			return false;
		}

		private bool CheckRunnerSecond()
		{
			if (runner_2) { return true; }
			return false;
		}

		private bool CheckRunnerThird()
		{
			if (runner_3) { return true; }
			return false;
		}

		private void Field_Image_Tapped(object sender, TappedRoutedEventArgs e)
		{
			if (field_tapped_flg)
			{
				FieldZone.Children.RemoveAt(FieldZone.Children.Count - 1);
				//ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
				field_tapped_flg = false;
				return;
			}

			if (!ResultSelectFlg)  // 打球位置を指定後は反応しないようにする
			{
				Point ptrPT = e.GetPosition(FiledImage);
				zone_x = ptrPT.X;
				zone_y = ptrPT.Y;

				zone_x_int = Convert.ToInt32(zone_x);
				zone_y_int = Convert.ToInt32(zone_y);
				var sl1 = new Windows.UI.Xaml.Shapes.Polygon();
				sl1.Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue);
				var points = new PointCollection();
				points.Add(new Windows.Foundation.Point(0, 0));
				points.Add(new Windows.Foundation.Point(0, 10));
				points.Add(new Windows.Foundation.Point(10, 10));
				points.Add(new Windows.Foundation.Point(10, 0));
				sl1.Points = points;
				sl1.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);

				FieldZone.Children.Add(sl1);

				FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
				field_tapped_flg = true;

			}
		}

		private void InitializePosition()
		{
			Position_dict = new Dictionary<int, string>();
			Runner_dict = new Dictionary<string, string>();

			Position_dict.Add(1, "投");
			Position_dict.Add(2, "捕");
			Position_dict.Add(3, "一");
			Position_dict.Add(4, "二");
			Position_dict.Add(5, "三");
			Position_dict.Add(6, "遊");
			Position_dict.Add(7, "左");
			Position_dict.Add(8, "中");
			Position_dict.Add(9, "右");
			Position_dict.Add(-1, "投E");
			Position_dict.Add(-2, "捕E");
			Position_dict.Add(-3, "一E");
			Position_dict.Add(-4, "二E");
			Position_dict.Add(-5, "三E");
			Position_dict.Add(-6, "遊E");
			Position_dict.Add(-7, "左E");
			Position_dict.Add(-8, "中E");
			Position_dict.Add(-9, "右E");

			Runner_dict.Add("BatterImage", "打者");
			Runner_dict.Add("Runner_1_Image", "一塁走者");
			Runner_dict.Add("Runner_2_Image", "二塁走者");
			Runner_dict.Add("Runner_3_Image", "三塁走者");
		}

		private void InitializeField()
		{

			Runner_btn_Collapsed();
			SafeOut_Collapsed();
			InitializePosition();
			Position_Button_Color_Initialize();
			//box_id = 0;
			//game_id = 0;
			zone_x = 0;
			zone_y = 0;
			zone_x_int = 0;
			zone_y_int = 0;
			in_play_result = 0;
			PositionSelectedFlg = false;
			ResultSelectFlg = false;
			BatterRunFlg = false;
			FirstRunFlg = false;
			SecondRunFlg = false;
			ThirdRunFlg = false;
			runner_1 = false;
			runner_2 = false;
			runner_3 = false;
			LastDragItemName = "";

			box_template = new List<BoxData.boxData>();
			HitResultInt = 0;
			inPlayBallData = new List<BallData.ballData>();
			top_order_id = "1";
			btm_order_id = "1";
			selectHitTypeIndex = 0;

			position_list = new List<int>();

			//game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);
			game = new List<GameData.gameData>();
			game = game_source;
			//game_source = GameData.GetRecords(game_id);     // リセット用

			/// 走塁の登録のため
			run_source = GameData.GetRecords(game_id)[0];
			run_source.player_id = 0;
			run_source.run_1_player_id = 0;
			run_source.run_2_player_id = 0;
			run_source.run_3_player_id = 0;
			run_source.run_1 = false;
			run_source.run_2 = false;
			run_source.run_3 = false;

			/// 守備IDの取得
			def_id = DefensiveData.GetRecordsCount()[0].def_count;

			top_order_id = game[0].top_order_id;
			btm_order_id = game[0].btm_order_id;

			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);

			top_btmFlg = game[0].top_btm_flg;   // 裏表フラグの取得

			count_b = game[0].count_b;
			count_s = game[0].count_s;
			count_o = game[0].count_o;

			FirstRunFlg = game[0].run_1;
			SecondRunFlg = game[0].run_2;
			ThirdRunFlg = game[0].run_3;
			runner_1 = game[0].run_1;
			runner_2 = game[0].run_2;
			runner_3 = game[0].run_3;

			runner_bat_player_id = game[0].player_id;
			runner_1_player_id = game[0].run_1_player_id;
			runner_2_player_id = game[0].run_2_player_id;
			runner_3_player_id = game[0].run_3_player_id;


			ining_box_id = game[0].ining_box_id;
			Ining = game[0].ining;
			run_play_id = game[0].run_play_id;

			var tmp = BoxData.GetRecordsCount();
			box_id = tmp[0].box_count - 1;
			box_template = BoxData.GetRecords(box_id);

			inPlayBallData = BallData.GetRecordsInPlay(box_id);
			HitTypeComboBox.ItemsSource = HitType.GetRecords();
			HitResultInt = 0;

			if (run_play_id > 0)  // 走塁のみ
			{
				SafeOut_Visible();          // ランナーセーフイメージの表示
				RunnerImgVisible();         // ランナーイメージの表示
			}
		}

		private void InPlay_Item(object sender, RoutedEventArgs e)
		{
			var selectFlyOutMenuItem = (MenuFlyoutItem)sender;
			in_play_result = 0;
			switch (selectFlyOutMenuItem.Name)
			{
				case "MenuFlyItemOut":
					in_play_result = 0;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,
									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now);

					BatterRunFlg = true;
					break;
				//case "MenuFlyItemHit":
				case "MenuFlyItemSingle":
					in_play_result = 1;
					HitResultInt = 1;
					Hit_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemTwoBase":
					in_play_result = 1;
					HitResultInt = 2;
					Hit_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemThreeBase":
					in_play_result = 1;
					HitResultInt = 3;
					Hit_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemHomeRun":
					in_play_result = 1;
					HitResultInt = 4;
					Hit_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemError":
					in_play_result = 2;
					errorFlg = true;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									//hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					Error_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					break;
				case "MenuFlyItemFeilder":
					in_play_result = 3;
					fielderchiceFlg = true;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									//hit_id: HitResultInt,
									res_hit_type: selectHitTypeIndex,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					Feilder_lamp.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					break;
				case "MenuFlyItemStrikeRun":
					in_play_result = 5;
					swingSoFlg = true;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									res_hit_type: selectHitTypeIndex,
									//swing_so: true,     // 振り逃げは空振りとしている

									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemS_bunt":
					in_play_result = 6;
					s_buntFlg = true;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									res_hit_type: selectHitTypeIndex,
									//s_bunt: true,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;
				case "MenuFlyItemS_fly":
					in_play_result = 7;
					s_flyFlg = true;
					BoxData.updateRecord(
									box_id: box_id,
									res_course_x: zone_x_int,
									res_course_y: zone_y_int,
									res_hit: in_play_result,
									res_hit_type: selectHitTypeIndex,
									//s_fly: true,
									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,

									top_bot: top_btmFlg,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					BatterRunFlg = true;
					break;

				case "MenuFlyItemCancel":
					in_play_result = -1;
					FieldZone.Children.RemoveAt(FieldZone.Children.Count - 1);
					break;
				default:
					break;
			}
			/// キャンセル以外の結果の処理
			if (in_play_result >= 0)
			{
				Position_Visible();
				ResultSelectFlg = true;
			}
		}

		private void Position_Visible()
		{
			投手_btn.Visibility = Visibility.Visible;
			捕手_btn.Visibility = Visibility.Visible;
			一塁手_btn.Visibility = Visibility.Visible;
			二塁手_btn.Visibility = Visibility.Visible;
			三塁手_btn.Visibility = Visibility.Visible;
			遊撃手_btn.Visibility = Visibility.Visible;
			左翼手_btn.Visibility = Visibility.Visible;
			中堅手_btn.Visibility = Visibility.Visible;
			右翼手_btn.Visibility = Visibility.Visible;
		}

		private void Position_Collapsed()
		{
			投手_btn.Visibility = Visibility.Collapsed;
			捕手_btn.Visibility = Visibility.Collapsed;
			一塁手_btn.Visibility = Visibility.Collapsed;
			二塁手_btn.Visibility = Visibility.Collapsed;
			三塁手_btn.Visibility = Visibility.Collapsed;
			遊撃手_btn.Visibility = Visibility.Collapsed;
			左翼手_btn.Visibility = Visibility.Collapsed;
			中堅手_btn.Visibility = Visibility.Collapsed;
			右翼手_btn.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// ポジションボタンの配色を初期状態へ戻す
		/// </summary>
		private void Position_Button_Color_Initialize()
		{
			投手_btn.Background = defaultColor_btn;
			捕手_btn.Background = defaultColor_btn;
			一塁手_btn.Background = defaultColor_btn;
			二塁手_btn.Background = defaultColor_btn;
			三塁手_btn.Background = defaultColor_btn;
			遊撃手_btn.Background = defaultColor_btn;
			左翼手_btn.Background = defaultColor_btn;
			中堅手_btn.Background = defaultColor_btn;
			右翼手_btn.Background = defaultColor_btn;
			投手_btn.Foreground = defaultColor_font;
			捕手_btn.Foreground = defaultColor_font;
			一塁手_btn.Foreground = defaultColor_font;
			二塁手_btn.Foreground = defaultColor_font;
			三塁手_btn.Foreground = defaultColor_font;
			遊撃手_btn.Foreground = defaultColor_font;
			左翼手_btn.Foreground = defaultColor_font;
			中堅手_btn.Foreground = defaultColor_font;
			右翼手_btn.Foreground = defaultColor_font;
		}


		private async void Position_Display(object sender, RoutedEventArgs e)
		{
			string tmp_throwsOrder = "";
			int tmp_count = 0;
			foreach (int i in position_list)
			{
				tmp_count++;
				//int tmp_i = Convert.ToInt32(Math.Abs(i));
				tmp_throwsOrder += "(" + tmp_count.ToString() + ")" + Position_dict[i].ToString();
				if (position_list.Count != tmp_count) { tmp_throwsOrder += " => "; }
			}
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "守備機会を登録しますか";
			dialog.Content = tmp_throwsOrder;
			dialog.PrimaryButtonText = "登録";
			dialog.SecondaryButtonText = "再選択";
			dialog.CloseButtonText = "続ける";
			dialog.DefaultButton = ContentDialogButton.Primary;
			//dialog.Content = new ContentDialogContent();
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				/// 守備の登録
				List<PlayerData.playerData> def_players;
				int def_inc = 1;
				int def_team_id = 0;
				int position_player_id = 0;
				if (!top_btmFlg)
				{
					def_team_id = game[0].field_first_team_id;
					def_players = PlayerData.GetRecords(team_id: game[0].field_first_team_id);
				}
				else
				{
					def_team_id = game[0].bat_first_team_id;
					def_players = PlayerData.GetRecords(team_id: game[0].bat_first_team_id);
				}

				int error_id = 0;  // 0:None, 1:Error

				foreach (int i in position_list)
				{
					int tmp_i = Convert.ToInt32(Math.Abs(i));
					if (i < 0)
					{
						error_id = 1;
					}
					else
					{
						error_id = 0;
					}

					int position_id = -1;
					if (int.TryParse(Position_dict[tmp_i], out position_id))
					{
						position_player_id = PlayerData.GetRecords(
															team_id: def_team_id,
															position: position_id
															)[0].player_id;
					}

					/// ver.1.0.9.0以降
					if (def_inc == 1) // 最初に指定されたポジション
					{
						BoxData.updateRecord(
									box_id: box_id,

									runner_1: runner_1,
									runner_2: runner_2,
									runner_3: runner_3,
									runner_1_player_id: runner_1_player_id,
									runner_2_player_id: runner_2_player_id,
									runner_3_player_id: runner_3_player_id,
									top_bot: top_btmFlg,
									res_position: tmp_i,
									error: errorFlg,
									fielder_choice: fielderchiceFlg,
									dead: deadFlg,
									swing_so: swingSoFlg,
									s_bunt: s_buntFlg,
									s_fly: s_flyFlg,
									update_date: DateTime.Now
									);
					}
					DefensiveData.addRecord(
										game_id: game_id,
										def_id: def_id,
										team_id: def_team_id,
										box_id: box_id,
										position_id: tmp_i,
										runner_1: run_source.run_1,
										runner_2: run_source.run_2,
										runner_3: run_source.run_3,
										runner_1_player_id: run_source.run_1_player_id,
										runner_2_player_id: run_source.run_2_player_id,
										runner_3_player_id: run_source.run_3_player_id,
										ining: Convert.ToInt32(game[0].ining),
										top_bot: top_btmFlg,
										count_o: game[0].count_o,
										position_player_id: position_player_id,
										etc_cd1: def_inc,
										etc_cd2: error_id,
										update_date: DateTime.Now
										);
					def_id++;
					def_inc++;
				}

				if (RunnerFlgCheck_ALL())
				{
					Position_Collapsed();       // ポジションボタンを隠す
					SafeOut_Visible();          // ランナーセーフイメージの表示
					RunnerImgVisible();         // ランナーイメージの表示
				}
				else
				{
					ResultRegistory();      // 凡打時の登録処理
				}

			}
			else if (result == ContentDialogResult.Secondary)
			{
				position_list.Clear();
				tmp_throwsOrder = "";
				Position_Button_Color_Initialize();
			}
		}

		private bool RunnerFlgCheck_ALL()
		{
			if (!BatterRunFlg && !FirstRunFlg && !SecondRunFlg && !ThirdRunFlg)
			{
				return false;
			}
			return true;
		}


		private async void ResultRegistory()
		{
			ContentDialog dialogResult = new ContentDialog();
			dialogResult.Title = "走塁結果の登録";
			dialogResult.Content = "打席結果の登録を完了しますか";
			dialogResult.PrimaryButtonText = "登録";
			dialogResult.SecondaryButtonText = "再選択";
			//dialogResult.CloseButtonText = "閉じる";
			dialogResult.DefaultButton = ContentDialogButton.Primary;
			//dialog.Content = new ContentDialogContent();
			var ret_dialogResult = await dialogResult.ShowAsync();
			if (ret_dialogResult == ContentDialogResult.Primary)
			{
				bool tmpFlg;
				int tmp_order_idInt;
				string tmp_order_id;

				if (!top_btmFlg)
				{
					tmpFlg = false;
					tmp_order_idInt = Convert.ToInt32(top_order_id) + 1;
					if (tmp_order_idInt > 9) { tmp_order_idInt = 1; }
					tmp_order_id = Convert.ToString(tmp_order_idInt);
					GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: tmpFlg,
						top_order_id: tmp_order_id,
						run_1: run_source.run_1,
						run_2: run_source.run_2,
						run_3: run_source.run_3,
						run_1_player_id: run_source.run_1_player_id,
						run_2_player_id: run_source.run_2_player_id,
						run_3_player_id: run_source.run_3_player_id,
						ining_box_id: ining_box_id + 1
						);
				}
				else
				{
					tmpFlg = true;
					tmp_order_idInt = Convert.ToInt32(btm_order_id) + 1;
					if (tmp_order_idInt > 9) { tmp_order_idInt = 1; }
					tmp_order_id = Convert.ToString(tmp_order_idInt);
					GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: tmpFlg,
						btm_order_id: tmp_order_id,
						run_1: run_source.run_1,
						run_2: run_source.run_2,
						run_3: run_source.run_3,
						run_1_player_id: run_source.run_1_player_id,
						run_2_player_id: run_source.run_2_player_id,
						run_3_player_id: run_source.run_3_player_id,
						ining_box_id: ining_box_id + 1
						);
				}

				/// ver.1.0.0.4
				EntryRunnerDataRegistory();


				/// 登録完了後、投球画面へ遷移
				Frame.Navigate(typeof(ScorePage));
			}
			else if (ret_dialogResult == ContentDialogResult.Secondary)
			{
				InitializeField();
			}
		}

		private void EntryRunnerDataRegistory() 
		{
			if(runnerList.Count == 0) { return; }
			foreach(RunnerData runner in runnerList) 
			{
				runner.addRecord();
			}
		}

		/// <summary>
		/// ランナーフラグにより走塁選択時に表示を行う
		/// </summary>
		private void RunnerImgVisible()
		{
			if (BatterRunFlg)
			{
				BatterImage.Visibility = Visibility.Visible;
			}
			else
			{
				BatterImage.Visibility = Visibility.Collapsed;
			}

			if (FirstRunFlg)
			{
				Runner_1_Image.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_1_Image.Visibility = Visibility.Collapsed;
			}

			if (SecondRunFlg)
			{
				Runner_2_Image.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_2_Image.Visibility = Visibility.Collapsed;
			}

			if (ThirdRunFlg)
			{
				Runner_3_Image.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_3_Image.Visibility = Visibility.Collapsed;
			}
		}

		/// <summary>
		/// 初期化時に用いる
		/// </summary>
		private void Runner_btn_Collapsed()
		{

			BatterImage.Visibility = Visibility.Collapsed;

			Runner_1_Image.Visibility = Visibility.Collapsed;

			Runner_2_Image.Visibility = Visibility.Collapsed;

			Runner_3_Image.Visibility = Visibility.Collapsed;
		}

		private void SafeOut_Visible()
		{
			HomeSafeImage.Visibility = Visibility.Visible;
			HomeOutImage.Visibility = Visibility.Visible;

			Runner_1_SafeImage.Visibility = Visibility.Visible;
			Runner_1_OutImage.Visibility = Visibility.Visible;

			Runner_2_SafeImage.Visibility = Visibility.Visible;
			Runner_2_OutImage.Visibility = Visibility.Visible;

			Runner_3_SafeImage.Visibility = Visibility.Visible;
			Runner_3_OutImage.Visibility = Visibility.Visible;
		}

		private void SafeOut_Collapsed()
		{
			HomeSafeImage.Visibility = Visibility.Collapsed;
			HomeOutImage.Visibility = Visibility.Collapsed;

			Runner_1_SafeImage.Visibility = Visibility.Collapsed;
			Runner_1_OutImage.Visibility = Visibility.Collapsed;

			Runner_2_SafeImage.Visibility = Visibility.Collapsed;
			Runner_2_OutImage.Visibility = Visibility.Collapsed;

			Runner_3_SafeImage.Visibility = Visibility.Collapsed;
			Runner_3_OutImage.Visibility = Visibility.Collapsed;
		}

		private void Runner_DragLeave(object sender, DragEventArgs e)
		{
			//Image img = (Image)sender;
			////string imgName = img.Name.ToString();
			//LastDragItemName = img.Name.ToString();
		}

		private void Runner_DragOver(object sender, DragEventArgs e)
		{
			Image image = (Image)sender;
			DragImgName = image.Name;
			e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
			e.Handled = true;
		}

		private void ScoreRecord(string get_score = "")
		{
			if (get_score.Length == 0) { return; }
			bool ScoreRecordTBFlg = game[0].top_btm_flg;
			string ScoreRecordIningSt = game[0].ining;
			string SocreRecordTarget;
			if (ScoreRecordTBFlg)
			{
				SocreRecordTarget = "btm_" + ScoreRecordIningSt;
			}
			else
			{
				SocreRecordTarget = "top_" + ScoreRecordIningSt;
			}
			int get_scoreInt;
			if (!int.TryParse(get_score, out get_scoreInt))
			{
				return;
			}

			GameData.updateScoreRecord(
								game_id: game_id,
								target: SocreRecordTarget,
								get_score: get_score);

			/// ver 1.1.2.0 打席データへ得点を追加する
			BoxData.updateRecordIningScore(
							game_id: game_id,
							box_id: box_id,
							ining: Convert.ToInt32(ScoreRecordIningSt),
							topbtmFlg: ScoreRecordTBFlg,
							//ining_score: get_scoreInt,
							ining_score: IningScore,
							update_date: DateTime.Now
							);

		}

		private void ScoreReSetRecord(string get_score = "")
		{
			if (get_score.Length == 0) { return; }
			bool ScoreRecordTBFlg = game[0].top_btm_flg;
			string ScoreRecordIningSt = game[0].ining;
			string SocreRecordTarget;
			if (ScoreRecordTBFlg)
			{
				SocreRecordTarget = "btm_" + ScoreRecordIningSt;
			}
			else
			{
				SocreRecordTarget = "top_" + ScoreRecordIningSt;
			}
			int get_scoreInt;
			if (!int.TryParse(get_score, out get_scoreInt))
			{
				return;
			}

			GameData.updateScoreRecordInitialize(
								game_id: game_id,
								target: SocreRecordTarget,
								get_score: get_score);

			/// ver 1.1.2.0 打席データへ得点を追加する
			BoxData.updateRecordIningScore(
							game_id: game_id,
							box_id: box_id,
							ining: Convert.ToInt32(ScoreRecordIningSt),
							topbtmFlg: ScoreRecordTBFlg,
							//ining_score: get_scoreInt,
							ining_score: IningScore,
							update_date: DateTime.Now
							);

		}


		private void IningScoreUpdate(
							bool top_btm_flg = false,
							string ining = "1")
		{
			if (top_btm_flg)
			{
				btmIningScoreUpdate(ining: ining);
			}
			else
			{
				topIningScoreUpdate(ining: ining);
			}
		}

		private void topIningScoreUpdate(string ining = "1")
		{

			if (!IningChangeFlg)
			{
				ScoreRecord(tmpIningScore.ToString());  // 計スコアを記帳する
			}
			else
			{
				if (IningScore == 0)
				{
					ScoreRecord(IningScore.ToString());  // 計スコアを記帳する
				}
			}
			//GameData.gameData getGameScore = GameData.GetRecords(game_id)[0];

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						//run_1: runner_1,
						//run_2: runner_2,
						//run_3: runner_3
						run_1: run_source.run_1,
						run_2: run_source.run_2,
						run_3: run_source.run_3,
						run_1_player_id: run_source.run_1_player_id,
						run_2_player_id: run_source.run_2_player_id,
						run_3_player_id: run_source.run_3_player_id
						);

		}

		private void btmIningScoreUpdate(string ining = "1")
		{
			//ScoreRecord(tmpIningScore.ToString());

			//if (IningScore == 0)
			//{
			//	ScoreRecord(IningScore.ToString());  // 計スコアを記帳する
			//}

			//ScoreRecord(IningScore.ToString());  // 計スコアを記帳する

			if (!IningChangeFlg)
			{
				ScoreRecord(tmpIningScore.ToString());  // 計スコアを記帳する
			}
			else
			{
				if (IningScore == 0)
				{
					ScoreRecord(IningScore.ToString());  // 計スコアを記帳する
				}
			}



			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						//run_1: runner_1,
						//run_2: runner_2,
						//run_3: runner_3
						run_1: run_source.run_1,
						run_2: run_source.run_2,
						run_3: run_source.run_3,
						run_1_player_id: run_source.run_1_player_id,
						run_2_player_id: run_source.run_2_player_id,
						run_3_player_id: run_source.run_3_player_id
						);
		}




		private void Runner_Check(
							string imgName,
							string baseName)
		{
			bool flg = false;
			if (baseName.Contains("Safe"))
			{
				flg = true;
			}

			/// セーフかアウトで処理を変更する
			if (flg) // セーフ
			{
				Runner_Check_Safe(imgName: imgName, baseName: baseName);
			}
			else // アウト
			{
				Runner_Check_Out(imgName: imgName, baseName: baseName);
			}
		}




		/// <summary>
		///  進塁したときの処理
		///  走塁結果登録時にrun_sourceリストの各データを書き換える
		/// </summary>
		/// <param name="imgName"></param>
		/// <param name="baseName"></param>
		private void Runner_Check_Safe(
							string imgName,
							string baseName)
		{
			int batter_id = runner_bat_player_id;
			int run_1_id = runner_1_player_id;
			int run_2_id = runner_2_player_id;
			int run_3_id = runner_3_player_id;

			string tmpIning = game_source[0].ining;
			bool tmpTopBtmFlg = game_source[0].top_btm_flg;
			switch (imgName)
			{
				case "BatterImage":
					switch (baseName)
					{
						case "Runner_1_SafeImage":
							run_source.run_1_player_id = batter_id;
							run_source.run_1 = true;

							break;
						case "Runner_2_SafeImage":
							run_source.run_2_player_id = batter_id;
							run_source.run_2 = true;

							break;
						case "Runner_3_SafeImage":
							run_source.run_3_player_id = batter_id;
							run_source.run_3 = true;
							break;
						case "HomeSafeImage":

							/// ver.1.1.1.0以降
							/// FLG:trueのみ得点を追加
							if (BatterRunFlg)
							{
								tmpIningScore = 1;
								IningScore += tmpIningScore;
								IningScoreUpdate(
											top_btm_flg: tmpTopBtmFlg,
											ining: tmpIning
											);
							}
							break;
					}
					break;
				case "Runner_1_Image":
					switch (baseName)
					{
						case "Runner_1_SafeImage":
							run_source.run_1_player_id = run_1_id;
							run_source.run_1 = true;
							break;
						case "Runner_2_SafeImage":
							run_source.run_2_player_id = run_1_id;
							run_source.run_2 = true;
							run_source.run_1 = false;
							if (StealFirstFlg)
							{
								/// 一塁ランナーの盗塁処理
								RunEntryRtn(base_id: 1, run_player_id: run_1_id, success: 0, inplay_id: run_play_id);
							}
							break;
						case "Runner_3_SafeImage":
							run_source.run_3_player_id = run_1_id;
							run_source.run_3 = true;
							run_source.run_1 = false;
							if (StealFirstFlg)
							{
								/// 一塁ランナーの盗塁処理
								RunEntryRtn(base_id: 1, run_player_id: run_1_id, success: 0, inplay_id: run_play_id);
							}
							break;
						case "HomeSafeImage":
							if (FirstRunFlg)
							{
								tmpIningScore = 1;
								IningScore += tmpIningScore;
								IningScoreUpdate(
											top_btm_flg: tmpTopBtmFlg,
											ining: tmpIning
											);
								if (StealFirstFlg)
								{
									/// 一塁ランナーの盗塁処理
									RunEntryRtn(base_id: 1, run_player_id: run_1_id, success: 0, inplay_id: run_play_id);
								}
							}
							break;
					}
					break;
				case "Runner_2_Image":
					switch (baseName)
					{
						case "Runner_1_SafeImage":
							run_source.run_1_player_id = run_2_id;
							run_source.run_1 = true;
							run_source.run_2 = false;
							break;
						case "Runner_2_SafeImage":
							run_source.run_2_player_id = run_2_id;
							run_source.run_2 = true;
							break;
						case "Runner_3_SafeImage":
							run_source.run_3_player_id = run_2_id;
							run_source.run_3 = true;
							run_source.run_2 = false;
							if (StealSecondFlg)
							{
								/// 二塁ランナーの盗塁処理
								RunEntryRtn(base_id: 2, run_player_id: run_2_id, success: 0, inplay_id: run_play_id);
							}
							break;
						case "HomeSafeImage":
							if (SecondRunFlg)
							{
								tmpIningScore = 1;
								IningScore += tmpIningScore;
								IningScoreUpdate(
											top_btm_flg: tmpTopBtmFlg,
											ining: tmpIning
											);
								if (StealSecondFlg)
								{
									/// 二塁ランナーの盗塁処理
									RunEntryRtn(base_id: 2, run_player_id: run_2_id, success: 0, inplay_id: run_play_id);
								}
							}
							break;
					}
					break;

				case "Runner_3_Image":
					switch (baseName)
					{
						case "Runner_1_SafeImage":
							run_source.run_1_player_id = run_3_id;
							run_source.run_1 = true;
							run_source.run_3 = false;
							break;
						case "Runner_2_SafeImage":
							run_source.run_2_player_id = run_3_id;
							run_source.run_2 = true;
							run_source.run_3 = false;
							break;
						case "Runner_3_SafeImage":
							run_source.run_3_player_id = run_3_id;
							run_source.run_3 = true;
							break;
						case "HomeSafeImage":
							if (ThirdRunFlg)
							{
								tmpIningScore = 1;
								IningScore += tmpIningScore;
								IningScoreUpdate(
											top_btm_flg: tmpTopBtmFlg,
											ining: tmpIning
											);
								if (StealThirdFlg)
								{
									/// 三塁ランナーの盗塁処理
									RunEntryRtn(base_id: 3, run_player_id: run_3_id, success: 0, inplay_id: run_play_id);
								}
							}
							break;
					}
					break;
				case "本塁":
					break;
			}
		}
		private void Runner_Check_Out(
							string imgName,
							string baseName)
		{
			switch (imgName)
			{
				case "BatterImage":
					run_source.count_o = run_source.count_o + 1;
					break;
				case "Runner_1_Image":
					run_source.run_1_player_id = 0;
					run_source.run_1 = false;
					run_source.count_o = run_source.count_o + 1;
					break;
				case "Runner_2_Image":
					run_source.run_2_player_id = 0;
					run_source.run_2 = false;
					run_source.count_o = run_source.count_o + 1;
					break;
				case "Runner_3_Image":
					run_source.run_3_player_id = 0;
					run_source.run_3 = false;
					run_source.count_o = run_source.count_o + 1;
					break;
				default:
					break;

			}
			if (StealFirstFlg || StealSecondFlg || StealThirdFlg)
			{
				StealFalseRtn(imgName: imgName, baseName: baseName);
			}

			if (run_source.count_o >= 3)
			{
				/// 走塁結果登録処理 & 攻守交代処理
				BatterRunFlg = false;
				FirstRunFlg = false;
				SecondRunFlg = false;
				ThirdRunFlg = false;
			}
		}

		private void StealFalseRtn(string imgName, string baseName)
		{
			switch (imgName)
			{
				case "Runner_1_Image":
					RunEntryRtn(base_id: 1, run_player_id: runner_1_player_id, success: 1, inplay_id: run_play_id);
					break;
				case "Runner_2_Image":
					RunEntryRtn(base_id: 2, run_player_id: runner_2_player_id, success: 1, inplay_id: run_play_id);
					break;
				case "Runner_3_Image":
					RunEntryRtn(base_id: 3, run_player_id: runner_3_player_id, success: 1, inplay_id: run_play_id);
					break;
			}

		}

		/// <summary>
		/// 打順を次へ遷移する
		/// </summary>
		private string BoxNextChange(bool flg = false)
		{
			string tmp_order_id;
			int tmp_order_idInt;

			if (!top_btmFlg)
			{
				if (flg) { return top_order_id; }
				tmp_order_idInt = Convert.ToInt32(top_order_id) + 1;
				if (tmp_order_idInt > 9)
				{
					tmp_order_idInt = 1;
				}
				tmp_order_id = Convert.ToString(tmp_order_idInt);
				ining_box_id += 1;
			}
			else
			{
				if (flg) { return btm_order_id; }
				tmp_order_idInt = Convert.ToInt32(btm_order_id) + 1;
				if (tmp_order_idInt > 9)
				{
					tmp_order_idInt = 1;
				}
				tmp_order_id = Convert.ToString(tmp_order_idInt);
				ining_box_id += 1;
			}
			return tmp_order_id;
		}


		private async void Runner_DragDrop(object sender, DragEventArgs e)
		{
			Image img = (Image)sender;
			string tmpName = img.Name.ToString();
			string DragName;
			bool tmpBat = BatterRunFlg;
			bool tmpRun_1 = runner_1;
			bool tmpRun_2 = runner_2;
			bool tmpRun_3 = runner_3;
			int tmp_run_1_player_id = run_source.run_1_player_id;
			int tmp_run_2_player_id = run_source.run_2_player_id;
			int tmp_run_3_player_id = run_source.run_3_player_id;
			bool resultRun_1;
			bool resultRun_2;
			bool resultRun_3;
			if (tmpName.Contains("1"))
			{
				DragName = "一塁への走塁\n";
			}
			else if (tmpName.Contains("2"))
			{
				DragName = "二塁への走塁\n";
			}
			else if (tmpName.Contains("3"))
			{
				DragName = "三塁への走塁\n";
			}
			else
			{
				DragName = "本塁への走塁\n";
			}
			if (tmpName.Contains("Safe"))
			{
				switch (LastDragItemName)
				{
					case "BatterImage":
						break;
					case "Runner_1_Image":
						runner_1 = true;
						break;
					case "Runner_2_Image":
						runner_2 = true;
						break;
					case "Runner_3_Image":
						runner_3 = true;
						break;
				}
				DragName += Runner_dict[LastDragItemName] + "の走塁結果を登録(セーフ)";
			}
			else
			{
				DragName += Runner_dict[LastDragItemName] + "の走塁結果を登録(アウト)";
			}
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "走塁を登録しますか";
			dialog.Content = DragName;
			dialog.PrimaryButtonText = "登録";
			//dialog.SecondaryButtonText = "再選択";
			dialog.CloseButtonText = "再選択";
			dialog.DefaultButton = ContentDialogButton.Primary;
			//dialog.Content = new ContentDialogContent();
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				Runner_Check(imgName: LastDragItemName, baseName: tmpName);
				switch (LastDragItemName)
				{
					case "BatterImage":
						BatterRunFlg = false;
						break;
					case "Runner_1_Image":
						FirstRunFlg = false;

						break;
					case "Runner_2_Image":
						SecondRunFlg = false;

						break;
					case "Runner_3_Image":
						ThirdRunFlg = false;

						break;
				}

				if (RunnerFlGCheck())
				{
					ContentDialog dialogResult = new ContentDialog();
					dialogResult.Title = "走塁結果の登録";
					dialogResult.Content = "打席結果の登録を完了しますか";
					dialogResult.PrimaryButtonText = "登録";
					dialogResult.SecondaryButtonText = "再選択";
					//dialogResult.CloseButtonText = "閉じる";
					dialogResult.DefaultButton = ContentDialogButton.Primary;
					//dialog.Content = new ContentDialogContent();
					var ret_dialogResult = await dialogResult.ShowAsync();
					if (ret_dialogResult == ContentDialogResult.Primary)
					{
						bool tmpFlg;
						int tmp_order_idInt;
						string tmp_order_id;
						if (!top_btmFlg)
						{
							if (run_source.count_o >= 3)
							{
								Ining_Change();
							}
							else
							{
								tmpFlg = false;
								if (run_play_id > 0)
								{
									tmp_order_idInt = Convert.ToInt32(top_order_id);
									tmp_order_id = Convert.ToString(tmp_order_idInt);
								}
								else
								{
									tmp_order_idInt = Convert.ToInt32(top_order_id) + 1;
									if (tmp_order_idInt > 9) { tmp_order_idInt = 1; }
									tmp_order_id = Convert.ToString(tmp_order_idInt);
									ining_box_id += 1;
								}
								if (run_play_id == 0)  // 打席結果の場合
								{
									GameData.updateRecord(
											game_id: game_id,
											top_btm_flg: tmpFlg,
											top_order_id: tmp_order_id,
											count_o: run_source.count_o,
											run_1: run_source.run_1,
											run_2: run_source.run_2,
											run_3: run_source.run_3,
											run_1_player_id: run_source.run_1_player_id,
											run_2_player_id: run_source.run_2_player_id,
											run_3_player_id: run_source.run_3_player_id,
											ining_box_id: ining_box_id,
											count_b: 0,
											count_s: 0
											);
								}
								//else if (run_play_id == 1)  // ScorePage 走塁ボタンを押下
								else
								{
									GameData.updateRecord(
											game_id: game_id,
											top_btm_flg: tmpFlg,
											top_order_id: tmp_order_id,
											count_o: run_source.count_o,
											run_1: run_source.run_1,
											run_2: run_source.run_2,
											run_3: run_source.run_3,
											run_1_player_id: run_source.run_1_player_id,
											run_2_player_id: run_source.run_2_player_id,
											run_3_player_id: run_source.run_3_player_id,
											ining_box_id: ining_box_id
											);
								}
								//else if (run_play_id == 2)  // 盗塁アクション 
								//{ }

							}
						}
						else
						{
							if (run_source.count_o >= 3)
							{
								Ining_Change();
							}
							else
							{

								tmpFlg = true;
								if (run_play_id > 0)
								{
									tmp_order_idInt = Convert.ToInt32(btm_order_id);
									tmp_order_id = Convert.ToString(tmp_order_idInt);
								}
								else
								{
									tmp_order_idInt = Convert.ToInt32(btm_order_id) + 1;
									if (tmp_order_idInt > 9)
									{
										tmp_order_idInt = 1;
									}
									tmp_order_id = Convert.ToString(tmp_order_idInt);
									ining_box_id += 1;
								}
								if (run_play_id == 0)  // 打席結果の場合
								{
									GameData.updateRecord(
												game_id: game_id,
												top_btm_flg: tmpFlg,
												btm_order_id: tmp_order_id,
												count_o: run_source.count_o,
												run_1: run_source.run_1,
												run_2: run_source.run_2,
												run_3: run_source.run_3,
												//run_1: runner_1,
												//run_2: runner_2,
												//run_3: runner_3,
												run_1_player_id: run_source.run_1_player_id,
												run_2_player_id: run_source.run_2_player_id,
												run_3_player_id: run_source.run_3_player_id,
												ining_box_id: ining_box_id,
												count_b: 0,
												count_s: 0
												);
								}
								else // 走塁のみ
								{
									GameData.updateRecord(
												game_id: game_id,
												top_btm_flg: tmpFlg,
												btm_order_id: tmp_order_id,
												count_o: run_source.count_o,
												run_1: run_source.run_1,
												run_2: run_source.run_2,
												run_3: run_source.run_3,
												//run_1: runner_1,
												//run_2: runner_2,
												//run_3: runner_3,
												run_1_player_id: run_source.run_1_player_id,
												run_2_player_id: run_source.run_2_player_id,
												run_3_player_id: run_source.run_3_player_id,
												ining_box_id: ining_box_id
												);
								}

							}
						}

						battingResult.UpBoxCount();
						battingResult.UpBatCount();
						if (HitResultInt > 0)
						{
							pitchingResult.UpHitCount();
							battingResult.UpHitCount();
						}
						if (HitResultInt == 4)
						{
							pitchingResult.UpHoumeRunCount();
							battingResult.UpHoumeRunCount();
						}

						if (in_play_result == 6)
						{
							battingResult.DownBatCount();
							battingResult.UpSacrificeCount();
						}
						if (in_play_result == 7)
						{
							battingResult.DownBatCount();
							battingResult.UpSacrificeCount();
						}

						int out_add_count = run_source.count_o - initialize_out_count;
						for (int i = 0; i < out_add_count; i++)
						{
							pitchingResult.UpOutCount();
						}
						for (int j = 0; j < IningScore; j++)
						{
							pitchingResult.UpLostRunsCount();
							pitchingResult.UpEarnedRunsCount();
							if (in_play_result == 2) { continue; }
							battingResult.UpRunBattedInCount();
						}

						pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();
						battingResult.BattingDataの登録atデータ有無による新規OR追記処理();


						/// ver.1.0.0.4
						EntryRunnerDataRegistory();

						/// 登録完了後、投球画面へ遷移
						Frame.Navigate(typeof(ScorePage));
					}
					else if (ret_dialogResult == ContentDialogResult.Secondary)
					{
						//GameData.updateScoreRecordInitialize(game_id:game_id,);
						ScoreReSetRecord(IningScore.ToString());
						InitializeField();
						//BatterRunFlg = tmpBat;
						//runner_1 = tmpRun_1;
						//runner_2 = tmpRun_2;
						//runner_3 = tmpRun_3;
					}
				}
				else { }

			}
			//else if (result == ContentDialogResult.Secondary)
			//{
			//	Runner_btn_Collapsed();
			//}
			else if (result == ContentDialogResult.None)
			{

			}
		}

		private void Ining_Change()
		{
			count_b = 0;
			count_s = 0;
			count_o = 0;
			runner_1 = false;
			runner_2 = false;
			runner_3 = false;
			IningScore = 0;

			IningChangeFlg = true;

			string tmp_order_id;
			if (run_play_id != 0)  // 打撃結果を伴わない場合は打順をそのままにする
			{
				tmp_order_id = BoxNextChange(flg: true);

			}
			else
			{
				tmp_order_id = BoxNextChange();  // 打順を次へ遷移する									 
			}
			if (top_btmFlg)
			{
				IningScoreUpdate(top_btm_flg: top_btmFlg, ining: Ining);    // イニング変更前に得点を記載する
																			// 次のイニングに行く前にゲームセットの確認をする
				GameSetCheck(game_id: game_id);

				top_btmFlg = false;
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						ining: Convert.ToString(Convert.ToInt32(Ining) + 1),
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: 0,
						run_2_player_id: 0,
						run_3_player_id: 0,
						run_play_id: 0  // スリーアウト時は走塁フラグも戻す
						, btm_order_id: tmp_order_id
						//, ining_box_id: ining_box_id
						);
			}
			else
			{
				IningScoreUpdate(top_btm_flg: top_btmFlg, ining: Ining);    // イニング変更前に得点を記載する
				GameSetCheck(game_id: game_id);

				top_btmFlg = true;
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: 0,
						run_2_player_id: 0,
						run_3_player_id: 0,
						run_play_id: 0,  // スリーアウト時は走塁フラグも戻す,
						top_order_id: tmp_order_id
						//, ining_box_id: ining_box_id
						);
			}
		}


		/// <summary>
		/// すべての走者処理を完遂したかチェックする
		/// </summary>
		/// <returns></returns>
		private bool RunnerFlGCheck()
		{
			if (BatterRunFlg) { return false; }
			if (FirstRunFlg) { return false; }
			if (SecondRunFlg) { return false; }
			if (ThirdRunFlg) { return false; }
			return true;
		}

		private int Runner_Count()
		{
			int count = 0;
			if (BatterRunFlg) { count++; }
			if (FirstRunFlg) { count++; }
			if (SecondRunFlg) { count++; }
			if (ThirdRunFlg) { count++; }
			return count;
		}

		private void Runner_DragStarting(UIElement sender, DragStartingEventArgs args)
		{
			Image img = (Image)sender;
			LastDragItemName = img.Name.ToString();
		}

		private void Runner_Result_Loop()
		{
			for (int i = 0; i < Runner_Count(); i++)
			{

			}
		}
		private void GameSetCheck(int game_id = 0)
		{
			GameData.gameData gameSet = GameData.GetRecords(game_id)[0];
			if (Convert.ToInt32(gameSet.ining) >= 9
				&& Convert.ToInt32(gameSet.ining) < 12)
			{
				if (gameSet.top_9.Length != 0
					&& gameSet.btm_9.Length == 0)
				{
					if (Convert.ToInt32(gameSet.top_total_score)
						< Convert.ToInt32(gameSet.btm_total_score))
					{
						GameSetRtn();
					}
				}
				else if (Convert.ToInt32(gameSet.top_total_score)
							!= Convert.ToInt32(gameSet.btm_total_score))
				{
					GameSetRtn();
				}
			}
			else if (gameSet.top_12.Length != 0
					&& gameSet.btm_12.Length != 0)
			{
				if (Convert.ToInt32(gameSet.top_total_score)
							== Convert.ToInt32(gameSet.btm_total_score))
				{
					/// 引き分け処理
					GameSetRtn();
				}
				else
				{
					GameSetRtn();
				}
			}
		}

		private void GameSetRtn()
		{
			/// 
			GameSetShowDialog();
		}

		private async void GameSetShowDialog()
		{
			GameData.gameData dialogGame = GameData.GetRecords(game_id)[0];
			string Comment = "";
			string dialog_team_name;
			if (Convert.ToInt32(dialogGame.top_total_score)
				> Convert.ToInt32(dialogGame.btm_total_score))
			{
				int dialog_team_id = dialogGame.bat_first_team_id;
				dialog_team_name = TeamData.GetRecordTeamData(dialog_team_id)[0].teamName;
				Comment = dialog_team_name + "の勝利";
			}
			else if (Convert.ToInt32(dialogGame.top_total_score)
				< Convert.ToInt32(dialogGame.btm_total_score))
			{
				int dialog_team_id = dialogGame.field_first_team_id;
				dialog_team_name = TeamData.GetRecordTeamData(dialog_team_id)[0].teamName;
				Comment = dialog_team_name + "の勝利";
			}
			else if (Convert.ToInt32(dialogGame.top_total_score)
				== Convert.ToInt32(dialogGame.btm_total_score))
			{
				Comment = "引き分けで試合を終了します";
			}
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "試合終了";
			dialog.Content = Comment;
			dialog.CloseButtonText = "OK";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				Frame.Navigate(typeof(ScorePage));
			}
			else if (result == ContentDialogResult.None)
			{
				Frame.Navigate(typeof(MainPage));
			}
		}


		private void ChangeTopBtn()
		{
			count_b = 0;
			count_s = 0;
			count_o = 0;

			if (top_btmFlg)
			{
				IningScoreUpdate(top_btm_flg: top_btmFlg, ining: Ining);    // イニング変更前に得点を記載する

				// 次のイニングに行く前にゲームセットの確認をする
				GameSetCheck(game_id: game_id);
				top_btmFlg = false;
				GameData.updateRecord(
					game_id: game_id,
					top_btm_flg: top_btmFlg,
					ining: Convert.ToString(Convert.ToInt32(Ining) + 1),
					count_b: count_b,
					count_s: count_s,
					count_o: count_o,
					run_1: runner_1,
					run_2: runner_2,
					run_3: runner_3,
					ining_box_id: 1
					);
				Ining = Convert.ToString(Convert.ToInt32(Ining) + 1);  // 次のイニングへ変更
			}
			else
			{
				IningScoreUpdate(top_btm_flg: top_btmFlg, ining: Ining);    // イニング変更前に得点を記載する
				GameSetCheck(game_id: game_id);
				top_btmFlg = true;
				GameData.updateRecord(
					game_id: game_id,
					top_btm_flg: top_btmFlg,
					count_b: count_b,
					count_s: count_s,
					count_o: count_o,
					run_1: runner_1,
					run_2: runner_2,
					run_3: runner_3,
					ining_box_id: 1
					);
			}
			//IningBoxId = 1; // イニング内打席順を初期化

			BoxData.updateRecord(
							box_id: box_id,
							etc_cd1: 1,
							runner_1: runner_1,
							runner_2: runner_2,
							runner_3: runner_3,
							runner_1_player_id: runner_1_player_id,
							runner_2_player_id: runner_2_player_id,
							runner_3_player_id: runner_3_player_id,
							top_bot: top_btmFlg,
							error: errorFlg,
							fielder_choice: fielderchiceFlg,
							dead: deadFlg,
							swing_so: swingSoFlg,
							s_bunt: s_buntFlg,
							s_fly: s_flyFlg,
							update_date: DateTime.Now
							);  // etc_cd1: 1= チェンジ


			Ining_Change();
		}

		private void HitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			selectHitTypeIndex = comboBox.SelectedIndex;
			BoxData.updateRecord(
						box_id: box_id,
						runner_1: runner_1,
						runner_2: runner_2,
						runner_3: runner_3,
						runner_1_player_id: runner_1_player_id,
						runner_2_player_id: runner_2_player_id,
						runner_3_player_id: runner_3_player_id,
						top_bot: top_btmFlg,
						res_hit_type: selectHitTypeIndex,
						error: errorFlg,
						fielder_choice: fielderchiceFlg,
						dead: deadFlg,
						swing_so: swingSoFlg,
						s_bunt: s_buntFlg,
						s_fly: s_flyFlg,
						update_date: DateTime.Now
				);

		}

		private void Position_Button_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
		{
			var btn = (Button)sender;
			if (!PositionSelectedFlg)
			{
				defaultColor_btn = btn.Background;
				defaultColor_font = btn.Foreground;
				PositionSelectedFlg = true;
			}
			switch (btn.Name)
			{
				case "投手_btn":
					position_list.Add(-1);
					break;
				case "捕手_btn":
					position_list.Add(-2);
					break;
				case "一塁手_btn":
					position_list.Add(-3);
					break;
				case "二塁手_btn":
					position_list.Add(-4);
					break;
				case "三塁手_btn":
					position_list.Add(-5);
					break;
				case "遊撃手_btn":
					position_list.Add(-6);
					break;
				case "左翼手_btn":
					position_list.Add(-7);
					break;
				case "中堅手_btn":
					position_list.Add(-8);
					break;
				case "右翼手_btn":
					position_list.Add(-9);
					break;
				default:
					break;
			}
			btn.Background = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
			btn.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

		}

		private void Position_Button_Tapped(object sender, TappedRoutedEventArgs e)
		{
			var btn = (Button)sender;
			if (!PositionSelectedFlg)
			{
				defaultColor_btn = btn.Background;
				defaultColor_font = btn.Foreground;
				PositionSelectedFlg = true;
			}
			switch (btn.Name)
			{
				case "投手_btn":
					position_list.Add(1);
					break;
				case "捕手_btn":
					position_list.Add(2);
					break;
				case "一塁手_btn":
					position_list.Add(3);
					break;
				case "二塁手_btn":
					position_list.Add(4);
					break;
				case "三塁手_btn":
					position_list.Add(5);
					break;
				case "遊撃手_btn":
					position_list.Add(6);
					break;
				case "左翼手_btn":
					position_list.Add(7);
					break;
				case "中堅手_btn":
					position_list.Add(8);
					break;
				case "右翼手_btn":
					position_list.Add(9);
					break;
				default:
					break;
			}
			btn.Background = new SolidColorBrush(Windows.UI.Colors.SteelBlue);
			btn.Foreground = new SolidColorBrush(Windows.UI.Colors.White);

		}

		private void Position_Button_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			var btn = (Button)sender;
			if (!PositionSelectedFlg)
			{
				defaultColor_btn = btn.Background;
				defaultColor_font = btn.Foreground;
				PositionSelectedFlg = true;
			}
			switch (btn.Name)
			{
				case "投手_btn":
					position_list.Add(-1);
					break;
				case "捕手_btn":
					position_list.Add(-2);
					break;
				case "一塁手_btn":
					position_list.Add(-3);
					break;
				case "二塁手_btn":
					position_list.Add(-4);
					break;
				case "三塁手_btn":
					position_list.Add(-5);
					break;
				case "遊撃手_btn":
					position_list.Add(-6);
					break;
				case "左翼手_btn":
					position_list.Add(-7);
					break;
				case "中堅手_btn":
					position_list.Add(-8);
					break;
				case "右翼手_btn":
					position_list.Add(-9);
					break;
				default:
					break;
			}
			btn.Background = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
			btn.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
		}

		private void Runner_Image_RightTapped(object sender, RightTappedRoutedEventArgs e)
		{
			Image image = (Image)sender;
			string name = image.Name;
			if (!name.Contains('_')) { return; }
			string base_num = name.Split('_')[1];
			int num = 0;
			if (int.TryParse(base_num, out num))
			{
				Runner_Image_Grid_BackGroundColor(num);
			}


		}


		private void Runner_Image_Grid_BackGroundColor(int id = 0)
		{
			if (id == 0) { return; }
			if (id == 1)
			{
				StealFirstFlg = !StealFirstFlg;
				if (StealFirstFlg)
				{
					Runner_1_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
				}


			}
			if (id == 2)
			{
				StealSecondFlg = !StealSecondFlg;
				if (StealSecondFlg)
				{
					Runner_2_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
				}
			}
			if (id == 3)
			{
				StealThirdFlg = !StealThirdFlg;
				if (StealThirdFlg)
				{
					Runner_3_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
				}
			}
		}

		private void Runner_Image_Grid_BorderInitialize()
		{
			Runner_1_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
			Runner_2_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
			Runner_3_Image_Grid.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
		}


		private void RunEntryRtn(int inplay_id = 0, int base_id = 0, int run_player_id = 0, int success = 0)
		{
			int run_id = RunData.GetRecordsCount()[0].run_count;

			RunData.addRecord(
				run_id: run_id,
				box_id: box_id,
				game_id: game_id,
				team_id: box_template[0].team_id,
				player_id: box_template[0].player_id,
				pitcher_id: box_template[0].pitcher_id,
				pit_team_id: box_template[0].pit_team_id,
				pit_hand_id: box_template[0].pit_hand_id,
				cat_id: box_template[0].cat_id,
				ump_id: box_template[0].ump_id,
				park_id: box_template[0].park_id,
				ball_type: box_template[0].last_ball_type,
				ball_speed: box_template[0].last_ball_speed,
				count_b: count_b,
				count_s: count_s,
				count_o: count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				runner_1_player_id: runner_1_player_id,
				runner_2_player_id: runner_2_player_id,
				runner_3_player_id: runner_3_player_id,
				ining: Convert.ToInt32(Ining),
				top_bot: top_btmFlg,
				etc_cd1: box_template[0].etc_cd1,
				etc_cd2: 0,
				etc_cd3: 0,
				etc_cd4: 0,
				etc_cd5: 0,
				etc_str1: "",
				etc_str2: "",
				etc_str3: "",
				etc_str4: "",
				etc_str5: "",
				inplay_id: inplay_id,
				success_id: success,  // 成功:0,失敗:1
				run_player_id: run_player_id,  // 盗塁した選手のID
				from_base: base_id,  // 盗塁元のベース
				to_base: -1,        // 盗塁先のベース

				update_date: DateTime.Now
				);
		}

		private int GetOrderId()
		{
			int result;
			if (top_btmFlg)
			{
				if (int.TryParse(btm_order_id, out result))
				{
					return result;
				}
			}
			if (int.TryParse(top_order_id, out result))
			{
				return result;
			}
			return 0;
		}
		private int GetTopBtmCode()
		{
			if (top_btmFlg)
			{
				return 1;
			}
			return 0;
		}



	}

}
