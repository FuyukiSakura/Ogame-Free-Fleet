using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.ViewModels.Modal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FreeFleet.Views.Modal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsModal : ContentPage
	{
	    private SettingsViewModel _vm;

		public SettingsModal ()
		{
			InitializeComponent ();
		    _vm = (SettingsViewModel) BindingContext;
		}
	}
}