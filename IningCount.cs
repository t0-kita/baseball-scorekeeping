using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSS
{
	/// <summary>
	/// イニングやアウトカウントを管理するクラス
	/// </summary>
	class IningCount
	{
		private int OutCount;
		private int PlayIningCount;
		private bool Iningside;
		private int OrderCount;
		

		public void PlayBall() 
		{
			this.OutCount = 0;
			this.PlayIningCount = 1;
			this.Iningside = true;
			this.OrderCount = 1;
		}

		/// <summary>
		/// 打席終了後に次の打順へカウントを追加する
		/// </summary>
		public void InPlayActionCount() 
		{
			this.OrderCount++;
			if (this.OrderCount > 9) 
			{
				this.OrderCount = 1;
			}
		}

		/// <summary>
		/// アウトカウントを追加する
		/// スリーアウトで攻守交代をする
		/// </summary>
		public void OutCountPlus() 
		{
			this.OutCount ++;
			if(this.OutCount > 2) 
			{
				IningSideChange();
			}
		}

		/// <summary>
		/// 攻守交代をする
		/// 裏の攻撃が終われば次のイニングへと移る
		/// </summary>
		public void IningSideChange() 
		{
			if (!this.Iningside) 
			{
				NextIning();
				this.Iningside = true;
				return;
			}
			this.Iningside = false;
			
		}

		/// <summary>
		/// 次のイニングへと移る
		/// アウトカウントはリセットする
		/// </summary>
		public void NextIning() 
		{
			this.OutCount = 0;
			this.PlayIningCount++;
		}
	}
	

}
