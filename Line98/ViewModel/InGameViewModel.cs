using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Line98.ViewModel
{
    public class InGameViewModel : ViewModelBase
    {
        private bool _isVolumeChecked;
        public bool IsVolumeChecked
        {
            get => _isVolumeChecked;
            set
            {
                _isVolumeChecked = value;
                OnPropertyChanged(nameof(IsVolumeChecked));
            }
        }
        public ICommand VolumeCommand { get; set; }
        public InGameViewModel()
        {
            VolumeCommand = new ViewModelCommand(Volume);
            IsVolumeChecked = true;
            var mainVM = Application.Current.Resources["MainViewModel"] as MainViewModel;
            if (mainVM != null)
            {
                mainVM.ViewChanged += OnViewChanged;
            }
        }

        private void OnViewChanged(string viewName)
        {
            if (viewName == nameof(InGameViewModel))
            {

            }
        }


        private void Volume(object obj)
        {
            if (IsVolumeChecked)
            {
                IsVolumeChecked = false;
            }
            else
            {
                IsVolumeChecked = true;
            }
        }
    }
}

~InGameViewModel()
        {
    var mainVM = Application.Current.Resources["MainViewModel"] as MainViewModel;
    if (mainVM != null)
    {
        mainVM.ViewChanged -= OnViewChanged;
    }
}

    }

}
