using Line98.Model;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for InGameUC.xaml
    /// </summary>
    public partial class InGameUC : UserControl
    {

        private CountdownTimer _countdownTimer = new CountdownTimer(15);
        private CountdownTimer countUp = new CountdownTimer(0);
        private GameLogic _gameLogic;
        private int _score;
        private bool paused = false;
        public event Action UndoClicked;
        public event Action PauseClicked;
        public event Action SaveClicked;
        public event Action GameOver;
        public InGameUC()
        {
            InitializeComponent();
            if (StyleBallManager.Instance.isCountingup == true) { countup(); }
            else
            {
                countdown();
            }
        }

        void countdown()
        {
            //  _countdownTimer = new CountdownTimer(15); // 5 phút = 300 giây

            // Đăng ký sự kiện TimeChanged để cập nhật TextBlock mỗi khi thời gian thay đổi
            _countdownTimer.TimeChanged += (time) => CountdownText.Text = time;

            // Đăng ký sự kiện TimeUp để thông báo khi hết thời gian
            _countdownTimer.TimeUp += () =>
            {
                CountdownText.Text = "Time's up!";
                GameOver?.Invoke();
            };

            // Bắt đầu đếm ngược
            _countdownTimer.Start();
        }

        void countup()
        {
            countUp = new CountdownTimer(isCountingUp: true); // Đếm thời gian từ 0
            countUp.TimeChanged += time => CountdownText.Text = time;
            countUp.Start();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

            UndoClicked?.Invoke(); // Gọi sự kiện khi nhấn nút Undo
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            PauseClicked?.Invoke();
            if (paused)
            {
                paused = !paused;
                _countdownTimer?.Start();
                countUp.Start();
            }
            else
            {
                paused = !paused;
                _countdownTimer?.Stop();
                countUp.Stop();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            _countdownTimer?.Stop();
            countUp.Stop();
            SaveClicked?.Invoke();
        }
        public int GetTime()
        {
            return _countdownTimer.GetTimeLeft();
        }
        public void SetTime(int time)
        {
            _countdownTimer.SetTimeLeft(time);
            countUp.SetTimeLeft(time);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ChangeMusic.Instance.isPlaying)
            {
                ChangeMusic.Instance.StopMusic();
                ChangeMusic.Instance.isPlaying = false;
            }
            else
            {
                ChangeMusic.Instance.PlayMusic();
                ChangeMusic.Instance.isPlaying = true;
            }
        }
    }
}
