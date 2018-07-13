using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.Model.Ogame;
using FreeFleet.Services.Web;
using FreeFleet.ViewModels.Modal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
	}
}