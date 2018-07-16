using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.Core;
using FreeFleet.Resources.Localization.General;
using FreeFleet.Resources.Localization.Layout;
using Xamarin.Forms;

namespace FreeFleet.ViewModels.Home
{
    public class GamePageViewModel : PageViewModelBase
    {
        private string _mainUrl;
        private bool _isMenuOpen;
        private bool _showLoginBtn;

        public GameManager GameManager { get; } = new GameManager();

        internal override Task InitializeAsync(object param = null)
        {
            // Control bar settings
            Title = SharedResources.AppName;
            AppBarLeftButtonCommand = new Command(ToggleControl);
            return base.InitializeAsync(param);
        }

        #region Sidebar

        /// <summary>
        /// Toggle control bar
        /// </summary>
        public void ToggleControl()
        {
            IsMenuOpen = !IsMenuOpen;
        }

        #endregion

        #region Auto Properties

        public string MainUrl
        {
            get => _mainUrl;
            set
            {
                _mainUrl = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the menu drawer open status
        /// </summary>
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets whether to show the login button
        /// </summary>
        public bool ShowLoginBtn
        {
            get => _showLoginBtn;
            set
            {
                _showLoginBtn = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Layout

        #region DataGrid

        public string CoordsOriginColumnHeader { get; } = GamePageResources.CoordsOriginColumnHeader;
        public string CoordsDestColumnHeader { get; } = GamePageResources.CoordsDestColumnHeader;
        public string DetailsFleetColumnHeader { get; } = GamePageResources.DetailsFleetColumnHeader;
        public string MissionTypeColumnHeader { get; } = GamePageResources.MissionTypeColumnHeader;
        public string StopAlarmButtonLabel { get; } = GamePageResources.StopAlarmButton;

        #endregion

        #region Menu

        public string ShowLogButton { get; } = GamePageResources.MenuShowLogButton;
        public string SettingsButton { get; } = GamePageResources.MenuSettingsButton;

        #endregion

        #endregion
    }
}
