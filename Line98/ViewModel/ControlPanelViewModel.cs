using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line98.ViewModel
{
    public class ControlPanelViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public MenuItemViewModel MenuItemViewModel { get; set; }
        public LoadGameViewModel LoadGameViewModel { get; set; }
        public ControlPanelViewModel()
        {
            MainViewModel = new MainViewModel();
            MenuItemViewModel = new MenuItemViewModel();
            LoadGameViewModel = new LoadGameViewModel();
        }
    }
}
