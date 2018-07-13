using System;
using System.Collections.Generic;
using System.Text;

namespace FreeFleet.ViewModels.Home
{
    public class GamePageViewModel : BindableBase
    {
        private string _mainUrl;

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
