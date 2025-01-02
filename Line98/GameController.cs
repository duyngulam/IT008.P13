using Line98.Control;
using Line98.Model;
using Line98.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace Line98
{
    public class GameController
    {
        private readonly GameControl gameControl;
        public readonly GameLogic gameLogic;
        private InGameUC inGameUC;

        public GameController(GameControl gameControl, GameLogic gameLogic, InGameUC inGameUC)
        {
            this.gameControl = gameControl;
            this.gameLogic = gameLogic;
            this.inGameUC = inGameUC;
            gameControl.CellClicked += HandleCellClick;

        }

        private void InitializeGame()
        {
            for (int i = 0; i < 3; i++)
            {
                gameLogic.MakeBigBall();
                gameLogic.MakeSmallBall();
            }
            UpdateUI();
        }

        public void UpdateUI()
        {
            Ball[,] Balllist = gameLogic.GetBalls();
            gameControl.BallOverlay.Children.Clear();
            for (int i = 0; i < gameLogic.getSize(); i++)
            {
                for (int j = 0; j < gameLogic.getSize(); j++)
                {
                    if (Balllist[i, j] != null)
                        gameControl.AddBall(i, j, Balllist[i, j].colorIndex, Balllist[i, j].isBig, gameLogic.SelectedBallPosition);
                }
            }
        }

        public async void HandleCellClick(int row, int col)
        {
            ClickState clickstate = gameLogic.HandleClick(row, col);
            if (clickstate == ClickState.selectNewBall)
            {
                UpdateUI();
                return;
            }
            if (clickstate == ClickState.moved)// move thì update UI click thì k update
            {
                await gameControl.AnimateBallMovement(gameControl.BallOverlay, gameLogic.MovingPath);
                UpdateUI();
                var ClearPosition = gameLogic.Scoring();
                if (ClearPosition.Count != 0)
                {
                    inGameUC.scoreText.Text = gameLogic.Score.ToString();
                    MessageBox.Show($"{gameLogic.CalculateScore(ClearPosition.Count)}");
                    await gameControl.ClearBallsAnimation(gameControl.BallOverlay, ClearPosition);
                    return;
                }
                gameLogic.NewTurn();
                ClearPosition = gameLogic.Scoring();
                if (ClearPosition.Count != 0)
                {
                    inGameUC.scoreText.Text = gameLogic.Score.ToString();
                    MessageBox.Show($"{gameLogic.CalculateScore(ClearPosition.Count)}");
                    await gameControl.ClearBallsAnimation(gameControl.BallOverlay, ClearPosition);
                    return;
                }
                UpdateUI();
                if (gameLogic.IsGameOVer())
                {
                    MessageBox.Show("Gameover");
                }
            }
        }
        

        public void NewGame()
        {

            InitializeGame();

        }
    }




}
