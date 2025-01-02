using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuScoreNormal.xaml
    /// </summary>
    public partial class MenuScoreNormal : UserControl
    {
        public MenuScoreNormal()
        {
            InitializeComponent();
        }
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Visible;
            pTimer.Visibility = Visibility.Collapsed;
            borderNormal.Visibility = Visibility.Visible;
            borderTimer.Visibility = Visibility.Collapsed;
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Collapsed;
            pTimer.Visibility = Visibility.Visible;
            borderNormal.Visibility = Visibility.Collapsed;
            borderTimer.Visibility = Visibility.Visible;
        }
    }
}
