using System;
using System.Collections.Generic;
using System.Text;
using FreeFleet.Core;

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
    }
}
