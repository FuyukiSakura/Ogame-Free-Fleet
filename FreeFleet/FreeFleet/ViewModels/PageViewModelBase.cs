using System.Threading.Tasks;
using System.Windows.Input;

namespace FreeFleet.ViewModels
{
    public abstract class PageViewModelBase : BindableBase
    {
        private string _title;
        private string _titleIcon;
        private ICommand _appBarLeftButtonCommand;
        private ICommand _appBarMiddleButtonCommand;
        private ICommand _appBarRightButtonCommand;

        /// <summary>
        /// Initialize view model
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        internal virtual Task InitializeAsync(object param = null)
        {
            return Task.FromResult(false);
        }

        public string Title
        {
            get => _title;
            protected set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public string TitleIcon
        {
            get => _titleIcon;
            protected set
            {
                if (_titleIcon == value) return;
                _titleIcon = value;
                OnPropertyChanged();
            }
        }

        public ICommand AppBarLeftButtonCommand
        {
            get => _appBarLeftButtonCommand;
            protected set
            {
                if (_appBarLeftButtonCommand == value) return;
                _appBarLeftButtonCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand AppBarMiddleButtonCommand
        {
            get => _appBarMiddleButtonCommand;
            protected set
            {
                if (_appBarMiddleButtonCommand == value) return;
                _appBarMiddleButtonCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand AppBarRightButtonCommand
        {
            get => _appBarRightButtonCommand;
            protected set
            {
                if (_appBarRightButtonCommand == value) return;
                _appBarRightButtonCommand = value;
                OnPropertyChanged();
            }
        }
    }
}