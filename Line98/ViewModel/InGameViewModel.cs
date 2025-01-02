using System.Windows;
namespace Line98.ViewModel
{
    public class InGameViewModel : ViewModelBase
    {
        public InGameViewModel()
        {
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
