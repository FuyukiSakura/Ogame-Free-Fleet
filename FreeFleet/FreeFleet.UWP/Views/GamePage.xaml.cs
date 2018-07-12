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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FreeFleet.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles browser navigation complated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GameBrowser_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.Uri.Host == "lobby.ogame.gameforge.com")
            {
                // if in lobby, get accounts
            }
        }
    }
}
