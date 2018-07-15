using System;
using System.Collections.Generic;
using System.Text;
using FreeFleet.Core;
using FreeFleet.Views;
using Xamarin.Forms;
using SettingsModal = FreeFleet.Resources.Localization.Layout.SettingsModalResources;

namespace FreeFleet.ViewModels.Modal
{
    public class SettingsViewModel : PageViewModelBase
    {
        public GameManager GameManager => GamePage.Instance.GameManger;

        public SettingsViewModel()
        {
            Title = SettingsModal.Title;
            AppBarLeftButtonCommand = new Command(NavigateBackAsync);
        }

        /// <summary>
        /// Leave setting
        /// </summary>
        public async void NavigateBackAsync()
        {
            if (Application.Current.MainPage is ContentPage page)
            {
                await page.Navigation.PopModalAsync();
            }
        }

        #region Layout

        public string TimerIntervalLabel { get; } = SettingsModal.TimerIntervalLabel;
        public string RandomUpToLabel { get; } = SettingsModal.RandomUpToLabel;
        public string AutoLoginLabel { get; } = SettingsModal.AutoLoginLabel;

        #endregion
    }
}
