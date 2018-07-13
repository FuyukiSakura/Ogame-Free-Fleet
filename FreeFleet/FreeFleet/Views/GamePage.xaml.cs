using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	    private GamePageViewModel _vm;

		public GamePage ()
		{
			InitializeComponent ();
		    _vm = (GamePageViewModel) BindingContext;
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

        private void GameView_OnNavigating(object sender, WebNavigatingEventArgs e)
	    {
	        _vm.MainUrl = e.Url;
	    }

        private async void GameView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var uri = new Uri(e.Url);
	        if (uri.Host == UriList.OgameLobbyHost)
	        {
	            // if in lobby, get accounts
	            await Navigation.PushModalAsync(new AccountSelectionModal());
	        }
        }

	    #endregion

	    #region Overrides

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

            // Load URI after the page is loaded
	        GameView.Source = new Uri(UriList.LandingUrl);
        }

	    #endregion
	}
}