using Line98.Control;
using Line98.Model;
using Line98.ViewModel;
using System.Windows.Controls;


namespace Line98.View
{
    public partial class InGameView : UserControl
    {
        int GridSize = 9;
        int BallCount = 5;
        private GameController _gameController;
        private ControlPanelViewModel _viewModel;


        public InGameView()
        {
            InitializeComponent();

            _viewModel = new ControlPanelViewModel();
            DataContext = _viewModel;

            BallCount = GameState.Instance.SelectedBallCount;
            GridSize = BallCount == 6 ? 12 : 9;
            // Tạo Board và GameLogic
            if (!GameState.Instance.IsPlaying)
            {
                GameState.Instance.board = new Board(GridSize); // 9x9 lưới
            }
            if (GameState.Instance.LoadGame)
            {
                GameState.Instance.board = new Board(GridSize); // 9x9 lưới
                GameState.Instance.board.Balls = GameSaveData.Instance.Board;
            }
            var gameLogic = new GameLogic(GameState.Instance.board, BallCount); // 5 bóng liên tiếp để xóa;

            // Tạo GameControl và GameController
            var gameControl = new GameControl(GridSize);
            _gameController = new GameController(gameControl, gameLogic, inGameUC);
            inGameUC.SetTime(GameState.Instance.Time);
            // Gán GameControl vào gameArea
            gameArea.Children.Add(gameControl);
            _gameController.NewGame(); GameState.Instance.IsPlaying = true;
            if (gameLogic.IsGameOVer() == true)
            {
                GameOver gameOver = new GameOver();
                gameOver.Show();
            }



            _gameController.NewGame();

                           
        }
    }
}
