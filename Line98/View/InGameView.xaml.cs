using Line98.Control;
using Line98.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Line98.View
{
    public partial class InGameView : UserControl
    {
        int GridSize = 9;
        int BallCount = 5;
        private GameController _gameController;

        public InGameView()
        {
            InitializeComponent();

            // Tạo Board và GameLogic
            var board = new Board(GridSize); // 9x9 lưới
            var gameLogic = new GameLogic(board, BallCount); // 5 bóng liên tiếp để xóa;

            // Tạo GameControl và GameController
            var gameControl = new GameControl(GridSize);
            _gameController = new GameController(gameControl, gameLogic, inGameUC);

            // Gán GameControl vào gameArea
            gameArea.Children.Add(gameControl);
            _gameController.NewGame();
        }
    }
}
