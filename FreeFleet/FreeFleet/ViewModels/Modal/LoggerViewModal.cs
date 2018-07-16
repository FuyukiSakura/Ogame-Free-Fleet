using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FreeFleet.Core;
using Xamarin.Forms;

namespace FreeFleet.ViewModels.Modal
{
    public class LoggerViewModal : PageViewModelBase
    {
        public LoggerViewModal()
        {
            Title = "Logs";
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

        public ObservableCollection<string> Logs => Logger.Logs;
    }
}
