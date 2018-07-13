using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreeFleet.Resources;
using FreeFleet.Services.Web;
using FreeFleet.ViewModels.Home;
using FreeFleet.Views.Modal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeFleet.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
        // Singleton
	    public static GamePage Instance = new GamePage();
        
	    private GamePageViewModel _vm;

		public GamePage ()
		{
			InitializeComponent ();
		    _vm = (GamePageViewModel) BindingContext;
		    GameView.Source = UriList.LandingUrl;
        }

        /// <summary>
        /// Make GameView navigate to the given URL
        /// </summary>
        /// <param name="url"></param>
	    public void GameViewNavigateTo(string url)
        {
            GameView.Source = url;
        }

	    #region Buttons

	    /// <summary>
	    /// Handles refresh button on click
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void RefreshBtn_OnClick(object sender, EventArgs e)
	    {
	        GameView.Source = (GameView.Source as UrlWebViewSource).Url;
	    }

	    /// <summary>
	    /// Handles back button on clicked
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void NavigateBackBtn_OnClick(object sender, EventArgs e)
	    {
	        if (GameView.CanGoBack)
	        {
	            GameView.GoBack();
	        }
	    }

	    /// <summary>
	    /// Handles forward button on click
	    /// </summary>
	    /// <param name="sender"></param>
	    /// <param name="e"></param>
	    private void NavigateForwardBtn_OnClick(object sender, EventArgs e)
	    {
	        if (GameView.CanGoForward)
	        {
	            GameView.GoForward();
	        }
	    }

	    #endregion

        #region Browser Handlers

        /// <summary>
        /// Handles GameView OnNavigating events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameView_OnNavigating(object sender, WebNavigatingEventArgs e)
	    {
	        _vm.MainUrl = e.Url;
	    }

        /// <summary>
        /// Handles GameView OnNavigated events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GameView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var uri = new Uri(e.Url);

            // Check if in Lobby
	        if (uri.Host == UriList.OgameLobbyHost)
	        {
	            // if in lobby, get accounts
	            await Navigation.PushModalAsync(new AccountSelectionModal());
	        }

            // Check if in Game
            var r = new Regex(@"s\d+-[a-z]+.ogame.gameforge.com");
            _vm.GameManager.IsLogin = r.Match(uri.Host).Success;
        }

	    #endregion
	}
}