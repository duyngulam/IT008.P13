using Line98.Control;
using Line98.Model;
using Line98.ViewModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Line98.Control.GameControl;

namespace Line98
{
    public partial class MainWindow : Window
    {
        private System.Media.SoundPlayer _player;
        private ControlPanelViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            _player = new System.Media.SoundPlayer("Resources/Background Music/Song 1.wav");
            _viewModel = new ControlPanelViewModel();
            DataContext = _viewModel;

            //BACKGROUND MUSIC
            System.Media.SoundPlayer player = new System.Media.SoundPlayer("Resources/Background Music/Song 1.wav");
            _player.Load();
            _player.PlayLooping();

            _viewModel.InGameViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(InGameViewModel.IsVolumeChecked))
            {
                if (_viewModel.InGameViewModel.IsVolumeChecked)
                {
                    _player.Load();
                    _player.PlayLooping();
                }
                else
                {
                    _player.Stop();
                }
            }
        }
    }
}