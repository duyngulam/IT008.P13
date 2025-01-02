using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        private int scoreSave;
        public GameOver(int score)
        {
            InitializeComponent();
            scoreSave = score;
        }
        
        void newGame()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Đặt cửa sổ mới là MainWindow
            App.Current.MainWindow = mainWindow;
            foreach (Window window in App.Current.Windows)
            {
                if (window != App.Current.MainWindow)
                {
                    window.Close();
                }
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (StyleBallManager.Instance.isCountingup == true)
            {
                ScoreSaveManager.SaveToSlot(1, txtName.Text + "." + scoreSave.ToString() + "\n");
            }
            else
            {
                ScoreSaveManager.SaveToSlot(2, txtName.Text + "." + scoreSave.ToString() + "\n");
            } 
                
            
            newGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            newGame();

        }
    }
}
