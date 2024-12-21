using Line98.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Line98.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private ViewModelBase _CurrentView;

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
        public ICommand ShowMenuCommand { get; set; }
        public ICommand ShowMenuStyleCommand { get; set; }
        public ICommand ShowMenuScoreNormalCommand { get; set; }
        public ICommand ShowMenuHelpCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        private void ShowStart(object obj) => CurrentView = new StartViewModel();
        private void ShowMenu(object obj) => CurrentView = new MenuViewModel();
        private void ShowMenuStyle(object obj) => CurrentView = new MenuStyleViewModel();
        private void ShowMenuScoreNormal(object obj) => CurrentView = new MenuScoreNormalViewModel();
        private void ShowMenuHelp(object obj) => CurrentView = new MenuHelpViewModel();
        private void ExitAciton(object obj)
        {
            
        }

        public MainViewModel()
        {
            ShowStartCommand = new ViewModelCommand(ShowStart);
            ShowMenuCommand = new ViewModelCommand(ShowMenu);
            ShowMenuStyleCommand = new ViewModelCommand(ShowMenuStyle);
            ShowMenuScoreNormalCommand = new ViewModelCommand(ShowMenuScoreNormal);
            ShowMenuHelpCommand = new ViewModelCommand(ShowMenuHelp);
            ExitCommand = new ViewModelCommand(ExitAciton);

            CurrentView = new StartViewModel();
        }

    }

}
