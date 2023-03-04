using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MMSS
{
	/// <summary>
	/// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
	/// </summary>
	public sealed partial class ScorePage : Page
	{
		public ScorePage()
		{
			this.InitializeComponent();

			game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);
			old_game = GameData.GetRecords(game_id);
			IningBoxId = game[0].ining_box_id;
			IningBoxId = BoxData.GetRecordsIningBoxsCount(
												game_id: game_id,
												ining: Convert.ToInt32(game[0].ining),
												top_btmFlg: game[0].top_btm_flg
												)[0].box_count;

			var tmp = BoxData.GetRecordsCount();
			//int tmp_iniBoxId = game[0].ining_box_id;
			int tmp_iniBoxId = BoxData.GetRecordsIningBoxsCount(
												game_id: game_id,
												ining: Convert.ToInt32(game[0].ining),
												top_btmFlg: game[0].top_btm_flg
												)[0].box_count;
			if (tmp_iniBoxId > 1)
			{
				RunnerPlace(box_id: box_id);
			}


			if (game[0].player_change_flg)  // 選手交代時の処理
			{
				box_id = tmp[0].box_count - 1;  // 試合中の画面遷移時
				ballData = BallData.GetRecordsBoxId(box_id);
				ball_flg = false;
				foreach (BallData.ballData ball_row in ballData)
				{
					Zone_Image_Replace(
						x: ball_row.ball_x,
						y: ball_row.ball_y,
						tmp_ball_type: ball_row.ball_type,
						ball_action: ball_row.ball_action,
						ball_img: ball_row.ball_img
						);
				}
				GameData.updateGameFlgRecord(
									game[0].game_id,
									game_start_flg: true,
									player_change_flg: false
									);
				BallScoreListView.ItemsSource
					= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId
									);
				BoxScoreListView.ItemsSource
					= BoxData.GetRecordsIningBoxs(
									game_id: game_id,
									ining: Convert.ToInt32(game[0].ining),
									//top_btmFlg: top_btmFlg
									top_btmFlg: game[0].top_btm_flg
									);
				count_b = game[0].count_b;
				count_s = game[0].count_s;
				count_o = game[0].count_o;

				runner_1_player_id = game[0].run_1_player_id;
				runner_2_player_id = game[0].run_2_player_id;
				runner_3_player_id = game[0].run_3_player_id;
			}
			else if (game[0].run_play_id > 0)
			{
				box_id = tmp[0].box_count - 1;  // 試合中の画面遷移時
				ballData = BallData.GetRecordsBoxId(box_id);


				ball_flg = false;
				foreach (BallData.ballData ball_row in ballData)
				{
					Zone_Image_Replace(
						x: ball_row.ball_x,
						y: ball_row.ball_y,
						tmp_ball_type: ball_row.ball_type,
						ball_action: ball_row.ball_action,
						ball_img: ball_row.ball_img
						);
				}
				/// 投球記録を実施するためにフラグを付ける
				ball_flg = false;
				BallScoreListView.ItemsSource
					= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId
									);
				BoxScoreListView.ItemsSource
					= BoxData.GetRecordsIningBoxs(
										game_id: game_id,
										ining: Convert.ToInt32(game[0].ining),
										//top_btmFlg: top_btmFlg
										top_btmFlg: game[0].top_btm_flg
										);
				count_b = game[0].count_b;
				count_s = game[0].count_s;
				count_o = game[0].count_o;

				runner_1_player_id = game[0].run_1_player_id;
				runner_2_player_id = game[0].run_2_player_id;
				runner_3_player_id = game[0].run_3_player_id;

				GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 0);
			}
			else  // 新たな打席になった時の処理
			{
				box_id = tmp[0].box_count;  // 試合開始・打席の移り変わり
				BoxData.addRecord(
							box_id: box_id,
							game_id: game_id,
							season: DateTime.Now,
							count_o: count_o,
							ining_box_id: IningBoxId,
							top_bot: game[0].top_btm_flg,
							ining: Convert.ToInt32(game[0].ining),
							update_date: DateTime.Now
							);

				BoxScoreListView.ItemsSource
					= BoxData.GetRecordsIningBoxs(
										game_id: game_id,
										ining: Convert.ToInt32(game[0].ining),
										//top_btmFlg: top_btmFlg
										top_btmFlg: game[0].top_btm_flg
										);
				count_o = game[0].count_o;
				IningBoxId = BoxData.GetRecordsIningBoxsCount(
												game_id: game_id,
												ining: Convert.ToInt32(game[0].ining),
												top_btmFlg: game[0].top_btm_flg
												)[0].box_count;
			}

			count_b = game[0].count_b;
			count_s = game[0].count_s;
			count_o = game[0].count_o;
			runner_1_player_id = game[0].run_1_player_id;
			runner_2_player_id = game[0].run_2_player_id;
			runner_3_player_id = game[0].run_3_player_id;
			countDisplay();  // フィールド画面から遷移した時に引き継いだカウントを表示する


			teams = TeamData.GetRecords();

			string topTeamName = teams.Find(x => x.team_id == game[0].bat_first_team_id).teamName;
			string btmTeamName = teams.Find(x => x.team_id == game[0].field_first_team_id).teamName;
			top_players = PlayerData.GetRecords(team_id: game[0].bat_first_team_id, selected: 0);
			btm_players = PlayerData.GetRecords(team_id: game[0].field_first_team_id, selected: 0);
			game[0].top_teamName = topTeamName;
			game[0].btm_teamName = btmTeamName;
			top_order_id = game[0].top_order_id;
			btm_order_id = game[0].btm_order_id;

			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);

			ump_id = game[0].ump_id;
			weather_id = game[0].weather_id;
			park_id = game[0].park_id;

			if (game[0].run_1 || game[0].run_2 || game[0].run_3)
			{
				Runner_Play_btn.Visibility = Visibility.Visible;
				PickOff_btn.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_Play_btn.Visibility = Visibility.Collapsed;
				PickOff_btn.Visibility = Visibility.Collapsed;
			}

			Ining = game[0].ining;
			top_btmFlg = game[0].top_btm_flg;   // 裏表フラグの取得
			if (!top_btmFlg)
			{
				PitcherDataTextBlock.Text = btm_players.Find(x => x.position == 1).name;
				PitcherHandId = btm_players.Find(x => x.position == 1).hand_id;

				BatterDataTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).name;
				BatterDataOrderIdTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).etc_str2;
				selectedPlayerId = top_players.Find(x => x.etc_str2 == top_order_id).player_id;
				BatterBatId = top_players.Find(x => x.etc_str2 == top_order_id).bat_id;
				selectedPitcherPlayerId = btm_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = btm_players.Find(x => x.position == 2).player_id;
				selectedTeamId = game[0].bat_first_team_id;
				selectedPitcherTeamId = game[0].field_first_team_id;
				PitcherTeamNameTextBlock.Text = btmTeamName;
				BatterTeamNameTextBlock.Text = topTeamName;

				PitcherDataHandTextBox.Text = btm_players.Find(x => x.position == 1).hand;
				BatterDataBatTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).bat;

				/// ver.1.0.9.0以降
				PitcherDataCommentTextBox.Text = btm_players.Find(x => x.position == 1).cmnt1;
				BatterDataCommentTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).cmnt1;

				if (!int.TryParse(top_order_id, out order_id))
				{
					order_id = 0;
				}

			}
			else
			{
				PitcherDataTextBlock.Text = top_players.Find(x => x.position == 1).name;
				PitcherHandId = top_players.Find(x => x.position == 1).hand_id;
				BatterDataTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).name;
				BatterDataOrderIdTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).etc_str2;
				selectedPlayerId = btm_players.Find(x => x.etc_str2 == btm_order_id).player_id;
				BatterBatId = BatterBatIdGet(btm_players.Find(x => x.etc_str2 == btm_order_id).bat);
				selectedPitcherPlayerId = top_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = top_players.Find(x => x.position == 2).player_id;
				selectedTeamId = game[0].field_first_team_id;
				selectedPitcherTeamId = game[0].bat_first_team_id;
				PitcherTeamNameTextBlock.Text = topTeamName;
				BatterTeamNameTextBlock.Text = btmTeamName;


				/// ver.1.0.9.0以降
				PitcherDataCommentTextBox.Text = top_players.Find(x => x.position == 1).cmnt1;
				BatterDataCommentTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).cmnt1;

				PitcherDataHandTextBox.Text = top_players.Find(x => x.position == 1).hand;
				BatterDataBatTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).bat;

				if (!int.TryParse(btm_order_id, out order_id))
				{
					order_id = 0;
				}


			}
			GameData.updateRecord(
						game_id: game_id,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						player_id: selectedPlayerId,
						top_btm_flg: top_btmFlg
						);
			ScoreBoardDisplay.ItemsSource = game;

			BallTypeListView.ItemsSource = BallType.GetRecordsBallType(hand: PitcherHandId);

			ReSetBoxFlg = false;

			/// ver.3.0.0.0
			top_btm_cd = GetTopBtmCode();
			pitchingResult = new PitchingResult(game_id, selectedPitcherPlayerId, top_btm_cd);
			pitchingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			int position_id = -1;
			if (top_btm_cd == 0)
			{
				position_id = top_players.Find(x => x.player_id == selectedPlayerId).position;
			}
			if (top_btm_cd == 1)
			{
				position_id = btm_players.Find(x => x.player_id == selectedPlayerId).position;
			}
			order_id = GetOrderId();
			battingResult = new BattingResult(game_id, selectedPlayerId, order_id, top_btm_cd, position_id);
			battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
		}
		private bool ball_flg = false;
		private int ball_action = 0;
		private int ball_type = 0;
		private int ball_count = 1;
		private double zone_x;
		private double zone_y;
		private int zone_x_int;
		private int zone_y_int;
		private DateTime update_time;
		private int box_id = 0;
		private int pit_hand = 0;
		private int bat_cd = 0;
		private int course_id = 0;
		private int count_b = 0;
		private int count_s = 0;
		private int count_o = 0;
		private bool runner_1 = false;
		private bool runner_2 = false;
		private bool runner_3 = false;
		private int runner_1_player_id = 0;
		private int runner_2_player_id = 0;
		private int runner_3_player_id = 0;
		private int game_id = 0;
		private List<GameData.gameData> game;
		private List<GameData.gameData> old_game;
		private List<TeamData.teamData> teams;
		private List<PlayerData.playerData> top_players;
		private List<PlayerData.playerData> btm_players;
		private string top_order_id = "1";
		private string btm_order_id = "1";

		private int selectedPlayerId = 0;
		private int selectedPitcherPlayerId = 0;
		private int selectedTeamId = 0;
		private int selectedPitcherTeamId = 0;
		private int ump_id = 0;
		private int weather_id = 0;
		private int selectedCatcherPlayerId = 0;
		private int park_id = 0;
		private bool top_btmFlg = false;
		private int ballSpeedEntry = 0;
		private int topScore = 0;
		private int btmScore = 0;
		private int IningScore = 0;
		private int IningScore_tmp = 0;
		private string Ining = "1";
		private int PitcherHandId = 0;
		private int BatterBatId = 0;
		private int IningBoxId = 1;
		private int ball_box_num = 0;
		private int ball_total_num = 0;
		private int game_box_num = 1;
		private Button flyout_btn;
		private int ball_img = 0;
		private bool dead_flg = false;
		private bool inPlayFlg = false;

		private bool ReSetBoxFlg = false;


		private int ball_type_flg = -1;

		private List<BallData.ballData> ballData;
		private List<BallData.ballData> tmp_ballData;
		private List<BallData.ballData> del_ballData;

		private MenuFlyoutItem steal_ball = new MenuFlyoutItem();
		private MenuFlyoutItem steal_strike = new MenuFlyoutItem();
		private MenuFlyoutItem steal_swing = new MenuFlyoutItem();
		private bool StealFlg = false;

		private string ball_comment = "";

		private PitchingResult pitchingResult;
		private BattingResult battingResult;
		private int order_id;
		private int top_btm_cd;

		private int selectedLang;

		//private List<object> Count_obj = new List<object>();

		/// <summary>
		/// Get ZoneImage X/Y Point (type:double)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Zone_Image_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Point ptrPT = e.GetPosition(ZoneImage);
			zone_x = ptrPT.X;
			zone_y = ptrPT.Y;
			zone_x_int = Convert.ToInt32(zone_x);
			zone_y_int = Convert.ToInt32(zone_y);
			if (ball_flg)
			{
				ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
				ball_flg = false;
				return;
			}


			Zone_Draw_Ball_img(
				ball_img: ball_img,
				tmp_zone_x: Convert.ToInt32(zone_x),
				tmp_zone_y: Convert.ToInt32(zone_y),
				in_ball_flg: true
				);

			course_id = BallVHType(zone_x_int, zone_y_int);

			if (ball_flg)
			{
				if (runner_1 || runner_2 || runner_3)
				{
					steal_ball = new MenuFlyoutItem();
					steal_ball.Name = "MenuFlyItemStealBall";
					steal_ball.Click += new RoutedEventHandler(Next_Ball);
					steal_strike = new MenuFlyoutItem();
					steal_strike.Name = "MenuFlyItemStealStrike";
					steal_strike.Click += new RoutedEventHandler(Next_Ball);
					steal_swing = new MenuFlyoutItem();
					steal_swing.Name = "MenuFlyItemStealSwing";
					steal_swing.Click += new RoutedEventHandler(Next_Ball);

					steal_ball.Text = "ボール + 盗塁";
					steal_strike.Text = "見逃し + 盗塁";
					steal_swing.Text = "空振り + 盗塁";
					MenuFlyItem.Items.Add(steal_ball);
					MenuFlyItem.Items.Add(steal_strike);
					MenuFlyItem.Items.Add(steal_swing);
				}

				FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
				//FlyoutBase.ShowAttachedFlyout((FrameworkElement)MenuFlyItem);
			}
		}

		/// <summary>
		/// Zoneへ選択した球種を描画する
		/// </summary>
		/// <param name="ball_img"></param>
		private void Zone_Draw_Ball_img(
							int ball_img = 0,
							int tmp_zone_x = 0,
							int tmp_zone_y = 0,
							object color = null,
							bool in_ball_flg = true
			)
		{
			if (tmp_zone_x < 0 && tmp_zone_y < 0) { return; }  // けん制球のときは配置しない


			var poly = new Polygon();
			var point = new PointCollection();
			poly.Fill = new SolidColorBrush((color is Color) ? (Color)color : Colors.AliceBlue);
			switch (ball_img)
			{
				case 0:
					point.Add(new Point(6, -8));
					point.Add(new Point(10, -8));
					point.Add(new Point(12, -7));
					point.Add(new Point(15, -4));
					point.Add(new Point(16, -2));
					point.Add(new Point(16, 2));
					point.Add(new Point(15, 4));
					point.Add(new Point(12, 7));
					point.Add(new Point(10, 8));
					point.Add(new Point(6, 8));
					point.Add(new Point(4, 7));
					point.Add(new Point(1, 4));
					point.Add(new Point(0, 2));
					point.Add(new Point(0, -2));
					point.Add(new Point(1, -4));
					point.Add(new Point(4, -7));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 1:
					point.Add(new Point(17, -8));
					point.Add(new Point(17, 8));
					point.Add(new Point(-1, 0));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 2:
					point.Add(new Point(7, -10));
					point.Add(new Point(18, 1));
					point.Add(new Point(-1, 9));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 3:
					point.Add(new Point(-1, -9));
					point.Add(new Point(17, -9));
					point.Add(new Point(8, 9));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 4:
					point.Add(new Point(-2, 1));
					point.Add(new Point(7, -10));
					point.Add(new Point(17, 9));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 5:
					point.Add(new Point(0, -8));
					point.Add(new Point(0, 8));
					point.Add(new Point(18, 0));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(tmp_zone_x, tmp_zone_y, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 6:
					point.Add(new Point(-4, -8));
					point.Add(new Point(12, -8));
					point.Add(new Point(12, 8));
					point.Add(new Point(-4, 8));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 7:
					point.Add(new Point(10, -10));
					point.Add(new Point(20, 0));
					point.Add(new Point(10, 10));
					point.Add(new Point(0, 0));
					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 8:
					point.Add(new Point(-4, -9));
					point.Add(new Point(14, -9));
					point.Add(new Point(14, 0));
					point.Add(new Point(5, 9));
					point.Add(new Point(-4, 0));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				default:
					break;

			}
			ball_flg = in_ball_flg;
		}



		/// <summary>
		/// ランナーの表示
		/// IningBoxIdが1以外の時に処理を実行する
		/// </summary>
		private void RunnerPlace(int box_id)
		{
			var runGameData = GameData.GetRecords(game_id: game_id);
			runner_1 = runGameData[0].run_1;
			runner_2 = runGameData[0].run_2;
			runner_3 = runGameData[0].run_3;
			run_1.IsChecked = runner_1;
			run_2.IsChecked = runner_2;
			run_3.IsChecked = runner_3;
		}

		/// <summary>
		/// 打席修正時のランナー
		/// </summary>
		/// <param name="ball_row"></param>
		private void RunnerResetPlace(BoxData.boxDataIning box_row)
		{
			runner_1 = box_row.runner_1;
			runner_2 = box_row.runner_2;
			runner_3 = box_row.runner_3;
			run_1.IsChecked = runner_1;
			run_2.IsChecked = runner_2;
			run_3.IsChecked = runner_3;
		}


		private int PitcherHandIdGet(string hand)
		{
			int hand_id = 0;
			if (hand == "左") { hand_id = 1; }
			return hand_id;
		}

		private int BatterBatIdGet(string bat)
		{
			int bat_id = 0;
			if (bat == "左") { bat_id = 1; }
			return bat_id;
		}

		private void DataInitialize()
		{
			this.InitializeComponent();
			BallData.InitializeDB();
			BallAction.InitializeDB();
			BallType.InitializeDB();
			BoxData.InitializeDB();
			BallCollor.InitializeDB();
			BallCourse.InitializeDB();
			game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);

			var tmp = BoxData.GetRecordsCount();
			box_id = tmp[0].box_count;

			/// 2022.03.08
			IningBoxId = BoxData.GetRecordsIningBoxsCount(
												game_id: game_id,
												ining: Convert.ToInt32(game[0].ining),
												top_btmFlg: game[0].top_btm_flg
												)[0].box_count;

			BoxData.addRecord(
							box_id: box_id,
							season: DateTime.Now,
							count_o: count_o,
							ining_box_id: IningBoxId,
							top_bot: game[0].top_btm_flg,
							ining: Convert.ToInt32(game[0].ining),
							update_date: DateTime.Now);
			//int tmp_iniBoxId = BoxData.GetRecordsIningBoxId(box_id)[0].ining_box_id;
			int tmp_iniBoxId = game[0].ining_box_id;
			tmp_iniBoxId = BoxData.GetRecordsIningBoxsCount(
											game_id: game_id,
											ining: Convert.ToInt32(game[0].ining),
											top_btmFlg: game[0].top_btm_flg
											)[0].box_count;
			if (tmp_iniBoxId > 1)
			{
				RunnerPlace(box_id: box_id);
			}

			IningBoxId = tmp_iniBoxId + 1;
			IningBoxId = BoxData.GetRecordsIningBoxsCount(game_id: game_id, ining: Convert.ToInt32(game[0].ining), top_btmFlg: game[0].top_btm_flg)[0].box_count;

			teams = TeamData.GetRecords();

			string topTeamName = teams.Find(x => x.team_id == game[0].bat_first_team_id).teamName;
			string btmTeamName = teams.Find(x => x.team_id == game[0].field_first_team_id).teamName;
			top_players = PlayerData.GetRecords(team_id: game[0].bat_first_team_id, selected: 0);
			btm_players = PlayerData.GetRecords(team_id: game[0].field_first_team_id, selected: 0);
			game[0].top_teamName = topTeamName;
			game[0].btm_teamName = btmTeamName;
			top_order_id = game[0].top_order_id;
			btm_order_id = game[0].btm_order_id;

			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);

			ump_id = game[0].ump_id;
			weather_id = game[0].weather_id;
			park_id = game[0].park_id;

			if (game[0].run_1 || game[0].run_2 || game[0].run_3)
			{
				Runner_Play_btn.Visibility = Visibility.Visible;
				PickOff_btn.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_Play_btn.Visibility = Visibility.Collapsed;
				PickOff_btn.Visibility = Visibility.Collapsed;
			}


			top_btmFlg = game[0].top_btm_flg;   // 裏表フラグの取得
			if (!top_btmFlg)
			{
				/// ver.3.0.0.0.　以降
				PitcherDataTextBlock.Text = btm_players.Find(x => x.position == 1).name;
				PitcherHandId = PitcherHandIdGet(btm_players.Find(x => x.position == 1).hand);
				selectedPitcherPlayerId = btm_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = btm_players.Find(x => x.position == 2).player_id;

				selectedPlayerId = top_players.Find(x => x.etc_str2 == top_order_id).player_id;
				BatterDataTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).name;
				BatterDataOrderIdTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).etc_str2;
				BatterBatId = BatterBatIdGet(top_players.Find(x => x.etc_str2 == top_order_id).bat);
				selectedTeamId = game[0].bat_first_team_id;
				selectedPitcherTeamId = game[0].field_first_team_id;
				PitcherTeamNameTextBlock.Text = btmTeamName;
				BatterTeamNameTextBlock.Text = topTeamName;

				/// ver.1.0.9.0以降
				BatterDataCommentTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).cmnt1;
				BatterDataBatTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).bat;

				/// ver.3.0.0.0.
				PitcherDataCommentTextBox.Text = btm_players.Find(x => x.position == 1).cmnt1;
				PitcherDataHandTextBox.Text = btm_players.Find(x => x.position == 1).hand;
			}
			else
			{
				/// ver.3.0.0.0.
				PitcherDataTextBlock.Text = top_players.Find(x => x.position == 1).name;
				PitcherHandId = PitcherHandIdGet(top_players.Find(x => x.position == 1).hand);
				selectedPitcherPlayerId = top_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = top_players.Find(x => x.position == 2).player_id;

				BatterDataTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).name;
				BatterDataOrderIdTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).etc_str2;
				selectedPlayerId = btm_players.Find(x => x.etc_str2 == btm_order_id).player_id;
				BatterBatId = BatterBatIdGet(btm_players.Find(x => x.etc_str2 == btm_order_id).bat);

				selectedTeamId = game[0].field_first_team_id;
				selectedPitcherTeamId = game[0].bat_first_team_id;
				PitcherTeamNameTextBlock.Text = topTeamName;
				BatterTeamNameTextBlock.Text = btmTeamName;

				/// ver.1.0.9.0以降
				BatterDataCommentTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).cmnt1;
				BatterDataBatTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).bat;

				/// ver.3.0.0.0.以降
				PitcherDataCommentTextBox.Text = top_players.Find(x => x.position == 1).cmnt1;
				PitcherDataHandTextBox.Text = top_players.Find(x => x.position == 1).hand;
			}
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						player_id: selectedPlayerId,
						park_id: park_id,
						ump_id: ump_id,
						weather_id: weather_id
						);
			ScoreBoardDisplay.ItemsSource = game;

			BallTypeListView.ItemsSource = BallType.GetRecordsBallType(hand: PitcherHandId);

			top_btm_cd = GetTopBtmCode();
			pitchingResult = new PitchingResult(game_id, selectedPitcherPlayerId, top_btm_cd);
			pitchingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();


			int position_id = -1;
			if (top_btm_cd == 0)
			{
				position_id = top_players.Find(x => x.player_id == selectedPlayerId).position;
			}
			if (top_btm_cd == 1)
			{
				position_id = btm_players.Find(x => x.player_id == selectedPlayerId).position;
			}
			order_id = GetOrderId();
			battingResult = new BattingResult(game_id, selectedPlayerId, order_id, top_btm_cd, position_id);
			battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();

			ReSetBoxFlg = false;
		}

		private void Zone_Image_Replace(
									int x,
									int y,
									int tmp_ball_type,
									int ball_action = 0,
									int ball_img = 0)
		{
			int tmp_zone_x_int = Convert.ToInt32(x);
			int tmp_zone_y_int = Convert.ToInt32(y);
			var poly = new Windows.UI.Xaml.Shapes.Polygon();
			var point = new PointCollection();
			Color color;
			switch (ball_action)
			{
				case 0:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
					color = Colors.Green;
					break;
				case 1:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					color = Colors.Orange;
					break;
				case 2:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
					color = Colors.DarkOrange;
					break;
				case 3:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
					color = Colors.Yellow;
					break;
				case 4:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.White);
					color = Colors.White;
					break;
				case 5:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
					color = Colors.Black;
					break;
				case 6:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
					color = Colors.Blue;
					break;
				case 7: // 死球
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
					color = Colors.DarkGreen;
					break;
				default:
					poly.Fill = new SolidColorBrush(Windows.UI.Colors.AliceBlue);
					color = Colors.AliceBlue;
					break;
			}
			Zone_Draw_Ball_img(
					ball_img: ball_img,
					tmp_zone_x: tmp_zone_x_int,
					tmp_zone_y: tmp_zone_y_int,
					color: color,
					in_ball_flg: false
					//in_ball_flg: true
					);

			course_id = BallVHType(tmp_zone_x_int, tmp_zone_y_int);
		}

		private void Ball_Tapped(object sender, TappedRoutedEventArgs e)
		{
			FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
		}

		/// <summary>
		/// ボールのコース別判定を行う
		/// 
		/// </summary>
		/// <param name="ball_x"></param>
		/// <param name="ball_y"></param>
		/// <returns>1:左上, 2:中上, 3:右上, 4:左中、5:中中, 6:右中, 7:左下, 8:中下, 9:右下, 0:指定なし</returns>
		private int BallVHType(int ball_x = 0, int ball_y = 0)
		{
			int xFlg = BallHorizonType(ball_x);
			int yFlg = BallVerticalType(ball_y);
			int xyFlg = xFlg + yFlg;
			int result = 0;
			switch (xyFlg)
			{
				/// 高め
				case 0:
					result = 0;
					break;
				case 10:
					result = 1;
					break;
				case 20:
					result = 2;
					break;

				/// 中
				case 1:
					result = 3;
					break;
				case 11:
					result = 4;
					break;
				case 21:
					result = 5;
					break;

				/// 低め
				case 2:
					result = 6;
					break;
				case 12:
					result = 7;
					break;
				case 22:
					result = 8;
					break;
				default:
					break;
			}
			return result;
		}

		private int BallHorizonType(int ball_x)
		{
			if (ball_x <= 133)
			{
				return 0;
			}
			else if (ball_x > 133 && ball_x <= 185)
			{
				return 10;
			}
			else
			{
				return 20;
			}
		}

		private int BallVerticalType(int ball_y)
		{
			if (ball_y <= 180)
			{
				return 0;
			}
			else if (ball_y > 180 && ball_y <= 260)
			{
				return 1;
			}
			else
			{
				return 2;
			}
		}


		/// <summary>
		/// ZoneImege以外の
		/// ボールをすべて削除する
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Next_Ball(object sender, RoutedEventArgs e)
		{
			ball_flg = false;
			update_time = DateTime.Now;
			var selectFlyOutMenuItem = (MenuFlyoutItem)sender;
			ballSpeedEntry = 0;
			StealFlg = false;
			if (BallSpeedText.Text.Length != 0)
			{
				ballSpeedEntry = Convert.ToInt32(BallSpeedText.Text);
			}

			ball_box_num = BallData.GetCountRecords(
										game_id: game_id,
										box_id: box_id,
										pitcher_id: selectedPitcherPlayerId)[0].ball_id;
			ball_total_num = BallData.GetCountRecords(
										game_id: game_id,
										pitcher_id: selectedPitcherPlayerId)[0].ball_id;
			game_box_num = BoxData.GetRecordsGameBoxCount(
											game_id: game_id,
											player_id: selectedPlayerId
											)[0].box_count;
			old_game = GameData.GetRecords(game_id);
			GameData.gameData game_count = GameData.GetRecords(game_id)[0];
			int old_count_b = old_game[0].count_b;
			int old_count_s = old_game[0].count_s;
			int old_count_o = old_game[0].count_o;

			/// 2022.03.24 コメント
			BallCmntRtn();

			pitchingResult.UpBallCount();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			switch (selectFlyOutMenuItem.Name)
			{
				case "MenuFlyItemBall":
					count_b = game_count.count_b + 1;
					count_s = game_count.count_s;
					count_o = game_count.count_o;
					ball_action = 0;
					break;
				case "MenuFlyItemStrike":
					count_b = game_count.count_b;
					count_s = game_count.count_s + 1;
					count_o = game_count.count_o;
					ball_action = 1;
					break;
				case "MenuFlyItemSwingStrike":
					count_b = game_count.count_b;
					count_s = game_count.count_s + 1;
					count_o = game_count.count_o;
					ball_action = 2;
					break;
				case "MenuFlyItemFoul":
					count_b = game_count.count_b;
					if (old_count_s < 2)
					{
						count_s = game_count.count_s + 1;
					}
					else
					{
						count_s = game_count.count_s;
					}
					count_o = game_count.count_o;
					ball_action = 3;

					break;
				case "MenuFlyItemInPlay":
					count_b = game_count.count_b;
					count_s = game_count.count_s;
					count_o = game_count.count_o;

					ball_action = 4;
					inPlayFlg = true;

					BoxData.updateRecord(
							box_id: box_id,
							player_id: selectedPlayerId,
							pitcher_id: selectedPitcherPlayerId,
							team_id: selectedTeamId,
							pit_team_id: selectedPitcherTeamId,
							pit_hand_id: PitcherHandId,
							bat_id: BatterBatId,
							ump_id: ump_id,
							cat_id: selectedCatcherPlayerId,
							game_id: game_id,
							game_box_num: game_box_num,
							ball_box_num: ball_box_num,
							ball_total_num: ball_total_num,
							last_ball_speed: ballSpeedEntry,
							last_ball_type: ball_type,
							count_b: old_game[0].count_b,
							count_s: old_game[0].count_s,
							count_o: old_game[0].count_o,
							ining: Convert.ToInt32(Ining),
							top_bot: top_btmFlg,
							runner_1: game[0].run_1,
							runner_2: game[0].run_2,
							runner_3: game[0].run_3,
							runner_1_player_id: game[0].run_1_player_id,
							runner_2_player_id: game[0].run_2_player_id,
							runner_3_player_id: game[0].run_3_player_id,
							update_date: update_time
							);

					InPlayAction();


					break;
				case "MenuFlyItemDead":
					dead_flg = true;
					count_b = game_count.count_b;
					count_s = game_count.count_s;
					count_o = game_count.count_o;
					ball_action = 7;


					BallData.addRecord(
						player_id: selectedPlayerId,
						pitcher_id: selectedPitcherPlayerId,
						team_id: selectedTeamId,
						pit_team_id: selectedPitcherTeamId,
						pit_hand_id: PitcherHandId,
						bat_id: BatterBatId,
						ump_id: ump_id,
						cat_id: selectedCatcherPlayerId,
						game_id: game_id,
						//in_play: true,
						park_id: park_id,
						game_box_num: game_box_num,
						box_id: box_id,
						ball_total_num: ball_total_num,
						cource_table_id: course_id,
						ball_level: 1,
						ball_action: ball_action,
						etc_cd1: ball_action,
						ball_box_num: ball_box_num,
						ball_speed: ballSpeedEntry,
						count_b: old_game[0].count_b,
						count_s: old_game[0].count_s,
						count_o: old_game[0].count_o,
						runner_1: runner_1,
						runner_2: runner_2,
						runner_3: runner_3,
						ball_x: zone_x_int,
						ball_y: zone_y_int,
						ball_type: ball_type,
						ining: Convert.ToInt32(Ining),
						etc_str5: ball_comment,
						top_bot: top_btmFlg,
						update_date: update_time
						);
					NextBatterDialog();

					return;
				case "MenuFlyItemStealBall":
					StealFlg = true;
					count_b = game_count.count_b + 1;
					count_s = game_count.count_s;
					count_o = game_count.count_o;
					ball_action = 0;
					break;
				case "MenuFlyItemStealStrike":
					StealFlg = true;
					count_b = game_count.count_b;
					count_s = game_count.count_s + 1;
					count_o = game_count.count_o;
					ball_action = 1;
					break;
				case "MenuFlyItemStealSwing":
					StealFlg = true;
					count_b = game_count.count_b;
					count_s = game_count.count_s + 1;
					count_o = game_count.count_o;
					ball_action = 2;
					break;
				default:
					break;
			}

			ReSet_Ball_one();  // 一度セットしたボールを削除する

			/// ボールアクションに応じた色を用いる
			Zone_Image_Replace(
				x: zone_x_int,
				y: zone_y_int,
				tmp_ball_type: ball_type,
				ball_action: ball_action,
				ball_img: ball_img
				);

			/// ver 1.1.1.0　以降
			EntryBallResultRtn();

			/// 盗塁アクション
			/// ver.1.1.2.0 以降
			if (StealFlg)
			{
				RunActionRtn();
			}
		}


		private void BallCmntRtn()
		{
			ball_comment = BallCommentTextBox.Text;
			BallCommentTextBox.Text = "";
		}

		private void Delete_Ball(object sender, RoutedEventArgs e)
		{
			while (ZoneGrid.Children.Count > 1)
			{
				ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			}
			ball_flg = false;
		}
		private void Delete_Ball()
		{
			while (ZoneGrid.Children.Count > 1)
			{
				ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			}
			ball_flg = false;
		}

		private void ReSet_Ball_one()
		{
			ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);

			///// 2022.04.06 球数削除のため
			//ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
		}



		private void Cancel_Ball_Action()
		{
			ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			//ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			ball_flg = false;

		}

		private void Cancel_Ball(object sender, RoutedEventArgs e)
		{
			if (ZoneGrid.Children.Count > 1)
			{
				Cancel_Ball_Action();
			}
		}

		private void Clear_Ball()
		{
			while (ZoneGrid.Children.Count > 1)
			{
				ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
				//ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			}
			ball_flg = false;
		}


		private void countDisplay()
		{

			if (count_b == 1)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 2)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 3)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 4)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
			}
			else if (count_b == 0)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}

			if (count_s == 1)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_s == 2)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_s == 3)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
			}
			else if (count_s == 0)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			if (count_o == 1)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 2)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 3)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
			}
			else if (count_o == 0)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
		}


		private void countUp()
		{
			if (count_b == 1)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			}
			else if (count_b == 2)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			}
			else if (count_b == 3)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			}
			else if (count_b == 4)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);

				NextBatterDialog();
			}
			else if (count_b == 0)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			if (count_s == 1)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_s == 2)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			}
			else if (count_s == 3)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);

				NextBatterDialog();
			}
			else if (count_s == 0)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			}
			
			if (count_o == 1)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 2)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 3)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
			}
			else if (count_o == 0)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			//ball_type = 0;
			ball_count++;
		}

		private void walksResult()
		{
			BoxData.updateRecord(
					box_id: box_id,
					res_hit: 4, // 四死球は4
					walks: true,
					player_id: selectedPlayerId,
					pitcher_id: selectedPitcherPlayerId,
					team_id: selectedTeamId,
					pit_team_id: selectedPitcherTeamId,
					pit_hand_id: PitcherHandId,
					bat_id: BatterBatId,
					ump_id: ump_id,
					cat_id: selectedCatcherPlayerId,
					game_id: game_id,
					ining: Convert.ToInt32(Ining),
					runner_1: game[0].run_1,
					runner_2: game[0].run_2,
					runner_3: game[0].run_3,
					runner_1_player_id: game[0].run_1_player_id,
					runner_2_player_id: game[0].run_2_player_id,
					runner_3_player_id: game[0].run_3_player_id,
					top_bot: top_btmFlg,
					ball_box_num: ball_box_num,
					ball_total_num: ball_total_num,
					last_ball_speed: ballSpeedEntry,
					last_ball_type: ball_type,
					park_id: park_id,
					count_b: game[0].count_b,
					count_s: game[0].count_s,
					count_o: game[0].count_o,
					weather_id: weather_id,
					update_date: DateTime.Now
					);

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						count_b: 0,
						count_s: 0
						);
			pitchingResult.UpFourBallCount();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			battingResult.UpBoxCount();
			battingResult.UpFourBallCount();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
		}


		private void AvoidResult()
		{
			BoxData.updateRecord(
					box_id: box_id,
					//res_hit: 6,  // 敬遠は6
					res_hit: 9,  // 敬遠は9
					walks: true,
					player_id: selectedPlayerId,
					pitcher_id: selectedPitcherPlayerId,
					team_id: selectedTeamId,
					pit_team_id: selectedPitcherTeamId,
					pit_hand_id: PitcherHandId,
					bat_id: BatterBatId,
					ump_id: ump_id,
					cat_id: selectedCatcherPlayerId,
					game_id: game_id,
					ining: Convert.ToInt32(Ining),
					runner_1: game[0].run_1,
					runner_2: game[0].run_2,
					runner_3: game[0].run_3,
					runner_1_player_id: game[0].run_1_player_id,
					runner_2_player_id: game[0].run_2_player_id,
					runner_3_player_id: game[0].run_3_player_id,
					top_bot: top_btmFlg,
					ball_box_num: ball_box_num,
					ball_total_num: ball_total_num,
					last_ball_speed: ballSpeedEntry,
					last_ball_type: ball_type,
					park_id: park_id,
					count_b: game[0].count_b,
					count_s: game[0].count_s,
					count_o: game[0].count_o,
					weather_id: weather_id,
					update_date: DateTime.Now
					);

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						count_b: 0,
						count_s: 0
						);
		}

		private void InterFerenceResult()
		{
			BoxData.updateRecord(
					box_id: box_id,
					//res_hit: 5, // 打撃妨害は5
					res_hit: 10, // 打撃妨害は10
					walks: true,
					player_id: selectedPlayerId,
					pitcher_id: selectedPitcherPlayerId,
					team_id: selectedTeamId,
					pit_team_id: selectedPitcherTeamId,
					pit_hand_id: PitcherHandId,
					bat_id: BatterBatId,
					ump_id: ump_id,
					cat_id: selectedCatcherPlayerId,
					game_id: game_id,
					ining: Convert.ToInt32(Ining),
					runner_1: game[0].run_1,
					runner_2: game[0].run_2,
					runner_3: game[0].run_3,
					runner_1_player_id: game[0].run_1_player_id,
					runner_2_player_id: game[0].run_2_player_id,
					runner_3_player_id: game[0].run_3_player_id,
					top_bot: top_btmFlg,
					ball_box_num: ball_box_num,
					ball_total_num: ball_total_num,
					last_ball_speed: ballSpeedEntry,
					last_ball_type: ball_type,
					park_id: park_id,
					count_b: game[0].count_b,
					count_s: game[0].count_s,
					count_o: game[0].count_o,
					weather_id: weather_id,
					update_date: DateTime.Now
					);

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						count_b: 0,
						count_s: 0
						);
		}


		private void WalkOrDeadResult()
		{
			GameData.gameData tmpRunGame = GameData.GetRecords(game_id)[0];
			//runner_1_player_id = tmpRunGame.run_1_player_id;
			runner_1_player_id = selectedPlayerId;
			runner_2_player_id = tmpRunGame.run_1_player_id;
			runner_3_player_id = tmpRunGame.run_2_player_id;
			if (!runner_1)  // 一塁走者がいない場合
			{
				runner_1 = true;
			}
			else
			{
				runner_2_player_id = tmpRunGame.run_1_player_id;
				if (!runner_2)   // 二塁走者がいない場合
				{
					runner_2 = true;
				}
				else
				{
					runner_3_player_id = tmpRunGame.run_2_player_id;
					if (!runner_3)   // 三塁走者がいない場合
					{
						runner_3 = true;
					}
					else
					{
						/// 押し出し処理
						IningScore_tmp = 1;
						ScoreRecord(get_score: IningScore_tmp.ToString());
						IningScore += IningScore_tmp;
					}
				}
			}

			run_1.IsChecked = runner_1;
			run_2.IsChecked = runner_2;
			run_3.IsChecked = runner_3;

		}

		private void DeadResult()
		{
			BoxData.updateRecord(
					box_id: box_id,
					res_hit: 4, // 四死球は4
					walks: false,
					dead: true,
					player_id: selectedPlayerId,
					pitcher_id: selectedPitcherPlayerId,
					team_id: selectedTeamId,
					pit_team_id: selectedPitcherTeamId,
					pit_hand_id: PitcherHandId,
					bat_id: BatterBatId,
					ump_id: ump_id,
					cat_id: selectedCatcherPlayerId,
					game_id: game_id,
					ining: Convert.ToInt32(Ining),
					runner_1: game[0].run_1,
					runner_2: game[0].run_2,
					runner_3: game[0].run_3,
					runner_1_player_id: game[0].run_1_player_id,
					runner_2_player_id: game[0].run_2_player_id,
					runner_3_player_id: game[0].run_3_player_id,
					count_b: game[0].count_b,
					count_s: game[0].count_s,
					count_o: game[0].count_o,
					weather_id: weather_id,
					top_bot: top_btmFlg,
					ball_box_num: ball_box_num,
					ball_total_num: ball_total_num,
					last_ball_speed: ballSpeedEntry,
					last_ball_type: ball_type,
					park_id: park_id,
					update_date: DateTime.Now
					);
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						count_b: 0,
						count_s: 0
						);
			pitchingResult.UpDeadBallCount();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			battingResult.UpBoxCount();
			battingResult.UpDeadBallCount();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
		}

		private void StrikeOut()
		{
			if (ball_action == 1)
			{
				BoxData.updateRecord(
						box_id: box_id,
						miss_so: true,
						runner_1: game[0].run_1,
						runner_2: game[0].run_2,
						runner_3: game[0].run_3,
						runner_1_player_id: game[0].run_1_player_id,
						runner_2_player_id: game[0].run_2_player_id,
						runner_3_player_id: game[0].run_3_player_id,
						top_bot: top_btmFlg,
						ball_box_num: ball_box_num,
						ball_total_num: ball_total_num,
						last_ball_speed: ballSpeedEntry,
						last_ball_type: ball_type,
						park_id: park_id,
						ump_id: ump_id,
						weather_id: weather_id,
						update_date: DateTime.Now
						);
			}
			else if (ball_action == 2)
			{
				BoxData.updateRecord(
						box_id: box_id,
						swing_so: true,
						runner_1: game[0].run_1,
						runner_2: game[0].run_2,
						runner_3: game[0].run_3,
						runner_1_player_id: game[0].run_1_player_id,
						runner_2_player_id: game[0].run_2_player_id,
						runner_3_player_id: game[0].run_3_player_id,
						top_bot: top_btmFlg,
						ball_box_num: ball_box_num,
						ball_total_num: ball_total_num,
						last_ball_speed: ballSpeedEntry,
						last_ball_type: ball_type,
						park_id: park_id,
						ump_id: ump_id,
						weather_id: weather_id,
						update_date: DateTime.Now
						);
			}
			pitchingResult.UpOutCount();
			pitchingResult.UpStrikeOutCount();
			pitchingResult.PitchingDataの登録atデータ有無による新規OR追記処理();

			battingResult.UpBoxCount();
			battingResult.UpBatCount();
			battingResult.UpStrikeOutCount();
			battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
		}

		private void ScoreRecord(string get_score = "")
		{
			if (get_score.Length == 0) { return; }
			bool ScoreRecordTBFlg = game[0].top_btm_flg;
			string ScoreRecordIningSt = game[0].ining;
			string SocreRecordTarget = "";
			if (ScoreRecordTBFlg)
			{
				SocreRecordTarget = "btm_" + ScoreRecordIningSt;
			}
			else
			{
				SocreRecordTarget = "top_" + ScoreRecordIningSt;
			}
			int get_scoreInt;
			if (!Int32.TryParse(get_score, out get_scoreInt))
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
							ining_score: get_scoreInt,
							update_date: DateTime.Now
							);
		}


		private void nextBatter(bool flg = false)
		{
			if(!flg)
			{
				BoxData.updateRecord(
							box_id: box_id,
							player_id: selectedPlayerId,
							team_id: selectedTeamId,
							pitcher_id: selectedPitcherPlayerId,
							pit_team_id: selectedPitcherTeamId,
							pit_hand_id: PitcherHandId,
							cat_id: selectedCatcherPlayerId,
							game_box_num: game_box_num,
							ump_id: ump_id,
							game_id: game_id,
							park_id: park_id,
							weather_id: weather_id,
							runner_1: game[0].run_1,
							runner_2: game[0].run_2,
							runner_3: game[0].run_3,
							runner_1_player_id: game[0].run_1_player_id,
							runner_2_player_id: game[0].run_2_player_id,
							runner_3_player_id: game[0].run_3_player_id,
							top_bot: top_btmFlg,
							last_ball_speed: ballSpeedEntry,
							last_ball_type: ball_type,
							ball_box_num: ball_box_num,
							ball_total_num: ball_total_num,
							top_score: topScore,
							bottom_score: btmScore,
							update_date: DateTime.Now);
			}
			oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);

			Clear_Ball();       // 記載したボールをクリアする
			ball_count = 0;
			box_id++;

			count_b = 0;
			count_s = 0;
			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);

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
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						ining_box_id: IningBoxId
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
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						ining_box_id: IningBoxId
					);
			}

			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
									game_id: game_id,
									ining: Convert.ToInt32(game[0].ining),
									top_btmFlg: top_btmFlg);
			if (count_o > 2)
			{
				ChangeTopBtn();
			}
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						ining_box_id: IningBoxId
						);
			DataInitialize();
			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
									game_id: game_id,
									ining: Convert.ToInt32(game[0].ining),
									top_btmFlg: top_btmFlg);
		}

		/// <summary>
		/// 攻守交代フラグを実行する
		/// </summary>
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
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
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
					run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
					ining_box_id: 1
					);
			}
			IningBoxId = 1; // イニング内打席順を初期化

			game_box_num = BoxData.GetRecordsGameBoxCount(
											game_id: game_id,
											player_id: selectedPlayerId
											)[0].box_count;
			BoxData.updateRecord(
						box_id: box_id, etc_cd1: 1,
						runner_1: game[0].run_1,
						runner_2: game[0].run_2,
						runner_3: game[0].run_3,
						runner_1_player_id: game[0].run_1_player_id,
						runner_2_player_id: game[0].run_2_player_id,
						runner_3_player_id: game[0].run_3_player_id,
						top_bot: top_btmFlg,
						game_box_num: game_box_num,
						ball_box_num: ball_box_num,
						ball_total_num: ball_total_num,
						last_ball_speed: ballSpeedEntry,
						last_ball_type: ball_type,
						park_id: park_id,
						ump_id: ump_id,
						weather_id: weather_id,
						update_date: DateTime.Now
						);
			Ining_Change();
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

			if (IningScore == 0)
			{
				ScoreRecord(IningScore.ToString());  // 計スコアを記帳する
			}
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3
						);
		}


		private void btmIningScoreUpdate(string ining = "1")
		{
			if (IningScore == 0)
			{
				ScoreRecord(IningScore.ToString());  // 計スコアを記帳する
			}

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id
						);
		}


		private void Ining_Change()
		{
			count_o = 0;
			countUp();
			runner_1 = false;
			runner_2 = false;
			runner_3 = false;
			runner_1_player_id = 0;
			runner_2_player_id = 0;
			runner_3_player_id = 0;
			run_1.IsChecked = runner_1;
			run_2.IsChecked = runner_2;
			run_3.IsChecked = runner_3;
			IningScore = 0;
			IningScore_tmp = 0;
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						ining_box_id: 1
						);
		}

		private void GameSetCheck(int game_id = 0)
		{
			GameData.gameData gameSet = GameData.GetRecords(game_id)[0];
			/// 9回から12回までのイニングを対象
			if (Convert.ToInt32(gameSet.ining) >= 9
				&& Convert.ToInt32(gameSet.ining) < 12)
			{
				if (Convert.ToInt32(gameSet.ining) == 9)
				{
					GameSetCheckNine(gameSet);
				}

				if (Convert.ToInt32(gameSet.ining) == 10)
				{
					GameSetCheckTen(gameSet);
				}

				if (Convert.ToInt32(gameSet.ining) == 11)
				{
					GameSetCheckEleven(gameSet);
				}

				if (Convert.ToInt32(gameSet.ining) == 12)
				{
					GameSetCheckTwelve(gameSet);
				}
			}
		}

		private void GameSetCheckNine(GameData.gameData gameSet)
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

		private void GameSetCheckTen(GameData.gameData gameSet) 
		{
			if (gameSet.top_10.Length != 0
					&& gameSet.btm_10.Length == 0)
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

		private void GameSetCheckEleven(GameData.gameData gameSet) 
		{
			if (gameSet.top_11.Length != 0
					&& gameSet.btm_11.Length == 0)
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

		private void GameSetCheckTwelve(GameData.gameData gameSet) 
		{
			if (gameSet.top_12.Length != 0
					&& gameSet.btm_12.Length == 0)
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
			else if (gameSet.top_12.Length != 0 && gameSet.btm_12.Length != 0)
			{
				if (Convert.ToInt32(gameSet.top_total_score)
					== Convert.ToInt32(gameSet.btm_total_score))
				{
					/// 引き分け処理
					GameSetRtn();
				}
			}
		}


		private void GameSetRtn()
		{
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
			dialog.PrimaryButtonText = "トップ画面";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				Frame.Navigate(typeof(MainPage));
			}
			else if (result == ContentDialogResult.None)
			{
				//Frame.Navigate(typeof(MainPage));
			}
		}


		private void InPlayAction()
		{
			Frame.Navigate(typeof(FieldPage));
		}

		private void Type_Item_Tapped(object sender, RoutedEventArgs e)
		{
			var btn = (Button)sender;
			object[] btns = { type0_btn, type1_btn, type2_btn, type3_btn, type4_btn, type5_btn };
			foreach (Button _btn in btns)
			{
				_btn.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			switch (btn.Name)
			{
				case "type0_btn":
					ball_type = 0;
					break;
				case "type1_btn":
					ball_type = 1;
					break;
				case "type2_btn":
					ball_type = 2;
					break;
				case "type3_btn":
					ball_type = 3;
					break;
				case "type4_btn":
					ball_type = 4;
					break;
				case "type5_btn":
					ball_type = 5;
					break;
			}
			btn.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
		}

		private void Type_Item_Load(object sender, RoutedEventArgs e)
		{
			type0_btn.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
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
					GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 1);
					Frame.Navigate(typeof(MainPage));
					break;
				case "Data":
					GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 1);
					Frame.Navigate(typeof(DataAnalysisPage));
					break;
			}
		}


		private void Runner_Item_Clicked(object sender, RoutedEventArgs e)
		{
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: Convert.ToBoolean(run_1.IsChecked),
						run_2: Convert.ToBoolean(run_2.IsChecked),
						run_3: Convert.ToBoolean(run_3.IsChecked),
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id
						);
			BoxData.updateRecord(
						box_id: box_id,
						runner_1: game[0].run_1,
						runner_2: game[0].run_2,
						runner_3: game[0].run_3,
						runner_1_player_id: game[0].run_1_player_id,
						runner_2_player_id: game[0].run_2_player_id,
						runner_3_player_id: game[0].run_3_player_id,
						top_bot: top_btmFlg,
						update_date: DateTime.Now
						);
		}

		/// <summary>
		/// 球速テキストボックス内の入力範囲してい
		/// 0 < Entry < 170
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BallSpeedText_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox str = (TextBox)sender;
			var srtText = str.Text;
			int resultInt;
			if (int.TryParse(srtText, out resultInt))
			{
				if (0 < resultInt && resultInt < 180)
				{
					BallSpeedText.Text = resultInt.ToString();
				}
				else { BallSpeedText.Text = ""; }
			}
			else { BallSpeedText.Text = ""; }
		}

		private void GamePlayingPlayerChangMsg_btn_Clicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(OrderListPage));
		}

		private void Runner_Unchecked(object sender, RoutedEventArgs e)
		{
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: Convert.ToBoolean(run_1.IsChecked),
						run_2: Convert.ToBoolean(run_2.IsChecked),
						run_3: Convert.ToBoolean(run_3.IsChecked),
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id
						);
			BoxData.updateRecord(
				box_id: box_id,
				runner_1: game[0].run_1,
					runner_2: game[0].run_2,
					runner_3: game[0].run_3,
					runner_1_player_id: game[0].run_1_player_id,
					runner_2_player_id: game[0].run_2_player_id,
					runner_3_player_id: game[0].run_3_player_id,
				update_date: DateTime.Now
				);

		}

		private void Runner_Play_btn_Click(object sender, RoutedEventArgs e)
		{
			GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 1);
			Frame.Navigate(typeof(FieldPage));
		}

		private void CommentDataUpdate(bool flg = false)
		{
			if (flg)
			{
				PlayerData.updateCmntRecord(
								team_id: selectedPitcherTeamId,
								player_id: selectedPitcherPlayerId,
								cmnt1: PitcherDataCommentTextBox.Text,
								update_date: DateTime.Now);
			}
			else
			{
				PlayerData.updateCmntRecord(
								team_id: selectedTeamId,
								player_id: selectedPlayerId,
								cmnt1: BatterDataCommentTextBox.Text,
								update_date: DateTime.Now);
			}
		}

		private void Cmnt_btn_Click(object sender, RoutedEventArgs e)
		{
			bool flg = false;
			Button tb = (Button)sender;
			if (tb.Name.Contains("Pitcher")) { flg = true; }
			CommentDataUpdate(flg);
		}

		private void BallScoreListView_Button_Click(object sender, RoutedEventArgs e)
		{
			//Button btn = (Button)sender;
			flyout_btn = (Button)sender;
			int tmp_game_id = game_id;
			int tmp_box_id = box_id;
			Flyout.ShowAttachedFlyout(sender as FrameworkElement);
		}

		private void BallScoreListView_FlyoutItem_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem flyout = (MenuFlyoutItem)sender;
			BallData.ballScoreList flyout_ball_list = (BallData.ballScoreList)flyout_btn.DataContext;
			switch (flyout.Text)
			{
				case "修正":
					/// 更新処理
					BallSetFix(flyout_ball_list.ball_id);
					ball_flg = false;
					break;

				case "削除":
					/// 削除処理
					/// カウントの修正処理
					BallSetReplay(flyout_ball_list.ball_id);
					ball_flg = false;
					break;
			}

		}

		private void DeletaBallAction_Count(int delete_ball_action)
		{
			switch (delete_ball_action)
			{
				case 0:
					count_b--;
					break;
				case 1:
				case 2:
					count_s--;
					break;
			}

		}

		private void BoxBallZone_Replace(int box_id)
		{
			List<BallData.ballData> zone_ballData = BallData.GetRecordsBoxId(box_id);
			foreach (BallData.ballData ball_row in zone_ballData)
			{
				Zone_Image_Replace(
					x: ball_row.ball_x,
					y: ball_row.ball_y,
					tmp_ball_type: ball_row.ball_type,
					ball_action: ball_row.ball_action,
					ball_img: ball_row.ball_img
					);
			}
		}

		private void BallSetFix(int ball_id = 0)
		{
			del_ballData = BallData.GetRecordsBoxId(box_id);
			tmp_ballData = BallData.GetRecordsBoxId(box_id);
			/// 更新処理
			int tmpSpeedText;
			if (BallSpeedText.Text != "")
			{
				tmpSpeedText = Convert.ToInt32(BallSpeedText.Text);
			}
			else { tmpSpeedText = 0; }

			foreach (BallData.ballData ball_row in del_ballData)
			{
				BallData.delRecord(ball_row.ball_id);
			}

			Delete_Ball();  // Zone内のボールをすべて削除
			int ball_box_count = 0;

			foreach (BallData.ballData ball_row in tmp_ballData)
			{
				ball_box_count++;
				ball_total_num = BallData.GetCountRecords(
										game_id: game_id,
										pitcher_id: selectedPitcherPlayerId)[0].ball_id;
				if (ball_row.ball_id == ball_id)
				{
					//BallData.ballData delete_balldata = ball_row;

					BallData.addRecord(
						player_id: ball_row.player_id,
						pitcher_id: ball_row.pitcher_id,
						team_id: ball_row.team_id,
						pit_team_id: ball_row.pit_team_id,
						pit_hand_id: ball_row.pit_hand_id,
						bat_id: ball_row.bat_id,
						ball_id: ball_row.ball_id,
						cat_id: ball_row.cat_id,
						ump_id: ball_row.ump_id,
						game_id: ball_row.game_id,
						park_id: ball_row.park_id,
						//weather_id:weather_id,
						game_box_num: ball_row.game_box_num,
						box_id: ball_row.box_id,
						//ball_total_num: ball_row.ball_total_num - 1,
						ball_total_num: ball_total_num,
						cource_table_id: ball_row.cource_table_id,
						ball_level: ball_row.ball_level,
						ball_action: ball_action,
						ball_box_num: ball_box_count,
						ball_speed: tmpSpeedText,
						count_b: ball_row.count_b,
						count_s: ball_row.count_s,
						count_o: ball_row.count_o,
						runner_1: ball_row.runner_1,
						runner_2: ball_row.runner_2,
						runner_3: ball_row.runner_3,
						ball_x: zone_x_int,
						ball_y: zone_y_int,
						ball_type: ball_type,
						etc_cd1: ball_action,
						etc_str5: ball_comment,
						ining: ball_row.ining,
						top_bot: ball_row.top_bot,
						update_date: DateTime.Now
						);
					Zone_Image_Replace(
						x: zone_x_int,
						y: zone_y_int,
						tmp_ball_type: ball_type,
						ball_action: ball_action,
						ball_img: ball_img
						);
				}
				else
				{
					BallData.addRecord(
							player_id: ball_row.player_id,
							pitcher_id: ball_row.pitcher_id,
							team_id: ball_row.team_id,
							pit_team_id: ball_row.pit_team_id,
							pit_hand_id: ball_row.pit_hand_id,
							bat_id: ball_row.bat_id,
							ball_id: ball_row.ball_id,
							cat_id: ball_row.cat_id,
							ump_id: ball_row.ump_id,
							game_id: ball_row.game_id,
							park_id: ball_row.park_id,
							//weather_id:weather_id,
							game_box_num: ball_row.game_box_num,
							box_id: ball_row.box_id,
							//ball_total_num: ball_row.ball_total_num - 1,
							ball_total_num: ball_total_num,
							cource_table_id: ball_row.cource_table_id,
							ball_level: ball_row.ball_level,
							ball_action: ball_row.ball_action,
							ball_box_num: ball_box_count,
							ball_speed: ball_row.ball_speed,
							count_b: ball_row.count_b,  // 状況別でのデータを取得するため 2022.02.13
							count_s: ball_row.count_s,
							count_o: ball_row.count_o,
							runner_1: ball_row.runner_1,
							runner_2: ball_row.runner_2,
							runner_3: ball_row.runner_3,
							ball_x: ball_row.ball_x,
							ball_y: ball_row.ball_y,
							ball_type: ball_row.ball_type,
							etc_cd1: ball_row.etc_cd1,
							etc_str5: ball_row.etc_str5,
							ining: ball_row.ining,
							top_bot: ball_row.top_bot,
							//update_date: ball_row.update_date
							update_date: DateTime.Now
							);
					Zone_Image_Replace(
						x: ball_row.ball_x,
						y: ball_row.ball_y,
						tmp_ball_type: ball_row.ball_type,
						ball_action: ball_row.ball_action,
						ball_img: ball_row.ball_img
						);
				}

			}


			CountExchang(box_id: box_id);
			GameCountUpdate();  // 変更したカウントをGameDataにも反映させる
			countDisplay();

			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
								box_id: box_id,
								hand: PitcherHandId,
								bat: BatterBatId
								);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
								game_id: game_id,
								ining: Convert.ToInt32(game[0].ining),
								//top_btmFlg: top_btmFlg
								top_btmFlg: game[0].top_btm_flg
								);
		}


		private void BallSetReplay(int ball_id = 0)
		{
			del_ballData = BallData.GetRecordsBoxId(box_id);
			tmp_ballData = BallData.GetRecordsBoxId(box_id);
			//BallData.delRecord();
			int delete_ball_action;
			foreach (BallData.ballData ball_row in del_ballData)
			{
				delete_ball_action = ball_row.ball_action;
				BallData.delRecord(ball_row.ball_id);
			}
			Delete_Ball();  // Zone内のボールをすべて削除
			int ball_box_count = 0;
			foreach (BallData.ballData ball_row in tmp_ballData)
			{
				if (ball_row.ball_id == ball_id)
				{
					BallData.ballData delete_balldata = ball_row;
					continue;
				}
				ball_total_num = BallData.GetCountRecords(
										game_id: game_id,
										pitcher_id: selectedPitcherPlayerId)[0].ball_id;
				ball_box_count++;
				Zone_Image_Replace(
					x: ball_row.ball_x,
					y: ball_row.ball_y,
					tmp_ball_type: ball_row.ball_type,
					ball_action: ball_row.ball_action,
					ball_img: ball_row.ball_img
					);
				BallData.addRecord(
						player_id: ball_row.player_id,
						pitcher_id: ball_row.pitcher_id,
						team_id: ball_row.team_id,
						pit_team_id: ball_row.pit_team_id,
						pit_hand_id: ball_row.pit_hand_id,
						bat_id: ball_row.bat_id,
						ball_id: ball_row.ball_id,
						cat_id: ball_row.cat_id,
						ump_id: ball_row.ump_id,
						game_id: ball_row.game_id,
						park_id: ball_row.park_id,
						//weather_id:weather_id,
						game_box_num: ball_row.game_box_num,
						box_id: ball_row.box_id,
						//ball_total_num: ball_row.ball_total_num - 1,
						ball_total_num: ball_total_num,
						cource_table_id: ball_row.cource_table_id,
						ball_level: ball_row.ball_level,
						ball_action: ball_row.ball_action,
						ball_box_num: ball_box_count,
						ball_speed: ball_row.ball_speed,
						count_b: ball_row.count_b,  // 状況別でのデータを取得するため 2022.02.13
						count_s: ball_row.count_s,
						count_o: ball_row.count_o,
						runner_1: ball_row.runner_1,
						runner_2: ball_row.runner_2,
						runner_3: ball_row.runner_3,
						ball_x: ball_row.ball_x,
						ball_y: ball_row.ball_y,
						ball_type: ball_row.ball_type,
						etc_cd1: ball_row.etc_cd1,
						etc_str5: ball_row.etc_str5,
						ining: ball_row.ining,
						top_bot: ball_row.top_bot,
						//update_date: ball_row.update_date
						update_date: DateTime.Now
						);
			}

			CountExchang(box_id: box_id);
			GameCountUpdate();  // 変更したカウントをGameDataにも反映させる
			countDisplay();

			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
								box_id: box_id,
								hand: PitcherHandId,
								bat: BatterBatId
								);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
								game_id: game_id,
								ining: Convert.ToInt32(game[0].ining),
								//top_btmFlg: top_btmFlg
								top_btmFlg: game[0].top_btm_flg
								);

		}

		private void BallTypeListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			BallType.ballType balltypes = (BallType.ballType)listView.SelectedItem;
			ball_type = balltypes.ball_type_id;
			ball_img = balltypes.ball_img;

		}

		private void BallTypeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void OutCountChange_Item_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem item = (MenuFlyoutItem)sender;
			int tmp_count_b = 0;
			int tmp_count_s = 0;
			int tmp_count_o = 0;
			switch (item.Text)
			{

				case "0ボール":
					tmp_count_b = 0;
					tmp_count_s = count_s;
					tmp_count_o = count_o;
					break;
				case "1ボール":
					tmp_count_b = 1;
					tmp_count_s = count_s;
					tmp_count_o = count_o;
					break;
				case "2ボール":
					tmp_count_b = 2;
					tmp_count_s = count_s;
					tmp_count_o = count_o;
					break;
				case "3ボール":
					tmp_count_b = 3;
					tmp_count_s = count_s;
					tmp_count_o = count_o;
					break;

				case "0ストライク":
					tmp_count_b = count_b;
					tmp_count_s = 0;
					tmp_count_o = count_o;
					break;
				case "1ストライク":
					tmp_count_b = count_b;
					tmp_count_s = 1;
					tmp_count_o = count_o;
					break;
				case "2ストライク":
					tmp_count_b = count_b;
					tmp_count_s = 2;
					tmp_count_o = count_o;
					break;

				case "0アウト":
					tmp_count_b = count_b;
					tmp_count_s = count_s;
					tmp_count_o = 0;
					break;
				case "1アウト":
					tmp_count_b = count_b;
					tmp_count_s = count_s;
					tmp_count_o = 1;
					break;
				case "2アウト":
					tmp_count_b = count_b;
					tmp_count_s = count_s;
					tmp_count_o = 2;
					break;
				case "チェンジ":
					IningChangeDialog();
					return;

				//break;
				default:
					break;
			}
			GameData.updateRecord(
						game_id: game_id,
						count_b: tmp_count_b,
						count_s: tmp_count_s,
						count_o: tmp_count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						player_id: selectedPlayerId,
						top_btm_flg: top_btmFlg
						);
			GameData.gameData game_count = GameData.GetRecords(game_id)[0];
			count_b = game_count.count_b;
			count_s = game_count.count_s;
			count_o = game_count.count_o;
			countUp();
		}


		private void GameCountUpdate()
		{
			GameData.updateRecord(
					game_id: game_id,
					top_btm_flg: top_btmFlg,
					count_b: count_b,
					count_s: count_s,
					count_o: count_o,
					run_1: runner_1,
					run_2: runner_2,
					run_3: runner_3,
					run_1_player_id: runner_1_player_id,
					run_2_player_id: runner_2_player_id,
					run_3_player_id: runner_3_player_id
					);
		}



		/// <summary>
		/// カウントをball_actionによって再出力する
		/// </summary>
		/// <param name="box_id"></param>
		private void CountExchang(int box_id = 0)
		{
			List<BallData.ballData> ballcount = BallData.GetRecordsBoxId(box_id);
			count_b = 0;
			count_s = 0;
			foreach (BallData.ballData ball in ballcount)
			{
				switch (ball.ball_action)
				{
					case 0:
						count_b++;
						break;
					case 1:
						count_s++;
						break;
					case 2:
						count_s++;
						break;
					case 3:
						if (count_s < 2)
						{
							count_s++;
						}
						break;
					case 4:  // インプレー
					case 5:  // けん制
					case 6:  // 空白
						break;

				}
			}

		}

		private void BatterIdChange_Item_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem item = (MenuFlyoutItem)sender;
			switch (item.Text)
			{
				case "次の打者へ":
					NextSkipBatter();
					break;
				case "前の打者へ":
					ReturnSkipBatter();
					break;
				default:
					break;
			}
			DataInitialize();
			Delete_Ball();
		}



		private async void IningChangeDialog()
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "イニング交代";
			dialog.Content = "イニングチェンジをしますか";
			dialog.PrimaryButtonText = "OK";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				GameData.updateRecord(
						game_id: game_id,
						count_b: count_b,
						count_s: count_s,
						count_o: 3,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						player_id: selectedPlayerId,
						top_btm_flg: top_btmFlg
						);
				GameData.gameData game_count = GameData.GetRecords(game_id)[0];
				count_b = game_count.count_b;
				count_s = game_count.count_s;
				count_o = game_count.count_o;
				countUp();

				DataInitialize();
				Delete_Ball();
				BallScoreListView.ItemsSource
					= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId
									);
				BoxScoreListView.ItemsSource
					= BoxData.GetRecordsIningBoxs(
									game_id: game_id,
									ining: Convert.ToInt32(game[0].ining),
									//top_btmFlg: top_btmFlg
									top_btmFlg: game[0].top_btm_flg
									);

			}

		}
		private void NextSkipBatter()
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
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						ining_box_id: IningBoxId
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
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						ining_box_id: IningBoxId
					);
			}
		}


		private void ReturnSkipBatter()
		{
			bool tmpFlg;
			int tmp_order_idInt;
			string tmp_order_id;
			if (!top_btmFlg)
			{
				tmpFlg = false;
				tmp_order_idInt = Convert.ToInt32(top_order_id) - 1;
				if (tmp_order_idInt == 0) { tmp_order_idInt = 9; }
				tmp_order_id = Convert.ToString(tmp_order_idInt);
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: tmpFlg,
						top_order_id: tmp_order_id,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						ining_box_id: IningBoxId
						);
			}
			else
			{
				tmpFlg = true;
				tmp_order_idInt = Convert.ToInt32(btm_order_id) - 1;
				if (tmp_order_idInt == 0) { tmp_order_idInt = 9; }
				tmp_order_id = Convert.ToString(tmp_order_idInt);
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: tmpFlg,
						btm_order_id: tmp_order_id,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						ining_box_id: IningBoxId
					);
			}
		}


		private void BallScoreListView_DragItemsComplete(ListViewBase sender, DragItemsCompletedEventArgs args)
		{

		}

		private void BallScoreListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
		{

		}

		private void BallScoreListView_Drop(object sender, DragEventArgs e)
		{

		}

		private void BallScoreListView_DropComplete(UIElement sender, DropCompletedEventArgs args)
		{

		}

		private void BoxScoreListView_Button_Click(object sender, RoutedEventArgs e)
		{
			flyout_btn = (Button)sender;
			Flyout.ShowAttachedFlyout(sender as FrameworkElement);
		}

		private bool ScoreBoardDisplayProgress_Check(bool top_btmFlg, int ining, string btn_name)
		{
			string[] btn_name_split = btn_name.Split('_');
			int btn_ining;
			if (!int.TryParse(btn_name_split[1], out btn_ining))
			{
				return false;
			}
			if (ining < btn_ining)
			{
				return false;
			}
			if (ining == btn_ining)
			{
				if (btn_name_split[0].Contains("top"))
				{
					if (!top_btmFlg)
					{
						return false;
					}
				}
				else
				{
					if (!top_btmFlg)
					{
						return false;
					}
				}
			}
			return true;
		}

		private async void DontFixScoreDialog()
		{

			ContentDialog dialog = new ContentDialog();
			dialog.Title = "注意";
			dialog.Content = "修正はできません";
			dialog.CloseButtonText = "閉じる";
			dialog.DefaultButton = ContentDialogButton.Close;
			//var result = await dialog.ShowAsync();
			await dialog.ShowAsync();

		}


		private void ScoreBoardDisplay_Button_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;

			GameData.gameData fromGame = GameData.GetRecords(game_id: game_id)[0];
			BoxData.boxDataIning dataIning = new BoxData.boxDataIning();
			if (!ScoreBoardDisplayProgress_Check(top_btmFlg: fromGame.top_btm_flg, ining: Convert.ToInt32(fromGame.ining), btn_name: btn.Name))
			{
				/// 修正処理へ遷移しない
				DontFixScoreDialog();
				return;
			}
			string[] btn_name_split = btn.Name.Split('_');
			string target_flg = btn_name_split[0];
			bool ScoreBoardDisplayProgress_TopBtmFlg = false;
			if (target_flg.Contains("btm"))
			{
				ScoreBoardDisplayProgress_TopBtmFlg = true;
			}
			int target_ining = Convert.ToInt32(btn_name_split[1]);

			TmpGameData.updateRecord(
						game_id: fromGame.game_id,
						count_b: fromGame.count_b,
						count_s: fromGame.count_s,
						count_o: fromGame.count_o,
						run_1: fromGame.run_1,
						run_2: fromGame.run_2,
						run_3: fromGame.run_3,
						run_1_player_id: fromGame.run_1_player_id,
						run_2_player_id: fromGame.run_2_player_id,
						run_3_player_id: fromGame.run_3_player_id,
						ining: fromGame.ining,
						ining_box_id: fromGame.ining_box_id,
						player_id: fromGame.player_id,
						top_btm_flg: fromGame.top_btm_flg,
						park_id: fromGame.park_id,
						ump_id: fromGame.ump_id,
						weather_id: fromGame.weather_id,
						bat_first_team_id: fromGame.bat_first_team_id,
						field_first_team_id: fromGame.field_first_team_id,
						top_order_id: fromGame.top_order_id,
						btm_order_id: fromGame.btm_order_id,
						top_1: fromGame.top_1,
						top_2: fromGame.top_2,
						top_3: fromGame.top_3,
						top_4: fromGame.top_4,
						top_5: fromGame.top_5,
						top_6: fromGame.top_6,
						top_7: fromGame.top_7,
						top_8: fromGame.top_8,
						top_9: fromGame.top_9,
						top_10: fromGame.top_10,
						top_11: fromGame.top_11,
						top_12: fromGame.top_12,
						top_total_score: fromGame.top_total_score,
						top_hit: fromGame.top_hit,
						top_error: fromGame.top_error,
						btm_1: fromGame.btm_1,
						btm_2: fromGame.btm_2,
						btm_3: fromGame.btm_3,
						btm_4: fromGame.btm_4,
						btm_5: fromGame.btm_5,
						btm_6: fromGame.btm_6,
						btm_7: fromGame.btm_7,
						btm_8: fromGame.btm_8,
						btm_9: fromGame.btm_9,
						btm_10: fromGame.btm_10,
						btm_11: fromGame.btm_11,
						btm_12: fromGame.btm_12,
						btm_total_score: fromGame.btm_total_score,
						btm_hit: fromGame.btm_hit,
						btm_error: fromGame.btm_error
						);
			try
			{
				dataIning = BoxData.GetRecordsIningLast(game_id: game_id, ining: target_ining, top_btmFlg: ScoreBoardDisplayProgress_TopBtmFlg)[0];
			}
			catch (Exception err) { Console.WriteLine(err.ToString()); }
			if (dataIning != null)
			{
				BackToIningsDialog(topbtmFlg: ScoreBoardDisplayProgress_TopBtmFlg, iningInt: target_ining, dataIning: dataIning);
			}
		}

		private void EntryBallResultRtn()
		{
			if (ball_flg)
			{
				ReSet_Ball_one();  // 一度セットしたボールを削除する
			}
			/// ボールアクションに応じた色を用いる
			Zone_Image_Replace(
				x: zone_x_int,
				y: zone_y_int,
				tmp_ball_type: ball_type,
				ball_action: ball_action,
				ball_img: ball_img
				);
			ballSpeedEntry = 0;
			if (BallSpeedText.Text.Length != 0)
			{
				ballSpeedEntry = Convert.ToInt32(BallSpeedText.Text);
			}
			old_game = GameData.GetRecords(game_id);
			GameData.gameData game_count = GameData.GetRecords(game_id)[0];
			int old_count_b = old_game[0].count_b;
			int old_count_s = old_game[0].count_s;
			int old_count_o = old_game[0].count_o;
			BallData.addRecord(
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				team_id: selectedTeamId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				bat_id: BatterBatId,
				cat_id: selectedCatcherPlayerId,
				ump_id: ump_id,
				game_id: game_id,
				park_id: park_id,
				game_box_num: game_box_num,
				box_id: box_id,
				ball_total_num: ball_total_num,
				cource_table_id: course_id,
				ball_level: 1,
				ball_action: ball_action,
				etc_cd1: ball_action,
				etc_str5: ball_comment,
				ball_box_num: ball_box_num,
				ball_speed: ballSpeedEntry,
				count_b: old_count_b,
				count_s: old_count_s,
				count_o: old_count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				ball_x: zone_x_int,
				ball_y: zone_y_int,
				ball_type: ball_type,
				steal: StealFlg,
				ining: Convert.ToInt32(Ining),
				in_play: inPlayFlg,
				top_bot: top_btmFlg,
				update_date: update_time
				);
			BoxData.updateRecord(
							box_id: box_id,
							player_id: selectedPlayerId,
							pitcher_id: selectedPitcherPlayerId,
							team_id: selectedTeamId,
							pit_team_id: selectedPitcherTeamId,
							pit_hand_id: PitcherHandId,
							bat_id: BatterBatId,
							ump_id: ump_id,
							cat_id: selectedCatcherPlayerId,
							game_id: game_id,
							game_box_num: game_box_num,
							ball_box_num: ball_box_num,
							ball_total_num: ball_total_num,
							last_ball_speed: ballSpeedEntry,
							last_ball_type: ball_type,
							count_b: old_game[0].count_b,
							count_s: old_game[0].count_s,
							count_o: old_game[0].count_o,
							ining: Convert.ToInt32(Ining),
							top_bot: top_btmFlg,
							runner_1: game[0].run_1,
							runner_2: game[0].run_2,
							runner_3: game[0].run_3,
							runner_1_player_id: game[0].run_1_player_id,
							runner_2_player_id: game[0].run_2_player_id,
							runner_3_player_id: game[0].run_3_player_id,
							update_date: update_time
							);
			GameData.updateRecord(
							game_id: game_id,
							top_btm_flg: top_btmFlg,
							count_b: count_b,
							count_s: count_s,
							count_o: count_o,
							run_1: runner_1,
							run_2: runner_2,
							run_3: runner_3,
							run_1_player_id: runner_1_player_id,
							run_2_player_id: runner_2_player_id,
							run_3_player_id: runner_3_player_id
							);

			game_count = GameData.GetRecords(game_id)[0];
			count_b = game_count.count_b;
			count_s = game_count.count_s;
			count_o = game_count.count_o;
			countUp();
			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
								box_id: box_id,
								hand: PitcherHandId,
								bat: BatterBatId
								);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
								game_id: game_id,
								ining: Convert.ToInt32(game[0].ining),
								//top_btmFlg: top_btmFlg
								top_btmFlg: game[0].top_btm_flg
								);
			BallSpeedText.Text = "";        // 投球入力後は球速テキストを空白にする
			inPlayFlg = false;
			ball_flg = false;
		}


		private void EntryBallResult_Button_Click(object sender, RoutedEventArgs e)
		{
			ballSpeedEntry = 0;
			if (BallSpeedText.Text.Length != 0)
			{
				ballSpeedEntry = Convert.ToInt32(BallSpeedText.Text);
			}
			old_game = GameData.GetRecords(game_id);
			GameData.gameData game_count = GameData.GetRecords(game_id)[0];
			int old_count_b = old_game[0].count_b;
			int old_count_s = old_game[0].count_s;
			int old_count_o = old_game[0].count_o;
			BallData.addRecord(
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				team_id: selectedTeamId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				bat_id: BatterBatId,
				cat_id: selectedCatcherPlayerId,
				ump_id: ump_id,
				game_id: game_id,
				park_id: park_id,
				game_box_num: game_box_num,
				box_id: box_id,
				ball_total_num: ball_total_num,
				cource_table_id: course_id,
				ball_level: 1,
				ball_action: ball_action,
				etc_cd1: ball_action,
				etc_str5: ball_comment,
				ball_box_num: ball_box_num,
				ball_speed: ballSpeedEntry,
				count_b: old_count_b,
				count_s: old_count_s,
				count_o: old_count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				ball_x: zone_x_int,
				ball_y: zone_y_int,
				ball_type: ball_type,
				ining: Convert.ToInt32(Ining),
				top_bot: top_btmFlg,
				update_date: update_time
				);
			BoxData.updateRecord(
							box_id: box_id,
							player_id: selectedPlayerId,
							pitcher_id: selectedPitcherPlayerId,
							team_id: selectedTeamId,
							pit_team_id: selectedPitcherTeamId,
							pit_hand_id: PitcherHandId,
							bat_id: BatterBatId,
							ump_id: ump_id,
							cat_id: selectedCatcherPlayerId,
							game_id: game_id,
							game_box_num: game_box_num,
							ball_box_num: ball_box_num,
							ball_total_num: ball_total_num,
							last_ball_speed: ballSpeedEntry,
							last_ball_type: ball_type,
							count_b: old_game[0].count_b,
							count_s: old_game[0].count_s,
							count_o: old_game[0].count_o,
							ining: Convert.ToInt32(Ining),
							top_bot: top_btmFlg,
							runner_1: game[0].run_1,
							runner_2: game[0].run_2,
							runner_3: game[0].run_3,
							runner_1_player_id: game[0].run_1_player_id,
							runner_2_player_id: game[0].run_2_player_id,
							runner_3_player_id: game[0].run_3_player_id,
							update_date: update_time
							);
			GameData.updateRecord(
							game_id: game_id,
							top_btm_flg: top_btmFlg,
							count_b: count_b,
							count_s: count_s,
							count_o: count_o,
							run_1: runner_1,
							run_2: runner_2,
							run_3: runner_3,
							run_1_player_id: runner_1_player_id,
							run_2_player_id: runner_2_player_id,
							run_3_player_id: runner_3_player_id
							);


			game_count = GameData.GetRecords(game_id)[0];
			count_b = game_count.count_b;
			count_s = game_count.count_s;
			count_o = game_count.count_o;
			countUp();
			BallScoreListView.ItemsSource = BallData.GetScoreDisplayRecords(box_id: box_id, hand: PitcherHandId, bat: BatterBatId);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
								game_id: game_id,
								ining: Convert.ToInt32(game[0].ining),
								//top_btmFlg: top_btmFlg
								top_btmFlg: game[0].top_btm_flg
								);
			BallSpeedText.Text = "";        // 投球入力後は球速テキストを空白にする

			ball_flg = false;

		}


		private async void NextBatterDialog()
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "確認";
			dialog.Content = "打席結果を登録しますか";
			dialog.PrimaryButtonText = "OK";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			bool flg = false;
			if (result == ContentDialogResult.Primary)
			{
				if (count_s == 3)
				{
					StrikeOut();        // BallDataに三振を記録する
					count_o++;
				}
				else if (count_b == 4)
				{
					walksResult();      // 四球
					WalkOrDeadResult();
					flg = true;
				}
				else if (dead_flg)
				{
					DeadResult();
					WalkOrDeadResult();
					dead_flg = false;
					flg = true;
				}
				nextBatter(flg);
				countDisplay();
			}
			else
			{
				if (dead_flg)
				{
					ball_flg = true;
					dead_flg = false;
				}
			}
		}

		private async void BackToIningsDialog(bool topbtmFlg = false, int iningInt = -1, BoxData.boxDataIning dataIning = null)
		{
			if (iningInt < 0) { return; }
			string topbtmST = "表";
			if (topbtmFlg) { topbtmST = "裏"; }
			string commentST = String.Format("{0}回の{1}を修正しますか", iningInt.ToString(), topbtmST);
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "確認";
			dialog.Content = commentST;
			dialog.PrimaryButtonText = "OK";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				BackToIningsGameDataUpdate(topbtmFlg: topbtmFlg, iningInt: iningInt, dataIning: dataIning);
				BackToIningsDataInitialize(topbtmFlg: topbtmFlg, iningInt: iningInt, dataIning: dataIning);
			}
		}

		private void BackToIningsGameDataUpdate(bool topbtmFlg = false, int iningInt = -1, BoxData.boxDataIning dataIning = null)
		{
			if (!topbtmFlg)
			{
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: topbtmFlg,
						ining: iningInt.ToString(),
						count_b: dataIning.count_b,
						count_s: dataIning.count_s,
						count_o: dataIning.count_o,
						run_1: dataIning.runner_1,
						run_2: dataIning.runner_2,
						run_3: dataIning.runner_3,
						run_1_player_id: dataIning.runner_1_player_id,
						run_2_player_id: dataIning.runner_2_player_id,
						run_3_player_id: dataIning.runner_3_player_id,
						player_id: dataIning.player_id,
						ining_box_id: dataIning.ining_box_id,
						top_order_id: dataIning.order_id.ToString()
						);
			}
			else
			{
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: topbtmFlg,
						ining: iningInt.ToString(),
						count_b: dataIning.count_b,
						count_s: dataIning.count_s,
						count_o: dataIning.count_o,
						run_1: dataIning.runner_1,
						run_2: dataIning.runner_2,
						run_3: dataIning.runner_3,
						run_1_player_id: dataIning.runner_1_player_id,
						run_2_player_id: dataIning.runner_2_player_id,
						run_3_player_id: dataIning.runner_3_player_id,
						player_id: dataIning.player_id,
						ining_box_id: dataIning.ining_box_id,
						btm_order_id: dataIning.order_id.ToString()
						);
			}
		}

		private void BackToIningsDataInitialize(bool topbtmFlg = false, int iningInt = -1, BoxData.boxDataIning dataIning = null)
		{
			if (iningInt < 0) { return; }
			this.InitializeComponent();
			BallData.InitializeDB();
			BallAction.InitializeDB();
			BallType.InitializeDB();
			BoxData.InitializeDB();
			BallCollor.InitializeDB();
			BallCourse.InitializeDB();
			game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);
			box_id = dataIning.box_id;
			count_b = dataIning.count_b;
			count_s = dataIning.count_s;
			count_o = dataIning.count_o;

			runner_1 = dataIning.runner_1;
			runner_2 = dataIning.runner_2;
			runner_3 = dataIning.runner_3;

			runner_1_player_id = dataIning.runner_1_player_id;
			runner_2_player_id = dataIning.runner_2_player_id;
			runner_3_player_id = dataIning.runner_3_player_id;

			Ining = game[0].ining;

			/// ver 1.1.2.0 イニングの得点を打席データから取得する
			ScoreRecord(get_score: dataIning.ining_score.ToString());

			BoxData.updateRecord(
							box_id: dataIning.box_id,
							count_o: dataIning.count_o,
							runner_1: game[0].run_1,
							runner_2: game[0].run_2,
							runner_3: game[0].run_3,
							runner_1_player_id: game[0].run_1_player_id,
							runner_2_player_id: game[0].run_2_player_id,
							runner_3_player_id: game[0].run_3_player_id,
							top_bot: topbtmFlg,
							ining: Convert.ToInt32(game[0].ining),
							update_date: DateTime.Now);

			RunnerResetPlace(dataIning);

			//IningBoxId = iningInt;
			IningBoxId = game[0].ining_box_id;
			IningBoxId = BoxData.GetRecordsIningBoxsCount(game_id: game_id, ining: Convert.ToInt32(game[0].ining), top_btmFlg: game[0].top_btm_flg)[0].box_count;
			teams = TeamData.GetRecords();

			string topTeamName = teams.Find(x => x.team_id == game[0].bat_first_team_id).teamName;
			string btmTeamName = teams.Find(x => x.team_id == game[0].field_first_team_id).teamName;
			top_players = PlayerData.GetRecords(team_id: game[0].bat_first_team_id, selected: 0);
			btm_players = PlayerData.GetRecords(team_id: game[0].field_first_team_id, selected: 0);
			game[0].top_teamName = topTeamName;
			game[0].btm_teamName = btmTeamName;
			top_order_id = game[0].top_order_id;
			btm_order_id = game[0].btm_order_id;

			topScore = Convert.ToInt32(game[0].top_total_score);
			btmScore = Convert.ToInt32(game[0].btm_total_score);

			ump_id = game[0].ump_id;
			weather_id = game[0].weather_id;
			park_id = game[0].park_id;


			if (dataIning.runner_1 || dataIning.runner_2 || dataIning.runner_3)
			{
				Runner_Play_btn.Visibility = Visibility.Visible;
				PickOff_btn.Visibility = Visibility.Visible;
			}
			else
			{
				Runner_Play_btn.Visibility = Visibility.Collapsed;
				PickOff_btn.Visibility = Visibility.Collapsed;
			}

			top_btmFlg = topbtmFlg;   // 裏表フラグの取得
			if (!top_btmFlg)
			{
				/// ver.3.0.0.0.
				PitcherDataTextBlock.Text = btm_players.Find(x => x.position == 1).name;
				PitcherHandId = PitcherHandIdGet(btm_players.Find(x => x.position == 1).hand);
				selectedPitcherPlayerId = btm_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = btm_players.Find(x => x.position == 2).player_id;

				selectedPlayerId = top_players.Find(x => x.etc_str2 == top_order_id).player_id;
				BatterDataTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).name;
				BatterDataOrderIdTextBlock.Text = top_players.Find(x => x.etc_str2 == top_order_id).etc_str2;
				BatterBatId = BatterBatIdGet(top_players.Find(x => x.etc_str2 == top_order_id).bat);
				selectedTeamId = game[0].bat_first_team_id;
				selectedPitcherTeamId = game[0].field_first_team_id;
				PitcherTeamNameTextBlock.Text = btmTeamName;
				BatterTeamNameTextBlock.Text = topTeamName;

				/// ver.1.0.9.0以降
				BatterDataCommentTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).cmnt1;
				BatterDataBatTextBox.Text = top_players.Find(x => x.etc_str2 == top_order_id).bat;

				/// ver.3.0.0.0.
				PitcherDataCommentTextBox.Text = btm_players.Find(x => x.position == 1).cmnt1;
				PitcherDataHandTextBox.Text = btm_players.Find(x => x.position == 1).hand;
			}
			else
			{
				/// ver.3.0.0.0.
				PitcherDataTextBlock.Text = top_players.Find(x => x.position == 1).name;
				PitcherHandId = PitcherHandIdGet(top_players.Find(x => x.position == 1).hand);
				selectedPitcherPlayerId = top_players.Find(x => x.position == 1).player_id;
				selectedCatcherPlayerId = top_players.Find(x => x.position == 2).player_id;


				BatterDataTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).name;
				BatterDataOrderIdTextBlock.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).etc_str2;
				selectedPlayerId = btm_players.Find(x => x.etc_str2 == btm_order_id).player_id;
				BatterBatId = BatterBatIdGet(btm_players.Find(x => x.etc_str2 == btm_order_id).bat);


				selectedTeamId = game[0].field_first_team_id;
				selectedPitcherTeamId = game[0].bat_first_team_id;
				PitcherTeamNameTextBlock.Text = topTeamName;
				BatterTeamNameTextBlock.Text = btmTeamName;

				/// ver.1.0.9.0以降
				BatterDataCommentTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).cmnt1;

				BatterDataBatTextBox.Text = btm_players.Find(x => x.etc_str2 == btm_order_id).bat;

				/// ver.3.0.0.0.
				PitcherDataCommentTextBox.Text = top_players.Find(x => x.position == 1).cmnt1;
				PitcherDataHandTextBox.Text = top_players.Find(x => x.position == 1).hand;
			}
			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_1: runner_1,
						run_2: runner_2,
						run_3: runner_3,
						run_1_player_id: runner_1_player_id,
						run_2_player_id: runner_2_player_id,
						run_3_player_id: runner_3_player_id,
						player_id: selectedPlayerId,
						park_id: park_id,
						ump_id: ump_id,
						weather_id: weather_id
						);
			ScoreBoardDisplay.ItemsSource = game;

			BallTypeListView.ItemsSource = BallType.GetRecordsBallType(hand: PitcherHandId);

			BallScoreListView.ItemsSource = BallData.GetScoreDisplayRecords(box_id: box_id, hand: PitcherHandId, bat: BatterBatId);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
								game_id: game_id,
								ining: Convert.ToInt32(game[0].ining),
								//top_btmFlg: top_btmFlg
								top_btmFlg: game[0].top_btm_flg
								);
			Delete_Ball();
			BoxBallZone_Replace(box_id: box_id);

			CountExchang(box_id: box_id);
			GameCountUpdate();  // 変更したカウントをGameDataにも反映させる

			countDisplay();
		}

		private void BallCountInitialize(bool flg = false)
		{
			if (flg)
			{
				count_o = 0;
			}
			count_b = 0;
			count_s = 0;
		}


		private void BoxScoreListView_FlyoutItem_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem flyout = (MenuFlyoutItem)sender;
			BoxData.boxDataIning flyout_ball_list = (BoxData.boxDataIning)flyout_btn.DataContext;
			switch (flyout.Text)
			{
				case "修正":
					BackToBox_Item_Flyout(ining_box_id: flyout_ball_list.ining_box_id);

					break;
				case "削除":
					BoxDataDelete(tmp_box_id: flyout_ball_list.box_id);
					IningBoxId = BoxData.GetRecordsIningBoxsCount(game_id: game_id, ining: Convert.ToInt32(game[0].ining), top_btmFlg: game[0].top_btm_flg)[0].box_count;
					break;
				default:
					break;

			}
			countDisplay();

			BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
								box_id: box_id,
								hand: PitcherHandId,
								bat: BatterBatId
								);
			BoxScoreListView.ItemsSource
				= BoxData.GetRecordsIningBoxs(
									game_id: game_id,
									ining: Convert.ToInt32(game[0].ining),
									//top_btmFlg: top_btmFlg
									top_btmFlg: game[0].top_btm_flg
									);
		}
		private void BoxDataDelete(int tmp_box_id = 0)
		{
			BoxData.deleteRecord(box_id: tmp_box_id);
			BallData.deleteRecordBoxId(box_id: tmp_box_id);
		}


		private void BackToBox_Item_Flyout(int ining_box_id = 0)
		{
			if (box_id == 0) { return; }
			if (ReSetBoxFlg)
			{
				DontFixScoreDialog();
			}
			else
			{
				BoxData.boxDataIning dataIning = new BoxData.boxDataIning();
				try
				{
					dataIning = BoxData.GetRecordsIningLast(game_id: game_id, ining: Convert.ToInt32(Ining), top_btmFlg: top_btmFlg, ining_box_id: ining_box_id)[0];
				}
				catch (Exception err)
				{
					Console.WriteLine(err.ToString());
				}

				try
				{
					BoxData.deleteRecord(box_id: box_id);
				}
				catch (Exception err)
				{
					Console.WriteLine(err.ToString());
				}

				if (dataIning != null)
				{
					BackToBoxGameDataUpdate(topbtmFlg: top_btmFlg, iningInt: Convert.ToInt32(Ining), dataIning: dataIning);
					BackToIningsDataInitialize(topbtmFlg: top_btmFlg, iningInt: Convert.ToInt32(Ining), dataIning: dataIning);
				}
			}
			ReSetBoxFlg = true;

		}

		private void BackToBoxGameDataUpdate(bool topbtmFlg = false, int iningInt = -1, BoxData.boxDataIning dataIning = null)
		{
			if (!topbtmFlg)
			{
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: topbtmFlg,
						ining: iningInt.ToString(),
						count_b: dataIning.count_b,
						count_s: dataIning.count_s,
						count_o: dataIning.count_o,
						run_1: dataIning.runner_1,
						run_2: dataIning.runner_2,
						run_3: dataIning.runner_3,
						run_1_player_id: dataIning.runner_1_player_id,
						run_2_player_id: dataIning.runner_2_player_id,
						run_3_player_id: dataIning.runner_3_player_id,
						player_id: dataIning.player_id,
						ining_box_id: dataIning.ining_box_id,
						top_order_id: dataIning.order_id.ToString()
						);
			}
			else
			{
				GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: topbtmFlg,
						ining: iningInt.ToString(),
						count_b: dataIning.count_b,
						count_s: dataIning.count_s,
						count_o: dataIning.count_o,
						run_1: dataIning.runner_1,
						run_2: dataIning.runner_2,
						run_3: dataIning.runner_3,
						run_1_player_id: dataIning.runner_1_player_id,
						run_2_player_id: dataIning.runner_2_player_id,
						run_3_player_id: dataIning.runner_3_player_id,
						player_id: dataIning.player_id,
						ining_box_id: dataIning.ining_box_id,
						btm_order_id: dataIning.order_id.ToString()
						);
			}
		}

		private void OtherAction_Item_Click(object sender, RoutedEventArgs e)
		{
			var selectFlyOutMenuItem = (MenuFlyoutItem)sender;
			bool flg = false;
			OtherActionDialog(item: selectFlyOutMenuItem, flg: flg);
		}


		private async void OtherActionDialog(MenuFlyoutItem item, bool flg = false)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "確認";
			dialog.Content = item.Text + "の結果を登録しますか";
			dialog.PrimaryButtonText = "OK";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				switch (item.Text)
				{
					case "申告敬遠":
						AvoidResult();
						WalkOrDeadResult();
						nextBatter(flg);
						countDisplay();
						break;
					case "打撃妨害":
						flg = true;
						InterFerenceResult();
						WalkOrDeadResult();
						nextBatter(flg);
						countDisplay();
						break;
				}
			}
			else
			{
				return;
			}
		}


		private void PickOff_Item_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem item = sender as MenuFlyoutItem;
			PickOffDialog(item);
		}

		private void PickOff_Item_SafeRtn(string name)
		{
			int pickOffBase = 0;
			ball_action = 5;
			course_id = 10;
			if (name.Contains("1"))
			{
				pickOffBase = 1;
				course_id = 11;
			}
			else if (name.Contains("2"))
			{
				pickOffBase = 2;
				course_id = 12;
			}
			else if (name.Contains("3"))
			{
				pickOffBase = 3;
				course_id = 13;
			}
			BallData.addRecord(
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				team_id: selectedTeamId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				bat_id: BatterBatId,
				ump_id: ump_id,
				cat_id: selectedCatcherPlayerId,
				game_id: game_id,
				//in_play: true,
				park_id: park_id,
				game_box_num: game_box_num,
				box_id: box_id,
				ball_total_num: ball_total_num,
				pick_off: pickOffBase,
				cource_table_id: course_id,
				//ball_level: 1,
				ball_action: ball_action,
				etc_cd1: ball_action,
				etc_str5: ball_comment,
				ball_box_num: ball_box_num,
				//ball_speed: ballSpeedEntry,
				count_b: old_game[0].count_b,
				count_s: old_game[0].count_s,
				count_o: old_game[0].count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				ball_x: -1,
				ball_y: -1,
				ball_type: ball_type,
				ining: Convert.ToInt32(Ining),
				top_bot: top_btmFlg,
				update_date: update_time
				);
			BallScoreListView.ItemsSource
					= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId
									);

		}


		/// <summary>
		/// けん制アウトの処理
		/// アウトのデータはpick_offを二桁数字にする
		/// </summary>
		/// <param name="name"></param>
		private void PickOff_Item_OutRtn(string name)
		{
			GameData.gameData pickOffGame = GameData.GetRecords(game_id)[0];
			int pickOffBase = 0;
			ball_action = 5;
			course_id = 10;

			if (name.Contains("1"))
			{
				pickOffBase = 11;
				course_id = 11;
				pickOffGame.run_1 = false;
				runner_1 = false;
				pickOffGame.run_1_player_id = 0;
				runner_1_player_id = 0;
				pickOffGame.count_o++;
			}
			else if (name.Contains("2"))
			{
				pickOffBase = 12;
				course_id = 12;
				pickOffGame.run_2 = false;
				runner_2 = false;
				pickOffGame.run_2_player_id = 0;
				runner_2_player_id = 0;
				pickOffGame.count_o++;
			}
			else if (name.Contains("3"))
			{
				pickOffBase = 13;
				course_id = 13;
				pickOffGame.run_3 = false;
				runner_3 = false;
				pickOffGame.run_3_player_id = 0;
				runner_3_player_id = 0;
				pickOffGame.count_o++;
			}
			BallData.addRecord(
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				team_id: selectedTeamId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				bat_id: BatterBatId,
				ump_id: ump_id,
				cat_id: selectedCatcherPlayerId,
				game_id: game_id,
				park_id: park_id,
				game_box_num: game_box_num,
				box_id: box_id,
				ball_total_num: ball_total_num,
				pick_off: pickOffBase,
				cource_table_id: course_id,
				//ball_level: 1,
				ball_action: ball_action,
				etc_cd1: ball_action,
				etc_str5: ball_comment,
				ball_box_num: ball_box_num,
				//ball_speed: ballSpeedEntry,
				count_b: old_game[0].count_b,
				count_s: old_game[0].count_s,
				count_o: old_game[0].count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				ball_x: -1,
				ball_y: -1,
				ball_type: ball_type,
				ining: Convert.ToInt32(Ining),
				top_bot: top_btmFlg,
				update_date: update_time
				);

			GameData.updateRecord(
						game_id: game_id,
						top_btm_flg: top_btmFlg,
						run_1: pickOffGame.run_1,
						run_2: pickOffGame.run_2,
						run_3: pickOffGame.run_3,
						run_1_player_id: pickOffGame.run_1_player_id,
						run_2_player_id: pickOffGame.run_2_player_id,
						run_3_player_id: pickOffGame.run_3_player_id,
						count_o: pickOffGame.count_o
						);
			count_o = pickOffGame.count_o;

			if (count_o > 2)
			{
				ChangeTopBtn();
				DataInitialize();
				Delete_Ball();
				BallScoreListView.ItemsSource
				= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId);
				BoxScoreListView.ItemsSource
					= BoxData.GetRecordsIningBoxs(
										game_id: game_id,
										ining: Convert.ToInt32(game[0].ining),
										top_btmFlg: top_btmFlg);
			}

			BallScoreListView.ItemsSource
					= BallData.GetScoreDisplayRecords(
									box_id: box_id,
									hand: PitcherHandId,
									bat: BatterBatId
									);

		}

		private void RunnerDisplaySet()
		{
			run_1.IsChecked = runner_1;
			run_2.IsChecked = runner_2;
			run_3.IsChecked = runner_3;
		}


		private async void PickOffDialog(MenuFlyoutItem item)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "確認";
			dialog.Content = "けん制結果を登録しますか";
			dialog.PrimaryButtonText = "OK";
			dialog.CloseButtonText = "NO";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				if (item.Name.Contains("Safe"))
				{

					PickOff_Item_SafeRtn(item.Name);
				}
				else if (item.Name.Contains("Out"))
				{

					PickOff_Item_OutRtn(item.Name);
				}
				else if (item.Name.Contains("Field"))
				{
					PickOff_Item_Field(item.Name);
				}
				RunnerDisplaySet();
				countDisplay();
			}
			else
			{
				return;
			}
		}



		private void PickOff_Item_Field(string name)
		{
			int pickOffBase = 0;
			ball_action = 5;
			course_id = 10;
			if (name.Contains("1"))
			{
				pickOffBase = 1;
				course_id = 11;
			}
			else if (name.Contains("2"))
			{
				pickOffBase = 2;
				course_id = 12;
			}
			else if (name.Contains("3"))
			{
				pickOffBase = 3;
				course_id = 13;
			}
			BallData.addRecord(
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				team_id: selectedTeamId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				bat_id: BatterBatId,
				ump_id: ump_id,
				cat_id: selectedCatcherPlayerId,
				game_id: game_id,
				//in_play: true,
				park_id: park_id,
				game_box_num: game_box_num,
				box_id: box_id,
				ball_total_num: ball_total_num,
				pick_off: pickOffBase,
				cource_table_id: course_id,
				ball_action: ball_action,
				etc_cd1: ball_action,
				etc_str5: ball_comment,
				ball_box_num: ball_box_num,
				count_b: old_game[0].count_b,
				count_s: old_game[0].count_s,
				count_o: old_game[0].count_o,
				runner_1: runner_1,
				runner_2: runner_2,
				runner_3: runner_3,
				ball_x: -1,
				ball_y: -1,
				ball_type: ball_type,
				ining: Convert.ToInt32(Ining),
				top_bot: top_btmFlg,
				update_date: update_time
				);
			GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 1);
			Frame.Navigate(typeof(FieldPage));
		}

		private void RunActionRtn()
		{
			GameData.updateRunPlayIdFlgRecord(game_id: game_id, run_play_id: 1);
			Frame.Navigate(typeof(FieldPage));
		}



		private void AttackChange_Item_Click(object sender, RoutedEventArgs e)
		{
			MenuFlyoutItem item = (MenuFlyoutItem)sender;
			int forward_back = 0;
			switch (item.Text)
			{
				case "次の攻撃へ":
					forward_back = 1;
					break;

				case "前の攻撃へ":
					forward_back = 2;
					break;
				default:
					break;
			}
			AttackChangeDialog(forward_back);
		}



		private async void AttackChangeDialog(int forward_back)
		{
			bool returnFlg = false;
			if (forward_back == 0) { returnFlg = true; }

			string topbtmST = "表";
			int tmp_ining = Convert.ToInt32(Ining);
			if (forward_back == 1)  // 次へ
			{
				if (top_btmFlg)
				{
					tmp_ining++;
				}
				if (!top_btmFlg)
				{
					topbtmST = "裏";
				}
			}
			else if (forward_back == 2)  // 前へ
			{
				if (tmp_ining == 1 && !top_btmFlg) { returnFlg = true; }  // 1回表は戻れない
				if (!top_btmFlg)  // 現在の裏表を基準に変更する
				{
					tmp_ining--;
					topbtmST = "裏";
				}
			}
			if (!returnFlg)
			{

				ContentDialog dialog = new ContentDialog();
				dialog.Title = "確認";
				string iningST = String.Format("{0}回の{1}へ移動しますか", tmp_ining.ToString(), topbtmST);
				dialog.Content = iningST;
				dialog.PrimaryButtonText = "OK";
				dialog.CloseButtonText = "NO";
				dialog.DefaultButton = ContentDialogButton.Primary;
				var result = await dialog.ShowAsync();
				if (result != ContentDialogResult.Primary)
				{
					return;
				}
			}
		}


		private void MenuFlyItem_Closed(object sender, object e)
		{
			if (MenuFlyItem.Items.Contains(steal_ball))
			{
				MenuFlyItem.Items.Remove(steal_ball);
			}
			if (MenuFlyItem.Items.Contains(steal_strike))
			{
				MenuFlyItem.Items.Remove(steal_strike);
			}
			if (MenuFlyItem.Items.Contains(steal_swing))
			{
				MenuFlyItem.Items.Remove(steal_swing);
			}
		}

		private void RunEntryRtn()
		{
			int run_id = RunData.GetRecordsCount()[0].run_count;
			RunData.addRecord(
				run_id: run_id,
				box_id: box_id,
				game_id: game_id,
				team_id: selectedTeamId,
				player_id: selectedPlayerId,
				pitcher_id: selectedPitcherPlayerId,
				pit_team_id: selectedPitcherTeamId,
				pit_hand_id: PitcherHandId,
				cat_id: selectedCatcherPlayerId,
				ump_id: ump_id,
				park_id: park_id,
				ball_type: ball_type,
				ball_speed: ballSpeedEntry,
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
				etc_cd1: ball_action,
				etc_cd2: 0,
				etc_cd3: 0,
				etc_cd4: 0,
				etc_cd5: 0,
				etc_str1: "",
				etc_str2: "",
				etc_str3: "",
				etc_str4: "",
				etc_str5: "",
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
