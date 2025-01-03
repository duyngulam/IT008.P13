using Line98.Model;
using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    public partial class NewGameView : UserControl
    {
        public NewGameView()
        {
            GameState.Instance.Reset();
            InitializeComponent();
        }

        private void rdBtnNormal_Checked(object sender, RoutedEventArgs e)
        {

            StyleBallManager.Instance.SetMode(true);
        }

        private void btnSmallNext_Click(object sender, RoutedEventArgs e)
        {
            int selectedBall = 5;
            if (rdBtn4.IsChecked == true)
            {
                selectedBall = 4;
            }
            else if (rdBtn5.IsChecked == true)
            {
                selectedBall = 5;
            }
            else if (rdBtn6.IsChecked == true) { selectedBall = 6; }

            if (GameState.Instance.GameMode == true)
            {
                GameState.Instance.Time = 0;
            }
            GameState.Instance.SelectedBallCount = selectedBall;
            GameState.Instance.board = new Board(selectedBall);
        }

        private void rdBtnTimer_Checked(object sender, RoutedEventArgs e)
        {
            GameState.Instance.GameMode = false;
            StyleBallManager.Instance.SetMode(false);
        }
    }
}
