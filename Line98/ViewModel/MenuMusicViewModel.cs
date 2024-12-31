using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Line98.ViewModel
{
    public class MenuMusicViewModel : ViewModelBase
    {
        private bool _is1stSongPlaying;
        public bool Is1stSongPlaying
        {
            get => _is1stSongPlaying;
            set
            {
                if (_is1stSongPlaying != value)
                {
                    _is1stSongPlaying = value;
                    OnPropertyChanged(nameof(Is1stSongPlaying));
                }
            }
        }

        private bool _is2ndSongPlaying;
        public bool Is2ndSongPlaying
        {
            get => _is2ndSongPlaying;
            set
            {
                if (_is2ndSongPlaying != value)
                {
                    _is2ndSongPlaying = value;
                    OnPropertyChanged(nameof(Is2ndSongPlaying));
                }
            }
        }

        private bool _is3rdSongPlaying;
        public bool Is3rdSongPlaying
        {
            get => _is3rdSongPlaying;
            set
            {
                if (_is3rdSongPlaying != value)
                {
                    _is3rdSongPlaying = value;
                    OnPropertyChanged(nameof(Is3rdSongPlaying));
                }
            }
        }

        private bool _is4thSongPlaying;
        public bool Is4thSongPlaying
        {
            get => _is4thSongPlaying;
            set
            {
                if (_is4thSongPlaying != value)
                {
                    _is4thSongPlaying = value;
                    OnPropertyChanged(nameof(Is4thSongPlaying));
                }
            }
        }

        private bool _is5thSongPlaying;
        public bool Is5thSongPlaying
        {
            get => _is5thSongPlaying;
            set
            {
                if (_is5thSongPlaying != value)
                {
                    _is5thSongPlaying = value;
                    OnPropertyChanged(nameof(Is5thSongPlaying));
                }
            }
        }

        private string _currentSong;
        public string CurrentSong
        {
            get => _currentSong;
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    OnPropertyChanged(nameof(CurrentSong));
                }
            }
        }
        public ICommand Play1stSongCommand { get; set; }
        public ICommand Play2ndSongCommand { get; set; }
        public ICommand Play3rdSongCommand { get; set; }
        public ICommand Play4thSongCommand { get; set; }
        public ICommand Play5thSongCommand { get; set; }
        public MenuMusicViewModel()
        {
            Play1stSongCommand = new ViewModelCommand(Play1stSong);
            Play2ndSongCommand = new ViewModelCommand(Play2ndSong);
            Play3rdSongCommand = new ViewModelCommand(Play3rdSong);
            Play4thSongCommand = new ViewModelCommand(Play4thSong);
            Play5thSongCommand = new ViewModelCommand(Play5thSong);
            Is1stSongPlaying = true;
            Is2ndSongPlaying = false;
            CurrentSong = "Resources/BackgroundMusic/Song1.wav";
        }

        private void Play5thSong(object obj)
        {
            throw new NotImplementedException();
        }

        private void Play4thSong(object obj)
        {
            throw new NotImplementedException();
        }

        private void Play3rdSong(object obj)
        {
            throw new NotImplementedException();
        }

        private void Play2ndSong(object obj)
        {
            Is1stSongPlaying = false;
            Is2ndSongPlaying = true;
            Is3rdSongPlaying = false;
            Is4thSongPlaying = false;
            Is5thSongPlaying = false;
            CurrentSong = "Resources/BackgroundMusic/Song2.wav";
            MessageBox.Show(CurrentSong);
        }

        private void Play1stSong(object obj)
        {
            Is1stSongPlaying = true;
            Is2ndSongPlaying = false;
            Is3rdSongPlaying = false;
            Is4thSongPlaying = false;
            Is5thSongPlaying = false;
            CurrentSong = "Resources/BackgroundMusic/Song1.wav";
            MessageBox.Show(CurrentSong);
        }
    }
}
