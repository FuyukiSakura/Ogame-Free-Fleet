using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.ViewModels;

namespace FreeFleet.UWP.ViewModels
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
