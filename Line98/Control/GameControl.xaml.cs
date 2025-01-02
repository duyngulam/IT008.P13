using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
namespace Line98.Control
{

    public partial class GameControl : UserControl
    {
        public static readonly DependencyProperty Is12x12Property =
            DependencyProperty.Register("Is12x12", typeof(bool), typeof(GameControl),
            new PropertyMetadata(false));
        private int ballSize;
        public bool Is12x12
        {
            get => (bool)GetValue(Is12x12Property);
            set => SetValue(Is12x12Property, value);
        }
        public event Action<int, int> CellClicked;
        private int GridSize { get; set; }
        int cellSize; // Kích thước ô lưới
        public GameControl(int GridSize)
        {
            SetValue(Is12x12Property, false);
            InitializeComponent();
            if (GameState.Instance.SelectedBallCount == 6)
            {
                SetValue(Is12x12Property, true);
            }
            this.GridSize = GridSize;
            cellSize = Is12x12 ? 60 : 80;

        }

        // Thêm bóng vào Canvas
        public void AddBall(int row, int column, int colorIndex, bool isBig, (int x, int y)? SelectedBallPosition)
        {
            if (Is12x12)
            {
                ballSize = isBig ? 35 : 15;
            }
            else
            {
                ballSize = isBig ? 55 : 30;
            }
            // Tính toán vị trí tâm của ô
            double centerX = column * cellSize + cellSize / 2.0;
            double centerY = row * cellSize + cellSize / 2.0;

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
            BallOverlay.Children.Add(ballImage);
            ballImage.Tag = $"{row}_{column}";

            if (isBig)
            {
                ballImage.MouseLeftButtonDown += (s, e) =>
                {

                    AddClickAnimation((Image)s, centerY - ballSize / 2.0);
                };
            }
            if (SelectedBallPosition != null && SelectedBallPosition.Value.x == row && SelectedBallPosition.Value.y == column)
            {
                AddClickAnimation(ballImage, centerY - ballSize / 2.0);
            }
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


        // Xóa toàn bộ bóng khỏi lớp Canvas
        public void ClearBalls()
        {
            BallOverlay.Children.Clear();
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickedPoint = e.GetPosition((Canvas)sender);
            int cellSize = 720 / GridSize; // Kích thước 1 ô trong lưới (9x9)
            int row = (int)(clickedPoint.Y / cellSize);
            int col = (int)(clickedPoint.X / cellSize);
            CellClicked?.Invoke(row, col);
        }
        public void AddClickAnimation(UIElement element, double Y)
        {
            // Tìm bóng tại vị trí cụ thể (sử dụng phương thức GetBallAtPosition)
            var ballImage = GetBallAtPosition(BallOverlay, (int)Canvas.GetTop(element) / cellSize, (int)Canvas.GetLeft(element) / cellSize);

            if (ballImage != null)
            {
                // Tạo một Storyboard cho animation
                var storyboard = (Storyboard)FindResource("VerticalMoveAnimation");

                // Tùy chỉnh giá trị From và To
                foreach (var child in storyboard.Children)
                {
                    if (child is DoubleAnimation animation)
                    {
                        animation.From = Y - 5;
                        animation.To = Y + 5;
                    }
                }

                // Đặt Transform gốc cho element
                ballImage.RenderTransform = new ScaleTransform(1, 1);

                // Đăng ký sự kiện Completed của Storyboard
                storyboard.Completed += (s, e) =>
                {
                    // Xử lý khi animation hoàn tất (nếu cần)
                };

                // Chạy Storyboard
                Storyboard.SetTarget(storyboard, ballImage);
                storyboard.Begin();
            }
        }

        public Task ClearBallsAnimation(Canvas canvas, List<(int x, int y)> positions)
        {
            var tcs = new TaskCompletionSource<bool>();
            var tasks = new List<Task>();

            foreach (var position in positions)
            {

                // Tìm bóng tại vị trí x, y
                var ball = GetBallAtPosition(canvas, position.x, position.y);
                if (ball != null)
                {
                    // Tạo một TaskCompletionSource để đợi animation hoàn thành cho bóng này
                    var animationTcs = new TaskCompletionSource<bool>();

                    var storyboard = (Storyboard)FindResource("FadeAnimation");

                    Storyboard.SetTarget(storyboard, ball);

                    // Gắn sự kiện hoàn thành cho Storyboard
                    storyboard.Completed += (s, e) =>
                    {
                        canvas.Children.Remove(ball);
                        animationTcs.TrySetResult(true);
                    };
                    storyboard.Completed -= (s, e) => { };

                    // Thêm Task vào danh sách
                    tasks.Add(animationTcs.Task);

                    // Khởi chạy animation
                    storyboard.Begin();
                }
            }

            // Khi tất cả animation hoàn thành, hoàn thành Task chính
            Task.WhenAll(tasks).ContinueWith(t =>
            {
                tcs.SetResult(true);
            });

            return tcs.Task;
        }
        public async Task AnimateBallMovement(Canvas canvas, List<(int x, int y)> path)
        {
            if (path == null) return;

            var cellSize = this.cellSize; // Kích thước ô
            var start = path[0];
            var end = path[path.Count - 1];

            // Lấy bóng cũ tại điểm bắt đầu
            var ballImage = GetBallAtPosition(canvas, start.x, start.y);
            if (ballImage == null) return;
            Panel.SetZIndex(ballImage, int.MaxValue);
            // Di chuyển bóng qua từng điểm trong path
            for (int i = 1; i < path.Count; i++)
            {
                var current = path[i];
                double targetX = current.y * cellSize + (cellSize - ballSize) / 2.0;
                double targetY = current.x * cellSize + (cellSize - ballSize) / 2.0;


                // Tạo animation di chuyển
                var animationX = new DoubleAnimation(Canvas.GetLeft(ballImage), targetX, new Duration(TimeSpan.FromMilliseconds(50)));
                var animationY = new DoubleAnimation(Canvas.GetTop(ballImage), targetY, new Duration(TimeSpan.FromMilliseconds(50)));

                // Chạy animation
                var tcs = new TaskCompletionSource<bool>();
                animationY.Completed += (s, e) => tcs.SetResult(true);

                ballImage.BeginAnimation(Canvas.LeftProperty, animationX);
                ballImage.BeginAnimation(Canvas.TopProperty, animationY);

                await tcs.Task; // Đợi animation hoàn tất
            }

            // Đặt vị trí cuối cùng sau khi hoàn tất
            Canvas.SetLeft(ballImage, end.y * cellSize);
            Canvas.SetTop(ballImage, end.x * cellSize);
        }

        public void ShowNumberWithAnimation(Canvas canvas, int number, int row, int col)
        {
            // Tính toán vị trí trung tâm của ô
            double centerX = col * cellSize + cellSize / 2.0;
            double centerY = row * cellSize + cellSize / 2.0;

            // Tạo TextBlock để hiển thị số
            var textBlock = new TextBlock
            {
                Text = number.ToString(),
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.IndianRed,
                Background = Brushes.Transparent
            };

            // Đặt vị trí ban đầu (tọa độ canvas)
            Canvas.SetLeft(textBlock, centerX - textBlock.FontSize / 2); // Canh giữa theo X
            Canvas.SetTop(textBlock, centerY - textBlock.FontSize / 2); // Canh giữa theo Y

            // Thêm TextBlock vào Canvas
            canvas.Children.Add(textBlock);

            // Tạo animation di chuyển lên trên
            var moveUpAnimation = new DoubleAnimation
            {
                From = centerY,
                To = centerY - 50, // Di chuyển lên trên 50 pixels
                Duration = TimeSpan.FromSeconds(1), // Thời gian di chuyển
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            // Tạo animation fade-out
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(1) // Thời gian biến mất
            };

            // Gắn animation fade-out vào thuộc tính Opacity
            textBlock.BeginAnimation(OpacityProperty, fadeOutAnimation);

            // Gắn animation di chuyển vào thuộc tính Top
            var tcs = new TaskCompletionSource<bool>();
            moveUpAnimation.Completed += (s, e) =>
            {
                // Xóa TextBlock khỏi Canvas sau khi animation hoàn tất
                canvas.Children.Remove(textBlock);
                tcs.SetResult(true);
            };

            textBlock.BeginAnimation(Canvas.TopProperty, moveUpAnimation);
        }


        private UIElement? GetBallAtPosition(Canvas canvas, int row, int column)
        {
            return canvas.Children.OfType<Image>()
    .FirstOrDefault(ball => ball.Tag?.ToString() == $"{row}_{column}");

        }
    }
}
