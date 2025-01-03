using NAudio.Wave;
using System;
using System.IO;
using System.Windows;

namespace Line98.Model
{
    class ChangeMusic
    {
        private static ChangeMusic _instance;
        private static readonly object _lock = new object();

        // Biến lưu style hiện tại
        public string ResourcePath { get; private set; } = "pack://application:,,,/resources/BackgroundMusic/Song1.wav";

        // Đối tượng WaveOutEvent để phát nhạc
        private WaveOutEvent _waveOut;
        private WaveStream _audioFileReader;
        public bool isPlaying =true;
        private ChangeMusic()
        {
            // Khởi tạo đối tượng WaveOutEvent
            _waveOut = new WaveOutEvent();
        }

        public static ChangeMusic Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ChangeMusic();
                }
                return _instance;
            }
        }

        // Phương thức đổi nhạc
        public void ChangeMusicTo(string musicName)
        {
            // Đảm bảo dừng nhạc hiện tại
            StopMusic();

            // Thay đổi đường dẫn nhạc
            ResourcePath = $"pack://application:,,,/Resources/BackgroundMusic/{musicName}.wav";

            // Chơi nhạc mới
            PlayMusic();
        }

        // Phương thức phát nhạc
        public void PlayMusic()
        {
            try
            {
                // Lấy tài nguyên âm thanh từ URI pack
                Uri resourceUri = new Uri(ResourcePath, UriKind.Absolute);
                Stream resourceStream = Application.GetResourceStream(resourceUri)?.Stream;

                if (resourceStream == null)
                {
                    throw new Exception($"Không tìm thấy tài nguyên âm thanh: {ResourcePath}");
                }

                // Đọc tài nguyên âm thanh từ Stream và phát nhạc
                _audioFileReader = new WaveFileReader(resourceStream); // Sử dụng WaveFileReader để đọc file WAV
                _waveOut.Init(_audioFileReader);
                _waveOut.Play();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có (ví dụ: file không tồn tại)
                Console.WriteLine($"Error playing music: {ex.Message}");
            }
        }

        // Phương thức dừng nhạc
        public void StopMusic()
        {
            if (_waveOut.PlaybackState == PlaybackState.Playing)
            {
                _waveOut.Stop();
            }

            // Giải phóng tài nguyên WaveStream
            _audioFileReader?.Dispose();
        }
    }
}
