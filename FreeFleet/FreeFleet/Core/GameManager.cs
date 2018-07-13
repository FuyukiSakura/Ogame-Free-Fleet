using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FreeFleet.Model.Ogame;
using FreeFleet.ViewModels;

namespace FreeFleet.Core
{
    public class GameManager : BindableBase
    {
        private bool _isLogin;
        public ObservableCollection<EventFleet> EventFleets { get; } = new ObservableCollection<EventFleet>();

        #region Auto Properties

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
