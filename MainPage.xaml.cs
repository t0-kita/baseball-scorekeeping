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

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace MMSS
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            BallData.InitializeDB();
            BallAction.InitializeDB();
            BallCollor.InitializeDB();
            BallCourse.InitializeDB();
            BallPark.InitializeDB();
            BallType.InitializeDB();
            PlayerData.InitializeDB();
            PlayOrder.InitializeDB();
            Weather.InitializeDB();
            Umpire.InitializeDB();
            Prefectures.InitializeDB();
            TeamData.InitializeDB();
            DefensiveData.InitializeDB();
            TmpGameData.InitializeDB();
            RunData.InitializeDB();
            UseLanguage.InitializeDB();
            UseDisplay.InitializeDB();
            BoxData.InitializeDB();
            BoxFieldDir.InitializeDB();
            BoxResult.InitializeDB();
            HitType.InitializeDB();
            GameData.InitializeDB();
            BoxResultName.InitializeDB();
            PositionName.InitializeDB();
            PitchingResult.InitializeDB();
            BattingResult.InitializeDB();
            RunnerData.InitializeDB();
            RunResult.InitializeDB();
			
        }


        /// <summary>
        /// Move Pages by NavigationView clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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

        

        /// <summary>
        /// Determin the destination by tag name
        /// </summary>
        /// <param name="item"></param>
        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag) 
            {
                case "Score":
                    Frame.Navigate(typeof(ScorePage));
                    break;
                case "Data":
                    //Frame.Navigate(typeof(DataPage));
                    Frame.Navigate(typeof(DataAnalysisPage));
                    break;
            }
        }

		private void PlayBall_btn_Clicked(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(GameOptions));
        }

		private void DataAnalysisPage_Button_Click(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(DataAnalysisPage));
        }

		private void Navigatation_Click(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(GameResultPage));
        }

		private void SettingPlayerDataPageNavi_Button_Click(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(SettingPlayerDataPage));
        }

		private void GameResult_Button_Clicked(object sender, RoutedEventArgs e)
		{
            Frame.Navigate(typeof(GameResultPage));
        }
	}
}
