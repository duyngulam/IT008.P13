using System.Windows;
using System.Windows.Input;

namespace Line98.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _CurrentView;
        public event Action<string> ViewChanged;

        private void OnViewChanged(string viewName)
        {
            ViewChanged?.Invoke(viewName);
        }
        public ViewModelBase CurrentView
        {
            get => _CurrentView;

            set
            {
                _CurrentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        //command
        public ICommand ShowStartCommand { get; set; }
        public ICommand ShowNewGameCommand { get; set; }
        public ICommand ShowMenuCommand { get; set; }
        public ICommand ShowMenuStyleCommand { get; set; }
        public ICommand ShowMenuScoreNormalCommand { get; set; }
        public ICommand ShowMenuHelpCommand { get; set; }
        public ICommand ShowLoadGameCommand { get; set; }
        public ICommand ShowInGameCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand LoadbtnCommand { get; set; }
        private void ShowStart(object obj)
        {
            if (!GameState.Instance.IsPlaying)
                CurrentView = new StartViewModel();
            else

                CurrentView = new InGameViewModel();
            OnViewChanged(nameof(CurrentView));
        }
        private void BackLoad(object obj)
        {
            if (!GameState.Instance.IsPlaying)
                return;
            else
                CurrentView = new InGameViewModel();
            OnViewChanged(nameof(CurrentView));
        }
        private void ShowNewGame(object obj) => CurrentView = new NewGameViewModel();
        private void ShowMenu(object obj) => CurrentView = new MenuViewModel();
        private void ShowMenuStyle(object obj) => CurrentView = new MenuStyleViewModel();
        private void ShowMenuScoreNormal(object obj) => CurrentView = new MenuScoreNormalViewModel();
        private void ShowMenuHelp(object obj) => CurrentView = new MenuMusicViewModel();
        private void ShowLoadGame(object obj) => CurrentView = new LoadGameViewModel();
        private void ShowInGame(object obj) => CurrentView = new InGameViewModel();
        private void ExitAciton(object obj)
        {
            var result = MessageBox.Show(
        "Are you sure you want to exit the game?",
        "Confirmation",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                CurrentView = new StartViewModel();
                GameState.Instance.IsPlaying = false;
            }
        }

        public MainViewModel()
        {
            ShowStartCommand = new ViewModelCommand(ShowStart);
            ShowNewGameCommand = new ViewModelCommand(ShowNewGame);
            ShowMenuCommand = new ViewModelCommand(ShowMenu);
            ShowMenuStyleCommand = new ViewModelCommand(ShowMenuStyle);
            ShowMenuScoreNormalCommand = new ViewModelCommand(ShowMenuScoreNormal);
            ShowMenuHelpCommand = new ViewModelCommand(ShowMenuHelp);
            ShowLoadGameCommand = new ViewModelCommand(ShowLoadGame);
            ShowInGameCommand = new ViewModelCommand(ShowInGame);
            ExitCommand = new ViewModelCommand(ExitAciton);
            LoadbtnCommand = new ViewModelCommand(BackLoad);

            CurrentView = new StartViewModel();
        }

    }

}
