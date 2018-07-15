using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.Model.Ogame;
using FreeFleet.Resources.Localization.General;
using FreeFleet.Services.Web;
using FreeFleet.ViewModels.Modal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ErrorMessage = FreeFleet.Resources.Localization.General.ErrorMessageResources;
using PopupMessage = FreeFleet.Resources.Localization.General.PopupMessageResources;

namespace FreeFleet.Views.Modal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountSelectionModal : ContentPage
	{
	    private AccountSelectionViewModel _vm;

		public AccountSelectionModal ()
		{
			InitializeComponent ();
		    _vm = (AccountSelectionViewModel) BindingContext;
		}

        #region Buttons

        private async void LoginBtn_OnClicked(object sender, EventArgs e)
	    {
	        if (AccountList.SelectedItem == null)
	        {
                // No account selected
	            await DisplayWarning(ErrorMessage.LoginNoAccountSelected);
	        }
            
            // Login to account
	        var account = (ServerAccount) AccountList.SelectedItem;
	        try
	        {
	            await GamePage.Instance.GameManger.Login(account);
	            await Navigation.PopModalAsync();
	        }
	        catch (WebException)
	        {
	            await DisplayWarning(ErrorMessage.LoginFailed);
	        }
	    }

	    private async void CancelBtl_OnClicked(object sender, EventArgs e)
	    {
            // TODO: Show login instruction and Confirm
	        await Navigation.PopModalAsync();
        }

        #endregion

        #region Shared Functions

        /// <summary>
        /// Display a popup with pre-inserted Warning title and OK button
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Task DisplayWarning(string msg)
	    {
	        return DisplayAlert(PopupMessage.TitleWarning, msg, SharedResources.ButtonOk);
	    }

	    #endregion

        #region Overrides

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        try
	        {
	            var accounts = await DependencyService.Get<IHttpService>().GetAccountsAsync();
	            foreach (var account in accounts)
	            {
	                _vm.Accounts.Add(account);
	            }
	        }
	        catch (WebException) { }
	    }

        #endregion

	}
}