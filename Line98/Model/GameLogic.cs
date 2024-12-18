﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line98.Model
{
    public class GameLogic
    {
        private Board board;
        private int minLength;
        private Random random = new Random();
        private (int row, int col)? selectedBallPosition; // Nullable Tuple
        public (int row, int col)? SelectedBallPosition
        {
            get => selectedBallPosition;
            private set => selectedBallPosition = value;
        }

        // Các hướng di chuyển: lên, xuống, trái, phải
        private readonly int[][] directions = new int[][]
        {
        new int[] { -1, 0 },  // Lên
        new int[] { 1, 0 },   // Xuống
        new int[] { 0, -1 },  // Trái
        new int[] { 0, 1 }    // Phải
        };

        public GameLogic(Board board, int minLength)
        {
            this.board = board;
            this.minLength = minLength;
        }


        // Hàm tạo bóng nhỏ ở vị trí ngẫu nhiên
        public void MakeSmallBall()
        {
            var emptyCells = GetEmptyCells();
            if (emptyCells.Count > 0)
            {
                var randomCell = emptyCells[random.Next(emptyCells.Count)];
                board.AddBall(randomCell.x, randomCell.y, new Ball(random.Next(7), IsBig: false));
            }
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


        private void MoveBall((int x, int y) from, (int x, int y) to, List<(int x, int y)> path)
        {
            // Di chuyển bóng theo đường đi
            foreach (var step in path)
            {
                if (board.IsEmpty(step.x, step.y))
                {
                    board.Balls[step.x, step.y] = board.Balls[from.x, from.y];
                    board.RemoveBall(from.x, from.y);
                    from = step; // Cập nhật vị trí bóng hiện tại
                }
            }

            // Cập nhật vị trí cuối cùng
            board.Balls[to.x, to.y] = board.Balls[from.x, from.y];
            board.RemoveBall(from.x, from.y);

            // Hủy chọn bóng sau khi di chuyển
            SelectedBallPosition = null;
        }



        // Hàm tính điểm
        public List<(int x, int y)> Scoring(int minLength)
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

            // Kiểm tra điều kiện đầu vào: vị trí hợp lệ
            if (!board.IsWithinBounds(start.x, start.y) ||
                !board.IsWithinBounds(end.x, end.y) ||
                board.Balls[start.x, start.y] == null ||
                board.Balls[end.x, end.y] != null)
            {
                return null;
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
                foreach (var dir in directions)
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
        public bool HandleClick(int row, int col)
        {
            if (!board.IsWithinBounds(row, col)) return false;

            if (SelectedBallPosition == null)
            {
                // Chọn bóng tại vị trí này
                SelectBall(row, col);
                return true;
            }
            else
            {
                // Nếu đã có bóng được chọn, thử di chuyển đến vị trí mới
                bool moved = TryMoveBallTo(row, col);
                if (moved)
                {
                    SelectedBallPosition = null; // Hủy chọn bóng sau khi di chuyển
                    return true;
                }
                else
                {
                    // Nếu không di chuyển được, chọn bóng mới
                    SelectBall(row, col);
                    return false;
                }
            }
        }


        private bool TryMoveBallTo(int row, int col)
        {
            if (SelectedBallPosition == null) return false;

            // Tìm đường và di chuyển bóng
            var path = FindPath(SelectedBallPosition.Value, (row, col));
            if (path != null)
            {
                MoveBall(SelectedBallPosition.Value, (row, col), path);
                return true;
            }

            return false;
        }


        public void SelectBall(int row, int col)
        {
            if (!board.IsWithinBounds(row, col)) return;

            var ball = board.Balls[row, col];
            if (ball != null)
            {
                // Đặt bóng tại vị trí (row, col) làm bóng được chọn
                SelectedBallPosition = (row, col);
            }
            else
            {
                // Nếu ô trống được click, hủy chọn bóng
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

    }

}
