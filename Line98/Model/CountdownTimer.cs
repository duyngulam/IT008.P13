using System.Security.Policy;
using System.Windows.Threading;

namespace Line98.Model
{
    class CountdownTimer
    {
        private int _timeInSeconds; // Lưu trữ thời gian (có thể tăng hoặc giảm)
        private DispatcherTimer _timer;
        private bool _isCountingUp; // Biến để xác định chế độ đếm (lên hoặc xuống)

        // Sự kiện khi thời gian thay đổi
        public event Action<string> TimeChanged;

        // Sự kiện khi thời gian kết thúc (chỉ áp dụng cho đếm ngược)
        public event Action TimeUp;

        // Khởi tạo CountdownTimer với chế độ và thời gian bắt đầu
        public CountdownTimer(int startTimeInMinutes = 0, bool isCountingUp = false)
        {
            _timeInSeconds = isCountingUp ? 0 : startTimeInMinutes * 60;
            _isCountingUp = isCountingUp;

            // Khởi tạo Timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            _timer.Tick += Timer_Tick;
        }

        // Bắt đầu đếm
        public void Start()
        {
            _timer.Start();
        }

        // Dừng đếm
        public void Stop()
        {
            _timer.Stop();
        }

        // Hàm xử lý sự kiện mỗi giây
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_isCountingUp)
            {
                // Tăng thời gian
                _timeInSeconds++;
                TimeChanged?.Invoke(FormatTime(_timeInSeconds));
                GameState.Instance.Time = _timeInSeconds;
            }
            else
            {
                // Giảm thời gian
                _timeInSeconds--;

                // Cập nhật thời gian lên giao diện
                TimeChanged?.Invoke(FormatTime(_timeInSeconds));
                GameState.Instance.Time = _timeInSeconds;

                // Nếu hết thời gian, dừng Timer và gọi sự kiện TimeUp
                if (_timeInSeconds <= 0)
                {
                    _timer.Stop();
                    TimeUp?.Invoke();
                }
            }
        }
        // Hàm định dạng lại thời gian dưới dạng "mm:ss"
        private string FormatTime(int timeInSeconds)
        {
            int minutes = timeInSeconds / 60;
            int seconds = timeInSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}"; // Hiển thị dưới dạng "mm:ss"
        }
        public int GetTimeLeft()
        {
            return _timeInSeconds;
        }
        public void SetTimeLeft(int timeLeftInSeconds)
        {
            _timeInSeconds = timeLeftInSeconds;
        }
    }
}
