using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        private int scoreSave;
        public GameOver(int score)
        {
            InitializeComponent();
            scoreSave = score;
        }
        
        void newGame()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Đặt cửa sổ mới là MainWindow
            App.Current.MainWindow = mainWindow;
            foreach (Window window in App.Current.Windows)
            {
                if (window != App.Current.MainWindow)
                {
                    window.Close();
                }
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string fileName;

            // Kiểm tra trạng thái đếm thời gian và đặt tên file phù hợp
            if (StyleBallManager.Instance.isCountingup)
            {
                fileName = "NormalScore.txt";
            }
            else
            {
                fileName = "TimerScore.txt";
            }

            // Đường dẫn tuyệt đối đến thư mục chứa file trong dự án
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "ScoreData", fileName);

            // Kiểm tra nếu thư mục chứa file không tồn tại, tạo nó nếu cần thiết
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Ghi dữ liệu vào tệp
            string content = txtName.Text;
            // Thực hiện bắt đầu trò chơi mới sau khi lưu
            //   filePath = "D:\\KTPM\\HK3\\LapTrinhTrucQuan\\DoAn\\Line98\\ScoreData\\NormalScore.txt";
                        //  "D:\\KTPM\\HK3\\LapTrinhTrucQuan\\DoAn\\Line98\\Line98\\ScoreData\\NormalScore.txt"
            try
            {
                File.AppendAllText(filePath, content + "." + scoreSave.ToString() + "\n");
                MessageBox.Show("Dữ liệu đã được lưu thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi dữ liệu vào file: {ex.Message}");
            }

            newGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            newGame();

        }
    }
}
