using System;
using System.Collections.Generic;
using System.Text;
using FreeFleet.Core;
using FreeFleet.Resources.Localization.Layout;

namespace FreeFleet.ViewModels.Home
{
    public class GamePageViewModel : BindableBase
    {
        private string _mainUrl;
        public GameManager GameManager { get; } = new GameManager();

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

        #endregion

        #region Layout

        #region DataGrid

        public string CoordsOriginColumnHeader { get; } = GamePageResources.CoordsOriginColumnHeader;
        public string CoordsDestColumnHeader { get; } = GamePageResources.CoordsDestColumnHeader;
        public string DetailsFleetColumnHeader { get; } = GamePageResources.DetailsFleetColumnHeader;
        public string MissionTypeColumnHeader { get; } = GamePageResources.MissionTypeColumnHeader;

        #endregion

        #endregion
    }
}
