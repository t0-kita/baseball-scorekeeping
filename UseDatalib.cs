using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using Microsoft.Toolkit.Uwp;



namespace MMSS
{
	class UseDatalib
	{

		public UseDatalib()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			if (!File.Exists(pathToDB))
			{
				using (FileStream stream = File.Create(pathToDB))
				{
					if (null != stream)
					{
						stream.Close();
					}
				}
			}

			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				string initCMD = "CREATE TABLE IF NOT EXISTS "
					+ "userinfo (userEmail NVARCHAR(50) PRIMARY KEY,"
					+ "userName NVARCHAR(50) NOT NULL)";
				SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);  // Create command (string Command, FileName)
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(string Email, string Name)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				CMD_Insert.CommandText = "INSERT INTO userInfo VALUES(@userEmail, @userName);";
				CMD_Insert.Parameters.AddWithValue("@userEmail", Email);
				CMD_Insert.Parameters.AddWithValue("@userName", Name);
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void delRecord(string Email = "", string Name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Delete = new SqliteCommand();
				CMD_Delete.Connection = con;
				CMD_Delete.CommandText = "DELETE from userInfo where userEmail=@userEmail or userName=@userName;";
				CMD_Delete.Parameters.AddWithValue("@userEmail", Email);
				CMD_Delete.Parameters.AddWithValue("@userName", Name);
				CMD_Delete.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(string Email = "", string Name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				CMD_Update.CommandText = "UPDATE userInfo set userName=@userName  where userEmail=@userEmail;";
				CMD_Update.Parameters.AddWithValue("@userEmail", Email);
				CMD_Update.Parameters.AddWithValue("@userName", Name);
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class userDatails
		{
			public string name { get; set; }
			public string email { get; set; }
			public userDatails(string UserName, string UserEmail)
			{
				name = UserName;
				email = UserEmail;
			}
		}

		public static List<userDatails> GetRecords()
		{
			List<userDatails> userList = new List<userDatails>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				string selectCmd = "SELECT userName,userEmail  FROM userinfo";
				SqliteCommand cmd_getRec = new SqliteCommand(selectCmd, con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					userList.Add(new userDatails(reader.GetString(0), reader.GetString(1)));
				}
				con.Close();
			}
			return userList;
		}
	}

	#region イニングクラス
	class IningDetails
	{
		public int IsSideID { get; set; }

		public IningDetails()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				string initCMD = "CREATE TABLE IF NOT EXISTS "
					+ "iniScore ("
					+ "teamName,"
					+ "_1  ,"
					+ "_2  ,"
					+ "_3  ,"
					+ "_4  ,"
					+ "_5  ,"
					+ "_6  ,"
					+ "_7  ,"
					+ "_8  ,"
					+ "_9  ,"
					+ "_10  ,"
					+ "_11  ,"
					+ "_12  ,"
					+ "total_score,"
					+ "hit,"
					+ "error)";
				SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);  // Create command (string Command, FileName)
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		/// <summary>
		/// イニングの初期画面表示用に先行・後攻を作るために用いる
		/// </summary>
		/// <param name="Name"></param>
		public static void addRecord(string teamName)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	iniScore ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@teamName, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	0, ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	 ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@teamName", teamName);
				CMD_Insert.ExecuteReader();
				con.Close();


			}
		}

		public static void delRecord(string teamName)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Delete = new SqliteCommand();
				CMD_Delete.Connection = con;
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE FROM iniScore ");
				sqlST.AppendLine("WHERE teamName=@teamName ");
				//CMD_Delete.CommandText = "DELETE from userInfo where userEmail=@userEmail or userName=@userName;";
				CMD_Delete.CommandText = sqlST.ToString();
				CMD_Delete.Parameters.AddWithValue("@teamName", teamName);
				CMD_Delete.ExecuteReader();
				con.Close();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Name"></param> 指定するチーム
		/// <param name="target"></param> 指定する変更位置
		/// <param name="score"></param> 変更値
		public static void updateRecord(string teamName, int? hit, int? error,
										int? _1, int? _2, int? _3, int? _4, int? _5, int? _6,
										int? _7, int? _8, int? _9, int? _10, int? _11, int? _12)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE iniScore ");
				sqlST.AppendLine("	SET ");
				int total_score = 0;
				if (_1 != null)
				{
					sqlST.AppendLine("		_1 ");
					sqlST.AppendLine("			=@ini_1, ");
					CMD_Update.Parameters.AddWithValue("@ini_1", _1);
					total_score += Convert.ToInt32(_1);

				}
				if (_2 != null)
				{
					sqlST.AppendLine("		_2 ");
					sqlST.AppendLine("			=@ini_2, ");
					CMD_Update.Parameters.AddWithValue("@ini_2", _2);
					total_score += Convert.ToInt32(_2);
				}
				if (_3 != null)
				{
					sqlST.AppendLine("		_3 ");
					sqlST.AppendLine("			=@ini_3, ");
					CMD_Update.Parameters.AddWithValue("@ini_3", _3);
					total_score += Convert.ToInt32(_3);
				}
				if (_4 != null)
				{
					sqlST.AppendLine("		_4 ");
					sqlST.AppendLine("			=@ini_4, ");
					CMD_Update.Parameters.AddWithValue("@ini_4", _4);
					total_score += Convert.ToInt32(_4);
				}
				if (_5 != null)
				{
					sqlST.AppendLine("		_5 ");
					sqlST.AppendLine("			=@ini_5, ");
					CMD_Update.Parameters.AddWithValue("@ini_5", _5);
					total_score += Convert.ToInt32(_5);
				}
				if (_6 != null)
				{
					sqlST.AppendLine("		_6 ");
					sqlST.AppendLine("			=@ini_6, ");
					CMD_Update.Parameters.AddWithValue("@ini_6", _6);
					total_score += Convert.ToInt32(_6);
				}
				if (_7 != null)
				{
					sqlST.AppendLine("		_7 ");
					sqlST.AppendLine("			=@ini_7, ");
					CMD_Update.Parameters.AddWithValue("@ini_7", _7);
					total_score += Convert.ToInt32(_7);
				}
				if (_8 != null)
				{
					sqlST.AppendLine("		_8 ");
					sqlST.AppendLine("			=@ini_8, ");
					CMD_Update.Parameters.AddWithValue("@ini_8", _8);
					total_score += Convert.ToInt32(_8);
				}
				if (_9 != null)
				{
					sqlST.AppendLine("		_9 ");
					sqlST.AppendLine("			=@ini_9, ");
					CMD_Update.Parameters.AddWithValue("@ini_9", _9);
					total_score += Convert.ToInt32(_9);
				}
				if (_10 != null)
				{
					sqlST.AppendLine("		_10 ");
					sqlST.AppendLine("			=@ini_10, ");
					CMD_Update.Parameters.AddWithValue("@ini_10", _10);
					total_score += Convert.ToInt32(_10);
				}
				if (_11 != null)
				{
					sqlST.AppendLine("		_11 ");
					sqlST.AppendLine("			=@ini_11, ");
					CMD_Update.Parameters.AddWithValue("@ini_11", _11);
					total_score += Convert.ToInt32(_11);
				}
				if (_12 != null)
				{
					sqlST.AppendLine("		_12 ");
					sqlST.AppendLine("			=@ini_12, ");
					CMD_Update.Parameters.AddWithValue("@ini_12", _12);
					total_score += Convert.ToInt32(_12);
				}
				if (hit != null)
				{
					sqlST.AppendLine("		hit ");
					sqlST.AppendLine("			=@hit, ");
					CMD_Update.Parameters.AddWithValue("@hit", hit);

				}
				if (error != null)
				{
					sqlST.AppendLine("		error ");
					sqlST.AppendLine("			=@error ");
					CMD_Update.Parameters.AddWithValue("@error", error);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName=@teamName; ");
				CMD_Update.Parameters.AddWithValue("@teamName", teamName);
				CMD_Update.CommandText = sqlST.ToString();



				sqlST.AppendLine("		計 ");
				sqlST.AppendLine("			=@total_score, ");

				//CMD_Update.CommandText = "UPDATE iniScore set " + ini_1.ToString() + "=@ini_1 where teamName=@teamName;";
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
			List<iningDetails> orderList = new List<iningDetails>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				StringBuilder sql = new StringBuilder();
				sql.AppendLine("SELECT	");
				sql.AppendLine("	teamName AS チーム名,	");
				sql.AppendLine("	_1 AS １,	");
				sql.AppendLine("	_2 AS ２,	");
				sql.AppendLine("	_3 AS ３,	");
				sql.AppendLine("	_4 AS ４,	");
				sql.AppendLine("	_5 AS ５,	");
				sql.AppendLine("	_6 AS ６,	");
				sql.AppendLine("	_7 AS ７,	");
				sql.AppendLine("	_8 AS ８,	");
				sql.AppendLine("	_9 AS ９,	");
				sql.AppendLine("	_10 AS １０,	");
				sql.AppendLine("	_11 AS １１,	");
				sql.AppendLine("	_12 AS １２,	");
				sql.AppendLine("	total_score AS 計,	");
				sql.AppendLine("	hit AS H,	");
				sql.AppendLine("	error AS E,	");
				sql.AppendLine("FROM	");
				sql.AppendLine("	iniScore	");
				//string selectCmd = "SELECT teamName AS FROM iniScore";
				SqliteCommand cmd_getRec = new SqliteCommand(sql.ToString(), precon);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new iningDetails(teamName: reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)
												, reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7)
												, reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)
												, reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15)));
				}
				precon.Close();
			}
		}

		public static void scoreChangeUpdateRecord(string teamName = null, string hit = null, string error = null,
										string _1 = null, string _2 = null, string _3 = null, string _4 = null, string _5 = null, string _6 = null,
										string _7 = null, string _8 = null, string _9 = null, string _10 = null, string _11 = null, string _12 = null)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE iniScore ");
				sqlST.AppendLine("	SET ");
				if (_1 != null)
				{
					sqlST.AppendLine("		ini_1 ");
					sqlST.AppendLine("			=@ini_1, ");
					CMD_Update.Parameters.AddWithValue("@ini_1", _1);
				}
				if (_2 != null)
				{
					sqlST.AppendLine("		ini_2 ");
					sqlST.AppendLine("			=@ini_2, ");
					CMD_Update.Parameters.AddWithValue("@ini_2", _2);
				}
				if (_3 != null)
				{
					sqlST.AppendLine("		ini_3 ");
					sqlST.AppendLine("			=@ini_3, ");
					CMD_Update.Parameters.AddWithValue("@ini_3", _3);
				}
				if (_4 != null)
				{
					sqlST.AppendLine("		ini_4 ");
					sqlST.AppendLine("			=@ini_4, ");
					CMD_Update.Parameters.AddWithValue("@ini_4", _4);
				}
				if (_5 != null)
				{
					sqlST.AppendLine("		ini_5 ");
					sqlST.AppendLine("			=@ini_5, ");
					CMD_Update.Parameters.AddWithValue("@ini_5", _5);
				}
				if (_6 != null)
				{
					sqlST.AppendLine("		ini_6 ");
					sqlST.AppendLine("			=@ini_6, ");
					CMD_Update.Parameters.AddWithValue("@ini_6", _6);
				}
				if (_7 != null)
				{
					sqlST.AppendLine("		ini_7 ");
					sqlST.AppendLine("			=@ini_7, ");
					CMD_Update.Parameters.AddWithValue("@ini_7", _7);

				}
				if (_8 != null)
				{
					sqlST.AppendLine("		ini_8 ");
					sqlST.AppendLine("			=@ini_8, ");
					CMD_Update.Parameters.AddWithValue("@ini_8", _8);

				}
				if (_9 != null)
				{
					sqlST.AppendLine("		ini_9 ");
					sqlST.AppendLine("			=@ini_9, ");
					CMD_Update.Parameters.AddWithValue("@ini_9", _9);

				}
				if (_10 != null)
				{
					sqlST.AppendLine("		ini_10 ");
					sqlST.AppendLine("			=@ini_10, ");
					CMD_Update.Parameters.AddWithValue("@ini_10", _10);

				}
				if (_11 != null)
				{
					sqlST.AppendLine("		ini_11 ");
					sqlST.AppendLine("			=@ini_11, ");
					CMD_Update.Parameters.AddWithValue("@ini_11", _11);

				}
				if (_12 != null)
				{
					sqlST.AppendLine("		ini_12 ");
					sqlST.AppendLine("			=@ini_12, ");
					CMD_Update.Parameters.AddWithValue("@ini_12", _12);
				}
				//sqlST.AppendLine("		total_score ");
				//sqlST.AppendLine("			=@total_score, ");
				if (hit != null)
				{
					sqlST.AppendLine("		hit ");
					sqlST.AppendLine("			=@hit, ");
					CMD_Update.Parameters.AddWithValue("@hit", hit);

				}
				if (error != null)
				{
					sqlST.AppendLine("		error ");
					sqlST.AppendLine("			=@error ");
					CMD_Update.Parameters.AddWithValue("@error", error);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName=@teamName; ");
				//CMD_Update.CommandText = "UPDATE iniScore set " + ini_1.ToString() + "=@ini_1 where teamName=@teamName;";
				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				List<string> score_list = new List<string>();
				List<int> score_Intlist = new List<int>();
				string[] ini_list = new string[] { _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12 };
				foreach (string s in ini_list)
				{
					if (s == "-1") { score_list.Add(""); }
					else
					{
						score_list.Add(s);
						if (s == "") { continue; }
						score_Intlist.Add(Convert.ToInt32(s));
					}
				}
				int total_score = score_Intlist.Sum();
				CMD_Update.Parameters.AddWithValue("@teamName", teamName);
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class iningDetails
		{
			public string teamname { get; set; }
			#region 変数　イニング
			public int _1 { get; set; }
			public int _2 { get; set; }
			public int _3 { get; set; }
			public int _4 { get; set; }
			public int _5 { get; set; }
			public int _6 { get; set; }
			public int _7 { get; set; }
			public int _8 { get; set; }
			public int _9 { get; set; }
			public int _10 { get; set; }
			public int _11 { get; set; }
			public int _12 { get; set; }
			public int total_score { get; set; }
			#endregion
			public int hit { get; set; }
			public int error { get; set; }

			public iningDetails(string teamName,
								int _1 = 0, int _2 = 0, int _3 = 0, int _4 = 0, int _5 = 0, int _6 = 0,
								int _7 = 0, int _8 = 0, int _9 = 0, int _10 = 0, int _11 = 0, int _12 = 0,
								int total_score = 0,
								int H = 0, int E = 0)
			{
				this.teamname = teamName;
				this._1 = _1;
				this._2 = _2;
				this._3 = _3;
				this._4 = _4;
				this._5 = _5;
				this._6 = _6;
				this._7 = _7;
				this._8 = _8;
				this._9 = _9;
				this._10 = _10;
				this._11 = _11;
				this._12 = _12;
				int[] ini_list = new int[] { _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12 };
				this.total_score = ini_list.Sum();
				this.hit = H;
				this.error = E;
			}
		}

		public static List<iningDetails> GetRecords()
		{
			List<iningDetails> iniList = new List<iningDetails>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sql = new StringBuilder();
				sql.AppendLine("SELECT	");
				sql.AppendLine("	teamName AS チーム名,	");
				sql.AppendLine("	_1 AS １,	");
				sql.AppendLine("	_2 AS ２,	");
				sql.AppendLine("	_3 AS ３,	");
				sql.AppendLine("	_4 AS ４,	");
				sql.AppendLine("	_5 AS ５,	");
				sql.AppendLine("	_6 AS ６,	");
				sql.AppendLine("	_7 AS ７,	");
				sql.AppendLine("	_8 AS ８,	");
				sql.AppendLine("	_9 AS ９,	");
				sql.AppendLine("	_10 AS １０,	");
				sql.AppendLine("	_11 AS １１,	");
				sql.AppendLine("	_12 AS １２,	");
				sql.AppendLine("	total_score AS 計,	");
				sql.AppendLine("	hit AS H,	");
				sql.AppendLine("	error AS E,	");
				sql.AppendLine("FROM	");
				sql.AppendLine("	iniScore	");
				//string selectCmd = "SELECT teamName AS FROM iniScore";
				SqliteCommand cmd_getRec = new SqliteCommand(sql.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					iniList.Add(new iningDetails(teamName: reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)
												, reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7)
												, reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11)
												, reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14), reader.GetInt32(15)));
				}
				con.Close();
			}
			return iniList;
		}

		public bool IsSide
		{
			get
			{
				if (IsSideID == 0) { return true; }
				else { return false; }
			}
		}
	}
	#endregion

	#region オーダー
	class PlayOrder
	{

		public PlayOrder()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	playorder ( ");
				sqlST.AppendLine("		teamName , ");
				sqlST.AppendLine("		order_id   , ");
				sqlST.AppendLine("		name   , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		bat   , ");
				sqlST.AppendLine("		hand    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(string teamName,
									 string order_id,
									 string name,
									 string position,
									 string bat,
									 string hand,
									 string player_id,
									 string team_id,
									 string game_id
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@teamName, ");
				sqlST.AppendLine("	@order_id, ");
				sqlST.AppendLine("	@name, ");
				sqlST.AppendLine("	@position, ");
				sqlST.AppendLine("	@bat, ");
				sqlST.AppendLine("	@hand ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@team_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@game_id ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@teamName", teamName);
				CMD_Insert.Parameters.AddWithValue("@order_id", order_id);
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@position", position);
				CMD_Insert.Parameters.AddWithValue("@bat", bat);
				CMD_Insert.Parameters.AddWithValue("@hand", hand);
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}


		public static void updateRecord(string teamName,
										string order_id = "",
										string name = "",
										string position = "",
										string bat = "",
										string hand = "",
										string player_id = "",
										string team_id = "",
										string game_id = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				bool selFlg = false;
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE	");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("SET ");
				if (name.Length != 0)
				{
					sqlST.AppendLine("		name ");
					sqlST.AppendLine("			=@name ");
					CMD_Update.Parameters.AddWithValue("@name", name);
					selFlg = true;
				}
				if (position.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		position ");
					sqlST.AppendLine("			=@position ");
					CMD_Update.Parameters.AddWithValue("@position", position);
					selFlg = true;
				}
				if (bat.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		bat ");
					sqlST.AppendLine("			=@bat ");
					CMD_Update.Parameters.AddWithValue("@bat", bat);
					selFlg = true;
				}
				if (hand.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		hand ");
					sqlST.AppendLine("			=@hand ");
					CMD_Update.Parameters.AddWithValue("@hand", hand);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName=@teamName ");

				if (order_id.Length != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	order_id ");
					sqlST.AppendLine("		=@order_id ");
				}
				if (player_id.Length != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	player_id ");
					sqlST.AppendLine("		=@player_id ");
				}
				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.Parameters.AddWithValue("@player_id", player_id);
				CMD_Update.Parameters.AddWithValue("@teamName", teamName);
				CMD_Update.Parameters.AddWithValue("@order_id", order_id);

				CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				CMD_Update.Parameters.AddWithValue("@game_id", game_id);


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}



		public static void deleteRecord(string teamName = "",
										string player_id = "",
										string team_id = "",
										string order_id = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id='{0}'", team_id).AppendLine();
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					CmdDelete.Parameters.AddWithValue("@teamName", teamName);
				}
				if (player_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	player_id='{0}' ", player_id).AppendLine();
					//sqlST.AppendLine("	player_id=@player_id ");
					//CmdDelete.Parameters.AddWithValue("@player_id", player_id);
				}
				if (order_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	order_id='{0}' ", order_id).AppendLine();
					//sqlST.AppendLine("	order_id=@order_id ");
					//CmdDelete.Parameters.AddWithValue("@order_id", order_id);
				}

				CmdDelete.CommandText = sqlST.ToString();



				#endregion

				if (teamName != "")
				{
					CmdDelete.ExecuteReader();
				}
				con.Close();
			}
		}


		public class playOrder
		{

			#region オーダー変数
			public string teamName { get; set; }
			public string order_id { get; set; }
			public string name { get; set; }
			public string position { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			public string player_id { get; set; }
			public string team_id { get; set; }
			public string game_id { get; set; }
			#endregion


			public playOrder(string teamName = "",
							 string order_id = "",
							 string name = "",
							 string position = "",
							 string bat = "",
							 string hand = "",
							 string player_id = "",
							 string team_id = "",
							 string game_id = "")
			{
				this.teamName = teamName;
				this.order_id = order_id;
				this.name = name;
				this.position = position;
				this.bat = bat;
				this.hand = hand;
				this.player_id = player_id;
				this.team_id = team_id;
				this.game_id = game_id;

			}
		}

		/// <summary>
		/// CSVファイルによる選手データの取得のための
		/// 空データ
		/// </summary>
		/// <param name="teamName"></param>
		/// <returns></returns>
		public static List<playOrder> GetRecordsScvFile(string teamName = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			return orderList;
		}

		public static List<playOrder> GetRecordsAllMember(
												string teamName = "",
												string team_id = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id='{0}' ", team_id).AppendLine();
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}

		public class playerDataCount
		{
			public int player_count { get; set; }
			public playerDataCount(int count = 0)
			{
				this.player_count = count;
			}
		}
		public static List<playerDataCount> GetRecordsCount()
		{
			List<playerDataCount> countList = new List<playerDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(player_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		/// <summary>
		/// Player_idの生成に用いる
		/// </summary>
		public class playerIdGetCount
		{
			public int player_id { get; set; }
			public playerIdGetCount(int count = 0)
			{
				this.player_id = count;
			}
		}
		public static List<playerIdGetCount> GetPlayerIdRecordsCount()
		{
			List<playerIdGetCount> countList = new List<playerIdGetCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	Max(player_id) + 1 ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerIdGetCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public class playerPositionSearch
		{
			public int player_id { get; set; }
			public int team_id { get; set; }
			public int game_id { get; set; }
			public int position { get; set; }
			public playerPositionSearch(
				int player_id = 0,
				int team_id = 0,
				int game_id = 0,
				int position = 0)
			{
				this.player_id = player_id;
				this.team_id = team_id;
				this.game_id = game_id;
				this.position = position;

			}
		}
		public static List<playerPositionSearch> SearchPosition(
													int team_id = 0,
													int game_id = 0,
													int position = 0
			)
		{
			List<playerPositionSearch> countList = new List<playerPositionSearch>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	team_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	game_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	position ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	position={0} ", position).AppendLine();
				if (game_id != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(
							new playerPositionSearch(
								reader.GetInt32(0),
								reader.GetInt32(1),
								reader.GetInt32(2),
								reader.GetInt32(3)
								)
							);
				}
			}
			return countList;
		}

		public class teamCount
		{
			public int team_count { get; set; }
			public teamCount(int count = 0)
			{
				this.team_count = count;

			}
		}
		public static List<teamCount> GetRecordsTeamCount(int team_id)
		{
			List<teamCount> countList = new List<teamCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(team_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id='{0}'", team_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new teamCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public static List<playOrder> GetRecords(
											string teamName = "",
											string team_id = ""
			)
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	(order_id<>'0' AND length(order_id)>0) ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				if (team_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	team_id=@team_id ");
					cmd_getRec.Parameters.AddWithValue("@team_id", team_id);
				}
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	order_id ");


				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<playOrder> GetRecordsReserve(
											string teamName = "",
											string team_id = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	(order_id='0' OR length(order_id)=0) ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				if (team_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	team_id=@team_id ");
					cmd_getRec.Parameters.AddWithValue("@team_id", team_id);
				}

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}


	}
	#endregion

	#region オーダー サブ
	class PlayOrderSub
	{

		public PlayOrderSub()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	playorder_sub ( ");
				sqlST.AppendLine("		teamName , ");
				sqlST.AppendLine("		order_id   , ");
				sqlST.AppendLine("		name   , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		bat   , ");
				sqlST.AppendLine("		hand    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_num    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(string teamName, string order_id, string name,
									 string position, string bat, string hand,
									 string player_id,
									 string team_id,
									 string game_id,
									 string player_num = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	playorder_sub ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@teamName, ");
				sqlST.AppendLine("	@order_id, ");
				sqlST.AppendLine("	@name, ");
				sqlST.AppendLine("	@position, ");
				sqlST.AppendLine("	@bat, ");
				sqlST.AppendLine("	@hand ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@team_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@game_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	@player_num ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@teamName", teamName);
				CMD_Insert.Parameters.AddWithValue("@order_id", order_id);
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@position", position);
				CMD_Insert.Parameters.AddWithValue("@bat", bat);
				CMD_Insert.Parameters.AddWithValue("@hand", hand);
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);
				CMD_Insert.Parameters.AddWithValue("@player_num", player_num);
				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(string teamName,
										string order_id = "",
										string name = "",
										string position = "",
										string bat = "",
										string hand = "",
										string player_id = "",
										string team_id = "",
										string game_id = "",
										string player_num = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				bool selFlg = false;

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE playorder_sub ");
				sqlST.AppendLine("	SET ");
				if (name.Length != 0)
				{
					sqlST.AppendLine("		name ");
					sqlST.AppendLine("			=@name ");
					CMD_Update.Parameters.AddWithValue("@name", name);
					selFlg = true;
				}
				if (position.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		position ");
					sqlST.AppendLine("			=@position ");
					CMD_Update.Parameters.AddWithValue("@position", position);
					selFlg = true;
				}
				if (bat.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		bat ");
					sqlST.AppendLine("			=@bat ");
					CMD_Update.Parameters.AddWithValue("@bat", bat);
					selFlg = true;
				}
				if (hand.Length != 0)
				{
					if (selFlg)
					{
						sqlST.AppendLine("		, ");
					}
					sqlST.AppendLine("		hand ");
					sqlST.AppendLine("			=@hand ");
					CMD_Update.Parameters.AddWithValue("@hand", hand);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName=@teamName ");

				if (order_id.Length != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	order_id ");
					sqlST.AppendLine("		=@order_id ");
				}
				if (player_id.Length != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	player_id ");
					sqlST.AppendLine("		=@player_id ");
				}

				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.Parameters.AddWithValue("@player_id", player_id);
				CMD_Update.Parameters.AddWithValue("@teamName", teamName);
				CMD_Update.Parameters.AddWithValue("@order_id", order_id);

				CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				CMD_Update.Parameters.AddWithValue("@game_id", game_id);


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public static void deleteRecord(string teamName = "",
										string player_id = "",
										string team_id = "",
										string order_id = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM playorder_sub ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id='{0}'", team_id).AppendLine();
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					CmdDelete.Parameters.AddWithValue("@teamName", teamName);
				}
				if (player_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	player_id='{0}' ", player_id).AppendLine();
					//sqlST.AppendLine("	player_id=@player_id ");
					//CmdDelete.Parameters.AddWithValue("@player_id", player_id);
				}
				if (order_id.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	order_id='{0}' ", order_id).AppendLine();
					//sqlST.AppendLine("	order_id=@order_id ");
					//CmdDelete.Parameters.AddWithValue("@order_id", order_id);
				}

				CmdDelete.CommandText = sqlST.ToString();



				#endregion

				if (teamName != "")
				{
					CmdDelete.ExecuteReader();
				}
				con.Close();
			}
		}


		public class playOrder
		{

			#region オーダー変数
			public string teamName { get; set; }
			public string order_id { get; set; }
			public string name { get; set; }
			public string position { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			public string player_id { get; set; }
			public string team_id { get; set; }
			public string game_id { get; set; }
			public string player_num { get; set; }
			#endregion


			public playOrder(string teamName = "",
							 string order_id = "",
							 string name = "",
							 string position = "",
							 string bat = "",
							 string hand = "",
							 string player_id = "",
							 string team_id = "",
							 string game_id = "",
							 string player_num = "")
			{
				this.teamName = teamName;
				this.order_id = order_id;
				this.name = name;
				this.position = position;
				this.bat = bat;
				this.hand = hand;
				this.player_id = player_id;
				this.team_id = team_id;
				this.game_id = game_id;
				this.player_num = player_num;

			}
		}

		/// <summary>
		/// CSVファイルによる選手データの取得のための
		/// 空データ
		/// </summary>
		/// <param name="teamName"></param>
		/// <returns></returns>
		public static List<playOrder> GetRecordsScvFile(
												string teamName = "",
												string team_id = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			return orderList;
		}

		public static List<playOrder> GetRecordsAllMember(
												string teamName = "",
												string team_id = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		team_id='{0}' ", team_id).AppendLine();
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8),
												player_num: reader.GetString(9)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}

		public class playerDataCount
		{
			public int player_count { get; set; }
			public playerDataCount(int count = 0)
			{
				this.player_count = count;

			}
		}
		public static List<playerDataCount> GetRecordsCount()
		{
			List<playerDataCount> countList = new List<playerDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(player_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		/// <summary>
		/// Player_idの生成に用いる
		/// </summary>
		public class playerIdGetCount
		{
			public int player_id { get; set; }
			public playerIdGetCount(int count = 0)
			{
				this.player_id = count;
			}
		}
		public static List<playerIdGetCount> GetPlayerIdRecordsCount()
		{
			List<playerIdGetCount> countList = new List<playerIdGetCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(player_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(player_id) + 1 ");
				sqlST.AppendLine("	END AS player_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerIdGetCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public class teamCount
		{
			public int team_count { get; set; }
			public teamCount(int count = 0)
			{
				this.team_count = count;

			}
		}
		public static List<teamCount> GetRecordsTeamCount(int team_id)
		{
			List<teamCount> countList = new List<teamCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(team_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id='{0}'", team_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new teamCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}




		public static List<playOrder> GetRecords(string teamName = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	(order_id<>'0' AND length(order_id)>0) ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<playOrder> GetRecordsReserve(string teamName = "")
		{
			List<playOrder> orderList = new List<playOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder_sub ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	(order_id='0' OR length(order_id)=0) ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new playOrder(teamName: reader.GetString(0),
												order_id: reader.GetString(1),
												name: reader.GetString(2),
												position: reader.GetString(3),
												bat: reader.GetString(4),
												hand: reader.GetString(5),
												player_id: reader.GetString(6),
												team_id: reader.GetString(7),
												game_id: reader.GetString(8)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}


	}
	#endregion

	#region 先攻チームオーダー
	class BatFirst
	{

		public BatFirst()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	batfirst ");
				sqlST.AppendLine("		order_id   , ");
				sqlST.AppendLine("		name   , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		bat   , ");
				sqlST.AppendLine("		hand    ");
				//sqlST.AppendLine("		teamName NVARCHAR(50) PRIMARY KEY, ");
				//sqlST.AppendLine("		order_id  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		name  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		position  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		bat  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		hand  NVARCHAR(50) NOT NULL  ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public class batFirst
		{

			#region オーダー変数
			public string order_id { get; set; }
			public string name { get; set; }
			public string position { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			#endregion


			public batFirst(string order_id = "", string name = "", string position = "", string bat = "", string hand = "")
			{
				this.order_id = order_id;
				this.name = name;
				this.position = position;
				this.bat = bat;
				this.hand = hand;
			}

		}
		public static List<batFirst> GetRecords()
		{
			List<batFirst> orderList = new List<batFirst>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				//string selectCmd = "SELECT order_id AS 打順, name AS 選手名,position AS 守, bat AS 打,hand AS 投 FROM playorder";
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	order_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	position ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	bat ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	hand ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName='Firstteam' ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new batFirst(order_id: reader.GetString(0),
											  name: reader.GetString(1),
											  position: reader.GetString(2),
											  bat: reader.GetString(3),
											  hand: reader.GetString(4)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 後攻チームオーダー
	class FieldFirst
	{

		public FieldFirst()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	fieldfirst ");
				sqlST.AppendLine("		order_id   , ");
				sqlST.AppendLine("		name   , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		bat   , ");
				sqlST.AppendLine("		hand    ");
				//sqlST.AppendLine("		teamName NVARCHAR(50) PRIMARY KEY, ");
				//sqlST.AppendLine("		order_id  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		name  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		position  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		bat  NVARCHAR(50) NOT NULL , ");
				//sqlST.AppendLine("		hand  NVARCHAR(50) NOT NULL  ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public class fieldFirst
		{

			#region オーダー変数
			public string order_id { get; set; }
			public string name { get; set; }
			public string position { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			#endregion


			public fieldFirst(string order_id = "", string name = "", string position = "", string bat = "", string hand = "")
			{
				this.order_id = order_id;
				this.name = name;
				this.position = position;
				this.bat = bat;
				this.hand = hand;
			}

		}
		public static List<fieldFirst> GetRecords()
		{
			List<fieldFirst> orderList = new List<fieldFirst>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	order_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	position ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	bat ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	hand ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	teamName='Secondteam' ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new fieldFirst(order_id: reader.GetString(0),
											  name: reader.GetString(1), position: reader.GetString(2), bat: reader.GetString(3), hand: reader.GetString(4)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region チーム
	class TeamData
	{

		public TeamData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	teams ( ");
				sqlST.AppendLine("		team_id , ");
				sqlST.AppendLine("		teamName , ");
				sqlST.AppendLine("		distinct_id , ");
				sqlST.AppendLine("		member_num , ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP  , ");
				sqlST.AppendLine("		bat   ,		");
				sqlST.AppendLine("		hand	,  ");
				sqlST.AppendLine("		Game_nm   , ");
				sqlST.AppendLine("		Game_win   , ");
				sqlST.AppendLine("		Game_lose	, ");
				sqlST.AppendLine("		update_dt    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}




		public static void addRecord(int team_id, string teamName = "", int distinct_id = 0, int member_num = 0,
									double BA = 0, double OPS = 0, double OBP = 0,
									double ERA = 0, double WHIP = 0,
									string bat = "", string hand = "",
									int Game_nm = 0, int Game_win = 0, int Game_lose = 0,
									DateTime? update_dt = null)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");

			/// team_idを新たに作成する
			//string sqlGetCount = "SELECT COUNT(team_id) FROM teams";
			//using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			//{
			//	precon.Open();
			//	SqliteCommand CMD_GetCount = new SqliteCommand();
			//	CMD_GetCount.Connection = precon;
			//	CMD_GetCount.CommandText = sqlGetCount;
			//	team_id = Convert.ToInt32(CMD_GetCount.ExecuteReader()) + 1;
			//	precon.Close();
			//}
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	teams ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@team_id, ");
				sqlST.AppendLine("	@teamName, ");
				sqlST.AppendLine("	@distinct_id, ");
				sqlST.AppendLine("	@member_num, ");
				sqlST.AppendLine("	@BA, ");
				sqlST.AppendLine("	@OPS, ");
				sqlST.AppendLine("	@OBP, ");
				sqlST.AppendLine("	@ERA, ");
				sqlST.AppendLine("	@WHIP, ");
				sqlST.AppendLine("	@bat, ");
				sqlST.AppendLine("	@hand, ");
				sqlST.AppendLine("	@Game_nm, ");
				sqlST.AppendLine("	@Game_win, ");
				sqlST.AppendLine("	@Game_lose, ");
				sqlST.AppendLine("	@update_dt ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);
				CMD_Insert.Parameters.AddWithValue("@teamName", teamName);
				CMD_Insert.Parameters.AddWithValue("@distinct_id", distinct_id);
				CMD_Insert.Parameters.AddWithValue("@member_num", member_num);
				CMD_Insert.Parameters.AddWithValue("@BA", BA);
				CMD_Insert.Parameters.AddWithValue("@OPS", OPS);
				CMD_Insert.Parameters.AddWithValue("@OBP", OBP);
				CMD_Insert.Parameters.AddWithValue("@ERA", ERA);
				CMD_Insert.Parameters.AddWithValue("@WHIP", WHIP);
				CMD_Insert.Parameters.AddWithValue("@bat", bat);
				CMD_Insert.Parameters.AddWithValue("@hand", hand);
				CMD_Insert.Parameters.AddWithValue("@Game_nm", Game_nm);
				CMD_Insert.Parameters.AddWithValue("@Game_win", Game_win);
				CMD_Insert.Parameters.AddWithValue("@Game_lose", Game_lose);
				CMD_Insert.Parameters.AddWithValue("@update_dt", update_dt);
				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(int team_id, string teamName = "", int distinct_id = 0, int member_num = 0,
										double BA = 0, double OPS = 0, double OBP = 0,
										double ERA = 0, double WHIP = 0,
										string bat = "", string hand = "",
										int Game_nm = 0, int Game_win = 0, int Game_lose = 0,
										DateTime? update_dt = null)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region チーム情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE teams ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendLine("		teamName ");
				sqlST.AppendLine("			=@teamName, ");
				sqlST.AppendLine("		distinct_id ");
				sqlST.AppendLine("			=@distinct_id, ");
				sqlST.AppendLine("		member_num ");
				sqlST.AppendLine("			=@member_num, ");
				sqlST.AppendLine("		BA ");
				sqlST.AppendLine("			=@BA, ");
				sqlST.AppendLine("		OPS ");
				sqlST.AppendLine("			=@OPS, ");
				sqlST.AppendLine("		OBP ");
				sqlST.AppendLine("			=@OBP, ");
				sqlST.AppendLine("		ERA ");
				sqlST.AppendLine("			=@ERA ");
				sqlST.AppendLine("		WHIP ");
				sqlST.AppendLine("			=@WHIP ");
				sqlST.AppendLine("		bat ");
				sqlST.AppendLine("			=@bat, ");
				sqlST.AppendLine("		hand ");
				sqlST.AppendLine("			=@hand, ");
				sqlST.AppendLine("		Game_nm ");
				sqlST.AppendLine("			=@Game_nm, ");
				sqlST.AppendLine("		Game_win ");
				sqlST.AppendLine("			=@Game_win, ");
				sqlST.AppendLine("		Game_lose ");
				sqlST.AppendLine("			=@Game_lose, ");
				sqlST.AppendLine("		update_dt ");
				sqlST.AppendLine("			=@update_dt ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	team_id=@team_id ");

				CMD_Update.CommandText = sqlST.ToString();
				#endregion


				CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				CMD_Update.Parameters.AddWithValue("@teamName", teamName);
				CMD_Update.Parameters.AddWithValue("@distinct_id", distinct_id);
				CMD_Update.Parameters.AddWithValue("@member_num", member_num);
				CMD_Update.Parameters.AddWithValue("@BA", BA);
				CMD_Update.Parameters.AddWithValue("@OPS", OPS);
				CMD_Update.Parameters.AddWithValue("@OBP", OBP);
				CMD_Update.Parameters.AddWithValue("@ERA", ERA);
				CMD_Update.Parameters.AddWithValue("@WHIP", WHIP);
				CMD_Update.Parameters.AddWithValue("@bat", bat);
				CMD_Update.Parameters.AddWithValue("@hand", hand);
				CMD_Update.Parameters.AddWithValue("@Game_nm", Game_nm);
				CMD_Update.Parameters.AddWithValue("@Game_win", Game_win);
				CMD_Update.Parameters.AddWithValue("@Game_lose", Game_lose);
				CMD_Update.Parameters.AddWithValue("@update_dt", update_dt);

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class teamDataCount
		{
			public int count { get; set; }
			public teamDataCount(int count = 0)
			{
				this.count = count;
			}
		}
		public static List<teamDataCount> GetRecordCount()
		{
			List<teamDataCount> orderList = new List<teamDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(team_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	teams ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new teamDataCount(
							reader.GetInt32(0)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}




		public class teamData
		{

			#region チーム変数
			public int team_id { get; set; }
			public string teamName { get; set; }
			public int distinct_id { get; set; }
			public int member_num { get; set; }
			public double BA { get; set; }     // 打率

			public double OPS { get; set; }     // OPS
			public double OBP { get; set; } // 出塁率
			public double ERA { get; set; } // 防御率

			public double WHIP { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			public int Game_nm { get; set; }
			public int Game_win { get; set; }
			public int Game_lose { get; set; }
			public DateTime? update_dt { get; set; }
			#endregion


			public teamData(int team_id, string teamName = "", int distinct_id = 0, int member_num = 0,
							double BA = 0, double OPS = 0, double OBP = 0,
							double ERA = 0, double WHIP = 0,
							string bat = "", string hand = "",
							int Game_nm = 0, int Game_win = 0, int Game_lose = 0,
							DateTime? update_dt = null)
			{
				this.team_id = team_id;
				this.teamName = teamName;
				this.distinct_id = distinct_id;
				this.member_num = member_num;
				this.BA = BA;
				this.OPS = OPS;
				this.OBP = OBP;
				this.ERA = ERA;
				this.WHIP = WHIP;
				this.bat = bat;
				this.hand = hand;
				this.Game_nm = Game_nm;
				this.Game_win = Game_win;
				this.Game_lose = Game_lose;
				this.update_dt = update_dt;
			}

		}
		public static List<teamData> GetRecords()
		{
			List<teamData> orderList = new List<teamData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	teams ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new teamData(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
												reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6),
												reader.GetDouble(7), reader.GetDouble(8),
												reader.GetString(9), reader.GetString(10),
												reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13),
												reader.GetDateTime(14)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}

		public class teamDataTeamSelect
		{

			#region チーム変数
			public int team_id { get; set; }
			public string teamName { get; set; }
			public int distinct_id { get; set; }
			public int member_num { get; set; }
			public double BA { get; set; }     // 打率

			public double OPS { get; set; }     // OPS
			public double OBP { get; set; } // 出塁率
			public double ERA { get; set; } // 防御率

			public double WHIP { get; set; }
			public string bat { get; set; }
			public string hand { get; set; }
			public int Game_nm { get; set; }
			public int Game_win { get; set; }
			public int Game_lose { get; set; }
			public DateTime? update_dt { get; set; }
			public string distinct_name { get; set; }
			#endregion


			public teamDataTeamSelect(
							int team_id,
							string teamName = "",
							int distinct_id = 0,
							int member_num = 0,
							double BA = 0, double OPS = 0, double OBP = 0,
							double ERA = 0, double WHIP = 0,
							string bat = "", string hand = "",
							int Game_nm = 0, int Game_win = 0, int Game_lose = 0,
							DateTime? update_dt = null,
							string distinct_name = "")
			{
				this.team_id = team_id;
				this.teamName = teamName;
				this.distinct_id = distinct_id;
				this.member_num = member_num;
				this.BA = BA;
				this.OPS = OPS;
				this.OBP = OBP;
				this.ERA = ERA;
				this.WHIP = WHIP;
				this.bat = bat;
				this.hand = hand;
				this.Game_nm = Game_nm;
				this.Game_win = Game_win;
				this.Game_lose = Game_lose;
				this.update_dt = update_dt;
				this.distinct_name = distinct_name;
			}

		}

		public static List<teamDataTeamSelect> GetRecordsGameMatchTeamSelect(
																string teamName = "",
																string distinctName = "")
		{
			List<teamDataTeamSelect> orderList = new List<teamDataTeamSelect>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	teams ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	prefectures ");
				sqlST.AppendLine("	ON  ");
				sqlST.AppendLine("	teams.distinct_id=prefectures.distinct_id ");
				if (teamName.Length != 0 || distinctName.Length != 0)
				{
					sqlST.AppendLine("WHERE  ");
				}
				if (teamName.Length != 0)
				{
					sqlST.AppendFormat("	teams.teamName LIKE '%{0}%'  ", teamName).AppendLine();
				}
				if (distinctName.Length != 0)
				{
					sqlST.AppendFormat("	prefectures.name LIKE '%{0}%'  ", distinctName).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new teamDataTeamSelect(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
												reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6),
												reader.GetDouble(7), reader.GetDouble(8),
												reader.GetString(9), reader.GetString(10),
												reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13),
												reader.GetDateTime(14),
												distinct_name: reader.GetString(16)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<teamData> GetRecordTeamData(int team_id = 0)
		{
			List<teamData> orderList = new List<teamData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	teams ");
				sqlST.AppendLine("Where ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new teamData(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
												reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6),
												reader.GetDouble(7), reader.GetDouble(8),
												reader.GetString(9), reader.GetString(10),
												reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13),
												reader.GetDateTime(14)
												)
								);
				}
				con.Close();
			}
			return orderList;
		}

		public static int GetRecordsRowsCount()
		{
			int RecordRowCount = 1;
			List<teamData> orderList = new List<teamData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	team_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	teams ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					RecordRowCount++;
					orderList.Add(new teamData(team_id: reader.GetInt32(0)
											  )
								);
				}
				//RecordRowCount = reader.GetInt32(0);
				con.Close();
			}
			return RecordRowCount;
		}

	}
	#endregion

	#region 選手成績
	class PlayerData
	{
		public PlayerData() { }

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	player ( ");
				/// 基本情報
				sqlST.AppendLine("		player_id , ");
				sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter    ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win    ,  ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat     ,  ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		pit_hit    ,   ");
				sqlST.AppendLine("		pit_home   ,   ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				sqlST.AppendLine("		etc_date1   , ");
				sqlST.AppendLine("		etc_date2   , ");
				sqlST.AppendLine("		etc_date3   , ");
				sqlST.AppendLine("		etc_date4   , ");
				sqlST.AppendLine("		etc_date5   , ");

				/// ver.1.0.9.0以降
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");


				/// ver.3.0.0.0 以降
				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(
								DateTime? season = null,
								int player_id = 0,
								string player_name = "",
								string bat = "",
								string hand = "",
								int position = 0,
								int player_num = 0,
								int team_id = 0,
								int game = 0,
								int run = 0,
								int hit = 0,
								int at_bat = 0,
								int plate = 0,
								int two_base = 0,
								int three_base = 0,
								int home_run = 0,
								int total_base = 0,
								int RBI = 0,
								int SB = 0,
								int not_SB = 0,
								int s_bunt = 0,
								int s_fly = 0,
								int BB = 0,
								int IBB = 0,
								int DBB = 0,
								int SO = 0,
								int DP = 0,
								double BA = 0.00,
								double SLG = 0.00,
								double OPS = 0.00,
								double OBP = 0.00,
								int game_pitch = 0,
								int starter = 0,
								int CG = 0,
								int shut_out = 0,
								int win = 0,
								int lose = 0,
								int save = 0,
								int hold = 0,
								double WP = 0.00,
								int pit_bat = 0,
								int IP = 0,
								int pit_hit = 0,
								int pit_home = 0,
								int pit_BB = 0,
								int pit_IBB = 0,
								int pit_DBB = 0,
								int pit_SO = 0,
								int wild_pitch = 0,
								int balk = 0,
								int pit_run = 0,
								int ER = 0,
								double ERA = 0,
								double WHIP = 0,
								DateTime? update_date = null,
								int etc_cd1 = 0,
								int etc_cd2 = 0,
								int etc_cd3 = 0,
								int etc_cd4 = 0,
								int etc_cd5 = 0,
								string etc_str1 = "",
								string etc_str2 = "",
								string etc_str3 = "",
								string etc_str4 = "",
								string etc_str5 = "",
								DateTime? etc_date1 = null,
								DateTime? etc_date2 = null,
								DateTime? etc_date3 = null,
								DateTime? etc_date4 = null,
								DateTime? etc_date5 = null,
								string cmnt1 = "",
								string cmnt2 = "",
								string cmnt3 = "",
								int bat_id = 0,
								int hand_id = 0,
								int selected = 0
								)
		{
			if (player_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("		(");
				/// 基本
				sqlST.AppendLine("		player_id , ");
				if (season != null)
				{
					sqlST.AppendLine("		season  , ");
				}
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter   ,   ");
				sqlST.AppendLine("		CG    ,  ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win   ,   ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat   ,    ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		pit_hit   ,    ");
				sqlST.AppendLine("		pit_home  ,    ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				if (etc_date1 != null)
				{
					sqlST.AppendLine("		etc_date1   , ");
				}
				if (etc_date2 != null)
				{
					sqlST.AppendLine("		etc_date2   , ");
				}
				if (etc_date3 != null)
				{
					sqlST.AppendLine("		etc_date3   , ");
				}
				if (etc_date4 != null)
				{
					sqlST.AppendLine("		etc_date4   , ");
				}
				if (etc_date5 != null)
				{
					sqlST.AppendLine("		etc_date5   , ");
				}
				//if (cmnt1.Length != 0)
				//{
				//	sqlST.AppendLine("		cmnt1   , ");
				//}
				//if (cmnt2.Length != 0)
				//{
				//	sqlST.AppendLine("		cmnt2   , ");
				//}
				//if (cmnt3.Length != 0)
				//{
				//	sqlST.AppendLine("		cmnt3   , ");
				//}
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");

				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		)    ");
				sqlST.AppendLine("VALUES( ");
				/// 基本
				sqlST.AppendFormat("		{0} , ", player_id).AppendLine();
				if (season != null)
				{
					sqlST.AppendFormat("		'{0}'  , ", season).AppendLine();
				}
				sqlST.AppendFormat("		'{0}'  , ", player_name).AppendLine();
				sqlST.AppendFormat("		{0}  , ", team_id).AppendLine();
				sqlST.AppendFormat("		'{0}'  , ", bat).AppendLine();
				sqlST.AppendFormat("		'{0}' , ", hand).AppendLine();
				sqlST.AppendFormat("		{0}  , ", position).AppendLine();
				sqlST.AppendFormat("		{0}  , ", player_num).AppendLine();
				/// 野手情報
				sqlST.AppendFormat("		{0}  , ", game).AppendLine();
				sqlST.AppendFormat("		{0}  , ", run).AppendLine();
				sqlST.AppendFormat("		{0} , ", hit).AppendLine();
				sqlST.AppendFormat("		{0}  , ", at_bat).AppendLine();
				sqlST.AppendFormat("		{0}  , ", plate).AppendLine();
				sqlST.AppendFormat("		{0}  , ", two_base).AppendLine();
				sqlST.AppendFormat("		{0}  , ", three_base).AppendLine();
				sqlST.AppendFormat("		{0}    , ", home_run).AppendLine();
				sqlST.AppendFormat("		{0}    , ", total_base).AppendLine();
				sqlST.AppendFormat("		{0}    , ", RBI).AppendLine();
				sqlST.AppendFormat("		{0}    , ", SB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", not_SB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", s_bunt).AppendLine();
				sqlST.AppendFormat("		{0}     , ", s_fly).AppendLine();
				sqlST.AppendFormat("		{0}    , ", BB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", IBB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", DBB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", SO).AppendLine();
				sqlST.AppendFormat("		{0}    , ", DP).AppendLine();
				sqlST.AppendFormat("		{0}   , ", BA).AppendLine();
				sqlST.AppendFormat("		{0}   , ", SLG).AppendLine();
				sqlST.AppendFormat("		{0}   , ", OPS).AppendLine();
				sqlST.AppendFormat("		{0}   , ", OBP).AppendLine();
				/// 投手情報
				sqlST.AppendFormat("		{0}    ,  ", game_pitch).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", starter).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", CG).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", shut_out).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", win).AppendLine();
				sqlST.AppendFormat("		{0}    , ", lose).AppendLine();
				sqlST.AppendFormat("		{0}    , ", save).AppendLine();
				sqlST.AppendFormat("		{0}    , ", hold).AppendLine();
				sqlST.AppendFormat("		{0}    , ", WP).AppendLine();
				sqlST.AppendFormat("		{0}    ,   ", pit_bat).AppendLine();
				sqlST.AppendFormat("		{0}    ,   ", IP).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", CG).AppendLine();
				sqlST.AppendFormat("		{0}    ,   ", pit_hit).AppendLine();
				sqlST.AppendFormat("		{0}    ,  ", pit_home).AppendLine();
				sqlST.AppendFormat("		{0}     , ", pit_BB).AppendLine();
				sqlST.AppendFormat("		{0}    , ", pit_IBB).AppendLine();
				sqlST.AppendFormat("		{0}     , ", pit_DBB).AppendLine();
				sqlST.AppendFormat("		{0}     , ", pit_SO).AppendLine();
				sqlST.AppendFormat("		{0}      , ", wild_pitch).AppendLine();
				sqlST.AppendFormat("		{0}     , ", balk).AppendLine();
				sqlST.AppendFormat("		{0}      , ", pit_run).AppendLine();
				sqlST.AppendFormat("		{0}      , ", ER).AppendLine();
				sqlST.AppendFormat("		{0}   , ", ERA).AppendLine();
				sqlST.AppendFormat("		{0}   , ", WHIP).AppendLine();
				/// 予備情報
				sqlST.AppendFormat("		{0}   , ", etc_cd1).AppendLine();
				sqlST.AppendFormat("		{0}   , ", etc_cd2).AppendLine();
				sqlST.AppendFormat("		{0}   , ", etc_cd3).AppendLine();
				sqlST.AppendFormat("		{0}   , ", etc_cd4).AppendLine();
				sqlST.AppendFormat("		{0}   , ", etc_cd5).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", etc_str1).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", etc_str2).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", etc_str3).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", etc_str4).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", etc_str5).AppendLine();
				if (etc_date1 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date1).AppendLine();
				}
				if (etc_date2 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date2).AppendLine();
				}
				if (etc_date3 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date3).AppendLine();
				}
				if (etc_date4 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date4).AppendLine();
				}
				if (etc_date5 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date5).AppendLine();
				}

				sqlST.AppendFormat("		'{0}'   , ", cmnt1).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", cmnt2).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", cmnt3).AppendLine();

				sqlST.AppendFormat("		'{0}'   , ", bat_id).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", hand_id).AppendLine();
				sqlST.AppendFormat("		'{0}'   , ", selected).AppendLine();
				///　更新日
				//sqlST.AppendFormat("		{0}    ", update_date).AppendLine();
				if (update_date != null)
				{
					sqlST.AppendLine("		@update_date    ");
					CMD_Insert.Parameters.AddWithValue("@update_date", update_date);
				}
				sqlST.AppendLine(" ); ");

				#region 旧コード
				///// 基本情報
				//#region 基本
				//sqlST.AppendLine("		@player_id , ");
				//CMD_Insert.Parameters.AddWithValue("@player_id", player_id);
				//if (season != null)
				//{
				//	sqlST.AppendLine("		@season  , ");
				//	CMD_Insert.Parameters.AddWithValue("@season", season);
				//}
				//if (player_name != "")
				//{
				//	sqlST.AppendLine("		@player_name  , ");
				//	CMD_Insert.Parameters.AddWithValue("@player_name", player_name);
				//}
				//if (team_id != 0)
				//{
				//	sqlST.AppendLine("		@team_id  , ");
				//	CMD_Insert.Parameters.AddWithValue("@team_id", team_id);
				//}
				//if (bat != "")
				//{
				//	sqlST.AppendLine("		@bat  , ");
				//	CMD_Insert.Parameters.AddWithValue("@bat", bat);
				//}
				//if (hand != "")
				//{
				//	sqlST.AppendLine("		@hand , ");
				//	CMD_Insert.Parameters.AddWithValue("@hand", hand);
				//}
				//if (position != 0)
				//{
				//	sqlST.AppendLine("		@position  , ");
				//	CMD_Insert.Parameters.AddWithValue("@position", position);
				//}
				//if (player_num != 0)
				//{
				//	sqlST.AppendLine("		@player_num  , ");
				//	CMD_Insert.Parameters.AddWithValue("@player_num", player_num);
				//}
				//#endregion
				///// 野手情報
				//#region 野手
				//if (game != 0)
				//{
				//	sqlST.AppendLine("		@game  , ");
				//	CMD_Insert.Parameters.AddWithValue("@game", game);
				//}
				//if (run != 0)
				//{
				//	sqlST.AppendLine("		@run  , ");
				//	CMD_Insert.Parameters.AddWithValue("@run", run);
				//}
				//if (hit != 0)
				//{
				//	sqlST.AppendLine("		@hit , ");
				//	CMD_Insert.Parameters.AddWithValue("@hit", hit);
				//}
				//if (at_bat != 0)
				//{
				//	sqlST.AppendLine("		@at_bat  , ");
				//	CMD_Insert.Parameters.AddWithValue("@at_bat", at_bat);
				//}
				//if (plate != 0)
				//{
				//	sqlST.AppendLine("		@plate  , ");
				//	CMD_Insert.Parameters.AddWithValue("@plate", plate);
				//}
				//if (two_base != 0)
				//{
				//	sqlST.AppendLine("		@two_base  , ");

				//	CMD_Insert.Parameters.AddWithValue("@two_base", two_base);
				//}
				//if (three_base != 0)
				//{
				//	sqlST.AppendLine("		@three_base  , ");
				//	CMD_Insert.Parameters.AddWithValue("@three_base", three_base);
				//}
				//if (home_run != 0)
				//{
				//	sqlST.AppendLine("		@home_run    , ");
				//	CMD_Insert.Parameters.AddWithValue("@home_run", home_run);
				//}
				//if (total_base != 0)
				//{
				//	sqlST.AppendLine("		@total_base    , ");
				//	CMD_Insert.Parameters.AddWithValue("@total_base", total_base);
				//}
				//if (RBI != 0)
				//{
				//	sqlST.AppendLine("		@RBI    , ");
				//	CMD_Insert.Parameters.AddWithValue("@RBI", RBI);
				//}
				//if (SB != 0)
				//{
				//	sqlST.AppendLine("		@SB    , ");
				//	CMD_Insert.Parameters.AddWithValue("@SB", SB);
				//}
				//if (s_bunt != 0)
				//{
				//	sqlST.AppendLine("		@s_bunt    , ");
				//	CMD_Insert.Parameters.AddWithValue("@s_bunt", s_bunt);
				//}
				//if (s_fly != 0)
				//{
				//	sqlST.AppendLine("		@s_fly     , ");
				//	CMD_Insert.Parameters.AddWithValue("@s_fly", s_fly);
				//}
				//if (BB != 0)
				//{
				//	sqlST.AppendLine("		@BB     ");
				//	CMD_Insert.Parameters.AddWithValue("@BB", BB);
				//}
				//if (IBB != 0)
				//{
				//	sqlST.AppendLine("		@IBB     ");
				//	CMD_Insert.Parameters.AddWithValue("@IBB", IBB);
				//}
				//if (DBB != 0)
				//{
				//	sqlST.AppendLine("		@DBB     ");
				//	CMD_Insert.Parameters.AddWithValue("@DBB", DBB);
				//}
				//if (SO != 0)
				//{
				//	sqlST.AppendLine("		@SO     ");
				//	CMD_Insert.Parameters.AddWithValue("@SO", SO);
				//}
				//if (DP != 0)
				//{
				//	sqlST.AppendLine("		@DP     ");
				//	CMD_Insert.Parameters.AddWithValue("@DP", DP);
				//}
				//if (BA != 0)
				//{
				//	sqlST.AppendLine("		@BA   , ");
				//	CMD_Insert.Parameters.AddWithValue("@BA", BA);
				//}
				//if (SLG != 0)
				//{
				//	sqlST.AppendLine("		@SLG   , ");
				//	CMD_Insert.Parameters.AddWithValue("@SLG", SLG);
				//}
				//if (OPS != 0)
				//{
				//	sqlST.AppendLine("		@OPS   , ");
				//	CMD_Insert.Parameters.AddWithValue("@OPS", OPS);
				//}
				//if (OBP != 0)
				//{
				//	sqlST.AppendLine("		@OBP   , ");
				//	CMD_Insert.Parameters.AddWithValue("@OBP", OBP);
				//}
				//#endregion

				///// 投手情報
				//#region 投手
				//if (game_pitch != 0)
				//{
				//	sqlST.AppendLine("		@game_pitch      ");
				//	CMD_Insert.Parameters.AddWithValue("@game_pitch", game_pitch);
				//}
				//if (starter != 0)
				//{
				//	sqlST.AppendLine("		@starter      ");
				//	CMD_Insert.Parameters.AddWithValue("@starter", starter);
				//}
				//if (CG != 0)
				//{
				//	sqlST.AppendLine("		@CG      ");
				//	CMD_Insert.Parameters.AddWithValue("@CG", CG);
				//}
				//if (shut_out != 0)
				//{
				//	sqlST.AppendLine("		@shut_out      ");
				//	CMD_Insert.Parameters.AddWithValue("@shut_out", shut_out);
				//}
				//if (win != 0)
				//{
				//	sqlST.AppendLine("		@win      ");
				//	CMD_Insert.Parameters.AddWithValue("@win", win);
				//}
				//if (lose != 0)
				//{
				//	sqlST.AppendLine("		@lose    , ");
				//	CMD_Insert.Parameters.AddWithValue("@lose", lose);
				//}
				//if (save != 0)
				//{
				//	sqlST.AppendLine("		@save    , ");
				//	CMD_Insert.Parameters.AddWithValue("@save", save);
				//}
				//if (hold != 0)
				//{
				//	sqlST.AppendLine("		@hold    , ");
				//	CMD_Insert.Parameters.AddWithValue("@hold", hold);
				//}
				//if (WP != 0)
				//{
				//	sqlST.AppendLine("		@WP    , ");
				//	CMD_Insert.Parameters.AddWithValue("@WP", WP);
				//}
				//if (pit_bat != 0)
				//{
				//	sqlST.AppendLine("		@pit_bat       ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_bat", pit_bat);
				//}
				//if (IP != 0)
				//{
				//	sqlST.AppendLine("		@IP       ");
				//	CMD_Insert.Parameters.AddWithValue("@IP", IP);
				//}
				//if (CG != 0)
				//{
				//	sqlST.AppendLine("		@CG      ");
				//	CMD_Insert.Parameters.AddWithValue("@CG", CG);
				//}
				//if (pit_hit != 0)
				//{
				//	sqlST.AppendLine("		@pit_hit       ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_hit", pit_hit);
				//}
				//if (pit_home != 0)
				//{
				//	sqlST.AppendLine("		@pit_home      ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_home", pit_home);
				//}
				//if (pit_BB != 0)
				//{
				//	sqlST.AppendLine("		@pit_BB     , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_BB", pit_BB);
				//}
				//if (pit_IBB != 0)
				//{
				//	sqlST.AppendLine("		@pit_IBB    , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_IBB", pit_IBB);
				//}
				//if (pit_DBB != 0)
				//{
				//	sqlST.AppendLine("		@pit_DBB     , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_DBB", pit_DBB);
				//}
				//if (pit_SO != 0)
				//{
				//	sqlST.AppendLine("		@pit_SO     , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_SO", pit_SO);
				//}
				//if (wild_pitch != 0)
				//{
				//	sqlST.AppendLine("		@wild_pitch      , ");
				//	CMD_Insert.Parameters.AddWithValue("@wild_pitch", wild_pitch);
				//}
				//if (balk != 0)
				//{
				//	sqlST.AppendLine("		@balk     , ");
				//	CMD_Insert.Parameters.AddWithValue("@balk", balk);
				//}
				//if (pit_run != 0)
				//{
				//	sqlST.AppendLine("		@pit_run      , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_run", pit_run);
				//}
				//if (ER != 0)
				//{
				//	sqlST.AppendLine("		@ER      , ");
				//	CMD_Insert.Parameters.AddWithValue("@ER", ER);
				//}
				//if (ERA != 0)
				//{
				//	sqlST.AppendLine("		@ERA   , ");
				//	CMD_Insert.Parameters.AddWithValue("@ERA", ERA);
				//}
				//if (WHIP != 0)
				//{
				//	sqlST.AppendLine("		@WHIP   , ");
				//	CMD_Insert.Parameters.AddWithValue("@WHIP", WHIP);
				//}
				//#endregion

				///// 予備情報
				//#region 予備
				//if (etc_cd1 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				//}
				//if (etc_cd2 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				//}
				//if (etc_cd3 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				//}
				//if (etc_cd4 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				//}
				//if (etc_cd5 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				//}
				//if (etc_str1 != "")
				//{
				//	sqlST.AppendLine("		@etc_str1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);
				//}
				//if (etc_str2 != "")
				//{
				//	sqlST.AppendLine("		@etc_str2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);
				//}
				//if (etc_str3 != "")
				//{
				//	sqlST.AppendLine("		@etc_str3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);
				//}
				//if (etc_str4 != "")
				//{
				//	sqlST.AppendLine("		@etc_str4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);
				//}
				//if (etc_str5 != "")
				//{
				//	sqlST.AppendLine("		@etc_str5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);
				//}
				//if (etc_date1 != null)
				//{
				//	sqlST.AppendLine("		@etc_date1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date1", etc_date1);
				//}
				//if (etc_date2 != null)
				//{
				//	sqlST.AppendLine("		@etc_date2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date2", etc_date2);
				//}
				//if (etc_date3 != null)
				//{
				//	sqlST.AppendLine("		@etc_date3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date3", etc_date3);
				//}
				//if (etc_date4 != null)
				//{
				//	sqlST.AppendLine("		@etc_date4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date4", etc_date4);
				//}
				//if (etc_date5 != null)
				//{
				//	sqlST.AppendLine("		@etc_date5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date5", etc_date5);
				//}
				//#endregion
				/////　更新日
				//if (update_date != null)
				//{
				//	sqlST.AppendLine("		@update_date    ");
				//	CMD_Insert.Parameters.AddWithValue("@update_date", update_date);
				//}
				//sqlST.AppendLine(" ); ");
				#endregion

				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(
								DateTime? season = null,
								int player_id = 0,
								string player_name = "",
								string bat = "",
								string hand = "",
								int position = 0,
								int player_num = -1,
								int team_id = 0,
								int game = 0,
								int run = 0,
								int hit = 0,
								int at_bat = 0,
								int plate = 0,
								int two_base = 0,
								int three_base = 0,
								int home_run = 0,
								int total_base = 0,
								int RBI = 0,
								int SB = 0,
								int not_SB = 0,
								int s_bunt = 0,
								int s_fly = 0,
								int BB = 0,
								int IBB = 0,
								int DBB = 0,
								int SO = 0,
								int DP = 0,
								double BA = 0.00,
								double SLG = 0.00,
								double OPS = 0.00,
								double OBP = 0.00,
								int game_pitch = 0,
								int starter = 0,
								int CG = 0,
								int shut_out = 0,
								int win = 0,
								int lose = 0,
								int save = 0,
								int hold = 0,
								double WP = 0.00,
								int pit_bat = 0,
								int IP = 0,
								int pit_hit = 0,
								int pit_home = 0,
								int pit_BB = 0,
								int pit_IBB = 0,
								int pit_DBB = 0,
								int pit_SO = 0,
								int wild_pitch = 0,
								int balk = 0,
								int pit_run = 0,
								int ER = 0,
								double ERA = 0,
								double WHIP = 0,
								DateTime? update_date = null,
								int etc_cd1 = -1,
								int etc_cd2 = -1,
								int etc_cd3 = -1,
								int etc_cd4 = -1,
								int etc_cd5 = -1,
								string etc_str1 = "",
								string etc_str2 = "",
								string etc_str3 = "",
								string etc_str4 = "",
								string etc_str5 = "",
								DateTime? etc_date1 = null,
								DateTime? etc_date2 = null,
								DateTime? etc_date3 = null,
								DateTime? etc_date4 = null,
								DateTime? etc_date5 = null,
								string cmnt1 = "",
								string cmnt2 = "",
								string cmnt3 = "",
								int bat_id = -1,
								int hand_id = -1,
								int selected = -1
							)
		{
			if (player_id == 0)
			{
				Console.WriteLine("player_idが指定されていません");
				return;
			}

			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE player ");
				sqlST.AppendLine("	SET ");

				#region 2022.01.09 ~
				sqlST.AppendFormat("		player_id={0} , ", player_id).AppendLine();
				if (season != null)
				{
					sqlST.AppendFormat("		season='{0}'  , ", season).AppendLine();
				}
				if (player_name.Length != 0)
				{
					sqlST.AppendFormat("		player_name='{0}'  , ", player_name).AppendLine();
				}
				if (team_id != 0)
				{
					sqlST.AppendFormat("		team_id={0}  , ", team_id).AppendLine();
				}
				if (bat.Length != 0)
				{
					sqlST.AppendFormat("		bat='{0}'  , ", bat).AppendLine();
				}
				if (hand.Length != 0)
				{
					sqlST.AppendFormat("		hand='{0}' , ", hand).AppendLine();
				}
				if (position >= 0)
				{
					sqlST.AppendFormat("		position={0}  , ", position).AppendLine();
				}
				if (player_num >= 0)
				{
					sqlST.AppendFormat("		player_num={0}  , ", player_num).AppendLine();
				}
				/// 野手情報
				if (game != 0)
				{
					sqlST.AppendFormat("		game={0}  , ", game).AppendLine();
				}
				if (run != 0)
				{
					sqlST.AppendFormat("		run={0}  , ", run).AppendLine();
				}
				if (hit != 0)
				{
					sqlST.AppendFormat("		hit={0} , ", hit).AppendLine();
				}
				if (at_bat != 0)
				{
					sqlST.AppendFormat("		at_bat={0}  , ", at_bat).AppendLine();
				}
				if (plate != 0)
				{
					sqlST.AppendFormat("		plate={0}  , ", plate).AppendLine();
				}
				if (two_base != 0)
				{
					sqlST.AppendFormat("		two_base={0}  , ", two_base).AppendLine();
				}
				if (three_base != 0)
				{
					sqlST.AppendFormat("		three_base={0}  , ", three_base).AppendLine();
				}
				if (home_run != 0)
				{
					sqlST.AppendFormat("		home_run={0}    , ", home_run).AppendLine();
				}
				if (total_base != 0)
				{
					sqlST.AppendFormat("		total_base={0}    , ", total_base).AppendLine();
				}
				if (RBI != 0)
				{
					sqlST.AppendFormat("		RBI={0}    , ", RBI).AppendLine();
				}
				if (SB != 0)
				{
					sqlST.AppendFormat("		SB={0}    , ", SB).AppendLine();
				}
				if (not_SB != 0)
				{
					sqlST.AppendFormat("		not_SB={0}    , ", not_SB).AppendLine();
				}
				if (s_bunt != 0)
				{
					sqlST.AppendFormat("		s_bunt={0}    , ", s_bunt).AppendLine();
				}
				if (s_fly != 0)
				{
					sqlST.AppendFormat("		s_fly={0}     , ", s_fly).AppendLine();
				}
				if (BB != 0)
				{
					sqlST.AppendFormat("		BB={0}    , ", BB).AppendLine();
				}
				if (IBB != 0)
				{
					sqlST.AppendFormat("		IBB={0}    , ", IBB).AppendLine();
				}
				if (DBB != 0)
				{
					sqlST.AppendFormat("		DBB={0}    , ", DBB).AppendLine();
				}
				if (SO != 0)
				{
					sqlST.AppendFormat("		SO={0}    , ", SO).AppendLine();
				}
				if (DP != 0)
				{
					sqlST.AppendFormat("		DP={0}    , ", DP).AppendLine();
				}
				if (BA != 0)
				{
					sqlST.AppendFormat("		BA={0}   , ", BA).AppendLine();
				}
				if (SLG != 0)
				{
					sqlST.AppendFormat("		SLG={0}   , ", SLG).AppendLine();
				}
				if (OPS != 0)
				{
					sqlST.AppendFormat("		OPS={0}   , ", OPS).AppendLine();
				}
				if (OBP != 0)
				{
					sqlST.AppendFormat("		OBP={0}   , ", OBP).AppendLine();
				}
				/// 投手情報
				if (game_pitch != 0)
				{
					sqlST.AppendFormat("		game_pitch={0}    ,  ", game_pitch).AppendLine();
				}
				if (starter != 0)
				{
					sqlST.AppendFormat("		starter={0}    ,  ", starter).AppendLine();
				}
				if (CG != 0)
				{
					sqlST.AppendFormat("		CG={0}    ,  ", CG).AppendLine();
				}
				if (shut_out != 0)
				{
					sqlST.AppendFormat("		shut_out={0}    ,  ", shut_out).AppendLine();
				}
				if (win != 0)
				{
					sqlST.AppendFormat("		win={0}    ,  ", win).AppendLine();
				}
				if (lose != 0)
				{
					sqlST.AppendFormat("		lose={0}    , ", lose).AppendLine();
				}

				if (save != 0)
				{
					sqlST.AppendFormat("		save={0}    , ", save).AppendLine();
				}
				if (hold != 0)
				{
					sqlST.AppendFormat("		hold={0}    , ", hold).AppendLine();
				}
				if (WP != 0)
				{
					sqlST.AppendFormat("		WP={0}    , ", WP).AppendLine();
				}
				if (pit_bat != 0)
				{
					sqlST.AppendFormat("		pit_bat={0}    ,   ", pit_bat).AppendLine();
				}
				if (IP != 0)
				{
					sqlST.AppendFormat("		IP={0}    ,   ", IP).AppendLine();
				}
				if (pit_hit != 0)
				{
					sqlST.AppendFormat("		pit_hit={0}    ,   ", pit_hit).AppendLine();
				}
				if (pit_home != 0)
				{
					sqlST.AppendFormat("		pit_home={0}    ,  ", pit_home).AppendLine();
				}
				if (pit_BB != 0)
				{
					sqlST.AppendFormat("		pit_BB={0}     , ", pit_BB).AppendLine();
				}
				if (pit_IBB != 0)
				{
					sqlST.AppendFormat("		pit_IBB={0}    , ", pit_IBB).AppendLine();
				}
				if (pit_DBB != 0)
				{
					sqlST.AppendFormat("		pit_DBB={0}     , ", pit_DBB).AppendLine();
				}
				if (pit_SO != 0)
				{
					sqlST.AppendFormat("		pit_SO={0}     , ", pit_SO).AppendLine();
				}
				if (wild_pitch != 0)
				{
					sqlST.AppendFormat("		wild_pitch={0}      , ", wild_pitch).AppendLine();
				}
				if (balk != 0)
				{
					sqlST.AppendFormat("		balk={0}     , ", balk).AppendLine();
				}
				if (pit_run != 0)
				{
					sqlST.AppendFormat("		pit_run={0}      , ", pit_run).AppendLine();
				}
				if (ER >= 0)
				{
					sqlST.AppendFormat("		ER={0}      , ", ER).AppendLine();
				}
				if (ERA >= 0)
				{
					sqlST.AppendFormat("		ERA={0}   , ", ERA).AppendLine();
				}
				if (WHIP >= 0)
				{
					sqlST.AppendFormat("		WHIP={0}   , ", WHIP).AppendLine();
				}
				/// 予備情報

				if (etc_cd1 >= 0)
				{
					sqlST.AppendFormat("		etc_cd1={0}   , ", etc_cd1).AppendLine();
				}
				if (etc_cd2 >= 0)
				{
					sqlST.AppendFormat("		etc_cd2={0}   , ", etc_cd2).AppendLine();
				}
				if (etc_cd3 >= 0)
				{
					sqlST.AppendFormat("		etc_cd3={0}   , ", etc_cd3).AppendLine();
				}
				if (etc_cd4 >= 0)
				{
					sqlST.AppendFormat("		etc_cd4={0}   , ", etc_cd4).AppendLine();
				}
				if (etc_cd5 >= 0)
				{
					sqlST.AppendFormat("		etc_cd5={0}   , ", etc_cd5).AppendLine();
				}

				if (etc_str1.Length != 0)
				{
					sqlST.AppendFormat("		etc_str1='{0}'   , ", etc_str1).AppendLine();
				}
				else if (etc_str2.Length != 0 && etc_str2 == "0")
				{
					sqlST.AppendLine("		etc_str1=''   , ");
				}
				if (etc_str2.Length != 0)
				{
					sqlST.AppendFormat("		etc_str2='{0}'   , ", etc_str2).AppendLine();
				}
				if (etc_str3.Length != 0)
				{
					sqlST.AppendFormat("		etc_str3='{0}'   , ", etc_str3).AppendLine();
				}
				if (etc_str4.Length != 0)
				{
					sqlST.AppendFormat("		etc_str4='{0}'   , ", etc_str4).AppendLine();
				}
				if (etc_str5.Length != 0)
				{
					sqlST.AppendFormat("		etc_str5='{0}'   , ", etc_str5).AppendLine();
				}
				if (etc_date1 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date1).AppendLine();
				}
				if (etc_date2 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date2).AppendLine();
				}
				if (etc_date3 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date3).AppendLine();
				}
				if (etc_date4 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date4).AppendLine();
				}
				if (etc_date5 != null)
				{
					sqlST.AppendFormat("		'{0}'   , ", etc_date5).AppendLine();
				}
				if (cmnt1.Length != 0)
				{
					sqlST.AppendFormat("		cmnt1='{0}'   , ", cmnt1).AppendLine();
				}
				if (cmnt2.Length != 0)
				{
					sqlST.AppendFormat("		cmnt2='{0}'   , ", cmnt2).AppendLine();
				}
				if (cmnt3.Length != 0)
				{
					sqlST.AppendFormat("		cmnt3='{0}'   , ", cmnt3).AppendLine();
				}

				if (bat_id > -1)
				{
					sqlST.AppendFormat("		bat_id={0}   , ", bat_id).AppendLine();
				}

				if (hand_id > -1)
				{
					sqlST.AppendFormat("		hand_id={0}   , ", hand_id).AppendLine();
				}
				if (selected > -1)
				{
					sqlST.AppendFormat("		selected={0}   , ", selected).AppendLine();
				}


				///　更新日
				//sqlST.AppendFormat("		{0}    ", update_date).AppendLine();
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}
				else
				{
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", DateTime.Now);
				}

				#endregion 2022.01.09

				#region 旧コード
				///// 基本情報
				//#region 基本

				//sqlST.AppendLine("	player_id ");
				//sqlST.AppendLine("		=@player_id ");
				//CMD_Update.Parameters.AddWithValue("@player_id", player_id);

				//if (season != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		season   ");
				//	sqlST.AppendLine("		=@season   ");
				//	CMD_Update.Parameters.AddWithValue("@season", season);
				//}
				//if (player_name != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		player_name   ");
				//	sqlST.AppendLine("		=@player_name   ");
				//	CMD_Update.Parameters.AddWithValue("@player_name", player_name);
				//}
				//if (team_id != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		team_id   ");
				//	sqlST.AppendLine("		=@team_id   ");
				//	CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				//}
				//if (bat != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		bat=   ");
				//	sqlST.AppendLine("		@bat   ");
				//	CMD_Update.Parameters.AddWithValue("@bat", bat);
				//}
				//if (hand != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		hand=   ");
				//	sqlST.AppendLine("		@hand  ");
				//	CMD_Update.Parameters.AddWithValue("@hand", hand);
				//}
				//if (position != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		position=   ");
				//	sqlST.AppendLine("		@position   ");
				//	CMD_Update.Parameters.AddWithValue("@position", position);
				//}
				//if (player_num != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		player_num=   ");
				//	sqlST.AppendLine("		@player_num   ");
				//	CMD_Update.Parameters.AddWithValue("@player_num", player_num);
				//}
				//#endregion
				///// 野手情報
				//#region 野手
				//if (game != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		game=   ");
				//	sqlST.AppendLine("		@game   ");
				//	CMD_Update.Parameters.AddWithValue("@game", game);
				//}
				//if (run != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		run=   ");
				//	sqlST.AppendLine("		@run   ");
				//	CMD_Update.Parameters.AddWithValue("@run", run);
				//}
				//if (hit != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		hit=   ");
				//	sqlST.AppendLine("		@hit  ");
				//	CMD_Update.Parameters.AddWithValue("@hit", hit);
				//}
				//if (at_bat != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		at_bat=   ");
				//	sqlST.AppendLine("		@at_bat   ");
				//	CMD_Update.Parameters.AddWithValue("@at_bat", at_bat);
				//}
				//if (plate != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		plate=   ");
				//	sqlST.AppendLine("		@plate   ");
				//	CMD_Update.Parameters.AddWithValue("@plate", plate);
				//}
				//if (two_base != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		two_base=   ");
				//	sqlST.AppendLine("		@two_base   ");
				//	CMD_Update.Parameters.AddWithValue("@two_base", two_base);
				//}
				//if (three_base != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		three_base=   ");
				//	sqlST.AppendLine("		@three_base   ");
				//	CMD_Update.Parameters.AddWithValue("@three_base", three_base);
				//}
				//if (home_run != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		home_run=   ");
				//	sqlST.AppendLine("		@home_run     ");
				//	CMD_Update.Parameters.AddWithValue("@home_run", home_run);
				//}
				//if (total_base != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		total_base=   ");
				//	sqlST.AppendLine("		@total_base     ");
				//	CMD_Update.Parameters.AddWithValue("@total_base", total_base);
				//}
				//if (RBI != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		RBI=   ");
				//	sqlST.AppendLine("		@RBI     ");
				//	CMD_Update.Parameters.AddWithValue("@RBI", RBI);
				//}
				//if (SB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		SB=   ");
				//	sqlST.AppendLine("		@SB     ");
				//	CMD_Update.Parameters.AddWithValue("@SB", SB);
				//}
				//if (s_bunt != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		s_bunt=   ");
				//	sqlST.AppendLine("		@s_bunt     ");
				//	CMD_Update.Parameters.AddWithValue("@s_bunt", s_bunt);
				//}
				//if (s_fly != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		s_fly=   ");
				//	sqlST.AppendLine("		@s_fly      ");
				//	CMD_Update.Parameters.AddWithValue("@s_fly", s_fly);
				//}
				//if (BB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		BB=   ");
				//	sqlST.AppendLine("		@BB     ");
				//	CMD_Update.Parameters.AddWithValue("@BB", BB);
				//}
				//if (IBB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		IBB=   ");
				//	sqlST.AppendLine("		@IBB     ");
				//	CMD_Update.Parameters.AddWithValue("@IBB", IBB);
				//}
				//if (DBB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		DBB=   ");
				//	sqlST.AppendLine("		@DBB     ");
				//	CMD_Update.Parameters.AddWithValue("@DBB", DBB);
				//}
				//if (SO != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		SO=   ");
				//	sqlST.AppendLine("		@SO     ");
				//	CMD_Update.Parameters.AddWithValue("@SO", SO);
				//}
				//if (DP != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		DP=   ");
				//	sqlST.AppendLine("		@DP     ");
				//	CMD_Update.Parameters.AddWithValue("@DP", DP);
				//}
				//if (BA != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		BA=   ");
				//	sqlST.AppendLine("		@BA    ");
				//	CMD_Update.Parameters.AddWithValue("@BA", BA);
				//}
				//if (SLG != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		SLG=   ");
				//	sqlST.AppendLine("		@SLG    ");
				//	CMD_Update.Parameters.AddWithValue("@SLG", SLG);
				//}
				//if (OPS != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		OPS=   ");
				//	sqlST.AppendLine("		@OPS    ");
				//	CMD_Update.Parameters.AddWithValue("@OPS", OPS);
				//}
				//if (OBP != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		OBP=   ");
				//	sqlST.AppendLine("		@OBP    ");
				//	CMD_Update.Parameters.AddWithValue("@OBP", OBP);
				//}
				//#endregion

				///// 投手情報
				//#region 投手
				//if (game_pitch != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		game_pitch=   ");
				//	sqlST.AppendLine("		@game_pitch      ");
				//	CMD_Update.Parameters.AddWithValue("@game_pitch", game_pitch);
				//}
				//if (starter != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		starter=   ");
				//	sqlST.AppendLine("		@starter      ");
				//	CMD_Update.Parameters.AddWithValue("@starter", starter);
				//}
				//if (CG != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		CG=   ");
				//	sqlST.AppendLine("		@CG      ");
				//	CMD_Update.Parameters.AddWithValue("@CG", CG);
				//}
				//if (shut_out != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		shut_out=   ");
				//	sqlST.AppendLine("		@shut_out      ");
				//	CMD_Update.Parameters.AddWithValue("@shut_out", shut_out);
				//}
				//if (win != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		win=   ");
				//	sqlST.AppendLine("		@win      ");
				//	CMD_Update.Parameters.AddWithValue("@win", win);
				//}
				//if (lose != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		lose=   ");
				//	sqlST.AppendLine("		@lose     ");
				//	CMD_Update.Parameters.AddWithValue("@lose", lose);
				//}
				//if (save != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		save=   ");
				//	sqlST.AppendLine("		@save     ");
				//	CMD_Update.Parameters.AddWithValue("@save", save);
				//}
				//if (hold != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		hold=   ");
				//	sqlST.AppendLine("		@hold     ");
				//	CMD_Update.Parameters.AddWithValue("@hold", hold);
				//}
				//if (WP != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		WP=   ");
				//	sqlST.AppendLine("		@WP     ");
				//	CMD_Update.Parameters.AddWithValue("@WP", WP);
				//}
				//if (pit_bat != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_bat=   ");
				//	sqlST.AppendLine("		@pit_bat       ");
				//	CMD_Update.Parameters.AddWithValue("@pit_bat", pit_bat);
				//}
				//if (IP != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		IP=   ");
				//	sqlST.AppendLine("		@IP       ");
				//	CMD_Update.Parameters.AddWithValue("@IP", IP);
				//}

				//if (pit_hit != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_hit=   ");
				//	sqlST.AppendLine("		@pit_hit       ");
				//	CMD_Update.Parameters.AddWithValue("@pit_hit", pit_hit);
				//}
				//if (pit_home != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_home=   ");
				//	sqlST.AppendLine("		@pit_home      ");
				//	CMD_Update.Parameters.AddWithValue("@pit_home", pit_home);
				//}
				//if (pit_BB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_BB=   ");
				//	sqlST.AppendLine("		@pit_BB      ");
				//	CMD_Update.Parameters.AddWithValue("@pit_BB", pit_BB);
				//}
				//if (pit_IBB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_IBB=   ");
				//	sqlST.AppendLine("		@pit_IBB     ");
				//	CMD_Update.Parameters.AddWithValue("@pit_IBB", pit_IBB);
				//}
				//if (pit_DBB != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_DBB=   ");
				//	sqlST.AppendLine("		@pit_DBB      ");
				//	CMD_Update.Parameters.AddWithValue("@pit_DBB", pit_DBB);
				//}
				//if (pit_SO != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_SO=   ");
				//	sqlST.AppendLine("		@pit_SO      ");
				//	CMD_Update.Parameters.AddWithValue("@pit_SO", pit_SO);
				//}
				//if (wild_pitch != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		wild_pitch=   ");
				//	sqlST.AppendLine("		@wild_pitch       ");
				//	CMD_Update.Parameters.AddWithValue("@wild_pitch", wild_pitch);
				//}
				//if (balk != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		balk=   ");
				//	sqlST.AppendLine("		@balk      ");
				//	CMD_Update.Parameters.AddWithValue("@balk", balk);
				//}
				//if (pit_run != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		pit_run=   ");
				//	sqlST.AppendLine("		@pit_run       ");
				//	CMD_Update.Parameters.AddWithValue("@pit_run", pit_run);
				//}
				//if (ER != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		ER=   ");
				//	sqlST.AppendLine("		@ER       ");
				//	CMD_Update.Parameters.AddWithValue("@ER", ER);
				//}
				//if (ERA != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		ERA=   ");
				//	sqlST.AppendLine("		@ERA    ");
				//	CMD_Update.Parameters.AddWithValue("@ERA", ERA);
				//}
				//if (WHIP != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		WHIP=   ");
				//	sqlST.AppendLine("		@WHIP    ");
				//	CMD_Update.Parameters.AddWithValue("@WHIP", WHIP);
				//}
				//#endregion

				///// 予備情報
				//#region 予備
				//if (etc_cd1 != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_cd1=   ");
				//	sqlST.AppendLine("		@etc_cd1    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				//}
				//if (etc_cd2 != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_cd2=   ");
				//	sqlST.AppendLine("		@etc_cd2    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				//}
				//if (etc_cd3 != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_cd3=   ");
				//	sqlST.AppendLine("		@etc_cd3    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				//}
				//if (etc_cd4 != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_cd4=   ");
				//	sqlST.AppendLine("		@etc_cd4    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				//}
				//if (etc_cd5 != 0)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_cd5=   ");
				//	sqlST.AppendLine("		@etc_cd5    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				//}
				//if (etc_str1 != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_str1=   ");
				//	sqlST.AppendLine("		@etc_str1    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_str1", etc_str1);
				//}
				//if (etc_str2 != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_str2=   ");
				//	sqlST.AppendLine("		@etc_str2    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_str2", etc_str2);
				//}
				//if (etc_str3 != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_str3=   ");
				//	sqlST.AppendLine("		@etc_str3    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_str3", etc_str3);
				//}
				//if (etc_str4 != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_str4=   ");
				//	sqlST.AppendLine("		@etc_str4    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_str4", etc_str4);
				//}
				//if (etc_str5 != "")
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_str5=   ");
				//	sqlST.AppendLine("		@etc_str5    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_str5", etc_str5);
				//}
				//if (etc_date1 != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_date1=   ");
				//	sqlST.AppendLine("		@etc_date1    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date1", etc_date1);
				//}
				//if (etc_date2 != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_date2=   ");
				//	sqlST.AppendLine("		@etc_date2    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date2", etc_date2);
				//}
				//if (etc_date3 != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_date3=   ");
				//	sqlST.AppendLine("		@etc_date3    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date3", etc_date3);
				//}
				//if (etc_date4 != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_date4=   ");
				//	sqlST.AppendLine("		@etc_date4    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date4", etc_date4);
				//}
				//if (etc_date5 != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		etc_date5=   ");
				//	sqlST.AppendLine("		@etc_date5    ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date5", etc_date5);
				//}
				//#endregion
				/////　更新日
				//if (update_date != null)
				//{
				//	sqlST.AppendLine("		,	");
				//	sqlST.AppendLine("		update_date=   ");
				//	sqlST.AppendLine("		@update_date    ");
				//	CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				//}
				#endregion 旧コード


				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	player_id ");
				sqlST.AppendLine("		=@player_id ");
				CMD_Update.Parameters.AddWithValue("@player_id", player_id);
				if (team_id != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	team_id=@team_id ");
					CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				}



				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				//CMD_Update.Parameters.AddWithValue("@BA", BA);
				//CMD_Update.Parameters.AddWithValue("@OPS", OPS);
				//CMD_Update.Parameters.AddWithValue("@OBP", OBP);
				//CMD_Update.Parameters.AddWithValue("@ERA", ERA);
				//CMD_Update.Parameters.AddWithValue("@WHIP", WHIP);
				//CMD_Update.Parameters.AddWithValue("@bat", bat);
				//CMD_Update.Parameters.AddWithValue("@hand", hand);

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public static void SettingPlayerDataUpdate(int player_id = -1, string name = "", int player_num = -1, int hand_id = -1, int bat_id = -1, int team_id = -1, int selected = -1)
		{
			if (player_id < 0) { return; }
			bool queryFlg = false;
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("SET	");
				sqlST.AppendLine("		update_date=@update_date    ");
				CMD_Update.Parameters.AddWithValue("@update_date", DateTime.Now);
				if (name.Length != 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		player_name='{0}'    ", name).AppendLine();
					queryFlg = true;
				}
				if (hand_id >= 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		hand_id={0}    ", hand_id).AppendLine();
					queryFlg = true;
				}
				if (bat_id >= 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		bat_id={0}    ", bat_id).AppendLine();
					queryFlg = true;
				}
				if (team_id >= 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		team_id={0}    ", team_id).AppendLine();
					queryFlg = true;
				}
				if (selected >= 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		selected={0}    ", selected).AppendLine();
					queryFlg = true;
				}
				if (player_num >= 0)
				{
					sqlST.AppendLine("	,	");
					sqlST.AppendFormat("		player_num={0}    ", player_num).AppendLine();
					queryFlg = true;
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				if (queryFlg)
				{
					CMD_Update.CommandText = sqlST.ToString();
					CMD_Update.ExecuteReader();
				}
				con.Close();
			}
		}

		public static void BeforeGameStartReSetUpdate()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("SET	");
				sqlST.AppendLine("		etc_str2='R'    ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	etc_str2='RR' ");
				CMD_Update.CommandText = sqlST.ToString();
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void resetReserveFlgRecord(
						int team_id = 0,
						DateTime? update_date = null
					)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE player ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendLine("		etc_str1=''   , ");
				sqlST.AppendLine("		etc_str2='0'   , ");
				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}
				else
				{
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", DateTime.Now);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	team_id=@team_id ");
				CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				sqlST.AppendLine("		AND    ");
				sqlST.AppendLine("		(etc_str1='-1'    ");
				sqlST.AppendLine("		OR    ");
				sqlST.AppendLine("		etc_str2='-1')    ");


				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void updateCmntRecord(
								int team_id = 0,
								int player_id = 0,
								string cmnt1 = "",
								string cmnt2 = "",
								string cmnt3 = "",
								DateTime? update_date = null
								)
		{
			if (cmnt1.Length == 0) { return; }
			//if (cmnt2.Length == 0) { return; }
			//if (cmnt3.Length == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE player ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		cmnt1='{0}'    ", cmnt1).AppendLine();
				if (cmnt2.Length != 0)
				{
					sqlST.AppendLine("		, ");
					sqlST.AppendFormat("		cmnt2='{0}'    ", cmnt2).AppendLine();
				}
				if (cmnt3.Length != 0)
				{
					sqlST.AppendLine("		, ");
					sqlST.AppendFormat("		cmnt3='{0}'    ", cmnt3).AppendLine();
				}
				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		, ");
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}
				else
				{
					sqlST.AppendLine("		, ");
					sqlST.AppendLine("		update_date=@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", DateTime.Now);
				}
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
				sqlST.AppendLine("		AND    ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();



				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}





		public static void deleteRecord(int player_id = 0)
		{
			if (player_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	player_id=@player_id ");
				CmdDelete.Parameters.AddWithValue("@player_id", player_id);
				CmdDelete.CommandText = sqlST.ToString();


				#endregion
				CmdDelete.ExecuteReader();
				con.Close();
			}
		}

		public static bool SearchExistingOrderId(
								int team_id = 0,
								string order_id = ""
			)
		{
			if (team_id == 0) { return false; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT	");
				sqlST.AppendLine("	etc_str2	");
				sqlST.AppendLine("FROM	 ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	team_id=@team_id ");
				CMD.Parameters.AddWithValue("@team_id", team_id);
				if (order_id.Length != 0)       /// 打順
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	etc_str2=@order_id ");
					CMD.Parameters.AddWithValue("@order_id", order_id);
				}
				CMD.CommandText = sqlST.ToString();
				SqliteDataReader reader = CMD.ExecuteReader();
				int count = 0;
				while (reader.Read())
				{
					//countList.Add(new playerIdGetCount(reader.GetInt32(0)));
					count++;
				}

				#endregion

				con.Close();
				if (count > 0)
				{
					return false;
				}
			}
			return true;
		}

		public static bool SearchExistingPosition(
								int team_id = 0,
								string positionST = ""
			)
		{
			if (team_id == 0) { return false; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT	");
				sqlST.AppendLine("	etc_str1	");
				sqlST.AppendLine("FROM	 ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	team_id=@team_id ");
				CMD.Parameters.AddWithValue("@team_id", team_id);
				if (positionST.Length != 0)       /// 打順
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	etc_str1=@positionST ");
					CMD.Parameters.AddWithValue("@positionST", positionST);
				}
				CMD.CommandText = sqlST.ToString();
				SqliteDataReader reader = CMD.ExecuteReader();
				int count = 0;
				while (reader.Read())
				{
					//countList.Add(new playerIdGetCount(reader.GetInt32(0)));
					count++;
				}

				#endregion

				con.Close();
				if (count > 0)
				{
					return false;
				}
			}
			return true;
		}


		public class playerIdGetCount
		{
			public int player_id { get; set; }
			public playerIdGetCount(int count = 0)
			{
				this.player_id = count;
			}
		}

		/// <summary>
		/// player_id生成用
		/// </summary>
		/// <returns></returns>
		public static List<playerIdGetCount> GetPlayerIdRecordsCount()
		{
			List<playerIdGetCount> countList = new List<playerIdGetCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(player_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(player_id) + 1 ");
				sqlST.AppendLine("	END AS player_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerIdGetCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}


		public class playerData
		{

			#region チーム変数
			public DateTime? season { get; set; }        // 年度 ex)2021
			public int player_id { get; set; }          // 選手識別番号
			public string name { get; set; }            // 選手名 player_name
			public int team_id { get; set; }            // チーム識別番号
			public string bat { get; set; }             // 打席識別番号(0:右, 1:左, 2;両打)
			public string hand { get; set; }            // 利き手識別番号(0:右, 1:左)
			public int position { get; set; }           // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }         // 背番号

			public int game { get; set; }               // 試合数
			public int run { get; set; }                // 得点数
			public int hit { get; set; }                // 安打数
			public int at_bat { get; set; }             // 打数
			public int plate { get; set; }              // 打席数
			public int two_base { get; set; }           // 二塁打数
			public int three_base { get; set; }         // 三塁打数
			public int home_run { get; set; }           // 本塁打数
			public int total_base { get; set; }         // 塁打数
			public int RBI { get; set; }                // 打点数
			public int SB { get; set; }                 // 盗塁数
			public int not_SB { get; set; }             // 盗塁死数
			public int s_bunt { get; set; }             // 犠牲バント
			public int s_fly { get; set; }              // 犠牲フライ
			public int BB { get; set; }                 // 四球
			public int IBB { get; set; }                // 敬遠
			public int DBB { get; set; }                // 死球
			public int SO { get; set; }                 // 三振
			public int DP { get; set; }                 // 併殺打


			public double BA { get; set; }              // 打率
			public double SLG { get; set; }             // 長打率(塁打数 / 打数)
			public double OPS { get; set; }             // OPS(長打率 + 出塁率)
			public double OBP { get; set; }             // 出塁率(出塁数 / 打席)




			public DateTime? update_date { get; set; }      // 更新日

			public int game_pitch { get; set; }             // 登板数
			public int starter { get; set; }                // 先発数
			public int CG { get; set; }                     // 完投数
			public int shut_out { get; set; }               // 完封数
			public int win { get; set; }                    // 勝利数
			public int lose { get; set; }                   // 敗戦数
			public int save { get; set; }                   // セーブ数
			public int hold { get; set; }                   // ホールド数
			public double WP { get; set; }                  // 勝率
			public int pit_bat { get; set; }                // 対戦打者数
			public int IP { get; set; }                     // 投球数
			public int pit_hit { get; set; }                // 被安打数
			public int pit_home { get; set; }               // 被本塁打数
			public int pit_BB { get; set; }                 // 与四球数
			public int pit_IBB { get; set; }                // 与敬遠数
			public int pit_DBB { get; set; }                // 与死球数
			public int pit_SO { get; set; }                 // 奪三振数
			public int wild_pitch { get; set; }             // 暴投数
			public int balk { get; set; }                   // ボーク数
			public int pit_run { get; set; }                // 失点数
			public int ER { get; set; }                     // 自責点数

			public double ERA { get; set; }                 // 防御率
			public double WHIP { get; set; }                // WHIP( (与四球 + 被安打) / 投球回 )



			public int etc_cd1 { get; set; }                // 予備
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }             // 予備 2022.01.06 守備位置
			public string etc_str2 { get; set; }             // 予備 2022.01.07 打順
			public string etc_str3 { get; set; }             // 予備
			public string etc_str4 { get; set; }             // 予備
			public string etc_str5 { get; set; }             // 予備
			public DateTime? etc_date1 { get; set; }             // 予備
			public DateTime? etc_date2 { get; set; }             // 予備
			public DateTime? etc_date3 { get; set; }             // 予備
			public DateTime? etc_date4 { get; set; }             // 予備
			public DateTime? etc_date5 { get; set; }             // 予備
			public int cat_id { get; set; }                     // 捕手識別番号
			public int ump_id { get; set; }                     // 審判識別番号

			public string cmnt1 { get; set; }
			public string cmnt2 { get; set; }
			public string cmnt3 { get; set; }

			public int bat_id { get; set; }                     // 打席識別番号
			public int hand_id { get; set; }                    // 利き腕識別番号
			public int selected { get; set; }                    // 登録対象選手　0:対象 / 1:対象外

			#endregion


			public playerData(
				int player_id = 0,
				DateTime? season = null,
				string name = "",       // player_name
				string bat = "",
				string hand = "",
				int position = 0,
				int player_num = 0,
				int team_id = 0,
				int game = 0,
				int run = 0,
				int hit = 0,
				int at_bat = 0,
				int plate = 0,
				int two_base = 0,
				int three_base = 0,
				int home_run = 0,
				int total_base = 0,
				int RBI = 0,
				int SB = 0,
				int not_SB = 0,
				int s_bunt = 0,
				int s_fly = 0,
				int BB = 0,
				int IBB = 0,
				int DBB = 0,
				int SO = 0,
				int DP = 0,
				double BA = 0.00,
				double SLG = 0.00,
				double OPS = 0.00,
				double OBP = 0.00,
				int game_pitch = 0,
				int starter = 0,
				int CG = 0,
				int shut_out = 0,
				int win = 0,
				int lose = 0,
				int save = 0,
				int hold = 0,
				double WP = 0.00,
				int pit_bat = 0,
				int IP = 0,
				int pit_hit = 0,
				int pit_home = 0,
				int pit_BB = 0,
				int pit_IBB = 0,
				int pit_DBB = 0,
				int pit_SO = 0,
				int wild_pitch = 0,
				int balk = 0,
				int pit_run = 0,
				int ER = 0,
				double ERA = 0,
				double WHIP = 0,
				int etc_cd1 = 0,
				int etc_cd2 = 0,
				int etc_cd3 = 0,
				int etc_cd4 = 0,
				int etc_cd5 = 0,
				string etc_str1 = "",
				string etc_str2 = "",
				string etc_str3 = "",
				string etc_str4 = "",
				string etc_str5 = "",
				DateTime? etc_date1 = null,
				DateTime? etc_date2 = null,
				DateTime? etc_date3 = null,
				DateTime? etc_date4 = null,
				DateTime? etc_date5 = null,
				int cat_id = 0,
				int ump_id = 0,
				string cmnt1 = "",
				string cmnt2 = "",
				string cmnt3 = "",
				int bat_id = 0,
				int hand_id = 0,
				int selected = 0,
				DateTime? update_date = null
				)
			{

				this.player_id = player_id;
				this.season = season;
				this.name = name;
				this.bat = bat;
				this.hand = hand;
				this.position = position;
				this.player_num = player_num;
				this.team_id = team_id;
				this.game = game;
				this.run = run;
				this.hit = hit;
				this.at_bat = at_bat;
				this.plate = plate;
				this.two_base = two_base;
				this.three_base = three_base;
				this.home_run = home_run;
				this.total_base = total_base;
				this.RBI = RBI;
				this.SB = SB;
				this.not_SB = not_SB;
				this.s_bunt = s_bunt;
				this.s_fly = s_fly;
				this.BB = BB;
				this.IBB = IBB;
				this.DBB = DBB;
				this.SO = SO;
				this.DP = DP;
				this.BA = BA;
				this.SLG = SLG;
				this.OPS = OPS;
				this.OBP = OBP;

				this.game_pitch = game_pitch;
				this.starter = starter;
				this.CG = CG;
				this.shut_out = shut_out;
				this.win = win;
				this.lose = lose;
				this.save = save;
				this.hold = hold;
				this.WP = WP;
				this.pit_bat = pit_bat;
				this.IP = IP;
				this.pit_hit = pit_hit;
				this.pit_home = pit_home;
				this.pit_BB = pit_BB;
				this.pit_IBB = pit_IBB;
				this.pit_DBB = pit_DBB;
				this.pit_SO = pit_SO;
				this.wild_pitch = wild_pitch;
				this.balk = balk;
				this.pit_run = pit_run;
				this.ER = ER;
				this.ERA = ERA;
				this.WHIP = WHIP;


				this.etc_cd1 = etc_cd1;
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;

				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;

				this.etc_date1 = etc_date1;
				this.etc_date2 = etc_date2;
				this.etc_date3 = etc_date3;
				this.etc_date4 = etc_date4;
				this.etc_date5 = etc_date5;

				this.cmnt1 = cmnt1;
				this.cmnt2 = cmnt2;
				this.cmnt3 = cmnt3;

				this.bat_id = bat_id;
				this.hand_id = hand_id;
				this.selected = selected;
				this.update_date = update_date;
			}

		}
		public static List<playerData> GetRecords(
											int team_id = 0,
											string etc_str2 = "",
											string searchText = "",
											int seachNum = -1,
											int position = -1,
											int selected = -1)
		{
			List<playerData> orderList = new List<playerData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	* ");
				sqlST.AppendLine("		player_id , ");
				//sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter    ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win    ,  ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat     ,  ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		pit_hit    ,   ");
				sqlST.AppendLine("		pit_home   ,   ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				/// ver.1.0.9.0以降
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");
				/// ver.3.0.0.0　以降
				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				if (team_id != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
					if (etc_str2.Length != 0)
					{
						if (etc_str2 == "0")
						{
							sqlST.AppendLine("		AND ");
							sqlST.AppendLine("	etc_str2='0' ");
						}
						else
						{
							sqlST.AppendLine("		AND ");
							sqlST.AppendLine("	etc_str2<>'0' ");
							sqlST.AppendLine("		AND ");
							sqlST.AppendLine("	etc_str2<>'-1' ");
						}
					}
					else
					{
						sqlST.AppendLine("		AND ");
						sqlST.AppendLine("	etc_str2<>'0' ");
						sqlST.AppendLine("		AND ");
						sqlST.AppendLine("	etc_str2<>'-1' ");
					}
				}
				if (searchText.Length != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	player_name LIKE '%{0}%' ", searchText).AppendLine();
				}

				if (seachNum >= 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	player_num ={0} ", seachNum).AppendLine();
				}
				if (position > -1)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	position ={0} ", position).AppendLine();
				}

				if (selected > -1)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	selected ={0} ", selected).AppendLine();
				}

				sqlST.AppendLine("ORDER BY ");
				sqlST.Append("	etc_str2 ");
				if (etc_str2 == "0")
				{
					sqlST.AppendLine("	, ");
					sqlST.AppendLine("	player_num ");

				}

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new playerData(
							player_id: reader.GetInt32(0),
							//season: reader.GetDateTime(1),
							name: reader.GetString(1),
							bat: reader.GetString(2),
							hand: reader.GetString(3),
							position: reader.GetInt32(4),
							player_num: reader.GetInt32(5),
							team_id: reader.GetInt32(6),
							game: reader.GetInt32(7),
							run: reader.GetInt32(8),
							hit: reader.GetInt32(9),
							at_bat: reader.GetInt32(10),
							plate: reader.GetInt32(11),
							two_base: reader.GetInt32(12),
							three_base: reader.GetInt32(13),
							home_run: reader.GetInt32(14),
							total_base: reader.GetInt32(15),
							RBI: reader.GetInt32(16),
							SB: reader.GetInt32(17),
							not_SB: reader.GetInt32(18),
							s_bunt: reader.GetInt32(19),
							s_fly: reader.GetInt32(20),
							BB: reader.GetInt32(21),
							IBB: reader.GetInt32(22),
							DBB: reader.GetInt32(23),
							SO: reader.GetInt32(24),
							DP: reader.GetInt32(25),
							BA: reader.GetDouble(26),
							SLG: reader.GetDouble(27),
							OPS: reader.GetDouble(28),
							OBP: reader.GetDouble(29),
							game_pitch: reader.GetInt32(30),
							starter: reader.GetInt32(31),
							CG: reader.GetInt32(32),
							shut_out: reader.GetInt32(33),
							win: reader.GetInt32(34),
							lose: reader.GetInt32(35),
							save: reader.GetInt32(36),
							hold: reader.GetInt32(37),
							WP: reader.GetDouble(38),
							pit_bat: reader.GetInt32(39),
							IP: reader.GetInt32(40),
							pit_hit: reader.GetInt32(41),
							pit_home: reader.GetInt32(42),
							pit_BB: reader.GetInt32(43),
							pit_IBB: reader.GetInt32(44),
							pit_DBB: reader.GetInt32(45),
							pit_SO: reader.GetInt32(46),
							wild_pitch: reader.GetInt32(47),
							balk: reader.GetInt32(48),
							pit_run: reader.GetInt32(49),
							ER: reader.GetInt32(50),
							ERA: reader.GetInt32(51),
							WHIP: reader.GetDouble(52),
							etc_cd1: reader.GetInt32(53),
							etc_cd2: reader.GetInt32(54),
							etc_cd3: reader.GetInt32(55),
							etc_cd4: reader.GetInt32(56),
							etc_cd5: reader.GetInt32(57),
							etc_str1: reader.GetString(58),
							etc_str2: reader.GetString(59),
							etc_str3: reader.GetString(60),
							etc_str4: reader.GetString(61),
							etc_str5: reader.GetString(62),
							cmnt1: reader.GetString(63),
							cmnt2: reader.GetString(64),
							cmnt3: reader.GetString(65),
							bat_id: reader.GetInt32(66),
							hand_id: reader.GetInt32(67),
							selected: reader.GetInt32(68),
							update_date: reader.GetDateTime(69)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<playerData> GetRecordsPitchers(
											int team_id = 0)
		{
			List<playerData> orderList = new List<playerData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	* ");
				sqlST.AppendLine("		player_id , ");
				//sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter    ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win    ,  ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat     ,  ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		pit_hit    ,   ");
				sqlST.AppendLine("		pit_home   ,   ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				/// ver.1.0.9.0以降
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");
				/// ver.3.0.0.0　以降
				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		player_id ");
				sqlST.AppendLine("		IN ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("			SELECT ");
				sqlST.AppendLine("				pitcher_id ");
				sqlST.AppendLine("			FROM ");
				sqlST.AppendLine("				ball ");
				sqlST.AppendLine("		) ");
				if (team_id != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
				}


				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new playerData(
							player_id: reader.GetInt32(0),
							//season: reader.GetDateTime(1),
							name: reader.GetString(1),
							bat: reader.GetString(2),
							hand: reader.GetString(3),
							position: reader.GetInt32(4),
							player_num: reader.GetInt32(5),
							team_id: reader.GetInt32(6),
							game: reader.GetInt32(7),
							run: reader.GetInt32(8),
							hit: reader.GetInt32(9),
							at_bat: reader.GetInt32(10),
							plate: reader.GetInt32(11),
							two_base: reader.GetInt32(12),
							three_base: reader.GetInt32(13),
							home_run: reader.GetInt32(14),
							total_base: reader.GetInt32(15),
							RBI: reader.GetInt32(16),
							SB: reader.GetInt32(17),
							not_SB: reader.GetInt32(18),
							s_bunt: reader.GetInt32(19),
							s_fly: reader.GetInt32(20),
							BB: reader.GetInt32(21),
							IBB: reader.GetInt32(22),
							DBB: reader.GetInt32(23),
							SO: reader.GetInt32(24),
							DP: reader.GetInt32(25),
							BA: reader.GetDouble(26),
							SLG: reader.GetDouble(27),
							OPS: reader.GetDouble(28),
							OBP: reader.GetDouble(29),
							game_pitch: reader.GetInt32(30),
							starter: reader.GetInt32(31),
							CG: reader.GetInt32(32),
							shut_out: reader.GetInt32(33),
							win: reader.GetInt32(34),
							lose: reader.GetInt32(35),
							save: reader.GetInt32(36),
							hold: reader.GetInt32(37),
							WP: reader.GetDouble(38),
							pit_bat: reader.GetInt32(39),
							IP: reader.GetInt32(40),
							pit_hit: reader.GetInt32(41),
							pit_home: reader.GetInt32(42),
							pit_BB: reader.GetInt32(43),
							pit_IBB: reader.GetInt32(44),
							pit_DBB: reader.GetInt32(45),
							pit_SO: reader.GetInt32(46),
							wild_pitch: reader.GetInt32(47),
							balk: reader.GetInt32(48),
							pit_run: reader.GetInt32(49),
							ER: reader.GetInt32(50),
							ERA: reader.GetInt32(51),
							WHIP: reader.GetDouble(52),
							etc_cd1: reader.GetInt32(53),
							etc_cd2: reader.GetInt32(54),
							etc_cd3: reader.GetInt32(55),
							etc_cd4: reader.GetInt32(56),
							etc_cd5: reader.GetInt32(57),
							etc_str1: reader.GetString(58),
							etc_str2: reader.GetString(59),
							etc_str3: reader.GetString(60),
							etc_str4: reader.GetString(61),
							etc_str5: reader.GetString(62),
							cmnt1: reader.GetString(63),
							cmnt2: reader.GetString(64),
							cmnt3: reader.GetString(65),
							bat_id: reader.GetInt32(66),
							hand_id: reader.GetInt32(67),
							selected: reader.GetInt32(68),
							update_date: reader.GetDateTime(69)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<playerData> GetRecordsAllMember(int team_id = 0, int selected = -1)
		{
			List<playerData> orderList = new List<playerData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	* ");
				sqlST.AppendLine("		player_id , ");
				//sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter    ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win    ,  ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat     ,  ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		pit_hit    ,   ");
				sqlST.AppendLine("		pit_home   ,   ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");

				/// ver.1.0.9.0以降
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");
				/// ver.3.0.0.0　以降
				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();

				if (selected >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	selected={0} ", selected).AppendLine();
				}

				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	etc_str2 ");

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new playerData(
							player_id: reader.GetInt32(0),
							//season: reader.GetDateTime(1),
							name: reader.GetString(1),
							bat: reader.GetString(2),
							hand: reader.GetString(3),
							position: reader.GetInt32(4),
							player_num: reader.GetInt32(5),
							team_id: reader.GetInt32(6),
							game: reader.GetInt32(7),
							run: reader.GetInt32(8),
							hit: reader.GetInt32(9),
							at_bat: reader.GetInt32(10),
							plate: reader.GetInt32(11),
							two_base: reader.GetInt32(12),
							three_base: reader.GetInt32(13),
							home_run: reader.GetInt32(14),
							total_base: reader.GetInt32(15),
							RBI: reader.GetInt32(16),
							SB: reader.GetInt32(17),
							not_SB: reader.GetInt32(18),
							s_bunt: reader.GetInt32(19),
							s_fly: reader.GetInt32(20),
							BB: reader.GetInt32(21),
							IBB: reader.GetInt32(22),
							DBB: reader.GetInt32(23),
							SO: reader.GetInt32(24),
							DP: reader.GetInt32(25),
							BA: reader.GetDouble(26),
							SLG: reader.GetDouble(27),
							OPS: reader.GetDouble(28),
							OBP: reader.GetDouble(29),
							game_pitch: reader.GetInt32(30),
							starter: reader.GetInt32(31),
							CG: reader.GetInt32(32),
							shut_out: reader.GetInt32(33),
							win: reader.GetInt32(34),
							lose: reader.GetInt32(35),
							save: reader.GetInt32(36),
							hold: reader.GetInt32(37),
							WP: reader.GetDouble(38),
							pit_bat: reader.GetInt32(39),
							IP: reader.GetInt32(40),
							pit_hit: reader.GetInt32(41),
							pit_home: reader.GetInt32(42),
							pit_BB: reader.GetInt32(43),
							pit_IBB: reader.GetInt32(44),
							pit_DBB: reader.GetInt32(45),
							pit_SO: reader.GetInt32(46),
							wild_pitch: reader.GetInt32(47),
							balk: reader.GetInt32(48),
							pit_run: reader.GetInt32(49),
							ER: reader.GetInt32(50),
							ERA: reader.GetInt32(51),
							WHIP: reader.GetDouble(52),
							etc_cd1: reader.GetInt32(53),
							etc_cd2: reader.GetInt32(54),
							etc_cd3: reader.GetInt32(55),
							etc_cd4: reader.GetInt32(56),
							etc_cd5: reader.GetInt32(57),
							etc_str1: reader.GetString(58),
							etc_str2: reader.GetString(59),
							etc_str3: reader.GetString(60),
							etc_str4: reader.GetString(61),
							etc_str5: reader.GetString(62),
							cmnt1: reader.GetString(63),
							cmnt2: reader.GetString(64),
							cmnt3: reader.GetString(65),
							bat_id: reader.GetInt32(66),
							hand_id: reader.GetInt32(67),
							selected: reader.GetInt32(68),
							update_date: reader.GetDateTime(69)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}




		public static List<playerData> GetRecordsWhereEtc_str1(
											int team_id = 0,
											string etc_str1 = "")
		{
			List<playerData> orderList = new List<playerData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	* ");
				sqlST.AppendLine("		player_id , ");
				//sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		bat  , ");
				sqlST.AppendLine("		hand , ");
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 野手情報
				sqlST.AppendLine("		game  , ");
				sqlST.AppendLine("		run  , ");
				sqlST.AppendLine("		hit , ");
				sqlST.AppendLine("		at_bat  , ");
				sqlST.AppendLine("		plate  , ");
				sqlST.AppendLine("		two_base  , ");
				sqlST.AppendLine("		three_base  , ");
				sqlST.AppendLine("		home_run    , ");
				sqlST.AppendLine("		total_base    , ");
				sqlST.AppendLine("		RBI    , ");
				sqlST.AppendLine("		SB    , ");
				sqlST.AppendLine("		not_SB    , ");
				sqlST.AppendLine("		s_bunt    , ");
				sqlST.AppendLine("		s_fly     , ");
				sqlST.AppendLine("		BB   ,  ");
				sqlST.AppendLine("		IBB  ,   ");
				sqlST.AppendLine("		DBB  ,   ");
				sqlST.AppendLine("		SO   ,  ");
				sqlST.AppendLine("		DP   ,  ");
				sqlST.AppendLine("		BA   , ");
				sqlST.AppendLine("		SLG   , ");
				sqlST.AppendLine("		OPS   , ");
				sqlST.AppendLine("		OBP   , ");
				/// 投手情報
				sqlST.AppendLine("		game_pitch  ,    ");
				sqlST.AppendLine("		starter    ,  ");
				sqlST.AppendLine("		CG     , ");
				sqlST.AppendLine("		shut_out    ,  ");
				sqlST.AppendLine("		win    ,  ");
				sqlST.AppendLine("		lose    , ");
				sqlST.AppendLine("		save    , ");
				sqlST.AppendLine("		hold    , ");
				sqlST.AppendLine("		WP    , ");
				sqlST.AppendLine("		pit_bat     ,  ");
				sqlST.AppendLine("		IP     ,  ");
				sqlST.AppendLine("		pit_hit    ,   ");
				sqlST.AppendLine("		pit_home   ,   ");
				sqlST.AppendLine("		pit_BB     , ");
				sqlST.AppendLine("		pit_IBB    , ");
				sqlST.AppendLine("		pit_DBB     , ");
				sqlST.AppendLine("		pit_SO     , ");
				sqlST.AppendLine("		wild_pitch      , ");
				sqlST.AppendLine("		balk     , ");
				sqlST.AppendLine("		pit_run      , ");
				sqlST.AppendLine("		ER      , ");
				sqlST.AppendLine("		ERA   , ");
				sqlST.AppendLine("		WHIP   , ");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");

				/// ver.1.0.9.0以降
				sqlST.AppendLine("		cmnt1   , ");
				sqlST.AppendLine("		cmnt2   , ");
				sqlST.AppendLine("		cmnt3   , ");
				/// ver.2.0.0.0　以降
				sqlST.AppendLine("		bat_id   , ");
				sqlST.AppendLine("		hand_id   , ");
				sqlST.AppendLine("		selected   , ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();

				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("	etc_str1='{0}' ", etc_str1).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("	etc_str2<>'0' ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("	etc_str2<>'-1' ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new playerData(
							player_id: reader.GetInt32(0),
							//season: reader.GetDateTime(1),
							name: reader.GetString(1),
							bat: reader.GetString(2),
							hand: reader.GetString(3),
							position: reader.GetInt32(4),
							player_num: reader.GetInt32(5),
							team_id: reader.GetInt32(6),
							game: reader.GetInt32(7),
							run: reader.GetInt32(8),
							hit: reader.GetInt32(9),
							at_bat: reader.GetInt32(10),
							plate: reader.GetInt32(11),
							two_base: reader.GetInt32(12),
							three_base: reader.GetInt32(13),
							home_run: reader.GetInt32(14),
							total_base: reader.GetInt32(15),
							RBI: reader.GetInt32(16),
							SB: reader.GetInt32(17),
							not_SB: reader.GetInt32(18),
							s_bunt: reader.GetInt32(19),
							s_fly: reader.GetInt32(20),
							BB: reader.GetInt32(21),
							IBB: reader.GetInt32(22),
							DBB: reader.GetInt32(23),
							SO: reader.GetInt32(24),
							DP: reader.GetInt32(25),
							BA: reader.GetDouble(26),
							SLG: reader.GetDouble(27),
							OPS: reader.GetDouble(28),
							OBP: reader.GetDouble(29),
							game_pitch: reader.GetInt32(30),
							starter: reader.GetInt32(31),
							CG: reader.GetInt32(32),
							shut_out: reader.GetInt32(33),
							win: reader.GetInt32(34),
							lose: reader.GetInt32(35),
							save: reader.GetInt32(36),
							hold: reader.GetInt32(37),
							WP: reader.GetDouble(38),
							pit_bat: reader.GetInt32(39),
							IP: reader.GetInt32(40),
							pit_hit: reader.GetInt32(41),
							pit_home: reader.GetInt32(42),
							pit_BB: reader.GetInt32(43),
							pit_IBB: reader.GetInt32(44),
							pit_DBB: reader.GetInt32(45),
							pit_SO: reader.GetInt32(46),
							wild_pitch: reader.GetInt32(47),
							balk: reader.GetInt32(48),
							pit_run: reader.GetInt32(49),
							ER: reader.GetInt32(50),
							ERA: reader.GetInt32(51),
							WHIP: reader.GetDouble(52),
							etc_cd1: reader.GetInt32(53),
							etc_cd2: reader.GetInt32(54),
							etc_cd3: reader.GetInt32(55),
							etc_cd4: reader.GetInt32(56),
							etc_cd5: reader.GetInt32(57),
							etc_str1: reader.GetString(58),
							etc_str2: reader.GetString(59),
							etc_str3: reader.GetString(60),
							etc_str4: reader.GetString(61),
							etc_str5: reader.GetString(62),
							cmnt1: reader.GetString(63),
							cmnt2: reader.GetString(64),
							cmnt3: reader.GetString(65),
							bat_id: reader.GetInt32(66),
							hand_id: reader.GetInt32(67),
							selected: reader.GetInt32(68),
							update_date: reader.GetDateTime(69)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class playerOrder
		{
			public int player_id { get; set; }
			public string name { get; set; }
			public int position { get; set; }
			public string positionST { get; set; }  // 守備位置の文字列
			public string bat { get; set; }
			public string hand { get; set; }
			public int bat_id { get; set; }
			public int hand_id { get; set; }
			public playerOrder(
					int player_id = 0,
					string name = "",
					int position = 0,
					string bat = "",
					string hand = "",
					string positionST = "",
					int bat_id = 0,
					int hand_id = 0)
			{
				this.player_id = player_id;
				this.name = name;
				this.position = position;
				this.bat = bat;
				this.hand = hand;
				this.positionST = positionST;
				this.bat_id = bat_id;
				this.hand_id = hand_id;


			}
		}
		public static List<playerOrder> GetRecordsCount(
													int team_id = 0,
													int position = 0,
													string positionST = "",
													int selected = -1
			)
		{
			List<playerOrder> countList = new List<playerOrder>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	player_id, ");
				sqlST.AppendLine("	player_name, ");
				sqlST.AppendLine("	position, ");
				sqlST.AppendLine("	bat, ");
				sqlST.AppendLine("	hand, ");
				sqlST.AppendLine("	etc_str1 ");        // 守備位置の文字列
				sqlST.AppendLine("	bat_id, ");
				sqlST.AppendLine("	hand_id, ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	team_id={0} ", team_id).AppendLine();
				if (position > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	position={0} ", position).AppendLine();
				}
				if (positionST.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	etc_str1={0} ", positionST).AppendLine();
				}
				if (selected > -1)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	selected={0} ", selected).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(
						new playerOrder(
							player_id: reader.GetInt32(0),
							name: reader.GetString(1),
							position: reader.GetInt32(2),
							bat: reader.GetString(3),
							hand: reader.GetString(4),
							positionST: reader.GetString(5),
							bat_id: reader.GetInt32(6),
							hand_id: reader.GetInt32(7)
							)
						);
				}
			}
			return countList;
		}


		public class playerDataCount
		{
			public int player_count { get; set; }
			public playerDataCount(int count = 0)
			{
				this.player_count = count;

			}
		}
		public static List<playerDataCount> GetRecordsCount()
		{
			List<playerDataCount> countList = new List<playerDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(player_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	playorder ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new playerDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



	}
	#endregion

	#region 打席成績
	class BoxData
	{

		public BoxData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	box ( ");
				/// 基本情報
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_id , ");
				//sqlST.AppendLine("		player_name  , ");
				sqlST.AppendLine("		team_id  , ");
				/// 5
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		ball_box_num   , ");
				/// 10
				sqlST.AppendLine("		ball_total_num   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		game_box_num    , ");
				/// 15
				sqlST.AppendLine("		park_id    , ");
				sqlST.AppendLine("		bat_id     , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				sqlST.AppendLine("		weather_id    , ");
				/// 20
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				/// 25
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				/// 30
				sqlST.AppendLine("		last_ball_type   , ");
				sqlST.AppendLine("		last_ball_speed   , ");
				sqlST.AppendLine("		res_hit   , ");
				sqlST.AppendLine("		res_hit_type   , ");
				sqlST.AppendLine("		res_course     , ");
				/// 35
				sqlST.AppendLine("		res_course_x     , ");
				sqlST.AppendLine("		res_course_y     , ");
				sqlST.AppendLine("		res_position     , ");
				sqlST.AppendLine("		res_position_x     , ");
				sqlST.AppendLine("		res_position_y      , ");
				/// 40
				sqlST.AppendLine("		get_score      ,");
				sqlST.AppendLine("		error      ,");
				sqlST.AppendLine("		fielder_choice      ,");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				/// 45
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				/// 50
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				sqlST.AppendLine("		etc_date1   , ");
				sqlST.AppendLine("		etc_date2   , ");
				/// 55
				sqlST.AppendLine("		etc_date3   , ");
				sqlST.AppendLine("		etc_date4   , ");
				sqlST.AppendLine("		etc_date5   , ");

				sqlST.AppendLine("		hit_id   , ");
				sqlST.AppendLine("		miss_so   , ");
				sqlST.AppendLine("		swing_so   , ");
				sqlST.AppendLine("		walks   , ");
				sqlST.AppendLine("		dead   , ");
				sqlST.AppendLine("		s_bunt   , ");
				sqlST.AppendLine("		s_fly   , ");
				sqlST.AppendLine("		ining_box_id   , ");
				sqlST.AppendLine("		runner_1_player_id   , ");
				sqlST.AppendLine("		runner_2_player_id   , ");
				sqlST.AppendLine("		runner_3_player_id   , ");
				sqlST.AppendLine("		ining_score ,    ");

				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				/// 60
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(int box_id = 0, DateTime? season = null, int player_id = 0, int team_id = 0, int position = 0, int player_num = 0,
									int pitcher_id = 0, int pit_team_id = 0, int ball_box_num = 0, int ball_total_num = 0, int cat_id = 0, int ump_id = 0,
									int game_id = 0, int game_box_num = 0, int park_id = 0, int bat_id = 0,
									int pit_hand_id = 0, int pit_throw_id = 0, int weather_id = 0,
									int count_b = 0, int count_s = 0, int count_o = 0,
									bool runner_1 = false, bool runner_2 = false, bool runner_3 = false,
									int ining = 0, bool top_bot = false, int top_score = 0, int bottom_score = 0,
									int last_ball_type = 0, int last_ball_speed = 0, int res_hit = 0, int res_hit_type = 0, int res_course = 0, int res_course_x = 0, int res_course_y = 0,
									int res_position = 0, int res_position_x = 0, int res_position_y = 0,
									int get_score = 0, bool error = false, bool fielder_choice = false,
									int etc_cd1 = 0, int etc_cd2 = 0, int etc_cd3 = 0, int etc_cd4 = 0, int etc_cd5 = 0,
									string etc_str1 = "", string etc_str2 = "", string etc_str3 = "", string etc_str4 = "", string etc_str5 = "",
									DateTime? etc_date1 = null, DateTime? etc_date2 = null, DateTime? etc_date3 = null, DateTime? etc_date4 = null, DateTime? etc_date5 = null,
									int hit_id = 0,
									bool miss_so = false,
									bool swing_so = false,
									bool walks = false,
									bool dead = false,
									bool s_bunt = false,
									bool s_fly = false,
									int ining_box_id = 1,
									int runner_1_player_id = 0,
									int runner_2_player_id = 0,
									int runner_3_player_id = 0,
									int ining_score = 0,
									DateTime? update_date = null
									)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	box ");

				sqlST.AppendLine("		( ");
				/// 基本情報
				#region 基本
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id , ");

				sqlST.AppendLine("		season  , ");

				sqlST.AppendLine("		team_id  , ");

				sqlST.AppendLine("		position   , ");

				sqlST.AppendLine("		player_num    , ");

				sqlST.AppendLine("		pitcher_id     , ");

				sqlST.AppendLine("		pit_team_id     , ");

				sqlST.AppendLine("		ball_box_num      , ");

				sqlST.AppendLine("		ball_total_num       , ");

				sqlST.AppendLine("		cat_id        , ");

				sqlST.AppendLine("		ump_id         , ");

				sqlST.AppendLine("		game_id          , ");

				sqlST.AppendLine("		game_box_num          , ");

				sqlST.AppendLine("		park_id          , ");

				sqlST.AppendLine("		bat_id  , ");

				sqlST.AppendLine("		pit_hand_id  , ");

				sqlST.AppendLine("		pit_throw_id   , ");

				sqlST.AppendLine("		weather_id  , ");

				#endregion
				/// カウント
				#region カウント

				sqlST.AppendLine("		count_b   , ");

				sqlST.AppendLine("		count_s  , ");

				sqlST.AppendLine("		count_o , ");

				sqlST.AppendLine("		runner_1  , ");
				sqlST.AppendLine("		runner_2  , ");
				sqlST.AppendLine("		runner_3  , ");

				sqlST.AppendLine("		ining  , ");

				sqlST.AppendLine("		top_bot    , ");

				sqlST.AppendLine("		top_score    , ");

				sqlST.AppendLine("		bottom_score    , ");

				sqlST.AppendLine("		last_ball_type    , ");

				sqlST.AppendLine("		last_ball_speed     , ");

				sqlST.AppendLine("		res_hit      , ");

				sqlST.AppendLine("		res_hit_type    ,  ");

				sqlST.AppendLine("		res_course     , ");

				sqlST.AppendLine("		res_course_x    , ");

				sqlST.AppendLine("		res_course_y    , ");

				sqlST.AppendLine("		res_position    , ");

				sqlST.AppendLine("		res_position_x   , ");

				sqlST.AppendLine("		res_position_y   , ");

				sqlST.AppendLine("		get_score   , ");

				sqlST.AppendLine("		error   , ");
				sqlST.AppendLine("		fielder_choice   , ");
				#endregion

				/// 予備情報
				#region 予備

				sqlST.AppendLine("		etc_cd1   , ");

				sqlST.AppendLine("		etc_cd2   , ");

				sqlST.AppendLine("		etc_cd3   , ");

				sqlST.AppendLine("		etc_cd4   , ");

				sqlST.AppendLine("		etc_cd5   , ");

				sqlST.AppendLine("		etc_str1   , ");

				sqlST.AppendLine("		etc_str2   , ");

				sqlST.AppendLine("		etc_str3   , ");

				sqlST.AppendLine("		etc_str4   , ");

				sqlST.AppendLine("		etc_str5   , ");

				//sqlST.AppendLine("		etc_date1   , ");

				//sqlST.AppendLine("		etc_date2   , ");

				//sqlST.AppendLine("		etc_date3   , ");

				//sqlST.AppendLine("		etc_date4   , ");

				//sqlST.AppendLine("		etc_date5   , ");

				#endregion



				sqlST.AppendLine("		hit_id	,    ");
				sqlST.AppendLine("		miss_so   , ");
				sqlST.AppendLine("		swing_so   , ");
				sqlST.AppendLine("		walks   , ");
				sqlST.AppendLine("		dead   , ");
				sqlST.AppendLine("		s_bunt   , ");
				sqlST.AppendLine("		s_fly   , ");
				sqlST.AppendLine("		ining_box_id   , ");


				sqlST.AppendLine("		runner_1_player_id   , ");
				sqlST.AppendLine("		runner_2_player_id   , ");
				sqlST.AppendLine("		runner_3_player_id   , ");

				sqlST.AppendLine("		ining_score ,    ");
				///　更新日

				sqlST.AppendLine("		update_date    ");

				sqlST.AppendLine(" ) ");


				sqlST.AppendLine("VALUES( ");

				/// 基本情報
				#region 基本
				sqlST.AppendLine("		@box_id , ");
				CMD_Insert.Parameters.AddWithValue("@box_id", box_id);
				sqlST.AppendLine("		@player_id , ");
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);

				sqlST.AppendLine("		@season  , ");
				CMD_Insert.Parameters.AddWithValue("@season", season);



				sqlST.AppendLine("		@team_id  , ");
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);

				sqlST.AppendLine("		@position   , ");
				CMD_Insert.Parameters.AddWithValue("@position", position);

				sqlST.AppendLine("		@player_num    , ");
				CMD_Insert.Parameters.AddWithValue("@player_num", player_num);

				sqlST.AppendLine("		@pitcher_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pitcher_id", pitcher_id);

				sqlST.AppendLine("		@pit_team_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pit_team_id", pit_team_id);

				sqlST.AppendLine("		@ball_box_num      , ");
				CMD_Insert.Parameters.AddWithValue("@ball_box_num", ball_box_num);

				sqlST.AppendLine("		@ball_total_num       , ");
				CMD_Insert.Parameters.AddWithValue("@ball_total_num", ball_total_num);

				sqlST.AppendLine("		@cat_id        , ");
				CMD_Insert.Parameters.AddWithValue("@cat_id", cat_id);

				sqlST.AppendLine("		@ump_id         , ");
				CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);

				sqlST.AppendLine("		@game_id          , ");
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);

				sqlST.AppendLine("		@game_box_num          , ");
				CMD_Insert.Parameters.AddWithValue("@game_box_num", game_box_num);

				sqlST.AppendLine("		@park_id          , ");
				CMD_Insert.Parameters.AddWithValue("@park_id", park_id);

				sqlST.AppendLine("		@bat_id  , ");
				CMD_Insert.Parameters.AddWithValue("@bat_id", bat_id);

				sqlST.AppendLine("		@pit_hand_id  , ");
				CMD_Insert.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);

				sqlST.AppendLine("		@pit_throw_id   , ");
				CMD_Insert.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);

				sqlST.AppendLine("		@weather_id  , ");
				CMD_Insert.Parameters.AddWithValue("@weather_id", weather_id);

				#endregion
				/// カウント
				#region カウント

				sqlST.AppendLine("		@count_b   , ");
				CMD_Insert.Parameters.AddWithValue("@count_b", count_b);

				sqlST.AppendLine("		@count_s  , ");
				CMD_Insert.Parameters.AddWithValue("@count_s", count_s);

				sqlST.AppendLine("		@count_o , ");
				CMD_Insert.Parameters.AddWithValue("@count_o", count_o);

				sqlST.AppendLine("		@runner_1  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1", runner_1);
				sqlST.AppendLine("		@runner_2  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2", runner_2);
				sqlST.AppendLine("		@runner_3  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3", runner_3);

				sqlST.AppendLine("		@ining  , ");
				CMD_Insert.Parameters.AddWithValue("@ining", ining);

				sqlST.AppendLine("		@top_bot    , ");
				CMD_Insert.Parameters.AddWithValue("@top_bot", top_bot);

				sqlST.AppendLine("		@top_score    , ");
				CMD_Insert.Parameters.AddWithValue("@top_score", top_score);

				sqlST.AppendLine("		@bottom_score    , ");
				CMD_Insert.Parameters.AddWithValue("@bottom_score", bottom_score);

				sqlST.AppendLine("		@last_ball_type    , ");
				CMD_Insert.Parameters.AddWithValue("@last_ball_type", last_ball_type);

				sqlST.AppendLine("		@last_ball_speed     , ");
				CMD_Insert.Parameters.AddWithValue("@last_ball_speed", last_ball_speed);

				sqlST.AppendLine("		@res_hit      , ");
				CMD_Insert.Parameters.AddWithValue("@res_hit", res_hit);

				sqlST.AppendLine("		@res_hit_type    ,  ");
				CMD_Insert.Parameters.AddWithValue("@res_hit_type", res_hit_type);




				sqlST.AppendLine("		@res_course     , ");
				CMD_Insert.Parameters.AddWithValue("@res_course", res_course);

				sqlST.AppendLine("		@res_course_x    , ");
				CMD_Insert.Parameters.AddWithValue("@res_course_x", res_course_x);


				sqlST.AppendLine("		@res_course_y    , ");
				CMD_Insert.Parameters.AddWithValue("@res_course_y", res_course_y);




				sqlST.AppendLine("		@res_position    , ");
				CMD_Insert.Parameters.AddWithValue("@res_position", res_position);

				sqlST.AppendLine("		@res_position_x   , ");
				CMD_Insert.Parameters.AddWithValue("@res_position_x", res_position_x);

				sqlST.AppendLine("		@res_position_y   , ");
				CMD_Insert.Parameters.AddWithValue("@res_position_y", res_position_y);

				sqlST.AppendLine("		@get_score   , ");
				CMD_Insert.Parameters.AddWithValue("@get_score", get_score);

				sqlST.AppendLine("		@error   , ");
				CMD_Insert.Parameters.AddWithValue("@error", error);
				sqlST.AppendLine("		@fielder_choice   , ");
				CMD_Insert.Parameters.AddWithValue("@fielder_choice", fielder_choice);
				#endregion

				/// 予備情報
				#region 予備

				sqlST.AppendLine("		@etc_cd1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);

				sqlST.AppendLine("		@etc_cd2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);

				sqlST.AppendLine("		@etc_cd3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);

				sqlST.AppendLine("		@etc_cd4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);

				sqlST.AppendLine("		@etc_cd5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);

				sqlST.AppendLine("		@etc_str1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);

				sqlST.AppendLine("		@etc_str2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);

				sqlST.AppendLine("		@etc_str3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);

				sqlST.AppendLine("		@etc_str4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);

				sqlST.AppendLine("		@etc_str5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);

				//sqlST.AppendLine("		@etc_date1   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date1", etc_date1);

				//sqlST.AppendLine("		@etc_date2   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date2", etc_date2);

				//sqlST.AppendLine("		@etc_date3   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date3", etc_date3);

				//sqlST.AppendLine("		@etc_date4   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date4", etc_date4);

				//sqlST.AppendLine("		@etc_date5   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date5", etc_date5);

				#endregion


				//sqlST.AppendLine("		hit_id	,    ");
				sqlST.AppendLine("		@hit_id ,   ");
				CMD_Insert.Parameters.AddWithValue("@hit_id", hit_id);
				sqlST.AppendLine("		@miss_so   , ");
				CMD_Insert.Parameters.AddWithValue("@miss_so", miss_so);
				sqlST.AppendLine("		@swing_so   , ");
				CMD_Insert.Parameters.AddWithValue("@swing_so", swing_so);
				sqlST.AppendLine("		@walks   , ");
				CMD_Insert.Parameters.AddWithValue("@walks", walks);
				sqlST.AppendLine("		@dead   , ");
				CMD_Insert.Parameters.AddWithValue("@dead", dead);
				sqlST.AppendLine("		@s_bunt   , ");
				CMD_Insert.Parameters.AddWithValue("@s_bunt", s_bunt);
				sqlST.AppendLine("		@s_fly   , ");
				CMD_Insert.Parameters.AddWithValue("@s_fly", s_fly);
				sqlST.AppendLine("		@ining_box_id   , ");
				CMD_Insert.Parameters.AddWithValue("@ining_box_id", ining_box_id);

				sqlST.AppendFormat("		{0}   , ", runner_1_player_id).AppendLine();
				sqlST.AppendFormat("		{0}   , ", runner_2_player_id).AppendLine();
				sqlST.AppendFormat("		{0}   , ", runner_3_player_id).AppendLine();

				sqlST.AppendFormat("		{0}   , ", ining_score).AppendLine();
				///　更新日

				sqlST.AppendLine("		@update_date    ");
				CMD_Insert.Parameters.AddWithValue("@update_date", update_date);

				sqlST.AppendLine(" ); ");

				#region 旧コード
				//sqlST.AppendLine("		( ");
				///// 基本情報
				//#region 基本
				//sqlST.AppendLine("		box_id , ");
				//sqlST.AppendLine("		player_id , ");
				//if (season != null)
				//{
				//	sqlST.AppendLine("		season  , ");
				//}
				//if (team_id != 0)
				//{
				//	sqlST.AppendLine("		team_id  , ");
				//}
				//if (position != 0)
				//{
				//	sqlST.AppendLine("		position   , ");
				//}
				//if (player_num != 0)
				//{
				//	sqlST.AppendLine("		player_num    , ");
				//}
				//if (pitcher_id != 0)
				//{
				//	sqlST.AppendLine("		pitcher_id     , ");
				//}
				//if (pit_team_id != 0)
				//{
				//	sqlST.AppendLine("		pit_team_id     , ");
				//}
				//if (ball_box_num != 0)
				//{
				//	sqlST.AppendLine("		ball_box_num      , ");
				//}
				//if (ball_total_num != 0)
				//{
				//	sqlST.AppendLine("		ball_total_num       , ");
				//}
				//if (cat_id != 0)
				//{
				//	sqlST.AppendLine("		cat_id        , ");
				//}
				//if (ump_id != 0)
				//{
				//	sqlST.AppendLine("		ump_id         , ");
				//}
				//if (game_id != 0)
				//{
				//	sqlST.AppendLine("		game_id          , ");
				//}
				//if (game_box_num != 0)
				//{
				//	sqlST.AppendLine("		game_box_num          , ");
				//}
				//if (park_id != 0)
				//{
				//	sqlST.AppendLine("		park_id          , ");
				//}
				//if (bat_id != 0)
				//{
				//	sqlST.AppendLine("		bat_id  , ");
				//}
				//if (pit_hand_id != 0)
				//{
				//	sqlST.AppendLine("		pit_hand_id  , ");
				//}
				//if (pit_throw_id != 0)
				//{
				//	sqlST.AppendLine("		pit_throw_id   , ");
				//}
				//if (weather_id != 0)
				//{
				//	sqlST.AppendLine("		weather_id  , ");
				//}
				//#endregion
				///// カウント
				//#region カウント
				//if (count_b != 0)
				//{
				//	sqlST.AppendLine("		count_b   , ");
				//}
				//if (count_s != 0)
				//{
				//	sqlST.AppendLine("		count_s  , ");
				//}
				//if (count_o != 0)
				//{
				//	sqlST.AppendLine("		count_o , ");
				//}
				//sqlST.AppendLine("		runner_1  , ");
				//sqlST.AppendLine("		runner_2  , ");
				//sqlST.AppendLine("		runner_3  , ");
				//if (ining != 0)
				//{
				//	sqlST.AppendLine("		ining  , ");
				//}
				//sqlST.AppendLine("		top_bot    , ");
				//if (top_score != 0)
				//{
				//	sqlST.AppendLine("		top_score    , ");
				//}
				//if (bottom_score != 0)
				//{
				//	sqlST.AppendLine("		bottom_score    , ");
				//}
				//if (last_ball_type != 0)
				//{
				//	sqlST.AppendLine("		last_ball_type    , ");
				//}
				//if (last_ball_speed >= 0)
				//{
				//	sqlST.AppendLine("		last_ball_speed     , ");
				//}
				//if (res_hit != 0)
				//{
				//	sqlST.AppendLine("		res_hit      , ");
				//}
				//if (res_hit_type != 0)
				//{
				//	sqlST.AppendLine("		res_hit_type    ,  ");
				//}
				//if (res_course != 0)
				//{
				//	sqlST.AppendLine("		res_course     , ");
				//}
				//if (res_course_x != 0)
				//{
				//	sqlST.AppendLine("		res_course_x    , ");
				//}
				//if (res_course_y != 0)
				//{
				//	sqlST.AppendLine("		res_course_y    , ");
				//}
				//if (res_position != 0)
				//{
				//	sqlST.AppendLine("		res_position    , ");
				//}
				//if (res_position_x != 0)
				//{
				//	sqlST.AppendLine("		res_position_x   , ");
				//}
				//if (res_position_y != 0)
				//{
				//	sqlST.AppendLine("		res_position_y   , ");
				//}
				//if (get_score != 0)
				//{
				//	sqlST.AppendLine("		get_score   , ");
				//}
				//sqlST.AppendLine("		error   , ");
				//sqlST.AppendLine("		fielder_choice   , ");
				//#endregion

				///// 予備情報
				//#region 予備
				//if (etc_cd1 != 0)
				//{
				//	sqlST.AppendLine("		etc_cd1   , ");
				//}
				//if (etc_cd2 != 0)
				//{
				//	sqlST.AppendLine("		etc_cd2   , ");
				//}
				//if (etc_cd3 != 0)
				//{
				//	sqlST.AppendLine("		etc_cd3   , ");
				//}
				//if (etc_cd4 != 0)
				//{
				//	sqlST.AppendLine("		etc_cd4   , ");
				//}
				//if (etc_cd5 != 0)
				//{
				//	sqlST.AppendLine("		etc_cd5   , ");
				//}
				//if (etc_str1 != "")
				//{
				//	sqlST.AppendLine("		etc_str1   , ");
				//}
				//if (etc_str2 != "")
				//{
				//	sqlST.AppendLine("		etc_str2   , ");
				//}
				//if (etc_str3 != "")
				//{
				//	sqlST.AppendLine("		etc_str3   , ");
				//}
				//if (etc_str4 != "")
				//{
				//	sqlST.AppendLine("		etc_str4   , ");
				//}
				//if (etc_str5 != "")
				//{
				//	sqlST.AppendLine("		etc_str5   , ");
				//}
				//if (etc_date1 != null)
				//{
				//	sqlST.AppendLine("		etc_date1   , ");
				//}
				//if (etc_date2 != null)
				//{
				//	sqlST.AppendLine("		etc_date2   , ");
				//}
				//if (etc_date3 != null)
				//{
				//	sqlST.AppendLine("		etc_date3   , ");
				//}
				//if (etc_date4 != null)
				//{
				//	sqlST.AppendLine("		etc_date4   , ");
				//}
				//if (etc_date5 != null)
				//{
				//	sqlST.AppendLine("		etc_date5   , ");
				//}
				//#endregion


				//if (hit_id != 0) 
				//{
				//	sqlST.AppendLine("		hit_id	,    ");
				//}
				/////　更新日
				//if (update_date != null)
				//{
				//	sqlST.AppendLine("		update_date    ");
				//}
				//sqlST.AppendLine(" ) ");


				//sqlST.AppendLine("VALUES( ");

				///// 基本情報
				//#region 基本
				//sqlST.AppendLine("		@box_id , ");
				//CMD_Insert.Parameters.AddWithValue("@box_id", box_id);
				//sqlST.AppendLine("		@player_id , ");
				//CMD_Insert.Parameters.AddWithValue("@player_id", player_id);
				//if (season != null)
				//{
				//	sqlST.AppendLine("		@season  , ");
				//	CMD_Insert.Parameters.AddWithValue("@season", season);
				//}

				//if (team_id != 0)
				//{
				//	sqlST.AppendLine("		@team_id  , ");
				//	CMD_Insert.Parameters.AddWithValue("@team_id", team_id);
				//}
				//if (position != 0)
				//{
				//	sqlST.AppendLine("		@position   , ");
				//	CMD_Insert.Parameters.AddWithValue("@position", position);
				//}
				//if (player_num != 0)
				//{
				//	sqlST.AppendLine("		@player_num    , ");
				//	CMD_Insert.Parameters.AddWithValue("@player_num", player_num);
				//}
				//if (pitcher_id != 0)
				//{
				//	sqlST.AppendLine("		@pitcher_id     , ");
				//	CMD_Insert.Parameters.AddWithValue("@pitcher_id", pitcher_id);
				//}
				//if (pit_team_id != 0)
				//{
				//	sqlST.AppendLine("		@pit_team_id     , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_team_id", pit_team_id);
				//}
				//if (ball_box_num != 0)
				//{
				//	sqlST.AppendLine("		@ball_box_num      , ");
				//	CMD_Insert.Parameters.AddWithValue("@ball_box_num", ball_box_num);
				//}
				//if (ball_total_num != 0)
				//{
				//	sqlST.AppendLine("		@ball_total_num       , ");
				//	CMD_Insert.Parameters.AddWithValue("@ball_total_num", ball_total_num);
				//}
				//if (cat_id != 0)
				//{
				//	sqlST.AppendLine("		@cat_id        , ");
				//	CMD_Insert.Parameters.AddWithValue("@cat_id", cat_id);
				//}
				//if (ump_id != 0)
				//{
				//	sqlST.AppendLine("		@ump_id         , ");
				//	CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);
				//}
				//if (game_id != 0)
				//{
				//	sqlST.AppendLine("		@game_id          , ");
				//	CMD_Insert.Parameters.AddWithValue("@game_id", game_id);
				//}
				//if (game_box_num != 0)
				//{
				//	sqlST.AppendLine("		@game_box_num          , ");
				//	CMD_Insert.Parameters.AddWithValue("@game_box_num", game_box_num);
				//}
				//if (park_id != 0)
				//{
				//	sqlST.AppendLine("		@park_id          , ");
				//	CMD_Insert.Parameters.AddWithValue("@park_id", park_id);
				//}
				//if (bat_id != 0)
				//{
				//	sqlST.AppendLine("		@bat_id  , ");
				//	CMD_Insert.Parameters.AddWithValue("@bat_id", bat_id);
				//}
				//if (pit_hand_id != 0)
				//{
				//	sqlST.AppendLine("		@pit_hand_id  , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);
				//}
				//if (pit_throw_id != 0)
				//{
				//	sqlST.AppendLine("		@pit_throw_id   , ");
				//	CMD_Insert.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);
				//}
				//if (weather_id != 0)
				//{
				//	sqlST.AppendLine("		@weather_id  , ");
				//	CMD_Insert.Parameters.AddWithValue("@weather_id", weather_id);
				//}
				//#endregion
				///// カウント
				//#region カウント
				//if (count_b != 0)
				//{
				//	sqlST.AppendLine("		@count_b   , ");
				//	CMD_Insert.Parameters.AddWithValue("@count_b", count_b);
				//}
				//if (count_s != 0)
				//{
				//	sqlST.AppendLine("		@count_s  , ");
				//	CMD_Insert.Parameters.AddWithValue("@count_s", count_s);
				//}
				//if (count_o != 0)
				//{
				//	sqlST.AppendLine("		@count_o , ");
				//	CMD_Insert.Parameters.AddWithValue("@count_o", count_o);
				//}
				//sqlST.AppendLine("		@runner_1  , ");
				//CMD_Insert.Parameters.AddWithValue("@runner_1", runner_1);
				//sqlST.AppendLine("		@runner_2  , ");
				//CMD_Insert.Parameters.AddWithValue("@runner_2", runner_2);
				//sqlST.AppendLine("		@runner_3  , ");
				//CMD_Insert.Parameters.AddWithValue("@runner_3", runner_3);
				//if (ining != 0)
				//{
				//	sqlST.AppendLine("		@ining  , ");
				//	CMD_Insert.Parameters.AddWithValue("@ining", ining);
				//}
				//sqlST.AppendLine("		@top_bot    , ");
				//CMD_Insert.Parameters.AddWithValue("@top_bot", top_bot);
				//if (top_score != 0)
				//{
				//	sqlST.AppendLine("		@top_score    , ");
				//	CMD_Insert.Parameters.AddWithValue("@top_score", top_score);
				//}
				//if (bottom_score != 0)
				//{
				//	sqlST.AppendLine("		@bottom_score    , ");
				//	CMD_Insert.Parameters.AddWithValue("@bottom_score", bottom_score);
				//}
				//if (last_ball_type != 0)
				//{
				//	sqlST.AppendLine("		@last_ball_type    , ");
				//	CMD_Insert.Parameters.AddWithValue("@last_ball_type", last_ball_type);
				//}
				//if (last_ball_speed >= 0)
				//{
				//	sqlST.AppendLine("		@last_ball_speed     , ");
				//	CMD_Insert.Parameters.AddWithValue("@last_ball_speed", last_ball_speed);
				//}
				//if (res_hit != 0)
				//{
				//	sqlST.AppendLine("		@res_hit      , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_hit", res_hit);
				//}
				//if (res_hit_type != 0)
				//{
				//	sqlST.AppendLine("		@res_hit_type    ,  ");
				//	CMD_Insert.Parameters.AddWithValue("@res_hit_type", res_hit_type);
				//}


				//if (res_course != 0)
				//{
				//	sqlST.AppendLine("		@res_course     , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_course", res_course);
				//}
				//if (res_course_x != 0)
				//{
				//	sqlST.AppendLine("		@res_course_x    , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_course_x", res_course_x);
				//}
				//if (res_course_y != 0)
				//{
				//	sqlST.AppendLine("		@res_course_y    , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_course_y", res_course_y);
				//}


				//if (res_position != 0)
				//{
				//	sqlST.AppendLine("		@res_position    , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_position", res_position);
				//}
				//if (res_position_x != 0)
				//{
				//	sqlST.AppendLine("		@res_position_x   , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_position_x", res_position_x);
				//}
				//if (res_position_y != 0)
				//{
				//	sqlST.AppendLine("		@res_position_y   , ");
				//	CMD_Insert.Parameters.AddWithValue("@res_position_y", res_position_y);
				//}
				//if (get_score != 0)
				//{
				//	sqlST.AppendLine("		@get_score   , ");
				//	CMD_Insert.Parameters.AddWithValue("@get_score", get_score);
				//}
				//sqlST.AppendLine("		@error   , ");
				//CMD_Insert.Parameters.AddWithValue("@error", error);
				//sqlST.AppendLine("		@fielder_choice   , ");
				//CMD_Insert.Parameters.AddWithValue("@fielder_choice", fielder_choice);
				//#endregion

				///// 予備情報
				//#region 予備
				//if (etc_cd1 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				//}
				//if (etc_cd2 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				//}
				//if (etc_cd3 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				//}
				//if (etc_cd4 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				//}
				//if (etc_cd5 != 0)
				//{
				//	sqlST.AppendLine("		@etc_cd5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				//}
				//if (etc_str1 != "")
				//{
				//	sqlST.AppendLine("		@etc_str1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);
				//}
				//if (etc_str2 != "")
				//{
				//	sqlST.AppendLine("		@etc_str2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);
				//}
				//if (etc_str3 != "")
				//{
				//	sqlST.AppendLine("		@etc_str3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);
				//}
				//if (etc_str4 != "")
				//{
				//	sqlST.AppendLine("		@etc_str4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);
				//}
				//if (etc_str5 != "")
				//{
				//	sqlST.AppendLine("		@etc_str5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);
				//}
				//if (etc_date1 != null)
				//{
				//	sqlST.AppendLine("		@etc_date1   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date1", etc_date1);
				//}
				//if (etc_date2 != null)
				//{
				//	sqlST.AppendLine("		@etc_date2   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date2", etc_date2);
				//}
				//if (etc_date3 != null)
				//{
				//	sqlST.AppendLine("		@etc_date3   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date3", etc_date3);
				//}
				//if (etc_date4 != null)
				//{
				//	sqlST.AppendLine("		@etc_date4   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date4", etc_date4);
				//}
				//if (etc_date5 != null)
				//{
				//	sqlST.AppendLine("		@etc_date5   , ");
				//	CMD_Insert.Parameters.AddWithValue("@etc_date5", etc_date5);
				//}
				//#endregion

				//if (hit_id != 0)
				//{
				//	sqlST.AppendLine("		hit_id	,    ");
				//	sqlST.AppendLine("		@hit_id    ");
				//	CMD_Insert.Parameters.AddWithValue("@hit_id", hit_id);
				//}

				/////　更新日
				//if (update_date != null)
				//{
				//	sqlST.AppendLine("		@update_date    ");
				//	CMD_Insert.Parameters.AddWithValue("@update_date", update_date);
				//}
				//sqlST.AppendLine(" ); ");
				#endregion


				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(int box_id = 0,
										//DateTime? season = null,
										int player_id = 0,
										int team_id = 0,
										int position = 0,
										int player_num = 0,
										int pitcher_id = 0,
										int pit_team_id = 0,
										int ball_box_num = 0,
										int ball_total_num = 0,
										int cat_id = 0,
										int ump_id = 0,
										int game_id = 0,
										int game_box_num = 0,
										int park_id = 0,
										int bat_id = 0,
										int pit_hand_id = 0,
										int pit_throw_id = 0,
										int weather_id = 0,
										int count_b = 0,
										int count_s = 0,
										int count_o = 0,
										bool runner_1 = false,
										bool runner_2 = false,
										bool runner_3 = false,
										int runner_1_player_id = -1,
										int runner_2_player_id = -1,
										int runner_3_player_id = -1,
										int ining = 0,
										bool top_bot = false,
										int top_score = 0,
										int bottom_score = 0,
										int last_ball_type = 0,
										int last_ball_speed = 0,
										int res_hit = 0,
										int res_hit_type = 0,
										int res_course = 0,
										int res_course_x = 0,
										int res_course_y = 0,
										int res_position = 0,
										int res_position_x = 0,
										int res_position_y = 0,
										int get_score = 0,
										bool error = false,
										bool fielder_choice = false,
										int etc_cd1 = 0, int etc_cd2 = 0, int etc_cd3 = 0, int etc_cd4 = 0, int etc_cd5 = 0,
										string etc_str1 = "", string etc_str2 = "", string etc_str3 = "", string etc_str4 = "", string etc_str5 = "",
										//DateTime? etc_date1 = null, DateTime? etc_date2 = null, DateTime? etc_date3 = null, DateTime? etc_date4 = null, DateTime? etc_date5 = null,
										int hit_id = 0,
										bool miss_so = false,
									bool swing_so = false,
									bool walks = false,
									bool dead = false,
									bool s_bunt = false,
									bool s_fly = false,
									int ining_box_id = 1,
									int ining_score = -1,
										DateTime? update_date = null
										)
		{
			//if (player_id == 0)
			//{
			//	Console.WriteLine("player_idが指定されていません");
			//	return;
			//}

			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE box ");
				sqlST.AppendLine("	SET ");
				/// 基本情報
				#region 基本
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	, ", box_id).AppendLine();
				if (player_id != 0)
				{
					sqlST.AppendLine("		player_id= ");
					sqlST.AppendFormat("		{0}	, ", player_id).AppendLine();
				}

				if (team_id != 0)
				{
					sqlST.AppendLine("		team_id= ");
					sqlST.AppendFormat("		{0}	, ", team_id).AppendLine();
				}
				if (position != 0)
				{
					sqlST.AppendLine("		position=   ");
					sqlST.AppendFormat("		{0}	, ", position).AppendLine();
				}
				if (player_num != 0)
				{
					sqlST.AppendLine("		player_num=   ");
					sqlST.AppendFormat("		{0}	, ", player_num).AppendLine();
				}
				if (pitcher_id != 0)
				{
					sqlST.AppendLine("		pitcher_id=   ");
					sqlST.AppendFormat("		{0}	, ", pitcher_id).AppendLine();
				}
				if (pit_team_id != 0)
				{
					sqlST.AppendLine("		pit_team_id=      ");
					sqlST.AppendFormat("		{0}	, ", pit_team_id).AppendLine();
				}
				if (ball_box_num != 0)
				{
					sqlST.AppendLine("		ball_box_num=       ");
					sqlST.AppendFormat("		{0}	, ", ball_box_num).AppendLine();
				}
				if (ball_total_num != 0)
				{
					sqlST.AppendLine("		ball_total_num=       ");
					sqlST.AppendFormat("		{0}	, ", ball_total_num).AppendLine();
				}
				if (cat_id != 0)
				{
					sqlST.AppendLine("		cat_id=         ");
					sqlST.AppendFormat("		{0}	, ", cat_id).AppendLine();
				}
				if (ump_id != 0)
				{
					sqlST.AppendLine("		ump_id=          ");
					sqlST.AppendFormat("		{0}	, ", ump_id).AppendLine();
				}
				if (game_id != 0)
				{
					sqlST.AppendLine("		game_id=          ");
					sqlST.AppendFormat("		{0}	, ", game_id).AppendLine();
				}
				if (game_box_num != 0)
				{
					sqlST.AppendLine("		game_box_num=           ");
					sqlST.AppendFormat("		{0}	, ", game_box_num).AppendLine();
				}
				if (park_id != 0)
				{
					sqlST.AppendLine("		park_id=           ");
					sqlST.AppendFormat("		{0},	 ", park_id).AppendLine();
				}
				if (bat_id != 0)
				{
					sqlST.AppendLine("		bat_id=   ");
					sqlST.AppendFormat("		{0}	 ,", bat_id).AppendLine();
				}
				if (pit_hand_id != 0)
				{
					sqlST.AppendLine("		pit_hand_id= ");
					sqlST.AppendFormat("		{0}	 ,", pit_hand_id).AppendLine();
				}
				if (pit_throw_id != 0)
				{
					sqlST.AppendLine("		pit_throw_id=    ");
					sqlST.AppendFormat("		{0},	 ", pit_throw_id).AppendLine();
				}
				if (weather_id != 0)
				{
					sqlST.AppendLine("		weather_id=   ");
					sqlST.AppendFormat("		{0}	, ", weather_id).AppendLine();
				}
				#endregion
				/// カウント
				#region カウント
				if (count_b != 0)
				{
					sqlST.AppendLine("		count_b=    ");
					sqlST.AppendFormat("		{0}	, ", count_b).AppendLine();
				}
				if (count_s != 0)
				{
					sqlST.AppendLine("		count_s=    ");
					sqlST.AppendFormat("		{0}	, ", count_s).AppendLine();
				}
				if (count_o != 0)
				{
					sqlST.AppendLine("		count_o=    ");
					sqlST.AppendFormat("		{0}	, ", count_o).AppendLine();
				}
				sqlST.AppendLine("		runner_1=    ");
				sqlST.AppendFormat("		{0}	, ", runner_1).AppendLine();
				sqlST.AppendLine("		runner_2=    ");
				sqlST.AppendFormat("		{0}	, ", runner_2).AppendLine();
				sqlST.AppendLine("		runner_3=    ");
				sqlST.AppendFormat("		{0}	, ", runner_3).AppendLine();

				/// 2022.03.03 追加
				if (runner_1_player_id >= 0)
				{
					sqlST.AppendLine("		runner_1_player_id=    ");
					sqlST.AppendFormat("		{0}	, ", runner_1_player_id).AppendLine();
				}
				if (runner_2_player_id >= 0)
				{
					sqlST.AppendLine("		runner_2_player_id=    ");
					sqlST.AppendFormat("		{0}	, ", runner_2_player_id).AppendLine();
				}
				if (runner_3_player_id >= 0)
				{
					sqlST.AppendLine("		runner_3_player_id=    ");
					sqlST.AppendFormat("		{0}	, ", runner_3_player_id).AppendLine();
				}

				if (ining_score >= 0)
				{
					sqlST.AppendLine("		ining_score=    ");
					sqlST.AppendFormat("		{0}	, ", ining_score).AppendLine();
				}



				if (ining != 0)
				{
					sqlST.AppendLine("		ining=    ");
					sqlST.AppendFormat("		{0}	, ", ining).AppendLine();
				}
				sqlST.AppendLine("		top_bot=     ");
				sqlST.AppendFormat("		{0}	, ", top_bot).AppendLine();
				if (top_score != 0)
				{
					sqlST.AppendLine("		top_score=     ");
					sqlST.AppendFormat("		{0}	, ", top_score).AppendLine();
				}
				if (bottom_score != 0)
				{
					sqlST.AppendLine("		bottom_score=     ");
					sqlST.AppendFormat("		{0}	, ", bottom_score).AppendLine();
				}
				if (last_ball_type != 0)
				{
					sqlST.AppendLine("		last_ball_type=     ");
					sqlST.AppendFormat("		{0}	, ", last_ball_type).AppendLine();
				}
				if (last_ball_speed >= 0)
				{
					sqlST.AppendLine("		last_ball_speed=      ");
					sqlST.AppendFormat("		{0}	, ", last_ball_speed).AppendLine();
				}
				if (res_hit != 0)
				{
					sqlST.AppendLine("		res_hit=       ");
					sqlST.AppendFormat("		{0}	, ", res_hit).AppendLine();
				}
				if (res_hit_type != 0)
				{
					sqlST.AppendLine("		res_hit_type=      ");
					sqlST.AppendFormat("		{0}	, ", res_hit_type).AppendLine();
				}


				if (res_course != 0)
				{
					sqlST.AppendLine("		res_course=      ");
					sqlST.AppendFormat("		{0}	, ", res_course).AppendLine();
				}
				if (res_course_x != 0)
				{
					sqlST.AppendLine("		res_course_x=     ");
					sqlST.AppendFormat("		{0}	, ", res_course_x).AppendLine();
				}
				if (res_course_y != 0)
				{
					sqlST.AppendLine("		res_course_y=     ");
					sqlST.AppendFormat("		{0}	, ", res_course_y).AppendLine();
				}


				if (res_position != 0)
				{
					sqlST.AppendLine("		res_position=     ");
					sqlST.AppendFormat("		{0}	, ", res_position).AppendLine();
				}
				if (res_position_x != 0)
				{
					sqlST.AppendLine("		res_position_x=    ");
					sqlST.AppendFormat("		{0}	, ", res_position_x).AppendLine();
				}
				if (res_position_y != 0)
				{
					sqlST.AppendLine("		res_position_y=    ");
					sqlST.AppendFormat("		{0}	, ", res_position_y).AppendLine();
				}
				if (get_score != 0)
				{
					sqlST.AppendLine("		get_score=    ");
					sqlST.AppendLine("		@get_score   , ");
					CMD_Update.Parameters.AddWithValue("@get_score", get_score);
				}
				sqlST.AppendLine("		error=    ");
				sqlST.AppendFormat("		{0}	, ", error).AppendLine();
				sqlST.AppendLine("		fielder_choice=    ");
				sqlST.AppendFormat("		{0}	, ", fielder_choice).AppendLine();
				#endregion

				/// 予備
				#region 予備
				if (etc_cd1 != 0)
				{
					sqlST.AppendLine("		etc_cd1=   ");
					sqlST.AppendLine("				@etc_cd1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				}
				if (etc_cd2 != 0)
				{
					sqlST.AppendLine("		etc_cd2=   ");
					sqlST.AppendLine("				@etc_cd2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				}
				if (etc_cd3 != 0)
				{
					sqlST.AppendLine("		etc_cd3=   ");
					sqlST.AppendLine("			@etc_cd3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				}
				if (etc_cd4 != 0)
				{
					sqlST.AppendLine("		etc_cd4=   ");
					sqlST.AppendLine("			@etc_cd4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				}
				if (etc_cd5 != 0)
				{
					sqlST.AppendLine("		etc_cd5=   ");
					sqlST.AppendLine("			@etc_cd5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				}
				if (etc_str1 != "")
				{
					sqlST.AppendLine("		etc_str1=   ");
					sqlST.AppendLine("			@etc_str1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str1", etc_str1);
				}
				if (etc_str2 != "")
				{
					sqlST.AppendLine("		etc_str2=   ");
					sqlST.AppendLine("				@etc_str2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str2", etc_str2);
				}
				if (etc_str3 != "")
				{
					sqlST.AppendLine("		etc_str3=   ");
					sqlST.AppendLine("			@etc_str3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str3", etc_str3);
				}
				if (etc_str4 != "")
				{
					sqlST.AppendLine("		etc_str4=   ");
					sqlST.AppendLine("			@etc_str4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str4", etc_str4);
				}
				if (etc_str5 != "")
				{
					sqlST.AppendLine("		etc_str5=   ");
					sqlST.AppendLine("			@etc_str5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str5", etc_str5);
				}
				//if (etc_date1 != null)
				//{
				//	sqlST.AppendLine("		etc_date1=   ");
				//	sqlST.AppendLine("			@etc_date1   , ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date1", etc_date1);
				//}
				//if (etc_date2 != null)
				//{
				//	sqlST.AppendLine("		etc_date2=   ");
				//	sqlST.AppendLine("			@etc_date2   , ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date2", etc_date2);
				//}
				//if (etc_date3 != null)
				//{
				//	sqlST.AppendLine("		etc_date3=   ");
				//	sqlST.AppendLine("			@etc_date3   , ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date3", etc_date3);
				//}
				//if (etc_date4 != null)
				//{
				//	sqlST.AppendLine("		etc_date4=   ");
				//	sqlST.AppendLine("			@etc_date4   , ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date4", etc_date4);
				//}
				//if (etc_date5 != null)
				//{
				//	sqlST.AppendLine("		etc_date5=   ");
				//	sqlST.AppendLine("			@etc_date5   , ");
				//	CMD_Update.Parameters.AddWithValue("@etc_date5", etc_date5);
				//}
				#endregion

				if (hit_id != 0)
				{
					sqlST.AppendLine("		hit_id=	    ");
					sqlST.AppendFormat("		{0}	, ", hit_id).AppendLine();
				}
				if (miss_so)
				{
					sqlST.AppendLine("		miss_so=	    ");
					sqlST.AppendFormat("		{0}	, ", miss_so).AppendLine();
				}
				if (swing_so)
				{
					sqlST.AppendLine("		swing_so=	    ");
					sqlST.AppendFormat("		{0}	, ", swing_so).AppendLine();
				}
				if (walks)
				{
					sqlST.AppendLine("		walks=	    ");
					sqlST.AppendFormat("		{0}	, ", walks).AppendLine();
				}
				if (dead)
				{
					sqlST.AppendLine("		dead=	    ");
					sqlST.AppendFormat("		{0}	, ", dead).AppendLine();
				}
				if (s_bunt)
				{
					sqlST.AppendLine("		s_bunt=	    ");
					sqlST.AppendFormat("		{0}	, ", s_bunt).AppendLine();
				}
				if (s_fly)
				{
					sqlST.AppendLine("		s_fly=	    ");
					sqlST.AppendFormat("		{0}	, ", s_fly).AppendLine();
				}
				if (ining_box_id > 1)
				{
					sqlST.AppendLine("		ining_box_id=	    ");
					sqlST.AppendFormat("		{0}	, ", ining_box_id).AppendLine();
				}


				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=   ");
					//sqlST.AppendFormat("		{0}	 ", update_date).AppendLine();
					sqlST.AppendLine("			@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}
				else
				{
					update_date = DateTime.Now;
					sqlST.AppendFormat("		update_date='{0}'	 ", update_date).AppendLine();

				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	 ", box_id).AppendLine();


				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		/// <summary>
		/// 打席への得点結果の追記
		/// ver.1.1.1.0以降
		/// </summary>
		/// <param name="box_id"></param>
		/// <param name="ining_score"></param>
		/// <param name="ining"></param>
		/// <param name="topbtmFlg"></param>
		/// <param name="update_date"></param>
		public static void updateRecordIningScore(
									int game_id = 0,
									int box_id = 0,
									int ining_score = -1,
									int ining = -1,
									bool topbtmFlg = false,
									DateTime? update_date = null
										)
		{

			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE box ");
				sqlST.AppendLine("	SET ");
				/// 基本情報
				#region 基本
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	, ", box_id).AppendLine();

				if (ining_score >= 0)
				{
					sqlST.AppendLine("		ining_score=    ");
					sqlST.AppendFormat("		{0}	, ", ining_score).AppendLine();
				}

				if (ining >= 0)
				{
					sqlST.AppendLine("		ining=    ");
					sqlST.AppendFormat("		{0}	, ", ining).AppendLine();
				}

				sqlST.AppendFormat("		top_bot={0}	, ", topbtmFlg).AppendLine();
				#endregion

				DateTime update_time = DateTime.Now;
				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=   ");
					//sqlST.AppendFormat("		{0}	 ", update_date).AppendLine();
					sqlST.AppendLine("			@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}
				else
				{
					sqlST.AppendFormat("		update_date='{0}'	 ", update_time).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	 ", box_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		game_id= ");
				sqlST.AppendFormat("		{0}	 ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}




		public static void deleteRecord(int box_id = 0)
		{
			if (box_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM box ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	box_id=@box_id ");
				CmdDelete.Parameters.AddWithValue("@box_id", box_id);
				CmdDelete.CommandText = sqlST.ToString();
				#endregion
				CmdDelete.ExecuteReader();
				con.Close();
			}
		}



		public class boxDataCount
		{
			public int box_count { get; set; }
			public boxDataCount(int count = 0)
			{
				this.box_count = count;

			}
		}



		public static List<boxDataCount> GetRecordsIningBoxsCount(
														int game_id = 0,
														int ining = 1,
														bool top_btmFlg = false
			)
		{
			List<boxDataCount> orderList = new List<boxDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(box_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(box_id) + 1 ");
				sqlST.AppendLine("	END AS box_count ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.ining={0} ", ining).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.top_bot={0} ", top_btmFlg).AppendLine();
				//sqlST.AppendLine("	AND ");
				//sqlST.AppendLine("		box.player_id<>0 ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxDataCount(reader.GetInt32(0)

											  )
								);
				}
				con.Close();
			}
			return orderList;
		}






		public static List<boxDataCount> GetRecordsCount()
		{
			List<boxDataCount> countList = new List<boxDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(box_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(box_id) + 1 ");
				sqlST.AppendLine("	END AS box_id ");
				//sqlST.AppendLine("	COUNT(box_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new boxDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public static List<boxDataCount> GetRecordsGameBoxCount(
														int game_id = 0,
														int player_id = 0)
		{
			List<boxDataCount> countList = new List<boxDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(box_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(box_id) + 1 ");
				sqlST.AppendLine("	END AS box_count ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new boxDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public class boxDataIningBoxId
		{
			public int ining_box_id { get; set; }
			public boxDataIningBoxId(int count = 0)
			{
				this.ining_box_id = count;

			}
		}
		public static List<boxDataIningBoxId> GetRecordsIningBoxId(int box_id = 0)
		{
			List<boxDataIningBoxId> countList = new List<boxDataIningBoxId>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ining_box_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new boxDataIningBoxId(reader.GetInt32(0)));
				}
			}
			return countList;
		}


		public class boxDataRunner
		{
			public bool runner_1 { get; set; }
			public bool runner_2 { get; set; }
			public bool runner_3 { get; set; }
			public boxDataRunner(
								bool runner_1 = false,
								bool runner_2 = false,
								bool runner_3 = false
				)
			{
				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;

			}
		}
		public static List<boxDataRunner> GetRecordsRunner(int box_id = 0)
		{
			List<boxDataRunner> countList = new List<boxDataRunner>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	runner_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_3 ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new boxDataRunner(
										reader.GetBoolean(0),
										reader.GetBoolean(1),
										reader.GetBoolean(2)
										));
				}
			}
			return countList;
		}


		public class boxData
		{

			#region 打席変数
			public int box_id { get; set; }                 // 打席識別番号		1
			public DateTime? season { get; set; }           // 年度 ex)2021		2
			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6

			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号
			public int weather_id { get; set; }             // 天気識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int last_ball_type { get; set; }         // 打席結果の球種
			public int last_ball_speed { get; set; }        // 打席結果の球速
			public int res_hit { get; set; }                // 打席の結果
			public int res_hit_type { get; set; }           // 打球の種類
			public int res_course { get; set; }             // 打ったコース		35
			public int res_course_x { get; set; }           // 打ったコースの詳細 X軸
			public int res_course_y { get; set; }           // 打ったコースの詳細 Y軸
			public int res_position { get; set; }           // 打った守備位置
			public int res_position_x { get; set; }         // 打った守備位置の詳細 X軸
			public int res_position_y { get; set; }         // 打った守備位置の詳細 Y軸		40

			public int get_score { get; set; }              // 打点
			public bool error { get; set; }                 // エラーフラグ (false:0=Nan,treu:1=error )
			public bool fielder_choice { get; set; }        // フィルダーチョイス(false:0=Nan,treu:1=fielder's choice )

			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		45
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		50
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備
			public DateTime? etc_date1 { get; set; }        // 予備		55
			public DateTime? etc_date2 { get; set; }        // 予備
			public DateTime? etc_date3 { get; set; }        // 予備
			public DateTime? etc_date4 { get; set; }        // 予備
			public DateTime? etc_date5 { get; set; }        // 予備

			public int hit_id { get; set; }     // 安打系の識別 / 0:凡打, 1:単打, 2:二塁打, 3:三塁打, 4:本塁打
			public bool miss_so { get; set; }
			public bool swing_so { get; set; }
			public bool walks { get; set; }
			public bool dead { get; set; }
			public bool s_bunt { get; set; }
			public bool s_fly { get; set; }
			public int ining_box_id { get; set; }
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }

			public int ining_score { get; set; }

			#endregion


			public boxData(int box_id = 0,
						   DateTime? season = null,
						   int player_id = 0,
						   int team_id = 0,
						   int position = 0,
						   int player_num = 0,
						   int pitcher_id = 0,
						   int pit_team_id = 0,
						   int ball_box_num = 0,
						   int ball_total_num = 0,
						   int cat_id = 0,
						   int ump_id = 0,
						   int game_id = 0,
						   int game_box_num = 0,
						   int park_id = 0,
						   int bat_id = 0,
						   int pit_hand_id = 0,
						   int pit_throw_id = 0,
						   int weather_id = 0,
						   int count_b = 0,
						   int count_s = 0,
						   int count_o = 0,
						   bool runner_1 = false,
						   bool runner_2 = false,
						   bool runner_3 = false,

						   int ining = 0,
						   bool top_bot = false,
						   int top_score = 0,
						   int bottom_score = 0,
						   int last_ball_type = 0,
						   int last_ball_speed = 0,
						   int res_hit = 0,
						   int res_hit_type = 0,
						   int res_course = 0,
						   int res_course_x = 0,
						   int res_course_y = 0,
						   int res_position = 0,
						   int res_position_x = 0,
						   int res_position_y = 0,
						   int get_score = 0,
						   bool error = false,
						   bool fielder_choice = false,
						   int etc_cd1 = 0,
						   int etc_cd2 = 0,
						   int etc_cd3 = 0,
						   int etc_cd4 = 0,
						   int etc_cd5 = 0,
						   string etc_str1 = "",
						   string etc_str2 = "",
						   string etc_str3 = "",
						   string etc_str4 = "",
						   string etc_str5 = "",
						   //DateTime? etc_date1 = null,
						   //DateTime? etc_date2 = null,
						   //DateTime? etc_date3 = null,
						   //DateTime? etc_date4 = null,
						   //DateTime? etc_date5 = null,
						   int hit_id = 0,
											bool miss_so = false,
											bool swing_so = false,
											   bool walks = false,
											   bool dead = false,
											   bool s_bunt = false,
											   bool s_fly = false,
											int ining_box_id = 1,
											int runner_1_player_id = 0,
						   int runner_2_player_id = 0,
						   int runner_3_player_id = 0,
						   DateTime? update_date = null
							  )
			{
				this.box_id = box_id;
				this.player_id = player_id;
				this.season = season;
				this.position = position;
				this.bat_id = bat_id;
				this.player_num = player_num;
				this.team_id = team_id;
				this.pitcher_id = pitcher_id;
				this.pit_team_id = pit_team_id;

				this.ball_box_num = ball_box_num;
				this.ball_total_num = ball_total_num;
				this.cat_id = cat_id;
				this.ump_id = ump_id;
				this.game_id = game_id;
				this.game_box_num = game_box_num;
				this.park_id = park_id;

				this.pit_throw_id = pit_throw_id;
				this.pit_hand_id = pit_hand_id;
				this.weather_id = weather_id;

				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;



				this.ining = ining;
				this.top_bot = top_bot;
				this.top_score = top_score;
				this.bottom_score = bottom_score;

				this.last_ball_type = last_ball_type;
				this.last_ball_speed = last_ball_speed;
				this.res_hit = res_hit;
				this.res_hit_type = res_hit_type;
				this.res_course = res_course;
				this.res_course_x = res_course_x;
				this.res_course_y = res_course_y;

				this.res_position = res_position;
				this.res_position_x = res_position_x;
				this.res_position_y = res_position_y;
				this.get_score = get_score;
				this.error = error;
				this.fielder_choice = fielder_choice;

				this.update_date = update_date;

				this.etc_cd1 = etc_cd1;             // イニング内打順
				this.etc_cd2 = etc_cd2;             // 前打者のbox_id
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;

				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;

				//this.etc_date1 = etc_date1;
				//this.etc_date2 = etc_date2;
				//this.etc_date3 = etc_date3;
				//this.etc_date4 = etc_date4;
				//this.etc_date5 = etc_date5;
				this.hit_id = hit_id;
				this.miss_so = miss_so;
				this.swing_so = swing_so;
				this.walks = walks;
				this.dead = dead;
				this.s_bunt = s_bunt;
				this.s_fly = s_fly;
				this.ining_box_id = ining_box_id;

				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;

			}

		}
		public static List<boxData> GetRecords(int box_id = 0)
		{
			List<boxData> orderList = new List<boxData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	box_id	,");
				sqlST.AppendLine("	season	,");            // DateTime
				sqlST.AppendLine("	player_id	,");
				sqlST.AppendLine("	team_id	,");
				sqlST.AppendLine("	position	,");        // 5
				sqlST.AppendLine("	player_num	,");
				sqlST.AppendLine("	pitcher_id	,");
				sqlST.AppendLine("	pit_team_id	,");
				sqlST.AppendLine("	ball_box_num	,");
				sqlST.AppendLine("	ball_total_num	,");    // 10
				sqlST.AppendLine("	cat_id	,");
				sqlST.AppendLine("	ump_id	,");
				sqlST.AppendLine("	game_id	,");
				sqlST.AppendLine("	game_box_num	,");
				sqlST.AppendLine("	park_id	,");            // 15
				sqlST.AppendLine("	bat_id	,");
				sqlST.AppendLine("	pit_hand_id	,");
				sqlST.AppendLine("	pit_throw_id	,");
				sqlST.AppendLine("	weather_id	,");
				sqlST.AppendLine("	count_b	,");            // 20
				sqlST.AppendLine("	count_s	,");
				sqlST.AppendLine("	count_o	,");
				sqlST.AppendLine("	runner_1	,");        // bool
				sqlST.AppendLine("	runner_2	,");        // bool
				sqlST.AppendLine("	runner_3	,");        // bool  25
				sqlST.AppendLine("	ining	,");            // bool
				sqlST.AppendLine("	top_bot	,");
				sqlST.AppendLine("	top_score	,");
				sqlST.AppendLine("	bottom_score	,");
				sqlST.AppendLine("	last_ball_type	,");    // 30
				sqlST.AppendLine("	last_ball_speed	,");
				sqlST.AppendLine("	res_hit	,");
				sqlST.AppendLine("	res_hit_type	,");
				sqlST.AppendLine("	res_course	,");
				sqlST.AppendLine("	res_course_x	,");    // 35
				sqlST.AppendLine("	res_course_y	,");
				sqlST.AppendLine("	res_position	,");
				sqlST.AppendLine("	res_position_x	,");
				sqlST.AppendLine("	res_position_y	,");
				sqlST.AppendLine("	get_score	,");        // 40
				sqlST.AppendLine("	error	,");            // bool
				sqlST.AppendLine("	fielder_choice	,");    // bool
				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");            // 45
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str 50
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
															//sqlST.AppendLine("	etc_date1	,");        // DateTime
															//sqlST.AppendLine("	etc_date2	,");        // DateTime
															//sqlST.AppendLine("	etc_date3	,");        // DateTime  55
															//sqlST.AppendLine("	etc_date4	,");        // DateTime
															//sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	hit_id	,");            // Int
				sqlST.AppendLine("		miss_so   , ");
				sqlST.AppendLine("		swing_so   , ");
				sqlST.AppendLine("		walks   , ");
				sqlST.AppendLine("		dead   , ");
				sqlST.AppendLine("		s_bunt   , ");
				sqlST.AppendLine("		s_fly   , ");
				sqlST.AppendLine("		ining_box_id   , ");
				sqlST.AppendLine("		runner_1_player_id   , ");
				sqlST.AppendLine("		runner_2_player_id   , ");
				sqlST.AppendLine("		runner_3_player_id   , ");
				sqlST.AppendLine("	update_date	");         // DateTime
															//sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				if (box_id != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxData(reader.GetInt32(0),
											  reader.GetDateTime(1),
											  reader.GetInt32(2),
											  reader.GetInt32(3),
											  reader.GetInt32(4),
											  reader.GetInt32(5),
											  reader.GetInt32(6),
											  reader.GetInt32(7),
											  reader.GetInt32(8),
											  reader.GetInt32(9),
											  reader.GetInt32(10),
											  reader.GetInt32(11),
											  reader.GetInt32(12),
											  reader.GetInt32(13),
											  reader.GetInt32(14),
											  reader.GetInt32(15),
											  reader.GetInt32(16),
											  reader.GetInt32(17),
											  reader.GetInt32(18),
											  reader.GetInt32(19),
											  reader.GetInt32(20),
											  reader.GetInt32(21),
											  reader.GetBoolean(22),
											  reader.GetBoolean(23),
											  reader.GetBoolean(24),
											  reader.GetInt32(25),
											  reader.GetBoolean(26),
											  reader.GetInt32(27),
											  reader.GetInt32(28),
											  reader.GetInt32(29),
											  reader.GetInt32(30),
											  reader.GetInt32(31),
											  reader.GetInt32(32),
											  reader.GetInt32(33),
											  reader.GetInt32(34),
											  reader.GetInt32(35),
											  reader.GetInt32(36),
											  reader.GetInt32(37),
											  reader.GetInt32(38),
											  reader.GetInt32(39),
											  reader.GetBoolean(40),
											  reader.GetBoolean(41),
											  reader.GetInt32(42),
											  reader.GetInt32(43),
											  reader.GetInt32(44),
											  reader.GetInt32(45),
											  reader.GetInt32(46),
											  reader.GetString(47),
											  reader.GetString(48),
											  reader.GetString(49),
											  reader.GetString(50),
											  reader.GetString(51),
											  //reader.GetDateTime(52),
											  //reader.GetDateTime(53),
											  //reader.GetDateTime(54),
											  //reader.GetDateTime(55),
											  //reader.GetDateTime(56),
											  reader.GetInt32(52),
											  reader.GetBoolean(53),
											  reader.GetBoolean(54),
											  reader.GetBoolean(55),
											  reader.GetBoolean(56),
											  reader.GetBoolean(57),
											  reader.GetBoolean(58),
											  reader.GetInt32(59),
											  reader.GetInt32(60),
											  reader.GetInt32(61),
											  reader.GetInt32(62),
											  reader.GetDateTime(63)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<boxData> GetBoxListRecodes()
		{
			List<boxData> orderList = new List<boxData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	position AS 守備位置	,");        // 5
				sqlST.AppendLine("	player_num AS 背番号	,");
				sqlST.AppendLine("	pitcher_id AS 投手ID	,");
				sqlST.AppendLine("	ball_box_num AS 球数	,");
				sqlST.AppendLine("	ball_total_num AS 投球数	,");    // 10
				sqlST.AppendLine("	count_b	AS ボール,");            // 20
				sqlST.AppendLine("	count_s AS ストライク	,");
				sqlST.AppendLine("	count_o	AS アウト,");
				sqlST.AppendLine("	last_ball_type AS 球種	,");    // 30
				sqlST.AppendLine("	last_ball_speed AS 球速	,");
				sqlST.AppendLine("	res_hit	,");
				sqlST.AppendLine("	res_hit_type	,");
				sqlST.AppendLine("	res_course	,");
				sqlST.AppendLine("	res_course_x	,");    // 35
				sqlST.AppendLine("	res_course_y	,");
				sqlST.AppendLine("	res_position	,");
				sqlST.AppendLine("	res_position_x	,");
				sqlST.AppendLine("	res_position_y	,");
				sqlST.AppendLine("	get_score	,");        // 40
				sqlST.AppendLine("	error	,");            // bool
				sqlST.AppendLine("	fielder_choice	,");    // bool
				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");            // 45
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str 50
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	etc_date1	,");        // DateTime
				sqlST.AppendLine("	etc_date2	,");        // DateTime
				sqlST.AppendLine("	etc_date3	,");        // DateTime  55
				sqlST.AppendLine("	etc_date4	,");        // DateTime
				sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	hit_id	,");            // Int
				sqlST.AppendLine("		miss_so   , ");
				sqlST.AppendLine("		swing_so   , ");
				sqlST.AppendLine("		walks   , ");
				sqlST.AppendLine("		dead   , ");
				sqlST.AppendLine("		s_bunt   , ");
				sqlST.AppendLine("		s_fly   , ");
				sqlST.AppendLine("	update_date	");         // DateTime
															//sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxData(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
											  reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
											  reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
											  reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
											  reader.GetInt32(20), reader.GetInt32(21),
											  reader.GetBoolean(22), reader.GetBoolean(23), reader.GetBoolean(24), reader.GetInt32(25),
											  reader.GetBoolean(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29),
											  reader.GetInt32(30), reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33), reader.GetInt32(34),
											  reader.GetInt32(35), reader.GetInt32(36), reader.GetInt32(37), reader.GetInt32(38), reader.GetInt32(39),
											  reader.GetBoolean(40), reader.GetBoolean(41),
											  reader.GetInt32(42), reader.GetInt32(43), reader.GetInt32(44), reader.GetInt32(45), reader.GetInt32(46),
											  reader.GetString(47), reader.GetString(48), reader.GetString(49), reader.GetString(50), reader.GetString(51),
											  //reader.GetDateTime(52), reader.GetDateTime(53), reader.GetDateTime(54), reader.GetDateTime(55), reader.GetDateTime(56),
											  reader.GetInt32(52),
											  reader.GetBoolean(53),
											  reader.GetBoolean(54),
											  reader.GetBoolean(55),
											  reader.GetBoolean(56),
											  reader.GetBoolean(57),
											  reader.GetBoolean(58),
											  reader.GetInt32(59),
											  reader.GetInt32(60),
											  reader.GetInt32(61),
											  reader.GetInt32(62),
											  reader.GetDateTime(63)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}



		public class boxDataIning
		{

			#region 打席変数
			public int box_id { get; set; }                 // 打席識別番号		1
			public DateTime? season { get; set; }           // 年度 ex)2021		2
			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6

			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号
			public int weather_id { get; set; }             // 天気識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int last_ball_type { get; set; }         // 打席結果の球種
			public int last_ball_speed { get; set; }        // 打席結果の球速
			public int res_hit { get; set; }                // 打席の結果
			public int res_hit_type { get; set; }           // 打球の種類
			public int res_course { get; set; }             // 打ったコース		35
			public int res_course_x { get; set; }           // 打ったコースの詳細 X軸
			public int res_course_y { get; set; }           // 打ったコースの詳細 Y軸
			public int res_position { get; set; }           // 打った守備位置
			public int res_position_x { get; set; }         // 打った守備位置の詳細 X軸
			public int res_position_y { get; set; }         // 打った守備位置の詳細 Y軸		40

			public int get_score { get; set; }              // 打点
			public bool error { get; set; }                 // エラーフラグ (false:0=Nan,treu:1=error )
			public bool fielder_choice { get; set; }        // フィルダーチョイス(false:0=Nan,treu:1=fielder's choice )

			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		45
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		50
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備
			public DateTime? etc_date1 { get; set; }        // 予備		55
			public DateTime? etc_date2 { get; set; }        // 予備
			public DateTime? etc_date3 { get; set; }        // 予備
			public DateTime? etc_date4 { get; set; }        // 予備
			public DateTime? etc_date5 { get; set; }        // 予備

			public int hit_id { get; set; }     // 安打系の識別 / 0:凡打, 1:単打, 2:二塁打, 3:三塁打, 4:本塁打
			public bool miss_so { get; set; }
			public bool swing_so { get; set; }
			public bool walks { get; set; }
			public bool dead { get; set; }
			public bool s_bunt { get; set; }
			public bool s_fly { get; set; }
			public int ining_box_id { get; set; }
			public string player_name { get; set; }
			public string box_result { get; set; }
			//public int order_id { get; set; }
			public string order_id { get; set; }
			public string positionST { get; set; }
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }

			public int ining_score { get; set; }
			#endregion


			public boxDataIning(int box_id = 0,
						   DateTime? season = null,
						   int player_id = 0,
						   int team_id = 0,
						   int position = 0,
						   int player_num = 0,
						   int pitcher_id = 0,
						   int pit_team_id = 0,
						   int ball_box_num = 0,
						   int ball_total_num = 0,
						   int cat_id = 0,
						   int ump_id = 0,
						   int game_id = 0,
						   int game_box_num = 0,
						   int park_id = 0,
						   int bat_id = 0,
						   int pit_hand_id = 0,
						   int pit_throw_id = 0,
						   int weather_id = 0,
						   int count_b = 0,
						   int count_s = 0,
						   int count_o = 0,
						   bool runner_1 = false,
						   bool runner_2 = false,
						   bool runner_3 = false,
						   int ining = 0,
						   bool top_bot = false,
						   int top_score = 0,
						   int bottom_score = 0,
						   int last_ball_type = 0,
						   int last_ball_speed = 0,
						   int res_hit = 0,
						   int res_hit_type = 0,
						   int res_course = 0,
						   int res_course_x = 0,
						   int res_course_y = 0,
						   int res_position = 0,
						   int res_position_x = 0,
						   int res_position_y = 0,
						   int get_score = 0,
						   bool error = false,
						   bool fielder_choice = false,
						   int etc_cd1 = 0,
						   int etc_cd2 = 0,
						   int etc_cd3 = 0,
						   int etc_cd4 = 0,
						   int etc_cd5 = 0,
						   string etc_str1 = "",
						   string etc_str2 = "",
						   string etc_str3 = "",
						   string etc_str4 = "",
						   string etc_str5 = "",
						   int hit_id = 0,
						   bool miss_so = false,
						   bool swing_so = false,
						   bool walks = false,
						   bool dead = false,
						   bool s_bunt = false,
						   bool s_fly = false,
						   int ining_box_id = 1,
						   DateTime? update_date = null,
						   string player_name = "",
						   //int order_id = 0,
						   string order_id = "",

						   string positionST = "",
						   //string box_result = "",
						   int runner_1_player_id = 0,
						   int runner_2_player_id = 0,
						   int runner_3_player_id = 0,
						   int ining_score = 0
							  )
			{
				this.box_id = box_id;
				this.player_id = player_id;
				this.season = season;
				this.position = position;
				this.bat_id = bat_id;
				this.player_num = player_num;
				this.team_id = team_id;
				this.pitcher_id = pitcher_id;
				this.pit_team_id = pit_team_id;

				this.ball_box_num = ball_box_num;
				this.ball_total_num = ball_total_num;
				this.cat_id = cat_id;
				this.ump_id = ump_id;
				this.game_id = game_id;
				this.game_box_num = game_box_num;
				this.park_id = park_id;
				this.pit_throw_id = pit_throw_id;
				this.pit_hand_id = pit_hand_id;
				this.weather_id = weather_id;

				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;
				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.ining = ining;
				this.top_bot = top_bot;
				this.top_score = top_score;
				this.bottom_score = bottom_score;

				this.last_ball_type = last_ball_type;
				this.last_ball_speed = last_ball_speed;
				this.res_hit = res_hit;
				this.res_hit_type = res_hit_type;
				this.res_course = res_course;
				this.res_course_x = res_course_x;
				this.res_course_y = res_course_y;
				this.res_position = res_position;
				this.res_position_x = res_position_x;
				this.res_position_y = res_position_y;

				this.get_score = get_score;
				this.error = error;
				this.fielder_choice = fielder_choice;
				this.update_date = update_date;
				this.etc_cd1 = etc_cd1;             // イニング内打順
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;
				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;

				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;
				this.hit_id = hit_id;
				this.miss_so = miss_so;
				this.swing_so = swing_so;
				this.walks = walks;
				this.dead = dead;
				this.s_bunt = s_bunt;
				this.s_fly = s_fly;

				this.ining_box_id = ining_box_id;
				this.player_name = player_name;
				this.order_id = order_id;
				this.positionST = positionST;

				//this.box_result = box_result;

				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;
				this.ining_score = ining_score;
			}

		}



		public static List<boxDataIning> GetRecordsIningBoxs(int game_id = 0, int ining = 1, bool top_btmFlg = false, int ining_box_id = -1)
		{
			List<boxDataIning> orderList = new List<boxDataIning>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		box.box_id	,");
				sqlST.AppendLine("		box.season	,");            // DateTime
				sqlST.AppendLine("		box.player_id	,");
				sqlST.AppendLine("		box.team_id	,");
				//sqlST.AppendLine("		box.position	,");
				sqlST.AppendLine("		player.position	,");        // 5													
																	//sqlST.AppendLine("		box.player_num	,");
				sqlST.AppendLine("		player.player_num	,");
				sqlST.AppendLine("		box.pitcher_id	,");
				sqlST.AppendLine("		box.pit_team_id	,");
				sqlST.AppendLine("		box.ball_box_num	,");
				sqlST.AppendLine("		box.ball_total_num	,");    // 10
				sqlST.AppendLine("		box.cat_id	,");
				sqlST.AppendLine("		box.ump_id	,");
				sqlST.AppendLine("		box.game_id	,");
				sqlST.AppendLine("		box.game_box_num	,");
				sqlST.AppendLine("		box.park_id	,");            // 15
				sqlST.AppendLine("		box.bat_id	,");
				sqlST.AppendLine("		box.pit_hand_id	,");
				sqlST.AppendLine("		box.pit_throw_id	,");
				sqlST.AppendLine("		box.weather_id	,");
				sqlST.AppendLine("		box.count_b	,");            // 20
				sqlST.AppendLine("		box.count_s	,");
				sqlST.AppendLine("		box.count_o	,");
				sqlST.AppendLine("		box.runner_1	,");        // bool
				sqlST.AppendLine("		box.runner_2	,");        // bool
				sqlST.AppendLine("		box.runner_3	,");        // bool  25
				sqlST.AppendLine("		box.ining	,");            // bool
				sqlST.AppendLine("		box.top_bot	,");
				sqlST.AppendLine("		box.top_score	,");
				sqlST.AppendLine("		box.bottom_score	,");
				sqlST.AppendLine("		box.last_ball_type	,");    // 30
				sqlST.AppendLine("		box.last_ball_speed	,");
				sqlST.AppendLine("		box.res_hit	,");
				sqlST.AppendLine("		box.res_hit_type	,");
				sqlST.AppendLine("		box.res_course	,");
				sqlST.AppendLine("		box.res_course_x	,");    // 35
				sqlST.AppendLine("		box.res_course_y	,");
				sqlST.AppendLine("		box.res_position	,");
				sqlST.AppendLine("		box.res_position_x	,");
				sqlST.AppendLine("		box.res_position_y	,");
				sqlST.AppendLine("		box.get_score	,");        // 40
				sqlST.AppendLine("		box.error	,");            // bool
				sqlST.AppendLine("		box.fielder_choice	,");    // bool
				sqlST.AppendLine("		box.etc_cd1	,");
				sqlST.AppendLine("		box.etc_cd2	,");
				sqlST.AppendLine("		box.etc_cd3	,");            // 45
				sqlST.AppendLine("		box.etc_cd4	,");
				sqlST.AppendLine("		box.etc_cd5	,");
				sqlST.AppendLine("		box.etc_str1	,");        // str
				sqlST.AppendLine("		box.etc_str2	,");        // str
				sqlST.AppendLine("		box.etc_str3	,");        // str 50
				sqlST.AppendLine("		box.etc_str4	,");        // str
				sqlST.AppendLine("		box.etc_str5	,");        // str
				sqlST.AppendLine("		box.hit_id	,");            // Int
				sqlST.AppendLine("		box.miss_so   , ");
				sqlST.AppendLine("		box.swing_so   , ");
				sqlST.AppendLine("		box.walks   , ");
				sqlST.AppendLine("		box.dead   , ");
				sqlST.AppendLine("		box.s_bunt   , ");
				sqlST.AppendLine("		box.s_fly   , ");
				sqlST.AppendLine("		box.ining_box_id   , ");
				sqlST.AppendLine("		box.update_date	,	");         // DateTime
				sqlST.AppendLine("		player.player_name	, 	");
				sqlST.AppendLine("		player.etc_str2	, 	");
				sqlST.AppendLine("		player.etc_str1 ,	");

				sqlST.AppendLine("		box.runner_1_player_id   , ");
				sqlST.AppendLine("		box.runner_2_player_id   , ");
				sqlST.AppendLine("		box.runner_3_player_id ,    ");
				sqlST.AppendLine("		box.ining_score    ");
				//sqlST.AppendLine("		box.update_date	");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("	ON player.player_id=box.player_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.ining={0} ", ining).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.top_bot={0} ", top_btmFlg).AppendLine();
				if (ining_box_id > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.ining_box_id={0} ", ining_box_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxDataIning(box_id: reader.GetInt32(0),
											  season: reader.GetDateTime(1),
											  player_id: reader.GetInt32(2),
											  team_id: reader.GetInt32(3),
											  position: reader.GetInt32(4),
											  player_num: reader.GetInt32(5),
											  pitcher_id: reader.GetInt32(6),
											  pit_team_id: reader.GetInt32(7),
											  ball_box_num: reader.GetInt32(8),
											  ball_total_num: reader.GetInt32(9),
											  cat_id: reader.GetInt32(10),
											  ump_id: reader.GetInt32(11),
											  game_id: reader.GetInt32(12),
											  game_box_num: reader.GetInt32(13),
											  park_id: reader.GetInt32(14),
											  bat_id: reader.GetInt32(15),
											  pit_hand_id: reader.GetInt32(16),
											  pit_throw_id: reader.GetInt32(17),
											  weather_id: reader.GetInt32(18),
											  count_b: reader.GetInt32(19),
											  count_s: reader.GetInt32(20),
											  count_o: reader.GetInt32(21),
											  runner_1: reader.GetBoolean(22),
											  runner_2: reader.GetBoolean(23),
											  runner_3: reader.GetBoolean(24),
											  ining: reader.GetInt32(25),
											  top_bot: reader.GetBoolean(26),
											  top_score: reader.GetInt32(27),
											  bottom_score: reader.GetInt32(28),
											  last_ball_type: reader.GetInt32(29),
											  last_ball_speed: reader.GetInt32(30),
											  res_hit: reader.GetInt32(31),
											  res_hit_type: reader.GetInt32(32),
											  res_course: reader.GetInt32(33),
											  res_course_x: reader.GetInt32(34),
											  res_course_y: reader.GetInt32(35),
											  res_position: reader.GetInt32(36),
											  res_position_x: reader.GetInt32(37),
											  res_position_y: reader.GetInt32(38),
											  get_score: reader.GetInt32(39),
											  error: reader.GetBoolean(40),
											  fielder_choice: reader.GetBoolean(41),
											  etc_cd1: reader.GetInt32(42),
											  etc_cd2: reader.GetInt32(43),
											  etc_cd3: reader.GetInt32(44),
											  etc_cd4: reader.GetInt32(45),
											  etc_cd5: reader.GetInt32(46),
											  etc_str1: reader.GetString(47),
											  etc_str2: reader.GetString(48),
											  etc_str3: reader.GetString(49),
											  etc_str4: reader.GetString(50),
											  etc_str5: reader.GetString(51),
											  hit_id: reader.GetInt32(52),
											  miss_so: reader.GetBoolean(53),
											  swing_so: reader.GetBoolean(54),
											  walks: reader.GetBoolean(55),
											  dead: reader.GetBoolean(56),
											  s_bunt: reader.GetBoolean(57),
											  s_fly: reader.GetBoolean(58),
											  ining_box_id: reader.GetInt32(59),
											  update_date: reader.GetDateTime(60),
											  player_name: reader.GetString(61),
											  //order_id: reader.GetInt32(62),
											  order_id: reader.GetString(62),
											  positionST: reader.GetString(63),
											  runner_1_player_id: reader.GetInt32(64),
											  runner_2_player_id: reader.GetInt32(65),
											  runner_3_player_id: reader.GetInt32(66),
											  ining_score: reader.GetInt32(67)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<boxDataIning> GetRecordsDataAnalysis(
												int player_id = -1,
												int pitcher_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int pit_hand_id = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												bool course_I = false
												,
												int field_direct_id_left = -1,
												int field_direct_id_center = -1,
												int field_direct_id_right = -1,
												bool res_hit_flg = false,
												bool res_hit_out = false,
												bool res_hit_hit = false,
												bool res_hit_error = false,
												bool res_hit_other = false,
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<boxDataIning> orderList = new List<boxDataIning>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		box.box_id	,");
				sqlST.AppendLine("		box.season	,");            // DateTime
				sqlST.AppendLine("		box.player_id	,");
				sqlST.AppendLine("		box.team_id	,");
				sqlST.AppendLine("		box.position	,");        // 5
				sqlST.AppendLine("		box.player_num	,");
				sqlST.AppendLine("		box.pitcher_id	,");
				sqlST.AppendLine("		box.pit_team_id	,");
				sqlST.AppendLine("		box.ball_box_num	,");
				sqlST.AppendLine("		box.ball_total_num	,");    // 10
				sqlST.AppendLine("		box.cat_id	,");
				sqlST.AppendLine("		box.ump_id	,");
				sqlST.AppendLine("		box.game_id	,");
				sqlST.AppendLine("		box.game_box_num	,");
				sqlST.AppendLine("		box.park_id	,");            // 15
				sqlST.AppendLine("		box.bat_id	,");
				sqlST.AppendLine("		box.pit_hand_id	,");
				sqlST.AppendLine("		box.pit_throw_id	,");
				sqlST.AppendLine("		box.weather_id	,");
				sqlST.AppendLine("		box.count_b	,");            // 20
				sqlST.AppendLine("		box.count_s	,");
				sqlST.AppendLine("		box.count_o	,");
				sqlST.AppendLine("		box.runner_1	,");        // bool
				sqlST.AppendLine("		box.runner_2	,");        // bool
				sqlST.AppendLine("		box.runner_3	,");        // bool  25
				sqlST.AppendLine("		box.ining	,");            // bool
				sqlST.AppendLine("		box.top_bot	,");
				sqlST.AppendLine("		box.top_score	,");
				sqlST.AppendLine("		box.bottom_score	,");
				sqlST.AppendLine("		box.last_ball_type	,");    // 30
				sqlST.AppendLine("		box.last_ball_speed	,");
				sqlST.AppendLine("		box.res_hit	,");
				sqlST.AppendLine("		box.res_hit_type	,");
				sqlST.AppendLine("		box.res_course	,");
				sqlST.AppendLine("		box.res_course_x	,");    // 35
				sqlST.AppendLine("		box.res_course_y	,");
				sqlST.AppendLine("		box.res_position	,");
				sqlST.AppendLine("		box.res_position_x	,");
				sqlST.AppendLine("		box.res_position_y	,");
				sqlST.AppendLine("		box.get_score	,");        // 40
				sqlST.AppendLine("		box.error	,");            // bool
				sqlST.AppendLine("		box.fielder_choice	,");    // bool
				sqlST.AppendLine("		box.etc_cd1	,");
				sqlST.AppendLine("		box.etc_cd2	,");
				sqlST.AppendLine("		box.etc_cd3	,");            // 45
				sqlST.AppendLine("		box.etc_cd4	,");
				sqlST.AppendLine("		box.etc_cd5	,");
				sqlST.AppendLine("		box.etc_str1	,");        // str
				sqlST.AppendLine("		box.etc_str2	,");        // str
				sqlST.AppendLine("		box.etc_str3	,");        // str 50
				sqlST.AppendLine("		box.etc_str4	,");        // str
				sqlST.AppendLine("		box.etc_str5	,");        // str
				sqlST.AppendLine("		box.hit_id	,");            // Int
				sqlST.AppendLine("		box.miss_so   , ");
				sqlST.AppendLine("		box.swing_so   , ");
				sqlST.AppendLine("		box.walks   , ");
				sqlST.AppendLine("		box.dead   , ");
				sqlST.AppendLine("		box.s_bunt   , ");
				sqlST.AppendLine("		box.s_fly   , ");
				sqlST.AppendLine("		box.ining_box_id   , ");
				sqlST.AppendLine("		box.update_date	,	");         // DateTime
				sqlST.AppendLine("		player.player_name	, 	");
				sqlST.AppendLine("		player.etc_str2	, 	");
				sqlST.AppendLine("		player.etc_str1 ,	");

				sqlST.AppendLine("		box.runner_1_player_id   , ");
				sqlST.AppendLine("		box.runner_2_player_id   , ");
				sqlST.AppendLine("		box.runner_3_player_id ,    ");
				sqlST.AppendLine("		box.ining_score    ");
				//sqlST.AppendLine("		box.update_date	");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	( ");
				sqlST.AppendLine("	SELECT ");
				sqlST.AppendLine("		* ");
				sqlST.AppendLine("	FROM ");
				sqlST.AppendLine("		ball ");
				sqlST.AppendLine("	ORDER BY ");
				//sqlST.AppendLine("		ball_id DESC ");
				sqlST.AppendLine("		ball_id ");
				sqlST.AppendLine("	) AS ball ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.box_id=box.box_id ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	ball.in_play=1 ");

				//sqlST.AppendLine("	ball ");
				//sqlST.AppendLine("	ON ");
				//sqlST.AppendLine("	ball.box_id=box.box_id ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("	ON player.player_id=box.player_id ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	box.pit_hand_id=balltype.hand ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	box.last_ball_type=balltype.ball_type_id ");

				/// 2022.5.19
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	box.box_id=box_field_dir.box_id ");

				/// 2022.5.21
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	box.box_id=box_result.box_id ");

				sqlST.AppendLine("WHERE ");
				if (pitcher_id >= 0)
				{
					sqlST.AppendFormat("	box.pitcher_id={0} ", pitcher_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendFormat("	box.player_id={0} ", player_id).AppendLine();
				}

				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>6 ");


				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}


				if (!(bat < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.bat_id={0} ", bat).AppendLine();
				}
				if (!(pit_hand_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.pit_hand_id={0} ", pit_hand_id).AppendLine();
				}


				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(box.runner_1=1 OR box.runner_2=1 OR box.runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.last_ball_type={0} ", ball_type).AppendLine();
				}

				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ball_action={0} ", ball_action).AppendLine();
				}

				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.last_ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.last_ball_speed<={0}", max_ball_speed).AppendLine();
				}

				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (!(course_id < 0))
				{
					//sqlST.AppendLine("	AND ");
					//sqlST.AppendFormat("	ball.cource_table_id={0} ", course_id).AppendLine();
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}

				//if(dir_left>=0 || dir_center >=0 || dir_right >= 0) 
				//{
				//	sqlST.AppendLine("	AND ");
				//	sqlST.AppendLine("		( ");
				//	sqlST.AppendLine("		1=1 ");
				//	if (dir_left >= 0) 
				//	{
				//		sqlST.AppendLine("		AND ");
				//		sqlST.AppendLine("		 ");
				//	}


				//	sqlST.AppendLine("		( ");

				//}


				//sqlST.AppendLine("INNER JOIN ");
				//sqlST.AppendLine("	player ");
				//sqlST.AppendLine("	ON player.player_id=box.player_id ");
				//sqlST.AppendLine("WHERE ");
				//sqlST.AppendLine("	1=1 ");
				//sqlST.AppendLine("	AND ");
				//sqlST.AppendFormat("	box.game_id={0} ", game_id).AppendLine();
				//sqlST.AppendLine("	AND ");
				//sqlST.AppendFormat("	box.ining={0} ", ining).AppendLine();
				//sqlST.AppendLine("	AND ");
				//sqlST.AppendFormat("	box.top_bot={0} ", top_btmFlg).AppendLine();
				//if (ining_box_id > 0)
				//{
				//	sqlST.AppendLine("	AND ");
				//	sqlST.AppendFormat("	box.ining_box_id={0} ", ining_box_id).AppendLine();
				//}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxDataIning(box_id: reader.GetInt32(0),
											  season: reader.GetDateTime(1),
											  player_id: reader.GetInt32(2),
											  team_id: reader.GetInt32(3),
											  position: reader.GetInt32(4),
											  player_num: reader.GetInt32(5),
											  pitcher_id: reader.GetInt32(6),
											  pit_team_id: reader.GetInt32(7),
											  ball_box_num: reader.GetInt32(8),
											  ball_total_num: reader.GetInt32(9),
											  cat_id: reader.GetInt32(10),
											  ump_id: reader.GetInt32(11),
											  game_id: reader.GetInt32(12),
											  game_box_num: reader.GetInt32(13),
											  park_id: reader.GetInt32(14),
											  bat_id: reader.GetInt32(15),
											  pit_hand_id: reader.GetInt32(16),
											  pit_throw_id: reader.GetInt32(17),
											  weather_id: reader.GetInt32(18),
											  count_b: reader.GetInt32(19),
											  count_s: reader.GetInt32(20),
											  count_o: reader.GetInt32(21),
											  runner_1: reader.GetBoolean(22),
											  runner_2: reader.GetBoolean(23),
											  runner_3: reader.GetBoolean(24),
											  ining: reader.GetInt32(25),
											  top_bot: reader.GetBoolean(26),
											  top_score: reader.GetInt32(27),
											  bottom_score: reader.GetInt32(28),
											  last_ball_type: reader.GetInt32(29),
											  last_ball_speed: reader.GetInt32(30),
											  res_hit: reader.GetInt32(31),
											  res_hit_type: reader.GetInt32(32),
											  res_course: reader.GetInt32(33),
											  res_course_x: reader.GetInt32(34),
											  res_course_y: reader.GetInt32(35),
											  res_position: reader.GetInt32(36),
											  res_position_x: reader.GetInt32(37),
											  res_position_y: reader.GetInt32(38),
											  get_score: reader.GetInt32(39),
											  error: reader.GetBoolean(40),
											  fielder_choice: reader.GetBoolean(41),
											  etc_cd1: reader.GetInt32(42),
											  etc_cd2: reader.GetInt32(43),
											  etc_cd3: reader.GetInt32(44),
											  etc_cd4: reader.GetInt32(45),
											  etc_cd5: reader.GetInt32(46),
											  etc_str1: reader.GetString(47),
											  etc_str2: reader.GetString(48),
											  etc_str3: reader.GetString(49),
											  etc_str4: reader.GetString(50),
											  etc_str5: reader.GetString(51),
											  hit_id: reader.GetInt32(52),
											  miss_so: reader.GetBoolean(53),
											  swing_so: reader.GetBoolean(54),
											  walks: reader.GetBoolean(55),
											  dead: reader.GetBoolean(56),
											  s_bunt: reader.GetBoolean(57),
											  s_fly: reader.GetBoolean(58),
											  ining_box_id: reader.GetInt32(59),
											  update_date: reader.GetDateTime(60),
											  player_name: reader.GetString(61),
											  //order_id: reader.GetInt32(62),
											  order_id: reader.GetString(62),
											  positionST: reader.GetString(63),
											  runner_1_player_id: reader.GetInt32(64),
											  runner_2_player_id: reader.GetInt32(65),
											  runner_3_player_id: reader.GetInt32(66),
											  ining_score: reader.GetInt32(67)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}


		public static List<boxDataIning> GetRecordsIningLast(int game_id = 0, int ining = 1, bool top_btmFlg = false, int ining_box_id = -1)
		{
			List<boxDataIning> orderList = new List<boxDataIning>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		box.box_id	,");
				sqlST.AppendLine("		box.season	,");            // DateTime
				sqlST.AppendLine("		box.player_id	,");
				sqlST.AppendLine("		box.team_id	,");
				sqlST.AppendLine("		box.position	,");        // 5
				sqlST.AppendLine("		box.player_num	,");
				sqlST.AppendLine("		box.pitcher_id	,");
				sqlST.AppendLine("		box.pit_team_id	,");
				sqlST.AppendLine("		box.ball_box_num	,");
				sqlST.AppendLine("		box.ball_total_num	,");    // 10
				sqlST.AppendLine("		box.cat_id	,");
				sqlST.AppendLine("		box.ump_id	,");
				sqlST.AppendLine("		box.game_id	,");
				sqlST.AppendLine("		box.game_box_num	,");
				sqlST.AppendLine("		box.park_id	,");            // 15
				sqlST.AppendLine("		box.bat_id	,");
				sqlST.AppendLine("		box.pit_hand_id	,");
				sqlST.AppendLine("		box.pit_throw_id	,");
				sqlST.AppendLine("		box.weather_id	,");
				sqlST.AppendLine("		box.count_b	,");            // 20
				sqlST.AppendLine("		box.count_s	,");
				sqlST.AppendLine("		box.count_o	,");
				sqlST.AppendLine("		box.runner_1	,");        // bool
				sqlST.AppendLine("		box.runner_2	,");        // bool
				sqlST.AppendLine("		box.runner_3	,");        // bool  25
				sqlST.AppendLine("		box.ining	,");            // bool
				sqlST.AppendLine("		box.top_bot	,");
				sqlST.AppendLine("		box.top_score	,");
				sqlST.AppendLine("		box.bottom_score	,");
				sqlST.AppendLine("		box.last_ball_type	,");    // 30
				sqlST.AppendLine("		box.last_ball_speed	,");
				sqlST.AppendLine("		box.res_hit	,");
				sqlST.AppendLine("		box.res_hit_type	,");
				sqlST.AppendLine("		box.res_course	,");
				sqlST.AppendLine("		box.res_course_x	,");    // 35
				sqlST.AppendLine("		box.res_course_y	,");
				sqlST.AppendLine("		box.res_position	,");
				sqlST.AppendLine("		box.res_position_x	,");
				sqlST.AppendLine("		box.res_position_y	,");
				sqlST.AppendLine("		box.get_score	,");        // 40
				sqlST.AppendLine("		box.error	,");            // bool
				sqlST.AppendLine("		box.fielder_choice	,");    // bool
				sqlST.AppendLine("		box.etc_cd1	,");
				sqlST.AppendLine("		box.etc_cd2	,");
				sqlST.AppendLine("		box.etc_cd3	,");            // 45
				sqlST.AppendLine("		box.etc_cd4	,");
				sqlST.AppendLine("		box.etc_cd5	,");
				sqlST.AppendLine("		box.etc_str1	,");        // str
				sqlST.AppendLine("		box.etc_str2	,");        // str
				sqlST.AppendLine("		box.etc_str3	,");        // str 50
				sqlST.AppendLine("		box.etc_str4	,");        // str
				sqlST.AppendLine("		box.etc_str5	,");        // str
				sqlST.AppendLine("		box.hit_id	,");            // Int
				sqlST.AppendLine("		box.miss_so   , ");
				sqlST.AppendLine("		box.swing_so   , ");
				sqlST.AppendLine("		box.walks   , ");
				sqlST.AppendLine("		box.dead   , ");
				sqlST.AppendLine("		box.s_bunt   , ");
				sqlST.AppendLine("		box.s_fly   , ");
				sqlST.AppendLine("		box.ining_box_id   , ");
				sqlST.AppendLine("		box.update_date	,	");         // DateTime
				sqlST.AppendLine("		player.player_name	, 	");
				sqlST.AppendLine("		player.etc_str2	, 	");
				sqlST.AppendLine("		player.etc_str1 ,	");
				sqlST.AppendLine("		box.runner_1_player_id   , ");
				sqlST.AppendLine("		box.runner_2_player_id   , ");
				sqlST.AppendLine("		box.runner_3_player_id    ");
				//sqlST.AppendLine("		box.update_date	");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	box ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	player ");
				sqlST.AppendLine("	ON player.player_id=box.player_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.ining={0} ", ining).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	box.top_bot={0} ", top_btmFlg).AppendLine();
				if (ining_box_id > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box.ining_box_id={0} ", ining_box_id).AppendLine();
				}
				sqlST.AppendLine("	ORDER BY ");
				sqlST.AppendLine("		ining_box_id DESC");
				sqlST.AppendLine("	LIMIT 1");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new boxDataIning(reader.GetInt32(0),
											  reader.GetDateTime(1),
											  reader.GetInt32(2),
											  reader.GetInt32(3),
											  reader.GetInt32(4),
											  reader.GetInt32(5),
											  reader.GetInt32(6),
											  reader.GetInt32(7),
											  reader.GetInt32(8),
											  reader.GetInt32(9),
											  reader.GetInt32(10),
											  reader.GetInt32(11),
											  reader.GetInt32(12),
											  reader.GetInt32(13),
											  reader.GetInt32(14),
											  reader.GetInt32(15),
											  reader.GetInt32(16),
											  reader.GetInt32(17),
											  reader.GetInt32(18),
											  reader.GetInt32(19),
											  reader.GetInt32(20),
											  reader.GetInt32(21),
											  reader.GetBoolean(22),
											  reader.GetBoolean(23),
											  reader.GetBoolean(24),
											  reader.GetInt32(25),
											  reader.GetBoolean(26),
											  reader.GetInt32(27),
											  reader.GetInt32(28),
											  reader.GetInt32(29),
											  reader.GetInt32(30),
											  reader.GetInt32(31),
											  reader.GetInt32(32),
											  reader.GetInt32(33),
											  reader.GetInt32(34),
											  reader.GetInt32(35),
											  reader.GetInt32(36),
											  reader.GetInt32(37),
											  reader.GetInt32(38),
											  reader.GetInt32(39),
											  reader.GetBoolean(40),
											  reader.GetBoolean(41),
											  reader.GetInt32(42),
											  reader.GetInt32(43),
											  reader.GetInt32(44),
											  reader.GetInt32(45),
											  reader.GetInt32(46),
											  reader.GetString(47),
											  reader.GetString(48),
											  reader.GetString(49),
											  reader.GetString(50),
											  reader.GetString(51),
											  reader.GetInt32(52),
											  reader.GetBoolean(53),
											  reader.GetBoolean(54),
											  reader.GetBoolean(55),
											  reader.GetBoolean(56),
											  reader.GetBoolean(57),
											  reader.GetBoolean(58),
											  reader.GetInt32(59),
											  reader.GetDateTime(60),
											  player_name: reader.GetString(61),
											  //order_id: reader.GetInt32(62),
											  order_id: reader.GetString(62),
											  positionST: reader.GetString(63),
											  runner_1_player_id: reader.GetInt32(64),
											  runner_2_player_id: reader.GetInt32(65),
											  runner_3_player_id: reader.GetInt32(66)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}




	}
	#endregion

	#region Ball Action
	class BallAction
	{
		public BallAction()
		{

		}
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	ballaction ( ");
				sqlST.AppendLine("	ball_action_id	, ");
				sqlST.AppendLine("	ball_action	 ");
				sqlST.AppendLine("	)	 ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		public static void addRecord(int id = 0, string action = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	ballaction ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		ball_action_id , ");
				sqlST.AppendLine("		ball_action ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("		@ball_action_id , ");
				CMD_Insert.Parameters.AddWithValue("@ball_action_id", id);
				sqlST.AppendLine("		@ball_action ");
				CMD_Insert.Parameters.AddWithValue("@ball_action", action);
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}
		public class ballAction
		{
			public int ball_action_id { get; set; }                // 投球アクション識別番号
			public string ball_action { get; set; }                 // 投球アクション

			public ballAction(int ball_action_id = 0, string ball_action = "")
			{
				this.ball_action_id = ball_action_id;
				this.ball_action = ball_action;
			}
		}

		public static List<ballAction> GetRecords()
		{
			List<ballAction> orderList = new List<ballAction>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ballaction ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballAction(reader.GetInt32(0), reader.GetString(1)));
				}
				con.Close();
			}
			return orderList;
		}
	}

	#endregion

	#region 打球タイプ
	class HitType
	{
		public HitType()
		{

		}
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	hit_type ( ");
				sqlST.AppendLine("	hit_type_id	, ");
				sqlST.AppendLine("	hit_type		 ");
				sqlST.AppendLine("	)	 ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		public static void addRecord(int id = 0, string type = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	hit_type ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		hit_type_id , ");
				sqlST.AppendLine("		hit_type  ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("		{0} , ", id).AppendLine();
				sqlST.AppendFormat("		'{0}'  ", type).AppendLine();
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int id = 0, string type = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("UPDATE SET ");
				sqlST.AppendLine("	hit_type ");
				sqlST.AppendFormat("		hit_type='{0}'  ", type).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		hit_type_id={0} , ", id).AppendLine();
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public class hitType
		{
			public int hit_type_id { get; set; }                // 打球識別番号
			public string hit_type { get; set; }                 // 打球アクション
			public hitType(int id = 0, string type = "", int hand = 0)
			{
				this.hit_type_id = id;
				this.hit_type = type;
			}
		}

		public static List<hitType> GetRecords()
		{
			List<hitType> orderList = new List<hitType>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	hit_type ");
				//sqlST.AppendLine("WHERE ");
				//sqlST.AppendLine("	hit_type_id ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new hitType(reader.GetInt32(0), reader.GetString(1)));
				}
				con.Close();
			}
			return orderList;
		}
	}

	#endregion

	#region 球種
	class BallType
	{
		public BallType()
		{

		}
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	balltype ( ");
				sqlST.AppendLine("	ball_type_id	, ");
				sqlST.AppendLine("	ball_type	,	 ");
				sqlST.AppendLine("	hand	, ");
				sqlST.AppendLine("	ball_order	, ");
				sqlST.AppendLine("	ball_color	,	 ");
				sqlST.AppendLine("	ball_img	 ");
				sqlST.AppendLine("	)	 ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		public static void addRecord(
								int id = 0,
								string type = "",
								int hand = 0,
								int ball_order = 0,
								int ball_color = 0,
								int ball_img = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		ball_type_id , ");
				sqlST.AppendLine("		ball_type , ");
				sqlST.AppendLine("		hand	, ");
				sqlST.AppendLine("		ball_order	, ");
				sqlST.AppendLine("		ball_color	,	 ");
				sqlST.AppendLine("		ball_img	 ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("		@ball_type_id , ");
				CMD_Insert.Parameters.AddWithValue("@ball_type_id", id);
				sqlST.AppendLine("		@ball_type , ");
				CMD_Insert.Parameters.AddWithValue("@ball_type", type);
				sqlST.AppendLine("		@hand , ");
				CMD_Insert.Parameters.AddWithValue("@hand", hand);
				sqlST.AppendLine("		@ball_order , ");
				CMD_Insert.Parameters.AddWithValue("@ball_order", ball_order);
				sqlST.AppendLine("		@ball_color , ");
				CMD_Insert.Parameters.AddWithValue("@ball_color", ball_color);
				sqlST.AppendLine("		@ball_img ");
				CMD_Insert.Parameters.AddWithValue("@ball_img", ball_img);
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(
									int before_id = -1,
									string before_type = "",
									int before_order = 0,
									int before_img = 0,
									int after_id = 0,
									string after_type = "",
									int after_order = 0,
									int after_img = 0,
									int hand = 0
									)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			if (before_id < 0) { return; }
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("UPDATE	");
				sqlST.AppendLine("	balltype	");
				sqlST.AppendLine("SET	");
				sqlST.AppendFormat("		ball_type_id = {0}  , ", after_id).AppendLine();
				sqlST.AppendFormat("		ball_type	 ='{0}' , ", after_type).AppendLine();
				sqlST.AppendFormat("		ball_order	 = {0}  , ", after_order).AppendLine();
				sqlST.AppendFormat("		ball_img	 = {0}   ", after_img).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1	");
				sqlST.AppendLine("	AND	");
				sqlST.AppendFormat("		ball_type_id={0}  ", before_id).AppendLine();
				//sqlST.AppendLine("	AND	");
				//sqlST.AppendFormat("		ball_type	={0}  ", before_type).AppendLine();
				sqlST.AppendLine("	AND	");
				sqlST.AppendFormat("		hand		={0}  ", hand).AppendLine();
				//sqlST.AppendLine("	AND	");
				//sqlST.AppendFormat("		ball_order	={0}  ", before_order).AppendLine();
				//sqlST.AppendLine("	AND	");
				//sqlST.AppendFormat("		ball_img	={0}  ", before_img).AppendLine();
				//sqlST.AppendFormat("		ball_color	={0} , ").AppendLine();
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}


		public class ballTypeImageCode
		{
			public int ball_img { get; set; }
			public ballTypeImageCode(int ball_img = 0)
			{
				this.ball_img = ball_img;
			}
		}

		public class ballTypeIdCount
		{
			public int ball_type_count { get; set; }
			public ballTypeIdCount(int ball_type_count = 0)
			{
				this.ball_type_count = ball_type_count;
			}
		}

		public static List<ballTypeIdCount> GetRecordsBallTypeIdCount(int hand = 0, bool flg = false)
		{
			List<ballTypeIdCount> orderList = new List<ballTypeIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	MAX( ");
				sqlST.AppendLine("	DISTINCT ");
				if (flg)
				{
					sqlST.AppendLine("	ball_order ");
				}
				else
				{
					sqlST.AppendLine("	ball_type_id ");
				}
				sqlST.AppendLine("	) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	hand={0} ", hand).AppendLine();

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballTypeIdCount(
							ball_type_count: reader.GetInt32(0)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class ballType
		{
			public int ball_type_id { get; set; }                // 投球アクション識別番号
			public string ball_type { get; set; }                 // 投球アクション
			public int hand { get; set; }
			public int ball_order { get; set; }
			public int ball_color { get; set; }
			public int ball_img { get; set; }
			public ballType(
								int id = 0,
								string type = "",
								int hand = 0,
								int ball_order = 0,
								int ball_color = 0,
								int ball_img = 0
				)
			{
				this.ball_type_id = id;
				this.ball_type = type;
				this.hand = hand;
				this.ball_order = ball_order;
				this.ball_color = ball_color;
				this.ball_img = ball_img;
			}
		}

		public static List<ballType> GetRecords()
		{
			List<ballType> orderList = new List<ballType>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	balltype ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballType(
							reader.GetInt32(0),
							reader.GetString(1),
							reader.GetInt32(2),
							reader.GetInt32(3),
							reader.GetInt32(4),
							reader.GetInt32(5)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<ballTypeImageCode> GetRecordsBallTypeCode(int hand = 0)
		{
			List<ballTypeImageCode> orderList = new List<ballTypeImageCode>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	DISTINCT ");
				sqlST.AppendLine("	ball_img ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	hand={0} ", hand).AppendLine();
				sqlST.AppendLine("ORDER BY	");
				//sqlST.AppendLine("	ball_type_id ");
				sqlST.AppendLine("	ball_img ");

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballTypeImageCode(
							ball_img: reader.GetInt32(0)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<ballType> GetRecordsBallType(int hand = 0)
		{
			List<ballType> orderList = new List<ballType>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	hand={0} ", hand).AppendLine();
				sqlST.AppendLine("ORDER BY	");
				//sqlST.AppendLine("	ball_type_id ");
				sqlST.AppendLine("	ball_order ");

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballType(
							reader.GetInt32(0),
							reader.GetString(1),
							reader.GetInt32(2),
							reader.GetInt32(3),
							reader.GetInt32(4),
							reader.GetInt32(5)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


	}



	#endregion

	#region ゾーン(コース)
	class BallCourse
	{
		public BallCourse()
		{

		}
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	course ( ");
				sqlST.AppendLine("	course_id	, ");
				sqlST.AppendLine("	course	,	 ");
				sqlST.AppendLine("	bat	 ");
				sqlST.AppendLine("	)	 ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		public static void addRecord(int id = 0, string course = "", int bat = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	course ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		course_id , ");
				sqlST.AppendLine("		course	, ");
				sqlST.AppendLine("		bat ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("		@course_id , ");
				CMD_Insert.Parameters.AddWithValue("@course_id", id);
				sqlST.AppendLine("		@course,  ");
				CMD_Insert.Parameters.AddWithValue("@course", course);
				sqlST.AppendLine("		@bat  ");
				CMD_Insert.Parameters.AddWithValue("@bat", bat);
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int id = 0, string course = "", int bat = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;

				sqlST.AppendLine("UPDATE course ");
				sqlST.AppendLine("	SET ");

				sqlST.AppendLine("		course= ");
				sqlST.AppendLine("		@course  ");
				CMD_Update.Parameters.AddWithValue("@course", course);
				sqlST.AppendLine("		bat= ");
				sqlST.AppendLine("		@bat  ");
				CMD_Update.Parameters.AddWithValue("@bat", bat);
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		course_id=@course_id  ");
				CMD_Update.Parameters.AddWithValue("@course_id", id);
				sqlST.AppendLine(" ); ");
				CMD_Update.CommandText = sqlST.ToString();
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public class ballCourse
		{
			public int course_id { get; set; }                // 識別番号
			public string course { get; set; }                 // 名前
			public int bat { get; set; }                 //　指定
			public ballCourse(int id = 0, string course = "", int bat = 0)
			{
				this.course_id = id;
				this.course = course;
				this.bat = bat;
			}
		}

		public static List<ballCourse> GetRecords(int bat = 0)
		{
			List<ballCourse> orderList = new List<ballCourse>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	course ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	bat={0} ", bat).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballCourse(
								reader.GetInt32(0),
								reader.GetString(1),
								reader.GetInt32(2)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}

	#endregion

	#region 色
	class BallCollor
	{
		public BallCollor()
		{

		}
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	color ( ");
				sqlST.AppendLine("	color_id	, ");
				sqlST.AppendLine("	color		 ");
				sqlST.AppendLine("	)	 ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		public static void addRecord(int id = 0, string color = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	color ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		color_id , ");
				sqlST.AppendLine("		color ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("		@color_id , ");
				CMD_Insert.Parameters.AddWithValue("@color_id", id);
				sqlST.AppendLine("		@color  ");
				CMD_Insert.Parameters.AddWithValue("@color", color);
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int id = 0, string color = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;

				sqlST.AppendLine("UPDATE  ");
				sqlST.AppendLine("	color ");
				sqlST.AppendLine("SET ");

				sqlST.AppendLine("		color= ");
				sqlST.AppendLine("		@color  ");
				CMD_Update.Parameters.AddWithValue("@color", color);
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		colore_id=@color_id  ");
				CMD_Update.Parameters.AddWithValue("@color_id", id);
				sqlST.AppendLine(" ); ");
				CMD_Update.CommandText = sqlST.ToString();
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public class ballColor
		{
			public int color_id { get; set; }                // 投球アクション識別番号
			public string color { get; set; }                 // 投球アクション
			public ballColor(int id = 0, string color = "")
			{
				this.color_id = id;
				this.color = color;
			}
		}

		public static List<ballColor> GetRecords()
		{
			List<ballColor> orderList = new List<ballColor>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	color ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballColor(reader.GetInt32(0), reader.GetString(1)));
				}
				con.Close();
			}
			return orderList;
		}
	}

	#endregion

	#region ゲーム
	class GameData
	{

		public GameData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	game ( ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		park_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ump_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		weather_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		start_datetime    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		bat_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		field_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_change_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_btm_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_b    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_s    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_o    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining_box_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_play_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd5    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(
									int game_id,
									int park_id,
									int ump_id,
									int weather_id,
									DateTime start_datetime,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "0",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "0",
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "1",
									string btm_order_id = "1",
									string ining = "1",
									int count_b = 0,
									int count_s = 0,
									int count_o = 0,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = 0,
									int run_2_player_id = 0,
									int run_3_player_id = 0,
									int player_id = 0,
									int ining_box_id = 1,
									int run_play_id = 0,
									int etc_cd1 = 0,
									int etc_cd2 = 0,
									int etc_cd3 = 0,
									int etc_cd4 = 0,
									int etc_cd5 = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		park_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ump_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		weather_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		start_datetime    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		bat_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		field_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_change_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_btm_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_b    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_s    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_o    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining_box_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_play_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd5    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("		{0}    ", game_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", park_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", ump_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", weather_id).AppendLine();
				sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		{0}    ", start_datetime).AppendLine();
				sqlST.AppendLine("		@start_datetime    ");
				CMD_Insert.Parameters.AddWithValue("@start_datetime", start_datetime);
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", bat_first_team_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", field_first_team_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", player_change_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", top_btm_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_5).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_6).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_7).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_8).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_9).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_10).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_11).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_12).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_hit).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_error).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_total_score).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_5).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_6).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_7).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_8).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_9).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_10).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_11).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_12).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_hit).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_error).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_total_score).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", top_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", btm_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_teamName).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_teamName).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_order_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_order_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", ining).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_b).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_s).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_o).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_1_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_2_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_3_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", ining_box_id).AppendLine();

				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_play_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd5).AppendLine();
				//sqlST.AppendLine("		) ");

				//sqlST.AppendLine("WHERE ");
				//sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(
									int game_id,
									int park_id = 0,
									int ump_id = 0,
									int weather_id = 0,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "",
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "",
									string btm_order_id = "",
									string ining = "",
									int count_b = -1,
									int count_s = -1,
									int count_o = -1,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = -1,
									int run_2_player_id = -1,
									int run_3_player_id = -1,
									int player_id = -1,
									int ining_box_id = -1,
									int run_play_id = -1,
									int etc_cd1 = -1,
									int etc_cd2 = -1,
									int etc_cd3 = -1,
									int etc_cd4 = -1,
									int etc_cd5 = -1)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				//sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();
				if (ump_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ump_id={0}    ", ump_id).AppendLine();
				}
				if (weather_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		weather_id={0}    ", weather_id).AppendLine();
				}
				if (bat_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		bat_first_team_id={0}    ", bat_first_team_id).AppendLine();
				}
				if (field_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		field_first_team_id={0}    ", field_first_team_id).AppendLine();
				}
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		top_btm_flg={0}    ", top_btm_flg).AppendLine();
				if (top_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_1='{0}'    ", top_1).AppendLine();
				}
				if (top_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_2='{0}'    ", top_2).AppendLine();
				}
				if (top_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_3='{0}'    ", top_3).AppendLine();
				}
				if (top_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_4='{0}'    ", top_4).AppendLine();
				}
				if (top_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_5='{0}'    ", top_5).AppendLine();
				}
				if (top_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_6='{0}'    ", top_6).AppendLine();
				}
				if (top_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_7='{0}'    ", top_7).AppendLine();
				}
				if (top_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_8='{0}'    ", top_8).AppendLine();
				}
				if (top_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_9='{0}'    ", top_9).AppendLine();
				}
				if (top_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_10='{0}'    ", top_10).AppendLine();
				}
				if (top_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_11='{0}'    ", top_11).AppendLine();
				}
				if (top_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_12='{0}'    ", top_12).AppendLine();
				}
				if (top_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_hit='{0}'    ", top_hit).AppendLine();
				}
				if (top_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_error='{0}'    ", top_error).AppendLine();
				}
				if (top_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", top_total_score).AppendLine();
				}
				if (btm_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_1='{0}'    ", btm_1).AppendLine();
				}
				if (btm_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_2='{0}'    ", btm_2).AppendLine();
				}
				if (btm_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_3='{0}'    ", btm_3).AppendLine();
				}
				if (btm_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_4='{0}'    ", btm_4).AppendLine();
				}
				if (btm_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_5='{0}'    ", btm_5).AppendLine();
				}
				if (btm_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_6='{0}'    ", btm_6).AppendLine();
				}
				if (btm_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_7='{0}'    ", btm_7).AppendLine();
				}
				if (btm_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_8='{0}'    ", btm_8).AppendLine();
				}
				if (btm_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_9='{0}'    ", btm_9).AppendLine();
				}
				if (btm_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_10='{0}'    ", btm_10).AppendLine();
				}
				if (btm_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_11='{0}'    ", btm_11).AppendLine();
				}
				if (btm_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_12='{0}'    ", btm_12).AppendLine();
				}
				if (btm_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_hit='{0}'    ", btm_hit).AppendLine();
				}
				if (btm_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_error='{0}'    ", btm_error).AppendLine();
				}
				if (btm_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", btm_total_score).AppendLine();
				}
				/// 2022.01.25 スターティングオーダーフラグは
				/// updateStartingOrderFlgRecordsで扱う
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				if (top_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_teamName='{0}'    ", top_teamName).AppendLine();
				}
				if (btm_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_teamName='{0}'    ", btm_teamName).AppendLine();
				}

				if (top_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_id='{0}'    ", top_order_id).AppendLine();
				}
				if (btm_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_id='{0}'    ", btm_order_id).AppendLine();
				}

				if (ining.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining='{0}'    ", ining).AppendLine();
				}
				if (count_b >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_b={0}    ", count_b).AppendLine();
				}
				if (count_s >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_s={0}    ", count_s).AppendLine();
				}
				if (count_o >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_o={0}    ", count_o).AppendLine();
				}

				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_1={0}    ", run_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_2={0}    ", run_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_3={0}    ", run_3).AppendLine();

				if (run_1_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_1_player_id={0}    ", run_1_player_id).AppendLine();
				}
				if (run_2_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_2_player_id={0}    ", run_2_player_id).AppendLine();
				}
				if (run_3_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_3_player_id={0}    ", run_3_player_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		player_id={0}    ", player_id).AppendLine();
				}
				if (ining_box_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining_box_id={0}    ", ining_box_id).AppendLine();
				}
				if (run_play_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_play_id={0}    ", run_play_id).AppendLine();
				}
				if (etc_cd1 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd1={0}    ", etc_cd1).AppendLine();
				}
				if (etc_cd2 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd2={0}    ", etc_cd2).AppendLine();
				}
				if (etc_cd3 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd3={0}    ", etc_cd3).AppendLine();
				}
				if (etc_cd4 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd4={0}    ", etc_cd4).AppendLine();
				}
				if (etc_cd5 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd5={0}    ", etc_cd5).AppendLine();
				}


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();




				CMD_Update.CommandText = sqlST.ToString();
				#endregion


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}
		public static void updateScoreRecord(
									int game_id,
									string target,
									string get_score)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			List<GameData.gameDataToTal> scores = GameData.GetRecordScores(game_id: game_id);
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				int tmp_ining_score = 0;
				string tmp_ining_scoreST = "";
				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				switch (target)
				{
					case "top_1":
						if (scores[0].top_1 != null)
						{
							if (scores[0].top_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_1);

							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_2":
						if (scores[0].top_2 != null)
						{
							if (scores[0].top_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_2);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_3":
						if (scores[0].top_3 != null)
						{
							if (scores[0].top_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_3);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_3='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_4":
						if (scores[0].top_4 != null)
						{
							if (scores[0].top_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_4);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_5":
						if (scores[0].top_5 != null)
						{
							if (scores[0].top_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_5);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_6":
						if (scores[0].top_6 != null)
						{
							if (scores[0].top_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_6);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_7":
						if (scores[0].top_7 != null)
						{
							if (scores[0].top_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_7);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_8":
						if (scores[0].top_8 != null)
						{
							if (scores[0].top_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_8);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_9":
						if (scores[0].top_9 != null)
						{
							if (scores[0].top_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_9);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_10":
						if (scores[0].top_10 != null)
						{
							if (scores[0].top_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_10);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_11":
						if (scores[0].top_11 != null)
						{
							if (scores[0].top_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_11);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_11='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_12":
						if (scores[0].top_12 != null)
						{
							if (scores[0].top_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_12);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_1":
						if (scores[0].btm_1 != null)
						{
							if (scores[0].btm_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_1);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_2":
						if (scores[0].btm_2 != null)
						{
							if (scores[0].btm_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_2);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_3":
						if (scores[0].btm_3 != null)
						{
							if (scores[0].btm_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_3);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_3='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_4":
						if (scores[0].btm_4 != null)
						{
							if (scores[0].btm_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_4);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_5":
						if (scores[0].btm_5 != null)
						{
							if (scores[0].btm_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_5);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_6":
						if (scores[0].btm_6 != null)
						{
							if (scores[0].btm_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_6);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_7":
						if (scores[0].btm_7 != null)
						{
							if (scores[0].btm_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_7);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_8":
						if (scores[0].btm_8 != null)
						{
							if (scores[0].btm_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_8);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_9":
						if (scores[0].btm_9 != null)
						{
							if (scores[0].btm_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_9);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_10":
						if (scores[0].btm_10 != null)
						{
							if (scores[0].btm_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_10);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_11":
						if (scores[0].btm_11 != null)
						{
							if (scores[0].btm_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_11);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_11='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_12":
						if (scores[0].btm_12 != null)
						{
							if (scores[0].btm_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_12);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
				}

				string total_score;
				if (target.Contains("top"))
				{
					total_score = Convert.ToString(Convert.ToInt32(get_score)
									+ Convert.ToInt32(scores[0].top_total_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", total_score).AppendLine();
				}
				else if (target.Contains("btm"))
				{
					total_score = Convert.ToString(Convert.ToInt32(get_score)
									+ Convert.ToInt32(scores[0].btm_total_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", total_score).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public static void updateScoreRecordInitialize(
									int game_id,
									string target,
									string get_score)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			List<GameData.gameDataToTal> scores = GameData.GetRecordScores(game_id: game_id);
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				int tmp_ining_score = 0;
				string tmp_ining_scoreST = "";
				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				switch (target)
				{
					case "top_1":
						if (scores[0].top_1 != null)
						{
							if (scores[0].top_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_1);

							}
						}
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_2":
						if (scores[0].top_2 != null)
						{
							if (scores[0].top_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_2);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_3":
						if (scores[0].top_3 != null)
						{
							if (scores[0].top_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_3);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_3='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_4":
						if (scores[0].top_4 != null)
						{
							if (scores[0].top_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_4);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_5":
						if (scores[0].top_5 != null)
						{
							if (scores[0].top_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_5);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_6":
						if (scores[0].top_6 != null)
						{
							if (scores[0].top_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_6);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_7":
						if (scores[0].top_7 != null)
						{
							if (scores[0].top_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_7);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_8":
						if (scores[0].top_8 != null)
						{
							if (scores[0].top_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_8);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_9":
						if (scores[0].top_9 != null)
						{
							if (scores[0].top_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_9);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_10":
						if (scores[0].top_10 != null)
						{
							if (scores[0].top_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_10);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_11":
						if (scores[0].top_11 != null)
						{
							if (scores[0].top_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_11);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_11='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_12":
						if (scores[0].top_12 != null)
						{
							if (scores[0].top_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_12);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_1":
						if (scores[0].btm_1 != null)
						{
							if (scores[0].btm_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_1);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_2":
						if (scores[0].btm_2 != null)
						{
							if (scores[0].btm_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_2);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_3":
						if (scores[0].btm_3 != null)
						{
							if (scores[0].btm_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_3);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_3='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_4":
						if (scores[0].btm_4 != null)
						{
							if (scores[0].btm_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_4);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_5":
						if (scores[0].btm_5 != null)
						{
							if (scores[0].btm_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_5);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_6":
						if (scores[0].btm_6 != null)
						{
							if (scores[0].btm_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_6);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_7":
						if (scores[0].btm_7 != null)
						{
							if (scores[0].btm_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_7);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_8":
						if (scores[0].btm_8 != null)
						{
							if (scores[0].btm_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_8);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_9":
						if (scores[0].btm_9 != null)
						{
							if (scores[0].btm_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_9);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_10":
						if (scores[0].btm_10 != null)
						{
							if (scores[0].btm_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_10);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_11":
						if (scores[0].btm_11 != null)
						{
							if (scores[0].btm_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_11);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_11='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_12":
						if (scores[0].btm_12 != null)
						{
							if (scores[0].btm_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_12);
							}
						}
						//tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) - tmp_ining_score);
						tmp_ining_scoreST = Convert.ToString(tmp_ining_score - Convert.ToInt32(get_score));
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
				}

				string total_score;
				if (target.Contains("top"))
				{
					total_score = Convert.ToString(Convert.ToInt32(scores[0].top_total_score) - Convert.ToInt32(get_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", total_score).AppendLine();
				}
				else if (target.Contains("btm"))
				{
					total_score = Convert.ToString(Convert.ToInt32(scores[0].btm_total_score) - Convert.ToInt32(get_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", total_score).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}




		public static void updateRecord_sub(
									int game_id,
									int park_id = 0,
									int ump_id = 0,
									int weather_id = 0,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "",
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "",
									string btm_order_id = "",
									string ining = "",
									int count_b = 0,
									int count_s = 0,
									int count_o = 0,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = 0,
									int run_2_player_id = 0,
									int run_3_player_id = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				//sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();
				if (ump_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ump_id={0}    ", ump_id).AppendLine();
				}
				if (weather_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		weather_id={0}    ", weather_id).AppendLine();
				}
				if (bat_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		bat_first_team_id={0}    ", bat_first_team_id).AppendLine();
				}
				if (field_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		field_first_team_id={0}    ", field_first_team_id).AppendLine();
				}
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		top_btm_flg={0}    ", top_btm_flg).AppendLine();
				if (top_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_1='{0}'    ", top_1).AppendLine();
				}
				if (top_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_2='{0}'    ", top_2).AppendLine();
				}
				if (top_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_3='{0}'    ", top_3).AppendLine();
				}
				if (top_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_4='{0}'    ", top_4).AppendLine();
				}
				if (top_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_5='{0}'    ", top_5).AppendLine();
				}
				if (top_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_6='{0}'    ", top_6).AppendLine();
				}
				if (top_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_7='{0}'    ", top_7).AppendLine();
				}
				if (top_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_8='{0}'    ", top_8).AppendLine();
				}
				if (top_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_9='{0}'    ", top_9).AppendLine();
				}
				if (top_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_10='{0}'    ", top_10).AppendLine();
				}
				if (top_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_11='{0}'    ", top_11).AppendLine();
				}
				if (top_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_12='{0}'    ", top_12).AppendLine();
				}
				if (top_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_hit='{0}'    ", top_hit).AppendLine();
				}
				if (top_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_error='{0}'    ", top_error).AppendLine();
				}
				if (top_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", top_total_score).AppendLine();
				}
				if (btm_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_1='{0}'    ", btm_1).AppendLine();
				}
				if (btm_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_2='{0}'    ", btm_2).AppendLine();
				}
				if (btm_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_3='{0}'    ", btm_3).AppendLine();
				}
				if (btm_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_4='{0}'    ", btm_4).AppendLine();
				}
				if (btm_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_5='{0}'    ", btm_5).AppendLine();
				}
				if (btm_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_6='{0}'    ", btm_6).AppendLine();
				}
				if (btm_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_7='{0}'    ", btm_7).AppendLine();
				}
				if (btm_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_8='{0}'    ", btm_8).AppendLine();
				}
				if (btm_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_9='{0}'    ", btm_9).AppendLine();
				}
				if (btm_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_10='{0}'    ", btm_10).AppendLine();
				}
				if (btm_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_11='{0}'    ", btm_11).AppendLine();
				}
				if (btm_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_12='{0}'    ", btm_12).AppendLine();
				}
				if (btm_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_hit='{0}'    ", btm_hit).AppendLine();
				}
				if (btm_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_error='{0}'    ", btm_error).AppendLine();
				}
				if (btm_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", btm_total_score).AppendLine();
				}
				if (top_order_start_flg)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				}
				if (btm_order_start_flg)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				}
				if (top_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_teamName='{0}'    ", top_teamName).AppendLine();
				}
				if (btm_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_teamName='{0}'    ", btm_teamName).AppendLine();
				}

				if (top_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_id='{0}'    ", top_order_id).AppendLine();
				}
				if (btm_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_id='{0}'    ", btm_order_id).AppendLine();
				}
				if (ining.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining='{0}'    ", ining).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		/// <summary>
		/// 試合中の選手交代フラグの変更
		/// </summary>
		/// <param name="game_id"></param>
		/// <param name="game_start_flg"></param>
		/// <param name="player_change_flg"></param>
		public static void updateGameFlgRecord(
									int game_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void updateStartingOrderFlgRecord(
									int game_id = 0,
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									bool game_start_flg = false
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRunPlayIdFlgRecord(
									int game_id = 0,
									int run_play_id = 0
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		run_play_id={0}    ", run_play_id).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public static void deleteRecord(int game_id = 0)
		{
			if (game_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	game_id=@game_id ");
				CmdDelete.Parameters.AddWithValue("@game_id", game_id);
				CmdDelete.CommandText = sqlST.ToString();


				#endregion
				CmdDelete.ExecuteReader();
				con.Close();
			}
		}


		public class gameData
		{

			#region オーダー変数
			public int game_id { get; set; }
			public int park_id { get; set; }
			public int ump_id { get; set; }
			public int weather_id { get; set; }
			public DateTime start_datetime { get; set; }
			public int bat_first_team_id { get; set; }
			public int field_first_team_id { get; set; }
			public bool game_start_flg { get; set; }
			public bool player_change_flg { get; set; }
			public bool top_btm_flg { get; set; }
			public string top_1 { get; set; }
			public string top_2 { get; set; }
			public string top_3 { get; set; }
			public string top_4 { get; set; }
			public string top_5 { get; set; }
			public string top_6 { get; set; }
			public string top_7 { get; set; }
			public string top_8 { get; set; }
			public string top_9 { get; set; }
			public string top_10 { get; set; }
			public string top_11 { get; set; }
			public string top_12 { get; set; }
			public string top_hit { get; set; }
			public string top_error { get; set; }
			public string top_total_score { get; set; }

			public string btm_1 { get; set; }
			public string btm_2 { get; set; }
			public string btm_3 { get; set; }
			public string btm_4 { get; set; }
			public string btm_5 { get; set; }
			public string btm_6 { get; set; }
			public string btm_7 { get; set; }
			public string btm_8 { get; set; }
			public string btm_9 { get; set; }
			public string btm_10 { get; set; }
			public string btm_11 { get; set; }
			public string btm_12 { get; set; }
			public string btm_hit { get; set; }
			public string btm_error { get; set; }
			public string btm_total_score { get; set; }
			public bool top_order_start_flg { get; set; }
			public bool btm_order_start_flg { get; set; }
			public string top_teamName { get; set; }
			public string btm_teamName { get; set; }
			public string top_order_id { get; set; }
			public string btm_order_id { get; set; }
			public string ining { get; set; }
			public int count_b { get; set; }
			public int count_s { get; set; }
			public int count_o { get; set; }
			public bool run_1 { get; set; }
			public bool run_2 { get; set; }
			public bool run_3 { get; set; }
			public int run_1_player_id { get; set; }
			public int run_2_player_id { get; set; }
			public int run_3_player_id { get; set; }
			public int player_id { get; set; }
			public int ining_box_id { get; set; }
			public int run_play_id { get; set; }
			public int etc_cd1 { get; set; }
			public int etc_cd2 { get; set; }
			public int etc_cd3 { get; set; }
			public int etc_cd4 { get; set; }
			public int etc_cd5 { get; set; }
			#endregion


			public gameData(
									int game_id,
									int park_id,
									int ump_id,
									int weather_id,
									DateTime start_datetime,
									int bat_first_team_id,
									int field_first_team_id,
									bool game_start_flg,
									bool player_change_flg,
									bool top_btm_flg,
									string top_1,
									string top_2,
									string top_3,
									string top_4,
									string top_5,
									string top_6,
									string top_7,
									string top_8,
									string top_9,
									string top_10,
									string top_11,
									string top_12,
									string top_hit,
									string top_error,
									string top_total_score,
									string btm_1,
									string btm_2,
									string btm_3,
									string btm_4,
									string btm_5,
									string btm_6,
									string btm_7,
									string btm_8,
									string btm_9,
									string btm_10,
									string btm_11,
									string btm_12,
									string btm_hit,
									string btm_error,
									string btm_total_score,
									bool top_order_start_flg,
									bool btm_order_start_flg,
									string top_teamName,
									string btm_teamName,
									string top_order_id,
									string btm_order_id,
									string ining,
									int count_b,
									int count_s,
									int count_o,
									bool run_1,
									bool run_2,
									bool run_3,
									int run_1_player_id,
									int run_2_player_id,
									int run_3_player_id,
									int player_id,
									int ining_box_id,
									int run_play_id,
									int etc_cd1,
									int etc_cd2,
									int etc_cd3,
									int etc_cd4,
									int etc_cd5)
			{
				this.game_id = game_id;
				this.park_id = park_id;
				this.ump_id = ump_id;
				this.weather_id = weather_id;
				this.start_datetime = start_datetime;
				this.bat_first_team_id = bat_first_team_id;
				this.field_first_team_id = field_first_team_id;
				this.game_start_flg = game_start_flg;
				this.player_change_flg = player_change_flg;
				this.top_btm_flg = top_btm_flg;
				this.top_1 = top_1;
				this.top_2 = top_2;
				this.top_3 = top_3;
				this.top_4 = top_4;
				this.top_5 = top_5;
				this.top_6 = top_6;
				this.top_7 = top_7;
				this.top_8 = top_8;
				this.top_9 = top_9;
				this.top_10 = top_10;
				this.top_11 = top_11;
				this.top_12 = top_12;
				this.top_hit = top_hit;
				this.top_error = top_error;
				this.top_total_score = top_total_score;
				this.btm_1 = btm_1;
				this.btm_2 = btm_2;
				this.btm_3 = btm_3;
				this.btm_4 = btm_4;
				this.btm_5 = btm_5;
				this.btm_6 = btm_6;
				this.btm_7 = btm_7;
				this.btm_8 = btm_8;
				this.btm_9 = btm_9;
				this.btm_10 = btm_10;
				this.btm_11 = btm_11;
				this.btm_12 = btm_12;
				this.btm_hit = btm_hit;
				this.btm_error = btm_error;
				this.btm_total_score = btm_total_score;
				this.top_order_start_flg = top_order_start_flg;
				this.btm_order_start_flg = btm_order_start_flg;
				this.top_teamName = top_teamName;
				this.btm_teamName = btm_teamName;
				this.top_order_id = top_order_id;
				this.btm_order_id = btm_order_id;
				this.ining = ining;
				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.run_1 = run_1;
				this.run_2 = run_2;
				this.run_3 = run_3;
				this.run_1_player_id = run_1_player_id;
				this.run_2_player_id = run_2_player_id;
				this.run_3_player_id = run_3_player_id;
				this.player_id = player_id;
				this.ining_box_id = ining_box_id;
				this.run_play_id = run_play_id;
				this.etc_cd1 = etc_cd1;
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;
			}



			public void CountBallUp()
			{
				this.count_b = count_b + 1;

			}

			private bool CountCheckFourBall()
			{
				if (this.count_b > 3)
				{
					return true;
				}
				return false;
			}

			public void CountStrikeUp()
			{
				this.count_s = count_s + 1;

			}

			private bool CountCheckStrikeOut()
			{
				if (this.count_s > 2)
				{
					return true;
				}
				return false;
			}

			public void CountOutUp()
			{
				this.count_o = count_o + 1;

			}

			private bool CountCheckThreeOut()
			{
				if (this.count_o > 2)
				{
					return true;
				}
				return false;
			}

			private bool TopBtmCheck()
			{
				if (this.top_btm_flg)
				{
					return true;
				}
				return false;
			}




		}

		public static List<gameData> GetRecordsAllMember(string teamName = "")
		{
			List<gameData> orderList = new List<gameData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game.* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new gameData(
							game_id: reader.GetInt32(0),
							park_id: reader.GetInt32(1),
							ump_id: reader.GetInt32(2),
							weather_id: reader.GetInt32(3),
							start_datetime: reader.GetDateTime(4),
							bat_first_team_id: reader.GetInt32(5),
							field_first_team_id: reader.GetInt32(6),
							game_start_flg: reader.GetBoolean(7),
							player_change_flg: reader.GetBoolean(8),
							top_btm_flg: reader.GetBoolean(9),
							top_1: reader.GetString(10),
							top_2: reader.GetString(11),
							top_3: reader.GetString(12),
							top_4: reader.GetString(13),
							top_5: reader.GetString(14),
							top_6: reader.GetString(15),
							top_7: reader.GetString(16),
							top_8: reader.GetString(17),
							top_9: reader.GetString(18),
							top_10: reader.GetString(19),
							top_11: reader.GetString(20),
							top_12: reader.GetString(21),
							top_hit: reader.GetString(22),
							top_error: reader.GetString(23),
							top_total_score: reader.GetString(24),
							btm_1: reader.GetString(25),
							btm_2: reader.GetString(26),
							btm_3: reader.GetString(27),
							btm_4: reader.GetString(28),
							btm_5: reader.GetString(29),
							btm_6: reader.GetString(30),
							btm_7: reader.GetString(31),
							btm_8: reader.GetString(32),
							btm_9: reader.GetString(33),
							btm_10: reader.GetString(34),
							btm_11: reader.GetString(35),
							btm_12: reader.GetString(36),
							btm_hit: reader.GetString(37),
							btm_error: reader.GetString(38),
							btm_total_score: reader.GetString(39),
							top_order_start_flg: reader.GetBoolean(40),
							btm_order_start_flg: reader.GetBoolean(41),
							top_teamName: reader.GetString(42),
							btm_teamName: reader.GetString(43),
							top_order_id: reader.GetString(44),
							btm_order_id: reader.GetString(45),
							ining: reader.GetString(46),
							count_b: reader.GetInt32(47),
							count_s: reader.GetInt32(48),
							count_o: reader.GetInt32(49),
							run_1: reader.GetBoolean(50),
							run_2: reader.GetBoolean(51),
							run_3: reader.GetBoolean(52),
							run_1_player_id: reader.GetInt32(53),
							run_2_player_id: reader.GetInt32(54),
							run_3_player_id: reader.GetInt32(55),
							player_id: reader.GetInt32(56),
							ining_box_id: reader.GetInt32(57),
							run_play_id: reader.GetInt32(58),
							etc_cd1: reader.GetInt32(59),
							etc_cd2: reader.GetInt32(60),
							etc_cd3: reader.GetInt32(61),
							etc_cd4: reader.GetInt32(62),
							etc_cd5: reader.GetInt32(63)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class gameDataCount
		{
			public int game_count { get; set; }
			public gameDataCount(int count = 0)
			{
				this.game_count = count;

			}
		}
		public static List<gameDataCount> GetRecordsCount()
		{
			List<gameDataCount> countList = new List<gameDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				//sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	CASE ");
				//sqlST.AppendLine("		WHEN ");
				//sqlST.AppendLine("			MAX(game_id) is null THEN 1 ");
				//sqlST.AppendLine("		ELSE ");
				//sqlST.AppendLine("			MAX(game_id) + 1 ");
				//sqlST.AppendLine("	END AS game_id ");
				//sqlST.AppendLine("FROM ");
				//sqlST.AppendLine("	game ");
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(game_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public class gameDataIningBoxId
		{
			public int ining_box_id { get; set; }
			public gameDataIningBoxId(int count = 0)
			{
				this.ining_box_id = count;

			}
		}
		public static List<gameDataIningBoxId> GetRecordsIningBoxId(int box_id = 0)
		{
			List<gameDataIningBoxId> countList = new List<gameDataIningBoxId>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ining_box_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameDataIningBoxId(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public class gameIdCount
		{
			public int game_id { get; set; }
			public gameIdCount(int count = 0)
			{
				this.game_id = count;
			}
		}
		public static List<gameIdCount> GetGameIdRecord()
		{
			List<gameIdCount> countList = new List<gameIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(game_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(game_id) + 1 ");
				sqlST.AppendLine("	END AS game_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");

				//sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	MAX(game_id) + 1 ");
				//sqlST.AppendLine("FROM ");
				//sqlST.AppendLine("	game ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public class gameTeamData
		{
			public int game_id { get; set; }
			public int top_team_id { get; set; }
			public int btm_team_id { get; set; }
			public string top_team_name { get; set; }
			public string btm_team_name { get; set; }
			public string top_score { get; set; }
			public string btm_score { get; set; }
			public string dateTime { get; set; }
			public gameTeamData(
						int game_id = 0,
						int top_team_id = 0,
						int btm_team_id = 0,
						string top_team_name = "",
						string btm_team_name = "",
						string top_score = "",
						string btm_score = "",
						string dateTime = ""
				)
			{
				this.game_id = game_id;
				this.top_team_id = top_team_id;
				this.btm_team_id = btm_team_id;
				this.top_team_name = top_team_name;
				this.btm_team_name = btm_team_name;
				this.top_score = top_score;
				this.btm_score = btm_score;
				this.dateTime = dateTime;
			}
		}

		public static List<gameTeamData> GetGameResultData(int game_id = 0)
		{
			List<gameTeamData> countList = new List<gameTeamData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game.game_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_team.team_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_team.team_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_team.teamName ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_team.teamName ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	game.top_total_score ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	game.btm_total_score ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	strftime('%Y-%m-%d',game.start_datetime) AS start_datetime ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	teams AS top_team ");
				sqlST.AppendLine("	ON top_team.team_id=game.bat_first_team_id ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	teams AS btm_team ");
				sqlST.AppendLine("	ON btm_team.team_id=game.field_first_team_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				if (game_id > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	game.game_id={0} ", game_id).AppendLine();
				}
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	game.game_id DESC ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(
						new gameTeamData(
							game_id: reader.GetInt32(0),
							top_team_id: reader.GetInt32(1),
							btm_team_id: reader.GetInt32(2),
							top_team_name: reader.GetString(3),
							btm_team_name: reader.GetString(4),
							top_score: reader.GetString(5),
							btm_score: reader.GetString(6),
							dateTime: reader.GetString(7)
							)
						);
				}
			}
			return countList;
		}

		public class gameFlgs
		{
			public int game_id { get; set; }
			public bool game_start_flg { get; set; }
			public bool player_change_flg { get; set; }
			public gameFlgs(
						int game_id = 0,
						bool game_start_flg = false,
						bool player_change_flg = false
				)
			{
				this.game_id = game_id;
				this.game_start_flg = game_start_flg;
				this.player_change_flg = player_change_flg;

			}
		}
		public static List<gameFlgs> GetPlayerChangeFlg(int game_id = 0)
		{
			List<gameFlgs> countList = new List<gameFlgs>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	game_start_flg ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	player_change_flg ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				if (game_id > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(
						new gameFlgs(
							game_id: reader.GetInt32(0),
							game_start_flg: reader.GetBoolean(1),
							player_change_flg: reader.GetBoolean(2)
							)
						);
				}
			}
			return countList;
		}



		public static List<gameData> GetRecords(int game_id = 0)
		{

			List<gameData> orderList = new List<gameData>();
			if (game_id == 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game.* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id);

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new gameData(
							game_id: reader.GetInt32(0),
							park_id: reader.GetInt32(1),
							ump_id: reader.GetInt32(2),
							weather_id: reader.GetInt32(3),
							start_datetime: reader.GetDateTime(4),
							bat_first_team_id: reader.GetInt32(5),
							field_first_team_id: reader.GetInt32(6),
							game_start_flg: reader.GetBoolean(7),
							player_change_flg: reader.GetBoolean(8),
							top_btm_flg: reader.GetBoolean(9),
							top_1: reader.GetString(10),
							top_2: reader.GetString(11),
							top_3: reader.GetString(12),
							top_4: reader.GetString(13),
							top_5: reader.GetString(14),
							top_6: reader.GetString(15),
							top_7: reader.GetString(16),
							top_8: reader.GetString(17),
							top_9: reader.GetString(18),
							top_10: reader.GetString(19),
							top_11: reader.GetString(20),
							top_12: reader.GetString(21),
							top_hit: reader.GetString(22),
							top_error: reader.GetString(23),
							top_total_score: reader.GetString(24),
							btm_1: reader.GetString(25),
							btm_2: reader.GetString(26),
							btm_3: reader.GetString(27),
							btm_4: reader.GetString(28),
							btm_5: reader.GetString(29),
							btm_6: reader.GetString(30),
							btm_7: reader.GetString(31),
							btm_8: reader.GetString(32),
							btm_9: reader.GetString(33),
							btm_10: reader.GetString(34),
							btm_11: reader.GetString(35),
							btm_12: reader.GetString(36),
							btm_hit: reader.GetString(37),
							btm_error: reader.GetString(38),
							btm_total_score: reader.GetString(39),
							top_order_start_flg: reader.GetBoolean(40),
							btm_order_start_flg: reader.GetBoolean(41),
							top_teamName: reader.GetString(42),
							btm_teamName: reader.GetString(43),
							top_order_id: reader.GetString(44),
							btm_order_id: reader.GetString(45),
							ining: reader.GetString(46),
							count_b: reader.GetInt32(47),
							count_s: reader.GetInt32(48),
							count_o: reader.GetInt32(49),
							run_1: reader.GetBoolean(50),
							run_2: reader.GetBoolean(51),
							run_3: reader.GetBoolean(52),
							run_1_player_id: reader.GetInt32(53),
							run_2_player_id: reader.GetInt32(54),
							run_3_player_id: reader.GetInt32(55),
							player_id: reader.GetInt32(56),
							ining_box_id: reader.GetInt32(57),
							run_play_id: reader.GetInt32(58),
							etc_cd1: reader.GetInt32(59),
							etc_cd2: reader.GetInt32(60),
							etc_cd3: reader.GetInt32(61),
							etc_cd4: reader.GetInt32(62),
							etc_cd5: reader.GetInt32(63)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<gameIdCount> GetGameIdRecords()
		{

			List<gameIdCount> orderList = new List<gameIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	game_id DESC ");

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new gameIdCount(
								reader.GetInt32(0)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


		public class gameDataToTal
		{

			#region オーダー変数
			public int game_id { get; set; }
			public string top_1 { get; set; }
			public string top_2 { get; set; }
			public string top_3 { get; set; }
			public string top_4 { get; set; }
			public string top_5 { get; set; }
			public string top_6 { get; set; }
			public string top_7 { get; set; }
			public string top_8 { get; set; }
			public string top_9 { get; set; }
			public string top_10 { get; set; }
			public string top_11 { get; set; }
			public string top_12 { get; set; }
			public string top_hit { get; set; }
			public string top_error { get; set; }
			public string top_total_score { get; set; }

			public string btm_1 { get; set; }
			public string btm_2 { get; set; }
			public string btm_3 { get; set; }
			public string btm_4 { get; set; }
			public string btm_5 { get; set; }
			public string btm_6 { get; set; }
			public string btm_7 { get; set; }
			public string btm_8 { get; set; }
			public string btm_9 { get; set; }
			public string btm_10 { get; set; }
			public string btm_11 { get; set; }
			public string btm_12 { get; set; }
			public string btm_hit { get; set; }
			public string btm_error { get; set; }
			public string btm_total_score { get; set; }

			#endregion


			public gameDataToTal(
									string top_1,
									string top_2,
									string top_3,
									string top_4,
									string top_5,
									string top_6,
									string top_7,
									string top_8,
									string top_9,
									string top_10,
									string top_11,
									string top_12,
									string top_hit,
									string top_error,
									string top_total_score,
									string btm_1,
									string btm_2,
									string btm_3,
									string btm_4,
									string btm_5,
									string btm_6,
									string btm_7,
									string btm_8,
									string btm_9,
									string btm_10,
									string btm_11,
									string btm_12,
									string btm_hit,
									string btm_error,
									string btm_total_score)
			{
				int total_top = 0;
				this.top_1 = top_1;
				this.top_2 = top_2;
				this.top_3 = top_3;
				this.top_4 = top_4;
				this.top_5 = top_5;
				this.top_6 = top_6;
				this.top_7 = top_7;
				this.top_8 = top_8;
				this.top_9 = top_9;
				this.top_10 = top_10;
				this.top_11 = top_11;
				this.top_12 = top_12;

				this.btm_1 = btm_1;
				this.btm_2 = btm_2;
				this.btm_3 = btm_3;
				this.btm_4 = btm_4;
				this.btm_5 = btm_5;
				this.btm_6 = btm_6;
				this.btm_7 = btm_7;
				this.btm_8 = btm_8;
				this.btm_9 = btm_9;
				this.btm_10 = btm_10;
				this.btm_11 = btm_11;
				this.btm_12 = btm_12;
				if (top_1.Length != 0)
				{
					total_top += Convert.ToInt32(top_1);
				}
				if (top_2.Length != 0)
				{
					total_top += Convert.ToInt32(top_2);
				}
				if (top_3.Length != 0)
				{
					total_top += Convert.ToInt32(top_3);
				}
				if (top_4.Length != 0)
				{
					total_top += Convert.ToInt32(top_4);
				}
				if (top_5.Length != 0)
				{
					total_top += Convert.ToInt32(top_5);
				}
				if (top_6.Length != 0)
				{
					total_top += Convert.ToInt32(top_6);
				}
				if (top_7.Length != 0)
				{
					total_top += Convert.ToInt32(top_7);
				}
				if (top_8.Length != 0)
				{
					total_top += Convert.ToInt32(top_8);
				}
				if (top_9.Length != 0)
				{
					total_top += Convert.ToInt32(top_9);
				}
				if (top_10.Length != 0)
				{
					total_top += Convert.ToInt32(top_10);
				}
				if (top_11.Length != 0)
				{
					total_top += Convert.ToInt32(top_11);
				}
				if (top_12.Length != 0)
				{
					total_top += Convert.ToInt32(top_12);
				}
				this.top_total_score = total_top.ToString();
				this.top_hit = top_hit;
				this.top_error = top_error;


				int total_btm = 0;
				if (btm_1.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_1);
				}
				if (btm_2.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_2);
				}
				if (btm_3.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_3);
				}
				if (btm_4.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_4);
				}
				if (btm_5.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_5);
				}
				if (btm_6.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_6);
				}
				if (btm_7.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_7);
				}
				if (btm_8.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_8);
				}
				if (btm_9.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_9);
				}
				if (btm_10.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_10);
				}
				if (btm_11.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_11);
				}
				if (btm_12.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_12);
				}
				this.btm_total_score = total_btm.ToString();
				this.btm_hit = btm_hit;
				this.btm_error = btm_error;
			}
		}
		public static List<gameDataToTal> GetRecordScores(int game_id = 0)
		{

			List<gameDataToTal> orderList = new List<gameDataToTal>();
			if (game_id == 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	top_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_4 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_5 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_6 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_7 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_8 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_9 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_10 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_11 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_12 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_hit ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_error ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_total_score ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_4 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_5 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_6 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_7 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_8 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_9 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_10 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_11 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_12 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_hit ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_error ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_total_score ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id);

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new gameDataToTal(
							top_1: reader.GetString(0),
							top_2: reader.GetString(1),
							top_3: reader.GetString(2),
							top_4: reader.GetString(3),
							top_5: reader.GetString(4),
							top_6: reader.GetString(5),
							top_7: reader.GetString(6),
							top_8: reader.GetString(7),
							top_9: reader.GetString(8),
							top_10: reader.GetString(9),
							top_11: reader.GetString(10),
							top_12: reader.GetString(11),
							top_hit: reader.GetString(12),
							top_error: reader.GetString(13),
							top_total_score: reader.GetString(14),
							btm_1: reader.GetString(15),
							btm_2: reader.GetString(16),
							btm_3: reader.GetString(17),
							btm_4: reader.GetString(18),
							btm_5: reader.GetString(19),
							btm_6: reader.GetString(20),
							btm_7: reader.GetString(21),
							btm_8: reader.GetString(22),
							btm_9: reader.GetString(23),
							btm_10: reader.GetString(24),
							btm_11: reader.GetString(25),
							btm_12: reader.GetString(26),
							btm_hit: reader.GetString(27),
							btm_error: reader.GetString(28),
							btm_total_score: reader.GetString(29)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


	}
	#endregion

	#region チーム
	class TmpGameData
	{

		public TmpGameData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	tmp_game ( ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		park_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ump_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		weather_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		start_datetime    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		bat_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		field_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_change_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_btm_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_b    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_s    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_o    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining_box_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_play_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd5    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(
									int game_id,
									int park_id,
									int ump_id,
									int weather_id,
									DateTime start_datetime,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "0",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "0",
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "1",
									string btm_order_id = "1",
									string ining = "1",
									int count_b = 0,
									int count_s = 0,
									int count_o = 0,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = 0,
									int run_2_player_id = 0,
									int run_3_player_id = 0,
									int player_id = 0,
									int ining_box_id = 1,
									int run_play_id = 0,
									int etc_cd1 = 0,
									int etc_cd2 = 0,
									int etc_cd3 = 0,
									int etc_cd4 = 0,
									int etc_cd5 = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	tmp_game ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		game_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		park_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ump_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		weather_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		start_datetime    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		bat_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		field_first_team_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		game_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_change_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_btm_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_5    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_6    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_7    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_8    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_9    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_10    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_11   ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_12    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_hit    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_error    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_total_score    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_start_flg    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_teamName    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		top_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		btm_order_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_b    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_s    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		count_o    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_1_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_2_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_3_player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		player_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		ining_box_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		run_play_id    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd1    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd2    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd3    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd4    ");
				sqlST.AppendLine("		,    ");
				sqlST.AppendLine("		etc_cd5    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("		{0}    ", game_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", park_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", ump_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", weather_id).AppendLine();
				sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		{0}    ", start_datetime).AppendLine();
				sqlST.AppendLine("		@start_datetime    ");
				CMD_Insert.Parameters.AddWithValue("@start_datetime", start_datetime);
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", bat_first_team_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", field_first_team_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", player_change_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", top_btm_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_5).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_6).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_7).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_8).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_9).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_10).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_11).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_12).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_hit).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_error).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_total_score).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_5).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_6).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_7).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_8).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_9).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_10).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_11).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_12).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_hit).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_error).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_total_score).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", top_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", btm_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_teamName).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_teamName).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", top_order_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", btm_order_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		'{0}'    ", ining).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_b).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_s).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", count_o).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_1_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_2_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_3_player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", player_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", ining_box_id).AppendLine();

				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", run_play_id).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd3).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd4).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		{0}    ", etc_cd5).AppendLine();
				//sqlST.AppendLine("		) ");

				//sqlST.AppendLine("WHERE ");
				//sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(
									int game_id,
									int park_id = 0,
									int ump_id = 0,
									int weather_id = 0,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "",
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "",
									string btm_order_id = "",
									string ining = "",
									int count_b = -1,
									int count_s = -1,
									int count_o = -1,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = -1,
									int run_2_player_id = -1,
									int run_3_player_id = -1,
									int player_id = -1,
									int ining_box_id = -1,
									int run_play_id = -1,
									int etc_cd1 = -1,
									int etc_cd2 = -1,
									int etc_cd3 = -1,
									int etc_cd4 = -1,
									int etc_cd5 = -1)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				//sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();
				if (ump_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ump_id={0}    ", ump_id).AppendLine();
				}
				if (weather_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		weather_id={0}    ", weather_id).AppendLine();
				}
				if (bat_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		bat_first_team_id={0}    ", bat_first_team_id).AppendLine();
				}
				if (field_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		field_first_team_id={0}    ", field_first_team_id).AppendLine();
				}
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		top_btm_flg={0}    ", top_btm_flg).AppendLine();
				if (top_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_1='{0}'    ", top_1).AppendLine();
				}
				if (top_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_2='{0}'    ", top_2).AppendLine();
				}
				if (top_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_3='{0}'    ", top_3).AppendLine();
				}
				if (top_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_4='{0}'    ", top_4).AppendLine();
				}
				if (top_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_5='{0}'    ", top_5).AppendLine();
				}
				if (top_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_6='{0}'    ", top_6).AppendLine();
				}
				if (top_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_7='{0}'    ", top_7).AppendLine();
				}
				if (top_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_8='{0}'    ", top_8).AppendLine();
				}
				if (top_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_9='{0}'    ", top_9).AppendLine();
				}
				if (top_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_10='{0}'    ", top_10).AppendLine();
				}
				if (top_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_11='{0}'    ", top_11).AppendLine();
				}
				if (top_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_12='{0}'    ", top_12).AppendLine();
				}
				if (top_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_hit='{0}'    ", top_hit).AppendLine();
				}
				if (top_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_error='{0}'    ", top_error).AppendLine();
				}
				if (top_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", top_total_score).AppendLine();
				}
				if (btm_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_1='{0}'    ", btm_1).AppendLine();
				}
				if (btm_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_2='{0}'    ", btm_2).AppendLine();
				}
				if (btm_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_3='{0}'    ", btm_3).AppendLine();
				}
				if (btm_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_4='{0}'    ", btm_4).AppendLine();
				}
				if (btm_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_5='{0}'    ", btm_5).AppendLine();
				}
				if (btm_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_6='{0}'    ", btm_6).AppendLine();
				}
				if (btm_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_7='{0}'    ", btm_7).AppendLine();
				}
				if (btm_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_8='{0}'    ", btm_8).AppendLine();
				}
				if (btm_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_9='{0}'    ", btm_9).AppendLine();
				}
				if (btm_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_10='{0}'    ", btm_10).AppendLine();
				}
				if (btm_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_11='{0}'    ", btm_11).AppendLine();
				}
				if (btm_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_12='{0}'    ", btm_12).AppendLine();
				}
				if (btm_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_hit='{0}'    ", btm_hit).AppendLine();
				}
				if (btm_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_error='{0}'    ", btm_error).AppendLine();
				}
				if (btm_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", btm_total_score).AppendLine();
				}
				/// 2022.01.25 スターティングオーダーフラグは
				/// updateStartingOrderFlgRecordsで扱う
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				if (top_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_teamName='{0}'    ", top_teamName).AppendLine();
				}
				if (btm_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_teamName='{0}'    ", btm_teamName).AppendLine();
				}

				if (top_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_id='{0}'    ", top_order_id).AppendLine();
				}
				if (btm_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_id='{0}'    ", btm_order_id).AppendLine();
				}

				if (ining.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining='{0}'    ", ining).AppendLine();
				}
				if (count_b >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_b={0}    ", count_b).AppendLine();
				}
				if (count_s >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_s={0}    ", count_s).AppendLine();
				}
				if (count_o >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		count_o={0}    ", count_o).AppendLine();
				}

				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_1={0}    ", run_1).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_2={0}    ", run_2).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		run_3={0}    ", run_3).AppendLine();

				if (run_1_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_1_player_id={0}    ", run_1_player_id).AppendLine();
				}
				if (run_2_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_2_player_id={0}    ", run_2_player_id).AppendLine();
				}
				if (run_3_player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_3_player_id={0}    ", run_3_player_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		player_id={0}    ", player_id).AppendLine();
				}
				if (ining_box_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining_box_id={0}    ", ining_box_id).AppendLine();
				}
				if (run_play_id >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		run_play_id={0}    ", run_play_id).AppendLine();
				}
				if (etc_cd1 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd1={0}    ", etc_cd1).AppendLine();
				}
				if (etc_cd2 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd2={0}    ", etc_cd2).AppendLine();
				}
				if (etc_cd3 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd3={0}    ", etc_cd3).AppendLine();
				}
				if (etc_cd4 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd4={0}    ", etc_cd4).AppendLine();
				}
				if (etc_cd5 >= 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		etc_cd5={0}    ", etc_cd5).AppendLine();
				}


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();




				CMD_Update.CommandText = sqlST.ToString();
				#endregion


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}
		public static void updateScoreRecord(
									int game_id,
									string target,
									string get_score)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			List<GameData.gameDataToTal> scores = GameData.GetRecordScores(game_id: game_id);
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();
				int tmp_ining_score = 0;
				string tmp_ining_scoreST = "";
				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				switch (target)
				{
					case "top_1":
						if (scores[0].top_1 != null)
						{
							if (scores[0].top_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_1);

							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_2":
						if (scores[0].top_2 != null)
						{
							if (scores[0].top_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_2);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_3":
						if (scores[0].top_3 != null)
						{
							if (scores[0].top_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_3);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_3='{0}'    ", get_score).AppendLine();
						break;
					case "top_4":
						if (scores[0].top_4 != null)
						{
							if (scores[0].top_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_4);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);

						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_5":
						if (scores[0].top_5 != null)
						{
							if (scores[0].top_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_5);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_6":
						if (scores[0].top_6 != null)
						{
							if (scores[0].top_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_6);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_7":
						if (scores[0].top_7 != null)
						{
							if (scores[0].top_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_7);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_8":
						if (scores[0].top_8 != null)
						{
							if (scores[0].top_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_8);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_9":
						if (scores[0].top_9 != null)
						{
							if (scores[0].top_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_9);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_10":
						if (scores[0].top_10 != null)
						{
							if (scores[0].top_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_10);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_11":
						if (scores[0].top_11 != null)
						{
							if (scores[0].top_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_11);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_11='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "top_12":
						if (scores[0].top_12 != null)
						{
							if (scores[0].top_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].top_12);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		top_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_1":
						if (scores[0].btm_1 != null)
						{
							if (scores[0].btm_1.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_1);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_1='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_2":
						if (scores[0].btm_2 != null)
						{
							if (scores[0].btm_2.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_2);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_2='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_3":
						if (scores[0].btm_3 != null)
						{
							if (scores[0].btm_3.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_3);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_3='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_4":
						if (scores[0].btm_4 != null)
						{
							if (scores[0].btm_4.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_4);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_4='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_5":
						if (scores[0].btm_5 != null)
						{
							if (scores[0].btm_5.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_5);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_5='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_6":
						if (scores[0].btm_6 != null)
						{
							if (scores[0].btm_6.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_6);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_6='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_7":
						if (scores[0].btm_7 != null)
						{
							if (scores[0].btm_7.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_7);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_7='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_8":
						if (scores[0].btm_8 != null)
						{
							if (scores[0].btm_8.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_8);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_8='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_9":
						if (scores[0].btm_9 != null)
						{
							if (scores[0].btm_9.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_9);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_9='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_10":
						if (scores[0].btm_10 != null)
						{
							if (scores[0].btm_10.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_10);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_10='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
					case "btm_11":
						if (scores[0].btm_11 != null)
						{
							if (scores[0].btm_11.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_11);
							}
						}
						get_score = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_11='{0}'    ", get_score).AppendLine();
						break;
					case "btm_12":
						if (scores[0].btm_12 != null)
						{
							if (scores[0].btm_12.Length != 0)
							{
								tmp_ining_score = Convert.ToInt32(scores[0].btm_12);
							}
						}
						tmp_ining_scoreST = Convert.ToString(Convert.ToInt32(get_score) + tmp_ining_score);
						sqlST.AppendLine("		,    ");
						sqlST.AppendFormat("		btm_12='{0}'    ", tmp_ining_scoreST).AppendLine();
						break;
				}

				string total_score;
				if (target.Contains("top"))
				{
					total_score = Convert.ToString(Convert.ToInt32(get_score)
									+ Convert.ToInt32(scores[0].top_total_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", total_score).AppendLine();
				}
				else if (target.Contains("btm"))
				{
					total_score = Convert.ToString(Convert.ToInt32(get_score)
									+ Convert.ToInt32(scores[0].btm_total_score));
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", total_score).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}




		public static void updateRecord_sub(
									int game_id,
									int park_id = 0,
									int ump_id = 0,
									int weather_id = 0,
									int bat_first_team_id = 0,
									int field_first_team_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false,
									bool top_btm_flg = false,
									string top_1 = "",
									string top_2 = "",
									string top_3 = "",
									string top_4 = "",
									string top_5 = "",
									string top_6 = "",
									string top_7 = "",
									string top_8 = "",
									string top_9 = "",
									string top_10 = "",
									string top_11 = "",
									string top_12 = "",
									string top_hit = "",
									string top_error = "",
									string top_total_score = "",
									string btm_1 = "",
									string btm_2 = "",
									string btm_3 = "",
									string btm_4 = "",
									string btm_5 = "",
									string btm_6 = "",
									string btm_7 = "",
									string btm_8 = "",
									string btm_9 = "",
									string btm_10 = "",
									string btm_11 = "",
									string btm_12 = "",
									string btm_hit = "",
									string btm_error = "",
									string btm_total_score = "",
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									string top_teamName = "",
									string btm_teamName = "",
									string top_order_id = "",
									string btm_order_id = "",
									string ining = "",
									int count_b = 0,
									int count_s = 0,
									int count_o = 0,
									bool run_1 = false,
									bool run_2 = false,
									bool run_3 = false,
									int run_1_player_id = 0,
									int run_2_player_id = 0,
									int run_3_player_id = 0)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();
				//sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				//sqlST.AppendLine("		,    ");
				//sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();
				if (ump_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ump_id={0}    ", ump_id).AppendLine();
				}
				if (weather_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		weather_id={0}    ", weather_id).AppendLine();
				}
				if (bat_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		bat_first_team_id={0}    ", bat_first_team_id).AppendLine();
				}
				if (field_first_team_id > 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		field_first_team_id={0}    ", field_first_team_id).AppendLine();
				}
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		top_btm_flg={0}    ", top_btm_flg).AppendLine();
				if (top_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_1='{0}'    ", top_1).AppendLine();
				}
				if (top_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_2='{0}'    ", top_2).AppendLine();
				}
				if (top_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_3='{0}'    ", top_3).AppendLine();
				}
				if (top_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_4='{0}'    ", top_4).AppendLine();
				}
				if (top_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_5='{0}'    ", top_5).AppendLine();
				}
				if (top_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_6='{0}'    ", top_6).AppendLine();
				}
				if (top_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_7='{0}'    ", top_7).AppendLine();
				}
				if (top_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_8='{0}'    ", top_8).AppendLine();
				}
				if (top_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_9='{0}'    ", top_9).AppendLine();
				}
				if (top_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_10='{0}'    ", top_10).AppendLine();
				}
				if (top_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_11='{0}'    ", top_11).AppendLine();
				}
				if (top_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_12='{0}'    ", top_12).AppendLine();
				}
				if (top_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_hit='{0}'    ", top_hit).AppendLine();
				}
				if (top_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_error='{0}'    ", top_error).AppendLine();
				}
				if (top_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_total_score='{0}'    ", top_total_score).AppendLine();
				}
				if (btm_1.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_1='{0}'    ", btm_1).AppendLine();
				}
				if (btm_2.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_2='{0}'    ", btm_2).AppendLine();
				}
				if (btm_3.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_3='{0}'    ", btm_3).AppendLine();
				}
				if (btm_4.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_4='{0}'    ", btm_4).AppendLine();
				}
				if (btm_5.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_5='{0}'    ", btm_5).AppendLine();
				}
				if (btm_6.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_6='{0}'    ", btm_6).AppendLine();
				}
				if (btm_7.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_7='{0}'    ", btm_7).AppendLine();
				}
				if (btm_8.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_8='{0}'    ", btm_8).AppendLine();
				}
				if (btm_9.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_9='{0}'    ", btm_9).AppendLine();
				}
				if (btm_10.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_10='{0}'    ", btm_10).AppendLine();
				}
				if (btm_11.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_11='{0}'    ", btm_11).AppendLine();
				}
				if (btm_12.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_12='{0}'    ", btm_12).AppendLine();
				}
				if (btm_hit.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_hit='{0}'    ", btm_hit).AppendLine();
				}
				if (btm_error.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_error='{0}'    ", btm_error).AppendLine();
				}
				if (btm_total_score.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_total_score='{0}'    ", btm_total_score).AppendLine();
				}
				if (top_order_start_flg)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				}
				if (btm_order_start_flg)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				}
				if (top_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_teamName='{0}'    ", top_teamName).AppendLine();
				}
				if (btm_teamName.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_teamName='{0}'    ", btm_teamName).AppendLine();
				}

				if (top_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		top_order_id='{0}'    ", top_order_id).AppendLine();
				}
				if (btm_order_id.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		btm_order_id='{0}'    ", btm_order_id).AppendLine();
				}
				if (ining.Length != 0)
				{
					sqlST.AppendLine("		,    ");
					sqlST.AppendFormat("		ining='{0}'    ", ining).AppendLine();
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		/// <summary>
		/// 試合中の選手交代フラグの変更
		/// </summary>
		/// <param name="game_id"></param>
		/// <param name="game_start_flg"></param>
		/// <param name="player_change_flg"></param>
		public static void updateGameFlgRecord(
									int game_id = 0,
									bool game_start_flg = false,
									bool player_change_flg = false
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		player_change_flg={0}    ", player_change_flg).AppendLine();


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void updateStartingOrderFlgRecord(
									int game_id = 0,
									bool top_order_start_flg = false,
									bool btm_order_start_flg = false,
									bool game_start_flg = false
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		top_order_start_flg={0}    ", top_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		btm_order_start_flg={0}    ", btm_order_start_flg).AppendLine();
				sqlST.AppendLine("		,    ");
				sqlST.AppendFormat("		game_start_flg={0}    ", game_start_flg).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRunPlayIdFlgRecord(
									int game_id = 0,
									int run_play_id = 0
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();

				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region イニングUpdate SQL
				StringBuilder sqlST = new StringBuilder();

				sqlST.AppendLine("UPDATE tmp_game ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		run_play_id={0}    ", run_play_id).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}    ", game_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion
				CMD_Update.ExecuteReader();
				con.Close();
			}
		}


		public static void deleteRecord(int game_id = 0)
		{
			if (game_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM tmp_game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	game_id=@game_id ");
				CmdDelete.Parameters.AddWithValue("@game_id", game_id);
				CmdDelete.CommandText = sqlST.ToString();


				#endregion
				CmdDelete.ExecuteReader();
				con.Close();
			}
		}


		public class gameData
		{

			#region オーダー変数
			public int game_id { get; set; }
			public int park_id { get; set; }
			public int ump_id { get; set; }
			public int weather_id { get; set; }
			public DateTime start_datetime { get; set; }
			public int bat_first_team_id { get; set; }
			public int field_first_team_id { get; set; }
			public bool game_start_flg { get; set; }
			public bool player_change_flg { get; set; }
			public bool top_btm_flg { get; set; }
			public string top_1 { get; set; }
			public string top_2 { get; set; }
			public string top_3 { get; set; }
			public string top_4 { get; set; }
			public string top_5 { get; set; }
			public string top_6 { get; set; }
			public string top_7 { get; set; }
			public string top_8 { get; set; }
			public string top_9 { get; set; }
			public string top_10 { get; set; }
			public string top_11 { get; set; }
			public string top_12 { get; set; }
			public string top_hit { get; set; }
			public string top_error { get; set; }
			public string top_total_score { get; set; }

			public string btm_1 { get; set; }
			public string btm_2 { get; set; }
			public string btm_3 { get; set; }
			public string btm_4 { get; set; }
			public string btm_5 { get; set; }
			public string btm_6 { get; set; }
			public string btm_7 { get; set; }
			public string btm_8 { get; set; }
			public string btm_9 { get; set; }
			public string btm_10 { get; set; }
			public string btm_11 { get; set; }
			public string btm_12 { get; set; }
			public string btm_hit { get; set; }
			public string btm_error { get; set; }
			public string btm_total_score { get; set; }
			public bool top_order_start_flg { get; set; }
			public bool btm_order_start_flg { get; set; }
			public string top_teamName { get; set; }
			public string btm_teamName { get; set; }
			public string top_order_id { get; set; }
			public string btm_order_id { get; set; }
			public string ining { get; set; }
			public int count_b { get; set; }
			public int count_s { get; set; }
			public int count_o { get; set; }
			public bool run_1 { get; set; }
			public bool run_2 { get; set; }
			public bool run_3 { get; set; }
			public int run_1_player_id { get; set; }
			public int run_2_player_id { get; set; }
			public int run_3_player_id { get; set; }
			public int player_id { get; set; }
			public int ining_box_id { get; set; }
			public int run_play_id { get; set; }
			public int etc_cd1 { get; set; }
			public int etc_cd2 { get; set; }
			public int etc_cd3 { get; set; }
			public int etc_cd4 { get; set; }
			public int etc_cd5 { get; set; }
			#endregion


			public gameData(
									int game_id,
									int park_id,
									int ump_id,
									int weather_id,
									DateTime start_datetime,
									int bat_first_team_id,
									int field_first_team_id,
									bool game_start_flg,
									bool player_change_flg,
									bool top_btm_flg,
									string top_1,
									string top_2,
									string top_3,
									string top_4,
									string top_5,
									string top_6,
									string top_7,
									string top_8,
									string top_9,
									string top_10,
									string top_11,
									string top_12,
									string top_hit,
									string top_error,
									string top_total_score,
									string btm_1,
									string btm_2,
									string btm_3,
									string btm_4,
									string btm_5,
									string btm_6,
									string btm_7,
									string btm_8,
									string btm_9,
									string btm_10,
									string btm_11,
									string btm_12,
									string btm_hit,
									string btm_error,
									string btm_total_score,
									bool top_order_start_flg,
									bool btm_order_start_flg,
									string top_teamName,
									string btm_teamName,
									string top_order_id,
									string btm_order_id,
									string ining,
									int count_b,
									int count_s,
									int count_o,
									bool run_1,
									bool run_2,
									bool run_3,
									int run_1_player_id,
									int run_2_player_id,
									int run_3_player_id,
									int player_id,
									int ining_box_id,
									int run_play_id,
									int etc_cd1,
									int etc_cd2,
									int etc_cd3,
									int etc_cd4,
									int etc_cd5)
			{
				this.game_id = game_id;
				this.park_id = park_id;
				this.ump_id = ump_id;
				this.weather_id = weather_id;
				this.start_datetime = start_datetime;
				this.bat_first_team_id = bat_first_team_id;
				this.field_first_team_id = field_first_team_id;
				this.game_start_flg = game_start_flg;
				this.player_change_flg = player_change_flg;
				this.top_btm_flg = top_btm_flg;
				this.top_1 = top_1;
				this.top_2 = top_2;
				this.top_3 = top_3;
				this.top_4 = top_4;
				this.top_5 = top_5;
				this.top_6 = top_6;
				this.top_7 = top_7;
				this.top_8 = top_8;
				this.top_9 = top_9;
				this.top_10 = top_10;
				this.top_11 = top_11;
				this.top_12 = top_12;
				this.top_hit = top_hit;
				this.top_error = top_error;
				this.top_total_score = top_total_score;
				this.btm_1 = btm_1;
				this.btm_2 = btm_2;
				this.btm_3 = btm_3;
				this.btm_4 = btm_4;
				this.btm_5 = btm_5;
				this.btm_6 = btm_6;
				this.btm_7 = btm_7;
				this.btm_8 = btm_8;
				this.btm_9 = btm_9;
				this.btm_10 = btm_10;
				this.btm_11 = btm_11;
				this.btm_12 = btm_12;
				this.btm_hit = btm_hit;
				this.btm_error = btm_error;
				this.btm_total_score = btm_total_score;
				this.top_order_start_flg = top_order_start_flg;
				this.btm_order_start_flg = btm_order_start_flg;
				this.top_teamName = top_teamName;
				this.btm_teamName = btm_teamName;
				this.top_order_id = top_order_id;
				this.btm_order_id = btm_order_id;
				this.ining = ining;
				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.run_1 = run_1;
				this.run_2 = run_2;
				this.run_3 = run_3;
				this.run_1_player_id = run_1_player_id;
				this.run_2_player_id = run_2_player_id;
				this.run_3_player_id = run_3_player_id;
				this.player_id = player_id;
				this.ining_box_id = ining_box_id;
				this.run_play_id = run_play_id;
				this.etc_cd1 = etc_cd1;
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;
			}
		}

		public static List<gameData> GetRecordsAllMember(string teamName = "")
		{
			List<gameData> orderList = new List<gameData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	tmp_game.* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				if (teamName.Length != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendLine("	teamName=@teamName ");
					cmd_getRec.Parameters.AddWithValue("@teamName", teamName);
				}
				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new gameData(
							game_id: reader.GetInt32(0),
							park_id: reader.GetInt32(1),
							ump_id: reader.GetInt32(2),
							weather_id: reader.GetInt32(3),
							start_datetime: reader.GetDateTime(4),
							bat_first_team_id: reader.GetInt32(5),
							field_first_team_id: reader.GetInt32(6),
							game_start_flg: reader.GetBoolean(7),
							player_change_flg: reader.GetBoolean(8),
							top_btm_flg: reader.GetBoolean(9),
							top_1: reader.GetString(10),
							top_2: reader.GetString(11),
							top_3: reader.GetString(12),
							top_4: reader.GetString(13),
							top_5: reader.GetString(14),
							top_6: reader.GetString(15),
							top_7: reader.GetString(16),
							top_8: reader.GetString(17),
							top_9: reader.GetString(18),
							top_10: reader.GetString(19),
							top_11: reader.GetString(20),
							top_12: reader.GetString(21),
							top_hit: reader.GetString(22),
							top_error: reader.GetString(23),
							top_total_score: reader.GetString(24),
							btm_1: reader.GetString(25),
							btm_2: reader.GetString(26),
							btm_3: reader.GetString(27),
							btm_4: reader.GetString(28),
							btm_5: reader.GetString(29),
							btm_6: reader.GetString(30),
							btm_7: reader.GetString(31),
							btm_8: reader.GetString(32),
							btm_9: reader.GetString(33),
							btm_10: reader.GetString(34),
							btm_11: reader.GetString(35),
							btm_12: reader.GetString(36),
							btm_hit: reader.GetString(37),
							btm_error: reader.GetString(38),
							btm_total_score: reader.GetString(39),
							top_order_start_flg: reader.GetBoolean(40),
							btm_order_start_flg: reader.GetBoolean(41),
							top_teamName: reader.GetString(42),
							btm_teamName: reader.GetString(43),
							top_order_id: reader.GetString(44),
							btm_order_id: reader.GetString(45),
							ining: reader.GetString(46),
							count_b: reader.GetInt32(47),
							count_s: reader.GetInt32(48),
							count_o: reader.GetInt32(49),
							run_1: reader.GetBoolean(50),
							run_2: reader.GetBoolean(51),
							run_3: reader.GetBoolean(52),
							run_1_player_id: reader.GetInt32(53),
							run_2_player_id: reader.GetInt32(54),
							run_3_player_id: reader.GetInt32(55),
							player_id: reader.GetInt32(56),
							ining_box_id: reader.GetInt32(57),
							run_play_id: reader.GetInt32(58),
							etc_cd1: reader.GetInt32(59),
							etc_cd2: reader.GetInt32(60),
							etc_cd3: reader.GetInt32(61),
							etc_cd4: reader.GetInt32(62),
							etc_cd5: reader.GetInt32(63)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class gameDataCount
		{
			public int game_count { get; set; }
			public gameDataCount(int count = 0)
			{
				this.game_count = count;

			}
		}
		public static List<gameDataCount> GetRecordsCount()
		{
			List<gameDataCount> countList = new List<gameDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				//sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	CASE ");
				//sqlST.AppendLine("		WHEN ");
				//sqlST.AppendLine("			MAX(game_id) is null THEN 1 ");
				//sqlST.AppendLine("		ELSE ");
				//sqlST.AppendLine("			MAX(game_id) + 1 ");
				//sqlST.AppendLine("	END AS game_id ");
				//sqlST.AppendLine("FROM ");
				//sqlST.AppendLine("	game ");
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(game_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public class gameDataIningBoxId
		{
			public int ining_box_id { get; set; }
			public gameDataIningBoxId(int count = 0)
			{
				this.ining_box_id = count;

			}
		}
		public static List<gameDataIningBoxId> GetRecordsIningBoxId(int box_id = 0)
		{
			List<gameDataIningBoxId> countList = new List<gameDataIningBoxId>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ining_box_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameDataIningBoxId(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		public class gameIdCount
		{
			public int game_id { get; set; }
			public gameIdCount(int count = 0)
			{
				this.game_id = count;

			}
		}
		public static List<gameIdCount> GetGameIdRecord()
		{
			List<gameIdCount> countList = new List<gameIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(game_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(game_id) + 1 ");
				sqlST.AppendLine("	END AS game_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");

				//sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	MAX(game_id) + 1 ");
				//sqlST.AppendLine("FROM ");
				//sqlST.AppendLine("	game ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new gameIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}


		public class gameFlgs
		{
			public int game_id { get; set; }
			public bool game_start_flg { get; set; }
			public bool player_change_flg { get; set; }
			public gameFlgs(
						int game_id = 0,
						bool game_start_flg = false,
						bool player_change_flg = false
				)
			{
				this.game_id = game_id;
				this.game_start_flg = game_start_flg;
				this.player_change_flg = player_change_flg;

			}
		}
		public static List<gameFlgs> GetPlayerChangeFlg(int game_id = 0)
		{
			List<gameFlgs> countList = new List<gameFlgs>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	game_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	game_start_flg ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	player_change_flg ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id ", game_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(
						new gameFlgs(
							game_id: reader.GetInt32(0),
							game_start_flg: reader.GetBoolean(1),
							player_change_flg: reader.GetBoolean(2)
							)
						);
				}
			}
			return countList;
		}



		public static List<gameData> GetRecords(int game_id = 0)
		{

			List<gameData> orderList = new List<gameData>();
			if (game_id == 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	tmp_game.* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id);

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new gameData(
							game_id: reader.GetInt32(0),
							park_id: reader.GetInt32(1),
							ump_id: reader.GetInt32(2),
							weather_id: reader.GetInt32(3),
							start_datetime: reader.GetDateTime(4),
							bat_first_team_id: reader.GetInt32(5),
							field_first_team_id: reader.GetInt32(6),
							game_start_flg: reader.GetBoolean(7),
							player_change_flg: reader.GetBoolean(8),
							top_btm_flg: reader.GetBoolean(9),
							top_1: reader.GetString(10),
							top_2: reader.GetString(11),
							top_3: reader.GetString(12),
							top_4: reader.GetString(13),
							top_5: reader.GetString(14),
							top_6: reader.GetString(15),
							top_7: reader.GetString(16),
							top_8: reader.GetString(17),
							top_9: reader.GetString(18),
							top_10: reader.GetString(19),
							top_11: reader.GetString(20),
							top_12: reader.GetString(21),
							top_hit: reader.GetString(22),
							top_error: reader.GetString(23),
							top_total_score: reader.GetString(24),
							btm_1: reader.GetString(25),
							btm_2: reader.GetString(26),
							btm_3: reader.GetString(27),
							btm_4: reader.GetString(28),
							btm_5: reader.GetString(29),
							btm_6: reader.GetString(30),
							btm_7: reader.GetString(31),
							btm_8: reader.GetString(32),
							btm_9: reader.GetString(33),
							btm_10: reader.GetString(34),
							btm_11: reader.GetString(35),
							btm_12: reader.GetString(36),
							btm_hit: reader.GetString(37),
							btm_error: reader.GetString(38),
							btm_total_score: reader.GetString(39),
							top_order_start_flg: reader.GetBoolean(40),
							btm_order_start_flg: reader.GetBoolean(41),
							top_teamName: reader.GetString(42),
							btm_teamName: reader.GetString(43),
							top_order_id: reader.GetString(44),
							btm_order_id: reader.GetString(45),
							ining: reader.GetString(46),
							count_b: reader.GetInt32(47),
							count_s: reader.GetInt32(48),
							count_o: reader.GetInt32(49),
							run_1: reader.GetBoolean(50),
							run_2: reader.GetBoolean(51),
							run_3: reader.GetBoolean(52),
							run_1_player_id: reader.GetInt32(53),
							run_2_player_id: reader.GetInt32(54),
							run_3_player_id: reader.GetInt32(55),
							player_id: reader.GetInt32(56),
							ining_box_id: reader.GetInt32(57),
							run_play_id: reader.GetInt32(58),
							etc_cd1: reader.GetInt32(59),
							etc_cd2: reader.GetInt32(60),
							etc_cd3: reader.GetInt32(61),
							etc_cd4: reader.GetInt32(62),
							etc_cd5: reader.GetInt32(63)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


		public class gameDataToTal
		{

			#region オーダー変数
			public int game_id { get; set; }
			public string top_1 { get; set; }
			public string top_2 { get; set; }
			public string top_3 { get; set; }
			public string top_4 { get; set; }
			public string top_5 { get; set; }
			public string top_6 { get; set; }
			public string top_7 { get; set; }
			public string top_8 { get; set; }
			public string top_9 { get; set; }
			public string top_10 { get; set; }
			public string top_11 { get; set; }
			public string top_12 { get; set; }
			public string top_hit { get; set; }
			public string top_error { get; set; }
			public string top_total_score { get; set; }

			public string btm_1 { get; set; }
			public string btm_2 { get; set; }
			public string btm_3 { get; set; }
			public string btm_4 { get; set; }
			public string btm_5 { get; set; }
			public string btm_6 { get; set; }
			public string btm_7 { get; set; }
			public string btm_8 { get; set; }
			public string btm_9 { get; set; }
			public string btm_10 { get; set; }
			public string btm_11 { get; set; }
			public string btm_12 { get; set; }
			public string btm_hit { get; set; }
			public string btm_error { get; set; }
			public string btm_total_score { get; set; }

			#endregion


			public gameDataToTal(
									string top_1,
									string top_2,
									string top_3,
									string top_4,
									string top_5,
									string top_6,
									string top_7,
									string top_8,
									string top_9,
									string top_10,
									string top_11,
									string top_12,
									string top_hit,
									string top_error,
									string top_total_score,
									string btm_1,
									string btm_2,
									string btm_3,
									string btm_4,
									string btm_5,
									string btm_6,
									string btm_7,
									string btm_8,
									string btm_9,
									string btm_10,
									string btm_11,
									string btm_12,
									string btm_hit,
									string btm_error,
									string btm_total_score)
			{
				int total_top = 0;
				this.top_1 = top_1;
				this.top_2 = top_2;
				this.top_3 = top_3;
				this.top_4 = top_4;
				this.top_5 = top_5;
				this.top_6 = top_6;
				this.top_7 = top_7;
				this.top_8 = top_8;
				this.top_9 = top_9;
				this.top_10 = top_10;
				this.top_11 = top_11;
				this.top_12 = top_12;

				this.btm_1 = btm_1;
				this.btm_2 = btm_2;
				this.btm_3 = btm_3;
				this.btm_4 = btm_4;
				this.btm_5 = btm_5;
				this.btm_6 = btm_6;
				this.btm_7 = btm_7;
				this.btm_8 = btm_8;
				this.btm_9 = btm_9;
				this.btm_10 = btm_10;
				this.btm_11 = btm_11;
				this.btm_12 = btm_12;
				if (top_1.Length != 0)
				{
					total_top += Convert.ToInt32(top_1);
				}
				if (top_2.Length != 0)
				{
					total_top += Convert.ToInt32(top_2);
				}
				if (top_3.Length != 0)
				{
					total_top += Convert.ToInt32(top_3);
				}
				if (top_4.Length != 0)
				{
					total_top += Convert.ToInt32(top_4);
				}
				if (top_5.Length != 0)
				{
					total_top += Convert.ToInt32(top_5);
				}
				if (top_6.Length != 0)
				{
					total_top += Convert.ToInt32(top_6);
				}
				if (top_7.Length != 0)
				{
					total_top += Convert.ToInt32(top_7);
				}
				if (top_8.Length != 0)
				{
					total_top += Convert.ToInt32(top_8);
				}
				if (top_9.Length != 0)
				{
					total_top += Convert.ToInt32(top_9);
				}
				if (top_10.Length != 0)
				{
					total_top += Convert.ToInt32(top_10);
				}
				if (top_11.Length != 0)
				{
					total_top += Convert.ToInt32(top_11);
				}
				if (top_12.Length != 0)
				{
					total_top += Convert.ToInt32(top_12);
				}
				this.top_total_score = total_top.ToString();
				this.top_hit = top_hit;
				this.top_error = top_error;


				int total_btm = 0;
				if (btm_1.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_1);
				}
				if (btm_2.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_2);
				}
				if (btm_3.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_3);
				}
				if (btm_4.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_4);
				}
				if (btm_5.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_5);
				}
				if (btm_6.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_6);
				}
				if (btm_7.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_7);
				}
				if (btm_8.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_8);
				}
				if (btm_9.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_9);
				}
				if (btm_10.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_10);
				}
				if (btm_11.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_11);
				}
				if (btm_12.Length != 0)
				{
					total_btm += Convert.ToInt32(btm_12);
				}
				this.btm_total_score = total_btm.ToString();
				this.btm_hit = btm_hit;
				this.btm_error = btm_error;
			}
		}
		public static List<gameDataToTal> GetRecordScores(int game_id = 0)
		{

			List<gameDataToTal> orderList = new List<gameDataToTal>();
			if (game_id == 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand cmd_getRec = new SqliteCommand();
				cmd_getRec.Connection = con;
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	top_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_4 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_5 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_6 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_7 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_8 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_9 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_10 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_11 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_12 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_hit ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_error ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	top_total_score ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_4 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_5 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_6 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_7 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_8 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_9 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_10 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_11 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_12 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_hit ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_error ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	btm_total_score ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	tmp_game ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id);

				cmd_getRec.CommandText = sqlST.ToString();
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new gameDataToTal(
							top_1: reader.GetString(0),
							top_2: reader.GetString(1),
							top_3: reader.GetString(2),
							top_4: reader.GetString(3),
							top_5: reader.GetString(4),
							top_6: reader.GetString(5),
							top_7: reader.GetString(6),
							top_8: reader.GetString(7),
							top_9: reader.GetString(8),
							top_10: reader.GetString(9),
							top_11: reader.GetString(10),
							top_12: reader.GetString(11),
							top_hit: reader.GetString(12),
							top_error: reader.GetString(13),
							top_total_score: reader.GetString(14),
							btm_1: reader.GetString(15),
							btm_2: reader.GetString(16),
							btm_3: reader.GetString(17),
							btm_4: reader.GetString(18),
							btm_5: reader.GetString(19),
							btm_6: reader.GetString(20),
							btm_7: reader.GetString(21),
							btm_8: reader.GetString(22),
							btm_9: reader.GetString(23),
							btm_10: reader.GetString(24),
							btm_11: reader.GetString(25),
							btm_12: reader.GetString(26),
							btm_hit: reader.GetString(27),
							btm_error: reader.GetString(28),
							btm_total_score: reader.GetString(29)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}


	}
	#endregion

	#region 投球成績
	class BallData
	{

		public BallData()
		{

		}
		#region 投球初期化
		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{

				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	ball ( ");
				/// 基本情報
				sqlST.AppendLine("		ball_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		season  , ");
				sqlST.AppendLine("		player_id , ");
				sqlST.AppendLine("		team_id  , ");
				/// 5
				sqlST.AppendLine("		position  , ");
				sqlST.AppendLine("		player_num  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		ball_box_num   , ");
				/// 10
				sqlST.AppendLine("		ball_total_num   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		game_box_num    , ");
				/// 15
				sqlST.AppendLine("		park_id    , ");
				sqlST.AppendLine("		bat_id     , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				//sqlST.AppendLine("		weather_id    , ");
				/// 20
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				/// 25
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				/// 30
				sqlST.AppendLine("		ball_type   , ");
				sqlST.AppendLine("		ball_speed   , ");
				sqlST.AppendLine("		ball_x   , ");
				sqlST.AppendLine("		ball_y   , ");
				sqlST.AppendLine("		cource_table_id     , ");
				/// 35
				sqlST.AppendLine("		pick_off     , ");
				sqlST.AppendLine("		in_play     , ");
				sqlST.AppendLine("		steal     , ");
				sqlST.AppendLine("		wildpitch     , ");
				sqlST.AppendLine("		passball     , ");

				/// 40
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				/// 45
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				/// 50
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				sqlST.AppendLine("		etc_date1   , ");
				sqlST.AppendLine("		etc_date2   , ");
				/// 55
				sqlST.AppendLine("		etc_date3   , ");
				sqlST.AppendLine("		etc_date4   , ");
				sqlST.AppendLine("		etc_date5   , ");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				//	追加
				sqlST.AppendLine("		,ball_level    ");
				sqlST.AppendLine("		,ball_action    ");
				sqlST.AppendLine("		) ");
				/// 60
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}
		#endregion

		#region 投球新規追加
		public static void addRecord(
							int ball_id = 0,
							int box_id = 0,
							int player_id = 0,
							int team_id = 0,
							int position = 0,
							int player_num = 0,
							int pitcher_id = 0,
							int pit_team_id = 0,
							int ball_box_num = 0,
							int ball_total_num = 0,
							int cat_id = 0,
							int ump_id = 0,
							int game_id = 0,
							int game_box_num = 0,
							int park_id = 0,
							int bat_id = 0,
							int pit_hand_id = 0,
							int pit_throw_id = 0,
							int count_b = 0,
							int count_s = 0,
							int count_o = 0,
							bool runner_1 = false,
							bool runner_2 = false,
							bool runner_3 = false,
							int ining = 0,
							bool top_bot = false,
							int top_score = 0,
							int bottom_score = 0,
							int ball_type = 0,
							int ball_speed = 0,
							int ball_x = 0,
							int ball_y = 0,
							int cource_table_id = 0,
							int pick_off = 0,
							bool in_play = false,
							bool steal = false,
							bool wildpitch = false,
							bool passball = false,
							int etc_cd1 = 0,
							int etc_cd2 = 0,
							int etc_cd3 = 0,
							int etc_cd4 = 0,
							int etc_cd5 = 0,
							string etc_str1 = "",
							string etc_str2 = "",
							string etc_str3 = "",
							string etc_str4 = "",
							string etc_str5 = "",
							DateTime? etc_date1 = null,
							DateTime? etc_date2 = null,
							DateTime? etc_date3 = null,
							DateTime? etc_date4 = null,
							DateTime? etc_date5 = null,
							DateTime? update_date = null,
							int ball_level = 0,
							int ball_action = -1
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");

			/// box_idを新たに作成する
			//string sqlGetCountST = "SELECT COUNT(ball_id) FROM ball";
			StringBuilder sqlGetCountST = new StringBuilder();
			sqlGetCountST.AppendLine("	SELECT ");
			sqlGetCountST.AppendLine("		CASE ");
			sqlGetCountST.AppendLine("			WHEN ");
			sqlGetCountST.AppendLine("				MAX(ball_id) is null THEN 0 ");
			sqlGetCountST.AppendLine("			ELSE ");
			sqlGetCountST.AppendLine("				MAX(ball_id) ");
			sqlGetCountST.AppendLine("		END AS _max ");
			sqlGetCountST.AppendLine("	FROM ");
			sqlGetCountST.AppendLine("		ball ");

			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCountST.ToString(), precon);
				CMD_GetCount.CommandText = sqlGetCountST.ToString();
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				while (reader.Read())
				{
					ball_id = Convert.ToInt32(reader.GetInt32(0));
				}
				//ball_id = Convert.ToInt32(reader);
				ball_id += 1;
				precon.Close();
			}


			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		ball_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id , ");
				#region 基本データ

				sqlST.AppendLine("		team_id  , ");

				sqlST.AppendLine("		position   , ");

				sqlST.AppendLine("		player_num    , ");

				sqlST.AppendLine("		pitcher_id     , ");

				sqlST.AppendLine("		pit_team_id     , ");

				sqlST.AppendLine("		ball_box_num      , ");

				sqlST.AppendLine("		ball_total_num       , ");

				sqlST.AppendLine("		cat_id        , ");

				sqlST.AppendLine("		ump_id         , ");

				sqlST.AppendLine("		game_id          , ");

				sqlST.AppendLine("		game_box_num          , ");

				sqlST.AppendLine("		park_id          , ");

				sqlST.AppendLine("		bat_id  , ");

				sqlST.AppendLine("		pit_hand_id  , ");

				sqlST.AppendLine("		pit_throw_id   , ");

				sqlST.AppendLine("		ball_action   , ");


				#endregion

				/// カウント
				#region カウント

				sqlST.AppendLine("		count_b   , ");

				sqlST.AppendLine("		count_s  , ");

				sqlST.AppendLine("		count_o , ");

				sqlST.AppendLine("		runner_1  , ");
				sqlST.AppendLine("		runner_2  , ");
				sqlST.AppendLine("		runner_3  , ");

				sqlST.AppendLine("		ining  , ");

				sqlST.AppendLine("		top_bot    , ");


				sqlST.AppendLine("		top_score    , ");

				sqlST.AppendLine("		bottom_score    , ");

				sqlST.AppendLine("		ball_type    , ");

				sqlST.AppendLine("		ball_speed     , ");

				#region 変化量

				sqlST.AppendLine("		ball_level,	");


				#endregion

				sqlST.AppendLine("		ball_x      , ");

				sqlST.AppendLine("		ball_y    ,  ");

				sqlST.AppendLine("		cource_table_id     , ");

				sqlST.AppendLine("		pick_off    , ");

				sqlST.AppendLine("		in_play    , ");
				sqlST.AppendLine("		steal    , ");
				sqlST.AppendLine("		wildpitch    , ");
				sqlST.AppendLine("		passball    , ");

				#endregion

				/// 予備情報
				#region 予備

				sqlST.AppendLine("		etc_cd1   , ");

				sqlST.AppendLine("		etc_cd2   , ");

				sqlST.AppendLine("		etc_cd3   , ");

				sqlST.AppendLine("		etc_cd4   , ");

				sqlST.AppendLine("		etc_cd5   , ");

				sqlST.AppendLine("		etc_str1   , ");

				sqlST.AppendLine("		etc_str2   , ");

				sqlST.AppendLine("		etc_str3   , ");

				sqlST.AppendLine("		etc_str4   , ");

				sqlST.AppendLine("		etc_str5   , ");

				//sqlST.AppendLine("		etc_date1   , ");

				//sqlST.AppendLine("		etc_date2   , ");

				//sqlST.AppendLine("		etc_date3   , ");

				//sqlST.AppendLine("		etc_date4   , ");

				//sqlST.AppendLine("		etc_date5   , ");

				#endregion


				///　更新日

				sqlST.AppendLine("		update_date    ");

				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");

				/// 基本情報
				#region 基本
				sqlST.AppendLine("		@ball_id , ");
				CMD_Insert.Parameters.AddWithValue("@ball_id", ball_id);
				sqlST.AppendLine("		@box_id , ");
				CMD_Insert.Parameters.AddWithValue("@box_id", box_id);
				sqlST.AppendLine("		@player_id , ");
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);



				sqlST.AppendLine("		@team_id  , ");
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);

				sqlST.AppendLine("		@position   , ");
				CMD_Insert.Parameters.AddWithValue("@position", position);

				sqlST.AppendLine("		@player_num    , ");
				CMD_Insert.Parameters.AddWithValue("@player_num", player_num);

				sqlST.AppendLine("		@pitcher_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pitcher_id", pitcher_id);

				sqlST.AppendLine("		@pit_team_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pit_team_id", pit_team_id);

				sqlST.AppendLine("		@ball_box_num      , ");
				CMD_Insert.Parameters.AddWithValue("@ball_box_num", ball_box_num);

				sqlST.AppendLine("		@ball_total_num       , ");
				CMD_Insert.Parameters.AddWithValue("@ball_total_num", ball_total_num);

				sqlST.AppendLine("		@cat_id        , ");
				CMD_Insert.Parameters.AddWithValue("@cat_id", cat_id);

				sqlST.AppendLine("		@ump_id         , ");
				CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);

				sqlST.AppendLine("		@game_id          , ");
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);

				sqlST.AppendLine("		@game_box_num          , ");
				CMD_Insert.Parameters.AddWithValue("@game_box_num", game_box_num);


				sqlST.AppendLine("		@park_id          , ");
				CMD_Insert.Parameters.AddWithValue("@park_id", park_id);

				sqlST.AppendLine("		@bat_id  , ");
				CMD_Insert.Parameters.AddWithValue("@bat_id", bat_id);

				sqlST.AppendLine("		@pit_hand_id  , ");
				CMD_Insert.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);

				sqlST.AppendLine("		@pit_throw_id   , ");
				CMD_Insert.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);

				sqlST.AppendLine("		@ball_action   , ");
				CMD_Insert.Parameters.AddWithValue("@ball_action", ball_action);


				#endregion
				/// カウント
				#region カウント

				sqlST.AppendLine("		@count_b   , ");
				CMD_Insert.Parameters.AddWithValue("@count_b", count_b);

				sqlST.AppendLine("		@count_s  , ");
				CMD_Insert.Parameters.AddWithValue("@count_s", count_s);

				sqlST.AppendLine("		@count_o , ");
				CMD_Insert.Parameters.AddWithValue("@count_o", count_o);

				sqlST.AppendLine("		@runner_1  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1", runner_1);
				sqlST.AppendLine("		@runner_2  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2", runner_2);
				sqlST.AppendLine("		@runner_3  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3", runner_3);

				sqlST.AppendLine("		@ining  , ");
				CMD_Insert.Parameters.AddWithValue("@ining", ining);

				sqlST.AppendLine("		@top_bot    , ");
				CMD_Insert.Parameters.AddWithValue("@top_bot", top_bot);

				sqlST.AppendLine("		@top_score    , ");
				CMD_Insert.Parameters.AddWithValue("@top_score", top_score);

				sqlST.AppendLine("		@bottom_score    , ");
				CMD_Insert.Parameters.AddWithValue("@bottom_score", bottom_score);

				sqlST.AppendLine("		@ball_type    , ");
				CMD_Insert.Parameters.AddWithValue("@ball_type", ball_type);

				sqlST.AppendLine("		@ball_speed     , ");
				CMD_Insert.Parameters.AddWithValue("@ball_speed", ball_speed);

				#endregion
				#region 変化量

				sqlST.AppendLine("		@ball_level,	");
				CMD_Insert.Parameters.AddWithValue("@ball_level", ball_level);


				sqlST.AppendLine("		@ball_x      , ");
				CMD_Insert.Parameters.AddWithValue("@ball_x", ball_x);

				sqlST.AppendLine("		@ball_y    ,  ");
				CMD_Insert.Parameters.AddWithValue("@ball_y", ball_y);




				sqlST.AppendLine("		@cource_table_id     , ");
				CMD_Insert.Parameters.AddWithValue("@cource_table_id", cource_table_id);

				sqlST.AppendLine("		@pick_off    , ");
				CMD_Insert.Parameters.AddWithValue("@pick_off", pick_off);


				sqlST.AppendLine("		@in_play    , ");
				CMD_Insert.Parameters.AddWithValue("@in_play", in_play);

				sqlST.AppendLine("		@steal    , ");
				CMD_Insert.Parameters.AddWithValue("@steal", steal);

				sqlST.AppendLine("		@wildpitch    , ");
				CMD_Insert.Parameters.AddWithValue("@wildpitch", wildpitch);

				sqlST.AppendLine("		@passball    , ");
				CMD_Insert.Parameters.AddWithValue("@passball", passball);

				#endregion

				/// 予備情報
				#region 予備

				sqlST.AppendLine("		@etc_cd1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);

				sqlST.AppendLine("		@etc_cd2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);

				sqlST.AppendLine("		@etc_cd3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);

				sqlST.AppendLine("		@etc_cd4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);

				sqlST.AppendLine("		@etc_cd5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);

				sqlST.AppendLine("		@etc_str1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);

				sqlST.AppendLine("		@etc_str2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);

				sqlST.AppendLine("		@etc_str3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);

				sqlST.AppendLine("		@etc_str4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);

				sqlST.AppendLine("		@etc_str5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);

				//sqlST.AppendLine("		@etc_date1   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date1", etc_date1);

				//sqlST.AppendLine("		@etc_date2   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date2", etc_date2);

				//sqlST.AppendLine("		@etc_date3   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date3", etc_date3);

				//sqlST.AppendLine("		@etc_date4   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date4", etc_date4);

				//sqlST.AppendLine("		@etc_date5   , ");
				//CMD_Insert.Parameters.AddWithValue("@etc_date5", etc_date5);

				#endregion


				///　更新日

				sqlST.AppendLine("		@update_date    ");
				CMD_Insert.Parameters.AddWithValue("@update_date", update_date);

				sqlST.AppendLine(" ); ");

				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}
		#endregion


		public class getCount
		{
			public int count { get; set; }
			public getCount(int count = 0)
			{
				this.count = count;
			}
		}

		#region カウント
		public static List<getCount> getCountRecord()
		{
			List<getCount> orderList = new List<getCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(ball_id)		");
				sqlST.AppendLine("FROM	");
				sqlST.AppendLine("	ball	");

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new getCount(
								reader.GetInt32(0)
								)
						);
				}
				con.Close();
			}
			return orderList;

		}
		#endregion


		#region 削除
		public static void delRecord(int ball_id)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM	");
				sqlST.AppendLine("	ball	");
				sqlST.AppendLine("WHERE	");
				sqlST.AppendFormat("	ball_id={0}	", ball_id).AppendLine();
				SqliteCommand cmdDel = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmdDel.ExecuteReader();
				con.Close();
			}
		}



		#endregion


		#region 投球　修正
		public static void updateRecord(
							int ball_id = 0,
							int box_id = 0,
							int player_id = 0,
							int team_id = 0,
							int position = 0,
							int player_num = 0,
							int pitcher_id = 0,
							int pit_team_id = 0,
							int ball_box_num = 0,
							int ball_total_num = 0,
							int cat_id = 0,
							int ump_id = 0,
							int game_id = 0,
							int game_box_num = 0,
							int park_id = 0,
							int bat_id = 0,
							int pit_hand_id = 0,
							int pit_throw_id = 0,
							int count_b = 0,
							int count_s = 0,
							int count_o = 0,
							bool runner_1 = false,
							bool runner_2 = false,
							bool runner_3 = false,
							int ining = 0,
							bool top_bot = false,
							int top_score = 0,
							int bottom_score = 0,
							int ball_type = 0,
							int ball_speed = 0,
							int ball_x = 0,
							int ball_y = 0,
							int cource_table_id = 0,
							int pick_off = 0,
							bool in_play = false,
							bool steal = false,
							bool wildpitch = false,
							bool passball = false,
							int etc_cd1 = 0,
							int etc_cd2 = 0,
							int etc_cd3 = 0,
							int etc_cd4 = 0,
							int etc_cd5 = 0,
							string etc_str1 = "",
							string etc_str2 = "",
							string etc_str3 = "",
							string etc_str4 = "",
							string etc_str5 = "",
							DateTime? etc_date1 = null,
							DateTime? etc_date2 = null,
							DateTime? etc_date3 = null,
							DateTime? etc_date4 = null,
							DateTime? etc_date5 = null,
							DateTime? update_date = null,
							int ball_level = 0,
							int ball_action = -1
			)
		{
			if (player_id == 0)
			{
				Console.WriteLine("player_idが指定されていません");
				return;
			}

			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE box ");
				sqlST.AppendLine("	SET ");
				/// 基本情報
				#region 基本
				//sqlST.AppendLine("		box_id= ");
				//sqlST.AppendLine("		@box_id , ");
				//CMD_Update.Parameters.AddWithValue("@box_id", box_id);

				sqlST.AppendLine("		player_id= ");
				sqlST.AppendLine("		@player_id , ");
				CMD_Update.Parameters.AddWithValue("@player_id", player_id);


				if (team_id != 0)
				{
					sqlST.AppendLine("		team_id= ");
					sqlST.AppendLine("		@team_id  , ");
					CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				}
				if (position != 0)
				{
					sqlST.AppendLine("		position=   ");
					sqlST.AppendLine("		@position   , ");
					CMD_Update.Parameters.AddWithValue("@position", position);
				}
				if (player_num != 0)
				{
					sqlST.AppendLine("		player_num=   ");
					sqlST.AppendLine("		@player_num    , ");
					CMD_Update.Parameters.AddWithValue("@player_num", player_num);
				}
				if (pitcher_id != 0)
				{
					sqlST.AppendLine("		pitcher_id=   ");
					sqlST.AppendLine("		@pitcher_id     , ");
					CMD_Update.Parameters.AddWithValue("@pitcher_id", pitcher_id);
				}
				if (pit_team_id != 0)
				{
					sqlST.AppendLine("		pit_team_id=      ");
					sqlST.AppendLine("		@pit_team_id     , ");
					CMD_Update.Parameters.AddWithValue("@pit_team_id", pit_team_id);
				}
				if (ball_box_num != 0)
				{
					sqlST.AppendLine("		ball_box_num=       ");
					sqlST.AppendLine("		@ball_box_num      , ");
					CMD_Update.Parameters.AddWithValue("@ball_box_num", ball_box_num);
				}
				if (ball_total_num != 0)
				{
					sqlST.AppendLine("		ball_total_num=       ");
					sqlST.AppendLine("		@ball_total_num       , ");
					CMD_Update.Parameters.AddWithValue("@ball_total_num", ball_total_num);
				}
				if (cat_id != 0)
				{
					sqlST.AppendLine("		cat_id=         ");
					sqlST.AppendLine("		@cat_id        , ");
					CMD_Update.Parameters.AddWithValue("@cat_id", cat_id);
				}
				if (ump_id != 0)
				{
					sqlST.AppendLine("		ump_id=          ");
					sqlST.AppendLine("		@ump_id         , ");
					CMD_Update.Parameters.AddWithValue("@ump_id", ump_id);
				}
				if (game_id != 0)
				{
					sqlST.AppendLine("		game_id=          ");
					sqlST.AppendLine("		@game_id          , ");
					CMD_Update.Parameters.AddWithValue("@game_id", game_id);
				}
				if (game_box_num != 0)
				{
					sqlST.AppendLine("		game_box_num=           ");
					sqlST.AppendLine("		@game_box_num          , ");
					CMD_Update.Parameters.AddWithValue("@game_box_num", game_box_num);
				}
				if (park_id != 0)
				{
					sqlST.AppendLine("		park_id=           ");
					sqlST.AppendLine("		@park_id          , ");
					CMD_Update.Parameters.AddWithValue("@park_id", park_id);
				}
				if (bat_id != 0)
				{
					sqlST.AppendLine("		bat_id=   ");
					sqlST.AppendLine("		@bat_id  , ");
					CMD_Update.Parameters.AddWithValue("@bat_id", bat_id);
				}
				if (pit_hand_id != 0)
				{
					sqlST.AppendLine("		pit_hand_id= ");
					sqlST.AppendLine("		@pit_hand_id  , ");
					CMD_Update.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);
				}
				if (pit_throw_id != 0)
				{
					sqlST.AppendLine("		pit_throw_id=    ");
					sqlST.AppendLine("		@pit_throw_id   , ");
					CMD_Update.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);
				}
				if (ball_action >= 0)
				{
					sqlST.AppendLine("		ball_action=    ");
					sqlST.AppendLine("		@ball_action   , ");
					CMD_Update.Parameters.AddWithValue("@ball_action", ball_action);
				}


				#endregion
				/// カウント
				#region カウント
				if (count_b >= 0)
				{
					sqlST.AppendLine("		count_b=    ");
					sqlST.AppendLine("		@count_b   , ");
					CMD_Update.Parameters.AddWithValue("@count_b", count_b);
				}
				if (count_s >= 0)
				{
					sqlST.AppendLine("		count_s=    ");
					sqlST.AppendLine("		@count_s  , ");
					CMD_Update.Parameters.AddWithValue("@count_s", count_s);
				}
				if (count_o >= 0)
				{
					sqlST.AppendLine("		count_o=    ");
					sqlST.AppendLine("		@count_o , ");
					CMD_Update.Parameters.AddWithValue("@count_o", count_o);
				}
				sqlST.AppendLine("		runner_1=    ");
				sqlST.AppendLine("		@runner_1  , ");
				CMD_Update.Parameters.AddWithValue("@runner_1", runner_1);
				sqlST.AppendLine("		runner_2=    ");
				sqlST.AppendLine("		@runner_2  , ");
				CMD_Update.Parameters.AddWithValue("@runner_2", runner_2);
				sqlST.AppendLine("		runner_3=    ");
				sqlST.AppendLine("		@runner_3  , ");
				CMD_Update.Parameters.AddWithValue("@runner_3", runner_3);
				if (ining != 0)
				{
					sqlST.AppendLine("		ining=    ");
					sqlST.AppendLine("		@ining  , ");
					CMD_Update.Parameters.AddWithValue("@ining", ining);
				}
				sqlST.AppendLine("		top_bot=     ");
				sqlST.AppendLine("		@top_bot    , ");
				CMD_Update.Parameters.AddWithValue("@top_bot", top_bot);
				if (top_score != 0)
				{
					sqlST.AppendLine("		top_score=     ");
					sqlST.AppendLine("		@top_score    , ");
					CMD_Update.Parameters.AddWithValue("@top_score", top_score);
				}
				if (bottom_score != 0)
				{
					sqlST.AppendLine("		bottom_score=     ");
					sqlST.AppendLine("		@bottom_score    , ");
					CMD_Update.Parameters.AddWithValue("@bottom_score", bottom_score);
				}

				if (ball_type != 0)
				{
					sqlST.AppendLine("		ball_type=     ");
					sqlST.AppendLine("		@ball_type    , ");
					CMD_Update.Parameters.AddWithValue("@ball_type", ball_type);
				}
				if (ball_speed >= 0)
				{
					sqlST.AppendLine("		ball_speed=     ");
					sqlST.AppendLine("		@ball_speed     , ");
					CMD_Update.Parameters.AddWithValue("@ball_speed", ball_speed);
				}
				#region 変化量
				if (ball_level != 0)
				{
					sqlST.AppendLine("		ball_level=	");
					sqlST.AppendLine("		@ball_level,	");
					CMD_Update.Parameters.AddWithValue("@ball_level", ball_level);
				}
				#endregion
				if (ball_x != 0)
				{
					sqlST.AppendLine("		ball_x=     ");
					sqlST.AppendLine("		@ball_x      , ");
					CMD_Update.Parameters.AddWithValue("@ball_x", ball_x);
				}
				if (ball_y != 0)
				{
					sqlST.AppendLine("		ball_y=     ");
					sqlST.AppendLine("		@ball_y    ,  ");
					CMD_Update.Parameters.AddWithValue("@ball_y", ball_y);
				}


				if (cource_table_id >= 0)
				{
					sqlST.AppendLine("		cource_table_id=     ");
					sqlST.AppendLine("		@cource_table_id     , ");
					CMD_Update.Parameters.AddWithValue("@cource_table_id", cource_table_id);
				}
				if (pick_off != 0)
				{
					sqlST.AppendLine("		pick_off=     ");
					sqlST.AppendLine("		@pick_off    , ");
					CMD_Update.Parameters.AddWithValue("@pick_off", pick_off);
				}

				sqlST.AppendLine("		in_play=     ");
				sqlST.AppendLine("		@in_play    , ");
				CMD_Update.Parameters.AddWithValue("@in_play", in_play);

				sqlST.AppendLine("		steal=     ");
				sqlST.AppendLine("		@steal    , ");
				CMD_Update.Parameters.AddWithValue("@steal", steal);

				sqlST.AppendLine("		wildpitch=     ");
				sqlST.AppendLine("		@wildpitch    , ");
				CMD_Update.Parameters.AddWithValue("@wildpitch", wildpitch);

				sqlST.AppendLine("		passball=     ");
				sqlST.AppendLine("		@passball    , ");
				CMD_Update.Parameters.AddWithValue("@passball", passball);



				#endregion

				/// 予備
				#region 予備
				if (etc_cd1 >= 0)
				{
					sqlST.AppendLine("		etc_cd1=   ");
					sqlST.AppendLine("		@etc_cd1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				}
				if (etc_cd2 != 0)
				{
					sqlST.AppendLine("		etc_cd2=   ");
					sqlST.AppendLine("		@etc_cd2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				}
				if (etc_cd3 != 0)
				{
					sqlST.AppendLine("		etc_cd3=   ");
					sqlST.AppendLine("		@etc_cd3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				}
				if (etc_cd4 != 0)
				{
					sqlST.AppendLine("		etc_cd4=   ");
					sqlST.AppendLine("		@etc_cd4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				}
				if (etc_cd5 != 0)
				{
					sqlST.AppendLine("		etc_cd5=   ");
					sqlST.AppendLine("		@etc_cd5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				}
				if (etc_str1 != "")
				{
					sqlST.AppendLine("		etc_str1=   ");
					sqlST.AppendLine("		@etc_str1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str1", etc_str1);
				}
				if (etc_str2 != "")
				{
					sqlST.AppendLine("		etc_str2=   ");
					sqlST.AppendLine("		@etc_str2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str2", etc_str2);
				}
				if (etc_str3 != "")
				{
					sqlST.AppendLine("		etc_str3=   ");
					sqlST.AppendLine("		@etc_str3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str3", etc_str3);
				}
				if (etc_str4 != "")
				{
					sqlST.AppendLine("		etc_str4=   ");
					sqlST.AppendLine("		@etc_str4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str4", etc_str4);
				}
				if (etc_str5 != "")
				{
					sqlST.AppendLine("		etc_str5=   ");
					sqlST.AppendLine("		@etc_str5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str5", etc_str5);
				}
				if (etc_date1 != null)
				{
					sqlST.AppendLine("		etc_date1=   ");
					sqlST.AppendLine("		@etc_date1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_date1", etc_date1);
				}
				if (etc_date2 != null)
				{
					sqlST.AppendLine("		etc_date2=   ");
					sqlST.AppendLine("		@etc_date2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_date2", etc_date2);
				}
				if (etc_date3 != null)
				{
					sqlST.AppendLine("		etc_date3=   ");
					sqlST.AppendLine("		@etc_date3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_date3", etc_date3);
				}
				if (etc_date4 != null)
				{
					sqlST.AppendLine("		etc_date4=   ");
					sqlST.AppendLine("		@etc_date4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_date4", etc_date4);
				}
				if (etc_date5 != null)
				{
					sqlST.AppendLine("		etc_date5=   ");
					sqlST.AppendLine("		@etc_date5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_date5", etc_date5);
				}
				#endregion

				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=   ");
					sqlST.AppendLine("		@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		ball_id= ");
				sqlST.AppendLine("		@ball_id , ");
				CMD_Update.Parameters.AddWithValue("@ball_id", ball_id);
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendLine("		@box_id , ");
				CMD_Update.Parameters.AddWithValue("@box_id", box_id);
				//if (player_id != 0)
				//{
				//	sqlST.AppendLine("		AND ");
				//	sqlST.AppendLine("	player_id ");
				//	sqlST.AppendLine("		=@player_id ");
				//	CMD_Update.Parameters.AddWithValue("@player_id", player_id);
				//}
				if (team_id != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("	team_id=@team_id ");
					CMD_Update.Parameters.AddWithValue("@team_id", team_id);
				}
				if (game_id != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("		game_id=	 ");
					sqlST.AppendLine("		@game_id	 ");
					CMD_Update.Parameters.AddWithValue("@game_id", game_id);
				}
				if (game_box_num != 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendLine("		game_box_num=	 ");
					sqlST.AppendLine("		@game_box_num	 ");
					CMD_Update.Parameters.AddWithValue("@game_box_num", game_box_num);
				}


				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}




		public static void updateRecordBallData(
							int ball_id = -1,
							int player_id = -1,
							int ball_box_num = -1,
							int ball_total_num = -1,
							int game_box_num = -1,
							int ball_type = -1,
							int ball_speed = -1,
							int ball_x = -1,
							int ball_y = -1,
							int cource_table_id = -1,
							int pick_off = -1,
							int ball_action = -1
			)
		{
			if (player_id == 0)
			{
				Console.WriteLine("player_idが指定されていません");
				return;
			}

			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE ball ");
				sqlST.AppendLine("	SET ");
				if (ball_box_num >= 0)
				{
					sqlST.AppendFormat("		ball_box_num={0}       ", ball_box_num).AppendLine();
					sqlST.AppendLine("		,	");
				}
				if (ball_total_num >= 0)
				{
					sqlST.AppendFormat("		ball_total_num={0}       ", ball_total_num).AppendLine();
					sqlST.AppendLine("		,	");
				}

				if (ball_action >= 0)
				{
					sqlST.AppendFormat("		ball_action={0}       ", ball_action).AppendLine();
					sqlST.AppendLine("		,	");
					sqlST.AppendFormat("		etc_cd1={0}       ", ball_action).AppendLine();
					sqlST.AppendLine("		,	");
				}

				if (ball_type >= 0)
				{
					sqlST.AppendFormat("		ball_type={0}       ", ball_type).AppendLine();
					sqlST.AppendLine("		,	");
				}
				if (ball_speed >= 0)
				{
					sqlST.AppendFormat("		ball_speed={0}       ", ball_speed).AppendLine();
					sqlST.AppendLine("		,	");
				}

				sqlST.AppendFormat("		ball_id={0}       ", ball_id).AppendLine();


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		ball_id={0}       ", ball_id).AppendLine();


				CMD_Update.CommandText = sqlST.ToString();


				CMD_Update.ExecuteReader();
				con.Close();
			}
		}
		#endregion



		#region 削除
		public static void deleteRecordBoxId(int box_id = 0)
		{
			if (box_id == 0) { return; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CmdDelete = new SqliteCommand();
				CmdDelete.Connection = con;
				#region 削除 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("DELETE	");
				sqlST.AppendLine("FROM ball ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	box_id=@box_id ");
				CmdDelete.Parameters.AddWithValue("@box_id", box_id);
				CmdDelete.CommandText = sqlST.ToString();
				#endregion
				CmdDelete.ExecuteReader();
				con.Close();
			}
		}
		#endregion


		public class ballData
		{

			#region 投球変数
			public int ball_id { get; set; }                // 投球識別番号
			public int box_id { get; set; }                 // 打席識別番号		1
			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6
			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int ball_type { get; set; }              // 球種
			public int ball_level { get; set; }             // 変化量	追加項目
			public int ball_speed { get; set; }             // 球速
			public int ball_x { get; set; }                 // ボールの横軸
			public int ball_y { get; set; }                 // ボールの縦軸
			public int cource_table_id { get; set; }        // コース別の範囲（5x5マス or 7x7マス）
			public int pick_off { get; set; }               // けん制球（0:なし, 1:一塁, 2:二塁, 3:三塁）
			public bool in_play { get; set; }               // インプレー
			public bool steal { get; set; }                 // 盗塁有無
			public bool wildpitch { get; set; }             // 暴投有無
			public bool passball { get; set; }              // 捕逸

			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備
			public DateTime? etc_date1 { get; set; }        // 予備		
			public DateTime? etc_date2 { get; set; }        // 予備
			public DateTime? etc_date3 { get; set; }        // 予備
			public DateTime? etc_date4 { get; set; }        // 予備
			public DateTime? etc_date5 { get; set; }        // 予備

			public int ball_action { get; set; }        // 投球結果 0:ボール, 1:見逃し(ストライク), 2:空振り(ストライク), 3 ファウル,


			public int ball_img { get; set; }
			#endregion


			public ballData(int ball_id = 0, int box_id = 0, int player_id = 0, int team_id = 0,
							int position = 0, int player_num = 0,
							int pitcher_id = 0, int pit_team_id = 0,
							int ball_box_num = 0, int ball_total_num = 0,
							int cat_id = 0, int ump_id = 0,
							int game_id = 0, int game_box_num = 0, int park_id = 0, int bat_id = 0,
							int pit_hand_id = 0, int pit_throw_id = 0,
							int count_b = 0, int count_s = 0, int count_o = 0,
							bool runner_1 = false, bool runner_2 = false, bool runner_3 = false,
							int ining = 0, bool top_bot = false, int top_score = 0, int bottom_score = 0,
							int ball_type = 0, int ball_speed = 0, int ball_x = 0, int ball_y = 0,
							int cource_table_id = 0, int pick_off = 0,
							bool in_play = false, bool steal = false, bool wildpitch = false, bool passball = false,
							int etc_cd1 = 0, int etc_cd2 = 0, int etc_cd3 = 0, int etc_cd4 = 0, int etc_cd5 = 0,
							string etc_str1 = "", string etc_str2 = "", string etc_str3 = "", string etc_str4 = "", string etc_str5 = "",
							DateTime? etc_date1 = null, DateTime? etc_date2 = null, DateTime? etc_date3 = null, DateTime? etc_date4 = null, DateTime? etc_date5 = null,
							DateTime? update_date = null,
							int ball_level = 0, int ball_action = 0, int ball_img = 0
							)
			{

				this.ball_id = ball_id;             // 1
				this.box_id = box_id;               // 2
				this.player_id = player_id;         // 3
				this.position = position;           // 4
				this.bat_id = bat_id;               // 5
				this.player_num = player_num;       // 6
				this.team_id = team_id;             // 7
				this.pitcher_id = pitcher_id;       // 8
				this.pit_team_id = pit_team_id;     // 9

				this.ball_box_num = ball_box_num;   // 10
				this.ball_total_num = ball_total_num;   // 11
				this.cat_id = cat_id;               // 12
				this.ump_id = ump_id;               // 13
				this.game_id = game_id;             // 14
				this.game_box_num = game_box_num;   // 15
				this.park_id = park_id;             // 16

				this.pit_throw_id = pit_throw_id;   // 17
				this.pit_hand_id = pit_hand_id;     // 18

				this.count_b = count_b;             // 19
				this.count_s = count_s;             // 20
				this.count_o = count_o;             // 21

				this.runner_1 = runner_1;           // 22
				this.runner_2 = runner_2;           // 23
				this.runner_3 = runner_3;           // 24

				this.ining = ining;                 // 25
				this.top_bot = top_bot;             // 26
				this.top_score = top_score;         // 27
				this.bottom_score = bottom_score;   // 28

				this.ball_type = ball_type;         // 29
				this.ball_speed = ball_speed;       // 30
				this.ball_x = ball_x;               // 31
				this.ball_y = ball_y;               // 32
				this.cource_table_id = cource_table_id; // 33
				this.pick_off = pick_off;           // 34

				this.in_play = in_play;             // 35
				this.steal = steal;                 // 36
				this.wildpitch = wildpitch;         // 37
				this.passball = passball;           // 38

				this.update_date = update_date;     // 39

				this.etc_cd1 = etc_cd1;             // e1
				this.etc_cd2 = etc_cd2;             // e2
				this.etc_cd3 = etc_cd3;             // e3
				this.etc_cd4 = etc_cd4;             // e4
				this.etc_cd5 = etc_cd5;             // e5

				this.etc_str1 = etc_str1;           // e6
				this.etc_str2 = etc_str2;           // e7
				this.etc_str3 = etc_str3;           // e8
				this.etc_str4 = etc_str4;           // e9
				this.etc_str5 = etc_str5;           // e10

				this.etc_date1 = etc_date1;         // e11
				this.etc_date2 = etc_date2;         // e12
				this.etc_date3 = etc_date3;         // e13
				this.etc_date4 = etc_date4;         // e14
				this.etc_date5 = etc_date5;         // e15

				this.ball_level = ball_level;
				this.ball_action = ball_action;

				this.ball_img = ball_img;

			}

		}

		public class ballData_img
		{

			#region 打席変数
			public int ball_id { get; set; }                // 投球識別番号
			public int box_id { get; set; }                 // 打席識別番号		1
			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6
			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int ball_type { get; set; }              // 球種
			public int ball_level { get; set; }             // 変化量	追加項目
			public int ball_speed { get; set; }             // 球速
			public int ball_x { get; set; }                 // ボールの横軸
			public int ball_y { get; set; }                 // ボールの縦軸
			public int cource_table_id { get; set; }        // コース別の範囲（5x5マス or 7x7マス）
			public int pick_off { get; set; }               // けん制球（0:なし, 1:一塁, 2:二塁, 3:三塁）
			public bool in_play { get; set; }               // インプレー
			public bool steal { get; set; }                 // 盗塁有無
			public bool wildpitch { get; set; }             // 暴投有無
			public bool passball { get; set; }              // 捕逸

			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備
			public DateTime? etc_date1 { get; set; }        // 予備		
			public DateTime? etc_date2 { get; set; }        // 予備
			public DateTime? etc_date3 { get; set; }        // 予備
			public DateTime? etc_date4 { get; set; }        // 予備
			public DateTime? etc_date5 { get; set; }        // 予備

			public int ball_action { get; set; }        // 投球結果 0:ボール, 1:見逃し(ストライク), 2:空振り(ストライク), 3 ファウル,
			public int ball_img { get; set; }
			#endregion


			public ballData_img(int ball_id = 0, int box_id = 0, int player_id = 0, int team_id = 0,
							int position = 0, int player_num = 0,
							int pitcher_id = 0, int pit_team_id = 0,
							int ball_box_num = 0, int ball_total_num = 0,
							int cat_id = 0, int ump_id = 0,
							int game_id = 0, int game_box_num = 0, int park_id = 0, int bat_id = 0,
							int pit_hand_id = 0, int pit_throw_id = 0,
							int count_b = 0, int count_s = 0, int count_o = 0,
							bool runner_1 = false, bool runner_2 = false, bool runner_3 = false,
							int ining = 0, bool top_bot = false, int top_score = 0, int bottom_score = 0,
							int ball_type = 0, int ball_speed = 0, int ball_x = 0, int ball_y = 0,
							int cource_table_id = 0, int pick_off = 0,
							bool in_play = false, bool steal = false, bool wildpitch = false, bool passball = false,
							int etc_cd1 = 0, int etc_cd2 = 0, int etc_cd3 = 0, int etc_cd4 = 0, int etc_cd5 = 0,
							string etc_str1 = "", string etc_str2 = "", string etc_str3 = "", string etc_str4 = "", string etc_str5 = "",
							DateTime? etc_date1 = null, DateTime? etc_date2 = null, DateTime? etc_date3 = null, DateTime? etc_date4 = null, DateTime? etc_date5 = null,
							DateTime? update_date = null,
							int ball_level = 0, int ball_action = 0, int ball_img = 0
							)
			{

				this.ball_id = ball_id;             // 1
				this.box_id = box_id;               // 2
				this.player_id = player_id;         // 3
				this.position = position;           // 4
				this.bat_id = bat_id;               // 5
				this.player_num = player_num;       // 6
				this.team_id = team_id;             // 7
				this.pitcher_id = pitcher_id;       // 8
				this.pit_team_id = pit_team_id;     // 9

				this.ball_box_num = ball_box_num;   // 10
				this.ball_total_num = ball_total_num;   // 11
				this.cat_id = cat_id;               // 12
				this.ump_id = ump_id;               // 13
				this.game_id = game_id;             // 14
				this.game_box_num = game_box_num;   // 15
				this.park_id = park_id;             // 16

				this.pit_throw_id = pit_throw_id;   // 17
				this.pit_hand_id = pit_hand_id;     // 18

				this.count_b = count_b;             // 19
				this.count_s = count_s;             // 20
				this.count_o = count_o;             // 21

				this.runner_1 = runner_1;           // 22
				this.runner_2 = runner_2;           // 23
				this.runner_3 = runner_3;           // 24

				this.ining = ining;                 // 25
				this.top_bot = top_bot;             // 26
				this.top_score = top_score;         // 27
				this.bottom_score = bottom_score;   // 28

				this.ball_type = ball_type;         // 29
				this.ball_speed = ball_speed;       // 30
				this.ball_x = ball_x;               // 31
				this.ball_y = ball_y;               // 32
				this.cource_table_id = cource_table_id; // 33
				this.pick_off = pick_off;           // 34

				this.in_play = in_play;             // 35
				this.steal = steal;                 // 36
				this.wildpitch = wildpitch;         // 37
				this.passball = passball;           // 38

				this.update_date = update_date;     // 39

				this.etc_cd1 = etc_cd1;             // e1
				this.etc_cd2 = etc_cd2;             // e2
				this.etc_cd3 = etc_cd3;             // e3
				this.etc_cd4 = etc_cd4;             // e4
				this.etc_cd5 = etc_cd5;             // e5

				this.etc_str1 = etc_str1;           // e6
				this.etc_str2 = etc_str2;           // e7
				this.etc_str3 = etc_str3;           // e8
				this.etc_str4 = etc_str4;           // e9
				this.etc_str5 = etc_str5;           // e10

				this.etc_date1 = etc_date1;         // e11
				this.etc_date2 = etc_date2;         // e12
				this.etc_date3 = etc_date3;         // e13
				this.etc_date4 = etc_date4;         // e14
				this.etc_date5 = etc_date5;         // e15

				this.ball_level = ball_level;
				this.ball_action = ball_action;
				this.ball_img = ball_img;

			}

		}


		#region 投球　データ取得
		public static List<ballData> GetRecords()
		{
			List<ballData> orderList = new List<ballData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball_id	,");            // 1
				sqlST.AppendLine("	box_id	,");            // 2
				sqlST.AppendLine("	player_id	,");        // 3
				sqlST.AppendLine("	team_id	,");            // 4
				sqlST.AppendLine("	position	,");        // 5
				sqlST.AppendLine("	player_num	,");        // 6
				sqlST.AppendLine("	pitcher_id	,");        // 7
				sqlST.AppendLine("	pit_team_id	,");        // 8
				sqlST.AppendLine("	ball_box_num	,");    // 9
				sqlST.AppendLine("	ball_total_num	,");    // 10
				sqlST.AppendLine("	cat_id	,");            // 11
				sqlST.AppendLine("	ump_id	,");            // 12
				sqlST.AppendLine("	game_id	,");            // 13
				sqlST.AppendLine("	game_box_num	,");    // 14
				sqlST.AppendLine("	park_id	,");            // 15
				sqlST.AppendLine("	bat_id	,");            // 16
				sqlST.AppendLine("	pit_hand_id	,");        // 17
				sqlST.AppendLine("	pit_throw_id	,");    // 18
															//sqlST.AppendLine("	weather_id	,");		// 19
				sqlST.AppendLine("	count_b	,");            // 20
				sqlST.AppendLine("	count_s	,");            // 21
				sqlST.AppendLine("	count_o	,");            // 22
				sqlST.AppendLine("	runner_1	,");        // 23
				sqlST.AppendLine("	runner_2	,");        // 24
				sqlST.AppendLine("	runner_3	,");        // 25
				sqlST.AppendLine("	ining	,");            // 26
				sqlST.AppendLine("	top_bot	,");            // 27
				sqlST.AppendLine("	top_score	,");        // 28
				sqlST.AppendLine("	bottom_score	,");    // 29
				sqlST.AppendLine("	ball_type	,");        // 30
				sqlST.AppendLine("	ball_speed	,");        // 31
				sqlST.AppendLine("	ball_x	,");            // 32
				sqlST.AppendLine("	ball_y	,");            // 33
				sqlST.AppendLine("	cource_table_id	,");    // 34
				sqlST.AppendLine("	pick_off	,");        // 35
				sqlST.AppendLine("	in_play	,");            // 36
				sqlST.AppendLine("	steal	,");            // 37
				sqlST.AppendLine("	wildpitch	,");        // 38
				sqlST.AppendLine("	passball	,");        // 39

				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str 
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	etc_date1	,");        // DateTime
				sqlST.AppendLine("	etc_date2	,");        // DateTime
				sqlST.AppendLine("	etc_date3	,");        // DateTime  
				sqlST.AppendLine("	etc_date4	,");        // DateTime
				sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	update_date	");         // DateTime
				sqlST.AppendLine("	,ball_level	");         // int
				sqlST.AppendLine("	,ball_action	");         // int

				// sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballData(
								reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
								reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
								reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
								reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
								reader.GetInt32(20),
								reader.GetBoolean(21), reader.GetBoolean(22), reader.GetBoolean(23),
								reader.GetInt32(24),
								reader.GetBoolean(25),
								reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30),
								reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33),
								reader.GetBoolean(34), reader.GetBoolean(35), reader.GetBoolean(36), reader.GetBoolean(37),
								reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42),
								reader.GetString(43), reader.GetString(44), reader.GetString(45), reader.GetString(46), reader.GetString(47),
								reader.GetDateTime(48), reader.GetDateTime(49), reader.GetDateTime(50), reader.GetDateTime(51), reader.GetDateTime(52),
								reader.GetDateTime(53),
								ball_level: reader.GetInt32(54), ball_action: reader.GetInt32(55)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}
		#endregion



		public class ballMinMaxAvg
		{
			public int min { get; set; }
			public int max { get; set; }
			public int avg { get; set; }
			public ballMinMaxAvg(
							int min = 0,
							int max = 0,
							int avg = 0
							)
			{
				this.min = min;
				this.max = max;
				this.avg = avg;
			}
		}
		public static List<ballMinMaxAvg> GetRecordsSpeedMinMaxAvg(
												int player_id = -1,
												int pitcher_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int pit_hand_id = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<ballMinMaxAvg> orderList = new List<ballMinMaxAvg>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			AVG(ball_speed) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			AVG(ball_speed) ");
				sqlST.AppendLine("	END AS _avg ");
				sqlST.AppendLine("	,");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MIN(ball_speed) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MIN(ball_speed) ");
				sqlST.AppendLine("	END AS _min ");
				sqlST.AppendLine("	,");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(ball_speed) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(ball_speed) ");
				sqlST.AppendLine("	END AS _max ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ball.box_id=box_field_dir.box_id ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ball.box_id=box_result.box_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	ball_speed<>0 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>6 ");
				if (!(pitcher_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	pitcher_id={0} ", pitcher_id).AppendLine();
				}
				if (!(player_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.player_id={0} ", player_id).AppendLine();
				}


				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}

				if (!(bat < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	bat_id={0} ", bat).AppendLine();
				}
				if (!(pit_hand_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	pit_hand_id={0} ", pit_hand_id).AppendLine();
				}

				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(runner_1=1 OR runner_2=1 OR runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_type={0} ", ball_type).AppendLine();
				}
				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_action={0} ", ball_action).AppendLine();
				}


				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed<={0}", max_ball_speed).AppendLine();
				}

				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (!(course_id < 0))
				{
					//sqlST.AppendLine("	AND ");
					//sqlST.AppendFormat("	ball.cource_table_id={0} ", course_id).AppendLine();
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballMinMaxAvg(
								avg: reader.GetInt32(0),
								min: reader.GetInt32(1),
								max: reader.GetInt32(2)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}



		#region 試合中の投球振り返り
		public static List<ballData> GetRecordsBoxId(int box_id = 0)
		{
			List<ballData> orderList = new List<ballData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball.ball_id	,");            // 1
				sqlST.AppendLine("	ball.box_id	,");            // 2
				sqlST.AppendLine("	ball.player_id	,");        // 3
				sqlST.AppendLine("	ball.team_id	,");            // 4
				sqlST.AppendLine("	ball.position	,");        // 5
				sqlST.AppendLine("	ball.player_num	,");        // 6
				sqlST.AppendLine("	ball.pitcher_id	,");        // 7
				sqlST.AppendLine("	ball.pit_team_id	,");        // 8
				sqlST.AppendLine("	ball.ball_box_num	,");    // 9
				sqlST.AppendLine("	ball.ball_total_num	,");    // 10
				sqlST.AppendLine("	ball.cat_id	,");            // 11
				sqlST.AppendLine("	ball.ump_id	,");            // 12
				sqlST.AppendLine("	ball.game_id	,");            // 13
				sqlST.AppendLine("	ball.game_box_num	,");    // 14
				sqlST.AppendLine("	ball.park_id	,");            // 15
				sqlST.AppendLine("	ball.bat_id	,");            // 16
				sqlST.AppendLine("	ball.pit_hand_id	,");        // 17
				sqlST.AppendLine("	ball.pit_throw_id	,");    // 18
																//sqlST.AppendLine("	weather_id	,");		// 19
				sqlST.AppendLine("	ball.count_b	,");            // 20
				sqlST.AppendLine("	ball.count_s	,");            // 21
				sqlST.AppendLine("	ball.count_o	,");            // 22
				sqlST.AppendLine("	ball.runner_1	,");        // 23
				sqlST.AppendLine("	ball.runner_2	,");        // 24
				sqlST.AppendLine("	ball.runner_3	,");        // 25
				sqlST.AppendLine("	ball.ining	,");            // 26
				sqlST.AppendLine("	ball.top_bot	,");            // 27
				sqlST.AppendLine("	ball.top_score	,");        // 28
				sqlST.AppendLine("	ball.bottom_score	,");    // 29
				sqlST.AppendLine("	ball.ball_type	,");        // 30
				sqlST.AppendLine("	ball.ball_speed	,");        // 31
				sqlST.AppendLine("	ball.ball_x	,");            // 32
				sqlST.AppendLine("	ball.ball_y	,");            // 33
				sqlST.AppendLine("	ball.cource_table_id	,");    // 34
				sqlST.AppendLine("	ball.pick_off	,");        // 35
				sqlST.AppendLine("	ball.in_play	,");            // 36
				sqlST.AppendLine("	ball.steal	,");            // 37
				sqlST.AppendLine("	ball.wildpitch	,");        // 38
				sqlST.AppendLine("	ball.passball	,");        // 39

				sqlST.AppendLine("	ball.etc_cd1	,");
				sqlST.AppendLine("	ball.etc_cd2	,");
				sqlST.AppendLine("	ball.etc_cd3	,");
				sqlST.AppendLine("	ball.etc_cd4	,");
				sqlST.AppendLine("	ball.etc_cd5	,");
				sqlST.AppendLine("	ball.etc_str1	,");        // str
				sqlST.AppendLine("	ball.etc_str2	,");        // str
				sqlST.AppendLine("	ball.etc_str3	,");        // str 
				sqlST.AppendLine("	ball.etc_str4	,");        // str
				sqlST.AppendLine("	ball.etc_str5	,");        // str
				sqlST.AppendLine("	ball.etc_date1	,");        // DateTime
				sqlST.AppendLine("	ball.etc_date2	,");        // DateTime
				sqlST.AppendLine("	ball.etc_date3	,");        // DateTime  
				sqlST.AppendLine("	ball.etc_date4	,");        // DateTime
				sqlST.AppendLine("	ball.etc_date5	,");        // DateTime
				sqlST.AppendLine("	ball.update_date	");         // DateTime
				sqlST.AppendLine("	,ball.ball_level	");         // int
				sqlST.AppendLine("	,ball.ball_action	");         // int
				sqlST.AppendLine("	,balltype.ball_img	");         // int
																	// sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.pit_hand_id=balltype.hand ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	ball.ball_type=balltype.ball_type_id ");


				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.box_id={0} ", box_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballData(
								reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
								reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
								reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
								reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
								reader.GetInt32(20),
								reader.GetBoolean(21), reader.GetBoolean(22), reader.GetBoolean(23),
								reader.GetInt32(24),
								reader.GetBoolean(25),
								reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30),
								reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33),
								reader.GetBoolean(34), reader.GetBoolean(35), reader.GetBoolean(36), reader.GetBoolean(37),
								reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42),
								reader.GetString(43), reader.GetString(44), reader.GetString(45), reader.GetString(46), reader.GetString(47),
								//reader.GetDateTime(48), reader.GetDateTime(49), reader.GetDateTime(50), reader.GetDateTime(51), reader.GetDateTime(52),
								reader.GetDateTime(53),
								ball_level: reader.GetInt32(54), ball_action: reader.GetInt32(55), ball_img: reader.GetInt32(56)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}

		#endregion


		#region 試合中の投球振り返り
		public static List<ballData> GetRecordsInPlay(
											int box_id = 0)
		{
			List<ballData> orderList = new List<ballData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball_id	,");            // 1
				sqlST.AppendLine("	box_id	,");            // 2
				sqlST.AppendLine("	player_id	,");        // 3
				sqlST.AppendLine("	team_id	,");            // 4
				sqlST.AppendLine("	position	,");        // 5
				sqlST.AppendLine("	player_num	,");        // 6
				sqlST.AppendLine("	pitcher_id	,");        // 7
				sqlST.AppendLine("	pit_team_id	,");        // 8
				sqlST.AppendLine("	ball_box_num	,");    // 9
				sqlST.AppendLine("	ball_total_num	,");    // 10
				sqlST.AppendLine("	cat_id	,");            // 11
				sqlST.AppendLine("	ump_id	,");            // 12
				sqlST.AppendLine("	game_id	,");            // 13
				sqlST.AppendLine("	game_box_num	,");    // 14
				sqlST.AppendLine("	park_id	,");            // 15
				sqlST.AppendLine("	bat_id	,");            // 16
				sqlST.AppendLine("	pit_hand_id	,");        // 17
				sqlST.AppendLine("	pit_throw_id	,");    // 18
															//sqlST.AppendLine("	weather_id	,");		// 19
				sqlST.AppendLine("	count_b	,");            // 20
				sqlST.AppendLine("	count_s	,");            // 21
				sqlST.AppendLine("	count_o	,");            // 22
				sqlST.AppendLine("	runner_1	,");        // 23
				sqlST.AppendLine("	runner_2	,");        // 24
				sqlST.AppendLine("	runner_3	,");        // 25
				sqlST.AppendLine("	ining	,");            // 26
				sqlST.AppendLine("	top_bot	,");            // 27
				sqlST.AppendLine("	top_score	,");        // 28
				sqlST.AppendLine("	bottom_score	,");    // 29
				sqlST.AppendLine("	ball_type	,");        // 30
				sqlST.AppendLine("	ball_speed	,");        // 31
				sqlST.AppendLine("	ball_x	,");            // 32
				sqlST.AppendLine("	ball_y	,");            // 33
				sqlST.AppendLine("	cource_table_id	,");    // 34
				sqlST.AppendLine("	pick_off	,");        // 35
				sqlST.AppendLine("	in_play	,");            // 36
				sqlST.AppendLine("	steal	,");            // 37
				sqlST.AppendLine("	wildpitch	,");        // 38
				sqlST.AppendLine("	passball	,");        // 39

				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str 
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	etc_date1	,");        // DateTime
				sqlST.AppendLine("	etc_date2	,");        // DateTime
				sqlST.AppendLine("	etc_date3	,");        // DateTime  
				sqlST.AppendLine("	etc_date4	,");        // DateTime
				sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	update_date	");         // DateTime
				sqlST.AppendLine("	,ball_level	");         // int
				sqlST.AppendLine("	,ball_action	");         // int
																// sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.box_id={0} ", box_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	ball.in_play=true ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballData(
								reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
								reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
								reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
								reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
								reader.GetInt32(20),
								reader.GetBoolean(21), reader.GetBoolean(22), reader.GetBoolean(23),
								reader.GetInt32(24),
								reader.GetBoolean(25),
								reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30),
								reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33),
								reader.GetBoolean(34), reader.GetBoolean(35), reader.GetBoolean(36), reader.GetBoolean(37),
								reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42),
								reader.GetString(43), reader.GetString(44), reader.GetString(45), reader.GetString(46), reader.GetString(47),
								//reader.GetDateTime(48), reader.GetDateTime(49), reader.GetDateTime(50), reader.GetDateTime(51), reader.GetDateTime(52),
								reader.GetDateTime(53),
								ball_level: reader.GetInt32(54), ball_action: reader.GetInt32(55)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}

		#endregion


		#region ピッチャー別
		public static List<ballData> GetRecordsPitcher(
												int pitcher_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												//int field_direct_id = -1,
												int field_direct_id_left = -1,
												int field_direct_id_center = -1,
												int field_direct_id_right = -1,
												bool res_hit_flg = false,
												bool res_hit_out = false,
												bool res_hit_hit = false,
												bool res_hit_error = false,
												bool res_hit_other = false,
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<ballData> orderList = new List<ballData>();
			if (pitcher_id < 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball.ball_id	,");            // 1
				sqlST.AppendLine("	ball.box_id	,");            // 2
				sqlST.AppendLine("	ball.player_id	,");        // 3
				sqlST.AppendLine("	ball.team_id	,");            // 4
				sqlST.AppendLine("	ball.position	,");        // 5
				sqlST.AppendLine("	ball.player_num	,");        // 6
				sqlST.AppendLine("	ball.pitcher_id	,");        // 7
				sqlST.AppendLine("	ball.pit_team_id	,");        // 8
				sqlST.AppendLine("	ball.ball_box_num	,");    // 9
				sqlST.AppendLine("	ball.ball_total_num	,");    // 10
				sqlST.AppendLine("	ball.cat_id	,");            // 11
				sqlST.AppendLine("	ball.ump_id	,");            // 12
				sqlST.AppendLine("	ball.game_id	,");            // 13
				sqlST.AppendLine("	ball.game_box_num	,");    // 14
				sqlST.AppendLine("	ball.park_id	,");            // 15
				sqlST.AppendLine("	ball.bat_id	,");            // 16
				sqlST.AppendLine("	ball.pit_hand_id	,");        // 17
				sqlST.AppendLine("	ball.pit_throw_id	,");    // 18
																//sqlST.AppendLine("	weather_id	,");		// 19
				sqlST.AppendLine("	ball.count_b	,");            // 20
				sqlST.AppendLine("	ball.count_s	,");            // 21
				sqlST.AppendLine("	ball.count_o	,");            // 22
				sqlST.AppendLine("	ball.runner_1	,");        // 23
				sqlST.AppendLine("	ball.runner_2	,");        // 24
				sqlST.AppendLine("	ball.runner_3	,");        // 25
				sqlST.AppendLine("	ball.ining	,");            // 26
				sqlST.AppendLine("	ball.top_bot	,");            // 27
				sqlST.AppendLine("	ball.top_score	,");        // 28
				sqlST.AppendLine("	ball.bottom_score	,");    // 29
				sqlST.AppendLine("	ball.ball_type	,");        // 30
				sqlST.AppendLine("	ball.ball_speed	,");        // 31
				sqlST.AppendLine("	ball.ball_x	,");            // 32
				sqlST.AppendLine("	ball.ball_y	,");            // 33
				sqlST.AppendLine("	ball.cource_table_id	,");    // 34
				sqlST.AppendLine("	ball.pick_off	,");        // 35
				sqlST.AppendLine("	ball.in_play	,");            // 36
				sqlST.AppendLine("	ball.steal	,");            // 37
				sqlST.AppendLine("	ball.wildpitch	,");        // 38
				sqlST.AppendLine("	ball.passball	,");        // 39

				sqlST.AppendLine("	ball.etc_cd1	,");
				sqlST.AppendLine("	ball.etc_cd2	,");
				sqlST.AppendLine("	ball.etc_cd3	,");
				sqlST.AppendLine("	ball.etc_cd4	,");
				sqlST.AppendLine("	ball.etc_cd5	,");
				sqlST.AppendLine("	ball.etc_str1	,");        // str
				sqlST.AppendLine("	ball.etc_str2	,");        // str
				sqlST.AppendLine("	ball.etc_str3	,");        // str 
				sqlST.AppendLine("	ball.etc_str4	,");        // str
				sqlST.AppendLine("	ball.etc_str5	,");        // str
																//sqlST.AppendLine("	etc_date1	,");        // DateTime
																//sqlST.AppendLine("	etc_date2	,");        // DateTime
																//sqlST.AppendLine("	etc_date3	,");        // DateTime  
																//sqlST.AppendLine("	etc_date4	,");        // DateTime
																//sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	ball.update_date	");         // DateTime
				sqlST.AppendLine("	,ball.ball_level	");         // int
				sqlST.AppendLine("	,ball.ball_action	");         // int
				sqlST.AppendLine("	,balltype.ball_img	");         // int
																	// sqlST.AppendLine("	* ");
																	//sqlST.AppendLine("	,box_field_dir.field_dir_id	");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.pit_hand_id=balltype.hand ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	ball.ball_type=balltype.ball_type_id ");

				/// 2022.05.19 
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.box_id=box_field_dir.box_id ");


				/// 2022.05.21
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.box_id=box_result.box_id ");



				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.pitcher_id={0} ", pitcher_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>6 ");


				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}


				if (!(bat < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.bat_id={0} ", bat).AppendLine();
				}
				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(ball.runner_1=1 OR ball.runner_2=1 OR ball.runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ball_type={0} ", ball_type).AppendLine();
				}

				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ball_action={0} ", ball_action).AppendLine();
				}

				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed<={0}", max_ball_speed).AppendLine();
				}
				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}


				if (!(course_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}



				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballData(
								reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
								reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
								reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
								reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
								reader.GetInt32(20),
								reader.GetBoolean(21), reader.GetBoolean(22), reader.GetBoolean(23),
								reader.GetInt32(24),
								reader.GetBoolean(25),
								reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30),
								reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33),
								reader.GetBoolean(34), reader.GetBoolean(35), reader.GetBoolean(36), reader.GetBoolean(37),
								reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42),
								reader.GetString(43), reader.GetString(44), reader.GetString(45), reader.GetString(46), reader.GetString(47),
								reader.GetDateTime(48),
								ball_level: reader.GetInt32(49), ball_action: reader.GetInt32(50), ball_img: reader.GetInt32(51)

								)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<ballCount> GetRecordsPitcherCount(
												int pitcher_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<ballCount> orderList = new List<ballCount>();
			if (pitcher_id < 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(ball.ball_id) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(ball.ball_id) ");
				sqlST.AppendLine("	END AS _id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ball.box_id=box_field_dir.box_id ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ball.box_id=box_result.box_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.pitcher_id={0} ", pitcher_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>6 ");


				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}


				if (!(bat < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	bat_id={0} ", bat).AppendLine();
				}
				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(runner_1=1 OR runner_2=1 OR runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_type={0} ", ball_type).AppendLine();
				}

				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_action={0} ", ball_action).AppendLine();
				}

				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed<={0}", max_ball_speed).AppendLine();
				}

				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}


				if (!(course_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}
				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}




				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballCount(
								reader.GetInt32(0)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}



		#endregion

		#region バッター別
		public static List<ballData> GetRecordsBatter(
												int player_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat_id = -1,
												int hand_id = -1,
												int pit_hand_id = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<ballData> orderList = new List<ballData>();
			if (player_id < 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball.ball_id	,");            // 1
				sqlST.AppendLine("	ball.box_id	,");            // 2
				sqlST.AppendLine("	ball.player_id	,");        // 3
				sqlST.AppendLine("	ball.team_id	,");            // 4
				sqlST.AppendLine("	ball.position	,");        // 5
				sqlST.AppendLine("	ball.player_num	,");        // 6
				sqlST.AppendLine("	ball.pitcher_id	,");        // 7
				sqlST.AppendLine("	ball.pit_team_id	,");        // 8
				sqlST.AppendLine("	ball.ball_box_num	,");    // 9
				sqlST.AppendLine("	ball.ball_total_num	,");    // 10
				sqlST.AppendLine("	ball.cat_id	,");            // 11
				sqlST.AppendLine("	ball.ump_id	,");            // 12
				sqlST.AppendLine("	ball.game_id	,");            // 13
				sqlST.AppendLine("	ball.game_box_num	,");    // 14
				sqlST.AppendLine("	ball.park_id	,");            // 15
				sqlST.AppendLine("	ball.bat_id	,");            // 16
				sqlST.AppendLine("	ball.pit_hand_id	,");        // 17
				sqlST.AppendLine("	ball.pit_throw_id	,");    // 18
																//sqlST.AppendLine("	weather_id	,");		// 19
				sqlST.AppendLine("	ball.count_b	,");            // 20
				sqlST.AppendLine("	ball.count_s	,");            // 21
				sqlST.AppendLine("	ball.count_o	,");            // 22
				sqlST.AppendLine("	ball.runner_1	,");        // 23
				sqlST.AppendLine("	ball.runner_2	,");        // 24
				sqlST.AppendLine("	ball.runner_3	,");        // 25
				sqlST.AppendLine("	ball.ining	,");            // 26
				sqlST.AppendLine("	ball.top_bot	,");            // 27
				sqlST.AppendLine("	ball.top_score	,");        // 28
				sqlST.AppendLine("	ball.bottom_score	,");    // 29
				sqlST.AppendLine("	ball.ball_type	,");        // 30
				sqlST.AppendLine("	ball.ball_speed	,");        // 31
				sqlST.AppendLine("	ball.ball_x	,");            // 32
				sqlST.AppendLine("	ball.ball_y	,");            // 33
				sqlST.AppendLine("	ball.cource_table_id	,");    // 34
				sqlST.AppendLine("	ball.pick_off	,");        // 35
				sqlST.AppendLine("	ball.in_play	,");            // 36
				sqlST.AppendLine("	ball.steal	,");            // 37
				sqlST.AppendLine("	ball.wildpitch	,");        // 38
				sqlST.AppendLine("	ball.passball	,");        // 39

				sqlST.AppendLine("	ball.etc_cd1	,");
				sqlST.AppendLine("	ball.etc_cd2	,");
				sqlST.AppendLine("	ball.etc_cd3	,");
				sqlST.AppendLine("	ball.etc_cd4	,");
				sqlST.AppendLine("	ball.etc_cd5	,");
				sqlST.AppendLine("	ball.etc_str1	,");        // str
				sqlST.AppendLine("	ball.etc_str2	,");        // str
				sqlST.AppendLine("	ball.etc_str3	,");        // str 
				sqlST.AppendLine("	ball.etc_str4	,");        // str
				sqlST.AppendLine("	ball.etc_str5	,");        // str
																//sqlST.AppendLine("	etc_date1	,");        // DateTime
																//sqlST.AppendLine("	etc_date2	,");        // DateTime
																//sqlST.AppendLine("	etc_date3	,");        // DateTime  
																//sqlST.AppendLine("	etc_date4	,");        // DateTime
																//sqlST.AppendLine("	etc_date5	,");        // DateTime
				sqlST.AppendLine("	ball.update_date	");         // DateTime
				sqlST.AppendLine("	,ball.ball_level	");         // int
				sqlST.AppendLine("	,ball.ball_action	");         // int
				sqlST.AppendLine("	,balltype.ball_img	");         // int
																	// sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.pit_hand_id=balltype.hand ");
				sqlST.AppendLine("	AND ");
				sqlST.AppendLine("	ball.ball_type=balltype.ball_type_id ");

				/// 2022.05.19 
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.box_id=box_field_dir.box_id ");


				/// 2022.05.21
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.box_id=box_result.box_id ");

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.player_id={0} ", player_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>6 ");

				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}

				if (!(bat_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.bat_id={0} ", bat_id).AppendLine();
				}

				if (!(hand_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.hand_id={0} ", hand_id).AppendLine();
				}

				if (!(pit_hand_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.pit_hand_id={0} ", pit_hand_id).AppendLine();
				}

				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(ball.runner_1=1 OR ball.runner_2=1 OR ball.runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ball_type={0} ", ball_type).AppendLine();
				}

				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball.ball_action={0} ", ball_action).AppendLine();
				}


				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed<={0}", max_ball_speed).AppendLine();
				}

				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}


				if (!(course_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}


				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballData(
								reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4),
								reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9),
								reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12), reader.GetInt32(13), reader.GetInt32(14),
								reader.GetInt32(15), reader.GetInt32(16), reader.GetInt32(17), reader.GetInt32(18), reader.GetInt32(19),
								reader.GetInt32(20),
								reader.GetBoolean(21), reader.GetBoolean(22), reader.GetBoolean(23),
								reader.GetInt32(24),
								reader.GetBoolean(25),
								reader.GetInt32(26), reader.GetInt32(27), reader.GetInt32(28), reader.GetInt32(29), reader.GetInt32(30),
								reader.GetInt32(31), reader.GetInt32(32), reader.GetInt32(33),
								reader.GetBoolean(34), reader.GetBoolean(35), reader.GetBoolean(36), reader.GetBoolean(37),
								reader.GetInt32(38), reader.GetInt32(39), reader.GetInt32(40), reader.GetInt32(41), reader.GetInt32(42),
								reader.GetString(43), reader.GetString(44), reader.GetString(45), reader.GetString(46), reader.GetString(47),
								reader.GetDateTime(48),
								ball_level: reader.GetInt32(49), ball_action: reader.GetInt32(50), ball_img: reader.GetInt32(51)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public static List<ballCount> GetRecordsBatterCount(
												int player_id = -1,
												string start_datetime = "",
												string end_datetime = "",
												int bat_id = -1,
												int hand_id = -1,
												int pit_hand_id = -1,
												int ining = -1,
												int run_exsist = -1,
												int run_1 = -1,
												int run_2 = -1,
												int run_3 = -1,
												int count_b = -1,
												int count_s = -1,
												int count_o = -1,
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
												//int res_hit_id = -1,
												int res_hit_type = -1
			)
		{
			List<ballCount> orderList = new List<ballCount>();
			if (player_id < 0) { return orderList; }
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(ball.ball_id) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(ball.ball_id) ");
				sqlST.AppendLine("	END AS _id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("	ON ball.box_id=box_field_dir.box_id ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("	ON ball.box_id=box_result.box_id ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.player_id={0} ", player_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>5 ");
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball_action<>6 ");

				//if (start_datetime.Length > 0 && end_datetime.Length > 0)
				//{
				//	sqlST.AppendLine("	AND ");
				//	sqlST.AppendLine("	update_date ");
				//	sqlST.AppendLine("	BETWEEN ");
				//	sqlST.AppendFormat("	'{0} 00:00:00' AND '{1} 23:59:59'", start_datetime, end_datetime).AppendLine();
				//}

				if (start_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date>'{0} 00:00:00' ", start_datetime).AppendLine();
				}
				if (end_datetime.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	update_date<'{0} 23:59:59' ", end_datetime).AppendLine();
				}

				if (!(bat_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	bat_id={0} ", bat_id).AppendLine();
				}
				if (!(pit_hand_id < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	pit_hand_id={0} ", pit_hand_id).AppendLine();
				}


				if (!(ining < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ining={0} ", ining).AppendLine();
				}
				if (!(count_b < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_b={0} ", count_b).AppendLine();
				}

				if (!(count_s < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_s={0} ", count_s).AppendLine();
				}

				if (!(count_o < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	count_o={0} ", count_o).AppendLine();
				}

				if (!(run_exsist < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("	(runner_1=1 OR runner_2=1 OR runner_3=1)  ");
				}

				if (!(run_1 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_1={0} ", run_1).AppendLine();
				}
				if (!(run_2 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_2={0} ", run_2).AppendLine();
				}
				if (!(run_3 < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	runner_3={0} ", run_3).AppendLine();
				}

				if (!(ball_type < 0))
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_type={0} ", ball_type).AppendLine();
				}

				if (!(ball_action < 0) && ball_action < 5)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_action={0} ", ball_action).AppendLine();
				}

				if (min_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed>={0}", min_ball_speed).AppendLine();
				}
				if (max_ball_speed > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	ball_speed<={0}", max_ball_speed).AppendLine();
				}

				if (field_direct_id_left >= 0
					|| field_direct_id_center >= 0
					|| field_direct_id_right >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (field_direct_id_left >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=0 ");
					}
					if (field_direct_id_center >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=1 ");
					}
					if (field_direct_id_right >= 0)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_field_dir.field_dir_id=2 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (!(course_id < 0))
				{
					//sqlST.AppendLine("	AND ");
					//sqlST.AppendFormat("	ball.cource_table_id={0} ", course_id).AppendLine();
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (course_A)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=0 ");
					}
					if (course_B)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=1 ");
					}
					if (course_C)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=2 ");
					}
					if (course_D)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=3 ");
					}
					if (course_E)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=4 ");
					}
					if (course_F)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=5 ");
					}

					if (course_G)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=6 ");
					}
					if (course_H)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=7 ");
					}
					if (course_I)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	ball.cource_table_id=8 ");
					}
					sqlST.AppendLine("		) ");
				}

				if (res_hit_flg)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendLine("		( ");
					sqlST.AppendLine("		1=0 ");
					if (res_hit_out)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=0 ");
					}
					if (res_hit_hit)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=1 ");
					}
					if (res_hit_error)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id=2 ");
					}
					if (res_hit_other)
					{
						sqlST.AppendLine("	OR ");
						sqlST.AppendLine("	box_result.hit_id>=3 ");
					}

					sqlST.AppendLine("		) ");
				}
				if (res_hit_type >= 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	box_result.hit_type_id={0} ", res_hit_type).AppendLine();
				}

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new ballCount(
								reader.GetInt32(0)
								)
						);
				}
				con.Close();
			}
			return orderList;
		}



		#endregion

		public class ballScoreList
		{
			public int ball_box_num { get; set; }
			public int ball_total_num { get; set; }
			//public int ball_type { get; set; }
			public string ball_type { get; set; }  // バインド表示
			public int ball_speed { get; set; }
			//public int cource_table_id { get; set; }
			public string course { get; set; }
			public int ball_level { get; set; }
			//public int ball_action { get; set; }
			public string ball_actionST { get; set; }
			public string ball_action_color { get; set; }
			public int ball_x { get; set; }
			public int ball_y { get; set; }
			public string update_date { get; set; }

			public int ball_id { get; set; }
			public int ball_action { get; set; }

			public ballScoreList(
							int ball_box_num = 0,
							int ball_total_num = 0,
							string ball_type = "",
							int ball_speed = 0,
							//int cource_table_id = 0,
							string course = "",
							int ball_level = 0,
							string ball_actionST = "",
							string ball_action_color = "",
							int ball_x = 0,
							int ball_y = 0,
							string date = "",
							int ball_id = 0,
							int ball_action = 0
							)
			{
				this.ball_box_num = ball_box_num;   // 10
				this.ball_total_num = ball_total_num;   // 11
				this.ball_type = ball_type;         // 29
				this.ball_speed = ball_speed;       // 30
													//this.cource_table_id = cource_table_id; // 33
				this.course = course;
				this.ball_level = ball_level;
				this.ball_actionST = ball_actionST;
				this.ball_action_color = ball_action_color;
				this.ball_x = ball_x;
				this.ball_y = ball_y;
				this.update_date = date;
				this.ball_id = ball_id;
				this.ball_action = ball_action;
			}
		}

		public class ballCount
		{
			public int ball_id { get; set; }
			public ballCount(
							int ball_id = 0
							)
			{
				this.ball_id = ball_id;
			}
		}
		public static List<ballCount> GetCountRecords(int game_id = 0, int box_id = 0, int pitcher_id = 0, int player_id = 0)
		{
			List<ballCount> ballCountList = new List<ballCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(ball_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(ball_id) + 1 ");
				sqlST.AppendLine("	END AS ball_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.game_id={0} ", game_id).AppendLine();
				if (pitcher_id > 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	ball.pitcher_id={0} ", pitcher_id).AppendLine();
				}
				if (player_id > 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	ball.player_id={0} ", player_id).AppendLine();
				}
				if (box_id > 0)
				{
					sqlST.AppendLine("		AND ");
					sqlST.AppendFormat("	ball.box_id={0} ", box_id).AppendLine();
				}
				sqlST.AppendLine("		AND ");
				sqlST.AppendLine("		ball.ball_action<>5 ");  // けん制を除く
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					ballCountList.Add(
						new ballCount(
									reader.GetInt32(0)
									)
						);
				}
				con.Close();
			}
			return ballCountList;
		}




		#region 投球　スコア表示
		/// <summary>
		/// ScorePageで表示用の投球DataGrid
		/// </summary>
		/// <returns></returns>
		public static List<ballScoreList> GetScoreDisplayRecords(int box_id = 0, int hand = 0, int bat = 0)
		{
			List<ballScoreList> _ballScoreList = new List<ballScoreList>();
			if (box_id == 0)
			{
				return _ballScoreList;
			}
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();

				#region 1テーブル
				//sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	ball_box_num	AS No	,");
				//sqlST.AppendLine("	ball_total_num	AS 球数	,");
				////sqlST.AppendLine("	ball_type		AS 球種	,");
				///// CASE(START)
				//sqlST.AppendLine("	CASE	");
				//sqlST.AppendLine("		WHEN ball_type =0 THEN 'ストレート'	");
				//sqlST.AppendLine("		WHEN ball_type =1 THEN 'スライダー'	");
				//sqlST.AppendLine("		WHEN ball_type =2 THEN 'カーブ'	");
				//sqlST.AppendLine("		WHEN ball_type =3 THEN 'フォーク'	");
				//sqlST.AppendLine("		WHEN ball_type =4 THEN 'シンカー'	");
				//sqlST.AppendLine("		WHEN ball_type =5 THEN 'シュート'	");
				//sqlST.AppendLine("		ELSE 'not'	");
				//sqlST.AppendLine("	END	AS 球種	,	");
				///// CASE(END)
				//sqlST.AppendLine("	ball_speed		AS 球速	,");
				//sqlST.AppendLine("	cource_table_id	AS コース別,");
				//sqlST.AppendLine("	ball_level		AS 変化量,	");
				////sqlST.AppendLine("	ball_action		AS 結果	");
				///// CASE(START)
				//sqlST.AppendLine("	CASE	");
				//sqlST.AppendLine("		WHEN ball_action =0 THEN 'ボール'	");
				//sqlST.AppendLine("		WHEN ball_action =1 THEN '見逃し'	");
				//sqlST.AppendLine("		WHEN ball_action =2 THEN '空振り'	");
				//sqlST.AppendLine("		WHEN ball_action =3 THEN 'ファウル'	");
				//sqlST.AppendLine("		WHEN ball_action =4 THEN 'インプレー'	");
				//sqlST.AppendLine("		WHEN ball_action =5 THEN 'けん制'	");
				//sqlST.AppendLine("		ELSE 'not'	");
				//sqlST.AppendLine("	END	AS 結果,		");
				///// CASE(END)
				///// CASE(START)
				//sqlST.AppendLine("	CASE	");
				//sqlST.AppendLine("		WHEN etc_cd1 =0 THEN 'Green'	");
				//sqlST.AppendLine("		WHEN etc_cd1 =1 THEN 'Orange'	");
				//sqlST.AppendLine("		WHEN etc_cd1 =2 THEN 'Orange'	");
				//sqlST.AppendLine("		WHEN etc_cd1 =3 THEN 'Orange'	");
				//sqlST.AppendLine("		WHEN etc_cd1 =4 THEN 'White'	");
				//sqlST.AppendLine("		WHEN etc_cd1 =5 THEN 'Blue'	");
				//sqlST.AppendLine("		ELSE 'Black'	");
				//sqlST.AppendLine("	END	AS 結果		");
				//sqlST.AppendLine("	,	");
				//sqlST.AppendLine("	ball_x		,	");
				//sqlST.AppendLine("	ball_y			");
				///// CASE(END)
				//sqlST.AppendLine("FROM ");
				//sqlST.AppendLine("	ball ");
				//sqlST.AppendLine("WHERE ");
				//sqlST.AppendFormat("	box_id={0} ",box_id).AppendLine();
				#endregion 1テーブル

				#region テーブル結合
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball.ball_box_num	AS No	,");
				sqlST.AppendLine("	ball.ball_total_num	AS 球数	,");
				sqlST.AppendLine("	balltype.ball_type	AS 球種	,");

				sqlST.AppendLine("	ball.ball_speed		AS 球速	,");
				//sqlST.AppendLine("	ball.cource_table_id	AS コース別,");
				sqlST.AppendLine("	course.course	AS コース別,");
				sqlST.AppendLine("	ball.ball_level		AS 変化量,	");
				sqlST.AppendLine("	ballaction.ball_action		AS 結果,	");
				sqlST.AppendLine("	color.color	AS 色		");
				sqlST.AppendLine("	,	");
				sqlST.AppendLine("	ball.ball_x		,	");
				sqlST.AppendLine("	ball.ball_y			");
				sqlST.AppendLine("	,			");
				sqlST.AppendLine("	strftime(			");
				sqlST.AppendLine("	'%H:%M:%S'			");
				sqlST.AppendLine("					,			");
				sqlST.AppendLine("	ball.update_date			");
				sqlST.AppendLine("			) AS 時間			");
				sqlST.AppendLine("					,			");
				sqlST.AppendLine("	ball.ball_id			");
				sqlST.AppendLine("					,			");
				sqlST.AppendLine("	ball.ball_action			");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				//sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	balltype ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.ball_type=balltype.ball_type_id ");
				sqlST.AppendLine("INNER JOIN ");
				//sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	ballaction ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.ball_action=ballaction.ball_action_id ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("	color ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.etc_cd1=color.color_id ");
				sqlST.AppendLine("INNER JOIN ");
				//sqlST.AppendLine("LEFT OUTER JOIN ");
				sqlST.AppendLine("	course ");
				sqlST.AppendLine("	ON ");
				sqlST.AppendLine("	ball.cource_table_id=course.course_id ");

				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.box_id={0} ", box_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("	balltype.hand={0} ", hand).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("	course.bat={0} ", bat).AppendLine();
				#endregion テーブル結合


				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					_ballScoreList.Add(new ballScoreList(
						reader.GetInt32(0),
						reader.GetInt32(1),
						//reader.GetInt32(2),
						reader.GetString(2),
						reader.GetInt32(3),
						//reader.GetInt32(4),
						reader.GetString(4),
						reader.GetInt32(5),
						//reader.GetInt32(6)
						reader.GetString(6),
						reader.GetString(7),
						reader.GetInt32(8),
						reader.GetInt32(9),
						reader.GetString(10),
						reader.GetInt32(11),
						reader.GetInt32(12)
						)
						);
				}
				con.Close();
			}
			return _ballScoreList;
		}
		#endregion


		#region カウント
		public class ballScoreCountList
		{
			public int ball_count_B { get; set; }
			public int ball_count_S { get; set; }
			public int ball_count_O { get; set; }
			public ballScoreCountList(
							int ball_count_B = 0,
							int ball_count_S = 0,
							int ball_count_O = 0)
			{
				this.ball_count_B = ball_count_B;
				this.ball_count_S = ball_count_S;
				this.ball_count_O = ball_count_O;
			}
		}
		public static List<ballScoreCountList> GetScoreCountRecords(int box_id = 0)
		{
			List<ballScoreCountList> _ballScoreCountList = new List<ballScoreCountList>();
			if (box_id == 0)
			{
				return _ballScoreCountList;
			}
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				#region テーブル結合
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ball.count_b		,	");
				sqlST.AppendLine("	ball.count_s		,	");
				sqlST.AppendLine("	ball.count_o			");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ball ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	ball.box_id={0} ", box_id).AppendLine();
				#endregion テーブル結合


				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					_ballScoreCountList.Add(new ballScoreCountList(
						reader.GetInt32(0),
						reader.GetInt32(1),
						reader.GetInt32(2)
						)
						);
				}
				con.Close();
			}
			return _ballScoreCountList;
		}
		#endregion
	}
	#endregion
	#endregion

	#region 走塁データ
	class RunData
	{

		public RunData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	run ( ");
				/// 基本情報
				sqlST.AppendLine("		run_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				sqlST.AppendLine("		weather_id    , ");
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		runner_1_player_id     , ");
				sqlST.AppendLine("		runner_2_player_id     , ");
				sqlST.AppendLine("		runner_3_player_id     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				sqlST.AppendLine("		ball_type   , ");
				sqlST.AppendLine("		speed   , ");
				sqlST.AppendLine("		get_score      ,");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");

				sqlST.AppendLine("		inplay_id   , ");
				sqlST.AppendLine("		success_id   , ");
				sqlST.AppendLine("		from_base   , ");
				sqlST.AppendLine("		to_base   , ");
				sqlST.AppendLine("		run_player_id   , ");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				/// 60
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(
					int run_id = 0,
					int box_id = 0,
					int player_id = 0,
					int team_id = 0,
					int pitcher_id = 0,
					int pit_team_id = 0,
					int cat_id = 0,
					int ump_id = 0,
					int game_id = 0,
					int park_id = 0,
					int pit_hand_id = 0,
					int pit_throw_id = 0,
					int weather_id = 0,
					int count_b = 0,
					int count_s = 0,
					int count_o = 0,
					bool runner_1 = false,
					bool runner_2 = false,
					bool runner_3 = false,
					int runner_1_player_id = 0,
					int runner_2_player_id = 0,
					int runner_3_player_id = 0,
					int ining = 0,
					bool top_bot = false,
					int top_score = 0,
					int bottom_score = 0,
					int ball_type = 0,
					int ball_speed = 0,
					int etc_cd1 = 0,
					int etc_cd2 = 0,
					int etc_cd3 = 0,
					int etc_cd4 = 0,
					int etc_cd5 = 0,
					string etc_str1 = "",
					string etc_str2 = "",
					string etc_str3 = "",
					string etc_str4 = "",
					string etc_str5 = "",
					int inplay_id = 0,
					int success_id = -1,
					int from_base = 0,
					int to_base = 0,
					int run_player_id = 0,
					DateTime? update_date = null
					)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	run ( ");
				/// 基本情報
				sqlST.AppendLine("		run_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				sqlST.AppendLine("		weather_id    , ");
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		runner_1_player_id     , ");
				sqlST.AppendLine("		runner_2_player_id     , ");
				sqlST.AppendLine("		runner_3_player_id     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				sqlST.AppendLine("		ball_type   , ");
				sqlST.AppendLine("		speed   , ");
				sqlST.AppendLine("		get_score      ,");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");

				sqlST.AppendLine("		inplay_id   , ");
				sqlST.AppendLine("		success_id   , ");
				sqlST.AppendLine("		from_base   , ");
				sqlST.AppendLine("		to_base   , ");
				sqlST.AppendLine("		run_player_id   , ");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");


				sqlST.AppendLine("VALUES( ");

				/// 基本情報
				#region 基本
				sqlST.AppendLine("		@run_id , ");
				CMD_Insert.Parameters.AddWithValue("@run_id", run_id);
				sqlST.AppendLine("		@box_id , ");
				CMD_Insert.Parameters.AddWithValue("@box_id", box_id);
				sqlST.AppendLine("		@player_id , ");
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);

				sqlST.AppendLine("		@team_id  , ");
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);

				sqlST.AppendLine("		@pitcher_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pitcher_id", pitcher_id);

				sqlST.AppendLine("		@pit_team_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pit_team_id", pit_team_id);

				sqlST.AppendLine("		@cat_id        , ");
				CMD_Insert.Parameters.AddWithValue("@cat_id", cat_id);

				sqlST.AppendLine("		@ump_id         , ");
				CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);

				sqlST.AppendLine("		@game_id          , ");
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);

				sqlST.AppendLine("		@park_id          , ");
				CMD_Insert.Parameters.AddWithValue("@park_id", park_id);

				sqlST.AppendLine("		@pit_hand_id  , ");
				CMD_Insert.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);

				sqlST.AppendLine("		@pit_throw_id   , ");
				CMD_Insert.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);

				sqlST.AppendLine("		@weather_id  , ");
				CMD_Insert.Parameters.AddWithValue("@weather_id", weather_id);

				#endregion
				/// カウント
				#region カウント

				sqlST.AppendLine("		@count_b   , ");
				CMD_Insert.Parameters.AddWithValue("@count_b", count_b);

				sqlST.AppendLine("		@count_s  , ");
				CMD_Insert.Parameters.AddWithValue("@count_s", count_s);

				sqlST.AppendLine("		@count_o , ");
				CMD_Insert.Parameters.AddWithValue("@count_o", count_o);

				sqlST.AppendLine("		@runner_1  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1", runner_1);
				sqlST.AppendLine("		@runner_2  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2", runner_2);
				sqlST.AppendLine("		@runner_3  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3", runner_3);

				sqlST.AppendLine("		@runner_1_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1_player_id", runner_1_player_id);
				sqlST.AppendLine("		@runner_2_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2_player_id", runner_2_player_id);
				sqlST.AppendLine("		@runner_3_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3_player_id", runner_3_player_id);

				sqlST.AppendLine("		@ining  , ");
				CMD_Insert.Parameters.AddWithValue("@ining", ining);

				sqlST.AppendLine("		@top_bot    , ");
				CMD_Insert.Parameters.AddWithValue("@top_bot", top_bot);

				sqlST.AppendLine("		@top_score    , ");
				CMD_Insert.Parameters.AddWithValue("@top_score", top_score);

				sqlST.AppendLine("		@bottom_score    , ");
				CMD_Insert.Parameters.AddWithValue("@bottom_score", bottom_score);

				sqlST.AppendLine("		@ball_type    , ");
				CMD_Insert.Parameters.AddWithValue("@ball_type", ball_type);

				sqlST.AppendLine("		@ball_speed     , ");
				CMD_Insert.Parameters.AddWithValue("@ball_speed", ball_speed);
				#endregion

				/// 予備情報
				sqlST.AppendLine("		@etc_cd1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);

				sqlST.AppendLine("		@etc_cd2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);

				sqlST.AppendLine("		@etc_cd3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);

				sqlST.AppendLine("		@etc_cd4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);

				sqlST.AppendLine("		@etc_cd5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);

				sqlST.AppendLine("		@etc_str1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);

				sqlST.AppendLine("		@etc_str2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);

				sqlST.AppendLine("		@etc_str3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);

				sqlST.AppendLine("		@etc_str4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);

				sqlST.AppendLine("		@etc_str5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);

				sqlST.AppendFormat("	{0}	,	", inplay_id).AppendLine();
				sqlST.AppendFormat("	{0}	,	", success_id).AppendLine();
				sqlST.AppendFormat("	{0}	,	", from_base).AppendLine();
				sqlST.AppendFormat("	{0}	,	", to_base).AppendLine();
				sqlST.AppendFormat("	{0}	,	", run_player_id).AppendLine();
				///　更新日
				sqlST.AppendLine("		@update_date    ");
				CMD_Insert.Parameters.AddWithValue("@update_date", update_date);

				sqlST.AppendLine(" ); ");



				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(
					int run_id = 0,
					int box_id = 0,
					int player_id = 0,
					int team_id = 0,
					int pitcher_id = 0,
					int pit_team_id = 0,
					int cat_id = 0,
					int ump_id = 0,
					int game_id = 0,
					int park_id = 0,
					int pit_hand_id = 0,
					int pit_throw_id = 0,
					int weather_id = 0,
					int count_b = 0,
					int count_s = 0,
					int count_o = 0,
					bool runner_1 = false,
					bool runner_2 = false,
					bool runner_3 = false,
					int runner_1_player_id = 0,
					int runner_2_player_id = 0,
					int runner_3_player_id = 0,
					int ining = 0,
					bool top_bot = false,
					int top_score = 0,
					int bottom_score = 0,
					int ball_type = 0,
					int ball_speed = 0,
					int etc_cd1 = 0,
					int etc_cd2 = 0,
					int etc_cd3 = 0,
					int etc_cd4 = 0,
					int etc_cd5 = 0,
					string etc_str1 = "",
					string etc_str2 = "",
					string etc_str3 = "",
					string etc_str4 = "",
					string etc_str5 = "",
					int inplay_id = 0,
					int success_id = -1,
					int from_base = 0,
					int to_base = 0,
					int run_player_id = 0,
					DateTime? update_date = null
					)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE run ");
				sqlST.AppendLine("	SET ");
				/// 基本情報
				#region 基本
				sqlST.AppendLine("		run_id= ");
				sqlST.AppendFormat("		{0}	, ", run_id).AppendLine();
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	, ", box_id).AppendLine();
				if (player_id != 0)
				{
					sqlST.AppendLine("		player_id= ");
					sqlST.AppendFormat("		{0}	, ", player_id).AppendLine();
				}

				if (team_id != 0)
				{
					sqlST.AppendLine("		team_id= ");
					sqlST.AppendFormat("		{0}	, ", team_id).AppendLine();
				}

				if (pitcher_id != 0)
				{
					sqlST.AppendLine("		pitcher_id=   ");
					sqlST.AppendFormat("		{0}	, ", pitcher_id).AppendLine();
				}
				if (pit_team_id != 0)
				{
					sqlST.AppendLine("		pit_team_id=      ");
					sqlST.AppendFormat("		{0}	, ", pit_team_id).AppendLine();
				}
				if (cat_id != 0)
				{
					sqlST.AppendLine("		cat_id=         ");
					sqlST.AppendFormat("		{0}	, ", cat_id).AppendLine();
				}
				if (ump_id != 0)
				{
					sqlST.AppendLine("		ump_id=          ");
					sqlST.AppendFormat("		{0}	, ", ump_id).AppendLine();
				}
				if (game_id != 0)
				{
					sqlST.AppendLine("		game_id=          ");
					sqlST.AppendFormat("		{0}	, ", game_id).AppendLine();
				}
				if (park_id != 0)
				{
					sqlST.AppendLine("		park_id=           ");
					sqlST.AppendFormat("		{0},	 ", park_id).AppendLine();
				}
				if (pit_hand_id != 0)
				{
					sqlST.AppendLine("		pit_hand_id= ");
					sqlST.AppendFormat("		{0}	 ,", pit_hand_id).AppendLine();
				}
				if (pit_throw_id != 0)
				{
					sqlST.AppendLine("		pit_throw_id=    ");
					sqlST.AppendFormat("		{0},	 ", pit_throw_id).AppendLine();
				}
				if (weather_id != 0)
				{
					sqlST.AppendLine("		weather_id=   ");
					sqlST.AppendFormat("		{0}	, ", weather_id).AppendLine();
				}
				#endregion
				/// カウント
				#region カウント
				if (count_b != 0)
				{
					sqlST.AppendLine("		count_b=    ");
					sqlST.AppendFormat("		{0}	, ", count_b).AppendLine();
				}
				if (count_s != 0)
				{
					sqlST.AppendLine("		count_s=    ");
					sqlST.AppendFormat("		{0}	, ", count_s).AppendLine();
				}
				if (count_o != 0)
				{
					sqlST.AppendLine("		count_o=    ");
					sqlST.AppendFormat("		{0}	, ", count_o).AppendLine();
				}
				sqlST.AppendLine("		runner_1=    ");
				sqlST.AppendFormat("		{0}	, ", runner_1).AppendLine();

				sqlST.AppendLine("		runner_2=    ");
				sqlST.AppendFormat("		{0}	, ", runner_2).AppendLine();

				sqlST.AppendLine("		runner_3=    ");
				sqlST.AppendFormat("		{0}	, ", runner_3).AppendLine();

				sqlST.AppendLine("		runner_1_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_1_player_id).AppendLine();

				sqlST.AppendLine("		runner_2_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_2_player_id).AppendLine();

				sqlST.AppendLine("		runner_3_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_3_player_id).AppendLine();

				if (ining != 0)
				{
					sqlST.AppendLine("		ining=    ");
					sqlST.AppendFormat("		{0}	, ", ining).AppendLine();
				}
				sqlST.AppendLine("		top_bot=     ");
				sqlST.AppendFormat("		{0}	, ", top_bot).AppendLine();
				if (top_score != 0)
				{
					sqlST.AppendLine("		top_score=     ");
					sqlST.AppendFormat("		{0}	, ", top_score).AppendLine();
				}
				if (bottom_score != 0)
				{
					sqlST.AppendLine("		bottom_score=     ");
					sqlST.AppendFormat("		{0}	, ", bottom_score).AppendLine();
				}
				if (ball_type != 0)
				{
					sqlST.AppendLine("		ball_type=     ");
					sqlST.AppendFormat("		{0}	, ", ball_type).AppendLine();
				}
				if (ball_speed >= 0)
				{
					sqlST.AppendLine("		ball_speed=      ");
					sqlST.AppendFormat("		{0}	, ", ball_speed).AppendLine();
				}

				#endregion

				/// 予備
				#region 予備
				if (etc_cd1 != 0)
				{
					sqlST.AppendLine("		etc_cd1=   ");
					sqlST.AppendLine("				@etc_cd1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				}
				if (etc_cd2 != 0)
				{
					sqlST.AppendLine("		etc_cd2=   ");
					sqlST.AppendLine("				@etc_cd2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				}
				if (etc_cd3 != 0)
				{
					sqlST.AppendLine("		etc_cd3=   ");
					sqlST.AppendLine("			@etc_cd3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				}
				if (etc_cd4 != 0)
				{
					sqlST.AppendLine("		etc_cd4=   ");
					sqlST.AppendLine("			@etc_cd4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				}
				if (etc_cd5 != 0)
				{
					sqlST.AppendLine("		etc_cd5=   ");
					sqlST.AppendLine("			@etc_cd5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				}
				if (etc_str1 != "")
				{
					sqlST.AppendLine("		etc_str1=   ");
					sqlST.AppendLine("			@etc_str1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str1", etc_str1);
				}
				if (etc_str2 != "")
				{
					sqlST.AppendLine("		etc_str2=   ");
					sqlST.AppendLine("				@etc_str2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str2", etc_str2);
				}
				if (etc_str3 != "")
				{
					sqlST.AppendLine("		etc_str3=   ");
					sqlST.AppendLine("			@etc_str3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str3", etc_str3);
				}
				if (etc_str4 != "")
				{
					sqlST.AppendLine("		etc_str4=   ");
					sqlST.AppendLine("			@etc_str4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str4", etc_str4);
				}
				if (etc_str5 != "")
				{
					sqlST.AppendLine("		etc_str5=   ");
					sqlST.AppendLine("			@etc_str5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str5", etc_str5);
				}
				#endregion

				sqlST.AppendFormat("	inplay_id={0}	,	", inplay_id).AppendLine();
				sqlST.AppendFormat("	success_id={0}	,	", success_id).AppendLine();
				sqlST.AppendFormat("	from_base={0}	,	", from_base).AppendLine();
				sqlST.AppendFormat("	to_base={0}	,	", to_base).AppendLine();
				sqlST.AppendFormat("	run_player_id={0}	,	", run_player_id).AppendLine();

				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=   ");
					//sqlST.AppendFormat("		{0}	 ", update_date).AppendLine();
					sqlST.AppendLine("			@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		run_id= ");
				sqlST.AppendFormat("		{0}	 ", run_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class runDataCount
		{
			public int run_count { get; set; }
			public runDataCount(int count = 0)
			{
				this.run_count = count;

			}
		}
		public static List<runDataCount> GetRecordsCount()
		{
			List<runDataCount> countList = new List<runDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(run_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(run_id) + 1 ");
				sqlST.AppendLine("	END AS run_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	run ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new runDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public class runDataRunner
		{
			public bool runner_1 { get; set; }
			public bool runner_2 { get; set; }
			public bool runner_3 { get; set; }
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }
			public runDataRunner(
								bool runner_1 = false,
								bool runner_2 = false,
								bool runner_3 = false,
								int runner_1_player_id = 0,
								int runner_2_player_id = 0,
								int runner_3_player_id = 0)
			{
				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;
			}
		}
		public static List<runDataRunner> GetRecordsRunner(int run_id = 0)
		{
			List<runDataRunner> countList = new List<runDataRunner>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	runner_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_1_player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_2_player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_3_player_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	run ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	run_id={0} ", run_id).AppendLine();

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new runDataRunner(
										reader.GetBoolean(0),
										reader.GetBoolean(1),
										reader.GetBoolean(2),
										reader.GetInt32(3),
										reader.GetInt32(4),
										reader.GetInt32(5)
										)
						);
				}
			}
			return countList;
		}


		public class runData
		{

			#region 走塁変数
			public int run_id { get; set; }
			public int box_id { get; set; }                 // 打席識別番号		1

			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6

			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号
			public int weather_id { get; set; }             // 天気識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int ball_type { get; set; }         // 打席結果の球種
			public int ball_speed { get; set; }        // 打席結果の球速
			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		45
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		50
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備



			#endregion


			public runData(
						int run_id = 0,
						int box_id = 0,
						   int player_id = 0,
						   int team_id = 0,
						   int pitcher_id = 0,
						   int pit_team_id = 0,
						   int cat_id = 0,
						   int ump_id = 0,
						   int game_id = 0,
						   int park_id = 0,
						   int pit_hand_id = 0,
						   int pit_throw_id = 0,
						   int weather_id = 0,
						   int count_b = 0,
						   int count_s = 0,
						   int count_o = 0,
						   bool runner_1 = false,
						   bool runner_2 = false,
						   bool runner_3 = false,
						   int runner_1_player_id = 0,
						   int runner_2_player_id = 0,
						   int runner_3_player_id = 0,
						   int ining = 0,
						   bool top_bot = false,
						   int top_score = 0,
						   int bottom_score = 0,
						   int ball_type = 0,
						   int ball_speed = 0,
						   int etc_cd1 = 0,
						   int etc_cd2 = 0,
						   int etc_cd3 = 0,
						   int etc_cd4 = 0,
						   int etc_cd5 = 0,
						   string etc_str1 = "",
						   string etc_str2 = "",
						   string etc_str3 = "",
						   string etc_str4 = "",
						   string etc_str5 = "",
						   DateTime? update_date = null
							  )
			{
				this.run_id = run_id;
				this.box_id = box_id;
				this.player_id = player_id;
				this.team_id = team_id;
				this.pitcher_id = pitcher_id;
				this.pit_team_id = pit_team_id;

				this.cat_id = cat_id;
				this.ump_id = ump_id;
				this.game_id = game_id;
				this.park_id = park_id;

				this.pit_throw_id = pit_throw_id;
				this.pit_hand_id = pit_hand_id;
				this.weather_id = weather_id;
				this.ball_type = ball_type;
				this.ball_speed = ball_speed;
				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;

				this.ining = ining;
				this.top_bot = top_bot;
				this.top_score = top_score;
				this.bottom_score = bottom_score;

				this.update_date = update_date;

				this.etc_cd1 = etc_cd1;             // イニング内打順
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;

				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;

			}

		}
		public static List<runData> GetRecords(int run_id = 0)
		{
			List<runData> orderList = new List<runData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	run_id	,");
				sqlST.AppendLine("	box_id	,");
				sqlST.AppendLine("	player_id	,");
				sqlST.AppendLine("	team_id	,");
				sqlST.AppendLine("	pitcher_id	,");
				sqlST.AppendLine("	pit_team_id	,");
				sqlST.AppendLine("	cat_id	,");
				sqlST.AppendLine("	ump_id	,");
				sqlST.AppendLine("	game_id	,");
				sqlST.AppendLine("	park_id	,");
				sqlST.AppendLine("	pit_hand_id	,");
				sqlST.AppendLine("	pit_throw_id	,");
				sqlST.AppendLine("	weather_id	,");
				sqlST.AppendLine("	count_b	,");
				sqlST.AppendLine("	count_s	,");
				sqlST.AppendLine("	count_o	,");
				sqlST.AppendLine("	runner_1	,");        // bool
				sqlST.AppendLine("	runner_2	,");        // bool
				sqlST.AppendLine("	runner_3	,");        // bool  
				sqlST.AppendLine("	runner_1_player_id	,");
				sqlST.AppendLine("	runner_2_player_id	,");
				sqlST.AppendLine("	runner_3_player_id	,");
				sqlST.AppendLine("	ining	,");            // bool
				sqlST.AppendLine("	top_bot	,");
				sqlST.AppendLine("	top_score	,");
				sqlST.AppendLine("	bottom_score	,");
				sqlST.AppendLine("	ball_type	,");
				sqlST.AppendLine("	ball_speed	,");

				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	update_date	");         // DateTime
															//sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	run ");
				if (run_id != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendFormat("	run_id={0} ", run_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new runData(
							reader.GetInt32(0),
							reader.GetInt32(1),
							reader.GetInt32(2),
							reader.GetInt32(3),
							reader.GetInt32(4),
							reader.GetInt32(5),
							reader.GetInt32(6),
							reader.GetInt32(7),
							reader.GetInt32(8),
							reader.GetInt32(9),
							reader.GetInt32(10),
							reader.GetInt32(11),
							reader.GetInt32(12),
							reader.GetInt32(13),
							reader.GetInt32(14),
							reader.GetInt32(15),
							reader.GetBoolean(16),
							reader.GetBoolean(17),
							reader.GetBoolean(18),
							reader.GetInt32(19),
							reader.GetInt32(20),
							reader.GetInt32(21),
							reader.GetInt32(22),
							reader.GetBoolean(23),
							reader.GetInt32(24),
							reader.GetInt32(25),
							reader.GetInt32(26),
							reader.GetInt32(27),
							reader.GetInt32(28),
							reader.GetInt32(29),
							reader.GetInt32(30),
							reader.GetInt32(31),
							reader.GetInt32(32),
							reader.GetString(33),
							reader.GetString(34),
							reader.GetString(35),
							reader.GetString(36),
							reader.GetString(37),
							reader.GetDateTime(38)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

	}
	#endregion

	#region 守備データ
	class DefensiveData
	{

		public DefensiveData()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	defensive ( ");
				/// 基本情報
				sqlST.AppendLine("		def_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				sqlST.AppendLine("		weather_id    , ");
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		runner_1_player_id     , ");
				sqlST.AppendLine("		runner_2_player_id     , ");
				sqlST.AppendLine("		runner_3_player_id     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				sqlST.AppendLine("		ball_type   , ");
				sqlST.AppendLine("		speed   , ");
				sqlST.AppendLine("		get_score      ,");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				sqlST.AppendLine("		position_id   , ");
				sqlST.AppendLine("		position_player_id	,");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				/// 60
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public static void addRecord(
					int def_id = 0,
					int box_id = 0,
					int player_id = 0,
					int team_id = 0,
					int pitcher_id = 0,
					int pit_team_id = 0,
					int cat_id = 0,
					int ump_id = 0,
					int game_id = 0,
					int park_id = 0,
					int pit_hand_id = 0,
					int pit_throw_id = 0,
					int weather_id = 0,
					int count_b = 0,
					int count_s = 0,
					int count_o = 0,
					bool runner_1 = false,
					bool runner_2 = false,
					bool runner_3 = false,
					int runner_1_player_id = 0,
					int runner_2_player_id = 0,
					int runner_3_player_id = 0,
					int ining = 0,
					bool top_bot = false,
					int top_score = 0,
					int bottom_score = 0,
					int ball_type = 0,
					int ball_speed = 0,
					int etc_cd1 = 0,
					int etc_cd2 = 0,
					int etc_cd3 = 0,
					int etc_cd4 = 0,
					int etc_cd5 = 0,
					string etc_str1 = "",
					string etc_str2 = "",
					string etc_str3 = "",
					string etc_str4 = "",
					string etc_str5 = "",
					int position_id = 0,
					int position_player_id = 0,
					DateTime? update_date = null
					)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	defensive ( ");
				/// 基本情報
				sqlST.AppendLine("		def_id , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		cat_id    , ");
				sqlST.AppendLine("		ump_id    , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		pit_hand_id    , ");
				sqlST.AppendLine("		pit_throw_id    , ");
				sqlST.AppendLine("		weather_id    , ");
				sqlST.AppendLine("		count_b    , ");
				sqlST.AppendLine("		count_s     , ");
				sqlST.AppendLine("		count_o     , ");
				sqlST.AppendLine("		runner_1     , ");
				sqlST.AppendLine("		runner_2     , ");
				sqlST.AppendLine("		runner_3     , ");
				sqlST.AppendLine("		runner_1_player_id     , ");
				sqlST.AppendLine("		runner_2_player_id     , ");
				sqlST.AppendLine("		runner_3_player_id     , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_bot   , ");
				sqlST.AppendLine("		top_score   , ");
				sqlST.AppendLine("		bottom_score  , ");
				sqlST.AppendLine("		ball_type   , ");
				sqlST.AppendLine("		speed   , ");
				sqlST.AppendLine("		get_score      ,");
				/// 予備情報
				sqlST.AppendLine("		etc_cd1   , ");
				sqlST.AppendLine("		etc_cd2   , ");
				sqlST.AppendLine("		etc_cd3   , ");
				sqlST.AppendLine("		etc_cd4   , ");
				sqlST.AppendLine("		etc_cd5   , ");
				sqlST.AppendLine("		etc_str1   , ");
				sqlST.AppendLine("		etc_str2   , ");
				sqlST.AppendLine("		etc_str3   , ");
				sqlST.AppendLine("		etc_str4   , ");
				sqlST.AppendLine("		etc_str5   , ");
				sqlST.AppendLine("		position_id   , ");
				sqlST.AppendLine("		position_player_id	,");
				///　更新日
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");


				sqlST.AppendLine("VALUES( ");

				/// 基本情報
				#region 基本
				sqlST.AppendLine("		@def_id , ");
				CMD_Insert.Parameters.AddWithValue("@def_id", def_id);
				sqlST.AppendLine("		@box_id , ");
				CMD_Insert.Parameters.AddWithValue("@box_id", box_id);
				sqlST.AppendLine("		@player_id , ");
				CMD_Insert.Parameters.AddWithValue("@player_id", player_id);

				sqlST.AppendLine("		@team_id  , ");
				CMD_Insert.Parameters.AddWithValue("@team_id", team_id);

				sqlST.AppendLine("		@pitcher_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pitcher_id", pitcher_id);

				sqlST.AppendLine("		@pit_team_id     , ");
				CMD_Insert.Parameters.AddWithValue("@pit_team_id", pit_team_id);

				sqlST.AppendLine("		@cat_id        , ");
				CMD_Insert.Parameters.AddWithValue("@cat_id", cat_id);

				sqlST.AppendLine("		@ump_id         , ");
				CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);

				sqlST.AppendLine("		@game_id          , ");
				CMD_Insert.Parameters.AddWithValue("@game_id", game_id);

				sqlST.AppendLine("		@park_id          , ");
				CMD_Insert.Parameters.AddWithValue("@park_id", park_id);

				sqlST.AppendLine("		@pit_hand_id  , ");
				CMD_Insert.Parameters.AddWithValue("@pit_hand_id", pit_hand_id);

				sqlST.AppendLine("		@pit_throw_id   , ");
				CMD_Insert.Parameters.AddWithValue("@pit_throw_id", pit_throw_id);

				sqlST.AppendLine("		@weather_id  , ");
				CMD_Insert.Parameters.AddWithValue("@weather_id", weather_id);

				#endregion
				/// カウント
				#region カウント

				sqlST.AppendLine("		@count_b   , ");
				CMD_Insert.Parameters.AddWithValue("@count_b", count_b);

				sqlST.AppendLine("		@count_s  , ");
				CMD_Insert.Parameters.AddWithValue("@count_s", count_s);

				sqlST.AppendLine("		@count_o , ");
				CMD_Insert.Parameters.AddWithValue("@count_o", count_o);

				sqlST.AppendLine("		@runner_1  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1", runner_1);
				sqlST.AppendLine("		@runner_2  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2", runner_2);
				sqlST.AppendLine("		@runner_3  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3", runner_3);

				sqlST.AppendLine("		@runner_1_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_1_player_id", runner_1_player_id);
				sqlST.AppendLine("		@runner_2_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_2_player_id", runner_2_player_id);
				sqlST.AppendLine("		@runner_3_player_id  , ");
				CMD_Insert.Parameters.AddWithValue("@runner_3_player_id", runner_3_player_id);

				sqlST.AppendLine("		@ining  , ");
				CMD_Insert.Parameters.AddWithValue("@ining", ining);

				sqlST.AppendLine("		@top_bot    , ");
				CMD_Insert.Parameters.AddWithValue("@top_bot", top_bot);

				sqlST.AppendLine("		@top_score    , ");
				CMD_Insert.Parameters.AddWithValue("@top_score", top_score);

				sqlST.AppendLine("		@bottom_score    , ");
				CMD_Insert.Parameters.AddWithValue("@bottom_score", bottom_score);

				sqlST.AppendLine("		@ball_type    , ");
				CMD_Insert.Parameters.AddWithValue("@ball_type", ball_type);

				sqlST.AppendLine("		@ball_speed     , ");
				CMD_Insert.Parameters.AddWithValue("@ball_speed", ball_speed);
				#endregion

				/// 予備情報
				sqlST.AppendLine("		@etc_cd1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd1", etc_cd1);

				sqlST.AppendLine("		@etc_cd2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd2", etc_cd2);

				sqlST.AppendLine("		@etc_cd3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd3", etc_cd3);

				sqlST.AppendLine("		@etc_cd4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd4", etc_cd4);

				sqlST.AppendLine("		@etc_cd5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_cd5", etc_cd5);

				sqlST.AppendLine("		@etc_str1   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str1", etc_str1);

				sqlST.AppendLine("		@etc_str2   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str2", etc_str2);

				sqlST.AppendLine("		@etc_str3   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str3", etc_str3);

				sqlST.AppendLine("		@etc_str4   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str4", etc_str4);

				sqlST.AppendLine("		@etc_str5   , ");
				CMD_Insert.Parameters.AddWithValue("@etc_str5", etc_str5);
				sqlST.AppendLine("		@position_id   , ");
				CMD_Insert.Parameters.AddWithValue("@position_id", position_id);
				sqlST.AppendLine("		@position_player_id   , ");
				CMD_Insert.Parameters.AddWithValue("@position_player_id", position_player_id);

				///　更新日
				sqlST.AppendLine("		@update_date    ");
				CMD_Insert.Parameters.AddWithValue("@update_date", update_date);

				sqlST.AppendLine(" ); ");



				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public static void updateRecord(
					int def_id = 0,
					int box_id = 0,
					int player_id = 0,
					int team_id = 0,
					int pitcher_id = 0,
					int pit_team_id = 0,
					int cat_id = 0,
					int ump_id = 0,
					int game_id = 0,
					int park_id = 0,
					int pit_hand_id = 0,
					int pit_throw_id = 0,
					int weather_id = 0,
					int count_b = 0,
					int count_s = 0,
					int count_o = 0,
					bool runner_1 = false,
					bool runner_2 = false,
					bool runner_3 = false,
					int runner_1_player_id = 0,
					int runner_2_player_id = 0,
					int runner_3_player_id = 0,
					int ining = 0,
					bool top_bot = false,
					int top_score = 0,
					int bottom_score = 0,
					int ball_type = 0,
					int ball_speed = 0,
					int etc_cd1 = 0,
					int etc_cd2 = 0,
					int etc_cd3 = 0,
					int etc_cd4 = 0,
					int etc_cd5 = 0,
					string etc_str1 = "",
					string etc_str2 = "",
					string etc_str3 = "",
					string etc_str4 = "",
					string etc_str5 = "",
					int position_id = 0,
					int position_player_id = 0,
					DateTime? update_date = null
					)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE defensive ");
				sqlST.AppendLine("	SET ");
				/// 基本情報
				#region 基本
				sqlST.AppendLine("		def_id= ");
				sqlST.AppendFormat("		{0}	, ", def_id).AppendLine();
				sqlST.AppendLine("		box_id= ");
				sqlST.AppendFormat("		{0}	, ", box_id).AppendLine();
				if (player_id != 0)
				{
					sqlST.AppendLine("		player_id= ");
					sqlST.AppendFormat("		{0}	, ", player_id).AppendLine();
				}

				if (team_id != 0)
				{
					sqlST.AppendLine("		team_id= ");
					sqlST.AppendFormat("		{0}	, ", team_id).AppendLine();
				}

				if (pitcher_id != 0)
				{
					sqlST.AppendLine("		pitcher_id=   ");
					sqlST.AppendFormat("		{0}	, ", pitcher_id).AppendLine();
				}
				if (pit_team_id != 0)
				{
					sqlST.AppendLine("		pit_team_id=      ");
					sqlST.AppendFormat("		{0}	, ", pit_team_id).AppendLine();
				}
				if (cat_id != 0)
				{
					sqlST.AppendLine("		cat_id=         ");
					sqlST.AppendFormat("		{0}	, ", cat_id).AppendLine();
				}
				if (ump_id != 0)
				{
					sqlST.AppendLine("		ump_id=          ");
					sqlST.AppendFormat("		{0}	, ", ump_id).AppendLine();
				}
				if (game_id != 0)
				{
					sqlST.AppendLine("		game_id=          ");
					sqlST.AppendFormat("		{0}	, ", game_id).AppendLine();
				}
				if (park_id != 0)
				{
					sqlST.AppendLine("		park_id=           ");
					sqlST.AppendFormat("		{0},	 ", park_id).AppendLine();
				}
				if (pit_hand_id != 0)
				{
					sqlST.AppendLine("		pit_hand_id= ");
					sqlST.AppendFormat("		{0}	 ,", pit_hand_id).AppendLine();
				}
				if (pit_throw_id != 0)
				{
					sqlST.AppendLine("		pit_throw_id=    ");
					sqlST.AppendFormat("		{0},	 ", pit_throw_id).AppendLine();
				}
				if (weather_id != 0)
				{
					sqlST.AppendLine("		weather_id=   ");
					sqlST.AppendFormat("		{0}	, ", weather_id).AppendLine();
				}
				#endregion
				/// カウント
				#region カウント
				if (count_b != 0)
				{
					sqlST.AppendLine("		count_b=    ");
					sqlST.AppendFormat("		{0}	, ", count_b).AppendLine();
				}
				if (count_s != 0)
				{
					sqlST.AppendLine("		count_s=    ");
					sqlST.AppendFormat("		{0}	, ", count_s).AppendLine();
				}
				if (count_o != 0)
				{
					sqlST.AppendLine("		count_o=    ");
					sqlST.AppendFormat("		{0}	, ", count_o).AppendLine();
				}
				sqlST.AppendLine("		runner_1=    ");
				sqlST.AppendFormat("		{0}	, ", runner_1).AppendLine();

				sqlST.AppendLine("		runner_2=    ");
				sqlST.AppendFormat("		{0}	, ", runner_2).AppendLine();

				sqlST.AppendLine("		runner_3=    ");
				sqlST.AppendFormat("		{0}	, ", runner_3).AppendLine();

				sqlST.AppendLine("		runner_1_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_1_player_id).AppendLine();

				sqlST.AppendLine("		runner_2_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_2_player_id).AppendLine();

				sqlST.AppendLine("		runner_3_player_id=    ");
				sqlST.AppendFormat("		{0}	, ", runner_3_player_id).AppendLine();

				if (ining != 0)
				{
					sqlST.AppendLine("		ining=    ");
					sqlST.AppendFormat("		{0}	, ", ining).AppendLine();
				}
				sqlST.AppendLine("		top_bot=     ");
				sqlST.AppendFormat("		{0}	, ", top_bot).AppendLine();
				if (top_score != 0)
				{
					sqlST.AppendLine("		top_score=     ");
					sqlST.AppendFormat("		{0}	, ", top_score).AppendLine();
				}
				if (bottom_score != 0)
				{
					sqlST.AppendLine("		bottom_score=     ");
					sqlST.AppendFormat("		{0}	, ", bottom_score).AppendLine();
				}
				if (ball_type != 0)
				{
					sqlST.AppendLine("		ball_type=     ");
					sqlST.AppendFormat("		{0}	, ", ball_type).AppendLine();
				}
				if (ball_speed >= 0)
				{
					sqlST.AppendLine("		ball_speed=      ");
					sqlST.AppendFormat("		{0}	, ", ball_speed).AppendLine();
				}

				#endregion

				/// 予備
				#region 予備
				if (etc_cd1 != 0)
				{
					sqlST.AppendLine("		etc_cd1=   ");
					sqlST.AppendLine("				@etc_cd1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd1", etc_cd1);
				}
				if (etc_cd2 != 0)
				{
					sqlST.AppendLine("		etc_cd2=   ");
					sqlST.AppendLine("				@etc_cd2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd2", etc_cd2);
				}
				if (etc_cd3 != 0)
				{
					sqlST.AppendLine("		etc_cd3=   ");
					sqlST.AppendLine("			@etc_cd3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd3", etc_cd3);
				}
				if (etc_cd4 != 0)
				{
					sqlST.AppendLine("		etc_cd4=   ");
					sqlST.AppendLine("			@etc_cd4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd4", etc_cd4);
				}
				if (etc_cd5 != 0)
				{
					sqlST.AppendLine("		etc_cd5=   ");
					sqlST.AppendLine("			@etc_cd5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_cd5", etc_cd5);
				}
				if (etc_str1 != "")
				{
					sqlST.AppendLine("		etc_str1=   ");
					sqlST.AppendLine("			@etc_str1   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str1", etc_str1);
				}
				if (etc_str2 != "")
				{
					sqlST.AppendLine("		etc_str2=   ");
					sqlST.AppendLine("				@etc_str2   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str2", etc_str2);
				}
				if (etc_str3 != "")
				{
					sqlST.AppendLine("		etc_str3=   ");
					sqlST.AppendLine("			@etc_str3   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str3", etc_str3);
				}
				if (etc_str4 != "")
				{
					sqlST.AppendLine("		etc_str4=   ");
					sqlST.AppendLine("			@etc_str4   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str4", etc_str4);
				}
				if (etc_str5 != "")
				{
					sqlST.AppendLine("		etc_str5=   ");
					sqlST.AppendLine("			@etc_str5   , ");
					CMD_Update.Parameters.AddWithValue("@etc_str5", etc_str5);
				}
				#endregion

				if (position_id != 0)
				{
					sqlST.AppendFormat("		position_id={0}   ", position_id).AppendLine();
				}
				if (position_player_id != 0)
				{
					sqlST.AppendFormat("		position_player_id={0}   ", position_player_id).AppendLine();
				}

				///　更新日
				if (update_date != null)
				{
					sqlST.AppendLine("		update_date=   ");
					//sqlST.AppendFormat("		{0}	 ", update_date).AppendLine();
					sqlST.AppendLine("			@update_date    ");
					CMD_Update.Parameters.AddWithValue("@update_date", update_date);
				}

				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("		def_id= ");
				sqlST.AppendFormat("		{0}	 ", def_id).AppendLine();

				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class defDataCount
		{
			public int def_count { get; set; }
			public defDataCount(int count = 0)
			{
				this.def_count = count;

			}
		}
		public static List<defDataCount> GetRecordsCount()
		{
			List<defDataCount> countList = new List<defDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(def_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(def_id) + 1 ");
				sqlST.AppendLine("	END AS def_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	defensive ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new defDataCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}



		public class defData
		{

			#region 守備変数
			public int def_id { get; set; }
			public int box_id { get; set; }                 // 打席識別番号		1

			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6

			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号
			public int weather_id { get; set; }             // 天気識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int ball_type { get; set; }         // 打席結果の球種
			public int ball_speed { get; set; }        // 打席結果の球速
			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		45
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		50
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備
			public int position_id { get; set; }
			public int position_player_id { get; set; }


			#endregion


			public defData(
						int def_id = 0,
						int box_id = 0,
						   int player_id = 0,
						   int team_id = 0,
						   int pitcher_id = 0,
						   int pit_team_id = 0,
						   int cat_id = 0,
						   int ump_id = 0,
						   int game_id = 0,
						   int park_id = 0,
						   int pit_hand_id = 0,
						   int pit_throw_id = 0,
						   int weather_id = 0,
						   int count_b = 0,
						   int count_s = 0,
						   int count_o = 0,
						   bool runner_1 = false,
						   bool runner_2 = false,
						   bool runner_3 = false,
						   int runner_1_player_id = 0,
						   int runner_2_player_id = 0,
						   int runner_3_player_id = 0,
						   int ining = 0,
						   bool top_bot = false,
						   int top_score = 0,
						   int bottom_score = 0,
						   int ball_type = 0,
						   int ball_speed = 0,
						   int etc_cd1 = 0,
						   int etc_cd2 = 0,
						   int etc_cd3 = 0,
						   int etc_cd4 = 0,
						   int etc_cd5 = 0,
						   string etc_str1 = "",
						   string etc_str2 = "",
						   string etc_str3 = "",
						   string etc_str4 = "",
						   string etc_str5 = "",
						   int position_id = 0,
						   int position_player_id = 0,
						   DateTime? update_date = null
							  )
			{
				this.def_id = def_id;
				this.box_id = box_id;
				this.player_id = player_id;
				this.team_id = team_id;
				this.pitcher_id = pitcher_id;
				this.pit_team_id = pit_team_id;

				this.cat_id = cat_id;
				this.ump_id = ump_id;
				this.game_id = game_id;
				this.park_id = park_id;

				this.pit_throw_id = pit_throw_id;
				this.pit_hand_id = pit_hand_id;
				this.weather_id = weather_id;
				this.ball_type = ball_type;
				this.ball_speed = ball_speed;
				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;

				this.ining = ining;
				this.top_bot = top_bot;
				this.top_score = top_score;
				this.bottom_score = bottom_score;

				this.update_date = update_date;

				this.etc_cd1 = etc_cd1;             // イニング内打順
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;

				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;
				this.position_id = position_id;
				this.position_player_id = position_player_id;

			}

		}
		public static List<defData> GetRecords(int def_id = 0)
		{
			List<defData> orderList = new List<defData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	def_id	,");
				sqlST.AppendLine("	box_id	,");
				sqlST.AppendLine("	player_id	,");
				sqlST.AppendLine("	team_id	,");
				sqlST.AppendLine("	pitcher_id	,");
				sqlST.AppendLine("	pit_team_id	,");
				sqlST.AppendLine("	cat_id	,");
				sqlST.AppendLine("	ump_id	,");
				sqlST.AppendLine("	game_id	,");
				sqlST.AppendLine("	park_id	,");
				sqlST.AppendLine("	pit_hand_id	,");
				sqlST.AppendLine("	pit_throw_id	,");
				sqlST.AppendLine("	weather_id	,");
				sqlST.AppendLine("	count_b	,");
				sqlST.AppendLine("	count_s	,");
				sqlST.AppendLine("	count_o	,");
				sqlST.AppendLine("	runner_1	,");        // bool
				sqlST.AppendLine("	runner_2	,");        // bool
				sqlST.AppendLine("	runner_3	,");        // bool  
				sqlST.AppendLine("	runner_1_player_id	,");
				sqlST.AppendLine("	runner_2_player_id	,");
				sqlST.AppendLine("	runner_3_player_id	,");
				sqlST.AppendLine("	ining	,");            // bool
				sqlST.AppendLine("	top_bot	,");
				sqlST.AppendLine("	top_score	,");
				sqlST.AppendLine("	bottom_score	,");
				sqlST.AppendLine("	ball_type	,");
				sqlST.AppendLine("	ball_speed	,");

				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	position_id	,");
				sqlST.AppendLine("	position_player_id	,");
				sqlST.AppendLine("	update_date	");         // DateTime
															//sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	defensive ");
				if (def_id != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendFormat("	def_id={0} ", def_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new defData(
							reader.GetInt32(0),
							reader.GetInt32(1),
							reader.GetInt32(2),
							reader.GetInt32(3),
							reader.GetInt32(4),
							reader.GetInt32(5),
							reader.GetInt32(6),
							reader.GetInt32(7),
							reader.GetInt32(8),
							reader.GetInt32(9),
							reader.GetInt32(10),
							reader.GetInt32(11),
							reader.GetInt32(12),
							reader.GetInt32(13),
							reader.GetInt32(14),
							reader.GetInt32(15),
							reader.GetBoolean(16),
							reader.GetBoolean(17),
							reader.GetBoolean(18),
							reader.GetInt32(19),
							reader.GetInt32(20),
							reader.GetInt32(21),
							reader.GetInt32(22),
							reader.GetBoolean(23),
							reader.GetInt32(24),
							reader.GetInt32(25),
							reader.GetInt32(26),
							reader.GetInt32(27),
							reader.GetInt32(28),
							reader.GetInt32(29),
							reader.GetInt32(30),
							reader.GetInt32(31),
							reader.GetInt32(32),
							reader.GetString(33),
							reader.GetString(34),
							reader.GetString(35),
							reader.GetString(36),
							reader.GetString(37),
							reader.GetInt32(38),
							reader.GetInt32(39),
							reader.GetDateTime(40)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

	}
	#endregion

	#region 都道府県ID
	class Prefectures
	{

		public Prefectures()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	prefectures( ");
				sqlST.AppendLine("		distinct_id   , ");
				sqlST.AppendLine("		name    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int distinct_id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	prefectures ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@distinct_id, ");
				sqlST.AppendLine("	@name ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@distinct_id", distinct_id);

				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public class prefectures
		{

			#region オーダー変数
			public int distinct_id { get; set; }
			public string name { get; set; }

			#endregion


			public prefectures(int distinct_id = 0, string name = "")
			{
				this.distinct_id = distinct_id;
				this.name = name;
			}

		}
		public static List<prefectures> GetRecords()
		{
			List<prefectures> orderList = new List<prefectures>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	distinct_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	prefectures ");
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	distinct_id ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new prefectures(distinct_id: reader.GetInt32(0),
											  name: reader.GetString(1)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 球場
	class BallPark
	{

		public BallPark()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	ballpark( ");
				sqlST.AppendLine("		park_id   , ");
				sqlST.AppendLine("		name	,    ");
				sqlST.AppendLine("		area_nm    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int park_id = 0, string name = "", string area_nm = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			/// box_idを新たに作成する
			string sqlGetCount = "SELECT park_id FROM ballpark";
			List<ballPark> orderList = new List<ballPark>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCount, precon);
				//CMD_GetCount.CommandText = sqlGetCount;
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				int count = 1;
				while (reader.Read())
				{
					count++;
					orderList.Add(new ballPark(park_id: reader.GetInt32(0)
											  )
								);
				}
				park_id = count;
				precon.Close();
			}

			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	ballpark ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@park_id, ");
				sqlST.AppendLine("	@name, ");
				sqlST.AppendLine("	@area_nm ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@area_nm", area_nm);
				CMD_Insert.Parameters.AddWithValue("@park_id", park_id);

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int park_id = 0, string name = "", string area_nm = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	ballpark ");
				sqlST.AppendLine("SET ");
				sqlST.AppendLine("	name=@name ");
				if (area_nm != "")
				{
					sqlST.AppendLine("	area_nm=@area_nm ");
				}
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("	park_id=@park_id, ");
				sqlST.AppendLine(" ); ");
				CMD.CommandText = sqlST.ToString();
				CMD.Parameters.AddWithValue("@name", name);
				CMD.Parameters.AddWithValue("@area_nm", area_nm);
				CMD.Parameters.AddWithValue("@park_id", park_id);
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class ballPark
		{

			#region オーダー変数
			public int park_id { get; set; }
			public string name { get; set; }
			public string area_nm { get; set; }

			#endregion


			public ballPark(int park_id = 0, string name = "", string area_nm = "")
			{
				this.park_id = park_id;
				this.name = name;
				this.area_nm = area_nm;
			}

		}
		public static List<ballPark> GetRecords()
		{
			List<ballPark> orderList = new List<ballPark>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	park_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	area_nm ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	ballpark ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new ballPark(park_id: reader.GetInt32(0),
											  name: reader.GetString(1),
											  area_nm: reader.GetString(2)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 審判
	class Umpire
	{

		public Umpire()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	umpire( ");
				sqlST.AppendLine("		ump_id   , ");
				sqlST.AppendLine("		name	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int ump_id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			/// box_idを新たに作成する
			string sqlGetCount = "SELECT ump_id FROM umpire";
			List<umpire> orderList = new List<umpire>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCount, precon);
				//CMD_GetCount.CommandText = sqlGetCount;
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				int count = 1;
				while (reader.Read())
				{
					count++;
					orderList.Add(new umpire(ump_id: reader.GetInt32(0)
											  )
								);
				}
				ump_id = count;
				precon.Close();
			}

			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	umpire ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@ump_id, ");
				sqlST.AppendLine("	@name ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@ump_id", ump_id);

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int ump_id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	umpire ");
				sqlST.AppendLine("SET ");
				sqlST.AppendLine("	name=@name ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("	ump_id=@ump_id, ");
				sqlST.AppendLine(" ); ");
				CMD.CommandText = sqlST.ToString();
				CMD.Parameters.AddWithValue("@name", name);
				CMD.Parameters.AddWithValue("@ump_id", ump_id);
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class umpire
		{

			#region オーダー変数
			public int ump_id { get; set; }
			public string name { get; set; }

			#endregion


			public umpire(int ump_id = 0, string name = "")
			{
				this.ump_id = ump_id;
				this.name = name;
			}

		}
		public static List<umpire> GetRecords()
		{
			List<umpire> orderList = new List<umpire>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	ump_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	umpire ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new umpire(ump_id: reader.GetInt32(0),
											  name: reader.GetString(1)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 天気
	class Weather
	{

		public Weather()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	weather( ");
				sqlST.AppendLine("		weather_id   , ");
				sqlST.AppendLine("		name	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int weather_id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			/// box_idを新たに作成する
			string sqlGetCount = "SELECT weather_id FROM weather";
			List<weather> orderList = new List<weather>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCount, precon);
				//CMD_GetCount.CommandText = sqlGetCount;
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				int count = 1;
				while (reader.Read())
				{
					count++;
					orderList.Add(new weather(weather_id: reader.GetInt32(0)
											  )
								);
				}
				weather_id = count;
				precon.Close();
			}

			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	weather ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendLine("	@weather_id, ");
				sqlST.AppendLine("	@name ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.Parameters.AddWithValue("@name", name);
				CMD_Insert.Parameters.AddWithValue("@weather_id", weather_id);

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int weather_id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	weather ");
				sqlST.AppendLine("SET ");
				sqlST.AppendLine("	name=@name ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("	weather_id=@weather_id, ");
				sqlST.AppendLine(" ); ");
				CMD.CommandText = sqlST.ToString();
				CMD.Parameters.AddWithValue("@name", name);
				CMD.Parameters.AddWithValue("@weather_id", weather_id);
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class weather
		{

			#region オーダー変数
			public int weather_id { get; set; }
			public string name { get; set; }

			#endregion


			public weather(int weather_id = 0, string name = "")
			{
				this.weather_id = weather_id;
				this.name = name;
			}

		}
		public static List<weather> GetRecords()
		{
			List<weather> orderList = new List<weather>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	weather_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	weather ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(new weather(weather_id: reader.GetInt32(0),
											  name: reader.GetString(1)
											  )
								);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 言語
	class UseLanguage
	{

		public UseLanguage()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	language( ");
				sqlST.AppendLine("		id   , ");
				sqlST.AppendLine("		name ,	    ");
				sqlST.AppendLine("		selected    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int id = 0, string name = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			/// box_idを新たに作成する
			string sqlGetCount = "SELECT id FROM language";
			List<language> orderList = new List<language>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCount, precon);
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				int count = 1;
				while (reader.Read())
				{
					count++;
					orderList.Add(new language(id: reader.GetInt32(0)
											  )
								);
				}
				id = count;
				precon.Close();
			}

			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	language ");
				sqlST.AppendLine("	( ");
				sqlST.AppendLine("		id, ");
				sqlST.AppendLine("		name, ");
				sqlST.AppendLine("		selected ");
				sqlST.AppendLine("	( ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("	{0}, ", id.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}' ", name).AppendLine();
				sqlST.AppendLine("		0 ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int id = 0)
		{
			if (!updateSelectedLang())
			{
				return;
			}
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	language ");
				sqlST.AppendLine("SET ");
				sqlST.AppendLine("	selected=1 ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendFormat("	id={0} ", id).AppendLine();

				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		private static bool updateSelectedLang()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			try
			{
				using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
				{
					con.Open();
					StringBuilder sqlST = new StringBuilder();
					SqliteCommand CMD = new SqliteCommand();
					CMD.Connection = con;
					sqlST.AppendLine("UPDATE ");
					sqlST.AppendLine("	language ");
					sqlST.AppendLine("SET ");
					sqlST.AppendLine("	selected=0 ");

					CMD.CommandText = sqlST.ToString();
					CMD.ExecuteReader();
					con.Close();
				}
			}
			catch
			{
				return false;
			}
			return true;

		}

		public class language
		{

			#region オーダー変数
			public int id { get; set; }
			public string name { get; set; }
			public int selected { get; set; }
			#endregion


			public language(int id = 0, string name = "", int selected = 0)
			{
				this.id = id;
				this.name = name;
				this.selected = selected;
			}

		}
		public static List<language> GetRecords(int selected = -1)
		{

			List<language> orderList = new List<language>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	name ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	selected ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	language ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				if (selected > -1)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	selected={0} ", selected).AppendLine();
				}
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	id ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new language(
							id: reader.GetInt32(0),
							name: reader.GetString(1),
							selected: reader.GetInt32(2)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 画面
	class UseDisplay
	{

		public UseDisplay()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	display( ");
				sqlST.AppendLine("		id   , ");
				sqlST.AppendLine("		prg	 ,   ");
				sqlST.AppendLine("		wedget	 ,   ");
				sqlST.AppendLine("		ja	 ,   ");
				sqlST.AppendLine("		en	 ,   ");
				sqlST.AppendLine("		etc_str1	 ,   ");
				sqlST.AppendLine("		etc_str2	 ,   ");
				sqlST.AppendLine("		etc_str3	 ,   ");
				sqlST.AppendLine("		etc_str4	 ,   ");
				sqlST.AppendLine("		etc_str5	 ,   ");
				sqlST.AppendLine("		etc_str6	 ,   ");
				sqlST.AppendLine("		etc_str7	 ,   ");
				sqlST.AppendLine("		etc_str8	 ,   ");
				sqlST.AppendLine("		etc_str9	 ,   ");
				sqlST.AppendLine("		etc_str10	 ,  ");
				sqlST.AppendLine("		clss	  , ");
				sqlST.AppendLine("		lang_k	 ,  ");
				sqlST.AppendLine("		text		 	   ");

				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(int id = 0, string prg = "")
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			/// box_idを新たに作成する
			string sqlGetCount = "SELECT id FROM display";
			List<display> orderList = new List<display>();
			using (SqliteConnection precon = new SqliteConnection($"Filename={pathToDB}"))
			{
				precon.Open();
				SqliteCommand CMD_GetCount = new SqliteCommand(sqlGetCount, precon);
				SqliteDataReader reader = CMD_GetCount.ExecuteReader();
				int count = 1;
				while (reader.Read())
				{
					count++;
					orderList.Add(new display(id: reader.GetInt32(0)
											  )
								);
				}
				id = count;
				precon.Close();
			}

			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	display ");
				sqlST.AppendLine("	( ");
				sqlST.AppendLine("		id   , ");
				sqlST.AppendLine("		prg	 ,   ");
				sqlST.AppendLine("		wedget	 ,   ");
				sqlST.AppendLine("		ja	 ,   ");
				sqlST.AppendLine("		en	 ,   ");
				sqlST.AppendLine("		etc_str1	 ,   ");
				sqlST.AppendLine("		etc_str2	 ,   ");
				sqlST.AppendLine("		etc_str3	 ,   ");
				sqlST.AppendLine("		etc_str4	 ,   ");
				sqlST.AppendLine("		etc_str5	 ,   ");
				sqlST.AppendLine("		etc_str6	 ,   ");
				sqlST.AppendLine("		etc_str7	 ,   ");
				sqlST.AppendLine("		etc_str8	 ,   ");
				sqlST.AppendLine("		etc_str9	 ,   ");
				sqlST.AppendLine("		etc_str10	 ,   ");
				sqlST.AppendLine("		clss		 ,   ");
				sqlST.AppendLine("		lang_k		 ,	   ");
				sqlST.AppendLine("		text		 	   ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("	( ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("	{0}, ", id.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}' ", prg).AppendLine();
				sqlST.AppendLine("		0 ");
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(int id = 0)
		{
			if (!updateSelectedLang())
			{
				return;
			}
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	display ");
				sqlST.AppendLine("SET ");
				sqlST.AppendLine("	selected=1 ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendFormat("	id={0} ", id).AppendLine();

				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		private static bool updateSelectedLang()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			try
			{
				using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
				{
					con.Open();
					StringBuilder sqlST = new StringBuilder();
					SqliteCommand CMD = new SqliteCommand();
					CMD.Connection = con;
					sqlST.AppendLine("UPDATE ");
					sqlST.AppendLine("	display ");
					//sqlST.AppendLine("SET ");
					//sqlST.AppendLine("	selected=0 ");

					CMD.CommandText = sqlST.ToString();
					CMD.ExecuteReader();
					con.Close();
				}
			}
			catch
			{
				return false;
			}
			return true;

		}

		public class display
		{

			#region オーダー変数
			public int id { get; set; }
			public string prg { get; set; }
			public string wedget { get; set; }
			public string ja { get; set; }
			public string en { get; set; }
			public string etc_str1 { get; set; }
			public string etc_str2 { get; set; }
			public string etc_str3 { get; set; }
			public string etc_str4 { get; set; }
			public string etc_str5 { get; set; }
			public string etc_str6 { get; set; }
			public string etc_str7 { get; set; }
			public string etc_str8 { get; set; }
			public string etc_str9 { get; set; }
			public string etc_str10 { get; set; }
			public string clss { get; set; }
			public int lang_k { get; set; }
			public string text { get; set; }

			#endregion


			public display(
				int id = 0,
				string prg = "",
				string wedget = "",
				string ja = "",
				string en = "",
				string etc_str1 = "",
				string etc_str2 = "",
				string etc_str3 = "",
				string etc_str4 = "",
				string etc_str5 = "",
				string etc_str6 = "",
				string etc_str7 = "",
				string etc_str8 = "",
				string etc_str9 = "",
				string etc_str10 = "",
				string clss = "",
				int lang_k = -1,
				string text = ""
				)
			{
				this.id = id;
				this.prg = prg;
				this.wedget = wedget;
				this.ja = ja;
				this.en = en;
				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;
				this.etc_str6 = etc_str6;
				this.etc_str7 = etc_str7;
				this.etc_str8 = etc_str8;
				this.etc_str9 = etc_str9;
				this.etc_str10 = etc_str10;
				this.clss = clss;
				this.lang_k = lang_k;
				this.text = text;
			}

		}
		public static List<display> GetRecords(int id = -1, string prg = "", string wedget = "", string clss = "", int lang_k = 1)
		{
			List<display> orderList = new List<display>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				//sqlST.AppendLine("	* ");

				sqlST.AppendLine("	id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	text ");

				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	display ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendLine("	1=1 ");
				if (id > -1)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	id={0} ", id).AppendLine();
				}

				if (prg.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	prg='{0}' ", prg).AppendLine();
				}

				if (wedget.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	wedget='{0}' ", wedget).AppendLine();
				}

				if (clss.Length > 0)
				{
					sqlST.AppendLine("	AND ");
					sqlST.AppendFormat("	clss='{0}' ", clss).AppendLine();
				}
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	lang_k={0} ", lang_k).AppendLine();
				sqlST.AppendLine("ORDER BY ");
				sqlST.AppendLine("	id ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new display(
							id: reader.GetInt32(0),
							text: reader.GetString(1)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 打球方向フラグ
	class BoxFieldDir
	{

		public BoxFieldDir()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	box_field_dir( ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		field_dir_id	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int field_dir_id = -1
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		field_dir_id	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", box_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", ball_id).AppendLine();
				sqlST.AppendFormat("	{0}, ", game_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", pit_player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0} ", field_dir_id.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int field_dir_id = -1
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	box_field_dir ");
				sqlST.AppendLine("SET ");
				sqlST.AppendFormat("	field_dir_id={0} ", field_dir_id).AppendLine();
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (box_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				}
				if (ball_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	ball_id={0} ", ball_id).AppendLine();
				}
				if (game_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				}
				if (pit_player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pit_player_id={0} ", pit_player_id).AppendLine();
				}
				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class boxFieldDir
		{

			#region オーダー変数
			public int box_id { get; set; }
			public int ball_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int pit_player_id { get; set; }
			public int field_dir_id { get; set; }
			#endregion


			public boxFieldDir(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int field_dir_id = -1
				)
			{
				this.box_id = box_id;
				this.ball_id = ball_id;
				this.game_id = game_id;
				this.player_id = player_id;
				this.pit_player_id = pit_player_id;
				this.field_dir_id = field_dir_id;

			}

		}
		public static List<boxFieldDir> GetRecords(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int field_dir_id = -1
			)
		{
			List<boxFieldDir> orderList = new List<boxFieldDir>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		field_dir_id	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		box_field_dir ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (box_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				}
				if (ball_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	ball_id={0} ", ball_id).AppendLine();
				}
				if (game_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				}
				if (pit_player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pit_player_id={0} ", pit_player_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new boxFieldDir(
							box_id: reader.GetInt32(0),
							ball_id: reader.GetInt32(1),
							game_id: reader.GetInt32(2),
							player_id: reader.GetInt32(3),
							pit_player_id: reader.GetInt32(4),
							field_dir_id: reader.GetInt32(5)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 打球結果
	class BoxResult
	{

		public BoxResult()
		{

		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	box_result( ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public static void addRecord(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int hit_id = -1,
								int hit_type_id = -1
			)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", box_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", ball_id).AppendLine();
				sqlST.AppendFormat("	{0}, ", game_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", pit_player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", hit_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0} ", hit_type_id.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public static void updateRecord(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int hit_id = -1,
								int hit_type_id = -1)
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("SET ");
				sqlST.AppendFormat("	hit_id={0} ", hit_id).AppendLine();
				if (hit_type_id >= 0)
				{
					sqlST.AppendFormat("	,hit_type_id={0} ", hit_type_id).AppendLine();
				}
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (box_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				}
				if (ball_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	ball_id={0} ", ball_id).AppendLine();
				}
				if (game_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				}
				if (pit_player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pit_player_id={0} ", pit_player_id).AppendLine();
				}
				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class boxResult
		{

			#region オーダー変数
			public int box_id { get; set; }
			public int ball_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int pit_player_id { get; set; }
			public int hit_id { get; set; }
			public int hit_type_id { get; set; }
			#endregion


			public boxResult(
								int box_id = -1,
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

			}

		}
		public static List<boxResult> GetRecords(
								int box_id = -1,
								int ball_id = -1,
								int game_id = -1,
								int player_id = -1,
								int pit_player_id = -1,
								int hit_id = -1,
								int hit_type_id = -1
			)
		{
			List<boxResult> orderList = new List<boxResult>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		box_id   , ");
				sqlST.AppendLine("		ball_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		pit_player_id	 ,   ");
				sqlST.AppendLine("		hit_id	 ,   ");
				sqlST.AppendLine("		hit_type_id	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		box_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (box_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	box_id={0} ", box_id).AppendLine();
				}
				if (ball_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	ball_id={0} ", ball_id).AppendLine();
				}
				if (game_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				}
				if (player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				}
				if (pit_player_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pit_player_id={0} ", pit_player_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new boxResult(
							box_id: reader.GetInt32(0),
							ball_id: reader.GetInt32(1),
							game_id: reader.GetInt32(2),
							player_id: reader.GetInt32(3),
							pit_player_id: reader.GetInt32(4),
							hit_id: reader.GetInt32(5),
							hit_type_id: reader.GetInt32(6)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 投球結果
	class PitchingResult
	{

		public PitchingResult(int game_id, int player_id, int top_btm_cd = 0)
		{
			MembersInisialize();
			this.game_id = game_id;
			this.player_id = player_id;
			this.top_btm_cd = top_btm_cd;
		}

		private int pitching_id;
		private int game_id;
		private int player_id;
		private int ball_count;
		private int ining_count;
		private int hit_count;
		private int homerun_count;
		private int out_count;
		private int strike_out_count;
		private int fourball_count;
		private int deadball_count;
		private int lost_runs;
		private int earned_runs;
		private int starter_id;
		private int top_btm_cd;

		public void SetPitchingIdCount()
		{
			this.pitching_id = this.GetRecordsCount()[0].pitching_id;
		}

		public void MembersInisialize()
		{
			this.pitching_id = 0;
			this.game_id = 0;
			this.player_id = 0;
			this.ball_count = 0;
			this.ining_count = 0;
			this.hit_count = 0;
			this.homerun_count = 0;
			this.out_count = 0;
			this.strike_out_count = 0;
			this.fourball_count = 0;
			this.deadball_count = 0;
			this.lost_runs = 0;
			this.earned_runs = 0;
			this.starter_id = 0;
			this.top_btm_cd = 0;
		}

		public void UpBallCount() { this.ball_count += 1; }
		public void UpIningCount() { this.ining_count += 1; }
		public void UpOutCount() { this.out_count += 1; }
		public void UpHitCount() { this.hit_count += 1; }
		public void UpHoumeRunCount() { this.homerun_count += 1; }
		public void UpStrikeOutCount() { this.strike_out_count += 1; }
		public void UpFourBallCount() { this.fourball_count += 1; }
		public void UpDeadBallCount() { this.deadball_count += 1; }
		public void UpLostRunsCount() { this.lost_runs += 1; }
		public void UpEarnedRunsCount() { this.earned_runs += 1; }

		public void DownBallCount() { this.ball_count -= 1; }
		public void DownIningCount() { this.ining_count -= 1; }
		public void DownOutCount() { this.out_count -= 1; }
		public void DownHitCount() { this.hit_count -= 1; }
		public void DownHoumeRunCount() { this.homerun_count -= 1; }
		public void DownStrikeOutCount() { this.strike_out_count -= 1; }
		public void DownFourBallCount() { this.fourball_count -= 1; }
		public void DownDeadBallCount() { this.deadball_count -= 1; }
		public void DownLostRunsCount() { this.lost_runs -= 1; }
		public void DownEarnedRunsCount() { this.earned_runs -= 1; }

		public void SetStarterIdTrue() { this.starter_id = 1; }
		public void SetStarterIdFalase() { this.starter_id = 0; }

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	pitching_result( ");
				sqlST.AppendLine("		pitching_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		ining_count	 ,   ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		out_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		lost_runs	,	    ");
				sqlST.AppendLine("		earned_runs	,	    ");
				sqlST.AppendLine("		starter_id	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public void PitchingDataの登録atデータ有無による新規OR追記処理()
		{
			if (CheckPitchingData())
			{
				updateRecord();
				return;
			}
			addRecord();
			return;
		}

		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	pitching_result ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		pitching_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		ining_count	 ,   ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		out_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		lost_runs	,	    ");
				sqlST.AppendLine("		earned_runs	,	    ");
				sqlST.AppendLine("		starter_id	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", this.pitching_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.game_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.ball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.ining_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.hit_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.homerun_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.strike_out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.fourball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.deadball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.lost_runs.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.earned_runs.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.starter_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.top_btm_cd.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}' ", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	pitching_result ");
				sqlST.AppendLine("SET ");
				sqlST.AppendFormat("	ball_count={0} ", this.ball_count).AppendLine();
				sqlST.AppendFormat("	,ining_count={0} ", this.ining_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,hit_count={0} ", this.hit_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,homerun_count={0} ", this.homerun_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,out_count={0} ", this.out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,strike_out_count={0} ", this.strike_out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,fourball_count={0} ", this.fourball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,deadball_count={0} ", this.deadball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,lost_runs={0} ", this.lost_runs.ToString()).AppendLine();
				sqlST.AppendFormat("	,earned_runs={0} ", this.earned_runs.ToString()).AppendLine();
				sqlST.AppendFormat("	,starter_id={0} ", this.starter_id.ToString()).AppendLine();
				sqlST.AppendFormat("	,top_btm_cd={0} ", this.top_btm_cd.ToString()).AppendLine();
				sqlST.AppendFormat("	,updatetime='{0}' ", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", this.game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", this.player_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", this.top_btm_cd).AppendLine();
				if (this.pitching_id > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pitching_id={0} ", this.pitching_id).AppendLine();
				}
				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}
		public class pitchingIdCount
		{
			public int pitching_id { get; set; }
			public pitchingIdCount(int count = 0)
			{
				this.pitching_id = count;
			}
		}
		private List<pitchingIdCount> GetRecordsCount()
		{
			List<pitchingIdCount> countList = new List<pitchingIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(pitching_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(pitching_id) + 1 ");
				sqlST.AppendLine("	END AS pitching_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	pitching_result ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new pitchingIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		private bool CheckPitchingData()
		{
			int check_pitching = GetPitchingData(this.game_id, this.player_id)[0].pitching_id;
			if (check_pitching > 0)
			{
				return true;
			}
			return false;
		}

		public void 画面遷移時既存データが存在した場合はデータをメソッドへ代入()
		{
			if (CheckPitchingData())
			{
				Get既存PitchingData();
			}
		}

		private void Get既存PitchingData()
		{
			pitchingResult resulet = GetRecords(this.game_id, this.player_id)[0];
			this.pitching_id = resulet.pitching_id;
			this.ball_count = resulet.ball_count;
			this.ining_count = resulet.ining_count;
			this.hit_count = resulet.hit_count;
			this.homerun_count = resulet.homerun_count;
			this.out_count = resulet.out_count;
			this.strike_out_count = resulet.strike_out_count;
			this.fourball_count = resulet.fourball_count;
			this.deadball_count = resulet.deadball_count;
			this.lost_runs = resulet.lost_runs;
			this.earned_runs = resulet.earned_runs;
			this.starter_id = resulet.starter_id;
			this.top_btm_cd = resulet.top_btm_cd;
		}

		private List<pitchingIdCount> GetPitchingData(int game_id, int player_id)
		{
			List<pitchingIdCount> countList = new List<pitchingIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(pitching_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	pitching_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new pitchingIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}


		public class pitchingResult
		{
			#region オーダー変数
			public int pitching_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int ball_count { get; set; }
			public int ining_count { get; set; }
			public int hit_count { get; set; }
			public int homerun_count { get; set; }
			public int out_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int lost_runs { get; set; }
			public int earned_runs { get; set; }
			public int starter_id { get; set; }
			public int top_btm_cd { get; set; }
			#endregion

			public pitchingResult(
				int pitching_id,
				int game_id,
				int player_id,
				int ball_count,
				int ining_count,
				int hit_count,
				int homerun_count,
				int out_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int lost_runs,
				int earned_runs,
				int starter_id,
				int top_btm_cd
				)
			{
				this.pitching_id = pitching_id;
				this.game_id = game_id;
				this.player_id = player_id;
				this.ball_count = ball_count;
				this.ining_count = ining_count;
				this.hit_count = hit_count;
				this.homerun_count = homerun_count;
				this.out_count = out_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.lost_runs = lost_runs;
				this.earned_runs = earned_runs;
				this.starter_id = starter_id;
				this.top_btm_cd = top_btm_cd;
			}
		}


		public List<pitchingResult> GetRecords(
								int game_id = -1,
								int player_id = -1,
								int pitching_id = -1,
								int top_btm_cd = -1
			)
		{
			List<pitchingResult> orderList = new List<pitchingResult>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		pitching_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		ining_count	 ,   ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		out_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		lost_runs	,	    ");
				sqlST.AppendLine("		earned_runs	,	    ");
				sqlST.AppendLine("		starter_id	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		pitching_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				if (top_btm_cd >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				}
				if (pitching_id > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pitching_id={0} ", pitching_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new pitchingResult(
							pitching_id: reader.GetInt32(0),
							game_id: reader.GetInt32(1),
							player_id: reader.GetInt32(2),
							ball_count: reader.GetInt32(3),
							ining_count: reader.GetInt32(4),
							hit_count: reader.GetInt32(5),
							homerun_count: reader.GetInt32(6),
							out_count: reader.GetInt32(7),
							strike_out_count: reader.GetInt32(8),
							fourball_count: reader.GetInt32(9),
							deadball_count: reader.GetInt32(10),
							lost_runs: reader.GetInt32(11),
							earned_runs: reader.GetInt32(12),
							starter_id: reader.GetInt32(13),
							top_btm_cd: reader.GetInt32(14)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class pitchingResultSum
		{
			#region オーダー変数
			public int ball_count { get; set; }
			public int ining_count { get; set; }
			public int hit_count { get; set; }
			public int homerun_count { get; set; }
			public int out_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int lost_runs { get; set; }
			public int earned_runs { get; set; }
			#endregion

			public pitchingResultSum(
				int ball_count,
				int ining_count,
				int hit_count,
				int homerun_count,
				int out_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int lost_runs,
				int earned_runs
				)
			{
				this.ball_count = ball_count;
				this.ining_count = ining_count;
				this.hit_count = hit_count;
				this.homerun_count = homerun_count;
				this.out_count = out_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.lost_runs = lost_runs;
				this.earned_runs = earned_runs;
			}
		}

		public static List<pitchingResultSum> GetSumRecords(
								int game_id = -1,
								int top_btm_cd = 0
			)
		{
			List<pitchingResultSum> orderList = new List<pitchingResultSum>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(ball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(ball_count) ");
				sqlST.AppendLine("	END AS ball_count , ");
				//sqlST.AppendLine("		SUM(ball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(ining_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(ining_count) ");
				sqlST.AppendLine("	END AS ining_count , ");
				//sqlST.AppendLine("		SUM(ining_count)	 ,   ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(hit_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(hit_count) ");
				sqlST.AppendLine("	END AS hit_count , ");
				//sqlST.AppendLine("		SUM(hit_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(homerun_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(homerun_count) ");
				sqlST.AppendLine("	END AS homerun_count , ");
				//sqlST.AppendLine("		SUM(homerun_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(out_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(out_count) ");
				sqlST.AppendLine("	END AS out_count , ");
				//sqlST.AppendLine("		SUM(out_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(strike_out_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(strike_out_count) ");
				sqlST.AppendLine("	END AS strike_out_count , ");
				//sqlST.AppendLine("		SUM(strike_out_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(fourball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(fourball_count) ");
				sqlST.AppendLine("	END AS fourball_count , ");
				//sqlST.AppendLine("		SUM(fourball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(deadball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(deadball_count) ");
				sqlST.AppendLine("	END AS deadball_count , ");
				//sqlST.AppendLine("		SUM(deadball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(lost_runs) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(lost_runs) ");
				sqlST.AppendLine("	END AS lost_runs , ");
				//sqlST.AppendLine("		SUM(lost_runs)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(earned_runs) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(earned_runs) ");
				sqlST.AppendLine("	END AS earned_runs  ");
				//sqlST.AppendLine("		SUM(earned_runs)		    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		pitching_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new pitchingResultSum(
							ball_count: reader.GetInt32(0),
							ining_count: reader.GetInt32(1),
							hit_count: reader.GetInt32(2),
							homerun_count: reader.GetInt32(3),
							out_count: reader.GetInt32(4),
							strike_out_count: reader.GetInt32(5),
							fourball_count: reader.GetInt32(6),
							deadball_count: reader.GetInt32(7),
							lost_runs: reader.GetInt32(8),
							earned_runs: reader.GetInt32(9)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class pitchingResultDisplay
		{
			#region オーダー変数
			public int pitching_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int ball_count { get; set; }
			public int ining_count { get; set; }
			public int hit_count { get; set; }
			public int homerun_count { get; set; }
			public int out_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int lost_runs { get; set; }
			public int earned_runs { get; set; }
			public int starter_id { get; set; }
			public int top_btm_cd { get; set; }
			public string player_name { get; set; }
			public string inings { get; set; }
			#endregion

			public pitchingResultDisplay(
				int pitching_id,
				int game_id,
				int player_id,
				int ball_count,
				int ining_count,
				int hit_count,
				int homerun_count,
				int out_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int lost_runs,
				int earned_runs,
				int starter_id,
				int top_btm_cd,
				string player_name,
				string inings
				)
			{
				this.pitching_id = pitching_id;
				this.game_id = game_id;
				this.player_id = player_id;
				this.ball_count = ball_count;
				this.ining_count = ining_count;
				this.hit_count = hit_count;
				this.homerun_count = homerun_count;
				this.out_count = out_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.lost_runs = lost_runs;
				this.earned_runs = earned_runs;
				this.starter_id = starter_id;
				this.top_btm_cd = top_btm_cd;
				this.player_name = player_name;
				this.inings = inings;
			}
		}
		public static List<pitchingResultDisplay> GetRecordsDisplay(
								int game_id = -1,
								int player_id = -1,
								int pitching_id = -1,
								int top_btm_cd = -1
			)
		{
			List<pitchingResultDisplay> orderList = new List<pitchingResultDisplay>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		pitching_result.pitching_id	 ,   ");
				sqlST.AppendLine("		pitching_result.game_id	 ,   ");
				sqlST.AppendLine("		pitching_result.player_id	 ,   ");
				sqlST.AppendLine("		pitching_result.ball_count	,	    ");
				sqlST.AppendLine("		pitching_result.ining_count	 ,   ");
				sqlST.AppendLine("		pitching_result.hit_count	,	    ");
				sqlST.AppendLine("		pitching_result.homerun_count	,	    ");
				sqlST.AppendLine("		pitching_result.out_count	,	    ");
				sqlST.AppendLine("		pitching_result.strike_out_count	,	    ");
				sqlST.AppendLine("		pitching_result.fourball_count	,	    ");
				sqlST.AppendLine("		pitching_result.deadball_count	,	    ");
				sqlST.AppendLine("		pitching_result.lost_runs	,	    ");
				sqlST.AppendLine("		pitching_result.earned_runs	,	    ");
				sqlST.AppendLine("		pitching_result.starter_id	,	    ");
				sqlST.AppendLine("		pitching_result.top_btm_cd	,	    ");
				sqlST.AppendLine("		player.player_name	,	    ");
				sqlST.AppendLine("		CASE	pitching_result.out_count % 3	    ");
				sqlST.AppendLine("			WHEN 0 THEN		    ");
				sqlST.AppendLine("				CAST(pitching_result.out_count/3 AS TEXT)		    ");
				sqlST.AppendLine("			ELSE		    ");
				sqlST.AppendLine("				CAST(pitching_result.out_count/3 AS TEXT)|| ' ' || CAST(pitching_result.out_count%3 AS TEXT) || '/' || '3'		    ");
				sqlST.AppendLine("			END AS inings	,		    ");
				sqlST.AppendLine("		pitching_result.updatetime	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		pitching_result ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("		player ");
				sqlST.AppendLine("		ON player.player_id=pitching_result.player_id ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	pitching_result.game_id={0} ", game_id).AppendLine();
				//sqlST.AppendLine("		AND	 ");
				//sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				if (top_btm_cd >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pitching_result.top_btm_cd={0} ", top_btm_cd).AppendLine();
				}
				if (pitching_id > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	pitching_result.pitching_id={0} ", pitching_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new pitchingResultDisplay(
							pitching_id: reader.GetInt32(0),
							game_id: reader.GetInt32(1),
							player_id: reader.GetInt32(2),
							ball_count: reader.GetInt32(3),
							ining_count: reader.GetInt32(4),
							hit_count: reader.GetInt32(5),
							homerun_count: reader.GetInt32(6),
							out_count: reader.GetInt32(7),
							strike_out_count: reader.GetInt32(8),
							fourball_count: reader.GetInt32(9),
							deadball_count: reader.GetInt32(10),
							lost_runs: reader.GetInt32(11),
							earned_runs: reader.GetInt32(12),
							starter_id: reader.GetInt32(13),
							top_btm_cd: reader.GetInt32(14),
							player_name: reader.GetString(15),
							inings: reader.GetString(16)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}




	#endregion

	#region 打席結果
	class BattingResult
	{

		public BattingResult(int game_id, int player_id, int order_id, int top_btm_cd, int position = -1)
		{
			MembersInisialize();
			this.game_id = game_id;
			this.player_id = player_id;
			this.order_id = order_id;
			this.top_btm_cd = top_btm_cd;
			SetPositonName(position);
		}

		private int batting_id;
		private int game_id;
		private int player_id;
		private int order_id;
		private int changed_count;
		private int ball_count;
		private int box_count;
		private int bat_count;
		private int runs_count;
		private int hit_count;
		private int run_batted_in_count;
		private int strike_out_count;
		private int fourball_count;
		private int deadball_count;
		private int sacrifice_count;
		private int steal_count;
		private int error_count;
		private int homerun_count;
		private int top_btm_cd;
		private string position_name;
		private void SetPositonName(int position)
		{
			if (position < 0) { return; }
			if (position == 0) { this.position_name = ""; }
			int selectedLang = UseLanguage.GetRecords(selected: 1)[0].id;
			string posiName = "";
			try
			{
				posiName = PositionName.GetRecords(position_id: position, language_id: selectedLang - 1)[0].position_name;
			}
			catch { }
			this.position_name = posiName;
		}
		private string GetPositonName(int position)
		{
			if (position < 0) { return ""; }
			if (position == 0) { this.position_name = ""; }
			int selectedLang = UseLanguage.GetRecords(selected: 1)[0].id;
			string posiName = "";
			try
			{
				posiName = PositionName.GetRecords(position_id: position, language_id: selectedLang - 1)[0].position_name;
			}
			catch { }
			return posiName;
		}


		public void SetBattingIdCount(int position)
		{
			this.batting_id = this.GetRecordsCount()[0].batting_id;
		}

		public void MembersInisialize()
		{
			this.batting_id = 0;
			this.game_id = 0;
			this.player_id = 0;
			this.order_id = 0;
			this.changed_count = 0;
			this.ball_count = 0;
			this.box_count = 0;
			this.bat_count = 0;
			this.runs_count = 0;
			this.hit_count = 0;
			this.run_batted_in_count = 0;
			this.strike_out_count = 0;
			this.fourball_count = 0;
			this.deadball_count = 0;
			this.sacrifice_count = 0;
			this.steal_count = 0;
			this.error_count = 0;
			this.homerun_count = 0;
			this.top_btm_cd = 0;
			this.position_name = "";
		}

		public void UpBallCount() { this.ball_count += 1; }
		public void UpBoxCount() { this.box_count += 1; }
		public void UpBatCount() { this.bat_count += 1; }
		public void UpHitCount() { this.hit_count += 1; }
		public void UpHoumeRunCount() { this.homerun_count += 1; }
		public void UpStrikeOutCount() { this.strike_out_count += 1; }
		public void UpFourBallCount() { this.fourball_count += 1; }
		public void UpDeadBallCount() { this.deadball_count += 1; }
		public void UpRunsCount() { this.runs_count += 1; }
		public void UpRunBattedInCount() { this.run_batted_in_count += 1; }
		public void UpSacrificeCount() { this.sacrifice_count += 1; }
		public void UpErrorCount() { this.error_count += 1; }
		public void UpStealCount() { this.steal_count += 1; }

		public void DownBallCount() { this.ball_count -= 1; }
		public void DownBoxCount() { this.box_count -= 1; }
		public void DownBatCount() { this.bat_count -= 1; }
		public void DownHitCount() { this.hit_count -= 1; }
		public void DownHoumeRunCount() { this.homerun_count -= 1; }
		public void DownStrikeOutCount() { this.strike_out_count -= 1; }
		public void DownFourBallCount() { this.fourball_count -= 1; }
		public void DownDeadBallCount() { this.deadball_count -= 1; }
		public void DownRunsCount() { this.runs_count -= 1; }
		public void DownRunBattedInCount() { this.run_batted_in_count -= 1; }
		public void DownSacrificeCount() { this.sacrifice_count -= 1; }
		public void DownErrorCount() { this.error_count -= 1; }
		public void DownStealCount() { this.steal_count -= 1; }
		public void SetPositionName(string position_name)
		{
			this.position_name = position_name;
		}
		public void SetAdditionPositionName(int position)
		{
			string add_position = GetPositonName(position);
			this.position_name = this.position_name + "-" + add_position;
		}

		public void SetPlayerId(int player_id)
		{
			this.player_id = player_id;
		}


		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	batting_result( ");
				sqlST.AppendLine("		batting_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		order_id	 ,   ");
				sqlST.AppendLine("		changed_count	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		box_count	,	    ");
				sqlST.AppendLine("		bat_count	,	    ");
				sqlST.AppendLine("		runs_count	,	    ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		run_batted_in_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		sacrifice_count	,	    ");
				sqlST.AppendLine("		steal_count	,	    ");
				sqlST.AppendLine("		error_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				sqlST.AppendLine("		position_name	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public void BattingDataの登録atデータ有無による新規OR追記処理()
		{
			if (CheckBattingData())
			{
				updateRecord();
				return;
			}
			this.changed_count = this.GetChangedCount(this.game_id, this.order_id, this.top_btm_cd)[0].batting_id;
			addRecord();
			return;
		}

		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	batting_result ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		batting_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		order_id	 ,   ");
				sqlST.AppendLine("		changed_count	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		box_count	,	    ");
				sqlST.AppendLine("		bat_count	,	    ");
				sqlST.AppendLine("		runs_count	,	    ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		run_batted_in_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		sacrifice_count	,	    ");
				sqlST.AppendLine("		steal_count	,	    ");
				sqlST.AppendLine("		error_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				sqlST.AppendLine("		position_name	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", this.batting_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.game_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.player_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.order_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.changed_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.ball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.box_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.bat_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.runs_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.hit_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.run_batted_in_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.strike_out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.fourball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.deadball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.sacrifice_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.steal_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.error_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.homerun_count.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.top_btm_cd.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}', ", this.position_name.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}' ", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	batting_result ");
				sqlST.AppendLine("SET ");
				sqlST.AppendFormat("	ball_count={0} ", this.ball_count).AppendLine();
				sqlST.AppendFormat("	,box_count={0} ", this.box_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,bat_count={0} ", this.bat_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,runs_count={0} ", this.runs_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,hit_count={0} ", this.hit_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,run_batted_in_count={0} ", this.run_batted_in_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,strike_out_count={0} ", this.strike_out_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,fourball_count={0} ", this.fourball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,deadball_count={0} ", this.deadball_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,sacrifice_count={0} ", this.sacrifice_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,steal_count={0} ", this.steal_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,error_count={0} ", this.error_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,homerun_count={0} ", this.homerun_count.ToString()).AppendLine();
				sqlST.AppendFormat("	,position_name='{0}' ", this.position_name.ToString()).AppendLine();
				sqlST.AppendFormat("	,updatetime='{0}' ", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", this.game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", this.player_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	order_id={0} ", this.order_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	changed_count={0} ", this.changed_count).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", this.top_btm_cd).AppendLine();
				if (this.batting_id > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	batting_id={0} ", this.batting_id).AppendLine();
				}
				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}



		public class battingIdCount
		{
			public int batting_id { get; set; }
			public battingIdCount(int count = 0)
			{
				this.batting_id = count;
			}
		}
		private List<battingIdCount> GetRecordsCount()
		{
			List<battingIdCount> countList = new List<battingIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(batting_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(batting_id) + 1 ");
				sqlST.AppendLine("	END AS batting_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	batting_result ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new battingIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		private List<battingIdCount> GetChangedCount(int game_id, int order_id, int top_btm_cd)
		{
			List<battingIdCount> countList = new List<battingIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			COUNT(order_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			COUNT(order_id) + 1 ");
				sqlST.AppendLine("	END AS batting_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	batting_result ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	order_id={0} ", order_id).AppendLine();
				sqlST.AppendLine("	AND ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new battingIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}

		private bool CheckBattingData()
		{
			int check_batting
				= GetBattingData(
						this.game_id,
						this.player_id,
						this.order_id,
						this.changed_count,
						this.top_btm_cd
						)[0].batting_id;
			if (check_batting > 0)
			{
				return true;
			}
			return false;
		}

		public void 画面遷移時既存データが存在した場合はデータをメソッドへ代入()
		{
			if (CheckBattingData())
			{
				Get既存BattingData();
			}
		}

		private void Get既存BattingData()
		{
			battingResult result
				= GetRecords(
						game_id: this.game_id,
						player_id: this.player_id,
						order_id: this.order_id,
						//changed_count:this.changed_count,
						top_btm_cd: this.top_btm_cd)[0];
			this.game_id = result.game_id;
			this.player_id = result.player_id;
			this.batting_id = result.batting_id;
			this.order_id = result.order_id;
			this.changed_count = result.changed_count;
			this.ball_count = result.ball_count;
			this.hit_count = result.hit_count;
			this.homerun_count = result.homerun_count;
			this.strike_out_count = result.strike_out_count;
			this.fourball_count = result.fourball_count;
			this.deadball_count = result.deadball_count;
			this.box_count = result.box_count;
			this.bat_count = result.bat_count;
			this.runs_count = result.runs_count;
			this.run_batted_in_count = result.run_batted_in_count;
			this.sacrifice_count = result.sacrifice_count;
			this.steal_count = result.steal_count;
			this.error_count = result.error_count;
			this.top_btm_cd = result.top_btm_cd;
			this.position_name = result.position_name;
		}

		private List<battingIdCount> GetBattingData(int game_id, int player_id, int order_id, int changed_count, int top_btm_cd)
		{
			List<battingIdCount> countList = new List<battingIdCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	COUNT(batting_id) ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	batting_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	order_id={0} ", order_id).AppendLine();
				//sqlST.AppendLine("		AND	 ");
				//sqlST.AppendFormat("	changed_count={0} ", changed_count).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new battingIdCount(reader.GetInt32(0)));
				}
			}
			return countList;
		}


		public class battingResult
		{
			#region オーダー変数
			public int batting_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int order_id { get; set; }
			public int changed_count { get; set; }
			public int box_count { get; set; }
			public int bat_count { get; set; }
			public int ball_count { get; set; }
			public int runs_count { get; set; }
			public int hit_count { get; set; }
			public int run_batted_in_count { get; set; }
			public int homerun_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int sacrifice_count { get; set; }
			public int steal_count { get; set; }
			public int error_count { get; set; }
			public int top_btm_cd { get; set; }
			public string position_name { get; set; }
			#endregion

			public battingResult(
				int batting_id,
				int game_id,
				int player_id,
				int order_id,
				int changed_count,
				int box_count,
				int bat_count,
				int ball_count,
				int runs_count,
				int hit_count,
				int run_batted_in_count,
				int homerun_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int sacrifice_count,
				int steal_count,
				int error_count,
				int top_btm_cd,
				string position_name
				)
			{
				this.batting_id = batting_id;
				this.game_id = game_id;
				this.player_id = player_id;
				this.order_id = order_id;
				this.changed_count = changed_count;
				this.box_count = box_count;
				this.bat_count = bat_count;
				this.ball_count = ball_count;
				this.runs_count = runs_count;
				this.hit_count = hit_count;
				this.run_batted_in_count = run_batted_in_count;
				this.homerun_count = homerun_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.sacrifice_count = sacrifice_count;
				this.steal_count = steal_count;
				this.error_count = error_count;
				this.top_btm_cd = top_btm_cd;
				this.position_name = position_name;
			}
		}
		public List<battingResult> GetRecords(
								int game_id = -1,
								int player_id = -1,
								int batting_id = -1,
								int order_id = -1,
								int changed_count = -1,
								int top_btm_cd = 0
			)
		{
			List<battingResult> orderList = new List<battingResult>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		batting_id	 ,   ");
				sqlST.AppendLine("		game_id	 ,   ");
				sqlST.AppendLine("		player_id	 ,   ");
				sqlST.AppendLine("		order_id	 ,   ");
				sqlST.AppendLine("		changed_count	 ,   ");
				sqlST.AppendLine("		ball_count	,	    ");
				sqlST.AppendLine("		box_count	,	    ");
				sqlST.AppendLine("		bat_count	,	    ");
				sqlST.AppendLine("		runs_count	,	    ");
				sqlST.AppendLine("		hit_count	,	    ");
				sqlST.AppendLine("		run_batted_in_count	,	    ");
				sqlST.AppendLine("		strike_out_count	,	    ");
				sqlST.AppendLine("		fourball_count	,	    ");
				sqlST.AppendLine("		deadball_count	,	    ");
				sqlST.AppendLine("		sacrifice_count	,	    ");
				sqlST.AppendLine("		steal_count	,	    ");
				sqlST.AppendLine("		error_count	,	    ");
				sqlST.AppendLine("		homerun_count	,	    ");
				sqlST.AppendLine("		top_btm_cd	,	    ");
				//sqlST.AppendLine("	CASE ");
				//sqlST.AppendLine("		WHEN ");
				//sqlST.AppendLine("			changed_count is 1 THEN '(' || position_name || ')' ");
				//sqlST.AppendLine("		ELSE ");
				//sqlST.AppendLine("			position_name ");
				//sqlST.AppendLine("	END AS position_name , ");
				sqlST.AppendLine("		position_name	,	    ");
				sqlST.AppendLine("		updatetime	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		batting_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	player_id={0} ", player_id).AppendLine();
				if (changed_count > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	changed_count={0} ", changed_count).AppendLine();
				}
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				if (batting_id > 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	batting_id={0} ", batting_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new battingResult(
							batting_id: reader.GetInt32(0),
							game_id: reader.GetInt32(1),
							player_id: reader.GetInt32(2),
							order_id: reader.GetInt32(3),
							changed_count: reader.GetInt32(4),
							ball_count: reader.GetInt32(5),
							box_count: reader.GetInt32(6),
							bat_count: reader.GetInt32(7),
							runs_count: reader.GetInt32(8),
							hit_count: reader.GetInt32(9),
							run_batted_in_count: reader.GetInt32(10),
							strike_out_count: reader.GetInt32(11),
							fourball_count: reader.GetInt32(12),
							deadball_count: reader.GetInt32(13),
							sacrifice_count: reader.GetInt32(14),
							steal_count: reader.GetInt32(15),
							error_count: reader.GetInt32(16),
							homerun_count: reader.GetInt32(17),
							top_btm_cd: reader.GetInt32(18),
							position_name: reader.GetString(19)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class battingResultSum
		{
			#region オーダー変数
			public int box_count { get; set; }
			public int bat_count { get; set; }
			public int ball_count { get; set; }
			public int runs_count { get; set; }
			public int hit_count { get; set; }
			public int run_batted_in_count { get; set; }
			public int homerun_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int sacrifice_count { get; set; }
			public int steal_count { get; set; }
			public int error_count { get; set; }
			#endregion

			public battingResultSum(
				int box_count,
				int bat_count,
				int ball_count,
				int runs_count,
				int hit_count,
				int run_batted_in_count,
				int homerun_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int sacrifice_count,
				int steal_count,
				int error_count
				)
			{
				this.box_count = box_count;
				this.bat_count = bat_count;
				this.ball_count = ball_count;
				this.runs_count = runs_count;
				this.hit_count = hit_count;
				this.run_batted_in_count = run_batted_in_count;
				this.homerun_count = homerun_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.sacrifice_count = sacrifice_count;
				this.steal_count = steal_count;
				this.error_count = error_count;
			}
		}
		public static List<battingResultSum> GetSumRecords(
								int game_id = -1,
								int top_btm_cd = 0
			)
		{
			List<battingResultSum> orderList = new List<battingResultSum>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(ball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(ball_count) ");
				sqlST.AppendLine("	END AS ball_count , ");
				//sqlST.AppendLine("		SUM(ball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(box_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(box_count) ");
				sqlST.AppendLine("	END AS box_count , ");
				//sqlST.AppendLine("		SUM(box_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(bat_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(bat_count) ");
				sqlST.AppendLine("	END AS bat_count , ");
				//sqlST.AppendLine("		SUM(bat_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(runs_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(runs_count) ");
				sqlST.AppendLine("	END AS runs_count , ");
				//sqlST.AppendLine("		SUM(runs_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(hit_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(hit_count) ");
				sqlST.AppendLine("	END AS hit_count , ");
				//sqlST.AppendLine("		SUM(hit_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(run_batted_in_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(run_batted_in_count) ");
				sqlST.AppendLine("	END AS run_batted_in_count , ");
				//sqlST.AppendLine("		SUM(run_batted_in_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(strike_out_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(strike_out_count) ");
				sqlST.AppendLine("	END AS strike_out_count , ");
				//sqlST.AppendLine("		SUM(strike_out_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(fourball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(fourball_count) ");
				sqlST.AppendLine("	END AS fourball_count , ");
				//sqlST.AppendLine("		SUM(fourball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(deadball_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(deadball_count) ");
				sqlST.AppendLine("	END AS deadball_count , ");
				//sqlST.AppendLine("		SUM(deadball_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(sacrifice_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(sacrifice_count) ");
				sqlST.AppendLine("	END AS sacrifice_count , ");
				//sqlST.AppendLine("		SUM(sacrifice_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(steal_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(steal_count) ");
				sqlST.AppendLine("	END AS steal_count , ");
				//sqlST.AppendLine("		SUM(steal_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(error_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(error_count) ");
				sqlST.AppendLine("	END AS error_count , ");
				//sqlST.AppendLine("		SUM(error_count)	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			SUM(homerun_count) is null THEN 0 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			SUM(homerun_count) ");
				sqlST.AppendLine("	END AS homerun_count  ");
				//sqlST.AppendLine("		SUM(homerun_count)		    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		batting_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new battingResultSum(
							ball_count: reader.GetInt32(0),
							box_count: reader.GetInt32(1),
							bat_count: reader.GetInt32(2),
							runs_count: reader.GetInt32(3),
							hit_count: reader.GetInt32(4),
							run_batted_in_count: reader.GetInt32(5),
							strike_out_count: reader.GetInt32(6),
							fourball_count: reader.GetInt32(7),
							deadball_count: reader.GetInt32(8),
							sacrifice_count: reader.GetInt32(9),
							steal_count: reader.GetInt32(10),
							error_count: reader.GetInt32(11),
							homerun_count: reader.GetInt32(12)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

		public class battingResultDisplay
		{
			#region オーダー変数
			public int batting_id { get; set; }
			public int game_id { get; set; }
			public int player_id { get; set; }
			public int order_id { get; set; }
			public int changed_count { get; set; }
			public int box_count { get; set; }
			public int bat_count { get; set; }
			public int ball_count { get; set; }
			public int runs_count { get; set; }
			public int hit_count { get; set; }
			public int run_batted_in_count { get; set; }
			public int homerun_count { get; set; }
			public int strike_out_count { get; set; }
			public int fourball_count { get; set; }
			public int deadball_count { get; set; }
			public int sacrifice_count { get; set; }
			public int steal_count { get; set; }
			public int error_count { get; set; }
			public int top_btm_cd { get; set; }
			public string player_name { get; set; }
			public string position_name { get; set; }
			#endregion

			public battingResultDisplay(
				int batting_id,
				int game_id,
				int player_id,
				int order_id,
				int changed_count,
				int box_count,
				int bat_count,
				int ball_count,
				int runs_count,
				int hit_count,
				int run_batted_in_count,
				int homerun_count,
				int strike_out_count,
				int fourball_count,
				int deadball_count,
				int sacrifice_count,
				int steal_count,
				int error_count,
				int top_btm_cd,
				string player_name,
				string position_name
				)
			{
				this.batting_id = batting_id;
				this.game_id = game_id;
				this.player_id = player_id;
				this.order_id = order_id;
				this.changed_count = changed_count;
				this.box_count = box_count;
				this.bat_count = bat_count;
				this.ball_count = ball_count;
				this.runs_count = runs_count;
				this.hit_count = hit_count;
				this.run_batted_in_count = run_batted_in_count;
				this.homerun_count = homerun_count;
				this.strike_out_count = strike_out_count;
				this.fourball_count = fourball_count;
				this.deadball_count = deadball_count;
				this.sacrifice_count = sacrifice_count;
				this.steal_count = steal_count;
				this.error_count = error_count;
				this.top_btm_cd = top_btm_cd;
				this.player_name = player_name;
				this.position_name = position_name;
			}
		}

		public static List<battingResultDisplay> GetGameResultDisplayRecords(
								int game_id = -1,
								int top_btm_cd = 0
			)
		{
			List<battingResultDisplay> orderList = new List<battingResultDisplay>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		batting_result.batting_id	 ,   ");
				sqlST.AppendLine("		batting_result.game_id	 ,   ");
				sqlST.AppendLine("		batting_result.player_id	 ,   ");
				sqlST.AppendLine("		batting_result.order_id	 ,   ");
				sqlST.AppendLine("		batting_result.changed_count	 ,   ");
				sqlST.AppendLine("		batting_result.ball_count	,	    ");
				sqlST.AppendLine("		batting_result.box_count	,	    ");
				sqlST.AppendLine("		batting_result.bat_count	,	    ");
				sqlST.AppendLine("		batting_result.runs_count	,	    ");
				sqlST.AppendLine("		batting_result.hit_count	,	    ");
				sqlST.AppendLine("		batting_result.run_batted_in_count	,	    ");
				sqlST.AppendLine("		batting_result.strike_out_count	,	    ");
				sqlST.AppendLine("		batting_result.fourball_count	,	    ");
				sqlST.AppendLine("		batting_result.deadball_count	,	    ");
				sqlST.AppendLine("		batting_result.sacrifice_count	,	    ");
				sqlST.AppendLine("		batting_result.steal_count	,	    ");
				sqlST.AppendLine("		batting_result.error_count	,	    ");
				sqlST.AppendLine("		batting_result.homerun_count	,	    ");
				sqlST.AppendLine("		batting_result.top_btm_cd	,	    ");
				sqlST.AppendLine("		player.player_name	,	    ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			batting_result.changed_count is 1 THEN '(' || batting_result.position_name || ')' ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			batting_result.position_name ");
				sqlST.AppendLine("	END AS position_name , ");
				//sqlST.AppendLine("		batting_result.position_name	,	    ");
				sqlST.AppendLine("		batting_result.updatetime	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		batting_result ");
				sqlST.AppendLine("INNER JOIN ");
				sqlST.AppendLine("		player ");
				sqlST.AppendLine("		ON player.player_id=batting_result.player_id ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	game_id={0} ", game_id).AppendLine();
				sqlST.AppendLine("		AND	 ");
				sqlST.AppendFormat("	top_btm_cd={0} ", top_btm_cd).AppendLine();

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new battingResultDisplay(
							batting_id: reader.GetInt32(0),
							game_id: reader.GetInt32(1),
							player_id: reader.GetInt32(2),
							order_id: reader.GetInt32(3),
							changed_count: reader.GetInt32(4),
							ball_count: reader.GetInt32(5),
							box_count: reader.GetInt32(6),
							bat_count: reader.GetInt32(7),
							runs_count: reader.GetInt32(8),
							hit_count: reader.GetInt32(9),
							run_batted_in_count: reader.GetInt32(10),
							strike_out_count: reader.GetInt32(11),
							fourball_count: reader.GetInt32(12),
							deadball_count: reader.GetInt32(13),
							sacrifice_count: reader.GetInt32(14),
							steal_count: reader.GetInt32(15),
							error_count: reader.GetInt32(16),
							homerun_count: reader.GetInt32(17),
							top_btm_cd: reader.GetInt32(18),
							player_name: reader.GetString(19),
							position_name: reader.GetString(20)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

	}
	#endregion

	#region 打球結果名
	class BoxResultName
	{
		private int result_id = -1;
		private int hit_id = -1;
		private int hit_type_id = -1;
		private int language_id = -1;
		private string result_name = "";

		public BoxResultName()
		{
		}

		public void SetResultId(int result_id)
		{
			this.result_id = result_id;
		}
		public void SetHitId(int hit_id)
		{
			this.hit_id = hit_id;
		}
		public void SetHitTypeId(int hit_type_id)
		{
			this.hit_type_id = hit_type_id;
		}
		public void SetLanguageId(int language_id)
		{
			this.language_id = language_id;
		}
		private void SetResultName(string result_name)
		{
			this.result_name = result_name;
		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	box_result_name( ");
				sqlST.AppendLine("		result_id	,	    ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		result_name	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	box_result ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		result_id	,	    ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		result_name	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", result_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", hit_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", hit_type_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", language_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0} ", result_name.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	box_result_name ");
				sqlST.AppendLine("SET ");
				if (this.result_name.Length > 0)
				{
					sqlST.AppendFormat("	,result_name={0} ", this.result_name).AppendLine();
				}
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (this.result_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	result_id={0} ", this.result_id).AppendLine();
				}
				if (this.hit_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_id={0} ", this.hit_id).AppendLine();
				}
				if (this.hit_type_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_type_id={0} ", this.hit_type_id).AppendLine();
				}
				if (this.language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", this.language_id).AppendLine();
				}

				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class boxResultName
		{

			#region オーダー変数
			public int result_id { get; set; }
			public int hit_id { get; set; }
			public int hit_type_id { get; set; }
			public int language_id { get; set; }
			public string result_name { get; set; }
			public int swing_miss_id { get; set; }
			public int fourball_id { get; set; }
			#endregion


			public boxResultName(
								int result_id = -1,
								int hit_id = -1,
								int hit_type_id = -1,
								int language_id = -1,
								string result_name = "",
								int swing_miss_id = -1,
								int fourball_id = -1
				)
			{
				this.result_id = result_id;
				this.hit_id = hit_id;
				this.hit_type_id = hit_type_id;
				this.language_id = language_id;
				this.result_name = result_name;
				this.swing_miss_id = swing_miss_id;
				this.fourball_id = fourball_id;
			}

		}
		public static List<boxResultName> GetRecords(
								int result_id = -1,
								int hit_id = -1,
								int hit_type_id = -1,
								int language_id = -1,
								int swing_miss_id = -1,
								int fourball_id = -1
			)
		{
			List<boxResultName> orderList = new List<boxResultName>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		result_id	 ,   ");
				sqlST.AppendLine("		hit_id	 ,   ");
				sqlST.AppendLine("		hit_type_id,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		result_name	,    ");
				sqlST.AppendLine("		swing_miss_id	,	    ");
				sqlST.AppendLine("		fourball_id	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		box_result_name ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (result_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	result_id={0} ", result_id).AppendLine();
				}
				if (hit_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_id={0} ", hit_id).AppendLine();
				}
				if (hit_type_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_type_id={0} ", hit_type_id).AppendLine();
				}
				if (language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", language_id).AppendLine();
				}
				if (swing_miss_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	swing_miss_id={0} ", swing_miss_id).AppendLine();
				}
				if (fourball_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	fourball_id={0} ", fourball_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new boxResultName(
							result_id: reader.GetInt32(0),
							hit_id: reader.GetInt32(1),
							hit_type_id: reader.GetInt32(2),
							language_id: reader.GetInt32(3),
							result_name: reader.GetString(4),
							swing_miss_id: reader.GetInt32(5),
							fourball_id: reader.GetInt32(6)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region ポジション名
	class PositionName
	{
		private int position_id = 0;
		private int hit_id = -1;
		private int hit_type_id = -1;
		private int language_id = -1;
		private string position_name = "";

		public PositionName()
		{
		}

		public void SetPositionId(int position_id)
		{
			this.position_id = position_id;
		}
		public void SetHitId(int hit_id)
		{
			this.hit_id = hit_id;
		}
		public void SetHitTypeId(int hit_type_id)
		{
			this.hit_type_id = hit_type_id;
		}
		public void SetLanguageId(int language_id)
		{
			this.language_id = language_id;
		}
		private void SetResultName(string position_name)
		{
			this.position_name = position_name;
		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	positions_name( ");
				sqlST.AppendLine("		position_id	,	    ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		position_name	    ");
				sqlST.AppendLine("		) ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	positions_name ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		position_id	,	    ");
				sqlST.AppendLine("		hit_id	,	    ");
				sqlST.AppendLine("		hit_type_id	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		position_name	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", position_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", hit_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", hit_type_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", language_id.ToString()).AppendLine();
				sqlST.AppendFormat("	{0} ", position_name.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	positions_name ");
				sqlST.AppendLine("SET ");
				if (this.position_name.Length > 0)
				{
					sqlST.AppendFormat("	,position_name={0} ", this.position_name).AppendLine();
				}
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (this.position_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	position_id={0} ", this.position_id).AppendLine();
				}
				if (this.hit_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_id={0} ", this.hit_id).AppendLine();
				}
				if (this.hit_type_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_type_id={0} ", this.hit_type_id).AppendLine();
				}
				if (this.language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", this.language_id).AppendLine();
				}

				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class positionName
		{

			#region オーダー変数
			public int position_id { get; set; }
			public int hit_id { get; set; }
			public int hit_type_id { get; set; }
			public int language_id { get; set; }
			public string position_name { get; set; }

			#endregion


			public positionName(
								int position_id = -1,
								int hit_id = -1,
								int hit_type_id = -1,
								int language_id = -1,
								string position_name = ""
				)
			{
				this.position_id = position_id;
				this.hit_id = hit_id;
				this.hit_type_id = hit_type_id;
				this.language_id = language_id;
				this.position_name = position_name;
			}

		}
		public static List<positionName> GetRecords(
								int position_id = -1,
								int hit_id = -1,
								int hit_type_id = -1,
								int language_id = -1
			)
		{
			List<positionName> orderList = new List<positionName>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		position_id	 ,   ");
				sqlST.AppendLine("		hit_id	 ,   ");
				sqlST.AppendLine("		hit_type_id,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		position_name	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		positions_name ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (position_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	position_id={0} ", position_id).AppendLine();
				}
				if (hit_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_id={0} ", hit_id).AppendLine();
				}
				if (hit_type_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	hit_type_id={0} ", hit_type_id).AppendLine();
				}
				if (language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", language_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new positionName(
							position_id: reader.GetInt32(0),
							hit_id: reader.GetInt32(1),
							hit_type_id: reader.GetInt32(2),
							language_id: reader.GetInt32(3),
							position_name: reader.GetString(4)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

	#region 走塁データ
	class RunnerData
	{
		private int run_id = 0;
		private int game_id = 0;
		private int team_id = 0;
		private int run_player_id = 0;
		private int ining = 0;
		private int top_btm_cd = 0;
		private int box_id = 0;
		private int count_b = 0;
		private int count_s = 0;
		private int count_o = 0;
		private int player_id = 0;
		private int pitcher_id = 0;
		private int pit_team_id = 0;
		private int base_cd = 0;
		private int from_base_cd = 0;
		private int to_base_cd = 0;
		private int run_code = 0;
		private int steal_cd = 0;
		private int earned_cd = 0;
		private int ball_id = 0;

		public RunnerData(
					int game_id,
					int top_btm_cd,
					int ining,
					int box_id,
					int ball_id,
					int run_player_id)
		{
			this.run_id = this.GetRecordsCount();
			this.game_id = game_id;
			this.top_btm_cd = top_btm_cd;
			this.ining = ining;
			this.box_id = box_id;
			this.ball_id = ball_id;
			this.run_player_id = run_player_id;
		}

		public void SetBaseCode(int base_cd) 
		{
			this.base_cd = base_cd;
		}
		public void SetStealCode(int steal_cd)
		{
			this.steal_cd = steal_cd;
		}
		public void SetBoxId(int box_id)
		{
			this.box_id = box_id;
		}
		public void SetCountOut(int count_o)
		{
			this.count_o = count_o;
		}
		public void SetCountBall(int count_b)
		{
			this.count_b = count_b;
		}
		public void SetCountStrike(int count_s)
		{
			this.count_s = count_s;
		}
		public void SetEarned(int earned_cd)
		{
			this.earned_cd = earned_cd;
		}
		public void SetRunCode(int run_code)
		{
			this.run_code = run_code;
		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	runner ( ");
				sqlST.AppendLine("		run_id , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		run_player_id   , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_btm_cd   , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		count_b   , ");
				sqlST.AppendLine("		count_s   , ");
				sqlST.AppendLine("		count_o   , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		base_cd     , ");
				sqlST.AppendLine("		from_base_cd     , ");
				sqlST.AppendLine("		to_base_cd     , ");
				sqlST.AppendLine("		run_code      ,");
				sqlST.AppendLine("		steal_cd   , ");
				sqlST.AppendLine("		earned_cd   , ");
				sqlST.AppendLine("		ball_id   , ");
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				/// 60
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}


		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	runner ( ");
				sqlST.AppendLine("		run_id , ");
				sqlST.AppendLine("		game_id    , ");
				sqlST.AppendLine("		team_id  , ");
				sqlST.AppendLine("		run_player_id   , ");
				sqlST.AppendLine("		ining      , ");
				sqlST.AppendLine("		top_btm_cd   , ");
				sqlST.AppendLine("		box_id , ");
				sqlST.AppendLine("		count_b   , ");
				sqlST.AppendLine("		count_s   , ");
				sqlST.AppendLine("		count_o   , ");
				sqlST.AppendLine("		player_id     , ");
				sqlST.AppendLine("		pitcher_id   , ");
				sqlST.AppendLine("		pit_team_id   , ");
				sqlST.AppendLine("		base_cd     , ");
				sqlST.AppendLine("		from_base_cd     , ");
				sqlST.AppendLine("		to_base_cd     , ");
				sqlST.AppendLine("		run_code      ,");
				sqlST.AppendLine("		steal_cd   , ");
				sqlST.AppendLine("		earned_cd   , ");
				sqlST.AppendLine("		ball_id   , ");
				sqlST.AppendLine("		update_date    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES( ");
				sqlST.AppendFormat("		{0}		,	", this.run_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.game_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.team_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.run_player_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0]     ,	", this.ining.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.top_btm_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.box_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.count_b.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.count_s.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.count_o.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}     ,	", this.player_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.pitcher_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.pit_team_id.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}     ,	", this.base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}     ,	", this.from_base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}     ,	", this.to_base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}     ,	", this.run_code.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.steal_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.earned_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		{0}		,	", this.ball_id.ToString()).AppendLine();
				sqlST.AppendFormat("		'{0}'		", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine(" ); ");
				CMD_Insert.CommandText = sqlST.ToString();
				CMD_Insert.ExecuteReader();
				con.Close();

			}
		}


		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				SqliteCommand CMD_Update = new SqliteCommand();
				CMD_Update.Connection = con;
				#region 選手情報更新 SQL
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("UPDATE runner ");
				sqlST.AppendLine("	SET ");
				sqlST.AppendFormat("		run_id={0}		,	", this.run_id.ToString()).AppendLine();
				//sqlST.AppendFormat("		game_id={0}		,	", this.game_id.ToString()).AppendLine();
				sqlST.AppendFormat("		team_id={0}		,	", this.team_id.ToString()).AppendLine();
				sqlST.AppendFormat("		run_player_id={0}		,	", this.run_player_id.ToString()).AppendLine();
				sqlST.AppendFormat("		ining={0]     ,	", this.ining.ToString()).AppendLine();
				sqlST.AppendFormat("		top_btm_cd={0}		,	", this.top_btm_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		box_id={0}		,	", this.box_id.ToString()).AppendLine();
				sqlST.AppendFormat("		count_b={0}		,	", this.count_b.ToString()).AppendLine();
				sqlST.AppendFormat("		count_s={0}		,	", this.count_s.ToString()).AppendLine();
				sqlST.AppendFormat("		count_o={0}		,	", this.count_o.ToString()).AppendLine();
				sqlST.AppendFormat("		player_id={0}     ,	", this.player_id.ToString()).AppendLine();
				sqlST.AppendFormat("		pitcher_id={0}		,	", this.pitcher_id.ToString()).AppendLine();
				sqlST.AppendFormat("		pit_team_id={0}		,	", this.pit_team_id.ToString()).AppendLine();
				sqlST.AppendFormat("		base_cd={0}     ,	", this.base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		from_base_cd={0}     ,	", this.from_base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		to_base_cd={0}     ,	", this.to_base_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		run_code={0}     ,	", this.run_code.ToString()).AppendLine();
				sqlST.AppendFormat("		steal_cd={0}		,	", this.steal_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		earned_cd={0}		,	", this.earned_cd.ToString()).AppendLine();
				sqlST.AppendFormat("		ball_id={0}		,	", this.ball_id.ToString()).AppendLine();
				sqlST.AppendFormat("		update_date='{0}'		", DateTime.Now.ToString()).AppendLine();
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("		game_id={0}	 ", this.game_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("		top_btm_cd={0}	 ", this.top_btm_cd).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("		run_player_id={0}	 ", this.run_player_id).AppendLine();
				sqlST.AppendLine("		AND ");
				sqlST.AppendFormat("		ining={0}	 ", this.ining).AppendLine();
				CMD_Update.CommandText = sqlST.ToString();
				#endregion

				CMD_Update.ExecuteReader();
				con.Close();
			}
		}

		public class runnerDataCount
		{
			public int run_count { get; set; }
			public runnerDataCount(int count = 0)
			{
				this.run_count = count;
			}
		}
		private int GetRecordsCount()
		{
			int result = 0;
			List<runnerDataCount> countList = new List<runnerDataCount>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	CASE ");
				sqlST.AppendLine("		WHEN ");
				sqlST.AppendLine("			MAX(run_id) is null THEN 1 ");
				sqlST.AppendLine("		ELSE ");
				sqlST.AppendLine("			MAX(run_id) + 1 ");
				sqlST.AppendLine("	END AS run_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	runner ");
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new runnerDataCount(reader.GetInt32(0)));
				}
				result = countList[0].run_count;
			}
			return result;
		}



		public class runDataRunner
		{
			public bool runner_1 { get; set; }
			public bool runner_2 { get; set; }
			public bool runner_3 { get; set; }
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }
			public runDataRunner(
								bool runner_1 = false,
								bool runner_2 = false,
								bool runner_3 = false,
								int runner_1_player_id = 0,
								int runner_2_player_id = 0,
								int runner_3_player_id = 0)
			{
				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;
			}
		}
		public static List<runDataRunner> GetRecordsRunner(int run_id = 0)
		{
			List<runDataRunner> countList = new List<runDataRunner>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	runner_1 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_2 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_3 ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_1_player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_2_player_id ");
				sqlST.AppendLine("	, ");
				sqlST.AppendLine("	runner_3_player_id ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	run ");
				sqlST.AppendLine("WHERE ");
				sqlST.AppendFormat("	run_id={0} ", run_id).AppendLine();

				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					countList.Add(new runDataRunner(
										reader.GetBoolean(0),
										reader.GetBoolean(1),
										reader.GetBoolean(2),
										reader.GetInt32(3),
										reader.GetInt32(4),
										reader.GetInt32(5)
										)
						);
				}
			}
			return countList;
		}


		public class runData
		{

			#region 走塁変数
			public int run_id { get; set; }
			public int box_id { get; set; }                 // 打席識別番号		1

			public int player_id { get; set; }              // 選手識別番号		3
			public int team_id { get; set; }                // チーム識別番号		4
			public int position { get; set; }               // 守備位置識別番号-登録ポジション(1:投手, 2:捕手, 3:一塁手,.. 9:右翼手)
			public int player_num { get; set; }             // 背番号		6

			public int pitcher_id { get; set; }             // 投手識別番号(選手識別番号と併用)		7
			public int pit_team_id { get; set; }            // 投手チーム識別番号(チーム識別番号と併用)	8
			public int ball_box_num { get; set; }           // 打席内投球数		9
			public int ball_total_num { get; set; }         // 投球数		10
			public int cat_id { get; set; }                 // 捕手識別番号(選手識別番号と併用)		11
			public int ump_id { get; set; }                 // 審判識別番号(ユニークテーブル使用)		12
			public int game_id { get; set; }                // 試合識別番号(ユニークテーブル使用)		13
			public int game_box_num { get; set; }           // 打席数(試合中の打席)		14
			public int park_id { get; set; }                // 球場識別番号
			public int bat_id { get; set; }                 // 左右打席識別番号
			public int pit_hand_id { get; set; }            // 投手左右識別番号
			public int pit_throw_id { get; set; }           // 投法識別番号
			public int weather_id { get; set; }             // 天気識別番号

			public int count_b { get; set; }                // ボールカウント		20
			public int count_s { get; set; }                // ストライクカウント
			public int count_o { get; set; }                // アウトカウント
			public bool runner_1 { get; set; }              // ファーストランナー
			public bool runner_2 { get; set; }              // セカンドランナー		25
			public bool runner_3 { get; set; }              // サードトランナー
			public int runner_1_player_id { get; set; }
			public int runner_2_player_id { get; set; }
			public int runner_3_player_id { get; set; }
			public int ining { get; set; }                  // イニング
			public bool top_bot { get; set; }               // false:0=表,true:1=裏
			public int top_score { get; set; }              // 先攻チームスコア
			public int bottom_score { get; set; }           // 後攻チームスコア		30
			public int ball_type { get; set; }         // 打席結果の球種
			public int ball_speed { get; set; }        // 打席結果の球速
			public DateTime? update_date { get; set; }      // 更新日

			public int etc_cd1 { get; set; }                // 予備		45
			public int etc_cd2 { get; set; }                // 予備
			public int etc_cd3 { get; set; }                // 予備
			public int etc_cd4 { get; set; }                // 予備
			public int etc_cd5 { get; set; }                // 予備
			public string etc_str1 { get; set; }            // 予備		50
			public string etc_str2 { get; set; }            // 予備
			public string etc_str3 { get; set; }            // 予備
			public string etc_str4 { get; set; }            // 予備
			public string etc_str5 { get; set; }            // 予備



			#endregion


			public runData(
						int run_id = 0,
						int box_id = 0,
						   int player_id = 0,
						   int team_id = 0,
						   int pitcher_id = 0,
						   int pit_team_id = 0,
						   int cat_id = 0,
						   int ump_id = 0,
						   int game_id = 0,
						   int park_id = 0,
						   int pit_hand_id = 0,
						   int pit_throw_id = 0,
						   int weather_id = 0,
						   int count_b = 0,
						   int count_s = 0,
						   int count_o = 0,
						   bool runner_1 = false,
						   bool runner_2 = false,
						   bool runner_3 = false,
						   int runner_1_player_id = 0,
						   int runner_2_player_id = 0,
						   int runner_3_player_id = 0,
						   int ining = 0,
						   bool top_bot = false,
						   int top_score = 0,
						   int bottom_score = 0,
						   int ball_type = 0,
						   int ball_speed = 0,
						   int etc_cd1 = 0,
						   int etc_cd2 = 0,
						   int etc_cd3 = 0,
						   int etc_cd4 = 0,
						   int etc_cd5 = 0,
						   string etc_str1 = "",
						   string etc_str2 = "",
						   string etc_str3 = "",
						   string etc_str4 = "",
						   string etc_str5 = "",
						   DateTime? update_date = null
							  )
			{
				this.run_id = run_id;
				this.box_id = box_id;
				this.player_id = player_id;
				this.team_id = team_id;
				this.pitcher_id = pitcher_id;
				this.pit_team_id = pit_team_id;

				this.cat_id = cat_id;
				this.ump_id = ump_id;
				this.game_id = game_id;
				this.park_id = park_id;

				this.pit_throw_id = pit_throw_id;
				this.pit_hand_id = pit_hand_id;
				this.weather_id = weather_id;
				this.ball_type = ball_type;
				this.ball_speed = ball_speed;
				this.count_b = count_b;
				this.count_s = count_s;
				this.count_o = count_o;

				this.runner_1 = runner_1;
				this.runner_2 = runner_2;
				this.runner_3 = runner_3;
				this.runner_1_player_id = runner_1_player_id;
				this.runner_2_player_id = runner_2_player_id;
				this.runner_3_player_id = runner_3_player_id;

				this.ining = ining;
				this.top_bot = top_bot;
				this.top_score = top_score;
				this.bottom_score = bottom_score;

				this.update_date = update_date;

				this.etc_cd1 = etc_cd1;             // イニング内打順
				this.etc_cd2 = etc_cd2;
				this.etc_cd3 = etc_cd3;
				this.etc_cd4 = etc_cd4;
				this.etc_cd5 = etc_cd5;

				this.etc_str1 = etc_str1;
				this.etc_str2 = etc_str2;
				this.etc_str3 = etc_str3;
				this.etc_str4 = etc_str4;
				this.etc_str5 = etc_str5;

			}

		}
		public static List<runData> GetRecords(int run_id = 0)
		{
			List<runData> orderList = new List<runData>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("	run_id	,");
				sqlST.AppendLine("	box_id	,");
				sqlST.AppendLine("	player_id	,");
				sqlST.AppendLine("	team_id	,");
				sqlST.AppendLine("	pitcher_id	,");
				sqlST.AppendLine("	pit_team_id	,");
				sqlST.AppendLine("	cat_id	,");
				sqlST.AppendLine("	ump_id	,");
				sqlST.AppendLine("	game_id	,");
				sqlST.AppendLine("	park_id	,");
				sqlST.AppendLine("	pit_hand_id	,");
				sqlST.AppendLine("	pit_throw_id	,");
				sqlST.AppendLine("	weather_id	,");
				sqlST.AppendLine("	count_b	,");
				sqlST.AppendLine("	count_s	,");
				sqlST.AppendLine("	count_o	,");
				sqlST.AppendLine("	runner_1	,");        // bool
				sqlST.AppendLine("	runner_2	,");        // bool
				sqlST.AppendLine("	runner_3	,");        // bool  
				sqlST.AppendLine("	runner_1_player_id	,");
				sqlST.AppendLine("	runner_2_player_id	,");
				sqlST.AppendLine("	runner_3_player_id	,");
				sqlST.AppendLine("	ining	,");            // bool
				sqlST.AppendLine("	top_bot	,");
				sqlST.AppendLine("	top_score	,");
				sqlST.AppendLine("	bottom_score	,");
				sqlST.AppendLine("	ball_type	,");
				sqlST.AppendLine("	ball_speed	,");

				sqlST.AppendLine("	etc_cd1	,");
				sqlST.AppendLine("	etc_cd2	,");
				sqlST.AppendLine("	etc_cd3	,");
				sqlST.AppendLine("	etc_cd4	,");
				sqlST.AppendLine("	etc_cd5	,");
				sqlST.AppendLine("	etc_str1	,");        // str
				sqlST.AppendLine("	etc_str2	,");        // str
				sqlST.AppendLine("	etc_str3	,");        // str
				sqlST.AppendLine("	etc_str4	,");        // str
				sqlST.AppendLine("	etc_str5	,");        // str
				sqlST.AppendLine("	update_date	");         // DateTime
															//sqlST.AppendLine("	* ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("	run ");
				if (run_id != 0)
				{
					sqlST.AppendLine("WHERE ");
					sqlST.AppendFormat("	run_id={0} ", run_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new runData(
							reader.GetInt32(0),
							reader.GetInt32(1),
							reader.GetInt32(2),
							reader.GetInt32(3),
							reader.GetInt32(4),
							reader.GetInt32(5),
							reader.GetInt32(6),
							reader.GetInt32(7),
							reader.GetInt32(8),
							reader.GetInt32(9),
							reader.GetInt32(10),
							reader.GetInt32(11),
							reader.GetInt32(12),
							reader.GetInt32(13),
							reader.GetInt32(14),
							reader.GetInt32(15),
							reader.GetBoolean(16),
							reader.GetBoolean(17),
							reader.GetBoolean(18),
							reader.GetInt32(19),
							reader.GetInt32(20),
							reader.GetInt32(21),
							reader.GetInt32(22),
							reader.GetBoolean(23),
							reader.GetInt32(24),
							reader.GetInt32(25),
							reader.GetInt32(26),
							reader.GetInt32(27),
							reader.GetInt32(28),
							reader.GetInt32(29),
							reader.GetInt32(30),
							reader.GetInt32(31),
							reader.GetInt32(32),
							reader.GetString(33),
							reader.GetString(34),
							reader.GetString(35),
							reader.GetString(36),
							reader.GetString(37),
							reader.GetDateTime(38)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}

	}
	#endregion

	#region 走塁結果名
	class RunResult
	{
		private int run_code = 0;
		private int language_id = 0;
		private string run_result_name = "";

		public RunResult()
		{
		}

		public void SetRunCode(int run_code)
		{
			this.run_code = run_code;
		}
		public void SetLanguageId(int language_id)
		{
			this.language_id = language_id;
		}

		public void SetRunResultName(string run_result_name)
		{
			this.run_result_name = run_result_name;
		}

		public async static void InitializeDB()
		{
			await ApplicationData.Current.LocalFolder.CreateFileAsync("userData.db", CreationCollisionOption.OpenIfExists);
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("CREATE TABLE IF NOT EXISTS ");
				sqlST.AppendLine("	run_result( ");
				sqlST.AppendLine("		run_code	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		run_result_name	    ");
				sqlST.AppendLine("		); ");
				SqliteCommand CMDcreateTable = new SqliteCommand(sqlST.ToString(), con);
				CMDcreateTable.ExecuteReader();
				con.Close();
			}
		}

		public void addRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD_Insert = new SqliteCommand();
				CMD_Insert.Connection = con;
				sqlST.AppendLine("INSERT INTO ");
				sqlST.AppendLine("	run_result ");
				sqlST.AppendLine("		( ");
				sqlST.AppendLine("		run_code	,	    ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		run_result_name	    ");
				sqlST.AppendLine("		) ");
				sqlST.AppendLine("VALUES	 ");
				sqlST.AppendLine("		( ");
				sqlST.AppendFormat("	{0}, ", this.run_code.ToString()).AppendLine();
				sqlST.AppendFormat("	{0}, ", this.language_id.ToString()).AppendLine();
				sqlST.AppendFormat("	'{0}' ", this.run_result_name.ToString()).AppendLine();
				sqlST.AppendLine("		); ");
				CMD_Insert.CommandText = sqlST.ToString();

				CMD_Insert.ExecuteReader();
				con.Close();
			}
		}

		public void updateRecord()
		{
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				SqliteCommand CMD = new SqliteCommand();
				CMD.Connection = con;
				sqlST.AppendLine("UPDATE ");
				sqlST.AppendLine("	run_result ");
				sqlST.AppendLine("SET ");
				if (this.run_result_name.Length > 0)
				{
					sqlST.AppendFormat("	,run_result_name='{0}' ", this.run_result_name).AppendLine();
				}
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (this.run_code >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	run_code={0} ", this.run_code).AppendLine();
				}
				if (this.language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", this.language_id).AppendLine();
				}

				CMD.CommandText = sqlST.ToString();
				CMD.ExecuteReader();
				con.Close();
			}
		}


		public class runResultName
		{

			#region オーダー変数
			public int run_code { get; set; }
			public int language_id { get; set; }
			public string run_result_name { get; set; }

			#endregion


			public runResultName(
								int run_code = -1,
								int language_id = -1,
								string run_result_name = ""
				)
			{
				this.run_code = run_code;
				this.language_id = language_id;
				this.run_result_name = run_result_name;
			}

		}
		public static List<runResultName> GetRecords(
								int run_code = -1,
								int language_id = -1
			)
		{
			List<runResultName> orderList = new List<runResultName>();
			string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "userData.db");
			using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
			{
				con.Open();
				StringBuilder sqlST = new StringBuilder();
				sqlST.AppendLine("SELECT ");
				sqlST.AppendLine("		run_code	 ,   ");
				sqlST.AppendLine("		language_id	,	    ");
				sqlST.AppendLine("		run_result_name	    ");
				sqlST.AppendLine("FROM ");
				sqlST.AppendLine("		run_result ");
				sqlST.AppendLine("WHERE	 ");
				sqlST.AppendLine("		1=1	 ");
				if (run_code >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	run_code={0} ", run_code).AppendLine();
				}
				if (language_id >= 0)
				{
					sqlST.AppendLine("		AND	 ");
					sqlST.AppendFormat("	language_id={0} ", language_id).AppendLine();
				}
				SqliteCommand cmd_getRec = new SqliteCommand(sqlST.ToString(), con);
				SqliteDataReader reader = cmd_getRec.ExecuteReader();
				while (reader.Read())
				{
					orderList.Add(
						new runResultName(
							run_code: reader.GetInt32(0),
							language_id: reader.GetInt32(1),
							run_result_name: reader.GetString(2)
							)
						);
				}
				con.Close();
			}
			return orderList;
		}
	}
	#endregion

}