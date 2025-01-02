using System.Windows.Input;

namespace Line98.ViewModel
{
    public class MenuItemViewModel : ViewModelBase
    {
        private object _menuCurrentView;
        public object MenuCurrentView
        {
            get { return _menuCurrentView; }
            set
            {
                _menuCurrentView = value;
                OnPropertyChanged(nameof(MenuCurrentView));
            }
        }
        public ICommand ShowMenuStyleCommand { get; set; }
        public ICommand ShowMenuScoreNormalCommand { get; set; }
        public ICommand ShowMenuMusicCommand { get; set; }
        private void ShowMenuStyle(object obj) => MenuCurrentView = new MenuStyleViewModel();
        private void ShowMenuScoreNormal(object obj) => MenuCurrentView = new MenuScoreNormalViewModel();
        private void ShowMenuMusic(object obj) => MenuCurrentView = new MenuMusicViewModel();
        public MenuItemViewModel()
        {
            ShowMenuStyleCommand = new ViewModelCommand(ShowMenuStyle);
            ShowMenuScoreNormalCommand = new ViewModelCommand(ShowMenuScoreNormal);
            ShowMenuMusicCommand = new ViewModelCommand(ShowMenuMusic);

            MenuCurrentView = new MenuStyleViewModel();
        }
    }

}
