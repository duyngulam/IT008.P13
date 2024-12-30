using Line98.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for InGameUC.xaml
    /// </summary>
    public partial class InGameUC : UserControl
    {
        private CountdownTimer _countdownTimer;
        private CountdownTimer countUp;
        private GameLogic _gameLogic;
        private int _score;
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
            _countdownTimer = new CountdownTimer(15); // 5 phút = 300 giây

            // Đăng ký sự kiện TimeChanged để cập nhật TextBlock mỗi khi thời gian thay đổi
            _countdownTimer.TimeChanged += (time) => CountdownText.Text = time;

            // Đăng ký sự kiện TimeUp để thông báo khi hết thời gian
            _countdownTimer.TimeUp += () => CountdownText.Text = "Time's up!";

            // Bắt đầu đếm ngược
            _countdownTimer.Start();
        }

        void countup()
        {
            countUp = new CountdownTimer(isCountingUp: true); // Đếm thời gian từ 0
            countUp.TimeChanged += time => CountdownText.Text = time;
            countUp.Start();
        }


    }
}
