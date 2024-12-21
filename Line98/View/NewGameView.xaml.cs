using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class NewGameView : UserControl
    {
        public event EventHandler BackRequested;
        public NewGameView()
        {
            InitializeComponent();
        }

        private void btnSmallBack_Click(object sender, RoutedEventArgs e)
        {
            canvasNewGame.Visibility = Visibility.Collapsed;
            BackRequested?.Invoke(this, EventArgs.Empty);

        }
    }
}
