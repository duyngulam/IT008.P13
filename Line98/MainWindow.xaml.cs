using Line98.Control;
using Line98.Model;
using Line98.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Line98.Control.GameControl;

namespace Line98
{
    public partial class MainWindow : Window
    {
        int GridSize = 9;
        int BallCount = 5;
        private GameController _gameController;
        public MainWindow()
        {
            InitializeComponent();

            //BACKGROUND MUSIC
            System.Media.SoundPlayer player = new System.Media.SoundPlayer("Resources/Background Music/Song 1.wav");
            player.Load();
            player.PlayLooping();
            

            //// Tạo Board và GameLogic
            //var board = new Board(GridSize); // 9x9 lưới
            //var gameLogic = new GameLogic(board, BallCount); // 5 bóng liên tiếp để xóa
            //var inGameUC = new View.InGameUC();

            //// Tạo GameControl và GameController
            //var gameControl = new GameControl(GridSize);
            //_gameController = new GameController(gameControl, gameLogic, inGameUC);

            //// Gán GameControl vào gameArea
            ////gameArea.Children.Add(gameControl);
            //_gameController.NewGame();

        }

    }
}