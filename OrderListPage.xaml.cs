using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
	public sealed partial class OrderListPage : Page
	{
		public OrderListPage()
		{
			this.InitializeComponent();

			LangInitialize();

			TextBoesInitialize();

			//game_id = GameData.GetRecordsCount()[0].game_count;
			//game = GameData.GetRecords(game_id: game_id);
			game_id = GameData.GetGameIdRecord()[0].game_id - 1;
			game = GameData.GetRecords(game_id);
			topFirstStartFlg = game[0].top_order_start_flg;
			btmFirstStartFlg = game[0].btm_order_start_flg;
			topTeamId = game[0].bat_first_team_id;
			btmTeamId = game[0].field_first_team_id;
			GameStartFlg = game[0].game_start_flg;
			if (!topFirstStartFlg && !btmFirstStartFlg)
			{
				TitleNameOrder.Text = "先攻";
				selectTeamId = topTeamId;
				selectTopBtmSt = "先攻チーム";
			}
			else if (topFirstStartFlg && !btmFirstStartFlg)
			{
				TitleNameOrder.Text = "後攻";
				selectTeamId = btmTeamId;
				selectTopBtmSt = "後攻チーム";
			}
			else
			{
				TitleNameOrder.Text = "先攻";
				selectTeamId = topTeamId;
				GamePlayingPlayerChangeMsg();
			}

			PlayerData.InitializeDB();
			//players = PlayerData.GetRecords(team_id: selectTeamId, selected:0);
			players = PlayerData.GetRecordsAllMember(team_id: selectTeamId, selected: 0);


			/// 出場選手リスト
			oldGame = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "game"
										);
			/// 控え選手リスト
			oldReserve = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "0"
										);


			/// 出場選手リスト
			gamePlayers = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "game"
										);
			/// 控え選手リスト
			reservePlayers = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "0"
										);
			/// 2022.01.08
			/// PlayOrderクラスから
			/// PlayerDataクラスデータへ置き換え作業を実施する
			TeamOrderNameListView.ItemsSource = gamePlayers;
			TeamOrderIdListView.ItemsSource = gamePlayers;
			TeamOrderPositionListView.ItemsSource = gamePlayers;
			TeamOrderBatListView.ItemsSource = gamePlayers;
			TeamOrderHandListView.ItemsSource = gamePlayers;
			TeamReserveNameListView.ItemsSource = reservePlayers;


			PlayOrder.InitializeDB();

			template = PlayOrder.GetRecords(team_id: selectTeamId.ToString());
			OrderTemplate = PlayOrder.GetRecords(team_id: selectTeamId.ToString());
			ReserveTemplate = PlayOrder.GetRecordsReserve(team_id: selectTeamId.ToString());
			NewCreatePlayerHandString = "右";
			NewCreatePlayerBatString = "右";


			//PutListDataAtPlayer();

			/// 2022.05.21
			bool retryFlg = false;

			if (CheckPositionCountExsist())
			{
				retryFlg = true;
			}
			if (CheckOrderCountExsist())
			{
				retryFlg = true;
			}
			if (retryFlg)
			{
				players = PlayerData.GetRecordsAllMember(team_id: selectTeamId, selected: 0);
			}

			PlayersToLists();

			ListToCopy();

			MembersListCreate();

			GetPlayersDataInitialize();

		}


		private bool TappedFlg = false;
		private string ChangeNameBefore;
		private string ChangeNameAfter;
		private int ChangeOrderBeforeIndex;
		private int ChangeOrderAfterIndex;
		private string ChangeOrderBeforeOrderId;
		private string ChangeOrderAfterOrderId;
		private int ChangeOrderBeforePlayerId;
		private int ChangeOrderAfterPlayerId;
		private string ChangeOrderBeforeBat;
		private string ChangeOrderBeforeHand;
		private string ChangeOrderAfterBat;
		private string ChangeOrderAfterHand;
		private string ChangeOrderBeforePosition;
		private string ChangeOrderAfterPosition;
		private List<PlayOrder.playOrder> OrderTemplate;
		private List<PlayOrder.playOrder> ReserveTemplate;
		private List<PlayOrder.playOrder> template;

		private List<PlayerData.playerData> players;
		private List<PlayerData.playerData> gamePlayers;
		private List<PlayerData.playerData> reservePlayers;


		private List<PlayerData.playerData> oldGame;
		private List<PlayerData.playerData> oldReserve;

		private List<PlayerData.playerData> oldTopPlayerData;
		private List<PlayerData.playerData> oldBtmPlayerData;



		private bool PositionFlg = false;
		private string ChangePositionBefore;
		private string ChangePositionAfter;
		private string ChangePositionNameBefore;
		private string ChangePositionNameAfter;
		private int positionBeforeIndex;
		private int positionAfterIndex;
		private int ChangePositionBeforePlayerId;
		private int ChangePositionAfterPlayerId;
		private string ChangePositionBeforeOrderId;
		private string ChangePositionAfterOrderId;

		private bool GameStartFlg = false;      // 試合前:false 試合中:true
		private bool ReserveViewFlg = false;    // 

		private List<GameData.gameData> game;
		private bool topFirstStartFlg;
		private bool btmFirstStartFlg;
		private int game_id;

		private int topTeamId;
		private int btmTeamId;
		private int selectTeamId;
		private string selectTopBtmSt;
		private string NewCreatePlayerHandString = "右";
		private string NewCreatePlayerBatString = "右";

		private string searchText = "";
		private int seachTextNum = -1;

		ObservableCollection<Throw> Throws = new ObservableCollection<Throw>();
		ObservableCollection<BatBox> BatBoxes = new ObservableCollection<BatBox>();
		ObservableCollection<Position> Positions = new ObservableCollection<Position>();
		ObservableCollection<Member> Members = new ObservableCollection<Member>();
		ObservableCollection<ReserveMember> ReserveMembers = new ObservableCollection<ReserveMember>();

		private string thr_r;
		private string thr_l;
		private string batbox_r;
		private string batbox_l;
		private string batbox_sw;
		private string pos_p;
		private string pos_c;
		private string pos_1b;
		private string pos_2b;
		private string pos_3b;
		private string pos_ss;
		private string pos_lf;
		private string pos_cf;
		private string pos_rf;
		private string pos_dh;


		private List<UseDisplay.display> disp_throw;
		private List<UseDisplay.display> disp_batbox;
		private List<UseDisplay.display> disp_position;

		string[] order_list = new string[30];
		string[] name_list = new string[30];
		int[] position_list = new int[10];
		int[] hand_list = new int[30];
		int[] bat_list = new int[30];
		int[] no_list = new int[30];
		int[] player_list = new int[30];
		int[] team_list = new int[30];

		/// <summary>
		///  Setting to reset
		/// </summary>
		string[] old_order_list = new string[30];
		string[] old_name_list = new string[30];
		int[] old_position_list = new int[10];
		int[] old_hand_list = new int[30];
		int[] old_bat_list = new int[30];
		int[] old_no_list = new int[30];
		int[] old_player_list = new int[30];
		int[] old_team_list = new int[30];


		private int stop_count = 30;

		private int selectedLang = UseLanguage.GetRecords(selected: 1)[0].id;

		private const int setFontSize_text = 14;


		private Member DragMember;
		private Member DropMember;

		private int DragItemId;
		private int DropItemId;

		private const int TOP_CODE = 0;
		private const int BTM_CODE = 1;


		private bool CheckPositionCountExsist()
		{
			bool check_flg = false;
			int position_count = 0;
			for (int count = 1; count <= 10; count++)
			{
				position_count = players.Where(p => p.position == count).Count();
				if (!CheckPositionDuplicationExist(position_count))
				{
					/// ポジション重複時
					int duplication_player_id = players.Where(p => p.position == count).First().player_id;
					PlayerData.updateRecord(player_id: duplication_player_id, position: 0, etc_str2: "R");
					check_flg = true;
				}
				if (!CheckPositionNotExist(position_count))
				{
					/// ポジションが存在しない時
					int reserve_player_id = players.Where(p => p.position == 0).First().player_id;
					PlayerData.updateRecord(player_id: reserve_player_id, position: count);
					check_flg = true;
				}
			}
			return check_flg;
		}

		private bool CheckPositionDuplicationExist(int position_count)
		{
			if (position_count > 1) { return false; }
			return true;
		}

		private bool CheckPositionNotExist(int position_count)
		{
			if (position_count == 0) { return false; }
			return true;
		}


		private bool CheckOrderCountExsist()
		{
			bool check_flg = false;
			int order = 0;
			for (int count = 1; count < 10; count++)
			{
				order = players.Where(p => p.etc_str2 == count.ToString()).Count();
				if (!CheckOrderDuplicationExist(order))
				{
					/// 打順重複時
					int duplication_player_id = players.Where(p => p.etc_str2 == count.ToString()).First().player_id;
					PlayerData.updateRecord(player_id: duplication_player_id, etc_str2: "R");
					check_flg = true;
				}
				if (!CheckOrderNotExist(order))
				{
					/// 打順が存在しない時
					int reserve_player_id = players.Where(p => p.etc_str2 == "R").First().player_id;
					PlayerData.updateRecord(player_id: reserve_player_id, etc_str2: count.ToString());
					check_flg = true;
				}
			}
			order = players.Where(p => p.etc_str2 == "P").Count();
			if (!CheckOrderDuplicationExist(order))
			{
				/// 打順重複時
				int duplication_player_id = players.Where(p => p.etc_str2 == "P").First().player_id;
				PlayerData.updateRecord(player_id: duplication_player_id, etc_str2: "R");
				check_flg = true;
			}
			if (!CheckOrderNotExist(order))
			{
				/// 打順が存在しない時
				int reserve_player_id = players.Where(p => p.etc_str2 == "R").First().player_id;
				PlayerData.updateRecord(player_id: reserve_player_id, etc_str2: "P");
				check_flg = true;
			}
			return check_flg;

		}

		private bool CheckOrderDuplicationExist(int order)
		{
			if (order > 1) { return false; }
			return true;
		}

		private bool CheckOrderNotExist(int order)
		{
			if (order == 0) { return false; }
			return true;
		}



		private void PlayerChangeInitialize()
		{
			ClearLists();
			//ObserversClear();
			PlayersToLists();

			ListToCopy();

			MembersListCreate();
		}

		private void ClearLists()
		{
			name_list = new string[]
									{ "", "", "", "", "",
									  "", "", "", "", "",
									 "", "", "", "", "",
									 "", "", "", "", "",
									 "", "", "", "", "",
									 "", "", "", "", ""};
			position_list = new int[10];
			hand_list = new int[30];
			bat_list = new int[30];
			no_list = new int[] { -1, -1, -1, -1, -1,
								  -1, -1, -1, -1, -1,
								  -1, -1, -1, -1, -1,
								  -1, -1, -1, -1, -1,
								  -1, -1, -1, -1, -1,
								  -1, -1, -1, -1, -1
								};
			player_list = new int[30];
			team_list = new int[30];
		}

		private void LangInitialize()
		{
			LangClassNameGetRecords();
			LangThrowInitialize();
			LangBatBoxInitialize();
			LangPositionInitialize();

			//StartingHeaderTextBlockInitialize();
			//ReserveHeaderTextBlockInitialize();
			//LangButtonInitialize();

		}

		private void TextBoesInitialize()
		{
			if (!ThrowsCheck())
			{
				ThrowsTextInitialize();
			}

			if (!BatBoxesCheck())
			{
				BatBoxTextInitialize();
			}

			if (!PositionsCheck())
			{
				PositionTextInitialize();
			}

		}

		#region Observer Clear
		private void MembersClear()
		{
			Members.Clear();
		}

		private void ReserveMembersClear()
		{
			ReserveMembers.Clear();
		}

		private void PositionsClear()
		{
			Positions.Clear();
		}
		private void BatBoxesClear()
		{
			BatBoxes.Clear();
		}
		private void ThrowsClear()
		{
			Throws.Clear();
		}

		private void ObserversClear()
		{
			ThrowsClear();
			PositionsClear();
			BatBoxesClear();
			MembersClear();
			ReserveMembersClear();
		}
		#endregion

		private void ThrowsTextInitialize()
		{
			Throws.Add(new Throw(0, thr_r));
			Throws.Add(new Throw(1, thr_l));
		}

		private void BatBoxTextInitialize()
		{
			BatBoxes.Add(new BatBox(0, batbox_r));
			BatBoxes.Add(new BatBox(1, batbox_l));
			BatBoxes.Add(new BatBox(2, batbox_sw));
		}

		private bool MembersDataSetListByCount(int count = 0)
		{
			int position_cd = PositionListCountOrverCheck(count);
			string odrCdx = order_list[count];
			int tmp_position_cd = position_cd;
			if (count >= 10)
			{
				odrCdx = "R";
				tmp_position_cd = 0;
			}
			try
			{
				Members.Add(
					new Member(
						player_id: player_list[count],
						team_id: team_list[count],
						order_id: odrCdx,
						name: name_list[count],
						position: tmp_position_cd,
						hand_id: hand_list[count],
						bat_id: bat_list[count],
						number: no_list[count]
						)
					);
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool ReserveMembersDataSetList(int count = 0)
		{
			try
			{
				ReserveMembers.Add(
					new ReserveMember(
						player_id: player_list[count],
						team_id: team_list[count],
						order_id: "RR",
						name: name_list[count],
						position: 0,
						hand_id: hand_list[count],
						bat_id: bat_list[count],
						number: no_list[count]
						)
					);
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}
			return true;
		}

		private int PositionListCountOrverCheck(int count)
		{
			if (position_list.Count() <= count)
			{
				return 0;
			}
			return position_list[count];
		}

		private bool MembersListCreate()
		{
			MembersClear();

			for (int count = 0; count < name_list.Count(); count++)
			{
				if (!MembersDataSetListByCount(count))
				{
					return false;
				}
			}

			return true;
		}


		private void PositionTextInitialize()
		{
			Positions.Add(new Position(1, pos_p));
			Positions.Add(new Position(2, pos_c));
			Positions.Add(new Position(3, pos_1b));
			Positions.Add(new Position(4, pos_2b));
			Positions.Add(new Position(5, pos_3b));
			Positions.Add(new Position(6, pos_ss));
			Positions.Add(new Position(7, pos_lf));
			Positions.Add(new Position(8, pos_cf));
			Positions.Add(new Position(9, pos_rf));
			Positions.Add(new Position(10, pos_dh));
		}

		private bool ThrowsCheck()
		{
			if (Throws.Count < 1)
			{
				return false;
			}
			return true;
		}

		private bool BatBoxesCheck()
		{
			if (BatBoxes.Count < 1)
			{
				return false;
			}
			return true;
		}

		private bool PositionsCheck()
		{
			if (Positions.Count < 1)
			{
				return false;
			}
			return true;
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

		private void LangPositionInitialize()
		{
			pos_p = disp_position[0].text;
			pos_c = disp_position[1].text;
			pos_1b = disp_position[2].text;
			pos_2b = disp_position[3].text;
			pos_3b = disp_position[4].text;
			pos_ss = disp_position[5].text;
			pos_lf = disp_position[6].text;
			pos_cf = disp_position[7].text;
			pos_rf = disp_position[8].text;
			pos_dh = disp_position[9].text;
		}

		private void LangClassNameGetRecords()
		{
			disp_throw = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "Throw", lang_k: selectedLang);
			disp_batbox = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "BatBox", lang_k: selectedLang);
			disp_position = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "Position", lang_k: selectedLang);
		}

		private void ListToCopy()
		{
			for (int count = 0; count < name_list.Length; count++)
			{
				old_name_list[count] = name_list[count];
				if (count < 10)
				{
					old_position_list[count] = position_list[count];
				}
				old_hand_list[count] = hand_list[count];
				old_bat_list[count] = bat_list[count];
				old_no_list[count] = no_list[count];
				old_player_list[count] = player_list[count];
				old_team_list[count] = team_list[count];
				old_order_list[count] = order_list[count];

			}
		}

		private void ListsReset()
		{
			for (int count = 0; count < old_name_list.Length; count++)
			{
				name_list[count] = old_name_list[count];
				if (count < 10)
				{
					position_list[count] = old_position_list[count];
				}
				hand_list[count] = old_hand_list[count];
				bat_list[count] = old_bat_list[count];
				no_list[count] = old_no_list[count];
				player_list[count] = old_player_list[count];
				team_list[count] = old_team_list[count];
				order_list[count] = old_order_list[count];
			}
		}


		private void PutListDataAtPlayer()
		{
			int count = 0;
			foreach (PlayerData.playerData player in gamePlayers)
			{
				name_list[count] = player.name;
				position_list[count] = player.position;
				hand_list[count] = player.hand_id;
				bat_list[count] = player.bat_id;
				no_list[count] = player.player_num;
				player_list[count] = player.player_id;
				team_list[count] = player.team_id;
				order_list[count] = player.etc_str2;
				count++;
			}

			if (!IntoStartingWebjets())
			{
				return;
			}

			foreach (PlayerData.playerData reserve in reservePlayers)
			{
				name_list[count] = reserve.name;
				hand_list[count] = reserve.hand_id;
				bat_list[count] = reserve.bat_id;
				no_list[count] = reserve.player_num;
				player_list[count] = reserve.player_id;
				team_list[count] = reserve.team_id;
				order_list[count] = reserve.etc_str2;
				count++;
			}
			if (!IntoReserveWebjets())
			{
				return;
			}
		}

		#region IntoWedgets

		private bool IntoStartingWebjets()
		{
			if (!IntoTextBoxStartingName())
			{
				return false;
			}

			if (!IntoTextBoxStartingPosition())
			{
				return false;
			}

			if (!IntoTextBoxStartingHand())
			{
				return false;
			}

			if (!IntoTextBoxStartingBat())
			{
				return false;
			}

			if (!IntoTextBoxStartingNumber())
			{
				return false;
			}

			return true;
		}

		private bool IntoReserveWebjets()
		{
			if (!IntoTextBoxReserveName())
			{
				return false;
			}

			if (!IntoTextBoxReserveHand())
			{
				return false;
			}

			if (!IntoTextBoxReserveBat())
			{
				return false;
			}

			if (!IntoTextBoxReserveNumber())
			{
				return false;
			}

			return true;
		}


		private bool IntoTextBoxStartingName()
		{
			int count = 0;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingNameTextBlocks())
				{
					text.Text = name_list[count] ?? "";
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxReserveName()
		{
			int count = 10;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveNameTextBlocks())
				{
					if (name_list[count] is null)
					{
						stop_count = count;
					}
					if (stop_count == count)
					{
						break;
					}
					string tmp_name = name_list[count] ?? "";

					text.Text = tmp_name;
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}


		private bool IntoTextBoxStartingHand()
		{
			int count = 0;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingHandTextBlocks())
				{
					text.Text = Throws.Where(p => p.id == hand_list[count]).First().name;
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxReserveHand()
		{
			int count = 10;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveHandTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = Throws.Where(p => p.id == hand_list[count]).First().name;
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxStartingBat()
		{
			int count = 0;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingBatTextBlocks())
				{
					text.Text = BatBoxes.Where(p => p.id == bat_list[count]).First().name;
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxReserveBat()
		{
			int count = 10;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveBatTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = BatBoxes.Where(p => p.id == bat_list[count]).First().name;
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxStartingPosition()
		{
			int count = 0;
			try
			{
				foreach (TextBlock text in AsEnumerablePositionTextBlocks())
				{
					if (position_list[count] != 0)
					{
						text.Text = Positions.Where(p => p.id == position_list[count]).First().name;
					}
					else
					{
						text.Text = Positions.Where(p => p.id == 10 - count).First().name;
					}
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxStartingNumber()
		{
			int count = 0;
			int _;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingNumberTextBlocks())
				{

					if (!int.TryParse(no_list[count].ToString(), out _))
					{
						return false;
					}
					if (no_list[count] > -1)
					{
						text.Text = no_list[count].ToString();
					}
					else
					{
						text.Text = "";
					}
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoTextBoxReserveNumber()
		{
			int count = 10;
			int _;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveNumberTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (!int.TryParse(no_list[count].ToString(), out _))
					{
						return false;
					}
					if (no_list[count] > -1)
					{
						text.Text = no_list[count].ToString();
					}
					else
					{
						text.Text = "";
					}
					text.FontSize = setFontSize_text;
					count++;
				}
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}


		private bool WedgetsInitialize()
		{
			if (!NameWedgetInitialize())
			{
				return false;
			}
			if (!NumberWedgetInitialize())
			{
				return false;
			}
			if (!PositionWedgetInitialize())
			{
				return false;
			}
			if (!HandWedgetInitialize())
			{
				return false;
			}
			if (!BatWedgetInitialize())
			{
				return false;
			}

			return true;

		}

		private bool NameWedgetInitialize()
		{
			try
			{
				foreach (var text in AsEnumerableNameTextBlocks())
				{
					text.Text = "";
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool NumberWedgetInitialize()
		{
			try
			{
				foreach (var text in AsEnumerableNumberTextBlocks())
				{
					text.Text = "";
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool PositionWedgetInitialize()
		{
			try
			{
				foreach (var text in AsEnumerablePositionTextBlocks())
				{
					text.Text = "";
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool HandWedgetInitialize()
		{
			try
			{
				foreach (var text in AsEnumerableHandTextBlocks())
				{
					text.Text = "";
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool BatWedgetInitialize()
		{
			try
			{
				foreach (var text in AsEnumerableBatTextBlocks())
				{
					text.Text = "";
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool PlayerChangeResetRtn()
		{

			if (WedgetsReset())
			{
				ListsReset();
				return true;
			}
			return false;
		}


		private bool WedgetsReset()
		{
			if (!NameWedgetReset())
			{
				return false;
			}
			if (!NumberWedgetReset())
			{
				return false;
			}
			if (!HandWedgetReset())
			{
				return false;
			}
			if (!BatWedgetReset())
			{
				return false;
			}
			if (!PositionWedgetReset())
			{
				return false;
			}
			return true;
		}

		private bool NameWedgetReset()
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableNameTextBlocks())
				{
					//text.Text = "";
					if (old_name_list[count] is null)
					{
						stop_count = count;
					}
					if (stop_count == count)
					{
						break;
					}
					text.Text = old_name_list[count] ?? "";
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool NumberWedgetReset()
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableNumberTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (!int.TryParse(old_no_list[count].ToString(), out _))
					{
						return false;
					}
					if (old_no_list[count] > -1)
					{
						text.Text = old_no_list[count].ToString();
					}
					else
					{
						text.Text = "";
					}
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool PositionWedgetReset()
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerablePositionTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (position_list[count] != 0)
					{
						text.Text = Positions.Where(p => p.id == old_position_list[count]).First().name;
					}
					else
					{
						text.Text = Positions.Where(p => p.id == 10 - count).First().name;
					}
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool HandWedgetReset()
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableHandTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = Throws.Where(p => p.id == old_hand_list[count]).First().name;
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool BatWedgetReset()
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableBatTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = BatBoxes.Where(p => p.id == old_bat_list[count]).First().name;
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}


		private bool IntoWedgets()
		{
			if (!IntoNameWedget(name_list))
			{
				return false;
			}
			if (!IntoNumberWedget(no_list))
			{
				return false;
			}
			if (!IntoHandWedget(hand_list))
			{
				return false;
			}
			if (!IntoBatWedget(bat_list))
			{
				return false;
			}
			if (!IntoPositionWedget(position_list))
			{
				return false;
			}
			return true;
		}

		private bool IntoNameWedget(string[] name_list)
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableNameTextBlocks())
				{
					//text.Text = "";
					if (name_list[count] is null)
					{
						stop_count = count;
					}
					if (stop_count == count)
					{
						break;
					}
					text.Text = name_list[count] ?? "";
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool IntoNumberWedget(int[] no_list)
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableNumberTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (!int.TryParse(no_list[count].ToString(), out _))
					{
						return false;
					}
					if (no_list[count] > -1)
					{
						text.Text = no_list[count].ToString();
					}
					else
					{
						text.Text = "";
					}
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool IntoPositionWedget(int[] position_list)
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerablePositionTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (position_list[count] != 0)
					{
						text.Text = Positions.Where(p => p.id == position_list[count]).First().name;
					}
					else
					{
						text.Text = Positions.Where(p => p.id == 10 - count).First().name;
					}
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool IntoHandWedget(int[] hand_list)
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableHandTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = Throws.Where(p => p.id == hand_list[count]).First().name;
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool IntoBatWedget(int[] bat_list)
		{
			int count = 0;
			try
			{
				foreach (var text in AsEnumerableBatTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					text.Text = BatBoxes.Where(p => p.id == bat_list[count]).First().name;
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool PlayersToLists()
		{
			if (!GetToListDataAtPlayers())
			{
				return false;
			}
			if (!IntoWedgets())
			{
				return false;
			}
			return true;
		}
		private bool GetToListDataAtPlayers()
		{
			int count = 0;
			try
			{
				foreach (PlayerData.playerData player in players)
				{
					if (player.etc_str2 == "RR") { continue; }
					team_list[count] = player.team_id;
					player_list[count] = player.player_id;
					name_list[count] = player.name;
					hand_list[count] = player.hand_id;
					bat_list[count] = player.bat_id;
					no_list[count] = player.player_num;
					if (count < 10)
					{
						position_list[count] = player.position;
					}
					order_list[count] = player.etc_str2;
					count++;
				}
			}
			catch
			{
				return false;
			}
			return true;
		}


		#endregion

		#region IntoLists

		private bool IntoStartingLists()
		{
			if (!IntoListStartingName())
			{
				return false;
			}

			if (!IntoListStartingPosition())
			{
				return false;
			}

			if (!IntoListStartingHand())
			{
				return false;
			}

			if (!IntoListStartingBat())
			{
				return false;
			}

			if (!IntoListStartingNumber())
			{
				return false;
			}

			return true;
		}

		private bool IntoReserveLists()
		{
			if (!IntoListReserveName())
			{
				return false;
			}

			if (!IntoListReserveHand())
			{
				return false;
			}

			if (!IntoListReserveBat())
			{
				return false;
			}

			if (!IntoListReserveNumber())
			{
				return false;
			}

			return true;
		}

		private bool IntoListStartingName()
		{
			int count = 0;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingNameTextBlocks())
				{
					name_list[count] = text.Text ?? "";
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListReserveName()
		{
			int count = 10;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveNameTextBlocks())
				{
					if (text.Text is null)
					{
						stop_count = count;
					}

					if (text.Text == "")
					{
						stop_count = count;
					}

					if (stop_count == count)
					{
						break;
					}
					string tmp_name = text.Text ?? "";
					name_list[count] = tmp_name;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}


		private bool IntoListStartingHand()
		{
			int count = 0;
			try
			{
				foreach (TextBlock comboBox in AsEnumerableStartingHandTextBlocks())
				{
					hand_list[count] = Throws.Where(p => p.id == hand_list[count]).First().id;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListReserveHand()
		{
			int count = 10;
			try
			{
				foreach (TextBlock comboBox in AsEnumerableReserveHandTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}

					hand_list[count] = Throws.Where(p => p.id == hand_list[count]).First().id;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListStartingBat()
		{
			int count = 0;
			try
			{
				foreach (TextBlock comboBox in AsEnumerableStartingBatTextBlocks())
				{
					hand_list[count] = BatBoxes.Where(p => p.id == bat_list[count]).First().id;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListReserveBat()
		{
			int count = 10;
			try
			{
				foreach (TextBlock comboBox in AsEnumerableReserveBatTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					hand_list[count] = BatBoxes.Where(p => p.id == bat_list[count]).First().id;
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListStartingPosition()
		{
			int count = 0;
			try
			{
				foreach (TextBlock comboBox in AsEnumerablePositionTextBlocks())
				{
					if (position_list[count] != 0)
					{
						position_list[count] = Positions.Where(p => p.id == position_list[count]).First().id;
					}
					else
					{
						position_list[count] = 10 - count;
					}
					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListStartingNumber()
		{
			int count = 0;
			int _;
			try
			{
				foreach (TextBlock text in AsEnumerableStartingNumberTextBlocks())
				{

					if (!int.TryParse(text.Text.ToString(), out _))
					{
						//return false;
						_ = 0;
					}

					no_list[count] = _;

					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}

		private bool IntoListReserveNumber()
		{
			int count = 10;
			int _;
			try
			{
				foreach (TextBlock text in AsEnumerableReserveNumberTextBlocks())
				{
					if (stop_count == count)
					{
						break;
					}
					if (!int.TryParse(text.Text.ToString(), out _))
					{
						//return false;
						_ = 0;
					}

					no_list[count] = _;

					count++;
				}
			}
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
				return false;
			}
			return true;
		}
		#endregion


		#region AsEnumerables
		private IEnumerable<TextBlock> AsEnumerableNameTextBlocks()
		{
			yield return name_1;
			yield return name_2;
			yield return name_3;
			yield return name_4;
			yield return name_5;
			yield return name_6;
			yield return name_7;
			yield return name_8;
			yield return name_9;
			yield return name_10;
			yield return name_11;
			yield return name_12;
			yield return name_13;
			yield return name_14;
			yield return name_15;
			yield return name_16;
			yield return name_17;
			yield return name_18;
			yield return name_19;
			yield return name_20;
			yield return name_21;
			yield return name_22;
			yield return name_23;
			yield return name_24;
			yield return name_25;
			yield return name_26;
			yield return name_27;
			yield return name_28;
			yield return name_29;
			yield return name_30;
		}

		private IEnumerable<TextBlock> AsEnumerableStartingNameTextBlocks()
		{
			yield return name_1;
			yield return name_2;
			yield return name_3;
			yield return name_4;
			yield return name_5;
			yield return name_6;
			yield return name_7;
			yield return name_8;
			yield return name_9;
			yield return name_10;
		}

		private IEnumerable<TextBlock> AsEnumerableReserveNameTextBlocks()
		{
			yield return name_11;
			yield return name_12;
			yield return name_13;
			yield return name_14;
			yield return name_15;
			yield return name_16;
			yield return name_17;
			yield return name_18;
			yield return name_19;
			yield return name_20;
			yield return name_21;
			yield return name_22;
			yield return name_23;
			yield return name_24;
			yield return name_25;
			yield return name_26;
			yield return name_27;
			yield return name_28;
			yield return name_29;
			yield return name_30;
		}


		private IEnumerable<TextBlock> AsEnumerablePositionTextBlocks()
		{
			yield return position_1;
			yield return position_2;
			yield return position_3;
			yield return position_4;
			yield return position_5;
			yield return position_6;
			yield return position_7;
			yield return position_8;
			yield return position_9;
			yield return position_10;
		}

		private IEnumerable<TextBlock> AsEnumerableHandTextBlocks()
		{
			yield return hand_1;
			yield return hand_2;
			yield return hand_3;
			yield return hand_4;
			yield return hand_5;
			yield return hand_6;
			yield return hand_7;
			yield return hand_8;
			yield return hand_9;
			yield return hand_10;
			yield return hand_11;
			yield return hand_12;
			yield return hand_13;
			yield return hand_14;
			yield return hand_15;
			yield return hand_16;
			yield return hand_17;
			yield return hand_18;
			yield return hand_19;
			yield return hand_20;
			yield return hand_21;
			yield return hand_22;
			yield return hand_23;
			yield return hand_24;
			yield return hand_25;
			yield return hand_26;
			yield return hand_27;
			yield return hand_28;
			yield return hand_29;
			yield return hand_30;
		}

		private IEnumerable<TextBlock> AsEnumerableStartingHandTextBlocks()
		{
			yield return hand_1;
			yield return hand_2;
			yield return hand_3;
			yield return hand_4;
			yield return hand_5;
			yield return hand_6;
			yield return hand_7;
			yield return hand_8;
			yield return hand_9;
			yield return hand_10;
		}

		private IEnumerable<TextBlock> AsEnumerableReserveHandTextBlocks()
		{
			yield return hand_11;
			yield return hand_12;
			yield return hand_13;
			yield return hand_14;
			yield return hand_15;
			yield return hand_16;
			yield return hand_17;
			yield return hand_18;
			yield return hand_19;
			yield return hand_20;
			yield return hand_21;
			yield return hand_22;
			yield return hand_23;
			yield return hand_24;
			yield return hand_25;
			yield return hand_26;
			yield return hand_27;
			yield return hand_28;
			yield return hand_29;
			yield return hand_30;
		}

		private IEnumerable<TextBlock> AsEnumerableBatTextBlocks()
		{
			yield return bat_1;
			yield return bat_2;
			yield return bat_3;
			yield return bat_4;
			yield return bat_5;
			yield return bat_6;
			yield return bat_7;
			yield return bat_8;
			yield return bat_9;
			yield return bat_10;
			yield return bat_11;
			yield return bat_12;
			yield return bat_13;
			yield return bat_14;
			yield return bat_15;
			yield return bat_16;
			yield return bat_17;
			yield return bat_18;
			yield return bat_19;
			yield return bat_20;
			yield return bat_21;
			yield return bat_22;
			yield return bat_23;
			yield return bat_24;
			yield return bat_25;
			yield return bat_26;
			yield return bat_27;
			yield return bat_28;
			yield return bat_29;
			yield return bat_30;
		}

		private IEnumerable<TextBlock> AsEnumerableStartingBatTextBlocks()
		{
			yield return bat_1;
			yield return bat_2;
			yield return bat_3;
			yield return bat_4;
			yield return bat_5;
			yield return bat_6;
			yield return bat_7;
			yield return bat_8;
			yield return bat_9;
			yield return bat_10;
		}

		private IEnumerable<TextBlock> AsEnumerableReserveBatTextBlocks()
		{
			yield return bat_11;
			yield return bat_12;
			yield return bat_13;
			yield return bat_14;
			yield return bat_15;
			yield return bat_16;
			yield return bat_17;
			yield return bat_18;
			yield return bat_19;
			yield return bat_20;
			yield return bat_21;
			yield return bat_22;
			yield return bat_23;
			yield return bat_24;
			yield return bat_25;
			yield return bat_26;
			yield return bat_27;
			yield return bat_28;
			yield return bat_29;
			yield return bat_30;
		}


		private IEnumerable<TextBlock> AsEnumerableNumberTextBlocks()
		{
			yield return no_1;
			yield return no_2;
			yield return no_3;
			yield return no_4;
			yield return no_5;
			yield return no_6;
			yield return no_7;
			yield return no_8;
			yield return no_9;
			yield return no_10;
			yield return no_11;
			yield return no_12;
			yield return no_13;
			yield return no_14;
			yield return no_15;
			yield return no_16;
			yield return no_17;
			yield return no_18;
			yield return no_19;
			yield return no_20;
			yield return no_21;
			yield return no_22;
			yield return no_23;
			yield return no_24;
			yield return no_25;
			yield return no_26;
			yield return no_27;
			yield return no_28;
			yield return no_29;
			yield return no_30;
		}

		private IEnumerable<TextBlock> AsEnumerableStartingNumberTextBlocks()
		{
			yield return no_1;
			yield return no_2;
			yield return no_3;
			yield return no_4;
			yield return no_5;
			yield return no_6;
			yield return no_7;
			yield return no_8;
			yield return no_9;
			yield return no_10;
		}

		private IEnumerable<TextBlock> AsEnumerableReserveNumberTextBlocks()
		{
			yield return no_11;
			yield return no_12;
			yield return no_13;
			yield return no_14;
			yield return no_15;
			yield return no_16;
			yield return no_17;
			yield return no_18;
			yield return no_19;
			yield return no_20;
			yield return no_21;
			yield return no_22;
			yield return no_23;
			yield return no_24;
			yield return no_25;
			yield return no_26;
			yield return no_27;
			yield return no_28;
			yield return no_29;
			yield return no_30;
		}

		#endregion


		#region Members to Lists
		private void MembersToLists()
		{
			int count = 0;

			foreach (var member in Members)
			{
				bat_list[count] = member.bat_id;
				hand_list[count] = member.hand_id;
				name_list[count] = member.name;
				order_list[count] = member.order_id;
				if (count < 10)
				{
					position_list[count] = member.position_id;
				}
				no_list[count] = member.number;
				player_list[count] = member.player_id;
				team_list[count] = member.team_id;
				count++;
			}
		}
		#endregion


		private TextBlock this[int index]
		{
			get
			{
				switch (index)
				{
					case 1: return name_1;
					case 2: return name_2;
					case 3: return name_3;
					case 4: return name_4;
					case 5: return name_5;
					case 6: return name_6;
					case 7: return name_7;
					case 8: return name_8;
					case 9: return name_9;
					case 10: return name_10;
					case 11: return name_11;
					case 12: return name_12;
					case 13: return name_13;
					case 14: return name_14;
					case 15: return name_15;
					case 16: return name_16;
					case 17: return name_17;
					case 18: return name_18;
					case 19: return name_19;
					case 20: return name_20;
					case 21: return name_21;
					case 22: return name_22;
					case 23: return name_23;
					case 24: return name_24;
					case 25: return name_25;
					case 26: return name_26;
					case 27: return name_27;
					case 28: return name_28;
					case 29: return name_29;
					case 30: return name_30;
					default:
						throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 1: name_1 = value; break;
					case 2: name_2 = value; break;
					case 3: name_3 = value; break;
					case 4: name_4 = value; break;
					case 5: name_5 = value; break;
					case 6: name_6 = value; break;
					case 7: name_7 = value; break;
					case 8: name_8 = value; break;
					case 9: name_9 = value; break;
					case 10: name_10 = value; break;
					case 11: name_11 = value; break;
					case 12: name_12 = value; break;
					case 13: name_13 = value; break;
					case 14: name_14 = value; break;
					case 15: name_15 = value; break;
					case 16: name_16 = value; break;
					case 17: name_17 = value; break;
					case 18: name_18 = value; break;
					case 19: name_19 = value; break;
					case 20: name_20 = value; break;
					case 21: name_21 = value; break;
					case 22: name_22 = value; break;
					case 23: name_23 = value; break;
					case 24: name_24 = value; break;
					case 25: name_25 = value; break;
					case 26: name_26 = value; break;
					case 27: name_27 = value; break;
					case 28: name_28 = value; break;
					case 29: name_29 = value; break;
					case 30: name_30 = value; break;

					default:
						throw new IndexOutOfRangeException();
				}
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
				case "Data":
					Frame.Navigate(typeof(DataAnalysisPage));
					break;
			}
		}


		private void OrderNameText_Drag_Starting(UIElement sender, DragStartingEventArgs args)
		{
			TextBox textBox = (TextBox)sender;
			string name = textBox.Name;
		}


		private async void GamePlayingPlayerChangeMsg()
		{
			string tmp_throwsOrder = "";
			GameData.updateGameFlgRecord(
									game[0].game_id,
									game_start_flg: true,
									player_change_flg: true
									);
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "選手交代";
			dialog.Content = tmp_throwsOrder;
			dialog.PrimaryButtonText = "先攻";
			dialog.SecondaryButtonText = "後攻";
			dialog.CloseButtonText = "戻る";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				TitleNameOrder.Text = "先攻";
				selectTeamId = topTeamId;
				selectTopBtmSt = "先攻チーム";
			}
			else if (result == ContentDialogResult.Secondary)
			{
				TitleNameOrder.Text = "後攻";
				selectTeamId = btmTeamId;
				selectTopBtmSt = "後攻チーム";
			}
			else if (result == ContentDialogResult.None)
			{
				Frame.Navigate(typeof(ScorePage));
				return;
			}

			PlayerData.InitializeDB();

			//players = PlayerData.GetRecords(team_id: selectTeamId, selected: 0);
			players = PlayerData.GetRecordsAllMember(team_id: selectTeamId, selected: 0);

			/// やり直し用
			/// 
			oldGame = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "game"
										, selected: 0
										);
			/// 控え選手リスト
			oldReserve = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "0",
										searchText: searchText,
										seachNum: seachTextNum
										, selected: 0
										);




			/// 出場選手リスト
			gamePlayers = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "game"
										, selected: 0
										);
			/// 控え選手リスト
			reservePlayers = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "0",
										searchText: searchText,
										seachNum: seachTextNum
										, selected: 0
										);

			TeamOrderNameListView.ItemsSource = gamePlayers;
			TeamOrderIdListView.ItemsSource = gamePlayers;
			TeamOrderPositionListView.ItemsSource = gamePlayers;
			TeamOrderBatListView.ItemsSource = gamePlayers;
			TeamOrderHandListView.ItemsSource = gamePlayers;
			TeamReserveNameListView.ItemsSource = reservePlayers;
			//GameData.updateGameFlgRecord(
			//						game[0].game_id,
			//						game_start_flg: true,
			//						player_change_flg: true
			//						);


			/// 2022.05.12 テスト配置
			/// 選手交代時にリスト・ウィジェットを再作成
			PlayerChangeInitialize();
		}


		private void OrderChangeBeforeGame(ListView listView)
		{
			if (TappedFlg)
			{
				ChangeOrderAfterIndex = listView.SelectedIndex;

				PlayerData.playerData tmpGameListAfter = gamePlayers[ChangeOrderAfterIndex];
				ChangeNameAfter = tmpGameListAfter.name;
				ChangeOrderAfterPlayerId = tmpGameListAfter.player_id;
				ChangeOrderAfterOrderId = tmpGameListAfter.etc_str2;
				ChangeOrderAfterBat = tmpGameListAfter.bat;
				ChangeOrderAfterHand = tmpGameListAfter.hand;
				ChangeOrderAfterPosition = tmpGameListAfter.etc_str1;
				/// 試合前時
				/// 控え -> オーダーの順で
				/// 選手選択を実行した場合
				if (ReserveViewFlg)
				{
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderAfterPlayerId,
						etc_str1: ChangeOrderBeforePosition,
						etc_str2: ChangeOrderBeforeOrderId
						);
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderBeforePlayerId,
						etc_str1: ChangeOrderAfterPosition,
						etc_str2: ChangeOrderAfterOrderId
						);

				}
				else
				{
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderAfterPlayerId,
						etc_str1: ChangeOrderAfterPosition,
						etc_str2: ChangeOrderBeforeOrderId
						);
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderBeforePlayerId,
						etc_str1: ChangeOrderBeforePosition,
						etc_str2: ChangeOrderAfterOrderId
						);
				}
				GetOrderListViewerSel(0);
				TappedFlg = false;
			}
			else
			{
				ChangeOrderBeforeIndex = listView.SelectedIndex;
				PlayerData.playerData tmpGameListBefore = gamePlayers[ChangeOrderBeforeIndex];
				ChangeNameBefore = tmpGameListBefore.name;
				ChangeOrderBeforePlayerId = tmpGameListBefore.player_id;
				ChangeOrderBeforeOrderId = tmpGameListBefore.etc_str2;
				ChangeOrderBeforeBat = tmpGameListBefore.bat;
				ChangeOrderBeforeHand = tmpGameListBefore.hand;
				ChangeOrderBeforePosition = tmpGameListBefore.etc_str1;
				TappedFlg = true;
			}
		}

		private void OrderChangeAfterGame(ListView listView)
		{
			if (TappedFlg)
			{
				ChangeOrderAfterIndex = listView.SelectedIndex;

				PlayerData.playerData tmpGameListAfter = gamePlayers[ChangeOrderAfterIndex];
				ChangeNameAfter = tmpGameListAfter.name;
				ChangeOrderAfterPlayerId = tmpGameListAfter.player_id;
				ChangeOrderAfterOrderId = tmpGameListAfter.etc_str2;
				ChangeOrderAfterBat = tmpGameListAfter.bat;
				ChangeOrderAfterHand = tmpGameListAfter.hand;
				ChangeOrderAfterPosition = tmpGameListAfter.etc_str1;


				PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderBeforePlayerId,
						etc_str1: ChangeOrderAfterPosition,
						etc_str2: ChangeOrderAfterOrderId
						);
				PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangeOrderAfterPlayerId,
						etc_str1: "-1",
						etc_str2: "-1"
						);

				GetOrderListViewerSel(3);
				TappedFlg = false;
			}
		}


		private void OrderName_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			//template = PlayOrder.GetRecords(team_id: selectTeamId.ToString());
			//players = PlayerData.GetRecords(team_id: selectTeamId);
			gamePlayers = PlayerData.GetRecords(team_id: selectTeamId);
			List<PlayOrder.playOrder> Order_list = PlayOrder.GetRecords(team_id: selectTeamId.ToString());
			if (GameStartFlg)
			{
				OrderChangeAfterGame(listView: listView);
			}
			else
			{
				OrderChangeBeforeGame(listView: listView);
			}
			ReserveViewFlg = false;
		}


		private void ReserveOrderBeforeSel(ListView listview)
		{
			ReserveViewFlg = true;

			if (!TappedFlg)
			{
				ChangeOrderBeforeIndex = listview.SelectedIndex;

				//PlayerData.playerData tmpGameListBefore = reserve_list[ChangeOrderBeforeIndex];
				PlayerData.playerData tmpGameListBefore = reservePlayers[ChangeOrderBeforeIndex];
				ChangeNameBefore = tmpGameListBefore.name;
				ChangeOrderBeforeOrderId = tmpGameListBefore.etc_str2;
				ChangeOrderBeforePlayerId = tmpGameListBefore.player_id;
				ChangeOrderBeforeBat = tmpGameListBefore.bat;
				ChangeOrderBeforeHand = tmpGameListBefore.hand;
				ChangeOrderBeforePosition = tmpGameListBefore.etc_str1;
				TappedFlg = true;
			}
		}

		private void ReservNameList_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{

			ListView listView = (ListView)sender;

			if (!TappedFlg)
			{
				ChangeOrderBeforeIndex = listView.SelectedIndex;
				//PlayerData.playerData tmpGameListBefore = reserve_list[ChangeOrderBeforeIndex];
				PlayerData.playerData tmpGameListBefore = reservePlayers[ChangeOrderBeforeIndex];

				ChangeNameBefore = tmpGameListBefore.name;
				ChangeOrderBeforeOrderId = tmpGameListBefore.etc_str2;
				ChangeOrderBeforePlayerId = tmpGameListBefore.player_id;
				ChangeOrderBeforeBat = tmpGameListBefore.bat;
				ChangeOrderBeforeHand = tmpGameListBefore.hand;
				ChangeOrderBeforePosition = tmpGameListBefore.etc_str1;
				ReserveViewFlg = true;
				TappedFlg = true;
			}
		}

		private void OrderPosition_Item_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;

			List<PlayerData.playerData> OrdersPosition_list
				= PlayerData.GetRecords(team_id: selectTeamId);
			//players = PlayerData.GetRecords(team_id: selectTeamId);
			if (!PositionFlg)
			{
				positionBeforeIndex = listView.SelectedIndex;
				PlayerData.playerData tmpGameListBefore = OrdersPosition_list[positionBeforeIndex];
				ChangePositionNameBefore = tmpGameListBefore.name;
				ChangePositionBeforePlayerId = tmpGameListBefore.player_id;
				ChangePositionBefore = tmpGameListBefore.etc_str1;
				ChangePositionBeforeOrderId = tmpGameListBefore.etc_str2;

				PositionFlg = true;
			}
			else
			{
				positionAfterIndex = listView.SelectedIndex;

				PlayerData.playerData tmpGameListAfter = OrdersPosition_list[positionAfterIndex];
				ChangePositionNameAfter = tmpGameListAfter.name;
				ChangePositionAfterPlayerId = tmpGameListAfter.player_id;
				ChangePositionAfter = tmpGameListAfter.etc_str1;
				ChangePositionAfterOrderId = tmpGameListAfter.etc_str2;


				if (ChangePositionBefore.Contains("投")
					|| ChangePositionAfter.Contains("投"))
				{

				}
				else
				{
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangePositionAfterPlayerId,
						etc_str1: ChangePositionBefore
						//etc_str2: ChangePositionAfterOrderId
						);
					PlayerData.updateRecord(
						team_id: selectTeamId,
						player_id: ChangePositionBeforePlayerId,
						etc_str1: ChangePositionAfter
						//etc_str2: ChangePositionBeforeOrderId
						);
				}

				//TeamOrderPositionListView.ItemsSource = PlayOrder.GetRecords(team_id: selectTeamId.ToString());
				TeamOrderPositionListView.ItemsSource = PlayerData.GetRecords(team_id: selectTeamId);


				ChangePositionNameBefore = "";
				ChangePositionBeforePlayerId = 0;
				ChangePositionBefore = "";
				ChangePositionBeforeOrderId = "";

				ChangePositionNameAfter = "";
				ChangePositionAfterPlayerId = 0;
				ChangePositionAfter = "";
				ChangePositionAfterOrderId = "";

				PositionFlg = false;
			}
		}

		private async void OrderEntryButton_Clicked(object sender, RoutedEventArgs e)
		{
			string tmp_throwsOrder = "";
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "オーダー変更を登録しますか";
			dialog.Content = tmp_throwsOrder;
			dialog.PrimaryButtonText = "登録";
			dialog.SecondaryButtonText = "やり直す";
			dialog.CloseButtonText = "続ける";
			dialog.DefaultButton = ContentDialogButton.Primary;
			var result = await dialog.ShowAsync();
			if (result == ContentDialogResult.Primary)
			{
				List<GameData.gameData> tmpGame = GameData.GetRecords(game_id);

				/// ver.3.0.0.0
				PlayerOrderEntry();

				if (tmpGame[0].player_change_flg)
				{
					/// ver.3.0.0.0
					UpdateBattingResultDataGameOn();

					Frame.Navigate(typeof(ScorePage));
				}
				else
				{
					if (!topFirstStartFlg && !btmFirstStartFlg)
					{
						GameData.updateStartingOrderFlgRecord(
											game_id: game_id,
											top_order_start_flg: true);
						Frame.Navigate(typeof(OrderListPage));
					}
					else
					{
						GameData.updateStartingOrderFlgRecord(
											game_id: game_id,
											top_order_start_flg: true,
											btm_order_start_flg: true,
											game_start_flg: true);

						/// ver.3.0.0.0
						UpdateBattingResultData();

						Frame.Navigate(typeof(ScorePage));
					}
				}

			}
			else if (result == ContentDialogResult.Secondary)
			{
				foreach (PlayerData.playerData _list in oldGame)
				{

					PlayerData.updateRecord(
									etc_str2: _list.etc_str2,
									player_name: _list.name,
									etc_str1: _list.etc_str1,
									bat: _list.bat,
									hand: _list.hand,
									player_id: _list.player_id,
									team_id: _list.team_id);
				}

				foreach (PlayerData.playerData _list in oldReserve)
				{
					PlayerData.updateRecord(
									etc_str2: _list.etc_str2,
									player_name: _list.name,
									etc_str1: _list.etc_str1,
									bat: _list.bat,
									hand: _list.hand,
									player_id: _list.player_id,
									team_id: _list.team_id
									);
				}
				GetOrderListViewerSel(0);

				/// ver.3.0.0.0
				if (!PlayerChangeResetRtn())
				{ }
			}
			else if (result == ContentDialogResult.None)
			{

			}

		}

		private void UpdateBattingResultData()
		{
			int top_team_id = GetTopTeamId(game_id);
			List<PlayerData.playerData> topPlayers = GetThisGamePlayerData(top_team_id);
			UpdateTeamBattingResult(topPlayers, TOP_CODE);

			int btm_team_id = GetBtmTeamId(game_id);
			List<PlayerData.playerData> btmPlayers = GetThisGamePlayerData(btm_team_id);
			UpdateTeamBattingResult(btmPlayers, BTM_CODE);
		}

		private void UpdateBattingResultDataGameOn()
		{
			int top_team_id = GetTopTeamId(game_id);
			List<PlayerData.playerData> topPlayers = GetThisGamePlayerData(top_team_id);
			CheckPlayerDataPosition(topPlayers, oldTopPlayerData, TOP_CODE);

			int btm_team_id = GetBtmTeamId(game_id);
			List<PlayerData.playerData> btmPlayers = GetThisGamePlayerData(btm_team_id);
			CheckPlayerDataPosition(btmPlayers, oldBtmPlayerData, BTM_CODE);
		}

		private void GetPlayersDataInitialize()
		{
			int top_team_id = GetTopTeamId(game_id);
			oldTopPlayerData = GetThisGamePlayerData(top_team_id);

			int btm_team_id = GetBtmTeamId(game_id);
			oldBtmPlayerData = GetThisGamePlayerData(btm_team_id);
		}


		private void CheckPlayerDataPosition(
							List<PlayerData.playerData> playersData,
							List<PlayerData.playerData> oldPlayersData,
							int top_btm_cd)
		{
			for (int count = 0; count < playersData.Count; count++)
			{
				PlayerData.playerData playerDataItem = playersData[count];
				PlayerData.playerData oldPlayerDataItem = oldPlayersData[count];
				
				if (playerDataItem.position != oldPlayerDataItem.position)
				{
					int target_position = playerDataItem.position;
					int target_player_id = oldPlayerDataItem.player_id;
					int target_order_id = 0;
					if (int.TryParse(oldPlayerDataItem.etc_str2, out target_order_id))
					{
						BattingResult battingResult
							= new BattingResult(
									game_id,
									target_player_id,
									target_order_id,
									top_btm_cd,
									target_position
									);
						battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
						battingResult.SetAdditionPositionName(position: target_position);
						battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
						continue;
					}
				}

				if (playerDataItem.player_id != oldPlayerDataItem.player_id)
				{
					int target_position = oldPlayerDataItem.position;
					int target_player_id = playerDataItem.player_id;
					int target_order_id = 0;
					if (int.TryParse(playerDataItem.etc_str2, out target_order_id))
					{
						BattingResult battingResult
							= new BattingResult(
									game_id,
									target_player_id,
									target_order_id,
									top_btm_cd,
									target_position
									);
						battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
						//battingResult.SetPlayerId()
						battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
						continue;
					}
				}


			}

		}

		private int GetTopTeamId(int game_id)
		{
			return GameData.GetRecords(game_id)[0].bat_first_team_id;
		}
		private int GetBtmTeamId(int game_id)
		{
			return GameData.GetRecords(game_id)[0].field_first_team_id;
		}

		private List<PlayerData.playerData> GetThisGamePlayerData(int team_id)
		{
			return PlayerData.GetRecords(team_id: team_id, selected: 0);
		}

		private bool UpdateTeamBattingResult(List<PlayerData.playerData> playerDatas, int top_btm_cd)
		{
			bool result = false;
			int count = playerDatas.Count;
			foreach (PlayerData.playerData playerDataItem in playerDatas)
			{
				int playerOrderId;
				if (int.TryParse(playerDataItem.etc_str2, out playerOrderId))
				{
					count -= 1;
					BattingResult battingResult
						= new BattingResult(
									game_id: game_id,
									player_id: playerDataItem.player_id,
									order_id: playerOrderId,
									top_btm_cd: top_btm_cd,
									position: playerDataItem.position);
					battingResult.画面遷移時既存データが存在した場合はデータをメソッドへ代入();
					battingResult.BattingDataの登録atデータ有無による新規OR追記処理();
					if (count == 0) { result = true; }
				}
			}
			return result;
		}

		/// <summary>
		/// 0:すべて再表示(打順はのぞく)/ 1:出場中/ 2:控え
		/// /3:守備以外すべて/ 4 守備のみ/ 10 すべて
		/// </summary>
		/// <param name="sel"></param>
		private void GetOrderListViewerSel(int sel)
		{
			gamePlayers = PlayerData.GetRecords(
										team_id: selectTeamId,
										etc_str2: "game"
										);

			reservePlayers = PlayerData.GetRecords(
					team_id: selectTeamId,
					etc_str2: "0",
					searchText: searchText,
					seachNum: seachTextNum
					);
			switch (sel)
			{
				case 0:  // すべて再表示(打順はのぞく)
					TeamOrderNameListView.ItemsSource = gamePlayers;
					TeamOrderPositionListView.ItemsSource = gamePlayers;
					TeamOrderHandListView.ItemsSource = gamePlayers;
					TeamOrderBatListView.ItemsSource = gamePlayers;
					TeamReserveNameListView.ItemsSource = reservePlayers;
					break;
				case 1:     // 出場選手のみ
					TeamOrderNameListView.ItemsSource = gamePlayers;
					TeamOrderIdListView.ItemsSource = gamePlayers;
					TeamOrderPositionListView.ItemsSource = gamePlayers;
					TeamOrderHandListView.ItemsSource = gamePlayers;
					TeamOrderBatListView.ItemsSource = gamePlayers;
					break;
				case 2:     // 控え選手のみ
					TeamReserveNameListView.ItemsSource = reservePlayers;
					break;
				case 3:     // すべて更新(守備をのぞく)
					TeamOrderNameListView.ItemsSource = gamePlayers;
					TeamOrderIdListView.ItemsSource = gamePlayers;
					TeamOrderHandListView.ItemsSource = gamePlayers;
					TeamOrderBatListView.ItemsSource = gamePlayers;
					TeamReserveNameListView.ItemsSource = reservePlayers;
					break;
				case 4:     // 守備のみ更新
					TeamOrderPositionListView.ItemsSource = gamePlayers;
					break;
				case 10:    // すべて再表示
					TeamOrderNameListView.ItemsSource = gamePlayers;
					TeamOrderIdListView.ItemsSource = gamePlayers;
					TeamOrderPositionListView.ItemsSource = gamePlayers;
					TeamOrderHandListView.ItemsSource = gamePlayers;
					TeamOrderBatListView.ItemsSource = gamePlayers;
					TeamReserveNameListView.ItemsSource = reservePlayers;
					break;

			}

		}

		private void NewPlayerCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewCreatePlayerName.Text.Length != 0)
			{
				NewCreatePlayer_btn.IsEnabled = true;
			}
			else
			{
				NewCreatePlayer_btn.IsEnabled = false;
			}
		}

		private void NeWCreatePlayer_Button_Clicked(object sender, RoutedEventArgs e)
		{
			string NewCreatePlayerNameST = NewCreatePlayerName.Text;
			string NewCreatePlayerNumberST = NewCreatePlayerNumber.Text;
			string NewCreatePlayerHandST = NewCreatePlayerHandString;
			string NewCreatePlayerBatST = NewCreatePlayerBatString;
			int player_id = PlayerData.GetPlayerIdRecordsCount()[0].player_id;

			PlayerData.addRecord(
				team_id: selectTeamId,
				season: DateTime.Now,
				player_id: player_id,
				player_name: NewCreatePlayerNameST,
				etc_str1: "",
				etc_str2: "0",
				hand: NewCreatePlayerHandST,
				bat: NewCreatePlayerBatST,
				player_num: Convert.ToInt32(NewCreatePlayerNumberST),
				update_date: DateTime.Now
				);
			GetOrderListViewerSel(0);
		}

		private void TestGameStartFlg_Clicked(object sender, RoutedEventArgs e)
		{
			GameStartFlg = !GameStartFlg;
			if (GameStartFlg)
			{
				TestGameStartFlgBtn.Content = "試合中";
			}
			else
			{
				TestGameStartFlgBtn.Content = "試合前";
			}
		}

		private void CsvFileOrderGetData_btn_Clicked(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(DataPage));
			//Frame.Navigate(typeof(OrderPage));
		}

		private void NewCreatePlayerHand_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			NewCreatePlayerHandString = comboBox.SelectedItem.ToString();
		}

		private void NewCreatePlayerBat_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			NewCreatePlayerBatString = comboBox.SelectedItem.ToString();
		}

		private void ReservePlayerTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox text = (TextBox)sender;
			searchText = text.Text;
			GetOrderListViewerSel(2);
		}

		private void ReservePlayerNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox text = (TextBox)sender;
			if (!int.TryParse(text.Text, out seachTextNum))
			{
				ReservePlayerNumTextBox.Text = "";
				seachTextNum = -1;
			}
			GetOrderListViewerSel(2);

		}

		private void TestSw_ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			ToggleSwitch toggleSwitch = sender as ToggleSwitch;
			if (toggleSwitch != null)
			{
				if (toggleSwitch.IsOn == true)
				{
					PlayingMember.Visibility = Visibility.Visible;
					ReserveMember.Visibility = Visibility.Visible;
					StartingTextBlocks.Visibility = Visibility.Collapsed;
					ReserveTextBlocks.Visibility = Visibility.Collapsed;
				}
				else
				{
					PlayingMember.Visibility = Visibility.Collapsed;
					ReserveMember.Visibility = Visibility.Collapsed;
					StartingTextBlocks.Visibility = Visibility.Visible;
					ReserveTextBlocks.Visibility = Visibility.Visible;
				}
			}

		}



		private string TextNumberExtract(string text, string split)
		{
			return text.Split(split)[1];
		}

		private void Starting_DragOver(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
			if (e.DragUIOverride != null)
			{
				e.DragUIOverride.Caption = "Change";
				e.DragUIOverride.IsContentVisible = true;
			}
			e.Handled = true;
		}

		private void Position_DragOver(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
			if (e.DragUIOverride != null)
			{
				e.DragUIOverride.Caption = "Change";
				e.DragUIOverride.IsContentVisible = true;
			}
			e.Handled = true;
		}



		private void NameTextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
		{
			TextBlock text = (TextBlock)sender;
			string text_name = text.Name;
			if (text_name.Contains('_'))
			{
				string odrST = TextNumberExtract(text_name, "_");
				DragItemId = DragAndDropItemId(odrST);
				int _index;
				if (!int.TryParse(odrST, out _index))
				{
					return;
				}
				DragMember = DragAndDropMemberInstance(_index - 1);
			}
		}

		private void name_TextBlock_Drop(object sender, DragEventArgs e)
		{
			TextBlock text = (TextBlock)sender;
			string text_name = text.Name;
			if (text_name.Contains('_'))
			{
				string odrST = TextNumberExtract(text_name, "_");
				DropItemId = DragAndDropItemId(odrST);
				int _index;
				if (!int.TryParse(odrST, out _index))
				{
					return;
				}
				DropMember = DragAndDropMemberInstance(_index - 1);
				DragAndDropMember();
			}
		}

		private void DragAndDropMember()
		{
			DragAndDropMemberOrderExChange();
		}

		private bool DragAndDropMemberOrderExChange()
		{
			if (!DragAndDropMemberOrderExChageIndexSwitchTenOverDrag())
			{
				return false;
			}

			if (!DragAndDropMemberOrderExChageIndexSwitchTenOverDrop())
			{
				return false;
			}


			if (GameStartFlg)
			{
				ChangedPlayerReseveMemberInGame();
				MembersListCreate();
				DuringPlayPlayerChangeRtn();
			}

			if (!AfterDragAndDropRtn())
			{
				return false;
			}


			MembersListCreate();

			return true;
		}


		private bool DragAndDropMemberOrderExChangeReseveCheck()
		{
			if (DragItemId >= 10)
			{
				return true;
			}
			if (DropItemId >= 10)
			{
				return true;
			}
			return false;
		}


		private bool DragAndDropMemberOrderExChangeDrag()
		{
			if (DropItemId >= 0)
			{
				bat_list[DropItemId] = DragMember.bat_id;
				hand_list[DropItemId] = DragMember.hand_id;
				name_list[DropItemId] = DragMember.name;
				player_list[DropItemId] = DragMember.player_id;
				team_list[DropItemId] = DragMember.team_id;
				position_list[DropItemId] = DragMember.position_id;
				no_list[DropItemId] = DragMember.number;
				return true;
			}
			return false;
		}

		private bool DragAndDropMemberOrderExChangeDragReserve()
		{
			try
			{
				bat_list[DropItemId] = DragMember.bat_id;
				hand_list[DropItemId] = DragMember.hand_id;
				name_list[DropItemId] = DragMember.name;
				player_list[DropItemId] = DragMember.player_id;
				team_list[DropItemId] = DragMember.team_id;
				no_list[DropItemId] = DragMember.number;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool DragAndDropMemberOrderExChangeDrop()
		{
			try
			{
				bat_list[DragItemId] = DropMember.bat_id;
				hand_list[DragItemId] = DropMember.hand_id;
				name_list[DragItemId] = DropMember.name;
				player_list[DragItemId] = DropMember.player_id;
				team_list[DragItemId] = DropMember.team_id;
				position_list[DragItemId] = DropMember.position_id;
				no_list[DragItemId] = DropMember.number;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool ChangedPlayerDragAndDrop(Member member)
		{
			try
			{
				ReserveMembers.Add(
					new ReserveMember(
						player_id: member.player_id,
						team_id: member.team_id,
						order_id: "RR",
						name: member.name,
						position: 0,
						hand_id: member.hand_id,
						bat_id: member.bat_id,
						number: member.number
						)
					);
			}
			catch
			{
				return false;
			}
			return true;
		}


		private bool DragAndDropMemberOrderExChangeDropReserve()
		{
			try
			{
				bat_list[DragItemId] = DropMember.bat_id;
				hand_list[DragItemId] = DropMember.hand_id;
				name_list[DragItemId] = DropMember.name;
				player_list[DragItemId] = DropMember.player_id;
				team_list[DragItemId] = DropMember.team_id;
				no_list[DragItemId] = DropMember.number;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool DragAndDropMemberOrderExChageIndexSwitch(int _index)
		{
			if (_index >= 0)
			{
				return true;
			}
			return false;
		}

		private bool DragAndDropMemberOrderExChageIndexSwitchTenOverDrop()
		{
			if (DragAndDropMemberOrderExChangeReseveCheck())
			{
				if (!DragAndDropMemberOrderExChangeDropReserve())
				{
					return false;
				}

				return true;
			}

			/// Don't Change Players in game
			if (!GameStartFlg)
			{
				if (!DragAndDropMemberOrderExChangeDrop())
				{
					return false;
				}
			}
			return true;
		}

		private void ChangedPlayerReseveMemberInGame()
		{
			if (!ChangedPlayerReseveMember())
			{
				return;
			}
			if (!ChangedPlayerReseveMemberInGameCheck())
			{
				return;
			}
			if (DropMember.order_id != "R")
			{
				ChangedPlayerDragAndDrop(DropMember);
			}
			if (DragMember.order_id != "R")
			{
				ChangedPlayerDragAndDrop(DragMember);
			}
		}

		private bool ChangedPlayerReseveMember()
		{
			if (GameStartFlg)
			{
				return true;
			}
			return false;
		}
		private bool ChangedPlayerReseveMemberInGameCheck()
		{
			if (DragAndDropMemberOrderExChangeReseveCheck())
			{
				if (ChangedPlayerReseveMemberInGameDropOrDragCheck())
				{
					return true;
				}
			}
			return false;
		}
		private bool ChangedPlayerReseveMemberInGameDropOrDragCheck()
		{
			if (DropMember.position_id == 0 && DragMember.position_id == 0)
			{ return false; }
			return true;
		}

		private bool DragAndDropMemberOrderExChageIndexSwitchTenOverDrag()
		{
			if (DragAndDropMemberOrderExChangeReseveCheck())
			{
				if (!DragAndDropMemberOrderExChangeDragReserve())
				{
					return false;
				}
				return true;
			}
			/// Don't Change Players in game
			if (!GameStartFlg)
			{
				if (!DragAndDropMemberOrderExChangeDrag())
				{
					return false;
				}
			}
			return true;
		}




		private bool AfterDragAndDropRtn()
		{
			if (!AfterDragAndDropRtnIntWedgets())
			{
				return false;
			}
			return true;
		}

		private bool AfterDragAndDropRtnIntWedgets()
		{
			if (!IntoStartingWebjets())
			{
				return false;
			}

			if (!IntoReserveWebjets())
			{
				return false;
			}
			return true;
		}


		private Member DragAndDropMemberInstance(int tmp_id)
		{
			return Members.Where(p => p.player_id == player_list[tmp_id]).First();
		}

		private ObservableCollection<Member> MembersPlayerChangRemove(Member member)
		{
			var EnumList = Members.Where(p => p.player_id != member.player_id);
			if (EnumList == null)
			{
				return null;
			}
			var observableCollection = new ObservableCollection<Member>();
			foreach (var item in EnumList)
				observableCollection.Add(item);
			return observableCollection;
		}

		private void PlayerChangeRemove(Member inMember)
		{
			int count = 0;
			foreach (var member in Members)
			{
				if (inMember.player_id == member.player_id)
				{
					continue;
				}
				name_list[count] = member.name;
				player_list[count] = member.player_id;
				order_list[count] = member.order_id;
				if (count < 10)
				{
					position_list[count] = member.position_id;
				}
				team_list[count] = member.team_id;
				no_list[count] = member.number;
				hand_list[count] = member.hand_id;
				bat_list[count] = member.bat_id;
				count++;
			}
		}



		private int DragAndDropItemId(string str)
		{
			int list_index;
			if (int.TryParse(str, out list_index))
			{
				list_index = list_index - 1;
				return Array.IndexOf(player_list, player_list[list_index]);
			}
			return -1;
		}

		private void Position_DragStarting(UIElement sender, DragStartingEventArgs args)
		{
			TextBlock text = (TextBlock)sender;
			string text_name = text.Name;
			if (text_name.Contains('_'))
			{
				string odrST = TextNumberExtract(text_name, "_");
				DragItemId = DragAndDropItemId(odrST);
				if (DragItemId < 0)
				{ }
				int _index;
				if (!int.TryParse(odrST, out _index))
				{
					return;
				}
				DragMember = DragAndDropMemberInstance(_index - 1);
			}
		}

		private void Position_Drop(object sender, DragEventArgs e)
		{
			TextBlock text = (TextBlock)sender;
			string text_name = text.Name;
			if (text_name.Contains('_'))
			{
				string odrST = TextNumberExtract(text_name, "_");
				DropItemId = DragAndDropItemId(odrST);
				if (DropItemId < 0)
				{ }
				int _index;
				if (!int.TryParse(odrST, out _index))
				{
					return;
				}
				DropMember = DragAndDropMemberInstance(_index - 1);
				DragAndDropPositionExChangeRtn();
			}
		}

		private bool DragAndDropPositionExChangeRtn()
		{
			if (!DragAndDropPositionExChangeDrag())
			{
				return false;
			}
			if (!DragAndDropPositionExChangeDrop())
			{
				return false;
			}
			if (!AfterDragAndDropRtn())
			{
				return false;
			}
			MembersListCreate();
			return true;

		}

		private bool DragAndDropPositionExChangeDrop()
		{
			try
			{
				position_list[DragItemId] = DropMember.position_id;
				player_list[DragItemId] = DropMember.player_id;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool DragAndDropPositionExChangeDrag()
		{
			try
			{
				position_list[DropItemId] = DragMember.position_id;
				player_list[DropItemId] = DragMember.player_id;
			}
			catch
			{
				return false;
			}
			return true;
		}

		private bool GetPositionIdForPlayerChangeRtn(Member member)
		{
			if (member.position_id == 0)
			{
				return true;
			}
			return false;
		}

		private void DuringPlayPlayerChangeRtn()
		{
			bool DragMember_is_Reserve_Flg = GetPositionIdForPlayerChangeRtn(DragMember);
			bool DropMember_is_Reserve_Flg = GetPositionIdForPlayerChangeRtn(DropMember);
			PlayerChangeFlgCheck(
						drag_flg: DragMember_is_Reserve_Flg,
						drop_flg: DropMember_is_Reserve_Flg
						);
			//MembersToLists();
		}

		private void PlayerChangeFlgCheck(bool drag_flg, bool drop_flg)
		{
			if (drag_flg && drop_flg)
			{
				return;
			}
			if (!drag_flg && !drop_flg)
			{
				return;
			}

			if (drag_flg)
			{
				//Members = MembersPlayerChangRemove(DropMember);
				WedgetsInitialize();
				PlayerChangeRemove(DropMember);
			}

			if (drop_flg)
			{
				//Members = MembersPlayerChangRemove(DragMember);
				WedgetsInitialize();
				PlayerChangeRemove(DragMember);
			}
		}

		private bool SettingResetRtn()
		{

			return true;
		}


		private void PlayerOrderEntry()
		{
			foreach (Member member in Members)
			{
				PlayerData.updateRecord(
								etc_str2: member.order_id,
								player_name: member.name,
								position: member.position_id,
								bat_id: member.bat_id,
								hand_id: member.hand_id,
								player_id: member.player_id,
								team_id: member.team_id,
								player_num: member.number
								);
			}
			ChangedPlayerOrderEntry();
		}

		private bool ChangedPlayerOrderEntryCountCheck()
		{
			if (ReserveMembers.Count != 0)
			{
				return true;
			}
			return false;
		}

		private void ChangedPlayerOrderEntry()
		{
			if (ChangedPlayerOrderEntryCountCheck())
			{
				foreach (ReserveMember member in ReserveMembers)
				{
					PlayerData.updateRecord(
									etc_str2: member.order_id,
									player_name: member.name,
									position: member.position_id,
									bat_id: member.bat_id,
									hand_id: member.hand_id,
									player_id: member.player_id,
									team_id: member.team_id,
									player_num: member.number
									);
				}
			}
		}

		private void GameOrderEntryNavigation_btn_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(GamePlayerOrderEntry));
		}
	}
}