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
using Windows.Web.Http.Filters;
using FreeFleet.Resources;
using FreeFleet.UWP.Services.Web;
using FreeFleet.UWP.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FreeFleet.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private GamePageViewModel _vm;

        public GamePage()
        {
            this.InitializeComponent();
            _vm = (GamePageViewModel) DataContext;
        }

        #region Buttons

        /// <summary>
        /// Handles refresh button on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            GameBrowser.Refresh();
        }

        /// <summary>
        /// Handles back button on clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (GameBrowser.CanGoBack)
            {
                GameBrowser.GoBack();
            }
        }

        /// <summary>
        /// Handles forward button on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateForwardBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (GameBrowser.CanGoForward)
            {
                GameBrowser.GoForward();
            }
        }

        #endregion


        #region Browser Handlers

        /// <summary>
        /// Handles when browser starts navigating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GameBrowser_OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            _vm.MainUrl = args.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Handles browser navigation completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void GameBrowser_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.Uri.Host == UriList.OgameLobbyHost)
            {
                // if in lobby, get accounts
                Console.WriteLine("In lobby...");
                var httpService = new HttpService();
                var uri = new Uri(UriList.OgameAccountList);
                var container = httpService.GetCookies(uri);

                var response = await httpService.GetResponseAsync(uri.AbsoluteUri, container);
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var accounts = await reader.ReadToEndAsync();
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Handles when the page become active
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            // Load URI after the page is loaded
            GameBrowser.Source = new Uri(UriList.LandingUrl);
        }

        #endregion
    }
}
