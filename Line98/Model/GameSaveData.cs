using Line98.Model;
using System;

public class GameSaveData
{
    private static GameSaveData _instance;
    private static readonly object _lock = new object();

    public Ball[,] Board { get; set; }
    public int Score { get; set; }
    public int SelectedBallCount { get; set; }
    public int Time { get; set; }

    // Constructor private để ngăn việc khởi tạo bên ngoài
    private GameSaveData()
    {
        Board = null;
        Score = 0;
        SelectedBallCount = 5;
        Time = 0;
    }

    // Truy cập instance duy nhất của class
    public static GameSaveData Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GameSaveData();
                }
                return _instance;
            }
        }
    }

    // Phương thức để reset dữ liệu
    public void Reset()
    {
        Board = null;
        Score = 0;
        SelectedBallCount = 5;
        Time = 0;
    }
}
