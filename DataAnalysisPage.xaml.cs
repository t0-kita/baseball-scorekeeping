using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public sealed partial class DataAnalysisPage : Page
	{
		public DataAnalysisPage()
		{
			this.InitializeComponent();
			IningInitialize();

			TeamName.ItemsSource = TeamData.GetRecords();
			PlayerName.ItemsSource = PlayerData.GetRecordsPitchers();
			balltype = BallType.GetRecordsBallType(hand: 0);
			BallCourseListInitialize();

			//FieldImageSetHitPoint(47, 179, 0);
			//FieldImageSetHitPoint(416, 340, 0);

			HitTypeComboBox.ItemsSource = HitType.GetRecords();



			//DirArrayInitialize();


			/// 2022.05.19 テスト
			//FieldDirectCreate();

			/// 2022.05.19 Fielddirコードの作成
			CheckFieldDirectBoxID();

			/// 2022.05.21 
			CheckHitResultBoxID();

		}
		private int team_id = -1;
		private int player_id = -1;
		private int runner_exsist = 0;
		private int bat_id = -1;
		private int ining_id = 1;
		private int count_b = 0;
		private int count_s = 0;
		private int count_o = 0;
		private int course_id = 0;
		private int ball_type_id = 0;
		private int ball_action_id = 0;
		private int min_ball_speed = 1;
		private int max_ball_speed = 180;

		private string start_datetime = "";
		private string end_datetime = "";

		private bool course_A = false;
		private bool course_B = false;
		private bool course_C = false;
		private bool course_D = false;
		private bool course_E = false;
		private bool course_F = false;
		private bool course_G = false;
		private bool course_H = false;
		private bool course_I = false;


		private bool BatSelectFlg = false;
		private bool IningSelectFlg = false;
		private bool CountSelectFlg = false;
		private bool RunnerSelectFlg = false;
		private bool BallTypeSelectFlg = false;
		private bool BallActionSelectFlg = false;
		private bool BallSpeedSelectFlg = false;
		private bool DateTimeSelectFlg = false;
		private bool CourseSelectFlg = false;
		private List<BallData.ballData> balls = new List<BallData.ballData>();
		private List<BallType.ballType> balltype = new List<BallType.ballType>();
		private PlayerData.playerData selectPlayerData = new PlayerData.playerData();
		private List<BallCourse.ballCourse> ballCourse = new List<BallCourse.ballCourse>();
		private BallCourse.ballCourse selectCourseData = new BallCourse.ballCourse();
		private bool displayZoneFlg = false;

		private List<int> ining_list = new List<int>();
		private List<int> ballAction_list = new List<int>();

		/// 2022.03.31 追加
		private bool HitTypeSelectFlg = false;


		private int HitTypeId = 0;

		private bool HitResultSelectFlg = false;


		private bool DirSelectFlg = false;



		//private const int ROWLIMIT = 300;
		//private const int COLLIMIT = 420;
		//private int[,] DirArray = new int[COLLIMIT, ROWLIMIT];

		private bool AnalysFoucusFlg = true;

		


		private void BallCourseListInitialize()
		{
			int tmp_bat_id = 0;
			if (bat_id > 0)
			{
				tmp_bat_id = bat_id;
			}
			ballCourse = BallCourse.GetRecords(tmp_bat_id);
			//CourseSelect.ItemsSource = ballCourse;
		}


		private void Zone_Image_Replace(
									int x,
									int y,
									int tmp_ball_type,
									int ball_action = 0,
									int ball_img = 0)
		{
			var poly = new Polygon();
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
					zone_x: x,
					zone_y: y,
					color: color,
					in_ball_flg: false
					);

			course_id = BallVHType(x, y);
		}



		private void Zone_Draw_Ball_img(int ball_img = 0, int zone_x = 0, int zone_y = 0, object color = null, bool in_ball_flg = true)
		{
			int zone_x_int = zone_x;
			int zone_y_int = zone_y;
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
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 1:
					point.Add(new Point(17, -8));
					point.Add(new Point(17, 8));
					point.Add(new Point(-1, 0));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 2:
					point.Add(new Point(7, -10));
					point.Add(new Point(18, 1));
					point.Add(new Point(-1, 9));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 3:
					//point.Add(new Point(-9, -9));
					//point.Add(new Point(9, -9));
					//point.Add(new Point(0, 9));

					point.Add(new Point(-1, -9));
					point.Add(new Point(17, -9));
					point.Add(new Point(8, 9));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 4:
					point.Add(new Point(-2, 1));
					point.Add(new Point(7, -10));
					point.Add(new Point(17, 9));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
					ZoneGrid.Children.Add(poly);
					break;
				case 5:
					point.Add(new Point(0, -8));
					point.Add(new Point(0, 8));
					point.Add(new Point(18, 0));

					poly.Points = point;
					poly.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
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
			//ball_flg = in_ball_flg;
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


		public class IningList
		{
			public int inings { get; set; }
			public IningList(
							int inings = 0
							)
			{
				this.inings = inings;
			}
		}



		private void IningInitialize()
		{
			ObservableCollection<IningList> iningLists = new ObservableCollection<IningList>();
			ining_list.Clear();
			for (int ining_i = 1; ining_i <= 12; ining_i++)
			{
				ining_list.Add(ining_i);
				iningLists.Add(new IningList(ining_i));
			}
			IningSelect.ItemsSource = iningLists;
		}

		public class BallActionsList
		{
			public int ballactions { get; set; }
			public int count { get; set; }
			public BallActionsList(
							int ballactions = 0,
							int count = 0
							)
			{
				this.ballactions = ballactions;
				this.count = count;
			}
		}


		/// <summary>
		/// カウントの表示を実施する
		/// </summary>
		private void countDisplay()
		{

			if (count_b == 1)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 2)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 3)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				//fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_b == 4)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
				//fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
			}
			else if (count_b == 0)
			{
				oneBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				threeBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//fourBall.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}

			if (count_s == 1)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_s == 2)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
				//threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_s == 0)
			{
				oneStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//threeStrike.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}

			if (count_o == 1)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 2)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				//threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
			else if (count_o == 3)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
				//threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
			}
			else if (count_o == 0)
			{
				oneOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				twoOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
				//threeOut.Fill = new SolidColorBrush(Windows.UI.Colors.Transparent);
			}
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
			}
		}

		private void BallTypeTextListView_Tappud(object sender, TappedRoutedEventArgs e)
		{
			TextBlock tkb = (TextBlock)sender;
			string name = tkb.Name;
		}


		private void Runner_Item_Clicked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}



		private void Runner_Unchecked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}

		#region 未使用
		private void Count_Tapped(object sender, TappedRoutedEventArgs e)
		{

		}
		#endregion

		private void Delete_Ball()
		{
			while (ZoneGrid.Children.Count > 1)
			{
				ZoneGrid.Children.RemoveAt(ZoneGrid.Children.Count - 1);
			}
		}


		private void TeamNameListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			TeamData.teamData teamDatas = (TeamData.teamData)lv.SelectedValue;
			team_id = teamDatas.team_id;
			if (AnalysFoucusFlg)
			{
				PlayerName.ItemsSource = PlayerData.GetRecordsPitchers(team_id: team_id);
				return;
			}
			PlayerName.ItemsSource = PlayerData.GetRecords(team_id: team_id);
		}

		private void PlayerNameListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			PlayerData.playerData playerData = (PlayerData.playerData)lv.SelectedValue;
			selectPlayerData = playerData;
			player_id = playerData.player_id;
			team_id = playerData.team_id;
			PlayerDataName_TextBlock.Text = playerData.name;
			PlayerDataComment_TextBox.Text = playerData.cmnt1;
			AnalysisDataDisplay(player_id: player_id);
		}


		private void AnalysisDataDisplay_BallCount(
												int player_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int ining = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int ball_type = -1,
												int ball_action = -1,
												int min_ball_speed = -1,
												int max_ball_speed = -1,
												int course_id = -1
												,
												bool course_A = false,
												bool course_B = false,
												bool course_C = false,
												bool course_D = false,
												bool course_E = false,
												bool course_F = false,
												bool course_G = false,
												bool course_H = false,
												bool course_I = false,
												int field_direct_id_left= -1,
												int field_direct_id_center = -1,
												int field_direct_id_right = -1,
												bool res_hit_flg = false,
												bool res_hit_out = false,
												bool res_hit_hit = false,
												bool res_hit_error = false,
												bool res_hit_other = false,
												int res_hit_type = -1
			)
		{
			if (player_id < 0) { return; }
			int tmp_total = 0;
			int tmp_select = 0;

			int tmp_ball;
			int tmp_strike;
			int tmp_foul;
			int tmp_inplay;
			double tmp_percent = 0;
			List<BallAction.ballAction> ballactions = BallAction.GetRecords();
			List<BallActionsList> ballActionsLists = new List<BallActionsList>();
			if (AnalysFoucusFlg)
			{
				tmp_total = BallData.GetRecordsPitcherCount(
													pitcher_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime)[0].ball_id;
				tmp_select = BallData.GetRecordsPitcherCount(
													pitcher_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime,
													bat: bat,
													ining: ining,
													count_b: count_b,
													count_s: count_s,
													count_o: count_o,
													run_exsist: run_exsist,
													run_1: run_1,
													run_2: run_2,
													run_3: run_3,
													ball_type: ball_type,
													ball_action: ball_action,
													min_ball_speed: min_ball_speed,
													max_ball_speed: max_ball_speed,
													course_id: course_id,
													course_A: course_A,
													course_B: course_B,
													course_C: course_C,
													course_D: course_D,
													course_E: course_E,
													course_F: course_F,
													course_G: course_G,
													course_H: course_H,
													course_I: course_I,
													field_direct_id_left: field_direct_id_left,
													field_direct_id_center: field_direct_id_center,
													field_direct_id_right: field_direct_id_right,
													res_hit_flg: HitResultSelectFlg,
													res_hit_out: res_hit_out,
													res_hit_hit: res_hit_hit,
													res_hit_error: res_hit_error,
													res_hit_other: res_hit_other,
													res_hit_type: res_hit_type
													)[0].ball_id;
				foreach (BallAction.ballAction ballaction in ballactions)
				{
					int sss = ballaction.ball_action_id;
					tmp_ball = BallData.GetRecordsPitcherCount(
													pitcher_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime,
													bat: bat,
													ining: ining,
													count_b: count_b,
													count_s: count_s,
													count_o: count_o,
													run_exsist: run_exsist,
													run_1: run_1,
													run_2: run_2,
													run_3: run_3,
													ball_type: ball_type,
													ball_action: ballaction.ball_action_id,
													min_ball_speed: min_ball_speed,
													max_ball_speed: max_ball_speed,
													course_id: course_id,
													course_A: course_A,
													course_B: course_B,
													course_C: course_C,
													course_D: course_D,
													course_E: course_E,
													course_F: course_F,
													course_G: course_G,
													course_H: course_H,
													course_I: course_I,
													field_direct_id_left: field_direct_id_left,
													field_direct_id_center: field_direct_id_center,
													field_direct_id_right: field_direct_id_right,
													res_hit_flg: HitResultSelectFlg,
													res_hit_out: res_hit_out,
													res_hit_hit: res_hit_hit,
													res_hit_error: res_hit_error,
													res_hit_other: res_hit_other,
													res_hit_type: res_hit_type	
													)[0].ball_id;
					ballActionsLists.Add(new BallActionsList(ballaction.ball_action_id, tmp_ball));
				}
			}
			if (!AnalysFoucusFlg)
			{
				tmp_total = BallData.GetRecordsBatterCount(
													player_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime)[0].ball_id;
				tmp_select = BallData.GetRecordsBatterCount(
													player_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime,
													//bat_id: bat,
													//hand_id:bat,
													pit_hand_id: bat,
													ining: ining,
													count_b: count_b,
													count_s: count_s,
													count_o: count_o,
													run_exsist: run_exsist,
													run_1: run_1,
													run_2: run_2,
													run_3: run_3,
													ball_type: ball_type,
													ball_action: ball_action,
													min_ball_speed: min_ball_speed,
													max_ball_speed: max_ball_speed,
													course_id: course_id,
													course_A: course_A,
													course_B: course_B,
													course_C: course_C,
													course_D: course_D,
													course_E: course_E,
													course_F: course_F,
													course_G: course_G,
													course_H: course_H,
													course_I: course_I,
													field_direct_id_left: field_direct_id_left,
													field_direct_id_center: field_direct_id_center,
													field_direct_id_right: field_direct_id_right,
													res_hit_flg: HitResultSelectFlg,
													res_hit_out: res_hit_out,
													res_hit_hit: res_hit_hit,
													res_hit_error: res_hit_error,
													res_hit_other: res_hit_other,
													res_hit_type: res_hit_type
													)[0].ball_id;
				foreach (BallAction.ballAction ballaction in ballactions)
				{
					int sss = ballaction.ball_action_id;
					tmp_ball = BallData.GetRecordsBatterCount(
													player_id: player_id,
													start_datetime: start_datetime,
													end_datetime: end_datetime,
													//bat_id: bat,
													//hand_id:bat,
													pit_hand_id: bat,
													ining: ining,
													count_b: count_b,
													count_s: count_s,
													count_o: count_o,
													run_exsist: run_exsist,
													run_1: run_1,
													run_2: run_2,
													run_3: run_3,
													ball_type: ball_type,
													ball_action: ballaction.ball_action_id,
													min_ball_speed: min_ball_speed,
													max_ball_speed: max_ball_speed,
													course_id: course_id,
													course_A: course_A,
													course_B: course_B,
													course_C: course_C,
													course_D: course_D,
													course_E: course_E,
													course_F: course_F,
													course_G: course_G,
													course_H: course_H,
													course_I: course_I,
													field_direct_id_left: field_direct_id_left,
													field_direct_id_center: field_direct_id_center,
													field_direct_id_right: field_direct_id_right,
													res_hit_flg: HitResultSelectFlg,
													res_hit_out: res_hit_out,
													res_hit_hit: res_hit_hit,
													res_hit_error: res_hit_error,
													res_hit_other: res_hit_other,
													res_hit_type: res_hit_type
													)[0].ball_id;
					ballActionsLists.Add(new BallActionsList(ballaction.ball_action_id, tmp_ball));
				}
			}




			tmp_percent = Convert.ToDouble(tmp_select) / Convert.ToDouble(tmp_total) * 100;
			TotalBallCount_TextBlock.Text = tmp_total.ToString();
			SelectBallCount_TextBlock.Text = tmp_select.ToString();
			SelectBallPercent_TextBlock.Text = tmp_percent.ToString("0.00");

			//BallCountPercent(ballActionsLists, tmp_total);
			if (!BallActionSelectFlg)  // ボール結果別のボタンを押下した時はパーセントを変更しない
			{
				BallCountPercent(ballActionsLists, tmp_select);
			}

		}


		private void BallCountPercent(List<BallActionsList> ballActionsLists, int total)
		{
			int tmp_ball_count;
			double tmp_percent;

			foreach (BallActionsList ballActions in ballActionsLists)
			{
				tmp_ball_count = ballActions.count;
				if (tmp_ball_count == 0 && total == 0)
				{
					tmp_percent = 0;
				}
				else
				{
					tmp_percent = Convert.ToDouble(tmp_ball_count) / Convert.ToDouble(total) * 100;
				}
				switch (ballActions.ballactions)
				{
					case 0:  // ボール
						BallAction_b_Count_TextBlock.Text = tmp_ball_count.ToString();
						BallAction_b_Percent_TextBlock.Text = tmp_percent.ToString("0");
						break;
					case 1:  // 見逃し
						BallAction_s_Count_TextBlock.Text = tmp_ball_count.ToString();
						BallAction_s_Percent_TextBlock.Text = tmp_percent.ToString("0");
						break;
					case 2:  // 空振り
						BallAction_sw_Count_TextBlock.Text = tmp_ball_count.ToString();
						BallAction_sw_Percent_TextBlock.Text = tmp_percent.ToString("0");
						break;
					case 3:  // ファール
						BallAction_f_Count_TextBlock.Text = tmp_ball_count.ToString();
						BallAction_f_Percent_TextBlock.Text = tmp_percent.ToString("0");
						break;
					case 4:  // インプレー
						BallAction_inplay_Count_TextBlock.Text = tmp_ball_count.ToString();
						BallAction_inplay_Percent_TextBlock.Text = tmp_percent.ToString("0");
						break;


				}
			}
		}


		private void AnalysisDataDisplay_BallSummarys(
												int player_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int ining = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int ball_type = -1,
												int ball_action = -1,
												int min_ball_speed = -1,
												int max_ball_speed = -1,
												int course_id = -1
												,
												bool course_A = false,
												bool course_B = false,
												bool course_C = false,
												bool course_D = false,
												bool course_E = false,
												bool course_F = false,
												bool course_G = false,
												bool course_H = false,
												bool course_I = false,
												int field_direct_id_left = -1,
												int field_direct_id_center = -1,
												int field_direct_id_right = -1,
												bool res_hit_flg = false,
												bool res_hit_out = false,
												bool res_hit_hit = false,
												bool res_hit_error = false,
												bool res_hit_other = false,
												int res_hit_type = -1
												)
		{
			if (player_id < 0) { return; }
			List<BallData.ballMinMaxAvg> totals = new List<BallData.ballMinMaxAvg>();
			/// ver.3.0.0.0.
			//string handST = selectPlayerData.hand;
			//int hand_id = 0;
			//if (handST != "右") { hand_id = 1; }
			int hand_id = selectPlayerData.hand_id;
			if (AnalysFoucusFlg)
			{
				totals
					= BallData.GetRecordsSpeedMinMaxAvg(
						pitcher_id: player_id,
						start_datetime: start_datetime,
						end_datetime: end_datetime,
						bat: bat,
						ining: ining,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_exsist: run_exsist,
						run_1: run_1,
						run_2: run_2,
						run_3: run_3,
						ball_type: ball_type,
						ball_action: ball_action,
						min_ball_speed: min_ball_speed,
						max_ball_speed: max_ball_speed,
						course_id: course_id,
						course_A: course_A,
						course_B: course_B,
						course_C: course_C,
						course_D: course_D,
						course_E: course_E,
						course_F: course_F,
						course_G: course_G,
						course_H: course_H,
						course_I: course_I,
						field_direct_id_left: field_direct_id_left,
						field_direct_id_center: field_direct_id_center,
						field_direct_id_right: field_direct_id_right,
						res_hit_flg: HitResultSelectFlg,
						res_hit_out: res_hit_out,
						res_hit_hit: res_hit_hit,
						res_hit_error: res_hit_error,
						res_hit_other: res_hit_other,
						res_hit_type: res_hit_type
						);
			}
			if (!AnalysFoucusFlg)
			{
				totals
					= BallData.GetRecordsSpeedMinMaxAvg(
						player_id: player_id,
						start_datetime: start_datetime,
						end_datetime: end_datetime,
						//bat: bat,
						pit_hand_id: bat,
						ining: ining,
						count_b: count_b,
						count_s: count_s,
						count_o: count_o,
						run_exsist: run_exsist,
						run_1: run_1,
						run_2: run_2,
						run_3: run_3,
						ball_type: ball_type,
						ball_action: ball_action,
						min_ball_speed: min_ball_speed,
						max_ball_speed: max_ball_speed,
						course_id: course_id,
						course_A: course_A,
						course_B: course_B,
						course_C: course_C,
						course_D: course_D,
						course_E: course_E,
						course_F: course_F,
						course_G: course_G,
						course_H: course_H,
						course_I: course_I,
						field_direct_id_left: field_direct_id_left,
						field_direct_id_center: field_direct_id_center,
						field_direct_id_right: field_direct_id_right,
						res_hit_flg: HitResultSelectFlg,
						res_hit_out: res_hit_out,
						res_hit_hit: res_hit_hit,
						res_hit_error: res_hit_error,
						res_hit_other: res_hit_other,
						res_hit_type: res_hit_type
						);
			}
			balltype = BallType.GetRecordsBallType(hand: hand_id);
			BallSpeedMin_TextBlock.Text = totals[0].min.ToString();
			BallSpeedMax_TextBlock.Text = totals[0].max.ToString();
			BallSpeedAverage_TextBlock.Text = totals[0].avg.ToString();
		}



		/// <summary>
		/// 選択したデータを表示する
		/// </summary>
		/// <param name="player_id"></param>
		private void AnalysisDataDisplay(int player_id = -1)
		{
			if (player_id < 0) { return; }
			if (AnalysFoucusFlg)
			{
				AnalysisPitcherRtn();
			}
			if (!AnalysFoucusFlg)
			{
				AnalysisBatterRtn();
			}

		}

		private void AnalysisPitcherRtn()
		{
			string tmp_start_datetime = "1900-01-01";
			string tmp_end_datetime = "2100-12-31";
			int tmp_bat = -1;
			int tmp_ining = -1;
			int tmp_run_exsist = -1;
			int tmp_run_1 = -1;
			int tmp_run_2 = -1;
			int tmp_run_3 = -1;
			int tmp_count_b = -1;
			int tmp_count_s = -1;
			int tmp_count_o = -1;
			int tmp_ball_type = -1;
			int tmp_ball_action = -1;
			int tmp_min_ball_speed = -1;
			int tmp_max_ball_speed = -1;
			int tmp_course_id = -1;
			int tmp_dir_left = -1;
			int tmp_dir_center = -1;
			int tmp_dir_right = -1;

			int tmp_res_hit_type_id = -1;

			bool outFlg = false;
			bool hitFlg = false;
			bool errorFlg = false;
			bool otherFlg = false;




			if (DateTimeSelectFlg)
			{
				tmp_start_datetime = start_datetime;
				tmp_end_datetime = end_datetime;
			}

			if (BatSelectFlg)
			{
				tmp_bat = bat_id;
			}


			if (IningSelectFlg)
			{
				tmp_ining = ining_id;
			}
			if (runner_exsist == 1)
			{
				tmp_run_exsist = runner_exsist;
			}
			if (RunnerSelectFlg)
			{
				tmp_run_1 = RunnerCheck_Int(Convert.ToBoolean(run_1.IsChecked));
				tmp_run_2 = RunnerCheck_Int(Convert.ToBoolean(run_2.IsChecked));
				tmp_run_3 = RunnerCheck_Int(Convert.ToBoolean(run_3.IsChecked));
			}
			if (CountSelectFlg)
			{
				tmp_count_b = count_b;
				tmp_count_s = count_s;
				if ((bool)OutCountOnOff_CheckBox.IsChecked)
				{
					tmp_count_o = count_o;
				}
			}
			if (BallTypeSelectFlg)
			{
				tmp_ball_type = ball_type_id;
			}
			if (BallSpeedSelectFlg)
			{
				tmp_min_ball_speed = min_ball_speed;
				tmp_max_ball_speed = max_ball_speed;
			}
			if (BallActionSelectFlg)
			{
				tmp_ball_action = ball_action_id;
			}

			if (CourseSelectFlg)
			{
				tmp_course_id = course_id;
				tmp_course_id = 1;
			}

			if (DirSelectFlg)
			{
				if ((bool)LetfDir.IsChecked)
				{
					tmp_dir_left = 0;
				}
				if ((bool)CenterDir.IsChecked)
				{
					tmp_dir_center = 1;
				}
				if ((bool)RightDir.IsChecked)
				{
					tmp_dir_right = 2;
				}
			}


			if (HitResultSelectFlg) 
			{
				outFlg = (bool)HitResultOut.IsChecked;
				hitFlg = (bool)HitResultHit.IsChecked;
				errorFlg = (bool)HitResultError.IsChecked;
				otherFlg = (bool)HitResultOther.IsChecked;
			}

			if (HitTypeSelectFlg) 
			{
				tmp_res_hit_type_id = HitTypeId;
			}

			List<BallData.ballData> balls = BallData.GetRecordsPitcher(
														pitcher_id: player_id,
														start_datetime: tmp_start_datetime,
														end_datetime: tmp_end_datetime,
														bat: tmp_bat,
														ining: tmp_ining,
														count_b: tmp_count_b,
														count_s: tmp_count_s,
														count_o: tmp_count_o,
														run_exsist: tmp_run_exsist,
														run_1: tmp_run_1,
														run_2: tmp_run_2,
														run_3: tmp_run_3,
														ball_type: tmp_ball_type,
														ball_action: tmp_ball_action,
														min_ball_speed: tmp_min_ball_speed,
														max_ball_speed: tmp_max_ball_speed,
														course_id: tmp_course_id,
														course_A: course_A,
														course_B: course_B,
														course_C: course_C,
														course_D: course_D,
														course_E: course_E,
														course_F: course_F,
														course_G: course_G,
														course_H: course_H,
														course_I: course_I,
														field_direct_id_left:tmp_dir_left,
														field_direct_id_center: tmp_dir_center,
														field_direct_id_right: tmp_dir_right,
														res_hit_flg: HitResultSelectFlg,
														res_hit_out:outFlg,
														res_hit_hit: hitFlg,
														res_hit_error: errorFlg,
														res_hit_other: otherFlg,
														res_hit_type: tmp_res_hit_type_id
														);
			if (displayZoneFlg)
			{
				Delete_Ball();
				displayZoneFlg = false;
			}
			foreach (BallData.ballData ball in balls)
			{
				Zone_Image_Replace(
							x: ball.ball_x,
							y: ball.ball_y,
							tmp_ball_type: ball.ball_type,
							ball_action: ball.ball_action,
							ball_img: ball.ball_img);
			}

			/// 集計結果表示
			AnalysisDataDisplay_BallSummarys(
									player_id: player_id,
									start_datetime: tmp_start_datetime,
									end_datetime: tmp_end_datetime,
									bat: tmp_bat,
									ining: tmp_ining,
									count_b: tmp_count_b,
									count_s: tmp_count_s,
									count_o: tmp_count_o,
									run_exsist: tmp_run_exsist,
									run_1: tmp_run_1,
									run_2: tmp_run_2,
									run_3: tmp_run_3,
									ball_type: tmp_ball_type,
									ball_action: tmp_ball_action,
									min_ball_speed: tmp_min_ball_speed,
									max_ball_speed: tmp_max_ball_speed,
									course_id: tmp_course_id,
									course_A: course_A,
									course_B: course_B,
									course_C: course_C,
									course_D: course_D,
									course_E: course_E,
									course_F: course_F,
									course_G: course_G,
									course_H: course_H,
									course_I: course_I,
									field_direct_id_left: tmp_dir_left,
									field_direct_id_center: tmp_dir_center,
									field_direct_id_right: tmp_dir_right,
									res_hit_flg: HitResultSelectFlg,
									res_hit_out: outFlg,
									res_hit_hit: hitFlg,
									res_hit_error: errorFlg,
									res_hit_other: otherFlg,
									res_hit_type: tmp_res_hit_type_id
									);

			/// 割合表示
			AnalysisDataDisplay_BallCount(
									player_id: player_id,
									start_datetime: tmp_start_datetime,
									end_datetime: tmp_end_datetime,
									bat: tmp_bat,
									ining: tmp_ining,
									count_b: tmp_count_b,
									count_s: tmp_count_s,
									count_o: tmp_count_o,
									run_exsist: tmp_run_exsist,
									run_1: tmp_run_1,
									run_2: tmp_run_2,
									run_3: tmp_run_3,
									ball_type: tmp_ball_type,
									ball_action: tmp_ball_action,
									min_ball_speed: tmp_min_ball_speed,
									max_ball_speed: tmp_max_ball_speed,
									course_id: tmp_course_id,
									course_A: course_A,
									course_B: course_B,
									course_C: course_C,
									course_D: course_D,
									course_E: course_E,
									course_F: course_F,
									course_G: course_G,
									course_H: course_H,
									course_I: course_I,
									field_direct_id_left: tmp_dir_left,
									field_direct_id_center: tmp_dir_center,
									field_direct_id_right: tmp_dir_right,
									res_hit_flg: HitResultSelectFlg,
									res_hit_out: outFlg,
									res_hit_hit: hitFlg,
									res_hit_error: errorFlg,
									res_hit_other: otherFlg,
									res_hit_type: tmp_res_hit_type_id
									);


			displayZoneFlg = true;


			/// 2022.03.31
			/// Fieldデータの配置
			AnalysisFieldDataDisplay(player_id: player_id);
		}
		private void AnalysisBatterRtn()
		{
			string tmp_start_datetime = "1900-01-01";
			string tmp_end_datetime = "2100-12-31";
			int tmp_bat = -1;
			int tmp_ining = -1;
			int tmp_run_exsist = -1;
			int tmp_run_1 = -1;
			int tmp_run_2 = -1;
			int tmp_run_3 = -1;
			int tmp_count_b = -1;
			int tmp_count_s = -1;
			int tmp_count_o = -1;
			int tmp_ball_type = -1;
			int tmp_ball_action = -1;
			int tmp_min_ball_speed = -1;
			int tmp_max_ball_speed = -1;
			int tmp_course_id = -1;
			int tmp_dir_left = -1;
			int tmp_dir_center = -1;
			int tmp_dir_right = -1;

			int tmp_res_hit_type_id = -1;

			bool outFlg = false;
			bool hitFlg = false;
			bool errorFlg = false;
			bool otherFlg = false;

			if (DateTimeSelectFlg)
			{
				tmp_start_datetime = start_datetime;
				tmp_end_datetime = end_datetime;
			}

			if (BatSelectFlg)
			{
				tmp_bat = bat_id;
			}


			if (IningSelectFlg)
			{
				tmp_ining = ining_id;
			}
			if (runner_exsist == 1)
			{
				tmp_run_exsist = runner_exsist;
			}
			if (RunnerSelectFlg)
			{
				tmp_run_1 = RunnerCheck_Int(Convert.ToBoolean(run_1.IsChecked));
				tmp_run_2 = RunnerCheck_Int(Convert.ToBoolean(run_2.IsChecked));
				tmp_run_3 = RunnerCheck_Int(Convert.ToBoolean(run_3.IsChecked));
			}
			if (CountSelectFlg)
			{
				tmp_count_b = count_b;
				tmp_count_s = count_s;
				if ((bool)OutCountOnOff_CheckBox.IsChecked)
				{
					tmp_count_o = count_o;
				}
			}
			if (BallTypeSelectFlg)
			{
				tmp_ball_type = ball_type_id;
			}
			if (BallSpeedSelectFlg)
			{
				tmp_min_ball_speed = min_ball_speed;
				tmp_max_ball_speed = max_ball_speed;
			}
			if (BallActionSelectFlg)
			{
				tmp_ball_action = ball_action_id;
			}

			if (CourseSelectFlg)
			{
				tmp_course_id = course_id;
				tmp_course_id = 1;
			}

			if (DirSelectFlg)
			{
				if ((bool)LetfDir.IsChecked)
				{
					tmp_dir_left = 0;
				}
				if ((bool)CenterDir.IsChecked)
				{
					tmp_dir_center = 1;
				}
				if ((bool)RightDir.IsChecked)
				{
					tmp_dir_right = 2;
				}
			}

			if (HitResultSelectFlg)
			{
				outFlg = (bool)HitResultOut.IsChecked;
				hitFlg = (bool)HitResultHit.IsChecked;
				errorFlg = (bool)HitResultError.IsChecked;
				otherFlg = (bool)HitResultOther.IsChecked;
			}

			if (HitTypeSelectFlg)
			{
				tmp_res_hit_type_id = HitTypeId;
			}

			List<BallData.ballData> balls = BallData.GetRecordsBatter(
														player_id: player_id,
														start_datetime: tmp_start_datetime,
														end_datetime: tmp_end_datetime,
														//bat_id: tmp_bat,
														pit_hand_id: tmp_bat,
														//hand_id:
														ining: tmp_ining,
														count_b: tmp_count_b,
														count_s: tmp_count_s,
														count_o: tmp_count_o,
														run_exsist: tmp_run_exsist,
														run_1: tmp_run_1,
														run_2: tmp_run_2,
														run_3: tmp_run_3,
														ball_type: tmp_ball_type,
														ball_action: tmp_ball_action,
														min_ball_speed: tmp_min_ball_speed,
														max_ball_speed: tmp_max_ball_speed,
														course_id: tmp_course_id,
														course_A: course_A,
														course_B: course_B,
														course_C: course_C,
														course_D: course_D,
														course_E: course_E,
														course_F: course_F,
														course_G: course_G,
														course_H: course_H,
														course_I: course_I,
														field_direct_id_left: tmp_dir_left,
									field_direct_id_center: tmp_dir_center,
									field_direct_id_right: tmp_dir_right,
									res_hit_flg: HitResultSelectFlg,
														res_hit_out: outFlg,
														res_hit_hit: hitFlg,
														res_hit_error: errorFlg,
														res_hit_other: otherFlg,
														res_hit_type: tmp_res_hit_type_id
														);
			if (displayZoneFlg)
			{
				Delete_Ball();
				displayZoneFlg = false;
			}
			foreach (BallData.ballData ball in balls)
			{
				Zone_Image_Replace(
							x: ball.ball_x,
							y: ball.ball_y,
							tmp_ball_type: ball.ball_type,
							ball_action: ball.ball_action,
							ball_img: ball.ball_img);
			}

			/// 集計結果表示
			AnalysisDataDisplay_BallSummarys(
									player_id: player_id,
									start_datetime: tmp_start_datetime,
									end_datetime: tmp_end_datetime,
									bat: tmp_bat,
									ining: tmp_ining,
									count_b: tmp_count_b,
									count_s: tmp_count_s,
									count_o: tmp_count_o,
									run_exsist: tmp_run_exsist,
									run_1: tmp_run_1,
									run_2: tmp_run_2,
									run_3: tmp_run_3,
									ball_type: tmp_ball_type,
									ball_action: tmp_ball_action,
									min_ball_speed: tmp_min_ball_speed,
									max_ball_speed: tmp_max_ball_speed,
									course_id: tmp_course_id,
									course_A: course_A,
									course_B: course_B,
									course_C: course_C,
									course_D: course_D,
									course_E: course_E,
									course_F: course_F,
									course_G: course_G,
									course_H: course_H,
									course_I: course_I,
									field_direct_id_left: tmp_dir_left,
									field_direct_id_center: tmp_dir_center,
									field_direct_id_right: tmp_dir_right,
									res_hit_flg: HitResultSelectFlg,
														res_hit_out: outFlg,
														res_hit_hit: hitFlg,
														res_hit_error: errorFlg,
														res_hit_other: otherFlg,
														res_hit_type: tmp_res_hit_type_id
									);

			/// 割合表示
			AnalysisDataDisplay_BallCount(
									player_id: player_id,
									start_datetime: tmp_start_datetime,
									end_datetime: tmp_end_datetime,
									bat: tmp_bat,
									ining: tmp_ining,
									count_b: tmp_count_b,
									count_s: tmp_count_s,
									count_o: tmp_count_o,
									run_exsist: tmp_run_exsist,
									run_1: tmp_run_1,
									run_2: tmp_run_2,
									run_3: tmp_run_3,
									ball_type: tmp_ball_type,
									ball_action: tmp_ball_action,
									min_ball_speed: tmp_min_ball_speed,
									max_ball_speed: tmp_max_ball_speed,
									course_id: tmp_course_id,
									course_A: course_A,
									course_B: course_B,
									course_C: course_C,
									course_D: course_D,
									course_E: course_E,
									course_F: course_F,
									course_G: course_G,
									course_H: course_H,
									course_I: course_I,
									field_direct_id_left: tmp_dir_left,
									field_direct_id_center: tmp_dir_center,
									field_direct_id_right: tmp_dir_right,
									res_hit_flg: HitResultSelectFlg,
									res_hit_out: outFlg,
									res_hit_hit: hitFlg,
									res_hit_error: errorFlg,
									res_hit_other: otherFlg,
									res_hit_type: tmp_res_hit_type_id
									);

			displayZoneFlg = true;


			/// 2022.03.31
			/// Fieldデータの配置
			AnalysisFieldDataDisplay(player_id: player_id);
		}



		private int RunnerCheck_Int(bool on = false)
		{
			if (on) { return 1; }
			return 0;
		}

		private void IningSelectListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			//AnalysisDataDisplay(player_id: player_id);

		}

		private void Zone_Image_Tapped(object sender, TappedRoutedEventArgs e)
		{
			//AnalysisDataDisplay(player_id: player_id);
		}

		private void CountSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					CountSelectFlg = true;
					CountSelect_StackPanel.Visibility = Visibility.Visible;
					CountHideCheckBox.IsEnabled = true;
				}
				else
				{
					CountSelectFlg = false;
					CountSelect_StackPanel.Visibility = Visibility.Collapsed;
					CountHideCheckBox.IsEnabled = false;
					CountHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
		}



		private void BatSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					BatSelectFlg = true;
					BatSelect_StackPanel.Visibility = Visibility.Visible;
					BatHideCheckBox.IsEnabled = true;
					bat_id = 0;
				}
				else
				{
					BatSelectFlg = false;
					BatSelect_StackPanel.Visibility = Visibility.Collapsed;
					BatHideCheckBox.IsEnabled = false;
					BatHideCheckBox.IsChecked = false;
					bat_id = -1;
				}
			}
			AnalysisDataDisplay(player_id: player_id);

		}



		private void BatLeftRight_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					bat_id = 1;
				}
				else
				{
					bat_id = 0;
				}
			}
			BallCourseListInitialize();  // 打席別に選択肢を変更する

			AnalysisDataDisplay(player_id: player_id);
		}



		private void IningSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					IningSelectFlg = true;
					IningSelect_StackPanel.Visibility = Visibility.Visible;
					IningHideCheckBox.IsEnabled = true;
				}
				else
				{
					IningSelectFlg = false;
					IningSelect_StackPanel.Visibility = Visibility.Collapsed;
					IningHideCheckBox.IsEnabled = false;
					IningHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);

		}

		private void RunnerSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					runner_exsist = 0;
					RunnerCheckBox.IsChecked = false;
					RunnerCheckBox.IsEnabled = false;
					RunnerSelectFlg = true;
					RunnerSelect_StackPanel.Visibility = Visibility.Visible;
					RunnerHideCheckBox.IsEnabled = true;
				}
				else
				{
					RunnerCheckBox.IsEnabled = true;
					RunnerSelectFlg = false;
					RunnerSelect_StackPanel.Visibility = Visibility.Collapsed;
					RunnerHideCheckBox.IsEnabled = false;
					RunnerHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
		}

		private void BallTypeSelectListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			BallType.ballType balltype = (BallType.ballType)lv.SelectedValue;
			ball_type_id = balltype.ball_type_id;
			AnalysisDataDisplay(player_id: player_id);

		}

		private void BallTypeSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					BallTypeSelect.ItemsSource = balltype;
					BallTypeSelectFlg = true;
					BallTypeSelect_StackPanel.Visibility = Visibility.Visible;
					BallTypeHideCheckBox.IsEnabled = true;
				}
				else
				{
					BallTypeSelectFlg = false;
					BallTypeSelect_StackPanel.Visibility = Visibility.Collapsed;
					BallTypeHideCheckBox.IsEnabled = false;
					BallTypeHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);

		}

		private void BallSpeedSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					BallSpeedSelectFlg = true;
					BallSpeedSelect_StackPanel.Visibility = Visibility.Visible;
					BallSpeedHideCheckBox.IsEnabled = true;
				}
				else
				{
					BallSpeedSelectFlg = false;
					BallSpeedSelect_StackPanel.Visibility = Visibility.Collapsed;
					BallSpeedHideCheckBox.IsEnabled = false;
					BallSpeedHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);

		}

		private void CountCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			CheckBox ch = (CheckBox)sender;
			if (Convert.ToBoolean(ch.IsChecked))
			{
				runner_exsist = 1;
			}
			else
			{
				runner_exsist = 0;
			}
			AnalysisDataDisplay(player_id: player_id);
		}

		private void CountCheckBox_UnChecked(object sender, RoutedEventArgs e)
		{
			CheckBox ch = (CheckBox)sender;
			if (Convert.ToBoolean(ch.IsChecked))
			{
				runner_exsist = 1;
			}
			else
			{
				runner_exsist = 0;
			}
			AnalysisDataDisplay(player_id: player_id);

		}

		private void BallTypeSelect_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == Windows.System.VirtualKey.Enter)
			{

			}
		}

		private void BallTypeSelect_Selection(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			try
			{
				BallType.ballType balltype = (BallType.ballType)lv.SelectedValue;
				if (balltype != null)
				{
					ball_type_id = balltype.ball_type_id;
					AnalysisDataDisplay(player_id: player_id);
				}
			}
			catch { }
		}

		private void TeamName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			TeamData.teamData teamDatas = (TeamData.teamData)lv.SelectedValue;
			team_id = teamDatas.team_id;
			if (AnalysFoucusFlg)
			{
				PlayerName.ItemsSource = PlayerData.GetRecordsPitchers(team_id: team_id);
				return;
			}
			PlayerName.ItemsSource = PlayerData.GetRecords(team_id: team_id);


		}

		private void PlayerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			try
			{
				PlayerData.playerData playerData = (PlayerData.playerData)lv.SelectedValue;
				if (playerData != null)
				{
					player_id = playerData.player_id;
					team_id = playerData.team_id;

					/// ver.3.0.0.0. 以降
					//string handST = playerData.hand;
					int hand_id = playerData.hand_id;
					//int hand_id = 0;
					//if (handST != "右") { hand_id = 1; }


					List<BallData.ballMinMaxAvg> totals = new List<BallData.ballMinMaxAvg>();
					if (AnalysFoucusFlg)
					{ totals = BallData.GetRecordsSpeedMinMaxAvg(pitcher_id: player_id); }
					if (!AnalysFoucusFlg)
					{ totals = BallData.GetRecordsSpeedMinMaxAvg(player_id: player_id); }

					balltype = BallType.GetRecordsBallType(hand: hand_id);
					BallSpeedMin_TextBlock.Text = totals[0].min.ToString();
					BallSpeedMax_TextBlock.Text = totals[0].max.ToString();
					BallSpeedAverage_TextBlock.Text = totals[0].avg.ToString();
					AnalysisDataDisplay(player_id: player_id);
				}
			}
			catch { }
		}

		private void BallSpeed_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox str = (TextBox)sender;
			var srtText = str.Text;
			switch (str.Name)
			{
				case "BallSpeedMIN_TextBox":
					BallSpeed_TextChanged_MIN(srtText);
					break;
				case "BallSpeedMAX_TextBox":
					BallSpeed_TextChanged_MAX(srtText);
					break;
			}

			if (BallSpeedMIN_TextBox.Text != "")
			{ min_ball_speed = Convert.ToInt32(BallSpeedMIN_TextBox.Text); }
			else { min_ball_speed = -1; }

			if (BallSpeedMAX_TextBox.Text != "")
			{ max_ball_speed = Convert.ToInt32(BallSpeedMAX_TextBox.Text); }
			else { max_ball_speed = -1; }

			AnalysisDataDisplay(player_id: player_id);
		}

		private void BallSpeed_TextChanged_MAX(string srtText)
		{
			int resultInt;
			if (int.TryParse(srtText, out resultInt))
			{
				if (0 < resultInt && resultInt < 180)
				{
					BallSpeedMAX_TextBox.Text = resultInt.ToString();
				}
				else { BallSpeedMAX_TextBox.Text = ""; }
			}
			else { BallSpeedMAX_TextBox.Text = ""; }
		}

		private void BallSpeed_TextChanged_MIN(string srtText)
		{
			int resultInt;
			if (int.TryParse(srtText, out resultInt))
			{
				if (0 < resultInt && resultInt < 180)
				{
					BallSpeedMIN_TextBox.Text = resultInt.ToString();

				}
				else { BallSpeedMIN_TextBox.Text = ""; }
			}
			else { BallSpeedMIN_TextBox.Text = ""; }
		}

		private void IningSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			IningList iningls = (IningList)lv.SelectedValue;
			ining_id = iningls.inings;
			AnalysisDataDisplay(player_id: player_id);

		}

		private void DateTimeSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					DateTimeSelectFlg = true;
					DateTimeSelect_StackPanel.Visibility = Visibility.Visible;
					DateTimeHideCheckBox.IsEnabled = true;
				}
				else
				{
					DateTimeSelectFlg = false;
					DateTimeSelect_StackPanel.Visibility = Visibility.Collapsed;
					DateTimeHideCheckBox.IsEnabled = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
		}

		private void DateTimePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
		{
			CalendarDatePicker cldt = (CalendarDatePicker)sender;
			switch (cldt.Name)
			{
				case "StartDateTimePicker":
					try
					{
						start_datetime = sender.Date.Value.Date.ToString("yyyy-MM-dd");
					}
					catch { }
					break;
				case "EndDateTimePicker":
					try
					{
						end_datetime = sender.Date.Value.Date.ToString("yyyy-MM-dd");
					}
					catch { }
					break;
			}
			AnalysisDataDisplay(player_id: player_id);
		}

		private void PlayerCmnt_Fix()
		{
			if (team_id < 0) { return; }
			if (player_id < 0) { return; }
			if (PlayerDataComment_TextBox.Text.Length == 0) { return; }
			PlayerData.updateCmntRecord(
								team_id: team_id,
								player_id: player_id,
								cmnt1: PlayerDataComment_TextBox.Text,
								update_date: DateTime.Now);
			try
			{
				int tmp_team_id = 0;
				if (team_id > 0) { tmp_team_id = team_id; }
				if (AnalysFoucusFlg)
				{
					PlayerName.ItemsSource = PlayerData.GetRecordsPitchers(tmp_team_id);
					return;
				}
				PlayerName.ItemsSource = PlayerData.GetRecords(team_id: team_id);
			}
			catch
			{ }
		}

		private void PlayerDataCmntFix_Button_Click(object sender, RoutedEventArgs e)
		{
			PlayerCmnt_Fix();
		}

		private void BallAction_Count_Button_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;
			switch (btn.Name)
			{
				case "BallAction_b_Count_Button":
					ball_action_id = 0;
					break;
				case "BallAction_s_Count_Button":
					ball_action_id = 1;
					break;
				case "BallAction_sw_Count_Button":
					ball_action_id = 2;
					break;
				case "BallAction_f_Count_Button":
					ball_action_id = 3;
					break;
				case "BallAction_inplay_Count_Button":
					ball_action_id = 4;
					break;
			}
			BallActionSelectFlg = true;
			AnalysisDataDisplay(player_id: player_id);
			BallActionSelectFlg = false;
		}

		private void HideCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			CheckBox check = (CheckBox)sender;
			switch (check.Name)
			{
				case "DateTimeHideCheckBox":
					DateTimeSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "BatHideCheckBox":
					BatSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "IningHideCheckBox":
					IningSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "CountHideCheckBox":
					CountSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "RunnerHideCheckBox":
					RunnerSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "BallTypeHideCheckBox":
					BallTypeSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "BallSpeedHideCheckBox":
					BallSpeedSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "CourseHideCheckBox":
					CourseSelect_StackPanel.Visibility = Visibility.Collapsed;

					break;
				case "HitTypeHideCheckBox":
					HitTypeSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
				case "DirHideCheckBox":
					DirSelect_StackPanel.Visibility = Visibility.Collapsed;
					break;
			}
		}

		private void HideCheckBox_UnChecked(object sender, RoutedEventArgs e)
		{
			CheckBox check = (CheckBox)sender;
			switch (check.Name)
			{
				case "DateTimeHideCheckBox":
					DateTimeSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "BatHideCheckBox":
					BatSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "IningHideCheckBox":
					IningSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "CountHideCheckBox":
					CountSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "RunnerHideCheckBox":
					RunnerSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "BallTypeHideCheckBox":
					BallTypeSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "BallSpeedHideCheckBox":
					BallSpeedSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "CourseHideCheckBox":
					CourseSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "HitTypeHideCheckBox":
					HitTypeSelect_StackPanel.Visibility = Visibility.Visible;
					break;
				case "DirHideCheckBox":
					DirSelect_StackPanel.Visibility = Visibility.Visible;
					break;

			}

		}

		private void Count_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string name = button.Name.Split('_')[0];
			switch (name)
			{
				case "oneBall":
					if (count_b == 1)
					{
						count_b = 0;
					}
					else
					{
						count_b = 1;
					}
					break;
				case "twoBall":
					if (count_b == 2)
					{
						count_b = 0;
					}
					else
					{
						count_b = 2;
					}
					break;
				case "threeBall":
					if (count_b == 3)
					{
						count_b = 0;
					}
					else
					{
						count_b = 3;
					}
					break;

				case "oneStrike":
					if (count_s == 1)
					{
						count_s = 0;
					}
					else
					{
						count_s = 1;
					}
					break;
				case "twoStrike":
					if (count_s == 2)
					{
						count_s = 0;
					}
					else
					{
						count_s = 2;
					}
					break;

				case "oneOut":
					if (count_o == 1)
					{
						count_o = 0;
					}
					else
					{
						count_o = 1;
					}
					break;
				case "twoOut":
					if (count_o == 2)
					{
						count_o = 0;
					}
					else
					{
						count_o = 2;
					}
					break;

			}
			countDisplay();
			AnalysisDataDisplay(player_id: player_id);

		}

		private void OutCountOnOff_CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}

		private void OutCountOnOff_CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}

		private void CourseSelect_Selection(object sender, SelectionChangedEventArgs e)
		{
			ListView lv = (ListView)sender;
			try
			{
				BallCourse.ballCourse courseData = (BallCourse.ballCourse)lv.SelectedValue;
				if (courseData != null)
				{
					selectCourseData = courseData;
					course_id = selectCourseData.course_id;
					AnalysisDataDisplay(player_id: player_id);
				}
			}
			catch { }

		}

		private void CourseSelectListView_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView lv = (ListView)sender;
			BallCourse.ballCourse courseData = (BallCourse.ballCourse)lv.SelectedValue;
			selectCourseData = courseData;
			course_id = selectCourseData.course_id;
			AnalysisDataDisplay(player_id: player_id);
		}

		private void CourseSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					CourseSelectFlg = true;
					CourseSelect_StackPanel.Visibility = Visibility.Visible;
					CourseHideCheckBox.IsEnabled = true;
				}
				else
				{
					CourseSelectFlg = false;
					CourseSelect_StackPanel.Visibility = Visibility.Collapsed;
					CourseHideCheckBox.IsEnabled = false;
					CourseHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
		}

		private void Course_CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Course_Check_Result();
		}

		private void Course_CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			Course_Check_Result();
		}

		private void Course_Check_Result()
		{
			course_A = (bool)Course_A_CheckBox.IsChecked;
			course_B = (bool)Course_B_CheckBox.IsChecked;
			course_C = (bool)Course_C_CheckBox.IsChecked;

			course_D = (bool)Course_D_CheckBox.IsChecked;
			course_E = (bool)Course_E_CheckBox.IsChecked;
			course_F = (bool)Course_F_CheckBox.IsChecked;

			course_G = (bool)Course_G_CheckBox.IsChecked;
			course_H = (bool)Course_H_CheckBox.IsChecked;
			course_I = (bool)Course_I_CheckBox.IsChecked;

			AnalysisDataDisplay(player_id: player_id);
		}


		/// 2022.03.31 
		/// Field画面の追加
		private void FieldImageSetHitPoint(int x, int y, int hit_id = 0, int hit_type_id = 0)
		{
			int zone_x_int = x / 2;
			int zone_y_int = y / 2;

			//if (!FiledDir(x: zone_x_int, y: zone_y_int)) { return; }


			//if (!FieldHitType(hit_type_id: hit_type_id)) { return; }

			/// 2022.04.03 
			/// 打席結果によるソート
			//if (!FieldHitResult(hit_id: hit_id)) { return; }

			var polygon = new Windows.UI.Xaml.Shapes.Polygon();
			polygon.Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue);
			var points = new PointCollection();
			switch (hit_type_id)
			{
				case 0:
					//points.Add(new Windows.Foundation.Point(-5, -5));
					//points.Add(new Windows.Foundation.Point(5, -5));
					//points.Add(new Windows.Foundation.Point(5, 5));
					//points.Add(new Windows.Foundation.Point(-5, 5));

					points.Add(new Point(1, -4));
					points.Add(new Point(4, -1));
					points.Add(new Point(4, 1));
					points.Add(new Point(1, 4));
					points.Add(new Point(-1, 4));
					points.Add(new Point(-4, 1));
					points.Add(new Point(-4, -1));
					points.Add(new Point(-1, -4));

					break;
				case 1:
					points.Add(new Point(-8, -6));
					points.Add(new Point(8, -6));
					points.Add(new Point(0, 4));
					//polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
					break;
				case 2:
					points.Add(new Point(0, -6));
					points.Add(new Point(-8, 4));
					points.Add(new Point(8, 4));
					//polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					break;
				case 3:
					points.Add(new Point(0, -8));
					points.Add(new Point(-6, 8));
					points.Add(new Point(6, 8));
					//polygon.Fill = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
					break;

				default:
					break;
			}

			/// box.res_hit
			/// 0:凡打
			/// 1:ヒット
			/// 2:エラー
			/// 3:野選
			/// 4:四死球
			/// 5:振り逃げ
			/// 6:犠打
			/// 7:犠飛
			switch (hit_id)
			{
				case 0:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
					break;
				case 1:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
					break;
				case 2:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Purple);
					break;
				case 3:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Gray);
					break;
				case 4:
					break;
				case 5:
					break;
				case 6:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
					break;
				case 7:
					polygon.Fill = new SolidColorBrush(Windows.UI.Colors.Green);
					break;
				default:
					break;

			}

			polygon.Points = points;
			polygon.Translation = new System.Numerics.Vector3(zone_x_int, zone_y_int, 0);
			FieldZone.Children.Add(polygon);
		}

		//private bool FiledDir(int x, int y)
		//{
		//	if (DirSelectFlg)
		//	{
		//		int field_point = DirArray[x, y];
		//		switch (field_point)
		//		{
		//			case 0:
		//				if ((bool)LetfDir.IsChecked)
		//				{
		//					return true;
		//				}
		//				break;
		//			case 1:
		//				if ((bool)CenterDir.IsChecked)
		//				{
		//					return true;
		//				}
		//				break;
		//			case 2:
		//				if ((bool)RightDir.IsChecked)
		//				{
		//					return true;
		//				}
		//				break;
		//			default:
		//				break;
		//		}
		//	}
		//	else
		//	{
		//		return true;
		//	}

		//	return false;
		//}

		private bool FieldHitType(int hit_type_id = 0)
		{
			if (HitTypeSelectFlg)
			{
				switch (HitTypeId)
				{
					/// なにもなし
					case 0:
						if (hit_type_id == HitTypeId)
						{
							return true;
						}
						else { return false; }

					/// ゴロ
					case 1:
						if (hit_type_id == HitTypeId)
						{
							return true;
						}
						else { return false; }

					/// フライ
					case 2:
						if (hit_type_id == HitTypeId)
						{
							return true;
						}
						else { return false; }

					/// ライナー
					case 3:
						if (hit_type_id == HitTypeId)
						{
							return true;
						}
						else { return false; }
					default:
						return false;
				}
			}
			else { return true; }
		}


		private bool FieldHitResult(int hit_id = 0)
		{
			if (HitResultSelectFlg)
			{
				bool outFlg = (bool)HitResultOut.IsChecked;
				bool hitFlg = (bool)HitResultHit.IsChecked;
				bool errorFlg = (bool)HitResultError.IsChecked;
				bool otherFlg = (bool)HitResultOther.IsChecked;
				switch (hit_id)
				{
					/// OUT
					case 0:
						if (outFlg)
						{
							return true;
						}
						else { return false; }

					/// Hit
					case 1:
						if (hitFlg)
						{
							return true;
						}
						else { return false; }

					/// ERROR
					case 2:
						if (errorFlg)
						{
							return true;
						}
						else { return false; }

					default:
						if (otherFlg)
						{
							return true;
						}
						return false;
				}
			}
			else { return true; }
		}


		private void AnalysisFieldDataDisplay(int player_id = -1)
		{
			if (player_id < 0) { return; }
			string tmp_start_datetime = "1900-01-01";
			string tmp_end_datetime = "2100-12-31";
			int tmp_bat = -1;
			int tmp_ining = -1;
			int tmp_run_exsist = -1;
			int tmp_run_1 = -1;
			int tmp_run_2 = -1;
			int tmp_run_3 = -1;
			int tmp_count_b = -1;
			int tmp_count_s = -1;
			int tmp_count_o = -1;
			int tmp_ball_type = -1;
			int tmp_ball_action = -1;
			int tmp_min_ball_speed = -1;
			int tmp_max_ball_speed = -1;
			int tmp_course_id = -1;

			int tmp_batball_id = -1;
			int tmp_hit_type_id = -1;
			int tmp_dir_id = -1;
			int tmp_dir_left = -1;
			int tmp_dir_center = -1;
			int tmp_dir_right = -1;
			int tmp_hit_id = -1;

			int tmp_res_hit_type_id = -1;

			bool outFlg = false;
			bool hitFlg = false;
			bool errorFlg = false;
			bool otherFlg = false;


			if (DateTimeSelectFlg)
			{
				tmp_start_datetime = start_datetime;
				tmp_end_datetime = end_datetime;
			}

			if (BatSelectFlg)
			{
				tmp_bat = bat_id;
			}


			if (IningSelectFlg)
			{
				tmp_ining = ining_id;
			}
			if (runner_exsist == 1)
			{
				tmp_run_exsist = runner_exsist;
			}
			if (RunnerSelectFlg)
			{
				tmp_run_1 = RunnerCheck_Int(Convert.ToBoolean(run_1.IsChecked));
				tmp_run_2 = RunnerCheck_Int(Convert.ToBoolean(run_2.IsChecked));
				tmp_run_3 = RunnerCheck_Int(Convert.ToBoolean(run_3.IsChecked));
			}
			if (CountSelectFlg)
			{
				tmp_count_b = count_b;
				tmp_count_s = count_s;
				if ((bool)OutCountOnOff_CheckBox.IsChecked)
				{
					tmp_count_o = count_o;
				}
			}
			if (BallTypeSelectFlg)
			{
				tmp_ball_type = ball_type_id;
			}
			if (BallSpeedSelectFlg)
			{
				tmp_min_ball_speed = min_ball_speed;
				tmp_max_ball_speed = max_ball_speed;
			}
			if (BallActionSelectFlg)
			{
				tmp_ball_action = ball_action_id;
			}

			if (CourseSelectFlg)
			{
				tmp_course_id = course_id;
				tmp_course_id = 1;
			}

			if (HitTypeSelectFlg)
			{
				tmp_hit_type_id = HitTypeId;
			}

			if (DirSelectFlg)
			{
				if ((bool)LetfDir.IsChecked)
				{
					tmp_dir_left = 0;
				}
				if ((bool)CenterDir.IsChecked)
				{
					tmp_dir_center = 1;
				}
				if ((bool)RightDir.IsChecked)
				{
					tmp_dir_right = 2;
				}
			}

			if (HitResultSelectFlg)
			{
				outFlg = (bool)HitResultOut.IsChecked;
				hitFlg = (bool)HitResultHit.IsChecked;
				errorFlg = (bool)HitResultError.IsChecked;
				otherFlg = (bool)HitResultOther.IsChecked;
			}

			if (HitTypeSelectFlg)
			{
				tmp_res_hit_type_id = HitTypeId;
			}

			List<BoxData.boxDataIning> boxs = new List<BoxData.boxDataIning>();
			if (AnalysFoucusFlg)
			{

				boxs = BoxData.GetRecordsDataAnalysis(
					pitcher_id: player_id,
					start_datetime: tmp_start_datetime,
					end_datetime: tmp_end_datetime,
					bat: tmp_bat,
					ining: tmp_ining,
					count_b: tmp_count_b,
					count_s: tmp_count_s,
					count_o: tmp_count_o,
					run_exsist: tmp_run_exsist,
					run_1: tmp_run_1,
					run_2: tmp_run_2,
					run_3: tmp_run_3,
					ball_type: tmp_ball_type,
					ball_action: tmp_ball_action,
					min_ball_speed: tmp_min_ball_speed,
					max_ball_speed: tmp_max_ball_speed,
					course_id: tmp_course_id,
					course_A: course_A,
					course_B: course_B,
					course_C: course_C,
					course_D: course_D,
					course_E: course_E,
					course_F: course_F,
					course_G: course_G,
					course_H: course_H,
					course_I: course_I,
					field_direct_id_left:tmp_dir_left,
					field_direct_id_center:tmp_dir_center,
					field_direct_id_right: tmp_dir_right,
					res_hit_flg: HitResultSelectFlg,
					res_hit_out: outFlg,
					res_hit_hit: hitFlg,
					res_hit_error: errorFlg,
					res_hit_other: otherFlg,
					res_hit_type: tmp_res_hit_type_id
					);
			}
			if (!AnalysFoucusFlg)
			{
				boxs = BoxData.GetRecordsDataAnalysis(
					player_id: player_id,
					start_datetime: tmp_start_datetime,
					end_datetime: tmp_end_datetime,
					bat: tmp_bat,
					ining: tmp_ining,
					count_b: tmp_count_b,
					count_s: tmp_count_s,
					count_o: tmp_count_o,
					run_exsist: tmp_run_exsist,
					run_1: tmp_run_1,
					run_2: tmp_run_2,
					run_3: tmp_run_3,
					ball_type: tmp_ball_type,
					ball_action: tmp_ball_action,
					min_ball_speed: tmp_min_ball_speed,
					max_ball_speed: tmp_max_ball_speed,
					course_id: tmp_course_id,
					course_A: course_A,
					course_B: course_B,
					course_C: course_C,
					course_D: course_D,
					course_E: course_E,
					course_F: course_F,
					course_G: course_G,
					course_H: course_H,
					course_I: course_I,
					field_direct_id_left: tmp_dir_left,
					field_direct_id_center: tmp_dir_center,
					field_direct_id_right: tmp_dir_right,
					res_hit_flg: HitResultSelectFlg,
					res_hit_out: outFlg,
					res_hit_hit: hitFlg,
					res_hit_error: errorFlg,
					res_hit_other: otherFlg,
					res_hit_type: tmp_res_hit_type_id
					);
			}
			Delete_Box();
			foreach (BoxData.boxDataIning box in boxs)
			{
				/// 三振や四死球等があった場合
				/// x,yが0の値となっているため
				/// 0以外を表示する
				/// ※データが適切に更新されていない場合も
				if (box.res_course_x != 0 && box.res_course_y != 0)
				{
					FieldImageSetHitPoint(
								x: box.res_course_x,
								y: box.res_course_y,
								hit_id: box.res_hit,
								hit_type_id: box.res_hit_type
								);
				}
			}

			//displayZoneFlg = true;
		}

		private void Delete_Box()
		{
			//while (FieldZone.Children.Count > 1)
			/// 2022.03.31 Lineも含める
			while (FieldZone.Children.Count > 3)
			{
				FieldZone.Children.RemoveAt(FieldZone.Children.Count - 1);
			}
		}

		private void Dir_Item_Clicked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}

		private void Dir_Unchecked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
		}


		private void HitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			HitTypeId = comboBox.SelectedIndex;
			AnalysisDataDisplay(player_id: player_id);
			//AnalysisFieldDataDisplay(player_id: player_id);
		}

		private void HitTypeSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					HitTypeSelectFlg = true;
					HitTypeSelect_StackPanel.Visibility = Visibility.Visible;
					HitTypeHideCheckBox.IsEnabled = true;
				}
				else
				{
					HitTypeSelectFlg = false;
					HitTypeSelect_StackPanel.Visibility = Visibility.Collapsed;
					HitTypeHideCheckBox.IsEnabled = false;
					HitTypeHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
			//AnalysisFieldDataDisplay(player_id: player_id);

		}

		private void DirSelect_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					DirSelectFlg = true;
					DirSelect_StackPanel.Visibility = Visibility.Visible;
					DirHideCheckBox.IsEnabled = true;
				}
				else
				{
					DirSelectFlg = false;
					DirSelect_StackPanel.Visibility = Visibility.Collapsed;
					DirHideCheckBox.IsEnabled = false;
					DirHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);

		}


		//private void DirArrayInitialize()
		//{
		//	int leftLine = 120;
		//	int rightLine = 300;

		//	int row = 0;
		//	int count = 0;
		//	for (int col = 0; col <= COLLIMIT; col++)
		//	{
		//		/// レフト範囲は0を代入
		//		if (leftLine >= col)
		//		{
		//			DirArray[col, row] = 0;
		//		}
		//		/// センター範囲は1を代入
		//		else if (leftLine < col && rightLine >= col)
		//		{
		//			DirArray[col, row] = 1;
		//		}
		//		/// ライト範囲は2を代入
		//		else if (rightLine < col && COLLIMIT > col)
		//		{
		//			DirArray[col, row] = 2;
		//		}
		//		else if (COLLIMIT - 1 <= col)
		//		{
		//			col = 0;
		//			row++;
		//			count++;
		//			if (count == 3)
		//			{
		//				leftLine++;
		//				rightLine--;
		//				count = 0;
		//			}
		//		}
		//		if (row >= ROWLIMIT)
		//		{
		//			break;
		//		}
		//	}
		//}

		private void HitResultItemClicked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
			//AnalysisFieldDataDisplay(player_id: player_id);
		}

		private void HitResultItemUnchecked(object sender, RoutedEventArgs e)
		{
			AnalysisDataDisplay(player_id: player_id);
			//AnalysisFieldDataDisplay(player_id: player_id);
		}

		private void HitReult_toggleSwitch(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					HitResultSelectFlg = true;
					HitResultSelect_StackPanel.Visibility = Visibility.Visible;
					HitResultHideCheckBox.IsEnabled = true;
				}
				else
				{
					HitResultSelectFlg = false;
					HitResultSelect_StackPanel.Visibility = Visibility.Collapsed;
					HitResultHideCheckBox.IsEnabled = false;
					HitResultHideCheckBox.IsChecked = false;
				}
			}
			AnalysisDataDisplay(player_id: player_id);
			//AnalysisFieldDataDisplay(player_id: player_id);

		}

		private void AnalysFlg_ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					AnalysFoucusFlg = true;

					try
					{
						if (!(PlayerName is null))
						{
							BatToggle.Header = "打席別";
							PlayerName.ItemsSource = PlayerData.GetRecordsPitchers();
						}
					}
					catch
					{ }
				}
				if (toggleSwitch.IsOn == false)
				{
					AnalysFoucusFlg = false;
					BatToggle.Header = "投球別";
					PlayerName.ItemsSource = PlayerData.GetRecords();
				}
			}
		}


		private bool FieldDirectCreate()
		{
			List<BoxData.boxData> boxDatas = BoxData.GetRecords();
			foreach (BoxData.boxData box in boxDatas)
			{
				FieldDirDataCreate fieldDir
					= new FieldDirDataCreate(
									box_id: box.box_id,
									ball_id: box.last_ball_type,
									game_id: box.game_id,
									player_id: box.player_id,
									pit_player_id: box.pitcher_id
									);
				if (box.res_course_x == 0 && box.res_course_y == 0) { continue; }

				fieldDir.FiledDirupdate(x: box.res_course_x, y: box.res_course_y);
				BoxFieldDir.addRecord(
					box_id: fieldDir.box_id,
					ball_id: fieldDir.ball_id,
					game_id: fieldDir.game_id,
					player_id: fieldDir.player_id,
					pit_player_id: fieldDir.pit_player_id,
					field_dir_id: fieldDir.field_dir_id
					);

			}

			return true;
		}

		private bool CheckFieldDirectBoxID()
		{
			List<BoxFieldDir.boxFieldDir> boxFieldDirs = BoxFieldDir.GetRecords();
			List<BoxData.boxData> boxDatas = BoxData.GetRecords();
			foreach (BoxData.boxData box in boxDatas)
			{
				if (box.res_course_x == 0 && box.res_course_y == 0) { continue; }
				int count = boxFieldDirs.Count(s => s.box_id == box.box_id);
				if (count == 0)
				{
					FieldDirDataCreate fieldDir
							= new FieldDirDataCreate(
									box_id: box.box_id,
									ball_id: box.last_ball_type,
									game_id: box.game_id,
									player_id: box.player_id,
									pit_player_id: box.pitcher_id
									);

					fieldDir.FiledDirupdate(x: box.res_course_x, y: box.res_course_y);

					BoxFieldDir.addRecord(
						box_id: fieldDir.box_id,
						ball_id: fieldDir.ball_id,
						game_id: fieldDir.game_id,
						player_id: fieldDir.player_id,
						pit_player_id: fieldDir.pit_player_id,
						field_dir_id: fieldDir.field_dir_id
						);
				}
			}
			return true;
		}

		private bool CheckHitResultBoxID()
		{
			List<BoxResult.boxResult> boxResults = BoxResult.GetRecords();
			List<BoxData.boxData> boxDatas = BoxData.GetRecords();
			foreach (BoxData.boxData box in boxDatas)
			{
				//if (box.res_course_x == 0 && box.res_course_y == 0) { continue; }
				int count = boxResults.Count(s => s.box_id == box.box_id);
				if (count == 0)
				{
					BoxResultDataCreate boxResultData
							= new BoxResultDataCreate(
									box_id: box.box_id,
									ball_id: box.last_ball_type,
									game_id: box.game_id,
									player_id: box.player_id,
									pit_player_id: box.pitcher_id,
									hit_id:box.res_hit,
									hit_type_id:box.res_hit_type
									);



					BoxResult.addRecord(
						box_id: boxResultData.box_id,
						ball_id: boxResultData.ball_id,
						game_id: boxResultData.game_id,
						player_id: boxResultData.player_id,
						pit_player_id: boxResultData.pit_player_id,
						hit_id: boxResultData.hit_id,
						hit_type_id: boxResultData.hit_type_id
						);
				}
			}
			return true;
		}

	}
}
