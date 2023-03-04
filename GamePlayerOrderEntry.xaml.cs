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
	public sealed partial class GamePlayerOrderEntry : Page
	{
		public GamePlayerOrderEntry()
		{
			this.InitializeComponent();


			LangInitialize();

			TextBoesInitialize();


			players = PlayerData.GetRecords(team_id: 5);
			/// 出場選手リスト
			gamePlayers = PlayerData.GetRecords(
										team_id: 5,
										etc_str2: "game"
										);
			/// 控え選手リスト
			reservePlayers = PlayerData.GetRecords(
										team_id: 5,
										etc_str2: "0"
										);
			TestTeamIDCreate();
		}


		string[] name_list = new string[30];
		int[] position_list = new int[10];
		int[] hand_list = new int[30];
		int[] bat_list = new int[30];
		int[] no_list = new int[30];
		int[] player_list = new int[30];
		int[] team_list = new int[30];

		ObservableCollection<Throw> Throws = new ObservableCollection<Throw>();
		ObservableCollection<BatBox> BatBoxes = new ObservableCollection<BatBox>();
		ObservableCollection<Position> Positions = new ObservableCollection<Position>();

		private List<PlayerData.playerData> players;
		private List<PlayerData.playerData> gamePlayers;
		private List<PlayerData.playerData> reservePlayers;

		private List<UseDisplay.display> disp_throw;
		private List<UseDisplay.display> disp_batbox;
		private List<UseDisplay.display> disp_position;

		private int stop_count = 30;

		private int selectedLang = UseLanguage.GetRecords(selected: 1)[0].id;


		//private string starting_h_tb;
		//private string starting_odr_h_tb;
		//private string starting_name_h_tb;
		//private string starting_pos_h_tb;
		//private string starting_thr_h_tb;
		//private string starting_bat_h_tb;
		//private string starting_no_h_tb;
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

		private void LangInitialize()
		{
			LangClassNameGetRecords();
			LangThrowInitialize();
			LangBatBoxInitialize();
			LangPositionInitialize();

			StartingHeaderTextBlockInitialize();
			ReserveHeaderTextBlockInitialize();
			LangButtonInitialize();

		}

		private void LangClassNameGetRecords()
		{
			disp_throw = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "Throw", lang_k: selectedLang);
			disp_batbox = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "BatBox", lang_k: selectedLang);
			disp_position = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", clss: "Position", lang_k: selectedLang);
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

		private void StartingHeaderTextBlockInitialize()
		{
			StartingHeaderTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "StartingHeaderTextBlock", lang_k: selectedLang)[0].text;
			OdrHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "OdrHTextBlock", lang_k: selectedLang)[0].text;
			NameHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "NameHTextBlock", lang_k: selectedLang)[0].text;
			PosHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "PosHTextBlock", lang_k: selectedLang)[0].text;
			ThrHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "ThrHTextBlock", lang_k: selectedLang)[0].text;
			BatHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "BatHTextBlock", lang_k: selectedLang)[0].text;
			NoHTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "NoHTextBlock", lang_k: selectedLang)[0].text;
		}

		private void LangButtonInitialize() 
		{
			DataEntryButton.Content = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "DataEntryButton", lang_k: selectedLang)[0].text;
		}

		private void ReserveHeaderTextBlockInitialize() 
		{
			ReserveHeaderTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "ReserveHeaderTextBlock", lang_k: selectedLang)[0].text;
			RrvNameTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "RrvNameTextBlock", lang_k: selectedLang)[0].text;
			RrvThrTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "RrvThrTextBlock", lang_k: selectedLang)[0].text;
			RrvBatTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "RrvBatTextBlock", lang_k: selectedLang)[0].text;
			RrvNoTextBlock.Text = UseDisplay.GetRecords(prg: "GamePlayerOrderEntry", wedget: "RrvNoTextBlock", lang_k: selectedLang)[0].text;

		}

		private void TextBoesInitialize() 
		{
			Throws.Add(new Throw(0, thr_r));
			Throws.Add(new Throw(1, thr_l));

			BatBoxes.Add(new BatBox(0, batbox_r));
			BatBoxes.Add(new BatBox(1, batbox_l));
			BatBoxes.Add(new BatBox(2, batbox_sw));


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


		private void TestTeamIDCreate()
		{
			for (int i = 0; i < 30; i++)
			{
				team_list[i] = 9999;
			}
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
				foreach (TextBox text in AsEnumerableStartingNameTextBoxes())
				{
					text.Text = name_list[count] ?? "";
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
				foreach (TextBox text in AsEnumerableReserveNameTextBoxes())
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
				foreach (ComboBox comboBox in AsEnumerableStartingHandComboBoxes())
				{
					comboBox.SelectedItem = Throws.Where(p => p.id == hand_list[count]).First();
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
				foreach (ComboBox comboBox in AsEnumerableReserveHandComboBoxes())
				{
					if (stop_count == count)
					{
						break;
					}
					comboBox.SelectedItem = Throws.Where(p => p.id == hand_list[count]).First();
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
				foreach (ComboBox comboBox in AsEnumerableStartingBatComboBoxes())
				{
					comboBox.SelectedItem = BatBoxes.Where(p => p.id == bat_list[count]).First();
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
				foreach (ComboBox comboBox in AsEnumerableReserveBatComboBoxes())
				{
					if (stop_count == count)
					{
						break;
					}
					comboBox.SelectedItem = BatBoxes.Where(p => p.id == bat_list[count]).First();
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
				foreach (ComboBox comboBox in AsEnumerablePositionComboBoxes())
				{
					//comboBox.SelectedIndex = bat_list[count];
					if (position_list[count] != 0)
					{
						comboBox.SelectedItem = Positions.Where(p => p.id == position_list[count]).First();
					}
					else
					{
						comboBox.SelectedItem = Positions.Where(p => p.id == 10 - count).First();
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

		private bool IntoTextBoxStartingNumber()
		{
			int count = 0;
			int _;
			try
			{
				foreach (TextBox text in AsEnumerableStartingNumberTextBoxes())
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
				foreach (TextBox text in AsEnumerableReserveNumberTextBoxes())
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
			catch (Exception err)
			{
				System.Console.WriteLine(err.ToString());
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
				foreach (TextBox text in AsEnumerableStartingNameTextBoxes())
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
				foreach (TextBox text in AsEnumerableReserveNameTextBoxes())
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
				foreach (ComboBox comboBox in AsEnumerableStartingHandComboBoxes())
				{
					Throw _thr = (Throw)comboBox.SelectedItem;
					if (_thr is null)
					{
						return false;
					}
					hand_list[count] = (int)_thr.id;
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
				foreach (ComboBox comboBox in AsEnumerableReserveHandComboBoxes())
				{
					if (stop_count == count)
					{
						break;
					}

					Throw _thr = (Throw)comboBox.SelectedItem;

					if (_thr is null)
					{
						break;
					}

					hand_list[count] = (int)_thr.id;
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
				foreach (ComboBox comboBox in AsEnumerableStartingBatComboBoxes())
				{
					BatBox batBox = (BatBox)comboBox.SelectedItem;
					if (batBox is null)
					{
						return false;
					}
					bat_list[count] = (int)batBox.id;
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
				foreach (ComboBox comboBox in AsEnumerableReserveBatComboBoxes())
				{
					if (stop_count == count)
					{
						break;
					}
					BatBox batBox = (BatBox)comboBox.SelectedItem;
					if (batBox is null)
					{
						break;
					}
					bat_list[count] = (int)batBox.id;
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
				foreach (ComboBox comboBox in AsEnumerablePositionComboBoxes())
				{
					if (position_list[count] != 0)
					{
						Position _position = (Position)comboBox.SelectedItem;
						position_list[count] = (int)_position.id;
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
				foreach (TextBox text in AsEnumerableStartingNumberTextBoxes())
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
				foreach (TextBox text in AsEnumerableReserveNumberTextBoxes())
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
		private IEnumerable<TextBox> AsEnumerableNameTextBoxes()
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

		private IEnumerable<TextBox> AsEnumerableStartingNameTextBoxes()
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

		private IEnumerable<TextBox> AsEnumerableReserveNameTextBoxes()
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


		private IEnumerable<ComboBox> AsEnumerablePositionComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableHandComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableStartingHandComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableReserveHandComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableBatComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableStartingBatComboBoxes()
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

		private IEnumerable<ComboBox> AsEnumerableReserveBatComboBoxes()
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


		private IEnumerable<TextBox> AsEnumerableNumberTextBoxes()
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

		private IEnumerable<TextBox> AsEnumerableStartingNumberTextBoxes()
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

		private IEnumerable<TextBox> AsEnumerableReserveNumberTextBoxes()
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

		private void Nomber_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox text = (TextBox)sender;
			var srtText = text.Text;
			int _;
			if (!int.TryParse(srtText, out _))
			{
				text.Text = "";
				return;
			}
		}

		private void TEST_Click(object sender, RoutedEventArgs e)
		{
		}

		private async void MsgWarning(string msg)
		{
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "Warning..";
			dialog.Content = msg;
			dialog.CloseButtonText = "Close";
			dialog.DefaultButton = ContentDialogButton.Close;
			await dialog.ShowAsync();
		}

		private async void MsgEntryResult(string msg = "")
		{
			if (msg.Length == 0)
			{
				msg = "Succeeded in capturing players";
			}
			ContentDialog dialog = new ContentDialog();
			dialog.Title = "Complete ";
			dialog.Content = msg;
			dialog.CloseButtonText = "Close";
			dialog.DefaultButton = ContentDialogButton.Close;
			await dialog.ShowAsync();
		}


		private void Position_cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox text = (ComboBox)sender;
			string name = text.Name.Split('_')[1];
			int _;
			if (int.TryParse(name, out _))
			{
				StartingBackGroundChange(_);
			}

		}

		private void StartingBackGroundChange(int id)
		{
			id = id - 1;

			PositionListDataCheck(id);
			StartingNameBackGroundChange(id);
			StartingPotisionBackGroundChange(id);
			StartingHandBackGroundChange(id);
			StartingBatBackGroundChange(id);
			StartingNoBackGroundChange(id);
		}


		private void StartingNameBackGroundChange(int id)
		{
			int count = 0;
			foreach (TextBox text in AsEnumerableStartingNameTextBoxes())
			{
				if (count == id)
				{
					switch (position_list[count])
					{
						case 1:
							text.Background = new SolidColorBrush(Windows.UI.Colors.LightCoral);
							break;
						case 2:
							text.Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Moccasin);
							break;
						case 7:
						case 8:
						case 9:
							text.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
							break;
						case 10:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
							break;
						default:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
							break;
					}
				}
				count++;
			}
		}

		private void StartingPotisionBackGroundChange(int id)
		{
			int count = 0;
			foreach (ComboBox comboBox in AsEnumerablePositionComboBoxes())
			{
				if (count == id)
				{
					switch (position_list[count])
					{
						case 1:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LightCoral);
							break;
						case 2:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Moccasin);
							break;
						case 7:
						case 8:
						case 9:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
							break;
						case 10:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
							break;
						default:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
							break;
					}
				}
				count++;
			}
		}

		private void StartingHandBackGroundChange(int id)
		{
			int count = 0;
			foreach (ComboBox comboBox in AsEnumerableStartingHandComboBoxes())
			{
				if (count == id)
				{
					switch (position_list[count])
					{
						case 1:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LightCoral);
							break;
						case 2:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Moccasin);
							break;
						case 7:
						case 8:
						case 9:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
							break;
						case 10:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
							break;
						default:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
							break;
					}
				}
				count++;
			}
		}

		private void StartingBatBackGroundChange(int id)
		{
			int count = 0;
			foreach (ComboBox comboBox in AsEnumerableStartingBatComboBoxes())
			{
				if (count == id)
				{
					switch (position_list[count])
					{
						case 1:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LightCoral);
							break;
						case 2:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Moccasin);
							break;
						case 7:
						case 8:
						case 9:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
							break;
						case 10:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
							break;
						default:
							comboBox.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
							break;
					}
				}
				count++;
			}
		}

		private void StartingNoBackGroundChange(int id)
		{
			int count = 0;
			foreach (TextBox text in AsEnumerableStartingNumberTextBoxes())
			{
				if (count == id)
				{
					switch (position_list[count])
					{
						case 1:
							text.Background = new SolidColorBrush(Windows.UI.Colors.LightCoral);
							break;
						case 2:
							text.Background = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Moccasin);
							break;
						case 7:
						case 8:
						case 9:
							text.Background = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
							break;
						case 10:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Orange);
							break;
						default:
							text.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
							break;
					}
				}
				count++;
			}
		}


		private void PositionListDataCheck(int id)
		{
			int count = 0;
			int _positon = 0;


			foreach (var t in AsEnumerablePositionComboBoxes())
			{
				Position tmp_pos = (Position)t.SelectedItem;
				if (tmp_pos != null)
				{
					if (int.TryParse(tmp_pos.id.ToString(), out _positon))
					{
						if (count == id)
						{
							position_list[count] = _positon;
						}
					}
				}
				count++;
			}

		}

		private void DataInto_Click(object sender, RoutedEventArgs e)
		{
			PutListDataAtPlayer();
		}

		private void ClearStarting_Click(object sender, RoutedEventArgs e)
		{
			ClearLists();
			IntoStartingWebjets();
			IntoReserveWebjets();
		}




		private void UpdateOrderEntryRtn()
		{
			/// 更新処理
			string orderST;
			for (int count = 0; count < 30; count++)
			{
				if (count <= 10)
				{
					orderST = (count + 1).ToString();
					if (count == 10) { orderST = "P"; }

					PlayerData.updateRecord(
					team_id: team_list[count],
					player_id: player_list[count],
					player_name: name_list[count],
					bat_id: bat_list[count],
					hand_id: hand_list[count],
					player_num: no_list[count],
					position: position_list[count],
					etc_str2: orderST
					);
				}
				else
				{
					PlayerData.updateRecord(
					   team_id: team_list[count],
					   player_id: player_list[count],
					   player_name: name_list[count],
					   bat_id: bat_list[count],
					   hand_id: hand_list[count],
					   player_num: no_list[count],
					   //position: position_list[count],
					   etc_str2: "0"
					   );

				}

			}
		}
		private void InsertOrderEntryRtn()
		{
			/// 新規追加処理
			int player_id;
			string orderST;
			for (int count = 0; count < 30; count++)
			{
				if (name_list[count] == "")
				{
					break;
				}
				if (name_list[count] is null)
				{
					break;
				}

				if (bat_list[count] < 0)
				{
					break;
				}
				if (hand_list[count] < 0)
				{
					break;
				}

				player_id = PlayerData.GetPlayerIdRecordsCount()[0].player_id;
				if (count < 10)
				{

					orderST = (count + 1).ToString();
					if (count == 9) { orderST = "P"; }

					PlayerData.addRecord(
						player_id: player_id,
						team_id: team_list[count],
						etc_str2: orderST,
						player_name: name_list[count],
						position: position_list[count],
						bat_id: bat_list[count],
						hand_id: hand_list[count],
						player_num: no_list[count],
						update_date: DateTime.Now
						);
				}
				else
				{
					PlayerData.addRecord(
						player_id: player_id,
						team_id: team_list[count],
						etc_str2: "0",
						player_name: name_list[count],
						bat_id: bat_list[count],
						hand_id: hand_list[count],
						player_num: no_list[count],
						update_date: DateTime.Now
						);
				}
			}
		}
		private void DeleteOrderEntryRtn()
		{
			/// 削除処理
		}

		private bool CheckStartingName()
		{
			string tmp_name;
			for (int i = 0; i < 10; i++)
			{
				tmp_name = name_list[i] ?? "";
				if (tmp_name == "")
				{
					return false;
				}
			}
			return true;
		}



		private bool CheckPositionDuplication()
		{
			string msg = "";
			var duplicates = position_list.GroupBy(id => id).Where(name => name.Count() > 1).Select(group => group.Key).ToList();

			if (duplicates.Count > 0)
			{
				string position_name = "";
				foreach (var ret_positon in duplicates)
				{
					position_name = Positions[ret_positon - 1].name;
					msg = "\"" + position_name + "\" is Duplication.";
				}
				MsgWarning(msg);
			}
			return true;
		}

		private bool CheckLists()
		{
			if (!CheckStartingName())
			{
				return false;
			}
			if (!CheckPositionDuplication())
			{
				return false;
			}
			return true;
		}


		private void Entry_btn_Click(object sender, RoutedEventArgs e)
		{
			string msg;
			if (!IntoStartingLists())
			{
				msg = "Failed to get starting lineup";
				MsgWarning(msg);
				return;
			}

			if (!IntoReserveLists())
			{
				msg = "Failed to get reserve lineup";
				MsgWarning(msg);
				return;
			}

			if (!CheckLists())
			{
				msg = "Falied to get lineup";
				MsgWarning(msg);
				return;
			}

			InsertOrderEntryRtn();
			MsgEntryResult();
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

		private void OrderListNavigation_btn_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(OrderListPage));
		}
	}
}
