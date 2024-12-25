using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Line98.ViewModel
{
    public class NewGameViewModel : ViewModelBase
    {
        private int ballCount;
        public int BallCount
        {
            get => ballCount;
            set
            {
                ballCount = value;
                OnPropertyChanged(nameof(BallCount));
            }
        }
        public ICommand FourBallCommand { get; set; }
        public ICommand FiveBallCommand { get; set; }
        public ICommand SixBallCommand { get; set; }
        public NewGameViewModel()
        {
            FourBallCommand = new ViewModelCommand(FourBall);
            FiveBallCommand = new ViewModelCommand(FiveBall);
            SixBallCommand = new ViewModelCommand(SixBall);
            BallCount = 5;
        }

        private void SixBall(object obj)
        {
            MessageBox.Show("6");
            BallCount = 6;
        }

        private void FiveBall(object obj)
        {
            BallCount = 5;
        }

        private void FourBall(object obj)
        {
            MessageBox.Show("4");
            BallCount = 4;
        }
    }
}
