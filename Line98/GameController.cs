﻿using Line98.Control;
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

namespace Line98
{
    public class GameController
    {
        private readonly GameControl gameControl;
        private GameLogic preGameLogic;
        private Board TempBoard;
        public GameLogic gameLogic;
        private InGameUC inGameUC;

        public GameController(GameControl gameControl, GameLogic gameLogic, InGameUC inGameUC)
        {
            this.gameControl = gameControl;
            this.gameLogic = gameLogic;
            TempBoard = new Board(GameState.Instance.SelectedBallCount == 6 ? 12 : 9);
            preGameLogic = new GameLogic(TempBoard);
            this.inGameUC = inGameUC;
            inGameUC.UndoClicked += Undo;
            gameLogic.BallWillMove += UpdatePreLogic;
            gameControl.CellClicked += HandleCellClick;
            inGameUC.PauseClicked += PauseScreen;
            inGameUC.SaveClicked += SaveToGameSaveData;
            inGameUC.GameOver += StopGame;
            inGameUC.SetTime(GameState.Instance.Time);
        }
        private void StopGame()
        {
            GameOver gameOver = new GameOver(gameLogic.Score); // Gán cửa sổ hiện tại (this) làm chủ
            GameState.Instance.Reset();
            gameOver.ShowDialog();
            gameControl.ClearBalls();
            GameState.Instance.Reset();
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

            inGameUC.scoreText.Text = gameLogic.Score.ToString();
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
                    int scoreChange = gameLogic.CalculateScore(ClearPosition.Count);
                    inGameUC.scoreText.Text = gameLogic.Score.ToString();
                    GameState.Instance.score = gameLogic.Score;
                    gameControl.ShowNumberWithAnimation(gameControl.BallOverlay, scoreChange, row, col);
                    await gameControl.ClearBallsAnimation(gameControl.BallOverlay, ClearPosition);
                    return;
                }
                gameLogic.NewTurn();
                ClearPosition = gameLogic.Scoring();
                if (ClearPosition.Count != 0)
                {
                    int scoreChange = gameLogic.CalculateScore(ClearPosition.Count);
                    inGameUC.scoreText.Text = gameLogic.Score.ToString();
                    GameState.Instance.score = gameLogic.Score;
                    gameControl.ShowNumberWithAnimation(gameControl.BallOverlay, scoreChange, row, col);
                    await gameControl.ClearBallsAnimation(gameControl.BallOverlay, ClearPosition);
                    UpdateUI();
                    return;
                }
                UpdateUI();
                if (gameLogic.CheckGameOver())
                {

                    StopGame();
                }

            }
        }

        public void NewGame()
        {
            if (GameState.Instance.IsPlaying)
            {
                gameLogic.Score = GameSaveData.Instance.Score;
                inGameUC.SetTime(GameSaveData.Instance.Time);
                UpdateUI();
                return;
            }
            InitializeGame();
        }
        private void PauseScreen()
        {
            if (gameControl.PauseOverlay.Visibility == Visibility.Collapsed)
            {

                gameControl.PauseOverlay.Visibility = Visibility.Visible;
            }
            else
            {

                gameControl.PauseOverlay.Visibility = Visibility.Collapsed;
            }
        }
        private void Undo()
        {
            if (preGameLogic != null)
            {
                if (preGameLogic.GetEmptyCells().Count != preGameLogic.getSize() * preGameLogic.getSize())
                {

                    gameLogic.SetBoard(preGameLogic.GetBalls());
                    gameLogic.Score = preGameLogic.Score;
                    UpdateUI();
                    inGameUC.scoreText.Text = gameLogic.Score.ToString();

                }
            }
        }


        private void UpdatePreLogic()
        {
            // Tạo bản sao mảng bóng từ GameLogic
            Ball[,] TempBall = gameLogic.GetBalls();
            Ball[,] newBallState = new Ball[gameLogic.getSize(), gameLogic.getSize()];

            for (int i = 0; i < gameLogic.getSize(); i++)
            {
                for (int j = 0; j < gameLogic.getSize(); j++)
                {
                    if (TempBall[i, j] != null)
                    {
                        // Tạo bản sao của từng bóng
                        newBallState[i, j] = new Ball(TempBall[i, j].colorIndex, TempBall[i, j].isBig);
                    }
                }
            }

            // Cập nhật trạng thái cho preGameLogic
            preGameLogic.SetBoard(newBallState);
            preGameLogic.Score = gameLogic.Score;
        }
        private void SaveToGameSaveData()
        {
            GameSaveData.Instance.Reset();
            GameSaveData.Instance.Board = new Ball[gameLogic.getSize(), gameLogic.getSize()];
            GameSaveData.Instance.Score = gameLogic.Score;
            GameSaveData.Instance.SelectedBallCount = GameState.Instance.SelectedBallCount;
            GameSaveData.Instance.Time = inGameUC.GetTime();
            GameSaveData.Instance.GameMode = GameState.Instance.GameMode;
            for (int i = 0; i < gameLogic.getSize(); i++)
            {
                for (int j = 0; j < gameLogic.getSize(); j++)
                {
                    if (gameLogic.board.Balls[i, j] != null)
                        GameSaveData.Instance.Board[i, j] = gameLogic.board.Balls[i, j].Clone();
                }
            }
        }

    }




}
