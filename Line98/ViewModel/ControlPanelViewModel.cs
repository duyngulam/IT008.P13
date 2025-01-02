using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Line98.ViewModel
{
    public class ControlPanelViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public MenuItemViewModel MenuItemViewModel { get; set; }
        public MenuMusicViewModel MenuMusicViewModel { get; set; }
        public LoadGameViewModel LoadGameViewModel { get; set; }
        public NewGameViewModel NewGameViewModel { get; set; }
        public InGameViewModel InGameViewModel { get; set; }
        public ControlPanelViewModel()
        {
            MainViewModel = new MainViewModel();
            MenuItemViewModel = new MenuItemViewModel();
            MenuMusicViewModel = new MenuMusicViewModel();
            LoadGameViewModel = new LoadGameViewModel();
            NewGameViewModel = new NewGameViewModel();
            InGameViewModel = new InGameViewModel();
        }
    }
}
