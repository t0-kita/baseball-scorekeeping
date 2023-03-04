using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
	public sealed partial class SettingsPage : Page
	{
		public SettingsPage()
		{
			this.InitializeComponent();
            BallTypeInitialize();
			LanguageInitialize();


		}

        private void BallTypeInitialize() 
        {
            BallTypeListView.ItemsSource= BallType.GetRecordsBallType();

			/// balltype.ball_img
			NewBallTypeImgCode.ItemsSource = BallType.GetRecordsBallTypeCode();

			//BallTypeImgCombobox.ItemsSource = BallType.GetRecordsBallTypeCode();

			for (int i = 0; i <= 8; i++) 
			{
				BallTypeImgDisp(i);
			}
			

		}

		private void LanguageInitialize() 
		{
			LanguageComboBox.ItemsSource = languages;
			LanguageComboBox.SelectedItem = languages.Where(p => p.selected == 1).First();
		}

		private int ball_id_create = -1;
		List<BallType.ballType> ballTypes = BallType.GetRecordsBallType();

		List<UseLanguage.language> languages = UseLanguage.GetRecords();

		/// <summary>
		/// 球種を新規追加する際の実行関数
		/// </summary>
		/// <param name="ball_img"></param>
		private void BallTypeAdd(int ball_img=0) 
		{

			int count = BallType.GetRecordsBallTypeIdCount()[0].ball_type_count;
			int order = BallType.GetRecordsBallTypeIdCount(flg:true)[0].ball_type_count;

			switch (ball_img) 
			{
				case 0:
				case 3:
				case 6:
				case 7:
				case 8:
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 0, ball_img: ball_img);
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 1, ball_img: ball_img);
					break;
				case 1:
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 0, ball_img: ball_img);
					ball_img = 5;
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 1, ball_img: ball_img);
					break;
				case 2:
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 0, ball_img: ball_img);
					ball_img = 4;
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 1, ball_img: ball_img);
					break;
				case 4:
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 0, ball_img: ball_img);
					ball_img = 2;
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 1, ball_img: ball_img);
					break;
				case 5:
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 0, ball_img: ball_img);
					ball_img = 1;
					BallType.addRecord(id: count + 1, ball_order: order + 1, type: NewBallTypeNameCreate.Text, hand: 1, ball_img: ball_img);
					break;
			}

			BallTypeInitialize();


		}


		private int BallImageIntChange(int ball_img,int hand) 
		{
			switch (ball_img)
			{
				case 0:
				case 3:
				case 6:
				case 7:
				case 8:
					return ball_img;
				case 1:
					if (hand == 0) 
					{ return 1; }
					else
					{ return 5; }
				case 2:
					if (hand == 0)
					{ return 2; }
					else
					{ return 4; }
				case 4:
					if (hand == 0)
					{ return 4; }
					else
					{ return 2; }
				case 5:
					if (hand == 0)
					{ return 5; }
					else
					{ return 1; }
				default:
					return -1;
			}
		}


        private void BallTypeImgDisp(int ball_img,object color=null) 
        {
			var poly = new Polygon();
			var point = new PointCollection();
			poly.Fill = new SolidColorBrush((color is Color) ? (Color)color : Colors.SteelBlue);
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
					BallImgGrid_0.Children.Add(poly);
					break;
				case 1:
					point.Add(new Point(17, -8));
					point.Add(new Point(17, 8));
					point.Add(new Point(-1, 0));
					poly.Points = point;
					BallImgGrid_1.Children.Add(poly);
					break;
				case 2:
					point.Add(new Point(7, -10));
					point.Add(new Point(18, 1));
					point.Add(new Point(-1, 9));
					poly.Points = point;
					BallImgGrid_2.Children.Add(poly);
					break;
				case 3:
					point.Add(new Point(-1, -9));
					point.Add(new Point(17, -9));
					point.Add(new Point(8, 9));

					poly.Points = point;
					BallImgGrid_3.Children.Add(poly);
					break;
				case 4:
					point.Add(new Point(-2, 1));
					point.Add(new Point(7, -10));
					point.Add(new Point(17, 9));

					poly.Points = point;
					BallImgGrid_4.Children.Add(poly);
					break;
				case 5:
					point.Add(new Point(0, -8));
					point.Add(new Point(0, 8));
					point.Add(new Point(18, 0));
					poly.Points = point;
					BallImgGrid_5.Children.Add(poly);
					break;
				case 6:
					point.Add(new Point(-4, -8));
					point.Add(new Point(12, -8));
					point.Add(new Point(12, 8));
					point.Add(new Point(-4, 8));
					poly.Points = point;
					BallImgGrid_6.Children.Add(poly);
					break;
				case 7:
					point.Add(new Point(10, -10));
					point.Add(new Point(20, 0));
					point.Add(new Point(10, 10));
					point.Add(new Point(0, 0));
					poly.Points = point;
					BallImgGrid_7.Children.Add(poly);
					break;
				case 8:
					point.Add(new Point(-4, -9));
					point.Add(new Point(14, -9));
					point.Add(new Point(14, 0));
					point.Add(new Point(5, 9));
					point.Add(new Point(-4, 0));

					poly.Points = point;
					BallImgGrid_8.Children.Add(poly);
					break;
				default:
					break;

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
                case "Order":
                    Frame.Navigate(typeof(OrderPage));
                    //Frame.Navigate(typeof(GameOrders));
                    break;
                case "Score":
                    Frame.Navigate(typeof(ScorePage));
                    break;
                case "Data":
                    //Frame.Navigate(typeof(DataPage));
                    Frame.Navigate(typeof(DataAnalysisPage));
                    break;
                case "Opt":
                    Frame.Navigate(typeof(GameOptions));
                    break;
                case "Team":
                    Frame.Navigate(typeof(GameMatchTeamSelect));
                    break;
            }
        }

		private void BallTypeAddButton_Tapped(object sender, TappedRoutedEventArgs e)
		{

		}

		private void BallTypeAddButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void NewBallTypeImgCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox combo = (ComboBox)sender;
			ball_id_create = combo.SelectedIndex;
			if (NewBallTypeNameCreate.Text != "" && ball_id_create > -1)
			{
				NewBallTypeCreate_btn.IsEnabled = true;
			}
			else
			{
				NewBallTypeCreate_btn.IsEnabled = false;
			}

		}

		private void NewBallTypeNameCreate_TextChanging(object sender, TextChangedEventArgs e)
		{
			if (NewBallTypeNameCreate.Text != "" && ball_id_create > -1)
			{
				NewBallTypeCreate_btn.IsEnabled = true;
			}
			else
			{
				NewBallTypeCreate_btn.IsEnabled = false;
			}

		}


		private void NewBallTypeCreate_btn_Clicked(object sender, RoutedEventArgs e)
		{
			if (ball_id_create < 0) { return; }
			if (NewBallTypeNameCreate.Text == "") { return; }

			BallTypeAdd(ball_id_create + 1);

			BallTypeListView.ItemsSource = BallType.GetRecordsBallType();
			ballTypes = BallType.GetRecordsBallType();
			NewBallTypeNameCreate.Text = "";
			ball_id_create = -1;

		}

		private void BallTypeFix_btn_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
		}

		private void BallTypeListView_Tapped(object sender, TappedRoutedEventArgs e)
		{
			ListView listView = (ListView)sender;
			
			object[] list_array = listView.Items.ToArray();
			BallType.ballType item = (BallType.ballType)list_array[listView.SelectedIndex];
			BallType.ballType ballType = ballTypes[listView.SelectedIndex];
			int tmp_ball_img = BallImageIntChange(ball_img:ballType.ball_img,hand:1);
			if (tmp_ball_img < 0) { return; }
			BallType.updateRecord(
						before_id: ballType.ball_type_id,
						before_type: ballType.ball_type,
						before_order: ballType.ball_order,
						before_img: ballType.ball_img,
						after_id: item.ball_type_id,
						after_type: item.ball_type,
						after_order: item.ball_order,
						after_img: item.ball_img,
						hand:0);
			BallType.updateRecord(
						before_id: ballType.ball_type_id,
						before_type: ballType.ball_type,
						before_order: ballType.ball_order,
						before_img: ballType.ball_img,
						after_id: item.ball_type_id,
						after_type: item.ball_type,
						after_order: item.ball_order,
						after_img: tmp_ball_img,
						hand: 1);
			BallTypeListView.ItemsSource = BallType.GetRecordsBallType();
			ballTypes = BallType.GetRecordsBallType();
		}

		private void BallTypeImg_GotFocus(object sender, RoutedEventArgs e)
		{
			
		}

		private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			int selected_language_id = languages[comboBox.SelectedIndex].id;
			UseLanguage.updateRecord(id : selected_language_id);
		}
	}
}
