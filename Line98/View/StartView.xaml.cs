using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            canvasStartMenu.Visibility = Visibility.Collapsed;

            NewGameView newGameView = new NewGameView();
            newGameView.BackRequested += NewGameView_BackRequested;

            if (this.Parent is Panel parentPanel)
            {
                parentPanel.Children.Add(newGameView);
            }
        }
        private void NewGameView_BackRequested(object sender, EventArgs e)
        {
            canvasStartMenu.Visibility = Visibility.Visible;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
