using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Line98.Model;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for LoadGameView.xaml
    /// </summary>
    public partial class LoadGameView : UserControl
    {

        public LoadGameView()
        {
            InitializeComponent();
            this.DataContextChanged += UserControl_DataContextChanged;
        }


        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            UpdateUI();
        }
        private void UpdateUI()
        {
            Saveslot1.Children.Clear();
            if (GameSaveManager.IsSlotOccupied(1))
            {
                var loadedData1 = GameSaveManager.LoadFromSlot(1);

                int sbc = loadedData1.SelectedBallCount;
                int size;
                if (sbc == 6)
                {
                    size = 12;
                    var imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Line98;component/Resources/Grid12x12.png"));

                    // Áp dụng ImageBrush cho nền của Canvas
                    Saveslot1.Background = imageBrush;
                }
                else
                {
                    size = 9;
                    var imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Line98;component/Resources/Grid9x9.png"));

                    // Áp dụng ImageBrush cho nền của Canvas
                    Saveslot1.Background = imageBrush;
                }
                Ball[,] board = new Ball[size, size];

                board = loadedData1.Board;
                if (board != null)
                {

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (board[i, j] != null)
                                AddBall(Saveslot1, i, j, board[i, j].colorIndex, board[i, j].isBig, sbc);
                        }
                    }
                }
            }
        }
        public void AddBall(Canvas c, int row, int column, int colorIndex, bool isBig, int BallCount)
        {
            bool is12x12 = false;
            if (BallCount == 6)
            {
                is12x12 = true;
            }
            else
                is12x12 = false;

            int ballSize;
            double CellSize = 33.333333333333333;
            if (is12x12)
            {
                CellSize = 25;
                ballSize = isBig ? 15 : 5;
            }
            else
            {
                ballSize = isBig ? 23 : 13;
            }
            // Tính toán vị trí tâm của ô
            double centerX = column * CellSize + CellSize / 2.0;
            double centerY = row * CellSize + CellSize / 2.0;

            // Tạo hình ảnh bóng
            var ballImage = new Image
            {
                Source = GetBallImage(colorIndex),
                Width = ballSize,
                Height = ballSize
            };

            // Đặt tâm của ảnh vào tọa độ
            Canvas.SetLeft(ballImage, centerX - ballSize / 2.0);
            Canvas.SetTop(ballImage, centerY - ballSize / 2.0);

            // Thêm bóng vào lớp Canvas
            c.Children.Add(ballImage);

        }


        private ImageSource GetBallImage(int colorIndex)
        {
            string resourcePath = "pack://application:,,,/resources/balls.png"; // Đường dẫn tài nguyên
            var bitmap = new BitmapImage();

            // Tải ảnh PNG từ tài nguyên
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(resourcePath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // Tải toàn bộ ảnh
            bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            bitmap.EndInit();

            // Cắt ảnh (các sprite bóng)
            return new CroppedBitmap(bitmap, new Int32Rect(colorIndex * 100, 0, 100, 100));
        }
        private void Save1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var saveData = GameSaveData.Instance;
            GameSaveManager.SaveToSlot(1, saveData);
            UpdateUI();
        }

        private void Load1_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            try
            {
                var loadedData = GameSaveManager.LoadFromSlot(1);
                if (loadedData != null)
                {
                    GameSaveData.Instance.Board = loadedData.Board;
                    GameSaveData.Instance.Score = loadedData.Score;
                    GameSaveData.Instance.SelectedBallCount = loadedData.SelectedBallCount;
                    GameSaveData.Instance.Time = loadedData.Time;
                    GameSaveData.Instance.GameMode = loadedData.GameMode;
                    if (GameSaveData.Instance.Board != null)
                    {
                        GameState.Instance.IsPlaying = true;
                        GameState.Instance.LoadGame = true;
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
