using Line98.Model;
using static System.Formats.Asn1.AsnWriter;

public class GameState
{
    private static GameState _instance;
    public static GameState Instance => _instance ??= new GameState();

    // Trạng thái hiện tại của trò chơi
    public int SelectedBallCount { get; set; }
    public bool GameMode { get; set; }
    public Board board { get; set; }
    public int score { get; set; }
    public int Time { get; set; }
    public bool IsPlaying { get; set; }
    public bool LoadGame { get; set; }
    public void Reset()
    {
        board = null;
        score = 0;
        SelectedBallCount = 5;
        Time = 15 * 60;
        IsPlaying = false;
        LoadGame = false;
    }
}
