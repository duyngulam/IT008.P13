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
using System.Windows.Media.Animation;
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
            Loaded += MainWindow_Loaded;

            _player = new System.Media.SoundPlayer("Resources/Background Music/Song 1.wav");
            _viewModel = new ControlPanelViewModel();
            DataContext = _viewModel;

            //BACKGROUND MUSIC
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var ImageAnimation = (Storyboard)FindResource("ImageAnimation");
            ImageAnimation.Begin(Planet1);

            var ImageAnimation2 = (Storyboard)FindResource("ImageAnimation2");
            ImageAnimation2.Begin(Planet2);

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
            Content.BeginAnimation(OpacityProperty, doubleAnimation);
        }

        public void ChangeBackgroundMusic(string musicFilePath)
        {
            _player.Stop();
            _player.SoundLocation = musicFilePath;
            _player.Load();
            _player.PlayLooping();
        }
    }
}