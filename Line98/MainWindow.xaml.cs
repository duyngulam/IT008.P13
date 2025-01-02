using System.Windows;

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
        }

    }
}