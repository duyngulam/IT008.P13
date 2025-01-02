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
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace Line98
{
    public partial class MainWindow : Window
    {
        private WaveOutEvent _waveOut;           // Đối tượng để phát nhạc
        private AudioFileReader _audioFile;     // Đối tượng đọc file nhạc
        private ControlPanelViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            _viewModel = new ControlPanelViewModel();
            DataContext = _viewModel;

            //BACKGROUND MUSIC
            _audioFile = new AudioFileReader(_viewModel.MenuMusicViewModel.CurrentSong);
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioFile);
            _waveOut.Play();

            _viewModel.InGameViewModel.PropertyChanged += InGameViewModel_PropertyChanged;
            _viewModel.MenuMusicViewModel.PropertyChanged += MenuMusicViewModel_PropertyChanged;
        }

        private void InGameViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(InGameViewModel.IsVolumeChecked))
            {
                if (_viewModel.InGameViewModel.IsVolumeChecked)
                {
                    _waveOut.Play();
                }
                else
                {
                    _waveOut.Pause();
                }
            }
        }
        
        private void MenuMusicViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MenuMusicViewModel.CurrentSong))
            {
                _audioFile = new AudioFileReader(_viewModel.MenuMusicViewModel.CurrentSong);
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_audioFile);
                _waveOut.Play();
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
            _waveOut.Stop();
            _audioFile.Dispose();
            _audioFile = new AudioFileReader(musicFilePath);
            _waveOut.Init(_audioFile);
            _waveOut.Play();
        }
    }
}