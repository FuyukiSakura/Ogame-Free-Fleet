using System;
using System.Reflection;
using FreeFleet.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace FreeFleet
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = GamePage.Instance;
		}

		protected override void OnStart ()
		{
            // Handle when your app starts

            // Load sound for alaert
            var assembly = typeof(App).GetTypeInfo().Assembly;
		    var audioStream = assembly.GetManifestResourceStream("FreeFleet.Resources.Audio." + "alarm.mp3");
		    var player = CrossSimpleAudioPlayer.Current;
            player.Loop = true;
		    player.Load(audioStream);
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
