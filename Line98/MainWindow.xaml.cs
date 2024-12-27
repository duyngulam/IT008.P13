using Line98.Control;
using Line98.Model;
using Line98.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Line98.Control.GameControl;

namespace Line98
{
    public partial class MainWindow : Window
    {
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