using Line98.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Line98.Model
{
    [Serializable]
    public class GameLogic
    public class GameLogic : ViewModelBase
    {
        public Board board;
        private int minLength;
        private Random random = new Random();
        private (int row, int col)? selectedBallPosition;// Nullable Tuple
        private List<(int row, int col)>? movingPath;
        private int score;
        public event Action BallWillMove;

        private void NeedPreLogic()
        {
            BallWillMove?.Invoke(); // Kích hoạt sự kiện khi bóng được di chuyển
        }
        public int Score
        {
            get => score;
            set => score = value;
        }

        public (int row, int col)? SelectedBallPosition
        {
            get => selectedBallPosition;
            set => selectedBallPosition = value;
        }
        public int MinLength
        {
            get => minLength;
            set
            {
                minLength = value;
                OnPropertyChanged(nameof(MinLength));
            }
        }
        public List<(int row, int col)>? MovingPath
        {
            get => movingPath;
            private set => movingPath = value;
        }

        // Các hướng di chuyển: lên, xuống, trái, phải
        private readonly int[][] directions = new int[][]
   {
    new int[] { -1, 0 },  // Lên
    new int[] { 1, 0 },   // Xuống
    new int[] { 0, -1 },  // Trái
    new int[] { 0, 1 },   // Phải
    new int[] { -1, -1 }, // Chéo trái trên
    new int[] { -1, 1 },  // Chéo phải trên
    new int[] { 1, -1 },  // Chéo trái dưới
    new int[] { 1, 1 }    // Chéo phải dưới
   };
        private readonly int[][] Pathdirections = new int[][]
   {
    new int[] { -1, 0 },  // Lên
    new int[] { 1, 0 },   // Xuống
    new int[] { 0, -1 },  // Trái
    new int[] { 0, 1 },   // Phải
    };


        public GameLogic(Board board, int minLength = 5)
        {
            this.board = board;
            this.minLength = minLength;
        }
        public List<(int x, int y)> GetEmptyCells()
        {
            List<(int x, int y)> emptyCells = new List<(int x, int y)>();

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.IsEmpty(i, j))
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }
            return emptyCells;
        }
        public bool CheckGameOver()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.IsEmpty(i, j))
                    {
                        return false; // Game chưa kết thúc
                    }
                }
            }
            // Nếu không có ô trống nào, game kết thúc
            return true;
        }

        // Hàm tạo bóng nhỏ ở vị trí ngẫu nhiên
        public void MakeSmallBall()
        {
            var emptyCells = GetEmptyCells();
            if (emptyCells.Count == 0) return;

            // Đếm số lượng bóng mỗi màu hiện tại
            var colorCount = new int[7];
            foreach (var ball in board.GetAllBalls())
            {
                if (ball != null)
                    colorCount[ball.colorIndex]++;
            }

            // Xác định màu có số lượng nhiều nhất
            int mostFrequentColor = 0;
            int maxCount = 0;

            for (int i = 0; i < colorCount.Length; i++)
            {
                if (colorCount[i] > maxCount)
                {
                    maxCount = colorCount[i];
                    mostFrequentColor = i;
                }
            }

            // Tính toán xác suất để chọn màu nhiều nhất 
            int chosenColor;
            if (random.NextDouble() < 0.2)
            {
                chosenColor = mostFrequentColor;
            }
            else
            {
                chosenColor = random.Next(7); // Chọn ngẫu nhiên một màu khác
            }

            // Thêm bóng vào ô trống ngẫu nhiên
            var randomCell = emptyCells[random.Next(emptyCells.Count)];
            board.AddBall(randomCell.x, randomCell.y, new Ball(chosenColor, IsBig: false));
        }


        // Hàm tạo bóng lớn ở vị trí ngẫu nhiên
        public void MakeBigBall()
        {
            var emptyCells = GetEmptyCells();
            if (emptyCells.Count > 0)
            {
                var randomCell = emptyCells[random.Next(emptyCells.Count)];
                board.AddBall(randomCell.x, randomCell.y, new Ball(random.Next(7), IsBig: true));

            }

        }


        private void MoveBall((int x, int y) from, (int x, int y) to)
        {
            board.Balls[to.x, to.y] = board.Balls[from.x, from.y];
            board.RemoveBall(from.x, from.y);
            // Hủy chọn bóng sau khi di chuyển
            SelectedBallPosition = null;
        }



        // Hàm tính điểm
        public List<(int x, int y)> Scoring()
        {
            List<(int x, int y)> toClear = new List<(int x, int y)>();
            HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();
            int size = board.Size;

            // Duyệt qua từng ô trong bảng
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board.Balls[i, j] == null || visited.Contains((i, j))) continue;

                    int colorIndex = board.Balls[i, j].colorIndex;

                    // Kiểm tra tất cả các hướng (hàng ngang, dọc, chéo)
                    foreach (var direction in directions)
                    {
                        if (CheckLine(i, j, direction[0], direction[1], colorIndex, minLength))
                        {
                            AddToClearList(i, j, direction[0], direction[1], toClear, minLength, visited);
                        }
                    }

                    // Xử lý giao điểm (trường hợp dấu cộng)
                    CheckIntersections(i, j, minLength, toClear, visited);
                }
            }

            // Xóa các ô cần xóa
            foreach (var pos in toClear)
            {
                board.RemoveBall(pos.x, pos.y);
            }
            score += CalculateScore(toClear.Count);

            return toClear;
        }
        private void CheckIntersections(int startX, int startY, int minLength, List<(int x, int y)> toClear, HashSet<(int x, int y)> visited)
        {
            int colorIndex = board.Balls[startX, startY].colorIndex;

            // Kiểm tra giao giữa hàng ngang và dọc
            if (CheckLine(startX, startY, 0, 1, colorIndex, minLength) &&
                CheckLine(startX, startY, 1, 0, colorIndex, minLength))
            {
                // Thêm giao điểm và đánh dấu
                for (int dx = -minLength + 1; dx < minLength; dx++)
                {
                    int x = startX + dx;
                    if (board.IsWithinBounds(x, startY) && board.Balls[x, startY]?.colorIndex == colorIndex)
                    {
                        if (!toClear.Contains((x, startY)))
                        {
                            toClear.Add((x, startY));
                        }
                        visited.Add((x, startY));
                    }
                }

                for (int dy = -minLength + 1; dy < minLength; dy++)
                {
                    int y = startY + dy;
                    if (board.IsWithinBounds(startX, y) && board.Balls[startX, y]?.colorIndex == colorIndex)
                    {
                        if (!toClear.Contains((startX, y)))
                        {
                            toClear.Add((startX, y));
                        }
                        visited.Add((startX, y));
                    }
                }
            }
        }

        private void AddToClearList(int startX, int startY, int deltaX, int deltaY, List<(int x, int y)> toClear, int minLength, HashSet<(int x, int y)> visited)
        {
            for (int k = 0; k < minLength; k++)
            {
                int x = startX + k * deltaX;
                int y = startY + k * deltaY;

                if (!toClear.Contains((x, y)))
                {
                    toClear.Add((x, y));
                }
                visited.Add((x, y)); // Đánh dấu ô đã xử lý
            }
        }



        // Hàm kiểm tra xem có 5 ô liên tiếp có màu giống nhau không
        private bool CheckLine(int startX, int startY, int deltaX, int deltaY, int colorIndex, int minLength)
        {
            for (int k = 0; k < minLength; k++)
            {
                int x = startX + k * deltaX;
                int y = startY + k * deltaY;

                if (!board.IsWithinBounds(x, y) ||
                    board.Balls[x, y] == null ||
                    board.Balls[x, y].colorIndex != colorIndex ||
                    !board.Balls[x, y].isBig)
                {
                    return false;
                }
            }
            return true;
        }
        // Hàm tìm đường đi từ điểm bắt đầu đến điểm kết thúc
        public List<(int x, int y)> FindPath((int x, int y) start, (int x, int y) end)
        {
            int size = board.Size;
            if (board.Balls[end.x, end.y] != null)
            {
                if (board.Balls[end.x, end.y].isBig)
                {
                    return null;
                }
            }

            // Hàng đợi BFS và danh sách thăm
            Queue<(int x, int y, List<(int x, int y)> path)> queue = new Queue<(int x, int y, List<(int x, int y)> path)>();

            queue.Enqueue((start.x, start.y, new List<(int x, int y)> { start }));

            bool[,] visited = new bool[size, size];
            visited[start.x, start.y] = true;

            // BFS để tìm đường
            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                // Nếu đến đích, trả về đường đi
                if ((currentX, currentY) == end)
                {
                    return path;
                }

                // Duyệt các hướng di chuyển
                foreach (var dir in Pathdirections)
                {
                    int newX = currentX + dir[0];
                    int newY = currentY + dir[1];

                    // Kiểm tra điều kiện di chuyển
                    if (board.IsWithinBounds(newX, newY) && !visited[newX, newY])
                    {
                        // Cho phép đi qua bóng nhỏ (isBig == false) hoặc ô trống
                        if (board.IsEmpty(newX, newY) || (board.Balls[newX, newY] != null && !board.Balls[newX, newY].isBig))
                        {
                            // Thêm điểm mới vào đường đi
                            List<(int x, int y)> newPath = new List<(int x, int y)>(path) { (newX, newY) };
                            queue.Enqueue((newX, newY, newPath));
                            visited[newX, newY] = true;
                        }
                    }
                }
            }

            // Không tìm thấy đường đi
            return null;
        }
        public ClickState HandleClick(int row, int col)
        {
            if (SelectedBallPosition == null)
            {
                // Chọn bóng tại vị trí này
                SelectBall(row, col);
                Scoring();
                return ClickState.selectBall;
            }
            else
            {
                // Nếu đã có bóng được chọn, thử di chuyển đến vị trí mới
                bool moved = TryMoveBallTo(row, col);
                if (moved)
                {

                    SelectedBallPosition = null; // Hủy chọn bóng sau khi di chuyển
                    return ClickState.moved;
                }
                else
                {
                    // Nếu không di chuyển được, chọn bóng mới
                    SelectBall(row, col);

                    return ClickState.selectNewBall;
                }
            }
        }


        private bool TryMoveBallTo(int row, int col)
        {
            if (SelectedBallPosition == null) return false;

            // Tìm đường và di chuyển bóng
            MovingPath = FindPath(SelectedBallPosition.Value, (row, col));
            if (MovingPath != null)
            {
                NeedPreLogic();
                MoveBall(SelectedBallPosition.Value, (row, col));
                return true;
            }
            return false;
        }


        public void SelectBall(int row, int col)
        {
            if (!board.IsWithinBounds(row, col)) return;

            var ball = board.Balls[row, col];
            if (ball != null && ball.isBig)
            {
                SelectedBallPosition = (row, col);

            }
            else
            {
                SelectedBallPosition = null;

            }
        }
        public int CalculateScore(int numCleared)
        {
            const int scorePerBall = 10; // Mỗi bóng được xóa tăng 10 điểm 
            const int bonusScore = 20;
            if (numCleared > 5)
            {
                return 50 + (numCleared - 5) * bonusScore;
            }
            return numCleared * scorePerBall;
        }
        public void NewTurn()
        {
            foreach (var SmallBall in board.Balls)
            {
                if (SmallBall != null)
                {
                    if (!SmallBall.isBig)
                        SmallBall.isBig = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                MakeSmallBall();
            }
        }
        public Ball[,] GetBalls()
        {
            var size = board.Balls.GetLength(0);
            var copy = new Ball[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    copy[i, j] = board.Balls[i, j];
                }
            }

            return copy;
        }

        public void SetBoard(Ball[,] NewBoard)
        {
            for (int i = 0; i < board.Size; i++)
            {

                for (int j = 0; j < board.Size; j++)
                {

                    if (NewBoard[i, j] != null)
                    {
                        board.Balls[i, j] = NewBoard[i, j].Clone();
                    }
                    else
                        board.RemoveBall(i, j);
                }
            }
        }

        public int getSize()
        {
            return (int)board.Size;
        }
        public bool IsGameOVer()
        {
            if(GetEmptyCells().Count != (board.Size * board.Size))
            {
                return false;
            }
            return true;
        }
    }
}
public enum ClickState
{
    selectNewBall,
    selectBall,
    moved
}

