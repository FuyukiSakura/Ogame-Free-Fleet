using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FreeFleet.Model.Ogame;
using FreeFleet.Resources.Localization.General;
using AccountSelectionModal = FreeFleet.Resources.Localization.Layout.AccountSelectionModalResources;

namespace FreeFleet.ViewModels.Modal
{
    public class AccountSelectionViewModel
    {
        public ObservableCollection<ServerAccount> Accounts { get; } = new ObservableCollection<ServerAccount>();

        #region Layout

        public string Title { get; } = AccountSelectionModal.Title;
        public string LoginButtonText { get; } = AccountSelectionModal.ButtonLogin;
        public string CancelButtonText { get; } = SharedResources.ButtonCancel;

        #endregion
    }
}
