using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MMSS
{
	class UseCommon
	{

	}
	public class Throw
	{
		public int id { get; set; }
		public string name { get; set; }
		public Throw(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
		public int GetThrowId()
		{
			return this.id;
		}

		public string GetThrowName()
		{
			return this.name;
		}
	}

	public class BatBox
	{
		public int id { get; set; }
		public string name { get; set; }
		public BatBox(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public int GetBatBoxId()
		{
			return this.id;
		}
		public string GetBatBoxName()
		{
			return this.name;
		}

	}

	public class Selected
	{
		public int id { get; set; }
		public string name { get; set; }
		public Selected(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public int GetSelectdId()
		{
			return this.id;
		}
		public string GetName()
		{
			return this.name;
		}
	}

	public class Position
	{
		public int id { get; set; }
		public string name { get; set; }
		public Position(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
		public int GetPositionId()
		{
			return this.id;
		}
		public string GetPositionName()
		{
			return this.name;
		}
	}

	public class PositionExChange
	{
		public string position_name { get; set; }
		public int position { get; }
		public PositionExChange(string position_name = "")
		{
			this.position_name = position_name;
		}
		private void Initialize()
		{

		}
	}

	public class LangClass
	{
		public string position { get; set; }
		public string hand { get; set; }
		public string bat { get; set; }
		public LangClass(string position, string hand, string bat)
		{
			this.position = position;
			this.hand = hand;
			this.bat = bat;
		}
		public int GetPosition()
		{
			string position_upper_string = PositionStringToUpper();
			if (CheckPitcher(position_upper_string))
			{
				return 1;
			}
			if (CheckChatcher(position_upper_string))
			{
				return 2;
			}
			if (CheckFirst(position_upper_string))
			{
				return 3;
			}
			if (CheckSecond(position_upper_string))
			{
				return 4;
			}
			if (CheckThird(position_upper_string))
			{
				return 5;
			}
			if (CheckShortStop(position_upper_string))
			{
				return 6;
			}

			if (CheckLeftFielder(position_upper_string))
			{
				return 7;
			}

			if (CheckCenterFielder(position_upper_string))
			{
				return 8;
			}

			if (CheckRightFielder(position_upper_string))
			{
				return 9;
			}
			if (CheckDH(position_upper_string))
			{
				return 10;
			}
			return 0;
		}

		private bool CheckPitcher(string position_upper_string)
		{
			if (position_upper_string == "1") { return true; }
			if (position_upper_string.Contains("P")) { return true; }
			if (position_upper_string.Contains("投")) { return true; }
			return false;
		}
		private bool CheckChatcher(string position_upper_string)
		{
			if (position_upper_string == "2") { return true; }
			if (position_upper_string.Contains("C") && position_upper_string.Length == 1) { return true; }
			if (position_upper_string.Contains("捕")) { return true; }
			return false;
		}
		private bool CheckFirst(string position_upper_string)
		{
			if (position_upper_string == "3") { return true; }
			if (position_upper_string == "1B") { return true; }
			if (position_upper_string.Contains("FIRST")) { return true; }
			if (position_upper_string.Contains("一")) { return true; }
			return false;
		}
		private bool CheckSecond(string position_upper_string)
		{
			if (position_upper_string == "4") { return true; }
			if (position_upper_string == "2B") { return true; }
			if (position_upper_string.Contains("SECOND")) { return true; }
			if (position_upper_string.Contains("二")) { return true; }
			return false;
		}
		private bool CheckThird(string position_upper_string)
		{
			if (position_upper_string == "5") { return true; }
			if (position_upper_string == "3B") { return true; }
			if (position_upper_string.Contains("THIRD")) { return true; }
			if (position_upper_string.Contains("三")) { return true; }
			return false;
		}
		private bool CheckShortStop(string position_upper_string)
		{
			if (position_upper_string == "6") { return true; }
			if (position_upper_string.Contains("S") && position_upper_string.Length == 1) { return true; }
			if (position_upper_string == "SS") { return true; }
			if (position_upper_string.Contains("遊")) { return true; }
			return false;
		}
		private bool CheckLeftFielder(string position_upper_string)
		{
			if (position_upper_string == "7") { return true; }
			if (position_upper_string.Contains("L")) { return true; }
			if (position_upper_string == "LF") { return true; }
			if (position_upper_string.Contains("左")) { return true; }
			return false;
		}
		private bool CheckCenterFielder(string position_upper_string)
		{
			if (position_upper_string == "8") { return true; }
			if (position_upper_string == "CF") { return true; }
			if (position_upper_string.Contains("中")) { return true; }
			return false;
		}
		private bool CheckRightFielder(string position_upper_string)
		{
			if (position_upper_string == "9") { return true; }
			if (position_upper_string == "RF") { return true; }
			if (position_upper_string.Contains("右")) { return true; }
			return false;
		}
		private bool CheckDH(string position_upper_string)
		{
			if (position_upper_string == "10") { return true; }
			if (position_upper_string == "D") { return true; }
			return false;
		}

		private string PositionStringToUpper()
		{
			return this.position.ToUpper();
		}
		private string HandStringToUpper()
		{
			return this.hand.ToUpper();
		}
		private string BatStringToUpper()
		{
			return this.bat.ToUpper();
		}

		public int GetHand_id()
		{
			string hand_upper_string = HandStringToUpper();
			if (CheckHandLeft(hand_upper_string))
			{
				return 0;
			}
			if (CheckHandRight(hand_upper_string))
			{
				return 1;
			}
			return 0;
		}
		private bool CheckHandRight(string hand_upper_string)
		{
			if (hand_upper_string.Contains("R")) { return true; }
			if (hand_upper_string.Contains("右")) { return true; }
			return false;
		}
		private bool CheckHandLeft(string hand_upper_string)
		{
			if (hand_upper_string.Contains("L")) { return true; }
			if (hand_upper_string.Contains("左")) { return true; }
			return false;
		}

		public int GetBat_id()
		{
			string bat_upper_string = BatStringToUpper();
			if (CheckBatLeft(bat_upper_string))
			{
				return 0;
			}
			if (CheckBatRight(bat_upper_string))
			{
				return 1;
			}
			if (CheckBatSwitch(bat_upper_string))
			{
				return 2;
			}
			return 0;
		}
		private bool CheckBatRight(string hand_upper_string)
		{
			if (hand_upper_string.Contains("R")) { return true; }
			if (hand_upper_string.Contains("右")) { return true; }
			return false;
		}
		private bool CheckBatLeft(string hand_upper_string)
		{
			if (hand_upper_string.Contains("L")) { return true; }
			if (hand_upper_string.Contains("左")) { return true; }
			return false;
		}
		private bool CheckBatSwitch(string hand_upper_string)
		{
			if (hand_upper_string.Contains("S")) { return true; }
			if (hand_upper_string.Contains("W")) { return true; }
			if (hand_upper_string.Contains("両")) { return true; }
			return false;
		}

	}


	public class Member
	{
		public int player_id { get; set; }
		public int team_id { get; set; }
		public string order_id { get; set; }
		public string name { get; set; }
		public int position_id { get; set; }
		public int hand_id { get; set; }
		public int bat_id { get; set; }
		public int number { get; set; }
		public Member(
				int player_id = 0,
				int team_id = 0,
				string order_id = "",
				string name = "",
				int position = 0,
				int hand_id = 0,
				int bat_id = 0,
				int number = -1
			)
		{
			this.player_id = player_id;
			this.team_id = team_id;
			this.order_id = order_id;
			this.name = name;
			this.position_id = position;
			this.hand_id = hand_id;
			this.bat_id = bat_id;
			this.number = number;
		}
	}

	public class ReserveMember
	{
		public int player_id { get; set; }
		public int team_id { get; set; }
		public string order_id { get; set; }
		public string name { get; set; }
		public int position_id { get; set; }
		public int hand_id { get; set; }
		public int bat_id { get; set; }
		public int number { get; set; }
		public ReserveMember(
				int player_id = 0,
				int team_id = 0,
				string order_id = "",
				string name = "",
				int position = 0,
				int hand_id = 0,
				int bat_id = 0,
				int number = -1
			)
		{
			this.player_id = player_id;
			this.team_id = team_id;
			this.order_id = order_id;
			this.name = name;
			this.position_id = position;
			this.hand_id = hand_id;
			this.bat_id = bat_id;
			this.number = number;
		}
	}


	public class NaviewNavi
	{
		public Frame frame { get; set; }
		public NaviewNavi(Frame frame)
		{
			this.frame = frame;
		}
		public void NavView_Navigate(NavigationViewItem item)
		{
			switch (item.Tag)
			{
				case "Top":
					frame.Navigate(typeof(MainPage));
					break;
				case "Order":
					frame.Navigate(typeof(OrderPage));
					//Frame.Navigate(typeof(GameOrders));
					break;
				case "Score":
					frame.Navigate(typeof(ScorePage));
					break;
				//case "Field":
				//    Frame.Navigate(typeof(FieldPage));
				//    break;
				case "Data":
					//Frame.Navigate(typeof(DataPage));
					frame.Navigate(typeof(DataAnalysisPage));
					break;
					//case "Opt":
					//    Frame.Navigate(typeof(GameOptions));
					//    break;
					//case "Team":
					//    Frame.Navigate(typeof(GameMatchTeamSelect));
					//    break;
			}
		}
	}

	public class FieldDirDataCreate
	{
		public int box_id { get; set; }
		public int ball_id { get; set; }
		public int game_id { get; set; }
		public int player_id { get; set; }
		public int pit_player_id { get; set; }
		public int field_dir_id { get; set; }

		private const int ROWLIMIT = 300;
		private const int COLLIMIT = 420;
		private int[,] DirArray = new int[COLLIMIT, ROWLIMIT];
		public FieldDirDataCreate(int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1
								//int field_dir_id = -1
				)
		{
			this.box_id = box_id;
			this.ball_id = ball_id;
			this.game_id = game_id;
			this.player_id = player_id;
			this.pit_player_id = pit_player_id;
			this.field_dir_id = field_dir_id;
			DirArrayInitialize();
		}
		private void DirArrayInitialize()
		{
			int leftLine = 120;
			int rightLine = 300;

			int row = 0;
			int count = 0;
			for (int col = 0; col <= COLLIMIT; col++)
			{
				/// レフト範囲は0を代入
				if (leftLine >= col)
				{
					DirArray[col, row] = 0;
				}
				/// センター範囲は1を代入
				else if (leftLine < col && rightLine >= col)
				{
					DirArray[col, row] = 1;
				}
				/// ライト範囲は2を代入
				else if (rightLine < col && COLLIMIT > col)
				{
					DirArray[col, row] = 2;
				}
				else if (COLLIMIT - 1 <= col)
				{
					col = 0;
					row++;
					count++;
					if (count == 3)
					{
						leftLine++;
						rightLine--;
						count = 0;
					}
				}
				if (row >= ROWLIMIT)
				{
					break;
				}
			}
		}
		public void FiledDirupdate(int x, int y)
		{
			int half_x = x / 2;
			int half_y = y / 2;
			this.field_dir_id = DirArray[half_x, half_y];
		}
	}

	public class BoxResultDataCreate
	{
		public int box_id { get; set; }
		public int ball_id { get; set; }
		public int game_id { get; set; }
		public int player_id { get; set; }
		public int pit_player_id { get; set; }
		public int hit_id { get; set; }
		public int hit_type_id { get; set; }

		public BoxResultDataCreate(int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int hit_id = -1,
								int hit_type_id = -1
				)
		{
			this.box_id = box_id;
			this.ball_id = ball_id;
			this.game_id = game_id;
			this.player_id = player_id;
			this.pit_player_id = pit_player_id;
			this.hit_id = hit_id;
			this.hit_type_id = hit_type_id;
			DirArrayInitialize();
		}
		private void DirArrayInitialize()
		{
			
		}
		public void FiledDirupdate(int x, int y)
		{

		}


	}


}
