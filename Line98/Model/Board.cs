using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Line98.Model
{
    public class Board
    {
        public int Size { get; set; }
        public Ball[,] Balls { get; private set; }

        public Board(int size)
        {
            Size = size;
            Balls = new Ball[size, size];
        }

        public bool IsEmpty(int x, int y) => Balls[x, y] == null;

        public void AddBall(int x, int y, Ball ball) => Balls[x, y] = ball;

        public void RemoveBall(int x, int y) => Balls[x, y] = null;
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }
        public List<Ball> GetAllBalls()
        {
            var balls = new List<Ball>();

            for (int i = 0; i < Balls.GetLength(0); i++)
            {
                for (int j = 0; j < Balls.GetLength(1); j++)
                {
                    if (Balls[i, j] != null)
                    {
                        balls.Add(Balls[i, j]);
                    }
                }
            }

            return balls;
        }

    }
}
