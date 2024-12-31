﻿using Line98.View;
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
        public ICommand ShowNewGameCommand { get; set; }
        public ICommand ShowMenuCommand { get; set; }
        public ICommand ShowMenuStyleCommand { get; set; }
        public ICommand ShowMenuScoreNormalCommand { get; set; }
        public ICommand ShowMenuHelpCommand { get; set; }
        public ICommand ShowLoadGameCommand { get; set; }
        public ICommand ShowInGameCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        private void ShowStart(object obj) => CurrentView = new StartViewModel();
        private void ShowNewGame(object obj) => CurrentView = new NewGameViewModel();
        private void ShowMenu(object obj) => CurrentView = new MenuViewModel();
        private void ShowMenuStyle(object obj) => CurrentView = new MenuStyleViewModel();
        private void ShowMenuScoreNormal(object obj) => CurrentView = new MenuScoreNormalViewModel();
        private void ShowMenuHelp(object obj) => CurrentView = new MenuMusicViewModel();
        private void ShowLoadGame(object obj) => CurrentView = new LoadGameViewModel();
        private void ShowInGame(object obj) => CurrentView = new InGameViewModel();
        private void ExitAciton(object obj)
        {
            
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

            CurrentView = new StartViewModel();
        }

    }

}
