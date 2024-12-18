using Line98.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand ShowMenuStyleCommand { get; set; }
        public ICommand ShowMenuScoreNormalCommand { get; set; }
        public ICommand ShowMenuHelpCommand { get; set; }

        private void ShowMenuStyle(object obj) => CurrentView = new MenuStyleViewModel();
        private void ShowMenuScoreNormal(object obj) => CurrentView = new MenuScoreNormalViewModel();
        private void ShowMenuHelp(object obj) => CurrentView = new MenuHelpViewModel();

        public MainViewModel()
        {
            ShowMenuStyleCommand = new ViewModelCommand(ShowMenuStyle);
            ShowMenuScoreNormalCommand = new ViewModelCommand(ShowMenuScoreNormal);
            ShowMenuHelpCommand = new ViewModelCommand(ShowMenuHelp);

            CurrentView = new MenuStyleViewModel();
        }

    }

}
