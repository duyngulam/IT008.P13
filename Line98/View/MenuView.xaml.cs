using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void btnStyle_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Visible;
            borderScore.Visibility = Visibility.Collapsed;
            borderHelp.Visibility = Visibility.Collapsed;
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Collapsed;
            borderScore.Visibility = Visibility.Visible;
            borderHelp.Visibility = Visibility.Collapsed;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Collapsed;
            borderScore.Visibility = Visibility.Collapsed;
            borderHelp.Visibility = Visibility.Visible;
        }
    }
}
