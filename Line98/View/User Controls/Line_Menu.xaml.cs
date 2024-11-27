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

namespace Line98.View.User_Controls
{
    /// <summary>
    /// Interaction logic for Line_Menu.xaml
    /// </summary>
    public partial class Line_Menu : UserControl
    {
        public Line_Menu()
        {
            InitializeComponent();
        }
        private void btnStyle_Click(object sender, RoutedEventArgs e)
        {
            cvStyle.Visibility = Visibility.Visible;
            cvHelp.Visibility = Visibility.Collapsed;
            cvScoreNormal.Visibility = Visibility.Collapsed;
            cvScoreTimer.Visibility = Visibility.Collapsed;
            brdHelp.Visibility = Visibility.Collapsed;
            brdScore.Visibility = Visibility.Collapsed;
            brdStyle.Visibility = Visibility.Visible;
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            cvStyle.Visibility = Visibility.Collapsed;
            cvHelp.Visibility = Visibility.Collapsed;
            cvScoreNormal.Visibility = Visibility.Visible;
            cvScoreTimer.Visibility = Visibility.Collapsed;
            brdHelp.Visibility = Visibility.Collapsed;
            brdScore.Visibility = Visibility.Visible;
            brdStyle.Visibility = Visibility.Collapsed;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            cvStyle.Visibility = Visibility.Collapsed;
            cvHelp.Visibility = Visibility.Visible;
            cvScoreNormal.Visibility = Visibility.Collapsed;
            cvScoreTimer.Visibility = Visibility.Collapsed;
            brdHelp.Visibility = Visibility.Visible;
            brdScore.Visibility = Visibility.Collapsed;
            brdStyle.Visibility = Visibility.Collapsed;
        }

        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            cvStyle.Visibility = Visibility.Collapsed;
            cvHelp.Visibility = Visibility.Collapsed;
            cvScoreNormal.Visibility = Visibility.Visible;
            cvScoreTimer.Visibility = Visibility.Collapsed;
            brdHelp.Visibility = Visibility.Collapsed;
            brdScore.Visibility = Visibility.Visible;
            brdStyle.Visibility = Visibility.Collapsed;
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            cvStyle.Visibility = Visibility.Collapsed;
            cvHelp.Visibility = Visibility.Collapsed;
            cvScoreNormal.Visibility = Visibility.Collapsed;
            cvScoreTimer.Visibility = Visibility.Visible;
            brdHelp.Visibility = Visibility.Collapsed;
            brdScore.Visibility = Visibility.Visible;
            brdStyle.Visibility = Visibility.Collapsed;
        }
    }
}
