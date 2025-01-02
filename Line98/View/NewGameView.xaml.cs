using Line98.Model;
using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    public partial class NewGameView : UserControl
    {
        public event EventHandler BackRequested;
        public NewGameView()
        {
            InitializeComponent();
        }

        private void btnSmallBack_Click(object sender, RoutedEventArgs e)
        {

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
            GameState.Instance.Reset();
            GameState.Instance.SelectedBallCount = selectedBall;
            GameState.Instance.board = new Board(selectedBall);



        }
    }
}
