using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.ViewModels.Modal;
using Plugin.SimpleAudioPlayer;
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

        /// <summary>
        /// Handles Test alarm button on clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
	    private void TestAlarmBtn_OnClicked(object sender, EventArgs e)
	    {
            var player = CrossSimpleAudioPlayer.Current;
	        if (player.IsPlaying)
	        {
                // Player is playing, stop
	            player.Stop();
            }
	        else
	        {
                // Start player
	            player.Play();
	        }
	    }
	}
}