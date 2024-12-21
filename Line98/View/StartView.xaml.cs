using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
    public partial class StartView : UserControl
    {
        private NewGameView newGameView;
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
            
        }
    }
}
