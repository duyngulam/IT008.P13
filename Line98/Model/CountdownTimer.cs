using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Line98.Model
{
    class CountdownTimer
    {
        private int _timeLeftInSeconds; // Lưu trữ thời gian còn lại tính bằng giây
        private DispatcherTimer _timer;

        // Sự kiện khi thời gian thay đổi
        public event Action<string> TimeChanged;

        // Sự kiện khi thời gian kết thúc
        public event Action TimeUp;

        // Khởi tạo CountdownTimer với thời gian bắt đầu là số giây
        public CountdownTimer(int startTimeInMinutes)
        {
            // Chuyển đổi thời gian từ phút sang giây
            _timeLeftInSeconds = startTimeInMinutes * 60;

            // Khởi tạo Timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);  // Cập nhật mỗi giây
            _timer.Tick += Timer_Tick;
        }

        // Bắt đầu đếm ngược
        public void Start()
        {
            _timer.Start();
        }

        // Dừng đếm ngược
        public void Stop()
        {
            _timer.Stop();
        }

        // Hàm xử lý sự kiện mỗi giây
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Giảm thời gian
            _timeLeftInSeconds--;

            // Cập nhật thời gian lên giao diện
            TimeChanged?.Invoke(FormatTime(_timeLeftInSeconds));

            // Nếu hết thời gian, dừng Timer và gọi sự kiện TimeUp
            if (_timeLeftInSeconds <= 0)
            {
                _timer.Stop();
                TimeUp?.Invoke();
            }
        }

        // Hàm định dạng lại thời gian dưới dạng "mm:ss"
        private string FormatTime(int timeInSeconds)
        {
            int minutes = timeInSeconds / 60;
            int seconds = timeInSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";  // Hiển thị dưới dạng "mm:ss"
        }
    }
}
